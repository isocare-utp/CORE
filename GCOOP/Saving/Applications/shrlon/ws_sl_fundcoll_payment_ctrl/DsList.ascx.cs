using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FUNDCOLLSTATEMENTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLSTATEMENT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string memberno)
        {
            string sql = @"select 1 as operate_flag, 'จ่ายกองทุนสมาชิก ' || fundmember_no as slipitem_desc, 
                    fundbalance
                    from fundcollmaster where fundmember_no = {0}";
            sql = WebUtil.SQLFormat(sql, memberno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}