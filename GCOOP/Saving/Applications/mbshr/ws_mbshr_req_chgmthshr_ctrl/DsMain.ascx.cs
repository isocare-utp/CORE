using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_req_chgmthshr_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SHPAYMENTADJUSTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHPAYMENTADJUST;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");

            this.Button.Add("bt_search");          

            this.Register();
        }

        public void RetrieveDocno(string docno)
        {
            string sql = @"select mbucfprename.prename_desc,   
                mbmembmaster.memb_name,   
                mbmembmaster.memb_surname,   
                mbmembmaster.membgroup_code,   
                mbucfmembgroup.membgroup_desc,   
                mbmembmaster.member_date,   
                mbmembmaster.salary_amount,   
                mbmembmaster.resign_status,   
                mbmembmaster.member_status,   
                shpaymentadjust.payadjust_docno,   
                shpaymentadjust.member_no,   
                shpaymentadjust.payadjust_date,   
                shpaymentadjust.sharebegin_value,   
                shpaymentadjust.sharestk_value,   
                shpaymentadjust.shrlast_period,   
                shpaymentadjust.periodbase_value,   
                shpaymentadjust.old_periodvalue,   
                shpaymentadjust.old_paystatus,   
                shpaymentadjust.new_periodvalue,   
                shpaymentadjust.new_paystatus,   
                shpaymentadjust.shrpayadj_status,   
                shpaymentadjust.apvimmediate_flag,   
                shpaymentadjust.remark,   
                shpaymentadjust.chgstop_flag,   
                shpaymentadjust.chgcont_flag,   
                shpaymentadjust.chgadd_flag,   
                shpaymentadjust.chglow_flag,   
                shpaymentadjust.entry_id,   
                shpaymentadjust.entry_date,   
                shpaymentadjust.approve_id,   
                shpaymentadjust.approve_date  ,
		        shpaymentadjust.coop_id,
		        shpaymentadjust.memcoop_id,
		        mbmembmaster.salary_id
            from shpaymentadjust,   
                mbmembmaster,   
                mbucfprename,   
                mbucfmembgroup  
            where ( shpaymentadjust.member_no = mbmembmaster.member_no )  
		        and ( shpaymentadjust.coop_id = mbmembmaster.coop_id )
                and ( mbucfprename.prename_code = mbmembmaster.prename_code )  
                and ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code )  
                and ( ( shpaymentadjust.coop_id = {0} )
		    and ( shpaymentadjust.payadjust_docno = {1} ) )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, docno);
            DataTable dt = WebUtil.Query(sql);
           
            this.ImportData(dt);
        }

    }
}