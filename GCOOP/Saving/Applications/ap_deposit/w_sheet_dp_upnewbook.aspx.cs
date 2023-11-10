using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saving.WcfDeposit;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_upnewbook : PageWebSheet, WebSheet
    {
        protected string postDeptAccountNo;
        protected string postReset;
        protected string postDeptAccountNoHd;
        protected string postBarcode;

        public void InitJsPostBack()
        {
            postDeptAccountNo = WebUtil.JsPostBack(this, "postDeptAccountNo");
            postDeptAccountNoHd = WebUtil.JsPostBack(this, "postDeptAccountNoHd");
            postReset = WebUtil.JsPostBack(this, "postReset");
            postBarcode = WebUtil.JsPostBack(this, "postBarcode");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DUtil.InsertFormMode(SlipMaster1);
                DUtil.SetItem(SlipMaster1, 1, "deptcoop_id", state.SsCoopControl);
                DUtil.SetItem(SlipMaster1, 1, "coop_id", state.SsCoopId);
                DUtil.SetItem(SlipMaster1, 1, "recppaytype_code", "PRINT");
                string clientId = DUtil.GetColumnClientId(SlipMaster1, 1, "deptslip_amt");
                this.SetFocusByClientId(clientId, this.GetType());
            }
            else
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDeptAccountNo")
            {
                JsPostDeptAccountNo();
            }
            else if (eventArg == "postDeptAccountNoHd")
            {
                JsPostDeptAccountNo();
            }
            else if (eventArg == "postReset")
            {
            }
            else if (eventArg == "postBarcode")
            {
                JsPostBarcode();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String deptAccountNo = SlipMaster1.GetItemString(1, "deptaccount_no");
                String bookNo = SlipMaster1.GetItemString(1, "deptslip_amt");
                String entry_id = state.SsUsername;
                DateTime workDate = state.SsWorkDate;
                if (String.IsNullOrEmpty(deptAccountNo) || String.IsNullOrEmpty(bookNo))
                {
                    throw new Exception("กรุณาใส่ค่าเลขบัญชี และ เลขสมุด");
                }
                wcf.Deposit.UpNewBook(state.SsWsPass, deptAccountNo, bookNo, entry_id, workDate);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            HdDumAccountNo.Value = "";
            HdDumCoopId.Value = "";
            try
            {
                //decimal deptslip_amt = DUtil.GetItemDecimalTryCatch(SlipMaster1, 1, "deptslip_amt");
                //DUtil.SetItem(SlipMaster1, 1, "total_slip_amt", deptslip_amt.ToString("#,##0"));
            }
            catch { }
        }

        private void JsPostDeptAccountNo()
        {
            DepositClient deptService = wcf.Deposit;
            string deptAccNo = SlipMaster1.GetItemStringTryCatch(1, "deptaccount_no").Trim();
            if (deptAccNo != "")
            {
                try
                {
                    deptAccNo = deptService.BaseFormatAccountNo(state.SsWsPass, deptAccNo);
                }
                catch { }
            }

            string deptCoopId = SlipMaster1.GetItemStringTryCatch(1, "deptcoop_id");

            if (HdDumAccountNo.Value != "")
            {
                SlipMaster1.arg_coop_id = HdDumCoopId.Value;
                SlipMaster1.arg_deptaccount_no = HdDumAccountNo.Value;
                deptAccNo = HdDumAccountNo.Value;
            }
            else
            {
                SlipMaster1.arg_coop_id = deptCoopId;
                SlipMaster1.arg_deptaccount_no = deptAccNo;
            }
            SlipMaster1.arg_application = state.SsApplication;

            int row = SlipMaster1.Retrieve(this);
            DUtil.SetItem(SlipMaster1, 1, "coop_id", state.SsCoopId);

            string depttype_code = SlipMaster1.GetItemStringTryCatch(1, "depttype_code");

            if (row <= 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขบัญชี " + deptAccNo + " กรุณากด <span onclick='MenubarNew()' style='cursor:pointer;color:#FF9900'><u>New[F2]</u></span>");
                return;
            }

            string deptFormat = deptService.ViewAccountNoFormat(state.SsWsPass, deptAccNo);
            SlipMaster1.SetItem(1, "deptformat", deptFormat);
            string clientId = DUtil.GetColumnClientId(SlipMaster1, 1, "deptslip_amt");
            this.SetFocusByClientId(clientId, this.GetType());
        }

        private void JsPostBarcode()
        {
            String barcodeNo = SlipMaster1.GetItemString(1, "deptslip_amt");
            String sql = "select * from dpdeptmaster where coop_id='" + state.SsCoopControl + "' and deptpassbook_no='" + barcodeNo + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            String deptaccountNo = "";
            if (dt.Next())
            {
                deptaccountNo = dt.GetString("deptaccount_no");
            }
            if (!string.IsNullOrEmpty(deptaccountNo))
            {
                SlipMaster1.arg_application = state.SsApplication;
                SlipMaster1.arg_coop_id = state.SsCoopControl;
                SlipMaster1.arg_deptaccount_no = deptaccountNo;
                int ii = SlipMaster1.Retrieve(this);
                try
                {
                    SlipMaster1.SetItem(1, "deptslip_amt", barcodeNo);
                    SlipMaster1.SetItem(1, "recppaytype_code", "PRINT");
                }
                catch { }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสบาร์โคด " + barcodeNo);
            }
        }
    }
}