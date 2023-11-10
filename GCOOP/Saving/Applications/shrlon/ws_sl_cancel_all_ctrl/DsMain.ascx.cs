using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cancel_all_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(string member_no)
        {
            string sql = @"select mbucfprename.prename_desc||mbmembmaster.memb_name||' '||mbmembmaster.memb_surname as memb_name,
                                    mbmembmaster.member_no
                               from 
                                    mbmembmaster,   
                                    mbucfprename
                               where
                                    ( mbmembmaster.prename_code = mbucfprename.prename_code ) and 
                                    (mbmembmaster.member_no = {0})";

            sql = WebUtil.SQLFormat(sql,member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}