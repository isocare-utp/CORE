using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNREQCONTADJUSTDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUST;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_search");
            this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveMembNo()
        {
            string sql = @"
            SELECT member_no,
                MBUCFPRENAME.PRENAME_DESC,
                MEMB_NAME,
                memb_surname
            FROM MBMEMBMASTER, 
                MBUCFPRENAME                 
            WHERE MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE
                and coop_id = {0} 
                and member_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            this.DdLoanContractNo();
        }

        public void DdLoanContractNo()
        {
            string sql = @"
                select loancontract_no, 1 as sorter 
                from lncontmaster 
                where coop_id = {0} and member_no = {1} and contract_status = 1
                union
                SELECT '', 0 
                ORDER BY SORTER, LOANCONTRACT_NO"
            ;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_NO);
            this.DropDownDataBind(sql, "loancontract_no", "loancontract_no", "loancontract_no");
        }

        public void DdContlaw()
        {
            string sql = @"
            select contlaw_status,
                description_short, 1 as sorter
            from lnucfcontlaw
            union
            SELECT 0, '', 0 
            ORDER BY SORTER, contlaw_status"
            ;
            sql = WebUtil.SQLFormat(sql);
            this.DropDownDataBind(sql, "contlaw_status", "description_short", "contlaw_status");
        }
    }
}