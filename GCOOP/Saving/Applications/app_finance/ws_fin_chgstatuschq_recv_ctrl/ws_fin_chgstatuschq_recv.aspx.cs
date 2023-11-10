using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_chgstatuschq_recv_ctrl
{
    public partial class ws_fin_chgstatuschq_recv : PageWebSheet, WebSheet
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

            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try
            {
                String sqlStr = "";

                string ls_bankcode = "", ls_bankbranch = "", ls_slipno = "", ls_accid = "", ls_towhom="";
                decimal ld_posttobank = 0,ld_cheque = 0;
                string ls_coopid = state.SsCoopId;
                string ls_control = state.SsCoopControl;
                DateTime ldt_workdate = state.SsWorkDate;
                for (int i = 0; i < dsMain.RowCount; i++)
                {
                    if (dsMain.DATA[i].CHK_STATUS == 1)
                    {
                        ls_towhom = dsMain.DATA[i].REFERDOC_NAME.Trim();
                        ls_bankcode = dsMain.DATA[i].BANK_CODE;
                        ls_bankbranch = dsMain.DATA[i].BANKBRANCH_CODE;
                        ls_slipno = dsMain.DATA[i].REFER_DOCNO;
                        ld_posttobank = dsMain.DATA[i].CHECKCLEAR_STATUS;
                        ld_cheque = dsMain.DATA[i].CHEQUE_AMT;
                        ls_accid = dsMain.DATA[i].ACCOUNT_ID;
                        if (ld_posttobank == 1 || ld_posttobank == 8)
                        {
                            sqlStr = @"UPDATE	FINCHECKRETRIEVE  
	                        SET		POST_TOBANK		= {4},
                                    CHECKCLEAR_STATUS	= {4},
				                    refer_app		= 'FIN',
				                    slip_no			= {1},
                                    TOBANK_CODE     = BANK_CODE,
                                    TOBANK_BRANCH   = BANKBRANCH_CODE
	                        WHERE	( FINCHECKRETRIEVE.BANK_CODE			= {3} ) AND  
				                        ( FINCHECKRETRIEVE.BANKBRANCH_CODE	= {2} ) AND  
				                        ( FINCHECKRETRIEVE.REFER_DOCNO	    = {1} ) and
				                    ( FINCHECKRETRIEVE.coop_id			= {0} ) ";
                            sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_slipno, ls_bankbranch, ls_bankcode, ld_posttobank);
                            exedinsert.SQL.Add(sqlStr);
                            //ผ่านรายการธนาคาร
                            if (ld_posttobank == 1)
                            {
                                string[] sql_insert = financeFunction.of_save_bank(state.SsCoopControl, ls_accid, state.SsUsername, ldt_workdate, ld_cheque, state.SsClientIp, ls_slipno, 1, "DCA", 1, 1, "รับเช็ค : " + ls_towhom);
                                if (sql_insert[0] != "") { exedinsert.SQL.Add(sql_insert[0]); }
                                if (sql_insert[1] != "") { exedinsert.SQL.Add(sql_insert[1]); }
                                if (sql_insert[2] != "") { exedinsert.SQL.Add(sql_insert[2]); }
                            }
                        }
                        else
                        {
                            sqlStr = @"UPDATE	FINCHECKRETRIEVE  
	                        SET		POST_TOBANK		= {4},
                                    CHECKCLEAR_STATUS	= {4},
				                    refer_app		= 'FIN',
				                    slip_no			= {1}
	                        WHERE	( FINCHECKRETRIEVE.BANK_CODE			= {3} ) AND  
				                        ( FINCHECKRETRIEVE.BANKBRANCH_CODE	= {2} ) AND  
				                    ( FINCHECKRETRIEVE.REFER_DOCNO	    = {1} ) and
				                    ( FINCHECKRETRIEVE.coop_id			= {0} ) ";
                            sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_slipno, ls_bankbranch, ls_bankcode, ld_posttobank);
                            exedinsert.SQL.Add(sqlStr);
                        }                        
                    }
                }
                exedinsert.Execute();
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                dsMain.RetrieveMain();
                dsList.ResetRow();
            }
            catch (Exception err)
            {
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + err.Message);    
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void GetDetail()
        {
            string ls_slipno = "", ls_bank = "", ls_branch = "";
            int ln_row = dsMain.GetRowFocus();
            ls_bank = dsMain.DATA[ln_row].BANK_CODE;
            ls_branch = dsMain.DATA[ln_row].BANKBRANCH_CODE;
            ls_slipno = dsMain.DATA[ln_row].REFER_DOCNO;
            dsList.RetrieveList(ls_bank, ls_branch, ls_slipno);
        }

    }
}