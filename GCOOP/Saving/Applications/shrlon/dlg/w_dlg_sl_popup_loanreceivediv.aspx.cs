using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using System.Text;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_popup_loanreceivediv : PageWebDialog, WebDialog
    {
        private n_shrlonClient slService;
        private n_commonClient commonSrv;
        private DwThDate tDwMain;

        protected String saveSlipLnRcv;
        protected String initDataWindow;
        protected String initLnRcvReCalInt;
        protected String calculateitempayamt;
        protected String jsRefresh;
        protected ArrayList dwList;
        protected String[] arrValue = new String[3];
        String loanContract_No;
        String as_initfrom = "", as_reqcontno = "", as_xmllnrcv = "";
        protected String GetNewLoan;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            saveSlipLnRcv = WebUtil.JsPostBack(this, "saveSlipLnRcv");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            initLnRcvReCalInt = WebUtil.JsPostBack(this, "initLnRcvReCalInt");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            GetNewLoan = WebUtil.JsPostBack(this, "GetNewLoan");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("operate_date", "operate_tdate");

        }

        public void WebDialogLoadBegin()
        {
            slService = wcf.NShrlon;
            commonSrv = wcf.NCommon;

            if (!IsPostBack)
            {
                this.InitDataWindow();
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_loan_receive.pbl", null);
                //DataWindowChild moneyType = DwMain.GetChild("moneytype_code");
                //String xml = commonSrv.GetDDDWXml(state.SsWsPass, "dddw_sl_ucfmoneytypeday");
                //moneyType.ImportString(xml, FileSaveAsType.Xml);


            }
            else
            {
                this.RestoreContextDw(DwMain);
                try
                {
                    String dtString = DwMain.GetItemString(1, "operate_tdate");
                    dtString = dtString.Replace("/", "");
                    DwMain.SetItemDateTime(1, "operate_date", DateTime.ParseExact(dtString, "ddMMyyyy", WebUtil.TH));
                }
                catch { }
                DwOperateLoan.RestoreContext();
                DwOperateEtc.RestoreContext();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveSlipLnRcv")
            {
                this.SaveSlipLnRcv();
            }
            if (eventArg == "initLnRcvReCalInt")
            {
                this.InitLnRcvReCalInt();
            }
            if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            if (eventArg == "fieldProperty")
            {

            }
            if (eventArg == "jsRefresh")
            {
                Refresh();
            }
            if (eventArg == "GetNewLoan")
            {
                JsGetNewLoan();
            }
            if (eventArg == "calculateitempayamt")
            {
                Calculateitempayamt();
            }
        }

        private void Calculateitempayamt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;

            Decimal dwmain_bfshrcontbalamt = DwMain.GetItemDecimal(1, "bfshrcont_balamt");

            //ยอดโอนชำระ
            Decimal payoutclramt = 0;



            for (int i = 1; i <= loanAllRow; i++)
            {
                Decimal interest_period = DwOperateLoan.GetItemDecimal(i, "interest_period");
                Decimal bfintarr_amt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt");
                Decimal sum_interest_bfintarr = interest_period + bfintarr_amt;
                Decimal interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                Decimal itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");

                //ต้นเงิน bfshrcont_balamt
                Decimal dwloan_bfshrcontbalamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                Decimal operateflag = DwOperateLoan.GetItemDecimal(i, "operate_flag");
                if (itempayamt > dwloan_bfshrcontbalamt)
                {
                    Response.Write("<script>alert('ยอดชำระมากกว่า ต้นเงิน');</script>");

                }
                else
                {
                    if (itempayamt != 0)
                    {
                        if (operateflag == 1)
                        {
                            if (itempayamt < sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr - itempayamt);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", 0);
                            }
                            else if (itempayamt > sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", itempayamt - sum_interest_bfintarr);
                            }

                        }
                    }


                    //ใช้คำนวณ ยอด ต้นคงเหลือ
                    Decimal principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                    DwOperateLoan.SetItemDecimal(i, "item_balance", dwloan_bfshrcontbalamt - principalpayamt);

                    //หายอดรวมที่ต้องชำระ

                    itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                    totalamt += itempayamt;
                    payoutclramt += principalpayamt + interest_payamt;
                }
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutclr_amt", payoutclramt);
                Decimal payout_amt = DwMain.GetItemDecimal(1, "payout_amt");
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutnet_amt", payout_amt - payoutclramt);
            }
        }
        private void JsGetNewLoan()
        {
            String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            String loan_no = slService.of_gennewcontractno(state.SsWsPass, state.SsCoopId, shrlontype_code);
            DwMain.SetItemString(1, "loancontract_no", loan_no);
        }
        public void WebDialogLoadEnd()
        {
            //Retrive DDDW
            try
            {
                //Clear ค่าใน DDDW
                DwMain.SetItemString(1, "tofrom_accid_1", "");
                //แสดงค่าใน tofrom_accid_1
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_loan_receive.pbl", null);
                DataWindowChild dc = DwMain.GetChild("tofrom_accid_1");
                String moneyType = DwUtil.GetString(DwMain, 1, "moneytype_code_1", "");
                //ตรวจสอบค่าว่าง
                if (!string.IsNullOrEmpty(moneyType))
                {
                    // กำหนด filter
                    dc.SetFilter("moneytype_code = '" + moneyType + "'");
                    dc.Filter();
                }
                //แสดงข้อมูลเฉพาะ ของ โอนธนาคาร และเช็คธนาคาร
                if ((moneyType == "CBT") || (moneyType == "CHQ"))
                {

                    DwMain.Modify("expense_bank_t.visible =1");
                    DwMain.Modify("expense_bank.visible =1");
                    DwMain.Modify("expense_branch_t.visible =1");
                    DwMain.Modify("expense_branch.visible =1");
                    DwMain.Modify("expense_accid_t.visible =1");
                    DwMain.Modify("expense_accid.visible =1");
                }
                else
                {
                    //ไม่แสดงข้อมูล
                    DwMain.Modify("expense_bank_t.visible =0");
                    DwMain.Modify("expense_bank.visible =0");
                    DwMain.Modify("expense_branch_t.visible =0");
                    DwMain.Modify("expense_branch.visible =0");
                    DwMain.Modify("expense_accid_t.visible =0");
                    DwMain.Modify("expense_accid.visible =0");

                    // เฉพาะเงินสดกำหนดค่าให้ได้เลย
                    if (moneyType == "CSH")
                    {
                        DwMain.SetItemString(1, "tofrom_accid_1", "11100001");
                    }
                }
                DwUtil.RetrieveDDDW(DwMain, "shrlontype_code_1", "sl_loan_receive.pbl", null);
            }
            catch { }
            DwMain.SaveDataCache();

        }

        #endregion

        private void InitDataWindow()
        {

            String memberNo = "";
            String memcoopid;
            try { memcoopid = DwMain.GetItemString(1, "memcoop_id"); }
            catch { memcoopid = state.SsCoopControl; }
            String as_coopid = state.SsCoopId;



            str_slippayout sSlipPayOut = new str_slippayout();
            sSlipPayOut.member_no = Request["member_no"].ToString();
            int result = slService.of_getmemblnrcv(state.SsWsPass, memcoopid, sSlipPayOut.member_no, ref as_initfrom, ref as_coopid, ref as_reqcontno, ref as_xmllnrcv);
            if (result == 1)
            {
                dw_list.Visible = false;

                sSlipPayOut.coop_id = state.SsCoopId;
                sSlipPayOut.contcoop_id = state.SsCoopControl;
                sSlipPayOut.entry_id = state.SsUsername;
                sSlipPayOut.operate_date = state.SsWorkDate;
                sSlipPayOut.loancontract_no = as_reqcontno;
                sSlipPayOut.member_no = memberNo;
                sSlipPayOut.slip_date = state.SsWorkDate;
                sSlipPayOut.initfrom_type = as_initfrom;
                HfFormtype.Value = as_initfrom;
                try
                {
                    slService.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                DwMain.Reset();
                DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);



                DwUtil.RetrieveDDDW(DwMain, "expense_bank", "sl_loansrv_slip_all_cen.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "expense_branch", "sl_loansrv_slip_all_cen.pbl", null);

                DwUtil.DeleteLastRow(DwMain);
                tDwMain.Eng2ThaiAllRow();

                try
                {
                    DwOperateLoan.Reset();
                    DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);

                }
                catch (Exception ex) { String ext = ex.ToString(); }
                try
                {
                    DwOperateEtc.Reset();
                    DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_loansrv_slip_all_cen.pbl", null);
                }
                catch (Exception ex) { String ext = ex.ToString(); }
                InitLnRcvReCalInt();
            }
            else if (result == 2)
            {
                dw_list.Visible = true;
                dw_list.Reset();
                TextBox2.Text = as_xmllnrcv;
                try
                {
                    dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                catch { DwUtil.ImportData(as_xmllnrcv, dw_list, null, Sybase.DataWindow.FileSaveAsType.Xml); }
            }

            if (HfFormtype.Value == "REQ")
            {
                Button1.Visible = true;
                TextBox1.Visible = true;
                String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
                String loan_no = slService.of_getnextcontractno(state.SsWsPass, state.SsCoopControl, shrlontype_code);
                TextBox1.Text = loan_no;
            }
            else
            {
                Button1.Visible = false;
                TextBox1.Visible = false;
            }

            //JsGetNewLoan();

        }

        private void InitLnRcvReCalInt()
        {
            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            try { strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no"); }
            catch { strPayOut.loancontract_no = loanContract_No; }

            strPayOut.member_no = DwMain.GetItemString(1, "member_no"); ;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;
            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML");
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;


            slService.of_initlnrcv_recalint(state.SsWsPass, ref strPayOut);

            try
            {
                DwMain.Reset();
                DwMain.ImportString(strPayOut.xml_sliphead, FileSaveAsType.Xml);
                if (DwMain.RowCount > 1)
                {
                    DwMain.DeleteRow(DwMain.RowCount);
                }
            }
            catch { DwMain.Reset(); }
            try
            {
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(strPayOut.xml_slipcutlon, FileSaveAsType.Xml);
            }
            catch { DwOperateLoan.Reset(); }
            try
            {
                DwOperateEtc.Reset();
                DwOperateEtc.ImportString(strPayOut.xml_slipcutetc, FileSaveAsType.Xml);
            }
            catch { DwOperateEtc.Reset(); }
        }

        private void SaveSlipLnRcv()
        {
            String memno = DwMain.GetItemString(1, "member_no");
            //int index = Convert.ToInt32(HfIndex.Value);
            //int allIndex = Convert.ToInt32(HfAllIndex.Value);

            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.contcoop_id = state.SsCoopControl;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            try
            {
                strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no");
            }
            catch { strPayOut.loancontract_no = ""; }
            strPayOut.member_no = memno;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;

            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            try { dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML"); }
            catch { dwLoanXML = ""; }
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;

            try
            {
                int result = slService.of_saveord_lnrcv(state.SsWsPass, ref strPayOut);
                //int nextIndex = index + 1;
                //if (nextIndex > allIndex)
                //{
                //    nextIndex = index - 1;
                //}
                //HfIndex.Value = nextIndex.ToString();
                HdIsPostBack.Value = "false";
                //Response.Write("<script>alert('บันทึกสำเร็จ');</script>");
                //if (nextIndex != allIndex)
                //{
                //    this.InitDataWindow();
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                // Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); 
            }




        }
        private void Refresh()
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //    String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //    String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
            //    TextBox1.Text = loan_no;
        }
    }
}
