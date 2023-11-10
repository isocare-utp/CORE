using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_addnewtype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DataTable1DataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Retrieve()
        {
            string sql = @"
        SELECT  DPDEPTTYPE.DEPTTYPE_CODE , DPDEPTTYPE.TYPE_PREFIX || ' - ' || DPDEPTTYPE.DEPTTYPE_DESC as depttype_desc,
                DPUCFDEPTGROUP.DEPTGROUP_DESC  , DPUCFACCTYPEGRP.GROUP_DESC    
        FROM    DPDEPTTYPE ,  DPUCFDEPTGROUP , DPUCFACCTYPEGRP     
        WHERE   ( DPDEPTTYPE.DEPTGROUP_CODE = DPUCFDEPTGROUP.DEPTGROUP_CODE ) and  (DPDEPTTYPE.PERSONGRP_CODE = DPUCFACCTYPEGRP.PERSONGRP_CODE )
        ORDER BY DPDEPTTYPE.DEPTTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}