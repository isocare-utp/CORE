using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_adjust_period_cancel_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.lnreqcontadjustdetDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.lnreqcontadjustdet;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            //this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveMemb(string member_no)
        {
            String sql = @"select mbucfprename.prename_desc ,
                mbmembmaster.member_no ,
	            mbmembmaster.memb_name ,
	            mbmembmaster.memb_surname
            from mbmembmaster ,
	            mbucfprename
            where mbmembmaster.prename_code = mbucfprename.prename_code(+)
                and mbmembmaster.coop_id = {0}
                and mbmembmaster.member_no = {1}";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
            this.DdLoanContractNo();
        }

        public void DdLoanContractNo()
        {
            string sql = @"select lnreqcontadjust.loancontract_no, 1 as sorter 
                from lnreqcontadjust , lnreqcontadjustdet
                where lnreqcontadjust.coop_id = lnreqcontadjustdet.coop_id
	                and lnreqcontadjust.contadjust_docno = lnreqcontadjustdet.contadjust_docno
                    and lnreqcontadjust.coop_id = {0} 
                    and lnreqcontadjust.member_no = {1} 
                    and lnreqcontadjust.contadjust_status = 8
                    and lnreqcontadjustdet.contadjust_code = 'PFX'
                union
                SELECT '', 0 FROM DUAL
                ORDER BY SORTER, LOANCONTRACT_NO"
            ;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO);
            this.DropDownDataBind(sql, "loancontract_no", "loancontract_no", "loancontract_no");
        }

        public void RetrieveContract(string loancontract_no)
        {
            String sql = @"select lnreqcontadjust.member_no ,
	            mbucfprename.prename_desc ,
	            mbmembmaster.memb_name ,
	            mbmembmaster.memb_surname ,
	            lnreqcontadjust.loancontract_no ,
	            lncontmaster.loanapprove_amt ,
	            lncontmaster.last_periodpay ,
	            lnreqcontadjust.bfprnbal_amt ,
	            lnreqcontadjust.bfperiod || '/' || lncontmaster.period_payamt as cp_period ,
	            lnreqcontadjustdet.coop_id ,
	            lnreqcontadjustdet.contadjust_docno ,
	            lnreqcontadjustdet.contadjust_code ,
	            lnreqcontadjustdet.oldperiod_payment ,
	            lnreqcontadjustdet.period_payment ,
	            lnreqcontadjust.int_contintrate
            from lnreqcontadjust ,
	            lnreqcontadjustdet , 
	            lncontmaster ,
	            mbmembmaster ,
	            mbucfprename
            where lnreqcontadjust.coop_id = lnreqcontadjustdet.coop_id
	            and lnreqcontadjust.contadjust_docno = lnreqcontadjustdet.contadjust_docno
	            and lnreqcontadjust.coop_id = lncontmaster.coop_id
	            and lnreqcontadjust.loancontract_no = lncontmaster.loancontract_no
	            and lnreqcontadjust.coop_id = mbmembmaster.coop_id
	            and lnreqcontadjust.member_no = mbmembmaster.member_no
	            and mbmembmaster.prename_code = mbucfprename.prename_code(+)
	            and lnreqcontadjust.coop_id = {0}
	            and lnreqcontadjust.loancontract_no = {1}
	            and lnreqcontadjust.contadjust_status = 8
	            and lnreqcontadjustdet.contadjust_code = 'PFX'";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontract_no);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
        }
    }
}