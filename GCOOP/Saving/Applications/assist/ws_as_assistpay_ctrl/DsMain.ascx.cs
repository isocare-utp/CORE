using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_assistpay_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Button.Add("b_pay");
            this.Button.Add("b_searchmem");
            this.Register();
        }

        public void DDAssisttype()
        {
            string sql = @" select 
	                        assucfassisttype.assisttype_code,   
	                        assucfassisttype.assisttype_code + ' : ' + assucfassisttype.assisttype_desc as fulltype_desc  ,
	                        1 as sorter
                        from assucfassisttype
                        where assucfassisttype.coop_id = {0}
                        union
                        select '','ทั้งหมด',0 order by sorter,assisttype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "fulltype_desc", "assisttype_code");

        }
        public void AssistPayType(string ls_asscode, ref string ls_minpaytype, ref string ls_maxpaytype)
        {
            string sql = @"select 
	                        assistpay_code, 
	                        assistpay_code + ' - ' + assistpay_desc display
                        from assucfassisttypepay 
                        where coop_id= {0} and assisttype_code = {1}
                        order by assistpay_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_asscode);
            DataTable dt = WebUtil.Query(sql);
            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
            ls_maxpaytype = dt.Rows[Convert.ToInt32(dt.Rows.Count) - 1].Field<string>("assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code1", "display", "assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code2", "display", "assistpay_code");
        }
    }
}