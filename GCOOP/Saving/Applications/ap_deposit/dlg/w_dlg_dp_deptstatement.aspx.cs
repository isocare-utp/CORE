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

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_deptstatement : PageWebDialog, WebDialog
    {
        DwThDate tDwMain;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("dpdeptstatement_entry_date", "entry_tdate");
        }

        public void WebDialogLoadBegin()
        {
            
            this.ConnectSQLCA();
            try
            {
                String accNo = Request["accountNo"];
                DwMain.SetTransaction(sqlca);
                DwMain.Retrieve(accNo, state.SsCoopId);
                tDwMain.Eng2ThaiAllRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
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
