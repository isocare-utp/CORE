using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_reprint_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Button.Add("b_cancel");
            
            this.Register();
        }

       
        public void memgroup()
        {
            string sql = @"
             SELECT MEMBGROUP_CODE,
                    MEMBGROUP_CODE + ' ' + MEMBGROUP_DESC  AS MEMBGROUP_DISPLAY
                 
             FROM MBUCFMEMBGROUP 
            union
            select ' ',' 'from dual ORDER BY MEMBGROUP_CODE ASC
";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MEMBGROUP_DESC", "MEMBGROUP_DISPLAY", "MEMBGROUP_CODE");

        }
    }
}