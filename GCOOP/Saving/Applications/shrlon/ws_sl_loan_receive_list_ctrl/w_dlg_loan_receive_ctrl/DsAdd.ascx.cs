using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl.w_dlg_loan_receive_ctrl
{
    public partial class DsAdd : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }
        public void InitDsAdd(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;
            this.EventItemChanged = "OnDsAddItemChanged";
            this.EventClicked = "OnDsAddClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsAdd");
            this.Button.Add("b_del");
            this.Register();
        }
        public void DdLoanType()
        {
            string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   
       (  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE + ' : '+  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC)   as SLIPITEMTYPE_DESC, 1 as sorter
    FROM SLUCFSLIPITEMTYPE  
   WHERE ( slucfslipitemtype.manual_flag = 1 )
union
select '','', 0 from dual order by sorter, SLIPITEMTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPITEMTYPE_CODE", "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_CODE");

        }

        public void DdLoanTypeEtc()
        {
            string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   
       (  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE +' : '+ SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC)   as SLIPITEMTYPE_DESC, 1 as sorter
    FROM SLUCFSLIPITEMTYPE  
   WHERE ( slucfslipitemtype.itemslipetc_flag = 1 or slucfslipitemtype.itemlnreqclr_flag = 1 )
union
select '','', 0 from dual order by sorter, SLIPITEMTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPITEMTYPE_CODE", "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_CODE");

        }

        public void ItemType(string slipitemtype_code)
        {
            string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC
                 FROM SLUCFSLIPITEMTYPE  
                 WHERE ( slucfslipitemtype.manual_flag = 1 ) and  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE={0}

            ";
            sql = WebUtil.SQLFormat(sql, slipitemtype_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_CODE");

        }
    }
}