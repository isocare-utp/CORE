using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.fund.ws_fund_process_payfundcoll_ctrl
{
    public partial class ws_fund_process_payfundcoll : PageWebSheet, WebSheet
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
                dsList.RetriveData(state.SsCoopControl);
            }  
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try {
                String sqlStr="";
                string ls_slipno = "", ls_memberno = "", ls_fullname = "", ls_tofromaccid = "", ls_loancontractno = "", ls_slipdesc="";
                decimal ld_fundbalance = 0;
                int ln_laststmno=0;
                DateTime now_date = DateTime.Now;
                DateTime work_date = state.SsWorkDate;
                string ls_usename = state.SsUsername;
                string ls_coopid = state.SsCoopId;
                string ls_clientip = state.SsClientIp;
                ls_tofromaccid = "11010001";
                for (int i = 0; i < dsList.RowCount;i++ )
                {
                    if (dsList.DATA[i].choose_flag == 1)
                    {
                        ls_memberno = dsList.DATA[i].member_no;
                        ld_fundbalance = dsList.DATA[i].fundbalance;
                        ls_fullname = dsList.DATA[i].fullname;
                        ls_slipdesc = "จ่ายคืนกองทุน " + ls_fullname;
                        ls_loancontractno = dsList.DATA[i].loancontract_no;
                        string sql = @"SELECT LASTSTM_NO+1 AS LASTSTM_NO
                    FROM FUNDCOLLMASTER WHERE COOP_ID = {0} AND MEMBER_NO={1} AND FUND_STATUS = 1";
                        sql = WebUtil.SQLFormat(sql, ls_coopid, ls_memberno);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            ln_laststmno = dt.GetInt32("LASTSTM_NO");
                        }

                        ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, ls_coopid, "FNRECEIVENO");

                        #region FUNDCOLLSTATEMENT
                        sqlStr = @"    INSERT INTO FUNDCOLLSTATEMENT  
                    (		COOP_ID,			MEMBER_NO ,			LOANCONTRACT_NO ,			SEQ_NO ,   
                            OPERATE_DATE ,		ITEMTYPE_CODE ,		ITEMPAY_AMT ,				BALANCE_FORWARD ,   
                            BALANCE ,			FUND_STATUS ,		ENTRY_DATE ,			    ENTRY_ID,
                            REFSLIP_NO
                    )  
                    VALUES
                    ( 		{0},   			    {1},					{2},				    {3},
                            {4},		        'CLS',			        {5},					{5},
                            0,		            1,		                {6},					{7},
                            {8}
                    ) ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_memberno, ls_loancontractno, ln_laststmno
                            , work_date, ld_fundbalance
                            , now_date, ls_usename, ls_slipno);
                        exedinsert.SQL.Add(sqlStr);
                        #endregion
                        #region FUNDCOLLMASTER
                        sqlStr = @"UPDATE	FUNDCOLLMASTER
	                                SET			FUND_STATUS  	= 0 , LASTSTM_NO = {3},
				                                FUNDBALANCE		= 0 ,
				                                LASTACCESS_DATE	= {2},RESIGN_DATE = {2}				
	                                WHERE	 COOP_ID            = {0}
                            AND MEMBER_NO={1} ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_memberno, work_date, ln_laststmno);
                        exedinsert.SQL.Add(sqlStr);
                        #endregion
                        #region FIN

                        sqlStr = @"    INSERT INTO FINSLIP  
                    (		SLIP_NO,			ENTRY_ID,				ENTRY_DATE,				OPERATE_DATE,   
                            FROM_SYSTEM,		PAYMENT_STATUS,		    CASH_TYPE,				PAYMENT_DESC,   
                            MEMBER_NO,			PAY_RECV_STATUS,		ITEMPAY_AMT,			PAY_TOWHOM,   
                            MEMBER_FLAG,		NONMEMBER_DETAIL,	    coop_id,				MACHINE_ID,   
                            TOFROM_ACCID,		ACCOUNT_NO,				ITEMPAYTYPE_CODE,       REF_SYSTEM ,
                            ITEM_AMTNET  
                    )  
                    VALUES
                    ( 		{0},   			    {1},					{2},				    {2},
                            'FIN',		        8,			            'CSH',					'จ่ายคืนกองทุน',
                            {3},		        0,		                {4},					{5},
                            1,   			    {5},			        {6},					{7},
                            {8},			    {8},				    'FUN',				    'LON',
                            {4}
                    ) ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_slipno, ls_usename, work_date
                            , ls_memberno, ld_fundbalance, ls_fullname, ls_coopid, ls_clientip
                            , ls_tofromaccid);
                        exedinsert.SQL.Add(sqlStr);

                        sqlStr = @"    INSERT INTO FINSLIPDET  
                    (		COOP_ID,			SLIP_NO,				SEQ_NO,				SLIPITEMTYPE_CODE,   
                            SLIPITEM_DESC,		SLIPITEM_STATUS,		ITEMPAY_AMT,		ACCOUNT_ID,   
                            ITEMPAYAMT_NET,		OPERATE_FLAG 
                    )  
                    VALUES
                    ( 		{0},   			    {1},					1,				    {2},
                            {3},	            1,			            {4},				{2},
                            {4},		        1
                    ) ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_slipno, ls_tofromaccid
                            , ls_slipdesc, ld_fundbalance);
                        exedinsert.SQL.Add(sqlStr);

                        #endregion
                    }    
                }
                exedinsert.Execute();
                exedinsert.SQL.Clear();
                dsList.RetriveData(state.SsCoopControl);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");
            }
            catch(Exception ex)
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