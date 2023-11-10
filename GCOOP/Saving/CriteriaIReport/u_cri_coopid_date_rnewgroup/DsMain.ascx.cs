using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_date_rnewgroup
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

        public void DdMembgroup()
        {
            string sql = @"
                select newgrp_code as membgroup_code, newgrp_code||' '||ft_memgrp(coop_id, newgrp_code) as display,1 as sorter from mbmembmaster 
                union
                select '','',0 from dual
                order by sorter,membgroup_code"
            ;
            sql = WebUtil.SQLFormat(sql);
            this.DropDownDataBind(sql, "membgroup_start", "display", "membgroup_code");
            this.DropDownDataBind(sql, "membgroup_end", "display", "membgroup_code");
        }
    }
}