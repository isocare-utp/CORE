using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_loancont_bycoopid
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name,1 as sorter from cmcoopmaster union 
                           select '','',0 from dual order by sorter,coop_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
            this.DropDownDataBind(dt, "coop_id2", "coop_name", "coop_id");
        }

        public void DdLoancont(string member_no)
        {
            String sql = @"select loancontract_no,1 as sorter from lncontmaster where coop_id = {0} and member_no = {1} union 
                           select '',0 from dual order by sorter,loancontract_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loancont", "loancontract_no", "loancontract_no");
        }
    }
}