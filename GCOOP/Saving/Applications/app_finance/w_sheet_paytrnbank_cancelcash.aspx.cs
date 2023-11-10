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
    public partial class w_sheet_paytrnbank_cancelcash : PageWebSheet, WebSheet
    {
        protected String postDetail;
        protected String checkAll;
        private n_financeClient finService;
        private DwThDate tDwMain;
        
        private void JsPostDetail()
        {
            DateTime date = DwMain.GetItemDateTime(1,"start_date");
            try
            { 
                String xmlMain = "";
                xmlMain = finService.of_getlistreappr_moneyorder(state.SsWsPass, date);
                DwDetail.Reset();
                if (xmlMain != null && xmlMain != "")
                {
                    try
                    {
                        DwUtil.ImportData(xmlMain, DwDetail, null, FileSaveAsType.Xml);
                        DwDetail.Sort();
                        chkAll.Checked = false;
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลวันที่ : " + date.ToString("dd/MM/yyyy"));
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลวันที่ : " + date.ToString("dd/MM/yyyy"));
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsCheckAll()
        {
            Decimal Set = 1;
            Boolean Select = chkAll.Checked;

            if (Select == true)
            {
                Set = 0;
            }
            else if (Select == false)
            {
                Set = 1;
            }
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                DwDetail.SetItemDecimal(i, "post_flag", Set);
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDetail = WebUtil.JsPostBack(this, "postDetail");
            checkAll = WebUtil.JsPostBack(this, "checkAll");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("start_date", "start_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                finService = wcf.NFinance;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "start_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDetail")
            {
                JsPostDetail();
            }
            else if (eventArg == "checkAll")
            {
                JsCheckAll();
            }
        }

        public void SaveWebSheet()
        {
            String xmlDetail = "";
            try
            {
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                finService.of_cancelappr_moneyorder(state.SsWsPass, xmlDetail, state.SsUsername, state.SsWorkDate);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว...");
                JsPostDetail();
                chkAll.Checked = false;
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
