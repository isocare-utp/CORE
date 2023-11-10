using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.account.w_acc_ucf_accperiod_ctrl
{
    public partial class wd_list : DataSourceRepeater
    {


        public DataSet1.ACCPERIODDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.ACCPERIOD;
            this.EventItemChanged = "OnWdlistItemChanged";
            this.EventClicked = "OnWdlistClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList(string account_year )
        {
            string sql = @"select * from ACCPERIOD where account_year = '" + account_year + "' order by period";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            
        }

      
    }
}