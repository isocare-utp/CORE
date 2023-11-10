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

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_paytrnbank_apv_list : PageWebSheet,WebSheet
    {
        protected String postDetail;
        private n_financeClient finService;
        private DwThDate tDwMain;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDetail = WebUtil.JsPostBack(this, "postDetail");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("start_date", "start_tdate");
            tDwMain.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
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
                DwMain.SetItemDateTime(1, "start_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "end_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDetail")
            {
                DateTime startDate = DwMain.GetItemDateTime(1, "start_date");
                DateTime endDate = DwMain.GetItemDateTime(1, "end_date");
                DwDetail.Retrieve(startDate, endDate);
            }
        }

        public void SaveWebSheet()
        {
            String xmlDetail = "";
            try
            {
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว...");
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
