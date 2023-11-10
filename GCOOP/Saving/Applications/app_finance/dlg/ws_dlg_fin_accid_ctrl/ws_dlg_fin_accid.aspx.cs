using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.ws_dlg_fin_accid_ctrl
{
    public partial class ws_dlg_fin_accid : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsCriteria.InitDsCriteria(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.RetrieveDetail(state.SsCoopControl,"");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                SearchData();
            } 
        }

        public void WebDialogLoadEnd()
        {

        }

        public void SearchData()
        {
            try
            {
                String ls_itemdesc = "", ls_code = "";

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;
                ls_itemdesc = dsCriteria.DATA[0].item_desc.Trim();
                ls_code = dsCriteria.DATA[0].slipitemtype_code.Trim();
                if (ls_itemdesc.Length > 0)
                {
                    ls_sqlext = " and (  FINUCFITEMTYPE.ITEM_DESC like '%" + ls_itemdesc + "%') ";
                }
                if (ls_code.Length > 0)
                {
                    ls_sqlext = " and (  FINUCFITEMTYPE.SLIPITEMTYPE_CODE like '%" + ls_code + "%') ";
                }
                dsList.RetrieveDetail(state.SsCoopControl, ls_sqlext);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
           
        }
    }
}