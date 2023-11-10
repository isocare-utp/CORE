using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.w_sheet_canecelchq_ctrl
{
    public partial class w_sheet_canecelchq : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostInitchqno { get; set; }
        [JsPostBack]
        public string PostGetBank { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
                RetriList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                RetriList();
            }
            else if (eventArg == PostInitchqno)
            {
                Initchqno();
            }
            else if (eventArg == PostGetBank)
            {
                string bank_code = dsMain.DATA[0].bank_code;
                dsMain.Ddbank();
                dsMain.Ddbankbranch(bank_code);
            }
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
                int row = dsList.RowCount;
                for (int i = 0; i < row; i++)
                {
                    decimal ai_flag = dsList.DATA[i].AI_FLAG;
                    string ls_chqno = dsList.DATA[i].CHEQUE_NO;
                    string ls_bank = dsList.DATA[i].BANK_CODE;
                    string ls_branch = dsList.DATA[i].BANK_BRANCH;
                    string ls_bankaccno = dsList.DATA[i].FROM_BANKACCNO;
                    decimal ldc_moneyamt = dsList.DATA[i].MONEY_AMT;
                    string ls_referslip = dsList.DATA[i].REFER_SLIPNO;
                    string ls_chqbookno = dsList.DATA[i].CHEQUEBOOK_NO;
                    string ls_cancelreson = dsList.DATA[i].CANCEL_RESON.Trim();
                    decimal li_chqstatus = dsList.DATA[i].CHQEUE_STATUS;
                    decimal li_chqseqno = dsList.DATA[i].SEQ_NO;
                    decimal li_act = dsList.DATA[i].ACTION_FLAG;
                    decimal li_update = 0;
                    if (row <= 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลรายการยกเลิกเช็ค ไม่สามารถตรวจสอบข้อมูลได้");
                    }
                    if (ai_flag == 1)
                    {
                        if (ls_cancelreson == "") { LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเหตุผลการยกเลิกเช็คทุกฉบับ ที่ทำการยกเลิก"); return; }
                        if (li_act == 1)
                        {
                            li_update = -1;
                        }
                        else if (li_act == 2)
                        {
                            decimal li_seqno = 0, li_used = 0, li_remain = 0, li_available = 0;
                            li_update = -9;
                            string sql = @"SELECT 	max(seq_no)+1  as seq_no
                                            FROM	FINCHQEUESTATEMENT
                                            WHERE	CHEQUE_NO		= {1} 
                                            AND		CHEQUEBOOK_NO	= {2}
                                            AND		BANK_CODE		= {3}
                                            AND		BANK_BRANCH		= {4}
                                            and		coop_id			= {0}";
                            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_chqno, ls_chqbookno, ls_bank, ls_branch);
                            WebUtil.ExeSQL(sql);
                            Sdt dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            {
                                li_seqno = dt.GetDecimal("seq_no");
                                ///li_seqno++;
                            }
                            try
                            {
                                string insert = @"INSERT INTO	FINCHQEUESTATEMENT
	                        (	CHEQUE_NO,				CHEQUEBOOK_NO,				DATE_ONCHQ,				SEQ_NO,
		                        BANK_CODE,				ENTRY_ID,					ENTRY_DATE,				BANK_BRANCH,
		                        TO_WHOM,				TYPECHQ_PAY,				MONEY_AMT,				CHQEUE_STATUS,
		                        ADVANCE_CHQ,			COOP_ID,					MACHINE_ID,				CANCEL_ID,
		                        CANCEL_DATE,			MEMBER_NO,					FROM_BANKACCNO,			REFER_SLIPNO,
		                        PRINTED_STATUS,			PRINTED_ID,					PRINTED_DATE,			PRINTED_TERMINAL,
		                        CHEQUE_TYPE,			USE_STATUS
	                        )  
	                        VALUES
	                        (	{1},				    {2},				        NULL,						{5},
		                        {3},			        NULL,						NULL,						{4},
		                        NULL,					NULL,						NULL,						1,
		                        NULL,					{0},					    NULL,						NULL,
		                        NULL,					NULL,						NULL,						NULL,
		                        0,						NULL,						NULL,						NULL,
		                        '99',					0  ) ";
                                insert = WebUtil.SQLFormat(insert, state.SsCoopControl, ls_chqno, ls_chqbookno, ls_bank, ls_branch, li_seqno);
                                //WebUtil.ExeSQL(insert);
                                exe.SQL.Add(insert);
                                try
                                {
                                    string sql1 = @"select	chqslip_used,	chqslip_remain
                                            from	finchequemas
                                            where	chequebook_no	= {1} 
                                            and		bank_code		= {2} 
                                            and		bank_branch		= {3} 
                                            and		coop_id			= {0} ";
                                    sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, ls_chqbookno, ls_bank, ls_branch);
                                    WebUtil.ExeSQL(sql1);
                                    Sdt dt1 = WebUtil.QuerySdt(sql1);
                                    if (dt1.Next())
                                    {
                                        li_used = dt1.GetDecimal("chqslip_used");
                                        li_remain = dt1.GetDecimal("chqslip_remain");
                                    }
                                    if (li_remain <= 0)
                                    {
                                        li_available = 0;
                                        li_remain = 0;
                                    }
                                    else if (li_remain >= 1)
                                    {
                                        li_used = li_used - 1;
                                        li_remain = li_remain + 1;
                                        li_available = 1;
                                    }
                                    string update = @"update	finchequemas
                                            set		chqslip_used	= {4},
		                                            chqslip_remain	= {5},
		                                            available_flag	= {6}
                                            where	chequebook_no	= {1} 
                                            and		bank_code		= {2} 
                                            and		bank_branch		= {3} 
                                            and		COOP_ID			= {0} ";
                                    update = WebUtil.SQLFormat(update, state.SsCoopControl, ls_chqbookno, ls_bank, ls_branch, li_used, li_remain, li_available);
                                    //WebUtil.ExeSQL(update);
                                    exe.SQL.Add(update);
                                }
                                catch
                                {
                                    exe.SQL.Clear();
                                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลง finchequemas");
                                }
                            }
                            catch (Exception ex)
                            {
                                exe.SQL.Clear();
                                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถเพิ่มรายการเช็คได้") + ex;
                            }
                        }
                        try
                        {
                            string sqlstr = @"update finslip
	                                        set payment_status = 8
	                                        where slip_no  = {1}
	                                        and coop_id = {0}
	                                        and payment_status = 1";
                            sqlstr = WebUtil.SQLFormat(sqlstr, state.SsCoopControl, ls_referslip);
                            WebUtil.ExeSQL(sqlstr);
                            exe.SQL.Add(sqlstr);
                        }
                        catch
                        {
                            exe.SQL.Clear();
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำการยกเลิก Return Slip Cheque ได้");
                        }
                        try
                        {
                            string sqlBri = @"UPDATE FINBRIDGECHQ  
 	                                SET			BRIDGE_STATUS	= -9
	                                WHERE		SLIP_NO = {1}	and 
					                                CHEQUEBOOK_NO = {2}		and 
					                                BANK_CODE = {3}		 and 
           			                                BANK_BRANCH = {4}	and 
					                                CHEQUE_NO = {5}	and
					                                seq_no		= {6} and 
					                                COOP_ID = {0}";
                            sqlBri = WebUtil.SQLFormat(sqlBri, state.SsCoopControl, ls_referslip, ls_chqbookno, ls_bank, ls_branch, ls_chqno, li_chqseqno);
                            //WebUtil.ExeSQL(sqlBri);
                            exe.SQL.Add(sqlBri);
                        }
                        catch
                        {
                            exe.SQL.Clear();
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึก ข้อมูลได้ ลง finbridgechq");
                        }

                        string updateall = @"   update finchqeuestatement
		                        set		cancel_id		= {0},
				                        cancel_date		= {1},
				                        cancel_reson	= {2},
				                        chqeue_status	= {3} 
		                        where	chequebook_no	= {4}
		                        and		bank_code		= {5}		
		                        and		bank_branch		= {6}	
		                        and		cheque_no		= {7}	
		                        and		seq_no			= {8}";
                        updateall = WebUtil.SQLFormat(updateall, state.SsUsername, state.SsWorkDate, ls_cancelreson, li_update, ls_chqbookno, ls_bank, ls_branch, ls_chqno, li_chqseqno);
                        //WebUtil.ExeSQL(updateall);
                        exe.SQL.Add(updateall);
                    }
                }
                exe.Execute();
                exe.SQL.Clear();
                RetriList();
                LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกการทำรายเรียบร้อย");
            }
            catch
            {
                exe.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถยกเลิกรายการได้");
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void Initchqno()
        {
            string chq_no = dsMain.DATA[0].chq_no;
            while (chq_no.Length < 8)
            {
                chq_no = "0" + chq_no;
            }
            dsMain.DATA[0].chq_no = chq_no;

        }
        public void RetriList()
        {
            string chq_no = dsMain.DATA[0].chq_no.Trim();
            string bank_code = dsMain.DATA[0].bank_code;
            dsMain.Ddbank();
            dsMain.Ddbankbranch(bank_code);
            string sqlwhere = "";
            if (chq_no != "" && chq_no != "00000000")
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.CHEQUE_NO ='" + dsMain.DATA[0].chq_no + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].bank_code != "")
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.BANK_CODE ='" + dsMain.DATA[0].bank_code + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].branch_code != "")
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.BANK_BRANCH ='" + dsMain.DATA[0].branch_code + "' ";
            }
            else { sqlwhere += ""; }
            if ((dsMain.DATA[0].start_date.ToString("dd/MM/yyyy") != "01/01/1500") && (dsMain.DATA[0].end_date.ToString("dd/MM/yyyy") != "01/01/1500"))
            {
                sqlwhere += " and FINCHQEUESTATEMENT.DATE_ONCHQ between ('" + dsMain.DATA[0].start_date.ToString("MM/dd/yyyy") + "') and ('" + dsMain.DATA[0].end_date.ToString("MM/dd/yyyy") + "')";
            }

            dsList.RetrieveList(sqlwhere);
            dsList.Ddcancel_reson();
        }
    }
}