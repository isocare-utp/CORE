using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.account.w_acc_ucf_accmaster_ctrl
{
    public partial class wd_main : DataSourceRepeater
    {
        
        
        public DataSet1.ACCMASTERDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.ACCMASTER;
            this.EventItemChanged = "OnWdmainItemChanged";
            this.EventClicked = "OnWdmainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_main");
           
            this.Button.Add("bt_addaccid");
            this.Button.Add("bt_edit");
            this.Button.Add("bt_delete");
            this.Register();

        }
        public void Retrievedetail(string account_id)
        {
            string sql = @"select * from accmaster where account_id = '" + account_id + "'";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            
        }

        public void acctype()
        {
            string sql = @"select account_type_id , account_type_desc  from accucfacctype  order by account_type_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_type_id", "account_type_desc", "account_type_id");


        }

        public void accbooktype()
        {
            string sql = @"select account_group_id , account_group_desc  from accucfgroup  order by account_group_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_group_id", "account_group_desc", "account_group_id");


        }
        public void accountrevgroup()
        {
            string sql = @"select account_group_id , account_group_desc  from accucfgroup  order by account_group_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt ,"account_rev_group", "account_group_desc", "account_group_id");


        }
      
    }
}