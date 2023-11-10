using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_coll : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String closeWebDialog;

        public void InitJsPostBack()
        {
            closeWebDialog = WebUtil.JsPostBack(this, "closeWebDialog");
        }

        public void WebDialogLoadBegin()
        {
            String as_colltype = "";
            String xmlHead = "";
            String xmlDetail = "";

           // str_itemchange strList = new str_itemchange();

            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_colldet);
            }
            else
            {
                String coop_id = state.SsCoopControl;// Request["coop_id"].ToString();
                String refCollNo = Request["refCollNo"].ToString();
                string description = Request["description"].ToString();
                string loancolltype_code = Request["loancolltype_code"].ToString();

                as_colltype = Request["collType"].ToString();
                decimal coll_amt = Convert.ToDecimal(Request["coll_amt"].ToString());
                decimal coll_use = Convert.ToDecimal(Request["coll_use"].ToString());
                decimal coll_blance = Convert.ToDecimal(Request["coll_blance"].ToString());
                decimal base_percent = Convert.ToDecimal(Request["base_percent"].ToString());
                string loantype_code;
                try
                {
                    loantype_code = Request["loantype_code"].ToString();
                }
                catch { loantype_code = ""; }
                if (base_percent == 0) { base_percent = 1; }

                string as_work = "", as_birth = "", as_retry = "", as_member = "";
                //   String strData = collType + refCollNo;
                DateTime ldtm_birth = new DateTime();
                DateTime ldtm_member = new DateTime();
                DateTime ldtm_work = new DateTime();
                DateTime ldtm_retry = new DateTime();
                DateTime requestDate = new DateTime();
                Decimal salary_amt = 0,
                        incomeetc_amt = 0;
                try
                {
                    requestDate = state.SsWorkDate; //DateTime.ParseExact(date, "ddMMyyyy", new CultureInfo("th-TH")); 
                }
                catch { requestDate = state.SsWorkDate; }

                DateTime ldtm_loanreceive = requestDate;


                dw_colldet.Reset();
                dw_colldet.InsertRow(0);

                object[] arg = new object[3];
                arg[0] = refCollNo;
                arg[1] = as_colltype;
                arg[2] = coop_id;
                try
                {
                    DwUtil.RetrieveDataWindow(dw_detail, "sl_loan_requestment.pbl", null, arg);
                    // DwUtil.RetrieveDDDW(dw_colldet, "loancolltype_code", "sl_loan_requestment_cen.pbl", null);

                    if (loantype_code != "" && as_colltype == "01")
                    {
                        String loangroup_code = "";
                        string sql = @"select loangroup_code from lnloantype where loantype_code = {0}";
                        sql = WebUtil.SQLFormat(sql, loantype_code);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            loangroup_code = dt.GetString("loangroup_code");
                        }

                        dw_detail.SetFilter("loangroup_code = '" + loangroup_code + "'");
                        dw_detail.Filter();
                    }
                }
                catch
                {

                }
                if (as_colltype == "01")
                {
                    {
                        dw_colldet.Visible = false;
                        dw_collmem.Visible = true;
                        dw_collshare.Visible = false;
                        try
                        {

                            Decimal coll_balance = 0, sharestk_amt = 0, total = 0, total_use = 0;
                            Decimal coll_useamt = 0;
                            Decimal sum_balance, coll_percent;

                            dw_collmem.Reset();
                            dw_collmem.InsertRow(1);
                            dw_collmem.SetItemString(1, "loancolltype_code", loancolltype_code);
                            //DwUtil.RetrieveDDDW(dw_collmem, "loancolltype_code", "sl_loan_requestment_cen.pbl", null);
                            dw_collmem.SetItemString(1, "loancolltype_code_1", loancolltype_code);
                            dw_collmem.SetItemString(1, "description", description);
                            dw_collmem.SetItemString(1, "ref_collno", refCollNo);
                            dw_collmem.SetItemDecimal(1, "collbase_amt", coll_amt);
                            dw_collmem.SetItemDecimal(1, "collused_amt", coll_use);
                            dw_collmem.SetItemDecimal(1, "collbalance_amt", coll_blance);
                            dw_collmem.SetItemDecimal(1, "collbase_percent", base_percent);

                            DwUtil.RetrieveDDDW(dw_collmem, "loancolltype_code_1", "sl_loan_requestment_cen.pbl", null);
                            String sqlstr = @"    SELECT
                                             MBMEMBMASTER.BIRTH_DATE,   
                                             MBMEMBMASTER.MEMBER_DATE,   
                                             MBMEMBMASTER.WORK_DATE,   
                                             MBMEMBMASTER.RETRY_DATE,
                                             MBMEMBMASTER.SALARY_AMOUNT,
                                             MBMEMBMASTER.INCOMEETC_AMT,
                                         MBMEMBMASTER.MEMBTYPE_CODE,   
                                         MBUCFMEMBTYPE.MEMBTYPE_DESC                                     
                                    FROM 
                                         MBMEMBMASTER,   
                                         MBUCFMEMBTYPE  
                                   WHERE  MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE and                                        
                                         ( MBMEMBMASTER.COOP_ID = MBUCFMEMBTYPE.COOP_ID )AND 
                                         ( mbmembmaster.member_no = '" + refCollNo + @"' ) AND  
                                          MBMEMBMASTER.COOP_ID   ='" + coop_id + @"' ";
                            Sdt dt = WebUtil.QuerySdt(sqlstr);//เป็น service
                            if (dt.Next())
                            {
                                string MEMBTYPE_CODE = dt.GetString("MEMBTYPE_CODE");
                                string MEMBTYPE_DESC = dt.GetString("MEMBTYPE_DESC");
                                salary_amt = dt.GetDecimal("SALARY_AMOUNT");
                                incomeetc_amt = dt.GetDecimal("INCOMEETC_AMT");
                                salary_amt += incomeetc_amt;
                                try
                                {

                                    ldtm_birth = dt.GetDate("birth_date");
                                    string dd = ldtm_birth.Day.ToString("00");
                                    string mm = ldtm_birth.Month.ToString("00");
                                    string yyyy = (Convert.ToInt32(ldtm_birth.Year) + 543).ToString();
                                    as_birth = dd + "/" + mm + "/" + yyyy;
                                }
                                catch { }
                                try
                                {
                                    try
                                    {
                                        ldtm_retry = dt.GetDate("RETRY_DATE");
                                    }
                                    catch
                                    {
                                        ///<หาวันที่เกษียณ>
                                        ldtm_retry = Convert.ToDateTime(of_getrerydate(ldtm_birth));// ldtm_retry = wcf.NShrlon.of_calretrydate(state.SsWsPass,ldtm_birth);

                                    }
                                    string dd = ldtm_retry.Day.ToString("00");
                                    string mm = ldtm_retry.Month.ToString("00");
                                    string yyyy = (Convert.ToInt32(ldtm_retry.Year) + 543).ToString();
                                    as_retry = dd + "/" + mm + "/" + yyyy;
                                    // ldtm_retry = shrlonService.CalReTryDate(state.SsWsPass, ldtm_birth);
                                }
                                catch { }
                                try
                                {
                                    ldtm_member = dt.GetDate("member_date");
                                    string dd = ldtm_member.Day.ToString("00");
                                    string mm = ldtm_member.Month.ToString("00");
                                    string yyyy = (Convert.ToInt32(ldtm_member.Year) + 543).ToString();
                                    as_member = dd + "/" + mm + "/" + yyyy;
                                }
                                catch { }
                                try
                                {
                                    ldtm_work = dt.GetDate("work_date");
                                    string dd = ldtm_work.Day.ToString("00");
                                    string mm = ldtm_work.Month.ToString("00");
                                    string yyyy = (Convert.ToInt32(ldtm_work.Year) + 543).ToString();
                                    as_work = dd + "/" + mm + "/" + yyyy;
                                }
                                catch { }
                                dw_collmem.SetItemString(1, "birth_tdate", as_birth);
                                dw_collmem.SetItemString(1, "member_tdate", as_member);
                                dw_collmem.SetItemString(1, "retry_tdate", as_retry);
                                dw_collmem.SetItemString(1, "work_tdate", as_work);
                                dw_collmem.SetItemDecimal(1, "salary_amount", salary_amt);

                                DateTime ldtm_reqloan = DateTime.Now;
                                try
                                {
                                    ///<หาเกษียณอายุ>
                                    decimal retry_age = (ldtm_retry.Year - ldtm_reqloan.Year) * 12;
                                    retry_age += (ldtm_retry.Month - ldtm_reqloan.Month);
                                    if (ldtm_reqloan.Day > ldtm_retry.Day) { retry_age--; }

                                    dw_collmem.SetItemDecimal(1, "retry_age", retry_age);
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


                                    dw_collmem.SetItemDecimal(1, "birth_age", mbage_age);
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

                                    dw_collmem.SetItemDecimal(1, "member_age", member_age);
                                }
                                catch { }
                                try
                                {
                                    ///<หาอายุการการทำงาน>
                                    ///
                                    decimal work_age = (ldtm_reqloan.Year - ldtm_work.Year) * 12;
                                    work_age += (ldtm_reqloan.Month - ldtm_work.Month);
                                    if (ldtm_work.Day > ldtm_reqloan.Day) { work_age--; }

                                    decimal age_year = Math.Truncate(work_age / 12);
                                    decimal age_month = (work_age % 12) / 100;

                                    work_age = age_year + age_month;

                                    dw_collmem.SetItemDecimal(1, "work_age", work_age);
                                }
                                catch { }


                                dw_collmem.SetItemString(1, "membtype_code", MEMBTYPE_CODE);
                                dw_collmem.SetItemString(1, "membtype_desc", MEMBTYPE_DESC);


                            }
                        }
                        catch { dw_collmem.InsertRow(0); }

                    }
                }
                else if ((as_colltype == "03") || (as_colltype == "04"))
                {
                    dw_colldet.Visible = true;
                    dw_collmem.Visible = false;
                    dw_collshare.Visible = false;

                    dw_colldet.Reset();
                    dw_colldet.InsertRow(0);
                    dw_colldet.SetItemString(1, "loancolltype_code", as_colltype);
                    dw_colldet.SetItemString(1, "loancolltype_code_1", as_colltype);
                    dw_colldet.SetItemString(1, "description", description);
                    dw_colldet.SetItemString(1, "ref_collno", refCollNo);
                    dw_colldet.SetItemDecimal(1, "collbase_amt", coll_amt);
                    dw_colldet.SetItemDecimal(1, "collused_amt", coll_use);
                    dw_colldet.SetItemDecimal(1, "collbalance_amt", coll_blance);
                    //base_percent = total / coll_balance;
                    dw_colldet.SetItemDecimal(1, "collbase_percent", base_percent);

                }
                else if (as_colltype == "02")
                {
                    dw_colldet.Visible = false;
                    dw_collmem.Visible = false;
                    dw_collshare.Visible = true;

                    dw_collshare.Reset();
                    dw_collshare.InsertRow(0);
                    dw_collshare.SetItemString(1, "loancolltype_code", as_colltype);
                    dw_collshare.SetItemString(1, "loancolltype_code_1", as_colltype);
                    dw_collshare.SetItemString(1, "description", description);
                    dw_collshare.SetItemString(1, "ref_collno", refCollNo);
                    dw_collshare.SetItemDecimal(1, "collbase_amt", coll_amt);
                    dw_collshare.SetItemDecimal(1, "collused_amt", coll_use);
                    dw_collshare.SetItemDecimal(1, "collbalance_amt", coll_blance);
                    //base_percent = total / coll_balance;
                    dw_collshare.SetItemDecimal(1, "collbase_percent", base_percent);

                    DwUtil.RetrieveDDDW(dw_collmem, "loancolltype_code_1", "sl_loan_requestment_cen.pbl", null);


                }



            }
            // }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "closeWebDialog")
            {
                OnCloseClick();
            }

        }
        private string of_getrerydate(DateTime birthDate)
        {
            string retry_date = "";
            string coop_id = state.SsCoopControl;
            try
            {
                Sdt dt = WebUtil.QuerySdt("select	* from	cmcoopmaster where coop_id ='" + coop_id + "' ");
                // dt.next คือเลื่อนเคอร์เซอร์เพื่อไปหาค่าแถวถัดไป
                if (dt.Next())
                {   //เอาค่า +ปีที่เกษียณ  + วันเกิด
                    int retry_age = dt.GetInt32("retry_age");
                    int retry_month = dt.GetInt32("retry_month");
                    int retry_day = dt.GetInt32("retry_day");

                    int year = birthDate.Year + retry_age;
                    int month = birthDate.Month;
                    int day = birthDate.Day;
                    int loop_day = 0;
                    //ตั้งค่าวันที่สิ้นสุดของแต่ล่ะเดือน

                    if (retry_day == 0)
                    {
                        int[] daysinmonth = new int[12];
                        for (int i = 0; i < 12; i++)
                        {
                            if (i == 1)
                            {
                                daysinmonth[i] = 28;
                            }
                            else
                            {

                                if (i == 0 || i == 2 || i == 4 || i == 6 || i == 7 || i == 9 || i == 11)
                                {
                                    daysinmonth[i] = 31;
                                }
                                else
                                { daysinmonth[i] = 30; }
                            }

                        }
                        for (int i = 0; i < 12; i++)
                        {
                            if (day > daysinmonth[i])
                            {   //เช็ควันที่สิ้นสุดของเดือน กุมภาพันธ์
                                if (i == 1)
                                {

                                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                                    {
                                        day = 29;
                                    }
                                }
                            }
                            else
                            {
                                if (month == i + 1)
                                {
                                    day = daysinmonth[i];
                                }
                            }

                        }
                        if (retry_month != 0)
                        {
                            loop_day = daysinmonth[retry_month - 1];
                            day = loop_day;
                        }
                    }
                    else
                    {
                        day = retry_day;
                    }

                    if (retry_month != 0)
                    {
                        //เช็คเกษียณครบรอบ
                        if (month > retry_month)
                        {
                            year = year + 1;

                        }

                        month = retry_month;
                    }


                    return day.ToString("00") + month.ToString("00") + year.ToString("0000");
                }
                else
                {
                    return retry_date;
                }
            }
            catch
            {
                return retry_date;
            }
        }
        public void WebDialogLoadEnd()
        {


            if (dw_collmem.RowCount > 1)
            {
                DwUtil.DeleteLastRow(dw_collmem);
            }
            if (dw_colldet.RowCount > 1)
            {
                DwUtil.DeleteLastRow(dw_colldet);
            }
            if (dw_collshare.RowCount > 1)
            {
                DwUtil.DeleteLastRow(dw_collshare);
            }

            dw_collmem.SaveDataCache();
            dw_detail.SaveDataCache();
            dw_colldet.SaveDataCache();
            dw_collshare.SaveDataCache();
        }
        public void OnCloseClick()
        {
            HfChkStatus.Value = "1";
        }

    }
}
