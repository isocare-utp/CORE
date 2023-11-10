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
    public partial class DsLoan : DataSourceFormView
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsLoan");
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(String loancontractNO)
        {
            string sql = @"
                SELECT
                  lncontmaster.coop_id,
                  LNCONTMASTER.LOANCONTRACT_NO,   
                  LNCONTMASTER.MEMBER_NO,   
                  LNCONTMASTER.LASTPAYMENT_DATE,   
                  LNCONTMASTER.PRINCIPAL_ARREAR,   
                  LNCONTMASTER.INTEREST_ARREAR,   
                  LNLOANTYPE.LOANTYPE_DESC,   
                  LNCONTMASTER.PRINCIPAL_BALANCE,   
                  LNCONTMASTER.INTEREST_ACCUM,
                  LNCONTMASTER.INTSET_ARREAR
                FROM
                  LNCONTMASTER,
                  LNLOANTYPE
                WHERE
                  lncontmaster.coop_id = lnloantype.coop_id and
                  lncontmaster.loantype_code = lnloantype.loantype_code and
                  lncontmaster.coop_id = {0} and
                  lncontmaster.loancontract_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNO);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}