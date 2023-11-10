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
    public partial class DsColl : DataSourceRepeater
    {
        public DataSet1.LNNPLCOLLMASTDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLCOLLMAST;
            this.EventItemChanged = "OnDsCollItemChanged";
            this.EventClicked = "OnDsCollClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsColl");
            this.TableName = "LNNPLCOLLMAST";
            this.Register();
        }

        public void Retrieve(string loancontractNo)
        {
            string sql = @"
                SELECT 
                    LNCONTCOLL.COOP_ID,
	                LNCONTCOLL.LOANCONTRACT_NO,
	                LNCONTCOLL.DESCRIPTION,
	                LNCONTCOLL.REF_COLLNO,

	                LNCOLLMASTER.COLLMAST_NO,
	                LNCOLLMASTER.COLLMAST_DESC,

	                LNCOLLMASTER.ESTIMATE_PRICE,
	                LNCOLLMASTER.MORTGAGE_DATE,
	                LNCOLLMASTER.MORTGAGE_PRICE,

	                LNNPLCOLLMAST.JPK_ESTPRICE,
	                LNNPLCOLLMAST.CONFISCATION,
	                LNNPLCOLLMAST.OBSCACLES,
	                LNNPLCOLLMAST.RESPONSIBLE,
	                LNNPLCOLLMAST.PRINTING,
	                LNNPLCOLLMAST.RECEIVED_DATE,
	                LNNPLCOLLMAST.DEBTOR_CLASS,
	                LNNPLCOLLMAST.WORK_ORDER,
	                LNNPLCOLLMAST.STATUS,
	                LNNPLCOLLMAST.SALE_DATE,
	                LNNPLCOLLMAST.SALE_PRICE,
	                LNNPLCOLLMAST.CONFISCATION_DATE,
	                LNNPLCOLLMAST.WITHDRAW_JUDGEDOC
                FROM
	                LNCONTCOLL,
	                LNCOLLMASTER,
	                LNNPLCOLLMAST
                WHERE
	                ( lncontcoll.coop_id = lncollmaster.coop_id (+)) and
	                ( lncontcoll.coop_id = LNNPLCOLLMAST.coop_id (+)) and
	                ( lncontcoll.ref_collno = lncollmaster.collmast_no (+)) and
	                ( lncontcoll.ref_collno = lnnplcollmast.ref_collno (+)) and
	                lncontcoll.coop_id = {0} and
	                LNCONTCOLL.LOANCONTRACT_NO = {1}
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}