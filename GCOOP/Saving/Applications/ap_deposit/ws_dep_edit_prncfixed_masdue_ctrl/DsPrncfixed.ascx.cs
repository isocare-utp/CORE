using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl
{
    public partial class DsPrncfixed : DataSourceRepeater
    {
        public DataSet1.DPDEPTPRNCFIXEDDataTable DATA { get; set; }
        public void InitDsPrncfixed(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTPRNCFIXED;
            this.EventItemChanged = "OnDsPrncfixedItemChanged";
            this.EventClicked = "OnDsPrncfixedClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPrncfixed");
            this.Register();

        }
        public void Retrieve(string coop_id, string deptaccount_no)
        {
            string sql = @"
                SELECT DPDEPTPRNCFIXED.COOP_ID,   
                DPDEPTPRNCFIXED.DEPTACCOUNT_NO,   
                DPDEPTPRNCFIXED.PRNC_NO,   
                DPDEPTPRNCFIXED.PRNC_AMT,   
                DPDEPTPRNCFIXED.PRNC_DATE,   
                DPDEPTPRNCFIXED.PRNCDUE_DATE,   
                DPDEPTPRNCFIXED.PRNCDUE_NMONTH,   
                DPDEPTPRNCFIXED.INTARR_AMT,   
                DPDEPTPRNCFIXED.INTPAY_AMT,   
                DPDEPTPRNCFIXED.TAXPAY_AMT,   
                DPDEPTPRNCFIXED.LASTCALINT_DATE,
                DPDEPTPRNCFIXED.INTEREST_RATE,   
                DPDEPTPRNCFIXED.FIRSTPRNC_DATE,   
                DPDEPTPRNCFIXED.LASTACESS_DATE,   
                DPDEPTPRNCFIXED.PRNC_BAL,   
                DPDEPTPRNCFIXED.INT_BF_ACCYEAR,   
                DPDEPTPRNCFIXED.INT_CUR_ACCYEAR,   
                DPDEPTPRNCFIXED.CALINT_METH,   
                DPDEPTPRNCFIXED.INT_REMAIN,   
                DPDEPTPRNCFIXED.UPINT_TIME,   
                DPDEPTPRNCFIXED.PRNCFIXED_STATUS,   
                DPDEPTPRNCFIXED.REFER_PRNC_NO,   
                DPDEPTPRNCFIXED.DEPTSLIP_NO
            FROM DPDEPTPRNCFIXED  
            WHERE ( DPDEPTPRNCFIXED.COOP_ID = {0} ) AND  
            ( DPDEPTPRNCFIXED.DEPTACCOUNT_NO = {1} ) ";
            sql = WebUtil.SQLFormat(sql, coop_id, deptaccount_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}