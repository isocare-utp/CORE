using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(string mem_no)
        {
           decimal year = DateTime.Now.Year;

           String sql = @"select  
                        mbmembmaster.coop_id,
                        mbmembmaster.member_no ,
                        mbucfmembgroup.membgroup_code ,
                        mbucfmembgroup.membgroup_desc ,
                        mbmembmaster.memb_name ,
                        mbmembmaster.memb_surname ,
                        mbmembmaster.birth_date ,
                        mbmembmaster.retry_date ,
                        mbucfprename.prename_desc,
                        mbmembmaster.salary_id,
                        FT_CALAGEMTH( mbmembmaster.birth_date, {2} ) as c_age,
                        mbmembmaster.gaincond_type,
                        mbmembmaster.gaincond_desc,
                        mbmembmaster.gaincond_date,
                        mbmembmaster.write_at
                from    mbmembmaster, 
                        mbucfmembgroup,   
                        mbucfprename  
                where   mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code  and  
                        mbmembmaster.coop_id = mbucfmembgroup.coop_id  and  
                        mbmembmaster.prename_code = mbucfprename.prename_code  and  
                        mbmembmaster.coop_id = {0} and mbmembmaster.member_no ={1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, mem_no, state.SsWorkDate);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}