using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rmembgroup_loantype
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name from cmcoopmaster ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }

        public void DdStartMembgroup() {
            string sql = @"select membgroup_code,membgroup_code||'-'||membgroup_desc as membgroup_desc,1 as sorter from mbucfmembgroup 
union
select '','',0 from dual order by sorter , membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "start_membgroup", "membgroup_desc", "membgroup_code");
        }

        public void DdEndMembgroup() {
            string sql = @"select membgroup_code,membgroup_code||'-'||membgroup_desc as membgroup_desc,1 as sorter from mbucfmembgroup 
union
select '','',0 from dual order by sorter , membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "end_membgroup", "membgroup_desc", "membgroup_code");
        }

        public void DefaultStartMembgroup() {
            string sql = "select min(membgroup_code) as membgroup_code from mbucfmembgroup";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next()) {
                this.DATA[0].start_membgroup = dt.GetString("membgroup_code");
            }
        }
        public void DefaultEndMembgroup()
        {
            string sql = "select max(membgroup_code) as membgroup_code from mbucfmembgroup";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].end_membgroup = dt.GetString("membgroup_code");
            }
        }
        public void DdLoantype()
        {
            String sql = @"select loantype_code, loantype_desc,1 as sorter from lnloantype 
union
select '','',0 from dual order by sorter , loantype_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "loantype_desc", "loantype_code");
        }
    }
}