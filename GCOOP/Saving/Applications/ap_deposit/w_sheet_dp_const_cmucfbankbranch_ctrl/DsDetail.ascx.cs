using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_const_cmucfbankbranch_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.CMUCFBANKBRANCHDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFBANKBRANCH;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Button.Add("b_delete");
            this.Register();
        }

        /// <summary>
        /// ดึงรายละเอียด สาขาธนาคาร
        /// </summary>
        /// <param name="bank_code"></param>
        public void Retrieve(String bank_code)
        {
            string sql = @"
                SELECT  * 
            FROM CMUCFBANKBRANCH where bank_code='" + bank_code + "' order by branch_id";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}