using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.admin.ws_am_constant_roundmoney_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMROUNDMONEYDataTable DATA { get; set; }

        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMROUNDMONEY;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList(String ls_applgrp)
        {
            string sql = @" select cmroundmoney.coop_id, 
                            cmroundmoney.applgroup_code, 
                            cmroundmoney.function_code, 
                            cmroundmoney.satang_type, 
                            cmroundmoney.truncate_pos_amt, 
                            cmroundmoney.round_type, 
                            cmroundmoney.round_pos_amt, 
                            cmroundmoney.use_flag 
                            from cmroundmoney";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, ls_applgrp);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

    }
}