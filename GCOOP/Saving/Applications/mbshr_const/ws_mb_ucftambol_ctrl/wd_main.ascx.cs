using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.ws_mb_ucftambol_ctrl
{
    public partial class wd_main : DataSourceFormView
    {
        public DataSet1.MBUCFTAMBOLDataTable DATA { get; set; }
        public void InitMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFTAMBOL;
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
        public void DdDistrict(string ls_province_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0 from dual ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, ls_province_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "district_code", "DISTRICT_DESC", "DISTRICT_CODE");
        }
    }
}