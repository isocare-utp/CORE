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
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finslip_retrievechq : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwMain;
        protected String postCashType;
        
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCashType = WebUtil.JsPostBack(this, "postCashType");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;
            tDwMain = new DwThDate(DwMain);

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                d_fin_slipspc_det_itemtype.InsertRow(0);

                DataWindowChild dc = DwMain.GetChild("bank_branch");
                string XmlB = fin.of_dddwbank(state.SsWsPass);
                dc.ImportString(XmlB, FileSaveAsType.Xml);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(d_fin_slipspc_det_itemtype);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCashType")
            {
                SetCashType();
            }
        }

        private void SetCashType()
        {
            String CashType = HfCashType.Value;
            CashType = fin.of_defaultaccid(state.SsWsPass, CashType);
            DwMain.SetItemString(1, "tofrom_accid", CashType);
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
