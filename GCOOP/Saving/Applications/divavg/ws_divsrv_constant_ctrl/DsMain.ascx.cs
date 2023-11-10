using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.YRCFCONSTANTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRCFCONSTANT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveMain()
        {
            String sql = @" SELECT YRCFCONSTANT.COOP_ID,   
                                     YRCFCONSTANT.CURRENT_YEAR,   
                                     YRCFCONSTANT.DIVTYPE_CODE,   
                                     YRCFCONSTANT.AVGTYPE_CODE,   
                                     YRCFCONSTANT.DIV_DAYFIX_FLAG,   
                                     YRCFCONSTANT.DIV_DAY_AMT,   
                                     YRCFCONSTANT.DIVCLRZERO_FLAG,   
                                     YRCFCONSTANT.DIV_DAYCALTYPE_CODE,   
                                     YRCFCONSTANT.DIV_DAYGRP_FLAG,   
                                     YRCFCONSTANT.MEMSET_CODE,   
                                     YRCFCONSTANT.METHPAY_TYPE  
                                FROM YRCFCONSTANT  
                               WHERE ( yrcfconstant.coop_id = {0} )      
";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        
    }
}