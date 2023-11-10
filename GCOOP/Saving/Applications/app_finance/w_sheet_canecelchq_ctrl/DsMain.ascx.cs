using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.w_sheet_canecelchq_ctrl
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
            this.Button.Add("b_search");
            this.Register();
        }
        public void Ddbank()
        {
            string sql = @"
                     select mas.* from
                    (
	                    select
	                    CMUCFBANK.BANK_CODE,   
		                CONCAT(CMUCFBANK.BANK_CODE,' ',CMUCFBANK.BANK_DESC) as BANK_DESC,   
		                CMUCFBANK.EDIT_FORMAT  ,
		                1 as sorter
		                FROM CMUCFBANK   
	                    union select
	                    '' as BANK_CODE,'--กรุณาเลือก--' as BANK_DESC,'' as EDIT_FORMAT,0 as sorter	
                    )mas
                    order by  mas.sorter , mas.BANK_DESC";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "BANK_DESC", "BANK_CODE");
        }
        public void Ddbankbranch(string bank)
        {
            string sql = @"
                        select mas.* from
                        (
	                        select
	                        CMUCFBANKBRANCH.BANK_CODE,   
	                        CMUCFBANKBRANCH.BRANCH_ID,   
	                        CONCAT(CMUCFBANKBRANCH.BRANCH_ID ,' ',CMUCFBANKBRANCH.BRANCH_NAME) as BRANCH_NAME   ,
	                        1 as sorter
	                        FROM CMUCFBANKBRANCH  
	                        WHERE CMUCFBANKBRANCH.BANK_CODE ={0}
	                        union select
	                        '' as BANK_CODE,'' as BRANCH_ID,'--กรุณาเลือก--' as BRANCH_NAME,0 as sorter	
                        )mas
                        order by  mas.sorter , mas.BANK_CODE,mas. BRANCH_ID";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "branch_code", "BRANCH_NAME", "BRANCH_ID");
        }
    }
}