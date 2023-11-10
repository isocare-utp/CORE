using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_loan_collredeem : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdCollmasttypeCode();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                string sql = @"SELECT * FROM LNCOLLMASTER WHERE COOP_ID = {0}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                DataTable dt = WebUtil.Query(sql);
                dsDetail.ImportData(dt);
            }
        }
        public void WebDialogLoadEnd()
        {
        }
    }
}