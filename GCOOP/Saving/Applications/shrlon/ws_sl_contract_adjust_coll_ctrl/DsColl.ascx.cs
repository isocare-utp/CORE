using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_coll_ctrl
{
    public partial class DsColl : DataSourceRepeater
    {
        public DataSet1.LNREQCONTADJUSTCOLLDataTable DATA { get; private set; }

        public void InitDsColl(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUSTCOLL;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsColl");
            this.EventItemChanged = "OnDsCollItemChanged";
            this.EventClicked = "OnDsCollClicked";
            this.Button.Add("b_del");
            this.Button.Add("b_show");
            this.Button.Add("b_search");
            this.Register();
        }

        public void DdLnCollType()
        {
            string sql = @"SELECT loancolltype_code ,
                loancolltype_desc ,
                1 as sorter
            FROM LNUCFLOANCOLLTYPE
            union select '','',0 from dual order by sorter, loancolltype_code";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loancolltype_code", "loancolltype_desc", "loancolltype_code");
        }

        public TextBox collactive_amt
        {
            get { return this.sum_collactive_amt; }
        }

        public TextBox collactive_percent
        {
            get { return this.sum_collactive_percent; }
        }
    }
}