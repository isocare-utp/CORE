﻿using System;
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
    public partial class w_sheet_dp_edit_atm : PageWebSheet, WebSheet
    {
        protected String jsRetiveDwDetail;
        protected String jsRetive;
        private String pbl = "dp_atm_edit.pbl";

        public void InitJsPostBack()
        {
            jsRetive = WebUtil.JsPostBack(this, "jsRetive");
            jsRetiveDwDetail = WebUtil.JsPostBack(this, "jsRetiveDwDetail");
        }

        public void WebSheetLoadBegin()
        {

            if (!IsPostBack)
            {
                DwMain.Reset();
                DwMain.InsertRow(0);
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
                case "jsRetive":
                    jsReTive();
                    break;
                case "jsRetiveDwDetail":
                    jSRetiveDwDetail();
                    break;
               
            }
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                ta.Transection();

                String coop_id = DwDetail.GetItemString(1, "COOP_ID");
                String member_no = DwDetail.GetItemString(1, "MEMBER_NO");
                String account_no = DwDetail.GetItemString(1, "DEPTACCOUNT_NO");
                Decimal receive_amt = DwDetail.GetItemDecimal(1, "receive_amt");
                Decimal pay_amt = DwDetail.GetItemDecimal(1, "pay_amt");
                Decimal sequest_amt = DwDetail.GetItemDecimal(1, "sequest_amt");
                Decimal account_hold = DwDetail.GetItemDecimal(1, "account_hold");

                String SqlUpdate = @"UPDATE ATMDEPT SET RECEIVE_AMT = '" + receive_amt + "', PAY_AMT = '" + pay_amt + "', SEQUEST_AMT = '" + sequest_amt + "', ACCOUNT_HOLD = '" + account_hold + "' WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND DEPTACCOUNT_NO = '" + account_no + "' ";
                ta.Query(SqlUpdate);
              
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
            
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        private void jsReTive()
        {
            string member_no = DwMain.GetItemString(1, "atmmember_member_no");
            try
            {
                DwUtil.RetrieveDataWindow(DwMain, pbl, null, member_no);
                string coop_id = DwMain.GetItemString(1, "atmmember_coop_id");
                DwUtil.RetrieveDDDW(DwMain, "deptaccount_no", pbl, coop_id, member_no);

                String Selecdeptacc = "select deptaccount_no from ATMDEPT WHERE COOP_ID = '" + coop_id + "' AND to_number(MEMBER_NO) = '" + member_no + "' AND ACCOUNT_STATUS = 1 AND ROWNUM = 1";
                Sdt dtselect = WebUtil.QuerySdt(Selecdeptacc);
                if (dtselect.Next())
                {
                    String deptaccount_no = dtselect.GetString("deptaccount_no");
                    DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);
                    jSRetiveDwDetail();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูล");
                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขทะเบียน ให้ถูกต้อง");
                DwMain.Reset();
                DwMain.InsertRow(0);
                WebSheetLoadEnd();
            }
        }

        private void jSRetiveDwDetail()
        {
            string coop_id = DwMain.GetItemString(1, "atmmember_coop_id");
            string member_no = DwMain.GetItemString(1, "atmmember_member_no");
            string deptaccount_no = DwMain.GetItemString(1, "deptaccount_no");
            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, coop_id, member_no, deptaccount_no);
        }
    }
}