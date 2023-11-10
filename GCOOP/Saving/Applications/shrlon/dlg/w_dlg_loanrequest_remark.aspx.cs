using System.Data;
using CoreSavingLibrary;
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
using System;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_loanrequest_remark : PageWebDialog,WebDialog
    {
      
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
           
            if (!IsPostBack)
            {
                //string remark = Request["reamrk"];
                dw_detail.Reset();
                dw_detail.InsertRow(0);
                //dw_detail.SetItemString(1, "remark",   remark.Replace("@", "" + Environment.NewLine));
                
            }
            else
            {
                this.RestoreContextDw(dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }


        public void WebDialogLoadEnd()
        {
            dw_detail.SaveDataCache();
        }

    }
}