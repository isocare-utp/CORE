using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Button.Add("b_detail");
            this.Register();
        }
        public void RetrieveList()
        {
            string sql = @"select *
            from mbucfmembgroup
            where ( coop_id = {0} )
                order by membgroup_code ASC";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
        public void DdGroupControl(string membgroup_code)
        {
            string sql = @"select membgroup_control,   
                membgroup_control + ' - ' + membgroup_controldesc as display , 1 as sorter
            from mbucfmembgroupcontrol where membgroup_code like {0}
            union 
            select '','',0 from dual order by sorter,membgroup_control asc";
            sql = WebUtil.SQLFormat(sql,membgroup_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "cp_groupcontrol", "display", "membgroup_control");
        }
    }
}