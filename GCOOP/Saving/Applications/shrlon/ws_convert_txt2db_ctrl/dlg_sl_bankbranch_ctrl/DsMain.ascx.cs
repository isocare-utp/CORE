using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl.dlg_sl_bankbranch_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.ucfbankDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ucfbank;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }

        public void RetrieveMain()
        {
            String sql = @"select account_id as bank_code,account_name as bank_desc from accmaster 
            where account_level = 4 and account_name like '%Bill Payment%' order by account_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}