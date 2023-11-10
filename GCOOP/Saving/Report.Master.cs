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
using System.Collections.Generic;
using System.Globalization;
using DataLibrary;

namespace Saving
{
    public partial class Report : System.Web.UI.MasterPage
    {
        private String titlePage;
        public String TitlePage
        {
            get { return titlePage; }
        }
        private String jsIncl;
        public String JsIncl
        {
            get { return jsIncl; }
        }
        protected WebStateFactory state;
        private String application;
        private String username;

        protected String siteLogo;
        protected String siteTName;
        protected String siteEName;
        protected String siteLinkName;
        protected String siteArgument;

        private String currentPage = null;
        private String app;
        private String gid;

        protected String backUrl = "";

        protected String PostRunReportOption;

        private WcfCalling wcf;

        public XmlConfigService xmlconfig;

        private bool IsWebSheet = true;

        private PageWeb pageWebReport;

        protected string urlSaving = "";
        protected string urlSavingCurrent = "";
        protected string urlSavingExtend = "";
        protected string winParameter = "";

        public String logoURL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            PnRetrieveReport.Visible = false;

            // ประกาศ xmlconfig
            xmlconfig = new XmlConfigService(WebUtil.GetGcoopPath());

            // ประกาศตัวแปร PageWebReport ถ้าเป็นค่าว่างให้หยุดการทำงาน
            try
            {
                pageWebReport = (PageWeb)ContentPlace.Page;
                pageWebReport.xmlconfig = xmlconfig;
                if (pageWebReport == null) return;
            }
            catch
            {
                pageWebReport = null;
                return;
            }

            // ประกาศใช้ WebState หากมีข้อผิดผลาดให้แจ้ง error และหยุดการทำงาน
            try
            {
                state = new WebStateFactory(pageWebReport, PageWebType.Report);
            }
            catch (Exception ex)
            {
                LtServerMessageFrame.Text = WebUtil.ErrorMessage(ex);
                ContentPlace.Page.Visible = false;
                return;
            }

            // ประกาศ wcf service
            wcf = new WcfCalling(xmlconfig);

            titlePage = state.SsApplicationName;

            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch
            {
                app = "";
            }

            if (app == null || app == "")
            {
                app = state.SsApplication;
            }

            try
            {
                gid = Request["gid"].ToString();
            }
            catch
            {
                gid = "";
            }

            //--- ตั้งค่า Javascript
            try
            {
                HApplication.Value = app;
                HUrl.Value = state.SsUrl;
                HCurrentPage.Value = state.CurrentPage;
            }
            catch { }

            String savingPath = state.SsUrl;
            jsIncl = "\n";
            jsIncl += "<script type=\"text/javascript\" src=\"" + savingPath + "js/DetectBrowser.js\"></script>\n";
            jsIncl += "<script type=\"text/javascript\" src=\"" + savingPath + "js/js.js\"></script>\n";
            jsIncl += "<script type=\"text/javascript\" src=\"" + savingPath + "js/WebState.js\"></script>\n";
            jsIncl += "<script type=\"text/javascript\" src=\"" + savingPath + "js/Gcoop.js\"></script>\n";

            // set ตัวแปร url path Saving ของโปรแกรม
            try
            {
                urlSaving = WebUtil.GetSavingUrlCore();
                urlSavingCurrent = WebUtil.GetSavingUrlCurrent();
                urlSavingExtend = WebUtil.GetSavingUrl();
            }
            catch
            {
                urlSaving = "";
                urlSavingCurrent = "";
                urlSavingExtend = "";
            }
            // หา winParameter
            String uri = Request.Url.AbsoluteUri;
            int indexOfUriApplications = uri.ToLower().IndexOf("/applications/");
            if (indexOfUriApplications >= 0)
            {
                String tmp = uri.Substring(uri.IndexOf("/Applications/") + 14);
                tmp = tmp.Substring(0, tmp.IndexOf("/"));
                winParameter = tmp;
            }
            if (String.IsNullOrEmpty(winParameter))
            {
                winParameter = state.SsApplication;
            }


