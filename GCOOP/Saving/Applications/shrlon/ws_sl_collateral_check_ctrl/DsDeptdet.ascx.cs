using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class DsDeptdet : DataSourceFormView
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; set; }

        public void InitDsDeptdet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;
            this.EventItemChanged = "OnDsDeptdetItemChanged";
            this.EventClicked = "OnDsDeptdetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDeptdet");

            this.Register();
        }
        public void RetrieveDsSharedet(String deptaccount_no)
        {
            String sql = @"  SELECT DPDEPTMASTER.DEPTACCOUNT_NO,   
                                     DPDEPTMASTER.DEPTTYPE_CODE,   
                                     DPDEPTTYPE.DEPTTYPE_DESC,   
                                     DPDEPTMASTER.MEMBER_NO,   
                                     DPDEPTMASTER.DEPTOPEN_DATE,   
                                     DPDEPTMASTER.DEPTACCOUNT_NAME,   
                                     DPDEPTMASTER.BEGINBAL,   
                                     DPDEPTMASTER.PRNCBAL,   
                                     DPDEPTMASTER.WITHDRAWABLE_AMT,   
                                     DPDEPTMASTER.SEQUEST_AMOUNT,   
                                     DPDEPTMASTER.DEPTCLOSE_DATE,   
                                     DPDEPTMASTER.DEPTCLOSE_STATUS  
                                FROM DPDEPTMASTER,   
                                     DPDEPTTYPE  
                               WHERE ( DPDEPTTYPE.DEPTTYPE_CODE = DPDEPTMASTER.DEPTTYPE_CODE ) and  
                                     ( DPDEPTMASTER.COOP_ID = DPDEPTTYPE.COOP_ID ) and  
                                     ( ( dpdeptmaster.deptaccount_no = {0} ) ) ";

            sql = WebUtil.SQLFormat(sql, deptaccount_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}