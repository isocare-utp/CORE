﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Saving.Applications.admin_const.w_sheet_am_const_cmucfmoneytype_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.CMUCFMONEYTYPEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFMONEYTYPE;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Button.Add("b_del");
            this.Register();
        }

        public void retrieve()
        {
            string sql = "select * from cmucfmoneytype order by MONEYTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}