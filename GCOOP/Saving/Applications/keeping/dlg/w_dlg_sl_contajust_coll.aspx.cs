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

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_contajust_coll : PageWebDialog, WebDialog
    {
        private String coll_no;
        private String coll_type;

        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_data.SetTransaction(sqlca);
            dw_item.SetTransaction(sqlca);

            coll_no = HiddenFieldCollNo.Value.Trim();
            coll_type = HiddenFieldCollType.Value.Trim();
            if (coll_no != null) {
                dw_item.Retrieve(coll_no);
                dw_data.Retrieve(coll_no, coll_type);

            }
            
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
