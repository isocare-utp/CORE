using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.w_dlg_deptno_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            //this.Button.Add("b_choose");
            this.Register();
        }
        public void RetrieveDeptno(string memberno)
        {
            string sql = @"
                         select t.depttype_code,t.depttype_code +' - '+t.depttype_desc as display,m.deptaccount_no,m.deptaccount_name from dpdeptmaster m left join dpdepttype t  on m.depttype_code = t.depttype_code
                         where m.coop_id={0} and m.member_no={1}  and m.deptclose_status=0
                         order by m.deptaccount_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memberno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}