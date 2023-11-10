using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.DsCriteriaDataTable DATA { get; set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsCriteria;
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsAssisttype");
            this.Register();
        }

        public void AssistList()
        {
            string sql = @"select
                        ASSISTTYPE_CODE, ASSISTTYPE_CODE+' : '+ASSISTTYPE_DESC as display, 1 as sorter
                        from ASSUCFASSISTTYPE
                        union
                        select '00','กรุณาเลือกสวัสดิการ',0  order by sorter, assisttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");

        }
    }
}