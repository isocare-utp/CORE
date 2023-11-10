using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;
using System.Text;

namespace Saving.Applications.shrlon.w_sheet_ln_nplreport_ctrl
{
    public partial class w_sheet_ln_nplreport : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string postSubmitReport { get; set; }
        [JsPostBack]
        public string postDeleteReport { get; set; }

        public void InitJsPostBack()
        {
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                tbYearTh.Text = (state.SsWorkDate.Year + 543).ToString();
                tbMonth.Text = state.SsWorkDate.Month.ToString();
                string sql = "select distinct year_th, to_char(year_th) as txt, 1 as sorter from lnnplreportmaster where coop_id = '" + state.SsCoopControl + @"' union select 0 , '', 0 from dual  order by sorter, year_th desc";
                DataTable dt = WebUtil.Query(sql);
                ddDelYearTh.DataTextField = "txt";
                ddDelYearTh.DataValueField = "year_th";
                ddDelYearTh.DataSource = dt;
                ddDelYearTh.DataBind();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postSubmitReport)
            {
                try
                {
                    int month = 0, year = 0;
                    try
                    {
                        month = int.Parse(tbMonth.Text);
                        if (month < 1 || month > 12)
                        {
                            throw new Exception();
                        }
                        year = int.Parse(tbYearTh.Text);
                        if (year < 2400 || year > 2999)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        throw new Exception("วันที่ออกรายงาน เดือน-ปี ไม่ถูกต้อง");
                    }
                    BuildReport(month, year);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == postDeleteReport)
            {
                try
                {
                    int month = 0, year = 0, seq_no = 0;
                    try
                    {
                        month = int.Parse(ddDelMonth.SelectedValue);
                        if (month < 1 || month > 12)
                        {
                            throw new Exception();
                        }
                        year = int.Parse(ddDelYearTh.SelectedValue);
                        if (year < 2400 || year > 2999)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        throw new Exception("วันที่ออกรายงาน เดือน-ปี ไม่ถูกต้อง");
                    }
                    try
                    {
                        seq_no = int.Parse(ddDelSeqNo.SelectedValue);
                    }
                    catch
                    {
                        throw new Exception("ลำดับรายงานไม่ถูกต้อง");
                    }
                    if (seq_no <= 0) throw new Exception("ลำดับรายงานต้องมากกว่า 0");
                    DeleteReport(month, year, seq_no);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        private string f_npl_getcollmast_report(string loancontract_no, int lawtype_code)
        {
            string ls_coll;
            if (lawtype_code != 4)
            {
                return "";
            }
            string sql = @"
                SELECT 
	                LNCONTCOLL.LOANCONTRACT_NO,   
	                LNCOLLMASTER.MORTGAGE_DATE,   
	                LNCONTCOLL.DESCRIPTION,   
	                LNNPLCOLLMAST.JPK_ESTPRICE,   
	                LNNPLCOLLMAST.SALE_PRICE,   
	                LNCONTCOLL.LOANCOLLTYPE_CODE,   
	                LNCONTCOLL.REF_COLLNO,   
	                LNCOLLMASTER.ESTIMATE_PRICE  
                FROM 
	                LNCOLLMASTER,   
	                LNCONTCOLL,   
	                LNNPLCOLLMAST  
                WHERE 
	                ( lncontcoll.ref_collno = lncollmaster.collmast_no (+)) and
	                ( lncontcoll.ref_collno = lnnplcollmast.ref_collno (+)) and
	                ( ( LNCONTCOLL.LOANCONTRACT_NO = '" + loancontract_no + @"' ) )
            ";
            List<string> list = new List<string>();
            Sdt dt = WebUtil.QuerySdt(sql);
            while (dt.Next())
            {
                StringBuilder sb = new StringBuilder("");
                string loancolltypeCode = dt.GetString("loancolltype_code");
                if (loancolltypeCode == "01" || loancolltypeCode == "02")
                {
                    sb.Append(dt.GetString("DESCRIPTION") + " (" + dt.GetString("ref_collno") + ")");
                }
                else if (loancolltypeCode == "03")
                {
                    sb.Append("เงินฝาก " + dt.GetString("DESCRIPTION"));
                }
                else
                {
                    sb.Append(dt.GetString("DESCRIPTION"));
                }
                sb.Append(" ");

                if (dt.GetDecimal("estimate_price") > 0)
                {
                    sb.Append("สกส. " + dt.GetDecimal("estimate_price").ToString("#,##0.00") + " ");
                }

                if (dt.GetDecimal("jpk_estprice") > 0)
                {
                    sb.Append("จพค. " + dt.GetDecimal("jpk_estprice").ToString("#,##0.00") + " ");
                }

                if (dt.GetDecimal("sale_price") > 0)
                {
                    sb.Append("ขาย " + dt.GetDecimal("sale_price").ToString("#,##0.00") + " ");
                }
                list.Add(sb.ToString());
            }

            if (list.Count <= 0)
            {
                return "";
            }

            ls_coll = "หลักประกัน:";
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    ls_coll = ls_coll + "   (1.)" + list[i].ToString();
                }
                else
                {
                    ls_coll = ls_coll + "  ,  (" + (i + 1) + ")" + list[i].ToString();
                }
            }
            return ls_coll;
        }

        private void BuildReport(int month, int yearThai)
        {
            DateTime calIntDate = new DateTime(yearThai - 543, month, 1);
            if (!state.IsWritable)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("คุณยังไม่มีสิทธิ์ในการบันทึกข้อมูลหน้าจอนี้");
                return;
            }
            ExecuteDataSource exec = new ExecuteDataSource(this);
            Sdt lds_report;
            int[] li_lawtypes;
            int li_tyear, li_month, li_seq_no;

            li_tyear = yearThai;
            li_month = month;
            if (li_tyear <= 0)
            {
                throw new Exception("");
            }
            else if (li_month <= 0)
            {
                throw new Exception("กรุณากรอกหมายเลขเดือนที่ต้องการ");
            }

            try
            {
                string sql = "select max(seq_no) as max_seq from lnnplreportmaster where year_th = " + li_tyear + " and month = " + li_month;
                DataTable dt0 = WebUtil.Query(sql);
                li_seq_no = Convert.ToInt32(dt0.Rows[0]["max_seq"]);
            }
            catch
            {
                li_seq_no = 0;
            }

            //DB
            decimal ldc_year_th, ldc_month, ldc_seq_no, ldc_principal_balance, ldc_int_balance, ldc_margin, ldc_total_balance;
            decimal ldc_share_balance, ldc_deposit_balance, ldc_half_price, ldc_total_contcoll, ldc_total_percent, ldc_percent;
            decimal ldc_lawtype_code, ldc_lawtype_code_old, ldc_indict_prnamt, ldc_indict_intamt, ldc_prince_last_year;
            decimal ldc_total_int_last_year, ldc_total_princ_last_year, ldc_int_last_year, ldc_issame;
            string ls_loancontract_no, ls_loantype_code, ls_member_no, ls_member_fullname, ls_lawtype_desc, ls_lawtype_desc_old;
            string ls_case_redno, ls_case_blackno, ls_remark, ls_all_contcoll, ls_entry_id, ls_collmast_all;
            //DateTime ;
            string ldtm_lastpayment_date, ldtm_loancontract_date, ldtm_indict_date, ldtm_judge_date, ldtm_entry_date;

            li_lawtypes = new int[] { 1, 2, 3, 4 };

            string sqlReportCreator = @"
                select 
                  distinct
                  lncontmaster.loancontract_no ,
                  lncontmaster.principal_arrear,
                  lncontmaster.principal_balance,
                  lncontmaster.interest_accum,
                  lncontmaster.interest_arrear,
                  lncontmaster.lastpayment_date ,
                  lncontmaster.startcont_date ,
                  lnloantype.loangroup_code,
                  lnloantype.loantype_code,
                  lnloantype.loantype_desc,
                  lnnplmaster.advance_payamt ,
                  lnnplmaster.case_redno,
                  lnnplmaster.case_blackno,
                  lnnplmaster.debtor_class ,
                  lnnplmaster.indict_date ,
                  lnnplmaster.indict_intamt,
                  lnnplmaster.indict_prnamt,
                  lnnplmaster.int_balance ,
                  lnnplmaster.int_last_year ,
                  lnnplmaster.int_lastcal ,
                  lnnplmaster.int_payment_year ,
                  lnnplmaster.interest_arrear ,
                  lnnplmaster.judge_date ,
                  lnnplmaster.last_int_date ,
                  lnnplmaster.lawtype_code_old,
                  lnnplmaster.lawtype_code,
                  lnnplmaster.loancontract_date,
                  lnnplmaster.margin ,
                  lnnplmaster.member_no ,
                  lnnplmaster.mortgage_halfpayment as halfprice,
                  lnnplmaster.percent_settingpayment,
                  lnnplmaster.period_payment ,
                  lnnplmaster.prince_last_year ,
                  lnnplmaster.princ_balance ,
                  lnnplmaster.princ_payment_year ,
                  lnnplmaster.payment_sum ,
                  lnnplmaster.received_date ,
                  lnnplmaster.remark ,
                  lnnplmaster.result_require ,
                  lnnplmaster.status ,
                  lnnplmaster.work_order ,
                  lnnplmaster.setting_payment,
                  lnnplmaster.follow_seq,
                  lnucfnpllawtype.lawtype_desc ,
                  mbmembmaster.memb_name ,
                  mbmembmaster.memb_surname,
                  mbucfprename.prename_desc,
                  type_old.lawtype_desc as lawtype_desc_old,
                  " + yearThai + @" as tyear,
                  (trim( substr( lnnplmaster.loancontract_no , 1 , 9))) as loan2,
                  case(lnnplmaster.lawtype_code) when lnnplmaster.lawtype_code_old then 1 else 0  end as issame,
                  lnnplfollowmaster.advance_amt as margin
                from lnnplmaster 
                  inner join lncontmaster on 
                    lnnplmaster.coop_id         = lncontmaster.coop_id and  
                    lnnplmaster.loancontract_no = lncontmaster.loancontract_no
                  inner join lnloantype on 
                    lnnplmaster.coop_id         = lnloantype.coop_id and  
                    lncontmaster.loantype_code  = lnloantype.loantype_code
                  inner join mbmembmaster on 
                    lnnplmaster.coop_id   = mbmembmaster.coop_id and  
                    lnnplmaster.member_no = mbmembmaster.member_no
                  inner join mbucfprename on 
                    mbmembmaster.prename_code = mbucfprename.prename_code
                  inner join lnucfnpllawtype on 
                    lnnplmaster.coop_id       = lnucfnpllawtype.coop_id and  
                    lnnplmaster.lawtype_code  = lnucfnpllawtype.lawtype_code
                  left join lnucfnpllawtype type_old on 
                    lnnplmaster.coop_id           = type_old.coop_id and  
                    lnnplmaster.lawtype_code_old  = type_old.lawtype_code
                  left join lnnplfollowmaster on 
                    lnnplmaster.coop_id     = lnnplfollowmaster.coop_id and 
                    lnnplmaster.member_no   = lnnplfollowmaster.member_no and 
                    lnnplmaster.follow_seq  = lnnplfollowmaster.follow_seq
                where lnnplmaster.lawtype_code in (1,2,3,4) order by member_no
            ";
            lds_report = WebUtil.QuerySdt(sqlReportCreator);

            li_seq_no += 1;
            while (lds_report.Next())
            {
                //require
                ldc_lawtype_code = lds_report.GetDecimal("LAWTYPE_CODE");
                ldc_year_th = li_tyear;
                ldc_month = li_month;
                ldc_seq_no = li_seq_no;
                ls_loancontract_no = lds_report.GetString("LOANCONTRACT_NO");
                ls_loantype_code = lds_report.GetString("LOANTYPE_CODE");
                if (string.IsNullOrEmpty(ls_loantype_code))
                {
                    ls_loantype_code = "00";
                }

                ls_member_no = lds_report.GetString("MEMBER_NO");
                ls_member_fullname = lds_report.GetString("PRENAME_DESC") + lds_report.GetString("MEMB_NAME") + "  " + lds_report.GetString("MEMB_SURNAME");
                ldc_principal_balance = Math.Round(lds_report.GetDecimal("PRINCIPAL_BALANCE"), 2);
                //	//round( IF( lnnplmaster_lawtype_code = 4, f_dnum( INT_BALANCE ) , 0) , 2)
                //	if ldc_lawtype_code = 4 then
                //		ldc_int_balance = round( f_dnum(  lds_report.getitemdecimal( li_i, "INT_BALANCE")  ), 2)
                //	else
                //		ldc_int_balance = 0
                //	end if

                ldc_int_balance = Math.Round(lds_report.GetDecimal("INT_BALANCE"), 2);

                ldc_margin = Math.Round(lds_report.GetDecimal("MARGIN"), 2);
                if (ldc_margin < 0)
                {
                    ldc_margin = 0;
                }

                // เพิ่มการนับ
                int advance_count = 0;
                try
                {
                    string sqlAdvanceCount = "select * from lnnplmaster where coop_id={0} and member_no={1} and follow_seq={2} and follow_seq > 0 order by loancontract_no";
                    sqlAdvanceCount = WebUtil.SQLFormat(sqlAdvanceCount, state.SsCoopControl, ls_member_no, lds_report.GetDecimal("follow_seq"));
                    DataTable dtAdvanceCount = WebUtil.Query(sqlAdvanceCount);
                    advance_count = dtAdvanceCount.Rows.Count;
                }
                catch { }
                advance_count = advance_count < 1 ? 1 : advance_count;
                ldc_margin = ldc_margin / advance_count;


                //round( cmp_principal + cmp_int +  if(  lnnplmaster_lawtype_code = 4 ,  cmp_margin , 0) , 2)
                if (ldc_lawtype_code == 4)
                {
                    ldc_total_balance = ldc_principal_balance + ldc_int_balance + ldc_margin;
                }
                else
                {
                    ldc_total_balance = ldc_principal_balance + ldc_int_balance;
                }
                ldtm_lastpayment_date = "to_date('" + lds_report.GetDateEn("LASTPAYMENT_DATE") + "', 'yyyy-mm-dd')";
                ldc_share_balance = 0;
                ldc_deposit_balance = 0;
                ldc_half_price = Math.Round(lds_report.GetDecimal("HALFPRICE"), 2);
                ldc_total_contcoll = ldc_share_balance + ldc_deposit_balance + ldc_half_price;
                //if((((f_dnum(cmp_sumdept) - f_dnum(cmp_half)) * f_dnum(percent_settingpayment)) / 100)  < 0, 0, (((f_dnum(cmp_sumdept) - f_dnum(cmp_half)) * f_dnum(percent_settingpayment)) / 100))
                ldc_percent = lds_report.GetDecimal("PERCENT_SETTINGPAYMENT");
                //	if ldc_lawtype_code = 4 then
                //		ldc_total_percent = round( (((ldc_total_balance - ldc_margin) -  ldc_total_contcoll) * ldc_percent) / 100 , 2)
                //	else
                //		ldc_total_percent = round( (((ldc_total_balance - 0) -  ldc_total_contcoll) * ldc_percent) / 100 , 2)
                //	end if
                //	ldc_total_percent = round((((ldc_principal_balance + ldc_int_balance) - ldc_total_contcoll) * ldc_percent) / 100, 2)

                ldc_total_percent = (ldc_principal_balance + ldc_int_balance) - ldc_total_contcoll;
                if (ldc_total_percent > 0)
                {
                    ldc_total_percent = (ldc_total_percent * ldc_percent) / 100m;
                }
                else
                {
                    ldc_total_percent = 0;
                }

                /***** ldc_lawtype_code = f_dnum(  lds_report.getitemdecimal( li_i, "LAWTYPE_CODE")  ) *****/
                ls_lawtype_desc = lds_report.GetString("LAWTYPE_DESC");
                ldc_lawtype_code_old = lds_report.GetDecimal("LAWTYPE_CODE_OLD");
                ls_lawtype_desc_old = lds_report.GetString("LAWTYPE_DESC_OLD");
                ldtm_loancontract_date = lds_report.GetDate("LOANCONTRACT_DATE").Year < 1700 ? "NULL" : "to_date('" + lds_report.GetDateEn("LOANCONTRACT_DATE") + "', 'yyyy-mm-dd')";
                ldc_indict_prnamt = Math.Round(lds_report.GetDecimal("INDICT_PRNAMT"), 2);
                ldc_indict_intamt = Math.Round(lds_report.GetDecimal("INDICT_PRNAMT"), 2);
                ldtm_indict_date = lds_report.GetDate("INDICT_DATE").Year < 1700 ? "NULL" : "to_date('" + lds_report.GetDateEn("INDICT_DATE") + "', 'yyyy-mm-dd')";
                ls_case_redno = lds_report.GetString("CASE_REDNO");
                ls_case_blackno = lds_report.GetString("CASE_BLACKNO");
                ldtm_judge_date = lds_report.GetDate("JUDGE_DATE").Year < 1700 ? "NULL" : "to_date('" + lds_report.GetDateEn("JUDGE_DATE") + "', 'yyyy-mm-dd')";
                ldc_prince_last_year = Math.Round(lds_report.GetDecimal("PRINCE_LAST_YEAR"), 2);
                ldc_int_last_year = Math.Round(lds_report.GetDecimal("INT_LAST_YEAR"), 2);
                ldc_total_princ_last_year = ldc_int_last_year - ldc_principal_balance;
                ldc_total_int_last_year = ldc_int_last_year - ldc_int_balance;
                ls_remark = lds_report.GetString("REMARK");
                ldtm_entry_date = "to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')";
                ls_entry_id = state.SsUsername;
                //NPLMASTER.LAWTYPE_CODE) when LNNPLMASTER.LAWTYPE_CODE_OLD
                if (ldc_lawtype_code == ldc_lawtype_code_old)
                {
                    ldc_issame = 1;
                }
                else
                {
                    ldc_issame = 0;
                }
                ls_collmast_all = f_npl_getcollmast_report(lds_report.GetString("LOAN2"), Convert.ToInt32(ldc_lawtype_code));
                decimal int_estimate = 0;
                try
                {
                    int_estimate = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopControl, ls_loancontract_no, calIntDate);
                }
                catch { }


                string sqlInsert = @"
                    insert into lnnplreportmaster (
                            year_th,					    month,							    seq_no,						        loancontract_no,			    loantype_code,					member_no,
                            member_fullname,		        principal_balance,				    int_balance,				        advance_amt,                    total_balance,					lastpayment_date,
                            share_balance,				    deposit_balance,				    half_price,					        total_contcoll,				    total_percent,					percent,
                            lawtype_code,				    lawtype_desc,					    lawtype_code_old,			        lawtype_desc_old,			    loancontract_date,				indict_prnamt,
                            indict_intamt,				    indict_date,					    case_redno,					        case_blackno,				    judge_date,						prince_last_year,
                            int_last_year,				    total_princ_last_year,			    total_int_last_year,		        remark,						    entry_date,						entry_id,
                            issame,						    collmast_all,                       coop_id,                            int_estimate
                    ) values ( 
                            " + ldc_year_th + ",		    " + ldc_month + ",					" + ldc_seq_no + ",				    '" + ls_loancontract_no + "',   '" + ls_loantype_code + "',     '" + ls_member_no + @"',
                            '" + ls_member_fullname + "',   " + ldc_principal_balance + ",		" + ldc_int_balance + ",			" + (ldc_margin) + ",				" + ldc_total_balance + ",		" + ldtm_lastpayment_date + @",
                            " + ldc_share_balance + ",      " + ldc_deposit_balance + ",		" + ldc_half_price + ",				" + ldc_total_contcoll + ",		" + ldc_total_percent + ",		" + ldc_percent + @",
                            " + ldc_lawtype_code + ",	    '" + ls_lawtype_desc + "',			" + ldc_lawtype_code_old + ",	    '" + ls_lawtype_desc_old + "',  " + ldtm_loancontract_date + ", " + ldc_indict_prnamt + @",
                            " + ldc_indict_intamt + ",	    " + ldtm_indict_date + ",			'" + ls_case_redno + "',			'" + ls_case_blackno + "',		" + ldtm_judge_date + ",		" + ldc_prince_last_year + @",
                            " + ldc_int_last_year + ",	    " + ldc_total_princ_last_year + ",  " + ldc_total_int_last_year + ",    '" + ls_remark + "',			" + ldtm_entry_date + ",		'" + ls_entry_id + @"',
                            " + ldc_issame + ",			    '" + ls_collmast_all + @"',         '" + state.SsCoopControl + @"',     " + int_estimate + @"
                    )
                ";

                exec.SQL.Add(sqlInsert);
            }

