using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_rdate_billpayment
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ucfbankDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ucfbank;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void RetrieveMain()
        {
            String sql = @"select bank_code,concat(bank_code,bank_desc) as bank_desc from cmucfbank order by bank_code";
            //String sql = @"select *, from cmucfbank order by bank_code";

            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
    }
}