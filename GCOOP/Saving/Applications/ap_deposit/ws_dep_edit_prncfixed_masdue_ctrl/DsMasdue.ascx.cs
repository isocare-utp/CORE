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
    public partial class DsMasdue : DataSourceRepeater
    {
        public DataSet1.DPDEPTMASDUEDataTable DATA { get; set; }
        public void InitDsMasdue(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASDUE;
            this.EventItemChanged = "OnDsMasdueItemChanged";
            this.EventClicked = "OnDsMasdueClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMasdue");
            this.Register();

        }
        public void Retrieve(string coop_id, string deptaccount_no)
        {
            string sql = @"  SELECT DPDEPTMASDUE.COOP_ID,   
                 DPDEPTMASDUE.DEPTACCOUNT_NO,   
                 DPDEPTMASDUE.SEQ_NO,   
                 DPDEPTMASDUE.START_DATE,   
                 DPDEPTMASDUE.END_DATE,   
                 DPDEPTMASDUE.STILL_USE,     
                 DPDEPTMASDUE.DEPTPERIOD_AMT,   
                 DPDEPTMASDUE.PEROID_COUNT,   
                 DPDEPTMASDUE.PEROID_LAST
            FROM DPDEPTMASDUE  
           WHERE ( DPDEPTMASDUE.DEPTACCOUNT_NO = {1} ) AND  
                 ( DPDEPTMASDUE.COOP_ID = {0} ) AND  
                 ( DPDEPTMASDUE.STILL_USE = 'Y' )    ";
            sql = WebUtil.SQLFormat(sql, coop_id, deptaccount_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}