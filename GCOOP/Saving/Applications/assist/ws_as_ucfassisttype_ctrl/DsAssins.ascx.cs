using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class DsAssins : DataSourceRepeater
    {
        public DataSet1.INSINSURETYPEDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.INSINSURETYPE;
            this.EventItemChanged = "OnDsAssinsItemChanged";
            this.EventClicked = "OnDsAssinsClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsAssins");
            this.Register();
        }
        public void RetrieveAssins(string as_asscode)
        {
            String sql = @"select a.coop_id, a.assisttype_code, a.assisttype_ins, 1 as check_flag, b.insuretype_code+' - '+b.insuretype_desc as insure_name,b.insuretype_code
                            from assucfassisttypeinsc as a
                            join insinsuretype as b on a.assisttype_ins = b.insuretype_code and b.active_status = 1
                            where assisttype_code = 0
                            select coop_id, 0, insuretype_code, 0 , insuretype_code+' - '+insuretype_desc,insuretype_code
                            from insinsuretype as a 
                            where not exists ( select 1 from assucfassisttypeins b where b.assisttype_code = 0 and a.insuretype_code = b.assisttype_ins ) and a.active_status = 1
                            order by assisttype_ins";
            sql = WebUtil.SQLFormat(sql, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}