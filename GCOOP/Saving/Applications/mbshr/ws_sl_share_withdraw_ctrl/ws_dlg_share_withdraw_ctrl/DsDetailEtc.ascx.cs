using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl
{
    public partial class DsDetailEtc : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDET1DataTable DATA { get; set; }
        public void InitDsDetailEtc(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET1;
            this.EventItemChanged = "OnDsDetailEtcItemChanged";
            this.EventClicked = "OnDsDetailEtcClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetailEtc");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdLoanTypeEtc()
        {
            string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   
       (  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE+':'+ SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC)   as SLIPITEMTYPE_DESC, 1 as sorter
    FROM SLUCFSLIPITEMTYPE  
   WHERE ( slucfslipitemtype.itemslipetc_flag = 1 )
union
select '','', 0 from dual order by sorter, SLIPITEMTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPITEMTYPE_CODE", "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_CODE");

        }
    }
}