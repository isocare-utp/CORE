using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.SHSHARETYPEDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHARETYPE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(string sharetype)
        {
            String sql = @"select coop_id,
                             sharetype_code,   
                             share_value,   
                             minshare_hold,   
                             maxshare_hold,   
                             minshare_low,   
                             minshare_stop,   
                             countperiod_status,   
                             maxmiss_pay,   
                             callonprms_status,   
                             sharegroup_code,   
                             timeminshare_stop,   
                             timeminshare_low,   
                             minshare_pay,   
                             buyshare_before,   
                             chgcount_type,   
                             chgcountall_amt,   
                             chgcountadd_amt,   
                             chgcountlow_amt,   
                             chgcountstop_amt,   
                             chgcountcont_amt,
                             adjsalarychgshrperiod_flag     
                           from shsharetype 
where sharetype_code={0}";
            sql = WebUtil.SQLFormat(sql, sharetype);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    
    }
}