using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.w_dlg_startday_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINCASHFLOWMASDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINCASHFLOWMAS;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            //this.EventItemChanged = "OnDsMainItemChanged";
            //this.EventClicked = "OnDsMainClicked";
            //this.Button.Add("b_retrieve");
            this.Register();
        }
        public void RetrieveData(string coop_id, DateTime work_date)
        {
            string sql = @"  SELECT FINCASHFLOWMAS.OPERATE_DATE,   
                     FINCASHFLOWMAS.ENTRY_ID,   
                     FINCASHFLOWMAS.ENTRY_DATE,   
                     FINCASHFLOWMAS.CASH_BEGIN, 
                     FINCASHFLOWMAS.CASH_BEGIN + FINCASHFLOWMAS.CHQINSAFT_BFAMT as AMOUNT_AMT,  
                     FINCASHFLOWMAS.CASH_AMT,   
                     FINCASHFLOWMAS.CASH_SUMAMT,   
                     FINCASHFLOWMAS.CASH_IN,   
                     FINCASHFLOWMAS.CASH_OUT,   
                     FINCASHFLOWMAS.CASH_FOWARD,   
                     FINCASHFLOWMAS.CLOSE_ID,   
                     FINCASHFLOWMAS.CLOSE_DATE,    
                     FINCASHFLOWMAS.CLOSE_STATUS,   
                     FINCASHFLOWMAS.CASH_DIFF,   
                     FINCASHFLOWMAS.MACHINE_ID,   
                     FINCASHFLOWMAS.ENTRY_TIME,   
                     FINCASHFLOWMAS.CLOSE_TIME,   
                     FINCASHFLOWMAS.LASTSEQ_NO + 1 as LASTSEQ_NO,   
                     FINCASHFLOWMAS.CHQINSAFE_COUNT,   
                     FINCASHFLOWMAS.CHQINSAFE_AMT,   
                     FINCASHFLOWMAS.CHQINSAFT_BFAMT,   
                     FINCASHFLOWMAS.COOP_ID  
                FROM FINCASHFLOWMAS  
               WHERE ( FINCASHFLOWMAS.COOP_ID = {0} ) AND  
                     ( FINCASHFLOWMAS.OPERATE_DATE = {1} )    ";
            sql = WebUtil.SQLFormat(sql, coop_id, work_date);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}