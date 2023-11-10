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
using CoreSavingLibrary.WcfNKeeping;
using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfPrint;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_mrc_receive_cutting : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;

        public string pbl = "kp_mrc_receive_cutting.pbl";
        protected String postProcStatus;
        protected String getXml;
        protected String postProcType;
        protected String postInitReport;
        //=====================
        private n_keepingClient kpService;

        private n_commonClient commonService;
        protected String chgProcDate;
        public string outputProcess;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInitReport = WebUtil.JsPostBack(this, "postInitReport");
            postProcStatus = WebUtil.JsPostBack(this, "postProcStatus");
            getXml = WebUtil.JsPostBack(this, "getXml");
            chgProcDate = WebUtil.JsPostBack(this, "chgProcDate");
            postProcType = WebUtil.JsPostBack(this, "postProcType");

        }

        public void WebSheetLoadBegin()
        {
            kpService = wcf.NKeeping;
            commonService = wcf.NCommon;
            HdRunProcess.Value = "false";
            HdOpenIFrame.Value = "false";
            this.ConnectSQLCA();
            Dw_Report.SetTransaction(sqlca);

            //--- Page Arguments
            try
            {
                app = "keeping";
            }
            catch { }

            try
            {
                gid = "KEEPING_CHECK";
            }
            catch { }
            try
            {
                rid = "KEEPING_CHECK03";
            }
            catch { }


            if (!IsPostBack)
            {
                if (DwMain.RowCount < 1)
                {
                    B_Print.Visible = false;

                    DwMain.InsertRow(0);
                    DwMain.SetItemString(1, "coop_id", state.SsCoopControl);
                    DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    DateTime last = commonService.of_lastdayofmonth(state.SsWsPass, state.SsWorkDate);
                    DwMain.SetItemDecimal(1, "receive_year", last.Year + 543);
                    DwMain.SetItemDecimal(1, "receive_month", last.Month);
                    if (state.SsCoopControl == "023001")
                    {
                        DwMain.SetItemDecimal(1, "proc_type", 4);
                    }
                }
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(Dw_Report);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postProcStatus")
            {

            }

            else if (eventArg == "postProcType")
            {
                DwMain.SetItemString(1, "memb_text", "");
                DwMain.SetItemString(1, "group_text", "");
                DwMain.SetItemString(1, "mem_text", "");
            }
            else if (eventArg == "getXml")
            {
                if (DwMain.GetItemString(1, "coop_id") == "010001")
                {
                    if (DwMain.GetItemDecimal(1, "other_status") == 1)
                        DwMain.SetItemDecimal(1, "insurefire_status", 1);
                }

                String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
                //เรียกเว็บเซอร์วิสประมวลผลตัดยอด
                CallWSRunPostProcess(xml_tmp);
            }
            else if (eventArg == "postInitReport")
            {
                JspostInitReport();
            }
        }


        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            Dw_Report.SaveDataCache();
        }

        #endregion

        private void CallWSRunPostProcess(string xml)
        {
            try
            {
                outputProcess = WebUtil.runProcessing(state, "KEEPINGPOST", xml, "", "");
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }
        }
        protected void cb_post_Click(object sender, EventArgs e)
        {
            String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
            //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
            CallWSRunPostProcess(xml_tmp);
        }

        private void JspostInitReport()
        {
            try
            {
                String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
                str_keep_proc astr_keep_proc = new str_keep_proc();
                astr_keep_proc.xml_option = xml_tmp;
                int result = kpService.of_init_rcv_post(state.SsWsPass, ref astr_keep_proc);
                if (result == 1)
                {
                    Dw_Report.Reset();
                    Dw_Report.SetSqlSelect(astr_keep_proc.sqlrpt_desc);
                    //Dw_Report.Retrieve();
                    DwUtil.RetrieveDataWindow(Dw_Report, "kp_mrc_receive_cutting.pbl", null, null);
                    B_Print.Visible = true;
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        protected void B_Print_Click(object sender, EventArgs e)
        {
            if (Dw_Report.RowCount > 0)
            {
                RunProcess();
            }
        }
        private void RunProcess()
        {
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            decimal year = DwMain.GetItemDecimal(1, "receive_year");
            decimal month = DwMain.GetItemDecimal(1, "receive_month");
            decimal number = 0; string ls_number = "";
            if (state.SsCoopControl == "023001")
            {
                number = DwMain.GetItemDecimal(1, "receive_number");
                ls_number = number.ToString();
            }
            string ls_month = month.ToString();

            if (ls_month.Length != 2)
            {
                ls_month = "0" + ls_month;
            }
            if (state.SsCoopControl == "023001")
            {
                if (ls_number.Length != 2)
                {
                    ls_number = "0" + ls_number;
                }
            }
            string recv_period = "";
            if (state.SsCoopControl == "023001")
            {
                recv_period = year.ToString() + ls_month + ls_number;
            }
            else
            {
                recv_period = year.ToString() + ls_month ;
            }
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(recv_period, ArgumentType.String);
            //----------------------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
        }
        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

    }
}
