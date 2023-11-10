using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;
using System.IO;
using System.Text;
using DataLibrary;
//using Saving.WcfFinance;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_pass_atm : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsTransactionCancel;
        private DwThDate tDwMain;

        private String pbl = "dp_pass_atm.pbl";
        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsTransactionCancel = WebUtil.JsPostBack(this, "jsTransactionCancel");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {

            if (!IsPostBack)
            {
                DwMain.Reset();
                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "operate_date", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                tDwMain.Eng2ThaiAllRow();
                DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
                DwUtil.RetrieveDataWindow(DwDetail, pbl, null, operate_date);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, operate_date);
                    HdOperate_Date.Value = operate_date.ToString();
                    break;
                case "jsTransactionCancel":
                    JsTransactionPass();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                ta.Transection();
                ta.Close();
                DwDetail.SetFilter("item_status = 0 and join_flag = 1");
                DwDetail.Filter();
                for (int i = 1; i <= DwDetail.RowCount; i++)
                {
                    String coop_id = String.Empty,
                           member_no = String.Empty,
                           account_no = String.Empty,
                    SqlUpdate = String.Empty;
                    Decimal item_status = 0,
                            join_flag = 0;
                    DateTime ccs_operate_date = DateTime.MinValue;

                    item_status = DwDetail.GetItemDecimal(i, "item_status");
                    join_flag = DwDetail.GetItemDecimal(i, "join_flag");

                    if (item_status == 0 && join_flag == 1)
                    {
                        coop_id = DwDetail.GetItemString(i, "coop_id");
                        member_no = DwDetail.GetItemString(i, "member_no");
                        account_no = DwDetail.GetItemString(i, "account_no");
                        ccs_operate_date = DwDetail.GetItemDateTime(i, "ccs_operate_date");

                        SqlUpdate = @"UPDATE ATMTRANSACTION SET ITEM_STATUS = 1, RECONCILE_DATE = sysdate WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND CCS_OPERATE_DATE = TO_DATE('" + ccs_operate_date.ToString("ddMMyyyy HHmmss") + "','ddmmyyyy hh24miss')";
                        ta.Query(SqlUpdate);
                    }
                }
                ta.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            try
            {
                DwDetail.SetFilter("");
                DwDetail.Filter();
                DwUtil.RetrieveDataWindow(DwDetail, pbl, null, Convert.ToDateTime(HdOperate_Date.Value));
                
            }
            catch
            { }
        }

        public void WebSheetLoadEnd()
        {
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        private void JsTransactionPass()
        {
            try
            {
                int RowNumber = Convert.ToInt32(HdRowPass.Value);
                String coop_id = DwDetail.GetItemString(RowNumber, "coop_id");
                String member_no = DwDetail.GetItemString(RowNumber, "member_no");
                String account_no = DwDetail.GetItemString(RowNumber, "account_no");
                DateTime ccs_operate_date = DwDetail.GetItemDateTime(RowNumber, "ccs_operate_date");
                Decimal item_amt = DwDetail.GetItemDecimal(RowNumber, "item_amt");
                String system_code = DwDetail.GetItemString(RowNumber, "system_code");
                String operate_code = DwDetail.GetItemString(RowNumber, "operate_code");


                String SqlSelect = @"SELECT * FROM ATMTRANSACTION WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND ACCOUNT_NO = '" + account_no + "' AND CCS_OPERATE_DATE = TO_DATE('" + ccs_operate_date.ToString("ddMMyyyy HHmmss") + "','ddmmyyyy hh24miss')";
                Sdt dt = WebUtil.QuerySdt(SqlSelect);
                if (dt.Next())
                {
                    if (dt.GetRowCount() != 1) throw new Exception("โปรแกรมพบข้อมูลมากกว่า 1 ข้อมูล กรุณาตรวจสอบข้อมูลอีกครั้ง");
                    String SqlUpdate = String.Empty;

                    SqlUpdate = @"UPDATE ATMTRANSACTION SET ITEM_STATUS = 1, RECONCILE_DATE = sysdate WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND CCS_OPERATE_DATE = TO_DATE('" + ccs_operate_date.ToString("ddMMyyyy HHmmss") + "','ddmmyyyy hh24miss')";
                    WebUtil.Query(SqlUpdate);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ผ่านรายการสำเร็จ");
                    DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, operate_date);
                }
                else
                {
                    throw new Exception("ไม่พบข้อมูล โปรดตรวจสอบ");

                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}