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
    public partial class u_cri_branchid_rdepttype_rdate_recppaytype_userid_netamt : PageWebSheet, WebSheet
    {

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        #region WebSheet Members

        public void InitJsPostBack()
        {  //op
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("select_date", "select_tdate");


        }

        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "branch_id", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "start_dp_type", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_dp_type", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "item_type", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "user_id", "criteria.pbl", null);
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                //op
                dw_criteria.InsertRow(0);

                dw_criteria.SetItemDateTime(1, "select_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "branch_id", state.SsCoopId);
                string[] minmax = ReportUtil.GetMinMaxDepttype();
                dw_criteria.SetItemString(1, "start_dp_type", minmax[0]);
                dw_criteria.SetItemString(1, "end_dp_type", minmax[1]);
                dw_criteria.SetItemString(1, "user_id", "0");
                dw_criteria.SetItemString(1, "item_type", " ");
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
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {

            //String start_tdate = dw_criteria.GetItemString(1, "start_tdate");
            //String end_tdate = dw_criteria.GetItemString(1, "end_tdate");
            //start_tdate = start_tdate.Replace("/", "");
            //end_tdate = end_tdate.Replace("/", "");
            //DateTime sdate = DateTime.ParseExact(start_tdate, "ddMMyyyy", WebUtil.TH);
            //DateTime edate = DateTime.ParseExact(end_tdate, "ddMMyyyy", WebUtil.TH);
            String select_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "select_tdate", null);
            String start_dp_type = dw_criteria.GetItemString(1, "start_dp_type");
            String end_dp_type = dw_criteria.GetItemString(1, "end_dp_type");
            String item_type = dw_criteria.GetItemString(1, "item_type");
            String user_id = dw_criteria.GetItemString(1, "user_id");
            string[] rappayminmax = ReportUtil.GetMinMaxRappaytype();
            string[] userminmax = ReportUtil.GetMinMaxUsertype();
            String end_userid = user_id;
            Decimal netamt = dw_criteria.GetItemDecimal(1, "netamt");

            if (user_id == "0")
            {
                user_id = userminmax[0];
                end_userid = userminmax[1];
            }

            String end_itemtype = item_type;
            if (item_type == "ฮฮฮ" || item_type == " ")
            {
                item_type = rappayminmax[0];
                end_itemtype = rappayminmax[1];
            }




            //String coop_name = state.SsCoopName;

            tdw_criteria.Eng2ThaiAllRow();


            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(select_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_dp_type, ArgumentType.String);
            lnv_helper.AddArgument(end_dp_type, ArgumentType.String);
            lnv_helper.AddArgument(item_type, ArgumentType.String);
            lnv_helper.AddArgument(end_itemtype, ArgumentType.String);
            lnv_helper.AddArgument(user_id, ArgumentType.String);
            lnv_helper.AddArgument(end_userid, ArgumentType.String);
            lnv_helper.AddArgument(netamt.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(coop_name, ArgumentType.String);



            //----------------------------------------------------

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
        #endregion
    }
}
