using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_closedayprocess_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_DsListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DsList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(DateTime entry_date)
        {
            String sql = @" 
            select  dpdeptmaster.deptaccount_no, dpdeptmaster.deptaccount_name,dpdeptprncfixed.prncdue_date,dpdeptprncfixed.prncdue_date-1 as select_date,dpdeptprncfixed.prnc_bal 
            from dpdeptprncfixed inner join dpdeptmaster on dpdeptprncfixed.deptaccount_no = dpdeptmaster.deptaccount_no and  dpdeptprncfixed.coop_id = dpdeptmaster.coop_id
            inner join dpdepttype on dpdepttype.depttype_code = dpdeptmaster.depttype_code 
            where 
            dpdeptmaster.deptclose_status = 0 and dpdeptmaster.coop_id={0} and prncfixed_status=1 and
            dpdeptprncfixed.prncdue_date <={1} and dpdeptprncfixed.prnc_bal > 0 and 
            isnull(dpdepttype.upint_time,0)=1 and isnull(dpdepttype.deptgroup_code,0)='01'
            order by dpdeptprncfixed.prncdue_date,dpdeptprncfixed.deptaccount_no,dpdeptprncfixed.prnc_no"
            ;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, entry_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}