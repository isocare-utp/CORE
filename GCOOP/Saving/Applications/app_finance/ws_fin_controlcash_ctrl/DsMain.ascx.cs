using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_controlcash_ctrl
{
    public partial class DsMain : DataSourceFormView
    {        
        public DataSet1.FINCASH_CONTROLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINCASH_CONTROL;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_user");
            this.Register();
        }
        public void RetrieveData(string coop_id, DateTime work_date)
        {
            string sql = @"  SELECT FINCASHFLOWMAS.OPERATE_DATE,   
                     FINCASHFLOWMAS.ENTRY_DATE,   
                     FINCASHFLOWMAS.CASH_BEGIN, 
                     FINCASHFLOWMAS.CASH_SUMAMT as CASH_AMT,   
                     FINCASHFLOWMAS.CASH_IN,   
                     FINCASHFLOWMAS.CASH_OUT,   
                     FINCASHFLOWMAS.CASH_FOWARD,   
                     FINCASHFLOWMAS.CLOSE_STATUS,   
                     FINCASHFLOWMAS.COOP_ID,
                     '' as XmlCashDetail  
                FROM FINCASHFLOWMAS  
               WHERE ( FINCASHFLOWMAS.COOP_ID = {0} ) AND  
                     ( FINCASHFLOWMAS.OPERATE_DATE = {1} )    ";
            sql = WebUtil.SQLFormat(sql, coop_id, work_date);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
        public decimal DDStatus(string sql_text)
        {
            decimal ld_return = 0;
            string sql = @"  SELECT FINUCFSTATUS.STATUS,   
                             FINUCFSTATUS.STATUS_DESC,   
                             FINUCFSTATUS.COOP_ID  
                        FROM FINUCFSTATUS  
                       WHERE ( FINUCFSTATUS.STATUS in (11,14,15,16) ) " + sql_text + @"
                        order by STATUS";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "ITEM_TYPE", "STATUS_DESC", "STATUS");
            ld_return = Convert.ToDecimal(dt.Rows[0].Field<decimal>("STATUS"));
            return ld_return;
        }
        
        public int DDUsername(string coopid,string username)
        {
            int ln_row = 0;
            string sql = @" SELECT * FROM AMSECUSERS  
                       WHERE ( AMSECUSERS.USER_NAME = {1} ) AND  
                             ( AMSECUSERS.COOP_CONTROL = {0} ) ";
            sql = WebUtil.SQLFormat(sql, coopid, username);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                ln_row = 1;
            }
            return ln_row;
        }

        public void RetriveUser(string coopid, string username, DateTime ldm_date)
        {
            string sql = @"SELECT	FINTABLEUSERMASTER.COOP_ID,FINTABLEUSERMASTER.OPDATEWORK AS OPERATE_DATE,
                        FINTABLEUSERMASTER.USER_NAME AS ENTRY_ID,FINCASHFLOWMAS.CASH_SUMAMT AS CASH_AMT,
                        FINTABLEUSERMASTER.STATUS,	FINTABLEUSERMASTER.AMOUNT_BALANCE as MONEY_REMAIN,AMSECUSERS.FULL_NAME
                        FROM	FINTABLEUSERMASTER,AMSECUSERS,FINCASHFLOWMAS
                        WHERE	
					    ( FINTABLEUSERMASTER.COOP_ID = FINCASHFLOWMAS.COOP_ID ) AND 
						( FINTABLEUSERMASTER.OPDATEWORK = FINCASHFLOWMAS.OPERATE_DATE ) AND
                        ( FINTABLEUSERMASTER.COOP_ID = AMSECUSERS.COOP_ID ) AND 
				        ( FINTABLEUSERMASTER.USER_NAME = AMSECUSERS.USER_NAME ) AND 
                        ( FINTABLEUSERMASTER.USER_NAME		= {2} ) AND
			            ( FINTABLEUSERMASTER.OPDATEWORK	    = {1} )  and
			            ( FINTABLEUSERMASTER.coop_id		= {0} )
                        ";
            sql = WebUtil.SQLFormat(sql, coopid, ldm_date , username);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);         
        }        
    }
}