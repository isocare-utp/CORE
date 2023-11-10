using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_cancelrequest_ctrl
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
            this.Button.Add("b_clear");
            this.Button.Add("b_searchmem");
            this.Register();
        }

        public void DDloantype()
        {
            string sql = @" select 
	                        assucfassisttype.assisttype_code,   
	                        assucfassisttype.assisttype_code + ' : ' + assucfassisttype.assisttype_desc as fulltype_desc  ,
	                        1 as sorter
                        from assucfassisttype
                        where assucfassisttype.coop_id = {0}
                        union
                        select '','',0  order by sorter,assisttype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "fulltype_desc", "assisttype_code");

        }
    }
}