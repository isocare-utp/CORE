using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_mg_mrtgmast_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNMRTGMASTERDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNMRTGMASTER;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void Retrieve(string as_memno)
        {
            string ls_sql = @"select mgm.coop_id,
                mgm.mrtgmast_no,
                mgm.assettype_code,
                mgm.assettype_code + ' - ' + lua.assettype_desc as assettype_desc,
                case mgm.mortgage_type when 0 then 'แปลงเดียว' when 1 then 'หลายแปลง' when 2 then 'เฉพาะส่วน' else '' end as cp_mrtgtype
                from lnmrtgmaster mgm, lnucfassettype lua
                where mgm.assettype_code = lua.assettype_code
                and mgm.coop_id = {0}
                and mgm.member_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_memno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}