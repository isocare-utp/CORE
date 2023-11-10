using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_set_intarrear_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNSETINTARREARDataTable DATA { get; private set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNSETINTARREAR;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_memsearch");
            this.Button.Add("b_contsearch");
            this.Register();
        }
        
        public void DdLoanContractNo()
        {
            string sql = @"
                select loancontract_no, 1 as sorter 
                from lncontmaster 
                where coop_id = {0} and member_no = {1} 
                    and contract_status = 1
                    and principal_balance > 0
                union
                SELECT '', 0 FROM DUAL
                ORDER BY SORTER, LOANCONTRACT_NO"
            ;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO);
            this.DropDownDataBind(sql, "loancontract_no", "loancontract_no", "loancontract_no");
        }

        public void RetrieveMembNo()
        {
            string sql = @"
            select 
                coop_id,
                {2} as intarrset_docno,
                member_no,
                mbmembmaster.prename_code,
                prename_desc || memb_name || '   ' || memb_surname as cp_name                
            from mbmembmaster 
                inner join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code
            where coop_id = {0} and  member_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO, "AUTO");
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.DATA[0].INTARRSET_DATE = state.SsWorkDate;
            this.DATA[0].INTARRSET_DOCNO = "AUTO";
            this.DdLoanContractNo();
        }

        public void RetrieveLoanContractNo()
        {
            string sql = @"
                select lnm.member_no ,
                    lnm.loancontract_no ,
	                lnm.loantype_code ,
	                lnm.loantype_code || ' - ' || lt.loantype_desc as cp_loantype ,
	                principal_balance as bfprnbal_amt,
	                lastcalint_date as bflastcalint_date,
	                interest_arrear as bfintarrear_amt,
	                mpn.prename_desc || mbm.memb_name || '   ' || mbm.memb_surname as cp_name
                from lncontmaster lnm , lnloantype lt , mbmembmaster mbm , mbucfprename mpn
                where lnm.coop_id = lt.coop_id
	                and lnm.loantype_code = lt.loantype_code
	                and lnm.coop_id = mbm.coop_id
	                and lnm.member_no = mbm.member_no
	                and mbm.prename_code = mpn.prename_code
	                and lnm.coop_id = {0}
	                and lnm.loancontract_no = {1}"
            ;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].LOANCONTRACT_NO);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}