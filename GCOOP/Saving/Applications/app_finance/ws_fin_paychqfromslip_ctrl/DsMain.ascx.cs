using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_paychqfromslip_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_accno");
            this.Button.Add("b_search");
            this.Register();
        }

        public void RetrieveMain()
        {
            string sql = @"
              ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void Ddbank()
        {
            string sql = @"
                SELECT BANK_CODE as value_code,BANK_DESC as value_desc,1 as sorter  
                FROM CMUCFBANK 
                WHERE BANK_CODE IN(SELECT BANK_CODE FROM FINCHEQUEMAS WHERE AVAILABLE_FLAG=1 )";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "as_bank", "display", "value_code");
        }
        public void Ddbankbranch(string bank)
        {
            string sql = @"
                SELECT BRANCH_ID as value_code,BRANCH_NAME as value_desc,1 as sorter 
                FROM CMUCFBANKBRANCH  WHERE BANK_CODE = {0}
                AND BRANCH_ID IN(SELECT BANK_BRANCH FROM FINCHEQUEMAS WHERE AVAILABLE_FLAG=1 AND BANK_CODE = {0})";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "as_bankbranch", "display", "value_code");
        }
        public void DdChqbookno(string coopid,string bank,string bankbranch)
        {
            string sql = @"
                SELECT FINCHEQUEMAS.CHEQUEBOOK_NO as value_code,
		        1 as sorter
                FROM FINCHEQUEMAS  
                WHERE
                ( FINCHEQUEMAS.COOP_ID = {0} ) AND  
                ( FINCHEQUEMAS.BANK_CODE = {1} ) AND  
                ( FINCHEQUEMAS.BANK_BRANCH = {2} ) AND  
                ( FINCHEQUEMAS.AVAILABLE_FLAG = 1 )    
           ";
            sql = WebUtil.SQLFormat(sql,coopid, bank, bankbranch);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "as_chqbookno", "display", "value_code");
        }
        
        public void DdChqno(string coopid, string bank, string bankbranch,string chqbook_no)
        {
            string sql = @"
                        SELECT  CHEQUE_NO as value_code,1 as sorter FROM 
                FINCHQEUESTATEMENT   
                WHERE (  FINCHQEUESTATEMENT.USE_STATUS = 0 ) and  
                ( FINCHQEUESTATEMENT.COOP_ID = {0}) and    
                ( FINCHQEUESTATEMENT.BANK_CODE = {1} ) And   
                ( FINCHQEUESTATEMENT.BANK_BRANCH = {2} ) And     
                ( FINCHQEUESTATEMENT.CHEQUEBOOK_NO = {3} )   ";
            sql = WebUtil.SQLFormat(sql, coopid, bank, bankbranch, chqbook_no);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "as_chqstartno", "display", "value_code");
        }
        public void DdChqType(string coopid)
        {
            string sql = @"
                SELECT FINCHEQUETYPE.CHEQUE_TYPE,   
                FINCHEQUETYPE.CHEQUE_DESC  
                FROM FINCHEQUETYPE  
                WHERE FINCHEQUETYPE.COOP_ID = {0}   
                order by  CHEQUE_TYPE";
            sql = WebUtil.SQLFormat(sql, coopid);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_chqtype", "CHEQUE_DESC", "CHEQUE_TYPE");
        }
    }
}