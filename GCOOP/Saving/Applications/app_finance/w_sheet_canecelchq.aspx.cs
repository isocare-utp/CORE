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
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_canecelchq : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwList;
        private DwThDate tDwHead;
        private String pbl = "canecelchq.pbl";
        protected String postChqSearch;
        protected String postBankBranch;
        protected String postFormat;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwList = new DwThDate(DwList,this);
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("end_date", "end_tdate");
            tDwHead.Add("start_date", "start_tdate");
            tDwList.Add("date_onchq", "date_tonchq");
            postFormat = WebUtil.JsPostBack(this, "postFormat");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postChqSearch = WebUtil.JsPostBack(this, "postChqSearch");
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwHead.InsertRow(0);

                DwHead.SetItemDateTime(1, "start_date", state.SsWorkDate);
                //tDwHead.Eng2ThaiAllRow();

                DwHead.SetItemDateTime(1, "end_date", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();

                String ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");
                String chqcancel = "";
                Int32 result = fin.of_retrievecancelchq(state.SsWsPass, state.SsCoopId, ls_cond_xml,ref chqcancel);
                if (chqcancel != "")
                {
                    //DwList.ImportString(chqcancel, FileSaveAsType.Xml);
                    DwUtil.ImportData(chqcancel, DwList, tDwHead, FileSaveAsType.Xml);
                    tDwList.Eng2ThaiAllRow();
                    DwList.Sort();

                    DwUtil.RetrieveDDDW(DwHead, "bank", "canecelchq.pbl", null);
                    DwUtil.RetrieveDDDW(DwList, "cancel_reson", "canecelchq.pbl", state.SsCoopId);
                }
               
            }
            else
            {
                //DateTime sdate = DwHead.GetItemDateTime(1, "start_date");
                //DwHead.SetItemDateTime(1, "start_date", sdate);
                String stdate = DwHead.GetItemString(1, "start_tdate");
                //stdate = str_rptexcel 
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postBankBranch":
                    GetBankBranch();
                    break;
                case "postChqSearch":
                    ChqSearch();
                    break;
                case "postFormat":
                    Format();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                //int row = 0;
                //Int16 i, li_action, li_flag;
                //String ls_chqno, ls_chqbookno, ls_bank, ls_bankbranch, ls_branch = "", ls_cancleid, ls_machine, ls_cancel_reson;
                //DateTime ldtm_wdate;

                String cancellist_xml = DwList.Describe("DataWindow.Data.XML");

                int re = fin.of_postcancelchq(state.SsWsPass, state.SsCoopId, state.SsWorkDate, state.SsUsername, state.SsClientIp, cancellist_xml);

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    String ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");
                    String chqcancel_xml = "";
                    Int32 result = fin.of_retrievecancelchq(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref chqcancel_xml);
                    DwList.Reset();

                    if (chqcancel_xml != "")
                    {
                        //DwList.ImportString(chqcancel_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(chqcancel_xml, DwList, tDwHead, FileSaveAsType.Xml);
                        tDwList.Eng2ThaiAllRow();
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion


        private void Format()
        {
            String ContactNo = HfContact.Value;
            if (ContactNo != "")
            {
                ContactNo = "0000000" + ContactNo;
                ContactNo = WebUtil.Right(ContactNo, 7);

                DwHead.SetItemString(1, "chq_no", ContactNo);
            }
            else
            {
                DwHead.SetItemString(1, "chq_no", "");
            }
        }

        private void ChqSearch()
        {
            //String chqNo, BankNo, BankBranch;
            //String FillStr = "";

            //try
            //{
            //    chqNo = DwHead.GetItemString(1, "chq_no");
            //}
            //catch { chqNo = ""; }

            //try
            //{
            //    BankNo = DwHead.GetItemString(1, "bank");
            //}
            //catch { BankNo = ""; }

            //try
            //{
            //    BankBranch = DwHead.GetItemString(1, "branch");
            //}
            //catch { BankBranch = ""; }

            //if ((chqNo != "") && (BankNo == "") && (BankBranch == ""))
            //{
            //    FillStr = "cheque_no = '" + chqNo + "'";
            //}
            //else if ((chqNo == "") && (BankNo != "") && (BankBranch == ""))
            //{
            //    FillStr = "bank_code = '" + BankNo + "'";
            //}
            //else if ((chqNo == "") && (BankNo == "") && (BankBranch != ""))
            //{
            //    FillStr = "bank_branch = '" + BankBranch + "'";
            //}
            //else if ((chqNo != "") && (BankNo != "") && (BankBranch == ""))
            //{
            //    FillStr = "cheque_no = '" + chqNo + "'and bank_code = '" + BankNo + "'";
            //}
            //else if ((chqNo != "") && (BankNo == "") && (BankBranch != ""))
            //{
            //    FillStr = "cheque_no = '" + chqNo + "'and bank_branch = '" + BankBranch + "'";
            //}
            //else if ((chqNo == "") && (BankNo != "") && (BankBranch != ""))
            //{
            //    FillStr = "bank_code = '" + BankNo + "'and bank_branch = '" + BankBranch + "'";
            //}
            //else if ((chqNo != "") && (BankNo != "") && (BankBranch != ""))
            //{
            //    FillStr = "cheque_no = '" + chqNo + "'and bank_code = '" + BankNo + "'and bank_branch = '" + BankBranch + "'";
            //}
            //else if ((chqNo == "") && (BankNo == "") && (BankBranch == ""))
            //{
            //    FillStr = "";
            //}

            //DwList.SetFilter(FillStr);
            //DwList.Filter();

            DateTime sdate = DwHead.GetItemDateTime(1, "start_date");
            DwHead.SetItemDateTime(1, "start_date", sdate);   
            String ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");
            String chqcancel_Xml = "";
            Int32 result = fin.of_retrievecancelchq(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref chqcancel_Xml);
            DwList.Reset();
            if (chqcancel_Xml != "")
            {
                //DwList.ImportString(chqcancel_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqcancel_Xml, DwList, tDwHead, FileSaveAsType.Xml);
                DwUtil.RetrieveDDDW(DwHead, "bank", "canecelchq.pbl", null);
                DwUtil.RetrieveDDDW(DwList, "cancel_reson", "canecelchq.pbl", state.SsCoopId);
                tDwList.Eng2ThaiAllRow();
                DwList.Sort();
            }

            tDwHead.Eng2ThaiAllRow();
            

        }

        private void GetBankBranch()
        {
            String ls_bank = DwHead.GetItemString(1, "bank");
            DwUtil.RetrieveDDDW(DwHead, "branch", pbl, ls_bank);
            //DataWindowChild DcBankBranch = DwHead.GetChild("branch");
            //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
            //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();
        }
    }
}
