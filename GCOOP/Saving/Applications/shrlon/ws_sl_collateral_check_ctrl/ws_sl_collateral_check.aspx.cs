using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class ws_sl_collateral_check : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostColltypeCode { get; set; }
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String Postprint { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsCollwho.InitDsCollwho(this);
            dsCollwholnreq.InitDsCollwholnreq(this);
            dsMemcoll.InitDsMemcoll(this);
            dsMemdet.InitDsMemdet(this);
            dsSharedet.InitDsSharedet(this);
            dsDeptdet.InitDsDeptdet(this);
            dsCollmast.InitDsCollmast(this);
            dsCollmemco.InitDsCollmemco(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdLoancolltype();
                dsSharedet.Visible = false;
                dsDeptdet.Visible = false;
                dsCollmast.Visible = false;
                dsCollmemco.Visible = false;
                dsMain.DATA[0].colltype_code_name = "01";
                dsMain.DATA[0].colltype_code = "01";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostColltypeCode)
            {
                CheckColltypeCode();
            }
            else if (eventArg == PostMemberNo)
            {
                CheckColl();
            }
            else if (eventArg == Postprint)
            {
                PrintColl_Click();
            }

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        public void CheckColltypeCode()
        {

            string colltype_code = dsMain.DATA[0].colltype_code_name;

            if (colltype_code == "01")
            {

                dsMemdet.Visible = true;
                dsMemcoll.Visible = true;
                dsSharedet.Visible = false;
                dsDeptdet.Visible = false;
                dsCollmast.Visible = false;
                dsCollmemco.Visible = false;
                dsCollwho.ResetRow();
                dsCollwholnreq.ResetRow();
                dsMemcoll.ResetRow();
                dsMemdet.ResetRow();
                dsSharedet.ResetRow();
                dsMain.DATA[0].collateral_no = "";
                dsMain.DATA[0].collateral_desc = "";

                dsCollwho.TContract.Text = "0.00";
                dsCollwholnreq.TContract.Text = "0.00";

            }
            else if (colltype_code == "02")
            {

                dsMemdet.Visible = false;
                dsMemcoll.Visible = false;
                dsSharedet.Visible = true;
                dsDeptdet.Visible = false;
                dsCollmast.Visible = false;
                dsCollmemco.Visible = false;
                dsCollwho.ResetRow();
                dsCollwholnreq.ResetRow();
                dsMemcoll.ResetRow();
                dsMemdet.ResetRow();
                dsSharedet.ResetRow();
                dsMain.DATA[0].collateral_no = "";
                dsMain.DATA[0].collateral_desc = "";
                dsCollwho.TContract.Text = "0.00";
                dsCollwholnreq.TContract.Text = "0.00";
            }
            else if (colltype_code == "03")
            {

                dsMemdet.Visible = false;
                dsMemcoll.Visible = false;
                dsSharedet.Visible = false;
                dsDeptdet.Visible = true;
                dsCollmast.Visible = false;
                dsCollmemco.Visible = false;
                dsCollwho.ResetRow();
                dsCollwholnreq.ResetRow();
                dsMemcoll.ResetRow();
                dsMemdet.ResetRow();
                dsSharedet.ResetRow();
                dsMain.DATA[0].collateral_no = "";
                dsMain.DATA[0].collateral_desc = "";
                dsCollwho.TContract.Text = "0.00";
                dsCollwholnreq.TContract.Text = "0.00";
            }
            else if (colltype_code == "04")
            {
                dsMemdet.Visible = false;
                dsMemcoll.Visible = false;
                dsSharedet.Visible = false;
                dsDeptdet.Visible = false;
                dsCollmast.Visible = true;
                dsCollmemco.Visible = true;
                dsCollwho.ResetRow();
                dsCollwholnreq.ResetRow();
                dsMemcoll.ResetRow();
                dsMemdet.ResetRow();
                dsSharedet.ResetRow();
                dsCollmast.DdCollmasttype();
                dsMain.DATA[0].collateral_no = "";
                dsMain.DATA[0].collateral_desc = "";
                dsCollwho.TContract.Text = "0.00";
                dsCollwholnreq.TContract.Text = "0.00";
            }

        }
        public void CheckColl()
        {
            string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].collateral_no);
            dsMemdet.RetrieveMemdet(member_no);
            string collTypeCode = dsMain.DATA[0].colltype_code;
            dsCollwho.RetrieveDsCollwho(collTypeCode, member_no);
            dsCollwholnreq.RetrieveDsCollwholnreq(member_no, collTypeCode);

            try
            {
                Dropgurantee(); //alert สถานะงดค้ำประกัน
            }
            catch
            {
            }
            try
            {
                SumTotal();
            }
            catch
            {

            }
            Sumbanlance();

            if (collTypeCode == "01")
            {
                try
                {

                    if (member_no != "" || member_no != null)
                    {


                        string mem_name = dsMemdet.DATA[0].memfull_name;
                        decimal member_type = dsMemdet.DATA[0].MEMBER_TYPE;
                        decimal resign_status = dsMemdet.DATA[0].RESIGN_STATUS;
                        dsMain.DATA[0].collateral_desc = mem_name;
                        JsCalageyearmonth();

                        if (resign_status == 1)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ ได้ลาออกจากสหกรณ์ไปแล้ว กรุณาตรวจสอบ");
                        }
                        else
                        {
                            dsMemcoll.RetrieveMemcoll(member_type);// dw_memcoll1.Retrieve(member_type);
                        }

                        decimal retry_age = dsMemdet.DATA[0].retry_age;
                        if (retry_age <= 0)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ ได้เกษียณอายุไปแล้ว กรุณาตรวจสอบ");

                        }



                        for (int i = 0; i <= dsMemcoll.RowCount; i++)
                        {
                            JsCalmembercoll(i);

                        }

                        try
                        {

                            //dsMemcoll.DATA[ai_row].coll_amt = ldc_maxcredit;

                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }


                    }


                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            else if (collTypeCode == "02")
            {

                dsSharedet.RetrieveDsSharedet(member_no);
                if (dsSharedet.RowCount > 0)
                {
                    string mem_name = "ทุนเรือนหุ้น " + dsSharedet.DATA[0].memfull_name;
                    dsMain.DATA[0].collateral_desc = mem_name;

                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + member_no);
                }
            }
            else if (collTypeCode == "03")
            {
                string collateral_no = dsMain.DATA[0].collateral_no;
                dsDeptdet.RetrieveDsSharedet(collateral_no);
                string memberno = dsMain.DATA[0].collateral_no;
                dsCollwho.RetrieveDsCollwho(collTypeCode, memberno);
                dsCollwholnreq.RetrieveDsCollwholnreq(memberno, collTypeCode);
                SumTotal();
                if (dsDeptdet.RowCount > 0)
                {
                    string mem_name = "บัญชีงเงินฝาก " + dsDeptdet.DATA[0].DEPTACCOUNT_NAME;
                    dsMain.DATA[0].collateral_desc = mem_name;

                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + member_no);
                }
            }
            else if (collTypeCode == "04")
            {
                string collateral_no = dsMain.DATA[0].collateral_no;
                dsCollmast.RetrieveDsCollmast(collateral_no);
                dsCollmemco.RetrieveDsCollmemco(collateral_no);
                dsCollwho.RetrieveDsCollwho(collTypeCode, collateral_no);
                dsCollwholnreq.RetrieveDsCollwholnreq(collateral_no, collTypeCode);
                dsCollmast.DdCollmasttype();
                SumTotal();
                if (dsCollmast.RowCount > 0)
                {
                    string mem_name = dsCollmast.DATA[0].COLLMAST_DESC;
                    dsMain.DATA[0].collateral_desc = mem_name;

                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + member_no);
                }
            }
        }
        public void JsCalageyearmonth()
        {
            try
            {

                DateTime ldtm_birth = DateTime.Now;
                DateTime ldtm_retry = DateTime.Now;
                DateTime ldtm_reqloan = DateTime.Now;
                DateTime ldtm_member = DateTime.Now;
                DateTime ldtm_startwork = DateTime.Now;
                try
                {
                    ldtm_birth = dsMemdet.DATA[0].BIRTH_DATE;

                }
                catch { }

                try
                {
                    ldtm_retry = dsMemdet.DATA[0].RETRY_DATE;
                }
                catch { }

                ldtm_reqloan = state.SsWorkDate;

                try
                {
                    ldtm_member = dsMemdet.DATA[0].MEMBER_DATE;
                }
                catch { }
                try
                {
                    ///<หาเกษียณอายุ>
                    decimal retry_age = (ldtm_retry.Year - ldtm_reqloan.Year) * 12;
                    retry_age += (ldtm_retry.Month - ldtm_reqloan.Month);
                    if (ldtm_reqloan.Day > ldtm_retry.Day) { retry_age--; }
                    decimal age_year = Math.Truncate(retry_age / 12);
                    decimal age_month = (retry_age % 12) / 100;

                    retry_age = age_year + age_month;

                    dsMemdet.DATA[0].retry_age = retry_age;


                }
                catch { }

                try
                {
                    ///<หาอายุ-ของสมาชิก>
                    ///
                    decimal mbage_age = (ldtm_reqloan.Year - ldtm_birth.Year) * 12;
                    mbage_age += (ldtm_reqloan.Month - ldtm_birth.Month);
                    if (ldtm_birth.Day > ldtm_reqloan.Day) { mbage_age--; }
                    decimal age_year = Math.Truncate(mbage_age / 12);
                    decimal age_month = (mbage_age % 12) / 100;

                    mbage_age = age_year + age_month;


                    dsMemdet.DATA[0].birth_age = mbage_age;
                }
                catch { }

                try
                {
                    ///<หาอายุการเป็นสมาชิก>
                    ///
                    decimal member_age = (ldtm_reqloan.Year - ldtm_member.Year) * 12;
                    member_age += (ldtm_reqloan.Month - ldtm_member.Month);
                    if (ldtm_member.Day > ldtm_reqloan.Day) { member_age--; }

                    decimal age_year = Math.Truncate(member_age / 12);
                    decimal age_month = (member_age % 12) / 100;


                    member_age = age_year + age_month;

                    dsMemdet.DATA[0].member_age = member_age;
                }
                catch { }

                try
                {
                    ldtm_startwork = dsMemdet.DATA[0].WORK_DATE;//dw_memdet1.GetItemDateTime(1, "work_date");
                }
                catch { }
                try
                {
                    decimal workage_age = (ldtm_reqloan.Year - ldtm_startwork.Year) * 12;
                    workage_age += (ldtm_reqloan.Month - ldtm_startwork.Month);
                    if (ldtm_startwork.Day > ldtm_reqloan.Day) { workage_age--; }
                    decimal age_year = Math.Truncate(workage_age / 12);
                    decimal age_month = (workage_age % 12) / 100;

                    workage_age = age_year + age_month;

                    dsMemdet.DATA[0].work_age = workage_age;
                }
                catch { }



            }
            catch
            {

            }
        }
        public void JsCalmembercoll(int ai_row)
        {
            try
            {

                string memcoop_id = state.SsCoopControl;

                decimal ldc_maxcredit = 0;
                decimal ldc_collcredit = 0;
                decimal ldc_salary = dsMemdet.DATA[0].SALARY_AMOUNT;
                decimal lastshare_period = dsMemdet.DATA[0].LAST_PERIOD;


                DateTime loanrequest_date = state.SsWorkDate;
                string mangrtpermgrp_code = dsMemcoll.DATA[ai_row].MANGRTPERMGRP_CODE;
                string ls_memcoopid = state.SsCoopControl;

                String sqlStrcredit = @"  SELECT MANGRTPERMGRP_CODE,   
                                             SEQ_NO,   
                                             STARTSHARE_AMT,   
                                             ENDSHARE_AMT,   
                                             STARTMEMBER_TIME,   
                                             ENDMEMBER_TIME,   
                                             multiple_share,   
                                             multiple_salary,    
                                             MAXGRT_AMT,   
                                             START_SALARY,   
                                             END_SALARY  
                                        FROM LNGRPMANGRTPERMDET   
                                        WHERE  MANGRTPERMGRP_CODE ='" + mangrtpermgrp_code + @"'  
                                             and STARTMEMBER_TIME <=" + lastshare_period + " and ENDMEMBER_TIME >" + lastshare_period + @" 
                                    ORDER BY  MANGRTPERMGRP_CODE,   
                                             SEQ_NO ";
                Sdt dt = WebUtil.QuerySdt(sqlStrcredit);
                while (dt.Next())
                {
                    ldc_maxcredit = ldc_salary * dt.GetDecimal("multiple_salary");
                    ldc_collcredit = dt.GetDecimal("MAXGRT_AMT");
                    if (ldc_maxcredit > ldc_collcredit) { ldc_maxcredit = ldc_collcredit; }
                }

                dsMemcoll.DATA[ai_row].coll_amt = ldc_maxcredit;

            }
            catch
            {

            }
        }
        public void SumTotal()
        {
            //คิดผลรวมเงินที่ใช้ค้ำ รายการค้ำประกันสัญญา
            int row_count = dsCollwho.RowCount;
            decimal cp_sumcp_colluse = 0;
            for (int i = 0; i < row_count; i++)
            {
                decimal cp_colluse = dsCollwho.DATA[i].COLLACTIVE_AMT;
                cp_sumcp_colluse += cp_colluse;


            }
            dsCollwho.TContract.Text = cp_sumcp_colluse.ToString("#,##0.00");
            //คิดผลรวมเงินที่ใช้ค้ำ รายการค้ำประกันใบคำขอ
            int row = dsCollwholnreq.RowCount;
            decimal cp_sumcp_collamt = 0;
            for (int i = 0; i < row; i++)
            {
                decimal cp_collamt = dsCollwholnreq.DATA[i].cp_collamt;
                cp_sumcp_collamt += cp_collamt;


            }




            dsCollwholnreq.TContract.Text = cp_sumcp_collamt.ToString("#,##0.00");
        }

        public void Sumbanlance()
        {
            string memcoop_id = state.SsCoopControl;

            decimal ldc_maxcredit = 0;
            decimal ldc_collcredit = 0;
            decimal ldc_salary = dsMemdet.DATA[0].SALARY_AMOUNT;
            decimal lastshare_period = dsMemdet.DATA[0].LAST_PERIOD;


            DateTime loanrequest_date = state.SsWorkDate;
            string mangrtpermgrp_code = "10";

            string ls_memcoopid = state.SsCoopControl;

            String sqlStrcredit = @"  SELECT MANGRTPERMGRP_CODE,   
                                             SEQ_NO,   
                                             STARTSHARE_AMT,   
                                             ENDSHARE_AMT,   
                                             STARTMEMBER_TIME,   
                                             ENDMEMBER_TIME,   
                                             multiple_share,   
                                             multiple_salary,   
                                             MAXGRT_AMT,   
                                             START_SALARY,   
                                             END_SALARY  
                                        FROM LNGRPMANGRTPERMDET   
                                        WHERE  MANGRTPERMGRP_CODE ='" + mangrtpermgrp_code + @"'  
                                             and STARTMEMBER_TIME <=" + lastshare_period + " and ENDMEMBER_TIME >" + lastshare_period + @" 
                                    ORDER BY  MANGRTPERMGRP_CODE,   
                                             SEQ_NO ";
            Sdt dt = WebUtil.QuerySdt(sqlStrcredit);
            if (dt.Next())
            {
                ldc_maxcredit = ldc_salary * dt.GetDecimal("multiple_salary");
                ldc_collcredit = dt.GetDecimal("MAXGRT_AMT");
                if (ldc_maxcredit > ldc_collcredit)
                {

                    ldc_maxcredit = ldc_collcredit;
                }

                //-------------------------สิทธิคำสูงสุด-----------------------//
                //คิดผลรวมเงินที่ใช้ค้ำ รายการค้ำประกันสัญญา
                int row_count = dsCollwho.RowCount;
                decimal cp_sumcp_colluse = 0;
                for (int i = 0; i < row_count; i++)
                {
                    decimal principal_balance = dsCollwho.DATA[i].PRINCIPAL_BALANCE;
                    decimal collactive_percent = dsCollwho.DATA[i].COLLACTIVE_PERCENT;
                    decimal collactive_amt = dsCollwho.DATA[i].COLLACTIVE_AMT;
                    //decimal cp_colluse = dsCollwho.DATA[i].cp_colluse;
                    decimal cp_colluse = collactive_amt;// principal_balance * (collactive_percent / 100);
                    //dsCollwho.DATA[i].cp_colluse = cp_colluse;
                    cp_sumcp_colluse += cp_colluse;


                }
                string sumstrg = cp_sumcp_colluse.ToString("#,##0.00");
                decimal sumdec = Convert.ToDecimal(sumstrg);
                Decimal ldc_sumcolluse = 0;
                try
                {
                    ldc_sumcolluse = dsCollwho.DATA[0].cp_sumcp_colluse;
                }
                catch
                {
                    ldc_sumcolluse = 0;
                }
                dsMemdet.DATA[0].cp_collmax_amt = ldc_maxcredit;
                dsMemdet.DATA[0].cp_collbalance_amt = ldc_maxcredit - sumdec;

                //-------------------------สิทธิคำสูงสุด-----------------------//
            }
        }

        public void Dropgurantee()
        {
            string coop_id = state.SsCoopControl;
            string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].collateral_no);

            String sql = @"SELECT DROPGURANTEE_FLAG FROM MBMEMBMASTER WHERE COOP_ID = {0} AND MEMBER_NO = {1}";
            sql = WebUtil.SQLFormat(sql, coop_id, member_no);
            Sdt dt = WebUtil.QuerySdt(sql);

            if (dt.Next())
            {
                int dropgurantee = dt.GetInt32("dropgurantee_flag");

                if (dropgurantee == 1)
                {
                    //this.SetOnLoadedScript("alert('สมาชิกมีสถานะ งดค้ำประกัน')");

                    LtServerMessage2.Text = WebUtil.WarningMessage("สมาชิกมีสถานะ งดค้ำประกัน");

                }
            }
        }
        public void PrintColl_Click()
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                args.Add("as_membno", iReportArgumentType.String, WebUtil.MemberNoFormat(dsMain.DATA[0].collateral_no));
                args.Add("as_colltype", iReportArgumentType.String, dsMain.DATA[0].colltype_code_name);

                iReportBuider report = new iReportBuider(this, "กำลังสร้างรายงานสิทธิการค้ำประกัน");
                report.AddCriteria("r_sl_etc_coll", "รายงานสิทธิการค้ำประกัน", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

    }
}