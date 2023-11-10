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
    public partial class DsIntdetail : DataSourceFormView
    {
        public DataSet1.LNLOANTYPE2DataTable DATA { get; set; }

        public void InitDsIntdetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE2;
            this.EventItemChanged = "OnDsIntdetailItemChanged";
            this.EventClicked = "OnDsIntdetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsIntdetail");
            this.TableName = "LNLOANTYPE";
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT LOANTYPE_CODE, 
                                    INTEREST_METHOD, 
                                    CONTINT_TYPE, 
                                    INTTABFIX_CODE, 
                                    INTTABRATE_CODE, 
                                    INTRATE_INCREASE, 
                                    INTSTEP_TYPE, 
                                    CALINTRCV_NOTTYPE, 
                                    CALINTRCV_NOTDATE, 
                                    CALINTRCV_HALFTYPE, 
                                    CALINTRCV_HALFDATE, 
                                    CALINTPAY_NOTTYPE, 
                                    CALINTPAY_NOTDATE, 
                                    CALINTPAY_HALFTYPE, 
                                    CALINTPAY_HALFDATE, 
                                    COOP_ID 
                                    FROM LNLOANTYPE
                               WHERE loantype_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdIntrateCode()
        {
            string sql = @"select  loanintrate_code, loanintrate_code+'  '+loanintrate_desc as display,1 as sorter from lncfloanintrate  
                        union
                        select '','',0  order by sorter, loanintrate_code ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "inttabfix_code", "display", "loanintrate_code");

        }

        public void DdInttabrateCode()
        {
            string sql = @"select  loanintrate_code, loanintrate_code+'  '+loanintrate_desc as display,1 as sorter from lncfloanintrate  
                        union
                        select '','',0  order by sorter, loanintrate_code ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "inttabrate_code", "display", "loanintrate_code");

        }
    }
}