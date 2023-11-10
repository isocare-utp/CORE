using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.ws_mb_ucfdistrinct_ctrl
{
    public partial class wd_main : DataSourceFormView
    {
        public DataSet1.MBUCFDISTRICTDataTable DATA { get; set; }
        public void InitMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFDISTRICT;
            this.EventItemChanged = "OnMainItemChanged";
            this.EventClicked = "OnMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "wd_main");
            // this.Button.Add("b_search");
            this.Register();
        }
        public void DdProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE 
            union 
            select '','',0 from dual order by sorter,PROVINCE_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "province_code", "PROVINCE_DESC", "PROVINCE_CODE");
        }
    }
}