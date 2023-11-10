using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl.w_dlg_sl_searchmembgroup_ctrl
{
    public partial class w_dlg_sl_searchmembgroup : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostSearch { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                string membgroup_code = "", membgroup_desc = "", sql_search = "";
                membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;
                membgroup_desc = dsMain.DATA[0].MEMBGROUP_DESC;
                if (membgroup_code.Length > 0)
                {
                    sql_search += "and (membgroup_code = '" + membgroup_code + "')";
                }
                if (membgroup_desc.Length > 0)
                {
                    sql_search += "and (membgroup_desc like '%" + membgroup_desc + "%')";
                }
                dsList.RetrieveList(sql_search);
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}