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
    public partial class wd_main : DataSourceRepeater
    {
        
        
        public DataSet1.ACCACCOUNTYEARDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.ACCACCOUNTYEAR;
            this.EventItemChanged = "OnWdmainItemChanged";
            this.EventClicked = "OnWdmainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_main");
            //this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"select * from ACCACCOUNTYEAR order by ACCOUNT_YEAR";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            
        }

      
    }
}