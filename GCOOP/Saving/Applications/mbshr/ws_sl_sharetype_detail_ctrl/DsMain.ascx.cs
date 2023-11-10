using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl
{
    public partial class DsMain :DataSourceFormView
    {
        public DataSet1.SHSHARETYPE1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHARETYPE1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.TableName = "SHSHARETYPE";
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"select coop_id,sharetype_code,sharetype_code+'_' +sharetype_desc  as display,1 as sorter
                from shsharetype
                union 
                select '','','',0  order by sorter,sharetype_code asc";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.DropDownDataBind(sql, "sharetype_code", "display", "sharetype_code");
        }
    }
}