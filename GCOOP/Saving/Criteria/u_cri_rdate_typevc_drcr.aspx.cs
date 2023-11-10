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
    public partial class u_cri_rdate_typevc_drcr : PageWebSheet, WebSheet
    {
        private String app;
        private String gid;
        private String rid;
        private DwThDate tdw_criteria;
        protected String outputReport;
        protected String checkFlag;

        #region WebSheet Members

        public void InitJsPostBack()
        {  //op
            outputReport = WebUtil.JsPostBack(this, "outputReport");
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
            //DwUtil.RetrieveDDDW(dw_criteria, "start_loantype", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "end_loantype", "criteria.pbl", null);
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                //op
                dw_criteria.InsertRow(0);

                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate.AddYears(-1));
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                //string[] minmax = ReportUtil.GetMinMaxLoantype();
                //dw_criteria.SetItemString(1, "start_loantype", minmax[0]);
                //dw_criteria.SetItemString(1, "end_loantype", minmax[1]);
                //dw_criteria.SetItemDecimal(1, "fixentry_flag", 1);
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
        {  //op
            if (eventArg == "outputReport")
            {
                OutputReport();
            }
            else if (eventArg == "checkFlag")
            {
                CheckFlag();
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
        private void CheckFlag()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "select_userid", null);

        }
        #endregion
        //op
        private void OutputReport()
        {
            //String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_date", null);
            String start_date = dw_criteria.GetItemString(1, "start_date").Trim();
            String start_tdate = dw_criteria.GetItemString(1, "start_tdate").Trim();
            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_date", null);
            String end_date = dw_criteria.GetItemString(1, "end_date").Trim();
            String end_tdate = dw_criteria.GetItemString(1, "end_tdate").Trim();
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            String select_drcr = dw_criteria.GetItemString(1, "sele_drcr").Trim();
            String type_group = dw_criteria.GetItemString(1, "type_group").Trim();

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_tdate, ArgumentType.String);
            lnv_helper.AddArgument(end_tdate, ArgumentType.String);
            lnv_helper.AddArgument(getCashAccountId("000"), ArgumentType.String);
            lnv_helper.AddArgument(select_drcr, ArgumentType.String);
            lnv_helper.AddArgument(type_group, ArgumentType.String);

            //----------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            String pdfURL = "";
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //int li_return = lws_report.ReportPDF(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                ///*LtServerMessage.Text = "<b>ReportPDF</b> return(" + Convert.ToString(li_return) + @") <br>
                //<b>criteria:</b><code> " + criteriaXML + @"</code><br>
                //<b>app:</b> " + app + @"<br>
                //<b>gid:</b>" + gid + @"<br>
                //<b>rid:</b>" + rid + @"<br>
                //<b>pdf:</b>" + pdfFileName;*/
                //if (1 != li_return)
                //{
                //    //throw new Exception("สร้างรายงานไม่สำเร็จ");
                //}
                //pdfURL = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            //LtServerMessage.Text = "PDF Created: <a href='"+pdfURL+"'>"+pdfURL+"</a>";

            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdfURL + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        private String getCashAccountId(String branch_id)
        {
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String tmp = "";
                tmp = @"select cash_account_code
                        from accconstant
                        where coop_registered_no =" + branch_id.Trim();
                Sdt dt = ta.Query(tmp);
                tmp = dt.Rows[0]["cash_account_code"].ToString();
                ta.Close();
                return tmp;
            }
            catch
            {
                return "00000000";
            }
        }

    }
}
