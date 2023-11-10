using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_date_bank_branch
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.TransferBankDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.TransferBank;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void DdBranch(string bank_code)
        {
            string sql = @"
            select 
finbankaccount.bank_code ,
cmucfbankbranch.branch_id ,
cmucfbankbranch.branch_name ,
1 as sorter
from cmucfbankbranch,finbankaccount
where finbankaccount.bankbranch_code = cmucfbankbranch.branch_id
and cmucfbankbranch.bank_code = {0}
--order by cmucfbankbranch.branch_name
union select '', '', '',0  from dual order by sorter ,branch_name
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_branch", "branch_name", "branch_id");
        }

        public void RetrieveMain()
        {
            String sql = @"select 	cmucfbank.bank_code, 
			cmucfbank.bank_desc,
			1 as sorter
from 		cmucfbank, 
			finbankaccount
where 	finbankaccount.bank_code = cmucfbank.bank_code (+)
group by cmucfbank.bank_code,cmucfbank.bank_desc
union select '', '', 0 from dual order by sorter";
            //String sql = @"select *, from cmucfbank order by bank_code";

            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
    }
}