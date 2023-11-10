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
using CoreSavingLibrary.WcfNAccount;
using Sybase.DataWindow;

using System.Data.OracleClient; //เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using System.Web.Services.Protocols; //เรียกใช้ service



namespace Saving.Applications.account
{
    public partial class w_acc_cashflow_formula : PageWebSheet,WebSheet
    {
        private n_accountClient accService;
        protected String postMain;
        protected String postDwDetail;
        protected String insertRowDetail;
        protected String deleteRowDetail;
        protected String postInsertAfterRow;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postMain = WebUtil.JsPostBack(this, "postMain");
            postDwDetail = WebUtil.JsPostBack(this, "postDwDetail");
            insertRowDetail = WebUtil.JsPostBack(this, "insertRowDetail");
            deleteRowDetail = WebUtil.JsPostBack(this, "deleteRowDetail");
            postInsertAfterRow = WebUtil.JsPostBack(this, "postInsertAfterRow");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwDetail.SetTransaction(sqlca);
            try
            {
                accService = wcf.NAccount;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Webservice ไม่ได้");
                return;
            }
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
            }
            else
            {
                String xmlDetail = "";
                this.RestoreContextDw(DwMain);
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                
                this.RestoreContextDw(DwDetail);
               
                xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDwDetail")
            {
                JsPostDwDetail();
            }
            else if (eventArg == "insertRowDetail")
            {
                JsInsertRowDetail();
            }
            else if (eventArg == "deleteRowDetail")
            {
                JsDeleteRowDetail();
            }
            else if (eventArg == "postInsertAfterRow")
            {
                JspostInsertAfterRow();
            }
        }

        public void SaveWebSheet()
        {
            short sheetType = short.Parse(DwMain.GetItemDecimal(1, "sheet_type").ToString());
            String accActivity = "";
            String xmlDetail = "";
            xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
            bool checkUpdate = true;
            if (sheetType == 1)
            {
                accActivity = DwMain.GetItemString(1, "type_drcr");
            }
            else
            {
                accActivity = DwMain.GetItemString(1, "account_activity");
            }
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                Decimal seqNo = 0;
                String accName = "";
                try
                {
                    seqNo = DwDetail.GetItemDecimal(i, "seq_no");
                }
                catch
                {
                    seqNo = 0;
                }
                try
                {
                    accName = DwDetail.GetItemString(i, "account_name");
                }
                catch
                {
                    accName = "";
                }
                if (seqNo == 0 || seqNo == null || accName == "" || accName == null)
                {
                    checkUpdate = false;
                    break;
                }
            }
            try
            {
                if (checkUpdate == true)
                {
                    xmlDetail = DwDetail.Describe("DataWindow.Data.XML");
                    accService.of_update_cashpaper_detail(state.SsWsPass, xmlDetail, sheetType, accActivity);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว...");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้เรียบร้อย");
                }
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

        private void JsPostDwDetail()
        {
            short sheetType = short.Parse(DwMain.GetItemDecimal(1, "sheet_type").ToString());
            String accActivity = "";
            String xmlDetail = "";
            String textError = "";
            if (sheetType == 1)
            {
                accActivity = DwMain.GetItemString(1, "type_drcr");
                textError = "เดบิต,เครดิต";
            }
            else
            {
                accActivity = DwMain.GetItemString(1, "account_activity");
                textError = "แยกกิจกรรม";
            }
            try
            {
                xmlDetail = accService.of_get_cashpaper_detail(state.SsWsPass, sheetType, accActivity, state.SsCoopId);
                if (xmlDetail != "")
                {
                    DwUtil.ImportData(xmlDetail, DwDetail, null, FileSaveAsType.Xml);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลประเภททำการ : " + textError);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsInsertRowDetail()
        {
            Decimal sheetType = DwMain.GetItemDecimal(1, "sheet_type");
            String accActivity = "";
            Decimal seqNo = 0;
            if (sheetType == 1)
            {
                accActivity = DwMain.GetItemString(1, "type_drcr");
            }
            else
            {
                accActivity = DwMain.GetItemString(1, "account_activity");
            }
            DwDetail.InsertRow(0);
            int row = DwDetail.RowCount;
            try
            {
                 seqNo = DwDetail.GetItemDecimal(row - 1, "seq_no");
            }
            catch
            {
                 seqNo = 0;
            }
            DwDetail.SetItemDecimal(row, "seq_no", seqNo + 1);
            DwDetail.SetItemDecimal(row,"sheet_type",sheetType);
            DwDetail.SetItemString(row,"account_activity",accActivity);
            DwDetail.SetItemString(row, "coop_id", state.SsCoopId);
        }

        


        private void JsDeleteRowDetail()
        {
            int row = int.Parse(HdDetailRow.Value);
            DwDetail.DeleteRow(row);
        }

        protected void Btn_Generate_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                DwDetail.SetItemDecimal(i, "seq_no", i * 5);
                
            }
            DwDetail.SaveDataCache();
        }

        private void JspostInsertAfterRow()
        {
            decimal sortOrder_before = 0;
            //int RowCurrent = DwDetail.CurrentRow;
            int RowCurrent = int.Parse(HdDetailRow.Value);
            decimal sortOrder = DwDetail.GetItemDecimal(RowCurrent, "seq_no");
            if (RowCurrent == 1)
            {
                sortOrder_before = 2;
            }
            else
            {
                 sortOrder_before = DwDetail.GetItemDecimal(RowCurrent - 1, "seq_no");
            }
            //decimal sortOrder_before = DwDetail.GetItemDecimal(RowCurrent -1 , "seq_no");
            decimal sortOrder_sum = sortOrder - sortOrder_before;
            if (sortOrder_sum <= 1)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("กรุณาจัดเลขลำดับใหม่");
            }
            else
            {

                sortOrder_before = sortOrder_before+1;   //141


                Decimal sheetType = DwMain.GetItemDecimal(1, "sheet_type");
                String accActivity = "";
                Decimal seqNo = 0;
                if (sheetType == 1)
                {
                    accActivity = DwMain.GetItemString(1, "type_drcr");
                }
                else
                {
                    accActivity = DwMain.GetItemString(1, "account_activity");
                }
                DwDetail.InsertRow(0);
                int row = DwDetail.RowCount;  //30 
                try
                {
                    seqNo = DwDetail.GetItemDecimal(RowCurrent - 1, "seq_no"); //
                }
                catch
                {
                    seqNo = 0;
                }
                DwDetail.SetItemDecimal(row, "seq_no", seqNo + 1);
                DwDetail.SetItemDecimal(row, "sheet_type", sheetType);
                DwDetail.SetItemString(row, "account_activity", accActivity);
                DwDetail.SetItemString(row, "coop_id", state.SsCoopId);

                
            }
        }

    }
}
