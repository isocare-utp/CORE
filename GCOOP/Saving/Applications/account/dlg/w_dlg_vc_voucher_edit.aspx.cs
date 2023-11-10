using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_vc_voucher_edit : PageWebDialog, WebDialog 
    {
        private DwThDate tDwMain;
        protected String postAccountID;
        protected String postInsertDwDetail; //insert แถว
        protected String postDropdownAccid; // ชื่อ Account_id ขึ้นอัตโนมัติ
        protected String postInsertAfterDwDetail; // แทรกแถว
        protected String postAccountSide;
        protected String postVoucherType;
        protected String postVoucherEdit;
        protected String postRefresh;
        protected String postAddnewUpdateVoucher;
        protected String postDeleteRowDetail;
        protected String postKeyAccId;
        protected String postKeyAccSearch;
        protected String postDwAccidClick;
        protected String postDrCr;
        protected String postNew;
        protected String postGetTemplate;
        protected String jsPostTempate;
        //protected String postSetitemCashType;

        private n_accountClient accService; //ประกาศเสมอ
        String pbl = "vc_voucher_edit.pbl";
        private string is_sql;
        int DataRow = 0;
        //========================================

        private void JsPostDrCr()
        {
            int RowAfter = int.Parse(HdCurrentrow.Value);
            Dw_detail.SetItemDecimal(RowAfter, "cr_amt", 0);
            Dw_detail.SetItemDecimal(RowAfter, "dr_amt", 0);
        }

        private void JspostNew()
        {
            Dw_main.Reset();
            Dw_detail.Reset();
            tDwMain = new DwThDate(Dw_main);
            tDwMain.Add("voucher_date", "voucher_tdate"); // วันที่ภาษาอังกฤษและวันที่ภาษาไทย

            try
            {
                Dw_main.InsertRow(0);
                Dw_search.InsertRow(0);
                Dw_accid.Retrieve(state.SsCoopControl);
                Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                //Dw_main.SetItemString(1, "vctype_branch_id", state.SsCoopId);
                Dw_main.SetItemDecimal(1, "post_toacc", 0);


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = ex.ToString();
            }

            String queryStrVcDate = "";
            try { queryStrVcDate = Request["vcDate"].Trim(); }
            catch { }
            if (queryStrVcDate != "")
            {
                DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));
                if (vcDate.Year > 1370)
                {
                    // การ set วันที่ด้วย code behind
                    Dw_main.SetItemDate(1, "voucher_date", vcDate);
                    Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                }
            }

            if (Dw_detail.RowCount <= 0)
            {
                String b_type = "";
                try { b_type = Request["b_type"].Trim(); }
                catch { }
                if (b_type == "JV")
                {
                    Dw_main.SetItemString(1, "voucher_type", "JV");
                    Dw_main.SetItemDecimal(1, "cash_type", 2);
                }
                else if (b_type == "PV")
                {
                    Dw_main.SetItemString(1, "voucher_type", "PV");
                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                }
                else if (b_type == "RV")
                {
                    Dw_main.SetItemString(1, "voucher_type", "RV");
                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                }
                else if (b_type == "AV")
                {
                    Dw_main.SetItemString(1, "voucher_type", "AV");
                    Dw_main.SetItemDecimal(1, "cash_type", 3);
                }

            }
        }


        private void JspostDwAccidClick()
        {
            int li_row = Dw_detail.FindRow("isnull( account_id ) or account_id = '' ", 0, Dw_detail.RowCount);
            String account_id = "";
            String account_name = "";
            account_id = HdAccid.Value.Trim();
            account_name = HdAccname.Value.Trim();

            if (li_row < 1)
            {
                li_row = Dw_detail.InsertRow(0);
            }

            Dw_detail.SetItemString(li_row, "account_id", account_id);
            Dw_detail.SetItemString(li_row, "account_name", account_name);
            Dw_detail.SetItemString(li_row, "coop_id", state.SsCoopControl);


            if (state.SsCoopControl == "006001")
            {

                String item_desc = "";
                try
                {
                    item_desc = Dw_main.GetItemString(1, "voucher_desc");
                }
                catch
                {
                    item_desc = "";
                }
                Dw_detail.SetItemString(li_row, "item_desc", item_desc);
            }
        }


        private void JspostKeyAccSearch()
        {
            string ls_acc_id, ls_sqltext, ls_acc_name;

            ls_sqltext = "";

            try
            {
                ls_acc_id = Dw_search.GetItemString(1, "account_id");
            }
            catch
            {
                ls_acc_id = "";
            }
            try
            {
                ls_acc_name = Dw_search.GetItemString(1, "account_name");
            }
            catch
            {
                ls_acc_name = "";
            }

            //===
            if (ls_acc_id.Length > 0)
            {
                ls_sqltext += "( ACCMASTER.ACCOUNT_ID like  '" + ls_acc_id + "%')";
            }
            if (ls_acc_name.Length > 0)
            {
                if (ls_sqltext != "")
                {
                    ls_sqltext += " and  ";
                }
                ls_sqltext += "( ACCMASTER.ACCOUNT_NAME like '%" + ls_acc_name + "%') ";
            }


            if (ls_sqltext == null)
            {
                ls_sqltext = "";
            }

            //ls_temp = is_sql + ls_sqltext   ;
            //HSqlTemp.Value = ls_temp;
            //Dw_accid.SetSqlSelect(ls_temp);

            Dw_accid.Retrieve(state.SsCoopControl);
            Dw_accid.SetFilter(ls_sqltext);
            Dw_accid.Filter();

            Dw_search.InsertRow(0);
            if (Dw_accid.RowCount < 1)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ยังไม่พบข้อมูลรหัสบัญชี/ชื่อบัญชีที่ค้นหา");
                Dw_accid.Reset();
                Dw_search.Reset();
                Dw_search.InsertRow(0);
            }
            if (Dw_accid.RowCount == 1)
            {
                String account_id = Dw_accid.GetItemString(Dw_accid.RowCount, "account_id");
                String account_name = Dw_accid.GetItemString(Dw_accid.RowCount, "account_name");
                HdAccid.Value = account_id;
                HdAccname.Value = account_name;
                JspostDwAccidClick();
            }
        }

        private void JspostKeyAccId()
        {
            try 
            {
                int rowdetail = int.Parse(Hd_accid.Value);
                String Acc_id = Dw_detail.GetItemString(rowdetail, "account_id").Trim();
                int RowAccId = Dw_accid.FindRow("account_id = '" + Acc_id + "'", 0, Dw_accid.RowCount);
                if (RowAccId > 0)
                {
                    String Acc_name = Dw_accid.GetItemString(RowAccId, "account_name").Trim();
                    Dw_detail.SetItemString(rowdetail, "account_name", Acc_name);
                }
                else 
                {
                    Dw_detail.SetItemString(rowdetail, "account_id", "");
                    //LtVoucerMessage.Text = WebUtil.WarningMessage("ใสรหัสบัญชีไม่ถูกต้อง กรุณาใสรหัสบัญชีใหม่อีกครั้ง");  //TKS
                    LtServerMessage.Text = WebUtil.ErrorMessage("ใสรหัสบัญชีไม่ถูกต้อง กรุณาใสรหัสบัญชีใหม่อีกครั้ง");  //FSCT
                }
            }
            catch (Exception ex)
            { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void GetTemplate()
        {
            string sqlcount = @"SELECT * FROM vcautomas";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            DataRow = count.GetRowCount();
            if (DataRow > 0)
            {
                HdOpenIFrame.Value = "True";
            }
        }

        private void PostTempate()
        {
            string vcauto_code = Hdtemp_code.Value;
            string vcauto_desc = Hdtemp_desc.Value;
            string vcauto_type = Hdtemp_type.Value;
            try
            {
                Dw_main.SetItemString(1, "voucher_type", vcauto_code.Substring(0, 2));
                Dw_main.SetItemString(1, "voucher_desc", vcauto_desc);
                if (vcauto_type == "JV")
                {
                    Dw_main.SetItemDecimal(1, "cash_type", 2);
                }
                else if (vcauto_type == "PV")
                {
                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                }
                else if (vcauto_type == "RV")
                {
                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                }

                string sqlcount = @"SELECT vc.account_side, vc.account_id, acc.account_name FROM vcautodet vc, accmaster acc where vc.account_id=acc.account_id and vcauto_code = '" + vcauto_code + "' ORDER BY vc.seq_no";
                Sdt count = WebUtil.QuerySdt(sqlcount);
                DataRow = count.GetRowCount();

                for (int i = 0; i < DataRow; i++)
                {
                    Dw_detail.InsertRow(0);
                    Dw_detail.SetItemString(Dw_detail.RowCount, "account_side", count.Rows[i][0].ToString());
                    Dw_detail.SetItemString(Dw_detail.RowCount, "account_id", count.Rows[i][1].ToString());
                    Dw_detail.SetItemString(Dw_detail.RowCount, "account_name", count.Rows[i][2].ToString());
                    Dw_detail.SetItemString(Dw_detail.RowCount, "coop_id", state.SsCoopId);
                    Hdrow.Value = Dw_detail.RowCount.ToString();
                }
            }
            catch { }
        }

        

        private void JsPostAccountID()
        {

        }

        private void JsPostRefresh()
        {
            //String vc_no = Dw_main.GetItemString(1, "voucher_no");
            //Dw_detail.Retrieve(vc_no, state.SsCoopControl);
            //JsPostDrCr();
        }

        private void JspostDeleteRowDetail()
        {
            Int16 row = Convert.ToInt16(HdDetailRow.Value);
            Dw_detail.DeleteRow(row);
        }

        //j-event Add New Update Voucher
        private void JspostAddnewUpdateVoucher()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                //การ ส่งค่าข้อมูลให้กับตัวแปรให้ครบ
                Dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                Dw_main.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                Dw_main.SetItemString(1, "system_code", "ACC");
                Dw_main.SetItemString(1, "entry_id", state.SsUsername);
                //Dw_detail.UpdateData();

                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                String xmlDw_main = Dw_main.Describe("Datawindow.Data.Xml");
                String xmlDw_detail = Dw_detail.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                String coop_id = state.SsCoopId;
                //เรียกใช้ webservice
                //int result = accService.AddNewUpdateVoucher(wsPass, xmlDw_main, xmlDw_detail);
                int result = wcf.NAccount.of_savevoucher(wsPass, xmlDw_main, xmlDw_detail);
                HdIsFinished.Value = "true";
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                    //HdIsFinished.Value = "true";
                    JspostNew();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่สามารถบันทึกรายการได้");
                    HdIsFinished.Value = "false";
                }

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                HdIsFinished.Value = "false";
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                HdIsFinished.Value = "false";
            }
        }

        // J-คลิกปุ่ม แก้ไขดึงข้อมูลมา show
        private void JsPostVoucherEdit()
        {
            try
            {

                String queryStrVcDate = "";
                try { queryStrVcDate = Request["vcDate"].Trim(); }
                catch { }
                String ls_xmlmain = "", ls_xmldet = "";
                ////vocher_no
                accService = wcf.NAccount;// ประกาศเสมอ
                String vcNo = Dw_main.GetItemString(1, "voucher_no");
                int vcMasDet = accService.of_init_vcmastdet(state.SsWsPass, vcNo, ref ls_xmlmain, ref ls_xmldet);
                Dw_main.Reset();
                Dw_detail.Reset();

                //Dw_main.ImportString(ls_xmlmain, FileSaveAsType.Xml);
                DwUtil.ImportData(ls_xmlmain, Dw_main, tDwMain, FileSaveAsType.Xml);
                //Dw_detail.ImportString(ls_xmldet, FileSaveAsType.Xml);
                DwUtil.ImportData(ls_xmldet, Dw_detail, tDwMain, FileSaveAsType.Xml);

                DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));
                Dw_main.SetItemDate(1, "voucher_date", vcDate);
                Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));


            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            //JavaScript PostBack
            postAccountID = WebUtil.JsPostBack(this, "postAccountID");
            postInsertDwDetail = WebUtil.JsPostBack(this, "postInsertDwDetail");
            postDropdownAccid = WebUtil.JsPostBack(this, "postDropdownAccid");
            postInsertAfterDwDetail = WebUtil.JsPostBack(this, "postInsertAfterDwDetail");
            postAccountSide = WebUtil.JsPostBack(this, "postAccountSide");
            postVoucherType = WebUtil.JsPostBack(this, "postVoucherType");
            postVoucherEdit = WebUtil.JsPostBack(this, "postVoucherEdit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postAddnewUpdateVoucher = WebUtil.JsPostBack(this, "postAddnewUpdateVoucher");
            postDeleteRowDetail = WebUtil.JsPostBack(this, "postDeleteRowDetail");
            postKeyAccId = WebUtil.JsPostBack(this, "postKeyAccId");
            postKeyAccSearch = WebUtil.JsPostBack(this, "postKeyAccSearch");
            postDwAccidClick = WebUtil.JsPostBack(this, "postDwAccidClick");
            postDrCr = WebUtil.JsPostBack(this, "postDrCr");
            postGetTemplate = WebUtil.JsPostBack(this, "postGetTemplate");
            jsPostTempate = WebUtil.JsPostBack(this, "jsPostTempate");
            //postSetitemCashType = WebUtil.JsPostBack(this, "postSetitemCashType");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA(); 

            accService = wcf.NAccount;//ประกาศ new
            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);
            Dw_accid.SetTransaction(sqlca);
            Dw_search.SetTransaction(sqlca);

            is_sql = Dw_accid.GetSqlSelect();

            tDwMain = new DwThDate(Dw_main);
            tDwMain.Add("voucher_date", "voucher_tdate"); // วันที่ภาษาอังกฤษและวันที่ภาษาไทย

            try
            {
                if (!IsPostBack)
                {
                    try
                    {
                        Dw_main.InsertRow(0);
                        Dw_search.InsertRow(0);
                        Dw_accid.Retrieve(state.SsCoopControl);
                        HSqlTemp.Value = is_sql;

                        //Dw_main.SetItemString(1, "branch_id", state.SsCoopId);
                        //Dw_main.SetItemString(1, "vctype_branch_id", state.SsCoopId);
                        //Dw_main.SetItemDecimal(1, "post_toacc", 0);

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = ex.ToString();
                    }

                   try
                    {
                        String queryStrVcDate = "";
                        String queryStrVcNo = "";
                        String B_status = "";
                        try { queryStrVcDate = Request["vcDate"].Trim(); }
                        catch { }

                        try { queryStrVcNo = Request["vcNo"].Trim(); }
                        catch { }

                        try { B_status = Request["B_status"].Trim(); }
                        catch { }

                        if (B_status == "New")
                        {
                            //GetTemplate();
                            if (Dw_detail.RowCount <= 0)
                            {
                                String b_type = "";
                                try { b_type = Request["b_type"].Trim(); }
                                catch { }
                                if (b_type == "JV")
                                {
                                    Dw_main.SetItemString(1, "voucher_type", "JV");
                                    Dw_main.SetItemDecimal(1, "cash_type", 2);
                                }
                                else if (b_type == "PV")
                                {
                                    Dw_main.SetItemString(1, "voucher_type", "PV");
                                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                                }
                                else if (b_type == "RV")
                                {
                                    Dw_main.SetItemString(1, "voucher_type", "RV");
                                    Dw_main.SetItemDecimal(1, "cash_type", 1);
                                }
                                else if (b_type == "AV")
                                {
                                    Dw_main.SetItemString(1, "voucher_type", "AV");
                                    Dw_main.SetItemDecimal(1, "cash_type", 3);
                                }

                            }
                        }

                        if (queryStrVcNo != "")
                        {
                            Dw_main.SetItemString(1, "voucher_no", queryStrVcNo);

                            JsPostVoucherEdit();
                        }

                        else if (queryStrVcDate != "")
                        {
                            DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));

                            if (vcDate.Year > 1370)
                            {
                                // การ set วันที่ด้วย code behind
                                Dw_main.SetItemDate(1, "voucher_date", vcDate);
                                Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = ex.ToString();
                    }

                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                    this.RestoreContextDw(Dw_detail);
                    this.RestoreContextDw(Dw_accid);
                    this.RestoreContextDw(Dw_search);
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = ex.ToString();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallSheetLoadComplete", "SheetLoadComplete()", true);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postAccountID")
            {
                //  JsPostAccountID();
            }
            else if (eventArg == "postNew")
            {
                JspostNew();
            }
            //else if (eventArg == "postSetitemCashType")
            //{
            //    JspostSetitemCashType();
            //}
            else if (eventArg == "postInsertDwDetail")
            {
                Dw_detail.InsertRow(Dw_detail.RowCount + 1);              
                Dw_detail.SetItemString(Dw_detail.RowCount, "coop_id", state.SsCoopControl);

                if (state.SsCoopControl == "006001")
                {

                    String item_desc = "";
                    try
                    {
                        item_desc = Dw_main.GetItemString(1, "voucher_desc");
                    }
                    catch
                    {
                        item_desc = "";
                    }
                    Dw_detail.SetItemString(Dw_detail.RowCount, "item_desc", item_desc);
                }
                         
            }
            else if (eventArg == "postAddnewUpdateVoucher")
            {
                JspostAddnewUpdateVoucher();
            }        
            else if (eventArg == "postInsertAfterDwDetail")
            {
                try
                {
                    //แปลงจาก string เป็น int
                    int RowAfter = int.Parse(HdCurrentrow.Value);

                    //ให้แทรกแถวบวก 1 เข้าไป
                    Dw_detail.InsertRow(RowAfter);
                    Dw_detail.SetItemString(RowAfter, "coop_id", state.SsCoopControl);

                    if (state.SsCoopControl == "006001")
                    {

                        String item_desc = "";
                        try
                        {
                            item_desc = Dw_main.GetItemString(1, "voucher_desc");
                        }
                        catch
                        {
                            item_desc = "";
                        }
                        Dw_detail.SetItemString(RowAfter, "item_desc", item_desc);
                    }
                  //  Dw_accid.Retrieve();
                }
                catch { }
            }
            else if (eventArg == "postAccountSide")
            {
                //สั่งให้ dr_amt หรือ cr_amt มีค่าเป็น 0.00 เมื่อเลือกอีกฝั่งหนึ่ง
                int RowAfter = -1;
                string AccountSide;

                try
                {
                    //แปลงจาก string เป็น int
                    RowAfter = int.Parse(HdCurrentrow.Value);
                    //ส่งค่าให้กับตัวแปร AccountSide ว่าตอนนี้กำลังเลือกฝั่ง Dr หรือ Cr
                    AccountSide = Dw_detail.GetItemString(RowAfter, "account_side");
                    if (AccountSide == "DR")
                    {
                        Dw_detail.SetItemDecimal(RowAfter, "cr_amt", 0);
                    }
                    else if (AccountSide == "CR")
                    {
                        Dw_detail.SetItemDecimal(RowAfter, "dr_amt", 0);
                    }
                }
                catch { }
            }
            else if (eventArg == "postVoucherType")
            {
                // code อยู่ใน webdialogloadend             
            }
            else if (eventArg == "postVoucherEdit")
            {
                JsPostVoucherEdit();
            }
            else if (eventArg == "postDeleteRowDetail")
            {
                Int16 RowDetail = Convert.ToInt16(HdDetailRow.Value);
                Dw_detail.DeleteRow(RowDetail);
                //JsPostDrCr();
                
            }
            else if (eventArg == "postKeyAccId")
            {
                JspostKeyAccId();
            }
            else if (eventArg == "postKeyAccSearch") {
                JspostKeyAccSearch();
            }
            else if (eventArg == "postDwAccidClick") {
                JspostDwAccidClick();
            }
            else if (eventArg == "postGetTemplate")
            {
                GetTemplate();
            }
            else if (eventArg == "jsPostTempate")
            {
                PostTempate();
            }
            else if (eventArg == "postDrCr") {
                JsPostDrCr();
            }
            else if (eventArg == "postRefresh")
            {
                JsPostRefresh();
                //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
        }

        public void WebDialogLoadEnd()
        {
            String queryB_Statue = "";
            try { queryB_Statue = Request["B_status"].Trim(); }
            catch { }


            if (queryB_Statue == "New")
            {
                try
                {

                }
                catch { }
            }
             if (queryB_Statue == "Edit")
            {
                try
                {
                        Dw_detail.Modify("account_side.Protect='0'");
                        Dw_detail.Modify("compute_4.Visible=true");
                        Dw_detail.Modify("t_4.Visible=true");
                   
                }
                catch { }
            }

           
            
            Dw_accid.Retrieve(state.SsCoopControl);
          

            if (Dw_search.RowCount > 1)
            {
                Dw_search.DeleteRow(Dw_search.RowCount);
            }

            Dw_accid.SaveDataCache();
            Dw_search.SaveDataCache();
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
            
        }

        #endregion

    }
}
