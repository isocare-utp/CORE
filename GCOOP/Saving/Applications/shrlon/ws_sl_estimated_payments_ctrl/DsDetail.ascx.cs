using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.LNGENESTIMATEDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNGENESTIMATE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            //this.Button.Add("b_memsearch");
            //this.Button.Add("b_del");
            this.Register();
        }
        public void RetrieveDetail(string member_no)
        {
            string sql = @"";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public TextBox prnpay_amt
        {
            get { return this.cp_sumprnpayamt; }
        }

        public TextBox intpay_amt
        {
            get { return this.cp_sumintpayamt; }
        }

        public TextBox total_pay
        {
            get { return this.cp_sumtotalpay; }
        }
    }
}