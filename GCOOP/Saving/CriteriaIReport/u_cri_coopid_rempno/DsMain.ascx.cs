using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rempno
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name from cmcoopmaster ";
            sql = WebUtil.SQLFormat(sql);            
            this.DropDownDataBind(sql, "coop_id", "coop_name", "coop_id");
        }

        public void DdEmpno()
        {
            string sql = @"select em.emp_no, 
                em.salary_id || ' - ' || pn.prename_desc || em.emp_name || '  ' || em.emp_surname as display,
                1 as sorter
                from hremployee em, mbucfprename pn 
                where em.prename_code = pn.prename_code
                union
                select '','',0 from dual
                order by sorter, emp_no"
            ;
            sql = WebUtil.SQLFormat(sql);
            this.DropDownDataBind(sql, "semp_name", "display", "emp_no");
            this.DropDownDataBind(sql, "eemp_name", "display", "emp_no");            
        }
    }
}