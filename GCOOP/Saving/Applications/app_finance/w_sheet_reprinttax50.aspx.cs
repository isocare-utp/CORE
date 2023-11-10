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
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_reprinttax50 : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwHead;
        protected String postPrint;
        protected String postSetItem;
        protected String postReceiptRetrieve;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("adtm_edate", "adtm_tedate");
            tDwHead.Add("adtm_sdate", "adtm_tsdate");
            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postSetItem = WebUtil.JsPostBack(this, "postSetItem");
            postReceiptRetrieve = WebUtil.JsPostBack(this, "postReceiptRetrieve");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                Int32 resultXml;
                String list_Xml = "";
                DwHead.InsertRow(0);
                DwHead.SetItemDateTime(1, "adtm_edate", state.SsWorkDate);
                DwHead.SetItemDateTime(1, "adtm_sdate", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();

                try
                {
                    String as_main_xml = DwHead.Describe("DataWindow.Data.XML");
                    resultXml = fin.of_retrievetaxpay(state.SsWsPass, state.SsCoopId, as_main_xml, ref list_Xml);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

                if (list_Xml != "")
                {
                    DwList.ImportString(list_Xml, FileSaveAsType.Xml);
                    DwList.Sort();
                }
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postReceiptRetrieve":
                    ReceiptRetrieve();
                    break;
                case "postSetItem":
                    SetItem();
                    break;
                case "postPrint":
                    Print();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion


        private void Print()
        {
            int row;
            row = DwList.FindRow("select_flag=1", 1, DwList.RowCount);

            if (row > 0)
            {

                try
                {
                    Boolean Check1, Check2, Check3;
                    Int16 ai_keep = 0, ai_formcoop = 0, ai_topay = 0;
                    DwList.SetFilter("select_flag=1");
                    DwList.Filter();

                    String as_slipno = DwList.GetItemString(1, "slip_no");

                    Check1 = CheckBox1.Checked;
                    if (Check1 == true)
                    {
                        ai_formcoop = 1;
                    }

                    Check2 = CheckBox2.Checked;
                    if (Check2 == true)
                    {
                        ai_topay = 1;
                    }

                    Check3 = CheckBox3.Checked;
                    if (Check3 == true)
                    {
                        ai_keep = 1;
                    }
                    //String result = fin.OfPostRePrintTaxPay(state.SsWsPass, state.SsCoopId, state.SsWorkDate, as_slipno, ai_topay, ai_keep, ai_formcoop, state.SsPrinterSet);

                    LtServerMessage.Text = WebUtil.CompleteMessage("พิมพ์เรียบร้อยแล้ว");
                    String list_Xml = "";
                    String as_main_xml = DwHead.Describe("DataWindow.Data.XML");
                    Int32 resultXml = fin.of_retrievetaxpay(state.SsWsPass, state.SsCoopId, as_main_xml, ref list_Xml);
                    DwList.Reset();
                    if (list_Xml != "")
                    {
                        DwList.ImportString(list_Xml, FileSaveAsType.Xml);
                    }

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่เลือกรายการ");
            }
        }

        private void SetItem()
        {
            String Coloum = HfColoum.Value;
            String NewValue = HfNewValue.Value;
            if (NewValue == "//" && Coloum == "adtm_tsdate")
            {
                DwHead.SetItemNull(1, "adtm_sdate");
                tDwHead.Eng2ThaiAllRow();
            }
            else if (NewValue == "//" && Coloum == "adtm_tedate")
            {
                DwHead.SetItemNull(1, "adtm_edate");
                tDwHead.Eng2ThaiAllRow();
            }
            else if (Coloum == "adtm_tsdate")
            {
                DwHead.SetItemDateTime(1, "adtm_sdate", Convert.ToDateTime(NewValue));
            }
            else if (Coloum == "adtm_tedate")
            {
                DwHead.SetItemDateTime(1, "adtm_edate", Convert.ToDateTime(NewValue));
            }
            else if (Coloum == "as_memberno")
            {
                String memberNo = WebUtil.MemberNoFormat(DwHead.GetItemString(1, "as_memberno"));
                DwHead.SetItemString(1, "as_memberno", memberNo);
            }
        }

        private void ReceiptRetrieve()
        {
            String ls_cond_xml, list_Xml = "";
            Int32 resultXml;
            try
            {
                tDwHead.Eng2ThaiAllRow();
                ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");

                resultXml = fin.of_retrievetaxpay(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref list_Xml);

                if (list_Xml != "")
                {
                    DwList.Reset();
                    DwList.ImportString(list_Xml, FileSaveAsType.Xml);
                    DwList.Sort();
                }
            }
            catch (Exception ex)
            {
                DwList.Reset();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}
