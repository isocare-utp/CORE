using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl
{
    public partial class DwTax : DataSourceFormView
    {
        public DataSet1.FINTAXDataTable DATA { get; set; }
        public void InitDwTax(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINTAX;
            this.InitDataSource(pw, FormView1, this.DATA, "dwTax");
            this.EventItemChanged = "OnDwTaxItemChanged";
            this.EventClicked = "OnDwTaxClicked";
            this.Register();
        }
        public void RetrieveData(string coop_control)
        {
            string sql = @"  SELECT FINTAX.TAXDOC_NO,   
                             FINTAX.COOP_ID,   
                             FINTAX.TAXPAY_NAME,   
                             FINTAX.TAXPAY_ADDR,   
                             FINTAX.TAXPAY_ID,   
                             FINTAX.TAXPAY_DATE,   
                             FINTAX.TAXPAY_DESC,   
                             FINTAX.MONEY_ALLAMT,   
                             FINTAX.MONEY_AFTAX,   
                             FINTAX.MONEY_TAXAMT,   
                             FINTAX.TAX_RATE,   
                             FINTAX.TAX_ACCNO,   
                             FINTAX.TAX_BRANCH,   
                             FINTAX.TAX_BANK,   
                             FINTAX.SLIP_NO,   
                             FINTAX.CANCEL_FLAG,   
                             FINTAX.TAX_CODE  
                        FROM FINTAX  
                       WHERE FINTAX.COOP_ID = {0} ";
            sql = WebUtil.SQLFormat(sql, coop_control);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}