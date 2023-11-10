using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_bankaccount_ctrl
{
    public partial class ws_fin_bankaccount : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostGetBank { get; set; }
        [JsPostBack]
        public string JsPostRrieveData { get; set; }
        [JsPostBack]
        public string JsPostRrieveDataFrmAccno { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                PostLoadBegin();
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostGetBank)
            {
                string bank_code = dsMain.DATA[0].BANK_CODE;
                dsMain.DDBank();
                dsMain.DDBankbranch(bank_code);
            }
            else if (eventArg == JsPostRrieveData)
            {
                string ls_accno = dsMain.DATA[0].ACCOUNT_NO;
                string ls_bankcode = dsMain.DATA[0].BANK_CODE;
                string ls_bankbranch = dsMain.DATA[0].BANKBRANCH_CODE;
                PostLoadBegin();
                PostRetriveData(ls_accno, ls_bankcode, ls_bankbranch);
            }
            else if (eventArg == JsPostRrieveDataFrmAccno)
            {

            }
        }
        private void PostLoadBegin()
        {
            dsMain.ResetRow();
            dsList.ResetRow();
            string bank = dsMain.DATA[0].BANK_CODE;
            dsMain.DDBank();
            dsMain.DDBankbranch(bank);
            dsMain.DDAccountid();
            dsMain.DATA[0].COOP_ID = state.SsCoopId;
            dsMain.DATA[0].LASTACCESS_DATE = state.SsWorkDate;
        }
        private void PostRetriveData(string ls_accno, string ls_bankcode, string ls_bankbranch)
        {
            dsMain.RetrieveMain(ls_accno, ls_bankcode, ls_bankbranch);
            if (dsMain.RowCount > 0)
            {
                ls_bankcode = dsMain.DATA[0].BANK_CODE;
                ls_bankbranch = dsMain.DATA[0].BANKBRANCH_CODE;
                dsMain.DDBank();
                dsMain.DDBankbranch(ls_bankcode);
                dsMain.DDAccountid();
                dsMain.DATA[0].LASTACCESS_DATE = state.SsWorkDate;
                dsList.RetrieveList(ls_accno, ls_bankcode, ls_bankbranch);
            }
            else
            {
                PostLoadBegin();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ระบุ!"); return;
            }
        }
        public void SaveWebSheet()
        {
            try
            {
                DateTime ldt_workdate = state.SsWorkDate;
                string ls_control = state.SsCoopControl;
                string ls_coopid = state.SsCoopId;
                string ls_accountno = dsMain.DATA[0].ACCOUNT_NO;
                string ls_bankcode = dsMain.DATA[0].BANK_CODE;
                string ls_bankbranchcode = dsMain.DATA[0].BANKBRANCH_CODE;
                string ls_accountname = dsMain.DATA[0].ACCOUNT_NAME;
                Decimal ld_balance = dsMain.DATA[0].BALANCE;
                Decimal ld_laststmno = dsMain.DATA[0].LASTSTM_SEQ;
                string ls_accounttype = dsMain.DATA[0].ACCOUNT_TYPE;
                string ls_bookno = dsMain.DATA[0].BOOK_NO;
                decimal ld_scobalance = dsMain.DATA[0].SCO_BALANCE;
                string ls_accountid = dsMain.DATA[0].ACCOUNT_ID;
                decimal ld_intrate = dsMain.DATA[0].INT_RATE;
                ld_intrate = ld_intrate / 100;
                string ls_remark = dsMain.DATA[0].REMARK;
                decimal ld_mbeginbal = dsMain.DATA[0].BALANCE;
                string ls_entryid = dsMain.DATA[0].ENTRY_ID;
                String sqlStr = "";
                decimal ld_count = 0;
                string sql = @"select count(*) as countdate from finbankaccount WHERE coop_id={0} and account_no not in({1}) and account_id={2}";
                sql = WebUtil.SQLFormat(sql, ls_control, ls_accountno, ls_accountid);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ld_count = dt.GetDecimal("countdate");
                }
                if (ld_count > 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("คู่บัญชีดังกล่าวมีการเพิ่มในระบบแล้ว กรุณาเลือกคู่บัญชีให้ถูกต้อง"); return;
                }

                sql = @"select count(*) as countdate from finbankaccount WHERE coop_id={0} and account_no={1} ";
                sql = WebUtil.SQLFormat(sql, ls_control, ls_accountno, ls_bankcode, ls_bankbranchcode, ls_accountid);
                dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ld_count = dt.GetDecimal("countdate");
                }
                if (ld_count == 1)
                {

                    sqlStr = @"update finbankaccount
	                            set		bank_code 	= {2}, account_name = {3}, laststm_seq = {4}, account_type 	= {5}, book_no = {6}, account_id = {7}, 
			                            remark 	= {8}, bankbranch_code	= {9},  sco_balance = {10} , balance     = {11}, int_rate = {12}
	                            where account_no = {1} and coop_id ={0}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_accountno, ls_bankcode, ls_accountname, ld_laststmno, ls_accounttype, ls_bookno, ls_accountid,
                        ls_remark, ls_bankbranchcode, ld_scobalance, ld_balance, ld_intrate);
                    WebUtil.ExeSQL(sqlStr);
                }
                else
                {
                    sqlStr = @"   INSERT INTO FINBANKACCOUNT  
		                (	COOP_ID,			ACCOUNT_NO,		BANK_CODE,			BANKBRANCH_CODE,		ACCOUNT_NAME,   
			                BEGINBAL,			BALANCE,		OPEN_DATE,          ENTRY_DATE,	            INT_RATE,
			                LASTSTM_SEQ,	    ENTRY_ID,		LASTACCESS_DATE,	ACCOUNT_TYPE,           ACCOUNT_ID,	  
			                DEPT_AMT,			WITH_AMT,		BOOK_LASTUPDATE,	BOOK_NO,	            SCO_BALANCE,			   
			                REMARK,             CLOSE_STATUS	)			
	                VALUES 
		                (	{0},			    {1},		    {2},			    {3},			        {4},
			                    0,				{5},	        {6},		        {6},					{7},
			                {8},	            {9},		    {6},			    {10},                   {11},
			                    0,		          0,            {6},				{12},	                {13},		
                            {14},                 0)";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_accountno, ls_bankcode, ls_bankbranchcode, ls_accountname,
                            ld_balance, ldt_workdate, ld_intrate,
                            ld_laststmno, ls_entryid, ls_accounttype, ls_accountid,
                            ls_bookno, ld_scobalance,
                            ls_remark);
                    WebUtil.ExeSQL(sqlStr);
                }
                PostLoadBegin();
                PostRetriveData(ls_accountno, ls_bankcode, ls_bankbranchcode);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}