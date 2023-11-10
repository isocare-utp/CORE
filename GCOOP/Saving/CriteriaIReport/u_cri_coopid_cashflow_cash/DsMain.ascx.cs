﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_cashflow_cash
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name from cmcoopmaster ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_coopid", "coop_name", "coop_id");
        }

        public void getaccountid()
        {
            String sql = @"select account_id ,(account_id||' '|| account_name ) as account_name  , 1 as sorter from accmaster where account_level = '4' union select '','',0 from dual order by sorter ,  account_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_id", "account_name", "account_id");
        }

        public void getaccountyear()
        {
            String sql = @"select  to_char(account_year + 543)  as accyear ,  1 as sorter from accaccountyear union select '',0 from dual order by sorter , accyear desc ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "year", "accyear", "accyear");
        }
    }
}