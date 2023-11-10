using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.account.w_acc_ucf_constant_ctrl
{
    public partial class wd_list : DataSourceRepeater
    {


        public DataSet1.ACCCONSTANTDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.ACCCONSTANT;
            this.EventItemChanged = "OnWdlistItemChanged";
            this.EventClicked = "OnWdlistClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_list");
           
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"select  account_id , lpad( '          ' , 5 *( account_level - 1 ) ) || ACCMASTER.ACCOUNT_ID || '  ' || ACCMASTER.ACCOUNT_NAME as account_name , account_type_id , account_group_id
                            from accmaster order by account_group_id ,   account_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt );
            
        }

        

      
    }
}