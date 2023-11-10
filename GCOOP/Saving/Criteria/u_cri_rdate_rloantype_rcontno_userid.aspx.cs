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
using Sybase.DataWindow;

namespace Saving.Criteria
{
    public partial class u_cri_rdate_rloantype_rcontno_userid : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String checkFlag;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {  //op
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");

        }

        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            int flag_user;
       
            DwUtil.RetrieveDDDW(dw_criteria, "start_loantype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_loantype", "criteria.pbl", null);
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                
                //default values.
                //op
                dw_criteria.InsertRow(0);                

                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                string[] minmax = ReportUtil.GetMinMaxLoantype();
                //dw_criteria.SetItemString(1, "start_loantype", minmax[0]);
                //dw_criteria.SetItemString(1, "end_loantype", minmax[1]);
                dw_criteria.SetItemString(1, "start_loantype", "");//MiW
                dw_criteria.SetItemString(1, "end_loantype", "");//MiW
                //dw_criteria.SetItemDecimal(1, "fixentry_flag", 0);
                dw_criteria.SetItemString(1, "start_contno", "");
                dw_criteria.SetItemString(1, "end_contno", "");
                CheckFlag();//MiW
                tdw_criteria.Eng2ThaiAllRow();
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

            else if (eventArg == "checkFlag")
            {
                CheckFlag();
            }

        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
        private void CheckFlag()
        {
            //ให้หน้า page refresh
            int flag_user;
            try
            {
                flag_user = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "fixentry_flag"));
            }
            catch { flag_user = 1; }

            if (flag_user == 1)
            {
                dw_criteria.SetItemString(1, "select_userid", state.SsUsername);
            }
            else
            {
                dw_criteria.SetItemString(1, "select_userid", "");
            }

        }
        #endregion
        #region Report Process

        private void RunProcess()
        {
            String start_loantype = "", end_loantype = "", sqlStr = "";
            string[] minmax = ReportUtil.GetMinMaxLoantype();
            Sdt dt;

            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);

            start_loantype = dw_criteria.GetItemString(1, "start_loantype");
            end_loantype = dw_criteria.GetItemString(1, "end_loantype");
            

            String start_contno = dw_criteria.GetItemString(1, "start_contno");
            String end_contno = dw_criteria.GetItemString(1, "end_contno");
            String select_userid = dw_criteria.GetItemString(1, "select_userid");

            //baac บวก prefix กับเลขที่สัญญา
            if ((start_contno != "" && start_contno != null) && ((start_loantype != "" && start_loantype != null)))
            {
                sqlStr = "select prefix from lnloantype where coop_id = {0} and loantype_code = {1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, start_loantype);
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    start_contno = dt.GetString("prefix") + start_contno;
                }
            }
            if ((end_contno != "" && end_contno != null) && ((end_loantype != "" && end_loantype != null)))
            {
                sqlStr = "select prefix from lnloantype where coop_id = {0} and loantype_code = {1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, end_loantype);
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    end_contno = dt.GetString("prefix") + end_contno;
                }
            }

            //เช็คค่าว่าง
            if (start_loantype == "" || start_loantype == null)
            {
                start_loantype = minmax[0];
            }
            if (end_loantype == "" || end_loantype == null)
            {
                end_loantype = minmax[1];
            }

            if (start_contno == "" || start_contno == null) { start_contno = "0000000000"; }
            if (end_contno == "" || end_contno == null) { end_contno = "ฮฮฮฮฮฮฮฮฮฮ"; }
            if (select_userid == "" || select_userid == null) { select_userid = "%"; }

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_loantype, ArgumentType.String);
            lnv_helper.AddArgument(end_loantype, ArgumentType.String);
            lnv_helper.AddArgument(start_contno, ArgumentType.String);
            lnv_helper.AddArgument(end_contno, ArgumentType.String);
            lnv_helper.AddArgument(select_userid, ArgumentType.String);

            //----------------------------------------------------

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
        #endregion
    }
}
