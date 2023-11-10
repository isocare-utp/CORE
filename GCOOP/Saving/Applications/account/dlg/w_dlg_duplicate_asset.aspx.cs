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
using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_duplicate_asset : PageWebDialog, WebDialog
    {
        private n_accountClient AccountService;

        protected String jsPostSave;
        protected String jsPostDuplicate;
        DwThDate tDwMain;

        #region WebDialog Members
        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("receive_date", "receive_tdate");

            jsPostSave = WebUtil.JsPostBack(this, "jsPostSave");
            jsPostDuplicate = WebUtil.JsPostBack(this, "jsPostDuplicate");
        }

        public void WebDialogLoadBegin()
        {
            AccountService = wcf.NAccount;

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                InitHead();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostSave":
                    SaveDuplicate();
                    break;
                case "jsPostDuplicate":
                    Duplicate();
                    break;
            }
        }

        public void WebDialogLoadEnd()
        {
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }
        #endregion

        #region Function
        private void InitHead()
        {
            String asset_doc = Request["asset_doc"];
            String rcv_date = Request["receive_tdate"];
            DateTime receive_date = DateTime.ParseExact(rcv_date, "ddMMyyyy", new CultureInfo("th-TH"));

            DwMain.SetItemString(1, "asset_doc", asset_doc);
            DwMain.SetItemDateTime(1, "receive_date", receive_date);
        }
        #endregion

        #region Function
        private void SaveDuplicate()
        {
            try
            {
                String as_xml = DwList.Describe("Datawindow.Data.XML");
                String asset_docno = DwMain.GetItemString(1, "asset_doc");
                String branch_id = state.SsCoopId;
                DateTime receive_date = DwMain.GetItemDateTime(1, "receive_date");

                //short result = AccountService.of_duplicate_asset(state.SsWsPass, as_xml, asset_docno, branch_id, receive_date);
                short result = 1;
                if (result == 1)
                {
                    SaveComplete.Value = "true";
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void Duplicate()
        {
            String asset_docno = DwMain.GetItemString(1, "asset_doc");
            Decimal dup_amt = DwMain.GetItemDecimal(1, "dup_amt");

            DwList.Reset();
            for (int i = 0; i < dup_amt; i++)
            {
                String index = (Convert.ToInt32(asset_docno.Substring(asset_docno.Length - 4, 4)) + 1).ToString();
                String new_asset_docno = asset_docno.Substring(0, asset_docno.Length - 4) + index.PadLeft(4, '0');
                asset_docno = new_asset_docno;

                int row = DwList.InsertRow(0);
                DwList.SetItemString(row, "asset_doc", asset_docno);
            }
        }
        #endregion
    }
}