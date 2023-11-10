using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void RetrieveMember(String member_no)
        {
            String sql = @"SELECT m.member_no , p.prename_desc || m.memb_name || '  ' || m.memb_surname as member_name,
                                  m.membtype_code || ' - ' || t.membtype_desc as member_typet, m.salary_id,
                                  m.membgroup_code || ' - ' || g.membgroup_desc as member_group, m.member_status
                           FROM   mbmembmaster m, mbucfprename p , mbucfmembtype t , mbucfmembgroup g
                           WHERE  m.coop_id = t.coop_id and m.coop_id = g.coop_id and 
                                  m.membgroup_code = g.membgroup_code and m.membtype_code = t.membtype_code and
                                  m.prename_code = p.prename_code and m.member_no = '" + member_no + "' ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

    }
}