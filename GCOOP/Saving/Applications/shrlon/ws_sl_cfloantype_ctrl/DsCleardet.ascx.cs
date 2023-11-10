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
    public partial class DsCleardet : DataSourceFormView
    {
        public DataSet1.LNLOANTYPE4DataTable DATA { get; set; }

        public void InitDsCleardet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE4;
            this.EventItemChanged = "OnDsCleardetItemChanged";
            this.EventClicked = "OnDsCleardetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCleardet");
            this.TableName = "LNLOANTYPE";
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"  SELECT COOP_ID,
                                     LOANTYPE_CODE,   
                                     SHRSTK_BUYTYPE,   
                                     CALINTFUTURE_FLAG,   
                                     CLCFSTRCVONLY_FLAG,   
                                     CLCCCLWORKSHT_FLAG,   
                                     SHRSTKCOUNT_FLAG,
                                     lnrcvclrfuture_type   
                                FROM LNLOANTYPE 
                               WHERE loantype_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}