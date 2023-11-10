using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Data;
using CoreSavingLibrary;
using System.Globalization;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl
{
    public partial class ws_dlg_share_withdraw : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostCancel { get; set; }
        [JsPostBack]
        public string PostSave { get; set; }
        [JsPostBack]
        public string PostMoneyType { get; set; }
        [JsPostBack]
        public string PostSlipDate { get; set; }
        [JsPostBack]
        public string PostExpenseBank { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostOperateFlagCheck { get; set; }
        [JsPostBack]
        public string PostSetshrarrFlag { get; set; }

        string[] share;
        int currentShare = 0;
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        DateTime idtm_lastDate, idtm_activedate;
        string exc = "";
        string alerts = "";
        string saveResult = "";
        bool isNotError = true;
        string slip_no = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetailLoan.InitDsDetailLoan(this);
            dsDetailEtc.InitDsDetailEtc(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                of_activeworkdate();
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE = idtm_activedate;
                if (idtm_activedate != state.SsWorkDate)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันแล้ว ระบบจะทำการเปลี่ยนวันที่เป็น " + idtm_activedate.ToString("dd/MM/yyyy", th));
                }

                share = Request["share"].Split(',');

                lbCurrentShare.Text = (currentShare + 1) + "/" + share.Length;

                InitShareWithdraw();
                SetshrarrFlag();
                HdIndex.Value = currentShare + "";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostCancel)
            {
                NextShare();
            }
            else if (eventArg == PostSave)
            {
                //เช็คยอดขอกู้ ยอดหักชำระ
                decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;

                if (payoutnet_amt >= 0)
                {
                    Boolean lbl_fin = of_checkfin();

                    if (lbl_fin == false)
                    {
                        isNotError = false;
                        NextShare();
                        return;
                    }
                    else
                    {
                        SaveWithdraw();
                    }
                }
                else
                {
                    NextShare();
                }
            }
            else if (eventArg == PostMoneyType)
            {
                string sliptype_code = "SWD";
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                SetDefaultTofromaccid();
                SetBankBranh(moneytype_code);
                CalBankFee(moneytype_code);

                if (dsMain.DATA[0].MONEYTYPE_CODE == "CSH") {

                    dsMain.DATA[0].TOFROM_ACCID = "11110100";
                }
                else if (dsMain.DATA[0].MONEYTYPE_CODE == "CBT" || dsMain.DATA[0].MONEYTYPE_CODE == "CHQ")
                {
                    dsMain.DATA[0].TOFROM_ACCID = "11130100";
                }
                 else if(dsMain.DATA[0].MONEYTYPE_CODE == "TRN")
                {
                    dsMain.DATA[0].TOFROM_ACCID = "21312000";
                }
                

            }
            else if (eventArg == PostSlipDate)
            {
                idtm_lastDate = dsMain.DATA[0].SLIP_DATE;
                currentShare = Convert.ToInt16(HdIndex.Value);
                share = Request["share"].Split(',');
                InitShareWithdraw();
            }
            else if (eventArg == PostExpenseBank)
            {
                string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                dsMain.DATA[0].EXPENSE_BRANCH = null;
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BRANCHColumn).Enabled = true;
                dsMain.FindTextBox(0, "expense_accid").ReadOnly = false;
                dsMain.DdBranch(expense_bank);
            }
            else if (eventArg == PostInsertRow)
            {
                dsDetailEtc.InsertLastRow();
                int currow = dsDetailEtc.RowCount - 1;
                try
                {
                    dsDetailEtc.DATA[currow].SEQ_NO = dsDetailEtc.GetMaxValueDecimal("SEQ_NO") + 1;
                }
                catch
                {
                    if (dsDetailEtc.RowCount < 1)
                    {
                        dsDetailEtc.DATA[currow].SEQ_NO = 1;
                    }
                }
            }
            else if (eventArg == PostDeleteRow)
            {
                int row = dsDetailEtc.GetRowFocus();
                dsDetailEtc.DeleteRow(row);

            }
            else if (eventArg == PostOperateFlagCheck)
            {
                int row = dsDetailEtc.GetRowFocus();
                decimal operate_flag = dsDetailEtc.DATA[row].OPERATE_FLAG;
                decimal item_payamt = dsDetailEtc.DATA[row].ITEM_PAYAMT;
                string slipitemtype_code = dsDetailEtc.DATA[row].SLIPITEMTYPE_CODE;
                string slipitem_desc = dsDetailEtc.DATA[row].SLIPITEM_DESC;
                if (operate_flag == 1)
                {
                    dsDetailEtc.DdLoanTypeEtc();
                }
                else if (operate_flag == 0)
                {
                    dsDetailEtc.DATA[row].SLIPITEMTYPE_CODE = "";
                    dsDetailEtc.DATA[row].SLIPITEM_DESC = "";
                    dsDetailEtc.DATA[row].ITEM_PAYAMT = 0;
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            for (int i = 0; i < dsDetailEtc.RowCount; i++)
            {
                if (dsDetailEtc.DATA[i].OPERATE_FLAG == 1)
                {
                    dsDetailEtc.FindDropDownList(i, dsDetailEtc.DATA.SLIPITEMTYPE_CODEColumn).Enabled = true;
                    dsDetailEtc.FindTextBox(i, dsDetailEtc.DATA.SLIPITEM_DESCColumn).ReadOnly = false;
                    dsDetailEtc.FindTextBox(i, dsDetailEtc.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                }
                else
                {
                    dsDetailEtc.FindDropDownList(i, dsDetailEtc.DATA.SLIPITEMTYPE_CODEColumn).Enabled = false;
                    dsDetailEtc.FindTextBox(i, dsDetailEtc.DATA.SLIPITEM_DESCColumn).ReadOnly = true;
                    dsDetailEtc.FindTextBox(i, dsDetailEtc.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                }
            }
        }

        public void InitShareWithdraw()
        {
            try
            {
                string shr = share[currentShare];

                string member_no = shr;

                idtm_activedate = dsMain.DATA[0].cp_activedate;
                idtm_lastDate = dsMain.DATA[0].cp_lastdate;

                str_slippayout sSlipPayOut = new str_slippayout();
                sSlipPayOut.member_no = member_no;
                sSlipPayOut.coop_id = state.SsCoopId;
                sSlipPayOut.contcoop_id = state.SsCoopControl;
                sSlipPayOut.memcoop_id = state.SsCoopControl;
                sSlipPayOut.entry_id = state.SsUsername;
                sSlipPayOut.operate_date = dsMain.DATA[0].OPERATE_DATE;
                sSlipPayOut.slip_date = dsMain.DATA[0].SLIP_DATE;
                sSlipPayOut.initfrom_type = "SWD";


                int result = wcf.NShrlon.of_initshrwtd(state.SsWsPass, ref sSlipPayOut);
                if (result == 1)
                {
                    dsMain.ResetRow();
                    dsMain.ImportData(sSlipPayOut.xml_sliphead);
                    dsDetailLoan.ImportData(sSlipPayOut.xml_slipcutlon);
                    dsDetailEtc.ImportData(sSlipPayOut.xml_slipcutetc);
                    dsMain.DdBankDesc();
                    string sliptype_code = "SWD";
                    string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

                    string sql = @" 
                    select sharetype_code + ' - ' + sharetype_desc as sharetype from shsharetype where coop_id = {0} and sharetype_code = {1}
                    ";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMain.DATA[0].SHRLONTYPE_CODE);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        string sharetype = dt.GetString("sharetype");
                        dsMain.DATA[0].CP_SHARETYPE = sharetype;
                    }
                    sql = "select seq_no from mbgainmaster where coop_id = {0} and member_no = {1}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO);
                    dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMain.DATA[0].cp_gainflag = 1;
                    }

                    decimal sharestatus = dsMain.DATA[0].BFSHARE_STATUS;

                    dsMain.DATA[0].cp_activedate = idtm_activedate;
                    dsMain.DATA[0].cp_lastdate = idtm_lastDate;

                    //tomy เช็คค่า ว่าได้ค่าอะไร ไม่ได้ใช้
                    //sharestatus = dsDetailLoan.DATA[0].BFINTRETURN_AMT;
                    //sharestatus = dsDetailLoan.DATA[0].BFINTARR_AMT;
                    //sharestatus = dsDetailLoan.DATA[0].INTARREAR_PAYAMT;
                    

                    HfFormType.Value = sSlipPayOut.initfrom_type;
                    dsMain.DdFromAccId(sliptype_code, moneytype_code);
                    dsMain.DdMoneyType();
                    SetDefaultTofromaccid();
                    SetBankBranh(moneytype_code);
                    CalBankFee(moneytype_code);

                    for (int i = 0; i < dsDetailLoan.RowCount; i++)
                    {
                        dsDetailLoan.FindCheckBox(i, dsDetailLoan.DATA.OPERATE_FLAGColumn).Enabled = true;
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void NextShare()
        {
            currentShare = Convert.ToInt16(HdIndex.Value);
            share = Request["share"].Split(',');

            currentShare += 1;
            if (currentShare < share.Length)
            {
                lbCurrentShare.Text = (currentShare + 1) + "/" + share.Length;
                InitShareWithdraw();
            }
            HdIndex.Value = currentShare + "";
            if ((currentShare) >= share.Length)
            {
                if (isNotError)
                {

                       // string document_no = UpdateSlippayout2(member_no);
                       // this.SetOnLoadedScript(" parent.PrintSlipoutGsb(\"" + member_no + "\",\"" + document_no + "\"); parent.RemoveIFrame();");
                        this.SetOnLoadedScript(" parent.GetShowData(\"" + slip_no + "\"); parent.RemoveIFrame();");
                }
                else
                {
                    dsMain.ResetRow();
                    dsDetailLoan.ResetRow();
                    dsDetailEtc.ResetRow();
                }
            }
        }

        private void SaveWithdraw()
        {
           
            //เขียนคำสั่ง save ในนี้
            String member_no = dsMain.DATA[0].MEMBER_NO;

            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.contcoop_id = state.SsCoopControl;
            strPayOut.memcoop_id = state.SsCoopControl;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = state.SsWorkDate;
            strPayOut.member_no = member_no;
            strPayOut.slip_date = dsMain.DATA[0].SLIP_DATE;
            strPayOut.initfrom_type = HfFormType.Value;

            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = dsMain.ExportXml();
            dwLoanXML = dsDetailLoan.ExportXml();
            dwEtcXML = dsDetailEtc.ExportXml();

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;

            try
            {
                int result = wcf.NShrlon.of_saveslip_shrwtd(state.SsWsPass, ref strPayOut);
                

                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    saveResult = "alert('บันทึกข้อมูลสำเร็จ') \n ";
                }
                string payinslip_no = strPayOut.payinslip_no;
                string payoutslip_no = strPayOut.payoutslip_no;
                if ((payinslip_no != "" && payinslip_no != null) || (payoutslip_no != "" && payoutslip_no != null))
                {
                    if (state.SsCoopControl == "008001")
                        {
                       // Printing.PrintSlippayinPEA(this, payinslip_no, state.SsCoopControl);
                    }
                    else if (state.SsCoopControl == "013001") {
                        slip_no = payinslip_no;
                        
                    }
                    else if (state.SsCoopControl == "022001" || state.SsCoopControl == "020001")
                    {
                        slip_no = payoutslip_no;
                    }
                    else if (state.SsCoopControl == "006001")
                    {
                        if (payinslip_no != "" && payinslip_no != null)
                        {
                           // Printing.ShrlonPrintSlipPayIn(this, state.SsCoopControl, payinslip_no, 2); 
                        }
                    }
                    else if (state.SsCoopControl == "040001" )
                    {



                        this.SetOnLoadedScript("parent.PrintSlipout(\"" + payinslip_no + "\",\"" + payoutslip_no + "\"); parent.RemoveIFrame();");
                         

                            //Printing.PrintSlipSlpayin_sqlserver(this, payinslip_no, state.SsCoopControl, bahtTH_sum);
                    }
                    else
                    {
                       // Printing.PrintSlipSlpayin(this, payinslip_no, state.SsCoopControl); ปิดไว้ก่อนเพราะไปเรียกที่ core
                        //PrintPaymentVoucher();
                    }
                }
            }
            catch (Exception ex)
            {
                isNotError = false;                            
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

           /* if (isNotError)
            {
                NextShare();
            }*/
        }

        private void SetshrarrFlag()
        {
            //หุ้นค้างจ่าย
            if (dsMain.DATA[0].PAYOUTNET_AMT < 0)
            {
                dsMain.FindCheckBox(0, dsMain.DATA.SETSHRARR_FLAGColumn).Enabled = false;
                dsMain.FindDropDownList(0, dsMain.DATA.MONEYTYPE_CODEColumn).Enabled = false;
                dsMain.FindDropDownList(0, dsMain.DATA.TOFROM_ACCIDColumn).Enabled = false;
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = false;
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BRANCHColumn).Enabled = false;
                dsMain.FindTextBox(0, dsMain.DATA.EXPENSE_ACCIDColumn).Enabled = false;
                dsMain.FindTextBox(0, dsMain.DATA.BANKSRV_AMTColumn).Enabled = false;
                dsMain.FindTextBox(0, dsMain.DATA.BANKFEE_AMTColumn).Enabled = false;
            }
        }

        /// <summary>
        /// set คู่บัญชี
        /// </summary>
        private void SetDefaultTofromaccid()
        {
            dsMain.DATA[0].TOFROM_ACCID = "";
            try
            {
                string sliptype_code = "SWD";
                string sql = @"select 
	            account_id
            from 
	            cmucftofromaccid where
	            coop_id={0} and 
	            moneytype_code={1} and
	            sliptype_code={2} and
	            default_flag=1";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMain.DATA[0].MONEYTYPE_CODE, sliptype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    string accid = dt.GetString("account_id");
                    dsMain.DATA[0].TOFROM_ACCID = accid;
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// ดึงสาขาธนาคาร
        /// </summary>        
        public void SetBankBranh(String moneytype_code)
        {
            try
            {
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = true;
                if (moneytype_code == "CBT" || moneytype_code == "CHQ")
                {
                    string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                    string expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                    dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BRANCHColumn).Enabled = true;
                    dsMain.FindTextBox(0, dsMain.DATA.EXPENSE_ACCIDColumn).Enabled = true;
                    dsMain.FindTextBox(0, dsMain.DATA.BANKSRV_AMTColumn).Enabled = true;
                    dsMain.FindTextBox(0, dsMain.DATA.BANKFEE_AMTColumn).Enabled = true;

                    if (expense_branch == "")
                    {
                        string sql1 = @"select expense_bank, expense_branch, expense_accid from mbmembmaster where coop_id = {0} and member_no = {1}";
                        sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO);
                        Sdt dt1 = WebUtil.QuerySdt(sql1);
                        if (dt1.Next())
                        {
                            expense_bank = dt1.GetString("expense_bank");
                            dsMain.DdBranch(expense_bank);
                            expense_branch = dt1.GetString("expense_branch");
                            string expense_accid = dt1.GetString("expense_accid");
                            dsMain.DATA[0].EXPENSE_BANK = expense_bank;
                            dsMain.DATA[0].EXPENSE_BRANCH = expense_branch;
                            dsMain.DATA[0].EXPENSE_ACCID = expense_accid;
                            SetDefaultTofromaccid();
                        }
                    }
                    else
                    {
                        dsMain.DdBranch(expense_bank);
                    }
                }
                else //if (moneytype_code == "CSH")
                {
                    dsMain.DATA[0].EXPENSE_BANK = "";
                    dsMain.DATA[0].EXPENSE_BRANCH = "";
                    dsMain.DATA[0].EXPENSE_ACCID = "";
                    dsMain.DATA[0].BANKSRV_AMT = 0;
                    dsMain.DATA[0].BANKFEE_AMT = 0;
                    dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = false;
                    dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BRANCHColumn).Enabled = false;
                    dsMain.FindTextBox(0, dsMain.DATA.BANKSRV_AMTColumn).Enabled = false;
                    dsMain.FindTextBox(0, dsMain.DATA.BANKFEE_AMTColumn).Enabled = false;
                    dsMain.FindTextBox(0, dsMain.DATA.EXPENSE_ACCIDColumn).Enabled = false;
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// คิดค่าธรรมเนียม ค่าบริการ
        /// </summary>        
        public void CalBankFee(String moneytype_code)
        {
            string bankbranch_code = dsMain.DATA[0].EXPENSE_BRANCH;
            string bank_code =  dsMain.DATA[0].EXPENSE_BANK ;
            string sql = "";
           
            try
            {
                if (state.SsCoopId == "011001")
                {
                     sql = "select fee_status from CMUCFBANKBRANCH where branch_id = '" + bankbranch_code + "'and bank_code = '" + bank_code + "'";
                }
                else
                {
                     sql = "select fee_status from CMUCFBANKBRANCH where branch_id = '" + bankbranch_code + "'";
                }
                sql = WebUtil.SQLFormat(sql, bankbranch_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    String fee_status = dt.GetString("fee_status");
                    double payoutnet_amt = Convert.ToDouble(dsMain.DATA[0].PAYOUTNET_AMT);
                    if (fee_status == "1" && (moneytype_code == "CBT"))// || moneytype_code == "CBO"
                    {
                        payoutnet_amt = payoutnet_amt * 0.001;

                        if (payoutnet_amt % 1 != 0)
                        {
                            payoutnet_amt = payoutnet_amt - (payoutnet_amt % 1) + 1;
                        }

                        if (payoutnet_amt < 10)
                        { payoutnet_amt = 10; }
                        else if (payoutnet_amt > 1000)
                        { payoutnet_amt = 1000; }

                        decimal payoutnet = Convert.ToDecimal(payoutnet_amt);
                        dsMain.DATA[0].BANKFEE_AMT = payoutnet;

                        dsMain.DATA[0].BANKSRV_AMT = 20;
                    }
                    else { dsMain.DATA[0].BANKFEE_AMT = 0; dsMain.DATA[0].BANKSRV_AMT = 0; }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// get วันทำการ
        /// </summary>       
        public void of_activeworkdate()
        {
            try
            {
                string sqlStr;
                int li_clsdaystatus = 0;
                DateTime ldtm_workdate;
                Sdt dt;
                sqlStr = @" select workdate, closeday_status
                    from amappstatus 
                    where coop_id = '" + state.SsCoopId + @"'
                    and application = 'mbshr'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    ldtm_workdate = dt.GetDate("workdate");
                    li_clsdaystatus = dt.GetInt32("closeday_status");
                    if (li_clsdaystatus == 1)
                    {
                        int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref idtm_activedate);
                        dsMain.DATA[0].cp_lastdate = idtm_activedate;
                        dsMain.DATA[0].cp_activedate = idtm_activedate;
                    }
                    else
                    {
                        dsMain.DATA[0].cp_activedate = state.SsWorkDate;
                        dsMain.DATA[0].cp_lastdate = state.SsWorkDate;
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// ตรวจสอบการเงิน
        /// </summary>
        private Boolean of_checkfin()
        {
            string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
            string sqlStr = "";
            decimal allpay_atfin = 0;
            Sdt dt;
            if (moneytype_code == "CSH")
            {
                //เช็คว่าวันจ่ายเงินกู้เป็นวันเดียวกันกับวันทำการหรือไม่
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                if (state.SsWorkDate != idtm_activedate)
                {
                    this.SetOnLoadedScript("alert('ประเภทการถอนหุ้นเป็นเงินสด ไม่สามารถถอนหุ้นล่วงหน้าได้ กรุณาตรวจสอบ') \n alert('ดึงข้อมูลรายการต่อไป')");
                    return false;
                }

                //เช็คว่าต้องการตรวจสอบการเงินหรือไม่
                sqlStr = @"select allpay_atfin from finconstant where coop_id = '" + state.SsCoopId + "'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    allpay_atfin = dt.GetInt32("allpay_atfin");
                }

                if (allpay_atfin == 1)
                {
                    //เช็คลิ้นชักการเงิน
                    try
                    {
                        sqlStr = @"select status from fintableusermaster where user_name = {0} and opdatework = {1}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsUsername, state.SsWorkDate);
                        dt = WebUtil.QuerySdt(sqlStr);
                        if (dt.Next())
                        {
                            int status = dt.GetInt32("status");
                            if (status == 14)
                            {
                                this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้เนื่องจากมีการปิดลิ้นชักไปแล้วของ " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            this.SetOnLoadedScript("alert('ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                            return false;
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }
            return true;
        }
    }
}