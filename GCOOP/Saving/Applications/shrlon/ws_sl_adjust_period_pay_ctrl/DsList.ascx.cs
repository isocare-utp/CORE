using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_adjust_period_pay_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.lnreqcontadjustdetDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.lnreqcontadjustdet;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string sloantype, string eloantype)
        {
            String sql = @"select lnreqcontadjust.member_no ,
	            mbucfprename.prename_desc ,
	            mbmembmaster.memb_name ,
	            mbmembmaster.memb_surname ,
	            lnreqcontadjust.loancontract_no ,
	            lncontmaster.loanapprove_amt ,
	            lncontmaster.last_periodpay ,
	            lnreqcontadjust.bfprnbal_amt ,
	            lnreqcontadjust.bfperiod ,
	            lnreqcontadjustdet.coop_id ,
	            lnreqcontadjustdet.contadjust_docno ,
	            lnreqcontadjustdet.contadjust_code ,
	            lnreqcontadjustdet.oldperiod_payamt ,
	            lnreqcontadjustdet.period_payamt ,
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
	            and lncontmaster.loantype_code between {1} and {2}
	            and lnreqcontadjust.contadjust_status = 8
	            and lnreqcontadjustdet.contadjust_code = 'PFX'";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, sloantype, eloantype);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
        }
    }
}