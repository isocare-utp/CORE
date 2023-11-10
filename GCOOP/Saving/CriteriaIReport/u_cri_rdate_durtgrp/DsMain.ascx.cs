using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_rdate_durtgrp
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTableDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }
        public void DdDurtgrpcode()
        {
            String sql = @"  SELECT durtgrp_code,   
                                            durtgrp_desc  
                                       FROM ptucfdurtgrpcode ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "durtgrp_code", "durtgrp_desc", "durtgrp_code");
            this.DATA[0].durtgrp_code = "001";
        }

    }
}