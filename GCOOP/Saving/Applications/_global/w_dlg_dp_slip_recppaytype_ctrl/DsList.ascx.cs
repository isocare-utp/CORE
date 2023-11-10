using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Saving.Applications._global.w_dlg_dp_slip_recppaytype_ctrl;

namespace Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPUCFRECPPAYTYPEDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFRECPPAYTYPE;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventItemChanged = "OnDsListtemChanged";
            this.EventClicked = "OnDsListClicked";
            this.Register();
        }

        public void RecpPayType(String type)
        {
            string sql = @"
            SELECT recppaytype_code , recppaytype_desc ,moneytype_support
            FROM   dpucfrecppaytype 
            WHERE  coop_id = {0} and recppaytype_code like '" + type + @"%' 
            order by recppaytype_code ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}