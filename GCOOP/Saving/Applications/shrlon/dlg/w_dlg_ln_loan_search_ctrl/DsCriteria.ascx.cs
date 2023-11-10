using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.dlg.w_dlg_ln_loan_search_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.LNNPLCRITERIADataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLCRITERIA;
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.Register();
        }

        public void DdLoanTypeCode(String coopId)
        {
            string sql = @"
                select 
                  loantype_code,    
                  loantype_code || ' (' || prefix || ') ' || loantype_desc as loantype_desc,
                  1 as sorter
                from lnloantype
                where
                  coop_id = '" + coopId + @"' and
                  uselnreq_flag = 1
                union select '', '', 0 from dual order by sorter, loantype_code";
            this.DropDownDataBind(sql, "loantype_code", "loantype_desc", "loantype_code");
        }

        public void DdLawtypeCode()
        {
            string sql = @"
                select 
	                lawtype_code,
	                lawtype_desc,
	                2 as sorter
                from lnucfnpllawtype where coop_id = '010001'
                union
                select -1 as lawtype_code, '', 0 as sorter from dual
                union
                select 999 as lawtype_code, '(ลูกหนี้มีปัญหาทั้งหมด)', 1 as sorter from dual
                order by sorter, lawtype_code";
            this.DropDownDataBind(sql, "lawtype_code", "lawtype_desc", "lawtype_code");
        }
    }
}