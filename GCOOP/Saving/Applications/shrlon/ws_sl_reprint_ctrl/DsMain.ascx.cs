using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_reprint_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_retrieve");//b_print
            this.Register();
        }

        public void DdCode()
        {
            string sql = @"select sliptype_code,sliptype_code +' ' + sliptype_desc as sliptype 
                           from slucfsliptype where sliptype_group = 'PAY' 
                           union
                           select '','' from dual";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "sliptype_code", "sliptype", "sliptype_code");
        }
    }
}