            if (!IsPostBack && state.SsUsername != "")
            {
                MenuReport menu = new MenuReport();
                List<MenuReport> list = menu.GetMenuReport(pageWebReport.oracleTA, state.SsApplication, state.SsUsername,state.SsCoopId);
                RepeaterMenuReport.DataSource = list;
                RepeaterMenuReport.DataBind();
            }

            //--- Create Report Group Menus
            if (state.SsUsername != "")
            {
                setCurrentMenu(app, gid);
                setPageLabel();
            }

            // ตั้งค่า sitename sitelogo //
            //ImgSiteLogo.ImageUrl = state.SiteLogo;
            //ImgSiteLogo.ImageUrl = state.CoopLogo;//aek
            siteTName = state.SsCoopName;
            siteEName = state.SsCoopNameEng;
            if (string.IsNullOrEmpty(siteEName))
            {
                siteEName = siteTName;
            }
            siteLinkName = "";

            //SETWEBSHEET w_sheet..........
            IsWebSheet = SetWebSheetBegin();
            if (!IsWebSheet)
            {
                SetWebReportBegin();
            }

            try
            {
                // logo
                string logo = state.SsCoopLogo;
                if (string.IsNullOrEmpty(logo))
                {
                    logo = "Image/band_black.jpg";
                }

                //ถ้าเป็นหน้าปกติให้เปลี่ยน Logo
                ImgSiteLogo.ImageUrl = savingPath + "" + logo + "?ddmmyyyy=" + DateTime.Today.ToString("ddMMyyyy");
            }
            catch { }

        }

        protected void Page_LoadComplete()
        {
            if (pageWebReport == null) return;
            try
            {

                string savingUrl = state.SsUrl;
                string strq = "?qdt=" + DateTime.Today.ToString("yyyyMMdd");
                string jsCss = @"
    <link rel='stylesheet' type='text/css' media='screen,projection,print' href='{0}Css/layout_page.css{1}' />
    <link rel='stylesheet' type='text/css' media='screen,projection,print' href='{0}Css/layout_text.css{1}' />
    <link rel='stylesheet' type='text/css' media='screen,projection,print' href='{0}JsCss/DataSourceTool.css{1}' />
    <script src='{0}JsCss/jquery-1.8.3.min.js' type='text/javascript'></script>
    <script src='{0}JsCss/DatePicker.js{1}' type='text/javascript'></script>
    <script src='{0}Js/DetectBrowser.js{1}' type='text/javascript'></script>
    <script src='{0}Js/WebState.js{1}' type='text/javascript'></script>
    <script src='{0}Js/Js.js{1}' type='text/javascript'></script>
    <script src='{0}Js/Gcoop.js{1}' type='text/javascript'></script>
    <script src='{0}JsCss/DataSourceTool.js{1}' type='text/javascript'></script>";
                jsCss = jsCss.Replace("'", "\"");
                jsCss = string.Format(jsCss, savingUrl, strq);
                LtJsCss.Text = jsCss;
                SetWebSheetEnd();
                if (!IsWebSheet)
                {
                    SetWebReportEnd();
                }
            }
            catch { }
            try
            {
                backUrl = String.Format(WebUtil.GetSavingUrl() + "ReportDefault.aspx?app={0}&setApp={0}&gid={1}", state.SsApplication, Request["gid"]);
            }
            catch { }
            try
            {
                wcf.Close();
            }
            catch { }
            try
            {
                pageWebReport.PrepareResponse(true);
            }
            catch { }
            try
            {
                pageWebReport.oracleTA.Close();
            }
            catch { }
        }

