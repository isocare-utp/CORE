using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;  // new deposit
using CoreSavingLibrary.WcfNCommon; // new common
using Sybase.DataWindow;
using DataLibrary;
using System.Web.Services.Protocols;
using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_reqdeposit_namethaieng : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private DwThDate tDwCheque;
    //    private DepositClient depService;
      //  private CommonClient cmdService;

        private n_depositClient ndept; // new deposit
        private n_commonClient ncommon; // new common
        private bool completeCheque;


        //POSTBACK
        protected String postChangeDeptType;
        protected String postMemberNo;
        protected String postTotalAmt;
        protected String postDeptAccountNo;
        protected String postPost;
        protected String postDwGainAddRow;
        protected String postGainMemberNo;
        protected String postDwGainDelRow;
        protected String postInsertRowCheque;
        protected String postDeleteRowCheque;
        protected String postBankCode;
        protected String postBankBranchCode;
        protected String postSaveNoCheckApv;
        protected String postPostOffice;
        protected String CheckCoop;
        protected String membNo;
        protected String postDeptPassbookNo;
        protected String postBankname;
        protected String postAccountStatus;
        private DepositConfig depConfig;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postChangeDeptType = WebUtil.JsPostBack(this, "postChangeDeptType");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postTotalAmt = WebUtil.JsPostBack(this, "postTotalAmt");
            postDeptAccountNo = WebUtil.JsPostBack(this, "postDeptAccountNo");
            postPost = WebUtil.JsPostBack(this, "postPost");
            postDwGainAddRow = WebUtil.JsPostBack(this, "postDwGainAddRow");
            postDwGainDelRow = WebUtil.JsPostBack(this, "postDwGainDelRow");
            postGainMemberNo = WebUtil.JsPostBack(this, "postGainMemberNo");
            postBankCode = WebUtil.JsPostBack(this, "postBankCode");
            postBankBranchCode = WebUtil.JsPostBack(this, "postBankBranchCode");
            postInsertRowCheque = WebUtil.JsPostBack(this, "postInsertRowCheque");
            postDeleteRowCheque = WebUtil.JsPostBack(this, "postDeleteRowCheque");
            postSaveNoCheckApv = WebUtil.JsPostBack(this, "postSaveNoCheckApv");
            postPostOffice = WebUtil.JsPostBack(this, "postPostOffice");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            postDeptPassbookNo = WebUtil.JsPostBack(this, "postDeptPassbookNo");
            postBankname = WebUtil.JsPostBack(this, "postBankname");
            postAccountStatus = WebUtil.JsPostBack(this, "postAccountStatus");
            //------------------------------------------------------------------
            //------------------------------------------------------------------
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("deptopen_date", "deptopen_tdate");
            //------------------------------------------------------------------
            tDwCheque = new DwThDate(DwCheque, this);
            tDwCheque.Add("cheque_date", "cheque_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //cmdService = wcf.NCommon;
                //depService = wcf.Deposit;

                ndept = wcf.NDeposit; //new deposit
                ncommon = wcf.NCommon; //new common
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ WebService ได้");
            }
            HdIsInsertCheque.Value = "";
            HdSaveAccept.Value = "false";
            completeCheque = true;
            //depConfig = new DepositConfig(depService);

            if (!IsPostBack)
            {
                //state.SsCoopId = state.SsCoopControl;
                DwMain.InsertRow(0);
                //DwDeptMonth.InsertRow(0);
                DwListCoop.InsertRow(0);
                DwMain.SetItemDateTime(1, "deptopen_date", state.SsWorkDate);
                DwMain.SetItemString(1, "memcoop_id", state.SsCoopId);
                tDwMain.Eng2ThaiAllRow();
                HfReset.Value = "False";
                HfCoopid.Value = state.SsCoopId;
                try
                {
                    HdDayPassCheq.Value = WebUtil.GetDpDeptConstant("daypasschq");
                }
                catch
                {
                    HdDayPassCheq.Value = "1";
                }
                HdIsPostBack.Value = "false";
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwGain);
                //this.RestoreContextDw(DwDeptMonth);
                this.RestoreContextDw(DwCheque);
                this.RestoreContextDw(DwListCoop);
            }
            int monthIntPayMeth = DwUtil.GetInt(DwMain, 1, "monthintpay_meth");
            if (monthIntPayMeth == 2)
            {
                try
                {
                    DwUtil.RetrieveDDDW(DwMain, "bank_code_1", "dp_reqdeposit.pbl", null);
                    String bankCode = DwUtil.GetString(DwMain, 1, "bank_code");
                    if (!string.IsNullOrEmpty(bankCode))
                    {
                        object[] argBB = new object[1] { bankCode };
                        DwUtil.RetrieveDDDW(DwMain, "bank_branch_1", "dp_reqdeposit.pbl", argBB);
                        DataWindowChild dcBB = DwMain.GetChild("bank_branch_1");
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                DwMain.SetItemString(1, "bank_code", "");
                DwMain.SetItemString(1, "bank_branch", "");
            }
            LoopCheque();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postChangeDeptType")
            {
                JsPostChangeDeptType();
            }
            else if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
                // JsSetDefaultDeptNo();
            }
            else if (eventArg == "postTotalAmt")
            {
                JsPostTotalAmt();
            }
            else if (eventArg == "postDeptAccountNo")
            {
                JsPostDeptAccountNo();
            }
            else if (eventArg == "postDwGainAddRow")
            {
                JsPostDwGainAddRow();
            }
            else if (eventArg == "postDwGainDelRow")
            {
                JsPostDwGainDelRow();
            }
            else if (eventArg == "postGainMemberNo")
            {
                JsPostGainMemberNo();
            }
            else if (eventArg == "postInsertRowCheque")
            {
                JsPostInsertRowCheque();
            }
            else if (eventArg == "postDeleteRowCheque")
            {
                JsPostDeleteRowCheque();
            }
            else if (eventArg == "postBankCode")
            {
                JsPostBankCode();
            }
            else if (eventArg == "postBankBranchCode")
            {
                JsPostBankBranchCode();
            }
            else if (eventArg == "postSaveNoCheckApv")
            {
                SaveSheet();
            }
            else if (eventArg == "postPostOffice")
            {
                JsPostPostOffice();
            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }
            else if (eventArg == "postPost")
            {
                JsPostCoop();
            }
            else if (eventArg == "postDeptPassbookNo")
            {
                JsPostDeptPassbookNo();
            }
            else if (eventArg == "postBankname")
            {
                JsBankname();
            }
            else if (eventArg == "postAccountStatus")
            {
                if (DwMain.GetItemDecimal(1, "account_status") == 1)
                {
                    DwMain.Modify("deptaccount_no.protect=0");
                }
                else
                {
                    DwMain.Modify("deptaccount_no.protect=1");
                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String as_apvdoc = Hdas_apvdoc.Value;
                DateTime w = state.SsWorkDate;
                String ChqXml = "", deptpassbook = "";

                // by Phai ตรวจสอบการ กรอกใบเลข PB/Cert.
                try
                {
                    deptpassbook = DwMain.GetItemString(1, "deptpassbook_no");
                }
                catch { deptpassbook = ""; }

                if (deptpassbook == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขที่ PB/Cert.!");
                    return;
                }
                //------------------------------------

                if (!completeCheque)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขที่เช็คให้ครบถ้วน!");
                    return;
                }
                try
                {
                    String trandept_accno = DwMain.GetItemString(1, "tran_deptacc_no");
                    trandept_accno = trandept_accno.Replace("-", "");
                    DwMain.SetItemString(1, "tran_deptacc_no", trandept_accno);
                    DwMain.SetItemString(1, "tran_bankacc_no", trandept_accno);
                }
                catch { }
                if (DwCheque.RowCount > 0)
                {
                    ChqXml = DwCheque.Describe("datawindow.data.xml");
                }
                String slipXml = DwMain.Describe("datawindow.data.xml");


                String[] apv = new string[20];//depService.CheckRightPermissionDep(state.SsWsPass, state.SsUsername, state.SsCoopId, slipXml, ChqXml, 1, ref as_apvdoc);

                apv[0] = "true";
                if (apv[0] == "true")
                {
                    SaveSheet();
                }
                else
                {
                    String deptAccountNo = "RequestOPN";
                    //deptAccountNo = depService.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);

                    deptAccountNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, deptAccountNo);

                    String memberNo = DwUtil.GetString(DwMain, 1, "member_no", "");
                    Decimal netAmt = DwUtil.GetDec(DwMain, 1, "deptreq_sumamt", 0);
                    String accName = DwUtil.GetString(DwMain, 1, "deptaccount_name", "");
                    String itemType = DwUtil.GetString(DwMain, 1, "recppaytype_code", "");
                    String itemTypeDesc = apv[1].Trim();

                    itemTypeDesc = itemTypeDesc.Length > 59 ? itemTypeDesc.Substring(0, 59) : itemTypeDesc;
                    try
                    {
                        //String reportNo = depService.AddApvTask(state.SsWsPass, state.SsUsername, state.SsApplication, state.SsClientIp, itemType, itemTypeDesc, deptAccountNo, memberNo, state.SsWorkDate, netAmt, deptAccountNo, accName, apv[0], "DEP", 1, state.SsCoopId, ref as_apvdoc);
                        //HdProcessId.Value = reportNo;
                        HdAvpCode.Value = apv[0];
                        HdItemType.Value = itemType;
                        HdAvpAmt.Value = netAmt.ToString();
                        HdCheckApvAlert.Value = "true";

                    }
                    catch (SoapException ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));

                    }
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));

            }
        }

        public void WebSheetLoadEnd()
        {
            String typeCode = DwUtil.GetString(DwMain, 1, "depttype_code");
            //            if (!string.IsNullOrEmpty(typeCode))
            //            {
            //                //DwUtil.RetrieveDDDW(DwMain, "deptpassbook_no", "dp_reqdeposit.pbl", null);
            //                String sql =
            //                @"select	book_group, book_stmbase
            //                from		dpdepttype
            //                where		depttype_code	= '" + typeCode + "'";
            //                try
            //                {
            //                    DataTable dt = WebUtil.Query(sql);
            //                    if (dt.Rows.Count > 0)
            //                    {
            //                        String bookGroup = dt.Rows[0][0].ToString();//.GetString(0);
            //                        String bookStmBase = dt.Rows[0][1].ToString();//dt.GetString(1);
            //                        //DataWindowChild dcBookNo = DwMain.GetChild("deptpassbook_no");

            //                        //dcBookNo.SetFilter("( coop_id = '" + state.SsCoopId + "' ) and ( book_type = '" + bookStmBase + "' ) and ( book_grp = '" + bookGroup + "' )");
            //                        //dcBookNo.Filter();
            //                        //if (dcBookNo.RowCount > 0)
            //                        //{
            //                        //    DwMain.SetItemString(1, "deptpassbook_no", dcBookNo.GetItemString(1, "book_no"));
            //                        //}
            //                    }
            //                }
            //                catch { }
            //            }
            String memberNo = DwUtil.GetString(DwMain, 1, "member_no");
            try
            {
                DataWindowChild dcRecp = DwMain.GetChild("recppaytype_code");
                dcRecp.SetFilter("GROUP_ITEMTPE='OPN' AND ACTIVE_FLAG=1");
                dcRecp.Filter();
            }
            catch { }
            if (DwGain.RowCount > 0)
            {
                DwUtil.RetrieveDDDW(DwGain, "prename_code", "dp_reqdeposit.pbl", null);
            }
            if (!string.IsNullOrEmpty(memberNo) && memberNo != "CIF")
            {
                DwUtil.RetrieveDDDW(DwMain, "acccont_type", "dp_reqdeposit.pbl");
                DwUtil.RetrieveDDDW(DwMain, "recppaytype_code", "dp_reqdeposit.pbl", state.SsCoopControl);
                //DwUtil.RetrieveDDDW(DwMain, "recppaytype_code", "dp_reqdeposit.pbl","");

                try
                {
                    DataWindowChild dcRPT = DwMain.GetChild("recppaytype_code");
                    dcRPT.SetFilter("group_itemtpe='OPN' and active_flag=1");
                    dcRPT.Filter();
                }
                catch { }
                try
                {
                    String membCard = WebUtil.GetCardPerson( memberNo);
                    membCard = WebUtil.ViewCardMemberFormat( membCard);
                    DwMain.Modify("t_member_card.text = '" + membCard + "'");
                }
                catch { }
            }
            else
            {
                DwMain.SetItemString(1, "recppaytype_code", "");
            }
            String recpPayTypeCode = DwUtil.GetString(DwMain, 1, "recppaytype_code");
            String cashType = "";
            if (!string.IsNullOrEmpty(recpPayTypeCode))
            {
                try
                {
                    String tfAccId = DwUtil.GetString(DwMain, 1, "tofrom_accid");
                    //cashType = depService.GetCashType(state.SsWsPass, recpPayTypeCode);
                    cashType = wcf.NDeposit.of_get_cashtype(state.SsWsPass, recpPayTypeCode);

                    DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "dp_reqdeposit.pbl", state.SsCoopControl);
                    //DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "dp_reqdeposit.pbl","");
                    DataWindowChild dcRP = DwMain.GetChild("tofrom_accid");
                    dcRP.SetFilter("cash_type='" + cashType + "'");
                    dcRP.Filter();
                    if (cashType == "CSH" && string.IsNullOrEmpty(tfAccId))
                    {
                        String tfAccNo = dcRP.GetItemString(1, "account_id");
                        DwMain.SetItemString(1, "tofrom_accid", tfAccNo);
                    }
                }
                catch (Exception) { }
            }
            else
            {
                DwMain.SetItemString(1, "tofrom_accid", "");
            }
            if (cashType == "CHQ")
            {
                DwMain.Modify("deptrequest_amt.Protect=1");
                DwMain.Modify("request_tranamt.Protect=1");
                if (DwCheque.RowCount <= 0)
                {
                    JsPostInsertRowCheque();
                }
            }
            else
            {
                int i = 0;
                while (DwCheque.RowCount > 0)
                {
                    DwCheque.DeleteRow(0);
                    if (i++ > 500) break;
                }
            }
            String dueDate = HdDueDate.Value;
            if (dueDate != "" && dueDate != null && dueDate != "01/01/2443")
            {
                DwMain.Modify("t_duedate.text= '" + dueDate + "'");
            }
            else
            {
                DwMain.Modify("t_duedate.text= 'ไม่มีกำหนด'");
            }
            try
            {
                DwUtil.RetrieveDDDW(DwMain, "tofdept_select", "dp_reqdeposit.pbl", state.SsCoopControl);
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(DwMain, "acctype_select", "dp_reqdeposit.pbl", state.SsCoopControl);
                DataWindowChild child = DwMain.GetChild("acctype_select");
                int rowchild = child.RowCount;

                if (rowchild > 0)
                {
                    DwMain.SetItemString(1, "acctype_select", child.GetItemString(1, "persongrp_code"));
                }
            }
            catch { }

            DwMain.SaveDataCache();
            DwGain.SaveDataCache();
            //DwDeptMonth.SaveDataCache();
            DwCheque.SaveDataCache();
            DwListCoop.SaveDataCache();
        }

        #endregion

        #region Function

        private void JsPostCoop()
        {
            for (int i = 1; i <= DwGain.RowCount; i++)
            {
                DwGain.SetItemString(i, "coop_id", state.SsCoopControl);
            }
        }
        private void NewClear()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            // DwDeptMonth.Reset();
            // DwDeptMonth.InsertRow(0);
            DwMain.SetItemDateTime(1, "deptopen_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            DwGain.Reset();
            DwCheque.Reset();
        }

        private void SaveSheet()
        {
            //edit by Joy 
            // Edit By BankCM For Co-Account
            bool chkErr = false;
            String accountNo = "";

            try
            {
                String reqChequeXml = DwCheque.RowCount > 0 ? DwCheque.Describe("DataWindow.Data.XML") : "";
                String reqCoDeptXml = DwGain.RowCount > 0 ? DwGain.Describe("DataWindow.Data.XML") : "";

                short period = 0;
                String reqMainXml = DwMain.Describe("datawindow.data.xml");
                //DwMain.SaveAs("C:\\dwMain.xml", Sybase.DataWindow.FileSaveAsType.Xml);
                
                String slip_no = "";
                String as_apvdoc = Hdas_apvdoc.Value;
                String acccont_type = DwMain.GetItemString(1, "acccont_type");
                if (acccont_type != "01")
                {

                    string RefId = DwGain.GetItemString(1, "ref_id");
                    chkErr = true;
                }

                //int result = depService.OpenAccount(state.SsWsPass, reqMainXml, reqChequeXml, reqCoDeptXml, period, ref accountNo, ref slip_no, ref as_apvdoc);
                int result = wcf.NDeposit.of_openaccount(state.SsWsPass, reqMainXml, reqChequeXml, reqCoDeptXml, period, ref accountNo, ref slip_no, ref as_apvdoc);
                //save masdue ไว้เช็คอุ่นใจ ลำปาง และบันทึกฝากรายการ
                if (state.SsCoopId == "027001")
                {
                    String deptmonth_status = DwMain.GetItemString(1, "deptmonth_status");
                    if (deptmonth_status == "1")
                    {
                        String deptmonth_amt = DwMain.GetItemString(1, "deptmonth_amt");
                        try
                        {
                            String updateMstatus = "update dpdeptmaster set deptmonth_amt = '" + deptmonth_amt +
                                         "' where deptaccount_no	='" + accountNo + "' and COOP_ID ='" + state.SsCoopId + "'  ";
                            Sdt sqlupdate = WebUtil.QuerySdt(updateMstatus);
                        }
                        catch { }
                    }
                    String ckdate = Convert.ToString(state.SsWorkDate);
                    String c_year = ckdate.Substring(6, 4);
                    String daydue = "02/01/" + c_year;
                    DateTime dt_start = Convert.ToDateTime(daydue);

                    if (state.SsWorkDate < dt_start)
                    {
                        String dayenddue = "03/31/" + c_year;
                        DateTime dt_end = Convert.ToDateTime(dayenddue);

                        String sql_masdue = "insert into dpdeptmasdue   " +
                            "(coop_id, DEPTACCOUNT_NO,seq_no ,start_date, end_date, still_use)  " +
                            "values     " +
                            "('" + state.SsCoopId + "','" + accountNo + "','1',to_date('" + dt_start.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'),to_date('" + dt_end.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'), 'Y') ";
                        Sdt sqlInsertmasdue = WebUtil.QuerySdt(sql_masdue);
                    }
                    else if (state.SsWorkDate > dt_start)
                    {
                        Decimal sn_year = Convert.ToDecimal(c_year) + 1;
                        String n_year = Convert.ToString(sn_year);
                        String ndaydue = "02/01/" + n_year;
                        String ndayenddue = "03/31/" + n_year;
                        DateTime dtstart = Convert.ToDateTime(ndaydue);
                        DateTime dtend = Convert.ToDateTime(ndayenddue);

                        String sql_masdue = "insert into dpdeptmasdue   " +
                            "(coop_id, DEPTACCOUNT_NO,seq_no ,start_date, end_date, still_use)  " +
                            "values     " +
                            "('" + state.SsCoopId + "','" + accountNo + "','1',to_date('" + dtstart.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'),to_date('" + dtend.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'), 'Y') ";
                        Sdt sqlInsertmasdue = WebUtil.QuerySdt(sql_masdue);
                    }
                }
                HdAccoutNo.Value = accountNo;
                try
                {
                    HdPassBookNo.Value = DwUtil.GetString(DwMain, 1, "deptpassbook_no");
                }
                catch (Exception)
                {
                    HdPassBookNo.Value = "";
                }
                NewClear();

                if (Convert.ToInt32( WebUtil.GetDpDeptConstant( "printslip_status") ) == 1)
                {
                    //XmlConfigService xmlcon = new XmlConfigService();
                    short printMode = 1;// xmlcon.DepositPrintMode;
                    String xml_return = "";
                    //int reSlip = depService.PrintSlip(state.SsWsPass, slip_no, state.SsCoopId, state.SsPrinterSet, printMode, ref xml_return);
                    int reSlip = wcf.NDeposit.of_print_slip(state.SsWsPass, slip_no, state.SsCoopId, state.SsPrinterSet, printMode, ref xml_return);
                    if (xml_return != "" && printMode == 1)
                    {
                        //if (printMode == 1)
                        //{
                        //    Printing.Print(this, "Slip/ap_deposit/PrintSlip_MHD.aspx", xml_return, 1);
                        //}
                        //else
                        //{
                        Printing.PrintApplet(this, "dept_slip", xml_return);
                        //}
                    }
                }
                DwListCoop.Reset();
                DwListCoop.InsertRow(0);
                HfCoopid.Value = state.SsCoopId;
                DwMain.SetItemString(1, "memcoop_id", HfCoopid.Value);
                LtServerMessage.Text = WebUtil.CompleteMessage("ทำการเปิดบัญชีเรียบร้อยแล้ว ได้เลขที่บัญชีใหม่คือ " + WebUtil.ViewAccountNoFormat( accountNo));
                HdSaveAccept.Value = "true";
                Hdas_apvdoc.Value = "";
            }
            //catch (SoapException ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));

            //}
            catch (Exception ex)
            {
                string Errmsg = "";
                if (chkErr)
                {
                    Errmsg = "กรุณาใส่ผู้ฝากร่วม";
                }
                LtServerMessage.Text = WebUtil.ErrorMessage("( " + Errmsg + " )" + ex.Message);
                String msg = WebUtil.ErrorMessage(ex);
                accountNo = WebUtil.ErrorMessage(ex);
                try
                {
                    accountNo = accountNo.Substring(0, accountNo.LastIndexOf("#"));
                    accountNo = accountNo.Substring( accountNo.LastIndexOf("#") + 1, 10);
                    accountNo = accountNo.Trim();
                    DwMain.SetItemString(1, "deptaccount_no", accountNo);
                 //   DwUtil.set (DwMain, 1, "deptpassbook_no");

                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch { }
            }
            HdIsPostBack.Value = "false";


        }

        private String TryDwMainGetString(String column)
        {
            try
            {
                return DwMain.GetItemString(1, column).Trim();
            }
            catch
            {
                return "";
            }
        }

        //JS-POSTBACK
        private void JsPostChangeDeptType()
        {
           
            try
            {
                String AccType = DwMain.GetItemString(1, "acctype_select").Trim();//DwUtil.GetString(DwMain, 1, "acctype_select");
                String TofDept = DwMain.GetItemString(1, "tofdept_select").Trim(); //DwUtil.GetString(DwMain, 1, "tofdept_select");

                HfMemno.Value = DwMain.GetItemString(1, "member_no").Trim();
                //String xml = depService.InitOpenSlip(state.SsWsPass, TofDept, AccType, HfCoopid.Value, state.SsWorkDate, state.SsUsername, state.SsClientIp);
              
                String xml = wcf.NDeposit.of_init_openslip(state.SsWsPass, TofDept, AccType, HfCoopid.Value, state.SsWorkDate, state.SsUsername, state.SsClientIp); 
                DwMain.Reset();
                DwMain.ImportString(xml, Sybase.DataWindow.FileSaveAsType.Xml);
                HfReset.Value = "True";
                //JsPostMemberNo(); // by Phai
                try
                {
                    DwUtil.RetrieveDDDW(DwMain, "depttype_select", "dp_reqdeposit.pbl", state.SsCoopControl, TofDept, AccType);
                    DwUtil.RetrieveDDDW(DwMain, "depttype_code_1", "dp_reqdeposit.pbl", null);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }


                decimal flag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                String memcoopid;
                if (flag == 1)
                {
                    memcoopid = HfCoopid.Value;
                }
                else
                {
                    memcoopid = state.SsCoopId;
                }
                DwMain.SetItemString(1, "memcoop_id", memcoopid);
                DwMain.SetItemDateTime(1, "deptopen_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
                String deptType = DwMain.GetItemString(1, "depttype_select");
                //DateTime dueDate = depService.GetDueDate(state.SsWsPass, deptType, state.SsWorkDate);

                DateTime dueDate = wcf.NDeposit.of_getduedate(state.SsWsPass, deptType, state.SsWorkDate);
                String tDueDate = dueDate.ToString("dd/MM/yyyy", WebUtil.TH);
                HdDueDate.Value = tDueDate;
                //String sql = " select deptgroup_code as deptGroup from dpdepttype where depttype_code = '" + deptType + "'";
                //DataTable dtDept = WebUtil.Query(sql);
                //if (dtDept.Rows.Count > 0)
                //{
                //    deptGrp = dtDept.Rows[0]["deptGroup"] != null ? dtDept.Rows[0]["deptGroup"].ToString().Trim() : "";
                //    if (deptGrp == "01" || deptGrp == "03")
                //    {
                //        DwMain.Modify("upint_time.Protect = 0");
                //        DwMain.Modify("upint_time.Background.Color = '16777215'");
                //        //DwMain.SetProperty("upint_time.Background.Color", "RGB(255, 255, 255)");
                //    }
                //    else
                //    {
                //        DwMain.Modify("upint_time.Protect = 1");
                //        DwMain.Modify("upint_time.Background.Color = '15132390'");
                //    }
                //}

                var booktype = DwMain.GetItemString(1, "depttype_select");
                DwUtil.RetrieveDDDW(DwMain, "deptpassbook_no", "dp_reqdeposit.pbl", state.SsCoopId, booktype);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-POSTBACK;
        private void JsPostMemberNo()
        {
            String memcoop_id;
            String deptType = DwUtil.GetString(DwMain, 1, "depttype_select");
            DwMain.SetItemString(1, "entry_id", state.SsUsername);
            DwMain.SetItemString(1, "recppaytype_code", "OCA");
            DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            DwMain.SetItemString(1, "machine_id", state.SsClientIp);
            memcoop_id = DwMain.GetItemString(1, "memcoop_id");
            short memberFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "member_flag"));
            membNo = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no"));
            DwMain.SetItemString(1, "member_no", membNo);

            // by Phai Edit Find 
            if (memberFlag == 1)
            {
                //if (HfReset.Value == "False")
                //{
                //membNo = WebUtil.BaseFormatMemberNo( TryDwMainGetString("member_no"));
                membNo = WebUtil.MemberNoFormat(TryDwMainGetString("member_no"));
                //}
                //else if (HfReset.Value == "True")
                //{
                //    membNo = HfMemno.Value;
                //}
            }

            // by dot edit
            string sqlchk = "select member_status from mbmembmaster where member_no = '" + membNo + "' and coop_id = '" + state.SsCoopControl + "'";
            DataTable dtchk = WebUtil.Query(sqlchk);
            String chk = dtchk.Rows[0]["member_status"].ToString().Trim();
            if (chk == "0" || chk == "-1" )
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกเลขที่ : " + membNo + " ได้ลาออกไปแล้ว");
                NewClear();
                return;
            }

            try
            {
                Hfbooktype.Value = deptType;
                //String membName = depService.GetNameMember(state.SsWsPass, membNo, memcoop_id, memberFlag);

                String membName = wcf.NDeposit.of_get_namemember(state.SsWsPass, membNo, memcoop_id, memberFlag);

                DwMain.SetItemString(1, "member_no", membNo);
                DwMain.SetItemString(1, "deptaccount_name", membName);
                DwMain.SetItemString(1, "condforwithdraw", membName + " แต่เพียงผู้เดียว");
                string sqlbook = "select min(Book_No) as mBN  from DPDEPTBOOKHIS,DPDEPTTYPE where DPDEPTBOOKHIS.Book_Status = '8' and DPDEPTBOOKHIS.Coop_Id = '" + state.SsCoopId + "' and (DPDEPTTYPE.BOOK_GROUP = DPDEPTBOOKHIS.BOOK_GRP) and DPDEPTTYPE.DEPTTYPE_CODE = '" + Hfbooktype.Value + "'";
                DataTable dtDeptb = WebUtil.Query(sqlbook);
                String bookn = dtDeptb.Rows[0]["mBN"].ToString().Trim();
                if (bookn != null)
                {
                    DwMain.SetItemString(1, "deptpassbook_no", bookn);

                }
                else
                {
                    DwMain.SetItemString(1, "deptpassbook_no", "");
                }

                String sql = " select deptgroup_code as deptGroup from dpdepttype where depttype_code = '" + deptType + "'";
                DataTable dtDept = WebUtil.Query(sql);
                if (dtDept.Rows.Count > 0)
                {
                    String deptGrp = dtDept.Rows[0]["deptGroup"] != null ? dtDept.Rows[0]["deptGroup"].ToString().Trim() : "";
                    if (membNo != "" && membNo != "CIF" && (deptGrp == "01" || deptGrp == "03"))
                    {
                        DwMain.Modify("upint_time.Protect = 0");
                        DwMain.Modify("upint_time.Background.Color = '16777215'");
                        DwMain.SetItemDecimal(1, "upint_time", 1);
                    }
                    else
                    {
                        DwMain.Modify("upint_time.Protect = 1");
                        DwMain.Modify("upint_time.Background.Color = '15132390'");
                    }
                }
            }
            catch (Exception)
            {
                DwMain.SetItemString(1, "member_no", "CIF");
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่สมาชิก " + membNo);
            }
            //String sql = "select member_no from mbmembmaster where member_no='" + membNo + "'";
            //DataTable dt = WebUtil.Query(sql);
            //if (dt.Rows.Count > 0)
            //{
            //String membNameFull = depService.GetNewAccountNames(state.SsWsPass, membNo);
            //String membCardPerson = depService.GetCardPerson(state.SsWsPass, membNo);
            //    DwMain.SetItemString(1, "member_no", membNo);
            //    DwMain.SetItemString(1, "deptaccount_name", membNameFull);
            //    DwMain.SetItemString(1, "condforwithdraw", membNameFull + " แต่เพียงผู้เดียว");
            //}
            //else
            //{
            //    DwMain.SetItemString(1, "member_no", "CIF");
            //    LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่สมาชิก " + membNo);
            //}

        }

        //JS-POSTBACK
        private void JsPostDwGainAddRow()
        {
            String acccont_type = DwMain.GetItemString(1, "acccont_type");

            if (acccont_type != "01")
            {
                DwGain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwGain, "prename_code", "dp_reqdeposit.pbl", null);
                SetDwGainSeq();
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("กรณีผูถือบัญชีเป็นเจ้าของคนเดียวไม่สามารถเพิ่มชื่อผู้ฝากร่วมได้");
            }
        }

        //JS-POSTBACK
        private void JsPostDwGainDelRow()
        {
            try
            {
                int row = int.Parse(HdDwGainCurrentRow.Value);
                DwGain.DeleteRow(row);
                SetDwGainSeq();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-POSTBACK
        private void JsPostGainMemberNo()
        {
            try
            {
                int row = int.Parse(HdDwGainCurrentRow.Value);
                String memNo = DwGain.GetItemString(row, "ref_id");
               // memNo = cmdService.BaseFormatMemberNo(state.SsWsPass, memNo);
                memNo = WebUtil.MemberNoFormat(memNo);   
                String sql = @" 
                SELECT
                    memb_name as NAME,
                    member_no as REF_ID,
                    memb_surname as SURNAME,
                    prename_code as PRENAME_CODE,
                    addr_no as HOUSE_NO,   
                    addr_moo as GROUP_NO,
                    addr_soi as SOI,
                    tambol_code as TUMBOL,
                    amphur_code as DISTRICT,
                    province_code as PROVINCE,
                    addr_phone as PHONE_NO,
                    addr_postcode as POST_CODE,
                    addr_road as ROAD,
                    coop_id
                FROM MBMEMBMASTER
                WHERE MEMBER_NO = '{1}'";
                sql = String.Format(sql, state.SsCoopId, memNo);
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String NAME = dt.Rows[0]["NAME"] != null ? dt.Rows[0]["NAME"].ToString().Trim() : "";
                    String REF_ID = dt.Rows[0]["REF_ID"] != null ? dt.Rows[0]["REF_ID"].ToString().Trim() : "";
                    String SURNAME = dt.Rows[0]["SURNAME"] != null ? dt.Rows[0]["SURNAME"].ToString().Trim() : "";
                    String PRENAME_CODE = dt.Rows[0]["PRENAME_CODE"] != null ? dt.Rows[0]["PRENAME_CODE"].ToString().Trim() : "";
                    String HOUSE_NO = dt.Rows[0]["HOUSE_NO"] != null ? dt.Rows[0]["HOUSE_NO"].ToString().Trim() : "";
                    String GROUP_NO = dt.Rows[0]["GROUP_NO"] != null ? dt.Rows[0]["GROUP_NO"].ToString().Trim() : "";
                    String SOI = dt.Rows[0]["SOI"] != null ? dt.Rows[0]["SOI"].ToString().Trim() : "";
                    String TUMBOL = dt.Rows[0]["TUMBOL"] != null ? dt.Rows[0]["TUMBOL"].ToString().Trim() : "";
                    String DISTRICT = dt.Rows[0]["DISTRICT"] != null ? dt.Rows[0]["DISTRICT"].ToString().Trim() : "";
                    String PROVINCE = dt.Rows[0]["PROVINCE"] != null ? dt.Rows[0]["PROVINCE"].ToString().Trim() : "";
                    String PHONE_NO = dt.Rows[0]["PHONE_NO"] != null ? dt.Rows[0]["PHONE_NO"].ToString().Trim() : "";
                    String POST_CODE = dt.Rows[0]["POST_CODE"] != null ? dt.Rows[0]["POST_CODE"].ToString().Trim() : "";
                    String ROAD = dt.Rows[0]["ROAD"] != null ? dt.Rows[0]["ROAD"].ToString().Trim() : "";
                    String COOP_ID = dt.Rows[0]["coop_id"] != null ? dt.Rows[0]["coop_id"].ToString().Trim() : "";
                    DwGain.SetItemString(row, "NAME", NAME);
                    DwGain.SetItemString(row, "REF_ID", REF_ID);
                    DwGain.SetItemString(row, "SURNAME", SURNAME);
                    DwGain.SetItemString(row, "PRENAME_CODE", PRENAME_CODE);
                    DwGain.SetItemString(row, "HOUSE_NO", HOUSE_NO);
                    DwGain.SetItemString(row, "GROUP_NO", GROUP_NO);
                    DwGain.SetItemString(row, "SOI", SOI);
                    DwGain.SetItemString(row, "TUMBOL", TUMBOL);
                    DwGain.SetItemString(row, "DISTRICT", DISTRICT);
                    DwGain.SetItemString(row, "PROVINCE", PROVINCE);
                    DwGain.SetItemString(row, "PHONE_NO", PHONE_NO);
                    DwGain.SetItemString(row, "POST_CODE", POST_CODE);
                    DwGain.SetItemString(row, "ROAD", ROAD);
                    DwGain.SetItemString(row, "COOP_ID", COOP_ID);
                    SetDwGainSeq();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostInsertRowCheque()
        {
            DwCheque.InsertRow(0);
            DwCheque.SetItemDateTime(DwCheque.RowCount, "cheque_date", state.SsWorkDate);
            DwCheque.SetItemDecimal(DwCheque.RowCount, "day_float", int.Parse(HdDayPassCheq.Value));
            tDwCheque.Eng2ThaiAllRow();
            HdIsInsertCheque.Value = "true";
        }

        //JS-EVENT
        private void JsPostBankCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String sql = "select bank_desc from cmucfbank where bank_code='" + bankCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String bankName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "bank_name", bankName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสธนาคาร " + bankCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostBankBranchCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String branchCode = DwCheque.GetItemString(row, "branch_code");
                String sql = "select branch_name from cmucfbankbranch where bank_code='" + bankCode + "' and branch_id='" + branchCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String branchName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "branch_name", branchName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสสาขาธนาคารเลขที่ " + branchCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostDeleteRowCheque()
        {
            try
            {
                DwCheque.DeleteRow(int.Parse(HdDwChequeRow.Value));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostTotalAmt()
        {
            //throw new NotImplementedException();
            //DwMain.setitem
        }

        //JS-EVENT
        private void JsPostDeptAccountNo()
        {
            try
            {
                String accNo;
                if (HfDeptaccount.Value != null)
                {
                    accNo = HfDeptaccount.Value;
                }
                else
                {
                    accNo = DwMain.GetItemString(1, "tran_deptacc_no");
                }
                //String newAccNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);

                String newAccNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, accNo);
                String viewAccNo = WebUtil.ViewAccountNoFormat( newAccNo);
                String sql = "select deptaccount_name from dpdeptmaster where deptaccount_no= '" + newAccNo + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwMain.SetItemString(1, "dept_tranacc_name", dt.Rows[0][0].ToString().Trim());
                    DwMain.SetItemString(1, "tran_deptacc_no", newAccNo);
                    DwMain.SetItemString(1, "tran_bankacc_no", newAccNo);
                    
                }
                else
                {
                    DwMain.SetItemString(1, "dept_tranacc_name", "");
                    throw new Exception("ไม่พบเลขที่บัญชี " + newAccNo);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostPostOffice()
        {
            //membgroup_code
            //membgroup_desc
            try
            {

                String postCode = DwMain.GetItemString(1, "tran_deptacc_no");

                String sql = "select membgroup_desc from mbucfmembgroup where membgroup_code = '" + postCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwMain.SetItemString(1, "dept_tranacc_name", dt.Rows[0][0].ToString().Trim());
                }
                else
                {
                    DwMain.SetItemString(1, "dept_tranacc_name", "");
                    throw new Exception("ไม่พบรหัสปณ. : " + postCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(ex);
            }
        }

        //INNER FUNCTION
        private void SetDwGainSeq()
        {
            for (int i = 1; i <= DwGain.RowCount; i++)
            {
                DwGain.SetItemDecimal(i, "seq_no", i);
                // DwUtil.RetrieveDDDW(DwGain, "prename_code", "dp_reqdeposit.pbl", null);
            }
        }

        //INNER FUNCTION
        private void LoopCheque()
        {
            try
            {
                for (int i = 1; i <= DwCheque.RowCount; i++)
                {
                    try
                    {
                        String chequeNo = DwUtil.GetString(DwCheque, i, "cheque_no", "");
                        completeCheque = chequeNo == "" ? false : completeCheque;
                        int ii = chequeNo == "" ? 0 : int.Parse(chequeNo);

                        if (ii > 0)
                        {
                            DwCheque.SetItemString(i, "cheque_no", ii.ToString("0000000"));
                        }
                        else
                        {
                            completeCheque = false;
                        }
                    }
                    catch { completeCheque = false; }
                }
            }
            catch { }
        }

        private void checkCoop()
        {

            decimal i = 0;
            decimal crossflag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
            if (crossflag == 1)
            {
                try
                {
                    i = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                }
                catch
                { }
                NewClear();
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopId, state.SsCoopControl);
            }
            else
            {
                try
                {
                    NewClear();
                    DwMain.SetItemString(1, "memcoop_id", state.SsCoopId);
                    HfCoopid.Value = state.SsCoopId + "";
                }
                catch
                { }
            }

        }

        //private void JsSetDefaultDeptNo()
        //{
        //    try
        //    {
        //        string sql = "select min( lpad( deptaccount_no, 9 ) ) as Deptno from dpdeptmaster where member_no ='" + DwMain.GetItemString(1, "member_no") + "'and depttype_code = '10' and deptclose_status = 0 ";
        //        DataTable dtDept = WebUtil.Query(sql);
        //        String deptno = dtDept.Rows[0]["Deptno"].ToString().Trim();
        //        sql = "select deptaccount_no,deptaccount_name from dpdeptmaster where deptaccount_no like '" + deptno + "%'";
        //        dtDept = WebUtil.Query(sql);
        //        deptno = dtDept.Rows[0]["deptaccount_no"].ToString().Trim();
        //        if (DwMain.GetItemString(1, "monthintpay_meth") == "1")
        //        {
        //            if (deptno != null || deptno != "")
        //            {
        //                String viewAccNo = depService.ViewAccountNoFormat(state.SsWsPass, deptno);
        //                DwMain.SetItemString(1, "tran_deptacc_no", viewAccNo);
        //                DwMain.SetItemString(1, "dept_tranacc_name", dtDept.Rows[0]["deptaccount_name"].ToString().Trim());

        //            }
        //        }
        //    }
        //    catch { }
        //}

        private void JsPostDeptPassbookNo()
        {
            String bookNo = DwMain.GetItemString(1, "deptpassbook_no");
            String sql = "select * from dpdeptbookhis where book_no = '" + bookNo + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                if (dt.GetInt32("book_status") != 8)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("เลขสมุด " + bookNo + " ได้ถูกใช้งานไปแล้ว (book_status = " + dt.GetInt32("book_status") + ")");
                }
                else if (state.SsCoopId != dt.GetString("coop_id"))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("เลขสมุด " + bookNo + " ลงทะเบียนไว้ต่างสาขา (" + dt.GetString("coop_id") + ")");
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบการลงทะเบียนเลขสมุด " + bookNo + "");
            }
        }

        private void JsBankname()
        {

            String tran_bankacc_no = DwMain.GetItemString(1, "tran_bankacc_no");
            String acc_name = @"select deptaccount_name from dpdeptmaster where deptaccount_no = '" + tran_bankacc_no + "'";
            Sdt acc_namedt = WebUtil.QuerySdt(acc_name);


            if (acc_namedt.Next())
            {
                DwMain.SetItemString(1, "dept_tranacc_name", acc_namedt.GetString("deptaccount_name"));
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบหมายเลขบัญชี " + tran_bankacc_no + "");
            }
        }
        #endregion
    }
}