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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_recvchq : PageWebDialog, WebDialog
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwMain;
        protected String postBankBranch;


        #region WebDialog Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("dateon_chq", "dateon_chq_tdate");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
        }

        public void WebDialogLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }

            DwUtil.RetrieveDDDW(DwMain, "bank_code", "finslip_spc.pbl", null);   
            DwMain.SetItemDateTime(1, "dateon_chq", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postBankBranch")
            {
                GetBankBranch();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion


        private void GetBankBranch()
        {
            String ls_bank, DddwName, ls_bankbranch = "";

            ls_bank = DwMain.GetItemString(1, "bank_code");

            DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
            Int32 BankBranchXml = fin.of_dddwbankbranch(state.SsWsPass, ls_bank, ref ls_bankbranch);
            DcBankBranch.ImportString(ls_bank, FileSaveAsType.Text);
            DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            DcBankBranch.Filter();
        }
    }
}
