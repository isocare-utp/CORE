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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;

using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_ccl_mbresign : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        //private ShrlonClient shrlonService;
        //private CommonClient commonService;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsMemberNo;
        protected String newClear;
        public void InitJsPostBack()
        {
            jsMemberNo = WebUtil.JsPostBack(this, "jsMemberNo");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("cancelresign_date", "cancelresign_tdate");         
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //shrlonService = wcf.NShrlon;
                //commonService = wcf.NCommon;
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
               this.RestoreContextDw(dw_main);             
            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "cancelresign_id", state.SsUsername);               
                dw_main.SetItemDate(1, "cancelresign_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            
               DwUtil.RetrieveDDDW(dw_main, "resigncause_code", "sl_cancelresign.pbl", state.SsCoopControl);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsMemberNo")
            {
                JsMemberNo();
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
                String xmlcancel = dw_main.Describe("DataWindow.Data.XML");
                String as_cancelid = state.SsUsername;
                DateTime adtm_cancel = dw_main.GetItemDate(1, "cancelresign_date");
                str_mbreqresign astr_mbreqresign = new str_mbreqresign();
                astr_mbreqresign.xml_request = xmlcancel;
                //int result = shrlonService.SaveCancelResign(state.SsWsPass, astr_mbreqresign);
                int result = shrlonService.of_saveccl_resign(state.SsWsPass, astr_mbreqresign);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            try 
            {
                DwUtil.RetrieveDDDW(dw_main, "resigncause_code", "sl_cancelresign.pbl", state.SsCoopControl);
                dw_main.SetItemDate(1, "cancelresign_date", state.SsWorkDate);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
          
            dw_main.SaveDataCache();
        }

        public void JsMemberNo()
        {
            try
            {
                String as_member_no = WebUtil.MemberNoFormat(HMember_no.Value);
                str_mbreqresign astr_mbreqresign = new str_mbreqresign();
                astr_mbreqresign.member_no = as_member_no;

                //int result = shrlonService.InitCancleResign(state.SsWsPass, ref astr_mbreqresign);
                int result = shrlonService.of_initccl_resign(state.SsWsPass, ref astr_mbreqresign); //Name
                //int result = shrlonService.of_initccl_resign(state.SsWsPass,ref astr_mbreqresign);
                if (result == 1)
                {
                   // dw_main.Reset();
                    DwUtil.RetrieveDataWindow(dw_main, "sl_cancelresign.pbl", null, state.SsCoopControl, as_member_no);
                    //  dw_main.ImportString(astr_mbreqresign.xml_request, FileSaveAsType.Xml);
                    dw_main.SetItemDate(1, "cancelresign_date", state.SsWorkDate);
                    //DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    dw_main.SetItemString(1, "cancelresign_id", state.SsUsername);
                    tDwMain.Eng2ThaiAllRow();
                    if (dw_main.RowCount > 1)
                    {
                        DwUtil.DeleteLastRow(dw_main);
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                dw_main.Reset(); dw_main.InsertRow(0);
            }

            DwUtil.RetrieveDDDW(dw_main, "resigncause_code", "sl_cancelresign.pbl", state.SsCoopId);
        }

        private void JsNewClear()
        {

            dw_main.Reset();
            dw_main.InsertRow(1);
            dw_main.SetItemString(1, "cancelresign_id", state.SsUsername);
            dw_main.SetItemDate(1, "cancelresign_date", state.SsWorkDate);
            dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            dw_main.SetItemString(1, "cancelresign_id", state.SsUsername);
            tDwMain.Eng2ThaiAllRow();
            DwUtil.RetrieveDDDW(dw_main, "resigncause_code", "sl_cancelresign.pbl", state.SsCoopId);

        }
    }
}
