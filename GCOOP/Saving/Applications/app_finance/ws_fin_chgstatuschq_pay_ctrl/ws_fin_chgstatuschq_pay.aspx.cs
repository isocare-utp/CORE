using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_chgstatuschq_pay_ctrl
{
    public partial class ws_fin_chgstatuschq_pay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostDetail { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveMain();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostDetail)
            {
                GetDetail();
            }

        }
        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                int num = 0;
                for (int i = 0; i < dsMain.RowCount; i++)
                {
                    decimal status = dsMain.DATA[i].STATUS;
                    if (status == 1)
                    {
                        string to_whom = dsMain.DATA[i].TO_WHOM;
                        string chqNo = dsMain.DATA[i].CHEQUE_NO;
                        string chqBkNo = dsMain.DATA[i].CHEQUEBOOK_NO;
                        string Bank = dsMain.DATA[i].BANK_CODE;
                        string Branch = dsMain.DATA[i].BANK_BRANCH;
                        decimal seqNo = dsMain.DATA[i].SEQ_NO;
                        decimal chqSts = dsMain.DATA[i].CHQEUE_STATUS;
                        DateTime date_onchq = dsMain.DATA[i].DATE_ONCHQ;
                        DateTime adtm_wdate = state.SsWorkDate;
                        decimal ldc_money = dsMain.DATA[i].MONEY_AMT;
                        string ls_bankacc = dsMain.DATA[i].FROM_BANKACCNO;
                        string ls_accid = dsMain.DATA[i].ACCOUNT_ID;
                        string ls_referslip = dsMain.DATA[i].REFER_SLIPNO;
                        int li_taxstatus = 0, li_chqreceivestatus = 0;
                        num = i + 1;
                        if (chqNo != "")
                        {
                            try
                            {
                                string sqlstr = @"update	FINCHQEUESTATEMENT
                                set		CHQEUE_STATUS			= {6}
                                where ( (CHEQUE_NO = {1} ) AND  
                                (CHEQUEBOOK_NO = {2} ) AND  
                                ( BANK_CODE = {3} ) AND  
                                ( BANK_BRANCH = {4} ) AND  
                                ( SEQ_NO = {5} ) ) AND  
                                COOP_ID = {0} ";
                                sqlstr = WebUtil.SQLFormat(sqlstr, state.SsCoopId, chqNo, chqBkNo, Bank, Branch, seqNo, chqSts);
                                exe.SQL.Add(sqlstr);

                                if (chqSts == 1)
                                {
                                    try
                                    {
                                        //ผ่านรายการธนาคาร
                                        string[] sql_insert = financeFunction.of_save_bank(state.SsCoopControl, ls_accid, state.SsUsername, adtm_wdate, ldc_money, state.SsClientIp, ls_referslip, -1, "WCA", 1, 1, "จ่ายเช็ค : " + to_whom);
                                        if (sql_insert[0] != "") { exe.SQL.Add(sql_insert[0]); }
                                        if (sql_insert[1] != "") { exe.SQL.Add(sql_insert[1]); }
                                        if (sql_insert[2] != "") { exe.SQL.Add(sql_insert[2]); }
                                    }
                                    catch
                                    {
                                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถปรับยอดเงินในบัญชีเงินฝากธนาคารได้");
                                    }
                                    try
                                    {
                                        string sql2 = @"SELECT  isnull(RECEIVE_STATUS,0) as RECEIVE_STATUS , isnull(tax_flag,0) as tax_flag
		                                        FROM	FINSLIP  
		                                        WHERE	SLIP_NO			= {1} 
		                                        and		coop_id	= {0}";
                                        sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, ls_referslip);
                                        Sdt dt2 = WebUtil.QuerySdt(sql2);
                                        if (dt2.Next())
                                        {
                                            li_taxstatus = dt2.GetInt32("tax_flag");
                                            li_chqreceivestatus = dt2.GetInt32("RECEIVE_STATUS");
                                        }
                                        if (li_taxstatus == 1)
                                        {
                                            string sqlaa = @"UPDATE	FINTAX
				                                SET		TAXPAY_DATE		= {2},
							                                cancel_flag		= 0
				                                WHERE		SLIP_NO			= {1} and
							                                coop_id	= {0}";
                                            sqlaa = WebUtil.SQLFormat(sqlaa, state.SsCoopId, ls_referslip, adtm_wdate);
                                            exe.SQL.Add(sqlaa);
                                        }
                                    }
                                    catch { }

                                }
                                else if (chqSts == 8)
                                {
                                    string sql2 = @"SELECT  isnull(RECEIVE_STATUS,0) as RECEIVE_STATUS , isnull(tax_flag,0) as tax_flag
		                                        FROM	FINSLIP  
		                                        WHERE	SLIP_NO			= {1} 
		                                        and		coop_id	= {0}";
                                    sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, ls_referslip);
                                    Sdt dt2 = WebUtil.QuerySdt(sql2);
                                    if (dt2.Next())
                                    {
                                        li_taxstatus = dt2.GetInt32("tax_flag");
                                        li_chqreceivestatus = dt2.GetInt32("RECEIVE_STATUS");
                                    }
                                    if (li_taxstatus == 1)
                                    {
                                        string sql3 = @"UPDATE	FINTAX
				                                SET		TAXPAY_DATE		= {2},
							                                cancel_flag		= 0
				                                WHERE		SLIP_NO			= {1} and
							                                coop_id	= {0}";
                                        sql3 = WebUtil.SQLFormat(sql3, state.SsCoopId, ls_referslip, adtm_wdate);
                                        exe.SQL.Add(sql3);
                                    }
                                }
                                else if (chqSts == 2)
                                {
                                    try
                                    {
                                        string sql = @"update finslip
		                                        set		PAYMENT_STATUS = 2,
				                                        entry_date = {2}
				                                    WHERE		SLIP_NO			= {1} and
							                                    coop_id	= {0}";
                                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, ls_referslip, adtm_wdate);
                                        exe.SQL.Add(sql);
                                    }
                                    catch
                                    {
                                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถปรับ สถานะของ finslip เช็คสำรองจ่าย ได้");
                                    }
                                }
                                else if (chqSts == 0)
                                {
                                }

                            }
                            catch (Exception ex)
                            {
                                exe.SQL.Clear();
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                            }
                        }
                    }
                }
                exe.Execute();
                exe.SQL.Clear();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                dsMain.RetrieveMain();
                dsList.ResetRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void GetDetail()
        {
            int row = Convert.ToInt16(HfRow.Value);
            string as_chqno, as_chqbkno, as_bank, as_branch;
            decimal an_seqno;
            as_chqno = dsMain.DATA[row].CHEQUE_NO;
            as_chqbkno = dsMain.DATA[row].CHEQUEBOOK_NO;
            as_bank = dsMain.DATA[row].BANK_CODE;
            as_branch = dsMain.DATA[row].BANK_BRANCH;
            an_seqno = dsMain.DATA[row].SEQ_NO;
            dsList.RetrieveList(as_chqno, as_chqbkno, as_bank, as_branch, an_seqno);
        }
    }
}