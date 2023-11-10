using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_getmembno_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.CMDOCUMENTCONTROLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMDOCUMENTCONTROL;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
            this.Button.Add("b_save");
            this.Button.Add("b_search");

        }
    }
}