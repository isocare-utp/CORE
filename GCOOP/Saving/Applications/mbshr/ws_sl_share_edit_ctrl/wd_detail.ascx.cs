using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_share_edit_ctrl
{
    public partial class wd_detail : DataSourceFormView
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }
        public void InitDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDetailItemChanged";
            this.EventClicked = "OnDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "wd_detail");
            // this.Button.Add("b_search");
            //this.Button.Add("b_show");            

            this.Register();
        }

        public void RetrieveDetail(string ls_member_no, string ls_sharetype_code)
        {
            string sql = @"
                  SELECT SHSHARETYPE.SHARE_VALUE,
                         SHSHARETYPE.SHARETYPE_DESC,   
                         SHSHAREMASTER.*  
        
                    FROM SHSHAREMASTER,   
                         SHSHARETYPE  
                   WHERE ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                         ( ( shsharemaster.member_no = {0} ) AND  
                         ( shsharemaster.sharetype_code = {1} ) )   ";
            sql = WebUtil.SQLFormat(sql, ls_member_no, ls_sharetype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}