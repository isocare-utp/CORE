using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon; // new common

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_deptconstant : PageWebSheet, WebSheet
    {
        private String pbl = "dp_deptconstant.pbl";
      //  protected String coop_ctrl = ;
        #region WebSheet Members

        public void InitJsPostBack()
        {
        }

        public void WebSheetLoadBegin()
        {
            //this.ConnectSQLCA();
            //DwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                //DwMain.Retrieve();
                //DwMain.SetItemDecimal(1, "admin", state.SsUserLevel);
               // DwUtil.RetrieveDataWindow(DwMain, pbl, null, null);
                DwUtil.RetrieveDataWindow(DwMain, pbl, null,state.SsCoopControl);
                DwMain.SetItemDecimal(1, "admin", state.SsUserLevel);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void SaveWebSheet()
        {
            try
            {
                //DwMain.UpdateData();
                DwUtil.UpdateDataWindow(DwMain, pbl, "DPDEPTCONSTANT");
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}