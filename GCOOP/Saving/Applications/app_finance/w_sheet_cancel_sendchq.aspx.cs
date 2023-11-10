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
    public partial class w_sheet_cancel_sendchq : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwDate;
        protected String postRetrieve;
        protected String postSumSelect;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwDate = new DwThDate(DwDate, this);
            tDwDate.Add("send_date", "send_tdate");
            postRetrieve = WebUtil.JsPostBack(this, "postRetrieve");
            postSumSelect = WebUtil.JsPostBack(this, "postSumSelect");
        }

        public void WebSheetLoadBegin()
        {
           // String ls_bank_xml;
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwDate.InsertRow(0);
                DwSendChqAcc.InsertRow(0);

                DwDate.SetItemDateTime(1, "send_date", state.SsWorkDate);
                tDwDate.Eng2ThaiAllRow();

                DwUtil.RetrieveDDDW(DwSendChqAcc, "as_account", "sendchq.pbl", state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(DwDate);
                this.RestoreContextDw(DwSendChqAcc);
                this.RestoreContextDw(DwCancelList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postRetrieve":
                    Retrieve();
                    break;
                case "postSumSelect":
                    SumSelect();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            String as_bank_xml, as_cancellist_xml;

            try
            {
                as_bank_xml = DwSendChqAcc.Describe("DataWindow.Data.XML");
                as_cancellist_xml = DwCancelList.Describe("DataWindow.Data.XML");

                int re = fin.of_postcancel_sendchq(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, as_bank_xml, as_cancellist_xml);

                if (re > 0)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");

                    String ls_bank_xml;
                    ls_bank_xml = DwSendChqAcc.Describe("DataWindow.Data.XML");
                    Int32 result = fin.of_retrievecancel_sendchq(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref ls_bank_xml, as_cancellist_xml);
                    DwCancelList.Reset();
                    if (ls_bank_xml != "")
                    {
                        DwCancelList.ImportString(ls_bank_xml, FileSaveAsType.Xml);
                    }
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
            DwDate.SaveDataCache();
            DwSendChqAcc.SaveDataCache();
            DwCancelList.SaveDataCache();
        }

        #endregion


        private void SumSelect()
        {
            Decimal flag, cheque_amt, sum = 0;
            for (int i = 1; i <= DwCancelList.RowCount; i++)
            {
                flag = DwCancelList.GetItemDecimal(i, "select_flag");
                if (flag == 1)
                {
                    cheque_amt = DwCancelList.GetItemDecimal(i, "cheque_amt");
                    sum = sum + cheque_amt;
                }
            }
            DwCancelList.SetItemDecimal(1, "compute_2", sum);
        }

        private void Retrieve()
        {
            String ls_bank_xml, as_cancellist_xml;
            ls_bank_xml = DwSendChqAcc.Describe("DataWindow.Data.XML");
            as_cancellist_xml = DwCancelList.Describe("DataWindow.Data.XML");
            try
            {
                Int32 result = fin.of_retrievecancel_sendchq(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref ls_bank_xml, as_cancellist_xml);
                DwCancelList.Reset();
                if (ls_bank_xml != "")
                {
                    DwCancelList.ImportString(ls_bank_xml, FileSaveAsType.Xml);
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
    }
}
