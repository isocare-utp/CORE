using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DataDsListDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataDsList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void Retrieve(string coop_id ,string system_code,DateTime tran_date,string sql)
        {
            string sql_q = @"SELECT 
             MBMEMBMASTER.MEMBER_NO,   
             DPDEPTMASTER.DEPTACCOUNT_NO,   
             DPDEPTMASTER.DEPTACCOUNT_NAME,   
             DPDEPTTRAN.DEPTITEM_AMT,   
             MBMEMBMASTER.MEMB_NAME,   
             MBMEMBMASTER.MEMB_SURNAME
             FROM DPDEPTTRAN  INNER JOIN  DPDEPTMASTER ON  DPDEPTMASTER.DEPTACCOUNT_NO = DPDEPTTRAN.DEPTACCOUNT_NO
             INNER JOIN    MBMEMBMASTER  ON  DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO
             WHERE 
             ( ( DPDEPTTRAN.COOP_ID = {0} ) AND  
            ( DPDEPTTRAN.SYSTEM_CODE = {1} ) AND  
            ( DPDEPTTRAN.TRAN_DATE = {2} )  AND 
            ( DPDEPTTRAN.TRAN_STATUS = 0 ) AND 
		    ( DPDEPTMASTER.DEPTCLOSE_STATUS = 0)) " + sql + " ORDER BY DPDEPTMASTER.MEMBER_NO ASC,DPDEPTMASTER.DEPTACCOUNT_NO ASC";
            sql_q = WebUtil.SQLFormat(sql_q, coop_id, system_code, tran_date);
            DataTable dt = WebUtil.Query(sql_q);
            dt.Columns.Add("fullname", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = WebUtil.GetMembName(state.SsCoopControl, row["MEMBER_NO"].ToString().Trim(), 1);
                row["fullname"] = ls_display;
            }
             
            this.ImportData(dt);
        }
       public decimal[] of_Total(string coop_id, string system_code, DateTime tran_date, string sql)
       {
           decimal count_mem = 0, sum_deptamt = 0, c_num = 0;
            string sql_q = @"SELECT 
             COUNT( *) AS C_NUM ,   
             COUNT( DISTINCT MBMEMBMASTER.MEMBER_NO) AS NUM_MEMBER_NO ,   
             ISNULL( SUM(DPDEPTTRAN.DEPTITEM_AMT),0) AS SUM_DEPTITEM_AMT
             FROM DPDEPTTRAN  INNER JOIN  DPDEPTMASTER ON  DPDEPTMASTER.DEPTACCOUNT_NO = DPDEPTTRAN.DEPTACCOUNT_NO
             INNER JOIN    MBMEMBMASTER  ON  DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO
             WHERE 
             ( ( DPDEPTTRAN.COOP_ID = {0} ) AND  
            ( DPDEPTTRAN.SYSTEM_CODE = {1}) AND  
            ( DPDEPTTRAN.TRAN_DATE = {2})  AND 
            ( DPDEPTTRAN.TRAN_STATUS = 0 ) AND 
		    ( DPDEPTMASTER.DEPTCLOSE_STATUS = 0)) " + sql ;
            sql_q = WebUtil.SQLFormat(sql_q, coop_id, system_code, tran_date);
            DataTable dt = WebUtil.Query(sql_q);
            foreach (DataRow row in dt.Rows)
            {                                
                count_mem = Convert.ToDecimal(row["NUM_MEMBER_NO"].ToString());
                sum_deptamt = Convert.ToDecimal(row["SUM_DEPTITEM_AMT"].ToString());
                c_num = Convert.ToDecimal(row["C_NUM"].ToString());    
            }
            return new decimal[3] { count_mem,sum_deptamt,c_num };             
        }
    }
}
