using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.ap_deposit.ws_dp_upbook_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;         
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_print");
            this.Register();
        }

        public void OfRetrieveDepAcc( string asdeptaccno)
        {
            String sql = @"SELECT DPDEPTMASTER.DEPTACCOUNT_NO,   
                                 DPDEPTMASTER.MEMBER_NO,   
                                 DPDEPTMASTER.DEPT_OBJECTIVE,   
                                 DPDEPTMASTER.DEPTPASSBOOK_NO,   
                                 DPDEPTMASTER.PRNCBAL,   
                                 DPDEPTMASTER.LASTREC_NO_PB + 1 as LASTREC_NO_PB,   
                                 DPDEPTMASTER.LASTPAGE_NO_PB,   
                                 DPDEPTMASTER.LASTLINE_NO_PB,   
                                 DPDEPTMASTER.MEMBGROUP_CODE,   
                                 DPDEPTMASTER.DEPTCLOSE_STATUS,   
                                 DPDEPTMASTER.DEPTTYPE_CODE,   
                                 DPDEPTMASTER.DEPTACCOUNT_NAME,   
                                 DPDEPTMASTER.CONDFORWITHDRAW,   
                                 DPDEPTMASTER.LASTSTMSEQ_NO,   
                                 DPDEPTMASTER.LASTPAGE_NO_CARD,   
                                 DPDEPTMASTER.LASTLINE_NO_CARD,   
                                 DPDEPTMASTER.LASTREC_NO_CARD,   
                                 DPDEPTMASTER.BOOKNORM_FLAG,   
                                 0 as prnpbkto,   
                                 0 as prn_bf,   
                                 DPDEPTMASTER.COOP_ID,   
                                 DPDEPTMASTER.MEMCOOP_ID  
                            FROM DPDEPTMASTER  
                           WHERE ( DPDEPTMASTER.COOP_ID = {0} ) AND  
                                 ( dpdeptmaster.deptaccount_no = {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, asdeptaccno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}