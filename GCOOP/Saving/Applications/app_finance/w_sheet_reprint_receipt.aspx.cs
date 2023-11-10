using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_reprint_receipt : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsFind;
        protected String newClear;
        protected String jsPrint;
        protected String jsFilter;
        protected String postcheckAll;

        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsFind = WebUtil.JsPostBack(this, "jsFind");
            jsPrint = WebUtil.JsPostBack(this, "jsPrint");
            jsFilter = WebUtil.JsPostBack(this, "jsFilter");
            postcheckAll = WebUtil.JsPostBack(this, "postcheckAll");


            tdwhead = new DwThDate(dw_main, this);
            tdwhead.Add("receiptdate_start", "receiptdate_tstart");
            tdwhead.Add("receiptdate_end", "receiptdate_tend");

        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
            dw_detail.SetTransaction(sqlca);
            if (IsPostBack)
            {

                try
                {
                    dw_main.RestoreContext();

                    this.RestoreContextDw(dw_detail);
                }
                catch { }

            }
            if (dw_main.RowCount < 1)
            {
                JsNewClear();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsFind")
            {
                JsFind();
            }
            else if (eventArg == "jsPrint")
            {
                JsPrint();
            }
            else if (eventArg == "jsFilter")
            {
                JsFilter();
            }
            else if (eventArg == "postcheckAll")
            {
                JsCheckAll();
            }


        }

        public void SaveWebSheet()
        {
            try
            {
                String dwdetail_XML = dw_detail.Describe("DataWindow.Data.XML");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        private void JsPostMember()
        {
            try
            {

                String memno = dw_main.GetItemString(1, "member_no");
                memno = WebUtil.MemberNoFormat(memno);
                dw_main.SetItemString(1, "member_no", memno);
                dw_main.SetItemString(1, "coop_id", state.SsCoopId);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }


        }

        private void JsNewClear()
        {
            dw_main.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(1);

            dw_main.SetItemDate(1, "receiptdate_start", state.SsWorkDate);
            dw_main.SetItemDate(1, "receiptdate_end", state.SsWorkDate);
            dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            tdwhead.Eng2ThaiAllRow();
            dw_detail.Retrieve(state.SsCoopId, state.SsWorkDate, state.SsWorkDate);
        }

        private void JsFind()
        {

            String xml_main = dw_main.Describe("Datawindow.data.XML");
            String xml_detail = dw_detail.Describe("Datawindow.data.XML");
            try
            {
                //int result = shrlonService.of_initlist_payinreceipt(state.SsWsPass, xml_main, ref xml_detail);
                //if (result == 1)
                //{
                //    dw_detail.Reset();
                //    if (xml_detail == "")
                //    {
                //        // LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
                //        dw_detail.Retrieve(state.SsCoopId);
                //    }
                //    else
                //    {
                //        dw_detail.ImportString(xml_detail, FileSaveAsType.Xml);
                //    }
                //}
                //else
                //{
                //    // LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
                //}
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsPrint()
        {


            dw_detail.SetFilter("operate_flag=" + 1 + "");
            dw_detail.Filter();
            String xml_detail = dw_detail.Describe("Datawindow.data.XML");
            String form_set = "";
            try
            {
                form_set = state.SsPrinterSet;
            }
            catch { form_set = "216"; }
            try
            {
                string as_xml = ""; string re = "";
                string payinslip_no = dw_detail.GetItemString(1, "payinslip_no");
                string memberNo = dw_detail.GetItemString(1, "member_no");
                if (xmlconfig.LnReceivePrintMode == 0)
                {
                    //re = shrlonService.of_printslippayin(state.SsWsPass, payinslip_no, form_set, state.SsCoopControl, memberNo, ref as_xml);
                }
                else
                {
                    JsPrintReceive(state.SsCoopId, payinslip_no, xmlconfig.LnReceivePrintMode );
                }

                if (re == "1")
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("สั่งพิมพ์เรียบร้อยแล้ว...");
                    JsFind();
                }
                else if (re == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("การพิมพ์ผิดพลาด...");
                }

            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                //ErrorMessage("กรุณาเลือกเครื่องพิมพ์... ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        private void JsFilter()
        {
            try
            {
                //    Decimal operate=dw_detail.GetItemDecimal(1, "operate_flag"); 


                //    if (operate == 1)
                //    {
                dw_detail.SetFilter("operate_flag= 1");
                dw_detail.Filter();
                //}



            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsCheckAll()
        {
            Boolean Select = CheckAll.Checked;
            Decimal Set = 1;

            if (Select == true)
            {
                Set = 1;
            }
            else if (Select == false)
            {
                Set = 0;
            }
            for (int i = 1; i <= dw_detail.RowCount; i++)
            {
                dw_detail.SetItemDecimal(i, "operate_flag", Set);
            }
        }

        private void JsPrintReceive(string coopId, string reqdoc_no, int printMode)
        {
            Printing.ShrlonPrintSlipPayIn(this, coopId, reqdoc_no, printMode);
        }
    }
}
