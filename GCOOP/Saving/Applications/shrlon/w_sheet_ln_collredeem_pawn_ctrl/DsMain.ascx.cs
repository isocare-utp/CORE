using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_collredeem_pawn_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNMRTGMASTERDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNMRTGMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");            
            this.Button.Add("b_sh_pawn");
            //this.Button.Add("b_print");
            this.Register();
        }

        public void Retrieve(string mrtgmast_no)
        {
            string sql = "select * from mbmembmaster,lnmrtgmaster where mbmembmaster.member_no = lnmrtgmaster.member_no and lnmrtgmaster.coop_id='" + state.SsCoopControl + "' and lnmrtgmaster.mrtgmast_no='" + mrtgmast_no + "'";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}