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
    public partial class DsFollow : DataSourceRepeater
    {
        public DataSet1.LNNPLFOLLOWDETAILDataTable DATA { get; private set; }

        public String OnClickAddRow { get; set; }

        public void InitDs(PageWeb pw, String id)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLFOLLOWDETAIL;
            this.EventItemChanged = "On" + UpperOne(id) + "Changed";
            this.EventClicked = "On" + UpperOne(id) + "Clicked";
            this.OnClickAddRow = "On" + UpperOne(id) + "Clicked(id, 0, 'new_row')";
            this.InitDataSource(pw, Repeater1, this.DATA, id);
            this.Button.Add("B_DEL");
            this.Register();
        }

        private String UpperOne(String text)
        {
            return text.Substring(0, 1).ToUpper() + text.Substring(1);
        }

        public void Retrieve(string member_no, decimal follow_seq, string flag)
        {
            string sql = @"
            SELECT
	            COOP_ID,
	            MEMBER_NO,
	            FOLLOW_SEQ,
	            SEQ_NO,
	            TOPIC,
	            FOLLOW_DATE,
	            FOLLOW_DESC,
	            DONE
            FROM LNNPLFOLLOWDETAIL
            WHERE
              COOP_ID = {0} AND
              MEMBER_NO = {1} AND
              FOLLOW_SEQ = {2} AND
              DONE = {3}
            ORDER BY FOLLOW_DATE DESC, SEQ_NO
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no, follow_seq, flag);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}