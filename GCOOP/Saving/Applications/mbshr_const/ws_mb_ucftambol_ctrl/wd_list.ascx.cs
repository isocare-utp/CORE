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
    public partial class wd_list : DataSourceRepeater
    {
        public DataSet1.MBUCFTAMBOLDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFTAMBOL;
            this.EventItemChanged = "OnListItemChanged";
            this.EventClicked = "OnListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
            this.Button.Add("b_delete");
            this.Register();
        }

        public void RetrieveList(string ls_district_code)
        {
            string sql = @"
              SELECT tambol_code,   
                     district_code,   
                     tambol_desc
               FROM MBUCFTAMBOL 
              where (district_code={0}) 
             ORDER BY tambol_code ASC  ";
            sql = WebUtil.SQLFormat(sql, ls_district_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}