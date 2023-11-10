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
using CoreSavingLibrary.WcfNFinance;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_paytrnbank_operate : PageWebSheet, WebSheet
    {
        protected String postListPaymentDoc;
        protected String postMainAndDetail;
        protected String postSearchPaymentDoc;
        private n_financeClient finService;
        private DwThDate tDwDate;
        private DwThDate tDwMain;



        public void InitJsPostBack()
        {
            postListPaymentDoc = WebUtil.JsPostBack(this, "postListPaymentDoc");
            postMainAndDetail = WebUtil.JsPostBack(this, "postMainAndDetail");
            postSearchPaymentDoc = WebUtil.JsPostBack(this, "postSearchPaymentDoc");
            tDwDate = new DwThDate(DwDate, this);
            tDwDate.Add("entry_date", "entry_tdate");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("trn_date", "trn_tdate");
            tDwMain.Add("doc_date", "doc_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                finService = wcf.NFinance;
            }
            catch (Exception)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
                this.RestoreContextDw(DwFind);
                this.RestoreContextDw(DwList);
                this.RestoreContextDw(DwDate);
            }
            if (DwMain.RowCount < 1)
            {
                DwDate.InsertRow(0);
                DwMain.InsertRow(0);
                DwFind.InsertRow(0);
                DwDate.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                tDwDate.Eng2ThaiAllRow();
            }
            JsPostListPaymentDoc();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postListPaymentDoc")
            {
                JsPostListPaymentDoc();
            }
            else if (eventArg == "postMainAndDetail")
            {
                JsPostMainAndDetail();
            }
            else if (eventArg == "postSearchPaymentDoc")
            {
                JsPostSearchPaymentDoc();
            }
        }

        public void SaveWebSheet()
        {
            String xmlDetail = "";
            try
            {
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว...");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwDetail.SaveDataCache();
            DwMain.SaveDataCache();
            DwFind.SaveDataCache();
            DwList.SaveDataCache();
            DwDate.SaveDataCache();
        }



        private void JsPostListPaymentDoc()
        {
            DateTime date = DwDate.GetItemDateTime(1, "entry_date");
            String xmlDate = "";
            try
            {
                xmlDate = finService.of_getlist_moneyorder(state.SsWsPass, date);
                if (xmlDate != "" && xmlDate != null)
                {
                    try
                    {
                        DwUtil.ImportData(xmlDate, DwList, null, FileSaveAsType.Xml);
                        DwList.Sort();
                        DwList.SelectRow(0, false);
                        DwList.SelectRow(1, true);
                        DwList.SetRow(1);
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลวันที่ : " + date.ToString("dd/MM/yyyy"));
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsPostMainAndDetail()
        {
            String payTrnDoc = "";
            int rowList = 0;
            payTrnDoc = HdPayTrnDoc.Value;
            rowList = int.Parse(HdRowList.Value);
            MainAndDetail(payTrnDoc, rowList);
        }

        private void JsPostSearchPaymentDoc()
        {
            String paymentDoc = "";
            paymentDoc = HdPaymentDoc.Value;
            try
            {
                int rowList = 0;
                try
                {
                    rowList = DwList.FindRow("paymentdoc_no = '" + paymentDoc + "'", 0, DwList.RowCount);
                }
                catch { rowList = 0; }
                if (rowList > 0)
                {
                    String payTrnDoc = "";
                    payTrnDoc = DwList.GetItemString(rowList, "paytrnbank_docno");
                    MainAndDetail(payTrnDoc, rowList);
                    DwList.SetFilter("paymentdoc_no = '" + paymentDoc + "'");
                    DwList.Filter();
                }
                else
                {
                    DwFind.Reset();
                    DwFind.InsertRow(0);
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void MainAndDetail(String payTrnDoc, int row)
        {
            String xmlMain = "";
            String xmlDetail = "";
            try
            {
                Int32 result;
                result = finService.of_getdata_moneyorder(state.SsWsPass, payTrnDoc, ref xmlMain, ref xmlDetail);
                if (xmlMain != "" && xmlMain != null)
                {
                    try
                    {
                        DwMain.Reset();
                        DwUtil.ImportData(xmlMain, DwMain, null, FileSaveAsType.Xml);
                        tDwMain.Eng2ThaiAllRow();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลเลขที่ธนาณัติ : " + payTrnDoc);
                    }
                }
                if (xmlDetail != "" && xmlDetail != null)
                {
                    try
                    {
                        DwDetail.Reset();
                        DwUtil.ImportData(xmlDetail, DwDetail, null, FileSaveAsType.Xml);
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลเลขที่ธนาณัติ : " + payTrnDoc);
                    }
                }

                DwList.SelectRow(0, false);
                DwList.SelectRow(row, true);
                DwList.SetRow(row);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}
