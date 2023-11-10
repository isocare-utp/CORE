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
    public partial class DsShare : DataSourceRepeater
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }
        public void InitDsShare(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDsShareItemChanged";
            this.EventClicked = "OnDsShareClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsShare");
            this.Register();
        }

        public void RetriveShare(string member_no)
        {
            string sql = @"select shsharemaster.sharetype_code,   
                 shsharetype.sharetype_desc,   
                 shsharemaster.sharetype_code,
                 shsharetype.sharetype_desc,   
                 shsharetype.unitshare_value,   
                 shsharemaster.sharebegin_amt,   
                 shsharemaster.sharestk_amt,   
                 shsharemaster.last_period,   
                 shsharemaster.periodshare_amt,   
                 shsharemaster.sharemaster_status,   
                 shsharemaster.payment_status,   
                 shsharemaster.member_no,   
                 shsharemaster.sharearrear_amt,   
                 shsharemaster.periodbase_amt,   
                 shsharemaster.paymentstop_date  
            from shsharemaster,   
                 shsharetype  
            where ( shsharemaster.sharetype_code = shsharetype.sharetype_code ) and  
                 ( shsharemaster.coop_id = shsharetype.coop_id ) and  
                 ( ( shsharemaster.coop_id = {0} ) and  
                 ( shsharemaster.member_no = {1} ) )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}