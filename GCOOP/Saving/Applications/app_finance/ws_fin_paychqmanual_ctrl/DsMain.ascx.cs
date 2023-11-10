using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_paychqmanual_ctrl
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
            this.Button.Add("b_1");
            this.Register();
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
            this.DropDownDataBind(dt, "bank_code", "display", "value_code");
            this.DropDownDataBind(dt, "frombank", "display", "value_code");
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
            this.DropDownDataBind(dt, "bank_branch", "display", "value_code");
            this.DropDownDataBind(dt, "frombranch", "display", "value_code");
        }
        public void Ddbookno(string bank, string bank_branch )
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
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, bank, bank_branch);
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
            this.DropDownDataBind(dt, "cheque_bookno", "display", "value_code");
        }
        public void Ddchequeno(string bank, string bank_branch,string book_no)
        {
            string sql = @"
                        SELECT  CHEQUE_NO as value_code,1 as sorter FROM 
                FINCHQEUESTATEMENT   
                WHERE (  FINCHQEUESTATEMENT.USE_STATUS = 0 ) and  
                ( FINCHQEUESTATEMENT.COOP_ID = {0}) and    
                ( FINCHQEUESTATEMENT.BANK_CODE = {1} ) And   
                ( FINCHQEUESTATEMENT.BANK_BRANCH = {2} ) And     
                ( FINCHQEUESTATEMENT.CHEQUEBOOK_NO = {3} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, bank, bank_branch, book_no);
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
            this.DropDownDataBind(dt, "account_no", "display", "value_code");
        }
        public void Ddtofromacc()
        {
            string sql = @"
                SELECT  cmucftofromaccid.account_id as value_code,accmaster.account_name  as value_desc,1 as sorter
                FROM cmucftofromaccid ,accmaster  where accmaster.account_id = cmucftofromaccid.account_id  and cmucftofromaccid.moneytype_code='CHQ' 
                and cmucftofromaccid.sliptype_code='PX ' ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "","", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "as_tofromaccid", "display", "value_code");
        }
        public void Ddchqtype()
        {
            string sql = @"
                SELECT  FINCHEQUETYPE.CHEQUE_TYPE ,  
                FINCHEQUETYPE.CHEQUE_DESC ,    
                FINCHEQUETYPE.COOP_ID   
                FROM FINCHEQUETYPE    
                WHERE ( FINCHEQUETYPE.COOP_ID = {0} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "cheque_type", "CHEQUE_DESC", "CHEQUE_TYPE");
        }
    }
}