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
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.SYS_LOGMODTBDETDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SYS_LOGMODTBDET;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Button.Add("btn_detail");
            this.Register();
        }
        public void RetrieveDetail(String doc_no)
        {
            String sql = @"  
                select  sys_logmodtbdet.seq_no , sys_logmodtb.modtb_code ,sys_logmodtbdet.clm_name , 
                        sys_logmodtbdet.clmold_desc , sys_logmodtbdet.clmnew_desc
                from    sys_logmodtbdet , sys_logmodtb
                where   sys_logmodtbdet.coop_id = {0} and
                        sys_logmodtbdet.modtbdoc_no = sys_logmodtb.modtbdoc_no and
                        sys_logmodtbdet.coop_id = sys_logmodtb.coop_id and
                        sys_logmodtb.modtbdoc_no = {1}    
                order by sys_logmodtbdet.seq_no";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, doc_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}