using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rassisttype
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

        public void DdStartassisttype()
        {
            string sql = @"select assisttype_code,assisttype_code||'-'||assisttype_desc as assisttype_desc,1 as sorter from assucfassisttype 
union
select '','',0 from dual order by sorter , assisttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_start", "assisttype_desc", "assisttype_code");
        }

        public void DdEndassisttype()
        {
            string sql = @"select assisttype_code,assisttype_code||'-'||assisttype_desc as assisttype_desc,1 as sorter from assucfassisttype 
union
select '','',0 from dual order by sorter , assisttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_end", "assisttype_desc", "assisttype_code");
        }

        public void DefaultStartassisttype()
        {
            string sql = "select min(assisttype_code) as assisttype_code from assucfassisttype";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next()) {
                this.DATA[0].assisttype_start = dt.GetString("assisttype_code");
            }
        }
        public void DefaultEndassisttype()
        {
            string sql = "select max(assisttype_code) as assisttype_code from assucfassisttype";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].assisttype_end = dt.GetString("assisttype_code");
            }
        }
    }
}