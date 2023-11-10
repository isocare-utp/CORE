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
    public partial class DsBoard : DataSourceRepeater
    {
        public DataSet1.LNNPLBOARDMEETINGSUBDataTable DATA { get; private set; }
        public String OnClickAddRow { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLBOARDMEETINGSUB;
            this.EventItemChanged = "OnDsBoardChanged";
            this.EventClicked = "OnDsBoardClicked";
            this.OnClickAddRow = "OnDsBoardClicked(id, 0, 'new_row')";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsBoard");
            this.Button.Add("B_DEL");
            this.Register();
        }

        public void Retrieve(string loancontractNo)
        {
            string sql = @"
            SELECT 
              COOP_ID,
              LOANCONTRACT_NO,   
              SEQ_NO,   
              TOPIC,   
              MEET_DESC,   
              PROPOSAL,   
              SUB_DATE
            FROM LNNPLBOARDMEETINGSUB  
            WHERE COOP_ID = {0} AND LOANCONTRACT_NO = {1}
            ORDER BY SUB_DATE DESC
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}