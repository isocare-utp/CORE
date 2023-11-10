using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_booktyp_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.DPUCFBOOKCONSTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
          

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFBOOKCONST;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Button.Add("B_DEL");
            this.Register();
        }

        public void retrieve() {
            String re = @"SELECT BOOK_TYPE,BOOK_GRP,BOOK_DESC,COOP_ID FROM DPUCFBOOKCONST ORDER BY book_grp";
            DataTable dt = WebUtil.Query(re);
            this.ImportData(dt);
        }
    }
}