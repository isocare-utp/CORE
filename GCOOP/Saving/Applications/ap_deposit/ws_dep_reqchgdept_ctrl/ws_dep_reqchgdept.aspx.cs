using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl
{
    public partial class ws_dep_reqchgdept : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostDeptaccountno { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JsLoadBegin();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostDeptaccountno")
            {
                string ls_deptno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, dsMain.DATA[0].DEPTACCOUNT_NO).Replace("-", "").Trim();
                ls_deptno = ls_deptno.Replace("-", "").Trim();
                string ls_docreqchg = dsMain.DATA[0].DPREQCHG_DOC;
                string sql_text="";
                if (ls_docreqchg == "AUTO")
                { 
                    sql_text = "AND DPDEPTMASTER.DEPTACCOUNT_NO='"+ls_deptno+"'";
                } else {
                    sql_text = "AND DPREQCHG_DEPT.DPREQCHG_DOC='" + ls_docreqchg + "'";
                }
                dsMain.ResetRow();
                dsList.ResetRow();
                dsMain.RetriveData(sql_text);
                JsLoadBegin();
                dsMain.DATA[0].DEPTACCOUNT_NO = WebUtil.ViewAccountNoFormat(ls_deptno);   
                dsList.RetriveData(state.SsCoopControl, ls_deptno);
            }
        }
        private void JsLoadBegin() { 
            DateTime work_date = state.SsWorkDate;
            dsMain.DATA[0].COOP_ID = state.SsCoopId;
            dsMain.DATA[0].DPREQCHG_DOC = "AUTO";
            dsMain.DATA[0].ENTRY_DATE = work_date;
            dsMain.DATA[0].DEPTMONTCHG_DATE = work_date;
            dsMain.DATA[0].REQCHG_DATE = work_date;
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            this.SetOnLoadedScript("dsMain.Focus(0,'deptaccount_no');");
        }
        public void SaveWebSheet()
        {
            try { 
                string ls_deptno =dsMain.DATA[0].DEPTACCOUNT_NO;
                ls_deptno = ls_deptno.Replace("-", "").Trim();
                if (!(ls_deptno == null || ls_deptno == ""))
                {
                    string ls_coopid = state.SsCoopId;
                    ls_deptno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, ls_deptno);                    
                    string sql = "";
                    decimal ld_deptmonth_amt = 0, ld_deptmonth_amt_old = 0 , ld_prncbal = 0 ;
                    string sqlStr = @"select deptmonth_amt,prncbal from dpdeptmaster where coop_id = {0} and deptaccount_no = {1}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_deptno);
                    Sdt dt = WebUtil.QuerySdt(sqlStr);
                    if (dt.Next())
                    {
                        ld_deptmonth_amt_old = dt.GetDecimal("deptmonth_amt");
                        ld_prncbal = dt.GetDecimal("prncbal");
                    }
                    ld_deptmonth_amt = dsMain.DATA[0].DEPTMONTH_NEWAMT; 
                    if (ld_deptmonth_amt == ld_deptmonth_amt_old) {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ยอดทำรายการไม่การเปลี่ยนแปลง กรุณาตรวจสอบ"); return;
                    }
                    string ls_remark = dsMain.DATA[0].REMARK.Trim();
                    string ls_docno = wcf.NCommon.of_getnewdocno(state.SsWsPass, ls_coopid, "DPCHGDOCNO");
                    DateTime approve_date = state.SsWorkDate;
                    ExecuteDataSource exed = new ExecuteDataSource(this);

                    // update ใบคำขอเปลี่ยนแปลง
                    sql = @"INSERT INTO DPREQCHG_DEPT  
                     ( 	 COOP_ID,   			DPREQCHG_DOC,   	DEPTACCOUNT_NO,   		DEPTMONTCHG_DATE,   	DEPTMONTH_OLDAMT,   
           	            DEPTMONTH_NEWAMT,   	APPROVE_FLAG,   	DEPTMONTH_APPAMT,   	REMARK,   				ENTRY_DATE,   
           	            ENTRY_ID,   			REQCHG_DATE,   		CHANGE_STATUS,   		PRNCBAL )  
                    VALUES 
  		            (  	     {0},			    {1},			    {2},			        {3},				        {4},
		  	                 {5},			    1,		            {5},			        {6},						{3},
			                 {7},			    {3},		        1,				        {8} )  ";
                    sql = WebUtil.SQLFormat(sql, ls_coopid, ls_docno, ls_deptno, approve_date, ld_deptmonth_amt_old,
                             ld_deptmonth_amt, ls_remark ,
                             state.SsUsername, ld_prncbal);
                    exed.SQL.Add(sql);
                    decimal ld_month_status = 0;
                    if (ld_deptmonth_amt == 0)
                    {
                        ld_month_status = 0;
                    }
                    else {
                        ld_month_status = 1;
                    }
                    //update master
                    sql = @"update dpdeptmaster set deptmonth_status = {1} , deptmonth_amt_old = {2}
                        , deptmonth_amt = {3} where deptaccount_no = {0}";
                    sql = WebUtil.SQLFormat(sql, ls_deptno,ld_month_status, ld_deptmonth_amt_old, ld_deptmonth_amt );
                        exed.SQL.Add(sql);
                    exed.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    
                    dsMain.ResetRow();
                    dsList.ResetRow();
                    JsLoadBegin();
                    dsMain.RetriveData("AND DPDEPTMASTER.DEPTACCOUNT_NO='" + ls_deptno + "'");                                  
                    dsList.RetriveData(state.SsCoopControl, ls_deptno);
                    ls_deptno = WebUtil.ViewAccountNoFormat(ls_deptno);
                    dsMain.DATA[0].DEPTACCOUNT_NO = ls_deptno;       
                    LtServerMessage.Text = WebUtil.CompleteMessage("เลขบัญชี  " + ls_deptno + " บันทึกการทำรายการเรียบร้อยแล้ว");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาใส่เลขที่บัญชี"); return;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}