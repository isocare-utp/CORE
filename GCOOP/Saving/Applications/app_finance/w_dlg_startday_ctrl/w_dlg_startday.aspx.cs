using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.w_dlg_startday_ctrl
{
    public partial class w_dlg_startday : PageWebSheet, WebSheet
    {
        decimal pb125_flag = 0;
        decimal c_openday = 0;
        String sqlStr = "";
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }
        public decimal GetSql_Value(string Select_Condition)
        {
            decimal max_value = 0;
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetDecimal("sql_value");
            }
            return max_value;
        }

        public void WebSheetLoadBegin()
        {

            try
            {
                string date_openday = "";
                String sql = @"select  CONVERT(VARCHAR(10), operate_date, 103) as operatedate,close_status  from fincashflowmas where operate_date = (select max(operate_date) from fincashflowmas where coop_id = '" + state.SsCoopId + "') and coop_id = '" + state.SsCoopId + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    date_openday = dt.GetString("operatedate");
                    c_openday = dt.GetDecimal("close_status");
                }
                if (c_openday == 0 && date_openday != state.SsWorkDate.ToString("dd/MM/yyyy"))
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่ได้ปิดสิ้นวันก่อนหน้านี้ กรุณาปิดสิ้นวันก่อนเปิดวันใหม่"); return;
                }
                else
                {
                    this.SetOnLoadedScript("document.getElementById('F_dsMain').style.display = 'block';");
                    financeFunction.ResultClass classChk = new financeFunction.ResultClass();           
                    classChk = financeFunction.of_is_openday(state.SsCoopId, state.SsWorkDate);
                    if (classChk.returnValue[0] > 0)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("ได้มีการเปิดงานประจำวันแล้ว ไม่สามารถทำการเปิดได้ใหม่");
                        //workdate                        
                        dsMain.RetrieveData(state.SsCoopId, state.SsWorkDate);
                        dsMain.DATA[0].COOP_NAME = state.SsCoopName;
                        string ls_datenow = dsMain.DATA[0].ENTRY_TIME.Day.ToString("00") + "/" + dsMain.DATA[0].ENTRY_TIME.Month.ToString("00") + "/" + (dsMain.DATA[0].ENTRY_TIME.Year + 543) + " " + dsMain.DATA[0].ENTRY_TIME.ToString("HH:mm:ss");// ToString("yyyy-MM-dd HH:mm:ss");
                        dsMain.DATA[0].Full_DATENOW = ls_datenow;
                    }
                    else
                    {
                        //max workdate
                        dsMain.ResetRow();
                        decimal ld_cash_foward = 0, ld_chqinsafe_amt = 0;
                        sql = @"select cash_foward, chqinsafe_amt from fincashflowmas where coop_id = {0} and operate_date  = (select max(operate_date) from fincashflowmas where coop_id = {0}) ";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                        dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            ld_cash_foward = dt.GetDecimal("cash_foward");
                            ld_chqinsafe_amt = dt.GetDecimal("chqinsafe_amt");
                        }
                        dsMain.DATA[0].COOP_NAME = state.SsCoopName;
                        dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                        dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                        dsMain.DATA[0].COOP_ID = state.SsCoopId;
                        dsMain.DATA[0].Full_DATENOW = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + (DateTime.Now.Year + 543) + " " + DateTime.Now.ToString("HH:mm:ss");
                        dsMain.DATA[0].ENTRY_TIME = DateTime.Now;
                        dsMain.DATA[0].ENTRY_DATE = DateTime.Now;
                        dsMain.DATA[0].CASH_BEGIN = ld_cash_foward;
                        dsMain.DATA[0].CASH_SUMAMT = ld_cash_foward;
                        dsMain.DATA[0].MACHINE_ID = state.SsClientIp;
                        dsMain.DATA[0].CHQINSAFT_BFAMT = ld_chqinsafe_amt;
                        dsMain.DATA[0].AMOUNT_AMT = ld_cash_foward;
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(state.SsConnectionString);
            ta.Transection();
            try
            {
                financeFunction.ResultClass classChk = new financeFunction.ResultClass();
                classChk = financeFunction.of_is_openday(state.SsCoopId, state.SsWorkDate);
                if (classChk.returnValue[0] > 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ได้มีการเปิดงานประจำวันแล้ว ไม่สามารถทำการเปิดได้ใหม่"); return;
                }
                else
                {

                    DateTime date_now = DateTime.Now;
                    string datenow = Convert.ToString(date_now.ToString("MM/dd/yyyy"));
                        

                    //บันทึกรายการ เปิดวัน
                    sqlStr = @"INSERT INTO FINCASHFLOWMAS  
	                (	COOP_ID,                OPERATE_DATE,           ENTRY_ID,   
                        ENTRY_DATE,             CASH_BEGIN,             CASH_AMT,   
                        CASH_SUMAMT,            CASH_IN,                CASH_OUT,   
                        CASH_FOWARD,            CLOSE_STATUS,           CASH_DIFF,              
                        MACHINE_ID,             ENTRY_TIME,             LASTSEQ_NO,          
                        CHQINSAFE_COUNT,        CHQINSAFE_AMT,          CHQINSAFT_BFAMT 
	                )  
                    values
	                (   {0},	                {1},	            {2},
		                {3},					{4},	             0,
		                {4},					0,				     0,
                        0,                      0,                   0,
                        {5},                    {6},                 1,
	                    0,                      0,                   0
                    )";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, state.SsWorkDate, state.SsUsername,
                     datenow, dsMain.DATA[0].CASH_BEGIN, state.SsClientIp, DateTime.Now);
                    //WebUtil.ExeSQL(sqlStr);
                    ta.Exe(sqlStr);

                    // บันทึกรายการธนาคาร สร้างยอดยกมา    FINCLOSEBANKDAY
                    sqlStr = @"INSERT INTO FINCLOSEBANKDAY  
	                (	ACCOUNT_NO,						BANK_CODE,					BANKBRANCH_CODE,
   		                COOP_ID,					    CLOSE_DATE,					BALANCE_BEGIN,
		                INCOMING_AMT,					OUTGOING_AMT,				BALANCE_END
	                )  
	                SELECT
	                   FINBANKACCOUNT.ACCOUNT_NO,	FINBANKACCOUNT.BANK_CODE,	   FINBANKACCOUNT.BANKBRANCH_CODE,
		               FINBANKACCOUNT.COOP_ID,		{1},	                       isnull(FINBANKACCOUNT.SCO_BALANCE,0),
		               0,						    0,					           0
	
                    FROM FINBANKACCOUNT,   
                         CMUCFBANKBRANCH  
                    WHERE ( FINBANKACCOUNT.BANK_CODE = CMUCFBANKBRANCH.BANK_CODE ) and  
                         ( FINBANKACCOUNT.BANKBRANCH_CODE = CMUCFBANKBRANCH.BRANCH_ID ) and  
                         ( ( FINBANKACCOUNT.COOP_ID = {0} ) )    ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, state.SsWorkDate);
                    //WebUtil.ExeSQL(sqlStr);
                    ta.Exe(sqlStr);

                    string ls_finslipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FINCASHSLIPNO");
                    decimal ld_item_amt = 0;
                    ld_item_amt = dsMain.DATA[0].CASH_AMT + dsMain.DATA[0].CASH_BEGIN;
                    // เพิ่มข้อมูลลงรายการเคลื่อนไหว    FINRQ_CASHFLOWSTAT
                    sqlStr = @"INSERT INTO FINRQ_CASHFLOWSTAT  
	                (	OPERATE_DATE,			SEQ_NO,				FINSLIP_NO,
		                ITEM_AMT,				BAL_FORWARD,		BALANCE_AMT,
		                ENTRY_ID,				ENTRY_DATE,		    APPROVE_DATE,
		                APPROVE_ID,				ITEM_FLAG,			ITEM_TYPECODE,
		                MACHINE_ID,				COOP_ID,	        item_desc
	                )  
                    values
	                (    {0},	                1,	                {1},
		                {2},					0,	                {2},
		                {3},					{4},				{4},
                        {3},                     1,                  'OPN',
                        {5},                    {6},                'เปิดงานประจำวัน')";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsWorkDate, ls_finslipno,
                    ld_item_amt, state.SsUsername, dsMain.DATA[0].ENTRY_DATE, state.SsClientIp, state.SsCoopId);
                    ta.Exe(sqlStr);
                    ta.Commit();
                    ta.Close();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
            }
            catch
            {
                ta.RollBack();
                ta.Close();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถเปิดงานประจำวันได้");
            }
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}