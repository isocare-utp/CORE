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
using DataLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_coopid_rcontract_loantype_bycoopid : PageWebSheet, WebSheet
    {
        protected string jsPostset;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        public String outputProcess = "";
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            jsPostset = WebUtil.JsPostBack(this, "jsPostset");

            tdw_criteria = new DwThDate(dw_criteria, this);
        }

        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            DwUtil.RetrieveDDDW(dw_criteria, "loantype", "criteria.pbl", state.SsCoopControl);
            
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_criteria, "select_coop", "criteria.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "select_coop2", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "select_coop", state.SsCoopId);
                dw_criteria.SetItemString(1, "select_coop2", state.SsCoopId);
            }

            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "jsPostset")
            {
                Postset();
            }
        }

        public void SaveWebSheet()
        {
        }
        public void Postset()
        {
            string loan = dw_criteria.GetItemString(1,"loantype");
            if (loan == "10")
            {
                dw_criteria.SetItemString(1, "start_loancont", "ฉฉ60");
                dw_criteria.SetItemString(1, "end_loancont", "ฉฉ60");
            }
            else if (loan == "24")
            {
                dw_criteria.SetItemString(1, "start_loancont", "สห60");
                dw_criteria.SetItemString(1, "end_loancont", "สห60");
            }
            else
            {
                dw_criteria.SetItemString(1, "start_loancont", "สม60");
                dw_criteria.SetItemString(1, "end_loancont", "สม60");
            }
            dw_criteria.SetItemString(1, "loantype", loan);
        }
        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion
        #region Report Process

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String coop_id = dw_criteria.GetItemString(1, "select_coop");
            String coop_id2 = dw_criteria.GetItemString(1, "select_coop2");
            String start_loancont = dw_criteria.GetItemString(1, "start_loancont").Trim();
            String end_loancont = dw_criteria.GetItemString(1, "end_loancont").Trim();
            String loantype = dw_criteria.GetItemString(1, "loantype");
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(coop_id2, ArgumentType.String);
            lnv_helper.AddArgument(start_loancont, ArgumentType.String);
            lnv_helper.AddArgument(end_loancont, ArgumentType.String);
            lnv_helper.AddArgument(loantype, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
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
        #endregion

    }
}
