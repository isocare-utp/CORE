using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MAINSETDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MAINSET;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
            

        }

        public void Retrieve(string depacc_no) {
            string sql = @" SELECT DPDEPTMASTER.DEPTTYPE_CODE,   
         DPDEPTMASTER.DEPTACCOUNT_NAME,   
         DPDEPTMASTER.WITHDRAWABLE_AMT,   
         DPDEPTTYPE.DEPTTYPE_DESC,   
         DPDEPTMASTER.MEMBER_NO,   
         DPDEPTMASTER.PRNCBAL,   
         DPDEPTMASTER.ACCUINT_AMT,   
         DPDEPTMASTER.DEPTACCOUNT_NO,   
         DPDEPTMASTER.LASTSTMSEQ_NO,   
         DPDEPTTYPE.DEPTGROUP_CODE,   
         DPDEPTMASTER.CHECKPEND_AMT,   
         DPDEPTMASTER.LASTCALINT_DATE,   
         DPDEPTMASTER.SPCINT_RATE_STATUS,   
         DPDEPTMASTER.SPCINT_RATE,   
         DPDEPTTYPE.DEPTTYPE_CODE,   
         DPDEPTMASTER.COOP_ID  
    FROM DPDEPTMASTER,   
         DPDEPTTYPE  
   WHERE ( DPDEPTMASTER.COOP_ID = DPDEPTTYPE.COOP_ID ) and  
         ( DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE ) and  
         ( ( DPDEPTMASTER.DEPTACCOUNT_NO = {0} ) AND  
         ( DPDEPTMASTER.COOP_ID = {1} ) )    ";
            sql = WebUtil.SQLFormat(sql, depacc_no, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}