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
    public partial class w_sheet_sl_loanrcv_cancel_day : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostLnrcvList;
        protected String newClear;

        //register event สำหรับการใช้งานในหน้าจ
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostLnrcvList = WebUtil.JsPostBack(this, "jsPostLnrcvList");
            tdwhead = new DwThDate(dw_head, this);
            tdwhead.Add("slip_date", "slip_tdate");
            tdwhead.Add("operate_date ", "operate_tdate ");
            newClear = WebUtil.JsPostBack(this, "newClear");
        }

        //method แรกเมื่อ sheet ดังกล่าวถูกเปิดขึ้น
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
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_head.InsertRow(0);
                dw_detail.InsertRow(0);

                DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
            }

        }

        //เป็นฟังก์ชันไว้สำหรับตรวจสอบ event ที่มีการ register ไว้ กรณีมีการเรียกใช้งาน event นั้นๆ
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsPostLnrcvList")
            {
                JsPostLnrcvList();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
        }

        //เป็น method สำหรับการบันทึกข้อมูลของ sheet นั้นๆ 
        public void SaveWebSheet()
        {
            try
            {

                //String dwhead_XML = dw_head.Describe("DataWindow.Data.XML");
                //String dwdetail_XML = dw_detail.Describe("DataWindow.Data.XML");
                //String slip_no = dw_head.GetItemString(1, "payoutslip_no");
                //String cancel_id = dw_head.GetItemString(1, "entry_id");
                //tdwhead.Eng2ThaiAllRow();
                //DateTime cancel_date = state.SsWorkDate;
                str_slipcancel slipcancle = new str_slipcancel();

                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = dw_head.GetItemString(1, "payoutslip_no");
                slipcancle.cancel_id = state.SsUsername;
                slipcancle.cancel_date = state.SsWorkDate;
                slipcancle.slipcoop_id = state.SsCoopId;
                slipcancle.memcoop_id = state.SsCoopId;
                slipcancle.cancel_coopid = state.SsCoopId;


                int result = shrlonService.of_saveccl_lnrcv(state.SsWsPass,ref slipcancle);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย");
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //เป็น method สุดท้ายของ web sheet นี้
        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            if (dw_main.RowCount > 1)
            {
                dw_main.DeleteRow(dw_main.RowCount);
            }
            dw_detail.SaveDataCache();
            dw_head.SaveDataCache();
        }

        //
        private void JsPostMember()
        {
            try
            {
                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.xml_memdet = dw_main.Describe("DataWindow.Data.XML");
                slipcancle.xml_sliplist = dw_list.Describe("DataWindow.Data.XML");
                slipcancle.cancel_date = state.SsWorkDate;
                slipcancle.member_no = Hfmember_no.Value;
                slipcancle.slipcoop_id = state.SsCoopId;
                slipcancle.memcoop_id = state.SsCoopId;
                slipcancle.cancel_coopid = state.SsCoopId;
                int result = shrlonService.of_initccl_sliplnrcvday(state.SsWsPass, ref slipcancle);
                if (result == 1)
                {

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
                        if (slipcancle.xml_memdet == "")
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                        }
                        else { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                        dw_main.Reset(); dw_main.InsertRow(0);

                    }
                    try
                    {

                        dw_list.Reset();
                        dw_list.ImportString(slipcancle.xml_sliplist, FileSaveAsType.Xml);

                    }
                    catch (Exception ex)
                    {
                        if (slipcancle.xml_sliplist == "")
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลของ ท." + slipcancle.member_no);
                        }
                        else { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        dw_list.Reset(); dw_list.InsertRow(0);

                    }

                }



            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }

        }

        //
        private void JsPostLnrcvList()
        {
            try
            {


                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = HfSlipNo.Value;
                slipcancle.slipcoop_id = state.SsCoopId;
                slipcancle.memcoop_id = state.SsCoopId;
                slipcancle.cancel_coopid = state.SsCoopId;
                int result = shrlonService.of_initccl_sliplnrcvdet(state.SsWsPass, ref slipcancle);
                try
                {

                    dw_head.Reset();
                    dw_head.ImportString(slipcancle.xml_sliphead, FileSaveAsType.Xml);
                    if (dw_head.RowCount > 1)
                    {
                        dw_head.DeleteRow(dw_head.RowCount);
                    }
                }
                catch (Exception ex)
                {
                    if (slipcancle.xml_sliphead == "")
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรายการ Slip ของสมาชิก");
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage(ex + "dw_head"); }

                    dw_head.Reset(); dw_head.InsertRow(0);
                }
                try
                {
                    dw_detail.Reset();
                    dw_detail.ImportString(slipcancle.xml_slipdetail, FileSaveAsType.Xml);


                }
                catch (Exception ex)
                {
                    if (slipcancle.xml_slipdetail == "")
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรายการ Slip ของสมาชิก");
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage(ex + "dw_detail"); }

                    dw_detail.Reset(); dw_detail.InsertRow(0);
                }
                DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

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
            dw_detail.InsertRow(0);

            DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
        }

    }
}
