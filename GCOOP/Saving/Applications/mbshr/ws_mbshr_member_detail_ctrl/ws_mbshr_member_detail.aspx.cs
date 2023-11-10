using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.IO;
using CoreSavingLibrary;
using System.Data;
using System.Threading;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class ws_mbshr_member_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostSalary { get; set; }
        [JsPostBack]
        public String PostCheckLoan { get; set; }
        [JsPostBack]
        public String PopupReportshr { get; set; }
        [JsPostBack]
        public String PopupReportloan { get; set; }
        [JsPostBack]
        public String PostAdjPeriod { get; set; }

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String popupReport;
        public String outputProcess = "";

        public void InitJsPostBack()
        {
            dsCollall.InitDsCollall(this);
            dsCollwho.InitDsCollwho(this);
            dsDetail.InitDsDetail(this);
            dsEtcpaymonth.InitDsEtepaymonth(this);
            dsExpense.InitDsExpense(this);
            dsGain.InitDsGain(this);
            dsInsurance.InitDsInsurance(this);
            dsKeepdata.InitDsKeepdata(this);
            dsLoan.InitDsLoan(this);
            dsLoan2.InitDsLoan2(this);
            dsMain.InitDsMain(this);
            dsMoneytr.InitDsMoneytr(this);
            dsOtherkeep.InitDsOtherkeep(this);
            dsPauseloan.InitDsPauseloan(this);
            dsShare.InitDsShare(this);
            dsStatus.InitDsStatus(this);
            dsTrnhistory.InitDsTrnhistory(this);
            dsWrt.InitDsWRT(this);

            //mike เพิ่มยอดค้างชำระ
            dsSlipadjmain.InitDsSlipadjmain(this);
            dsSlipadjust.InitDsSlipadjust(this);
            dsDeposit.InitDsDeposit(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMoneytr.DdMoneytrtype();
                //dsStatus.DdAppltype();

                
            }
            //mike เพิ่มส่วนการเช็คยอดหักไม่ได้
            HdSlipAdjust.Value = "";
            HdSlipAdjustDet.Value = "";

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                HdSlipAdjust.Value = "false";

                string ls_member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(ls_member_no);
                ReSet();
                dsCollall.RetrieveCollall(ls_member_no);
                //for (int i = 0; i < dsCollall.RowCount; i++)
                //{
                //    string ls_ref_no = dsCollall.DATA[i].REF_COLLNO;
                dsCollwho.RetrieveCollwho(ls_member_no);
                //}
                dsDetail.RetrieveDetail(ls_member_no);

                string ls_recv_period = "";
                string sql = "select max(recv_period) as recv_period from kptempreceive";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_recv_period = dt.GetString("recv_period");
                }
                dsEtcpaymonth.RetrieveEtepaymonth(ls_member_no, ls_recv_period);
                dsExpense.RetrieveExpense(ls_member_no);
                dsGain.RetrieveGain(ls_member_no);
                dsInsurance.RetrieveInsurance(ls_member_no);
                dsKeepdata.RetrieveKeepdata(ls_member_no);
                dsLoan.RetrieveLoan(ls_member_no);
                dsMoneytr.RetrieveMoneytr(ls_member_no);
                dsOtherkeep.RetrieveOtherkeep(ls_member_no);
                dsPauseloan.RetrievePauseloan(ls_member_no);
                dsShare.RetrieveShare(ls_member_no);
                dsStatus.RetrieveStatus(ls_member_no);
                dsTrnhistory.RetrieveTrnhistory(ls_member_no);

                //dsWrt.RetrieveWrt(ls_member_no);
                
                //mike เพิ่มบัญชีเงินฝาก
                dsDeposit.Retrieve(ls_member_no);

                dsLoan2.DATA[0].check_loan = 1;
                SumTotal();
                dsMoneytr.DdMoneytrtype();
                // dsStatus.DdAppltype();
                ShowImage_Member();
                DateTime ldtm_birth = new DateTime();
                DateTime ldtm_retry = new DateTime();
                string ls_deptaccount_no;
                try
                {
                    // li_cramationstatus = dt.GetInt32("cremation_status");
                    //เลขฌาปนกิจสงเคราะห์
                    string sqldapt = @"select deptaccount_no from wcdeptmaster where member_no ='" + ls_member_no + @"' and wftype_code = '01' "; //เพิ่มเงื่อนไข คือ and wftype_code = '01' โดยดึงมาเฉพาะประเภทของสมาชิกเท่านั้น
                    Sdt dtdapt = WebUtil.QuerySdt(sqldapt);
                    if (dtdapt.Next())
                    {
                        ls_deptaccount_no = dtdapt.GetString("deptaccount_no");
                        dsDetail.DATA[0].deptaccount_no = ls_deptaccount_no;
                    }
                }
                catch { ls_deptaccount_no = ""; }

                try
                {
                    ldtm_birth = dsDetail.DATA[0].BIRTH_DATE;//dw_data.GetItemDate(1, "birth_date");

                }
                catch { }

                try
                {
                    ///<หาวันที่เกษียณ>

                    //ldtm_retry = wcf.InterPreter.CalReTryDate(state.SsConnectionIndex, state.SsCoopControl, ldtm_birth);
                    ldtm_retry = wcf.NShrlon.of_calretrydate(state.SsWsPass, ldtm_birth);
                }
                catch { }
                //try
                //{
                //    dsDetail.DATA[0].RETRY_DATE = ldtm_retry;//dw_data.SetItemDateTime(1, "retry_date", ldtm_retry);
                //}
                //catch { dsDetail.DATA[0].RETRY_DATE = DateTime.Now; }//dw_data.SetItemDateTime(1, "retry_date", DateTime.Now); }

                try
                {
                    ///<หาเกษียณอายุ>
                    //Decimal retry_age = wcf.Busscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, ldtm_retry);
                    Decimal retry_age = wcf.NBusscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now,ldtm_retry);
                    String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                    String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                    int retryagel = Convert.ToInt32(retry_agel) * 12;
                    Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                    //dsDetail.DATA[0].r_age=retryagel + retryager;// dw_data.SetItemDecimal(1, "r_age", retryagel + retryager);
                }
                catch
                {
                   //Decimal retry_age = wcf.Busscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                    Decimal retry_age = wcf.NBusscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                    String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                    String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                    int retryagel = Convert.ToInt32(retry_agel) * 12;
                    Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                    //dw_data.SetItemDecimal(1, "r_age", retryagel + retryager);
                }

                try
                {
                    //mike เพิ่มส่วนการเช็คยอดหักไม่ได้
                    HdSlipAdjust.Value = "";
                    HdSlipAdjustDet.Value = "";
                    CheckSlipAdjust(ls_member_no);
                    dsSlipadjmain.DdRecvperiod(ls_member_no);

                }
                catch { }

            }
            else if (eventArg == PostSalary)
            {
                HdSlipAdjust.Value = "false";

                string ls_member_no = WebUtil.GetMembnoBySalaryid(dsMain.DATA[0].SALARY_ID, state.SsCoopControl);
                dsMain.RetrieveMain(ls_member_no);
                ReSet();
                dsCollall.RetrieveCollall(ls_member_no);
                //for (int i = 0; i < dsCollall.RowCount; i++)
                //{
                //    string ls_ref_no = dsCollall.DATA[i].REF_COLLNO;
                dsCollwho.RetrieveCollwho(ls_member_no);
                //}
                dsDetail.RetrieveDetail(ls_member_no);

                string ls_recv_period = "";
                string sql = "select max(recv_period) as recv_period from kptempreceive where member_no='" + ls_member_no + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_recv_period = dt.GetString("recv_period");
                }
                dsEtcpaymonth.RetrieveEtepaymonth(ls_member_no, ls_recv_period);
                dsExpense.RetrieveExpense(ls_member_no);
                dsGain.RetrieveGain(ls_member_no);
                dsInsurance.RetrieveInsurance(ls_member_no);
                dsKeepdata.RetrieveKeepdata(ls_member_no);
                dsLoan.RetrieveLoan(ls_member_no);
                dsMoneytr.RetrieveMoneytr(ls_member_no);
                dsOtherkeep.RetrieveOtherkeep(ls_member_no);
                dsPauseloan.RetrievePauseloan(ls_member_no);
                dsShare.RetrieveShare(ls_member_no);
                dsStatus.RetrieveStatus(ls_member_no);
                dsTrnhistory.RetrieveTrnhistory(ls_member_no);

                dsWrt.RetrieveWrt(ls_member_no);

                //mike เพิ่มบัญชีเงินฝาก
                dsDeposit.Retrieve(ls_member_no);
                dsLoan2.DATA[0].check_loan = 1;
                SumTotal();
                dsMoneytr.DdMoneytrtype();
                // dsStatus.DdAppltype();
                ShowImage_Member();
                DateTime ldtm_birth = new DateTime();
                DateTime ldtm_retry = new DateTime();
                string ls_deptaccount_no;
                try
                {
                    // li_cramationstatus = dt.GetInt32("cremation_status");
                    //เลขฌาปนกิจสงเคราะห์
                    string sqldapt = @"select deptaccount_no from wcdeptmaster where member_no ='" + ls_member_no + @"' and wftype_code = '01' "; //เพิ่มเงื่อนไข คือ and wftype_code = '01' โดยดึงมาเฉพาะประเภทของสมาชิกเท่านั้น
                    Sdt dtdapt = WebUtil.QuerySdt(sqldapt);
                    if (dtdapt.Next())
                    {
                        ls_deptaccount_no = dtdapt.GetString("deptaccount_no");
                        dsDetail.DATA[0].deptaccount_no = ls_deptaccount_no;
                    }
                }
                catch { ls_deptaccount_no = ""; }

                try
                {
                    ldtm_birth = dsDetail.DATA[0].BIRTH_DATE;//dw_data.GetItemDate(1, "birth_date");

                }
                catch { }

                try
                {
                    ///<หาวันที่เกษียณ>

                    //ldtm_retry = wcf.InterPreter.CalReTryDate(state.SsConnectionIndex, state.SsCoopControl, ldtm_birth);
                    ldtm_retry = wcf.NShrlon.of_calretrydate(state.SsWsPass, ldtm_birth);
                }
                catch { }
                //try
                //{
                //    dsDetail.DATA[0].RETRY_DATE = ldtm_retry;//dw_data.SetItemDateTime(1, "retry_date", ldtm_retry);
                //}
                //catch { dsDetail.DATA[0].RETRY_DATE = DateTime.Now; }//dw_data.SetItemDateTime(1, "retry_date", DateTime.Now); }

                try
                {
                    ///<หาเกษียณอายุ>
                    //Decimal retry_age = wcf.Busscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, ldtm_retry);
                    Decimal retry_age = wcf.NBusscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, ldtm_retry);
                    String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                    String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                    int retryagel = Convert.ToInt32(retry_agel) * 12;
                    Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                    //dsDetail.DATA[0].r_age=retryagel + retryager;// dw_data.SetItemDecimal(1, "r_age", retryagel + retryager);
                }
                catch
                {
                    //Decimal retry_age = wcf.Busscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                    Decimal retry_age = wcf.NBusscom.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                    String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                    String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                    int retryagel = Convert.ToInt32(retry_agel) * 12;
                    Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                    //dw_data.SetItemDecimal(1, "r_age", retryagel + retryager);
                }

                try
                {
                    //mike เพิ่มส่วนการเช็คยอดหักไม่ได้
                    HdSlipAdjust.Value = "";
                    HdSlipAdjustDet.Value = "";
                    CheckSlipAdjust(ls_member_no);
                    dsSlipadjmain.DdRecvperiod(ls_member_no);

                }
                catch { }

            }
            else if (eventArg == PopupReportshr)
            {
                print_report();
            }
            else if (eventArg == PopupReportloan)
            {
                RunProcess();
                Thread.Sleep(1000);
                String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
            }
            else if (eventArg == PostCheckLoan)
            {
                Decimal li_check_loan = dsLoan2.DATA[0].check_loan;
                String ls_member_no = dsMain.DATA[0].MEMBER_NO;
                if (li_check_loan == 0)
                {
                    String sql = @"  
                         SELECT LNCONTMASTER.LOANCONTRACT_NO,   
                                 LNCONTMASTER.LOANAPPROVE_AMT,   
                                 LNCONTMASTER.PERIOD_PAYMENT,   
                                 LNCONTMASTER.STARTCONT_DATE,   
                                 LNCONTMASTER.PRINCIPAL_BALANCE,   
                                 LNCONTMASTER.LAST_PERIODPAY,   
                                 LNCONTMASTER.PAYMENT_STATUS,   
                                 LNCONTMASTER.CONTRACT_STATUS,   
                                 LNCONTMASTER.LOANTYPE_CODE,   
                                 LNCONTMASTER.WITHDRAWABLE_AMT,   
                                 LNCONTMASTER.LASTPAYMENT_DATE,   
                                 LNCONTMASTER.PERIOD_PAYAMT,   
                                 LNCONTMASTER.CONTLAW_STATUS,   
                                 LNCONTMASTER.TRANSFER_STATUS ,
                                 LNCONTMASTER.trnlntocoll_flag 
	                   FROM LNCONTMASTER,   
                                 LNLOANTYPE  
                           WHERE ( LNLOANTYPE.LOANTYPE_CODE = LNCONTMASTER.LOANTYPE_CODE ) and  
                                 ( LNCONTMASTER.MEMCOOP_ID = LNLOANTYPE.COOP_ID ) and  
                                 ( ( LNCONTMASTER.MEMCOOP_ID = {0} )  AND  
                                 ( LNCONTMASTER.MEMBER_NO = {1} )   AND
                                 (LNCONTMASTER.CONTRACT_STATUS <0))  ";

                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
                    DataTable dt = WebUtil.Query(sql);
                    dsLoan.ImportData(dt);
                }
                else
                {
                    String sql = @"  
                         SELECT LNCONTMASTER.LOANCONTRACT_NO,   
                                 LNCONTMASTER.LOANAPPROVE_AMT,   
                                 LNCONTMASTER.PERIOD_PAYMENT,   
                                 LNCONTMASTER.STARTCONT_DATE,   
                                 LNCONTMASTER.PRINCIPAL_BALANCE,   
                                 LNCONTMASTER.LAST_PERIODPAY,   
                                 LNCONTMASTER.PAYMENT_STATUS,   
                                 LNCONTMASTER.CONTRACT_STATUS,   
                                 LNCONTMASTER.LOANTYPE_CODE,   
                                 LNCONTMASTER.WITHDRAWABLE_AMT,   
                                 LNCONTMASTER.LASTPAYMENT_DATE,   
                                 LNCONTMASTER.PERIOD_PAYAMT,   
                                 LNCONTMASTER.CONTLAW_STATUS,   
                                 LNCONTMASTER.TRANSFER_STATUS ,
                                 LNCONTMASTER.trnlntocoll_flag 
	                   FROM LNCONTMASTER,   
                                 LNLOANTYPE  
                           WHERE ( LNLOANTYPE.LOANTYPE_CODE = LNCONTMASTER.LOANTYPE_CODE ) and  
                                 ( LNCONTMASTER.MEMCOOP_ID = LNLOANTYPE.COOP_ID ) and  
                                 ( ( LNCONTMASTER.MEMCOOP_ID = {0} )  AND  
                                 ( LNCONTMASTER.MEMBER_NO = {1} )   AND
                                 (LNCONTMASTER.CONTRACT_STATUS >0))  ";

                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
                    DataTable dt = WebUtil.Query(sql);
                    dsLoan.ImportData(dt);
                }
            }
            else if (eventArg == PostAdjPeriod) {
                try
                {
                    string adjslip_no = "";
                    adjslip_no = dsSlipadjmain.DATA[0].ADJSLIP_NO;
                    dsSlipadjust.RetrieveSlipadjust(adjslip_no);
                }
                catch { }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        public void SumTotal()
        {
            //คิดผลรวมจำนวนเงินหัก รายการหักอื่นๆ
            int ls_row_count = dsOtherkeep.RowCount;
            decimal cp_sum_item_payment = 0;
            for (int i = 0; i < ls_row_count; i++)
            {
                decimal ldc_item_payment = dsOtherkeep.DATA[i].ITEM_PAYMENT;
                cp_sum_item_payment += ldc_item_payment;


            }
            dsOtherkeep.TContract.Text = cp_sum_item_payment.ToString("#,##0.00");

            //คิดผลรวมหักจำนวนเงิน รายการเรียกเก็บประจำเดือน
            int ls_row_count2 = dsEtcpaymonth.RowCount;
            decimal cp_sum_principal_payment = 0;
            decimal cp_sum_interest_payment = 0;
            decimal cp_sum_item_payment2 = 0;
            for (int i = 0; i < ls_row_count2; i++)
            {
                decimal ldc_principal = dsEtcpaymonth.DATA[i].PRINCIPAL_PAYMENT;
                decimal ldc_interest = dsEtcpaymonth.DATA[i].cp_netintpay;
                decimal ldc_item = dsEtcpaymonth.DATA[i].cp_netitempay;
                cp_sum_principal_payment += ldc_principal;
                cp_sum_interest_payment += ldc_interest;
                cp_sum_item_payment2 += ldc_item;


            }
            dsEtcpaymonth.TContract.Text = cp_sum_principal_payment.ToString("#,##0.00");
            dsEtcpaymonth.TContract2.Text = cp_sum_interest_payment.ToString("#,##0.00");
            dsEtcpaymonth.TContract3.Text = cp_sum_item_payment2.ToString("#,##0.00");

        }

        public void ShowImage_Member()
        {
            try
            {
                String memberNo = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO); //WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                string path_profile = "", path_signature = "";
                string sv_path_profile = "", sv_path_signature = "";
                if (memberNo != "" && memberNo != "00000000" && memberNo != null)
                {
                    Img_member_profile.Visible = true;
                    sv_path_profile = Server.MapPath("~/ImageMember/profile/profile_" + memberNo + ".jpg");
                    sv_path_signature = Server.MapPath("~/ImageMember/signature/signature_" + memberNo + ".jpg");
                    //path_profile = state.SsUrl + "ImageMember/profile/profile_" + memberNo + ".jpg";
                    //path_signature= state.SsUrl + "ImageMember/signature/signature_" + memberNo + ".jpg";
                    if (File.Exists(sv_path_profile))
                    {
                        Img_member_profile.ImageUrl = "../../../ImageMember/profile/profile_" + memberNo + ".jpg";
                    }
                    else
                    {
                        Img_member_profile.ImageUrl = "../../../ImageMember/profile/profile_nopic.jpg";
                    }

                    Img_member_signature.Visible = true;
                    if (File.Exists(sv_path_signature))
                    {
                        Img_member_signature.ImageUrl = "../../../ImageMember/signature/signature_" + memberNo + ".jpg";
                    }
                    else
                    {
                        Img_member_signature.ImageUrl = "../../../ImageMember/signature/signature_nopic.jpg";
                    }
                }
                else
                {
                    Img_member_profile.Visible = false;
                    Img_member_signature.Visible = false;
                }



            }
            catch
            {
                Img_member_profile.Visible = false;
                Img_member_signature.Visible = false;
            }
        }
        public void print_report()
        {
            
            try
            {
                String memberNo = dsMain.DATA[0].MEMBER_NO.Trim();
                wcf.NShrlon.of_genintestimate(state.SsWsPass, memberNo, state.SsWorkDate);
                iReportArgument args = new iReportArgument();
                args.Add("as_memno", iReportArgumentType.String, memberNo);
                iReportBuider report = new iReportBuider(this, "");
                report.AddCriteria("r_ln_print_loan_req_checkpermiss_mbshr", "ใบตรวจสอบสิทธิ์", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        public void RunProcess()
        {
            String memberNo = dsMain.DATA[0].MEMBER_NO.Trim();
            wcf.NShrlon.of_genintestimate(state.SsWsPass, memberNo, state.SsWorkDate);

            app = state.SsApplication;
            try
            {
                //gid = Request["gid"].ToString();
                gid = "shrlonchk";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "SHRLONCHK202";
            }
            catch { }


            string coop_id = state.SsCoopControl;
            String start_membno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
            if (start_membno == null || start_membno == "")
            {
                return;
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            //  lnv_helper.AddArgument(start_membno, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                string printer = "PDF";
                String criteriaXML = lnv_helper.PopArgumentsXML();
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;
            PopupReport();
        }

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        public void ReSet()
        {
            dsCollall.ResetRow();
            dsCollwho.ResetRow();
            dsDetail.ResetRow();
            dsEtcpaymonth.ResetRow();
            dsExpense.ResetRow();
            dsGain.ResetRow();
            dsInsurance.ResetRow();
            dsKeepdata.ResetRow();
            dsLoan.ResetRow();
            dsLoan2.ResetRow();
            //dsMain.ResetRow();
            dsMoneytr.ResetRow();
            dsOtherkeep.ResetRow();
            dsPauseloan.ResetRow();
            dsShare.ResetRow();
            dsStatus.ResetRow();
            dsTrnhistory.ResetRow();
        }

        //mike เอาไว้เช็คยอดค้างชำระ
        public void CheckSlipAdjust(string member_no)
        {
            string check = "false";
            string detail = "";
            try
            {
                string sql = @"select sa.ref_recvperiod,sd.* 
from slslipadjustdet sd, slslipadjust sa 
where sa.adjslip_no = sd.adjslip_no
and sa.coop_id = sd.coop_id
and sa.coop_id={0} 
and sa.member_no={1}
and sa.pmx_status=0
and sa.slip_status=1
and sd.bfshrcont_status=1
order by sa.ref_recvperiod,sd.bfmthpay_seqno";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                while (dt.Next())
                {
                    check = "true";
                    if (dt.GetString("slipitemtype_code") == "LON")
                    {
                        detail += "\r\n งวด " + dt.GetString("ref_recvperiod") + " รายการ " + dt.GetString("loancontract_no") + " เงินต้น " + dt.GetDecimal("principal_adjamt").ToString("#,##0.00") + " บาท ดอกเบี้ย " + dt.GetDecimal("interest_adjamt").ToString("#,##0.00") + " บาท";
                    }
                    else if (dt.GetString("slipitemtype_code") == "SHR")
                    {
                        detail += "\r\n งวด " + dt.GetString("ref_recvperiod") + " รายการ " + dt.GetString("slipitem_desc") + "จำนวน " + dt.GetDecimal("item_adjamt").ToString("#,##0.00") + " บาท";

                    }


                }
                HdSlipAdjustDet.Value = detail;
                HdSlipAdjust.Value = check;

            }
            catch { }
        }
    }
}