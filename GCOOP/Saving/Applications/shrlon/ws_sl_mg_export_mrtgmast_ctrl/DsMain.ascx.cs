using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_export_mrtgmast_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_retrieve");
            this.Register();
        }

        public void DdAssettype()
        {
            string sql = @" 
                SELECT assettype_code,   
                assettype_desc,   
                1 as sorter  
                FROM lnucfassettype
                union
                select '','',0 from dual order by sorter, assettype_code";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assettype_code", "assettype_desc", "assettype_code");
        }
    }
}