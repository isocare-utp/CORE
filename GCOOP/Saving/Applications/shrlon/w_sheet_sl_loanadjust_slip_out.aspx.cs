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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loanadjust_slip_out : PageWebSheet, WebSheet
    {
        protected String postPost;
        protected String postMemberNo;
        protected String setAMT;
        private n_shrlonClient slService;
        private DwThDate tDwMain;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postPost = WebUtil.JsPostBack(this, "postPost");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            setAMT = WebUtil.JsPostBack(this, "setAMT");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            slService = wcf.NShrlon;
            HdIsPostBack.Value = IsPostBack ? "true" : "false";
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "adjslip_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                if (DwDetail.GetItemDecimal(i, "operate_flag") <= 0)
                {
                    //principal_adjamt, interest_adjamt, item_adjamt
                    DwDetail.SetItemDecimal(i, "item_adjamt", 0);
                    DwDetail.SetItemDecimal(i, "interest_adjamt", 0);
                    DwDetail.SetItemDecimal(i, "principal_adjamt", 0);
                    DwDetail.SetItemDecimal(i, "operate_flag", 0);
                }
            }
            DwUtil.RetrieveDDDW(DwMain, "adjtype_code", "sl_loanadjust_slip_out.pbl", null);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
            else if (eventArg == "setAMT") {
                SetAMT();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_slipadjust s = new str_slipadjust();
                s.member_no = DwMain.GetItemString(1, "member_no");
                //DateTime adjDate = DwUtil.GetDateTime(DwMain, 1, "adjslip_date");
                s.adjslip_date = DwMain.GetItemDateTime(1, "adjslip_date");
                s.adjtype_code = DwMain.GetItemString(1, "adjtype_code");
                s.ref_slipno = DwMain.GetItemString(1, "ref_slipno");
                s.coop_id = state.SsCoopId;
                s.entry_id = state.SsUsername;
                //s.operate_date = state.SsWorkDate;
                s.xml_slipdet = DwDetail.Describe("datawindow.data.xml");
                s.xml_sliphead = DwMain.Describe("datawindow.data.xml");
                slService.of_saveslip_adjust(state.SsWsPass, ref s);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();

            //DwUtil.RetrieveDDDW(dw_main, "paytoorder_desc_1", "sl_loan_requestment.pbl", null);
            DwUtil.RetrieveDDDW(DwMain, "slipretcause_code", "sl_loanadjust_slip_out.pbl", null);

            
        }

        #endregion

        private void JsPostMemberNo()
        {
            try
            {
                String adjType = DwUtil.GetString(DwMain, 1, "adjtype_code", "").Trim();
                String refSlipno = DwUtil.GetString(DwMain, 1, "ref_slipno", "").Trim();
                if (string.IsNullOrEmpty(refSlipno))
                {
                    JustMember();
                }
                //else if (adjType == "MTH")
                else
                {
                    JustMthWithRefId();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JustMember()
        {
            str_slipadjust s = new str_slipadjust();
            s.member_no = DwMain.GetItemString(1, "member_no");
            String adjType = DwMain.GetItemString(1, "adjtype_code");
            DateTime adjDate = DwUtil.GetDateTime(DwMain, 1, "adjslip_date");
            s.coop_id = state.SsCoopControl;
            s.entry_id = state.SsUsername;
            s.operate_date = state.SsWorkDate;
            int ii = slService.of_initslipadjust(state.SsWsPass, ref s);
            DwMain.Reset();
            DwDetail.Reset();
            DwUtil.ImportData(s.xml_sliphead, DwMain, tDwMain, Sybase.DataWindow.FileSaveAsType.Xml);
            DwDetail.Reset();
            DwMain.SetItemString(1, "adjtype_code", adjType);
            if (adjDate.Year > 1370)
            {
                DwMain.SetItemDateTime(1, "adjslip_date", adjDate);
            }
        }

        private void JustMthWithRefId()
        {
            str_slipadjust s = new str_slipadjust();
            s.member_no = DwMain.GetItemString(1, "member_no");
            DateTime adjDate = DwUtil.GetDateTime(DwMain, 1, "adjslip_date");
            s.adjslip_date = DwMain.GetItemDateTime(1, "adjslip_date");
            s.adjtype_code = DwMain.GetItemString(1, "adjtype_code");
            s.ref_slipno = DwMain.GetItemString(1, "ref_slipno");
            s.coop_id = state.SsCoopId;
            s.entry_id = state.SsUsername;
            s.operate_date = state.SsWorkDate;
            int ii = slService.of_initslipadjust(state.SsWsPass, ref s);
            DwUtil.ImportData(s.xml_sliphead, DwMain, tDwMain, Sybase.DataWindow.FileSaveAsType.Xml);
            DwUtil.ImportData(s.xml_slipdet, DwDetail, null, Sybase.DataWindow.FileSaveAsType.Xml);
            //dw_main.Modify("compute_11.visible =1");
           
        }
        private void SetAMT() 
        { 
            Int32  rowNumber = Convert .ToInt32 (HdRow .Value );
            String adjType = DwMain.GetItemString(1, "adjtype_code");
            String opreateFlag = DwDetail.GetItemString(rowNumber, "operate_flag");

            if ((adjType == "CMN") || (adjType == "CPL")) {
                if (opreateFlag == "1")
                {
                    DwDetail.Modify("principal_adjamt.Edit.DisplayOnly=No");
                    DwDetail.Modify("interest_adjamt.Edit.DisplayOnly=No");
                }
                else {
                    DwDetail.Modify("principal_adjamt.Edit.DisplayOnly=Yes");
                    DwDetail.Modify("interest_adjamt.Edit.DisplayOnly=Yes");
                    DwDetail.SetItemDecimal(rowNumber, "principal_adjamt", 0);
                    DwDetail.SetItemDecimal(rowNumber, "interest_adjamt", 0); 
                }
                
            } 
        }
    }
}