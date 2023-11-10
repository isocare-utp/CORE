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
using DataLibrary;
using System.Data;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_loanrightchoose_share : PageWebDialog, WebDialog
    {

        protected String refreshDW;
        protected string jsLoanclrchg;
        protected string jscolldetail;
        public void InitJsPostBack()
        {
            refreshDW = WebUtil.JsPostBack(this, "refreshDW");
            jsLoanclrchg = WebUtil.JsPostBack(this, "jsLoanclrchg");
            jscolldetail = WebUtil.JsPostBack(this, "jscolldetail");
        }

        public void WebDialogLoadBegin()
        {
            //อ่านloantyperighcoll

            if (IsPostBack)
            {
                //dw_collright.RestoreContext();
                //dw_clear.RestoreContext();

                this.RestoreContextDw(dw_collright);
                this.RestoreContextDw(dw_clear);
            }
            else
            {
                string member_no = Request["member_no"].ToString();
                string loantype_code = Request["loantype_code"].ToString();
                member_no = WebUtil.MemberNoFormat(member_no);
                HdMemberNo.Value = member_no;
                HdLoantype.Value = loantype_code;
                JsCheckLoanrequestwait();
                Genbaseloanclear();
                of_setloanclearstatus();
                JsSetcollloanright();

            }

        }
        private decimal JsCheckCollmastrightBalance(string as_refcollno, string as_colltypecode)
        {
            try
            {
                string loantype = HdLoantype.Value;
                string membno = HdMemberNo.Value;
                string memcoop_id = state.SsCoopControl;
                string contno_clr = "", contclr_all = "";
                decimal clear_flag = 0;
                int k = 0;
                for (int i = 1; i <= dw_clear.RowCount; i++)
                {
                    contno_clr = dw_clear.GetItemString(i, "loancontract_no");
                    clear_flag = dw_clear.GetItemDecimal(i, "clear_status");
                    if (clear_flag == 1)
                    {
                        k++;
                        if (k > 1) { contclr_all += ","; }
                        contclr_all += "'" + contno_clr + "'";

                    }
                }
                if ((contclr_all.Length <= 7) || (contclr_all.Trim() == "")) { contclr_all = "'00000'"; }

                string sql_chkcoll = @" SELECT LNCONTMASTER.MEMBER_NO as member_no,   
             LNCONTMASTER.LOANCONTRACT_NO as loancontract_no,   
             LNCONTMASTER.LOANTYPE_CODE as loantype_code,
             LNCONTMASTER.PRINCIPAL_BALANCE as principal_balance ,
             LNCONTCOLL.REF_COLLNO as  ref_collno,   
             LNCONTCOLL.DESCRIPTION as DESCRIPTION ,   
             LNCONTCOLL.COLL_AMT as COLL_AMT ,   
             LNCONTCOLL.COLL_PERCENT as COLL_PERCENT , 
             LNCONTCOLL.BASE_PERCENT as BASE_PERCENT ,'Contno'
            FROM LNCONTCOLL,   
                 LNCONTMASTER  
           WHERE ( LNCONTCOLL.COOP_ID = LNCONTMASTER.COOP_ID ) and  
                 ( LNCONTCOLL.LOANCONTRACT_NO = LNCONTMASTER.LOANCONTRACT_NO ) and  
                 ( ( LNCONTCOLL.LOANCOLLTYPE_CODE = '" + as_colltypecode + @"' ) AND  
                 ( LNCONTMASTER.PRINCIPAL_BALANCE > 0 ) AND LNCONTMASTER.LOANCONTRACT_NO not in (" + contclr_all + @") and 
                 ( LNCONTCOLL.REF_COLLNO = '" + as_refcollno + @"'  ) )      
        Union
         SELECT LNREQLOAN.MEMBER_NO  as member_no ,   
                 LNREQLOAN.LOANREQUEST_DOCNO as loancontract_no , 
                 LNREQLOAN.LOANTYPE_CODE as loantype_code,  
                 LNREQLOAN.LOANREQUEST_AMT as  principal_balance,
                 LNREQLOANCOLL.REF_COLLNO as  ref_collno ,   
                 LNREQLOANCOLL.DESCRIPTION  as DESCRIPTION ,   
                 LNREQLOANCOLL.COLL_AMT as COLL_AMT ,   
                 LNREQLOANCOLL.COLL_PERCENT as COLL_PERCENT,   
                 LNREQLOANCOLL.BASE_PERCENT as BASE_PERCENT  , 'Lnreqloan'
            FROM LNREQLOAN,   
                 LNREQLOANCOLL  
           WHERE ( LNREQLOAN.COOP_ID = LNREQLOANCOLL.COOP_ID ) and  
                 ( LNREQLOAN.LOANREQUEST_DOCNO = LNREQLOANCOLL.LOANREQUEST_DOCNO ) and  
                 ( ( LNREQLOANCOLL.LOANCOLLTYPE_CODE = '" + as_colltypecode + @"' ) AND  
                 ( LNREQLOAN.LOANREQUEST_STATUS = 8 ) AND  
                 ( LNREQLOANCOLL.REF_COLLNO =  '" + as_refcollno + @"'  ) )  ";
                Sdt dt_coll = WebUtil.QuerySdt(sql_chkcoll);


                decimal principal_balance = 0, ldc_colluse = 0, ldc_collpercent = 0, ldc_collamt = 0;
                while (dt_coll.Next())
                {
                    string loantype_code = dt_coll.GetString("loantype_code");
                    decimal base_percent = dt_coll.GetDecimal("BASE_PERCENT");
                    principal_balance = dt_coll.GetDecimal("principal_balance");
                    ldc_collpercent = dt_coll.GetDecimal("COLL_PERCENT");
                    if (loantype_code == "54")
                    {
                        ldc_colluse = (principal_balance * ldc_collpercent * Convert.ToDecimal(0.9)) / base_percent;
                    }
                    else
                    {
                        ldc_colluse = principal_balance * ldc_collpercent;
                    }
                    ldc_collamt += ldc_colluse;

                }

                //if (ldc_collamt >= 0)
                //{

                //    decimal coll_balance = dw_collright.GetItemDecimal(row, "coll_balance");

                //    coll_balance = coll_balance - ldc_collamt;
                //    dw_collright.SetItemDecimal(row, "coll_balance", coll_balance);


                //}
                return ldc_collamt;


            }
            catch
            {
                return 0;
            }


        }

        protected int JsCheckLoanrequestwait()
        {
            try
            {
                String member_no = Request["member_no"].ToString();

                string ls_CoopControl = state.SsCoopControl; //สาขาที่ทำรายการ
                String ls_loantype = "";


                ls_loantype = Request["loantype_code"].ToString();

                string sql_chkloan = @"select loanrequest_docno, loanrequest_status, loanrequest_date, loanrequest_amt from lnreqloan where loanrequest_status in (11,8) and 
                                        member_no = '" + member_no + "' and  loantype_code = '" + ls_loantype + "'";
                Sdt dtchk = WebUtil.QuerySdt(sql_chkloan);
                if (dtchk.Next())
                {
                    string ls_reqloandocno = dtchk.GetString("loanrequest_docno");
                    decimal ldc_loanreqamt = dtchk.GetDecimal("loanrequest_amt");
                    DateTime ldtm_lnreq = dtchk.GetDate("loanrequest_date");
                    String entry_date = ldtm_lnreq.AddYears(543).ToString();

                    LtServerMessage.Text = WebUtil.WarningMessage("มีใบคำขอกู้สำหรับวันที่" + entry_date + "แล้ว กรุณาเปลี่ยนเป็นดึงใบคำขอเก่ามาแก้ไข");

                }


            }
            catch
            {

            }
            return 1;
        }

        private decimal of_getpercentcollmast(string as_coopid, string as_loantype, string as_colltype, string as_collmasttype)
        {
            decimal percent_collmast = 0;
            try
            {
                string sql_collperc = "select coll_percent from lnloantypecolluse where coop_id = '" + as_coopid + "' and loantype_code = '" + as_loantype + "' and  loancolltype_code  =  '" + as_colltype + "' and  collmasttype_code  = '" + as_collmasttype + "'";

                Sdt dt = WebUtil.QuerySdt(sql_collperc);
                if (dt.Next())
                {
                    percent_collmast = dt.GetDecimal("coll_percent");

                }
            }
            catch
            {
                percent_collmast = 0;

            }
            return percent_collmast;
        }

        private void JsSetcollloanright()
        {
            try
            {
                string coop_id = state.SsCoopControl;
                string member_no = HdMemberNo.Value;
                string loantype_code = HdLoantype.Value;
                //                string sql_righ = @"  select share_flag, deposit_flag, collmast_flag, maxloan_amt, dividend_flag from lnloantyperight
                //                                    where loantype_code = ' " +  loantype_code + "' and coop_id = ' " +coop_id + "' ";
                //
                string sql_righcoll = " SELECT LOANTYPE_CODE,   LOANCOLLTYPE_CODE,   COLLMASTTYPE_CODE,   RIGHT_TYPE,   RIGHT_PERC,   RIGHT_MAXAMT,   RIGHT_FORMAT,  RIGHT_RATIO  FROM LNLOANTYPERIGHTCOLL WHERE LOANTYPE_CODE = '" + loantype_code + "'";
                Sdt dtrigtcoll = WebUtil.QuerySdt(sql_righcoll);

                string collmast_type = "00", loancoll_type = "02", RIGHT_RATIO = "1:2";
                decimal RIGHT_TYPE = 0, RIGHT_PERC = 0, RIGHT_MAXAMT = 0, RIGHT_FORMAT = 0;
                while (dtrigtcoll.Next())
                {
                    try
                    {
                        loancoll_type = dtrigtcoll.GetString("LOANCOLLTYPE_CODE");
                        collmast_type = dtrigtcoll.GetString("COLLMASTTYPE_CODE");
                        RIGHT_TYPE = dtrigtcoll.GetDecimal("RIGHT_TYPE");
                        RIGHT_PERC = dtrigtcoll.GetDecimal("RIGHT_PERC");
                        RIGHT_MAXAMT = dtrigtcoll.GetDecimal("RIGHT_MAXAMT");
                        RIGHT_FORMAT = dtrigtcoll.GetDecimal("RIGHT_FORMAT");
                        RIGHT_RATIO = dtrigtcoll.GetString("RIGHT_RATIO");
                        Int16 li_row = 0;
                        decimal adc_mul = RIGHT_PERC;
                        decimal adc_div = 1;
                        li_row = 0;
                        if (loancoll_type == "02")
                        {
                            dw_collright.InsertRow(0);
                            li_row = Convert.ToInt16(dw_collright.RowCount);
                            dw_collright.SetItemString(li_row, "loancolltype_code", "02");
                            dw_collright.SetItemString(li_row, "ref_collno", member_no);
                            dw_collright.SetItemString(li_row, "description", " ทุนเรือนหุ้น"); //
                            dw_collright.SetItemString(li_row, "permiss_ratio", RIGHT_RATIO);
                            dw_collright.SetItemDecimal(li_row, "maxpermiss_amt", RIGHT_MAXAMT);
                            dw_collright.SetItemDecimal(li_row, "permiss_format", RIGHT_FORMAT);
                            dw_collright.SetItemDecimal(li_row, "permiss_percent", RIGHT_PERC);
                            dw_collright.SetItemString(li_row, "shrlontype_code", "01");
                            JsSetRatio(RIGHT_FORMAT, RIGHT_PERC, RIGHT_RATIO, ref  adc_mul, ref  adc_div);
                            string sql_share = "select a.sharestk_amt, b.unitshare_value from shsharemaster a, shsharetype b where a.sharetype_code = b.sharetype_code  and a.member_no = '" + member_no + "'";
                            Sdt dt = WebUtil.QuerySdt(sql_share);
                            decimal loan_permiss = 0;
                            if (dt.Next())
                            {
                                decimal sharestk_amt = dt.GetDecimal("sharestk_amt");
                                decimal share_value = dt.GetDecimal("unitshare_value");
                                decimal rightcoll_percent = of_getpercentcollmast(coop_id, loantype_code, "02", "00");
                                decimal sharestk_value = sharestk_amt * share_value;// *rightcoll_percent;

                                dw_collright.SetItemDecimal(li_row, "collfull_amt", sharestk_value);

                                decimal coll_use = JsCheckCollmastrightBalance(member_no, "02");
                                sharestk_value = (sharestk_value * rightcoll_percent) - coll_use;
                                sharestk_value = sharestk_value / rightcoll_percent;
                                if (RIGHT_FORMAT == 1)
                                {
                                    loan_permiss = sharestk_value * RIGHT_PERC * rightcoll_percent;
                                }
                                else
                                {
                                    loan_permiss = (adc_mul * sharestk_value / adc_div);
                                }
                                if (loan_permiss > RIGHT_MAXAMT) { loan_permiss = RIGHT_MAXAMT; }
                                dw_collright.SetItemDecimal(li_row, "colluse_amt", coll_use);
                                dw_collright.SetItemDecimal(li_row, "collbal_amt", sharestk_value);
                                dw_collright.SetItemDecimal(li_row, "permiss_amt", loan_permiss);
                            }

                        }
                        else if (loancoll_type == "03")
                        {

                            JsSetRatio(RIGHT_FORMAT, RIGHT_PERC, RIGHT_RATIO, ref  adc_mul, ref  adc_div);

                            JsGetDeptaccountinfo(li_row, member_no, loantype_code, RIGHT_RATIO, RIGHT_MAXAMT, RIGHT_PERC, RIGHT_FORMAT, adc_mul, adc_div);

                        }

                    }
                    catch
                    {

                    }
                }



            }
            catch { }

        }
        private void JsLoanclrchg()
        {
            try
            {
                int rowcount = dw_collright.RowCount;
                decimal loan_permiss = 0, coll_amt = 0, coll_use = 0, adc_mul = 1, adc_div = 1;
                decimal RIGHT_TYPE = 0, RIGHT_PERC = 0, RIGHT_MAXAMT = 0, RIGHT_FORMAT = 0;
                decimal rightcoll_percent = 0, collfull_amt = 0;
                string coop_id = state.SsCoopControl;
                string loantype_code = HdLoantype.Value;
                for (int i = 1; i <= rowcount; i++)
                {
                    string loancolltype_code = dw_collright.GetItemString(i, "loancolltype_code");
                    string shrlontype_code = dw_collright.GetItemString(i, "shrlontype_code");
                    string ref_collno = dw_collright.GetItemString(i, "ref_collno");
                    string RIGHT_RATIO = dw_collright.GetItemString(i, "permiss_ratio");
                    RIGHT_MAXAMT = dw_collright.GetItemDecimal(i, "maxpermiss_amt");
                    RIGHT_FORMAT = dw_collright.GetItemDecimal(i, "permiss_format");
                    RIGHT_PERC = dw_collright.GetItemDecimal(i, "permiss_percent");
                    collfull_amt = dw_collright.GetItemDecimal(i, "collfull_amt");
                    if (loancolltype_code == "02")
                    {
                        coll_use = JsCheckCollmastrightBalance(ref_collno, "02");
                        rightcoll_percent = of_getpercentcollmast(coop_id, loantype_code, "02", "00");
                    }
                    else if (loancolltype_code == "03")
                    {
                        coll_use = JsCheckCollmastrightBalance(ref_collno, "03");
                        rightcoll_percent = of_getpercentcollmast(coop_id, loantype_code, "03", shrlontype_code);
                    }
                    coll_amt = (collfull_amt * rightcoll_percent) - coll_use;
                    coll_amt = coll_amt / rightcoll_percent;
                    JsSetRatio(RIGHT_FORMAT, RIGHT_PERC, RIGHT_RATIO, ref adc_mul, ref adc_div);
                    if (RIGHT_FORMAT == 1)
                    {
                        loan_permiss = coll_amt * RIGHT_PERC;
                    }
                    else
                    {
                        loan_permiss = (adc_mul * coll_amt / adc_div);
                    }
                    if (loan_permiss > RIGHT_MAXAMT) { loan_permiss = RIGHT_MAXAMT; }

                    dw_collright.SetItemDecimal(i, "collbal_amt", coll_amt);
                    dw_collright.SetItemDecimal(i, "permiss_amt", loan_permiss);
                    dw_collright.SetItemDecimal(i, "colluse_amt", coll_use);
                }
            }
            catch
            {
            }
        }

        private void JsGetDeptaccountinfo(int row, string as_memno, string as_loantype, string RIGHT_RATIO, decimal RIGHT_MAXAMT, decimal RIGHT_PERC, decimal RIGHT_FORMAT, decimal adc_mul, decimal adc_div)
        {
            try
            {
                string coop_id = state.SsCoopControl;
                string member_no = HdMemberNo.Value;
                string loantype_code = HdLoantype.Value;
                string sql_deptcoll = "select  depttype_code, operate_flag from lnloantypedeptcancoll where loantype_code = '" + as_loantype + "'";
                Sdt dt = WebUtil.QuerySdt(sql_deptcoll);
                string depttype_code = "00";
                string sql_depmemno = " ";
                string deptaccount_no = " ";
                string dept_name = " ";
                decimal prncbal = 0, sequest_amount = 0, permiss_amt = 0;
                decimal rowright = 0;
                decimal rightcoll_percent = 0;
                while (dt.Next())
                {
                    depttype_code = dt.GetString("depttype_code");

                    sql_depmemno = "select depttype_code, deptaccount_no, deptaccount_name, prncbal, sequest_amount  from  dpdeptmaster where prncbal > 0 and member_no = '" + as_memno + "' and depttype_code = '" + depttype_code + "'";

                    Sdt dt_deptno = WebUtil.QuerySdt(sql_depmemno);
                    while (dt_deptno.Next())
                    {
                        deptaccount_no = dt_deptno.GetString("deptaccount_no");
                        dept_name = dt_deptno.GetString("deptaccount_name");
                        prncbal = dt_deptno.GetDecimal("prncbal");
                        sequest_amount = dt_deptno.GetDecimal("sequest_amount");
                        depttype_code = dt_deptno.GetString("depttype_code");
                        rightcoll_percent = of_getpercentcollmast(coop_id, loantype_code, "03", "00");
                        if (rightcoll_percent == 0) { rightcoll_percent = RIGHT_PERC; }
                        prncbal = prncbal - sequest_amount;
                       
                        row = dw_collright.InsertRow(0);
                        dw_collright.SetItemDecimal(row, "collfull_amt", prncbal);
                        prncbal = prncbal * rightcoll_percent;


                        decimal loan_permiss = 0;
                        decimal coll_use = JsCheckCollmastrightBalance(deptaccount_no, "03");
                        prncbal -= coll_use;
                        prncbal = prncbal / rightcoll_percent;
                        if (RIGHT_FORMAT == 1)
                        {
                            loan_permiss = prncbal * RIGHT_PERC;
                        }
                        else
                        {
                            loan_permiss = (adc_mul * prncbal / adc_div);
                        }
                        if (loan_permiss > RIGHT_MAXAMT) { loan_permiss = RIGHT_MAXAMT; }


                        permiss_amt = (prncbal * (adc_mul / adc_div));

                        if (permiss_amt > RIGHT_MAXAMT) { permiss_amt = RIGHT_MAXAMT; }


                        dw_collright.SetItemString(row, "loancolltype_code", "03");
                        dw_collright.SetItemString(row, "shrlontype_code", depttype_code);
                        dw_collright.SetItemString(row, "ref_collno", deptaccount_no);
                        dw_collright.SetItemString(row, "description", " เงินฝาก" + dept_name); //
                        dw_collright.SetItemString(row, "permiss_ratio", RIGHT_RATIO);
                        dw_collright.SetItemDecimal(row, "maxpermiss_amt", RIGHT_MAXAMT);
                        dw_collright.SetItemDecimal(row, "permiss_format", RIGHT_FORMAT);
                        dw_collright.SetItemDecimal(row, "permiss_percent", RIGHT_PERC);

                        dw_collright.SetItemDecimal(row, "collbal_amt", prncbal);
                        dw_collright.SetItemDecimal(row, "permiss_amt", permiss_amt);
                        dw_collright.SetItemDecimal(row, "colluse_amt", coll_use);

                    }


                }


            }
            catch
            {

            }


        }
        private void JsSetShareinfo(Int16 ai_row, string as_memno)
        {
            string sql_share = "select a.sharestk_amt, b.share_value from shsharemaster a, shucfsharetype b where a.sharetype_code = b.sharetype_code  and a.member_no = '" + as_memno + "'";
            Sdt dt = WebUtil.QuerySdt(sql_share);
            if (dt.Next())
            {
                decimal sharestk_amt = dt.GetDecimal("sharestk_amt");
                decimal sahre_value = dt.GetDecimal("share_value");


            }

        }
        private void Jscolldetail()
        {
            string contno = HdContno.Value;
            DwUtil.RetrieveDataWindow(dw_colldetail, "sl_member_detail.pbl", null, contno);
        }
        private void Genbaseloanclear()
        {

            try
            {
                String member_no = HdMemberNo.Value.Trim();
                DateTime loanrcvfix_date = state.SsWorkDate;

                String ls_memcoopid = state.SsCoopControl;

                string ls_contno, ls_conttype, ls_prefix, ls_permgrp, ls_loantype = "", ls_intcontintcode, ls_coopid = "";
                Decimal li_minperiod = 0, li_period, li_continttype, li_transfersts = 0, ldc_intestim30 = 0;
                Decimal li_paytype, li_contstatus, li_intcontinttype, li_intsteptype;
                Decimal li_periodamt, li_contlaw, li_paystatus, li_clearinsure, li_od_flag = 0;
                Decimal ldc_appvamt, ldc_balance = 0, ldc_withdrawable, ldc_rkeepprin, ldc_rkeepint, ldc_transbal;
                Decimal ldc_intarrear, ldc_payment, ldc_intestim;
                Decimal ldc_minpay = 0, ldc_intrate, ldc_intcontintrate, ldc_intincrease;
                DateTime ldtm_lastcalint, ldtm_lastproc, ldtm_approve, ldtm_startcont;
                int li_interestmethod = 0, li_roundtype = 1;
                ls_loantype = HdLoantype.Value.Trim();

                String sqlStr = @"   SELECT LNCONTMASTER.LOANCONTRACT_NO,   
                                             LNCONTMASTER.MEMBER_NO,   
                                             LNCONTMASTER.LOANTYPE_CODE,   
                                             LNCONTMASTER.LOANAPPROVE_AMT,   
                                             LNCONTMASTER.WITHDRAWABLE_AMT,   
                                             LNCONTMASTER.PRINCIPAL_BALANCE,   
                                             LNCONTMASTER.LAST_PERIODPAY,   
                                             LNCONTMASTER.LASTCALINT_DATE,   
                                             LNCONTMASTER.LASTPROCESS_DATE,   
                                             LNCONTMASTER.INTEREST_ARREAR,   
                                             LNCONTMASTER.RKEEP_PRINCIPAL,   
                                             LNCONTMASTER.RKEEP_INTEREST,   
                                             LNLOANTYPE.PREFIX,   
                                             LNCONTMASTER.LOANPAYMENT_TYPE,   
                                             LNCONTMASTER.PERIOD_PAYMENT,   
                                             LNLOANTYPE.LOANPERMGRP_CODE,   
                                             LNCONTMASTER.CONTRACT_STATUS,   
                                            LNCONTMASTER.INT_CONTINTTYPE as CONTRACTINT_TYPE,   
                                             LNCONTMASTER.INT_CONTINTRATE as  CONTRACT_INTEREST,   
                                             LNCONTMASTER.LOANAPPROVE_DATE,   
                                             LNCONTMASTER.STARTCONT_DATE,   
                                             LNCONTMASTER.INT_CONTINTTYPE,   
                                             LNCONTMASTER.INT_CONTINTRATE,   
                                             LNCONTMASTER.INT_CONTINTTABCODE,   
                                             LNCONTMASTER.INT_CONTINTINCREASE,   
                                             LNCONTMASTER.INT_INTSTEPTYPE,   
                                             LNCONTMASTER.PERIOD_PAYAMT,   
                                             LNCONTMASTER.CONTLAW_STATUS,   
                                             LNCONTMASTER.PRINCIPAL_TRANSBAL,   
                                             LNCONTMASTER.PAYMENT_STATUS,   
                                             LNLOANTYPE.CLEARINSURE_FLAG,   
                                             LNCONTMASTER.INSURECOLL_FLAG   ,
                                             LNLOANTYPE.interest_method,
                                             LNLOANTYPE.od_flag, LNLOANTYPE.shrstk_buytype
                                        FROM LNCONTMASTER,   
                                             LNLOANTYPE  
                                       WHERE ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                                             ( LNCONTMASTER.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                             ( ( lncontmaster.member_no = '" + member_no + @"' ) AND  
                                             ( lncontmaster.principal_balance + lncontmaster.withdrawable_amt > 0 ) AND  
                                             ( lncontmaster.contract_status > 0 ) AND  
                                             ( LNCONTMASTER.MEMCOOP_ID = '" + ls_memcoopid + @"' ) )   
                                    ORDER BY LNCONTMASTER.LOANCONTRACT_NO ASC  ";
                Sdt dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    dw_clear.Reset();

                    int rowCount = dt.GetRowCount();
                    for (int i = 0; i < rowCount; i++)
                    {
                        try { ls_contno = dt.Rows[i]["loancontract_no"].ToString(); }
                        catch { ls_contno = ""; }
                        try
                        {
                            ls_conttype = dt.Rows[i]["loantype_code"].ToString();
                        }
                        catch { ls_conttype = ""; }
                        try
                        {
                            ls_prefix = dt.Rows[i]["prefix"].ToString();
                        }
                        catch { ls_prefix = ""; }
                        try
                        {// คำนำหน้าประเภทสัญญา
                            ls_permgrp = dt.Rows[i]["loanpermgrp_code"].ToString();
                        }
                        catch { ls_permgrp = ""; }
                        try
                        {// กลุ่มวงเงินกู้
                            li_paytype = Convert.ToDecimal(dt.Rows[i]["loanpayment_type"]);
                        }
                        catch { li_paytype = 0; }
                        try
                        {
                            li_period = Convert.ToDecimal(dt.Rows[i]["last_periodpay"]);
                        }
                        catch { li_period = 0; }
                        try
                        {
                            li_contstatus = Convert.ToDecimal(dt.Rows[i]["contract_status"]);
                        }
                        catch { li_contstatus = 0; }
                        try
                        {
                            li_continttype = Convert.ToDecimal(dt.Rows[i]["contractint_type"]);

                        }
                        catch { li_continttype = 0; }
                        try
                        {
                            ldc_intrate = Convert.ToDecimal(dt.Rows[i]["contract_interest"]);
                        }
                        catch { ldc_intrate = 0; }
                        try
                        {
                            ldc_payment = Convert.ToDecimal(dt.Rows[i]["period_payment"]);
                        }
                        catch { ldc_payment = 0; }
                        try
                        {
                            ldc_appvamt = Convert.ToDecimal(dt.Rows[i]["loanapprove_amt"]);
                        }
                        catch { ldc_appvamt = 0; }
                        try
                        {
                            ldc_withdrawable = Convert.ToDecimal(dt.Rows[i]["withdrawable_amt"]);
                        }
                        catch { ldc_withdrawable = 0; }
                        try
                        {
                            ldc_balance = Convert.ToDecimal(dt.Rows[i]["principal_balance"]);
                        }
                        catch { ldc_balance = 0; }
                        try
                        {
                            ldc_intarrear = Convert.ToDecimal(dt.Rows[i]["interest_arrear"]);
                        }
                        catch { ldc_intarrear = 0; }
                        try
                        {
                            ldc_rkeepprin = Convert.ToDecimal(dt.Rows[i]["rkeep_principal"]);
                        }
                        catch { ldc_rkeepprin = 0; }
                        try
                        {
                            ldc_rkeepint = Convert.ToDecimal(dt.Rows[i]["rkeep_interest"]);
                        }
                        catch { ldc_rkeepint = 0; }
                        try
                        {
                            ldtm_lastcalint = Convert.ToDateTime(dt.Rows[i]["lastcalint_date"]);
                        }
                        catch { ldtm_lastcalint = DateTime.Now; }
                        try
                        {
                            ldtm_lastproc = Convert.ToDateTime(dt.Rows[i]["lastprocess_date"]);
                        }
                        catch { ldtm_lastproc = DateTime.Now; }
                        try
                        {
                            ldtm_approve = Convert.ToDateTime(dt.Rows[i]["loanapprove_date"]);
                        }
                        catch { ldtm_approve = DateTime.Now; }
                        try
                        {
                            ldtm_startcont = Convert.ToDateTime(dt.Rows[i]["startcont_date"]);
                        }
                        catch { ldtm_startcont = DateTime.Now; }
                        try
                        {
                            li_intcontinttype = Convert.ToDecimal(dt.Rows[i]["int_continttype"]);
                        }
                        catch { li_intcontinttype = 0; }
                        try
                        {
                            ldc_intcontintrate = Convert.ToDecimal(dt.Rows[i]["int_contintrate"]);
                        }
                        catch { ldc_intcontintrate = 0; }
                        try
                        {
                            ls_intcontintcode = dt.Rows[i]["int_continttabcode"].ToString();
                        }
                        catch { ls_intcontintcode = ""; }
                        try
                        {
                            ldc_intincrease = Convert.ToDecimal(dt.Rows[i]["int_contintincrease"]);
                        }
                        catch { ldc_intincrease = 0; }
                        try
                        {
                            li_intsteptype = Convert.ToDecimal(dt.Rows[i]["int_intsteptype"]);
                        }
                        catch { li_intsteptype = 0; }
                        try
                        {
                            li_periodamt = Convert.ToDecimal(dt.Rows[i]["period_payamt"]);
                        }
                        catch { li_periodamt = 0; }
                        try
                        {
                            li_transfersts = Convert.ToDecimal(dt.Rows[i]["transfer_status"]);
                        }
                        catch { li_transfersts = 0; }
                        try
                        {
                            ls_coopid = dt.Rows[i]["coop_id"].ToString();
                        }
                        catch { ls_coopid = ls_memcoopid; }
                        try
                        {

                            //li_contlaw = Convert.ToDecimal(dt.Rows[i]["contlaw_status"]);
                            li_contlaw = Convert.ToDecimal(dt.Rows[i]["shrstk_buytype"]); //wa เปลียนเป็น ธกส 1 2 ให้นับรวมหนี้เพือคิดหุ้น
                        }
                        catch { li_contlaw = 0; }
                        try
                        {
                            ldc_transbal = Convert.ToDecimal(dt.Rows[i]["principal_transbal"]);
                        }
                        catch { ldc_transbal = 0; }
                        try
                        {
                            li_paystatus = Convert.ToDecimal(dt.Rows[i]["payment_status"]);
                        }
                        catch { li_paystatus = 0; }
                        try
                        {
                            li_clearinsure = Convert.ToDecimal(dt.Rows[i]["insurecoll_flag"]);
                        }
                        catch { li_clearinsure = 0; }

                        try
                        {
                            li_interestmethod = Convert.ToInt16(dt.Rows[i]["interest_method"]);
                        }
                        catch { li_interestmethod = 1; }
                        try
                        {
                            li_od_flag = Convert.ToInt16(dt.Rows[i]["od_flag"]);
                        }
                        catch { li_od_flag = 0; }


                        dw_clear.InsertRow(i + 1);
                        dw_clear.SetItemString(i + 1, "loancontract_no", ls_contno);
                        dw_clear.SetItemString(i + 1, "coop_id", state.SsCoopId);
                        dw_clear.SetItemString(i + 1, "concoop_id", state.SsCoopControl);
                        dw_clear.SetItemString(i + 1, "loantype_code", ls_conttype);
                        dw_clear.SetItemString(i + 1, "prefix", ls_prefix);
                        dw_clear.SetItemDecimal(i + 1, "loanpayment_type", li_paytype);
                        dw_clear.SetItemDecimal(i + 1, "period_payment", ldc_payment);
                        dw_clear.SetItemDecimal(i + 1, "loanapprove_amt", ldc_appvamt);
                        dw_clear.SetItemDecimal(i + 1, "withdrawable_amt", ldc_withdrawable);
                        dw_clear.SetItemDecimal(i + 1, "principal_balance", ldc_balance);
                        dw_clear.SetItemDecimal(i + 1, "last_periodpay", li_period);
                        dw_clear.SetItemDecimal(i + 1, "minperiod_pay", li_minperiod);
                        dw_clear.SetItemDecimal(i + 1, "minpercent_pay", ldc_minpay);
                        dw_clear.SetItemDateTime(i + 1, "lastcalint_date", ldtm_lastcalint);
                        dw_clear.SetItemDecimal(i + 1, "contract_status", li_contstatus);
                        dw_clear.SetItemString(i + 1, "permissgroup_code", ls_permgrp);
                        // dw_clear.SetItemDecimal(i + 1, "clear_status", li_status);
                        dw_clear.SetItemDateTime(i + 1, "lastprocess_date", ldtm_lastproc);
                        dw_clear.SetItemDecimal(i + 1, "contractint_type", li_continttype);
                        dw_clear.SetItemDecimal(i + 1, "contract_interest", ldc_intrate);
                        dw_clear.SetItemDecimal(i + 1, "rkeep_principal", ldc_rkeepprin);
                        dw_clear.SetItemDecimal(i + 1, "rkeep_interest", ldc_rkeepint);
                        dw_clear.SetItemDecimal(i + 1, "interest_arrear", ldc_intarrear);//ldc_intarrear
                        dw_clear.SetItemDateTime(i + 1, "loanapprove_date", ldtm_approve);
                        dw_clear.SetItemDateTime(i + 1, "startcont_date", ldtm_startcont);
                        dw_clear.SetItemDecimal(i + 1, "int_continttype", li_intcontinttype);
                        dw_clear.SetItemDecimal(i + 1, "int_contintrate", ldc_intcontintrate);
                        dw_clear.SetItemString(i + 1, "int_continttabcode", ls_intcontintcode);
                        dw_clear.SetItemDecimal(i + 1, "int_contintincrease", ldc_intincrease);
                        dw_clear.SetItemDecimal(i + 1, "int_intsteptype", li_intsteptype);
                        dw_clear.SetItemDecimal(i + 1, "period_payamt", li_periodamt);
                        dw_clear.SetItemDecimal(i + 1, "contlaw_status", li_contlaw);
                        dw_clear.SetItemDecimal(i + 1, "payment_status", li_paystatus);
                        dw_clear.SetItemDecimal(i + 1, "principal_transbal", ldc_transbal);
                        dw_clear.SetItemDecimal(i + 1, "insurecoll_flag", li_clearinsure);
                        dw_clear.SetItemDecimal(i + 1, "countpay_flag", li_od_flag);

                        dw_clear.SetItemDecimal(i + 1, "intestimate_amt", Convert.ToDecimal(ldc_intestim30));
                        dw_clear.SetItemDecimal(i + 1, "intclear_amt", Convert.ToDecimal(ldc_intestim30));


                    }
                }
            }
            catch
            {

            }
        }
        private void JsSetRatio(decimal ai_ratiotype, decimal adc_percent, string as_ratio, ref decimal adc_mul, ref decimal adc_div)
        {

            try
            {
                decimal li_pos = 0;

                if (ai_ratiotype == 1)
                {
                    adc_div = 100;
                    adc_mul = adc_percent * 100;

                }
                else if (ai_ratiotype == 2)
                {

                    li_pos = as_ratio.IndexOf(":");

                    if (li_pos > 0)
                    {
                        decimal li_len = 1;
                        if (li_pos == 1) { li_len = 1; }
                        adc_mul = Convert.ToDecimal(as_ratio.Substring(0, Convert.ToInt16(li_len)));
                        li_len = as_ratio.Length - li_pos - 1;
                        adc_div = Convert.ToDecimal(as_ratio.Substring(Convert.ToInt16(li_pos + 1), Convert.ToInt16(li_len)));
                    }

                }
                else
                {

                }


            }
            catch { }

        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "refreshDW")
            {
                RefreshDW();
            }
            else if (eventArg == "jsLoanclrchg")
            {
                JsLoanclrchg();
            }
            else if (eventArg == "jscolldetail")
            {
                Jscolldetail();
            }


        }

        public void WebDialogLoadEnd()
        {
            dw_collright.SaveDataCache();
            dw_clear.SaveDataCache();
        }

        private void of_setloanclearstatus()
        {
            //wa
            Ltjspopupclr.Text = "";
            for (int k = 1; k <= dw_clear.RowCount; k++)
            {

                dw_clear.SetItemDecimal(k, "clear_status", 0);

            }

            //ตั้งค่าการหักชำระหนี้เก่า
            string loantypeclr, loantype, contclr_no;
            string ls_memcoopid = state.SsCoopControl;
            String member_no = HdMemberNo.Value;
            decimal ldc_minpay, li_minperiod, li_checkcontclr, last_payperid, li_calintflag = 0;
            string loantypereq_code = HdLoantype.Value;
            String sqlStr1 = @"SELECT LNLOANTYPECLR.LOANTYPE_CODE,   
                                 LNLOANTYPECLR.LOANTYPE_CLEAR,   
                                 LNLOANTYPECLR.MINPERIOD_PAY,   
                                 LNLOANTYPECLR.MINPERCENT_PAY,   
                                 LNLOANTYPECLR.CHKCONTCREDIT_FLAG,   
                                 LNLOANTYPE.LOANRIGHT_TYPE,   
                                 LNLOANTYPECLR.CONTRACT_STATUS  ,calint_flag
                            FROM LNLOANTYPECLR,   
                                 LNLOANTYPE  
                           WHERE ( LNLOANTYPECLR.LOANTYPE_CLEAR = LNLOANTYPE.LOANTYPE_CODE ) and  
                                 ( LNLOANTYPECLR.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                 ( ( LNLOANTYPECLR.LOANTYPE_CODE = '" + loantypereq_code + "' ) )    ";

            Sdt dt1 = WebUtil.QuerySdt(sqlStr1);
            if (dt1.Next())
            {
                for (int i = 0; i < dt1.GetRowCount(); i++)
                {
                    loantypeclr = Convert.ToString(dt1.Rows[i]["LOANTYPE_CLEAR"]);
                    li_minperiod = Convert.ToDecimal(dt1.Rows[i]["minperiod_pay"]);
                    ldc_minpay = Convert.ToDecimal(dt1.Rows[i]["minpercent_pay"]);
                    li_checkcontclr = Convert.ToDecimal(dt1.Rows[i]["contract_status"]);
                    li_calintflag = Convert.ToDecimal(dt1.Rows[i]["calint_flag"]);
                    for (int k = 1; k <= dw_clear.RowCount; k++)
                    {
                        contclr_no = dw_clear.GetItemString(k, "loancontract_no");
                        loantype = dw_clear.GetItemString(k, "loantype_code");
                        last_payperid = dw_clear.GetItemDecimal(k, "last_periodpay");
                        if (loantype == loantypeclr) //&& li_minperiod <= last_payperid
                        {
                            dw_clear.SetItemDecimal(k, "clear_status", 1);
                            if (li_minperiod > last_payperid)
                            {
                                Ltjspopupclr.Text += WebUtil.WarningMessage(contclr_no + " สัญญาชำระไม่ถึงตามเกณฑ์ที่กำหนด ต้องส่งมาแล้วขั้นต่ำ " + li_minperiod.ToString());
                            }
                        }
                        if (li_calintflag == 0)
                        {
                            dw_clear.SetItemDecimal(k, "intestimate_amt", 0);
                            dw_clear.SetItemDecimal(k, "intclear_amt", 0);
                        }

                    }
                }
            }



        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {


            HdChkConfirm.Value = "0";
            decimal loampermiss_amt = 0;
            decimal operateFlag = 0, coll_amt = 0;
            decimal loanper_balance = 0, base_percen = 0;
            decimal collful_amt = 0, colluse_amt = 0;
            String ref_collno_old = "";
            String ref_collno_new = "";
            String as_memno = HdMemberNo.Value;
            int coll_row = 0;
            dw_coll.Reset();
            int ii = 1;
            Boolean chk = true;
            decimal oper_flag;
            for (int j = 1; j <= dw_collright.RowCount; j++)
            {
                try
                {
                    oper_flag = dw_collright.GetItemDecimal(j, "operate_flag");
                }
                catch { oper_flag = 0; }

                if (oper_flag == 1)
                {
                    String sql_depmemno = "select depttype_code from  dpdeptmaster where prncbal > 0 and member_no = '" + as_memno.Trim() + "' and deptaccount_no = '" + dw_collright.GetItemString(j, "ref_collno").Trim() + "'";
                    Sdt dt_deptno = WebUtil.QuerySdt(sql_depmemno);
                    while (dt_deptno.Next())
                    {
                        if (ii == 1)
                        {
                            ref_collno_old = dt_deptno.GetString("depttype_code").Trim();
                        }

                        if (ii > 1)
                        {
                            ref_collno_new = dt_deptno.GetString("depttype_code").Trim();
                            if (ref_collno_old != ref_collno_new)
                            {
                                chk = false;
                                break;
                            }
                        }

                        ii++;
                    }
                }
            }
            if (chk)
            {
                for (int i = 1; i <= dw_collright.RowCount; i++)
                {

                    operateFlag = dw_collright.GetItemDecimal(i, "operate_flag");
                    coll_amt = dw_collright.GetItemDecimal(i, "collbal_amt");
                    colluse_amt = dw_collright.GetItemDecimal(i, "colluse_amt");
                    collful_amt = dw_collright.GetItemDecimal(i, "collfull_amt");
                    loanper_balance = dw_collright.GetItemDecimal(i, "permiss_amt");
                    base_percen = dw_collright.GetItemDecimal(i, "permiss_percent");
                    if (operateFlag == 1)
                    {
                        HdChkConfirm.Value = "1";
                        loampermiss_amt += loanper_balance;

                        coll_row = dw_coll.InsertRow(0);
                        dw_coll.SetItemString(coll_row, "loancolltype_code", dw_collright.GetItemString(i, "loancolltype_code"));
                        dw_coll.SetItemString(coll_row, "ref_collno", dw_collright.GetItemString(i, "ref_collno"));
                        dw_coll.SetItemString(coll_row, "description", dw_collright.GetItemString(i, "description"));
                        dw_coll.SetItemDecimal(coll_row, "coll_amt", collful_amt);
                        dw_coll.SetItemDecimal(coll_row, "coll_useamt", colluse_amt);
                        dw_coll.SetItemDecimal(coll_row, "coll_balance", loanper_balance);
                        dw_coll.SetItemDecimal(coll_row, "base_percent", base_percen);
                    }
                }
                if (HdChkConfirm.Value == "1")
                {
                    dw_collright.SetFilter("operate_flag = 1 ");
                    dw_collright.Filter();

                }
                HdLoanMiss.Value = loampermiss_amt.ToString();

                str_itemchange strList = new str_itemchange();
                strList = WebUtil.nstr_itemchange_session(this);
                strList.xml_collright = dw_collright.Describe("DataWindow.Data.XML");
                if (dw_clear.RowCount > 0)
                {
                    strList.xml_clear = dw_clear.Describe("DataWindow.Data.XML");
                }
                else
                {
                    strList.xml_clear = "";
                }
                strList.xml_guarantee = dw_coll.Describe("DataWindow.Data.XML");

                Hdxmlcoll.Value = strList.xml_guarantee;
                Hdxmlclear.Value = strList.xml_clear;

            }
            else
            {
                Response.Write("<script language='javascript'>alert('สิทธิการกู้นี้อนุญาติให้เลือกเงินฝากได้แค่ประเภทเดียวกันเท่านั้น กรุณาตรวจสอบ');</script>");

                HdChkConfirm.Value = "2";
                //  dw_collright.Reset();

                // Ltjspopupclr.Text += WebUtil.WarningMessage(" สัญญาชำระไม่ถึงตามเกณฑ์ที่กำหนด ต้องส่งมาแล้วขั้นต่ำ ");
            }
        }
        protected void RefreshDW()
        {

        }
    }
}
