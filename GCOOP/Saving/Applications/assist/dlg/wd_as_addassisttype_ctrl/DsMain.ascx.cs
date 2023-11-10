using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_addassisttype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_add");
            this.Button.Add("b_cancel");
            this.Register();
        }

        public void RetriveGroup()
        {
            string sql = @"select * from
                (
	                select assisttype_group,assisttype_group +'-'+ assisttype_groupdesc as display from assucfassisttypegroup 
	                union
	                select top 1 '00', 'กรุณาเลือกลุ่มสวัสดิการ' from assucfassisttypegroup 
                )as display order by assisttype_group";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_group", "display", "assisttype_group");
        }
    }
}