using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_ln_collmaster_search_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.LNCOLLMASTERCRITERIADataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTERCRITERIA;
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.Register();
        }

        public void DdCollmastTypeCode()
        {
            string sql = @"
                select
                  collmasttype_code,
                  (collmasttype_code||':'|| collmasttype_desc) as  collmast_desc,
                  1 as sorter
                from lnucfcollmasttype
                union select '', '', 0 from dual order by sorter , collmasttype_code";
            this.DropDownDataBind(sql, "collmasttype_code", "collmast_desc", "collmasttype_code");
        }
    }
}