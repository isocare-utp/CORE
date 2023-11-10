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
using Sybase.DataWindow;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_deptdetail : PageWebDialog, WebDialog
    {
        
        #region WebDialog Members

        public void InitJsPostBack()
        {
           
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                String member_no = Request["member_no"];
                dw_detail.Retrieve();
                dw_detail.SetFilter("member_no='" + member_no + "' and deptclose_status = 0");
                dw_detail.Filter();
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

        #endregion
    }
}