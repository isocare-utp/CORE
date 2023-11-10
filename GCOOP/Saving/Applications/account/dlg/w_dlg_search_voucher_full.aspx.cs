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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เรียกใช้ service
using CoreSavingLibrary;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_search_voucher_full : PageWebDialog, WebDialog
    {
        private DwThDate tDwDate;
        protected String jsPostSelectList;
        protected String jsPostVoucherDate;
        private n_accountClient accService;//ประกาศตัวแปร WebService

        #region WebDialog Members
        public void InitJsPostBack()
        {
            tDwDate = new DwThDate(DwDate, this);
            tDwDate.Add("voucher_date", "voucher_tdate");
            jsPostSelectList = WebUtil.JsPostBack(this, "jsPostSelectList");
            jsPostVoucherDate = WebUtil.JsPostBack(this, "jsPostVoucherDate");
        }

        public void WebDialogLoadBegin()
        {
            accService = wcf.NAccount;//ประกาศ new
            if (!IsPostBack)
            {
                DwList.InsertRow(0);
                String queryStrVcDate = "";
                try { queryStrVcDate = Request["vcDate"].Trim(); }
                catch { }
                if (queryStrVcDate != "")
                {
                    DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));
                    // DwList.Retrieve(vcDate, state.SsCoopId);
                    DwUtil.RetrieveDataWindow(DwList, "vc_voucher_edit.pbl", null, vcDate, state.SsCoopId);
                }
            }
            else
            {
                this.RestoreContextDw(DwDate);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            tDwDate.Eng2ThaiAllRow();
            DwDate.SaveDataCache();
            DwList.SaveDataCache();

        }
        #endregion


    }
}