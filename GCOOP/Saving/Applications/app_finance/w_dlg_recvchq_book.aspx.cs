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
    public partial class w_dlg_recvchq_book : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwMain;
        protected String postBankBranch;
        protected String postChangeChqNo;
        protected String postInitChqBook;



        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("entry_date", "entry_tdate");
            postInitChqBook = WebUtil.JsPostBack(this, "postInitChqBook");
            postChangeChqNo = WebUtil.JsPostBack(this, "postChangeChqNo");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
        }

        public void WebSheetLoadBegin()
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

            DwUtil.RetrieveDDDW(DwMain, "bank_code", "recvchq_book.pbl", null);

            DwMain.SetItemString(1, "entry_id", state.SsUsername);

            DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            //DataWindowChild DcBranchId = DwMain.GetChild("coopbranch_id");
            //DcBranchId.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

            DwMain.SetItemString(1, "machine_id", state.SsClientIp);

            DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postInitChqBook":
                    GetChqBook();
                    break;
                case "postChangeChqNo":
                    ChangeChqNo();
                    break;
                case "postBankBranch":
                    GetBankBranch();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String DwMainXml = DwMain.Describe("DataWindow.Data.XML");
                int result = fin.of_postchqmas(state.SsWsPass, DwMainXml);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    DwMain.Reset();
                    DwMain.InsertRow(0);

                    DwUtil.RetrieveDDDW(DwMain, "bank_code", "recvchq_book.pbl", null);

                    DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    DwMain.SetItemString(1, "coop_id", state.SsCoopId);

                    DwMain.SetItemString(1, "machine_id", state.SsClientIp);

                    DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();
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
        }

        #endregion

        private void ChangeChqNo()
        {
            Decimal ChqSlipAmt, Sum;
            String StartChqNo, NewValue, SumAmt, ColunmName;

            ColunmName = HfColumnName.Value;

            if (ColunmName == "chqslip_amt")
            {
                StartChqNo = DwMain.GetItemString(1, "start_chqno");
                NewValue = HfNewValue.Value;
                DwMain.SetItemDecimal(1, "chqslip_remain", Convert.ToDecimal(NewValue));
                NewValue = "00000000" + Convert.ToString(Convert.ToDecimal(NewValue) + Convert.ToDecimal(StartChqNo) - 1);
                NewValue = WebUtil.Right(NewValue, 8);
                if (StartChqNo != "00000000")
                {
                    DwMain.SetItemString(1, "end_chqno", NewValue);
                }
            }
            else if (ColunmName == "start_chqno")
            {
                NewValue = HfNewValue.Value;
                ChqSlipAmt = DwMain.GetItemDecimal(1, "chqslip_amt");
                Sum = ChqSlipAmt + Convert.ToDecimal(NewValue) - 1;
                SumAmt = "00000000" + Convert.ToString(Sum);
                SumAmt = WebUtil.Right(SumAmt, 8);
                DwMain.SetItemSqlString(1, "end_chqno", Convert.ToString(SumAmt));
                NewValue = "00000000" + NewValue;
                NewValue = WebUtil.Right(NewValue, 8);
                DwMain.SetItemString(1, "start_chqno", NewValue);
            }
        }

        private void GetChqBook()
        {
            try
            {
                Int32 result;
                String DwMainXml;

                DwMainXml = DwMain.Describe("DataWindow.Data.XML");
                result = fin.of_init_chq_bookno(state.SsWsPass,ref DwMainXml);

                DwMain.Reset();
                //DwMain.ImportString(result.ToString()+"", FileSaveAsType.Xml);
                DwUtil.ImportData(DwMainXml, DwMain, null, FileSaveAsType.Xml);

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

        private void GetBankBranch()
        {
            String ls_bank;
            //String ls_bankbranch = "";
            try
            {
                ls_bank = DwMain.GetItemString(1, "bank_code");
            }
            catch
            {
                ls_bank = DwMain.GetItemString(1, "bank_branch");
            }

            //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
            //Int32 BankBranchXml = fin.of_dddwbankbranch(state.SsWsPass, ls_bank, ref ls_bankbranch);
            //DcBankBranch.ImportString(BankBranchXml.ToString(), FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();
            
            //krit edit 07032017 :เนื่องจากเลือกธนาคารแล้วไม่ ไม่มีสาขามา
            object[] argBank = new object[1] { ls_bank };
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", "recvchq_book.pbl", argBank);
            //end

        }
    }
}