        private void setPageLabel()
        {
            username = state.SsUsername;
            application = state.SsApplication;
            if (String.IsNullOrEmpty(state.CurrentPageName))
            {
                ltr_headmainpage.Text = state.SsApplicationName;
            }
            else
            {
                ltr_headmainpage.Text = state.SsApplicationName + "  -  <span style=\" font-size:14px;\">" + state.CurrentPageName + "</span>";
            }
            LbAppName.Text = "รายงาน " + state.SsApplicationName;
            LbWorkDateLoginBy.Text = "วันทำการ : " + state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " [ " + username + " ]";
        }

        private void setCurrentMenu(String app, String curr)
        {
            List<MenuReport> menu = new MenuReport().GetMenuReport(pageWebReport.oracleTA, app, state.SsUsername,state.SsCoopId);
            for (int i = 0; i < menu.Count; i++)
            {
                if (menu[i].GroupID.Equals(curr))
                {
                    currentPage = menu[i].GroupName;
                    ltr_headmainpage.Text += currentPage;
                }
            }
        }

        private bool SetWebSheetBegin()
        {
            //if (DdDatabaseTmp.Text != DdDatabase.SelectedValue.Trim()) {
            try
            {
                string sql = "select printer_name from cmprinter where printer_name not in ('PDF','XLS') order by printer_name asc";
                DataTable dt = WebUtil.Query(sql);
                DpPrinter.DataTextField = "printer_name";
                    DpPrinter.DataValueField = "printer_name";
                    DpPrinter.DataSource = dt;
                    DpPrinter.DataBind();
                    DpPrinter.SelectedValue = this.HdReportPrinter.Value;
            }
            catch { }

            WebSheet wSheet = null;
            PageWebSheet pwSheet = null;
            siteArgument = "";
            bool isWebSheet = false;
            try
            {
                wSheet = (WebSheet)ContentPlace.Page;
                isWebSheet = true;
                pwSheet = (PageWebSheet)ContentPlace.Page;
                pwSheet.wcf = this.wcf;
                if (pwSheet.WebSheetType != "Saving.PageWebSheet") return false;
                pwSheet.state = this.state;

                try
                {
                    Literal lt = ((Literal)ContentPlace.FindControl("LtServerMessage"));
                    if (lt == null) throw new Exception();
                    pwSheet.LtServerMessage = lt;
                    pwSheet.LtServerMessage.Text = "";
                }
                catch
                {
                    pwSheet.LtServerMessage = LtServerMessageFrame;
                    pwSheet.LtServerMessage.Text = "";
                }

                //---------------------------------------------------------------------------

                try
                {
                    pwSheet.GenerateJsPostBack();
                    wSheet.InitJsPostBack();
                }
                catch { }
                try
                {
                    wSheet.WebSheetLoadBegin();
                }
                catch { }
                String eventArg = "";
                try { eventArg = Request["__EVENTARGUMENT"]; }
                catch { }
                if (eventArg == "SaveWebSheet")
                {
                    //เตรียมเช็คสิทธิ์เซฟบรรทัดนี้อีกรอบ --------------------------------------------------------
                    if (!state.IsWritable)
                    {
                        pwSheet.LtServerMessage.Text = WebUtil.PermissionDeny(PermissType.WriteDeny);
                        return isWebSheet;
                    }
                    try
                    {
                        wSheet.SaveWebSheet();
                    }
                    catch { }
                }
                else
                {
                    //เช็ค Event JsPostBack
                    try { if (IsPostBack) wSheet.CheckJsPostBack(eventArg); }
                    catch { }
                }
            }
            catch { }
            return isWebSheet;
        }

