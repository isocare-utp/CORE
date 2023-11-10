using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_am_coopconstant_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.CMCOOPCONSTANTDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMCOOPCONSTANT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"SELECT *
                                FROM CMCOOPCONSTANT
                                WHERE COOP_NO='" + state.SsCoopId+ "'";

            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}