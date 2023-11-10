using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.fund.ws_fund_cancel_payfundcoll_ctrl
{
    public partial class ws_fund_cancel_payfundcoll : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DateTime work_date = state.SsWorkDate;
                dsMain.DATA[0].ADT_DATE = work_date;
                dsList.RetriveData(state.SsCoopControl, work_date);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try
            {
                String sqlStr = "" ;
                string sql = "";
                string  ls_memberno = "", ls_fullname = "", ls_refslipno = "", ls_loancontractno = "";
                decimal ld_fundbalance = 0;
                int ln_laststmno = 0;
                DateTime now_date = DateTime.Now;
                DateTime work_date = state.SsWorkDate;
                string ls_usename = state.SsUsername;
                string ls_coopid = state.SsCoopId;
                string ls_clientip = state.SsClientIp;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    ls_memberno = dsList.DATA[i].MEMBER_NO;
                    ld_fundbalance = dsList.DATA[i].ITEMPAY_AMT;
                    ls_fullname = dsList.DATA[i].FULLNAME;
                    ls_loancontractno = dsList.DATA[i].LOANCONTRACT_NO;
                    ls_refslipno = dsList.DATA[i].REFSLIP_NO;
                    sql = @"SELECT LASTSTM_NO+1 AS LASTSTM_NO
                    FROM FUNDCOLLMASTER WHERE COOP_ID = {0} AND MEMBER_NO={1}  AND FUND_STATUS = 0";
                    sql = WebUtil.SQLFormat(sql, ls_coopid, ls_memberno);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        ln_laststmno = dt.GetInt32("LASTSTM_NO");
                    }
                    #region FUNDCOLLSTATEMENT
                    sqlStr = @"    INSERT INTO FUNDCOLLSTATEMENT  
                    (		COOP_ID,			MEMBER_NO ,			LOANCONTRACT_NO ,			SEQ_NO ,   
                            OPERATE_DATE ,		ITEMTYPE_CODE ,		ITEMPAY_AMT ,				BALANCE_FORWARD ,   
                            BALANCE ,			FUND_STATUS ,		ENTRY_DATE ,			    ENTRY_ID
                    )  
                    VALUES
                    ( 		{0},   			    {1},					{2},				    {3},
                            {4},		        'ERI',			        {5},					0,
                            {5},		           -1,		            {6},					{7}
                    ) ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_memberno, ls_loancontractno, ln_laststmno
                        , work_date, ld_fundbalance
                        , now_date, ls_usename);
                    exedinsert.SQL.Add(sqlStr);
                    sqlStr = @"UPDATE	FUNDCOLLSTATEMENT
	                                SET			FUND_STATUS  	= -1 		
	                                WHERE	 COOP_ID            = {0}
                            AND MEMBER_NO={1} AND REFSLIP_NO={2}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_memberno,ls_refslipno);
                    exedinsert.SQL.Add(sqlStr);

                    #endregion
                    #region FUNDCOLLMASTER
                    sqlStr = @"UPDATE	FUNDCOLLMASTER
	                                SET			FUND_STATUS  	= 1 , LASTSTM_NO = {3},
				                                FUNDBALANCE		= {4} ,
				                                LASTACCESS_DATE	= {5},RESIGN_DATE = NULL				
	                                WHERE	 COOP_ID            = {0}
                                    AND MEMBER_NO={1} ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_memberno, ls_loancontractno, ln_laststmno, ld_fundbalance, work_date);
                    exedinsert.SQL.Add(sqlStr);
                    #endregion

                    #region FIN

                    sql = @"SELECT SLIP_NO FROM FINSLIP WHERE COOP_ID = {0} AND SLIP_NO = {1} AND PAYMENT_STATUS = 8";
                    sql = WebUtil.SQLFormat(sql, ls_coopid, ls_refslipno);
                    Sdt dt_fin = WebUtil.QuerySdt(sql);
                    if (dt_fin.Next())
                    {
                        sqlStr = @"UPDATE	FINSLIP
	                                SET			PAYMENT_STATUS  = -9 , CANCEL_ID = {2},
				                                CANCEL_DATE		= {3} 		
	                                WHERE	 COOP_ID = {0}  AND SLIP_NO = {1} ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_refslipno,ls_usename, work_date);
                        exedinsert.SQL.Add(sqlStr);
                    }
                    #endregion
                }
                exedinsert.Execute();
                exedinsert.SQL.Clear();
                dsList.RetriveData(ls_coopid, work_date);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");
            }
            catch (Exception ex)
            {
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}