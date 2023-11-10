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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }
        public void RetrieveList(string sqlsearch)
        {
            String sql = @"select 
	                        dbo.ft_getmemname( ar.coop_id, ar.member_no ) as full_name,
	                        ar.assist_docno,   
	                        ar.member_no,
	                        ar.assistnet_amt,
	                        ar.req_status,
	                        ast.assisttype_desc+ ' : ' + asp.assistpay_desc assistpay_code
                        from assreqmaster ar
                        inner join assucfassisttype ast on ar.assisttype_code = ast.assisttype_code
                        inner join assucfassisttypepay asp on ar.assisttype_code = asp.assisttype_code and ar.assistpay_code = asp.assistpay_code
                        where ar.req_status = 8  and  
                        ar.coop_id = {0}
                        " + sqlsearch + @"
                        order by ar.assisttype_code, ar.assist_docno";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}