using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsFixed : DataSourceRepeater
    {
        public DataSet1.DT_FIXEDDataTable DATA { get; set; }

        public void InitDsFixed(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_FIXED;
            this.EventItemChanged = "OnDsDsFixedItemChanged";
            this.EventClicked = "OnDsDsFixedClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsFixed");
            this.Register();
        }

        public void RetrieveFixed(String ls_dept_no)
        {
            String sql = @"  SELECT DPDEPTPRNCFIXED.PRNC_NO,   
                             DPDEPTPRNCFIXED.PRNC_AMT,   
                             DPDEPTPRNCFIXED.PRNC_DATE,   
                             DPDEPTPRNCFIXED.PRNCDUE_DATE,   
                             DPDEPTPRNCFIXED.LASTCALINT_DATE,      
                             DPDEPTPRNCFIXED.FIRSTPRNC_DATE,      
                             DPDEPTPRNCFIXED.DEPTACCOUNT_NO,   
                             DPDEPTPRNCFIXED.INTARR_AMT,   
                             DPDEPTPRNCFIXED.INTPAY_AMT,   
                             DPDEPTPRNCFIXED.TAXPAY_AMT,   
                             DPDEPTPRNCFIXED.LASTACESS_DATE,   
                             '        ' as lastcalint_tdate,   
                             DPDEPTPRNCFIXED.PRNCDUE_NMONTH,      
                             '        ' as prnc_tdate,   
                             '        ' as prncdue_tdate,   
                             DPDEPTPRNCFIXED.INTEREST_RATE,   
                             DPDEPTPRNCFIXED.PRNC_BAL,   
                             DPDEPTPRNCFIXED.PRNC_BAL as prnc,   
                             DPDEPTPRNCFIXED.INT_BF_ACCYEAR,   
                             DPDEPTPRNCFIXED.COOP_ID,   
                             DPDEPTPRNCFIXED.DEPTSLIP_NO,   
                             DPDEPTPRNCFIXED.REFER_PRNC_NO,   
                             DPDEPTPRNCFIXED.PRNCFIXED_STATUS,   
                             DPDEPTPRNCFIXED.UPINT_TIME,     
                             DPDEPTPRNCFIXED.INT_REMAIN,   
                             DPDEPTPRNCFIXED.CALINT_METH,   
                             DPDEPTPRNCFIXED.INT_CUR_ACCYEAR  
                        FROM DPDEPTPRNCFIXED  
                       WHERE ( dpdeptprncfixed.deptaccount_no = {1} ) AND  
                             ( DPDEPTPRNCFIXED.COOP_ID = {0} ) AND  
                             ( DPDEPTPRNCFIXED.PRNC_BAL > 0 ) AND  
                             ( DPDEPTPRNCFIXED.PRNCFIXED_STATUS = 1 )  order by DPDEPTPRNCFIXED.PRNC_NO";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox TContract
        {
            get { return this.sum_brnbal; }
        }

        
    }
}