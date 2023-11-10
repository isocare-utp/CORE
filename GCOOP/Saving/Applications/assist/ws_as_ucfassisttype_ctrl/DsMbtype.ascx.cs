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
    public partial class DsMbtype : DataSourceRepeater
    {
        public DataSet1.ASSUCFASSISTTYPEMBTYPEDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPEMBTYPE;
            this.EventItemChanged = "OnDsMbtypeItemChanged";
            this.EventClicked = "OnDsMbtypeClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMbtype");
            this.Register();
        }

        public void RetrieveMembtype(string as_asscode)
        {
            String sql = @"select a.coop_id, a.assisttype_code, a.membtype_code, 1 as check_flag, b.membcat_code+' - '+b.membcat_desc as mbtype_name
                            from assucfassisttypembtype as a
                                join mbucfcategory as b on a.membtype_code = b.membcat_code
                            where assisttype_code = 0
                            union
                            select coop_id, {0}, membcat_code, 0 , membcat_code+' - '+membcat_desc
                            from mbucfcategoryas as a 
                            where not exists ( select 1 from assucfassisttypembtype as b where b.assisttype_code = 0 and a.membcat_code = b.membtype_code )
                            order by membtype_code";
            sql = WebUtil.SQLFormat(sql, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}