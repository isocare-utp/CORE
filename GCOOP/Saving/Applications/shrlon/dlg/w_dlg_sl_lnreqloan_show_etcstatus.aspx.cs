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
using Sybase.DataWindow;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_lnreqloan_show_etcstatus : PageWebDialog, WebDialog
    {
        protected String refresh;
        protected String setDocNo;

        public void InitJsPostBack()
        {
            setDocNo = WebUtil.JsPostBack(this, "setDocNo");
            refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebDialogLoadBegin()
        {

            if (IsPostBack)
            {
                dw_detail.RestoreContext();
            }
            else
            {
                String member_no = Request["member_no"].ToString();

                DwUtil.RetrieveDataWindow(dw_detail, "sl_loan_requestment_cen.pbl", null, member_no);

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
            if (eventArg == "refresh")
            {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_detail.SaveDataCache();

        }

       
        public void Refresh() { }

        

    }
}
