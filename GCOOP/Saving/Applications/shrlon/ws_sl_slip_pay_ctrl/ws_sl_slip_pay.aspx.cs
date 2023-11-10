using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
//using CoreSavingLibrary.WcfReport;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl
{
    public partial class ws_sl_slip_pay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostOperateFlag { get; set; }
        [JsPostBack]
        public string PostOperateFlagL { get; set; }
        [JsPostBack]
        public string PostOperateFlagE { get; set; }
        [JsPostBack]
        public string PostAccidFlag { get; set; }
        [JsPostBack]
        public string PostSlipItem { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostOperateDate { get; set; }
        [JsPostBack]
        public string PostMoneytype { get; set; }
        [JsPostBack]
        public string PostSearchRetrieve { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostSliptypecode { get; set; }
        [JsPostBack]
        public string PostPayspecMethod { get; set; }
        [JsPostBack]
        public string PostRePayspecMethod { get; set; }
        [JsPostBack]
        public string PostChkLoanPayment { get; set; }
        [JsPostBack]
        public string PostChkitemamt { get; set; }
        

        DateTime idtm_lastDate;
        DateTime idtm_activedate = new DateTime();
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetailShare.InitDsDetailShare(this);
            dsDetailLoan.InitDsDetailLoan(this);
            dsDetailEtc.InitDsDetailEtc(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                of_activeworkdate();
                dsMain.DdSliptype();
                dsMain.DdTofromAccBlank();//ทดสอบ Dd ช่องว่าง
                dsMain.DdMoneyType();
                dsDetailEtc.DdLoanType();
                dsMain.DATA[0].SLIPTYPE_CODE = "PX";
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                add_row.Visible = false;
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
            //dsMain.DATA[0].SLIPTYPE_CODE = "PX";
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                add_row.Visible = true;

                this.InitLnRcv();
            }
            else if (eventArg == PostMoneytype)
            {
                string sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsMain.DATA[0].TOFROM_ACCID = "";
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                SetDefaultTofromaccid();
                if (moneytype_code != "TRN")
                {
                    dsMain.DATA[0].REF_SLIPAMT = 0;
                    dsMain.DATA[0].REF_SLIPNO = "";
                    dsMain.DATA[0].REF_SYSTEM = "";
                    dsMain.DATA[0].EXPENSE_ACCID = "";
                    dsMain.DATA[0].cp_refslip = "";
                }
            }  
            else if (eventArg == PostChkitemamt) { /////เชคยอดชิ้อหุ้นเกิน 50000 /เดือนให้ขึ้น alart   by bale
                decimal itempayamt = 0, itemnowamt = 0, itempayamt_alert = 0;
                string sql1 = @" select  sum( slslippayindet.item_payamt) as item_payamt,slslippayindet.stm_itemtype from slslippayin,   
                                        slslippayindet  
                                        WHERE   slslippayindet.payinslip_no = slslippayin.payinslip_no  and  
                                        slslippayindet.slipitemtype_code = 'SHR' and 
						                slslippayindet.stm_itemtype  ='SPX' and 
                                        slslippayin.member_no = {0} and 
                                        slslippayin.coop_id = {1} and 
 						                (slslippayin.slip_date  between  DATEADD(MONTH, DATEDIFF(MONTH,0, {2} ),0) and DATEADD(d,-1, DATEADD(MONTH,DATEDIFF(MONTH,0, {2} )+1,0)) ) 						  
					 	                group by stm_itemtype";
                sql1 = WebUtil.SQLFormat(sql1, dsMain.DATA[0].MEMBER_NO, state.SsCoopControl, dsMain.DATA[0].OPERATE_DATE);
                Sdt dt1 = WebUtil.QuerySdt(sql1);
                if (dt1.Next())
                {
                    itempayamt = dt1.GetDecimal("item_payamt");
                }
                itemnowamt = dsDetailShare.DATA[0].ITEM_PAYAMT;
                itempayamt = itemnowamt + itempayamt;
                itempayamt_alert = itempayamt - itemnowamt;
                if (itempayamt > 50000)
                {
                    Response.Write("<script> alert('สมาชิกนี้ได้ชื้อหุ้นไปเเล้วรวม " + itempayamt_alert + " บาทแล้ว กรุณาตรวจสอบ'); </script>");
                }            
            }
            else if (eventArg == PostOperateFlag)
            {
                int row = dsDetailShare.GetRowFocus();
                decimal operate_flag = dsDetailShare.DATA[row].OPERATE_FLAG;
                decimal bfshrcont_balamt = dsDetailShare.DATA[row].BFSHRCONT_BALAMT;
                decimal item_balance = dsDetailShare.DATA[row].ITEM_BALANCE;
                decimal bfperiod = dsDetailShare.DATA[row].BFPERIOD;

                if (operate_flag == 1)
                {
                    decimal maxshare_hold = 0;
                    if (state.SsCoopControl == "022001") //สอ.การทางให้เช็คจำกัดการถือหุ้นตาม shsharetype
                    {
                        string sharetype = dsDetailShare.DATA[row].SHRLONTYPE_CODE;
                        string sql = "select maxshare_hold from shsharetype where coop_id = {0} and sharetype_code = {1}";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopControl, sharetype);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            maxshare_hold = dt.GetDecimal("maxshare_hold");
                            HdMaxshareHold.Value = Convert.ToString(maxshare_hold);
                        }
                        if (bfshrcont_balamt >= maxshare_hold)
                        {
                            this.SetOnLoadedScript("alert('เนื่องจากหุ้นที่ถือมีจำนวนตามเกณฑ์ สูงสุดแล้ว กรุณาตรวจสอบ')");
                            return;
                        }
                    }
                    /////เชคยอดชิ้อหุ้นเกิน 50000 /เดือนให้ขึ้น alart   by bale
                    decimal itempayamt = 0, itemnowamt = 0;
                    string sql1 = @" select  sum( slslippayindet.item_payamt) as item_payamt,slslippayindet.stm_itemtype from slslippayin,   
                                        slslippayindet  
                                        WHERE   slslippayindet.payinslip_no = slslippayin.payinslip_no  and  
                                        slslippayindet.slipitemtype_code = 'SHR' and 
						                slslippayindet.stm_itemtype  ='SPX' and 
                                        slslippayin.member_no = {0} and 
                                        slslippayin.coop_id = {1} and 
 						                (slslippayin.slip_date between  DATEADD(MONTH, DATEDIFF(MONTH,0, {2} ),0) and DATEADD(d,-1, DATEADD(MONTH,DATEDIFF(MONTH,0, {2} )+1,0)) ) 						  
					 	                group by stm_itemtype";
                    sql1 = WebUtil.SQLFormat(sql1, dsMain.DATA[0].MEMBER_NO, state.SsCoopControl, dsMain.DATA[0].OPERATE_DATE);
                    Sdt dt1 = WebUtil.QuerySdt(sql1);
                    if (dt1.Next())
                    {
                        itempayamt = dt1.GetDecimal("item_payamt");
                    }
                    itemnowamt = dsDetailShare.DATA[0].ITEM_PAYAMT;
                    itempayamt = itemnowamt + itempayamt;
                    if (itempayamt > 50000)
                    {                                              
                        Response.Write("<script> alert('สมาชิกนี้ได้ชื้อหุ้นไปเเล้ว " + itempayamt + " บาท กรุณาตรวจสอบ'); </script>");
                    }

                    dsDetailShare.DATA[row].BFSHRCONT_BALAMT = bfshrcont_balamt;
                    if (bfshrcont_balamt == 0 && bfperiod == 0)
                    {
                        dsDetailShare.DATA[row].PERIOD = 1;
                        dsDetailShare.DATA[row].PERIODCOUNT_FLAG = 1;
                        dsDetailShare.DATA[row].ITEM_PAYAMT = dsDetailShare.DATA[row].BFPERIOD_PAYMENT;
                    }
                    dsDetailShare.DATA[row].ITEM_BALANCE = item_balance;
                    //dsDetailShare.DATA[row].PERIOD
                    calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsDetailShare.DATA[row].PERIOD = bfperiod;
                    dsDetailShare.DATA[row].PERIODCOUNT_FLAG = 0;
                    dsDetailShare.DATA[row].PRINCIPAL_PAYAMT = 0;
                    dsDetailShare.DATA[row].INTEREST_PAYAMT = 0;
                    dsDetailShare.DATA[row].ITEM_PAYAMT = 0;
                    calItemPay();
                }
            }
            else if (eventArg == PostOperateFlagL)
            {
                int rowl = dsDetailLoan.GetRowFocus();
                decimal operate_flag_l = dsDetailLoan.DATA[rowl].OPERATE_FLAG;
                decimal bfshrcont_balamt_l = dsDetailLoan.DATA[rowl].BFSHRCONT_BALAMT;
                decimal principal_payamt = dsDetailLoan.DATA[rowl].PRINCIPAL_PAYAMT;
                decimal rkeep_principal = dsDetailLoan.DATA[rowl].RKEEP_PRINCIPAL;

                if (operate_flag_l == 1)
                {
                    //เช็คว่ามีเรียกเก็บอยู่หรือไม่
                    if (dsDetailLoan.DATA[rowl].BFPXAFTERMTHKEEP_TYPE == 1 && dsDetailLoan.DATA[rowl].BFLASTPROC_DATE >= dsMain.DATA[0].OPERATE_DATE)
                    {
                        principal_payamt = bfshrcont_balamt_l - rkeep_principal;
                    }
                    else
                    {
                        principal_payamt = bfshrcont_balamt_l;
                    }
                    dsDetailLoan.DATA[rowl].PRINCIPAL_PAYAMT = principal_payamt;
                    dsDetailLoan.DATA[rowl].INTEREST_PAYAMT = dsDetailLoan.DATA[rowl].CP_INTERESTPAY; //นำ comment ออกก่อน เนื่องจากกรณีที่ สัญญาที่ไม่เรียกเก็บกรณีมาชำระพิเศษแล้วดอกเบี้ยที่ต้องชำระไม่ set ค่าให้ by.cherry 
                    dsDetailLoan.DATA[rowl].ITEM_PAYAMT = dsDetailLoan.DATA[rowl].PRINCIPAL_PAYAMT + dsDetailLoan.DATA[rowl].INTEREST_PAYAMT;
                    dsDetailLoan.DATA[rowl].ITEM_BALANCE = bfshrcont_balamt_l - dsDetailLoan.DATA[rowl].PRINCIPAL_PAYAMT;

                }
                else
                {

                    dsDetailLoan.DATA[rowl].PRINCIPAL_PAYAMT = 0;
                    dsDetailLoan.DATA[rowl].INTEREST_PAYAMT = 0;
                    dsDetailLoan.DATA[rowl].ITEM_PAYAMT = 0;
                    dsDetailLoan.DATA[rowl].ITEM_BALANCE = bfshrcont_balamt_l;

                }

                if (dsDetailLoan.DATA[rowl].BFPAYSPEC_METHOD == 2)
                {
                    ReCalint();
                }

                calItemPay();
            }
            else if (eventArg == PostOperateFlagE)
            {
                int rowe = dsDetailEtc.GetRowFocus();
                decimal operate_flag_e = dsDetailEtc.DATA[rowe].OPERATE_FLAG;
                decimal ldc_bfshrcontbal = dsDetailEtc.DATA[rowe].BFSHRCONT_BALAMT;

                if (operate_flag_e == 1)
                {

                    dsDetailEtc.DATA[rowe].ITEM_PAYAMT = ldc_bfshrcontbal;

                    calItemPay();
                }
                else if (operate_flag_e == 0)
                {
                    dsDetailEtc.DATA[rowe].ITEM_PAYAMT = 0;
                    calItemPay();
                }
            }
            else if (eventArg == PostSlipItem)
            {
                int row = dsDetailEtc.GetRowFocus();
                string slipitemtype_code = dsDetailEtc.DATA[row].SLIPITEMTYPE_CODE;
                //dsAdd.ItemType(slipitemtype_code);
                string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC
                 FROM SLUCFSLIPITEMTYPE  
                 WHERE SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE = {0}";
                sql = WebUtil.SQLFormat(sql, slipitemtype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsDetailEtc.DATA[row].SLIPITEM_DESC = dt.GetString("SLIPITEMTYPE_DESC");
                }
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
                dsDetailEtc.DdLoanType();
            }
            else if (eventArg == PostDeleteRow)
            {
                int row = dsDetailEtc.GetRowFocus();
                dsDetailEtc.DeleteRow(row);
            }
            else if (eventArg == PostOperateDate)
            {
                ReCalint();
            }
            else if (eventArg == PostSearchRetrieve)
            {
                string payinslip_no = HdPayNo.Value;

                HdPayNo.Value = "";

                dsMain.RetrieveMain(payinslip_no);
                dsDetailLoan.RetrieveDetailLoan(payinslip_no);
                dsDetailShare.RetrieveDetailLoan(payinslip_no);
                dsDetailEtc.RetrieveDetailEtc(payinslip_no);

                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                string sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;

                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                dsMain.DdMoneyType();
                SetDefaultTofromaccid();
            }
            else if (eventArg == PostPrint)
            {
                string rslip = ""; decimal slip_amt = 0;

                rslip = dsMain.DATA[0].PAYINSLIP_NO;
                slip_amt = dsMain.DATA[0].SLIP_AMT;
                string sqlprint = "select printslip_type, ireport_obj from lnloanconstant ";
                Sdt dtp = WebUtil.QuerySdt(sqlprint);
                string printtype = "PB", ireportobj = "r_sl_slip_in_exat_a4_table";
                //alter table lnloanconstant add printslip_type varchar2(2);
                //alter table lnloanconstant add ireport_obj varchar2(50);
                if (dtp.Next())
                {
                    printtype = dtp.GetString("printslip_type");
                    ireportobj = dtp.GetString("ireport_obj");

                }
                else
                {
                    printtype = "PB";
                    ireportobj = "r_sl_slip_in_exat_a4_table";
                }
                if (rslip != "")
                {

                    string bahtTxt, n, bahtTH_sum = "";
                    double amount;
                    try { amount = Convert.ToDouble(slip_amt); }
                    catch { amount = 0; }
                    bahtTxt = amount.ToString("####.00");
                    string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                    string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                    string[] temp = bahtTxt.Split('.');
                    string intVal = temp[0];
                    string decVal = temp[1];
                    if (Convert.ToDouble(bahtTxt) == 0)
                        bahtTH_sum = "ศูนย์บาทถ้วน";
                    else
                    {
                        for (int j = 0; j < intVal.Length; j++)
                        {
                            n = intVal.Substring(j, 1);
                            if (n != "0")
                            {
                                if ((j == (intVal.Length - 1)) && (n == "1"))
                                    bahtTH_sum += "เอ็ด";
                                else if ((j == (intVal.Length - 2)) && (n == "2"))
                                    bahtTH_sum += "ยี่";
                                else if ((j == (intVal.Length - 2)) && (n == "1"))
                                    bahtTH_sum += "";
                                else
                                    bahtTH_sum += num[Convert.ToInt32(n)];
                                bahtTH_sum += rank[(intVal.Length - j) - 1];
                            }
                        }
                        bahtTH_sum += "บาท";
                        if (decVal == "00")
                            bahtTH_sum += "ถ้วน";
                        else
                        {
                            for (int j = 0; j < decVal.Length; j++)
                            {
                                n = decVal.Substring(j, 1);
                                if (n != "0")
                                {
                                    if ((j == decVal.Length - 1) && (n == "1"))
                                        bahtTH_sum += "เอ็ด";
                                    else if ((j == (decVal.Length - 2)) && (n == "2"))
                                        bahtTH_sum += "ยี่";
                                    else if ((j == (decVal.Length - 2)) && (n == "1"))
                                        bahtTH_sum += "";
                                    else
                                        bahtTH_sum += num[Convert.ToInt32(n)];
                                    bahtTH_sum += rank[(decVal.Length - j) - 1];
                                }
                            }
                            bahtTH_sum += "สตางค์";
                        }
                    }

                    if (printtype == "IR")
                    {
                        Printing.PrintSlipSlInOutIreportExat(this, rslip, state.SsCoopControl, ireportobj);
                    }
                    else if (printtype == "JS")
                    {
                        //Printing.ShrlonPrintSlipPayIn(this, state.SsCoopControl, rslip, 2);

                        //Printing.PrintSlipSlpayin_sqlserver(this, rslip, state.SsCoopControl, bahtTH_sum);
                    }
                    else
                    {
                        Printing.PrintSlipSlpayin(this, rslip, state.SsCoopControl);
                    }

                }
            }
            else if (eventArg == PostSliptypecode)
            {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                if (member_no != "")
                {
                    add_row.Visible = true;
                    this.InitLnRcv();
                }
            }
            else if (eventArg == PostPayspecMethod)
            {
                ReCalint();
                int r = dsDetailLoan.GetRowFocus();
                SetOnLoadedScript("ReCalculate(" + r + ")");
            }
            else if (eventArg == PostRePayspecMethod)
            {
                int row = dsDetailLoan.GetRowFocus();
                DateTime calintfrom = dsDetailLoan.DATA[row].BFLASTCALINT_DATE;
                string contno = dsDetailLoan.DATA[row].LOANCONTRACT_NO;
                decimal item_payamt = dsDetailLoan.DATA[row].ITEM_PAYAMT;
                DateTime calintto = dsMain.DATA[0].OPERATE_DATE;
                decimal prnbal_payamt = 0;
                decimal interest_payamt = 0;
                decimal intarrear = 0;
                try
                {
                    Int16 xml_re = wcf.NShrlon.of_calrevertpaymeth2(state.SsWsPass, contno, calintfrom, calintto, item_payamt, ref prnbal_payamt, ref interest_payamt);
                    if (xml_re == 1)
                    {

                        dsDetailLoan.DATA[row].ITEM_PAYAMT = item_payamt;
                        intarrear = dsDetailLoan.DATA[row].BFINTARR_AMT;
                        decimal sum = prnbal_payamt + interest_payamt;
                        decimal total = sum - item_payamt;
                        prnbal_payamt = prnbal_payamt - total;
                        dsDetailLoan.DATA[row].PRINCIPAL_PAYAMT = prnbal_payamt - intarrear;
                        dsDetailLoan.DATA[row].INTEREST_PAYAMT = interest_payamt + intarrear;

                        //mike 
                        dsDetailLoan.DATA[row].INTEREST_PERIOD = interest_payamt;
                        calItemPay();
                        dsDetailLoan.DATA[row].ITEM_BALANCE = dsDetailLoan.DATA[row].BFSHRCONT_BALAMT - dsDetailLoan.DATA[row].PRINCIPAL_PAYAMT;
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == PostChkLoanPayment)
            {
                string sql = "select loanpayment_type from lncontmaster where coop_id = {0} and loancontract_no = {1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, HdLoancontract.Value);
                Sdt dt = WebUtil.QuerySdt(sql);
                dt.Next();
                int r = Convert.ToInt32(HdRow.Value);
                decimal bfshrcont_balamt = dsDetailLoan.DATA[r].BFSHRCONT_BALAMT;
                decimal bfperiod_payment = dsDetailLoan.DATA[r].BFPERIOD_PAYMENT;
                decimal cp_interestpay = dsDetailLoan.DATA[r].CP_INTERESTPAY;
                decimal dec_sum = 0;

                if (dt.GetString("loanpayment_type") == "2")
                {//คงยอด
                    dsDetailLoan.DATA[r].PRINCIPAL_PAYAMT = bfperiod_payment - cp_interestpay;
                    dsDetailLoan.DATA[r].INTEREST_PAYAMT = cp_interestpay;
                    dsDetailLoan.DATA[r].ITEM_PAYAMT = bfperiod_payment;
                    dsDetailLoan.DATA[r].ITEM_BALANCE = bfshrcont_balamt - (bfperiod_payment - cp_interestpay);
                }
                else
                {//คงต้น
                    dsDetailLoan.DATA[r].PRINCIPAL_PAYAMT = bfperiod_payment;
                    dsDetailLoan.DATA[r].INTEREST_PAYAMT = cp_interestpay;
                    dsDetailLoan.DATA[r].ITEM_PAYAMT = bfperiod_payment + cp_interestpay;
                    dsDetailLoan.DATA[r].ITEM_BALANCE = bfshrcont_balamt - bfperiod_payment;
                }

                if (dsDetailShare.DATA[0].PERIODCOUNT_FLAG == 1)
                {
                    dec_sum = dec_sum + dsDetailShare.DATA[0].ITEM_PAYAMT;
                }

                for (var ii = 0; ii < dsDetailLoan.RowCount; ii++)
                {
                    if (dsDetailLoan.DATA[ii].OPERATE_FLAG == 1)
                    { //periodcount_flag
                        dec_sum = dec_sum + dsDetailLoan.DATA[ii].ITEM_PAYAMT;
                    }
                }
                if (dec_sum > 0)
                {
                    dsMain.DATA[0].SLIP_AMT = dec_sum;
                }
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void ReCalint()
        {
            DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
            string xml_sliplon = dsDetailLoan.ExportXml();
            string sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
            try
            {
                Int16 xml_re = wcf.NShrlon.of_initslippayin_calint(state.SsWsPass, ref xml_sliplon, sliptype_code, operate_date);
                if (xml_re == 1)
                {
                    dsDetailLoan.ResetRow();
                    dsDetailLoan.ImportData(xml_sliplon);
                }
                AbsIntpay();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_slippayin strslip = new str_slippayin();
                strslip.coop_id = state.SsCoopControl;
                strslip.entry_id = state.SsUsername;
                strslip.xml_sliphead = dsMain.ExportXml();
                strslip.xml_slipshr = dsDetailShare.ExportXml();
                strslip.xml_sliplon = dsDetailLoan.ExportXml();
                strslip.xml_slipetc = dsDetailEtc.ExportXml();

                idtm_lastDate = dsMain.DATA[0].SLIP_DATE;
                // strslip.xml_sliphead = dsMain.ExportXmlPBFormat("d_sl_payinslip");

                wcf.NShrlon.of_saveslip_payin(state.SsWsPass, ref strslip);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ ");

                decimal print = dsMain.DATA[0].PRINT;

               string sqlprint = "select printslip_type, ireport_obj from lnloanconstant ";
                Sdt dtp = WebUtil.QuerySdt(sqlprint);
                string printtype = "PB", ireportobj = "r_sl_slip_in_exat_a4_table";
                //alter table lnloanconstant add printslip_type varchar2(2);
                //alter table lnloanconstant add ireport_obj varchar2(50);
                if (dtp.Next())
                {
                    printtype = dtp.GetString("printslip_type");
                    ireportobj = dtp.GetString("ireport_obj");

                }
                else
                {
                    printtype = "PB";
                    ireportobj = "r_sl_slip_in_exat_a4_table";
                }
                if (print == 1)
                {

                    if (printtype == "IR")
                    {
                        //พิมพ์แบบireport

                        String PayinslipNo = strslip.payinslip_no.Trim();
                        if (state.SsCoopControl == "020001")
                        {

                            Printing.PrintIRSlippayInPBN(this, PayinslipNo, ireportobj);
                        }
                        else if (state.SsCoopControl == "022001")
                        {
                            Printing.RePrintSlipSlInIrExat(this, PayinslipNo, state.SsCoopControl);
                        }
                        else if (state.SsCoopControl == "053001")
                        {
                            Printing.RePrintSlipSlInIrUTP(this, PayinslipNo, state.SsCoopControl);
                        }
                        Printing.RePrintSlipSlInIreportExat(this, PayinslipNo, state.SsCoopControl, ireportobj);

                    }
                    else if (printtype == "JS")
                    {
                        //พิมพ์่jsslip
                        String PayinslipNo = strslip.payinslip_no.Trim();
                        if (state.SsCoopControl == "027001")
                        {
                            //ใช้ดป็น PBSLIP แต่ base เป็น JS
                        //    Printing.PrintSlipSlpayin(this, PayinslipNo, state.SsCoopControl);
                        }
                        else if (state.SsCoopControl == "034001" || state.SsCoopControl == "040001")
                        {
                           // Printing.PrintSlipSlpayinScr(this, PayinslipNo, state.SsCoopControl);

                            string payinslip_no = strslip.payinslip_no;
                            decimal slip_amt = dsMain.DATA[0].SLIP_AMT;

                            ////////////////////////////
                            //// ทำค่า sum รวมเป็นภาษาไทย
                            string bahtTxt, n, bahtTH_sum = "";
                            double amount;
                            try { amount = Convert.ToDouble(slip_amt); }
                            catch { amount = 0; }
                            bahtTxt = amount.ToString("####.00");
                            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                            string[] temp = bahtTxt.Split('.');
                            string intVal = temp[0];
                            string decVal = temp[1];
                            if (Convert.ToDouble(bahtTxt) == 0)
                                bahtTH_sum = "ศูนย์บาทถ้วน";
                            else
                            {
                                for (int j = 0; j < intVal.Length; j++)
                                {
                                    n = intVal.Substring(j, 1);
                                    if (n != "0")
                                    {
                                        if ((j == (intVal.Length - 1)) && (n == "1"))
                                            bahtTH_sum += "เอ็ด";
                                        else if ((j == (intVal.Length - 2)) && (n == "2"))
                                            bahtTH_sum += "ยี่";
                                        else if ((j == (intVal.Length - 2)) && (n == "1"))
                                            bahtTH_sum += "";
                                        else
                                            bahtTH_sum += num[Convert.ToInt32(n)];
                                        bahtTH_sum += rank[(intVal.Length - j) - 1];
                                    }
                                }
                                bahtTH_sum += "บาท";
                                if (decVal == "00")
                                    bahtTH_sum += "ถ้วน";
                                else
                                {
                                    for (int j = 0; j < decVal.Length; j++)
                                    {
                                        n = decVal.Substring(j, 1);
                                        if (n != "0")
                                        {
                                            if ((j == decVal.Length - 1) && (n == "1"))
                                                bahtTH_sum += "เอ็ด";
                                            else if ((j == (decVal.Length - 2)) && (n == "2"))
                                                bahtTH_sum += "ยี่";
                                            else if ((j == (decVal.Length - 2)) && (n == "1"))
                                                bahtTH_sum += "";
                                            else
                                                bahtTH_sum += num[Convert.ToInt32(n)];
                                            bahtTH_sum += rank[(decVal.Length - j) - 1];
                                        }
                                    }
                                    bahtTH_sum += "สตางค์";
                                }
                            }
                            /////////////////////////
                            string report_name = "", report_label = "";
                            report_name = "r_sl_slippayin_receipt_stk";
                            report_label = "ใบเสร็จรับเงิน";

                            iReportArgument args = new iReportArgument();
                            //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("payinslip_no", iReportArgumentType.String, payinslip_no);
                            args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                           // Printing.PrintSlipSlpayin_sqlserver(this, payinslip_no, state.SsCoopControl, bahtTH_sum);


                        }
                        else
                        {

                            Printing.ShrlonPrintSlipPayIn(this, state.SsCoopControl, PayinslipNo, 2);
                        }
                    }
                    else
                    {
                        //PBSLIP
                        string payinslip_no = strslip.payinslip_no;
                        Printing.PrintSlipSlpayin(this, payinslip_no, state.SsCoopControl);
                        
                    }

                }
                Session["sliptype"] = dsMain.DATA[0].SLIPTYPE_CODE;
                dsMain.ResetRow();
                dsDetailShare.ResetRow();
                dsDetailLoan.ResetRow();
                dsDetailEtc.ResetRow();
                refresh();
                if (idtm_lastDate != state.SsWorkDate)
                {
                    dsMain.DATA[0].SLIP_DATE = idtm_lastDate;
                    dsMain.DATA[0].OPERATE_DATE = idtm_lastDate;
                }
                else
                {
                    dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                    dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public string GetSql(string rslip)
        {
            String sql = @"select
                    a.payinslip_no,
                    a.member_no,
                    a.sliptype_code,
                    a.moneytype_code,
                    a.document_no,
                    a.slip_date,
                    a.operate_date,
                    a.sharestk_value,
                    a.intaccum_amt,
                    a.sharestkbf_value,
                    a.slip_amt,
                    a.slip_status,
                    a.entry_id,
                    a.entry_bycoopid,
                    b.slipitemtype_code,
                    b.shrlontype_code,
                    b.loancontract_no,
                    b.slipitem_desc,
                    b.period,
                    b.principal_payamt,
                    b.interest_payamt,
                    b.item_payamt,
                    b.item_balance,
                    b.calint_to,
                    d.prename_desc+c.memb_name+'  '+c.memb_surname as member_name,
                    a.membgroup_code,
                    e.membgroup_desc,
                    c.membtype_code,
				    c.addr_no,
                    c.addr_moo,
				    c.addr_soi,
				    c.addr_village,
                    c.addr_road,
				    h.tambol_desc,
				    i.district_desc,
				    j.province_desc,
				    c.addr_postcode,
                    f.membtype_desc,
                    g.receipt_remark1 as remark_line1,
                    g.receipt_remark2 as remark_line2,
                    '' AS  money_thaibaht 
                    from slslippayin a, 
slslippayindet b,
mbucfprename d, 
mbucfmembgroup e, 
mbucfmembtype f, 
cmcoopmaster g,  
mbmembmaster c LEFT JOIN mbucftambol h ON c.tambol_code	  = h.tambol_code        
LEFT JOIN mbucfdistrict i ON  c.amphur_code	  = i.district_code                
LEFT JOIN mbucfprovince j ON c.province_code	= j.province_code

where	a.coop_id = '" + state.SsCoopControl + @"'
                  and a.payinslip_no in ('" + rslip + @"')
                  and     a.coop_id   = b.coop_id
                  and a.payinslip_no	  = b.payinslip_no
                  and a.memcoop_id	  = c.coop_id
                  and	a.member_no = c.member_no
                  and c.prename_code	= d.prename_code
                  and a.memcoop_id	  = e.coop_id
                  and a.membgroup_code	= e.membgroup_code
                  and	c.coop_id	  = f.coop_id
                  and c.membtype_code	= f.membtype_code
                  and	a.coop_id	  = g.coop_id";
            return sql;
        }

        public void WebSheetLoadEnd()
        {
            for (int i = 0; i < dsDetailShare.RowCount; i++)
            {
                if (dsDetailShare.DATA[i].OPERATE_FLAG == 1)
                {
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.SLIPITEMColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.PERIODColumn).Enabled = true;
                    dsDetailShare.FindCheckBox(i, dsDetailShare.DATA.PERIODCOUNT_FLAGColumn).Enabled = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.ITEM_BALANCEColumn).ReadOnly = true;
                }
                else
                {
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.SLIPITEMColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.PERIODColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    dsDetailShare.FindTextBox(i, dsDetailShare.DATA.ITEM_BALANCEColumn).ReadOnly = true;
                    dsDetailShare.FindCheckBox(i, dsDetailShare.DATA.PERIODCOUNT_FLAGColumn).Enabled = false;
                }
            }
            for (int k = 0; k < dsDetailLoan.RowCount; k++)
            {
                if (dsDetailLoan.DATA[k].OPERATE_FLAG == 1)
                {
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetailLoan.FindCheckBox(k, dsDetailLoan.DATA.PERIODCOUNT_FLAGColumn).Enabled = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.PERIODColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;

                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.CP_INTERESTPAYColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = false;
                    if(state.SsCoopControl == "022001")
                    { dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.INTEREST_PAYAMTColumn).ReadOnly = true; }
                    else {dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.INTEREST_PAYAMTColumn).ReadOnly = false;}
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.ITEM_BALANCEColumn).ReadOnly = true;
                }
                else
                {
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetailLoan.FindCheckBox(k, dsDetailLoan.DATA.PERIODCOUNT_FLAGColumn).Enabled = false;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.PERIODColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.CP_INTERESTPAYColumn).ReadOnly = true;                    
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.INTEREST_PAYAMTColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    dsDetailLoan.FindTextBox(k, dsDetailLoan.DATA.ITEM_BALANCEColumn).ReadOnly = true;
                }
            }
            for (int k = 0; k < dsDetailEtc.RowCount; k++)
            {
                if (dsDetailEtc.DATA[k].OPERATE_FLAG == 1)
                {
                    dsDetailEtc.FindDropDownList(k, dsDetailEtc.DATA.SLIPITEMTYPE_CODEColumn).Enabled = true;
                    dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.SLIPITEM_DESCColumn).ReadOnly = true;
                    dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                    //dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.PRNCALINT_AMTColumn).ReadOnly = true;
                }
                else
                {
                    dsDetailEtc.FindDropDownList(k, dsDetailEtc.DATA.SLIPITEMTYPE_CODEColumn).Enabled = false;
                    dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.SLIPITEM_DESCColumn).ReadOnly = true;
                    dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    //dsDetailEtc.FindTextBox(k, dsDetailLoan.DATA.PRNCALINT_AMTColumn).ReadOnly = true;
                }
            }
            if (dsMain.DATA[0].ACCID_FLAG == 1)
            {
                dsMain.FindDropDownList(0, dsMain.DATA.TOFROM_ACCIDColumn).Enabled = true;
            }
            else
            {
                dsMain.FindDropDownList(0, dsMain.DATA.TOFROM_ACCIDColumn).Enabled = false;
            }

            if (HdSlipStatus.Value == "1")
            {
                //main 
                dsMain.FindTextBox(0, dsMain.DATA.OPERATE_DATEColumn).ReadOnly = true;
                dsMain.FindDropDownList(0, "SLIPTYPE_CODE").Enabled = false;
                dsMain.FindDropDownList(0, "moneytype_code").Enabled = false;
                dsMain.FindCheckBox(0, "accid_flag").Enabled = false;
                dsMain.FindDropDownList(0, "tofrom_accid").Enabled = false;
                dsMain.FindTextBox(0, dsMain.DATA.COMPUTE1Column).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.ENTRY_IDColumn).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.SLIP_AMTColumn).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.EXPENSE_ACCIDColumn).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.REF_SLIPAMTColumn).ReadOnly = true;

                //loan
                for (int i = 0; i < dsDetailLoan.RowCount; i++)
                {
                    dsDetailLoan.FindCheckBox(i, "operate_flag").Enabled = false;
                    dsDetailLoan.FindTextBox(i, "loancontract_no").ReadOnly = true;
                    dsDetailLoan.FindCheckBox(i, "periodcount_flag").Enabled = false;
                    dsDetailLoan.FindTextBox(i, "period").ReadOnly = true;
                    dsDetailLoan.FindTextBox(i, "bfshrcont_balamt").ReadOnly = true;
                    dsDetailLoan.FindTextBox(i, "bflastcalint_date").ReadOnly = true;
                    dsDetailLoan.FindTextBox(i, "COMPUTE1").ReadOnly = true;
                    dsDetailLoan.FindTextBox(i, "principal_payamt").ReadOnly = true;
                    if (state.SsCoopControl == "022001")
                    { dsDetailLoan.FindTextBox(i, "interest_payamt").ReadOnly = false; }
                    else { dsDetailLoan.FindTextBox(i, "interest_payamt").ReadOnly = true; }
                    dsDetailLoan.FindTextBox(i, "item_payamt").ReadOnly = true;
                    dsDetailLoan.FindTextBox(i, "item_balance").ReadOnly = true;
                }
                //share
                for (int i = 0; i < dsDetailShare.RowCount; i++)
                {
                    dsDetailShare.FindCheckBox(i, "operate_flag").Enabled = false;
                    dsDetailShare.FindTextBox(i, "slipitem").ReadOnly = true;
                    dsDetailShare.FindCheckBox(i, "periodcount_flag").Enabled = false;
                    dsDetailShare.FindTextBox(i, "period").ReadOnly = true;
                    dsDetailShare.FindTextBox(i, "bfshrcont_balamt").ReadOnly = true;
                    dsDetailShare.FindTextBox(i, "item_payamt").ReadOnly = true;
                    dsDetailShare.FindTextBox(i, "item_balance").ReadOnly = true;
                }
                // etc
                for (int i = 0; i < dsDetailEtc.RowCount; i++)
                {
                    dsDetailEtc.FindCheckBox(i, "operate_flag").Enabled = false;
                    dsDetailEtc.FindDropDownList(i, "slipitemtype_code").Enabled = false;
                    dsDetailEtc.FindTextBox(i, "slipitem_desc").ReadOnly = true;
                    dsDetailEtc.FindTextBox(i, "item_payamt").ReadOnly = true;
                    dsDetailEtc.FindTextBox(i, "prncalint_amt").ReadOnly = true;
                }
            }

            if (state.SsCoopId != "008001")
            {
                try
                {
                    dsMain.IsShow = "hidden";

                    dsMain.FindTextBox(0, "wrtfund").Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                }
                catch
                {

                }
            }
        }
        public void refresh()
        {
            dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            of_activeworkdate();
            dsMain.DdSliptype();
            dsMain.DdTofromAccBlank();//ทดสอบ Dd ช่องว่าง
            dsMain.DdMoneyType();
            dsDetailEtc.DdLoanType();
            dsMain.DATA[0].SLIPTYPE_CODE = Session["sliptype"].ToString();
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            add_row.Visible = false;
        }

        private void InitLnRcv()
        {
            try
            {
                //HfisCalInt.Value = "false";

                string member_no = dsMain.DATA[0].MEMBER_NO;
                string mem_no = WebUtil.MemberNoFormat(member_no);
                HdCoopControl.Value = state.SsCoopControl;
                str_slippayin slip = new str_slippayin();
                slip.member_no = mem_no;
                slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                slip.sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                slip.memcoop_id = state.SsCoopControl;
                wcf.NShrlon.of_initslippayin(state.SsWsPass, ref slip);
                dsMain.ImportData(slip.xml_sliphead);
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                String mType = dsMain.DATA[0].MONEYTYPE_CODE;
                if (mType == "")
                {
                    dsMain.DATA[0].MONEYTYPE_CODE = "CSH";//dsMain.SetItemString(1, "moneytype_code", "CSH");
                }
                dsMain.DATA[0].SLIPTYPE_CODE = dsMain.DATA[0].SLIPTYPE_CODE.Trim();
                string sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

                dsMain.DdSliptype();
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                dsMain.DdMoneyType();

                try
                {
                    slip.member_no = mem_no;
                    slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                    slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                    slip.sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                    slip.memcoop_id = state.SsCoopControl;
                    dsDetailShare.ImportData(slip.xml_slipshr);
                }
                catch { dsDetailShare.ResetRow(); }
                try
                {
                    slip.member_no = mem_no;
                    slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                    slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                    slip.sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                    slip.memcoop_id = state.SsCoopControl;
                    dsDetailLoan.ImportData(slip.xml_sliplon);
                    ///เพิ่มส่วนการคำนวณดอกเบี้ยที่ต้องชำระ pxaftermthkeep
                    AbsIntpay();
                }
                catch { dsDetailLoan.ResetRow(); }
                try
                {
                    //String reqDetailEtcXML = shrlonService.Initlist_lnreqapv(state.SsWsPass, state.SsCoopId, state.SsCoopId);
                    slip.member_no = mem_no;
                    slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                    slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                    slip.sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                    slip.memcoop_id = state.SsCoopControl;
                    dsDetailEtc.ImportData(slip.xml_slipetc);
                    dsDetailEtc.DdLoanType();
                }
                catch { dsDetailEtc.ResetRow(); }

                //เช็คการเรียกเก็บ
                //init กองทุน
                try
                {
                    string sql = "select wrtfund_balance from mbmembmaster where coop_id={0} and member_no={1}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, mem_no);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        decimal wrtfund = dt.GetDecimal("wrtfund_balance");
                        dsMain.DATA[0].WRTFUND = wrtfund;
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            SetDefaultTofromaccid();
        }

        public void calItemPay()
        {
            int row = dsDetailShare.RowCount;
            int rowl = dsDetailLoan.RowCount;
            int rowe = dsDetailEtc.RowCount;

            decimal slip_amt = 0;
            for (int i = 0; i < row; i++)
            {
                decimal item_payamt = dsDetailShare.DATA[i].ITEM_PAYAMT;
                slip_amt += item_payamt;
            }
            for (int k = 0; k < rowl; k++)
            {
                decimal item_payamt = dsDetailLoan.DATA[k].ITEM_PAYAMT;
                slip_amt += item_payamt;

            }
            for (int j = 0; j < rowe; j++)
            {
                decimal item_payamt = dsDetailEtc.DATA[j].ITEM_PAYAMT;
                slip_amt += item_payamt;

            }

            dsMain.DATA[0].SLIP_AMT = slip_amt;

        }

        /// <summary>
        /// set คู่บัญชี
        /// </summary>
        private void SetDefaultTofromaccid()
        {
            string sql = @"select account_id
                from cmucftofromaccid 
                where coop_id = {0} 
	            and moneytype_code = {1}
	            and sliptype_code = {2}
	            and default_flag = 1";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MONEYTYPE_CODE, dsMain.DATA[0].SLIPTYPE_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                string accid = dt.GetString("account_id");
                dsMain.DATA[0].TOFROM_ACCID = accid;
            }
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
                    where coop_id = '" + state.SsCoopControl + @"'
                    and application = 'shrlon'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    ldtm_workdate = dt.GetDate("workdate");
                    li_clsdaystatus = dt.GetInt32("closeday_status");
                    if (li_clsdaystatus == 1)
                    {
                        int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref idtm_lastDate);
                    }
                    else
                    {
                        idtm_lastDate = state.SsWorkDate;
                    }
                }
                if (state.SsWorkDate != idtm_lastDate)
                {
                    dsMain.DATA[0].OPERATE_DATE = idtm_lastDate;
                    dsMain.DATA[0].SLIP_DATE = idtm_lastDate;
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการเป็น " + idtm_lastDate.ToString("dd/MM/yyyy", th));
                    this.SetOnLoadedScript("alert('ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการเป็น " + idtm_lastDate.ToString("dd/MM/yyyy", th) + " ')");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void CheckReturnWrt()
        {
            try
            {
                string cont_no = "";
                string member_no = "";
                string sqlCont = "";
                string sqlWrt = "";

                string coop_id, slip_no, entry_id, from_system, cash_type, payment_desc, bank_code, bank_branch, pay_towhom;
                string itempaytype_code, nonmember_detail, machine_id, tofrom_accid, account_no, chequebook_no;
                string ref_slipno, remark, from_bankcode, from_branchcode, recvpay_id, ref_system;
                DateTime entry_date, operate_date, dateon_chq, receive_date, recvpay_time;
                decimal payment_status, itempay_amt, pay_recv_status, member_flag, bankfee_amt, banksrv_amt, cheque_status;
                decimal receive_status, item_amtnet;

                member_no = dsMain.DATA[0].MEMBER_NO;
                coop_id = state.SsCoopControl;
                entry_id = state.SsUsername;
                from_system = "LON";
                cash_type = dsMain.DATA[0].MONEYTYPE_CODE;
                payment_desc = "คืนกองทุนกสส สมาชิก " + member_no;
                bank_code = dsMain.DATA[0].EXPENSE_BANK;
                bank_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                pay_towhom = dsMain.DATA[0].NAMEB;
                nonmember_detail = dsMain.DATA[0].NAMEB;
                itempaytype_code = "RWT";
                machine_id = "";
                tofrom_accid = dsMain.DATA[0].TOFROM_ACCID;
                account_no = "";
                chequebook_no = "";
                ref_slipno = "";
                payment_status = 1;
                from_bankcode = "";
                from_branchcode = "";
                recvpay_id = entry_id;
                ref_system = "LON";
                entry_date = state.SsWorkDate;
                operate_date = dsMain.DATA[0].OPERATE_DATE;
                dateon_chq = new DateTime();
                receive_date = new DateTime();
                recvpay_time = new DateTime();
                pay_recv_status = 0;
                member_flag = 1;
                bankfee_amt = 0;
                banksrv_amt = 0;
                cheque_status = 0;
                receive_status = 8;

                for (int i = 0; i < dsDetailLoan.RowCount; i++)
                {
                    cont_no = dsDetailLoan.DATA[i].LOANCONTRACT_NO;
                    if (dsDetailLoan.DATA[i].OPERATE_FLAG == 1)
                    {
                        sqlCont = "select * from lncontmaster where loancontract_no = {0} and coop_id={1} and principal_balance=0 and contract_status=-1";
                        sqlCont = WebUtil.SQLFormat(sqlCont, cont_no, state.SsCoopControl);
                        Sdt dtCont = WebUtil.QuerySdt(sqlCont);
                        if (dtCont.Next())
                        {
                            sqlWrt = @"select * from wrtfundstatement 
where member_no = {0} and ref_contno ={1} and coop_id={2} and wrtitemtype_code='PWT'
and  ref_contno not in (select ref_contno from wrtfundstatement 
where member_no = {0} and ref_contno ={1} and coop_id={2} and wrtitemtype_code='RWT')";
                            sqlWrt = WebUtil.SQLFormat(sqlWrt, member_no, cont_no, state.SsCoopControl);
                            Sdt dtWrt = WebUtil.QuerySdt(sqlWrt);
                            if (dtWrt.Next())
                            {
                                remark = cont_no;
                                itempay_amt = dtWrt.GetDecimal("wrtfund_balance");
                                item_amtnet = itempay_amt;
                                slip_no = Get_NumberDOC("FNRECEIVENO");
                                string insFinWrt = @"insert into finslip
                                                    (
	                                                    coop_id,            slip_no,        entry_id,           entry_date,						
	                                                    operate_date,       from_system,    payment_status,     cash_type,				
	                                                    payment_desc,       bank_code,      bank_branch,        itempay_amt,
	                                                    pay_towhom,         dateon_chq,     member_no	,       itempaytype_code,
	                                                    pay_recv_status,    member_flag,    nonmember_detail,   machine_id,
	                                                    bankfee_amt,        banksrv_amt,    tofrom_accid,       account_no,
	                                                    chequebook_no,      ref_slipno,     remark,             from_bankcode,
	                                                    from_branchcode,    cheque_status,  receive_date,       receive_status,
	                                                    item_amtnet,        recvpay_id,     recvpay_time,       ref_system
                                                    )  
                                                    values
                                                    (
                                                        {0}, {1}, {2}, {3},	
                                                        {4}, {5}, {6}, {7},	
                                                        {8}, {9}, {10}, {11},	
                                                        {12}, {13}, {14}, {15},	
                                                        {16}, {17}, {18}, {19},	
                                                        {20}, {21}, {22}, {23},	
                                                        {24}, {25}, {26}, {27},	
                                                        {28}, {29}, {30}, {31},	
                                                        {32}, {33}, {34}, {35}
                                                    )";
                                try
                                {
                                    insFinWrt = WebUtil.SQLFormat(insFinWrt,
                                        coop_id, slip_no, entry_id, entry_date,
                                                            operate_date, from_system, payment_status, cash_type,
                                                            payment_desc, bank_code, bank_branch, itempay_amt,
                                                            pay_towhom, dateon_chq, member_no, itempaytype_code,
                                                            pay_recv_status, member_flag, nonmember_detail, machine_id,
                                                            bankfee_amt, banksrv_amt, tofrom_accid, account_no,
                                                            chequebook_no, ref_slipno, remark, from_bankcode,
                                                            from_branchcode, cheque_status, receive_date, receive_status,
                                                            item_amtnet, recvpay_id, recvpay_time, ref_system);
                                    Sdt dtIns = WebUtil.QuerySdt(insFinWrt);

                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        public void PrintSlippayin(string payinslip_no)
        {
            string sql = "";
            sql = @"SELECT pin.PAYINSLIP_NO,
            //TO_CHAR( pin.SLIP_DATE, 'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as TSLIPDATE,
            ''  as TSLIPDATE,
            pre.PRENAME_DESC + mas.MEMB_NAME + ' ' + mas.MEMB_SURNAME AS FULLNAME,
            mas.MEMBER_NO,
            mgrp.MEMBGROUP_DESC as memgroup,
            pin.INTACCUM_AMT,
            decode(pindet.slipitemtype_code,
            'SHR','ซื้อหุ้นพิเศษ',
            'LON','ชำระหนี้ '+ ISNULL(pindet.loancontract_no,''),
            'MUT',pindet.SLIPITEM_DESC,pindet.SLIPITEM_DESC) as slip_desc,
            pindet.PERIOD,
            pindet.PRINCIPAL_PAYAMT,
            pindet.INTEREST_PAYAMT,
            pindet.ITEM_PAYAMT,
            decode(pindet.slipitemtype_code,'SHR',shr.sharestk_amt*10,pindet.ITEM_BALANCE) as ITEM_BALANCE,
            pindet.Shrlontype_Code,
            pin.slip_amt as sumitempay,
            '' as tbaht

            FROM
            SLSLIPPAYIN pin,
            MBMEMBMASTER mas,
            SHSHAREMASTER shr,
            MBUCFPRENAME pre,
            SLSLIPPAYINDET pindet,
            MBUCFMEMBGROUP mgrp
            WHERE (mas.MEMBER_NO=shr.MEMBER_NO ) AND
            ( mas.MEMBER_NO = pin.MEMBER_NO ) AND
            ( mas.COOP_ID = pin.MEMCOOP_ID ) AND
            ( mas.PRENAME_CODE = pre.PRENAME_CODE ) AND
            ( mas.MEMBGROUP_CODE = mgrp.MEMBGROUP_CODE ) AND
            ( mas.COOP_ID = mgrp.COOP_ID ) AND
            ( pin.PAYINSLIP_NO = pindet.PAYINSLIP_NO ) AND
            ( pin.COOP_ID = pindet.COOP_ID ) AND
            ( ( pin.PAYINSLIP_NO = {0} ) AND
            ( pin.COOP_ID = {1}) )
            ORDER BY pindet.Shrlontype_Code
            ";
            sql = WebUtil.SQLFormat(sql, payinslip_no, state.SsCoopControl);
            DataTable data = WebUtil.Query(sql);

            Printing.PrintAppletPB(this, "sl_slip_payin", data);
        }

        public void AbsIntpay()
        {
            decimal intmonthArrear = 0, rkeepprin = 0, rkeepint = 0, nkeepint = 0, interestPeriod = 0, bfintarrAmt = 0, intreturnAmt = 0;
            int loanrow = dsDetailLoan.RowCount;
            DateTime lastprocess_date, ldtm_opedate;
            for (int r = 0; r < loanrow; r++)
            {
                intmonthArrear = dsDetailLoan.DATA[r].BFINTARRMTH_AMT;
                bfintarrAmt = dsDetailLoan.DATA[r].BFINTARR_AMT;
                rkeepprin = dsDetailLoan.DATA[r].RKEEP_PRINCIPAL;
                rkeepint = dsDetailLoan.DATA[r].RKEEP_INTEREST;
                nkeepint = dsDetailLoan.DATA[r].NKEEP_INTEREST;
                interestPeriod = dsDetailLoan.DATA[r].INTEREST_PERIOD;
                intreturnAmt = dsDetailLoan.DATA[r].BFINTRETURN_AMT;
                lastprocess_date = dsDetailLoan.DATA[r].BFLASTPROC_DATE;

                ldtm_opedate = dsMain.DATA[0].OPERATE_DATE;

                if (dsDetailLoan.DATA[r].BFPXAFTERMTHKEEP_TYPE == 1 && lastprocess_date >= ldtm_opedate)
                {
                    if (state.SsCoopControl == "006001") //ระยองหลังเรียกเก็บไม่ต้องเอาดอกเบี้ยค้างมาคิด เพราะยอดดอกเบี้ยค้าง คิดในเรียกเก็บแล้ว
                    {
                        dsDetailLoan.DATA[r].CP_INTERESTPAY = rkeepint - nkeepint;
                    }
                    else
                    {
                        dsDetailLoan.DATA[r].CP_INTERESTPAY = (bfintarrAmt + intmonthArrear) - ((intreturnAmt + rkeepint) - nkeepint);
                        if (dsDetailLoan.DATA[r].CP_INTERESTPAY < 0)
                        {
                            dsDetailLoan.DATA[r].CP_INTERESTPAY = 0;
                        }
                    }
                }
                else
                {
                    dsDetailLoan.DATA[r].CP_INTERESTPAY = interestPeriod + bfintarrAmt + intmonthArrear;
                }
            }
        }
    }
}