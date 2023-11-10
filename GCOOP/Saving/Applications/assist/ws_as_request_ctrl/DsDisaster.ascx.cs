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
    public partial class DsDisaster : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsDisaster(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsDisasterItemChanged";
            this.EventClicked = "OnDsDisasterClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDisaster");
            this.Button.Add("b_linkaddress");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            string sql = @"select * from assreqmaster where coop_id={0} and assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_reqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

//        public void DdAsspaytype(String assisttype_code, String ls_housestatus, ref String ls_minpaytype)
//        {
//            string sql = @"select
//		                        assistpay_code,
//		                        assistpay_desc
//	                        from assucfassisttypepay
//	                        where coop_id = {0} and assisttype_code = {1}
//	                        order by assistpay_code";
//            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, assisttype_code, ls_housestatus);
//            DataTable dt = WebUtil.Query(sql);
//            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
//            this.DropDownDataBind(dt, "assistpay_code", "assistpay_desc", "assistpay_code");
//        }

        public void DdAsspaytype(String assisttype_code, String ls_housestatus, ref String ls_minpaytype)
        {
            string sql = @" select
		                        assucfassisttypepay.assistpay_code as assistpay_code,
		                        assucfassisttypepay.assistpay_desc as assistpay_desc
	                        from assucfassisttypepay , assucfhousestatus
	                        where assucfassisttypepay.assistpay_code = assucfhousestatus.assistpay_code
						    and assucfassisttypepay.coop_id = {0} and assucfassisttypepay.assisttype_code = {1}
						    and assucfhousestatus.house_status = {2}
	                        order by assucfassisttypepay.assistpay_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, assisttype_code, ls_housestatus);
            DataTable dt = WebUtil.Query(sql);
            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code", "assistpay_desc", "assistpay_code");
        }
        

        public void Disaster(ref String ls_disaster)
        {
            string sql = @"select disaster_code,disaster_desc from assucfdisaster order by disaster_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            ls_disaster = dt.Rows[0].Field<string>("disaster_code");
            this.DropDownDataBind(dt, "disaster_code", "disaster_desc", "disaster_code");
        }

        public void HouseStatus(ref String ls_housestatus)
        {
            string sql = @"select distinct house_status , house_desc from assucfhousestatus order by house_status";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            ls_housestatus = dt.Rows[0].Field<string>("house_status");
            this.DropDownDataBind(dt, "dis_house_status", "house_desc", "house_status");
        }
    }
}
