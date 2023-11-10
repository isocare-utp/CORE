using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_dept_acclist : PageWebDialog, WebDialog
    {

        #region WebDialog Members

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            String memberNo;
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                try
                {
                    memberNo = Request["memberno"];
                }
                catch
                {
                    memberNo = "";
                }

                DwMain.Retrieve(state.SsCoopId, memberNo);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            this.DisConnectSQLCA();
        }

        #endregion
    }
}
