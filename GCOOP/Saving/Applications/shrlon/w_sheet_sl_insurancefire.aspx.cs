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
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_insurancefire : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostPayInList;
        protected String newClear;
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostPayInList = WebUtil.JsPostBack(this, "jsPostPayInList");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdwhead = new DwThDate(dw_head, this);
            tdwhead.Add("slip_date", "slip_tdate");
            tdwhead.Add("operate_date ", "operate_tdate ");
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

            if (IsPostBack)
            {

                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_head.RestoreContext();
                    this.RestoreContextDw(dw_detail);
                }
                catch { }

            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
                dw_list.InsertRow(1);
                dw_head.InsertRow(1);
                dw_detail.InsertRow(1);
                dw_head.SetItemDate(1, "slip_date", state.SsWorkDate);
                dw_head.SetItemDate(1, "operate_date ", state.SsWorkDate);
                tdwhead.Eng2ThaiAllRow();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsPostPayInList")
            {
                JsPostPayInList();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = dw_head.GetItemString(1, "payinslip_no");
                slipcancle.cancel_id = state.SsUsername;
                slipcancle.cancel_date = state.SsWorkDate;
                slipcancle.cancel_coopid = state.SsCoopId;
                slipcancle.slipcoop_id = state.SsCoopId;
                int result = shrlonService.of_saveccl_payin(state.SsWsPass, slipcancle);
                if (result == 1) { LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย"); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            dw_head.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        private void JsPostMember()
        {
            try
            {
                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.member_no = Hfmember_no.Value;
                slipcancle.xml_memdet = dw_main.Describe("DataWindow.Data.XML");
                slipcancle.xml_sliplist = dw_list.Describe("DataWindow.Data.XML");
                slipcancle.cancel_coopid = state.SsCoopId;
                slipcancle.slipcoop_id = state.SsCoopId;
                int result = shrlonService.of_initccl_slippayinall(state.SsWsPass, ref slipcancle);
                if (result == 1)
                {

                }
                try
                {
                    dw_main.Reset();
                    dw_main.ImportString(slipcancle.xml_memdet, FileSaveAsType.Xml);

                    if (dw_main.RowCount > 1)
                    {
                        dw_main.DeleteRow(dw_main.RowCount);
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                    dw_main.Reset(); dw_main.InsertRow(0);


                }
                try
                {

                    dw_list.Reset();
                    dw_list.ImportString(slipcancle.xml_sliplist, FileSaveAsType.Xml);

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรายการ Slip ของสมาชิก");

                    dw_list.Reset(); dw_list.InsertRow(0);

                }


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }


        }
        private void JsPostPayInList()
        {
            try
            {

                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = HfSlipNo.Value;
                slipcancle.cancel_coopid = state.SsCoopId;
                slipcancle.slipcoop_id = state.SsCoopId;
                int result = shrlonService.of_initccl_slippayindet(state.SsWsPass, ref slipcancle);
                try
                {


                    dw_head.Reset();
                    dw_head.ImportString(slipcancle.xml_sliphead, FileSaveAsType.Xml);

                    if (dw_head.RowCount > 1)
                    {
                        dw_head.DeleteRow(dw_head.RowCount);
                    }
                    // CalculateAmt();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                    dw_head.Reset(); dw_head.InsertRow(0);

                }

                try
                {
                    dw_detail.Reset();
                    dw_detail.ImportString(slipcancle.xml_slipdetail, FileSaveAsType.Xml);


                }
                catch (Exception ex)
                {
                    DwUtil.ImportData(slipcancle.xml_slipdetail, dw_detail, null, FileSaveAsType.Xml);
                    //LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่"+ex);
                    //dw_detail.Reset(); dw_detail.InsertRow(0);

                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }
        private void CalculateAmt()
        {
            Decimal totalamt = 0;

            int dw_detailrow = dw_detail.RowCount;
            for (int i = 1; i <= dw_detailrow; i++)
            {
                Decimal item_payamt = dw_detail.GetItemDecimal(i, "item_payamt");

                if (item_payamt != 0)
                {
                    totalamt = totalamt + item_payamt;

                }
            }
            dw_head.SetItemDecimal(1, "slip_amt", totalamt);

        }
        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_list.Reset();
            dw_head.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(1);
            dw_list.InsertRow(1);
            dw_head.InsertRow(1);
            dw_head.SetItemDate(1, "slip_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "operate_date ", state.SsWorkDate);
            tdwhead.Eng2ThaiAllRow();
            dw_detail.InsertRow(1);
        }
    }
}
