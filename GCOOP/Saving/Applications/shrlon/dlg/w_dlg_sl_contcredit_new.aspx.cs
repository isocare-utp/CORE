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
using CoreSavingLibrary.WcfNShrlon;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using CoreSavingLibrary.WcfNBusscom;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_contcredit_new : PageWebDialog, WebDialog
    {
        private WebState state;
        private DwTrans sqlca;
        private String pbl = "sl_loancredit.pbl";
        string memno = "";
        Decimal birth_age;
        Decimal member_age;
        private DwThDate tdwMain;
        private n_shrlonClient shrlonService;
        private n_busscomClient BusscomService;
        protected String jsGetMemberInfo;
        protected string jsmaxcreditperiod;
        protected string savewebsheet;
        protected string jsContPeriod;
        protected string jsChecksalabal;
        protected string jsExpensebankbrRetrieve;
        protected string jsSetBank;
        protected string jsGetexpensememno;
        protected string jsExpenseBank;
        protected string jsSetMonthpayCoop;
        Sta ta;
        Sdt dt;
        public void InitJsPostBack()
        {

            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsGetexpensememno = WebUtil.JsPostBack(this, "jsGetexpensememno");
            jsGetMemberInfo = WebUtil.JsPostBack(this, "jsGetMemberInfo");
            savewebsheet = WebUtil.JsPostBack(this, "savewebsheet");
            jsmaxcreditperiod = WebUtil.JsPostBack(this, "jsmaxcreditperiod");
            jsContPeriod = WebUtil.JsPostBack(this, "jsContPeriod");
            jsChecksalabal = WebUtil.JsPostBack(this, "jsChecksalabal");
            jsExpensebankbrRetrieve = WebUtil.JsPostBack(this, "jsExpensebankbrRetrieve");
            jsSetBank = WebUtil.JsPostBack(this, "jsSetBank");
            jsSetMonthpayCoop = WebUtil.JsPostBack(this, "jsSetMonthpayCoop");
            tdwMain = new DwThDate(dw_main, this);
            tdwMain.Add("operate_date", "operate_tdate");
            tdwMain.Add("startcont_date", "startcont_tdate");
            tdwMain.Add("expirecont_date", "expirecont_tdate");

        }

        public void WebDialogLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                BusscomService = wcf.NBusscom;

            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            state = new WebState(Session, Request);

            sqlca = new DwTrans();
            if (IsPostBack)
            {
                try
                {
                    dw_main.RestoreContext();
                    dw_detail.RestoreContext();
                }
                catch { }
            }
            try
            {
                memno = Request["memno"].ToString();
            }
            catch { }

            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);


                DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);

                DwUtil.RetrieveDDDW(dw_main, "loanrcv_code", pbl, null);
                dw_main.SetItemString(1, "loanrcv_code", "CBT");
                DwUtil.RetrieveDDDW(dw_main, "loanrcv_bank_1", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "loanrcv_branch_1", pbl, "034");
                dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
                tdwMain.Eng2ThaiAllRow();

                JsGetMemberInfo();

            }

        }
        private void JsExpenseBank()
        {
            try
            {

                //wa
                String bankCode;
                try { bankCode = dw_main.GetItemString(1, "loanrcv_bank").Trim(); }
                catch { bankCode = "034"; }
                String bankbranch;
                try { bankbranch = dw_main.GetItemString(1, "loanrcv_branch").Trim(); }
                catch { bankbranch = "0000"; }
                dw_main.SetItemString(1, "loanrcv_branch", "");
                DataWindowChild dwExpenseBranch = dw_main.GetChild("loanrcv_branch_1");
                DwUtil.RetrieveDDDW(dw_main, "loanrcv_branch_1", "sl_loancredit.pbl", bankCode);
                //dwExpenseBranch.SetFilter("CMUCFBANKBRANCH.bank_code ='" + bankCode + "'");
                //dwExpenseBranch.Filter();
                //dw_main.SetItemDouble(1, "retrive_bk_branchflag", 1);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void JsExpensebankbrRetrieve()
        {
            try
            {

                String bankCode;
                try { bankCode = dw_main.GetItemString(1, "loanrcv_bank").Trim(); }
                catch { bankCode = "034"; }
                DwUtil.RetrieveDDDW(dw_main, "loanrcv_branch_1", "sl_loancredit.pbl", bankCode);

            }
            catch { }


        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsGetMemberInfo")
            {
                JsGetMemberInfo();

            }
            else if (eventArg == "savewebsheet")
            {
                SaveWebSheet();
            }
            else if (eventArg == "jsmaxcreditperiod")
            {
                Jsmaxcreditperiod();
            }
            else if (eventArg == "jsContPeriod")
            {
                JsContPeriod();
            }
            else if (eventArg == "jsChecksalabal")
            {
                JsChecksalabal();
            }
            else if (eventArg == "jsExpensebankbrRetrieve")
            {
                JsExpensebankbrRetrieve();
            }
            else if (eventArg == "jsSetBank")
            {
                dw_main.SetItemString(1, "loanrcv_bank", "");
                dw_main.SetItemString(1, "loanrcv_branch", "");
                dw_main.SetItemString(1, "loanrcv_accid", "");
                JsSetBank();
            }
            else if (eventArg == "jsGetexpensememno")
            {
                dw_main.SetItemString(1, "loanrcv_bank", "");
                dw_main.SetItemString(1, "loanrcv_branch", "");
                dw_main.SetItemString(1, "loanrcv_accid", "");
                JsGetexpensememno();
            }
            else if (eventArg == "jsExpenseBank")
            {
               
                JsExpenseBank();
            }
            else if (eventArg == "jsSetMonthpayCoop")
            {
                JsSetMonthpayCoop();
            }
            
        }
      

        private void JsGetexpensememno()
        {
            try
            {
                string strsql = @"select expense_code,expense_bank, expense_branch, expense_accid from mbmembmaster where coop_id = {0} and member_no = {1}";
                strsql = WebUtil.SQLFormat(strsql, state.SsCoopControl, dw_main.GetItemString(1, "member_no"));
                try
                {
                    Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                    if (dtloanrcv.GetRowCount() <= 0)
                    {

                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + memno);
                    }
                    if (dtloanrcv.Next())
                    {

                        string loanrcv_code = dtloanrcv.GetString("expense_code");
                        string loanrcv_bank = dtloanrcv.GetString("expense_bank");
                        string loanrcv_branch = dtloanrcv.GetString("expense_branch");
                        string loanrcv_accid = dtloanrcv.GetString("expense_accid");

                        if (loanrcv_code != null)
                        {
                            dw_main.SetItemString(1, "loanrcv_code", loanrcv_code);
                            dw_main.SetItemString(1, "loanrcv_bank", loanrcv_bank);
                            dw_main.SetItemString(1, "loanrcv_branch", loanrcv_branch);
                            dw_main.SetItemString(1, "loanrcv_accid", loanrcv_accid);
                        }
                        else
                        {
                            dw_main.SetItemString(1, "loanrcv_code", "CBT");
                        }

                        //    if (loanrcv_code == "CBT" && loanrcv_bank.Length > 2)
                        //    {
                        //        string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                        //        Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                        //        string bankbranch = "";
                        //        if (dtk.Next())
                        //        {
                        //            bankbranch = dtk.GetString("branch_name").Trim();
                        //            dw_main.SetItemString(1, "bank_branch", bankbranch);
                        //            dw_main.SetItemDouble(1, "retrive_bk_branchflag", 0);
                        //            // dw_main.SetItemString(1, "expense_branch_1", bankbranch);
                        //           
                        //        }
                        //    }
                        //}
                        //JsExpenseBank();
                        JsExpensebankbrRetrieve();
                    }
                }
                catch { }

            }
            catch
            {
            }

        }
        private void JsSetBank()
        {
            string loanrcv_code = dw_main.GetItemString(1, "loanrcv_code");
            if ((loanrcv_code == "CHQ") || (loanrcv_code == "TRN") || (loanrcv_code == "CBT") || (loanrcv_code == "DRF") || (loanrcv_code == "TBK"))
            {
                dw_main.Modify("t_18.visible =1");
                dw_main.Modify("loanrcv_bank.visible =1");
                dw_main.Modify("loanrcv_accid.visible =1");
                dw_main.Modify("loanrcv_bank_1.visible =1");

                dw_main.Modify("t_17.visible =1");
                dw_main.Modify("loanrcv_branch.visible =1");
                dw_main.Modify("t_16.visible =1");
                dw_main.Modify("loanrcv_branch_1.visible =1");
               
              
                
                try
                {

                    DwUtil.RetrieveDDDW(dw_main, "loanrcv_bank_1", "sl_loancredit.pbl", null);
                    //DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment_cen.pbl", "006");
                    //DataWindowChild dwExpenseBranch = dw_main.GetChild("expense_branch_1");
                    //DataWindowChild dwExpenseBank = dw_main.GetChild("expense_bank_1");
                    // DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                    // JsGetexpensememno();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((loanrcv_code == "CSH") || (loanrcv_code == "BEX") || (loanrcv_code == "MON") || (loanrcv_code == "MOS") || (loanrcv_code == "MOO"))
            {
                dw_main.Modify("t_18.visible =0");
                dw_main.Modify("loanrcv_bank.visible =0");
                dw_main.Modify("loanrcv_accid.visible =0");
                dw_main.Modify("loanrcv_bank_1.visible =0");

                dw_main.Modify("t_17.visible =0");
                dw_main.Modify("loanrcv_branch.visible =0");
                dw_main.Modify("t_16.visible =0");
                dw_main.Modify("loanrcv_branch_1.visible =0");
               
              
                
            }
            if (loanrcv_code == "CBT")
            {
                JsGetexpensememno();
            }
        }

        private void JsGetMemberInfo()
        {
            try
            {
                Decimal ldc_sharestk, ldc_periodshramt, ldc_periodshrvalue, ldc_shrvalue, ldc_loanrequeststatus = 0;
                Decimal ldc_salary = 0, ldc_incomemth = 0, ldc_paymonth;
                Decimal ldc_shrstkvalue = 0;
                int li_shrpaystatus, li_lastperiod = 0, li_membertype, sequest_status = 0;

                DateTime ldtm_birth = new DateTime(), ldtm_member = new DateTime(), ldtm_work = new DateTime(), ldtm_retry = new DateTime();
                string ls_position, ls_remark, ls_membname, ls_membgroup, ls_groupname;
                string ls_membtypedesc = "", ls_controlname = "", ls_membcontrol = "", ls_appltype, ls_memno, ls_CoopControl;
                Decimal lndroploanall_flag = 0, li_memberstatus = 1, li_resignstatus = 0;
                String member_no = Request["memno"].ToString();
                dw_main.SetItemString(1, "member_no", member_no);

                ls_CoopControl = state.SsCoopControl; //สาขาที่ทำรายการ

                try
                {
                    //เช็คสิทธิ์ ก่อนขอกู้ว่ามีสัญญาเก่าค้างหรือไม่

                    String ls_memcoopid;
                    ls_memcoopid = state.SsCoopControl;


                    String sqlstr = @"   SELECT a.membgroup_control,
                                                b.membgroup_desc as control_desc,
                                                a.membgroup_code,
                                                a.membgroup_desc , 
                                             MBMEMBMASTER.BIRTH_DATE,   
                                             MBMEMBMASTER.MEMBER_DATE,   
                                             MBMEMBMASTER.WORK_DATE,   
                                             MBMEMBMASTER.RETRY_DATE,   
                                             MBMEMBMASTER.SALARY_AMOUNT,   
                                             MBMEMBMASTER.INCOMEETC_AMT,    
                                             MBMEMBMASTER.MEMBTYPE_CODE,   
                                             MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                             SHSHAREMASTER.LAST_PERIOD,   
                                             SHSHAREMASTER.PERIODSHARE_AMT,   
                                             mbucfprename.prename_desc||mbmembmaster.memb_name||'   '||mbmembmaster.memb_surname as member_name,   
                                             SHSHAREMASTER.PAYMENT_STATUS,   
                                             MBMEMBMASTER.POSITION_DESC,   
                                             MBMEMBMASTER.POSITION_CODE,   
                                             MBMEMBMASTER.REMARK,   
                                             MBMEMBMASTER.MEMBER_STATUS,   
                                             MBMEMBMASTER.RESIGN_STATUS,   
                                             SHSHAREMASTER.SHARESTK_AMT,   
                                             SHSHARETYPE.UNITSHARE_VALUE,   
                                             MBMEMBMASTER.MEMBER_TYPE,   
                                             MBMEMBMASTER.APPLTYPE_CODE,   
                                             MBMEMBMASTER.RETRY_STATUS,      MBMEMBMASTER.DROPLOANALL_FLAG, 
                                             MBMEMBMASTER.MEMBER_NO,   
                                             MBMEMBMASTER.PAUSEKEEP_FLAG,   
                                             MBMEMBMASTER.PAUSEKEEP_DATE,       SHSHAREMASTER.sequest_status,
                                             MBMEMBMASTER.COOP_ID  ,MBMEMBMASTER.CREMATION_STATUS
                               FROM MBMEMBMASTER,   
                                     MBUCFMEMBGROUP a ,   MBUCFMEMBGROUP b,
                                     MBUCFPRENAME,
                                     MBUCFMEMBTYPE,
                                     SHSHAREMASTER,   
                                     SHSHARETYPE  
                               WHERE ( a.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  a.membgroup_control = b.membgroup_code(+) and
                                        a.coop_id = b.coop_id(+) and 
                                    ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and    
                                     ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and
                                     ( MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE ) and  
                                     ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                                     ( MBMEMBMASTER.COOP_ID = a.COOP_ID ) and     ( MBMEMBMASTER.MEMBER_STATUS=1) AND
                                     ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID ) and  
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     ( ( mbmembmaster.member_no = '" + member_no + @"' ) AND  
                                     ( shsharemaster.sharetype_code = '01' ) )    and  
                                     MBMEMBMASTER.COOP_ID   ='" + ls_memcoopid + @"' ";
                    Sdt dt = WebUtil.QuerySdt(sqlstr);//เป็น service

                    if (dt.GetRowCount() < 1)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิก หรือ สมาชิกท่านได้ปิดบัญชีสมาชิกแล้ว");
                    }
                    while (dt.Next())
                    {

                        try
                        {
                            lndroploanall_flag = dt.GetDecimal("LNDROPLOANALL_FLAG");
                        }
                        catch { lndroploanall_flag = 0; }
                        try { sequest_status = dt.GetInt32("sequest_status"); }
                        catch { sequest_status = 0; }
                        ls_membname = dt.GetString("member_name");
                        ls_membcontrol = dt.GetString("membgroup_control");
                        ls_controlname = dt.GetString("control_desc");
                        ls_membgroup = dt.GetString("membgroup_code");
                        ls_groupname = dt.GetString("membgroup_desc");
                        ldc_salary = dt.GetDecimal("salary_amount");
                        li_lastperiod = dt.GetInt32("last_period");
                        li_memberstatus = dt.GetInt32("member_status");
                        li_resignstatus = dt.GetInt32("resign_status");
                        ldtm_birth = dt.GetDate("birth_date");
                        try
                        {
                            ldtm_member = dt.GetDate("member_date");
                        }
                        catch { }
                        try
                        {
                            ldtm_work = dt.GetDate("work_date");
                        }
                        catch { }
                        try
                        {
                            ldc_incomemth = dt.GetDecimal("incomeetc_amt");
                        }
                        catch
                        {
                            ldc_incomemth = 0;
                        }

                        ldc_paymonth = 0;
                        ls_position = dt.GetString("position_desc");
                        ls_remark = dt.GetString("remark");
                        ldc_shrvalue = dt.GetDecimal("unitshare_value");
                        ldc_sharestk = dt.GetDecimal("sharestk_amt");
                        ldc_periodshramt = dt.GetDecimal("periodshare_amt");
                        li_shrpaystatus = dt.GetInt32("payment_status");
                        li_membertype = dt.GetInt32("member_type");
                        ls_memno = dt.GetString("member_no");
                        ls_memcoopid = dt.GetString("coop_id");

                        // ls_membtype = dt.GetString("membtype_code");
                        ls_membtypedesc = dt.GetString("membtype_desc");
                        ldc_shrstkvalue = Convert.ToDecimal((Convert.ToInt32(ldc_shrvalue) * Convert.ToInt32(ldc_sharestk)));
                        ldc_periodshrvalue = Convert.ToDecimal((Convert.ToInt32(ldc_periodshramt) * Convert.ToInt32(ldc_shrvalue)));

                        if (li_resignstatus == 1)
                        {
                            LtServerMessage.Text = WebUtil.WarningMessage("สมาชิกท่านได้ลาออกจากสหกรณ์แล้ว กรุณาตรวจสอบ");
                        }

                        dw_main.SetItemDecimal(1, "salary_amt", ldc_salary);
                        dw_main.SetItemDecimal(1, "share_lastperiod", li_lastperiod);

                        dw_main.SetItemDecimal(1, "sharestk_value", ldc_shrstkvalue);
                        dw_main.SetItemDecimal(1, "periodshare_value", ldc_periodshrvalue);


                        try
                        {
                            ///<หาอายุสมาชิก>
                            birth_age = BusscomService.of_cal_yearmonth(state.SsWsPass, ldtm_birth, DateTime.Now);
                        }
                        catch
                        {
                            birth_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                        }

                        try
                        {
                            ///<หาอายุการเป็นสมาชิก>
                            member_age = BusscomService.of_cal_yearmonth(state.SsWsPass, ldtm_member, DateTime.Now);
                        }
                        catch
                        {
                            member_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                        }

                        DwUtil.RetrieveDataWindow(dw_detail, pbl, null, member_no);
                        if (dw_detail.RowCount > 0)
                        {
                            decimal ldc_balance = 0;
                            decimal ldc_intest = 0;
                            string ls_loantypeclr;
                            double intrate = 0.00;
                            for (int ii = 1; ii <= dw_detail.RowCount; ii++)
                            {
                                ldc_balance = dw_detail.GetItemDecimal(ii, "principal_balance");
                                ls_loantypeclr = dw_detail.GetItemString(ii, "loantype_code");
                                intrate = this.of_getinterestrate(ls_loantypeclr);
                                intrate = intrate / 100;
                                ldc_intest = ldc_balance * 30 * Convert.ToDecimal(intrate) / 365;

                                dw_detail.SetItemDecimal(ii, "interest_estimate", ldc_intest);

                            }

                        }
                        JsExpensedefault();
                    }


                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงรายการสมาชิกมาทำรายการได้");
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกประเภทเงินกู้ที่จะทำรายการ" + ex);
            }

        }
        private double of_getinterestrate(string loantype_code)
        {
            double intrate = 0;
            string sql = @"select inttabrate_code from lnloantype where loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            Sdt dt = WebUtil.QuerySdt(sql);
            string ls_intratetab = dt.GetString("inttabrate_code");
            String sqlint = @"  SELECT 
                                         INTEREST_RATE        
                                    FROM LNCFLOANINTRATEDET  
                                   WHERE ( COOP_ID ='" + state.SsCoopControl + @"'  ) AND  
                                         ( LOANINTRATE_CODE ='" + ls_intratetab + "'  ) order by effective_date ";
            Sdt dtint = WebUtil.QuerySdt(sqlint);
            while (dtint.Next())
            {
                intrate = dtint.GetDouble("INTEREST_RATE");


            }
            return intrate;
        }
        /// <summary>
        /// หาสิทธิ์กู้สูงสุดจากอายุสมาชิก
        /// </summary>
        private void Jsmaxcreditperiod()
        {
            try
            {
                JsSetloantype();
                String ls_loantype = dw_main.GetItemString(1, "loantype_code");
                string member_no = dw_main.GetItemString(1, "member_no");

                int ldc_share_lastperiod = Convert.ToInt16(dw_main.GetItemDecimal(1, "share_lastperiod"));

                Decimal ldc_shrstkvalue = dw_main.GetItemDecimal(1, "sharestk_value");
                Decimal ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");
                String ls_memcoopid = state.SsCoopId;
                ///<หาอายุงานของาสมาชิก>

                Decimal[] max_creditperiod = new Decimal[4];
                string loanrighttype_code = "01";
                Decimal loancredit_amt = 0, loanright_type = 1;
                int li_timemb = ldc_share_lastperiod;// Convert.ToInt16(ls_timembyear.Substring(0, 2)) * 12 + Convert.ToInt16(ls_timembyear.Substring(3, 2));   //ert.ToInt16(Math.Truncate(ldc_timembyear) * 12 + ((ldc_timembyear * 100) % 100));
                int li_timeage = 20;
                decimal customtime_type = 0;
                decimal maxloan_amt = 0;
                
                string loantype_code = dw_main.GetItemString(1, "loantype_code");
                string sqlpauseloan = "select  loantype_code , pauseloan_cause, expirefix_flag, expirefix_date  from  lnmembpauseloan  where member_no =  '" + member_no + "'   and  loantype_code =  '" + loantype_code + "'";
                Sdt dtp = WebUtil.QuerySdt(sqlpauseloan);
                string pauseloan_desc = "";
                DateTime expirefix_date;
                decimal expirefix_flag = 0;
                if (dtp.Next())
                {
                    dw_main.SetItemDecimal(1, "loancredit_amt", 0);
                    pauseloan_desc = dtp.GetString("pauseloan_cause");
                    expirefix_flag = dtp.GetDecimal("expirefix_flag");
                    expirefix_date = dtp.GetDate("expirefix_date");

                    dw_main.DisplayOnly = false;
                    dw_main.Enabled = false;
                    dw_main.EnableViewState = false;
                    dw_main.Modify("b_save.enabled = false");
                    dw_main.Modify("loancredit_amt.protect = 1");
                    dw_detail.DisplayOnly = false;

                    if (expirefix_flag == 1)
                    {

                        LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ได้ถูกห้ามกู้ประเภทนี้อยู่  " + loantype_code + " เหตผล " + pauseloan_desc + "  ถึงวันที่ " + Convert.ToString(expirefix_date.ToLongDateString()));
                        
                    }
                    else
                    {
                       
                        LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ได้ถูกห้ามกู้ประเภทนี้อยู่  " + loantype_code + " เหตผล " + pauseloan_desc);
                    }
                }

                if (dtp.GetRowCount() <= 0)
                {
                    string sqllntype = @"select  loanrighttype_code,customtime_type, loanright_type,maxloan_amt
                                        from lnloantype  where loantype_code ='" + ls_loantype + @"' ";
                    Sdt dtrigtht = WebUtil.QuerySdt(sqllntype);
                    if (dtrigtht.Next())
                    {
                        loanrighttype_code = dtrigtht.GetString("loanrighttype_code");
                        customtime_type = dtrigtht.GetDecimal("customtime_type");
                        loanright_type = dtrigtht.GetDecimal("loanright_type");
                        maxloan_amt = dtrigtht.GetDecimal("maxloan_amt");
                    }

                    try
                    {
                        max_creditperiod = Calloanpermisssurin(state.SsWsPass, ls_memcoopid, ls_loantype, ldc_salary, ldc_shrstkvalue, li_timemb, member_no, li_timeage);
                        //max_creditperiod = shrlonService.Calloanpermiss(state.SsWsPass, ls_memcoopid, ls_loantype, ldc_salary, ldc_shrstkvalue, li_timemb, member_no, li_timeage);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("shrlonService.Calloanpermisssurine " + ex);

                    }
                    Decimal ldc_percenshare = max_creditperiod[2];
                    Decimal ldc_percensaraly = max_creditperiod[3];

                    loancredit_amt = ldc_percensaraly * ldc_salary;  //max_creditperiod[0];

                    if (loancredit_amt > maxloan_amt) { loancredit_amt = maxloan_amt; }
                    //mong
                    string sql = @"select reqround_factor from lnloantype where loantype_code = {0}";
                    sql = WebUtil.SQLFormat(sql, ls_loantype);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    string ll_roundloan = dt.GetString("reqround_factor");
                    int roundloan = Convert.ToInt16(ll_roundloan);
                    if (loancredit_amt % roundloan > 0)
                    {
                        loancredit_amt = loancredit_amt - (loancredit_amt % roundloan);
                    }

                    dw_main.SetItemDecimal(1, "maxcredit_amt", loancredit_amt);
                    dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt);
                    dw_main.SetItemDecimal(1, "maxperiod_payamt", max_creditperiod[1]);
                    dw_main.SetItemDecimal(1, "contract_time", 36);

                    dw_main.SetItemDateTime(1, "startcont_date", state.SsWorkDate);

                    DateTime expire_date = state.SsWorkDate.AddMonths(Convert.ToInt16(36));

                    dw_main.SetItemDateTime(1, "expirecont_date", expire_date);
                    tdwMain.Eng2ThaiAllRow();
                    ////สิทธิกู้ตามเงินเดือนคงเลหือ
                    JsSetMonthpayCoop();
                    JsCalMaxLoanpermiss();
                    JsContPeriod();
                    
                    HdPeriodMax.Value = max_creditperiod[1].ToString();
                    HdCreditMax.Value = loancredit_amt.ToString(format:"###0");
                   
                    
                }
            }
            catch (Exception ex)
            {
                // LtServerMessage.Text = WebUtil.ErrorMessage("Jsmaxcreditperiod===>" + ex);
            }

        }
        public decimal[] Calloanpermisssurin(String wsPass, String as_coopid, String ls_loantype, Decimal ldc_salary, Decimal ldc_shrstkvalue, int li_time, String as_member_no, decimal li_timeage)
        {
            Decimal[] max_creditperiod = new Decimal[4];
            Decimal ldc_maxcredit = 0, ldc_percensaraly = 0, ldc_maxloanamt = 0, ldc_percenshare = 0, ldc_maxright = 0, ldc_maxdept = 0, ldc_maxcoll = 0, ldc_maxshk = 0; ;
            string sql = @"select loanright_type from lnloantype where loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, ls_loantype);
            Sdt dt = WebUtil.QuerySdt(sql);
            string loanright_type = dt.GetString("loanright_type");
            Decimal ldc_share_flag = 0, ldc_deposit_flag = 0, ldc_collamst_flag = 0;
            String sqlright = @"  SELECT COOP_ID,   
                                                 LOANTYPE_CODE,   
                                                 SEQ_NO,   
                                                 SHARE_FLAG,   
                                                 DEPOSIT_FLAG,   
                                                 COLLMAST_FLAG, 
                                                 dividend_flag,   
                                                 MAXLOAN_AMT  
                                            FROM LNLOANTYPERIGHT  
                                            WHERE COOP_ID ='" + as_coopid + @"'    and LOANTYPE_CODE='" + ls_loantype + @"'";
            Sdt dtright = ta.Query(sqlright);
            if (dtright.Next())
            {
                ldc_share_flag = dtright.GetDecimal("SHARE_FLAG");
                ldc_deposit_flag = dtright.GetDecimal("DEPOSIT_FLAG");
                ldc_collamst_flag = dtright.GetDecimal("COLLMAST_FLAG");
            }


            // หา maxcredit
            String sqlStrcredit = @" SELECT COOP_ID,   
                                    LOANTYPE_CODE,   
                                    SEQ_NO,   
                                    STARTSHARE_AMT,   
                                    ENDSHARE_AMT,   
                                    STARTMEMBER_TIME,   
                                    ENDMEMBER_TIME, 
                                    startage_amt,
                                    endage_amt,  
                                    PERCENTSHARE,   
                                    PERCENTSALARY,   
                                    MAXLOAN_AMT,   
                                    STARTSALARY_AMT,   
                                    ENDSALARY_AMT  
                                FROM LNLOANTYPECUSTOM  
                                WHERE  COOP_ID ='" + as_coopid + @"'  
                                    and LOANTYPE_CODE='" + ls_loantype + @"' 
                                    and STARTMEMBER_TIME <=" + li_time + @"
                                    and ENDMEMBER_TIME >" + li_time + @"
                                    and STARTAGE_AMT <=" + li_timeage + @"
                                    and ENDAGE_AMT >" + li_timeage + @" ";
            Sdt dtcredit = ta.Query(sqlStrcredit);
            if (dtcredit.Next())
            {
                try
                {
                    ldc_percenshare = dtcredit.GetDecimal("PERCENTSHARE");
                    if (ldc_percenshare == 100) { ldc_percenshare = 1; }
                }
                catch { ldc_percenshare = 0; }
                ldc_percensaraly = dtcredit.GetDecimal("PERCENTSALARY");
                ldc_maxloanamt = dtcredit.GetDecimal("MAXLOAN_AMT");
            }


            if (loanright_type == "1")
            {

                if (ldc_share_flag == 1) // หุ้น
                {
                    ldc_maxshk += ldc_shrstkvalue;
                }

                else if (ldc_deposit_flag == 1) // เงินฝาก
                {
                    String sqldept = @"   SELECT 
                                   sum(  DPDEPTMASTER.PRNCBAL) as   PRNCBAL
                                FROM DPDEPTMASTER,   
                                     MBMEMBMASTER,   
                                     DPDEPTTYPE  
                               WHERE ( DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                                     ( DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE ) and  
                                     ( DPDEPTMASTER.MEMCOOP_ID = DPDEPTTYPE.COOP_ID )  and 
                                     DPDEPTMASTER.DEPTCLOSE_STATUS=0  and 
                                     DPDEPTMASTER.MEMBER_NO ='" + as_member_no + @"'
                                 and DPDEPTMASTER.COOP_ID='" + as_coopid + @"'";
                    Sdt dtdept = ta.Query(sqldept);
                    if (dtdept.Next())
                    {
                        ldc_maxdept += dtdept.GetDecimal("PRNCBAL");
                    }
                }
                else if (ldc_collamst_flag == 1)// หลักทรัพย์
                {
                    String sqlcoll = @"  SELECT DISTINCT LNCOLLMASTER.COLLMAST_NO,   
                                             LNCOLLMASTER.COLLMAST_REFNO,   
                                             LNCOLLMASTER.COLLMASTTYPE_CODE,   
                                             LNCOLLMASTER.COLLMAST_DESC,   
                                             LNCOLLMASTER.MORTGAGE_PRICE,   
                                             LNCOLLMASTER.REDEEM_FLAG,   
                                             LNCOLLMASTMEMCO.MEMCO_NO  
                                        FROM LNCOLLMASTER,   
                                             LNCOLLMASTMEMCO  
                                       WHERE ( LNCOLLMASTER.COLLMAST_NO = LNCOLLMASTMEMCO.COLLMAST_NO ) and  
                                             ( ( LNCOLLMASTMEMCO.MEMCO_NO = '" + as_member_no + @"' ) ) ";
                    Sdt dtcoll = ta.Query(sqlcoll);
                    if (dtcoll.Next())
                    {
                        ldc_maxcoll += dtcoll.GetDecimal("PRNCBAL");
                    }


                }


            }
            else if (loanright_type == "2")
            {

                if (ldc_percenshare > 0)
                {


                    ldc_maxcredit = ldc_shrstkvalue * ldc_percenshare;
                }

                else
                {
                    ldc_maxcredit = ldc_salary * ldc_percensaraly;
                }
                if (ldc_maxcredit > ldc_maxloanamt)
                {
                    ldc_maxcredit = ldc_maxloanamt;
                }
            }
            else if (loanright_type == "3")
            {

                //ดูจากสัญญาหลัก  คือ อ่านตารางกำหนดวงเงินกู้ OD

                DateTime ldtm_expirecont, ldtm_today;
                string ls_contcreditno = "";
                string ls_Sql = @"  select	contcredit_no, loancreditbal_amt, expirecont_date
                                from	lncontcredit          where	(  member_no	 =  '" + as_member_no + @"'  ) and
		                        ( loantype_code	=  '" + ls_loantype + "') and      ( contcredit_status	= 1 )";


                Sdt dtcreditc = ta.Query(ls_Sql);

                if (dtcreditc.GetRowCount() <= 0)
                {
                    ldc_maxloanamt = 0;
                    ls_contcreditno = "";
                }
                if (dtcreditc.Next())
                {
                    try
                    {
                        ldc_maxloanamt = dtcreditc.GetDecimal("loancreditbal_amt");

                    }
                    catch { ldc_maxloanamt = 0; }
                    ldc_maxcredit = ldc_maxloanamt;
                    ldtm_expirecont = dtcredit.GetDate("expirecont_date");
                    ls_contcreditno = dtcredit.GetString("contcredit_no");
                }


            }

            else if (loanright_type == "4")
            {
                //คิดจากเงินเดือนคงเหลือ
                ldc_maxcredit = (ldc_salary * ldc_percensaraly) + (ldc_percenshare * ldc_shrstkvalue);
                if (ldc_maxcredit > ldc_maxloanamt)
                {
                    ldc_maxcredit = ldc_maxloanamt;
                }
            }
            else if (loanright_type == "5")
            {
                //คิดจากยอดหุ้น ยกมา
                double ldc_shrstkbegin = 0, ldc_divrate = 0, ldc_shrbuy = 0;
                string strat_date = " ", end_date = " ";
                String sqlstr = @"   SELECT  SHSHAREMASTER.SHARESTK_AMT,   (SHSHAREMASTER.SHAREBEGIN_AMT * SHSHARETYPE.UNITSHARE_VALUE ) as SHAREBEGIN_AMT,  
                                             SHSHARETYPE.UNITSHARE_VALUE, SHSHARETYPE.dividend_rate,SHSHARETYPE.loandivstart_date,SHSHARETYPE.loandivend_date
                               FROM   SHSHAREMASTER,   
                                     SHSHARETYPE  
                               WHERE     ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and 
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     ( ( SHSHAREMASTER.member_no = '" + as_member_no + @"' ) AND  
                                     ( shsharemaster.sharetype_code = '01' ) )    and  
                                     SHSHAREMASTER.COOP_ID   ='" + as_coopid + @"' ";

                Sdt dtshare = ta.Query(sqlstr);
                if (dtshare.Next())
                {
                    ldc_shrstkbegin = dtshare.GetDouble("SHAREBEGIN_AMT");
                    ldc_divrate = dtshare.GetDouble("dividend_rate");

                }
                //                string sqlstm = @" SELECT SUM (A.SHARE_AMOUNT * B.SIGN_FLAG * C.UNITSHARE_VALUE)  as sharebuy_amt
                //                                FROM SHSHARESTATEMENT A, SHUCFSHRITEMTYPE B , SHSHARETYPE C
                //                                WHERE A.SHARETYPE_CODE = C.SHARETYPE_CODE AND A.SHRITEMTYPE_CODE = B.SHRITEMTYPE_CODE AND A.MEMBER_NO = '" + as_member_no + @"'  AND 
                //                                A.SHRITEMTYPE_CODE <> 'B/F' and A.OPERATE_DATE > C.loandivstart_date  and A.OPERATE_DATE <= C.loandivend_date " + @" ";
                //                Sdt dtsharestm = ta.Query(sqlstm);
                //                if (dtshare.Next())
                //                {
                //                    try
                //                    {
                //                        ldc_shrbuy = dtshare.GetDouble("sharebuy_amt");
                //                    }
                //                    catch {
                //                        ldc_shrbuy = 0;
                //                    }
                //                }
                //ldc_shrstkbegin += ldc_shrbuy;
                double loancredit_amt = ldc_shrstkbegin * ldc_divrate;
                ldc_maxcredit = Convert.ToDecimal(loancredit_amt);
            }
            // หา maxperiod

            ldc_maxcredit = ldc_maxcredit + ldc_maxshk + ldc_maxcoll + ldc_maxdept;
            if (ldc_maxcredit > ldc_maxloanamt)
            {
                ldc_maxcredit = ldc_maxloanamt;
            }
            String sqlStrperiod = @"  SELECT COOP_ID,   
                                        LOANTYPE_CODE,   
                                        SEQ_NO,   
                                        MONEY_FROM,
                                        MONEY_TO,
                                        MAX_PERIOD
                                    FROM LNLOANTYPEPERIOD
                                    WHERE  COOP_ID ='" + as_coopid + @"'  
                                    and LOANTYPE_CODE='" + ls_loantype + @"' 
                                    and " + ldc_maxcredit + " between MONEY_FROM and  MONEY_TO "; //MONEY_FROM <=" + ldc_maxcredit + " and MONEY_TO >" + ldc_maxcredit + ";

            Sdt dtperiod = ta.Query(sqlStrperiod);
            Decimal ldc_maxperiod = 0;
            if (dtperiod.Next())
            {

                ldc_maxperiod = dtperiod.GetDecimal("MAX_PERIOD");
            }

            max_creditperiod[0] = ldc_maxcredit;
            max_creditperiod[1] = ldc_maxperiod;
            max_creditperiod[2] = ldc_percenshare;
            max_creditperiod[3] = ldc_percensaraly;
            ta.Close();
            ldc_maxcredit = 0;
            return max_creditperiod;
        }
        private void JsSetloantype()
        {


            string loantype = dw_main.GetItemString(1, "loantype_code");


            string sqllntype = @"select  mangrtpermgrp_code, notmoreshare_flag, loanrighttype_code, customtime_type, loanright_type,  maxloan_amt,loanpermgrp_code,  contint_type,  contractint_rate, inttabrate_code,salperct_balance,salamt_balance,loanpayment_type,loanpayment_status,reqround_factor,payround_factor,
                                     lngrpcutright_flag, inttabrate_code  from lnloantype  where loantype_code ='" + loantype + @"' ";
            Sdt dtlntype = WebUtil.QuerySdt(sqllntype);
            string intcontinttabcode = "INT01";
            double intcontintrate = 0.065;
            decimal intcontinttype = 2;
            decimal payment_stauts = 1;
            decimal ldc_minpercsal = 0;
            decimal ldc_maxloan = 0;
            decimal customtime_type = 4;
            decimal notmoreshare_flag = 0;
            decimal loanright_type = 2;
            decimal reqround_factor = 0;
            decimal payround_factor = 0;
            decimal lngrpcutright_flag = 0;
            string inttabrate_code = "01";
            string loanpermissgroup = "01";
            string loanrighttype_code = "01";
            string mangrtpermgrp_code = "01";
            decimal ldc_minsalaamt = 0, ldc_paymenttype = 1;
            if (dtlntype.Next())
            {

                intcontinttabcode = dtlntype.GetString("inttabrate_code");
                intcontinttype = dtlntype.GetDecimal("contint_type");
                ldc_minpercsal = dtlntype.GetDecimal("salperct_balance");
                ldc_minsalaamt = dtlntype.GetDecimal("salamt_balance");
                ldc_paymenttype = dtlntype.GetDecimal("loanpayment_type");
                ldc_maxloan = dtlntype.GetDecimal("maxloan_amt");
                loanpermissgroup = dtlntype.GetString("loanpermgrp_code");
                payment_stauts = dtlntype.GetDecimal("loanpayment_status");

                reqround_factor = dtlntype.GetDecimal("reqround_factor");
                payround_factor = dtlntype.GetDecimal("payround_factor");
                lngrpcutright_flag = dtlntype.GetDecimal("lngrpcutright_flag");
                inttabrate_code = dtlntype.GetString("inttabrate_code");
                customtime_type = dtlntype.GetDecimal("customtime_type");
                loanright_type = dtlntype.GetDecimal("loanright_type");
                notmoreshare_flag = dtlntype.GetDecimal("notmoreshare_flag");
                mangrtpermgrp_code = dtlntype.GetString("mangrtpermgrp_code");
            }
            string sqlint = @"select interest_rate from lncfloanintratedet where loanintrate_code =(select inttabrate_code   from lnloantype  where loantype_code = '" + loantype + @"') ";
            Sdt dtint = WebUtil.QuerySdt(sqlint);
            while (dtint.Next())
            {

                intcontintrate = dtint.GetDouble("interest_rate");
                // intcontintrate = intcontintrate / 100;
            }

            dw_main.SetItemDecimal(1, "int_continttype", intcontinttype);
            dw_main.SetItemDouble(1, "int_contintrate", intcontintrate);
            dw_main.SetItemString(1, "int_continttabcode", intcontinttabcode);
            dw_main.SetItemDecimal(1, "minsalary_perc", ldc_minpercsal);
            dw_main.SetItemDecimal(1, "minsalary_amt", ldc_minsalaamt);
            dw_main.SetItemDecimal(1, "loanpayment_type", ldc_paymenttype);

        }
        private decimal JsSetMonthpayCoop()
        {
            try
            {
                string ls_loantype;
                int li_index, li_count;
                int li_clrstatus, li_paytype;

                Decimal ldc_shrperiod, ldc_payment, ldc_intestm;
                Decimal ldc_sumpay = 0;

                // ดึงรายการหุ้น
                ldc_shrperiod = dw_main.GetItemDecimal(1, "periodshare_value");

                // ดึงรายการหนี้
                li_count = dw_detail.RowCount;
                Decimal ldc_loamappamt = 0;
                for (li_index = 1; li_index <= li_count; li_index++)
                {

                    li_clrstatus = Convert.ToInt32(dw_detail.GetItemDecimal(li_index, "operate_flag"));
                    ls_loantype = dw_detail.GetItemString(li_index, "loantype_code");

                    if (li_clrstatus == 1)
                    {
                        li_paytype = Convert.ToInt32(dw_detail.GetItemDecimal(li_index, "loanpayment_type"));
                        ldc_payment = dw_detail.GetItemDecimal(li_index, "period_payment");
                        ldc_loamappamt = dw_detail.GetItemDecimal(li_index, "loanapprove_amt");
                        try
                        {
                            ldc_intestm = dw_detail.GetItemDecimal(li_index, "interest_estimate");
                        }
                        catch { ldc_intestm = 0; }

                        if (li_paytype == 1)
                        {
                            ldc_sumpay += ldc_payment + ldc_intestm;
                        }
                        else
                        {
                            ldc_sumpay += ldc_payment;
                        }
                    }
                }

                ldc_sumpay = ldc_sumpay + ldc_shrperiod;
                dw_main.SetItemDecimal(1, "paymonth_coop", ldc_sumpay);
                return ldc_sumpay;
            }
            catch
            {

            }
            return 0;
        }
        private void JsContPeriod()
        {
            try
            {
                string ls_loantype = dw_main.GetItemString(1, "loantype_code");
                Decimal period_payamt = dw_main.GetItemDecimal(1, "maxperiod_payamt");
                Decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
                decimal loanpayment_type = dw_main.GetItemDecimal(1, "loanpayment_type");
                decimal ldc_intrate = 5 / 100;
               
                try
                {
                    ldc_intrate = dw_main.GetItemDecimal(1, "int_contintrate");
                }
                catch { ldc_intrate = 5 / 100; }
                Decimal period_payment = 0;
                // ปัดยอดชำระ
                string sql = @"select payround_factor from lnloantype where loantype_code = {0}";
                sql = WebUtil.SQLFormat(sql, ls_loantype);
                Sdt dt = WebUtil.QuerySdt(sql);
                string ll_roundpay = dt.GetString("payround_factor");
                //String ll_roundpay = "100";
                int roundpay = Convert.ToInt16(ll_roundpay);
                double ldc_fr = 1, ldc_temp = 1, loapayment_amt = 0;
                if (loanpayment_type == 1)
                {
                    //คงต้น
                    period_payment = loanrequest_amt / period_payamt;
                }
                else
                {
                    string sqlint = @"select fixpaycal_type from lnloanconstant ";
                    Sdt dtint = WebUtil.QuerySdt(sqlint);
                    int li_fixcaltype = Convert.ToInt16(dtint.GetString("fixpaycal_type"));
                    //คงยอด
                    //ธกส เปลี่ยนการใส่อัตราดอกเบี้ย
                    ldc_intrate = ldc_intrate / 100;

                    if (li_fixcaltype == 1)
                    {
                        // ด/บ ทั้งปี / 12
                        //ldc_fr			= exp( - ai_period * log( ( 1 + adc_intrate / 12 ) ) )
                        //ldc_payamt	= adc_principal * ( adc_intrate / 12 ) / ( 1 - ldc_fr )

                        ldc_temp = Math.Log(1 + (Convert.ToDouble(ldc_intrate / 12)));
                        ldc_fr = Math.Exp(-Convert.ToDouble(period_payamt) * ldc_temp);
                        loapayment_amt = (Convert.ToDouble(loanrequest_amt) * (Convert.ToDouble(ldc_intrate) / 12)) / ((1 - ldc_fr));


                    }
                    else
                    {
                        // ด/บ 30 วัน/เดือน
                        //ldc_fr			= exp( - ai_period * log( ( 1 + adc_intrate * ( 30/365 ) ) ) )
                        //ldc_payamt	= adc_principal * ( adc_intrate * 30/365 ) / ( 1 - ldc_fr )

                        ldc_temp = Math.Log(1 + (Convert.ToDouble(ldc_intrate * (30 / 365))));
                        ldc_fr = Math.Exp(-Convert.ToDouble(period_payamt) * ldc_temp);
                        loapayment_amt = (Convert.ToDouble(loanrequest_amt) * (Convert.ToDouble(ldc_intrate) * (30 / 365))) / ((1 - ldc_fr));


                        //แบบสุโขทัย
                        //ldc_fr			= ( adc_intrate * ( 1+ adc_intrate) ^ ai_period )/ (((1 + adc_intrate ) ^ ai_period ) - 1  )
                        //ldc_payamt = adc_principal / ldc_fr
                    }

                    period_payment = Math.Ceiling(Convert.ToDecimal(loapayment_amt));
                }
                if (roundpay > 0)
                {
                    //by mong
                    //period_payamt = Math.Round(period_payamt / 10) * 10;
                    //period_payment = Math.Ceiling(Convert.ToDecimal(loapayment_amt));
                    //period_payment = Math.Round(period_payment, roundpay, MidpointRounding.AwayFromZero);// *roundpay;
                    if (period_payment % roundpay > 0)
                    {
                        period_payment = period_payment + roundpay - (period_payment % roundpay);
                    }

                }
                dw_main.SetItemDecimal(1, "period_payment", period_payment);
                JsChecksalabal();
            }
            catch
            {

            }

        }
        private void JsExpensedefault()
        {
            string member_no = dw_main.GetItemString(1, "member_no");
            string strsql = @"select expense_code, expense_bank, expense_branch, expense_accid 
                                        from mbmembmaster where member_no = '" + member_no + "'";
            try
            {
                Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                if (dtloanrcv.GetRowCount() <= 0)
                {

                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + member_no);
                }
                if (dtloanrcv.Next())
                {

                    string loanrcv_code = dtloanrcv.GetString("expense_code");
                    string loanrcv_bank = dtloanrcv.GetString("expense_bank");
                    string loanrcv_branch = dtloanrcv.GetString("expense_branch");
                    string loanrcv_accid = dtloanrcv.GetString("expense_accid");

                    dw_main.SetItemString(1, "loanrcv_code", loanrcv_code);
                    dw_main.SetItemString(1, "loanrcv_bank", loanrcv_bank);
                    dw_main.SetItemString(1, "loanrcv_branch", loanrcv_branch);
                    dw_main.SetItemString(1, "loanrcv_accid", loanrcv_accid);

                    //string sql_bkk = "select branch_name from cmucfbankbranch where bank_code = '" + loanrcv_bank + "' and branch_id = '" + loanrcv_branch + "'";
                    //Sdt dtk = WebUtil.QuerySdt(sql_bkk);
                    //string bankbranch = "";
                    //if (dtk.Next())
                    //{
                    //    bankbranch = dtk.GetString("branch_name").Trim();
                    //    dw_main.SetItemString(1, "loanrcv_branch_1", bankbranch);
                    //}
                    if (loanrcv_code == "CBT" || loanrcv_code == "CBO") {

                        JsExpensebankbrRetrieve();
                    }
                }

            }
            catch
            {

            }

        }
        private void SaveWebSheet()
        {
            string newContNo, member_no, loantype_code, maxcredit_amt, loancredit_amt, loancreditbal_amt, contract_time, salary_amt, share_lastperiod, sharestk_value, periodshare_value, paymonth_coop, paymonth_other;
            string intestimate_amt, loanpayment_type, minsalary_perc, minsalary_amt, maxperiod_payamt, period_payment, contcredit_status, remark, entry_id, coopbranch_id, loanrcv_code, loanrcv_branch, loanrcv_bank;
            string loanrcv_accid, int_continttype, int_contintrate, int_contintincrease, int_intsteptype, int_continttabcode;
            DateTime operate_date, startcont_date, expirecont_date, entry_date;

            try
            {
                newContNo = wcf.NCommon.of_getnewdocno(state.SsWsPass,state.SsCoopId, "CREDITDOCNO");

                tdwMain.Thai2EngAllRow();
                //DwUtil.InsertDataWindow(dw_main, "", "LNCONTCREDIT");
                try
                {
                    member_no = dw_main.GetItemString(1, "MEMBER_NO");

                }
                catch { member_no = ""; }
                try
                {
                    loantype_code = dw_main.GetItemString(1, "LOANTYPE_CODE");
                }
                catch { loantype_code = ""; }
                try
                {
                    operate_date = dw_main.GetItemDateTime(1, "OPERATE_DATE");
                }
                catch { operate_date = state.SsWorkDate; }
                try
                {
                    maxcredit_amt = dw_main.GetItemString(1, "MAXCREDIT_AMT");
                }
                catch { maxcredit_amt = "0"; }
                try
                {
                    loancredit_amt = dw_main.GetItemString(1, "LOANCREDIT_AMT");
                }
                catch { loancredit_amt = "0"; }
                try
                {
                    loancreditbal_amt = dw_main.GetItemString(1, "LOANCREDIT_AMT");
                }
                catch { loancreditbal_amt = "0"; }
                try
                {
                    contract_time = dw_main.GetItemString(1, "CONTRACT_TIME");
                }
                catch { contract_time = "0"; }
                try
                {
                    startcont_date = dw_main.GetItemDateTime(1, "STARTCONT_DATE");
                }
                catch { startcont_date = state.SsWorkDate; }
                try
                {
                    expirecont_date = dw_main.GetItemDateTime(1, "EXPIRECONT_DATE");
                }
                catch { expirecont_date = state.SsWorkDate; }
                try
                {
                    salary_amt = dw_main.GetItemString(1, "SALARY_AMT");
                }
                catch { salary_amt = "0"; }
                try
                {
                    share_lastperiod = dw_main.GetItemString(1, "SHARE_LASTPERIOD");
                }
                catch { share_lastperiod = "0"; }
                try
                {
                    sharestk_value = dw_main.GetItemString(1, "SHARESTK_VALUE");
                }
                catch { sharestk_value = "0"; }
                try
                {
                    periodshare_value = dw_main.GetItemString(1, "PERIODSHARE_VALUE");
                }
                catch { periodshare_value = "0"; }
                try
                {
                    paymonth_coop = dw_main.GetItemString(1, "PAYMONTH_COOP");
                }
                catch { paymonth_coop = "0"; }
                try
                {
                    paymonth_other = dw_main.GetItemString(1, "PAYMONTH_OTHER");
                }
                catch { paymonth_other = "0"; }
                try
                {
                    intestimate_amt = dw_main.GetItemString(1, "INTESTIMATE_AMT");
                }
                catch { intestimate_amt = "0"; }
                try
                {
                    loanpayment_type = dw_main.GetItemString(1, "LOANPAYMENT_TYPE");
                }
                catch { loanpayment_type = ""; }
                try
                {
                    minsalary_perc = dw_main.GetItemString(1, "MINSALARY_PERC");
                }
                catch { minsalary_perc = "0"; }
                try
                {
                    minsalary_amt = dw_main.GetItemString(1, "MINSALARY_AMT");
                }
                catch { minsalary_amt = "0"; }
                try
                {
                    maxperiod_payamt = dw_main.GetItemString(1, "MAXPERIOD_PAYAMT");
                }
                catch { maxperiod_payamt = "0"; }
                try
                {
                    period_payment = dw_main.GetItemString(1, "PERIOD_PAYMENT");
                }
                catch { period_payment = "0"; }
                try
                {
                    contcredit_status = dw_main.GetItemString(1, "CONTCREDIT_STATUS");
                }
                catch { contcredit_status = ""; }
                try
                {
                    remark = dw_main.GetItemString(1, "REMARK");
                }
                catch { remark = ""; }
                try
                {
                    entry_id = dw_main.GetItemString(1, "ENTRY_ID");
                }
                catch { entry_id = ""; }
                try
                {

                    entry_date = dw_main.GetItemDateTime(1, "ENTRY_DATE");
                }
                catch { entry_date = state.SsWorkDate; }
                try
                {
                    coopbranch_id = dw_main.GetItemString(1, "COOPBRANCH_ID");
                }
                catch { coopbranch_id = ""; }
                try
                {

                    loanrcv_code = dw_main.GetItemString(1, "LOANRCV_CODE");
                }
                catch { loanrcv_code = ""; }
                try
                {
                    loanrcv_branch = dw_main.GetItemString(1, "LOANRCV_BRANCH");
                }
                catch { loanrcv_branch = ""; }
                try
                {
                    loanrcv_bank = dw_main.GetItemString(1, "LOANRCV_BANK");
                }
                catch { loanrcv_bank = ""; }
                try
                {
                    loanrcv_accid = dw_main.GetItemString(1, "LOANRCV_ACCID");
                }
                catch { loanrcv_accid = ""; }
                try
                {
                    int_continttype = dw_main.GetItemString(1, "INT_CONTINTTYPE");
                }
                catch { int_continttype = ""; }
                try
                {
                    int_contintrate = dw_main.GetItemString(1, "INT_CONTINTRATE");
                }
                catch { int_contintrate = "0"; }
                //try
                //{
                //    int_contintabcode = dw_main.GetItemString(1, "INT_CONTINTTABCODE");
                //}
                //catch { }
                try
                {
                    int_contintincrease = dw_main.GetItemString(1, "INT_CONTINTINCREASE");
                }
                catch { int_contintincrease = "0"; }
                try
                {
                    int_intsteptype = dw_main.GetItemString(1, "INT_INTSTEPTYPE");
                }
                catch { int_intsteptype = "0"; }

                string ls_entryid = state.SsUsername;
                DateTime ldtm_today = state.SsWorkDate;
                string ls_sqlexc = @"  update	lncontcredit      set		contcredit_status	= -9, 
		                cancel_id	= '" + ls_entryid + "',  cancel_date		= to_date('" + ldtm_today.ToString("yyyy-MM-dd HH:mm:ss", WebUtil.EN) +
                   @"', 'yyyy-mm-dd hh24:mi:ss')   where	( member_no		= '" + member_no + "' ) ";

                //Sta ta = new Sta(state.SsConnectionString);

                try
                {
                    //sql = WebUtil.SQLFormat(sql, arr);
                    int sql_q = WebUtil.ExeSQL(ls_sqlexc);
                    //ส่งค่า start_membgroup ไปให้ Stored Procedures ชื่อ SP_RPT_COLL_DETAIL
                    //ta.Exe(ls_sqlexc);

                    //ta.Commit(true);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกวงเงินเรียบร้อย");

                    //ta.Close();
                }
                catch (Exception ex)
                { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการ ยกเลิกวงเงิน " + ex); } 



                string sql = @"
INSERT INTO LNCONTCREDIT                        ( 
CONTCREDIT_NO,			MEMBER_NO,				LOANTYPE_CODE,			OPERATE_DATE,
MAXCREDIT_AMT,			LOANCREDIT_AMT,		    LOANCREDITBAL_AMT,	    CONTRACT_TIME,   
STARTCONT_DATE,		    EXPIRECONT_DATE,		SALARY_AMT,				SHARE_LASTPERIOD,
SHARESTK_VALUE,		    PERIODSHARE_VALUE,	    PAYMONTH_COOP,		    PAYMONTH_OTHER,   
INTESTIMATE_AMT,		LOANPAYMENT_TYPE,	    MINSALARY_PERC,		    MINSALARY_AMT,
MAXPERIOD_PAYAMT,	    PERIOD_PAYMENT,		    CONTCREDIT_STATUS,	    REMARK,
ENTRY_ID,               ENTRY_DATE,				COOPBRANCH_ID,			LOANRCV_CODE,			
LOANRCV_BANK,           LOANRCV_BRANCH,		    INT_CONTINTTYPE,		INT_CONTINTRATE,	
INT_CONTINTINCREASE,    INT_INTSTEPTYPE,        COOP_ID,                 loanrcv_accid)  
                  VALUES (
{0},                    {1},                    {2},                    {3},
{4},                    {5},                    {6},                    {7},
{8},                    {9},                    {10},                   {11},
{12},                   {13},                   {14},                   {15},
{16},                   {17},                   {18},                   {19},
{20},                   {21},                   {22},                   {23},
{24},                   {25},                   {26},                   {27},
{28},                   {29},                   {30},                   {31},
{32},                   {33},                   {34},                   {35})                
";
                object[] arr = new object[36];
                int ii = 0;
                arr[0] = newContNo;
                arr[1] = member_no;
                arr[2] = loantype_code;
                arr[3] = operate_date;
                arr[4] = maxcredit_amt;
                arr[5] = loancredit_amt;
                arr[6] = loancreditbal_amt;
                arr[7] = contract_time;
                try
                {
                    arr[8] = dw_main.GetItemDateTime(1, "startcont_date");// startcont_date;//----
                }
                catch
                {
                    arr[8] = new DateTime(1500, 1, 1);
                }
                try
                {
                    arr[9] = dw_main.GetItemDateTime(1, "expirecont_date");// expirecont_date;
                }
                catch
                {
                    arr[9] = new DateTime(1500, 1, 1);
                }
                arr[10] = salary_amt;
                arr[11] = share_lastperiod;
                arr[12] = sharestk_value;
                arr[13] = periodshare_value;
                arr[14] = paymonth_coop;
                arr[15] = paymonth_other;
                arr[16] = intestimate_amt;
                arr[17] = loanpayment_type;
                arr[18] = minsalary_perc;
                arr[19] = minsalary_amt;
                arr[20] = maxperiod_payamt;
                arr[21] = period_payment;
                arr[22] = contcredit_status;
                arr[23] = remark;
                arr[24] = state.SsUsername;
                arr[25] = state.SsWorkDate;
                arr[26] = coopbranch_id;
                arr[27] = loanrcv_code;
                arr[28] = loanrcv_bank;
                arr[29] = loanrcv_branch;
                arr[30] = int_continttype;
                arr[31] = int_contintrate;
                arr[32] = int_contintincrease;
                arr[33] = int_intsteptype;
                arr[34] = state.SsCoopId;
                arr[35] = loanrcv_accid;

                // Sdt ins = WebUtil.QuerySdt(sql_ins);
                sql = WebUtil.SQLFormat(sql, arr);
                int a = WebUtil.ExeSQL(sql);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                this.SetOnLoadedScript("parent.jsPostMember() \n parent.RemoveIFrame()");
            }

            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            dw_detail.Reset();
            dw_detail.DisplayOnly = true;
        }
        private void JsChecksalabal()
        {
            try
            {
                decimal salary_amt = dw_main.GetItemDecimal(1, "salary_amt");
                decimal ldc_sumpay = dw_main.GetItemDecimal(1, "paymonth_coop");
                decimal monthetc_pay = dw_main.GetItemDecimal(1, "paymonth_other");
                decimal loanperiod_payment = dw_main.GetItemDecimal(1, "period_payment");
                decimal ldc_minsalaamt = dw_main.GetItemDecimal(1, "minsalary_amt");
                decimal salary_bal = salary_amt - ldc_sumpay - monthetc_pay - loanperiod_payment;
                LtServerMessage.Text = "";
               // dw_main.SetItemDecimal(1, "paymonth_coop", salary_bal);
                if (salary_bal < ldc_minsalaamt)
                {
                    //LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกนี้มีเงินเดือนคงเหลือน้อยกว่า ขั้นต่ำที่กำหนด คือ " + ldc_minsalaamt.ToString() + " บาท คงเหลือแค่ " + salary_bal.ToString());
                    LtServerMessage.Text = WebUtil.ErrorMessage("เงินเดือนคงเหลือไม่ถึงตามกำหนด คงเหลือขั้นต่ำ 2,000 มีเงินเดือนคงเหลือ " + salary_bal.ToString() +" บาท กรุณาตรวจสอบด้วย");
                }
            }
            catch
            {

            }
        }
        private void JsCalMaxLoanpermiss()
        {
            //คำนวณสิทธิกู้จากยอดเงินเดือนคงเหลือขั้นต่ำ
            string loantype_code = dw_main.GetItemString(1, "loantype_code");
            decimal ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");
            decimal ldc_mthcoop = JsSetMonthpayCoop();// dw_main.GetItemDecimal(1, "paymonth_coop");
            //decimal ldc_minpercsal = dw_main.GetItemDecimal(1, "minsalary_perc");
            decimal ldc_minsalaamt = 2000;// dw_main.GetItemDecimal(1, "minsalary_amt");
            decimal ldc_maxloan = dw_main.GetItemDecimal(1, "maxcredit_amt");
            decimal ldc_periodsend = dw_main.GetItemDecimal(1, "maxperiod_payamt");
            decimal ldc_paymenttype = dw_main.GetItemDecimal(1, "loanpayment_type");
            decimal ldc_intrate = dw_main.GetItemDecimal(1, "int_contintrate");
            decimal ldc_incomeetc = 0;// dw_main.GetItemDecimal(1, "incomemonth_fixed");
            decimal ldc_paymtmetc = dw_main.GetItemDecimal(1, "paymonth_other");
            decimal ldc_sharestk = dw_main.GetItemDecimal(1, "sharestk_value");

            // ธกส เปลี่ยนการใส่อัตราดอกเบี้ย จาก 0.05  เป็น 5 
            ldc_intrate = ldc_intrate / 100;
            decimal salary_balance = ldc_salary - ldc_minsalaamt - ldc_mthcoop + ldc_incomeetc - ldc_paymtmetc; //
            if (ldc_minsalaamt <= 0) { salary_balance = ldc_salary; }
            decimal ldc_permamt;
            double li_maxperiod = Convert.ToDouble(ldc_periodsend);

            if (ldc_paymenttype == 1)
            {

                //คงต้น
                double ldc_dayyear = Convert.ToDouble(30) / Convert.ToDouble(366);
                double ldc_ddd = 1.00;
                double ldc_temp = Convert.ToDouble(ldc_periodsend) * (Convert.ToDouble(ldc_intrate) * ldc_dayyear) + ldc_ddd;
                ldc_permamt = Convert.ToDecimal((Convert.ToDouble(salary_balance) * Convert.ToDouble(ldc_periodsend)) / ldc_temp);

            }
            else
            { //คงยอด
                int li_fixcaltype = 1;//fixpaycal_type


                double ldc_permamttmp = 1.00, ldc_fr = 1.00, ldc_temp = 1.00;


                if (li_fixcaltype == 1)
                {
                    // ด/บ ทั้งปี / 12
                    // ldc_permamt = salary_balance * ((((1 + (ldc_intrate / 12)) ^ li_maxperiod) - 1) / ((ldc_intrate / 12) * ((1 + (ldc_intrate / 12)) ^ li_maxperiod)));
                    ldc_temp = Math.Log(1 + (Convert.ToDouble(Convert.ToDouble(ldc_intrate) / 12.00)));
                    ldc_fr = Math.Exp(-li_maxperiod * ldc_temp);
                    ldc_permamttmp = (Convert.ToDouble(salary_balance) * (1.00 - ldc_fr)) / ((Convert.ToDouble(ldc_intrate) / 12));
                }
                else
                {
                    // ด/บ 30 วัน/เดือน
                    ldc_temp = Math.Log(1 + (Convert.ToDouble(Convert.ToDouble(ldc_intrate) / (30.00 / 365.00))));
                    ldc_fr = Math.Exp(-li_maxperiod * ldc_temp);
                    ldc_permamttmp = (Convert.ToDouble(salary_balance) * (1.00 - ldc_fr)) / ((Convert.ToDouble(ldc_intrate) / (30.00 / 365.00)));

                }
                ldc_permamt = Convert.ToDecimal(ldc_permamttmp);
                decimal loan_credit = dw_main.GetItemDecimal(1, "maxcredit_amt");
                if (ldc_permamt > loan_credit)
                {
                    ldc_permamt = loan_credit;
                }


            }


            // ปัดยอดขอกู้
            string sql = @"select reqround_factor from lnloantype where loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            Sdt dt = WebUtil.QuerySdt(sql);
            string ll_roundloan = dt.GetString("reqround_factor");
            int roundloan = Convert.ToInt16(ll_roundloan);
            if (roundloan > 0)
            {
                if (ldc_permamt > 0)
                {
                    // by mong loanrequest_amt = Math.Round((loanrequest_amt) / roundloan) * roundloan;
                    if (ldc_permamt % roundloan > 0)
                    {
                        ldc_permamt = ldc_permamt - (ldc_permamt % roundloan);
                    }
                }
            }

            if (ldc_permamt > ldc_maxloan) { ldc_permamt = ldc_maxloan; }
            decimal ldc_shr90 = Convert.ToDecimal(ldc_sharestk * Convert.ToDecimal(0.90));

            if (ldc_permamt > ldc_shr90) { ldc_permamt = ldc_shr90; }
            dw_main.SetItemDecimal(1, "loancredit_amt", ldc_permamt);


        }

        /// <summary>
        /// จน.งวด==> หายอดชำระ

        public void WebDialogLoadEnd()
        {
            try
            {
                sqlca.Disconnect();
            }
            catch { }
        }
    }
}