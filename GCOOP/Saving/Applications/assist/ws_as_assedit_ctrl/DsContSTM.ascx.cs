using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_assedit_ctrl
{
    public partial class DsContSTM : DataSourceRepeater
    {
        public DataSet1.ASSCONTSTATEMENTDataTable DATA { get; private set; }
        public void InitDsContSTM(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSCONTSTATEMENT;
            this.EventItemChanged = "OnDsContSTMItemChanged";
            this.EventClicked = "OnDsContSTMClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsContSTM");
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveData(string as_asscontno)
        {
            string sql = @"select
                                astm.*
                           from asscontstatement astm
                           where astm.coop_id={0} and astm.asscontract_no ={1}
                           order by astm.seq_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_asscontno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAssItemCode()
        {
            string ls_sql;
            ls_sql = "select item_code, item_code||':'||item_desc as item_desc from assucfassitemcode ";
            DataTable dt = WebUtil.Query(ls_sql);
            this.DropDownDataBind(dt, "item_code", "item_desc", "item_code");
        }
    }
}