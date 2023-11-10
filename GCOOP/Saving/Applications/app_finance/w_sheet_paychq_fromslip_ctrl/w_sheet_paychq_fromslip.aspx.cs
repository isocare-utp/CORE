using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.w_sheet_paychq_fromslip_ctrl
{
    public partial class w_sheet_paychq_fromslip : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostGetBank { get; set; }
        [JsPostBack]
        public string PostGetBankBranch { get; set; }
        [JsPostBack]
        public string PostGetChqBookNo { get; set; }
        [JsPostBack]
        public string PostSearchData { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                BackToBegin();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostGetBank)
            {
                string bank_code = dsMain.DATA[0].as_bank;
                dsMain.Ddbankbranch(bank_code);
            }
            else if (eventArg == PostGetBankBranch)
            {
                string bank_code = dsMain.DATA[0].as_bank;
                string bank_branch = dsMain.DATA[0].as_bankbranch;
                dsMain.DdChqbookno(state.SsCoopId, bank_code, bank_branch);
                string ls_accountno = "";
                string sql = @"
                        SELECT	ACCOUNT_NO  
                        FROM		FINBANKACCOUNT  
                        WHERE
                        ( BANK_CODE				= {1} ) AND  
                        ( BANKBRANCH_CODE		= {2} ) AND  
                        ( ACCOUNT_TYPE			= '01') AND
                        ( coop_id			    = {0} )";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, bank_code, bank_branch);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_accountno = dt.GetString("ACCOUNT_NO");
                }
                dsMain.DATA[0].as_fromaccno = ls_accountno;
            }
            else if (eventArg == PostGetChqBookNo)
            {
                string bank_code = dsMain.DATA[0].as_bank;
                string bank_branch = dsMain.DATA[0].as_bankbranch;
                string chqbook_no = dsMain.DATA[0].as_chqbookno;
                dsMain.DdChqno(state.SsCoopId, bank_code, bank_branch, chqbook_no);
            }
            else if (eventArg == PostSearchData)
            {
                DateTime entry_date = dsMain.DATA[0].entry_date;
                dsList.Retrieve(state.SsCoopId, entry_date);
            }
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
                int row = dsList.RowCount;
                if (row <= 0) { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลได้ จาก finshqeuestatement เพื่อเตรียมพิมพ์เช็ค"); }
                string as_chqstart_no = "";
                int ls_count = 0;
                decimal li_used = 0, li_remain = 0;
                string ls_bridgeche_no = "";
                string as_bank = dsMain.DATA[0].as_bank;
                string as_bankbranch = dsMain.DATA[0].as_bankbranch;
                string as_chqbookno = dsMain.DATA[0].as_chqbookno;
                string as_chqtype = dsMain.DATA[0].as_chqtype;
                DateTime as_adtmtdate = dsMain.DATA[0].onchq_date;
                string as_accno = dsMain.DATA[0].as_fromaccno;
                string ai_chequestatus = dsMain.DATA[0].ai_chqstatus.Trim();
                string as_entry = state.SsUsername;
                string ai_prndate = dsMain.DATA[0].ai_prndate;
                string ai_killer = dsMain.DATA[0].ai_killer;
                string ai_payee = dsMain.DATA[0].ai_payee;

                string sqlstr = @"select max( bridgechq_no ) as bridgechq_no
                                                    from finbridgechq
                                                    where	coop_id = {0}";
                sqlstr = WebUtil.SQLFormat(sqlstr, state.SsCoopId);
                Sdt dt2 = WebUtil.QuerySdt(sqlstr);
                if (dt2.Next())
                {
                    ls_bridgeche_no = dt2.GetString("bridgechq_no");
                    if (ls_bridgeche_no == "" || ls_bridgeche_no == null)
                    {
                        ls_bridgeche_no = "0";
                    }
                }

                string sql1 = @"select	chqslip_used,	chqslip_remain
                                            from	finchequemas
                                            where	chequebook_no	= {1} 
                                            and		bank_code		= {2} 
                                            and		bank_branch		= {3} 
                                            and		coop_id			= {0} ";
                sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, as_chqbookno, as_bank, as_bankbranch);
                WebUtil.ExeSQL(sql1);
                Sdt dt3 = WebUtil.QuerySdt(sql1);
                if (dt3.Next())
                {
                    li_used = dt3.GetDecimal("chqslip_used");
                    li_remain = dt3.GetDecimal("chqslip_remain");
                }

                for (int i = 0; i < row; i++)
                {
                    decimal ldc_sumitemamt = 0, li_seqno = 0, li_available = 0, li_useds = 0,ls_usestatus = 0;
                    string account_id = "";
                    decimal li_payment_status = 1, li_receive_status = 1;
                    string ls_paywhom = dsList.DATA[i].PAY_TOWHOM;
                    decimal ldc_itemamt = dsList.DATA[i].ITEM_AMTNET;
                    string ls_referslip = dsList.DATA[i].SLIP_NO.Trim();
                    decimal choose_flag = dsList.DATA[i].choose_flag;
                    string ls_paydesc = dsList.DATA[i].PAYMENT_DESC;
                    string ls_remark = dsList.DATA[i].REMARK;
                    DateTime adtm_wdate = state.SsWorkDate;
                    string as_machine = state.SsClientIp;
                    string ls_member = dsList.DATA[i].MEMBER_NO.Trim();
                    decimal li_retail_flag = dsList.DATA[i].RETAIL_FLAG;
                    DateTime ldtm_today = DateTime.Now;

                    if (choose_flag == 1)
                    {
                        ls_count++;
                        if (ls_count == 1)
                        {
                            as_chqstart_no = dsMain.DATA[0].as_chqstartno;
                        }
                        else
                        {
                            do
                            {
                                as_chqstart_no = Convert.ToString(Convert.ToDecimal(as_chqstart_no) + 1);
                                while (as_chqstart_no.Length < 8)
                                {
                                    as_chqstart_no = "0" + as_chqstart_no;
                                }
                                string doo = @"SELECT USE_STATUS  
                                            FROM	 FINCHQEUESTATEMENT
                                            WHERE	CHEQUEBOOK_NO	= {1}
										    and CHEQUE_NO           = {2}
                                            AND		BANK_CODE		= {3}
                                            AND		BANK_BRANCH		= {4}
                                            and		coop_id			= {0}";
                                doo = WebUtil.SQLFormat(doo, state.SsCoopControl, as_chqbookno, as_chqstart_no, as_bank, as_bankbranch);
                                WebUtil.ExeSQL(doo);
                                Sdt dwhi = WebUtil.QuerySdt(doo);
                                if (dwhi.Next())
                                {
                                    ls_usestatus = dwhi.GetDecimal("USE_STATUS");
                                }
                            }
                            while (ls_usestatus == 1);
                        }
                        try
                        {
                            string sql = @"select	account_id as account_id
                                from		FINBANKACCOUNT
                                WHERE		
			                                (	ACCOUNT_NO= {3} ) AND  
			                                (	BANK_CODE			= {1} ) AND  
			                                ( 	BANKBRANCH_CODE	= {2}  ) and
			                                ( COOP_ID		= {0} )";
                            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_bank, as_bankbranch, as_accno);
                            Sdt dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            {
                                account_id = dt.GetString("account_id");
                            }
                            if (account_id == "") { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลได้ จาก FINBANKACCOUNT เพื่อปรับปรุงรหัสคู่บัญชี");  }
                            ldc_sumitemamt = ldc_sumitemamt + ldc_itemamt;
                           
                            string updatechq = @"update		finchqeuestatement
	                                                set			DATE_ONCHQ			= {1} ,
				                                                ENTRY_ID			= {2},
				                                                ENTRY_DATE			= {3},
				                                                TO_WHOM				= {4},
				                                                TYPECHQ_PAY			= 0,
				                                                MONEY_AMT			= {5},
				                                                CHQEUE_STATUS		= {6},
				                                                ADVANCE_CHQ			= 0,
				                                                COOP_ID				= {7},
				                                                MACHINE_ID			= {8},
				                                                MEMBER_NO			= {9},
				                                                from_bankaccno		= {10},
				                                                refer_slipno		= {11},
				                                                cheque_type			= {12} ,
				                                                printed_status		= 1,
				                                                PRINTED_ID			= {13},
				                                                PRINTED_DATE		= {14},
				                                                PRINTED_TERMINAL	= {15},
				                                                USE_STATUS			= 1,
				                                                remark				= {16}
	                                                where		CHEQUE_NO			= {17}
	                                                and			CHEQUEBOOK_NO		= {18}
	                                                and			BANK_CODE			= {19}
	                                                and			BANK_BRANCH			= {20}
	                                                and			USE_STATUS			= 0
	                                                and			COOP_ID				= {0} 
	                                                and			chqeue_status		not in(-9,-1)";
                            updatechq = WebUtil.SQLFormat(updatechq, state.SsCoopId, as_adtmtdate, as_entry, adtm_wdate, ls_paywhom, ldc_itemamt, ai_chequestatus, state.SsCoopId,
                               as_machine, ls_member, as_accno, ls_referslip, as_chqtype, as_entry, adtm_wdate, as_machine, ls_paydesc, as_chqstart_no, as_chqbookno, as_bank, as_bankbranch);
                            //WebUtil.ExeSQL(updatechq);
                            exe.SQL.Add(updatechq);
                            if (li_retail_flag == 3)
                            {
                                li_payment_status = 8;
                                li_receive_status = 0;
                            }
                            string updateslip = @"update	finslip
	                                                set		bank_code		= {1},
			                                                bank_branch		= {2},
			                                                chequebook_no	= {3},
			                                                account_no		= {4},
			                                                dateon_chq		= {5},
			                                                cheque_status	= {6},
			                                                from_accno		= {7},
			                                                from_bankcode	= {8},
			                                                from_branchcode	= {9},
			                                                cheque_type		= {10},
			                                                payment_status	= {11},
			                                                receive_date	= {12},
			                                                receive_status	= {13},
			                                                recvpay_id		= {14},
			                                                recvpay_time	= {15},
			                                                pay_towhom		= {16}
	                                                where	slip_no			= {17} 
	                                                and		COOP_ID			= {0} ";
                            updateslip = WebUtil.SQLFormat(updateslip, state.SsCoopId, as_bank, as_bankbranch, as_chqbookno, as_chqstart_no, as_adtmtdate, ai_chequestatus, as_accno, as_bank, as_bankbranch,
                               as_chqtype, li_payment_status, as_adtmtdate, li_receive_status, as_entry, ldtm_today, ls_paywhom, ls_referslip);
                            //WebUtil.ExeSQL(updateslip);
                            exe.SQL.Add(updateslip);
                            try
                            {
                                ls_bridgeche_no = Convert.ToString(Convert.ToDecimal(ls_bridgeche_no) + 1);
                                while (ls_bridgeche_no.Length < 10)
                                {
                                    ls_bridgeche_no = "0" + ls_bridgeche_no;
                                }
                                
                                string insertbrig = @"INSERT INTO FINBRIDGECHQ  
                                         ( 			BRIDGECHQ_NO,       SLIP_NO,            CHEQUEBOOK_NO,      BANK_CODE,   
           			                                BANK_BRANCH,        CHEQUE_NO,          SEQ_NO ,		    COOP_ID ,   BRIDGE_STATUS  )  
  	                                VALUES ( 	    {1},                {2},                {3},                {4},
	  			 	                                {5},	            {6},                {7} ,		        {0}			, 1) ";
                                insertbrig = WebUtil.SQLFormat(insertbrig, state.SsCoopId, ls_bridgeche_no, ls_referslip, as_chqbookno, as_bank, as_bankbranch, as_chqstart_no, li_seqno);
                                //WebUtil.ExeSQL(insertbrig);
                                exe.SQL.Add(insertbrig);
                            }
                            catch 
                            {
                                exe.SQL.Clear();
                                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึก ข้อมูลได้ ลง finbridgechq "); 
                            }
                            if (ai_chequestatus == "0" || ai_chequestatus == "2")
                            {
                                string updatecase = @"update	finslip
			                                                set		receive_status		= 0
			                                                where	slip_no				= {1} and
					                                                COOP_ID		= {0} ";
                                updatecase = WebUtil.SQLFormat(updatecase, state.SsCoopId, ls_referslip);
                                //WebUtil.ExeSQL(updatecase);
                                exe.SQL.Add(updatecase);
                            }
                            try
                            {
                                if (li_remain <= 0)
                                {
                                    li_available = 0;
                                    li_remain = 0;
                                }
                                else if (li_remain >= 1)
                                {
                                    li_used = li_used + 1;
                                    li_remain = li_remain - 1;
                                    li_available = 1;
                                }
                                string update = @"update	finchequemas
                                                    set		chqslip_used		= {1},
		                                                    chqslip_remain		= {2},
		                                                    available_flag		= {3},
		                                                    last_chqno			= {4}
                                                    where	chequebook_no		= {5} 
                                                    and		bank_code			= {6} 
                                                    and		bank_branch			= {7} 
                                                    and		COOP_ID				= {0}  ";
                                update = WebUtil.SQLFormat(update, state.SsCoopControl, li_used, li_remain, li_available, as_chqstart_no, as_chqbookno, as_bank, as_bankbranch);
                                //WebUtil.ExeSQL(update);
                                exe.SQL.Add(update);
                                
                            }
                            catch
                            {
                                exe.SQL.Clear();
                                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลง finchequemas");
                            }
                        }
                        catch {
                            exe.SQL.Clear();
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงสถานะเช็คจาก finshqeuestatement " + as_chqstart_no); 
                        }
                    }
                }
                exe.Execute();
                exe.SQL.Clear();
                //gen ireport
                if (state.SsCoopControl != "" || state.SsCoopControl != null)
                {
                    string report_name = "";
                    report_name = "ir_finchqslip";
                    string report_label = "พิมพ์เช็ค";
                    iReportArgument args = new iReportArgument();
                    args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                    args.Add("as_chqbookno", iReportArgumentType.String, as_chqbookno);
                    args.Add("as_chequeno", iReportArgumentType.String, as_chqstart_no);
                    args.Add("ai_prndate", iReportArgumentType.String, ai_prndate);
                    args.Add("ai_killer", iReportArgumentType.String, ai_killer);
                    args.Add("ai_payee", iReportArgumentType.String, ai_payee);
                    args.Add("as_bankbranch", iReportArgumentType.String, as_bankbranch);
                    args.Add("as_bank", iReportArgumentType.String, as_bank);
                   // args.Add("date_onchq", iReportArgumentType.String, as_adtmtdate);
                    iReportBuider report = new iReportBuider(this, "");
                    report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการทำรายเรียบร้อย");
                dsMain.ResetRow();
                BackToBegin();
            }
            catch
            {
                exe.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ"); 
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void BackToBegin()
        {
            dsMain.DATA[0].entry_date = state.SsWorkDate;
            dsMain.DATA[0].onchq_date = state.SsWorkDate;
            dsMain.Ddbank();
            dsMain.DdChqType(state.SsCoopControl);
            DateTime entry_date = dsMain.DATA[0].entry_date;
            dsList.Retrieve(state.SsCoopId, entry_date);
        }
    }
}