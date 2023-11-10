using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_requestment_lite_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNREQLOANDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQLOAN;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }

        public void Retrieve(string memberNO)
        {
            string sql = "select * from lnreqloan where coop_id='" + state.SsCoopControl + "' and member_no='" + memberNO + "'";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}