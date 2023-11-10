using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_auditloan_history_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SYS_LOGMODTBDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SYS_LOGMODTB;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(String search)
        {
            String sql = @"select top 50 sys.modtbdoc_no , 
	            sys.entry_date , 
	            sys.clmkey_desc , 
	            lnc.member_no ,	
	            mup.prename_desc + mbm.memb_name + ' ' + mbm.memb_surname as memb_name , 
	            sys.entry_id
            from sys_logmodtb sys ,
	            lncontmaster lnc ,
	            mbmembmaster mbm , 
	            mbucfprename mup
            where mup.prename_code = mbm.prename_code
	            and lnc.coop_id = sys.coop_id
	            and lnc.loancontract_no = sys.clmkey_desc
	            and lnc.coop_id = mbm.coop_id
	            and lnc.member_no = mbm.member_no
	            and sys.coop_id = {0}
	            and sys.modtb_tbname = 'LNCONTMASTER'" + search + @" ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}