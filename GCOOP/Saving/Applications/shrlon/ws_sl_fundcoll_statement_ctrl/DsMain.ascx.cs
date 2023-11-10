using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_fundcoll_statement_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FUNDCOLLMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
            this.Button.Add("b_printbook");
            this.Button.Add("b_printstate");
            this.Button.Add("b_processint");
        }

        public void Retrieve(string memberNo)
        {
            string sql = @"select  ft_getmemname(fundcollmaster.coop_id, fundcollmaster.member_no) as fullname,
                            fundcollmaster.*
                            from fundcollmaster
                            where fundcollmaster.coop_id = {0}
                            and fundcollmaster.member_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}