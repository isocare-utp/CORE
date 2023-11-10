using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmembgrptype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBUCFMEMBGRPTYPEDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGRPTYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"select 
                            trim(membgrptype_code) as membgrptype_code,
                            membgrptype_desc
                            from mbucfmembgrptype 
                            order by membgrptype_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}