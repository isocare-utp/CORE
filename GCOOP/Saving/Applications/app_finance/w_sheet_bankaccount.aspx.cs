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

namespace Saving.Applications.app_finance
{

    public partial class w_sheet_bankaccount : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private String pbl = "bankaccount.pbl";
        protected String postRetrieve;
        protected String postBankBranch;



        #region WebSheet Members

        public void InitJsPostBack()
        {
            postRetrieve = WebUtil.JsPostBack(this, "postRetrieve");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
        }

        public void WebSheetLoadBegin()
        {
            //this.ConnectSQLCA();
            fin = wcf.NFinance;
            //DwMain.SetTransaction(sqlca);
            //DwList.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                //DwMain.Reset();
                //DwList.Reset();

                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "bankaccount.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "account_id", "bankaccount.pbl",state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRetrieve")
            {
                PostRetreiveAcc();
            }
            else if (eventArg == "postBankBranch")
            {
                GetBankBranch();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                //DwMain.UpdateData();
                String as_bank_xml = DwMain.Describe("DataWindow.Data.XML");
                int re = fin.of_postupdatebankaccount(state.SsWsPass, as_bank_xml);

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");
                    DwMain.Reset();
                    DwList.Reset();
                    DwMain.InsertRow(0);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
            //this.DisConnectSQLCA();
        }

        #endregion



        protected void GetBankBranch()
        {
            String ls_bank;

            try
            {
                ls_bank = DwMain.GetItemString(1, "bank_code");
            }
            catch
            {
                ls_bank = "";
            }

            DwUtil.RetrieveDDDW(DwMain, "bankbranch_code", pbl, ls_bank);
            //DataWindowChild DcBankBranch = DwMain.GetChild("bankbranch_code");
            //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
            //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();
        }

        protected void PostRetreiveAcc()
        {
            Int32 resultXml;

            try
            {
                String as_bank_xml = DwMain.Describe("DataWindow.Data.XML");
                String as_bankstm_xml = "";
                resultXml = fin.of_retrievebankaccount(state.SsWsPass, state.SsCoopId, ref as_bank_xml, ref as_bankstm_xml);
                DwMain.Reset();
                //DwMain.ImportString(as_bank_xml, FileSaveAsType.Xml);
                DwUtil.ImportData(as_bank_xml, DwMain, null, FileSaveAsType.Xml);

                GetBankBranch();

                DwList.Reset();
                //DwList.ImportString(as_bankstm_xml, FileSaveAsType.Xml);
                DwUtil.ImportData(as_bankstm_xml, DwList, null, FileSaveAsType.Xml);
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

            //PostSetFilter(bankCode, bankBranch);
            DwMain.Modify("bank_code.Protect=1");
            DwMain.Modify("bankbranch_code.Protect=1");
        }

        protected void PostSetFilter(String bankCode, String bankbranch)
        {
            DataWindowChild dc = DwMain.GetChild("bankbranch_code");
            dc.SetTransaction(sqlca);
            dc.Retrieve();

            dc.SetFilter("bank_code='" + bankCode.Trim() + "'");
            dc.Filter();
        }
    }
}
