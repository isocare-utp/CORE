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
    public partial class DsNPL : DataSourceFormView
    {
        public DataSet1.LNNPLMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsNPL");
            this.EventItemChanged = "OnDsNPLItemChanged";
            this.EventClicked = "OnDsNPLClicked";
            this.Button.Add("b_getdata");
            this.Register();
        }

        public void Retrieve(String loancontract_no)
        {
            string sql = @"
                SELECT
                  LNNPLMASTER.COOP_ID,
                  LNNPLMASTER.LOANCONTRACT_NO,   
                  LNNPLMASTER.MEMBER_NO,   
                  LNNPLMASTER.LAWTYPE_CODE,   
                  LNNPLMASTER.COURT_NAME,   
                  LNNPLMASTER.INDICT_DATE,   
                  LNNPLMASTER.CASE_BLACKNO,   
                  LNNPLMASTER.CASE_REDNO,   
                  LNNPLMASTER.JUDGE_DATE,   
                  LNNPLMASTER.JUDGE_DESC,   
                  LNNPLMASTER.ENFORCEMENT_DATE,   
                  LNNPLMASTER.JUDGE_INTRATE,   
                  LNNPLMASTER.ADVANCE_PAYAMT,   
                  LNNPLMASTER.INDICT_PRNAMT,   
                  LNNPLMASTER.INDICT_INTAMT,   
                  LNNPLMASTER.RECIEVEABLE_BYCOURT,   
                  LNNPLMASTER.MARGIN,   
                  LNNPLMASTER.RESOLUTION,   
                  LNNPLMASTER.LAST_INT_DATE,   
                  LNNPLMASTER.INT_LASTCAL,   
                  LNNPLMASTER.PERIOD_PAYMENT,   
                  LNNPLMASTER.PRINC_BALANCE,   
                  LNNPLMASTER.PRINCE_LAST_YEAR,   
                  LNNPLMASTER.PRINC_PAYMENT_YEAR,   
                  LNNPLMASTER.INT_BALANCE,   
                  LNNPLMASTER.INT_LAST_YEAR,   
                  LNNPLMASTER.INT_PAYMENT_YEAR,   
                  LNNPLMASTER.PAYMENT_SUM,   
                  LNNPLMASTER.RESULT_REQUIRE,   
                  LNNPLMASTER.REMARK,   
                  LNNPLMASTER.WORK_ORDER,   
                  LNNPLMASTER.RECEIVED_DATE,   
                  LNNPLMASTER.DEBTOR_CLASS,   
                  LNNPLMASTER.STATUS,   
                  LNCONTMASTER.PRINCIPAL_BALANCE,
                  LNCONTMASTER.INTEREST_ACCUM,
                  LNCONTMASTER.INTEREST_ARREAR,
                  LNCONTMASTER.LASTCALINT_DATE, 
                  LNNPLMASTER.LOANCONTRACT_DATE,   
                  LNNPLMASTER.LAWTYPE_CODE_OLD,   
                  LNNPLMASTER.PERCENT_SETTINGPAYMENT,   
                  LNNPLMASTER.SETTING_PAYMENT,
                  LNNPLMASTER.FOLLOW_SEQ
                FROM 
                  LNNPLMASTER,   
                  LNCONTMASTER
                WHERE 
                  lnnplmaster.loancontract_no = lncontmaster.loancontract_no and  
                  lnnplmaster.coop_id = lncontmaster.coop_id and
                  lnnplmaster.coop_id = {0} and
                  LNNPLMASTER.LOANCONTRACT_NO = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontract_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdLAWTYPE_CODE()
        {
            string sql = "SELECT LAWTYPE_CODE, LAWTYPE_DESC FROM LNUCFNPLLAWTYPE where coop_id = {0} order by LAWTYPE_CODE";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "LAWTYPE_CODE", "LAWTYPE_DESC", "LAWTYPE_CODE");
        }

        public void DdLAWTYPE_CODE_OLD()
        {
            string sql = "SELECT LAWTYPE_CODE, LAWTYPE_DESC FROM LNUCFNPLLAWTYPE where coop_id = {0} order by LAWTYPE_CODE";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "lawtype_code_old", "LAWTYPE_DESC", "LAWTYPE_CODE");
        }

        public void DdWork_order()
        {
            string sql = @"
            SELECT 
                STEP,   
                STEP || ' - ' || DESCRIPTION as DESCRIPTION,
                1 as sorter
            FROM LNUCFNPLWORKORDER where coop_id = {0}
            union
            select '', '', 0 from dual order by sorter, step";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "work_order", "DESCRIPTION", "STEP");
        }
    }
}