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
using CoreSavingLibrary.WcfNFinance;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_paytrnbank : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;
        protected String postMemberNo;
        protected String postRefresh;
        private DwThDate tDwMain;
        private n_financeClient finService;


        private void JsPostRefresh()
        {

        }

        private void JsPostDeleteRow()
        {
            int row = int.Parse(HdDetailRow.Value);
            DwDetail.DeleteRow(row);
        }

        private void JsPostInsertRow()
        {
            DwDetail.InsertRow(0);
            WebUtil.RetrieveDDDW(DwDetail, "paytrnitemtype_code", "paytrnbank.pbl", null);
        }

        private void JsPostMemberNo()
        {
            String membgroup_code, membgroup_desc;
            String memberNo = DwMain.GetItemString(1, "member_no");
            try
            {
                //service
                String xmlMain = "";
                xmlMain = DwMain.Describe("DataWindow.Data.XML");
                xmlMain = finService.of_init_moneyorder(state.SsWsPass, xmlMain, state.SsUsername, state.SsWorkDate);
                DwMain.Reset();
                DwDetail.Reset();
                if (xmlMain != null && xmlMain != "")
                {
                    DwUtil.ImportData(xmlMain, DwMain, null, FileSaveAsType.Xml);
                    tDwMain.Eng2ThaiAllRow();
                    if (DwMain.RowCount > 1)
                    {
                        DwMain.DeleteRow(2);
                    }
                    if (DwMain.GetItemDecimal(1, "member_flag") == 1)
                    {
                        try
                        {
                            membgroup_code = DwMain.GetItemString(1, "membgroup_code");
                            DwUtil.RetrieveDDDW(DwMain, "membgroup_desc", "paytrnbank.pbl", null);
                            DataWindowChild DcMembDesc = DwMain.GetChild("membgroup_desc");
                            DcMembDesc.SetFilter("membgroup_code='" + membgroup_code + "'");
                            DcMembDesc.Filter();
                            membgroup_desc = DcMembDesc.GetItemString(1, "membgroup_desc");
                            DwMain.SetItemString(1, "membgroup_desc", membgroup_desc);
                        }
                        catch { }
                    }
                    else
                    {

                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีเลขที่สมาชิก : " + memberNo);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("trn_date", "trn_tdate");
            tDwMain.Add("doc_date", "doc_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                finService = wcf.NFinance;
            }
            catch (Exception)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                try
                {
                    DwUtil.RetrieveDDDW(DwMain, "paytrntype_code", "paytrnbank.pbl", null);
                    DwUtil.RetrieveDDDW(DwMain, "payment_branch_name", "paytrnbank.pbl", null);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }

            DwMain.SetItemDateTime(1, "trn_date", state.SsWorkDate);
            DwMain.SetItemDateTime(1, "doc_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertRow")
            {
                JsPostInsertRow();
            }
            else if (eventArg == "postDeleteRow")
            {
                JsPostDeleteRow();
            }
            else if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
            else if (eventArg == "postRefresh")
            {
                JsPostRefresh();
            }
        }

        public void SaveWebSheet()
        {
            String xmlMain = "";
            String xmlDetail = "";
            try
            {
                xmlMain = DwMain.Describe("DataWindow.Data.XML");
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                finService.of_save_moneyorder(state.SsWsPass, xmlMain, xmlDetail, state.SsUsername, state.SsWorkDate);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว...");

                DwMain.Reset();
                DwDetail.Reset();

                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "trn_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "doc_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
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
        }

        #endregion
    }
}
