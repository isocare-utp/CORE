using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_coll_me : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String closeWebDialog;

        public void InitJsPostBack()
        {
            closeWebDialog = WebUtil.JsPostBack(this, "closeWebDialog");
        }

        public void WebDialogLoadBegin()
        {
            String as_colltype = "";

            this.ConnectSQLCA();
            dw_detail.SetTransaction(sqlca);

            str_itemchange strList = new str_itemchange();
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }

            if (IsPostBack)
            {

            }
            else
            {

                strList = WebUtil.nstr_itemchange_session(this);
                String row = Request["row"].ToString();
                String coop_id = Request["coop_id"].ToString();
                String refCollNo = Request["refCollNo"].ToString();
                as_colltype = Request["collType"].ToString();

                int row_m = Convert.ToInt32(row) - 1;
                DateTime requestDate = new DateTime();

                try
                {
                    requestDate = state.SsWorkDate;
                }
                catch { requestDate = state.SsWorkDate; }

                DateTime ldtm_loanreceive = requestDate;
                dw_detail.InsertRow(1);
                dw_detail.Retrieve(refCollNo, as_colltype, coop_id);


            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "closeWebDialog")
            {
                OnCloseClick();
            }

        }
        public void WebDialogLoadEnd()
        {

            dw_detail.SaveDataCache();

        }
        public void OnCloseClick()
        {
            HfChkStatus.Value = "1";
        }

    }
}
