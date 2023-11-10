using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public partial class DsMort : DataSourceFormView
    {
        public DataSet1.LNNPLMORTDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLMORT;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMort");
            this.EventItemChanged = "OnDsMortItemChanged";
            this.EventClicked = "OnDsMortClicked";
            this.TableName = "LNNPLMASTER";
            this.Register();
        }

        public void Retrieve(String loancontractNO)
        {
            string sql = @"
            SELECT 
              COOP_ID,
              LOANCONTRACT_NO,   
              MEMBER_NO,   
              MORTGAGE_HALFPAYMENT  
            FROM LNNPLMASTER  
            WHERE COOP_ID = {0} AND LOANCONTRACT_NO = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNO);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}