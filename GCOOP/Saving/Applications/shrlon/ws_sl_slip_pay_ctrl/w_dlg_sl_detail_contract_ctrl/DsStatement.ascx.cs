using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsStatement : DataSourceRepeater
    {
        public DataSet1.DT_STATEMENTDataTable DATA { get; set; }

        public void InitDsStatement(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_STATEMENT;
            this.EventItemChanged = "OnDsStatementItemChanged";
            this.EventClicked = "OnDsStatementClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsStatement");
            this.Register();
        }
        public void RetrieveStatement(String ls_docno)
        {
            String sql = @"  
                      SELECT LNCONTSTATEMENT.LOANCONTRACT_NO,   
                             LNCONTSTATEMENT.SEQ_NO,   
                             LNCONTSTATEMENT.LOANITEMTYPE_CODE,   
                             LNCONTSTATEMENT.OPERATE_DATE,   
                             LNCONTSTATEMENT.REF_DOCNO,   
                             LNCONTSTATEMENT.PERIOD,   
                             LNCONTSTATEMENT.PRINCIPAL_PAYMENT,   
                             LNCONTSTATEMENT.INTEREST_PAYMENT,   
                             LNCONTSTATEMENT.PRINCIPAL_BALANCE,   
                             LNCONTSTATEMENT.CALINT_FROM,   
                             LNCONTSTATEMENT.CALINT_TO,   
                             LNCONTSTATEMENT.INTEREST_PERIOD,   
                             LNCONTSTATEMENT.INTEREST_ARREAR,   
                             LNCONTSTATEMENT.INTEREST_RETURN,   
                             LNCONTSTATEMENT.ENTRY_ID,   
                             LNCONTSTATEMENT.ENTRY_DATE,   
                             LNCONTSTATEMENT.MONEYTYPE_CODE,   
                             LNCONTSTATEMENT.SLIP_DATE,   
                             LNUCFLOANITEMTYPE.SIGN_FLAG,   
                             LNCONTSTATEMENT.REMARK  
                        FROM LNCONTSTATEMENT,   
                             LNUCFLOANITEMTYPE  
                       WHERE ( LNCONTSTATEMENT.LOANITEMTYPE_CODE = LNUCFLOANITEMTYPE.LOANITEMTYPE_CODE ) and  
                             ( LNCONTSTATEMENT.COOP_ID = LNUCFLOANITEMTYPE.COOP_ID ) and  
                             ( ( lncontstatement.loancontract_no = {0} ) ) 
                             ORDER BY LNCONTSTATEMENT.SEQ_NO  asc  
";

            sql = WebUtil.SQLFormat(sql, ls_docno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}