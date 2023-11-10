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
    public partial class DsAssCut : DataSourceRepeater
    {
        public DataSet1.ASSUCFASSISTTYPECUTDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPECUT;
            this.EventItemChanged = "OnDsAssCutItemChanged";
            this.EventClicked = "OnDsAssCutClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsAssCut");
            this.Register();
        }

        public void RetrieveAssCut(string as_asscode)
        {
            String sql = @"select a.coop_id, a.assisttype_code, a.assisttype_cut, 1 as check_flag, b.assisttype_code +' - '+ b.assisttype_desc as asstypedesc
                            from assucfassisttypecut as a
                            join assucfassisttype as b on a.assisttype_cut = b.assisttype_code
                            where a.assisttype_code = 0
                            union
                            select coop_id, 0, assisttype_code, 0 , assisttype_code+' - '+assisttype_desc
                            from assucfassisttype as a 
                            where not exists ( select 1 from assucfassisttypecut as b where b.assisttype_code = 0 and a.assisttype_code = b.assisttype_cut )
                            order by assisttype_cut";
            sql = WebUtil.SQLFormat(sql, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}