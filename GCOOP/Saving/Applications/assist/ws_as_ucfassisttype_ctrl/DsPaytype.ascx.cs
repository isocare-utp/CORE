using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class DsPaytype : DataSourceRepeater
    {
        public DataSet1.ASSUCFASSISTTYPEPAYDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPEPAY;
            this.EventItemChanged = "OnDsPaytypeItemChanged";
            this.EventClicked = "OnDsPaytypeClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPaytype");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve( string as_asstype)
        {
            string sql = "SELECT * FROM ASSUCFASSISTTYPEPAY where COOP_ID = '" + state.SsCoopControl + "' and ASSISTTYPE_CODE = '" + as_asstype + "' ORDER BY ASSISTPAY_CODE ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}