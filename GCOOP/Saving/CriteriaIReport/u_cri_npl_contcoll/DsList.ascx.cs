using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_npl_contcoll
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNUCFNPLLAWTYPEDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNUCFNPLLAWTYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve()
        {
            string sql = "select * from lnucfnpllawtype where COOP_ID = '" + state.SsCoopControl + "' ORDER BY lawtype_code ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}