            if (exec.CountSQL <= 0) throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากไม่พบข้อมูลลูกหนี้มีปัญหา");
            int ii = exec.Execute();
            if (ii <= 0) throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากใช้คำสั่งผิดผลาด");
            LtServerMessage.Text = WebUtil.CompleteMessage("สร้างรายงานคงยอดสำเร็จ, ทั้งหมด " + ii + " สัญญา");
        }

        private void DeleteReport(int month, int yearThai, int seqNo)
        {
            if (!state.IsWritable)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("คุณยังไม่มีสิทธิ์ในการบันทึกข้อมูลหน้าจอนี้");
                return;
            }

            if (month <= 0 || yearThai <= 0 || seqNo <= 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("คุณยังกรอกรายละเอียดไม่ครบ");
                return;
            }

            string sql = "delete from lnnplreportmaster where coop_id = {0} and year_th = {1} and month = {2} and seq_no = {3}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, yearThai, month, seqNo);
            int ii = oracleTA.Exe(sql);
            if (ii <= 0) throw new Exception("ไม่พบข้อมูลรายงานคงยอด");
            LtServerMessage.Text = WebUtil.CompleteMessage("ลบรายงานคงยอดสำเร็จ, ทั้งหมด " + ii + " สัญญา");
        }

        protected void ddDelYearTh_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 0;
            try
            {
                year = int.Parse(ddDelYearTh.SelectedValue);
            }
            catch { }
            if (year <= 0)
            {
                ddDelMonth.DataSource = null;
                ddDelMonth.DataBind();
                ddDelMonth.DataSource = null;
                ddDelMonth.DataBind();

                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();
                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();

                tbDelEntryDate.Text = "";
            }
            else
            {
                string sql = "select distinct month, 1 as sorter from lnnplreportmaster where coop_id = {0} and year_th = {1} union select 0 , 0 from dual  order by sorter, month";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, year);
                DataTable dt = WebUtil.Query(sql);
                ddDelMonth.DataTextField = "month";
                ddDelMonth.DataValueField = "month";
                ddDelMonth.DataSource = dt;
                ddDelMonth.DataBind();
                ddDelMonth.SelectedIndex = 0;

                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();
                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();

                tbDelEntryDate.Text = "";
            }
        }

        protected void ddDelMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 0;
            int month = 0;
            try
            {
                year = int.Parse(ddDelYearTh.SelectedValue);
            }
            catch { }
            try
            {
                month = int.Parse(ddDelMonth.Text);
            }
            catch { }
            if (month <= 0)
            {
                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();
                ddDelSeqNo.DataSource = null;
                ddDelSeqNo.DataBind();

                tbDelEntryDate.Text = "";
            }
            else
            {
                string sql = "select distinct seq_no from lnnplreportmaster where coop_id = {0} and year_th = {1} and month = {2} union select 0 from dual  order by seq_no";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, year, month);
                DataTable dt = WebUtil.Query(sql);
                ddDelSeqNo.DataTextField = "seq_no";
                ddDelSeqNo.DataValueField = "seq_no";
                ddDelSeqNo.DataSource = dt;
                ddDelSeqNo.DataBind();

                tbDelEntryDate.Text = "";
            }
        }

        protected void ddDelSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 0;
            int month = 0;
            int seq_no = 0;
            try
            {
                year = int.Parse(ddDelYearTh.SelectedValue);
            }
            catch { }
            try
            {
                month = int.Parse(ddDelMonth.Text);
            }
            catch { }
            try
            {
                seq_no = int.Parse(ddDelSeqNo.Text);
            }
            catch { }
            if (seq_no <= 0)
            {
                tbDelEntryDate.Text = "";
            }
            else
            {
                string sql = "select distinct entry_date from lnnplreportmaster where coop_id = {0} and year_th = {1} and month = {2} and seq_no = {3} order by entry_date";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, year, month, seq_no);
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    tbDelEntryDate.Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd/MM/yyyy HH:mm", WebUtil.TH);
                }
                else
                {
                    tbDelEntryDate.Text = "";
                }
            }
        }
    }
}