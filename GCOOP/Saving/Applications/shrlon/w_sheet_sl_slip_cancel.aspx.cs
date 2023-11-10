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
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_slip_cancel : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private DwThDate tdwdetail;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostPayInList;
        protected String newClear;
        DateTime operate_date;

        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostPayInList = WebUtil.JsPostBack(this, "jsPostPayInList");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdwhead = new DwThDate(dw_head, this);
            tdwdetail = new DwThDate(dw_detail, this);
            tdwhead.Add("slip_date", "slip_tdate");
            tdwhead.Add("operate_date", "operate_tdate");
            tdwdetail.Add("cancel_date", "cancel_tdate");
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
                HdIsPostBack.Value = "false";
                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_head.RestoreContext();
                    this.RestoreContextDw(dw_detail);
                }
                catch { }

            }
            else
            {
                
                HdIsPostBack.Value = "true";
                if (dw_main.RowCount < 1)
                {
                    dw_main.InsertRow(0);
                    dw_list.InsertRow(0);
                    dw_head.InsertRow(0);
                    dw_detail.InsertRow(0);
                    dw_head.SetItemDate(1, "slip_date", state.SsWorkDate);
                    dw_head.SetItemDate(1, "operate_date", state.SsWorkDate);
                    dw_detail.SetItemDate(1, "cancel_date", state.SsWorkDate);
                    tdwhead.Eng2ThaiAllRow();
                    tdwdetail.Eng2ThaiAllRow();
                }
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
                ChkClsDay();
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
                slipcancle.cancel_date = dw_head.GetItemDateTime(1, "cancel_date");
                slipcancle.cancel_coopid = state.SsCoopId;
                slipcancle.slipcoop_id = dw_head.GetItemString(1, "coop_id");
                int result = shrlonService.of_saveccl_payin(state.SsWsPass, slipcancle);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย");
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            HdIsPostBack.Value = "true";
        }

        public void WebSheetLoadEnd()
        {
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            dw_head.SaveDataCache();
            dw_detail.SaveDataCache();

            dw_main.SaveDataCache();
            dw_list.SaveDataCache();
        }

        //เช็คปิดวัน
        private void ChkClsDay()
        {
            CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            try
            {
                decimal closeday_status = state.SsCloseDayStatus;
                if (closeday_status == 1)
                {
                    try
                    {
                        DateTime adtm_nextworkdate = new DateTime();
                        int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref adtm_nextworkdate);
                        if (result == 1)
                        {
                            this.SetOnLoadedScript("alert('ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy", th) + " ')");
                            dw_head.SetItemDate(1, "cancel_date", adtm_nextworkdate);
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                else
                {
                    operate_date = state.SsWorkDate;
                    dw_head.SetItemDate(1, "cancel_date", state.SsWorkDate);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JsPostMember()
        {
            try
            {
                string ls_membno = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                dw_main.SetItemString(1, "member_no", ls_membno);

                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.member_no = ls_membno;
                slipcancle.memcoop_id = state.SsCoopControl;

                int result = shrlonService.of_initccl_slippayinall(state.SsWsPass,ref slipcancle);

                try
                {
                    dw_main.Reset();
                    //dw_main.ImportString(slipcancle.xml_memdet, FileSaveAsType.Xml);
                    DwUtil.ImportData(slipcancle.xml_memdet, dw_main, null, FileSaveAsType.Xml);

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
                    //dw_list.ImportString(slipcancle.xml_sliplist, FileSaveAsType.Xml);

                    DwUtil.ImportData(slipcancle.xml_sliplist, dw_list, null, FileSaveAsType.Xml);
                    dw_list.SetSort("slip_date desc,payinslip_no desc");
                    dw_list.Sort();
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
                //slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                //slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = HfSlipNo.Value;
                //slipcancle.cancel_coopid = state.SsCoopId;
                slipcancle.slipcoop_id = state.SsCoopControl;
                int result = shrlonService.of_initccl_slippayindet(state.SsWsPass, ref slipcancle);
                try
                {
                    dw_head.Reset();
                    DwUtil.ImportData(slipcancle.xml_sliphead, dw_head, null, FileSaveAsType.Xml);
                    dw_head.SetItemDate(1, "cancel_date", state.SsWorkDate);
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
                    DwUtil.ImportData(slipcancle.xml_slipdetail, dw_detail, null, FileSaveAsType.Xml);
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
            dw_main.InsertRow(0);
            dw_list.InsertRow(0);
            dw_head.InsertRow(0);
            dw_head.SetItemDate(1, "slip_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "operate_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "cancel_date", state.SsWorkDate);
            tdwhead.Eng2ThaiAllRow();
            tdwdetail.Eng2ThaiAllRow();
            dw_detail.InsertRow(0);
        }
    }
}
