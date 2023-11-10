using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_opr_exp_textfile_ctrl
{
    public partial class DsCbt : DataSourceFormView
    {
        public DataSet1.DT_CBTDataTable DATA { get; private set; }

        public void InitDsCbt(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CBT;
            this.EventItemChanged = "OnDsCbtItemChanged";
            this.EventClicked = "OnDsCbtClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "DsCbt");
            this.Register();
        }

        public void DdBank()
        {
            string sql = "select bank_code, bank_desc from cmucfbank order by bank_code";            
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
    }
}