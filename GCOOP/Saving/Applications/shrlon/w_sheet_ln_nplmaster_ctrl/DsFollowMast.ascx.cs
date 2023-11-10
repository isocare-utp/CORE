using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public partial class DsFollowMast : DataSourceFormView
    {
        public DataSet1.LNNPLFOLLOWMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLFOLLOWMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsFollowMast");
            this.EventItemChanged = "OnDsFollowMastItemChanged";
            this.EventClicked = "OnDsFollowMasterClicked";
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(String loancontractNo)
        {
            string sql = @"
                select 
	                lnnplmaster.coop_id,
	                lnnplmaster.member_no,
	                lnnplmaster.loancontract_no,
	                lnnplmaster.follow_seq,
	                lnnplfollowmaster.description,
                    lnnplfollowmaster.description as description2,
	                lnnplfollowmaster.advance_amt
                from lnnplfollowmaster, lnnplmaster	
                where
	                lnnplmaster.coop_id = lnnplfollowmaster.coop_id (+) and
	                lnnplmaster.member_no = lnnplfollowmaster.member_no (+) and
	                lnnplmaster.follow_seq = lnnplfollowmaster.follow_seq (+) and
	                lnnplmaster.coop_id = {0} and 
	                lnnplmaster.loancontract_no = {1}
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void Retrieve(String loancontractNo, String memberNo, decimal followSeq)
        {
            string sql = @"
                select 
	                lnnplfollowmaster.coop_id,
	                lnnplfollowmaster.member_no,
                    {1} as loancontract_no,
	                lnnplfollowmaster.follow_seq,
	                lnnplfollowmaster.description,
                    lnnplfollowmaster.description as description2,
	                lnnplfollowmaster.advance_amt
                from lnnplfollowmaster
                where
	                lnnplfollowmaster.coop_id = {0} and 
	                lnnplfollowmaster.member_no = {2} and
                    lnnplfollowmaster.follow_seq = {3}
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNo, memberNo, followSeq);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdFollowSeq(String memberNo)
        {
            string sql = @"
            select follow_seq, DESCRIPTION as description,  2 as sorter from lnnplfollowmaster where coop_id={0} and member_no={1}
            union
            select 0, '' as description, 0 as sorter from dual
            union
            select 999, '(สร้างการติดตามใหม่)' as description, 1 as sorter from dual
            order by sorter, description
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "follow_seq", "DESCRIPTION", "follow_seq");
        }
    }
}