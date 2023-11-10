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
using DataLibrary;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_report_progress : PageWebDialog, WebDialog
    {

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;

        protected String jsUrl;
        protected String criAjaxUrl;

        #region WebDialog Members

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            try
            {
                app = Request["app"].ToString();
                gid = Request["gid"].ToString();
                rid = Request["rid"].ToString();
                pdf = Request["pdf"].ToString();
            }
            catch { }
            //XmlConfigService xml = new XmlConfigService();
            jsUrl = state.SsUrl + "JsCss/Ajax.js";// xml.SavProtocal + "://" + xml.SavDomain + ":" + xml.SavPort + "/" + xml.SavPathPattern + "JsCss/Ajax.js";
            criAjaxUrl = state.SsUrl + "Criteria/AjaxReportProcess.aspx";// xml.SavProtocal + "://" + xml.SavDomain + ":" + xml.SavPort + "/" + xml.SavPathPattern + "Criteria/AjaxReportProcess.aspx";
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {

        }

        #endregion

    }
}
