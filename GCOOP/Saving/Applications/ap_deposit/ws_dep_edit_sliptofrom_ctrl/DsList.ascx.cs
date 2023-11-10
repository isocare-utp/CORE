using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_edit_sliptofrom_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void Retrieve(string coop_id, DateTime start_date, string ls_sqlext)
        {
            string sql = @"SELECT              
            DPDEPTSLIP.DEPTSLIP_NO,      	DPDEPTSLIP.DEPTSLIP_DATE ,              DPDEPTSLIP.RECPPAYTYPE_CODE ,       
            DPDEPTSLIP.ENTRY_ID ,           DPDEPTMASTER.DEPTACCOUNT_NO ,           DPDEPTSLIP.ENTRY_DATE ,
            DPDEPTMASTER.MEMBER_NO ,        DPDEPTMASTER.DEPTACCOUNT_NAME ,         DPDEPTMASTER.DEPTTYPE_CODE ,          
            MBMEMBMASTER.MEMB_NAME ,        MBMEMBMASTER.MEMB_SURNAME ,           	MBMEMBMASTER.MEMBGROUP_CODE ,        
            DPDEPTSLIP.DEPTSLIP_NETAMT,     DPDEPTSLIP.TOFROM_ACCID,                DPDEPTSLIP.CASH_TYPE     
            FROM DPDEPTSLIP ,           DPDEPTMASTER ,           MBMEMBMASTER     
            WHERE ( DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO) and         
            ( DPDEPTMASTER.COOP_ID = MBMEMBMASTER.COOP_ID) and          
            ( DPDEPTSLIP.DEPTACCOUNT_NO = DPDEPTMASTER.DEPTACCOUNT_NO ) and               
            ( DPDEPTSLIP.DEPTCOOP_ID = DPDEPTMASTER.COOP_ID ) and                      
            ( DPDEPTSLIP.CASH_TYPE <> 'CSH') AND 
            ( DPDEPTSLIP.COOP_ID = {0} ) AND 
            ( DPDEPTSLIP.posttovc_flag = 0 ) AND
            ( DPDEPTSLIP.DEPTSLIP_DATE = {1}  " + ls_sqlext + ") ORDER BY DPDEPTSLIP.DEPTSLIP_DATE DESC,DPDEPTSLIP.DEPTSLIP_NO DESC ";
            sql = WebUtil.SQLFormat(sql, coop_id, start_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);              
        }
        public void DD_Tofromaccid(String cash_type, int rowfocus)
        {
            string sql = @"
                   SELECT account_id  AS VALUE_CODE,   
                    account_desc AS VALUE_DESC,
                    1 as sort 
                    FROM dpucftofromaccid  
                    WHERE cash_type ={0}";
            sql = WebUtil.SQLFormat(sql, cash_type);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "TOFROM_ACCID", "display", "value_code", rowfocus);
        }
    }
}