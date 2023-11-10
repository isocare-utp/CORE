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
            this.Button.Add("b_add");
            this.Register();
        }

        public void DdGroupControl()
        {
            string sql = @"select membgroup_control as value_code,   
            membgroup_controldesc as value_desc, 1 as sort
            from mbucfmembgroupcontrol";
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + "  " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "00", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "cp_groupcontrol", "display", "value_code");

        }
    }
}