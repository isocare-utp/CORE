using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.account.w_acc_ucf_group_ctrl
{
    public partial class wd_list : DataSourceRepeater
    {
        public DataSet1.ACCUCFGROUPDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.ACCUCFGROUP;
            this.EventItemChanged = "OnWdListItemChanged";
            this.EventClicked = "OnWdListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"SELECT ACCOUNT_GROUP_ID,   
      ACCOUNT_GROUP_DESC,   
         COOP_ID
    FROM ACCUCFGROUP  
  WHERE COOP_ID = {0}";
            sql = WebUtil.SQLFormat(sql,state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}