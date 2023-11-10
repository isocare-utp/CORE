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
using DataLibrary;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;



namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_multislip_width : PageWebSheet, WebSheet
    {

        private DwThDate tDwCheque;
        private n_depositClient depService;
        private String pblFileName = "dp_multislip.pbl";
        private bool completeCheque;

        //POSTBACK

        protected String newClear;
        protected String postPost;
        protected String postDeptAccountNo;
        protected String postchequeno;
        protected String postChequeType;
        protected String postInsertRowCheque;
        protected String postDeleteRowCheque;
        protected String postInsertRowCash;
        protected String postDeleteRowCash;
        protected String postBankCode;
        protected String postBankBranchCode;


        Sta ta;
        //OPEN DIALOG
        protected String openDialog;

        public void InitJsPostBack()
        {
            newClear = WebUtil.JsPostBack(this, "newClear");
            postPost = WebUtil.JsPostBack(this, "postPost");
            postDeptAccountNo = WebUtil.JsPostBack(this, "postDeptAccountNo");
            postchequeno = WebUtil.JsPostBack(this, "postchequeno");
            postChequeType = WebUtil.JsPostBack(this, "postchequetype");
            postInsertRowCheque = WebUtil.JsPostBack(this, "postInsertRowCheque");
            postInsertRowCash = WebUtil.JsPostBack(this, "postInsertRowCash");
            postDeleteRowCash = WebUtil.JsPostBack(this, "postDeleteRowCash");
            postDeleteRowCheque = WebUtil.JsPostBack(this, "postDeleteRowCheque");
            postBankCode = WebUtil.JsPostBack(this, "postBankCode");
            postBankBranchCode = WebUtil.JsPostBack(this, "postBankBranchCode");

            tDwCheque = new DwThDate(DwCheque, this);
            tDwCheque.Add("cheque_date", "cheque_tdate");
        }
        public String GetDpDeptConstant(String wsPass, String column)
        {
            String result = "";
            try
            {
                Sdt dt = ta.Query("select " + column + " from DPDEPTCONSTANT");
                if (!dt.Next())
                {
                    //ta.Close();
                    throw new Exception("ไม่มีข้อมูล column " + column);
                }
                result = dt.GetString(0);
            }
            catch { }
            ta.Close();
            return result;
        }
        public void WebSheetLoadBegin()
        {
            HdIsInsertCash.Value = "false";
            HdIsInsertCheque.Value = "false";
            completeCheque = true;
            try
            {
                depService = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ WebService ได้");
                return;
            }
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                HdDeptAccountNo.Value = "";
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "branch_id", state.SsCoopId);
                try
                {
                    HdDayPassCheq.Value = GetDpDeptConstant(state.SsWsPass, "daypasschq");
                }
                catch
                {
                    HdDayPassCheq.Value = "1";
                }
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwCash);
                this.RestoreContextDw(DwCheque);
            }
            LoopCheque();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postBankCode")
            {
                JsPostBankCode();
            }
            else if (eventArg == "postBankBranchCode")
            {
                JsPostBankBranchCode();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "postDeptAccountNo")
            {
                JsPostDeptAccountNo();
            }
            else if (eventArg == "postchequetype")
            {
                JsPostchequetype();
            }
            else if (eventArg == "postInsertRowCheque")
            {
                JsPostInsertRowCheque();
            }
            else if (eventArg == "postInsertRowCash")
            {
                JsPostInsertRowCash();
            }
            else if (eventArg == "postDeleteRowCash")
            {
                JsPostDeleteRowCash();
            }
            else if (eventArg == "postDeleteRowCheque")
            {
                JsPostDeleteRowCheque();
            }
        }

        public void SaveWebSheet()
        {
            if (!completeCheque)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขที่เช็คให้ครบถ้วน!");
                return;
            }
            try
            {
                String depFormat = DwMain.GetItemString(1, "deptaccount_no");
                DwMain.SetItemString(1, "deptaccount_no", HdDeptAccountNo.Value);
                String xmlMain = DwMain.Describe("DataWindow.Data.XML");
                String xmlDeptCash = "";
                String xmlDeptCheque = "";
                String as_apvdoc = "";
                if (!string.IsNullOrEmpty(HdDeptAccountNo.Value) && !string.IsNullOrEmpty(DwMain.GetItemString(1, "branch_id")))
                {
                    xmlDeptCheque = DwCheque.RowCount > 0 ? DwCheque.Describe("DataWindow.Data.XML") : "";
                    xmlDeptCash = DwCash.RowCount > 0 ? DwCash.Describe("DataWindow.Data.XML") : "";
                }
                bool result = depService.of_multi_deposit(state.SsWsPass, xmlMain, xmlDeptCash, xmlDeptCheque, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, ref as_apvdoc);
                if (result)
                {
                    JsNewClear();
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลการฝากหลายยอดบัญชี " + depFormat + " สำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwCash.SaveDataCache();
            DwCheque.SaveDataCache();
        }

        //JS-EVENT
        private void JsNewClear()
        {
            HdDeptAccountNo.Value = "";
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemString(1, "branch_id", state.SsCoopId);
            DwCash.Reset();
            DwCheque.Reset();
        }

        //JS-EVENT
        private void JsPostchequetype()
        {
            Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
            String cheque_type = DwCheque.GetItemString(row, "cheque_type");
            if (cheque_type == "BC")
            {
                DwCheque.SetItemDecimal(row, "day_float", 99);
            }
            else
            {
                DwCheque.SetItemDecimal(row, "day_float", Convert.ToDecimal(HdDayPassCheq.Value));
            }
        }

        //JS-EVENT
        private void JsPostBankCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String sql = "select bank_desc from cmucfbank where bank_code='" + bankCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String bankName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "bank_name", bankName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสธนาคาร " + bankCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostBankBranchCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String branchCode = DwCheque.GetItemString(row, "branch_code");
                String sql = "select branch_name from cmucfbankbranch where bank_code='" + bankCode + "' and branch_id='" + branchCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String branchName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "branch_name", branchName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสสาขาธนาคารเลขที่ " + branchCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public String ViewAccountNoFormat(String wsPass, String deptAccountNo)
        {
            Sdt dt = ta.Query("select deptcode_mask from DPDEPTCONSTANT");
            if (!dt.Next())
            {
                ta.Close();
                throw new Exception("ไม่สามารถติดต่อกับ Database ได้");
            }
            else
            {
                ta.Close();
            }
            String format = dt.GetString(0).ToUpper();//"X-XX-XXXXXXX";
            char[] fc = format.ToCharArray();
            char[] ac = deptAccountNo.ToCharArray();
            String accNo = "";
            int j = 0;
            for (int i = 0; i < fc.Length; i++)
            {
                if (fc[i] != 'X')
                {
                    accNo += fc[i].ToString();
                }
                else
                {
                    try
                    {
                        accNo += ac[j++];
                    }
                    catch { accNo += ""; }
                }
            }
            return accNo;
        }
        //JS-EVENT
        private void JsPostDeptAccountNo()
        {
            String deptAccountNo = "";
            String branchId = "";
            try
            {
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                deptAccountNo = depService.of_analizeaccno(state.SsWsPass, deptAccountNo);
                branchId = DwMain.GetItemString(1, "branch_id");
                DwMain.Reset();
                object[] args = new object[2] { deptAccountNo, branchId };
                DwUtil.RetrieveDataWindow(DwMain, pblFileName, null, args);
                String deptNewFormat = ViewAccountNoFormat(state.SsWsPass, deptAccountNo);
                DwMain.SetItemString(1, "deptaccount_no", deptNewFormat);
                HdDeptAccountNo.Value = deptAccountNo;
            }
            catch (Exception ex)
            {
                HdDeptAccountNo.Value = "";
                ex.ToString();
            }
            if (DwMain.RowCount < 1 || string.IsNullOrEmpty(HdDeptAccountNo.Value))
            {
                JsNewClear();
            }
        }

        //JS-EVENT
        private void JsPostInsertRowCheque()
        {
            //throw new NotImplementedException();
            DwCheque.InsertRow(0);
            DwCheque.SetItemDateTime(DwCheque.RowCount, "cheque_date", state.SsWorkDate);
            DwCheque.SetItemDecimal(DwCheque.RowCount, "day_float", int.Parse(HdDayPassCheq.Value));
            tDwCheque.Eng2ThaiAllRow();
            HdIsInsertCheque.Value = "true";
        }

        //JS-EVENT
        private void JsPostInsertRowCash()
        {
            DwCash.InsertRow(0);
            HdIsInsertCash.Value = "true";
        }

        //JS-EVENT
        private void JsPostDeleteRowCash()
        {
            try
            {
                DwCash.DeleteRow(int.Parse(HdDwCashRow.Value));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostDeleteRowCheque()
        {
            try
            {
                DwCheque.DeleteRow(int.Parse(HdDwChequeRow.Value));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void LoopCheque()
        {
            try
            {
                for (int i = 1; i <= DwCheque.RowCount; i++)
                {
                    try
                    {
                        String chequeNo = DwUtil.GetString(DwCheque, i, "cheque_no", "");
                        completeCheque = chequeNo == "" ? false : completeCheque;
                        int ii = chequeNo == "" ? 0 : int.Parse(chequeNo);

                        if (ii > 0)
                        {
                            DwCheque.SetItemString(i, "cheque_no", ii.ToString("0000000"));
                        }
                        else
                        {
                            completeCheque = false;
                        }
                    }
                    catch { completeCheque = false; }
                }
            }
            catch { }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox1.Text = int.Parse(TextBox1.Text).ToString("#,###,###,##0.00");
            }
            catch
            {
                TextBox1.Text = "0.00";
            }
        }
    }
}
