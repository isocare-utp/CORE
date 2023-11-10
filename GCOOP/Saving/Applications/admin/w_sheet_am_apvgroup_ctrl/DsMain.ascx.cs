using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_apvgroup_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.AMSECAPVGROUPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECAPVGROUP;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Button.Add("B_DEL");
            this.Register();

        }
        public void Retrieve()
        {

            string sql = @"select * from amsecapvgroup order by apvlevel_id";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.RetrieveDD();


        }
        public void RetrieveDD()
        {
            string sql = @"select apvlevel_id,description,1 as sorter from amsecapvlevel
            union
            select '', '', 0 from dual order by sorter, apvlevel_id";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "APVLEVEL_ID", "description", "apvlevel_id");
        }
    }


}