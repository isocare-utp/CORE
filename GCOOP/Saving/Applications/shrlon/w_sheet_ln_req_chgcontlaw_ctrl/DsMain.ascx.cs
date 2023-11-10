using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_req_chgcontlaw_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LCREQCHGCONTLAW2DataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LCREQCHGCONTLAW2;
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
                select loancontract_no, 1 as sorter from Lccontmaster where branch_id={0} and member_no = {1}
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
            SELECT 
                BRANCH_ID,
                {2} as INTARRSET_DOCNO,
                MEMBER_NO,
                MBMEMBMASTER.PRENAME_CODE,
                PRENAME_DESC,
                MEMB_NAME,
                SUFFNAME_DESC
            FROM MBMEMBMASTER 
                INNER JOIN MBUCFPRENAME ON MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE
            WHERE BRANCH_ID = {0} AND  MEMBER_NO = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO, "AUTO");
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.DATA[0].LNCHGCONTLAW_DATE = state.SsWorkDate;
            this.DATA[0].LNCHGCONTLAW_DOCNO = "AUTO";
            this.DdLoanContractNo();
        }
    }
}