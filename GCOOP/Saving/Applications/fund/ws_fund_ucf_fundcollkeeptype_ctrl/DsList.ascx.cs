using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_ucf_fundcollkeeptype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FUNDCOLLKEEPTYPEDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLKEEPTYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }
        public void Retrieve()
        {
            string sql = "SELECT * FROM FUNDCOLLKEEPTYPE where COOP_ID = '" + state.SsCoopControl + "' ORDER BY SORT,FUNDKEEPTYPE ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }        
    }    
}