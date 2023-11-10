using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfdeptaccgroup_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPUCFDEPTACCGROUPDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFDEPTACCGROUP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        /// <summary>
        /// ดึงข้อมูลกลุ่มประเภทเงินฝาก
        /// </summary>
        public void Retrieve()
        {
            string sql = "SELECT * FROM DPUCFDEPTACCGROUP where COOP_ID = '" + state.SsCoopControl + "' ORDER BY DEPTTYPE_ACCGRP ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}