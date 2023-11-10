using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_deptaccountno_search_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_ListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_List;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
        public void RetrieveDeptno(String memberno)
        {
            string sql = @"
                         select t.depttype_code,t.depttype_code +' - '+t.depttype_desc as display,m.deptaccount_no,m.deptaccount_name from dpdeptmaster m left join dpdepttype t  on m.depttype_code = t.depttype_code
                         where m.coop_id={0} and m.member_no={1}  and m.deptclose_status=0
                         order by m.deptaccount_no                         
                         ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memberno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}