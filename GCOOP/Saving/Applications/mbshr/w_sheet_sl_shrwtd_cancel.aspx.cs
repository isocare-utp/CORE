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


namespace Saving.Applications.mbshr
{
    public partial class w_sheet_sl_shrwtd_cancel : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostCancelSwd;
        protected String jsPostCancelSwdDet;
        protected String newClear;
        public void InitJsPostBack()
        {
            jsPostCancelSwd = WebUtil.JsPostBack(this, "jsPostCancelSwd");
            jsPostCancelSwdDet = WebUtil.JsPostBack(this, "jsPostCancelSwdDet");
            tdwhead = new DwThDate(dw_head, this);
            newClear = WebUtil.JsPostBack(this, "newClear");
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
                    HdIsPostBack.Value = "true";
                }
                catch { }

            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_head.InsertRow(1);
                dw_detail.InsertRow(0);
                HdIsPostBack.Value = "false";
                //dw_head.SetItemDate(1, "slip_date", state.SsWorkDate);
                //dw_head.SetItemDate(1, "operate_date ", state.SsWorkDate);
                //tdwhead.Eng2ThaiAllRow();
                DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_sharewtd_cancel.pbl", null);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostCancelSwd")
            {
                JsPostCancelSwd();
            }
            else if (eventArg == "jsPostCancelSwdDet")
            {
                JsPostCancelSwdDet();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
        }

        private void JsPostCancelSwd()
        {
            try
            {
                String member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
                DwUtil.RetrieveDataWindow(dw_main, "sl_slipall.pbl", null, state.SsCoopControl, member_no);

                if (dw_main.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิก กรุณาตรวจสอบใหม่");
                }
                else
                {
                    try
                    {
                        DwUtil.RetrieveDataWindow(dw_list, "sl_slipall.pbl", null, state.SsCoopControl, member_no);

                        if (dw_list.RowCount < 1)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //private void JsPostCancelSwd()
        //{
        //    try
        //    {
        //        str_slipcancel slipcancle = new str_slipcancel();
        //        slipcancle.member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
        //        slipcancle.xml_memdet = dw_main.Describe("DataWindow.Data.XML");
        //        slipcancle.xml_sliplist = dw_list.Describe("DataWindow.Data.XML");
        //        slipcancle.memcoop_id = state.SsCoopId;

        //        int result = shrlonService.InitCancelSwd(state.SsWsPass, ref slipcancle);
        //        if (result == 1)
        //        {
        //            try
        //            {
        //                dw_main.Reset();
        //                dw_main.ImportString(slipcancle.xml_memdet, FileSaveAsType.Xml);

        //            }
        //            catch (Exception ex)
        //            {
        //                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
        //                dw_main.Reset(); dw_main.InsertRow(0);

        //            }
        //            try
        //            {
        //                dw_list.Reset();
        //                dw_list.ImportString(slipcancle.xml_sliplist, FileSaveAsType.Xml);

        //            }
        //            catch (Exception ex)
        //            {
        //                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขที่จ่าย ");

        //                dw_list.Reset(); dw_list.InsertRow(0);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);

        //    }
        //    if (dw_main.RowCount > 1)
        //    {
        //        dw_main.DeleteRow(dw_main.RowCount);
        //    }
        //}

        private void JsPostCancelSwdDet()
        {
            try
            {
                String slip_no = HfSlipNo.Value;
                DwUtil.RetrieveDataWindow(dw_head, "sl_slipall.pbl", null, state.SsCoopControl, slip_no);

                if (dw_main.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                }

                    str_slipcancel slip = new str_slipcancel();
                    slip.slipcoop_id = state.SsCoopControl;
                    slip.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                    slip.slip_no = HfSlipNo.Value;
               
                    int result = shrlonService.of_initccl_slipshrwtddet(state.SsWsPass, ref slip); 
                    try
                    {
                        dw_detail.Reset();
                        DwUtil.ImportData(slip.xml_slipdetail, dw_detail, null, FileSaveAsType.Xml);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //private void JsPostCancelSwdDet()
        //{
        //    try
        //    {
        //        str_slipcancel slip = new str_slipcancel();
        //        slip.slipcoop_id = state.SsCoopId;
        //        slip.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
        //        slip.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
        //        slip.slip_no = HfSlipNo.Value;

        //        int result = shrlonService.InitCancelSwdDet(state.SsWsPass, ref slip);
        //        try
        //        {


        //            dw_head.Reset();
        //            dw_head.ImportString(slip.xml_sliphead, FileSaveAsType.Xml);
        //            if (dw_head.RowCount > 1)
        //            {
        //                dw_head.DeleteRow(dw_head.RowCount);
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
        //            dw_head.Reset(); dw_head.InsertRow(0);
        //        }
        //        try
        //        {
        //            dw_detail.Reset();
        //            dw_detail.ImportString(slip.xml_slipdetail, FileSaveAsType.Xml);                    

        //        }
        //        catch (Exception ex)
        //        {
        //            if (slip.xml_slipdetail == "")
        //            {
        //                // LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรายการหัก");
        //            }
        //            else { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        //            //  LtServerMessage.Text = WebUtil.ErrorMessage(ex);
        //          //  DwUtil.ImportData(slip.xml_slipdetail, dw_detail, null);
        //           // dw_detail.Reset(); dw_detail.InsertRow(0);
        //        }
        //        DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_slipall.pbl", null);

        //    }
        //    catch (Exception ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
        //    }
        //}

        public void SaveWebSheet()
        {
            try
            {
                str_slipcancel slipcancle = new str_slipcancel();
                slipcancle.coop_id = state.SsCoopControl;
                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = dw_head.GetItemString(1, "payoutslip_no");
                slipcancle.cancel_id = state.SsUsername;
                slipcancle.cancel_date = state.SsWorkDate;
                slipcancle.slipcoop_id = state.SsCoopControl;

                int result = shrlonService.of_saveccl_shrwtd(state.SsWsPass,ref slipcancle);
                if (result == 1) { LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย"); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            JsNewClear();
            dw_head.Reset();
            dw_detail.Reset();
            dw_head.InsertRow(0);
            dw_detail.InsertRow(0);
        }

        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_slipall.pbl", null);
            if (dw_main.RowCount > 1)
            {
                dw_main.DeleteRow(dw_main.RowCount);
            }
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            dw_head.SaveDataCache();
            dw_detail.SaveDataCache();
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
        }
    }
}
