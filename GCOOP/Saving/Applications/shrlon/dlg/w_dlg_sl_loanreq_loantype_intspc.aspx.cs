using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanreq_loantype_intspc : PageWebDialog , WebDialog 
    {
        
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();


            if (IsPostBack)
            {
                dw_intspc.RestoreContext();
            }
            
            if (dw_intspc.RowCount < 1)
            {
                String reqno = Request["reqno"].ToString();
                try
                {
                    dw_intspc.Reset();
                    dw_intspc.InsertRow(0);
                    dw_intspc.Retrieve(reqno);
                }
                catch  { }
            }
            if (dw_intspc.RowCount > 1)
            {
                dw_intspc.DeleteRow(dw_intspc.RowCount);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {

        }
    }
}
