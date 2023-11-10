using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loan_requestment : PageWebSheet, WebSheet
    {
        public String lc_loantype = null;
        public int ii_righttype;
        public int ii_apvtype;
        public string is_massalert = "";    //เก็บข้อความเตือน
        public Boolean ibl_step = true;     //ใช้ตรวจสอบการดึง dlg ประเภท ดูจากหลักประกัน ดูจากเงินฝาก
        private DwThDate tDwMain;
        //*******ประกาศตัวเกี่ยวกับ  Javascript********//
        protected string jsPostmember;
        protected string jsPostSetPeriodSend;
        protected string jsPostSumClear;
        protected string jsPostSetPeriodPay;
        protected string jsPostChangePeriodPay;
        protected string jsPostRePermiss;
        protected string jsPostRightColl;
        protected String jsExpenseBank;
        protected String jsExpenseCode;
        protected String jsGetMemberCollno;
        protected String jsReNewPage;
        protected String jsOpenOldDocNo;
        protected String jsPostSetZero;
        protected String jsCancelRequest;
        protected String jsRefresh;
        protected String jsSetDataList;
        protected string jsSetloantypechg;
        protected string jsPostDelOtherclr;
        protected string jsExpensebankbrRetrieve;
        protected string jsGetitemdescetc;
        protected string jsGetexpensememno;
        protected string jsPostGetCollPermiss;
        protected string jsPostRequestamt;
        protected string jsPostDelMthexp;
        protected string jsGetitemdescmthexp;
        protected string jsPostPrtRpt;
        protected string jsPostChgrqDate;
        protected String jsChgReal;
        protected String jsPostMthpay;

        string reqdoc_no = "";
        String member_no = "";
        int x = 1;

        //private n_shrlonClient shrlonService;
        //private ShrlonClient shrlonService;

        String ls_membtype = "";
        String ls_loanpermgrp = "";
        String loantype = "";
        String pbl = "sl_loan_requestment.pbl";
        private int flag;//ตรวจสอบการดึงข้อมูล

        Sdt dt;
        Sta ta;

        /// <summary>
        /// Check Init Javascript
        /// </summary>
        public void InitJsPostBack()
        {
            jsPostmember = WebUtil.JsPostBack(this, "jsPostmember");
            jsPostSetPeriodSend = WebUtil.JsPostBack(this, "jsPostSetPeriodSend");
            jsPostSumClear = WebUtil.JsPostBack(this, "jsPostSumClear");
            jsPostSetPeriodPay = WebUtil.JsPostBack(this, "jsPostSetPeriodPay");
            jsPostChangePeriodPay = WebUtil.JsPostBack(this, "jsPostChangePeriodPay");
            jsPostRePermiss = WebUtil.JsPostBack(this, "jsPostRePermiss");
            jsPostRightColl = WebUtil.JsPostBack(this, "jsPostRightColl");
            jsGetexpensememno = WebUtil.JsPostBack(this, "jsGetexpensememno");
            jsExpenseCode = WebUtil.JsPostBack(this, "jsExpenseCode");
            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsGetMemberCollno = WebUtil.JsPostBack(this, "jsGetMemberCollno");
            jsReNewPage = WebUtil.JsPostBack(this, "jsReNewPage");
            jsOpenOldDocNo = WebUtil.JsPostBack(this, "jsOpenOldDocNo");
            jsPostSetZero = WebUtil.JsPostBack(this, "jsPostSetZero");
            jsCancelRequest = WebUtil.JsPostBack(this, "jsCancelRequest");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsSetDataList = WebUtil.JsPostBack(this, "jsSetDataList");
            jsPostDelOtherclr = WebUtil.JsPostBack(this, "jsPostDelOtherclr");
            jsSetloantypechg = WebUtil.JsPostBack(this, "jsSetloantypechg");
            jsExpensebankbrRetrieve = WebUtil.JsPostBack(this, "jsExpensebankbrRetrieve");
            jsPostGetCollPermiss = WebUtil.JsPostBack(this, "jsPostGetCollPermiss");
            jsGetitemdescetc = WebUtil.JsPostBack(this, "jsGetitemdescetc");
            jsPostRequestamt = WebUtil.JsPostBack(this, "jsPostRequestamt");
            jsPostDelMthexp = WebUtil.JsPostBack(this, "jsPostDelMthexp");
            jsGetitemdescmthexp = WebUtil.JsPostBack(this, "jsGetitemdescmthexp");
            jsPostPrtRpt = WebUtil.JsPostBack(this, "jsPostPrtRpt");
            jsPostChgrqDate = WebUtil.JsPostBack(this, "jsPostChgrqDate");
            jsChgReal = WebUtil.JsPostBack(this, "jsChgReal");
            jsPostMthpay = WebUtil.JsPostBack(this, "jsPostMthpay");

            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("loanrequest_date", "loanrequest_tdate");
            tDwMain.Add("loanrcvfix_date", "loanrcvfix_tdate");
            tDwMain.Add("startkeep_date", "startkeep_tdate");
        }

        public void WebSheetLoadBegin()
        {
            //this.ConnectSQLCA();
            //ta = new Sta(sqlca.ConnectionString);
            //sqlca = new DwTrans();
            //sqlca.Connect();

            //try
            //{
            //    shrlonService = wcf.NShrlon;
            //}
            //catch
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            //    return;
            //}

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_coll);
                this.RestoreContextDw(dw_clear);
                this.RestoreContextDw(dw_otherclr);
                this.RestoreContextDw(dw_mthexp);
                LtServerMessagecoll.Text = "";
            }
            else
            {
                JsReNewPage();
                HdShowRemark.Value = "false";
            }

            if (dw_main.RowCount > 1)
            {
                dw_main.DeleteRow(dw_main.RowCount);
            }

            dw_message.Reset();
            dw_message.InsertRow(0);
            dw_message.DisplayOnly = false;
            dw_message.Visible = false;
            HdCheckRemark.Value = "false";
            Panel1.Visible = true;

            decimal ldc_inttype = dw_main.GetItemDecimal(1, "int_continttype");
        }

        /// <summary>
        /// Check  PostBack Javascript
        /// </summary>
        /// <param name="eventArg"></param>
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostmember")
            {
                JsPostmember();
            }
            else if (eventArg == "jsPostChgrqDate")
            {
                tDwMain.Thai2EngAllRow();
                dw_main.SetItemDateTime(1, "loanrcvfix_date", dw_main.GetItemDateTime(1, "loanrequest_date"));
                wcf.NShrlon.of_genintestimate(state.SsWsPass, dw_main.GetItemString(1, "member_no"), dw_main.GetItemDateTime(1, "loanrequest_date"));
            }
            else if (eventArg == "jsPostSetPeriodSend")
            {
                of_initperiodsend();
                of_calperiodpayment();
            }
            else if (eventArg == "jsPostSumClear")
            {
                of_initpaymonthcoop();
                if (Hdcheckrepermiss.Value != "1")//กรณีเลือดรายการหักหนี้เก่าไม่่ต้องคีร์ยอดเงินใหม่
                {
                    of_initpermiss();
                    of_calperiodpayment();
                }

                // of_calperiodpayment();
                of_initotherpay_fsv();
                of_initclearshare();
                //of_recalcollpermiss();
                of_initwarnings();
                of_sumclearall();
            }
            else if (eventArg == "jsPostSetPeriodPay")
            {
                int li_payflag = Convert.ToInt32(dw_main.GetItemDecimal(1, "custompayment_flag"));
                if (li_payflag == 1)
                {
                    return;
                }
                of_calperiodpayment();
                of_initpaymonthcoop();
                SetSalaryBal(); //เซตเงินเดือนคงเหลือสุทธิ:
            }
            else if (eventArg == "jsPostRequestamt")
            {
                of_setintestimateloanreq();
                ls_membtype = dw_main.GetItemString(1, "membtype_code");
                decimal ldc_reqamt = dw_main.GetItemDecimal(1, "loanreqregis_amt");  //pom
                string ls_loantype = dw_main.GetItemString(1, "loantype_Code");  //pom
                of_initmaxperiod(ls_loantype, ldc_reqamt); //pom
                of_initperiodsend();
                of_initpaymonthcoop();
                of_calperiodpayment();
                of_initclearshare();
                of_sumclearall();
                SetSalaryBal(); //เซตเงินเดือนคงเหลือสุทธิ:
            }
            else if (eventArg == "jsPostChangePeriodPay")
            {
                int li_payflag = Convert.ToInt32(dw_main.GetItemDecimal(1, "custompayment_flag"));
                if (li_payflag == 1)
                {
                    return;
                }

                int li_period = 0, li_maxsend = 0;
                decimal ldc_salarybal = 0, ldc_periodpay = 0, ldc_minsalary;
                li_maxsend = Convert.ToInt32(dw_main.GetItemDecimal(1, "maxsend_payamt"));

                decimal ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");
                decimal ldc_incomefixed = dw_main.GetItemDecimal(1, "incomemonth_fixed");
                decimal ldc_incomeoth = dw_main.GetItemDecimal(1, "incomemonth_other");
                decimal ldc_paycoop = dw_main.GetItemDecimal(1, "paymonth_coop");
                decimal ldc_paylnreq = dw_main.GetItemDecimal(1, "paymonth_lnreq");
                decimal ldc_payexp = dw_main.GetItemDecimal(1, "paymonth_exp");
                decimal ldc_payoth = dw_main.GetItemDecimal(1, "paymonth_other");
                ldc_periodpay = dw_main.GetItemDecimal(1, "period_payment");
                ldc_minsalary = dw_main.GetItemDecimal(1, "minsalary_amt");

                ldc_salarybal = (ldc_salary + ldc_incomefixed + ldc_incomeoth) - (ldc_paycoop + ldc_payexp + ldc_payoth) - ldc_periodpay;

                //if (ldc_salarybal < ldc_minsalary)
                //{
                //    dw_main.SetItemDecimal(1, "period_payment", Convert.ToDecimal(Hdperiodpay.Value));
                //    LtServerMessage.Text = WebUtil.ErrorMessage("เงินเดือนคงเหลือไม่ถึงตามกำหนด");
                //}

                // คำนวณจำนวนงวดที่ต้องชำระ
                this.of_calinstallment();

                of_setpaymthlnreq();
            }
            else if (eventArg == "jsPostRePermiss")
            {
                if (Hdcheckrepermiss.Value != "1")
                {
                    of_initpermiss();
                }
                of_calperiodpayment();
                of_initclearshare();
                of_sumclearall();
            }
            else if (eventArg == "jsPostRightColl")
            {
                try
                {
                    string ls_loantype = dw_main.GetItemString(1, "loantype_code");
                    string ls_membno = dw_main.GetItemString(1, "member_no");
                    string ls_coll = Hdcoll.Value;
                    string ls_contclr = Hdcontclr.Value;

                    string[] larr_contclr = ls_contclr.Split(',');

                    for (int r = 0; r < larr_contclr.Length; r++)
                    {
                        for (int row = 1; row <= dw_clear.RowCount; row++)
                        {
                            string ls_contno = dw_clear.GetItemString(row, "loancontract_no");
                            if (ls_contno == larr_contclr[r])
                            {
                                dw_clear.SetItemDecimal(row, "clear_status", 1);
                            }
                        }
                    }

                    of_initreqloanregis(ls_membno, ls_loantype);
                    decimal ldc_maxreq = dw_main.GetItemDecimal(1, "loanreqregis_amt");  //pom

                    of_initmaxperiod(ls_loantype, ldc_maxreq); //pom
                    of_initpaymonthcoop();
                    of_initperiodsend();
                    of_initpermiss();

                    string[] larr_coll = ls_coll.Split(',').ToArray();
                    for (int i = 0; i < larr_coll.Length; i++)
                    {
                        string[] larr_colltemp = larr_coll[i].Split(':').ToArray();
                        HdRefcoll.Value = larr_colltemp[larr_colltemp.Length - 1];
                        HdRefcollrow.Value = (i + 1).ToString();
                        int li_collrow = dw_coll.InsertRow(0);
                        dw_coll.SetItemString(li_collrow, "loancolltype_code", larr_colltemp[0]);
                        dw_coll.SetItemString(li_collrow, "ref_collno", HdRefcoll.Value);
                        CheckCollPermiss();
                    }

                    of_calperiodpayment();
                    of_initotherpay_fsv();
                    of_initclearshare();
                    of_sumclearall();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();
            }
            else if (eventArg == "jsExpenseCode")
            {
                dw_main.SetItemString(1, "expense_bank", "");
                dw_main.SetItemString(1, "expense_branch", "");
                dw_main.SetItemString(1, "expense_accid", "");
                JsExpenseCode();
            }
            else if (eventArg == "jsGetMemberCollno")
            {
                CheckCollPermiss();
            }
            else if (eventArg == "jsReNewPage")
            {
                JsReNewPage();
            }
            else if (eventArg == "jsOpenOldDocNo")
            {
                JsOpenOldDocNo();
            }
            else if (eventArg == "jsPostSetZero")
            {
                of_sumclearall();
            }
            else if (eventArg == "jsCancelRequest")
            {
                JsCancelRequest();
            }
            else if (eventArg == "jsChgReal")
            {
                jsChg();
            }
            else if (eventArg == "jsRefresh")
            {
            }
            else if (eventArg == "jsSetDataList")
            {
                JsSetDataList();
            }
            else if (eventArg == "jsExpensebankbrRetrieve")
            {
                JsExpensebankbrRetrieve();
            }
            else if (eventArg == "jsGetexpensememno")
            {
                JsGetexpensememno();
            }
            else if (eventArg == "jsPostGetCollPermiss")
            {
                HdCondition.Value = "true";
                int row_coll;                   //แถว ค้ำประกัน
                string as_collno = "", loancolltype_code = "";

                try { row_coll = Convert.ToInt32(HdRefcollrow.Value); }
                catch { row_coll = 1; }

                as_collno = dw_coll.GetItemString(row_coll, "ref_collno");
                loancolltype_code = dw_coll.GetItemString(row_coll, "loancolltype_code");
                Boolean result_chk = ChkSameColl(as_collno, row_coll, loancolltype_code);
                if (result_chk == true && as_collno != "")
                {
                    GetCollPermiss();
                }
            }
            else if (eventArg == "jsGetitemdescetc")
            {
                JsGetitemdescetc();
            }
            else if (eventArg == "jsPostDelOtherclr")
            {
                int row = int.Parse(HdRowNumber.Value);
                dw_otherclr.DeleteRow(row);
                of_sumclearall();
            }
            else if (eventArg == "jsPostDelMthexp")
            {
                int row = int.Parse(HdRowNumber.Value);
                dw_mthexp.DeleteRow(row);
            }
            else if (eventArg == "jsGetitemdescmthexp")
            {
                JsGetitemdescmthexp();
            }
            else if (eventArg == "jsPostPrtRpt")
            {
                try
                {
                    iReportArgument args = new iReportArgument();
                    string coop_id = state.SsCoopControl;
                    string loanreq_docno = dw_main.GetItemString(1, "loanrequest_docno");
                    //ใส่อกิวเม้น
                    //loanreq_docno = "Q570001197";
                    args.Add("as_loanreq_docno", iReportArgumentType.String, loanreq_docno);
                    args.Add("as_coop_id", iReportArgumentType.String, coop_id);
                    iReportBuider report1 = new iReportBuider(this, "หนังสือเสนอกรรมการ");
                    report1.AddCriteria("r_sl_loan_req_paper", "เปิด เพื่อพิมพ์หนังสือเสนอกรรมการ", ReportType.pdf, args);
                    report1.AutoOpenPDF = true;
                    report1.Retrieve();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("พิมพ์หนังสือไม่สำเร็จ " + ex);
                }
            }
            else if (eventArg == "jsPostMthpay") { JsPostMthpay(); }
        }

        private void JsPostMthpay()
        {
            String member_no = dw_main.GetItemString(1, "member_no");
            Decimal salary_amt = dw_main.GetItemDecimal(1, "salary_amt");
            Decimal income = dw_main.GetItemDecimal(1, "incomemonth_other") + dw_main.GetItemDecimal(1, "incomemonth_fixed");
            Decimal paymonth = dw_main.GetItemDecimal(1, "paymonth_other") + dw_main.GetItemDecimal(1, "paymonth_coop") + dw_main.GetItemDecimal(1, "paymonth_lnreq") + dw_main.GetItemDecimal(1, "paymonth_exp");
            Decimal paymonthoth = dw_main.GetItemDecimal(1, "paymonth_other");
            //หุ้น
            Decimal share_periodvalue = dw_main.GetItemDecimal(1, "share_periodvalue");

            //ใบคำขอ
            String loantype_code = dw_main.GetItemString(1, "loantype_code");
            Decimal loanpayment_type = dw_main.GetItemDecimal(1, "loanpayment_type");
            Decimal intestimate_amt = dw_main.GetItemDecimal(1, "intestimate_amt");
            Decimal period_payment = dw_main.GetItemDecimal(1, "period_payment");
            Decimal rkeep_interest = dw_clear.GetItemDecimal(1, "rkeep_interest");
            Decimal rkeep_principal = dw_clear.GetItemDecimal(1, "rkeep_principal");
            //รายการหักกลบ
            String loanclr = "";
            for (int i = 1; i <= dw_clear.RowCount; i++)
            {
                if (loanclr != "")
                {
                    loanclr += ";";
                }

                loanclr += dw_clear.GetItemString(i, "loancontract_no") + ",";
                loanclr += dw_clear.GetItemString(i, "clear_status") + ",";
                loanclr += dw_clear.GetItemString(i, "loanpayment_type") + ",";
                loanclr += dw_clear.GetItemString(i, "period_payment") + ",";
                loanclr += dw_clear.GetItemString(i, "intestnow_amt") + ",";
                loanclr += dw_clear.GetItemString(i, "loanpay_code") + ",";
                loanclr += dw_clear.GetItemString(i, "rkeep_principal") + ",";
                loanclr += dw_clear.GetItemString(i, "rkeep_interest");
            }
            //หักจากเงินเดือน
            String mthexp = "";
            for (int i = 1; i <= dw_mthexp.RowCount; i++)
            {
                Decimal clear_status = dw_mthexp.GetItemDecimal(i, "clear_status");
                if (clear_status == 0)
                {
                    mthexp += ";";
                    mthexp += dw_mthexp.GetItemString(i, "mthexp_code") + ",";
                    mthexp += dw_mthexp.GetItemString(i, "mthexp_desc") + ",";
                    mthexp += dw_mthexp.GetItemDecimal(i, "mthexp_amt");
                }
            }
            if (mthexp != "")
            {
                //mthexp = mthexp.substring(1);
                mthexp = mthexp.Substring(1);
            }

            //Gcoop.OpenIFrameExtend(570, 550, "w_dlg_sl_loanrequest_monthpay.aspx", "?income=" + income + "&paymonth=" + paymonth + "&member_no=" + member_no + "&salary_amt=" + salary_amt
            //                    + "&share_periodvalue=" + share_periodvalue + "&rkeep_interest=" + rkeep_interest + "&rkeep_principal=" + rkeep_principal
            //                    + "&loantype_code=" + loantype_code + "&loanpayment_type=" + loanpayment_type + "&intestnow_amt=" + intestimate_amt + "&period_payment=" + period_payment
            //                    + "&loanclr=" + loanclr + "&mthexp=" + mthexp + "&paymonthoth=" + paymonthoth);
            this.SetOnLoadedScript("Gcoop.OpenIFrameExtend(570, 550, 'w_dlg_sl_loanrequest_monthpay.aspx', '?income=" + income + "&paymonth=" + paymonth + "&member_no=" + member_no + "&salary_amt=" + salary_amt
                                + "&share_periodvalue=" + share_periodvalue + "&rkeep_interest=" + rkeep_interest + "&rkeep_principal=" + rkeep_principal
                                + "&loantype_code=" + loantype_code + "&loanpayment_type=" + loanpayment_type + "&intestnow_amt=" + intestimate_amt + "&period_payment=" + period_payment
                                + "&loanclr=" + loanclr + "&mthexp=" + mthexp + "&paymonthoth=" + paymonthoth + "')");
        }

        /// <summary>
        ///  reset หน้าใหม่
        /// </summary>
        private void JsReNewPage()
        {
            try
            {
                dw_main.Reset();
                dw_main.InsertRow(0);
                dw_coll.Reset();
                dw_clear.Reset();
                dw_otherclr.Reset();
                dw_mthexp.Reset();
                dw_intspc.Reset();
                dw_main.SetItemString(1, "memcoop_id", state.SsCoopId);
                dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                dw_main.SetItemDateTime(1, "loanrequest_date", state.SsWorkDate);
                dw_main.SetItemDate(1, "loanrcvfix_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
                RetreiveDDDW();
                HdIsPostBack.Value = "false";
                HdCheckRemark.Value = "false";
                HdShowRemark.Value = "false";
                Ltjspopup.Text = " ";
                Ltjspopupclr.Text = "";
                is_massalert = "";
            }
            catch { }
        }

        /// <summary>
        ///  retreive datawindows dropdown
        /// </summary>
        public void RetreiveDDDW()
        {
            try
            {
                loantype = Session["loantypeCode"].ToString();
                Session.Remove("loantype");
            }
            catch
            {
                String sql = "";

                sql = "select min(loantype_code) from lnloantype where  coop_id = '" + state.SsCoopControl + "'";

                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    lc_loantype = dt.Rows[0][0].ToString().Trim();
                    loantype = lc_loantype;
                    Session.Remove("loantype");
                }
                else
                {
                    throw new Exception("ไม่พบประเภทเงินกู้");
                }
            }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                if (loantype == "")
                {
                    ///<กำหนดค่าเริ่มต้น เป็นสามัญ>
                    dw_main.SetItemString(1, "loantype_code", lc_loantype);
                }
                else
                {
                    DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                    //dwloantype_code.SetFilter("LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
                    //dwloantype_code.Filter();
                    dw_main.SetItemString(1, "loantype_code", loantype);
                }
                DwUtil.RetrieveDDDW(dw_main, "committee_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "apvcondition_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "expense_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "sl_loan_requestment.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "loanobjective_code_1", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "membtype_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_coll, "loancolltype_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_coll, "coop_id", pbl, null);
                DwUtil.RetrieveDDDW(dw_otherclr, "clrothertype_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_mthexp, "mthexp_code", pbl, null);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        ///  init ใบคำขอเก่า
        /// </summary>
        private void of_initlnreq()
        {
            try
            {
                string ls_coopid = "", ls_reqloandocno = "";

                ls_coopid = state.SsCoopControl;
                ls_reqloandocno = dw_main.GetItemString(1, "loanrequest_docno");

                txt_reqNo.Text = ls_reqloandocno;
                txt_member_no.Text = member_no;

                string[] arg = new string[2] { ls_coopid, ls_reqloandocno };
                try { DwUtil.RetrieveDataWindow(dw_main, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }
                tDwMain.Eng2ThaiAllRow();
                try { DwUtil.RetrieveDataWindow(dw_clear, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }
                try { DwUtil.RetrieveDataWindow(dw_coll, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }
                try { DwUtil.RetrieveDataWindow(dw_otherclr, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }
                try { DwUtil.RetrieveDataWindow(dw_intspc, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }
                try { DwUtil.RetrieveDataWindow(dw_mthexp, pbl, null, arg); }
                catch (Exception ex) { ex.ToString(); }

                wcf.NShrlon.of_genintestimate(state.SsWsPass, member_no, dw_main.GetItemDateTime(1, "loanrequest_date"));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            decimal paymonth_exp = 0;
            try
            {
                paymonth_exp = dw_main.GetItemDecimal(1, "paymonth_exp");
            }
            catch { paymonth_exp = 0; }
            Hdpaymouthexp.Value = Convert.ToString(paymonth_exp);
        }

        /// <summary>
        ///  เช็คใบคำขอเก่า
        /// </summary>
        private Boolean of_checklnreq(string member_no)
        {
            try
            {
                string ls_CoopControl = state.SsCoopControl; //สาขาที่ทำรายการ
                String ls_loantype = "",
                    ls_lnrequestdate = "";

                ls_loantype = dw_main.GetItemString(1, "loantype_code");
                ls_lnrequestdate = dw_main.GetItemString(1, "loanrequest_tdate");

                string sqlStr = @"select loanrequest_docno, 
                        loanrequest_status, 
                        loanrequest_date
                    from lnreqloan 
                    where loanrequest_status in (11,8) 
                    and coop_id         = {0}
                    and member_no       = {1} 
                    and loantype_code   = {2}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no, ls_loantype);
                Sdt dtchk = WebUtil.QuerySdt(sqlStr);

                if (dtchk.Next())
                {
                    dw_main.SetItemString(1, "loanrequest_docno", dtchk.GetString("loanrequest_docno"));
                    DateTime ldtm_lnreq = dtchk.GetDate("loanrequest_date");
                    String entry_date = ldtm_lnreq.AddYears(543).ToString();

                    LtServerMessage.Text = WebUtil.WarningMessage("มีใบคำขอกู้สำหรับวันที่ " + entry_date + " แล้ว ระบบจะดึงข้อมูลใบคำขอให้อัตโนมัติ");

                    of_initlnreq();
                    return false;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("of_checklnreq" + ex);
            }
            return true;
        }

        /// <summary>
        ///  Post เลขสมาชิก
        /// </summary>
        private void JsPostmember()
        {
            string member_no = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            JsReNewPage();
            string[] arg = new string[2] { state.SsCoopId, member_no };
            DwUtil.RetrieveDDDW(dw_main, "loanobjective_code_1", pbl, ls_loantype);
            DwUtil.RetrieveDataWindow(dw_pmx, pbl, null, arg);
            dw_main.SetItemString(1, "member_no", member_no);
            dw_main.SetItemString(1, "loantype_code", ls_loantype);

            string sql = @"select loanpermgrp_code from lnloantype where coop_id = {0} and loantype_code = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_loantype);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                ls_loanpermgrp = dt.GetString("loanpermgrp_code");
            }
            Boolean isvalid_result = false,
                lnreq_result = true;

            lnreq_result = of_checklnreq(member_no);

            if (lnreq_result == true)
            {
                isvalid_result = of_isvalidmember(member_no);
            }
            if (isvalid_result == true)
            {
                of_setmemberinfo(member_no);
                of_setloantypeinfo(ls_loantype);
                of_setexpensedefault(member_no); ////pom : setexpense-default 2

                of_initloanclr(member_no);
                of_initloancredit(ls_loantype);
                of_sumclearall(); //stou

                if (ibl_step == true)
                {
                    of_initreqloanregis(member_no, ls_loantype);

                    //of_initmaxperiod(ls_loantype, 1);    3240857  //pom
                    decimal ldc_maxreq = dw_main.GetItemDecimal(1, "loanreqregis_amt");  //pom
                    of_initmaxperiod(ls_loantype, ldc_maxreq); //pom

                    of_initperiodsend();
                    of_initpaymonthcoop();
                    of_initpermiss();
                    of_calperiodpayment();
                    of_initclearshare();
                    of_initotherpay_fsv();
                    of_initwarnings();

                    SetLoanrequestAmt(); //เซตค่ายอดให้กู้ :
                    of_setintestimateloanreq();
                    of_calperiodpayment(); //เซตค่าชำระ/งวด:
                    of_initcolluse();  //tomy ย้าย
                    of_sumclearall();  //tomy ย้าย
                    SetSalaryBal(); //เซตเงินเดือนคงเหลือสุทธิ:
                }
            }
            //else { of_initwarnings(); }
        }
        //เซตเงินเดือนคงเหลือสุทธิ:
        private void SetSalaryBal()
        {
            //ยอดหักสหกรณ์: = ยอดหักสหกรณ์: + ชำระ/งวด: + ด/บ.ประมาณการ
            decimal ldc_paycoop = dw_main.GetItemDecimal(1, "paymonth_coop"); //ยอดหักสหกรณ์:
            Decimal period_payment = dw_main.GetItemDecimal(1, "period_payment"); //ชำระ/งวด:
            Decimal intestimate_amt = dw_main.GetItemDecimal(1, "intestimate_amt"); //ด/บ.ประมาณการ
            Decimal loanpayment_type = dw_main.GetItemDecimal(1, "loanpayment_type"); //ประเภทส่ง: 1=คงต้น, 2=คงยอด
            if (loanpayment_type == 1) { dw_main.SetItemDecimal(1, "paymonth_coop", ldc_paycoop + period_payment + intestimate_amt); }//เซตค่ายอดหักสหกรณ์: = ยอดหักสหกรณ์: + ชำระ/งวด: + ด/บ.ประมาณการ
            else if (loanpayment_type == 2) { dw_main.SetItemDecimal(1, "paymonth_coop", ldc_paycoop + period_payment); }//เซตค่ายอดหักสหกรณ์: = ยอดหักสหกรณ์: + ชำระ/งวด:
        }

        /*****
         *  choose case ai_paytype
            case 1 // คงต้น
            dec ldc_temp

            ldc_temp = ( ai_period * ( ldc_intrate * ( 30 / 365 ) ) + 1 )
            ldc_permamt = ( adc_payment * ai_period ) / ldc_temp

            case 2 // คงยอด
            dec ldc_fr = 0.00

            li_fixcaltype =  integer( inv_loansrv.of_getattribconstant( "fixpaycal_type" ) )

            if li_fixcaltype = 1 then
            // ด/บ ทั้งปี / 12
            ldc_fr = exp( - ai_period * log( ( 1 + ldc_intrate / 12 ) ) )
            ldc_permamt = adc_payment * ( 1 - ldc_fr ) / ( ldc_intrate / 12 )
            else
            // ด/บ 30 วัน/เดือน
            ldc_fr = exp( - ai_period * log( ( 1 + ldc_intrate * ( 30 / 365 ) ) ) )
            ldc_permamt = adc_payment * ( 1 - ldc_fr ) / ( ldc_intrate * ( 30 / 365 ) )
            end if
            end choose
         * */
        /// <summary>
        /// เซตค่ายอดให้กู้
        /// </summary>        
        private void SetLoanrequestAmt()
        {
            Decimal loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt"); //สิทธิกู้สูงสุด:
            Decimal salary_amt = dw_main.GetItemDecimal(1, "salary_amt"); //เงินเดือน:
            Decimal incomemonth_fixed = dw_main.GetItemDecimal(1, "incomemonth_fixed"); //เงินได้อื่นๆ
            Decimal incomemonth_other = dw_main.GetItemDecimal(1, "incomemonth_other"); //เงินได้อื่นๆ2
            Decimal paymonth_coop = dw_main.GetItemDecimal(1, "paymonth_coop"); //ยอดหักสหกรณ์:
            Decimal paymonth_lnreq = dw_main.GetItemDecimal(1, "paymonth_lnreq"); //ยอดหักสหกรณ์:2
            Decimal paymonth_exp = dw_main.GetItemDecimal(1, "paymonth_exp"); //ยอดหักอื่นๆ:
            Decimal paymonth_other = dw_main.GetItemDecimal(1, "paymonth_other"); //ยอดหักอื่นๆ:2
            Decimal ldc_minsalamt = dw_main.GetItemDecimal(1, "minsalary_amt"); //เงินเดือนคงเหลือขั้นต่ำ (600)
            Decimal period_payamt = dw_main.GetItemDecimal(1, "period_payamt"); //จำนวนงวด:
            Decimal ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate"); // อัตรา ด/บ ที่ใช้ในสัญญา

            //ง/ด คงเหลือ = (salary_amt + incomemonth_fixed + incomemonth_other ) - (  paymonth_coop + paymonth_exp + paymonth_other + paymonth_lnreq)
            Decimal salary_bal = (salary_amt + incomemonth_fixed + incomemonth_other) - (paymonth_coop + paymonth_exp + paymonth_other + paymonth_lnreq);
            //case 1 // คงต้น
            Double ldc_temp = 0, ldc_permamt = 0, adc_payment = 0;
            adc_payment = Convert.ToDouble(salary_bal - ldc_minsalamt);
            //เงินต้น = ง/ด คงเหลือ -  เงินเดือนคงเหลือขั้นต่ำ * จำนวนงวด
            salary_bal = (salary_bal - ldc_minsalamt) * period_payamt;
            ldc_temp = ((Convert.ToDouble(period_payamt) * (Convert.ToDouble(ldc_intrate) / 100.00) * (30.00 / 365.00)) + 1);
            //ldc_temp = (Convert.ToDouble(period_payamt) * (Convert.ToDouble(ldc_intrate) * (30 / 365)) + 1);
            ldc_permamt = Convert.ToDouble(adc_payment) * Convert.ToDouble(period_payamt) / ldc_temp;
            decimal ldc_roundfacter = 0, ldc_loanreq = 0;
            string sqlStr = "", ls_loantype = "";
            ls_loantype = dw_main.GetItemString(1, "loantype_code");

            sqlStr = @"select		lngrpcutright_flag, notmoreshare_flag, maxloan_amt, loanpermgrp_code, lngrpkeepsum_flag, showright_flag,
                    reqround_factor, reqround_type
                from		lnloantype
                where	( coop_id		= {0} )
                and		( loantype_code	= {1} )";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype);
            Sdt dt = WebUtil.QuerySdt(sqlStr);

            if (dt.Next())
            {
                ldc_roundfacter = dt.GetDecimal("reqround_factor");
            }
            // ปัดยอดให้กู้
            if (ldc_roundfacter != 0)
            {
                decimal ldc_tempfacter = Math.Abs(ldc_roundfacter);
                Decimal ldc_numberfac = 0;
                ldc_loanreq = Convert.ToDecimal(ldc_permamt);
                ldc_numberfac = (ldc_loanreq % ldc_roundfacter);
                if (ldc_numberfac > 0)
                {
                    //if (ldc_roundfacter > 0)
                    //{
                    //    ldc_loanreq = ldc_loanreq - (ldc_loanreq % ldc_roundfacter) + ldc_roundfacter;
                    //}
                    //else
                    //{
                    //    ldc_loanreq = ldc_loanreq - (ldc_loanreq % ldc_roundfacter);
                    //}
                    //เศษ >= 5 ปัดขึ้น 
                    if (ldc_numberfac >= 5) { ldc_loanreq = ldc_loanreq - ldc_numberfac + ldc_roundfacter; }
                    //เศษ < 5 ปัดลง
                    else { ldc_loanreq = ldc_loanreq - ldc_numberfac; }
                }
            }
            //ถ้า ยอดให้กู้ <= 0 หรือ ยอดให้กู้ > สิทธิกู้สูงสุด ให้ข้ามไปไม่ต้องเซตค่ายอดให้กู้ :
            if (ldc_loanreq <= 0 || ldc_loanreq > loancredit_amt) { return; }
            else
            {
                dw_main.SetItemDecimal(1, "loanrequest_amt", ldc_loanreq); //เซตค่ายอดให้กู้ :
            }
        }

        /// <summary>
        ///  ตรวจสอบการขอกู้ของสมาชิก
        /// </summary>
        private Boolean of_isvalidmember(string member_no)
        {
            string sqlStr = "",
                pauseloan_cause = "",
                ls_memtypedesc = "",
                ls_lnmemtypedesc = "",
                membtype_code = "",
                loancontract_no = "",
                ls_loantype = "";
            int resign_status = 0,
                droploanall_flag = 0,
                member_type = 0,
                li_lnmemtype = 0;

            string loantype_code = dw_main.GetItemString(1, "loantype_code");

            sqlStr = @"select  mbucfprename.prename_desc + mbmembmaster.memb_name + '   ' + mbmembmaster.memb_surname as member_name,resign_status,
	            droploanall_flag,
                member_type,
                membtype_code
                from mbmembmaster ,mbucfprename
                
                where mbmembmaster.prename_code = mbucfprename.prename_code
                and coop_id = {0}
	            and member_no = {1}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no);

            Sdt dt1 = WebUtil.QuerySdt(sqlStr);
            if (dt1.Next())
            {
                dw_main.SetItemString(1, "member_name", dt1.GetString("member_name"));
                resign_status = dt1.GetInt32("resign_status");
                droploanall_flag = dt1.GetInt32("droploanall_flag");
                member_type = dt1.GetInt32("member_type");
                membtype_code = dt1.GetString("membtype_code");

                if (resign_status == 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ได้ลาออกแล้ว ไม่สามารถทำรายการกู้เงินได้อีก");
                    return false;
                }
                if (droploanall_flag == 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ งดกู้ทุกประเภท");
                    return false;
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนสมาชิกคนนี้ #" + member_no + " กรุณาตรวจสอบ");
                return false;
            }

            // ตรวจสอบว่ามีการงดกู้บางประเภทหรือเปล่า
            sqlStr = @"select pauseloan_cause
                from lnmembpauseloan
                where coop_id = {0}
	            and loantype_code = {1}
	            and member_no = {2}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, loantype_code, member_no);

            Sdt dt2 = WebUtil.QuerySdt(sqlStr);

            if (dt2.Next())
            {
                pauseloan_cause = dt2.GetString("pauseloan_cause");
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ เป็นสมาชิกที่งดกู้เงินประเภทนี้อยู่ เหตุผล " + pauseloan_cause);
                return false;
            }

            sqlStr = @"select member_type
                from	lnloantype
                where	( coop_id		= {0} )
                and		( loantype_code	= {1} )";

            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, loantype_code);

            Sdt dt3 = WebUtil.QuerySdt(sqlStr);

            if (dt3.Next())
            {
                li_lnmemtype = dt3.GetInt32("member_type");
                if (li_lnmemtype != 0)
                {

                    if (member_type != li_lnmemtype)
                    {
                        if (member_type == 1)
                        {
                            ls_memtypedesc = "สมาชิกปกติ";
                        }
                        else
                        {
                            ls_memtypedesc = "สมาชิกสมทบ";
                        }

                        if (li_lnmemtype == 1)
                        {
                            ls_lnmemtypedesc = "สมาชิกปกติ";
                        }
                        else
                        {
                            ls_lnmemtypedesc = "สมาชิกสมทบ";
                        }
                        LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้เป็น '" + ls_memtypedesc + "' ไม่สามารถกู้เงินประเภทนี้ได้ เงินกู้นี้ให้กู้ได้เฉพาะ '" + ls_lnmemtypedesc + "' เท่านั้น");
                        //is_massalert += "\\n\\nสมาชิกคนนี้เป็น '" + ls_memtypedesc + "' ไม่สามารถกู้เงินประเภทนี้ได้ เงินกู้นี้ให้กู้ได้เฉพาะ '" + ls_lnmemtypedesc + "' เท่านั้น";
                        return false;
                    }
                }
            }

            // ตรวจสอบกลุ่มย่อยของสมาชิกสมทบว่ากู้ได้หรือเปล่า
            if (member_type == 2)
            {
                sqlStr = @"select   membtype_code
	                from		lnloantypembtype
	                where	( coop_id		= {0} )
	                and		( loantype_code	= {1} )
	                and		( membtype_code	= {2} )";

                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, loantype_code, membtype_code);

                Sdt dt4 = WebUtil.QuerySdt(sqlStr);

                if (dt4.Rows.Count < 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกสมทบกลุ่มนี้ไม่ได้รับอนุญาติให้กู้เงินประเภทนี้ กรุณาตรวจสอบ");
                    return false;
                }
            }

            sqlStr = @"select	a.loancontract_no, a.loantype_code
                from		lncontmaster a, lnloantypepause b
                where	( a.coop_id			= b.coop_id )
                and		( a.loantype_code	= b.loantype_code )
                and		( a.memcoop_id	= {0} )
                and		( a.member_no		= {1} )
                and		( a.contract_status > 0 )
                and		( b.coop_id			= {0} )
                and		( b.loantype_pause	= {2} )";

            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no, loantype_code);

            Sdt dt5 = WebUtil.QuerySdt(sqlStr);

            if (dt5.Rows.Count < 0)
            {
                loancontract_no = dt5.GetString("loancontract_no");
                ls_loantype = dt5.GetString("loantype_code");

                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้มีเงินกู้ " + loancontract_no + "(" + ls_loantype + ") อยู่ ไม่สามารถกู้เงินประเภทนี้ได้อีก");
                return false;
            }

            return true;
        }

        /// <summary>
        ///  set ข้อมูลสมาชิก
        /// </summary>
        private void of_setmemberinfo(string member_no)
        {
            try
            {
                string ls_membtypedesc = "";
                decimal ldc_sharestk = 0, ldc_periodshramt = 0, ldc_shrstkvalue = 0, ldc_periodshrvalue = 0;
                decimal ldc_retryage = 0, ck_incomeetc = 0, birth_age = 0, work_age = 0, member_age = 0 ;
                DateTime ldtm_birth = DateTime.Now, ldtm_reqdate;

                ldtm_reqdate = dw_main.GetItemDateTime(1, "loanrequest_date");

                string sqlstr =/* @"select mbmembmaster.coop_id,
                        mbmembmaster.member_no,
		                mbmembmaster.membgroup_code,
		                mbucfmembgroup.membgroup_desc , 
		                isnull(mbmembmaster.birth_date,convert(datetime,'1900-01-01')) as birth_date,
		                isnull(mbmembmaster.member_date,convert(datetime,'1900-01-01')) as member_date,   
		                isnull(mbmembmaster.work_date,convert(datetime,'1900-01-01')) as work_date,   
		                isnull(mbmembmaster.retry_date,convert(datetime,'1900-01-01')) as retry_date,

		               // to_number(ft_calage(birth_date,{2},4 )) 10 as birth_age,
		               // to_number(ft_calage(member_date,{2},8 ))20  as member_age,
		                //to_number(ft_calage(work_date,{2},8 )) 20 as work_age,
		                //to_number(ft_calage({2},retry_date,8 )) 30 as retry_age,

		                mbmembmaster.salary_amount,   		                  
		                mbmembmaster.membtype_code,   
		                mbucfmembtype.membtype_desc,   
		                shsharemaster.last_period,   
		                shsharemaster.sharestk_amt,   
		                shsharemaster.periodshare_amt,   
		                mbucfprename.prename_desc + mbmembmaster.memb_name + '   ' + mbmembmaster.memb_surname as member_name,   
		                shsharemaster.payment_status,      
		                mbmembmaster.remark,   		                		                 		                		                
		                mbmembmaster.rememb_flag,
                        mbmembmaster.retry_status,
                        mbmembmaster.incomeetc_amt
                from mbmembmaster,   
		                mbucfmembgroup,
		                mbucfprename,
		                mbucfmembtype,
		                shsharemaster  
                where ( mbmembmaster.coop_id = mbucfmembgroup.coop_id ) 
                and  ltrim( rtrim(mbmembmaster.membgroup_code)) =ltrim( rtrim(mbucfmembgroup.membgroup_code) )
                and  ( mbmembmaster.prename_code = mbucfprename.prename_code ) 
                and  ( mbmembmaster.coop_id = mbucfmembtype.coop_id )
                and  ( mbmembmaster.membtype_code = mbucfmembtype.membtype_code ) 
                and  ( mbmembmaster.coop_id = shsharemaster.coop_id ) 
                and  ( mbmembmaster.member_no = shsharemaster.member_no ) 
                and  ( ( mbmembmaster.coop_id   = {0} )
                and  ( mbmembmaster.member_no = {1}) 
                and  ( shsharemaster.sharetype_code = '01' ) )";*/

                    @"select mbmembmaster.coop_id,
                        mbmembmaster.member_no,
		                mbmembmaster.membgroup_code,
		                mbucfmembgroup.membgroup_desc , 

		                isnull(mbmembmaster.birth_date,convert(datetime,'1900-01-01')) as birth_date,
		                isnull(mbmembmaster.member_date,convert(datetime,'1900-01-01')) as member_date,   
		                isnull(mbmembmaster.work_date,convert(datetime,'1900-01-01')) as work_date,   
		                isnull(mbmembmaster.retry_date,convert(datetime,'1900-01-01')) as retry_date,
					    CONVERT(VARCHAR, (round( DATEDIFF(month, birth_date,getdate()) - ( DATEDIFF(month, birth_date,getdate()) % 12 ) , 0 )/12) ) 
						+ '.'+
						CONVERT(VARCHAR, round( ( DATEDIFF(month, birth_date,getdate()) % 12 ), 0 )) as birth_age,
						
                        CONVERT(VARCHAR, (round( DATEDIFF(month, work_date,getdate()) - ( DATEDIFF(month, work_date,getdate()) % 12 ) , 0 )/12) ) 
						+ '.'+
						CONVERT(VARCHAR, round( ( DATEDIFF(month, work_date,getdate()) % 12 ), 0 )) as work_age, 
						
                        CONVERT(VARCHAR, (round( DATEDIFF(month,getdate(), retry_date) - ( DATEDIFF(month, retry_date,getdate()) % 12 ) , 0 )/12) )*12  as retry_age_1, 
		                CONVERT(VARCHAR, round( ( DATEDIFF(month, getdate(),retry_date) % 12 ), 0 )) as retry_age_2 ,
						
                        CONVERT(VARCHAR, (round( DATEDIFF(month, member_date,getdate()) - ( DATEDIFF(month, member_date,getdate()) % 12 ) , 0 )/12) )*12 as member_age_1,
						CONVERT(VARCHAR, round( ( DATEDIFF(month, member_date,getdate()) % 12 ), 0 )) as member_age_2,
		                mbmembmaster.salary_amount,   		                  
		                mbmembmaster.membtype_code,   
		                mbucfmembtype.membtype_desc,   
		                shsharemaster.last_period,   
		                shsharemaster.sharestk_amt,   
		                shsharemaster.periodshare_amt,   
		                mbucfprename.prename_desc + mbmembmaster.memb_name + '   ' + mbmembmaster.memb_surname as member_name,   
		                shsharemaster.payment_status,      
		                mbmembmaster.remark,   		                		                 		                		                
		                mbmembmaster.rememb_flag,
                        mbmembmaster.retry_status,
                        mbmembmaster.incomeetc_amt
                from mbmembmaster,   
		                mbucfmembgroup,
		                mbucfprename,
		                mbucfmembtype,
		                shsharemaster  
                where ( mbmembmaster.coop_id = mbucfmembgroup.coop_id ) 
                and  ltrim( rtrim(mbmembmaster.membgroup_code)) =ltrim( rtrim(mbucfmembgroup.membgroup_code) )
                and  ( mbmembmaster.prename_code = mbucfprename.prename_code ) 
                and  ( mbmembmaster.coop_id = mbucfmembtype.coop_id )
                and  ( mbmembmaster.membtype_code = mbucfmembtype.membtype_code ) 
                and  ( mbmembmaster.coop_id = shsharemaster.coop_id ) 
                and  ( mbmembmaster.member_no = shsharemaster.member_no ) 
 			  and  ( ( mbmembmaster.coop_id   = {0} )
                and  ( mbmembmaster.member_no = {1}) 
                and  ( shsharemaster.sharetype_code = '01' ) )";

                sqlstr = WebUtil.SQLFormat(sqlstr, state.SsCoopControl, member_no, ldtm_reqdate);

                Sdt dt = WebUtil.QuerySdt(sqlstr);

                
                if (dt.Next())
                {

                    ldc_retryage = dt.GetDecimal("retry_age_1");  //ปี
                    ldc_retryage = ldc_retryage + dt.GetDecimal("retry_age_2"); //เดือน
                   
                  
                    if (ldc_retryage < 0)
                    {
                        ldc_retryage = 0;
                    }

                    birth_age = dt.GetDecimal("birth_age");
                    {
                    if (birth_age < 0)
                        birth_age = 0;
                    }
                    work_age = dt.GetDecimal("work_age");
                    if (work_age < 0)
                    {
                        work_age = 0;
                    }

                    member_age = dt.GetDecimal("member_age_1");  //ปี
                    member_age = member_age + dt.GetDecimal("member_age_2"); //เดือน
                    
                    if (member_age < 0)
                    {
                        member_age = 0;
                    }

                    ldc_sharestk = dt.GetDecimal("sharestk_amt");
                    ldc_periodshramt = dt.GetDecimal("periodshare_amt");
                    ls_membtype = dt.GetString("membtype_code");
                    ls_membtypedesc = dt.GetString("membtype_desc");
                    ldc_shrstkvalue = ldc_sharestk * 10;
                    ldc_periodshrvalue = ldc_periodshramt * 10;
                    if (dt.GetInt32("payment_status") != 1)
                    {
                        ldc_periodshrvalue = 0;
                    }
                    dw_main.SetItemString(1, "coop_id", dt.GetString("coop_id"));
                    dw_main.SetItemString(1, "member_name", dt.GetString("member_name"));
                    dw_main.SetItemString(1, "membgroup_code", dt.GetString("membgroup_code"));
                    dw_main.SetItemString(1, "membgroup_desc", dt.GetString("membgroup_desc"));
                    dw_main.SetItemDecimal(1, "salary_amt", dt.GetDecimal("salary_amount"));
                    dw_main.SetItemDecimal(1, "share_lastperiod", dt.GetInt32("last_period"));
                    dw_main.SetItemDateTime(1, "birth_date", dt.GetDate("birth_date"));
                    dw_main.SetItemDateTime(1, "member_date", dt.GetDate("member_date"));
                    dw_main.SetItemDateTime(1, "work_date", dt.GetDate("work_date"));
                    dw_main.SetItemDateTime(1, "retry_date", dt.GetDate("retry_date"));
                    dw_main.SetItemDecimal(1, "retry_status", dt.GetInt32("retry_status"));
                    dw_main.SetItemDecimal(1, "birth_age", dt.GetDecimal("birth_age"));
                    dw_main.SetItemDecimal(1, "member_age", member_age);
                    dw_main.SetItemDecimal(1, "work_age", dt.GetDecimal("work_age"));
                    dw_main.SetItemDecimal(1, "retry_age", ldc_retryage);
                    //tomy ตรวจสอบเงินวิทยะฐานะ เกิน 57 ปี 30/04/2018
                    if (dt.GetDecimal("birth_age") >= 57)
                    {
                        ck_incomeetc = 0;
                    }
                    else
                    {
                        ck_incomeetc = dt.GetDecimal("incomeetc_amt");
                    }
                    dw_main.SetItemDecimal(1, "incomemonth_other", ck_incomeetc);
                    dw_main.SetItemDecimal(1, "paymonth_other", 0);
                    dw_main.SetItemString(1, "member_remark", dt.GetString("remark"));
                    dw_main.SetItemString(1, "membtype_code", ls_membtype);
                    dw_main.SetItemDecimal(1, "share_balance", ldc_shrstkvalue);
                    dw_main.SetItemDecimal(1, "share_periodvalue", ldc_periodshrvalue);
                    dw_main.SetItemDecimal(1, "intestimate_amt", 0);
                    dw_main.SetItemDecimal(1, "rememb_flag", dt.GetInt32("rememb_flag"));
                }

                //set เงินหักเงินเพิ่ม
                sqlstr = @"select isnull(sum( case when sign_flag = 1 then mthexpense_amt else 0 end ), 0) as incomemonth_fixed , 
	                isnull(sum( case when sign_flag = -1 then mthexpense_amt else 0 end ), 0) as paymonth_exp
                from mbmembmthexpense 
                where coop_id = {0}
                and member_no = {1}";
                sqlstr = WebUtil.SQLFormat(sqlstr, state.SsCoopControl, member_no);

                dt = WebUtil.QuerySdt(sqlstr);
                if (dt.Next())
                {
                    decimal paymonth_exp = dt.GetDecimal("paymonth_exp");
                    dw_main.SetItemDecimal(1, "incomemonth_fixed", dt.GetDecimal("incomemonth_fixed"));
                    dw_main.SetItemDecimal(1, "paymonth_exp", paymonth_exp);
                    Hdpaymouthexp.Value = Convert.ToString(paymonth_exp);
                }

                string ls_expcode = dw_main.GetItemString(1, "expense_code");

                if (ls_expcode != "CSH")
                {
                    of_initexpense(member_no);
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("of_setmemberinfo" + ex);
            }
        }

        /// <summary>
        /// init ประเภทการจ่ายเงิน
        /// </summary>
        private void of_initexpense(string as_membno)
        {
            try
            {
                string strsql = @"select expense_code, expense_bank, expense_branch, expense_accid 
                                        from mbmembmaster where member_no = '" + as_membno + "'";
                Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                if (dtloanrcv.GetRowCount() <= 0)
                {

                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + as_membno);
                }
                if (dtloanrcv.Next())
                {
                    string loanrcv_code = "", loanrcv_bank = "";

                    try { loanrcv_code = dw_main.GetItemString(1, "expense_code"); }
                    catch { loanrcv_code = ""; }
                    if (loanrcv_code == "" || loanrcv_code == null) { loanrcv_code = dtloanrcv.GetString("expense_code"); }

                    try { loanrcv_bank = dw_main.GetItemString(1, "expense_bank"); }
                    catch { loanrcv_bank = ""; }
                    if (loanrcv_bank == "" || loanrcv_bank == null) { loanrcv_bank = dtloanrcv.GetString("expense_bank"); }

                    string loanrcv_branch = dtloanrcv.GetString("expense_branch");
                    string loanrcv_accid = dtloanrcv.GetString("expense_accid");

                    if (loanrcv_code != null)
                    {
                        dw_main.SetItemString(1, "expense_code", loanrcv_code);
                        dw_main.SetItemString(1, "expense_bank", loanrcv_bank);
                        if (loanrcv_branch == "" || loanrcv_branch == null)
                        {
                            DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment.pbl", loanrcv_bank);
                        }
                        else
                        {
                            dw_main.SetItemString(1, "expense_branch", loanrcv_branch);
                        }
                        dw_main.SetItemString(1, "expense_accid", loanrcv_accid);

                        // DwUtil.RetrieveDDDW(dw_main, "expense_branch", "sl_loan_requestment.pbl", null);
                    }
                    else
                    {
                        dw_main.SetItemString(1, "expense_code", "CBT");
                    }

                    if (loanrcv_code == "CBT" && loanrcv_bank.Length > 2)
                    {
                        string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                        Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                        string bankbranch = "";
                        if (dtk.Next())
                        {
                            bankbranch = dtk.GetString("branch_name").Trim();
                            JsExpensebankbrRetrieve();
                        }
                    }
                    if (loanrcv_code == "CBO" && loanrcv_bank.Length > 2)
                    {
                        string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                        Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                        string bankbranch = "";
                        if (dtk.Next())
                        {
                            bankbranch = dtk.GetString("branch_name").Trim();
                            JsExpensebankbrRetrieve();
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        ///  set ข้อมูลประเภทเงินกู้
        /// </summary>
        private void of_setloantypeinfo(string loantype_code)
        {
            try
            {
                string ls_salarycode = "", ls_intratetab = "", ls_intfixtab, ls_lnpermgrp;
                int li_apvflag = 0, li_salarychk = 0, li_chkdept, li_inttype = 0, li_intsteptype = 0;
                int li_intcertstatus, li_intcertlntype = 0, li_lnpaycount = 0, li_defaultpay = 0;
                decimal ldc_minsalamt, ldc_minsalinc = 0, ldc_salamt, ldc_maxloan;
                decimal ldc_minsalperc, ldc_intincrease = 0, ldc_intfixrate = 0, ldc_idenintrate = 0;
                DateTime ldtm_reqdate;
                string sqlStr;
                ii_apvtype = 0;

                ldtm_reqdate = dw_main.GetItemDateTime(1, "loanrequest_date");

                sqlStr =/* @"select lnt.defaultobj_code,  
                    lnt.salarybal_status,
                    lnt.salarybal_code, 
                    lnt.loanright_type, 
                    lnt.loanpayment_type, 
                    lnt.contract_time, 
                    lnt.od_flag, 
                    lnt.intstep_type,  
                    lnt.intcertificate_status,
                    lnt.approve_flag,  
                    dbo.ft_getlntypintrate({0}, {1},1,{2},1) as idenintrate,  
                    lnt.contint_type, 
                    lnt.inttabrate_code,
                    lnt.inttabfix_code,
                    lnt.intrate_increase,
                    lnt.loanpayment_type,
                    lnt.loanpayment_status,
                    lnt.reqround_factor,
                    lnt.payround_factor,
                    lnt.inttabfix_code,  
                    lnt.lngrpcutright_flag, 
                    lnt.inttabrate_code, 
                    lnt.resign_timeadd,
                    lnt.retryloansend_time,
                    lnt.retryloansend_type,
                    lnt.loanpayment_count,
                    lnt.loanpermgrp_code,
                    lnc.fixpaycal_type,
                    lnt.maxloan_amt,
                    lnt.defaultpay_type
                from lnloantype lnt,
                    lnloanconstant lnc  
                where lnt.coop_id = lnc.coop_id
                and lnt.coop_id = {0}
                and lnt.loantype_code = {1}";*/

                     @"select lnt.defaultobj_code,  
                    lnt.salarybal_status,
                    lnt.salarybal_code, 
                    lnt.loanright_type, 
                    lnt.loanpayment_type, 
                    lnt.contract_time, 
                    lnt.od_flag, 
                    lnt.intstep_type,  
                    lnt.intcertificate_status,
                    lnt.approve_flag,  

(case when lnt.loantype_code = 10 then '6.50' when  lnt.loantype_code = 20 then '6.50'  
 when lnt.loantype_code = 21 then '6.50' when  lnt.loantype_code = 22 then '5.75 '  
 when lnt.loantype_code = 23 then '6.50' when  lnt.loantype_code = 24 then '6.50'
 when lnt.loantype_code = 34 then '6.00' when  lnt.loantype_code = 34 then '6.00'
 when lnt.loantype_code = 25 then '6.38' when  lnt.loantype_code = 26 then '6.50'  
 when lnt.loantype_code = 27 then '6.50' when  lnt.loantype_code = 28 then '5.75'  
 when lnt.loantype_code = 29 then '0.10' when  lnt.loantype_code = 30 then '6.50'  
 when lnt.loantype_code = 31 then '6.50' when  lnt.loantype_code = 40 then '6.50'  
 when lnt.loantype_code = 50 then '6.38' when  lnt.loantype_code = 60 then '6.50'  
 when lnt.loantype_code = 70 then '6.50' when  lnt.loantype_code = 80 then '6.50'  
 else  ' ' end )  as idenintrate,  

                    lnt.contint_type, 
                    lnt.inttabrate_code,
                    lnt.inttabfix_code,
                    lnt.intrate_increase,
                    lnt.loanpayment_type,
                    lnt.loanpayment_status,
                    lnt.reqround_factor,
                    lnt.payround_factor,
                    lnt.inttabfix_code,  
                    lnt.lngrpcutright_flag, 
                    lnt.inttabrate_code, 
                    lnt.resign_timeadd,
                    lnt.retryloansend_time,
                    lnt.retryloansend_type,
                    lnt.loanpayment_count,
                    lnt.loanpermgrp_code,
                    lnc.fixpaycal_type,
                    lnt.maxloan_amt,
                    lnt.defaultpay_type
                from lnloantype lnt,
                    lnloanconstant lnc  
                where lnt.coop_id = lnc.coop_id
                and lnt.coop_id = {0}
                and lnt.loantype_code = {1}";

                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, loantype_code, ldtm_reqdate);
                Sdt dtlntype = WebUtil.QuerySdt(sqlStr);

                if (dtlntype.Next())
                {
                    li_salarychk = dtlntype.GetInt32("salarybal_status");
                    ls_salarycode = dtlntype.GetString("salarybal_code");
                    li_intcertlntype = dtlntype.GetInt32("intcertificate_status");
                    li_apvflag = dtlntype.GetInt32("approve_flag");
                    //try
                    //{
                    //    ldc_idenintrate = dtlntype.GetDecimal("idenintrate");
                    //}
                    //catch { ldc_idenintrate = 0; }
                    li_inttype = dtlntype.GetInt32("contint_type");
                    ls_intratetab = dtlntype.GetString("inttabrate_code");

                    if (loantype_code == "34") {
                        ls_intratetab = "INT34";
                    } 

                    ls_intfixtab = dtlntype.GetString("inttabfix_code");
                    //tomy ดึงเรดดอกเบี้ย จากตาราง
                     string sqlStrint = @"select * from lncfloanintrate ,lncfloanintratedet
                                                where lncfloanintrate.loanintrate_code = lncfloanintratedet.loanintrate_code
                                                and  lncfloanintrate.coop_id = {0}
                                                and  lncfloanintrate.loanintrate_code = {1}
                                                and {2} between effective_date and expire_date;";
                    sqlStrint = WebUtil.SQLFormat(sqlStrint, state.SsCoopControl, ls_intratetab, ldtm_reqdate);
                    Sdt dtint = WebUtil.QuerySdt(sqlStrint);

                    if (dtint.Next())
                    {
                        ldc_idenintrate = dtint.GetDecimal("interest_rate");
                    }
                    else
                    { ldc_idenintrate = 0; }

                    ldc_intincrease = dtlntype.GetDecimal("intrate_increase");
                    li_lnpaycount = dtlntype.GetInt32("loanpayment_count");
                    ldc_maxloan = dtlntype.GetDecimal("maxloan_amt");
                    li_defaultpay = dtlntype.GetInt32("defaultpay_type");

                    // กำหนดรายละเอียดทั่วไปลงใบขอกู้
                    dw_main.SetItemDecimal(1, "loanright_type", dtlntype.GetInt32("loanright_type"));
                    dw_main.SetItemDecimal(1, "loanpayment_type", dtlntype.GetInt32("loanpayment_type"));
                    dw_main.SetItemDecimal(1, "contract_time", dtlntype.GetInt32("contract_time"));
                    dw_main.SetItemString(1, "loanobjective_code", dtlntype.GetString("defaultobj_code"));
                    dw_main.SetItemDecimal(1, "od_flag", dtlntype.GetInt32("od_flag"));
                    dw_main.SetItemDecimal(1, "int_intsteptype", dtlntype.GetInt32("intstep_type"));
                    dw_main.SetItemDecimal(1, "retryloansend_type", dtlntype.GetInt32("retryloansend_type"));
                    dw_main.SetItemDecimal(1, "lnoverretry_period", dtlntype.GetInt32("retryloansend_time"));
                    dw_main.SetItemDecimal(1, "reqround_factor", dtlntype.GetDecimal("reqround_factor"));
                    dw_main.SetItemDecimal(1, "payround_factor", dtlntype.GetDecimal("payround_factor"));
                    dw_main.SetItemDecimal(1, "fixpaycal_type", dtlntype.GetInt32("fixpaycal_type"));
                    dw_main.SetItemString(1, "loanpermgrp_code", dtlntype.GetString("loanpermgrp_code"));

                    //dw_main.SetItemDecimal(1, "intcertlntype_status", li_intcertlntype);
                    //dw_main.SetItemDecimal(1, "intcertificate_status", li_intcertstatus);
                    ls_lnpermgrp = dtlntype.GetString("loanpermgrp_code");
                    if (ls_lnpermgrp != null && ls_lnpermgrp != "")
                    {
                        sqlStr = @"select maxpermiss_amt from lngrploanpermiss 
                            where coop_id = {0}
                            and loanpermgrp_code = {1}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_lnpermgrp);
                        Sdt dt = WebUtil.QuerySdt(sqlStr);

                        if (dt.Next())
                        {
                            dw_main.SetItemDecimal(1, "loangrpcredit_amt", dt.GetDecimal("maxpermiss_amt"));
                        }
                        else
                        {
                            dw_main.SetItemDecimal(1, "loangrpcredit_amt", 999999999999);
                        }
                    }
                    else
                    {
                        dw_main.SetItemDecimal(1, "loangrpcredit_amt", 999999999999);
                    }

                    if (li_defaultpay == 1)
                    {
                        dw_main.SetItemString(1, "expense_code", "CSH");
                        //tomy set null เมื่อ เป็นเงินสด 
                        dw_main.SetItemString(1, "expense_bank", "");
                        dw_main.SetItemString(1, "expense_branch", "");
                        dw_main.SetItemString(1, "expense_accid", "");
                    }
                    else if (li_defaultpay == 3)
                    {
                        dw_main.SetItemString(1, "expense_code", "TRN");
                        dw_main.SetItemString(1, "expense_bank", "");
                        dw_main.SetItemString(1, "expense_branch", "");
                        dw_main.SetItemString(1, "expense_accid", "");
                    }
                    else

                    {
                        dw_main.SetItemString(1, "expense_code", "CBT");
                    }
                }

                ldc_minsalperc = 0;
                ldc_minsalamt = 0;

                // เงินเดือนคงเหลือขั้นต่ำ
                if (li_salarychk == 1)
                {
                    sqlStr = @"select salarybal_amt, 
                        salarybal_percent, 
                        chkdept_flag, 
                        increment_amt
	                from cmucfsalarybalance
	                where ( salarybal_code	= '" + ls_salarycode + "' )";
                    Sdt dt1 = WebUtil.QuerySdt(sqlStr);

                    if (dt1.Next())
                    {
                        ldc_minsalamt = dt1.GetInt32("salarybal_amt");
                        ldc_minsalperc = dt1.GetDecimal("salarybal_percent");
                        li_chkdept = dt1.GetInt32("chkdept_flag");

                        if (ldc_minsalperc > 0)
                        {
                            ldc_salamt = dw_main.GetItemDecimal(1, "salary_amt");
                            ldc_minsalperc = ldc_salamt * (ldc_minsalperc / 100);
                            if (ldc_minsalperc > ldc_minsalamt)
                            {
                                ldc_minsalamt = ldc_minsalperc;
                            }
                        }
                        dw_main.SetItemDecimal(1, "minsalary_perc", 30);
                        dw_main.SetItemDecimal(1, "minsalary_amt", ldc_minsalamt);
                    }
                }

                if (li_intcertlntype == 1)
                {
                    li_intcertstatus = 1;
                }
                else
                {
                    li_intcertstatus = 0;
                }

                //กำหนดรูปแบบการชำระ
                if (li_lnpaycount == 0)
                {
                    dw_main.SetItemString(1, "loanpay_code", "KOT");
                }
                else
                {
                    dw_main.SetItemString(1, "loanpay_code", "KEP");
                }

                // การให้อนุมัติเลย
                if (li_apvflag == 1)
                {
                    if (ii_apvtype == 0)
                    {
                        ii_apvtype = 1;
                    }
                    dw_main.SetItemDecimal(1, "apvimmediate_status", ii_apvtype);
                }
                else
                {
                    ii_apvtype = 0;
                    dw_main.SetItemDecimal(1, "apvimmediate_status", 0);
                }

                // อัตรา ด/บ ที่ใช้ในสัญญา
                dw_main.SetItemDecimal(1, "identifycont_intrate", ldc_idenintrate);

                switch (li_inttype)
                {
                    case 0:
                        // ไม่คิดด/บ
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", 0);
                        dw_main.SetItemString(1, "int_continttabcode", "");
                        dw_main.SetItemDecimal(1, "int_contintincrease", 0);
                        break;
                    case 1:
                        // อัตรา ด/บ คงที่
                        //ldc_intfixrate	= inv_intsrv.of_getloanintrate( ls_intfixtab, ldtm_reqdate )		
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", ldc_intfixrate);
                        dw_main.SetItemString(1, "int_continttabcode", "");
                        dw_main.SetItemDecimal(1, "int_contintincrease", 0);
                        break;
                    case 2:
                        // อัตรา ด/บ ตามประกาศ
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", 0);
                        dw_main.SetItemString(1, "int_continttabcode", ls_intratetab);
                        dw_main.SetItemDecimal(1, "int_contintincrease", ldc_intincrease);
                        break;
                    case 3:
                        // อัตรา ด/บ พิเศษเป็นช่วง
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", 0);
                        dw_main.SetItemString(1, "int_continttabcode", "");
                        dw_main.SetItemDecimal(1, "int_contintincrease", 0);

                        sqlStr = @"select coop_id, seq_no,   
	                        inttime_type, inttime_amt,   
	                        intrate_type, intratetab_code,   
	                        intratefix_rate, intrate_increase,   
	                        inttabrate_code  
                        from lnloantypeintspc
                        where coop_id = {0}
                        and loantype_code = {1}";

                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, loantype_code);
                        Sdt dt3 = WebUtil.QuerySdt(sqlStr);

                        int i = 0;
                        while (dt3.Next())
                        {
                            i = dw_intspc.InsertRow(0);
                            dw_intspc.SetItemString(i, "coop_id", state.SsCoopControl);
                            dw_intspc.SetItemDecimal(i, "seq_no", i);
                            dw_intspc.SetItemDecimal(i, "int_continttype", dt3.GetInt32("intrate_type"));
                            dw_intspc.SetItemDecimal(i, "int_contintrate", dt3.GetDecimal("intratefix_rate"));
                            dw_intspc.SetItemString(i, "int_continttabcode", dt3.GetString("intratetab_code"));
                            dw_intspc.SetItemDecimal(i, "int_contintincrease", dt3.GetDecimal("intrate_increase"));
                            dw_intspc.SetItemDecimal(i, "int_timetype", dt3.GetInt32("inttime_type"));
                            dw_intspc.SetItemDecimal(i, "int_timeamt", dt3.GetInt32("inttime_amt"));
                            i++;
                        }
                        break;
                    case 4:
                        // อัตรา ด/บ ตามประกาศของเงินฝาก
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", 0);
                        dw_main.SetItemString(1, "int_continttabcode", ls_intratetab);
                        dw_main.SetItemDecimal(1, "int_contintincrease", ldc_intincrease);
                        break;
                    case 5:
                        // อัตรา ด/บ ตามเงินฝากที่มาใช้กู้
                        dw_main.SetItemDecimal(1, "int_continttype", li_inttype);
                        dw_main.SetItemDecimal(1, "int_contintrate", 0);
                        dw_main.SetItemString(1, "int_continttabcode", "");
                        dw_main.SetItemDecimal(1, "int_contintincrease", ldc_intincrease);
                        break;
                };


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("of_setloantypeinfo" + ex);
            }
        }

        /// <summary>
        ///  set ข้อมูลหักกลบ
        /// </summary>
        private void of_initloanclr(string member_no)
        {
            wcf.NShrlon.of_genintestimate(state.SsWsPass, member_no, dw_main.GetItemDateTime(1, "loanrequest_date"));
            string ls_loantype, ls_lntypeclr;
            int li_lastperiod, li_minperiod, li_finetype;
            int ll_row = 0;
            decimal ldc_apvamt, ldc_wtdamt, ldc_prnbal, ldc_rkeepprn;
            decimal ldc_intrate, ldc_intestim, ldc_payperc;
            decimal ldc_minpercent, ldc_fineamt, ldc_fineperc, ldc_finemax, ldc_finemin;
            Boolean lb_period = true, lb_payment = true;
            string sqlStr, ls_clr = "";
            try
            {
                sqlStr =/* @"select lm.coop_id, lm.loancontract_no, lm.loantype_code, lt.loanpermgrp_code ,
			        lm.loanapprove_amt, lm.withdrawable_amt, lm.principal_balance,
			        lm.loanpayment_type, lm.period_payment, lm.last_periodpay,lm.intestnow_amt,
			        nvl( lm.lastcalint_date, to_date('19000101','yyyymmdd') ) as lastcalint_date, lm.interest_arrear,
			        nvl( lm.lastprocess_date, to_date('19000101','yyyymmdd') ) as lastprocess_date, lm.rkeep_principal, 
                    lm.rkeep_interest, lm.contract_status, lm.transfer_status, lm.loanpay_code,
			        ft_getcontintrate( lm.coop_id, lm.loancontract_no, lm.lastcalint_date ) as contint_rate,
			        lt.loanpermgrp_code, lt.shrstkcount_flag , nvl( lm.startcont_date, to_date('19000101','yyyymmdd') ) as startcont_date,
                    nvl( lm.loanapprove_date, to_date('19000101','yyyymmdd') ) as loanapprove_date
                from lncontmaster lm, lnloantype lt
                where	( lm.coop_id		= lt.coop_id )
                and		( lm.loantype_code	= lt.loantype_code )
                and		( lm.memcoop_id		= {0} )
                and		( lm.member_no		= {1} )
                and		( lm.contract_status > 0 )
                order by lm.loantype_code";*/

                    @"select lm.coop_id, lm.loancontract_no, lm.loantype_code, lt.loanpermgrp_code ,
			        lm.loanapprove_amt, lm.withdrawable_amt, lm.principal_balance,
			        lm.loanpayment_type, lm.period_payment, lm.last_periodpay,lm.intestnow_amt,

			        isnull( lm.lastcalint_date, convert(datetime,'1900-01-01') ) as lastcalint_date, lm.interest_arrear,
			        isnull( lm.lastprocess_date, convert(datetime,'1900-01-01')  ) as lastprocess_date, lm.rkeep_principal, 

                    lm.rkeep_interest, lm.contract_status, lm.transfer_status, lm.loanpay_code,

(case when lt.loantype_code = 10 then '6.50' when  lt.loantype_code = 20 then '6.50'  
 when lt.loantype_code = 21 then '6.50' when  lt.loantype_code = 22 then '5.75 '  
 when lt.loantype_code = 23 then '6.50' when  lt.loantype_code = 24 then '6.50'  
 when lt.loantype_code = 25 then '6.38' when  lt.loantype_code = 26 then '6.50'  
 when lt.loantype_code = 27 then '6.50' when  lt.loantype_code = 28 then '5.75'  
 when lt.loantype_code = 29 then '0.10' when  lt.loantype_code = 30 then '6.50'  
when lt.loantype_code =  34 then '6.00' when  lt.loantype_code = 34 then '6.00'  
 when lt.loantype_code = 31 then '6.50' when  lt.loantype_code = 40 then '6.50'  
 when lt.loantype_code = 50 then '6.38' when  lt.loantype_code = 60 then '6.50'  
 when lt.loantype_code = 70 then '6.50' when  lt.loantype_code = 80 then '6.50'  
 else  ' ' end )  as contint_rate,

			        lt.loanpermgrp_code, lt.shrstkcount_flag , 
                    isnull( lm.startcont_date,convert(datetime,'1900-01-01')  ) as startcont_date,
                    isnull( lm.loanapprove_date, convert(datetime,'1900-01-01')  ) as loanapprove_date

                from lncontmaster lm, lnloantype lt
                where	( lm.coop_id		= lt.coop_id )
                and		( lm.loantype_code	= lt.loantype_code )
                and		( lm.memcoop_id		= {0} )
                and		( lm.member_no		= {1})
                and		( lm.contract_status > 0 )
                order by lm.loantype_code";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no);

                Sdt dt = WebUtil.QuerySdt(sqlStr);
                while (dt.Next())
                {
                    ll_row = dw_clear.InsertRow(0);

                    ldc_prnbal = dt.GetDecimal("principal_balance");
                    ldc_intrate = dt.GetDecimal("contint_rate");

                    dw_clear.SetItemString(ll_row, "loanpermgrp_code", dt.GetString("loanpermgrp_code"));
                    dw_clear.SetItemDecimal(ll_row, "shrstkcount_flag", dt.GetInt32("shrstkcount_flag"));

                    dw_clear.SetItemString(ll_row, "concoop_id", dt.GetString("coop_id"));
                    dw_clear.SetItemString(ll_row, "loancontract_no", dt.GetString("loancontract_no"));
                    dw_clear.SetItemString(ll_row, "loantype_code", dt.GetString("loantype_code"));

                    dw_clear.SetItemDecimal(ll_row, "loanapprove_amt", dt.GetDecimal("loanapprove_amt"));
                    //dw_clear.SetItemDecimal(ll_row, "loanapprove_date", dt.GetDecimal("loanapprove_date")); 
                    dw_clear.SetItemDateTime(ll_row, "startcont_date", dt.GetDate("startcont_date"));
                    dw_clear.SetItemDecimal(ll_row, "principal_balance", ldc_prnbal);
                    dw_clear.SetItemDecimal(ll_row, "withdrawable_amt", dt.GetDecimal("withdrawable_amt"));

                    dw_clear.SetItemDecimal(ll_row, "loanpayment_type", dt.GetInt32("loanpayment_type"));
                    dw_clear.SetItemDecimal(ll_row, "period_payment", dt.GetDecimal("period_payment"));
                    dw_clear.SetItemDecimal(ll_row, "last_periodpay", dt.GetInt32("last_periodpay"));
                    dw_clear.SetItemString(ll_row, "loanpay_code", dt.GetString("loanpay_code"));

                    dw_clear.SetItemDecimal(ll_row, "intestnow_amt", dt.GetDecimal("intestnow_amt"));
                    //dw_clear.SetItemDecimal(ll_row, "interest_accum", dt.GetDecimal("interest_accum"));
                    dw_clear.SetItemDateTime(ll_row, "lastcalint_date", dt.GetDate("lastcalint_date"));
                    dw_clear.SetItemDecimal(ll_row, "intarrear_amt", dt.GetDecimal("intarrear_amt"));
                    dw_clear.SetItemDecimal(ll_row, "intestimate_rate", ldc_intrate);

                    //dw_clear.SetItemDateTime(ll_row, "lastprocess_date", dt.GetDate("lastprocess_date"));
                    dw_clear.SetItemDecimal(ll_row, "rkeep_principal", dt.GetDecimal("rkeep_principal"));
                    dw_clear.SetItemDecimal(ll_row, "rkeep_interest", dt.GetDecimal("rkeep_interest"));

                    dw_clear.SetItemDecimal(ll_row, "contract_status", dt.GetInt32("contract_status"));
                    dw_clear.SetItemDecimal(ll_row, "clear_amount", 0);
                    dw_clear.SetItemDecimal(ll_row, "fine_amt", 0);

                    dw_clear.SetItemDecimal(ll_row, "transfer_status", dt.GetInt32("transfer_status"));
                    // ด/บ ประมาณการ 30 วัน สำหรับประมาณการชำระ / เดือน
                    if (ldc_prnbal > 0)
                    {
                        ldc_intestim = Convert.ToDecimal(Convert.ToDouble(ldc_prnbal) * (Convert.ToDouble(ldc_intrate) / 100.00) * (30.00 / 365.00));
                    }
                    else
                    {
                        ldc_intestim = 0;
                    }
                    dw_clear.SetItemDecimal(ll_row, "intestnow_amt", ldc_intestim);

                }

                sqlStr = @"select		loantype_clear, minperiod_pay, minpercent_pay, finecond_type, fine_amt, fine_percent, fine_maxamt
                    from		lnloantypeclr
                    where	( coop_id		= {0} )
                    and		( loantype_code	= {1} )";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, dw_main.GetItemString(1, "loantype_code"));
                Sdt dt1 = WebUtil.QuerySdt(sqlStr);

                while (dt1.Next())
                {
                    ls_lntypeclr = dt1.GetString("loantype_clear");

                    for (int i = 1; i <= dw_clear.RowCount; i++)
                    {
                        ls_loantype = dw_clear.GetItemString(i, "loantype_code");

                        if (ls_lntypeclr == ls_loantype)
                        {
                            li_minperiod = dt1.GetInt32("minperiod_pay");
                            ldc_minpercent = dt1.GetDecimal("minpercent_pay");

                            ldc_apvamt = dw_clear.GetItemDecimal(i, "loanapprove_amt");
                            ldc_wtdamt = dw_clear.GetItemDecimal(i, "withdrawable_amt");
                            ldc_prnbal = dw_clear.GetItemDecimal(i, "principal_balance");
                            ldc_rkeepprn = dw_clear.GetItemDecimal(i, "rkeep_principal");

                            li_lastperiod = Convert.ToInt32(dw_clear.GetItemDecimal(i, "last_periodpay"));

                            // % การชำระ (คิดจากยอดรับเงินไปแล้ว เทียบกับยอดคงเหลือ)
                            ldc_payperc = (((ldc_apvamt - ldc_wtdamt) - ldc_prnbal) / (ldc_apvamt - ldc_wtdamt)) * 100;

                            // ตรวจสอบว่างวดชำระถึงที่กำหนดหรือเปล่า
                            if (li_lastperiod < li_minperiod || ldc_payperc < ldc_minpercent)
                            {
                                if (li_lastperiod < li_minperiod)
                                {
                                    lb_period = false;
                                }
                                if (ldc_payperc < ldc_minpercent)
                                {
                                    lb_payment = false;
                                }

                                //เตือนชำระไม่ถึงกำหนด
                                if (lb_period == false || lb_payment == false)
                                {
                                    string ls_contno = dw_clear.GetItemString(i, "loancontract_no");
                                    ls_clr += "\\nสัญญา " + ls_contno + " งวดชำระแล้ว = " + li_lastperiod.ToString() + ", %ชำระแล้ว = " + ldc_payperc.ToString("#,##0.00") + "%";
                                }

                                li_finetype = dt1.GetInt32("finecond_type");
                                ldc_fineperc = dt1.GetDecimal("fine_percent");
                                ldc_finemin = dt1.GetDecimal("fine_amt");
                                ldc_finemax = dt1.GetDecimal("fine_maxamt");

                                if ((li_finetype == 1 && (lb_period == false || lb_payment == false)) || (li_finetype == 2 && lb_period == false && lb_payment == false))
                                {
                                    // ส่วนของค่าปรับกรณีชำระไม่ครบตามที่กำหนด
                                    ldc_fineamt = ldc_prnbal * (ldc_fineperc / 100);

                                    if (ldc_fineamt < ldc_finemin) { ldc_fineamt = ldc_finemin; }
                                    if (ldc_fineamt > ldc_finemax) { ldc_fineamt = ldc_finemax; }

                                    dw_clear.SetItemDecimal(i, "fine_amt", ldc_fineamt);

                                    if (ldc_fineamt > 0)
                                    {
                                        ls_clr += "  ต้องคิดค่าบริการ เป็นจำนวนเงิน " + ldc_fineamt.ToString("#,##0.00");
                                    }
                                }
                            }

                            ldc_prnbal = ldc_prnbal - ldc_rkeepprn;


                            dw_clear.SetItemDecimal(i, "clear_status", 1);
                            dw_clear.SetItemDecimal(i, "clear_amount", ldc_prnbal);
                        }
                    }
                    if (ls_clr != "")
                    {
                        is_massalert = "\\nมีสัญญาเก่าชำระหนี้ไม่ถึงตามที่กำหนด " + ls_clr;
                    }
                }
                of_setclearintreal();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("of_initloanclr" + ex);
            }
        }

        private void of_setclearintreal()
        {
            string ls_contno = "";
            decimal ldc_clearint = 0, ldc_prncalint = 0;
            decimal ldc_rkeepint = 0, ldc_rkeepprn = 0;
            DateTime ldtm_calfrom, ldtm_calto;

            // ถ้ามีวันที่จ่ายเงินกู้ให้คิด ด/บ ถึงวันนั้น
            try
            {
                ldtm_calto = dw_main.GetItemDateTime(1, "loanrcvfix_date");
            }
            catch
            {
                ldtm_calto = state.SsWorkDate;
            }

            for (int i = 1; i <= dw_clear.RowCount; i++)
            {
                ldc_prncalint = dw_clear.GetItemDecimal(i, "principal_balance");
                ldc_rkeepint = dw_clear.GetItemDecimal(i, "rkeep_interest");
                ldc_rkeepprn = dw_clear.GetItemDecimal(i, "rkeep_principal");

                if (ldc_prncalint > 0 && ldc_rkeepint == 0 && ldc_rkeepprn == 0)
                {
                    ls_contno = dw_clear.GetItemString(i, "loancontract_no");
                    ldtm_calfrom = dw_clear.GetItemDateTime(i, "lastcalint_date");

                    ldc_prncalint = ldc_prncalint - ldc_rkeepprn;

                    ldc_clearint = wcf.NShrlon.of_computeinterest_single(state.SsWsPass, state.SsCoopControl, ls_contno, ldc_prncalint, ldtm_calfrom, ldtm_calto);
                }
                else
                {
                    ldc_clearint = 0;
                }
                dw_clear.SetItemDecimal(i, "intreal_amt", ldc_clearint);
            }
        }
        /// <summary>
        ///  set สิทธิ์กู้
        /// </summary>
        private void of_initloancredit(string as_loantype)
        {
            try
            {
                string ls_memno, ls_ccdtno;
                int li_mintime = 0, li_membtime, li_ccdtperiod;
                int li_righttype = 0, li_timefrom = 0, li_righttime = 0, li_resignaddtime = 0, li_remembflag;
                decimal ldc_salary, ldc_shrstkvalue, ldc_permissamt = 0;
                DateTime ldtm_membdate, ldtm_workdate, ldtm_expcont, ldtm_rqdate;
                string sqlStr;

                sqlStr = @"select loanright_type, customtime_type, member_time, resign_timeadd, maxloan_amt
                    from lnloantype
                    where	( coop_id		= {0} )
                    and		( loantype_code	= {1} )";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_loantype);
                Sdt dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    li_mintime = dt.GetInt32("member_time");
                    li_resignaddtime = dt.GetInt32("resign_timeadd");
                    li_righttype = dt.GetInt32("loanright_type");
                    li_timefrom = dt.GetInt32("customtime_type");
                }
                // ดึงค่าต่างๆ ที่ใช้ในการคำนวณสิทธิ
                ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");
                li_remembflag = Convert.ToInt32(dw_main.GetItemDecimal(1, "rememb_flag"));
                ldtm_membdate = dw_main.GetItemDateTime(1, "member_date");
                ldtm_workdate = dw_main.GetItemDateTime(1, "work_date");

                // ระยะเวลาการเป็นสมาชิก
                if (ldtm_membdate != null && ldtm_membdate.ToString("yyyy-mm-dd") != "1900-01-01")
                {
                    li_membtime = Convert.ToInt32(dw_main.GetItemDecimal(1, "member_age"));
                }
                else
                {
                    li_membtime = 0;
                }

                // ตรวจสอบว่าได้ตามเกณท์มั้ย
                //if (li_membtime < li_mintime)
                //{
                //    Ltjspopup.Text = WebUtil.ErrorMessage("อายุการเป็นสมาชิกไม่ถึงตามที่กำหนดจะกู้เงินได้ " + li_membtime + "/" + li_mintime);
                //}
                //else
                //{
                // ลาออกสมัครใหม่ ให้เพิ่มระยะเวลาด้วย
                if ((li_remembflag == 1 && li_resignaddtime > 0) && (li_membtime < (li_mintime + li_resignaddtime)))
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้เคยลาออกแล้ว อายุการเป็นสมาชิกไม่ถึงตามที่กำหนดจะกู้เงินได้ " + li_membtime + "/" + li_mintime + "(+" + li_resignaddtime + ")");
                }
                //}

                switch (li_righttype)
                {
                    case 1:// แบบดูจากหลักประกัน
                    case 4:// แบบดูจากเงินฝาก
                        ls_memno = dw_main.GetItemString(1, "member_no");
                        this.SetOnLoadedScript("Gcoop.OpenIFrame2Extend('760', '600', 'ws_dlg_rightcoll.aspx', '?memb_no=" + ls_memno + "&loan_type=" + as_loantype + "')");
                        ibl_step = false;
                        break;
                    case 2:// แบบกำหนดเอง
                        ldc_shrstkvalue = dw_main.GetItemDecimal(1, "share_balance");
                        switch (li_timefrom)
                        {
                            case 1:	// อายุการเป็นสมาชิก
                                li_righttime = Convert.ToInt32(dw_main.GetItemDecimal(1, "member_age"));
                                break;
                            case 2:	// อายุการทำงาน
                                li_righttime = Convert.ToInt32(dw_main.GetItemDecimal(1, "work_age"));
                                break;
                            case 3:	// งวดหุ้น
                                li_righttime = Convert.ToInt32(dw_main.GetItemDecimal(1, "share_lastperiod"));
                                break;
                        }
                        ldc_permissamt = this.of_callncredit_custom(as_loantype, li_righttime, ldc_shrstkvalue, ldc_salary);



                        break;
                    case 3:// แบบดูจากสัญญาหลัก
                        ls_memno = dw_main.GetItemString(1, "member_no");
                        ldtm_rqdate = dw_main.GetItemDateTime(1, "loanrequest_date");
                        sqlStr = @"select	contcredit_no, loancreditbal_amt, expirecont_date, maxperiod_payamt , loanrcv_code , loanrcv_bank , loanrcv_branch , loanrcv_accid 
                            from	lncontcredit
                            where	( coop_id		= {0} )
                            and		( member_no		= {1} )
                            and		( loantype_code	= {2} )
                            and		( contcredit_status	= 1 )
                            order by contcredit_no desc";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_memno, as_loantype);
                        Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                        if (dt1.Next())
                        {
                            ls_ccdtno = dt1.GetString("contcredit_no");
                            ldc_permissamt = dt1.GetDecimal("loancreditbal_amt");
                            ldtm_expcont = dt1.GetDate("expirecont_date");
                            li_ccdtperiod = dt1.GetInt32("maxperiod_payamt");
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลวงเงินกู้ในสัญญาหลักของสมาชิกทะเบียน " + ls_memno + " ประเภทเงินกู้ " + as_loantype + " กรุณาไปกำหนดวงเงินกู้ก่อน");
                            break;
                        }

                        if (ldtm_expcont < ldtm_rqdate)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("สัญญา " + ls_ccdtno + " หมดอายุสัญญาไปแล้ว วันที่ " + ldtm_expcont.ToString("dd/mm/") + ldtm_expcont.ToString("yyyy") + 543);
                        }
                        // เพราะดูจากสัญญาหลัก ถ้าไม่ error ให้ใส่ค่าให้เลย
                        dw_main.SetItemDecimal(1, "maxperiod_payamt", li_ccdtperiod);
                        dw_main.SetItemDecimal(1, "maxsend_payamt", li_ccdtperiod);
                        dw_main.SetItemString(1, "ref_contmastno", ls_ccdtno);
                        dw_main.SetItemString(1, "expense_code", dt1.GetString("loanrcv_code"));
                        dw_main.SetItemString(1, "expense_bank", dt1.GetString("loanrcv_bank"));
                        dw_main.SetItemString(1, "expense_branch", dt1.GetString("loanrcv_branch"));
                        dw_main.SetItemString(1, "expense_accid", dt1.GetString("loanrcv_accid"));
                        break;
                }
                if (ldc_permissamt > dt.GetInt32("maxloan_amt"))
                {
                    dw_main.SetItemDecimal(1, "loanreqregis_amt", dt.GetInt32("maxloan_amt"));
                    dw_main.SetItemDecimal(1, "loancredit_amt", dt.GetInt32("maxloan_amt"));
                    dw_main.SetItemDecimal(1, "loanrequest_amt", dt.GetInt32("maxloan_amt"));
                }
                else
                {
                    dw_main.SetItemDecimal(1, "loanreqregis_amt", ldc_permissamt);
                    dw_main.SetItemDecimal(1, "loancredit_amt", ldc_permissamt);
                    dw_main.SetItemDecimal(1, "loanrequest_amt", ldc_permissamt);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("of_callncredit" + ex);
            }
        }

        /// <summary>
        ///  คำนวณสิทธิ์กู้
        /// </summary>
        private decimal of_callncredit_custom(string as_lntypereq, int ai_righttime, decimal adc_sharestk, decimal adc_salary)
        {
            string sqlStr;
            decimal ldc_maxpermiss, ldc_permiss, ldc_abspermiss = 0;
            decimal ldc_mulsalary, ldc_mulshare;

            try
            {
                // สิทธิกู้สุทธิกำหนดให้ unlimit ไว้ก่อน
                ldc_abspermiss = 99999999999;

                sqlStr = @"select		multiple_share, multiple_salary, maxloan_amt
                from		lnloantypecustom
                where	( coop_id		= {0} )
                and		( loantype_code	= {1} )
                and		( {2} between startmember_time and endmember_time )
                and		( {3} between startshare_amt and endshare_amt )
                and		( {4} between startsalary_amt and endsalary_amt )";

                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_lntypereq, ai_righttime, adc_sharestk, adc_salary);
                Sdt dt = WebUtil.QuerySdt(sqlStr);

                while (dt.Next())
                {
                    // สิทธิกู้ขั้นนี้
                    ldc_mulshare = dt.GetDecimal("multiple_share");
                    ldc_mulsalary = dt.GetDecimal("multiple_salary");
                    ldc_maxpermiss = dt.GetDecimal("maxloan_amt");

                    if (state.SsCoopControl == "061001")
                    {
                        if (as_lntypereq == "10") 
                        {
                            ldc_abspermiss = ldc_maxpermiss;
                        }
                    }
                    else {
                        ldc_permiss = (ldc_mulsalary * adc_salary) + (ldc_mulshare * adc_sharestk);

                        if (ldc_permiss > ldc_maxpermiss)
                        {
                            ldc_permiss = ldc_maxpermiss;
                        }
                        // กรณีสิทธิกู้ขั้นนี้น้อยกว่าสิทธิกู้สุทธิ(เอาน้อย)
                        if (ldc_permiss < ldc_abspermiss)
                        {
                            ldc_abspermiss = ldc_permiss;
                            dw_main.SetItemDecimal(1, "loanright_percsalary", ldc_mulsalary);
                            dw_main.SetItemDecimal(1, "loanright_percshare", ldc_mulshare);
                        }
                    }
                    
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_callncredit_custom" + ex); }

            return ldc_abspermiss;
        }

        /// <summary>
        /// set ยอดขอกู้
        /// </summary>
        private void of_initpermiss()
        {
            try
            {
                decimal ldc_permissamt, ldc_grpuse, ldc_maxloan = 0, ldc_loancredit, ldc_grpcredit, ldc_loanregis, ldc_collperc, ldc_sharestk, ldc_temp;
                decimal ldc_loanreq, ldc_roundfacter = 0;
                string ls_permissgrp = "", ls_loantype;
                int li_kpsumflag = 0, li_lngrpcutright = 0, li_notmoreshare = 0, li_showright, li_roundtype = 0;
                string sqlStr;

                ls_loantype = dw_main.GetItemString(1, "loantype_code");
                ldc_loancredit = dw_main.GetItemDecimal(1, "loancredit_amt");
                ldc_grpcredit = dw_main.GetItemDecimal(1, "loangrpcredit_amt");
                ldc_loanregis = dw_main.GetItemDecimal(1, "loanreqregis_amt");

                ldc_permissamt = of_calmaxreqforsal();

                sqlStr = @"select		lngrpcutright_flag, notmoreshare_flag, maxloan_amt, loanpermgrp_code, lngrpkeepsum_flag, showright_flag,
                    reqround_factor, reqround_type
                from		lnloantype
                where	( coop_id		= {0} )
                and		( loantype_code	= {1} )";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype);
                Sdt dt = WebUtil.QuerySdt(sqlStr);

                if (dt.Next())
                {
                    li_lngrpcutright = dt.GetInt32("lngrpcutright_flag");
                    li_notmoreshare = dt.GetInt32("notmoreshare_flag");
                    ldc_maxloan = dt.GetDecimal("maxloan_amt");
                    ls_permissgrp = dt.GetString("loanpermgrp_code");
                    li_kpsumflag = dt.GetInt32("lngrpkeepsum_flag");
                    li_showright = dt.GetInt32("showright_flag");
                    li_roundtype = dt.GetInt32("reqround_type");
                    ldc_roundfacter = dt.GetDecimal("reqround_factor");
                }
                //lngrpcutright_flag		ขอกู้ได้ไม่เกินวงเงินกู้กลุ่ม
                //notmoreshare_flag		กู้ได้ไม่เกินหุ้นที่มี
                //maxloan_amt				วงเงินกู้สูงสุดของเงินกู้ประเภทนี้
                //loanpermgrp_code		เงินกู้ประเภทนี้อยู่ในกลุ่มเงินกู้รหัสอะไร
                //lngrpkeepsum_flag		การนับเงินกู้รวมใช้ไปหักยอดรอเรียกเก็บหรือเปล่า
                //showright_flag			แสดงสิทธิกู้ตอนคำนวณสิทธิ์เสร็จแล้วหรือเปล่า

                // วงเงินกู้รวมใช้ไป
                ldc_grpuse = of_calpermissloangrpused(ls_permissgrp, li_kpsumflag);

                // ตรวจว่าต้องตัดยอดขอกู้จากยอดกู้กลุ่มหรือไม่
                if (li_lngrpcutright == 1)
                {
                    if (ldc_permissamt > (ldc_loancredit - ldc_grpuse))
                    {
                        ldc_permissamt = (ldc_loancredit - ldc_grpuse);
                    }
                    if (ldc_permissamt < 0)
                    {
                        ldc_permissamt = 0;
                    }
                }
                // check กับยอด credit
                if (ldc_permissamt > (ldc_grpcredit - ldc_grpuse))
                {
                    ldc_permissamt = (ldc_grpcredit - ldc_grpuse);
                }
                if (ldc_permissamt > ldc_loancredit)
                {
                    ldc_permissamt = ldc_loancredit;
                }
                if (ldc_permissamt > ldc_maxloan)
                {
                    ldc_permissamt = ldc_maxloan;
                }

                // ตรวจว่าห้ามกู้เกินหุ้นหรือเปล่า
                if (li_notmoreshare == 1)
                {
                    sqlStr = @"select coll_percent
                    from lnloantypecolluse
                    where coop_id = {0}
                    and loantype_code = {1}
                    and loancolltype_code = '02'";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype);
                    Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                    if (dt1.Next())
                    {
                        ldc_collperc = dt1.GetDecimal("coll_percent");

                        ldc_sharestk = dw_main.GetItemDecimal(1, "share_balance");
                        ldc_temp = ldc_sharestk * (ldc_collperc / 100); //tomy หาร 100  19/04/2018

                        // ถ้ามากกว่าหุ้นที่ใช้ได้ตัดยอดลง
                        if (ldc_permissamt > ldc_temp)
                        {
                            ldc_permissamt = ldc_temp;
                        }
                    }
                }

                if (ldc_permissamt < 0)
                {
                    ldc_permissamt = 0;
                }

                dw_main.SetItemDecimal(1, "loanpermiss_amt", ldc_permissamt);

                // ค่าเงินที่จะขอกู้ได้
                if (ldc_permissamt > ldc_loanregis)
                {
                    ldc_loanreq = ldc_loanregis;
                }
                else
                {
                    ldc_loanreq = ldc_permissamt;
                }

                // ปัดยอดขอกู้

                if (ldc_roundfacter != 0)
                {
                    decimal ldc_tempfacter = Math.Abs(ldc_roundfacter);

                    if ((ldc_loanreq % ldc_roundfacter) > 0)
                    {
                        if (ldc_roundfacter > 0)
                        {
                            ldc_loanreq = ldc_loanreq - (ldc_loanreq % ldc_roundfacter) + ldc_roundfacter;
                        }
                        else
                        {
                            ldc_loanreq = ldc_loanreq - (ldc_loanreq % ldc_roundfacter);
                        }
                    }
                }
                dw_main.SetItemDecimal(1, "loanrequest_amt", ldc_loanreq);
                HdCondition.Value = "true";
                //pom - คิดงวดตามยอดขอกู้ 
                of_initmaxperiod(ls_loantype, ldc_loanreq);
                of_setintestimateloanreq();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initpermiss " + ex); }
        }

        /// <summary>
        /// set ดอกเบี้ยประมาณการ สำหรับคำนวณเงินเดือนคงเหลือ
        /// </summary>
        private void of_setintestimateloanreq()
        {
            decimal ldc_loanreq, ldc_intrate, ldc_intestimate;

            ldc_loanreq = dw_main.GetItemDecimal(1, "loanrequest_amt");
            ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate");
            ldc_intestimate = Convert.ToDecimal((Convert.ToDouble(ldc_loanreq) * (Convert.ToDouble(ldc_intrate) / 100.00) * (30.00 / 365.00)));
            dw_main.SetItemDecimal(1, "intestimate_amt", ldc_intestimate);


        }

        /// <summary>
        /// set ยอดหักจากใบคำขอ
        /// </summary>
        private void of_setpaymthlnreq()
        {
            string ls_loanpay;
            int li_lnpaytype;
            decimal ldc_intestimate, ldc_paymthlnreq = 0, ldc_reqpay;

            ls_loanpay = dw_main.GetItemString(1, "loanpay_code");
            li_lnpaytype = Convert.ToInt32(dw_main.GetItemDecimal(1, "loanpayment_type"));

            ldc_reqpay = dw_main.GetItemDecimal(1, "period_payment");
            ldc_intestimate = dw_main.GetItemDecimal(1, "intestimate_amt");

            if (ls_loanpay != "KEP")
            {
                ldc_paymthlnreq = 0;
            }
            else
            {
                if (li_lnpaytype == 0)
                {
                    ldc_paymthlnreq = 0;
                }
                else if (li_lnpaytype == 1)
                {
                    ldc_paymthlnreq = ldc_reqpay + ldc_intestimate;
                }
                else if (li_lnpaytype == 2)
                {
                    ldc_paymthlnreq = ldc_reqpay;
                }
                else if (li_lnpaytype == 3)
                {
                    ldc_paymthlnreq = ldc_intestimate;
                }
            }
            dw_main.SetItemDecimal(1, "paymonth_lnreq", ldc_paymthlnreq);

        }

        /// <summary>
        /// คำนวณยอดขอกู้จากการชำระต่องวด
        /// </summary>
        private decimal of_calnetlnpermiss(int ai_paymenttype, int ai_period, decimal adc_netmthpay, decimal adc_intrate)
        {
            decimal ldc_permamt = 0;
            try
            {
                string sqlStr;
                int li_fixcaltype = 1;

                double ldc_temp, ldb_dayyear, ldc_ft = 1.00;

                ldb_dayyear = Convert.ToDouble(30) / Convert.ToDouble(365);
                adc_intrate = adc_intrate / 100;

                switch (ai_paymenttype)
                {
                    case 0://ไม่มีการเรียกเก็บ
                        ldc_permamt = 999999999999;
                        break;
                    case 1://คงต้น                        
                        ldc_temp = Convert.ToDouble(ai_period) * (Convert.ToDouble(adc_intrate) * ldb_dayyear) + ldc_ft;
                        ldc_permamt = Convert.ToDecimal((Convert.ToDouble(adc_netmthpay) * Convert.ToDouble(ai_period)) / ldc_temp);
                        break;

                    case 2://คงยอด

                        double ldc_permamttmp = 1.00, ldc_fr = 0.00;

                        //ดึงค่า config การคำนวณดอกเบี้ยเฉลี่ยต่อเดือน 1 คือ 30/365, 2 คือ /12
                        sqlStr = @"select fixpaycal_type
                            from lnloanconstant
                            where coop_id = '" + state.SsCoopControl + "'";
                        Sdt dt = WebUtil.QuerySdt(sqlStr);
                        if (dt.Next())
                        {
                            li_fixcaltype = dt.GetInt32("fixpaycal_type");
                        }

                        if (li_fixcaltype == 1)
                        {
                            // ด/บ ทั้งปี / 12
                            ldc_temp = Math.Log(1 + (Convert.ToDouble(Convert.ToDouble(adc_intrate) / 12.00)));
                            ldc_fr = Math.Exp(-ai_period * ldc_temp);
                            ldc_permamttmp = (Convert.ToDouble(adc_netmthpay) * (ldc_ft - ldc_fr)) / ((Convert.ToDouble(adc_intrate) / 12));
                        }
                        else
                        {
                            // ด/บ 30 วัน/เดือน
                            ldc_temp = Math.Log(1 + (Convert.ToDouble(Convert.ToDouble(adc_intrate) / ldb_dayyear)));
                            ldc_fr = Math.Exp(-ai_period * ldc_temp);
                            ldc_permamttmp = (Convert.ToDouble(adc_netmthpay) * (ldc_ft - ldc_fr)) / ((Convert.ToDouble(adc_intrate) / ldb_dayyear));
                        }
                        ldc_permamt = Convert.ToDecimal(ldc_permamttmp);

                        break;
                    case 3://เก็บแต่ดอกเบี้ย
                        if (li_fixcaltype == 1)
                        {
                            ldc_permamt = Convert.ToDecimal(((Convert.ToDouble(adc_netmthpay) * 12) / Convert.ToDouble(adc_intrate)));
                        }
                        else
                        {
                            ldc_permamt = Convert.ToDecimal((Convert.ToDouble(adc_netmthpay) / (Convert.ToDouble(adc_intrate) * ldb_dayyear)));
                        }
                        break;
                    case 4://คงต้นไม่ส่งดอก                        
                        ldc_temp = Convert.ToDouble(ai_period) * (Convert.ToDouble(adc_intrate) * ldb_dayyear) + ldc_ft;
                        ldc_permamt = Convert.ToDecimal((Convert.ToDouble(adc_netmthpay) * Convert.ToDouble(ai_period)) / ldc_temp);
                        break;
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initpermiss" + ex); }

            return ldc_permamt;
        }

        /// <summary>
        /// คำนวณยอดขอกู้จากเงินเดือนคงเหลือ
        /// </summary>
        private decimal of_calmaxreqforsal()
        {
            string ls_loanpay;
            int li_paytype, li_retrystatus, li_period;
            decimal ldc_loancredit, ldc_salaryamt, ldc_mthpayother, ldc_mthpayexp, ldc_minsalary, ldc_mthpaycoop;
            decimal ldc_salarybal, ldc_netreq = 0, ldc_intrate, ldc_incomeother, ldc_incomefix;
            ldc_loancredit = dw_main.GetItemDecimal(1, "loancredit_amt");
            li_retrystatus = Convert.ToInt32(dw_main.GetItemDecimal(1, "retry_status"));
            ls_loanpay = dw_main.GetItemString(1, "loanpay_code");
            li_paytype = Convert.ToInt32(dw_main.GetItemDecimal(1, "loanpayment_type"));

            if (ls_loanpay != "KEP" || li_paytype == 0)
            {
                return ldc_loancredit;
            }

            ldc_salaryamt = dw_main.GetItemDecimal(1, "salary_amt");
            ldc_incomeother = dw_main.GetItemDecimal(1, "incomemonth_other");
            ldc_incomefix = dw_main.GetItemDecimal(1, "incomemonth_fixed");

            ldc_mthpayother = dw_main.GetItemDecimal(1, "paymonth_other");
            ldc_mthpayexp = dw_main.GetItemDecimal(1, "paymonth_exp");
            //ยอดหักสหกรณ์
            ldc_mthpaycoop = dw_main.GetItemDecimal(1, "paymonth_coop");
            // เงินเดือนคงเหลือขั้นต่ำ
            ldc_minsalary = dw_main.GetItemDecimal(1, "minsalary_amt");

            // เงินเดือนเหลือ
            ldc_salarybal = (ldc_salaryamt + ldc_incomefix + ldc_incomeother) - (ldc_mthpaycoop + ldc_mthpayexp + ldc_mthpayother + ldc_minsalary);


            if (ldc_salarybal > 0)
            {
                li_period = Convert.ToInt32(dw_main.GetItemDecimal(1, "period_payamt"));
                ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate");
                ldc_netreq = this.of_calnetlnpermiss(li_paytype, li_period, ldc_salarybal, ldc_intrate);
                if (ldc_netreq > ldc_loancredit)
                {
                    ldc_netreq = ldc_loancredit;
                }
            }
            else
            {
                //return 0;
                ldc_netreq = ldc_loancredit;  // เฉพาะ GSB
            }
            return ldc_netreq;
        }

        /// <summary>
        /// คำนวณเงินกู้กลุ่มที่ใช้ไป
        /// </summary>
        private decimal of_calpermissloangrpused(string as_permissgrp, int ai_kpsumflag)
        {
            decimal ldc_withdraw, ldc_balance, ldc_rkeepprin, ldc_grpuse = 0;
            int li_clrstatus, li_trnfer;
            string ls_lnpermgrp, ls_loantype;
            ls_loantype = dw_main.GetItemString(1, "loantype_code");
            if (ls_loantype == "15")
            {
                for (int i = 1; i <= dw_clear.RowCount; i++)
                {
                    ldc_withdraw = dw_clear.GetItemDecimal(i, "withdrawable_amt");
                    ldc_balance = dw_clear.GetItemDecimal(i, "principal_balance");
                    ldc_rkeepprin = dw_clear.GetItemDecimal(i, "rkeep_principal");
                    li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(i, "clear_status"));
                    li_trnfer = Convert.ToInt32(dw_clear.GetItemDecimal(i, "transfer_status"));
                    ls_lnpermgrp = dw_clear.GetItemString(i, "loanpermgrp_code");

                    if (li_clrstatus == 0 && li_trnfer == 0 && ls_lnpermgrp == as_permissgrp)
                    {
                        if (ai_kpsumflag == 1)
                        {
                            ldc_grpuse += ((ldc_withdraw + ldc_balance) - ldc_rkeepprin);
                        }
                        else
                        {
                            ldc_grpuse += (ldc_withdraw + ldc_balance);
                        }
                    }
                }
                return ldc_grpuse;
            }
            else
            {
                return ldc_grpuse;
            }
        }
       ////////////////////////////////////////////////////////////////////////////////////////
        private decimal of_calperiodpayment(decimal adc_prnamt, ref Int16 ai_installment, ref decimal adc_lastpay)
        {
            decimal ldc_periodpay = 0, ldc_intrate = 0;
            string ls_loantype = "";
            Int16 li_paymenttype = 0;

            ls_loantype = dw_main.GetItemString(1, "loantype_code");
            ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate");
            li_paymenttype = Convert.ToInt16(dw_main.GetItemDecimal(1, "loanpayment_type"));

            str_lncalperiod lstr_lncalperiod = new str_lncalperiod();

            lstr_lncalperiod.loantype_code = ls_loantype;
            lstr_lncalperiod.loanpayment_type = li_paymenttype;
            lstr_lncalperiod.calperiod_prnamt = adc_prnamt;
            lstr_lncalperiod.calperiod_intrate = ldc_intrate;
            lstr_lncalperiod.period_installment = ai_installment;

            try
            {
                wcf.NShrlon.of_calperiodpay(state.SsWsPass, ref lstr_lncalperiod);

                ai_installment = lstr_lncalperiod.period_installment;
                ldc_periodpay = lstr_lncalperiod.period_payment;
                adc_lastpay = lstr_lncalperiod.period_lastpayment;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            return ldc_periodpay;
        }  ////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// คำนวณยอดชำระ
        /// </summary>
        private void of_calperiodpayment()
        {


            decimal ldc_roundfacter = dw_main.GetItemDecimal(1, "payround_factor");
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            Int16 li_installment = Convert.ToInt16(dw_main.GetItemDecimal(1, "period_payamt"));
            Decimal ldc_loanreq = dw_main.GetItemDecimal(1, "loanrequest_amt");
            Int16 li_paymenttype = Convert.ToInt16(dw_main.GetItemDecimal(1, "loanpayment_type"));
            decimal ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate");
            decimal ldc_inttype = dw_main.GetItemDecimal(1, "int_continttype");
            //bee ตรวจสอบรูปแบบการคิดดอกเบี้ย
            if (ldc_intrate == 0 && ldc_inttype != 0) { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบอัตราดอกเบี้ย กรุณาตรวจสอบ"); }
            
            str_lncalperiod lstr_lncalperiod = new str_lncalperiod();

            lstr_lncalperiod.loantype_code = ls_loantype;
            lstr_lncalperiod.loanpayment_type = li_paymenttype;
            lstr_lncalperiod.calperiod_prnamt = ldc_loanreq;
            lstr_lncalperiod.calperiod_intrate = ldc_intrate;
            lstr_lncalperiod.period_installment = li_installment;

            try
            {

                wcf.NShrlon.of_calperiodpay(state.SsWsPass, ref lstr_lncalperiod);
                //bee
                //dw_main.SetItemDecimal(1, "period_payment", 100);
                //dw_main.SetItemDecimal(1, "period_payamt", 10000);
                //dw_main.SetItemDecimal(1, "period_lastpayment", 0);

                dw_main.SetItemDecimal(1, "period_payment", lstr_lncalperiod.period_payment);
                dw_main.SetItemDecimal(1, "period_payamt", lstr_lncalperiod.period_installment);
                dw_main.SetItemDecimal(1, "period_lastpayment", lstr_lncalperiod.period_lastpayment);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            // ใส่ค่าประมาณการชำระต่อเดือนของคำขอนี้
            of_setpaymthlnreq();

        }

        /// <summary>
        /// คำนวณ
        /// </summary>
        private void of_calinstallment()
        {
            //--------------------------
            // หาจำนวน งวดที่ต้องผ่อนชำระ
            //--------------------------

            int li_factor = 0, li_period = 0;
            Int16 li_paytype = 0, li_maxperiod = 0;
            decimal ldc_mod = 0, ldc_principal = 0, ldc_payamt = 0, ldc_intrate = 0;


            ldc_principal = dw_main.GetItemDecimal(1, "loanrequest_amt");
            ldc_payamt = dw_main.GetItemDecimal(1, "period_payment");
            li_paytype = Convert.ToInt16(dw_main.GetItemDecimal(1, "loanpayment_type"));
            li_factor = Convert.ToInt32(dw_main.GetItemDecimal(1, "payround_factor"));
            ldc_intrate = dw_main.GetItemDecimal(1, "identifycont_intrate");

            li_maxperiod = Convert.ToInt16(dw_main.GetItemDecimal(1, "maxsend_payamt"));

            // parameter check
            if (ldc_principal == 0 || ldc_payamt == 0)
            {
                return;
            }

            // ปรับค่าจำนวนชำระต่องวดด้วย factor
            if (li_factor != 0)
            {
                decimal ldc_factor = Math.Abs(Convert.ToDecimal(li_factor));
                ldc_mod = (ldc_payamt % ldc_factor);

                if (ldc_mod > 0)
                {
                    if (ldc_payamt > ldc_factor)
                    {
                        ldc_payamt = ldc_payamt - ldc_mod;
                        if (li_factor > 0)
                        {
                            ldc_payamt = ldc_payamt + li_factor;
                        }
                    }
                    else
                    {
                        ldc_payamt = ldc_factor;
                    }
                }
            }

            str_lncalperiod lstr_lncalperiod = new str_lncalperiod();

            lstr_lncalperiod.loanpayment_type = li_paytype;
            lstr_lncalperiod.calperiod_prnamt = ldc_principal;
            lstr_lncalperiod.calperiod_intrate = ldc_intrate;
            lstr_lncalperiod.period_payment = ldc_payamt;
            lstr_lncalperiod.calperiod_maxinstallment = li_maxperiod;

            try
            {
                wcf.NShrlon.of_calinstallment(state.SsWsPass, ref lstr_lncalperiod);

                dw_main.SetItemDecimal(1, "period_payamt", lstr_lncalperiod.period_installment);
                dw_main.SetItemDecimal(1, "period_payment", lstr_lncalperiod.period_payment);
                dw_main.SetItemDecimal(1, "period_lastpayment", lstr_lncalperiod.period_lastpayment);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// set ค่าจากข้อมูลลงรับ
        /// </summary>
        private void of_initreqloanregis(string member_no, string loantype_code)
        {
            try
            {
                string SqlLoanRegis = "select top 1 reqregister_docno, loanrequest_amt,loanapprove_amt,remark " +
                    " from lnreqloanregister " +
                    " where  member_no = '" + member_no + "' " +
                    " and loantype_code = '" + loantype_code + "' " +
                    " and reqregister_status = 8 " +
                    " order by lnreqreceive_date desc ";
                Sdt DtLoanRegis = WebUtil.QuerySdt(SqlLoanRegis);
                if (DtLoanRegis.Rows.Count > 0)
                {
                    dw_main.SetItemDecimal(1, "loanreqregis_amt", Convert.ToDecimal(DtLoanRegis.Rows[0]["loanrequest_amt"].ToString()));
                    dw_main.SetItemString(1, "ref_registerno", DtLoanRegis.GetString("reqregister_docno"));
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_Initreqloanregis" + ex); }
        }

        /// <summary>
        /// set ยอดหักจากสหกรณ์
        /// </summary>
        private void of_initpaymonthcoop()
        {
            try
            {
                decimal ldc_sumpay = 0, ldc_periodpay = 0, ldc_sumloan = 0, ldc_shrperiod, ldc_intest, chk, prn;
                int li_clrstatus, li_shrpaystatus, li_paytype;
                string ls_loanpay, ls_reqloanpay;

                // ดึงรายการหุ้น
                ldc_shrperiod = dw_main.GetItemDecimal(1, "share_periodvalue");

                //ดึงรายการหนี้
                for (int i = 1; i <= dw_clear.RowCount; i++)
                {
                    li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(i, "clear_status"));
                    string contno = dw_clear.GetItemString(i, "loancontract_no");
                    if (li_clrstatus == 0)
                    {
                        try
                        {
                            ls_loanpay = dw_clear.GetItemString(i, "loanpay_code");
                        }
                        catch
                        {
                            ls_loanpay = "KEP";
                        }

                        if (ls_loanpay == "KEP")
                        {
                            li_paytype = Convert.ToInt32(dw_clear.GetItemDecimal(i, "loanpayment_type"));
                            chk = dw_clear.GetItemDecimal(i, "rkeep_interest");
                            if (chk == 0)
                            {
                                ldc_periodpay = dw_clear.GetItemDecimal(i, "period_payment");
                                ldc_intest = dw_clear.GetItemDecimal(i, "intestnow_amt");

                                if (li_paytype == 0)
                                {
                                    ldc_periodpay = 0;
                                }
                                else if (li_paytype == 1)
                                {
                                    ldc_periodpay += ldc_intest;
                                }
                                else if (li_paytype == 3)
                                {
                                    ldc_periodpay = ldc_intest;
                                }
                            }
                            else
                            {
                                prn = dw_clear.GetItemDecimal(i, "rkeep_principal");
                                //ประเภทการชำระแบบ: 2 = คงยอด ให้เซตค่าเงินต้น
                                if (li_paytype == 2) { ldc_periodpay = dw_clear.GetItemDecimal(i, "period_payment"); }
                                //ประเภทการชำระแบบ: 1 = คงต้น ให้เซตค่าเงินต้น + ด/บ ปัจจุบัน (intestnow_amt)
                                else if (li_paytype == 1) { ldc_periodpay = prn + dw_clear.GetItemDecimal(i, "intestnow_amt"); }
                                else { ldc_periodpay = chk + prn; }

                            }

                            ldc_sumloan = ldc_sumloan + ldc_periodpay;
                        }
                    }
                }

                ldc_sumpay = ldc_sumloan + ldc_shrperiod;
                dw_main.SetItemDecimal(1, "paymonth_coop", ldc_sumpay);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initpaymonthcoop" + ex); }
        }

        /// <summary>
        /// set งวดสูงสุด
        /// </summary>
        private void of_initmaxperiod(string as_loantype, decimal adc_requestamt)
        {
            try
            {
                string sqlStr;
                sqlStr = @"select max_period
                    from lnloantypeperiod 
                    where coop_id = {0}
                    and loantype_code = {1}
                    and {2} between money_from and money_to";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_loantype, adc_requestamt);
                Sdt dt = WebUtil.QuerySdt(sqlStr);

                if (dt.Next())
                {
                    dw_main.SetItemDecimal(1, "maxperiod_payamt", dt.GetInt32("max_period"));
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initmaxperiod" + ex); }
        }

        /// <summary>
        /// set งวดส่ง
        /// </summary>
        private void of_initperiodsend()
        {
            int li_loanright = 0;
            li_loanright = Convert.ToInt32(dw_main.GetItemDecimal(1, "loanright_type"));

            if (li_loanright == 3)
            {
                dw_main.SetItemDecimal(1, "period_payamt", dw_main.GetItemDecimal(1, "maxsend_payamt"));
                return;
            }

            try
            {
                int li_retryage, li_maxperiod = 0, li_periodpay = 0, li_lnoverretry, li_retrysendtype, li_lnoverstatus, ldc_retryage = 0 ;
                decimal ldc_retryage_d =  0 ;


                string ls_loantype = dw_main.GetItemString(1, "loantype_code");

                li_lnoverretry = Convert.ToInt32(dw_main.GetItemDecimal(1, "lnoverretry_period"));
                li_retrysendtype = Convert.ToInt32(dw_main.GetItemDecimal(1, "retryloansend_type"));
                li_lnoverstatus = Convert.ToInt32(dw_main.GetItemDecimal(1, "lnoverretry_status"));
                
                // คำนวณอายุเกษียณทำปีให้เป็นเดือน 
                string sqlStr;
                sqlStr = @"select CONVERT(VARCHAR, (round( DATEDIFF(month,getdate(), retry_date) - ( DATEDIFF(month, retry_date,getdate()) % 12 ) , 0 )/12) )*12  as retry_age_1, 
		                    CONVERT(VARCHAR, round( ( DATEDIFF(month, getdate(),retry_date) % 12 ), 0 )) as retry_age_2  
                     from mbmembmaster   
                    where coop_id = {0}
                    and member_no ={1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, dw_main.GetItemString(1, "member_no"));
                Sdt dt = WebUtil.QuerySdt(sqlStr);

                if (dt.Next())
                {
                    
                    
                    ldc_retryage = dt.GetInt32("retry_age_1");  //ปี
                    ldc_retryage = ldc_retryage + dt.GetInt32("retry_age_2"); //เดือน
                }
                
                li_retryage = ldc_retryage;
                li_maxperiod = Convert.ToInt32(dw_main.GetItemDecimal(1, "maxperiod_payamt"));

                //ห้ามส่งเกินเกษียณ
                if (li_retrysendtype == 0)
                {
                    li_periodpay = ldc_retryage * 12;
                }
                //ส่งเกินได้ตามงวดที่ระบุ ไม่ตรวจสอบเงินไข
                if (li_retrysendtype == 1)
                {
                    li_periodpay = li_retryage + li_lnoverretry;
                }
                //ส่งเกินได้ตามงวดที่ระบุ ตรวจสอบเงินไข
                if (li_retrysendtype == 2)
                {
                    if (li_lnoverstatus == 1)
                    {
                        li_periodpay = li_retryage + li_lnoverretry;
                    }
                    else
                    {
                        li_periodpay = li_retryage;
                    }
                }

                if (li_periodpay > li_maxperiod)
                {
                    li_periodpay = li_maxperiod;
                }

                dw_main.SetItemDecimal(1, "maxsend_payamt", li_periodpay);
                dw_main.SetItemDecimal(1, "period_payamt", li_periodpay);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initperiodsend" + ex); }
        }

        /// <summary>
        /// init ค่าธรรมเนียมค่าบริการ
        /// </summary>
        private void of_initotherpay_fsv()
        {
            string ls_itemcode, ls_contno;
            int li_clrstatus, li_lastperiod = 0;
            decimal ldc_fineamt = 0, ldc_sumfine = 0, ldc_lnapprove = 0, ldc_paypercent = 0, ldc_prnbalance = 0, ldc_withdraw = 0, ldc_loanamt = 0, ldc_payamt;

            for (int i = 1; i <= dw_clear.RowCount; i++)
            {
                li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(i, "clear_status"));
                ldc_fineamt = dw_clear.GetItemDecimal(i, "fine_amt");
                if (li_clrstatus == 1 && ldc_fineamt > 0)
                {
                    ldc_sumfine += ldc_fineamt;
                    //ls_contno = dw_clear.GetItemString(i, "loancontract_no");
                    //li_lastperiod = Convert.ToInt32(dw_clear.GetItemDecimal(i, "last_periodpay"));
                    //ldc_lnapprove = dw_clear.GetItemDecimal(i, "loanapprove_amt");
                    //ldc_prnbalance = dw_clear.GetItemDecimal(i, "principal_balance");
                    //ldc_withdraw = dw_clear.GetItemDecimal(i, "withdrawable_amt");
                    //ldc_loanamt = ldc_lnapprove - ldc_withdraw;
                    //ldc_payamt = ldc_loanamt - ldc_prnbalance;
                    //ldc_paypercent = (ldc_payamt * 100) / ldc_loanamt;
                    //is_massalert += "\\nสัญญา " + ls_contno + " งวดชำระแล้ว = " + li_lastperiod.ToString() + ",% ชำระแล้ว = " + ldc_paypercent.ToString("#,##0.00") + "% ต้องคิดค่าบริการ เป็นจำนวนเงิน " + ldc_fineamt.ToString("#,##0.00");
                }
            }
            if (ldc_sumfine > 0)
            {
                int row = 0;

                for (int r = 1; r <= dw_otherclr.RowCount; r++)
                {
                    ls_itemcode = dw_otherclr.GetItemString(r, "clrothertype_code");
                    if (ls_itemcode == "FSV")
                    {
                        row = r;
                        break;
                    }
                }
                if (row == 0)
                {
                    row = dw_otherclr.InsertRow(0);
                }

                dw_otherclr.SetItemString(row, "clrothertype_code", "FSV");
                dw_otherclr.SetItemString(row, "clrother_desc", "ค่าธรรมเนียมในการบริหาร");
                dw_otherclr.SetItemDecimal(row, "clrother_amt", ldc_sumfine);
                dw_otherclr.SetItemDecimal(row, "clear_status", 1);
                //is_massalert = "\\nมีสัญญาเก่าชำระหนี้ไม่ถึงตามที่กำหนด " + is_massalert;
            }
        }

        /// <summary>
        /// ซื้อหุ้นเพิ่ม
        /// </summary>
        private void of_initclearshare()
        {
            try
            {
                string sqlStr, ls_loantype, ls_lnpermgrp, ls_clrpermgrp;
                int li_shrbuytype, li_clrstatus, li_shrcntflag;
                decimal ldc_calamt = 0, ldc_loanreq = 0, ldc_shrpercent = 0, ldc_shrstk = 0, ldc_reqshare, ldc_clrshare;
                ls_loantype = dw_main.GetItemString(1, "loantype_code");
                ldc_loanreq = dw_main.GetItemDecimal(1, "loanrequest_amt");
                ldc_shrstk = dw_main.GetItemDecimal(1, "share_balance");
                sqlStr = @"select  shrstk_buytype
                from lnloantype  
                where coop_id = {0}
                and loantype_code = {1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype);

                Sdt dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    li_shrbuytype = dt.GetInt32("shrstk_buytype");

                    if (li_shrbuytype == 0)
                    {
                        return;
                    }

                    switch (li_shrbuytype)
                    {
                        case 1://เทียบ %หุ้น ที่มีกับยอดขอกู้
                            ldc_calamt = 0;
                            break;
                        case 2://เทียบ %หุ้น ที่มีกับยอดหนี้คงเหลือทั้งหมดกับยอดขอกู้
                            for (int r = 1; r <= dw_clear.RowCount; r++)
                            {
                                li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(r, "clear_status"));
                                li_shrcntflag = Convert.ToInt32(dw_clear.GetItemDecimal(r, "shrstkcount_flag"));
                                if (li_clrstatus == 0 && li_shrcntflag == 1)
                                {
                                    ldc_calamt += dw_clear.GetItemDecimal(r, "principal_balance");
                                }
                            }
                            break;
                        case 3://เทียบ %หุ้น ที่มีกับยอดหนี้ขอกลุ่มเงินกู้นี้    
                            ls_lnpermgrp = dw_main.GetItemString(1, "loanpermgrp_code");

                            for (int r = 1; r <= dw_clear.RowCount; r++)
                            {
                                li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(r, "clear_status"));
                                li_shrcntflag = Convert.ToInt32(dw_clear.GetItemDecimal(r, "shrstkcount_flag"));
                                ls_clrpermgrp = dw_clear.GetItemString(r, "loanpermgrp_code");
                                if (li_clrstatus == 0 && li_shrcntflag == 1 && ls_lnpermgrp == ls_clrpermgrp)
                                {
                                    ldc_calamt += dw_clear.GetItemDecimal(r, "principal_balance");
                                }
                            }
                            break;
                    }
                    ldc_calamt = ldc_calamt + ldc_loanreq;

                    sqlStr = @"select sharestk_percent
                    from lnloantypebuyshare 
                    where coop_id = {0}
                    and loantype_code = {1}
                    and {2} between startloan_amt and endloan_amt";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype, ldc_calamt);

                    dt = WebUtil.QuerySdt(sqlStr);
                    if (dt.Next())
                    {
                        int row = 0;
                        for (int r = 1; r <= dw_otherclr.RowCount; r++)
                        {
                            string ls_item = dw_otherclr.GetItemString(r, "clrothertype_code");
                            if (ls_item == "SHR")
                            {
                                row = r;
                            }
                        }

                        ldc_shrpercent = dt.GetDecimal("sharestk_percent");
                        ldc_reqshare = ldc_calamt * (ldc_shrpercent / 100);
                        if (ldc_shrstk > ldc_reqshare && row > 0)
                        {
                            dw_otherclr.DeleteRow(row);
                        }
                        else if (ldc_shrstk < ldc_reqshare)
                        {
                            ldc_clrshare = ldc_reqshare - ldc_shrstk;
                            if ((ldc_clrshare % 10) > 0)
                            {
                                ldc_clrshare = ldc_clrshare - (ldc_clrshare % 10) + 10;
                            }

                            if (row == 0)
                            {
                                row = dw_otherclr.InsertRow(0);
                            }
                            dw_otherclr.SetItemString(row, "clrothertype_code", "SHR");
                            dw_otherclr.SetItemString(row, "clrother_desc", "ซื้อหุ้นเพิ่ม");
                            dw_otherclr.SetItemDecimal(row, "clear_status", 1);
                            dw_otherclr.SetItemDecimal(row, "clrother_amt", ldc_clrshare);

                            is_massalert += "\\n\\nมีทุนเรือนหุ้นไม่ถึงตามที่กำหนด \\nมูลค่าหนี้เงินกู้ที่นำมาคำนวณการซื้อหุ้น = " + ldc_calamt.ToString("#,##0.00") + "\\nต้องซื้อหุ้นเพิ่มเป็นจำนวนเงิน = " + ldc_clrshare.ToString("#,##0.00");
                        }
                    }
                }
                else
                {
                    for (int r = 1; r <= dw_otherclr.RowCount; r++)
                    {
                        string ls_item = dw_otherclr.GetItemString(r, "clrothertype_code");
                        if (ls_item == "SHR")
                        {
                            dw_otherclr.DeleteRow(r);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("of_initclearshare" + ex); };
        }

        /// <summary>
        /// alert ข้อความแจ้งเตือนต่างๆ       
        /// </summary>
        private void of_initwarnings()
        {
            if (is_massalert != "")
            {
                this.SetOnLoadedScript("alert('" + is_massalert + "')");
            }
        }

        /// <summary>
        /// init หลักประกันตามค่าคงที่
        /// </summary>
        private void of_initcolluse()
        {
            string sqlStr, ls_loantype;
            int li_useshare = 0, li_useman, li_row = 0;
            decimal ldc_loanreq;
            ls_loantype = dw_main.GetItemString(1, "loantype_code");
            ldc_loanreq = dw_main.GetItemDecimal(1, "loanrequest_amt");

            if (ldc_loanreq <= 0)
            {
                ldc_loanreq = 1;
            }
            sqlStr = @"select useman_amt,
	            useshare_flag
            from lnloantypereqgrt
            where coop_id = {0}
            and loantype_code = {1}
            and {2} between money_from and money_to";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_loantype, ldc_loanreq);

            dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                li_useman = dt.GetInt32("useman_amt");
                li_useshare = dt.GetInt32("useshare_flag");

                if (li_useshare == 1)
                {
                    for (int r = 1; r <= dw_coll.RowCount; r++)
                    {
                        string ls_colltype;
                        ls_colltype = dw_coll.GetItemString(r, "loancolltype_code");
                        if (ls_colltype == "02")
                        {
                            li_row = r;
                            break;
                        }
                    }

                    if (li_row <= 0)
                    {
                        li_row = dw_coll.InsertRow(0);
                        dw_coll.SetItemString(li_row, "loancolltype_code", "02");
                    }

                    HdRefcollrow.Value = li_row.ToString();
                    CheckCollPermiss();
                }

                if (li_useman > 0)
                {
                    li_row = 0;
                    for (int r = 1; r <= li_useman; r++)
                    {
                        li_row = dw_coll.InsertRow(0);
                        dw_coll.SetItemString(li_row, "loancolltype_code", "01");
                        CheckCollPermiss();
                    }

                }
                string[] colltype = new string[li_row];
                for (int k = 0; k != colltype.Length; k++)
                {
                    string h = dw_coll.GetItemString(k + 1, "loancolltype_code");
                    colltype[k] = dw_coll.GetItemString(k + 1, "loancolltype_code");
                }
                Hdcollarray.Value = String.Join(",", colltype.Distinct().ToArray());
            }
        }

        /// <summary>
        /// ตรวจสอบหลักประกัน
        /// </summary>
        private void CheckCollPermiss()
        {
            string as_loantype = "", as_colltype = "", as_collno = "";
            int row_coll;                   //แถว ค้ำประกัน

            try { row_coll = Convert.ToInt32(HdRefcollrow.Value); }
            catch { row_coll = 1; }

            as_loantype = dw_main.GetItemString(1, "loantype_code");
            try { as_colltype = dw_coll.GetItemString(row_coll, "loancolltype_code"); }
            catch { as_colltype = ""; }
            as_collno = HdRefcoll.Value;

            //ประเภทหลักประกันเป็น หุ้น
            if (as_colltype == "02")
            {
                try { as_collno = dw_main.GetItemString(1, "member_no"); }
                catch { as_collno = ""; }
            }//ประเภทหลักประกันเป็น หลักทรัพย์
            else if (as_colltype == "04" && as_collno == "")
            {
                try { as_collno = HdRefcollNO.Value; }
                catch { as_collno = ""; }
            }//ประเภทหลักประกันเป็น เงินฝาก
            else if (as_colltype == "03")
            {
                try { as_collno = dw_coll.GetItemString(row_coll, "ref_collno"); }
                catch { as_collno = ""; }
            }//ประเภทหลักประกัน ตำแหน่ง
            else if (as_colltype == "55")
            {
                try { as_collno = dw_main.GetItemString(1, "member_no"); }
                catch { as_collno = ""; }
            }
            else if (as_collno != "")
            {
                try
                {
                    as_collno = WebUtil.MemberNoFormat(as_collno);
                }
                catch { as_collno = ""; }
            }

            try
            {
                Boolean result_chk = ChkSameColl(as_collno, row_coll, as_colltype);


                if (result_chk == true && as_collno != "")
                {
                    string sqlStr = "";
                    string description = "";

                    //คน                    
                    if (as_colltype == "01")
                    {
                        //เช็คสิทธิ์การค้ำประกัน (as_reqlntype, as_colltype, as_refcoopid, as_refcollno);
                        //  int result = wcf.Shrlon.of_isvalidcoll(state.SsWsPass, as_loantype, as_colltype, state.SsCoopControl, as_collno);//pb120
                        int result = wcf.NShrlon.of_isvalidcoll(state.SsWsPass, as_loantype, as_colltype, state.SsCoopControl, as_collno);  //pn125
                        if (result == 1 && as_collno != "")
                        {
                            DateTime ldtm_loanreq = dw_main.GetItemDateTime(1, "loanrequest_date");
                            decimal li_retryage = 0;
                            string li_retryage_str = "";
                            string sql = /*@"select mbucfprename.prename_desc||mbmembmaster.memb_name||'   '||mbmembmaster.memb_surname as member_name,
                                to_number(ft_calage({2},retry_date,8 )) as retry_age
                            from mbmembmaster,                                        
                                mbucfprename                                
                            where ( mbmembmaster.prename_code = mbucfprename.prename_code ) 
                                and ( mbmembmaster.coop_id = {0} )
                                and ( mbmembmaster.member_no = {1} )";*/


                                @"select mbucfprename.prename_desc + mbmembmaster.memb_name + '   '+ mbmembmaster.memb_surname as member_name,
                      
                           dbo.ft_calage(convert(varchar,convert(datetime,{2})),retry_date,8 )  as retry_age
                            from mbmembmaster,                                        
                                mbucfprename                                
                            where ( mbmembmaster.prename_code = mbucfprename.prename_code ) 
                                and ( mbmembmaster.coop_id = {0})
                                and ( mbmembmaster.member_no = {1} ) ";


                            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_collno, ldtm_loanreq);
                            Sdt dt = WebUtil.QuerySdt(sql);
                           // dt = ta.Query(sql);
                            if (dt.Next())
                            {
                                description = dt.GetString("member_name");
                                dw_coll.SetItemString(row_coll, "ref_collno", as_collno);
                                dw_coll.SetItemString(row_coll, "description", description);
                                li_retryage_str = dt.GetString("retry_age");
                                li_retryage = Convert.ToDecimal(li_retryage_str);

                                int li_periodpay = Convert.ToInt32(dw_main.GetItemDecimal(1, "period_payamt"));
                                if (li_retryage < li_periodpay)
                                {
                                    LtServerMessagecoll.Text = WebUtil.WarningMessage("ผู้ค้ำคนนี้ ค้ำประกันได้สูงสุด " + li_retryage + " งวด ซื่งน้อยกว่างวดขอกู้");
                                }


                                //เช็คคนค้ำที่ค้ำไปเเล้วเเต่สัญญานั้นกำลังรออนุมัติ
                                if (as_collno != "")
                                {
                                    decimal count_ref_collno = 0;

                                    string sql_1 = @"select  count(lc.ref_collno) as count_ref_collno 
                                                 from  lnreqloan ln left join  lnreqloancoll lc on ln.loanrequest_docno = lc.loanrequest_docno
                                                 where ln.loanrequest_status = 8
                                                 and lc.ref_collno = '" + as_collno + "'";
                                    sql_1 = WebUtil.SQLFormat(sql_1);
                                    dt = WebUtil.QuerySdt(sql_1);
                                    //dt = ta.Query(sql_1);
                                    if (dt.Next())
                                    {
                                        count_ref_collno = dt.GetDecimal("count_ref_collno");
                                    }
                                    if (count_ref_collno > 0)
                                    {

                                        LtServerMessagecoll.Text = WebUtil.ErrorMessage("ผู้ค้ำทะเบียนนี้ " + as_collno + " ติดค้ำประกันสัญญาเงินกู้ที่รออนุมัติอยู่");
                                    }
                                }

                                //เช็คคนค้ำที่ค้ำไปเเล้วเเต่สัญญานั้นกำลังรออนุมัติ
                                if (as_collno != "")
                                {
                                    decimal count_ref_collno = 0;

                                    string sql_1 = @"select  count(lc.ref_collno) as count_ref_collno 
                                                 from  lnreqloan ln left join  lnreqloancoll lc on ln.loanrequest_docno = lc.loanrequest_docno
                                                 where ln.loanrequest_status = 8
                                                 and lc.ref_collno = '" + as_collno + "'";
                                    sql_1 = WebUtil.SQLFormat(sql_1);
                                    dt = WebUtil.QuerySdt(sql_1);
                                   // dt = ta.Query(sql_1);
                                    if (dt.Next())
                                    {
                                        count_ref_collno = dt.GetDecimal("count_ref_collno");
                                    }
                                    if (count_ref_collno > 0)
                                    {

                                        LtServerMessagecoll.Text = WebUtil.ErrorMessage("ผู้ค้ำทะเบียนนี้ " + as_collno + " ติดค้ำประกันสัญญาเงินกู้ที่รออนุมัติอยู่");
                                    }
                                }
                            }
                        }

                        GetCollPermiss();
                    }//หุ้น
                    else if (as_colltype == "02")
                    {
                        dw_coll.SetItemString(row_coll, "ref_collno", as_collno);
                        dw_coll.SetItemString(row_coll, "description", "ทุนเรือนหุ้น " + dw_main.GetItemString(1, "member_name"));
                        GetCollPermiss();
                    }
                    else if (as_colltype == "03")
                    {
                        sqlStr = @"select deptaccount_name 
                        from dpdeptmaster 
                        where deptclose_status = 0
                            and deptaccount_no = '" + as_collno + "'";
                        Sdt dt = WebUtil.QuerySdt(sqlStr);
                       // dt = ta.Query(sqlStr);
                        if (dt.Next())
                        {
                            description = dt.GetString("deptaccount_name");
                            dw_coll.SetItemString(row_coll, "ref_collno", as_collno);
                            dw_coll.SetItemString(row_coll, "description", "บัญชี" + description);
                        }
                        GetCollPermiss();
                    }//หลักทรัพย์
                    else if (as_colltype == "04")
                    {
                        sqlStr = @"select collmast_desc , redeem_flag
                            from lncollmaster
                            where collmast_no = '" + as_collno + "'";
                        Sdt dt = WebUtil.QuerySdt(sqlStr);
                      //  dt = ta.Query(sqlStr);
                        if (dt.Next())
                        {
                            description = dt.GetString("collmast_desc");
                            dw_coll.SetItemString(row_coll, "ref_collno", as_collno);
                            dw_coll.SetItemString(row_coll, "description", description);
                        }
                        GetCollPermiss();
                    }
                    //tomy ปันผล
                    else if (as_colltype == "06")
                    {
                        int percent06 = 100;
                        
                        dw_coll.SetItemString(row_coll, "ref_collno", as_collno);
                        dw_coll.SetItemString(row_coll, "description", "ปันผล " + dw_main.GetItemString(1, "member_name"));
                        dw_coll.SetItemDecimal(row_coll, "collbalance_amt", dw_main.GetItemDecimal(1, "loanrequest_amt"));
                        dw_coll.SetItemDecimal(row_coll, "collactive_amt", dw_main.GetItemDecimal(1, "loanrequest_amt"));
                        dw_coll.SetItemDecimal(row_coll, "collactive_percent", percent06);
                    }
                    //Edit By BankCM For get position coll
                    else if (as_colltype == "55")
                    {
                        decimal collactive_percent = 0, collpermiss = 0, collmaxcoll = 0;
                        if ((dw_coll.RowCount - 1) <= 1)
                        {
                            for (int i = 1; i <= dw_coll.RowCount - 1; i++)
                            {
                                collactive_percent = 0;
                                collpermiss = 0;
                                try
                                {
                                    collpermiss = collpermiss + dw_coll.GetItemDecimal(i, "collactive_amt");
                                }
                                catch { collpermiss = collpermiss + 0; }

                                try
                                {
                                    collactive_percent = collactive_percent + dw_coll.GetItemDecimal(i, "collactive_percent");
                                }
                                catch { collactive_percent = collactive_percent + 0; }
                            }
                            collpermiss = dw_main.GetItemDecimal(1, "loanrequest_amt") - collpermiss;
                            collactive_percent = 100 - collactive_percent;
                            collmaxcoll = dw_main.GetItemDecimal(1, "loanrequest_amt");
                        }
                        else
                        {
                            collpermiss = dw_main.GetItemDecimal(1, "loanrequest_amt");
                            collactive_percent = 100;
                            collmaxcoll = dw_main.GetItemDecimal(1, "loanrequest_amt");
                        }

                        dw_coll.SetItemString(row_coll, "ref_collno", dw_main.GetItemString(1, "member_no"));
                        dw_coll.SetItemString(row_coll, "description", dw_main.GetItemString(1, "member_name"));
                        dw_coll.SetItemDecimal(row_coll, "collbalance_amt", collpermiss);
                        dw_coll.SetItemDecimal(row_coll, "collbase_amt", collpermiss);
                        dw_coll.SetItemDecimal(row_coll, "collactive_amt", collpermiss);
                        dw_coll.SetItemDecimal(row_coll, "collactive_percent", collactive_percent);
                        dw_coll.SetItemDecimal(row_coll, "collbase_percent", 100);
                        dw_coll.SetItemDecimal(row_coll, "collmax_amt", collmaxcoll);

                    }

                }

            }

            catch (Exception ex) { LtServerMessagecoll.Text += WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// ตรวจสอบสิทธิ์ค้ำ
        /// </summary>
        private void of_recalcollpermiss()
        {
            HdCondition.Value = "true";
            string ls_memno = "", ls_loantype = "", ls_colltype = "", ls_collno = "", ls_reqdocno, ls_excludecont = "";
            decimal ldc_collbaseamt = 0, ldc_maxcoll = 0, ldc_collbasepercent = 0, ldc_collusecont = 0, ldc_collusereq = 0, ldc_collbal = 0;
            int li_clrstatus = 0;
            DateTime ldtm_lnreq;
            ls_memno = dw_main.GetItemString(1, "member_no");
            ls_loantype = dw_main.GetItemString(1, "loantype_code");
            ls_reqdocno = dw_main.GetItemString(1, "loanrequest_docno");
            ldtm_lnreq = dw_main.GetItemDateTime(1, "loanrequest_date");

            for (int i = 1; i <= dw_clear.RowCount; i++)
            {//ดึงเลขที่สัญญาหักกลบที่ถูกเลือก
                li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(i, "clear_status"));
                if (li_clrstatus == 1)
                {
                    ls_excludecont += "," + dw_clear.GetItemString(i, "loancontract_no");
                }
            }

            if (ls_excludecont != "")
            {
                ls_excludecont = ls_excludecont.Substring(1);
            }

            for (int r = 1; r <= dw_coll.RowCount; r++)
            {
                ls_colltype = dw_coll.GetItemString(r, "loancolltype_code");
                ls_collno = dw_coll.GetItemString(r, "ref_collno");

                if (ls_collno.Trim() == "")
                {
                    continue;
                }
                try
                {//สิทธิค้ำสูงสุด
                    if (ls_colltype == "04")
                    {
                        string sql = @"select est_price, est_percent from lncollmaster where ( coop_id = {0} ) and ( collmast_no = {1} )";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_collno);
                        Sdt dt = WebUtil.QuerySdt(sql);
                   //     dt = ta.Query(sql);
                        if (dt.Next())
                        {
                            ldc_collbaseamt = dt.GetDecimal("est_price");
                            ldc_collbasepercent = dt.GetDecimal("est_percent");
                            ldc_maxcoll = ldc_collbaseamt;
                        }
                    }
                    else
                    {
                        wcf.NShrlon.of_getcollpermiss(state.SsWsPass, ls_loantype, ls_colltype, state.SsCoopControl, ls_collno, ldtm_lnreq, ref ldc_collbaseamt, ref ldc_maxcoll, ref ldc_collbasepercent);  //pb125
                    }
                    dw_coll.SetItemDecimal(r, "collbase_amt", ldc_collbaseamt);
                    dw_coll.SetItemDecimal(r, "collbase_percent", ldc_collbasepercent);
                    dw_coll.SetItemDecimal(r, "collmax_amt", ldc_maxcoll);
                }
                catch (Exception ex)
                {
                    LtServerMessagecoll.Text += WebUtil.WarningMessage(ex);
                }

                //เฉพาะคนค้ำต้องตรวจนับสัญญา
                if (ls_colltype == "01")
                {
                    try
                    {
                        //int result = wcf.Shrlon.of_checkcollmancount(state.SsWsPass, state.SsCoopControl, ls_collno, ls_memno, ls_loantype, ls_excludecont, ls_reqdocno);  //pb120
                        int result = wcf.NShrlon.of_checkcollmancount(state.SsWsPass, state.SsCoopControl, ls_collno, ls_memno, ls_loantype, ls_excludecont, ls_reqdocno);  //pb125
                    }
                    catch (Exception ex)
                    {
                        LtServerMessagecoll.Text += WebUtil.ErrorMessage(ex);
                    }
                }

                try
                {//สิทธิ์ค้ำที่ใช้ไปของใบคำขอ
                    //ldc_collusecont = wcf.Shrlon.of_getcollusecontamt(state.SsWsPass, state.SsCoopControl, ls_collno, ls_loantype, ls_colltype, ls_excludecont, ls_reqdocno); //pb120
                    ldc_collusecont = wcf.NShrlon.of_getcollusecontamt(state.SsWsPass, state.SsCoopControl, ls_collno, ls_loantype, ls_colltype, ls_excludecont, ls_reqdocno); //pb125
                }
                catch (Exception ex) { ex.ToString(); }

                try
                {//เช็คจำนวนสัญญาที่สามารถใช้ค้ำได้
                    //ldc_collusereq = wcf.Shrlon.of_getcollusereqamt(state.SsWsPass, state.SsCoopControl, ls_collno, ls_loantype, ls_colltype, ls_reqdocno);//pb120//
                    ldc_collusereq = wcf.NShrlon.of_getcollusereqamt(state.SsWsPass, state.SsCoopControl, ls_collno, ls_loantype, ls_colltype, ls_reqdocno);  //pb125
                }
                catch (Exception ex) { ex.ToString(); }


                ldc_collbal = (ldc_collbaseamt - ldc_collusecont - ldc_collusereq) * ldc_collbasepercent / 100;


                dw_coll.SetItemDecimal(r, "collbalance_amt", ldc_collbal);
            }
        }

        /// <summary>
        /// ตรวจสอบสิทธิ์ค้ำ
        /// </summary>
        private void GetCollPermiss()
        {
            string as_memno = "", as_loantype = "", as_colltype = "", as_collno = "", as_reqdocno, as_excludecont = "";
            int row_coll, row_clr;
            decimal clear_status = 0,       //สถาณะสัญญาหักกลบ
                    loanreqregis_amt, loanrequest_amt,      //ยอดขอกู้
                    collpermiss = 0,        //สิทธิค้ำสูงสุด
                    collactive_percent = 0, //% ค้ำ
                    collmaxcoll = 0;        //สิทธิ์ค้ำสูงสุดของสัญญาเงินกู้
            decimal permiss = 0,            //ref service of_getcollpermiss
                    percent = 0,            //ref service of_getcollpermiss
                    maxcoll = 0;            //ref service of_getcollpermiss
            decimal collusecontamt = 0,     //สิทธิ์ค้ำที่ใช้ไปของใบคำขอ
                    collusereqamt = 0,      //เช็คจำนวนสัญญาที่สามารถใช้ค้ำได้
                    collbalance_amt,        //สิทธิค้ำคงเหลือ
                    collactive_amt = 0;     //ค้ำประกัน
            DateTime adtm_check;            //วันที่ทำรายการ

            try { row_coll = Convert.ToInt32(HdRefcollrow.Value); }
            catch { row_coll = 1; }

            as_loantype = dw_main.GetItemString(1, "loantype_code");
            adtm_check = dw_main.GetItemDateTime(1, "loanrequest_date");
            as_memno = dw_main.GetItemString(1, "member_no");
            as_reqdocno = dw_main.GetItemString(1, "loanrequest_docno");
            if (as_reqdocno == "Auto") { as_reqdocno = null; }
            loanreqregis_amt = dw_main.GetItemDecimal(1, "loanreqregis_amt");
            loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");

            try { as_colltype = dw_coll.GetItemString(row_coll, "loancolltype_code"); }
            catch { as_colltype = ""; }

            as_collno = dw_coll.GetItemString(row_coll, "ref_collno"); ;

            row_clr = dw_clear.RowCount;

            for (int i = 1; i <= row_clr; i++)
            {//ดึงเลขที่สัญญาหักกลบที่ถูกเลือก
                clear_status = dw_clear.GetItemDecimal(i, "clear_status");
                if (clear_status == 1)
                {
                    if (flag == 1)
                    {
                        as_excludecont += ",";
                    }
                    as_excludecont += dw_clear.GetItemString(i, "loancontract_no");
                    flag = 1;
                }
            }

            try
            {//สิทธิค้ำสูงสุด
                if (as_colltype == "04")
                {
                    string sql = @"select est_price, est_percent from lncollmaster where ( coop_id = {0} ) and ( collmast_no = {1} )";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_collno);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    //dt = ta.Query(sql);
                    if (dt.Next())
                    {
                        collpermiss = dt.GetDecimal("est_price");
                        collactive_percent = dt.GetDecimal("est_percent");
                        collmaxcoll = collpermiss;
                        percent = 100;
                    }
                }
                else
                {
                    //int result = wcf.Shrlon.of_getcollpermiss(state.SsWsPass, as_loantype, as_colltype, state.SsCoopControl, as_collno, adtm_check, ref permiss, ref maxcoll, ref percent); //pb120
                    int result = wcf.NShrlon.of_getcollpermiss(state.SsWsPass, as_loantype, as_colltype, state.SsCoopControl, as_collno, adtm_check, ref permiss, ref maxcoll, ref percent);  //pb125

                    collpermiss = permiss;
                    collactive_percent = percent;
                    collmaxcoll = maxcoll;

                    // ถ้าเป็นหุ้นต้องเอายอดที่ซื้อเพิ่มมาบวกด้วย
                    if (as_colltype == "02")
                    {
                        decimal ldc_shradd = 0;
                        string ls_itemcode;

                        for (int i = 1; i <= dw_otherclr.RowCount; i++)
                        {
                            ls_itemcode = dw_otherclr.GetItemString(i, "clrothertype_code");

                            if (ls_itemcode == "SHR")
                            {
                                ldc_shradd = dw_otherclr.GetItemDecimal(i, "clrother_amt");
                            }
                        }

                        collpermiss = collpermiss + ldc_shradd;
                        collmaxcoll = collmaxcoll + ldc_shradd;
                    }
                }

                dw_coll.SetItemDecimal(row_coll, "collbase_amt", collpermiss);
                dw_coll.SetItemDecimal(row_coll, "collbase_percent", collactive_percent);
                dw_coll.SetItemDecimal(row_coll, "collmax_amt", collmaxcoll);
            }
            catch (Exception ex)
            {//สิทธิ์ค้ำประกันเต็มวงเงินแล้ว
                LtServerMessagecoll.Text += WebUtil.WarningMessage(ex);
            }

            //เฉพาะคนค้ำต้องตรวจนับสัญญา
            if (as_colltype == "01")
            {
                try
                {
                    wcf.NShrlon.of_checkcollmancount(state.SsWsPass, state.SsCoopControl, as_collno, as_memno, as_loantype, as_excludecont, as_reqdocno); //pb125
                }
                catch (Exception ex)
                {
                    LtServerMessagecoll.Text += WebUtil.WarningMessage(ex);
                }
            }

            //สิทธิ์ค้ำที่ใช้ไปของใบคำขอ
            try
            {
                collusecontamt = wcf.NShrlon.of_getcollusecontamt(state.SsWsPass, state.SsCoopControl, as_collno, as_loantype, as_colltype, as_excludecont, as_reqdocno); //pb125
            }
            catch (Exception ex) { ex.ToString(); }

            //เช็คจำนวนสัญญาที่สามารถใช้ค้ำได้
            try
            {
                collusereqamt = wcf.NShrlon.of_getcollusereqamt(state.SsWsPass, state.SsCoopControl, as_collno, as_loantype, as_colltype, as_reqdocno);  //pb125
            }
            catch (Exception ex) { ex.ToString(); }

            try
            {
                collbalance_amt = (collpermiss - collusecontamt - collusereqamt) * percent / 100;

                if (collbalance_amt <= 0)
                {
                    LtServerMessagecoll.Text += WebUtil.WarningMessage("ยอดค้ำประกันคงเหลือไม่เพียงพอ ยอดใช้ไปแล้ว " + Convert.ToString(collusecontamt + collusereqamt));
                }

                //ถ้า สิทธิ์ค้ำคงเหลือ มากกว่า สิทธิ์ค้ำสูงสุดของสัญญานี้ ให้ สิทธิ์ค้ำ = สิทธิ์ค้ำสูงสุด
                if (collbalance_amt > maxcoll)
                {
                    collactive_amt = maxcoll;
                }
                else
                {
                    collactive_amt = collbalance_amt;
                }

                //ถ้า ขอกู้ น้อยกว่า สิทธิ์ค้ำ ให้ สิทธิ์ค้ำ = ขอกู้
                if (loanrequest_amt <= collactive_amt) { collactive_amt = loanrequest_amt; }

                //หา %ค้ำชำระ
                collactive_percent = collactive_amt * 100 / loanrequest_amt;

                if (collbalance_amt > 0)
                {
                    dw_coll.SetItemDecimal(row_coll, "collbalance_amt", collbalance_amt);
                    dw_coll.SetItemDecimal(row_coll, "collactive_amt", collactive_amt);
                    dw_coll.SetItemDecimal(row_coll, "collactive_percent", collactive_percent);
                    dw_coll.SetItemDecimal(row_coll, "collused_amt", collusecontamt);
                    dw_coll.SetItemDecimal(row_coll, "collbase_percent", percent);
                }
                else
                {
                    dw_coll.SetItemString(row_coll, "ref_collno", "");
                    dw_coll.SetItemString(row_coll, "description", "");
                    dw_coll.SetItemDecimal(row_coll, "collbalance_amt", 0);
                    dw_coll.SetItemDecimal(row_coll, "collactive_amt", 0);
                    dw_coll.SetItemDecimal(row_coll, "collactive_percent", 0);
                }
            }
            catch (Exception ex) { LtServerMessagecoll.Text += WebUtil.WarningMessage(ex); }
        }

        /// <summary>
        /// ตรวจสอบการซ้ำของหลักประกัน
        /// </summary>
        public Boolean ChkSameColl(String as_collno, int row_coll, string as_colltype)
        {
            string ref_collno = "", loancolltype_code = "";
            Boolean retrn = true;
            for (int i = 1; i <= dw_coll.RowCount; i++)
            {
                if (row_coll != i && as_collno != "")
                {
                    try
                    {
                        ref_collno = dw_coll.GetItemString(i, "ref_collno");
                        loancolltype_code = dw_coll.GetItemString(i, "loancolltype_code");
                    }
                    catch { ref_collno = ""; loancolltype_code = ""; }
                    if (ref_collno != "")
                    {
                        if ((as_collno == ref_collno) && (as_colltype == loancolltype_code))
                        {
                            LtServerMessagecoll.Text += WebUtil.WarningMessage("พบหลักประกันซ้ำ " + ref_collno);
                            //Response.Write(@"<script language='javascript'>alert('พบหลักประกันซ้ำ '" + ref_collno + "');</script>");
                            dw_coll.SetItemString(row_coll, "ref_collno", "");
                            dw_coll.SetItemString(row_coll, "description", "");
                            retrn = false;
                        }
                        else { retrn = true; }
                    }
                }
            }
            return retrn;
        }

       
        /// <summary>
        ///บันทึกใบคำขอ
        /// </summary>
        public void SaveWebSheet()
        {
            try
            {
                int li_return = JsCheckDataBeforesave();
                if (li_return == 1)
                {
                    dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                    String memcoop_id = dw_main.GetItemString(1, "memcoop_id");
                    member_no = dw_main.GetItemString(1, "member_no");
                    String dwMain_XML = dw_main.Describe("DataWindow.Data.XML");
                    String dwColl_XML = "";
                    String dwClear_XML = "";
                    String dwOtherClr_XML = "";
                    String dwMthexp_XML = "";
                    String dwintspc_XML = "";
                    DateTime ldtm_loanrequest = dw_main.GetItemDateTime(1, "loanrequest_date");
                    string ls_message = "";
                    Decimal use_amt = 0;
                    String as_deptaccount = "";

                    decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");

                    //mai กรณี ไม่มีการค้ำประกันให้สามารถบันทึกได้ 
                    if (dw_coll.RowCount > 0)
                    {
                        dwColl_XML = dw_coll.Describe("DataWindow.Data.XML");
                    }

                    if (dw_clear.RowCount > 0)
                    {
                        dwClear_XML = dw_clear.Describe("DataWindow.Data.XML");
                    }

                    if (dw_otherclr.RowCount > 0)
                    {
                        dwOtherClr_XML = dw_otherclr.Describe("DataWindow.Data.XML");
                    }

                    if (dw_mthexp.RowCount > 0)
                    {
                        dwMthexp_XML = dw_mthexp.Describe("DataWindow.Data.XML");
                    }

                    if (dw_intspc.RowCount > 0)
                    {
                        dwintspc_XML = dw_intspc.Describe("DataWindow.Data.XML");
                    }


                    str_savereqloan strSave = new str_savereqloan();
                    strSave.xml_main = dwMain_XML;
                    strSave.xml_clear = dwClear_XML;
                    strSave.xml_guarantee = dwColl_XML;
                    strSave.xml_otherclr = dwOtherClr_XML;
                    strSave.xml_mthexp = dwMthexp_XML;
                    strSave.xml_intspc = dwintspc_XML;



                    strSave.contcoopid = state.SsCoopControl;
                    strSave.format_type = "TKS";
                    strSave.entry_id = state.SsUsername;
                    strSave.coop_id = state.SsCoopId;
                    String period_payamt = dw_main.GetItemDecimal(1, "period_payamt").ToString("0.00");
                    bool is_point1 = period_payamt.IndexOf(".00") < 0;

                    String period_payment = dw_main.GetItemDecimal(1, "period_payment").ToString("0.00");
                    bool is_point2 = period_payment.IndexOf(".00") < 0;

                    if (is_point1 == true || is_point2 == true)
                    {
                        if (is_point1 == true) { LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนงวดเป็นทศนิยม =" + period_payamt); }
                        else if (is_point2 == true) { LtServerMessage.Text = WebUtil.ErrorMessage("ต้นชำระเป็นทศนิยม =" + period_payment); }
                        else { LtServerMessage.Text = WebUtil.ErrorMessage("ยอดค้ำน้อยกว่ายอดขอกู้ กรุณาตรวจสอบ หรือกด คำนวณ % ใหม่อีกครั้ง "); }
                    }
                    else
                    {
                        int runningNO = wcf.NShrlon.of_save_lnreq(state.SsWsPass, ref strSave);//pb125
                        string committee = "";
                        string apvcondition = "";
                        decimal flag = 0;
                        decimal baseinsure = 0;
                        decimal insure = 0;
                        decimal year = 0;
                        try
                        {
                            committee = dw_main.GetItemString(1, "committee_code");
                        }
                        catch { committee = ""; }
                        try
                        {
                            apvcondition = dw_main.GetItemString(1, "apvcondition_code");
                        }
                        catch { apvcondition = ""; }
                        try
                        {
                            flag = dw_main.GetItemDecimal(1, "insure_flag");
                        }
                        catch { flag = 0; }
                        try
                        {
                            baseinsure = dw_main.GetItemDecimal(1, "baseinsure_amt");
                        }
                        catch { baseinsure = 0; }
                        try
                        {
                            insure = dw_main.GetItemDecimal(1, "insure_amt");
                        }
                        catch { insure = 0; }
                        try
                        {
                            year = dw_main.GetItemDecimal(1, "insure_year");
                        }
                        catch { year = 0; }

                        string sql1 = @"update lnreqloan set committee_code = {2},apvcondition_code = {3},insure_flag = {4},
                                   baseinsure_amt = {5},insure_amt = {6},insure_year = {7} where coop_id = {0} and loanrequest_docno = {1}";
                        sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, strSave.request_no, committee, apvcondition, flag, baseinsure, insure, year);
                        Sdt dt1 = WebUtil.QuerySdt(sql1);

                        reqdoc_no = strSave.request_no;
                        txt_reqNo.Text = reqdoc_no;
                        txt_member_no.Text = member_no;
                        int li_apv = Convert.ToInt16(dw_main.GetItemDecimal(1, "apvimmediate_status"));

                        if (li_apv == 1 || li_apv == 2)
                        {
                            string contno = strSave.loancontract_no;
                            Hdcontno.Value = contno;
                            HdReturn.Value = "11";
                        }
                        x = 2;

                        dw_main.SetItemString(1, "loanrequest_docno", reqdoc_no);

                        //---Srart Script---
                        Response.Write(@"<script language='javascript'>
                                                alert('ใบคำขอเลขที่ " + reqdoc_no + @" หลังจากนี้จะรายละเอียดเงินกู้ ');
                                         </script>");

                        //---End Script---

                        // Edit By BankCM For print contract,coll contract and cover sheet with iReport
                        //if (!(PrintContracConsider(reqdoc_no)))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "AlertMessage", "<script language='javascript'> alert('พิมพ์ใบรายละเอียดพิจารณาเงินกู้มีข้อผิดพลาด สามารพิมพ์ย้อนหลังได้ที่ ระบบรายงาน');</script>", false);
                        //}

                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");

                        //พิมพ์หนังสือเสนอกรรมการ เงินกู้พิเศษ
                        string lngrp_code = "";
                        String sql = @"select loangroup_code from lnloantype where loantype_code = {0}";
                        sql = WebUtil.SQLFormat(sql, dw_main.GetItemString(1, "loantype_code"));
                        Sdt result = WebUtil.QuerySdt(sql);

                        while (result.Next())
                        {
                            lngrp_code = result.GetString("loangroup_code");
                        }
                        if (lngrp_code == "03")
                        {
                            Panel1.Visible = true;
                            HdPrtReqdocno.Value = reqdoc_no;
                        }

                        //Printcont(reqdoc_no);

                        Ltdividen.Text = " ";
                        Ltjspopup.Text = " ";

                        if (li_apv == 0)
                        {
                            String ls_loantype = dw_main.GetItemString(1, "loantype_code");
                            Session["loantypeCode"] = ls_loantype;
                            JsReNewPage();
                        }
                    }
                }


                // reset f2

                this.SetOnLoadedScript(" parent.SetPage();");

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private static string XmlReadVar(string responseData, string szVar)
        {
            int i1stLoc = responseData.IndexOf("<" + szVar + ">");
            if (i1stLoc < 0)
                return string.Empty;
            int ilstLoc = responseData.IndexOf("</" + szVar + ">");
            if (ilstLoc < 0)
                return string.Empty;
            int len = szVar.Length;
            return responseData.Substring(i1stLoc + len + 2, ilstLoc - i1stLoc - len - 2);
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                //หาจำนวนงวดที่ชำระ
                string member_no = dw_main.GetItemString(1, "member_no");
                if (member_no != null || member_no != "")
                {
                    DateTime startkeep_date = dw_main.GetItemDate(1, "startkeep_date");
                    DateTime retry_date = dw_main.GetItemDateTime(1, "retry_date");
                    Int32 month_ = 12 - (startkeep_date.Month - 1);
                    Int32 retryage = (retry_date.Year - startkeep_date.Year - 1) * 12 + 9 + month_;
                    dw_main.SetItemDecimal(1, "retry_age", retryage);
                }
            }
            catch { }
            HdRefcoll.Value = "";
            dw_main.SaveDataCache();//main
            dw_coll.SaveDataCache();//หลักประกัน
            dw_clear.SaveDataCache();//หักกลบ     
            dw_otherclr.SaveDataCache();//หักอื่น
            dw_mthexp.SaveDataCache();//หัก slip เงินเดือน
            dw_intspc.SaveDataCache();
            //is_massalert = "";
            Session["loantypeCode"] = dw_main.GetItemString(1, "loantype_code");
        }

        private DateTime JsGetPostingdate(int year, int month, DateTime request_date)
        {
            string ls_sql = "select postingdate from amworkcalendar where  year = " + year.ToString() + "  and month  = " + month.ToString();
            DateTime postingdate = request_date;
            Sdt dtpro = WebUtil.QuerySdt(ls_sql);
            if (dtpro.Next())
            {
                try
                {
                    int daypost = dtpro.GetInt32("postingdate");
                    year = year - 543;
                    string postdate = daypost.ToString() + "/" + month.ToString() + "/" + year.ToString();
                    postingdate = Convert.ToDateTime(postdate);
                    // if (postingdate < request_date) { postingdate = request_date; }
                }
                catch
                { postingdate = request_date; }
                //กรณี วันที่จ่ายเงิน=วันที่ขอกู้
            }
            else
            {
                postingdate = request_date;
            }
            return postingdate;
        }

        /// <summary>
        /// fillter ประเภทการจ่ายเงิน
        /// </summary>
        private void JsGetexpensememno()
        {
            try
            {
                string memno = dw_main.GetItemString(1, "member_no");
                string strsql = @"select expense_code, expense_bank, expense_branch, expense_accid 
                                        from mbmembmaster where member_no = '" + memno + "'";
                try
                {
                    Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                    if (dtloanrcv.GetRowCount() <= 0)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + memno);
                    }
                    if (dtloanrcv.Next())
                    {
                        string loanrcv_code = "", loanrcv_bank = "";

                        try { loanrcv_code = dw_main.GetItemString(1, "expense_code"); }
                        catch { loanrcv_code = ""; }
                        if (loanrcv_code == "" || loanrcv_code == null) { loanrcv_code = dtloanrcv.GetString("expense_code"); }

                        try { loanrcv_bank = dw_main.GetItemString(1, "expense_bank"); }
                        catch { loanrcv_bank = ""; }
                        if (loanrcv_bank == "" || loanrcv_bank == null) { loanrcv_bank = dtloanrcv.GetString("expense_bank"); }

                        string loanrcv_branch = dtloanrcv.GetString("expense_branch");
                        string loanrcv_accid = dtloanrcv.GetString("expense_accid");

                        if (loanrcv_code != null)
                        {
                            dw_main.SetItemString(1, "expense_code", loanrcv_code);
                            dw_main.SetItemString(1, "expense_bank", loanrcv_bank);
                            if (loanrcv_branch == "" || loanrcv_branch == null)
                            {
                                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment.pbl", loanrcv_bank);
                            }
                            else
                            {
                                dw_main.SetItemString(1, "expense_branch", loanrcv_branch);
                            }
                            dw_main.SetItemString(1, "expense_accid", loanrcv_accid);
                        }
                        else
                        {
                            dw_main.SetItemString(1, "expense_code", "CBT");
                        }

                        if (loanrcv_code == "CBT" && loanrcv_bank.Length > 2)
                        {
                            string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                            Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                            string bankbranch = "";
                            if (dtk.Next())
                            {
                                bankbranch = dtk.GetString("branch_name").Trim();
                                JsExpensebankbrRetrieve();
                            }
                        }
                        if (loanrcv_code == "CBO" && loanrcv_bank.Length > 2)
                        {
                            string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                            Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                            string bankbranch = "";
                            if (dtk.Next())
                            {
                                bankbranch = dtk.GetString("branch_name").Trim();
                                JsExpensebankbrRetrieve();
                            }
                        }
                    }
                }
                catch { }
            }
            catch { }
        }

        private void JsExpenseCode()
        {
            string expendCode = "";
            try
            {
                expendCode = dw_main.GetItemString(1, "expense_code");
            }
            catch { }
            if (expendCode == null || expendCode == "") return;

            if ((expendCode == "CHQ") || (expendCode == "TRN") || (expendCode == "CBT") || (expendCode == "DRF") || (expendCode == "TBK"))
            {
                try
                {
                    DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "sl_loan_requestment.pbl", null);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

            }
            else if (expendCode == "CBO")
            {
                //ฝั่งธนาคาร
                dw_main.Modify("t_20.visible =1");
                dw_main.Modify("expense_bank.visible =1");
                dw_main.Modify("t_30.visible =1");
                dw_main.Modify("expense_bank_1.visible =1");

                dw_main.Modify("t_39.visible =1");
                dw_main.Modify("expense_branch.visible =1");
                dw_main.Modify("t_27.visible =1");
                dw_main.Modify("expense_branch_1.visible =1");
                dw_main.Modify("b_expense_branch.visible = 1");

                try
                {
                    DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "sl_loan_requestment.pbl", null);
                    JsGetexpensememno();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }

            else if ((expendCode == "CSH") || (expendCode == "BEX") || (expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {
                //ฝั่งธนาคาร
                dw_main.Modify("t_20.visible =0");
                dw_main.Modify("expense_bank.visible =0");
                dw_main.Modify("t_30.visible =0");
                dw_main.Modify("expense_bank_1.visible =0");

                dw_main.Modify("t_39.visible =0");
                dw_main.Modify("expense_branch.visible =0");
                dw_main.Modify("t_27.visible =0");
                dw_main.Modify("expense_branch_1.visible =0");
                dw_main.Modify("b_expense_branch.visible =0");
                dw_main.Modify("t_38.visible =0");
                dw_main.Modify("expense_accid.visible =0");
            }
        }

        private void JsGetitemdescetc()
        {
            try
            {
                int row = Convert.ToInt16(Hdothercltrow.Value);
                string itemetc_code = dw_otherclr.GetItemString(row, "clrothertype_code");
                string sql_prmgrp = "select    slipitemtype_desc    from slucfslipitemtype  where slipitemtype_code = '" + itemetc_code + "'";
                Sdt dt_itemetc = WebUtil.QuerySdt(sql_prmgrp);
                if (dt_itemetc.Next())
                {
                    string item_desc = dt_itemetc.GetString("slipitemtype_desc");
                    dw_otherclr.SetItemString(row, "clrother_desc", item_desc);
                }
            }
            catch { }
        }

        /// <summary>
        /// set รายละเอียด ยอดหักจาก slip เงินเดือน
        /// </summary>
        private void JsGetitemdescmthexp()
        {
            try
            {
                int row = Convert.ToInt16(Hdmthexprow.Value);
                string mthexp_code = dw_mthexp.GetItemString(row, "mthexp_code");
                string sql_mthexp = "select mthexpense_desc from mbucfmthexpense where mthexpense_code = '" + mthexp_code + "'";
                Sdt dt_mthexp = WebUtil.QuerySdt(sql_mthexp);
                if (dt_mthexp.Next())
                {
                    string mthexp_desc = dt_mthexp.GetString("mthexpense_desc");
                    dw_mthexp.SetItemString(row, "mthexp_desc", mthexp_desc);
                }
            }
            catch { }
        }

        /// <summary>
        /// Retrieve ธนาคาร
        /// </summary>
        private void JsExpenseBank()
        {
            try
            {
                String bankCode;
                try { bankCode = dw_main.GetItemString(1, "expense_bank").Trim(); }
                catch { bankCode = "034"; }
                String bankbranch;
                try { bankbranch = dw_main.GetItemString(1, "expense_branch").Trim(); }
                catch { bankbranch = "0000"; }

                DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "sl_loan_requestment.pbl", "");
                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment.pbl", bankCode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Retrieve สาขาธนาคาร
        /// </summary>
        private void JsExpensebankbrRetrieve()
        {
            try
            {
                String bankCode;
                try { bankCode = dw_main.GetItemString(1, "expense_bank").Trim(); }
                catch { bankCode = "034"; }
                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment.pbl", bankCode);
            }
            catch { }
        }

        /// <summary>
        /// open ใบคำขอเก่า
        /// </summary>
        private void JsOpenOldDocNo()
        {
            try
            {
                string ls_reqloandocno = dw_main.GetItemString(1, "loanrequest_docno");
                string ls_CoopControl = state.SsCoopControl;
                string[] arg = new string[2] { ls_CoopControl, ls_reqloandocno };
                DateTime ldtm_now = DateTime.Now;

                txt_reqNo.Text = ls_reqloandocno;
                txt_member_no.Text = member_no;
                Hdcoopid.Value = ls_CoopControl;
                DwUtil.RetrieveDataWindow(dw_main, pbl, null, arg);
                DwUtil.RetrieveDataWindow(dw_clear, pbl, null, arg);
                DwUtil.RetrieveDataWindow(dw_coll, pbl, null, arg);
                DwUtil.RetrieveDataWindow(dw_otherclr, pbl, null, arg);
                DwUtil.RetrieveDataWindow(dw_mthexp, pbl, null, arg);
                tDwMain.Eng2ThaiAllRow();
                string loantype_code = dw_main.GetItemString(1, "loantype_code");

                try
                {
                    RetreiveDDDW();
                    // DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                    DwUtil.RetrieveDDDW(dw_main, "loanobjective_code_1", pbl, loantype_code);
                    if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);

                    JsExpenseBank();
                    JsExpensebankbrRetrieve();
                    dw_main.SetItemString(1, "loantype_code", loantype_code);
                    Decimal loanrequestStatus = dw_main.GetItemDecimal(1, "loanrequest_status");
                    //เปิดให้แก้ไขได้หลังจาก open 
                    if ((loanrequestStatus != 8) && (loanrequestStatus != 81) && (loanrequestStatus != 11))
                    {
                        dw_main.DisplayOnly = true;
                        dw_clear.DisplayOnly = true;
                        dw_coll.DisplayOnly = true;
                        dw_otherclr.DisplayOnly = true;
                    }
                    else
                    {
                        dw_main.DisplayOnly = false;
                        dw_clear.DisplayOnly = false;
                        dw_coll.DisplayOnly = false;
                        dw_otherclr.DisplayOnly = false;
                    }
                }
                catch (Exception ex)
                {
                    //LtServerMessage.Text = WebUtil.ErrorMessage("JsOpenOldDocNo====>" + ex); 
                }
                decimal paymonth_exp = dw_main.GetItemDecimal(1, "paymonth_exp");
                Hdpaymouthexp.Value = Convert.ToString(paymonth_exp);
            }
            catch { }
        }

        /// <summary>
        /// ยกเลิก ใบคำขอกู้
        /// </summary>    
        private void JsCancelRequest()
        {
            String dwXmlMain = dw_main.Describe("DataWindow.Data.XML");
            String cancelID = state.SsUsername;
            String coop_id = state.SsCoopId;
            string cancle_date = state.SsWorkDate.ToShortDateString();
            try
            {
                Decimal loanrequestStatus = dw_main.GetItemDecimal(1, "loanrequest_status");
                string loanreqdocno = dw_main.GetItemString(1, "loanrequest_docno");

                //เปิดให้แก้ไขได้หลังจาก open 
                if ((loanrequestStatus == 8) || (loanrequestStatus == 11) || (loanrequestStatus == 81))
                {
                    string sql_up = "update lnreqloan set loanrequest_status = -9, cancel_id = '" + cancelID + "' where loanrequest_docno = '" + loanreqdocno + "'";
                    WebUtil.ExeSQL(sql_up);
                    //WebUtil.ExeSQL("commit");
                    //LtServerMessage.Text = "ยกเลิกใบคำขอก้เรียบร้อยแล้ว";
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกใบคำขอก้เรียบร้อยแล้ว");
                    dw_main.DisplayOnly = true;
                    dw_clear.DisplayOnly = true;
                    dw_coll.DisplayOnly = true;
                    dw_otherclr.DisplayOnly = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("JsCancelRequest===>" + ex);
            }
        }

        private void JsSetDataList()
        {

        }

        private int JsCheckDataBeforesave()
        {
            //ตรวจค้ำประกัน  wa
            int coll_num = 0;
            string loantype_code = dw_main.GetItemString(1, "loantype_code");
            decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");
            int collrow = dw_coll.RowCount;

            String sqlpro = @" select useman_amt, useshare_flag from lnloantypereqgrt  
                            where loantype_code = '" + loantype_code + "' and money_from <=  " + loanrequest_amt.ToString() + @" 
                                    and  money_to >= " + loanrequest_amt.ToString();
            Sdt dtgrt = WebUtil.QuerySdt(sqlpro);
            if (dtgrt.Next())
            {
                coll_num = Convert.ToInt32(dtgrt.GetDecimal("useman_amt"));
                int coll_share = Convert.ToInt32(dtgrt.GetDecimal("useshare_flag"));

            }
            else { coll_num = 0; }

            if (coll_num > collrow)
            {
                //แสดงข้อความ
                LtServerMessage.Text = WebUtil.ErrorMessage("ท่านป้อนสมาชิกค้ำประกันไม่ครบ กรุณาป้อนให้ครบด้วย ต้องระบุคนค้ำประกัน จำนวน " + coll_num.ToString());
                return 1;
            }
            return 1;
        }

        private void of_setexpensedefault(string as_memno)  //pom : setexpense-default
        {
            string memberNo = as_memno;
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            try
            {
                decimal defaultpaytype = 1;
                String sqlStrdefaultpay = @" SELECT defaultpay_type  
                                    FROM LNLOANTYPE
                                    WHERE LOANTYPE_CODE='" + ls_loantype + "'";

                Sdt dt1 = WebUtil.QuerySdt(sqlStrdefaultpay);
                if (dt1.GetRowCount() < 1) { defaultpaytype = 1; }
                if (dt1.Next())
                {
                    try
                    {
                        defaultpaytype = dt1.GetDecimal("defaultpay_type");
                    }
                    catch
                    {
                        defaultpaytype = 1;
                    }
                }

                if (defaultpaytype == 2)
                {
                    JsGetexpensememno();
                }
                else if (defaultpaytype == 1)  //pom :  pay_by_default_loantyp - เงินสด / โอน =ไปเอาเลขบัญชีจากทะเบียนสมาขิก
                {
                    dw_main.SetItemString(1, "expense_code", "CSH");
                }
            }
            catch { }
        }

        private bool PrintContracConsider(String RequestDocNo)
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("lnreqdocno", iReportArgumentType.String, RequestDocNo);
                iReportBuider report = new iReportBuider(this, "");
                report.AddCriteria("r_ln_print_loan_req_checkpermiss", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void jsChg()
        {
            string member_no = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");


            decimal sum_mthexp = 0;
            decimal mthexp_amt = 0;
            decimal paymonth_exp = 0;
            decimal clear_status = 0;
            for (var i = 1; i <= dw_mthexp.RowCount; i++)
            {
                clear_status = dw_mthexp.GetItemDecimal(i, "clear_status");
                if (clear_status == 0)
                {
                    mthexp_amt = Convert.ToDecimal(float.Parse(dw_mthexp.GetItemDecimal(i, "mthexp_amt").ToString()));
                    sum_mthexp = Convert.ToDecimal(float.Parse(sum_mthexp.ToString()) + float.Parse(mthexp_amt.ToString("0.00")));
                }
            }
            dw_main.SetItemDecimal(1, "paymonth_exp", Convert.ToDecimal(float.Parse(sum_mthexp.ToString("0.00"))));

            of_initloanclr(member_no);
            of_initpaymonthcoop();
            of_setpaymthlnreq();
            of_initpermiss();
            of_calperiodpayment();
            of_initclearshare();
            of_initotherpay_fsv();
        }
        //bee
        private void of_sumclearall()
        {
            Decimal clrother_amt = 0, ldc_rkeepprin = 0;
            Decimal principal_balance = 0, intestimate_amt = 0;
            Decimal sum_clear1 = 0;
            Decimal otherclr_amt = 0;

            Decimal clear_status;
            string ls_chktype;
            int i, j;
            int row_clr = dw_otherclr.RowCount;
            int row_clear = dw_clear.RowCount;
            int row_main = dw_main.RowCount;

            //ls_chkpeiord = dw_main.GetItemString(1, "period_loan");

            if (row_clr > 0)
            {
                for (i = 0; i < row_clr; i++)
                {
                    try
                    {
                        clrother_amt = dw_otherclr.GetItemDecimal(i + 1, "clrother_amt");
                    }
                    catch { clrother_amt = 0; }
                    ls_chktype = dw_otherclr.GetItemString(i + 1, "clrothertype_code");
                    if (ls_chktype != "FSV")
                    {
                        otherclr_amt = otherclr_amt + clrother_amt;
                    }
                }

            }

            if (row_clear > 0)
            {
                for (j = 0; j < row_clear; j++)
                {
                    Decimal loanpayment_type = dw_clear.GetItemDecimal(j + 1, "loanpayment_type");
                    string ls_contno = dw_clear.GetItemString(j + 1, "loancontract_no");
                    try
                    {
                        clear_status = dw_clear.GetItemDecimal(j + 1, "clear_status");
                    }
                    catch { clear_status = 0; }
                    if (clear_status == 1)
                    {
                        try
                        {
                            principal_balance = dw_clear.GetItemDecimal(j + 1, "principal_balance");
                        }
                        catch { principal_balance = 0; }

                        try
                        {
                            ldc_rkeepprin = dw_clear.GetItemDecimal(j + 1, "rkeep_principal");
                        }
                        catch
                        {
                            ldc_rkeepprin = 0;
                        }

                        try
                        {
                            intestimate_amt = dw_clear.GetItemDecimal(j + 1, "intreal_amt");
                        }
                        catch
                        {
                            intestimate_amt = 0;
                        }



                        //sum_clear1 = sum_clear1 + principal_balance + ldc_rkeepprin + intestimate_amt; // ldc_rkeepprin หักใน of_initloanclr แลัว
                        //tomy ยังไงก็ต้องลบ เพราะดึง principal_balance ไม่ใช้ช่องสุทธิ 05/04/2018
                        sum_clear1 = sum_clear1 + principal_balance + intestimate_amt - ldc_rkeepprin;
                    }
                }
            }

            decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");
            decimal intest = dw_main.GetItemDecimal(1, "intestimate_amt");
            decimal netrequest = loanrequest_amt - sum_clear1 - otherclr_amt;// -intest;

            //dw_main.SetItemDecimal(1, "wfcoll2_amt", sumwfcoll_amt);
            //dw_main.SetItemDecimal(1, "subloanclr_amt1", sum_clear1);
            dw_main.SetItemDecimal(1, "subperiod_amt", netrequest);
        }
    }
}

