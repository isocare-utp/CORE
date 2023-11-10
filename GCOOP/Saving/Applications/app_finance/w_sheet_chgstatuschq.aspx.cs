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
    public partial class w_sheet_chgstatuschq : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        protected String postDetail;
        private DwThDate tDwMain;
        private DwThDate tDwDetail;
        

        String chqNo, chqBkNo, Bank, Branch;
        Int16 row, chqSts, seqNo;
        DateTime ldtm_wdate;



        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDetail = WebUtil.JsPostBack(this, "postDetail");
            tDwMain = new DwThDate(DwMain);
            tDwDetail = new DwThDate(DwDetail);
            tDwMain.Add("date_onchq", "date_tonchq");
            tDwDetail.Add("entry_date", "entry_tdate");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                String chqlist_Xml = "";
                Int32 result = fin.of_retrievechangechqstatus(state.SsWsPass, state.SsCoopId, ref chqlist_Xml);
                if (chqlist_Xml != "")
                {
                    DwMain.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                    DwMain.Sort();
                    decimal row = DwMain.RowCount;
                    
                    if (row > 0)
                    {
                        for (int i = 1; i <= row;i++ )
                        {
                            DwMain.SetItemString(i, "coop_id", state.SsCoopId);
                        }
                    }
                    tDwMain.Eng2ThaiAllRow();
                }
                DwDetail.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDetail")
            {
                GetDetail();
            }
        }

        public void SaveWebSheet()
        {
            int rowNum = DwMain.RowCount;
            int re = 0;

            for (int i = 1; i <= rowNum; i++)
            {
                chqNo = DwMain.GetItemString(i, "cheque_no");
                chqBkNo = DwMain.GetItemString(i, "chequebook_no");
                Bank = DwMain.GetItemString(i, "bank_code");
                Branch = DwMain.GetItemString(i, "bank_branch");
                seqNo = Convert.ToInt16(DwMain.GetItemDecimal(i, "seq_no"));
                chqSts = Convert.ToInt16(DwMain.GetItemDecimal(i, "chqeue_status"));
                ldtm_wdate = DwMain.GetItemDateTime(i, "date_onchq");

                if (chqNo != "")
                {
                    try
                    {
                        re = fin.of_postchangedstatuschq(state.SsWsPass, state.SsCoopId, state.SsUsername, ldtm_wdate, state.SsClientIp, chqNo, chqBkNo, Bank, Branch, seqNo, chqSts);
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

            if (re == 1)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                String chqlist_xml = "";
                Int32 result = fin.of_retrievechangechqstatus(state.SsWsPass, state.SsCoopId, ref chqlist_xml);
                DwMain.Reset();
                DwMain.ImportString(chqlist_xml, FileSaveAsType.Xml);
                DwMain.Sort();
                tDwMain.Eng2ThaiAllRow();
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion



        private void GetDetail()
        {
            row = Convert.ToInt16(HfRow.Value);

            chqNo = DwMain.GetItemString(row, "cheque_no");
            chqBkNo = DwMain.GetItemString(row, "chequebook_no");
            Bank = DwMain.GetItemString(row, "bank_code");
            Branch = DwMain.GetItemString(row, "bank_branch");
            seqNo = Convert.ToInt16(DwMain.GetItemDecimal(row, "seq_no"));
            string chqXml = "";
            Int32 result = fin.of_retrievechangchqdetail(state.SsCoopId, chqNo, chqBkNo, Bank, Branch, seqNo, ref chqXml,state.SsWsPass);
            DwDetail.Reset();
            DwDetail.ImportString(chqXml, FileSaveAsType.Xml);
            DwDetail.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            tDwDetail.Eng2ThaiAllRow();
        }
    }
}
