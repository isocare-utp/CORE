using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon
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
        protected String jsPostEntry;
        protected String jsPostcoopId;

        string reqdoc_no = "";
        String member_no = "";
        string fromset = "";
        int x = 1;
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsFind = WebUtil.JsPostBack(this, "jsFind");
            jsPrint = WebUtil.JsPostBack(this, "jsPrint");
            jsFilter = WebUtil.JsPostBack(this, "jsFilter");
            postcheckAll = WebUtil.JsPostBack(this, "postcheckAll");
            jsPostEntry = WebUtil.JsPostBack(this, "jsPostEntry");
            jsPostcoopId = WebUtil.JsPostBack(this, "jsPostcoopId");
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


                this.RestoreContextDw(dw_main);

                this.RestoreContextDw(dw_detail);

            }
            else
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
            else if (eventArg == "jsPostEntry")
            {
                JsPostEntry();
            }
            else if (eventArg == "jsPostcoopId")
            {
                JsPostcoopId();
            }

        }

        private void JsPostcoopId()
        {
            string coop_id = dw_main.GetItemString(1, "coop_id");
        
            dw_detail.Retrieve(coop_id,state.SsWorkDate);
        }

        private void JsPostEntry()
        {
            try
            {


                String entry_id = "";
                try { entry_id = dw_main.GetItemString(1, "entry_id"); }
                catch { entry_id = ""; }

                dw_main.SetItemString(1, "entry_id", entry_id);
                dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                dw_detail.SetFilter(" SLSLIPPAYIN.ENTRY_ID='" + entry_id + "'");
                dw_detail.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

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
                dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                dw_detail.SetFilter("SLSLIPPAYIN.MEMBER_NO='" + memno + "'");
                dw_detail.Filter();
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
            dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_main, "coop_id", "reprint_receipt.pbl", null);
            tdwhead.Eng2ThaiAllRow();
            string coop_id = state.SsCoopControl;
            string entry_id = state.SsUsername;
            dw_detail.Retrieve(coop_id, entry_id, state.SsWorkDate);
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
                //        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
                //        string coop_id = state.SsCoopId;                     
                //        dw_detail.Retrieve(coop_id,  state.SsWorkDate);
                //    }
                //    else
                //    {
                //        dw_detail.ImportString(xml_detail, FileSaveAsType.Xml);
                //    }
                //}
                //else
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
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

            String xml_detail = dw_detail.Describe("Datawindow.data.XML");
            String form_set = "";

            int row;
            row = dw_detail.FindRow("operate_flag=1", 1, dw_detail.RowCount);
            string as_xml = "";
            if (row > 0)
            {


                try
                {
                    try
                    {
                        form_set = state.SsPrinterSet;
                    }
                    catch { form_set = "216"; }
                    member_no = dw_detail.GetItemString(row, "member_no");
                    reqdoc_no = dw_detail.GetItemString(row, "payinslip_no");

                    if (xmlconfig.LnReceivePrintMode == 0)
                    {
                        //string re = wcf.NShrlon.of_printreceipt(state.SsWsPass, reqdoc_no, form_set, state.SsCoopControl, member_no, ref as_xml);
                        //if (re == "1") { LtServerMessage.Text = WebUtil.CompleteMessage("พิมพ์ใบเสร็จ  " + member_no + "  เรียบร้อย "); }
                    }
                    else
                    {
                        JsPrintReceive(state.SsCoopId, reqdoc_no, xmlconfig.LnReceivePrintMode);
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }

            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่เลือกรายการ");
            }


            string coop_id = state.SsCoopControl;
            string entry_id = state.SsUsername;
            dw_detail.Retrieve(coop_id, state.SsWorkDate);



        }

        private void JsPrintReceive(string coopId, string reqdoc_no, int printMode)
        {
            Printing.ShrlonPrintSlipPayout(this, coopId, reqdoc_no, printMode);
        }

        private void JsFilter()
        {
            //try
            //{
            //    Decimal operate = dw_detail.GetItemDecimal(1, "operate_flag");


            //    if (operate == 1)
            //    {
            //        dw_detail.SetFilter("operate_flag= 1");
            //        dw_detail.Filter();
            //    }



            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //}
            dw_main.SetItemString(1, "member_no", "");
            string coop_id = state.SsCoopControl;
            string entry_id = state.SsUsername;
            dw_detail.Retrieve(coop_id, state.SsWorkDate);
        }

        private void JsCheckAll()
        {
            Boolean Select = false;// CheckAll.Checked;
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
    }
}
