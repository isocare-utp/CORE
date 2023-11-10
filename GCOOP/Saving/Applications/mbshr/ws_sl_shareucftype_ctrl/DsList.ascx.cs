using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_shareucftype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SHSHARETYPEDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHARETYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        /// <summary>
        /// ดึงข้อมูลประเภทหุ้น
        /// </summary>
        public void Retrieve()
        {
            string sql = "SELECT * FROM shsharetype where coop_id = '" + state.SsCoopControl + "' ORDER BY sharetype_code ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }    
}