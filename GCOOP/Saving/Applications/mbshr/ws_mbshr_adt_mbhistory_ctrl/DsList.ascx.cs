using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_adt_mbhistory_ctrl
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
            String sql = @"  
               select  top 50  sys_logmodtb.entry_date , sys_logmodtb.clmkey_desc , sys_logmodtb.modtbdoc_no , 
                        mbucfprename.prename_desc  + mbmembmaster.memb_name + ' ' + mbmembmaster.memb_surname as memb_name , 
                        sys_logmodtb.entry_id
               from     sys_logmodtb , mbmembmaster , mbucfprename
               where    mbucfprename.prename_code = mbmembmaster.prename_code and 
                        mbmembmaster.member_no = sys_logmodtb.clmkey_desc and 
                        sys_logmodtb.coop_id = {0} " + search + "";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}