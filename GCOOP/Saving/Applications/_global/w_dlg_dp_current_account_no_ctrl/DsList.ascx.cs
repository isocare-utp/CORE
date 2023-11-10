using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_dp_current_account_no_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMDOCUMENTCONTROLDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMDOCUMENTCONTROL;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            //this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void LastDocNo()
        {
            string sql = @"
            SELECT SUBSTR(document_code,11,12) AS document_code ,document_name,last_documentno 
            FROM   cmdocumentcontrol 
            WHERE  document_code like 'DPACCDOCNO%'and document_name != 'เลขที่บัญชีเงินฝาก'
            ORDER BY document_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}