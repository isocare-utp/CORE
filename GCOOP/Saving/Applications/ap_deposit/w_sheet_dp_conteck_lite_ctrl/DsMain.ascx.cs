using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_conteck_lite_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChange";
            this.EventClicked = "OnDsMainClick";
            this.Register();
        }

        public int Retrieve(string coop_id, string deptmaster)
        {
            string sql1 = @"
                    SELECT
                        DPDEPTMASTER.DEPTTYPE_CODE, DPDEPTMASTER.DEPTACCOUNT_NAME, DPDEPTMASTER.WITHDRAWABLE_AMT, DPDEPTTYPE.DEPTTYPE_DESC,
                        DPDEPTMASTER.MEMBER_NO, DPDEPTMASTER.PRNCBAL, DPDEPTMASTER.ACCUINT_AMT, DPDEPTMASTER.DEPTACCOUNT_NO,
                        DPDEPTMASTER.LASTSTMSEQ_NO, DPDEPTTYPE.DEPTGROUP_CODE, DPDEPTMASTER.CHECKPEND_AMT, DPDEPTMASTER.DEPTCLOSE_STATUS,
                        DPDEPTMASTER.SPCINT_RATE_STATUS, DPDEPTMASTER.SPCINT_RATE, DPDEPTMASTER.LASTCALINT_DATE, DPDEPTMASTER.DEPTPASSBOOK_NO,
                        DPDEPTMASTER.WITH_AMT, DPDEPTMASTER.DEPT_AMT, DPDEPTMASTER.F_TAX_RATE, DPDEPTMASTER.TAXSPCRATE_STATUS,
                        DPDEPTMASTER.ACCCONT_TYPE, DPDEPTMASTER.COOP_ID, DPDEPTMASTER.SEQUEST_AMOUNT, 0 AS deptpass_flag
                    FROM    DPDEPTMASTER, DPDEPTTYPE
                    WHERE   DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE AND DPDEPTMASTER.COOP_ID = DPDEPTTYPE.COOP_ID AND
                            (DPDEPTMASTER.DEPTACCOUNT_NO = '" + deptmaster + "') AND (DPDEPTMASTER.COOP_ID = '" + coop_id + @"')
                    ";
            DataTable dt = WebUtil.Query(sql1);
            this.ImportData(dt);
            return dt.Rows.Count;
        }
    }
}