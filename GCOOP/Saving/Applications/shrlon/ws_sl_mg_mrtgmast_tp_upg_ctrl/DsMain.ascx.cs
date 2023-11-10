using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_tp_upg_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void MemberNoRetrieve()
        {
            string ls_sql = @"select m.member_no,
                m.memb_name,
                m.memb_surname,
                m.mariage_status,
                m.card_person,
                m.mate_name,
                m.membgroup_code,
                mup.prename_desc,
                mug.membgroup_desc
                from mbmembmaster m,
                mbucfprename mup,
                mbucfmembgroup mug
                where m.prename_code = mup.prename_code
                and m.coop_id = mug.coop_id
                and m.membgroup_code = mug.membgroup_code
                and m.coop_id = {0}
                and m.member_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, this.DATA[0].MEMBER_NO);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}