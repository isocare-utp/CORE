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
    public partial class w_sheet_dp_confirm_atm : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsTransactionCancel;
        private DwThDate tDwMain;

        private String pbl = "dp_confirm_atm.pbl";
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
                    break;
                case "jsTransactionCancel":
                    JsTransactionCancel();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        private void JsTransactionCancel()
        {
            try
            {
                int RowNumber = Convert.ToInt32(HdRowCancel.Value);
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
                    if (system_code == "01") // Loan
                    {
                        SqlSelect = @"SELECT * FROM ATMLOAN WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND LOANCONTRACT_NO = '" + account_no + @"'";
                        Sdt dtloan = WebUtil.QuerySdt(SqlSelect);
                        if (dtloan.GetRowCount() != 1)
                        {
                            throw new Exception("พบข้อมูลเงินกู้เลขที่สัญญา " + account_no + " ทั้งหมด " + dtloan.GetRowCount());
                        }
                        if (operate_code == "002") // receive
                        {
                            SqlUpdate = @"UPDATE ATMLOAN SET RECEIVE_AMT = RECEIVE_AMT - " + item_amt + @" WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND LOANCONTRACT_NO = '" + account_no + @"'";
                            WebUtil.Query(SqlUpdate);
                        }
                        else if (operate_code == "003") // payment
                        {
                            SqlUpdate = @"UPDATE ATMLOAN SET PAY_AMT = PAY_AMT - " + item_amt + @" WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND LOANCONTRACT_NO = '" + account_no + @"'";
                            WebUtil.Query(SqlUpdate);
                        }
                        else
                        {
                            throw new Exception("ไม่พบการทำรายการ เงินกู้ประเภท " + operate_code);
                        }

                    }
                    else if (system_code == "02") // Dept
                    {
                        SqlSelect = @"SELECT * FROM ATMDEPT WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND DEPTACCOUNT_NO = '" + account_no + @"'";
                        Sdt dtdept = WebUtil.QuerySdt(SqlSelect);
                        if (dtdept.GetRowCount() != 1)
                        {
                            throw new Exception("พบข้อมูลเงินกู้เลขที่สัญญา " + account_no + " ทั้งหมด " + dtdept.GetRowCount());
                        }

                        if (operate_code == "002") // withdraw
                        {
                            SqlUpdate = @"UPDATE ATMDEPT SET RECEIVE_AMT = RECEIVE_AMT - " + item_amt + @" WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND DEPTACCOUNT_NO = '" + account_no + @"'";
                            WebUtil.Query(SqlUpdate);
                        }
                        else if (operate_code == "003") // deposit
                        {
                            SqlUpdate = @"UPDATE ATMDEPT SET PAY_AMT = PAY_AMT - " + item_amt + @" WHERE COOP_ID = '" + coop_id + @"' AND MEMBER_NO = '" + member_no + @"' AND DEPTACCOUNT_NO = '" + account_no + @"'";
                            WebUtil.Query(SqlUpdate);
                        }
                        else
                        {
                            throw new Exception("ไม่พบการทำรายการ เงินฝากประเภท" + operate_code);
                        }
                    }
                    else
                    {
                        throw new Exception("ไม่พบประเภทรายการ " + system_code);
                    }
                    SqlUpdate = @"UPDATE ATMTRANSACTION SET ITEM_STATUS = -9, RECONCILE_DATE = sysdate WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND ACCOUNT_NO = '" + account_no + "' AND CCS_OPERATE_DATE = TO_DATE('" + ccs_operate_date.ToString("ddMMyyyy HHmmss") + "','ddmmyyyy hh24miss')";
                    WebUtil.Query(SqlUpdate);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกรายการสำเร็จ");
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