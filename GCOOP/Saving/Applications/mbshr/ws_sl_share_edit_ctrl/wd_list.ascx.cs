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
    public partial class wd_list : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnListItemChanged";
            this.EventClicked = "OnListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
            // this.Button.Add("b_memsearch");
            // this.Button.Add("b_collwho");
            this.Register();
        }

        public void RetrieveList(string ls_member_no)
        {
            string sql = @"
                SELECT SHSHARETYPE.SHARETYPE_DESC,   
                         SHSHARETYPE.SHARE_VALUE,   
                         SHSHAREMASTER.MEMBER_NO,   
                         SHSHAREMASTER.SHARETYPE_CODE,   
                         SHSHAREMASTER.SHARESTK_AMT  
                    FROM SHSHAREMASTER,   
                         SHSHARETYPE  
                   WHERE ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                         ( ( shsharemaster.member_no = {0} ) ) ";
            sql = WebUtil.SQLFormat(sql, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}