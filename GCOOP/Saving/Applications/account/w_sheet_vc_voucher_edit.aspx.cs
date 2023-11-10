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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เรียกใช้ service
using System.Threading;

namespace Saving.Applications.account
{
    public partial class w_sheet_vc_voucher_edit : PageWebSheet, WebSheet
    {
        private string is_sql;
        protected String postMoneyCode;
        private CultureInfo th;
        private DwThDate tdw_list;
        private DwThDate tdw_main;
        private DwThDate tdw_detail;
        private DwThDate tdw_date;
        private DwThDate tdw_search;
        private n_accountClient accService;//ประกาศตัวแปร WebService

        //ประกาศ JavaScript Postback
        protected String postSelectList;
        protected String postVoucherDate;
        protected String postW_dlg_Click;
        protected String postNewClear;
        protected String postSearchVoucher;
        protected String postPrint;

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        public String outputProcess = "";
        //JS-Event
        private void JspostSearchVoucher()
        {
            String vc_no = Dw_find.GetItemString(1,"voucher_no");

            try 
            { 
                int li_row = Dw_list.FindRow("voucher_no = '" +vc_no.Trim()+ "'",0,Dw_list.RowCount);
                if (li_row > 0)
                {
                    //retrieve Master Detail
                    try
                    {
                        String voucher_no = "";
                        voucher_no = Dw_list.GetItemString(li_row, "voucher_no").Trim();
                        String as_vcmas_xml = Dw_main.Describe("Datawindow.Data.Xml");
                        String as_vcdet_xml = Dw_detail.Describe("Datawindow.Data.Xml");
                        int result = accService.of_init_vcmastdet(state.SsWsPass, voucher_no, ref as_vcmas_xml, ref as_vcdet_xml);
                        if (result == 1)
                        {
                            Dw_main.Reset();
                            Dw_detail.Reset();
                            Dw_footer.Reset();
                            DwUtil.ImportData(as_vcmas_xml, Dw_main, tdw_main, FileSaveAsType.Xml);
                            //Dw_main.ImportString(as_vcmas_xml, FileSaveAsType.Xml);
                            DwUtil.ImportData(as_vcdet_xml, Dw_detail, tdw_detail, FileSaveAsType.Xml);
                            //Dw_detail.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                            DwUtil.ImportData(as_vcdet_xml, Dw_footer, tdw_detail, FileSaveAsType.Xml);
                            //Dw_footer.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                       }

                        Dw_list.SelectRow(0, false);
                        Dw_list.SelectRow(li_row, true );
                        Dw_list.SetRow(li_row);
                        


                    }
                    catch (SoapException ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    }
                 

                }
                else 
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายการเลขที่ voucher ที่ค้นหา");
                    Dw_find.Reset();
                    Dw_find.InsertRow(0);
                }
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            // ให้แสดงแต่ Voucher ที่ค้นหา
            String VcNo = vcno.Value.Trim();
            Dw_list.SetFilter("voucher_no = '" + VcNo + "'");
            Dw_list.Filter();
        
        }
        private void JspostNewClear()
        {
            Dw_date.Reset();
            Dw_date.InsertRow(0);
            Dw_date.SetItemDate(1, "voucher_date", state.SsWorkDate);
            DwUtil.RetrieveDDDW(Dw_date, "cash_type", "vc_voucher_edit.pbl", null);
            tdw_date.Eng2ThaiAllRow();
            
            Dw_list.Reset();
         

            Dw_main.Reset();
            Dw_main.InsertRow(0);
          

            Dw_detail.Reset();
            Dw_footer.Reset();
        
            Dw_find.Reset();
            Dw_find.InsertRow(0);

            lbl_moneybg.Text = "";
            lbl_moneyfw.Text = "";
            Panel1.Visible = false;

            JspostVoucherDate();
            
        }
        private void JspostSelectList()
        {
            try
            {
                String vcNo = HdVoucherNo.Value;
                String as_vcmas_xml = Dw_main.Describe("Datawindow.Data.Xml");;
                String as_vcdet_xml = Dw_detail.Describe("Datawindow.Data.Xml"); ;
                int result = accService.of_init_vcmastdet(state.SsWsPass, vcNo.Trim(), ref as_vcmas_xml, ref as_vcdet_xml);
                if (result == 1)
                {
                    Dw_main.Reset();
                    Dw_detail.Reset();
                    Dw_footer.Reset();
                    if ((as_vcmas_xml != "") || (as_vcmas_xml == null))
                    {

                        //Dw_main.ImportString(as_vcmas_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(as_vcmas_xml, Dw_main, tdw_main, FileSaveAsType.Xml);
                    }
                    if ((as_vcdet_xml != "") || (as_vcdet_xml == null))
                    {
                        //Dw_detail.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                        //Dw_footer.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(as_vcdet_xml, Dw_detail, tdw_detail, FileSaveAsType.Xml);
                        DwUtil.ImportData(as_vcdet_xml, Dw_footer, tdw_detail, FileSaveAsType.Xml);
                    }

                    
                }
                
                //try 
                //{
                //    int RowClickList = int.Parse(HdRowListClick.Value);
                //    Dw_list.SelectRow(0, false);
                //    Dw_list.SelectRow(RowClickList, true);
                //    Dw_list.SetRow(RowClickList);
                //}
                //catch 
                //{
                //    Dw_list.SelectRow(0, false);
                //    Dw_list.SelectRow(1, true);
                //    Dw_list.SetRow(1);
                //}
                
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostVoucherDate()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                DateTime VcDate = Dw_date.GetItemDate(1, "voucher_date");
                String wsPass = state.SsWsPass;
                String as_vclist_xml = Dw_list.Describe("Datawindow.Data.Xml");
                //short result = accService.GetVcListDay(state.SsWsPass, VcDate, ref as_vclist_xml);
                short result = wcf.NAccount.of_init_vclistday(state.SsWsPass, VcDate, ref as_vclist_xml);

                Decimal Begin = 0;
                Decimal Forward = 0;
                //Decimal[] CashBeginForward = accService.GetCashBeginForward(wsPass, VcDate, state.SsCoopId);
                //int CashBeginForward = accService.GetCashBeginForward(wsPass, VcDate, state.SsCoopId);
                Int32 CashBeginForward = wcf.NAccount.of_get_cash_bg_fw(wsPass, VcDate, state.SsCoopId, ref Begin, ref Forward);
                //int CashBeginForward = wcf.NAccount.of_get_cash_bg_fw(state.SsCoopControl, VcDate, state.SsCoopId, ref Begin, ref Forward);

                if (result == 1)
                {
                    Dw_list.Reset();
                    DwUtil.ImportData(as_vclist_xml, Dw_list, null, FileSaveAsType.Xml);

                    Dw_main.Reset();
                    Dw_detail.Reset();
                    Dw_footer.Reset();
                    Dw_main.InsertRow(0);

                    //ส่วนที่กำหนด จัดรูปแบบวันที่ให้กับ Dw_date
                    Dw_main.SetItemDate(1, "voucher_date", VcDate);
                    Dw_main.SetItemString(1, "voucher_tdate", VcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                    tdw_date.Eng2ThaiAllRow();

                    //lbl_moneybg.Text = Begin.ToString("#,##0.00");
                    //lbl_moneyfw.Text = Forward.ToString("#,##0.00");
                    lbl_moneybg.Text = Begin.ToString("#,##0.00");
                    lbl_moneyfw.Text = Forward.ToString("#,##0.00");
                    Panel1.Visible = true;
                    //   ส่งค่า Vc_no ให้ dw_main และ dw_detail retreive
                    if (Dw_list.RowCount > 0)
                    {
                        String listVcno = Dw_list.GetItemString(1, "voucher_no");
                        HdVoucherNo.Value = listVcno;
                        JspostSelectList();
                    }
                }
                else
                {
                    Dw_list.Reset();
                    Dw_main.Reset();
                    Dw_detail.Reset();
                    Dw_footer.Reset();
                    Dw_main.InsertRow(0);

                    Dw_main.SetItemDate(1, "voucher_date", VcDate);
                    Dw_main.SetItemString(1, "voucher_tdate", VcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                    tdw_date.Eng2ThaiAllRow();
                }
                Dw_find.Reset();
                Dw_find.InsertRow(0);
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }

            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostMoneyCode()
        {
            try
            {
                String vcNo = voucher_no_input.Value.Trim();
                String as_vcmas_xml = Dw_main.Describe("Datawindow.Data.Xml"); ;
                String as_vcdet_xml = Dw_detail.Describe("Datawindow.Data.Xml"); ;
                int result = accService.of_init_vcmastdet(state.SsWsPass, vcNo.Trim(), ref as_vcmas_xml, ref as_vcdet_xml);
                if (result == 1)
                {
                    Dw_main.Reset();
                    Dw_detail.Reset();
                    Dw_footer.Reset();
                    if ((as_vcmas_xml != "") || (as_vcmas_xml == null))
                    {
                        DwUtil.ImportData(as_vcmas_xml, Dw_main, tdw_main, FileSaveAsType.Xml);
                        //Dw_main.ImportString(as_vcmas_xml, FileSaveAsType.Xml);
                    }
                    if ((as_vcdet_xml != "") || (as_vcdet_xml == null))
                    {
                        //Dw_detail.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                        //Dw_footer.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(as_vcdet_xml, Dw_detail, tdw_detail, FileSaveAsType.Xml);
                        DwUtil.ImportData(as_vcdet_xml, Dw_footer, tdw_detail, FileSaveAsType.Xml);
                    }
                }

                //try 
                //{
                //    int RowClickList = int.Parse(HdRowListClick.Value);
                //    Dw_list.SelectRow(0, false);
                //    Dw_list.SelectRow(RowClickList, true);
                //    Dw_list.SetRow(RowClickList);
                //}
                //catch 
                //{
                //    Dw_list.SelectRow(0, false);
                //    Dw_list.SelectRow(1, true);
                //    Dw_list.SetRow(1);
                //}

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

            String VcNo = voucher_no_input.Value.Trim();
            Dw_list.SetFilter("voucher_no = '" + VcNo + "'");
            Dw_list.Filter();

        }

        #region WebSheet Members



        public void InitJsPostBack()
        {
            //ตั้งค่า JsPostBack
            postSelectList = WebUtil.JsPostBack(this, "postSelectList");
            postVoucherDate = WebUtil.JsPostBack(this, "postVoucherDate");
            postW_dlg_Click = WebUtil.JsPostBack(this, "postW_dlg_Click");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postSearchVoucher = WebUtil.JsPostBack(this, "postSearchVoucher");
            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postMoneyCode = WebUtil.JsPostBack(this, "postMoneyCode");

            //การเรียกใช้ DwThDate
            tdw_date = new DwThDate(Dw_date, this);
            tdw_date.Add("voucher_date", "voucher_tdate");

            tdw_main = new DwThDate(Dw_main, this);
            tdw_main.Add("voucher_date", "voucher_tdate");

          
        }

        //  พิมพ์ใบเสร็จหน้าลงรายวัน
        public void PopupReportshr()
        {
            Print();
            // Thread.Sleep(5000);
            Thread.Sleep(700);
           
            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }

        //  พิมพ์ใบเสร็จหน้าลงรายวัน
        private void Print()
        {

            app = state.SsApplication;
            try
            {
                gid = "acc_1_daily";
            }
            catch { }
            try
            {
                rid = "[ACD090]";
            }
            catch { }


            String voucher_no = Dw_main.GetItemString(1, "voucher_no");
            String account_id = "";
            String sql_txt = "select cash_account_code from accconstant";
            DataTable dt = WebUtil.Query(sql_txt);

            //if (dt.Rows.Count > 0)
            //{
            //    account_id = dt.Rows[0][0].ToString().Trim();
            //}
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(voucher_no, ArgumentType.String);
            //lnv_helper.AddArgument(account_id, ArgumentType.String);
         

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, "PDF");
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}

                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;

                //string printer = dw_criteria.GetItemString(1, "printer");
                //outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;
            // PopupReport();
        }



        public void WebSheetLoadBegin()
        {

            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
            th = new CultureInfo("th-TH");

            Dw_date.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);
            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);
            Dw_find.SetTransaction(sqlca);
            Dw_footer.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
                //Dw_date.InsertRow(0);
                //Dw_main.InsertRow(0);
                //Dw_find.InsertRow(0);
                //HdBranchId.Value = state.SsCoopId;
                //Panel1.Visible = false;
            }
            else
            {
                this.RestoreContextDw(Dw_date);
                this.RestoreContextDw(Dw_list);
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);
                this.RestoreContextDw(Dw_find);
                this.RestoreContextDw(Dw_footer);
              
            }
   

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSelectList")
            {
                JspostSelectList();
            }
            else if (eventArg == "postVoucherDate")
            {
                JspostVoucherDate();
            }
            else if (eventArg == "postW_dlg_Click") {
                JspostSelectList();
                DateTime   Vc_date = Dw_date.GetItemDateTime  (1, "voucher_date");

                Dw_list.Retrieve(Vc_date,state.SsCoopControl);
            }
            else if (eventArg == "postNewClear") {
                JspostNewClear();
            }
            else if (eventArg == "postSearchVoucher") {
                JspostSearchVoucher();
            }
            else if (eventArg == "postPrint")
            {
                PopupReportshr();
            }
            else if (eventArg == "postMoneyCode")
            {
                JspostMoneyCode();
            }
            
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            //ป้องกันการ load Datawindow เบิ้ล
            if (Dw_date.RowCount > 1)
            {
                Dw_date.DeleteRow(Dw_date.RowCount);
            }
            else if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);
            }
            else if (Dw_find.RowCount > 1)
            {
                Dw_find.DeleteRow(Dw_find.RowCount);
            }
                    


            Dw_main.SaveDataCache();
            Dw_list.SaveDataCache();
            Dw_detail.SaveDataCache();
            Dw_date.SaveDataCache();
            Dw_find.SaveDataCache();
            Dw_footer.SaveDataCache();
        }

        #endregion
    }
}
