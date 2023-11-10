using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_auditloan_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_memsearch");
            this.Register();
        }

        public void RetrieveMain(string member_no)
        {
            string sql = @"select mbmembmaster.member_no,   
                mbmembmaster.memb_name,   
                mbmembmaster.memb_surname,   
                mbmembmaster.membgroup_code,   
                mbmembmaster.member_date,   
                mbucfprename.prename_desc,   
                mbucfmembgroup.membgroup_desc,
                mbmembmaster.membgroup_code + ' - ' + mbucfmembgroup.membgroup_desc as cp_membgroup,
                shsharemaster.sharestk_amt,   
                shsharetype.share_value,   
                mbmembmaster.member_status,   
                mbmembmaster.salary_amount  
            from mbmembmaster,   
                mbucfmembgroup,   
                mbucfprename,   
                shsharemaster,   
                shsharetype  
            where ( mbmembmaster.coop_id = mbucfmembgroup.coop_id )
                and ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code )  
                and ( mbmembmaster.prename_code = mbucfprename.prename_code )  
                and ( mbmembmaster.coop_id = shsharemaster.coop_id )
                and ( mbmembmaster.member_no = shsharemaster.member_no )  
                and ( shsharemaster.coop_id = shsharetype.coop_id ) 
                and ( shsharemaster.sharetype_code = shsharetype.sharetype_code )  
                and ( ( mbmembmaster.coop_id = {0} )
                and ( mbmembmaster.member_no = {1} ) )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}