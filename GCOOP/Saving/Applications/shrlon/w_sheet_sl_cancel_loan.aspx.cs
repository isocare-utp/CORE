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
    public partial class w_sheet_sl_cancel_loan : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jscontract_no;
        protected String newClear;
        public void InitJsPostBack()
        {
            jscontract_no = WebUtil.JsPostBack(this, "jscontract_no");
            tDwMain = new DwThDate(dw_main, this);

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
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
                dw_main.SetItemString(1, "cancel_id", state.SsUsername);
                dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            try
            {
                if (!IsPostBack)
                {
                    HdIsPostBack.Value = "true";
                }
                else
                {
                    HdIsPostBack.Value = "false";
                }
            }
            catch { }
            DwUtil.RetrieveDDDW(dw_main, "contcclcause_code", "sl_slipreqall.pbl", null);
        }



        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jscontract_no")
            {

                Jscontract_no();

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

                String cancel_cause="";
                try
                {
                    cancel_cause = Hcause.Value;
                    dw_main.SetItemString(1, "cancel_cause", cancel_cause);
                }
                catch { dw_main.SetItemString(1, "cancel_cause", "ยกเลิกสัญญา"); }
                string as_coopid = state.SsCoopControl;
              
                String xmlcontccl = dw_main.Describe("DataWindow.Data.XML");
                String as_cancelid = state.SsUsername;
                DateTime adtm_cancel = state.SsWorkDate;
                int result = shrlonService.of_savereq_contcancel(state.SsWsPass, xmlcontccl, as_cancelid, adtm_cancel, as_coopid);

                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ  กรณีเป็นสัญญาบุคลลค้ำ อย่าลืมแก้ไขสถานะผ่อนผันของสัญญาหักกลบด้วย");
                }                
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            HdIsPostBack.Value = "true";
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
        }
        public void Jscontract_no()
        {
            try
            {
                string as_coopid = state.SsCoopControl;
                String as_contno = HContract.Value;
                String xmlcontno = shrlonService.of_initreq_contcancel(state.SsWsPass, as_coopid, as_contno);
                dw_main.Reset();
                //dw_main.ImportString(xmlcontno, FileSaveAsType.Xml);
                DwUtil.ImportData(xmlcontno, dw_main, tDwMain, FileSaveAsType.Xml);
                tDwMain.Eng2ThaiAllRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                ///"ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                dw_main.Reset(); dw_main.InsertRow(1); dw_main.SetItemString(1, "cancel_id", state.SsUsername);
                dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate); tDwMain.Eng2ThaiAllRow();
            }



        }
        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_main.InsertRow(1);
            dw_main.SetItemString(1, "cancel_id", state.SsUsername);
            dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }

    }
}
