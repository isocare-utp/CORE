using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsPaymentdet : DataSourceFormView
    {
        public DataSet1.LNLOANTYPE5DataTable DATA { get; set; }

        public void InitDsPaymentdet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE5;
            this.EventItemChanged = "OnDsPaymentdetItemChanged";
            this.EventClicked = "OnDsPaymentdetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsPaymentdet");
            this.TableName = "LNLOANTYPE";
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"  SELECT LOANTYPE_CODE,   
                                 LOANPAYMENT_TYPE,   
                                 LASTPAYMENT_TYPE,   
                                 RETRYLOANSEND_TIME,   
                                 DROPPRNCPAY_FLAG,
                                 COOP_ID,
                                loanpayment_count  ,
                                retryloansend_type
                            FROM LNLOANTYPE
                               WHERE loantype_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}