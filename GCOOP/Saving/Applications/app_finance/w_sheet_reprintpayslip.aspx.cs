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
    public partial class w_sheet_reprintpayslip : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwHead;
        protected String postPaySlipRetrieve;
        protected String postSetItem;
        protected String postPrint;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("adtm_date", "adtm_tdate");
            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postPaySlipRetrieve = WebUtil.JsPostBack(this, "postPaySlipRetrieve");
            postSetItem = WebUtil.JsPostBack(this, "postSetItem");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwHead.InsertRow(0);

                DwUtil.RetrieveDDDW(DwHead, "from_system", "reprintpayslip.pbl", null);
                DwHead.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                String slip_Xml = "";
                String ls_cond_xml = DwHead.Describe("DataWindow.Data.Xml");

                Int32 resultXml = fin.of_retrievereprintpayslip(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref slip_Xml);

                if (slip_Xml != "")
                {
                    DwList.Reset();
                    //DwList.ImportString(slip_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(slip_Xml, DwList, tDwHead, FileSaveAsType.Xml);
                    DwList.Sort();
                }

            }
            else
            {
                this.RestoreContextDw(DwHead, tDwHead);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postPaySlipRetrieve":
                    PaySlipRetrieve();
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
            if (state.SsCoopControl == "006001") //แก้ไขให้ระยองสามารถเลือกระบบงานที่จะพิมพ์ได้
            {
                DwHead.Modify("t_4.Visible=true");
                DwHead.Modify("from_system.Visible=true");
            }
            tDwHead.Eng2ThaiAllRow();
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion


        private void Print()
        {
            int row;
            row = DwList.FindRow("ai_select=1", 1, DwList.RowCount);

            if (row > 0)
            {
                try
                {
                    DwList.SetFilter("ai_select=1");
                    DwList.Filter();
                    String as_list_xml = DwList.Describe("DataWindow.Data.XML");
                    String slipNo = DwList.GetItemString(1, "slip_no").Trim();

                    String result = "";
                    if (state.SsCoopId == "009001")
                    {
                        //เรียกปริ้น pbslip
                        Printing.PrintFinSlipPayHND(this, state.SsCoopId, slipNo);
                    }
                    else if (state.SsCoopId == "011001")
                    {
                        //สอ.ปตท.
                        Printing.PrintFinSlipPayPTT(this, state.SsCoopId, slipNo);
                    }
                    else if (state.SsCoopControl == "006001")
                    {
                        Printing.PrintFinPaySlipIreportExat(this, slipNo);
                    }
                    else if (state.SsCoopControl == "034001" || state.SsCoopControl == "040001" || state.SsCoopControl == "038001")
                    {
                        try
                        {

                            String ReportName = "";
                            if (state.SsCoopControl == "034001") { ReportName = "ir_printfin_payslip_clound"; }
                            else if (state.SsCoopControl == "040001") { ReportName = "ir_printfin_payslip_clound_a4"; }
                            else if (state.SsCoopControl == "038001") { ReportName = "ir_printfin_payslip_clound_tak_a4"; }
                            iReportArgument args = new iReportArgument();
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_slipno", iReportArgumentType.String, slipNo);
                            iReportBuider report = new iReportBuider(this, "กำลังสร้างใบสำคัญจ่าย");
                            report.AddCriteria(ReportName, "ใบสำคัญจ่าย", ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างใบสำคัญจ่ายได้");
                        }
                    }
                    else
                    {
                        Printing.PrintFinSlipPay(this, state.SsCoopId, slipNo);
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

        private void PaySlipRetrieve()
        {
            try
            {
                String slip_Xml = "";
                String ls_cond_xml = DwHead.Describe("DataWindow.Data.Xml");
                Int32 resultXml = fin.of_retrievereprintpayslip(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref slip_Xml);
                if (slip_Xml != "")
                {
                    DwList.Reset();
                    //DwList.ImportString(slip_Xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(slip_Xml, DwList, tDwHead, FileSaveAsType.Xml);
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
