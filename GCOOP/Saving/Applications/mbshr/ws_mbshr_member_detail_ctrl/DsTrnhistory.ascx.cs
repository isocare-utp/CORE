using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsTrnhistory : DataSourceRepeater
    {
        public DataSet1.DT_TRNHISTORYDataTable DATA { get; set; }

        public void InitDsTrnhistory(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_TRNHISTORY;
            this.EventItemChanged = "OnDsTrnhistoryItemChanged";
            this.EventClicked = "OnDsTrnhistoryClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsTrnhistory");
            this.Register();
        }

        public void RetrieveTrnhistory(String ls_member_no)
        {
            String sql = @"  select
                                    memold_no,
                                    memnew_no,
                                    apv_date,
                                    apv_id
                                    from 
                                    mbreqtranmb
                                    where
                                    ( trnmbreq_status = 1) and
                                    ( coop_id = {0} )and
                                    ((memold_no={1}) or ( memnew_no = {1} ) )
                                    order by trnmbreq_docno desc";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}