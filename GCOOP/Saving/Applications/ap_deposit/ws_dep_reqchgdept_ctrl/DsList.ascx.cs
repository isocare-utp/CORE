using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPREQCHG_DEPTDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPREQCHG_DEPT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        
        public void RetriveData(string coop_control,string dept_no)
        {
            string sql = @"  SELECT DPREQCHG_DEPT.DEPTACCOUNT_NO ,     DPREQCHG_DEPT.DEPTMONTCHG_DATE ,   DPREQCHG_DEPT.DEPTMONTH_OLDAMT ,    
            DPREQCHG_DEPT.DEPTMONTH_NEWAMT , DPREQCHG_DEPT.ENTRY_DATE ,     DPREQCHG_DEPT.ENTRY_ID ,        
            DPREQCHG_DEPT.REMARK ,           DPREQCHG_DEPT.DPREQCHG_DOC     FROM DPREQCHG_DEPT     
            WHERE ( DPREQCHG_DEPT.DEPTACCOUNT_NO ={1} AND DPREQCHG_DEPT.COOP_ID ={0} )
            ORDER BY DPREQCHG_DEPT.ENTRY_DATE,DPREQCHG_DEPT.DPREQCHG_DOC DESC";
            sql = WebUtil.SQLFormat(sql,coop_control,dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}