using System;
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
using Sybase.DataWindow;
using CoreSavingLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_coopid_membno_caldate : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postmembno;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postmembno = WebUtil.JsPostBack(this, "postmembno");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("select_date", "select_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            

            if (!IsPostBack)
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "select_date", state.SsWorkDate.AddMonths(2));
                dw_criteria.SetItemString(1, "select_tdate", "");
                //dw_criteria.SetItemString(1, "accname", state.SsUsername);
                tdw_criteria.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(dw_criteria);
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
            else if (eventArg == "postmembno")
            {
                membno();
            }
            //else if (eventArg == "checkFlag")
            //{
            //    String as_startmem = "";
            //    try
            //    {
            //        as_startmem = dw_criteria.GetItemString(1, "start_membno");
            //        as_startmem = WebUtil.MemberNoFormat(as_startmem.Trim());
            //        dw_criteria.SetItemString(1, "start_membno",as_startmem);
            //    }
            //    catch
            //    {
            //        as_startmem = "";
            //    }
            //    DwUtil.RetrieveDDDW(dw_criteria, "loancont", "criteria.pbl", state.SsCoopControl, as_startmem);
            //}
        }
        public void membno()
        {
            string membno = dw_criteria.GetItemString(1, "start_membno");
            membno = WebUtil.MemberNoFormat(membno.Trim());
            dw_criteria.SetItemString(1, "start_membno", membno);
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //tdw_criteria.Thai2EngAllRow();
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String membno = dw_criteria.GetItemString(1, "start_membno");
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "select_tdate", null);
            String accname = dw_criteria.GetItemString(1, "accname");
            String manager = dw_criteria.GetItemString(1, "manager");
            
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(membno, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(accname, ArgumentType.String);
            lnv_helper.AddArgument(manager, ArgumentType.String);

            //----------------------------------------------------------------

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

                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
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
        private string Convert(string p)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
