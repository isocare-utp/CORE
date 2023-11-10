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
    public partial class wd_list : DataSourceRepeater
    {
        public DataSet1.MBUCFDISTRICTDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFDISTRICT;
            this.EventItemChanged = "OnListItemChanged";
            this.EventClicked = "OnListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
            this.Button.Add("b_delete");
            this.Register();
        }

        public void RetrieveList(string ls_province_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
             ORDER BY DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, ls_province_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}