using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNBusscom;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_collateral_check : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;


        protected String jsPostCheckColl;
        protected String openNew;
        protected String refresh;

        String pbl = "sl_collateral_check.pbl";

        public void InitJsPostBack()
        {

            jsPostCheckColl = WebUtil.JsPostBack(this, "jsPostCheckColl");
            openNew = WebUtil.JsPostBack(this, "openNew");
            refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebSheetLoadBegin()
        {
            String collType = "";
            dw_memcoll1.Visible = true;
            dw_memdet1.Visible = true;
            dw_sharedet2.Visible = true;
            dw_deptdet3.Visible = true;
            dw_collmast4.Visible = true;
            dw_collmemco4.Visible = true;
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);

            }
            else
            {
                try
                {
                    dw_main.Reset();
                    dw_main.InsertRow(0);

                    dw_main.SetItemString(1, "colltype_code", "01");
                }
                catch
                {

                }

            }

            try
            {
                try
                {
                    collType = dw_main.GetItemString(1, "colltype_code");
                    String collType_1 = dw_main.GetItemString(1, "colltype_code_1");
                }
                catch
                {
                    collType = "01";
                }
                if ((collType == "01") || (collType == "คนค้ำประกัน"))
                {
                    dw_memcoll1.Visible = true;
                    dw_memdet1.Visible = true;
                    dw_sharedet2.Visible = false;
                    dw_deptdet3.Visible = false;
                    dw_collmast4.Visible = false;
                    dw_collmemco4.Visible = false;
                    dw_collwho.Visible = true;
                    Label2.Visible = true;
                    dw_memcoll1.Reset();
                    dw_memcoll1.InsertRow(0);

                    dw_memdet1.Reset();
                    dw_memdet1.InsertRow(0);

                }
                else if ((collType == "02") || (collType == "หุ้นค้ำประกัน"))
                {
                    dw_memcoll1.Visible = false;
                    dw_memdet1.Visible = false;
                    dw_sharedet2.Visible = true;
                    dw_deptdet3.Visible = false;
                    dw_collmast4.Visible = false;
                    dw_collmemco4.Visible = false;

                    dw_collwho.Visible = false;

                    Label2.Visible = false;
                    dw_sharedet2.Reset();
                    dw_sharedet2.InsertRow(0);
                }
                else if ((collType == "03") || (collType == "เงินฝากค้ำประกัน"))
                {
                    dw_memcoll1.Visible = false;
                    dw_memdet1.Visible = false;
                    dw_sharedet2.Visible = false;
                    dw_deptdet3.Visible = true;
                    dw_collmast4.Visible = false;
                    dw_collmemco4.Visible = false;

                    dw_collwho.Visible = false;

                    Label2.Visible = false;
                    dw_deptdet3.Reset();
                    dw_deptdet3.InsertRow(0);
                }
                else if ((collType == "04") || (collType == "หลักทรัพย์ค้ำประกัน"))
                {
                    dw_memcoll1.Visible = false;
                    dw_memdet1.Visible = false;
                    dw_sharedet2.Visible = false;
                    dw_deptdet3.Visible = false;
                    dw_collmast4.Visible = true;
                    dw_collmemco4.Visible = true;

                    dw_collwho.Visible = false;

                    Label2.Visible = false;
                    dw_collmast4.Reset();
                    dw_collmast4.InsertRow(0);
                    dw_collmemco4.Reset();
                    dw_collmemco4.InsertRow(0);
                }
                else if ((collType == "05") || (collType == "พันธบัตรรัฐบาล"))
                {
                    dw_memcoll1.Visible = true;
                    dw_memdet1.Visible = true;
                    dw_sharedet2.Visible = false;
                    dw_deptdet3.Visible = false;
                    dw_collmast4.Visible = false;
                    dw_collmemco4.Visible = false;

                    dw_memcoll1.Reset();
                    dw_memcoll1.InsertRow(0);

                    dw_collwho.Visible = false;

                    Label2.Visible = false;
                    dw_memdet1.Reset();
                    dw_memdet1.InsertRow(0);
                }
                else
                {
                    dw_memcoll1.Visible = false;
                    dw_memdet1.Visible = false;
                    dw_sharedet2.Visible = false;
                    dw_deptdet3.Visible = false;
                    dw_collmast4.Visible = false;
                    dw_collmemco4.Visible = false;

                    dw_collwho.Visible = false;
                    Label2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            DwUtil.RetrieveDDDW(dw_main, "colltype_code", "sl_loan_requestment.pbl", null);
            //DwUtil.RetrieveDDDW(dw_main, "colltype_code", "sl_collateral_check.pbl", null);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostCheckColl")
            {
                JsPostCheckColl();
            }
            if (eventArg == "openNew")
            {
                OpenNew();
            }
            if (eventArg == "refresh")
            {
                Refresh();
            }

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
        }

        public void Refresh() { }
        public void OpenNew()
        {
            try
            {
                dw_main.Reset();
                dw_main.InsertRow(0);

                dw_main.SetItemString(1, "colltype_code", "01");
            }
            catch
            {

            }
        }
        public void JsCalmembercoll(int ai_row)
        {
            try
            {

                string memcoop_id = state.SsCoopId;

                decimal ldc_maxcredit = 0;
                decimal ldc_collcredit = 0;
                decimal ldc_salary = dw_memdet1.GetItemDecimal(1, "salary_amount");
                decimal lastshare_period = dw_memdet1.GetItemDecimal(1, "last_period");


                DateTime loanrequest_date = state.SsWorkDate;
                string mangrtpermgrp_code = dw_memcoll1.GetItemString(ai_row, "mangrtpermgrp_code");//dw_main.GetItemString(1,"colltype_code_1");
                string ls_memcoopid = state.SsCoopId;

                String sqlStrcredit = @"  SELECT MANGRTPERMGRP_CODE,   
                                             SEQ_NO,   
                                             STARTSHARE_AMT,   
                                             ENDSHARE_AMT,   
                                             STARTMEMBER_TIME,   
                                             ENDMEMBER_TIME,   
                                             PERCENTSHARE,   
                                             PERCENTSALARY,   
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
                    ldc_maxcredit = ldc_salary * dt.GetDecimal("PERCENTSALARY");
                    ldc_collcredit = dt.GetDecimal("MAXGRT_AMT");
                    if (ldc_maxcredit > ldc_collcredit) { ldc_maxcredit = ldc_collcredit; }
                }

                dw_memcoll1.SetItemDecimal(ai_row, "coll_amt", ldc_maxcredit);

            }
            catch
            {

            }

        }
        private void JsCalageyearmonth()
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
                    ldtm_birth = dw_memdet1.GetItemDateTime(1, "birth_date");
                }
                catch { }

                try
                {
                    ldtm_retry = dw_memdet1.GetItemDateTime(1, "retry_date");
                }
                catch { }

                ldtm_reqloan = state.SsWorkDate;
               
                try
                {
                    ldtm_member = dw_memdet1.GetItemDateTime(1, "member_date");
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

                    dw_memdet1.SetItemDecimal(1, "retry_age", retry_age);

                    
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


                    dw_memdet1.SetItemDecimal(1, "birth_age", mbage_age);
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

                    dw_memdet1.SetItemDecimal(1, "member_age", member_age);
                }
                catch { }

                try
                {
                    ldtm_startwork = dw_memdet1.GetItemDateTime(1, "work_date");
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

                    dw_memdet1.SetItemDecimal(1, "work_age", workage_age);
                }
                catch { }



            }
            catch
            {

            }

        }
        public void JsPostCheckColl()
        {
            string concoll_no = Hdrefcoll_no.Value; //dw_main.GetItemString(1, "collateral_no");
            String collTypeCode = dw_main.GetItemString(1, "colltype_code");
            concoll_no = WebUtil.MemberNoFormat(concoll_no);
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_collwho.SetTransaction(sqlca);
            dw_memdet1.SetTransaction(sqlca);
            dw_memcoll1.SetTransaction(sqlca);
            dw_collwholnreq.SetTransaction(sqlca);

            string[] arg = new string[2] { collTypeCode, concoll_no };

            DwUtil.RetrieveDataWindow(dw_collwho, pbl, null, arg );
            DwUtil.RetrieveDataWindow(dw_collwholnreq, pbl, null, arg );
           
         
            if (collTypeCode == "01")
            {

                try
                {
                   
                    if (concoll_no != "" || concoll_no != null)
                    {
                        
                           
                        if (  dw_memdet1.Retrieve(concoll_no) > 0)
                        {
                            string mem_name = dw_memdet1.GetItemString(1, "memfull_name");
                            decimal member_type = dw_memdet1.GetItemDecimal(1, "member_type");
                            decimal resign_status = dw_memdet1.GetItemDecimal(1, "resign_status");
                            dw_main.SetItemString(1, "collateral_desc", mem_name);
                            JsCalageyearmonth();

                            if (resign_status == 1)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ ได้ลาออกจากสหกรณ์ไปแล้ว กรุณาตรวจสอบ");
                            }
                            else
                            {
                                dw_memcoll1.Retrieve(member_type);
                            }

                            decimal retry_age = dw_memdet1.GetItemDecimal(1, "retry_age");
                            if (retry_age <= 0) {
                                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ ได้เกษียณอายุไปแล้ว กรุณาตรวจสอบ");
                                
                            }
                             
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + concoll_no);
                        }

                        for (int i = 1; i <= dw_memcoll1.RowCount; i++)
                        {
                            JsCalmembercoll(i);

                        }

                    }


                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            else if (collTypeCode == "02")
            {
                DwUtil.RetrieveDataWindow(dw_sharedet2, pbl, null, concoll_no);
                if (dw_sharedet2.RowCount > 0)
                {
                    string mem_name = dw_sharedet2.GetItemString(1, "memfull_name");
                    dw_main.SetItemString(1, "collateral_desc", mem_name);
                }
                else {
                    LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + concoll_no);
                }
            }
            else if (collTypeCode == "03")
            {

                DwUtil.RetrieveDataWindow(dw_deptdet3, pbl, null, concoll_no);
                if (dw_deptdet3.RowCount > 0)
                {
                    //string mem_name = dw_sharedet2.GetItemString(1, "memfull_name");
                    //dw_main.SetItemString(1, "collateral_desc", mem_name);
                }
                else
                {
                    //LtServerMessage.Text = WebUtil.ErrorMessage(" ไม่พบข้อมลสมาชิก ทะเบียน " + concoll_no);
                }

            }
            else if (collTypeCode == "04")
            {
                try
                {
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
        }


    }
}
