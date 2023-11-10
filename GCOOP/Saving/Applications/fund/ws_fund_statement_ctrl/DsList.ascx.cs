using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_statement_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FUNDCOLLSTATEMENTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLSTATEMENT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveStatement(string ls_memno)
        {
            String sql = @"  
                    SELECT SEQ_NO,LOANCONTRACT_NO,OPERATE_DATE,ITEMPAY_AMT,BALANCE_FORWARD,BALANCE,
                    (CASE WHEN FUND_STATUS=1 THEN 'ปกติ'  else 'ยกเลิก' end) STATUSDISPLAY,
                    ENTRY_ID,ITEMTYPE_CODE
                    FROM FUNDCOLLSTATEMENT 
                    WHERE COOP_ID={0} AND MEMBER_NO={1} ORDER BY SEQ_NO";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_memno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}