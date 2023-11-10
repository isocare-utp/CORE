using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl
{
    public partial class DsSearch : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }

        public void InitDsSearch(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsSearchItemChanged";
            this.EventClicked = "OnDsSearchClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSearch");
            this.Button.Add("b_search");
            this.Button.Add("b_next");
            this.Button.Add("b_add");
            this.Register();
        }

        public void DdGroupControl()
        {
            string sql = @"select membgroup_control,   
                membgroup_control +' - ' + membgroup_controldesc as display , 1 as sorter
            from mbucfmembgroupcontrol
            union 
            select '','',0 from dual order by sorter,membgroup_control asc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "cp_groupcontrol", "display", "membgroup_control");
        }
    }
}