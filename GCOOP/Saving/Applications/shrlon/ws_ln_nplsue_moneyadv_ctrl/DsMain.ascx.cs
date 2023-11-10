using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MainDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_Main;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.TableName = "LNCOLLMASTER";
            this.Button.Add("b_memsearch");
            this.Register();
        }

        public void RetrieveMain(String member_no)
        {
            String sql = @"select mbmembmaster.member_no,
                mbucfprename.prename_desc || mbmembmaster.memb_name || '  ' || mbmembmaster.memb_name as cp_name,
                mbmembmaster.membtype_code || ' - ' || mbucfmembtype.membtype_desc as membtype_desc,
                mbmembmaster.membgroup_code || ' - ' || mbucfmembgroup.membgroup_desc as membgroup_desc
                from mbmembmaster,
                mbucfprename,
                mbucfmembgroup,
                mbucfmembtype
                where mbmembmaster.prename_code =  mbucfprename.prename_code
                and mbmembmaster.membgroup_code =  mbucfmembgroup.membgroup_code
                and mbmembmaster.membtype_code =  mbucfmembtype.membtype_code
                and mbmembmaster.coop_id = {0}
                and mbmembmaster.member_no = {1}";

            sql = WebUtil.SQLFormat(sql,state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}