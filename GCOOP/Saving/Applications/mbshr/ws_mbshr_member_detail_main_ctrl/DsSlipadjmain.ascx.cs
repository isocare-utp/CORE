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
    public partial class DsSlipadjmain : DataSourceFormView
    {
        public DataSet1.DT_SLIPADJUSTDataTable DATA { get; set; }

        public void InitDsSlipadjmain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SLIPADJUST;
            this.EventItemChanged = "OnDsSlipadjmainItemChanged";
            this.EventClicked = "OnDsSlipadjmainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSlipadjmain");
            this.Register();
        }

        public void DdRecvperiod(string member_no) {
            string sql = @"select adjslip_no,ltrim(rtrim(ref_recvperiod)) as  ref_recvperiod ,1 as sorter from slslipadjust where member_no={0} and pmx_status=0 and coop_id={1} and slip_status=1
union
select '','',0 from dual order by sorter,ref_recvperiod";
            sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "adjslip_no", "ref_recvperiod", "adjslip_no");
        }
    }
}