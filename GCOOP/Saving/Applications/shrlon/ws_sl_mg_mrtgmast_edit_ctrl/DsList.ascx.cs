﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string as_memno)
        {
            string ls_sql = @"select mgm.coop_id,
                mgm.mrtgmast_no,
                mgm.assettype_code,
                mgm.assettype_code || ' - ' || lua.assettype_desc as assettype_desc,
                case mgm.mortgage_type when 0 then 'แปลงเดียว' when 1 then 'เฉพาะส่วน' when 2 then 'หลายแปลง' else '' end as cp_mrtgtype
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