        private void SetWebSheetEnd()
        {
            if (!IsWebSheet) return;
            WebSheet wSheet = null;
            PageWebSheet pwSheet = null;
            try
            {
                wSheet = (WebSheet)ContentPlace.Page;
                pwSheet = (PageWebSheet)ContentPlace.Page;
                if (pwSheet.WebSheetType != "Saving.PageWebSheet") return;
                try
                {
                    wSheet.WebSheetLoadEnd();
                }
                catch { }
            }
            catch { }
            try
            {
                for (int i = 0; i < pwSheet.binder.Count; i++)
                {
                    pwSheet.binder[i].HtmlJsControlBuild();
                }
            }
            catch { }
            try
            {
                pwSheet.DisConnectSQLCA();
            }
            catch { }
            try
            {
                LtDwThDateJavaScript.Text = pwSheet.SetDwThDateJavaScriptEvent();
            }
            catch { }
            try
            {
                pwSheet.SetOnLoadedScript();
            }
            catch { }

            logoURL = xmlconfig.SavPathOpenType1 + "/GCOOP/Saving/Image/band_black.jpg";
        }

        private void SetWebReportBegin()
        {
            WebReport wSheet = null;
            PageWebReport pwSheet = null;
            siteArgument = "";
            try
            {
                wSheet = (WebReport)ContentPlace.Page;
                pwSheet = (PageWebReport)ContentPlace.Page;
                pwSheet.wcf = this.wcf;
                pwSheet.state = this.state;
                PnRetrieveReport.Visible = true;
                iReportBuider.printerName = this.HdReportPrinter.Value;
                PostRunReportOption = WebUtil.JsPostBack(pwSheet, "PostRunReportOption");

                try
                {
                    Literal lt = ((Literal)ContentPlace.FindControl("LtServerMessage"));
                    if (lt == null) throw new Exception();
                    pwSheet.LtServerMessage = lt;
                    pwSheet.LtServerMessage.Text = "";
                }
                catch
                {
                    pwSheet.LtServerMessage = LtServerMessageFrame;
                    pwSheet.LtServerMessage.Text = "";
                }

                //---------------------------------------------------------------------------

                try
                {
                    pwSheet.GenerateJsPostBack();
                    wSheet.InitJsPostBack();
                }
                catch { }
                try
                {
                    wSheet.WebSheetLoadBegin();
                }
                catch { }

                String eventArg = "";
                try { eventArg = Request["__EVENTARGUMENT"]; }
                catch { }

                if (eventArg == "PostRunReportOption")
                {
                    try
                    {
                        pwSheet.LabelName = Request["rid"];
                        string sql = "select * from webreportdetail where group_id='" + gid + "' and report_id='" + pwSheet.LabelName + "'";
                        DataTable dt = WebUtil.Query(sql);
                        pwSheet.ReportId = dt.Rows[0]["report_dwobject"].ToString();
                        pwSheet.ReportName = dt.Rows[0]["report_name"].ToString();
                        pwSheet.ReportType = (ReportType)Enum.Parse(typeof(ReportType), HdReportOption.Value);
                        try
                        {
                            wSheet.RunReport();
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessageFrame.Text = WebUtil.ErrorMessage(ex);
                    }
                }
                else
                {
                    try { if (IsPostBack) wSheet.CheckJsPostBack(eventArg); }
                    catch { }
                }
            }
            catch { }
            return;
        }

        private void SetWebReportEnd()
        {
            WebReport wSheet = null;
            PageWebReport pwSheet = null;
            try
            {
                wSheet = (WebReport)ContentPlace.Page;
                pwSheet = (PageWebReport)ContentPlace.Page;
                if (pwSheet.WebSheetType != "Saving.PageWebSheet") return;
                try
                {
                    wSheet.WebSheetLoadEnd();
                }
                catch { }
            }
            catch { }
            try
            {
                for (int i = 0; i < pwSheet.binder.Count; i++)
                {
                    pwSheet.binder[i].HtmlJsControlBuild();
                }
            }
            catch { }
            try
            {
                pwSheet.DisConnectSQLCA();
            }
            catch { }
            try
            {
                LtDwThDateJavaScript.Text = pwSheet.SetDwThDateJavaScriptEvent();
            }
            catch { }
            try
            {
                pwSheet.SetOnLoadedScript();
            }
            catch { }

        }
    }
}