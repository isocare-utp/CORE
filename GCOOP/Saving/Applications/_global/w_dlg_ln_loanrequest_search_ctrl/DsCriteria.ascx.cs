using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_ln_loanrequest_search_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.LNLOANREQUESTCRITERIADataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANREQUESTCRITERIA;
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
    }
}