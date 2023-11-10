using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsFamilydecease : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsFamilydecease(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsFamilyItemChanged";
            this.EventClicked = "OnDsFamilyClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsFamilydecease");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            string sql = @"select * from assreqmaster where coop_id={0} and assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_reqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAsspaytype(String assisttype_code, ref String ls_minpaytype)
        {
            string sql = @"select
		                        assistpay_code,
		                        assistpay_desc
	                        from assucfassisttypepay
	                        where coop_id = {0} and assisttype_code = {1}
	                        order by assistpay_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, assisttype_code);
            DataTable dt = WebUtil.Query(sql);
            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code", "assistpay_desc", "assistpay_code");
        }
    }
}