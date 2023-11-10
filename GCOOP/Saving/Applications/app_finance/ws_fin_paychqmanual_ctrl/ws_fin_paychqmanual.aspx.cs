using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_paychqmanual_ctrl
{
    public partial class ws_fin_paychqmanual : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostGetBank { get; set; }
        [JsPostBack]
        public string PostGetbook { get; set; }
        [JsPostBack]
        public string PostGetchequeno { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].ai_prndate = "1";
                dsMain.DATA[0].ai_killer = "1";
                dsMain.DATA[0].ai_payee = "0";
                resetback();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            string bank_code = dsMain.DATA[0].bank_code;
            string bank_branch = dsMain.DATA[0].bank_branch;
            string book_no = dsMain.DATA[0].cheque_bookno;
            if (eventArg == PostGetBank)
            {
                dsMain.Ddbank();
                dsMain.Ddbankbranch(bank_code);
                dsMain.Ddtofromacc();
            }
           else if (eventArg == PostGetbook)
            {
                PostGetBanklast();
                dsMain.Ddbookno(bank_code, bank_branch);
            }
            else if (eventArg == PostGetchequeno)
            {
                PostInitaccno();
                dsMain.Ddchequeno(bank_code, bank_branch,book_no);
            }
            
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
            
            DateTime ldtm_onchq = state.SsWorkDate ;
            string as_entry = state.SsUsername;
            DateTime adtm_date = ldtm_onchq;
            string ls_paywhom = dsMain.DATA[0].pay_whom;
            decimal TYPECHQ_PAY = 0;
            decimal ldc_itemamt = dsMain.DATA[0].cheque_amt;
            decimal li_chequestatus = dsMain.DATA[0].cheque_status;
            decimal li_apv = 0;
            string as_coopid = state.SsCoopId;
            string as_machine = state.SsClientIp;
            string ls_member = "";
            string ls_fromacc = dsMain.DATA[0].fromaccount_no;
            string ls_frombank = dsMain.DATA[0].frombank;
            string ls_frombranch = dsMain.DATA[0].frombranch;
            string ls_referslip = "";
            string ls_chequetype = "01";
            decimal printed_status = 1;
            decimal USE_STATUS = 1;
            string remark = dsMain.DATA[0].remark;
            string ls_chqno = dsMain.DATA[0].account_no;
            string ls_chqbookno = dsMain.DATA[0].cheque_bookno;
            string ls_bankcode = dsMain.DATA[0].bank_code;
            string ls_bankbranch = dsMain.DATA[0].bank_branch;
            string account_id ="";
            int li_used = 0;
            int li_remain = 0;
            decimal li_available = 1;
            string ai_prndate = dsMain.DATA[0].ai_prndate.Trim();
            string ai_killer = dsMain.DATA[0].ai_killer.Trim();
            string ai_payee = dsMain.DATA[0].ai_payee.Trim();
            string sql = @"select	account_id as account_id
                            from		FINBANKACCOUNT
                            WHERE		
			                            (	ACCOUNT_NO = {3} ) AND  
			                            (	BANK_CODE			= {1} ) AND  
			                            ( 	BANKBRANCH_CODE	= {2}  ) and
			                            ( COOP_ID		= {0} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, ls_frombank, ls_frombranch, ls_fromacc);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                account_id = dt.GetString("account_id");
                
            }
            if (account_id != "")
            {
                string sqlstr = @"update	finchqeuestatement
                            set		DATE_ONCHQ		= {5},
		                            ENTRY_ID		= {6},
		                            ENTRY_DATE		= {7},
		                            TO_WHOM			= {8},
		                            TYPECHQ_PAY		= {9},
		                            MONEY_AMT		= {10},
		                            CHQEUE_STATUS	= {11},
		                            ADVANCE_CHQ		= {12},
		                            MACHINE_ID		= {13},
		                            MEMBER_NO		= {14},
		                            from_bankaccno	= {15},
		                            refer_slipno	= {16},
		                            cheque_type		= {17},
		                            printed_status	= {18},
		                            USE_STATUS		= {19},
		                            remark			= {20}
                            where	( CHEQUE_NO		= {4} ) and
		                            ( CHEQUEBOOK_NO	= {3} ) and
		                            ( BANK_CODE		= {2} ) and 
		                            ( BANK_BRANCH	= {1} ) and
		                            ( coop_id		= {0} ) and
		                            ( chqeue_status		<> -9 )";
                sqlstr = WebUtil.SQLFormat(sqlstr, as_coopid, ls_bankbranch, ls_bankcode, ls_chqbookno, ls_chqno, ldtm_onchq, as_entry, adtm_date, ls_paywhom, TYPECHQ_PAY, ldc_itemamt,
                        li_chequestatus, li_apv, as_machine, ls_member, ls_fromacc, ls_referslip, ls_chequetype, printed_status, USE_STATUS, remark);
                //WebUtil.ExeSQL(sqlstr);
                exe.SQL.Add(sqlstr);
                
                string sql1 = @"select	chqslip_used, chqslip_remain
                            from		finchequemas
                            where		( chequebook_no	= {3} ) and
			                            ( bank_code		= {2} ) and
			                            ( bank_branch		= {1}) and
			                            ( coop_id	= {0} )";
                sql1 = WebUtil.SQLFormat(sql1, as_coopid, ls_bankbranch, ls_bankcode, ls_chqbookno);
                Sdt dt1 = WebUtil.QuerySdt(sql1);
                if (dt1.Next())
                {
                    li_used = dt1.GetInt32("chqslip_used");
                    li_remain = dt1.GetInt32("chqslip_remain");
                }
                if (li_remain != 0)
                {
                    li_used++;
                    li_remain--;

                    string sqlstr1 = @"update	finchequemas
                                set		chqslip_used		= {4},
		                                chqslip_remain		= {5},
		                                available_flag		= {6},
		                                last_chqno			= {7}
                                 where		( chequebook_no	= {3} ) and
			                            ( bank_code		= {2} ) and
			                            ( bank_branch		= {1}) and
			                            ( coop_id	= {0} )";
                    sqlstr1 = WebUtil.SQLFormat(sqlstr1, as_coopid, ls_bankbranch, ls_bankcode, ls_chqbookno, li_used, li_remain, li_available, ls_chqno);
                    exe.SQL.Add(sqlstr1);
                    exe.Execute();
                    exe.SQL.Clear();

                    
                    if (state.SsCoopControl != "" || state.SsCoopControl != null)
                    {
                        //gen ireport
                        string report_name = "";
                        report_name = WebUtil.GetNamePrintChq(ls_bankcode);
                        if (state.SsCoopControl != "" || state.SsCoopControl != null)
                        {
                            string report_label = "เช็ค";
                            iReportArgument args = new iReportArgument();
                            args.Add("ai_killer", iReportArgumentType.String, dsMain.DATA[0].ai_killer);
                            args.Add("ai_payee", iReportArgumentType.String, dsMain.DATA[0].ai_payee);
                            args.Add("ai_prndate", iReportArgumentType.String, dsMain.DATA[0].ai_prndate);
                            args.Add("as_bankbranch", iReportArgumentType.String, ls_bankbranch);
                            args.Add("as_bankcode", iReportArgumentType.String, ls_bankcode);
                            args.Add("as_chequebookno", iReportArgumentType.String, ls_chqbookno);
                            args.Add("as_chequeno", iReportArgumentType.String, ls_chqno);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }
                    }

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    dsMain.ResetRow();
                    resetback();
                }
                else {
                    exe.SQL.Clear();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลง finchequemas"); 
                }
            }
            else {
                exe.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลง finshqeuestatement");
                }
            }
            catch (Exception ex){
                exe.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลได้ จาก FINBANKACCOUNT เพื่อปรับปรุงรหัสคู่บัญชี");
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
        public void PostGetBanklast()
        {
            string bank_code = dsMain.DATA[0].bank_code;
            string bank_branch = dsMain.DATA[0].bank_branch;
            dsMain.DATA[0].frombank = bank_code;
            dsMain.DATA[0].frombranch = bank_branch;
        }
        public void PostInitaccno()
        {
           
            string bank_code = dsMain.DATA[0].bank_code;
            string bank_branch = dsMain.DATA[0].bank_branch;
            string fromaccount_no = "";
            string sql = @"SELECT	ACCOUNT_NO FROM		FINBANKACCOUNT  
	            WHERE	( BANK_CODE				={1}  ) AND  
				            ( BANKBRANCH_CODE		= {2} ) AND  
				            ( coop_id			= {0} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, bank_code, bank_branch);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                fromaccount_no = dt.GetString("ACCOUNT_NO");
                //if (fromaccount_no.Length == 10)
                //{
                //    fromaccount_no = fromaccount_no.Substring(0, 3) + "-" + fromaccount_no.Substring(4, 1) + "-" + fromaccount_no.Substring(5, 5) + "-" + fromaccount_no.Substring(10, 1);
                //    Console.WriteLine(fromaccount_no);
                //}

            }
            
            dsMain.DATA[0].fromaccount_no = fromaccount_no;
        }
        public void resetback()
        {
            dsMain.DATA[0].cheque_date = state.SsWorkDate;
            string bank = dsMain.DATA[0].bank_code;
            dsMain.Ddbank();
            dsMain.Ddbankbranch(bank);
            dsMain.Ddchqtype();
        }
    }
}