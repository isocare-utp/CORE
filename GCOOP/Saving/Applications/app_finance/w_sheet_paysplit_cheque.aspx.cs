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
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_paysplit_cheque : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private String pbl = "paysplit_cheque.pbl";
        private DwThDate tDwCon;
        protected String postInit;
        protected String postChqBook;
        protected String postCheckList;
        protected String postDeleteRow;
        protected String postBankBranch;
        protected String postSetToWhom;
        protected String postRefresh;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInit = WebUtil.JsPostBack(this, "postInit");
            postChqBook = WebUtil.JsPostBack(this, "postChqBook");
            postSetToWhom = WebUtil.JsPostBack(this, "postSetToWhom");
            postCheckList = WebUtil.JsPostBack(this, "postCheckList");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            tDwCon = new DwThDate(DwCon);
            tDwCon.Add("adtm_date", "adtm_tdate");

        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;
            this.ConnectSQLCA();
            if (!IsPostBack)
            {
                Int32 result;
                String chqcond_Xml = "", cutbank_Xml = "", chqtype_Xml = "", chqlist_Xml = "";
                result = fin.of_init_paychq_split(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref chqcond_Xml, ref cutbank_Xml, ref chqtype_Xml, ref chqlist_Xml);

                DwCon.Reset();
                //DwCon.ImportString(chqcond_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqcond_Xml, DwCon, tDwCon, FileSaveAsType.Xml);
                tDwCon.Eng2ThaiAllRow();

                DwFrom.Reset();
                //DwFrom.ImportString(chqtype_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqtype_Xml, DwFrom, tDwCon, FileSaveAsType.Xml);

                DwType.Reset();
                //DwType.ImportString(cutbank_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(cutbank_Xml, DwType, tDwCon, FileSaveAsType.Xml);

                if (chqlist_Xml != "")
                {
                    DwSlip.Reset();
                    //DwSlip.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(chqlist_Xml, DwSlip, tDwCon, FileSaveAsType.Xml);
                }
                
                DwUtil.RetrieveDDDW(DwCon, "as_bank", "paysplit_cheque.pbl", null);
                DwUtil.RetrieveDDDW(DwType, "as_chqprint_chqtype", "paysplit_cheque.pbl", null);
                tDwCon.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwCon);
                this.RestoreContextDw(DwFrom);
                this.RestoreContextDw(DwType);
                this.RestoreContextDw(DwSlip);
                this.RestoreContextDw(DwSlipChq);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postBankBranch":
                    GetBankBranch();
                    break;
                case "postChqBook":
                    GetChqBook();
                    break;
                case "postInit":
                    GetInit();
                    break;
                case "postCheckList":
                    CheckList();
                    break;
                case "DeleteRow":
                    DeleteRow();
                    break;
                case "postDeleteRow":
                    DeleteRow();
                    break;
                case "postRefresh":
                    GetRefresh();
                    break;
            }
        }

        private void GetRefresh()
        {
            String as_towhom = "";
            Decimal adc_money = 0;
            if (DwSlipChq.RowCount > 0)
            {
                //เพิ่มแสดงยอดรวม
                for (int i = 1; i <= DwSlipChq.RowCount; i++)
                {
                    try
                    {
                        as_towhom = DwSlipChq.GetItemString(i, "as_towhom");
                        adc_money = DwSlipChq.GetItemDecimal(i, "adc_money");
                    }
                    catch { adc_money = 0; }
                    DwSlipChq.SetItemString(i, "as_towhom", as_towhom);
                    DwSlipChq.SetItemDecimal(i, "adc_money", adc_money);
                    // adc_money = adc_money + adc_money;
                }

                // lbl_itembalance.Text = adc_money.ToString("#,###,###.00");
            }
            else
            {
                //  lbl_itembalance.Text = adc_money.ToString("#,###,###.00");
            }





        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            String ls_cond_xml, ls_bankcut_xml, ls_chqtype_xml, ls_chqlist_xml, ls_chqspilt_xml;

            DwSlip.SetFilter("ai_selected=" + 1 + "");
            DwSlip.Filter();
            ls_chqlist_xml = DwSlip.Describe("DataWindow.Data.XML");
            ls_cond_xml = DwCon.Describe("DataWindow.Data.XML");
            ls_bankcut_xml = DwFrom.Describe("DataWindow.Data.XML");
            ls_chqtype_xml = DwType.Describe("DataWindow.Data.XML");
            ls_chqspilt_xml = DwSlipChq.Describe("DataWindow.Data.XML");
            int row_SlipChq = DwSlipChq.RowCount;

            Decimal item_amtnet = 0, adc_money = 0, sumpay = 0;
            String SLIP_NO = "", COOPBRANCH_ID = "000";
            try
            {
                //
                for (int i = 1; i <= row_SlipChq; i++)
                {
                    adc_money = DwSlipChq.GetItemDecimal(i, "adc_money");
                    adc_money = adc_money + adc_money;
                }
                item_amtnet = DwSlip.GetItemDecimal(DwSlip.RowCount, "item_amtnet");

                SLIP_NO = DwSlip.GetItemString(DwSlip.RowCount, "slip_no");
            }
            catch { }
            try
            {

                Int32 re = fin.of_postpaychq_split(state.SsWsPass, state.SsCoopControl, state.SsUsername, state.SsWorkDate, state.SsClientIp, ls_cond_xml, ls_bankcut_xml, ls_chqtype_xml, ls_chqlist_xml, ls_chqspilt_xml, state.SsPrinterSet);

             

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");


               Int32 result;
               string chqcond_Xml = "", cutbank_Xml = "", chqtype_Xml = "", chqlist_Xml = "";
               result = fin.of_init_paychq_split(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref chqcond_Xml, ref cutbank_Xml, ref chqtype_Xml, ref chqlist_Xml);

                DwCon.Reset();
                //DwCon.ImportString(chqcond_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqcond_Xml, DwCon, tDwCon, FileSaveAsType.Xml);
                tDwCon.Eng2ThaiAllRow();

                DwFrom.Reset();
                //DwFrom.ImportString(chqtype_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqtype_Xml, DwFrom, tDwCon, FileSaveAsType.Xml);

                DwType.Reset();
                //DwType.ImportString(cutbank_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(cutbank_Xml, DwType, tDwCon, FileSaveAsType.Xml);

                if (chqlist_Xml != "")
                {
                    DwSlip.Reset();
                    //DwSlip.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(chqlist_Xml, DwSlip, tDwCon, FileSaveAsType.Xml);
                }

                DwUtil.RetrieveDDDW(DwCon, "as_bank", "paysplit_cheque.pbl", null);
                DwUtil.RetrieveDDDW(DwType, "as_chqprint_chqtype", "paysplit_cheque.pbl", null);
                tDwCon.Eng2ThaiAllRow();

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
            DwCon.SaveDataCache();
            DwFrom.SaveDataCache();
            DwType.SaveDataCache();
            DwSlip.SaveDataCache();

            //int row = Convert.ToInt32(HdRow.Value);
            //if (row > 0)
            //{
            //    if (DwSlipChq.RowCount > 0)
            //    {
            //        String towhom = DwSlip.GetItemString(row, "pay_towhom");
            //        for (int i = 1; i <= DwSlipChq.RowCount; i++)
            //        {
            //            DwSlipChq.SetItemString(i, "as_towhom", towhom);
            //        }
            //    }
            //}

            DwSlipChq.SaveDataCache();

            //  lbl_itembalance.Text = Hadc_money.Value;//.ToString("#,###,###.00");

        }

        #endregion



        private void DeleteRow()
        {
            int row = Convert.ToInt32(HdRow.Value);
            DwSlipChq.DeleteRow(row);
        }

        private void CheckList()
        {
            int row = Convert.ToInt32(HdRow.Value);
           decimal Check = Convert.ToDecimal(HdCheck.Value);

            for (int i = 1; i <= DwSlip.RowCount; i++)
            {
                DwSlip.SetItemDecimal(i, "ai_selected", 0);
            }
            DwSlip.SetItemDecimal(row, "ai_selected", Check);

            if (DwSlipChq.RowCount > 0)
            {
                String towhom = DwSlip.GetItemString( row, "pay_towhom");
                for (int i = 1; i <= DwSlipChq.RowCount; i++)
                {
                    DwSlipChq.SetItemString(i, "as_towhom", towhom);
                }
            }
        }

        private void GetInit()
        {
            Int32 result;
            String ls_bank, ls_bankbranch, ls_chqbookno, accno = "", startchqno = "";

            ls_bank = DwCon.GetItemString(1, "as_bank");
            ls_bankbranch = DwCon.GetItemString(1, "as_bankbranch");
            ls_chqbookno = DwCon.GetItemString(1, "as_chqbookno");

            result = fin.of_init_chqnoandbank(state.SsWsPass, ls_bank, ls_bankbranch, ls_chqbookno, state.SsCoopId, ref accno, ref startchqno);

            //DwCon.SetItemString(1, "as_chqstart_no", startchqno);
            //DwFrom.SetItemString(1, "as_fromaccno", accno);


            DwUtil.RetrieveDDDW(DwCon, "as_chqstart_no", pbl, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno );
           //DwUtil.RetrieveDDDW(DwCon, "as_chqstart_no", "paychq_fromslip.pbl", null);
           // DataWindowChild DcChqNo = DwCon.GetChild("as_chqstart_no");
           // DcChqNo.SetFilter("chequebook_no='" + ls_chqbookno + "'");
           // DcChqNo.Filter();
           // DwCon.SetItemString(1, "as_chqstart_no", startchqno);
           // DwCon.SetItemString(1, "as_fromaccno", accno);

        }

        private void GetChqBook()
        {
            String BankCode, BankBranch; //, DddwName, DddwChqBookNo;

            BankCode = DwCon.GetItemString(1, "as_bank");
            BankBranch = DwCon.GetItemString(1, "as_bankbranch");

           // DataWindowChild DcChqBookNo = DwCon.GetChild("as_chqbookno");
            DwUtil.RetrieveDDDW(DwCon, "as_chqbookno", pbl, state.SsCoopId, BankCode, BankBranch);
            
            //DcChqBookNo.SetTransaction(sqlca);
            //DcChqBookNo.Retrieve();
            //DddwName = DwCon.Describe("as_chqbookno.dddw.name");
            //DddwChqBookNo = com.of_getdddwxml(state.SsWsPass, DddwName);
            //DcChqBookNo.ImportString(DddwChqBookNo, FileSaveAsType.Xml);
            //DcChqBookNo.SetFilter("bank_code='" + BankCode + "' and bank_branch ='" + BankBranch + "'");
            //DcChqBookNo.Filter();
        }

        private void GetBankBranch()
        {
            String ls_bank = DwCon.GetItemString(1, "as_bank");
            String ls_bankbranch = "";
            DataWindowChild DcBankBranch = DwCon.GetChild("as_bankbranch");
            Int32 BankBranchXml = fin.of_dddwbankbranch(state.SsWsPass, ls_bank, ref ls_bankbranch);
            DcBankBranch.ImportString(ls_bankbranch, FileSaveAsType.Text);
            DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            DcBankBranch.Filter();
        }
    }
}
