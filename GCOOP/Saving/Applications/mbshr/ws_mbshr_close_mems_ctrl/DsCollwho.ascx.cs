using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl
{
    public partial class DsCollwho : DataSourceRepeater
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }
        public void InitDsCollwho(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.EventItemChanged = "OnDsCollwhoItemChanged";
            this.EventClicked = "OnDsCollwhoClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCollwho");
            this.Register();
        }

        public void RetriveCollwho(string member_no)
        {
            string sql = @"select lncontcoll.loancontract_no,   
                 lncontmaster.member_no,   
                 lncontcoll.ref_collno,   
                 mbucfprename.prename_desc,   
                 mbmembmaster.memb_name,   
                 mbmembmaster.memb_surname,   
                 lncontmaster.principal_balance,   
                 lncontcoll.coll_percent, 
                 lncontcoll.collactive_percent,
                 lncontmaster.withdrawable_amt,   
                 lncontmaster.loanapprove_amt,   
                 lncontmaster.startcont_date,   
                 shsharemaster.sharestk_amt,   
                 shsharemaster.sharearrear_amt,   
                 shsharetype.unitshare_value  
            from lncontcoll,   
                 lncontmaster,   
                 mbmembmaster,   
                 mbucfprename,   
                 shsharemaster,   
                 shsharetype  
            where ( lncontmaster.loancontract_no = lncontcoll.loancontract_no )  
                 and ( mbmembmaster.member_no = lncontmaster.member_no )  
                 and ( mbmembmaster.prename_code = mbucfprename.prename_code )  
                 and ( mbmembmaster.member_no = shsharemaster.member_no )  
                 and ( shsharemaster.sharetype_code = shsharetype.sharetype_code )  
                 and ( lncontcoll.coop_id = lncontmaster.coop_id )  
                 and ( lncontmaster.memcoop_id = mbmembmaster.coop_id )  
                 and ( shsharemaster.coop_id = shsharetype.coop_id )  
                 and ( ( lncontcoll.loancolltype_code in ( '01', '05' ) )  
                 and ( lncontcoll.coop_id = {0} )
                 and ( lncontcoll.ref_collno = {1} )  
                 and ( lncontmaster.contract_status > 0 )  
                 and ( lncontcoll.coll_status = 1 ) )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}