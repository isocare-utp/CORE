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
using CoreSavingLibrary.WcfNCommon;
using Saving.CustomControl;
using DataLibrary;
using System.Text;
using System.IO;
using EncryptDecryptEngine;

namespace Saving
{
    public partial class Frame : System.Web.UI.MasterPage
    {
        protected String downloadPatch = "", restartIreportBuilder = "";
        protected String postCleanXmlJavaAppletFromFrameMaster;
        protected String urlSaving = "";
        protected String urlSavingCurrent = "";
        protected String urlSavingExtend = "";
        protected String absoluteFullPath = "";
        protected String isConfirmOnNew = "false";
        public XmlConfigService xmlconfig = null;
        protected String currentApp = "";
        protected String currentPage = "";
        protected String currentUser = "";
        protected String currentConn = "";
        protected String currentProcessing = "";
        protected String isSheetWebDataInFolder = "false";
        protected String winParameter = "";
        private WebStateFactory state;
        private MenuBar menu;
        private DateTime dtTime;
        private String serverMessage = "";
        private String serverMessageType = "N";
        private WcfCalling wcf;
        private PageWebSheet pageWebSheet = null;
        private bool databaseConnected = false;

        public string selectedPrinter
        {
            get
            {
                return state.SsSelectedPrinter;
            }
        }
        public string FrameContext
        {
            get
            {
                return "t=" + state.SsTokenId + "&d=" + (Encryption.UrlFormat(state.SsConnectionString));
            }
        }
        protected void DpAutoExpendSessionTextChanged(object sender, EventArgs e)
        {

        }
        public string VirtualDir
        {
            get
            {
                return WebUtil.GetVirtualDirectory();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string restartIreport = Request["r"];
            try
            {
                if (restartIreport != null && restartIreport == "1")
                {
                    WebUtil.StartIreportBuilder(true);
                    LtServerMessageFrame.Text = " ทำการ Restart Ireport Builder สำเร็จ ";
                }
                else
                {
                    WebUtil.StartIreportBuilder(false);
                }
            }
            catch { }

            // ตั้งค่าตัวแปรเอาไว้จับเวลาต่อ 1 request
            dtTime = DateTime.Now;

            // ประกาศ xmlconfig
            xmlconfig = new XmlConfigService(WebUtil.GetGcoopPath());

            // ประกาศตัวแปร PageWebSheet ถ้าเป็นค่าว่างให้หยุดการทำงาน
            try
            {
                pageWebSheet = (PageWebSheet)ContentPlace.Page;
                pageWebSheet.xmlconfig = xmlconfig;
                if (pageWebSheet == null) return;
            }
            catch
            {
                pageWebSheet = null;
                return;
            }

            // ประกาศใช้ WebState หากมีข้อผิดผลาดให้แจ้ง error และหยุดการทำงาน
            try
            {
                state = new WebStateFactory(pageWebSheet, PageWebType.Sheet);
            }
            catch (Exception ex)
            {
                LtServerMessageFrame.Text = WebUtil.ErrorMessage(ex);
                ContentPlace.Page.Visible = false;
                return;
            }

            // ประกาศ wcf service
            wcf = new WcfCalling(xmlconfig);

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
                try
                {
                    currentUser = state.SsUsername;
                    XmlService xs = new XmlService(WebUtil.GetGcoopPath());
                    XmlConnection xc = xs.GetXmlConnection(state.SsConnectionIndex);
                    currentConn = xc.ConnectionString;
                }
                catch { }
            // logo
            string logo = state.SsCoopLogo;
            if (string.IsNullOrEmpty(logo))
            {
                logo = "Image/band_black.jpg";
            }

            //ถ้าเป็นหน้าปกติให้เปลี่ยน Logo
            Image1.ImageUrl = urlSaving + "" + logo + "?ddmmyyyy=" + DateTime.Today.ToString("ddMMyyyy");

            //เริ่มกระบวนการเกี่ยวกับโหลด WebSheet
            try
            {
                SetWebSheetBegin();
            }
            catch { }
        }

        protected void Page_LoadComplete()
        {
            if (pageWebSheet == null) return;
            String timming = "";
            try
            {
                string savingUrl = state.SsUrl;
                string strq = "?qdt=" + DateTime.Today.ToString("yyyyMMdd");
                String css = "";
                css += "\n\t<link type=\"text/css\" rel=\"stylesheet\" href=\"" + savingUrl + "JsCss/jquery-ui.css\" />";
                css += "\n\t<link type=\"text/css\" rel=\"stylesheet\" href=\"" + savingUrl + "JsCss/FrameLayout.css" + strq + "\" />";
                css += "\n\t<link type=\"text/css\" rel=\"stylesheet\" href=\"" + savingUrl + "JsCss/PageWeb.css" + strq + "\" />";
                css += "\n\t<link type=\"text/css\" rel=\"stylesheet\" href=\"" + savingUrl + "JsCss/DataSourceTool.css" + strq + "\" />\n";
                String js = "";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/jquery-1.10.2.min.js\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/jquery-ui.js\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/DatePicker.js" + strq + "\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/JsScriptFrame.js" + strq + "\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/DataSourceTool.js" + strq + "\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/AjaxCall.js" + strq + "\"></script>\n";
                js += "\n\t<script language=\"javascript\" src=\"" + savingUrl + "JsCss/XmlDataSourceTool.js" + strq + "\"></script>\n";
                LtJScriptFrame.Text = css + js;
            }
            catch { }

            try
            {
                DeleteTempFile();
            }
            catch { }

            try
            {
                this.currentApp = state.SsApplication;

                this.currentPage = state.CurrentPage;

                SetWebSheetEnd();
            }
            catch { }

            // สร้างเมนู
            try
            {
                menu = new MenuBar(state, xmlconfig);
            }
            catch
            {
                menu = null;
            }
            if (menu != null)
            {
                MenuBarControl1.LoadBegin(menu);
                MenuSubControl1.LoadBegin(menu);
                String pageName = state.CurrentPageName;
                head.Page.Title = (pageName == "" ? menu.GroupName : pageName) + " - ICOOP Isocare Systems.";
                LbSiteNameThai.Text = state.SsCoopName;
                LbSiteNameEnglish.Text = state.SsCoopNameEng;
                LbSystemAndPage.Text = state.SsApplicationName + "" + (pageName == "" ? "" : " &nbsp; - &nbsp; " + pageName);
            }

            try
            {
                String savUrl = state.SsUrl + "DownloadFiles.aspx";
                downloadPatch = "<a href=\"" + savUrl + "\">{0}Download Files{1}</a>";
                downloadPatch = string.Format(downloadPatch, "<font color=\"blue\">", "</font>");
            }
            catch { }

            try
            {
                String savUrl = state.SsUrl + "Default.aspx?r=1";
                restartIreportBuilder = "<a href=\"" + savUrl + "\">{0}Restart Ireport Builder{1}</a>";
                restartIreportBuilder = string.Format(restartIreportBuilder, "<font color=\"blue\">", "</font>");
            }
            catch { }

            try
            {
                pageWebSheet.PrepareResponse(databaseConnected);
            }
            catch { }

            try
            {
                wcf.Close();
            }
            catch { }

            try
            {
                pageWebSheet.oracleTA.Close();
            }
            catch { }

            try
            {
                timming = TopBarControl1.LoadBegin(state, xmlconfig, dtTime, databaseConnected);
            }
            catch { }

            // Loging -----------------------------------------------------------------

            Logging(timming);

            try
            {
                OpenWorkdate(state.SsApplication);
            }
            catch (Exception ex) { 
                LtServerMessageFrame.Text = WebUtil.ErrorMessage(ex.Message); 
            }
        }

        private void RedirectToDomain()
        {
            if (xmlconfig.SavDomainRedirect)
            {
                String protocal = Request.Url.Scheme;
                String domain = Request.Url.Authority;
                int port = Request.Url.Port;

                String savProtocal = xmlconfig.SavProtocol;
                String savDomain = xmlconfig.SavDomain;
                int savPort = xmlconfig.SavPort;

                absoluteFullPath = Request.Url.AbsoluteUri;

                if (protocal != savProtocal || domain != savDomain || port != savPort)
                {
                    String sPort;
                    if (savProtocal == "http" && savPort == 80)
                    {
                        sPort = "";
                    }
                    else if (savProtocal == "https" && savPort == 443)
                    {
                        sPort = "";
                    }
                    else
                    {
                        sPort = ":" + savPort;
                    }
                    String urlAb = savProtocal + "://" + savDomain + "" + sPort + "/" + xmlconfig.SavPathPattern;
                    Response.Redirect(urlAb);
                }
            }
        }

        private void Logging(String timming)
        {
            int logStatus = 0;
            String logMessage = "";
            try
            {
                if (xmlconfig.CentLogUsing)
                {
                    Sta ta = new Sta(xmlconfig.CentLogConnectionString);
                    try
                    {
                        String svIp = "'" + System.Net.Dns.GetHostAddresses(Request.Url.Host)[0].ToString() + "'";
                        String clIp = "'" + state.SsClientIp + "'";
                        String app = "'" + state.SsApplication + "'";
                        String coId = "'" + state.SsCoopId + "'";
                        String coCt = "'" + state.SsCoopControl + "'";
                        String pName = "'" + state.CurrentPage + "'";
                        String pDesc = "'" + state.CurrentPageName + "'";
                        String user = string.IsNullOrEmpty(state.SsUsername) ? "''" : "'" + state.SsUsername + "'";
                        String url = "'" + Request.Url.AbsoluteUri + "'";
                        String meth = IsPostBack ? "'POST'" : "'GET'";
                        String jsPB = IsPostBack ? "'" + Request["__EVENTARGUMENT"] + "'" : "''";
                        String ws = "NULL";
                        String wsr = "'" + System.Net.Dns.GetHostAddresses(xmlconfig.WsrDomain)[0].ToString() + "'";
                        String tmpMsg = serverMessage;
                        if (!string.IsNullOrEmpty(tmpMsg))
                        {
                            tmpMsg = tmpMsg.Replace("'", "\\'");
                            tmpMsg = tmpMsg.Replace("\"", "\\'");
                        }
                        else
                        {
                            tmpMsg = "";
                        }
                        String svMs = "'" + tmpMsg + "'";
                        String svMsT = "'" + serverMessageType + "'";
                        decimal time = 0;
                        try
                        {
                            //"'" + timming + "'";
                            time = Convert.ToDecimal(timming);
                        }
                        catch { }
                        String ssId = "'" + Session.SessionID + "'";
                        String sqlCentLog = @"
                        insert into hitlog
                        (
                            hit_date,               server_ip,              client_ip,
                            application,            coop_id,                coop_control,
                            current_page,           current_pagedesc,       message_type,
                            username,               url_absolute,           methode, 
                            jspostback,             webservice,             webservice_ram,
                            webservicereport,       webservicereport_ram,   server_message,
                            load_time,              session_id
                        )
                        values
                        (
                            now(),                  " + svIp + @",          " + clIp + @",
                            " + app + @",           " + coId + @",          " + coCt + @",
                            " + pName + @",         " + pDesc + @",         " + svMsT + @",
                            " + user + @",          " + url + @",           " + meth + @",
                            " + jsPB + @",          " + ws + @",            " + 0 + @",
                            " + wsr + @",           " + 0 + @",             " + svMs + @",
                            " + time + @",          " + ssId + @"
                        )";
                        logStatus = ta.Exe(sqlCentLog);
                        ta.Close();
                    }
                    catch (Exception ex)
                    {
                        logStatus = -1;
                        ta.Close();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                logMessage = ex.Message;
            }
            if (logStatus > 0)
            {
                logMessage = "เปิดการใช้งาน Log";
            }
            else if (logStatus == 0)
            {
                logMessage = "ปิดการใช้งาน Log";
            }
            TopBarControl1.SetLog(logStatus, logMessage);
        }

        private void SetWebSheetBegin()
        {
            try
            {
                this.HdReportPrinter.Value = this.selectedPrinter;
                string sql = "select printer_name from cmprinter where printer_name not in ('PDF','XLS') order by printer_name asc";
                DataTable dt = WebUtil.Query(sql);
                DpPrinter.DataTextField = "printer_name";
                DpPrinter.DataValueField = "printer_name";
                DpPrinter.DataSource = dt;
                DpPrinter.DataBind();
                DpPrinter.SelectedValue = this.HdReportPrinter.Value;

                this.DpAutoExpendSession.SelectedValue = state.SsExpendSession.ToString();
            }
            catch { }
            WebSheet wSheet = null;
            PageWebSheet pwSheet = null;
            LtDwThDateJavaScript.Text = "";
            try
            {
                wSheet = (WebSheet)ContentPlace.Page;
                pwSheet = (PageWebSheet)ContentPlace.Page;
                pwSheet.wcf = this.wcf;

                this.pageWebSheet = pwSheet;
                if (pwSheet.WebSheetType != "Saving.PageWebSheet") return;

                pwSheet.state = this.state;
                pwSheet.xmlconfig = this.xmlconfig;

                try
                {
                    string url = Request.Url.AbsoluteUri;
                    string pageName = state.CurrentPage.Replace(".aspx", "");
                    if (url.IndexOf("/" + pageName + "_ctrl/") > 0)
                    {
                        isSheetWebDataInFolder = "true";
                    }
                }
                catch { }

                try
                {
                    Literal lt = ((Literal)ContentPlace.FindControl("LtServerMessage"));
                    if (lt == null) throw new Exception();
                    pwSheet.LtServerMessage = lt;
                    pwSheet.LtServerMessage.Text = "";
                }
                catch
                {
                    try
                    {
                        pwSheet.LtServerMessage = LtServerMessageFrame;
                        pwSheet.LtServerMessage.Text = "";
                    }
                    catch { }
                }

                //---------------------------------------------------------------------------

                try
                {
                    String ajaxCmd = Request["xmd"];
                    if (!String.IsNullOrEmpty(ajaxCmd))
                    {
                        pwSheet.CallAjaxPostBack(ajaxCmd);
                    }
                }
                catch { }


                try
                {
                    pwSheet.GenerateJsPostBack();
                    wSheet.InitJsPostBack();
                }
                catch { }


                if (!pwSheet.IgnoreReadable)
                {
                    if (!state.IsReadable)
                    {
                        LtServerMessageFrame.Text = WebUtil.PermissionDeny(PermissType.ReadDeny);
                        pwSheet.Visible = false;
                        return;
                    }
                }

                if (pwSheet.FullScreen)
                {
                    tdMenuLeft.Visible = false;//.Style.Value = "height: 400px; vertical-align: top; text-align: left;";
                    tdMenuRight.Style.Value = "width: 100%; vertical-align: top; text-align: left;";
                }

                try
                {
                    wSheet.WebSheetLoadBegin();
                }
                catch (Exception ex)
                {
                    //pwSheet.ErrorMessage(ex);
                }
                String eventArg = "";
                try { eventArg = Request["__EVENTARGUMENT"]; }
                catch { }
                if (eventArg == "SaveWebSheet")
                {
                    //เตรียมเช็คสิทธิ์เซฟบรรทัดนี้อีกรอบ --------------------------------------------------------
                    if (!state.IsWritable)
                    {
                        //state.LogAct(state.SsUsername, "save", "พยายามบันทึก" + state.CurrentPageName, state.SsApplication, state.CurrentPage);
                        pwSheet.LtServerMessage.Text = WebUtil.PermissionDeny(PermissType.WriteDeny);
                        return;
                    }
                    //state.LogAct(state.SsUsername, "save", "บันทึก" + state.CurrentPageName, state.SsApplication, state.CurrentPage);
                    try
                    {
                        wSheet.SaveWebSheet();
                    }
                    catch { }
                }
                else
                {
                    //เช็ค Event JsPostBack
                    try
                    {
                        if (IsPostBack)
                        {
                            wSheet.CheckJsPostBack(eventArg);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void SetWebSheetEnd()
        {
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
                catch (Exception e) { }

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
                    isConfirmOnNew = pwSheet.IsConfirmOnNew ? "true" : "false";
                }
                catch { }

                try
                {
                    String msgType = "";
                    serverMessage = WebUtil.MessageClearText(pwSheet.LtServerMessage.Text, out msgType);
                    if (!string.IsNullOrEmpty(msgType))
                    {
                        serverMessageType = msgType;
                    }
                }
                catch { }
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

            try
            {
                if (pwSheet.ScrollTop >= 0)
                {
                    HdCurrentTopScroll.Value = pwSheet.ScrollTop + "";
                }
            }
            catch { }


            logoURL = xmlconfig.SavPathOpenType1 + "/GCOOP/Saving/Image/band_black.jpg";
        }
        public String logoURL = "";

        private void DeleteTempFile()
        {
            try
            {
                if (state.SsUsername.ToUpper() == "ADMIN")
                {
                    postCleanXmlJavaAppletFromFrameMaster = WebUtil.JsPostBack(ContentPlace.Page, "postCleanXmlJavaAppletFromFrameMaster");
                    String eventArg = "";
                    try { eventArg = Request["__EVENTARGUMENT"]; }
                    catch { }
                    //ถ้าคลิกจาก link "clean xml japplet"
                    if (IsPostBack && eventArg == "postCleanXmlJavaAppletFromFrameMaster")
                    {
                        try
                        {
                            deleteAppletXml();
                        }
                        catch { }
                        try
                        {
                            deleteIReportFile();
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private void deleteAppletXml()
        {
            String physPath = WebUtil.PhysicalPath + "Saving\\SlipApp\\xml_file_japplet";
            DirectoryInfo dir = new DirectoryInfo(physPath);
            if (dir.Exists)
            {
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        private void deleteIReportFile()
        {
            DirectoryInfo dir = new DirectoryInfo(xmlconfig.iReportSavePath);
            if (dir.Exists)
            {
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        private void BookVersion()
        {
            try
            {
                if (state.SsBookVersion == "")
                {
                    state.SsBookVersion = System.Configuration.ConfigurationManager.AppSettings["bookVersion"].ToString();
                }
                String postBack = WebUtil.JsPostBack(pageWebSheet, "postBookVersionFrame");
                String eventArg = "";
                try { eventArg = Request["__EVENTARGUMENT"]; }
                catch { }
                if (IsPostBack && eventArg == "postBookVersionFrame")
                {
                    if (state.SsBookVersion == "new")
                    {
                        state.SsBookVersion = "old";
                    }
                    else
                    {
                        state.SsBookVersion = "new";
                    }
                }
                String thaiBookVersion = state.SsBookVersion == "new" ? "ใหม่" : "เก่า";
                LtBookVersion.Text = postBack + "<span class=\"bookversion" + state.SsBookVersion + "\" style=\"font-size:16px;\" onclick=\"postBookVersionFrame()\" title=\"คลิกเพื่อสลับค่า\">พิมพ์สมุด" + thaiBookVersion + "</span>";
            }
            catch { }
        }

        //กรณีจะให้แสดง สถานะ Process 
        //outputProcess=WebUtil.runProcessing(state, "pb1", "CRITERIA_XML", "CRITERIA_XML_1", "CRITERIA_XML_2");

        #region  เพิ่มส่วนของการเปิดวันอัตโนมัติ

        public int OpenWorkdate(string application)
        {
            Sta ta = new Sta(state.SsConnectionString);

            DateTime workdate, currentdate;
            bool chkDate = true, currentdate_isworkdate = false;
            string sqlUpdate = "", sql = "";
            string app = "";
            currentdate = DateTime.Today;
            sql = @"select * from amappstatus where appltime_code in 
(select appltime_code from amappstatus where application='" + application + @"')
and workdate != to_date('" + currentdate.ToString("yyyy/MM/dd") + @"','yyyy/MM/dd')
and auto_flag=1 and used_flag=1";
            try
            {
                Sdt dt = ta.Query(sql);

                currentdate_isworkdate = IsWorkDate(currentdate);
                while (dt.Next())
                {
                    app = dt.GetString("application");
                    chkDate = true;
                    if (dt.GetDecimal("check_close_status") == 0)
                    {
                        workdate = dt.GetDate("workdate");
                        OpenNextWorkDate(workdate, application);
                        //                        while (workdate <= currentdate)
                        //                        {
                        //                            workdate = AddDaySpecial(workdate);
                        //                            if (workdate.Date == currentdate.Date && currentdate_isworkdate)
                        //                            {
                        //                                sqlUpdate = @"update amappstatus set last_workdate=workdate 
                        //, workdate=to_date('" + currentdate.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy') ,closeday_status=0,closemonth_status=0,closeyear_status=0 
                        //where application='" + application + @"' ";
                        //                                Sdt dtUpdate = ta.Query(sqlUpdate);

                        //                            }
                        //                        }



                    }
                    else
                    {
                        decimal close_day_status = 0, close_month_status = 0, close_year_status = 0, last_workdate_in_month = 0;
                        string sqlAmworkCalendar = "", sqlAccYear = "";
                        DateTime new_workdate, last_workdate, ending_acc;

                        workdate = dt.GetDate("workdate");
                        close_day_status = dt.GetDecimal("closeday_status");
                        close_month_status = dt.GetDecimal("closemonth_status");
                        close_year_status = dt.GetDecimal("closeyear_status");
                        sqlAmworkCalendar = @"select * from amworkcalendar where year=" + (workdate.Year + 543) + " and month=" + workdate.Month;
                        Sdt dtWorkCalendar = WebUtil.QuerySdt(sqlAmworkCalendar);
                        if (dtWorkCalendar.Next())
                        {
                            last_workdate_in_month = dtWorkCalendar.GetDecimal("lastworkdate");
                        }

                        if (workdate.Day >= Convert.ToInt16(last_workdate_in_month))
                        {
                            sqlAccYear = "select ending_of_account from accaccountyear where account_year =" + workdate.Year;
                            Sdt dtAccYear = WebUtil.QuerySdt(sqlAccYear);
                            if (dtAccYear.Next())
                            {
                                ending_acc = dtAccYear.GetDate("ending_of_account");
                                if (workdate.Month == ending_acc.Month)
                                {
                                    if (close_year_status == 1)
                                    {
                                        OpenNextWorkDate(workdate, app);

                                    }
                                }
                                else
                                {
                                    if (close_month_status == 1)
                                    {
                                        OpenNextWorkDate(workdate, app);
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (close_day_status == 1)
                            {
                                OpenNextWorkDate(workdate, app);
                            }
                            else
                            {
                                throw new Exception("ยังไม่ได้ปิดงานประจำวัน");
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally {
                ta.Close();
            }

            ta.Close();
            return 0;
        }

        public void OpenNextWorkDate(DateTime workdate, string application)
        {

            DateTime nextWorkDate = new DateTime();

            DateTime currentDate = new DateTime();
            bool chk = true;
            bool isworkd = false;
            try
            {
                nextWorkDate = workdate;
                currentDate = DateTime.Today;
                if (workdate < currentDate)
                {
                    while (chk)
                    {
                        nextWorkDate = nextWorkDate.AddDays(1);
                        isworkd = IsWorkDate(nextWorkDate);

                        //1.สำหรับเลื่อนวันถึงวันทำการถัดไปแต่ไม่ถึงวันปัจจุบัน
                        //if (isworkd || (nextWorkDate > currentDate))
                        //{
                        //    chk = false;
                        //}

                        ////2.สำหรับเลื่อนวันทำการมาถึงวันปัจจุบัน
                        if (  nextWorkDate >= currentDate)
                        {
                            chk = false;
                        }
                    }

                    if (isworkd)
                    {
                        string sqlupdate = @"update amappstatus set last_workdate=to_date('" + workdate.ToString("yyyy/MM/dd") + @"','yyyy/MM/dd')  
, workdate=to_date('" + nextWorkDate.ToString("yyyy/MM/dd") + @"','yyyy/MM/dd') ,closeday_status=0,closemonth_status=0,closeyear_status=0 
where application='" + application + @"' ";
                        Sdt dt = WebUtil.QuerySdt(sqlupdate);
                    }
                    else {
                        throw new Exception("วันปัจจุบันไม่ได้กำหนดเป็นวันทำงาน");
                    }
                }
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public bool IsWorkDate(DateTime InputDate)
        {
            bool chk = false;
            string date = Convert.ToString(InputDate.Day);
            string month = Convert.ToString(InputDate.Month);
            string year = Convert.ToString(InputDate.Year + 543);
            string workdays = "";
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                string sql = "select * from amworkcalendar where year=" + year + " and month=" + month;
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    workdays = dt.GetString("workdays");
                    if (workdays.Length > 0)
                    {
                        for (int i = 1; i <= workdays.Length; i++)
                        {
                            if (i == Convert.ToInt16(date) && workdays[i - 1] == 'W')
                            {
                                chk = true;
                            }


                        }

                    }
                }
                else
                {
                    throw new Exception("ไม่พบปฏิทินระบบ ");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally {
                ta.Close();
            }
            ta.Close();
            return chk;
        }

        #endregion
    }
}