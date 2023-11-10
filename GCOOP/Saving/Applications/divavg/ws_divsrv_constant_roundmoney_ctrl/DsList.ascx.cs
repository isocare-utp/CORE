using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_roundmoney_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMROUNDMONEYDataTable DATA { get; set; }

        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMROUNDMONEY;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList(String ls_applgrp)
        {
            string sql = @" SELECT  CMROUNDMONEY.COOP_ID ,           
                                    CMROUNDMONEY.APPLGROUP_CODE ,           
                                    CMROUNDMONEY.FUNCTION_CODE ,          
                                    CMROUNDMONEY.SATANG_TYPE ,           
                                    CMROUNDMONEY.TRUNCATE_POS_AMT ,           
                                    CMROUNDMONEY.ROUND_TYPE ,           
                                    CMROUNDMONEY.ROUND_POS_AMT ,           
                                    CMROUNDMONEY.USE_FLAG     
                                    FROM CMROUNDMONEY      
                              WHERE ( cmroundmoney.coop_id = {0} ) And          
                                    ( cmroundmoney.applgroup_code = {1} ) ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, ls_applgrp);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

    }
}