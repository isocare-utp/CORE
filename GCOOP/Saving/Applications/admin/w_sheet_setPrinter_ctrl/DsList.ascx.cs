using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_setPrinter_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public PaPrintingMaster.PAPRINTINGMASTERDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            PaPrintingMaster ds = new PaPrintingMaster();
            this.DATA = ds.PAPRINTINGMASTER;
            this.Button.Add("b_del");
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void retrieve()
        {
            string sql = @"    select * from paprintingmaster";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}