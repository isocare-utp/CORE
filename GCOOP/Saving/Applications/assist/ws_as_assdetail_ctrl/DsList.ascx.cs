using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_assdetail_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.ASSLISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSLIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void RetrieveList(string as_memno)
        {
            String ls_sql = "";

            ls_sql = @"
                        select	ass.asscontract_no,
		                        ass.assisttype_code,
                                ass.approve_date
                        from	asscontmaster as ass
                        where   ass.coop_id = {0} and ass.member_no = {1} ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_memno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}