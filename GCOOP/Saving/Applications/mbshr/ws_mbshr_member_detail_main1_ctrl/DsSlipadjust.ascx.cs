using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsSlipadjust : DataSourceRepeater
    {
        public DataSet1.DT_SLIPADJUSTDETDataTable DATA { get; set; }

        public void InitDsSlipadjust(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SLIPADJUSTDET;
            this.EventItemChanged = "OnDsSlipadjustItemChanged";
            this.EventClicked = "OnDsSlipadjustClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsSlipadjust");
            this.Register();
        }

        public void RetrieveSlipadjust(string adjslip_no) {
            string sql = @"select slipitemtype_code,(case when slipitemtype_code='LON' then  loancontract_no else slipitem_desc end ) as loancontract_no ,principal_adjamt,interest_adjamt,item_adjamt
from slslipadjustdet 
where adjslip_no={0} and coop_id={1}and bfshrcont_status=1";
            sql = WebUtil.SQLFormat(sql, adjslip_no, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}