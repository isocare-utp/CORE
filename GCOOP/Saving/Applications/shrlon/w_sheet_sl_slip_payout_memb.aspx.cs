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
    public partial class w_sheet_sl_slip_payout_memb : PageWebSheet, WebSheet
    {
        protected String postInitDwMain;
        protected String postNewClear;

        private n_shrlonClient slServ;
        private DwThDate tDwMain;
        private String tempMoneyType;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInitDwMain = WebUtil.JsPostBack(this, "postInitDwMain");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("slip_date", "slip_tdate");
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            slServ = wcf.NShrlon;
            
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "moneytype_code", "CSH");
                DwMain.SetItemString(1, "sliptype_code", "LRT");
                DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
            tempMoneyType = DwUtil.GetString(DwMain, 1, "moneytype_code", "CSH");
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInitDwMain")
            {
                JsPostInitDwMain();
            }
            else if (eventArg == "postNewClear")
            {
                JsPostNewClear();
            }
        }

        public void SaveWebSheet()
        {
            bool isSelectOperateFlag = false;
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                if (DwDetail.GetItemDecimal(i, "operate_flag") == 1)
                {
                    isSelectOperateFlag = true;
                    break;
                }
            }
            if (isSelectOperateFlag)
            {
                try
                {
                    String memberNo = DwMain.GetItemString(1, "member_no");
                    str_slippayout str = new str_slippayout();
                    str.coop_id = state.SsCoopId;
                    str.entry_id = state.SsUsername;
                    str.member_no = memberNo;
                    str.operate_date = DwMain.GetItemDateTime(1, "operate_date");
                    str.slip_date = DwMain.GetItemDateTime(1, "slip_date");
                    str.xml_sliphead = DwMain.Describe("datawindow.data.xml");
                    str.xml_slipdet = DwDetail.Describe("datawindow.data.xml");
                    int ii = slServ.of_saveslip_moneyret(state.SsWsPass, str);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลการจ่ายเงินให้สมาชิกสำเร็จ");
                    JsPostNewClear();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรายการจ่ายคืนก่อนบันทึกข้อมูล");
            }
        }

        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slip_payout_memb.pbl", null);
            DwUtil.RetrieveDDDW(DwMain, "sliptype_code", "sl_slip_payout_memb.pbl", null);

            DataWindowChild dcSlipTypeCode = DwMain.GetChild("sliptype_code");
            dcSlipTypeCode.SetFilter("sliptypesign_flag = -1 and slipmanual_flag = 1");
            dcSlipTypeCode.Filter();

            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion

        private void JsPostInitDwMain()
        {
            try
            {
                String memberNo = DwMain.GetItemString(1, "member_no");

                str_slippayout str = new str_slippayout();
                str.coop_id = state.SsCoopId;
                str.entry_id = state.SsUsername;
                str.member_no = memberNo;
                str.operate_date = DwMain.GetItemDateTime(1, "operate_date");
                str.slip_date = DwMain.GetItemDateTime(1, "slip_date");
                //str.xml_sliphead = DwMain.Describe("datawindow.data.xml");
                Int32 result = slServ.of_initslipmoneyret(state.SsWsPass, str);
                DwUtil.ImportData(str.xml_sliphead, DwMain, tDwMain, Sybase.DataWindow.FileSaveAsType.Xml);
                DwUtil.ImportData(str.xml_slipdet, DwDetail, null, Sybase.DataWindow.FileSaveAsType.Xml);
                DwMain.SetItemString(1, "moneytype_code", tempMoneyType);
                for (int i = 1; i <= DwDetail.RowCount; i++)
                {
                    DwDetail.SetItemDecimal(i, "operate_flag", 1);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsPostNewClear()
        {
            HdIsPostBack.Value = "false";
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemString(1, "moneytype_code", "CSH");
            DwMain.SetItemString(1, "sliptype_code", "LRT");
            DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            DwDetail.Reset();
        }
    }
}