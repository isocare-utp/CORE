using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_detail_day_ctrl
{
    public partial class DsMethodpayment : DataSourceRepeater
    {
        public DataSet1.DT_METHODPAYMENTDataTable DATA { get; set; }
        public void InitMethodpayment(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_METHODPAYMENT;
            this.EventItemChanged = "OnDsMethodpaymentItemChanged";
            this.EventClicked = "OnDsMethodpaymentClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMethodpayment");
            this.Register();
        }

        public void RetrieveMethodpayment(String ls_member_no, String ls_year)
        {
            string sql = @"select  yrdivmethpay.coop_id ,           
	            yrdivmethpay.member_no ,           
	            yrdivmethpay.seq_no ,           
	            yrdivmethpay.div_year ,           
	            yrdivmethpay.methpaytype_code ,    
	            yrucfmethpay.methpaytype_desc,         
	            yrdivmethpay.moneytype_code ,        
	            yrdivmethpay.expense_bank ,           
	            yrdivmethpay.expense_accid ,           
	            yrdivmethpay.expense_branch ,           
	            yrdivmethpay.bizzcoop_id ,           
	            yrdivmethpay.bizztype_code ,           
	            yrdivmethpay.bizzaccount_no ,           
	            yrdivmethpay.div_amt ,           
	            yrdivmethpay.avg_amt ,           
	            yrdivmethpay.expense_amt ,           
	            yrdivmethpay.ref_slipcoop ,           
	            yrdivmethpay.ref_slipno ,           
	            yrdivmethpay.ref_reqdoccoop ,           
	            yrdivmethpay.ref_reqdocno ,           
	            yrdivmethpay.methpay_status ,           
	            yrdivmethpay.etc_amt,
                cmucfbankbranch.branch_name
            from yrdivmethpay ,
	            yrucfmethpay ,
			    cmucfbankbranch
            where ( yrdivmethpay.methpaytype_code = yrucfmethpay.methpaytype_code )
                and ( yrdivmethpay.expense_bank = cmucfbankbranch.bank_code (+) )
			    and ( yrdivmethpay.expense_branch = cmucfbankbranch.branch_id  (+) )
	            and ( yrdivmethpay.coop_id = {0} )
	            and ( yrdivmethpay.member_no = {1} )
	            and ( yrdivmethpay.div_year = {2} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no, ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdMethpaytype()
        {
            string sql = @"
            select methpaytype_code,   
                methpaytype_desc,   
                methpaytype_sort ,1 as sorter
            from yrucfmethpay  
            where coop_id = {0}
                and showlist_flag = 1   
            union select '','',0,0 from dual order by sorter, methpaytype_code asc";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "methpaytype_code", "METHPAYTYPE_DESC", "METHPAYTYPE_CODE");
        }
    }
}