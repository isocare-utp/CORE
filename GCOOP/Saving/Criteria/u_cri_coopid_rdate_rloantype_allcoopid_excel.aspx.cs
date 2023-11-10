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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;
using Sybase.DataWindow;
using System.Data.OracleClient;

namespace Saving.Criteria
{
    public partial class u_cri_coopid_rdate_rloantype_allcoopid_excel : PageWebSheet, WebSheet
    {
        private n_commonClient commonService;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");

            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            try
            {
                commonService = wcf.NCommon;
            }
            catch { LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ WebService ไม่ได้"); }

            InitJsPostBack();


            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "start_tdate", "");
                dw_criteria.SetItemString(1, "end_tdate", "");
                dw_criteria.SetItemString(1, "start_entry", "");
                dw_criteria.SetItemString(1, "end_entry", "");
                DwUtil.RetrieveDDDW(dw_criteria, "select_coop", "criteria.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "endselect_coop", "criteria.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "start_loantype", "criteria.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "end_loantype", "criteria.pbl", null);
                dw_criteria.SetItemString(1, "select_coop", state.SsCoopId);
                dw_criteria.SetItemString(1, "endselect_coop", state.SsCoopId);
                //ดึงค่าน้อยสุด และค่ามากสุด ของประเภทเงินกู้ มาเก็บไว้ในตัวแปร Array ชื่อ minmax
                string[] minmax = ReportUtil.GetMinMaxLoantype();
                dw_criteria.SetItemString(1, "start_loantype", minmax[0]);
                dw_criteria.SetItemString(1, "end_loantype", minmax[1]);
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
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
        #region Report Process

        private void RunProcess()
        {
            //ออก Report เป็น Excel
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String coop_id = dw_criteria.GetItemString(1, "select_coop");
            String endcoop_id = dw_criteria.GetItemString(1, "endselect_coop");
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String start_loantype = dw_criteria.GetItemString(1, "start_loantype");
            String end_loantype = dw_criteria.GetItemString(1, "end_loantype");

            try
            {
                String filename = "Report_Excel" + ".xls";
                str_rptexcel astr_rptexcel = new str_rptexcel();
                astr_rptexcel.as_path = WebUtil.PhysicalPath + "Saving//filecommon//" + filename;
                astr_rptexcel.as_dwobject = "r_shrlon_lnreqloan_coopid_spec_excel";
                astr_rptexcel.as_argument01 = "S" + coop_id;
                astr_rptexcel.as_argument02 = "D" + start_date;
                astr_rptexcel.as_argument03 = "D" + end_date;
                astr_rptexcel.as_argument04 = "S" + start_loantype;
                astr_rptexcel.as_argument05 = "S" + end_loantype;
                astr_rptexcel.as_argument06 = "S" + endcoop_id;
                int result = commonService.of_dwexportexcel_rpt(state.SsWsPass,ref astr_rptexcel);
                LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่  <a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายงานตามเงื่อนไขที่ระบุ กรุุณาทำรายการใหม่");
                //JspostNewClear();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

            ////แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            //ReportHelper lnv_helper = new ReportHelper();
            //lnv_helper.AddArgument(coop_id, ArgumentType.String);           
            //lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            //lnv_helper.AddArgument(start_loantype, ArgumentType.String);
            //lnv_helper.AddArgument(end_loantype, ArgumentType.String);
            //lnv_helper.AddArgument(endcoop_id, ArgumentType.String);
            ////lnv_helper.AddArgument(start_entry, ArgumentType.String);
            ////lnv_helper.AddArgument(end_entry, ArgumentType.String);
            ////ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            //String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            //pdfFileName += "_" + gid + "_" + rid + ".pdf";
            //pdfFileName = pdfFileName.Trim();
            ////ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            //try
            //{
            //    //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
            //    //String criteriaXML = lnv_helper.PopArgumentsXML();
            //    //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
            //    String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
            //    if (li_return == "true")
            //    {
            //        HdOpenIFrame.Value = "True";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //    return;
            //}
        }
        public void PopupReport()
        {
            ////เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('" + pdf + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
        #endregion

    }
}
