using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.dlg.ws_dlg_sl_shareucftype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SHSHARETYPEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHARETYPE;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_add");
            this.Button.Add("b_cancel");
            this.Register();
        }
        public void Ddshrtype()
        {
            string sql = @"select
                        sharetype_code, sharetype_code||' - '||sharetype_desc as display, 1 as sorter
                        from shsharetype
                        union
                        select '','',0 from dual order by sorter, sharetype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "usepattern_shrcode", "display", "sharetype_code");

        }
    }
}