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
    public partial class w_sheet_reprintreceipt : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwHead;
        protected String postPrint;
        protected String postReceiptRetrieve;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("adtm_date", "adtm_tdate");

            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postReceiptRetrieve = WebUtil.JsPostBack(this, "postReceiptRetrieve");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwHead.InsertRow(0);

                DwUtil.RetrieveDDDW(DwHead, "from_system", "reprintreceipt.pbl", null);
                DwHead.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                String ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");
                String list_Xml = "";
                Dwlist.Reset();
                Int32 resultXml = fin.of_retrievereprintreceipt(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref list_Xml);
                if (list_Xml != "")
                {
                    //Dwlist.ImportString(list_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(list_Xml, Dwlist, tDwHead, FileSaveAsType.Xml);
                }
            }
            else
            {
                this.RestoreContextDw(DwHead, tDwHead);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postReceiptRetrieve":
                    ReceiptRetrieve();
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
            DwUtil.RetrieveDDDW(DwHead, "from_system", "reprintreceipt.pbl", null);
            tDwHead.Eng2ThaiAllRow();
            DwHead.SaveDataCache();
            Dwlist.SaveDataCache();
        }

        #endregion


        private void Print()
        {
            int row;
            row = Dwlist.FindRow("ai_select=1", 1, Dwlist.RowCount);

            if (row > 0)
            {
                try
                {
                    Dwlist.SetFilter("ai_select=1");
                    Dwlist.Filter();
                    String as_list_xml = Dwlist.Describe("DataWindow.Data.XML");
                    String slipNo = Dwlist.GetItemString(1, "slip_no").Trim();
                    //DateTime wDate = DwHead.GetItemDateTime(1, "adtm_date");
                    String result = "";
                    if (state.SsCoopId == "009001")
                    {
                        //เรียกปริ้น pbslip
                        Printing.PrintFinSlipRecvHND(this, state.SsCoopId, slipNo);
                    }
                    else if( state.SsCoopId == "008001") {
                        Printing.PrintFinslipRecvPEA(this, slipNo ,state.SsCoopId ); 
                    }
                    else if (state.SsCoopControl == "006001")
                    {
                        Printing.PrintFinRecvSlipIreportExat(this, slipNo);
                    }
                    else if (state.SsCoopControl == "034001" || state.SsCoopControl == "040001" || state.SsCoopControl == "038001")
                    {
                        try
                        {
                            String ReportName = "";
                            if (state.SsCoopControl == "034001") { ReportName = "ir_printfin_receipt_clound"; }
                            else if (state.SsCoopControl == "040001") { ReportName = "ir_printfin_receipt_clound_a4"; }
                            else if (state.SsCoopControl == "038001") { ReportName = "ir_printfin_receipt_clound_tak_a4"; }
                            iReportArgument args = new iReportArgument();
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_slipno", iReportArgumentType.String, slipNo);
                            iReportBuider report = new iReportBuider(this, "กำลังสร้างใบเสร็จรับเงิน");
                            report.AddCriteria(ReportName, "ใบเสร็จรับเงิน", ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างใบเสร็จรับเงินได้");
                            //return false;
                        }
                    }
                    else
                    {
                        Printing.PrintFinSlipRecv(this, state.SsCoopId, slipNo);
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
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่เลือกรายการ");
            }
        }

        private void ReceiptRetrieve()
        {
            try
            {
                String ls_cond_xml = DwHead.Describe("DataWindow.Data.XML");
                String list_Xml = "";
                Int32 resultXml = fin.of_retrievereprintreceipt(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref list_Xml);
                if (list_Xml != "")
                {
                    Dwlist.Reset();
                    //Dwlist.ImportString(list_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(list_Xml, Dwlist, tDwHead, FileSaveAsType.Xml);
                    Dwlist.Sort();
                }
            }
            catch (Exception ex)
            {
                Dwlist.Reset();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}
