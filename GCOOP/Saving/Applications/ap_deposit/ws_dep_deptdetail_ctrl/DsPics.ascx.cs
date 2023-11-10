using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsPics : DataSourceRepeater
    {
        public DataSet1.DT_PISCDataTable DATA { get; set; }

        public void InitDsPics(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_PISC;
            this.EventItemChanged = "OnDsDsPicsItemChanged";
            this.EventClicked = "OnDsDsPicsClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPics");
            this.Register();
        }

        public void RetrievePisc(String ls_dept_no)
        {
//            String sql = @"      SELECT DPSIGNATUR.DEPTACCOUNT_NO,   
//                                 DPSIGNATUR.SEQ_NO,   
//                                 '  ' as picname,   
//                                 DPSIGNATUR.COOP_ID  
//                            FROM DPSIGNATUR  
//                           WHERE ( DPSIGNATUR.DEPTACCOUNT_NO = {1} ) AND  
//                                 ( DPSIGNATUR.COOP_ID = {0} )   
//                        ORDER BY DPSIGNATUR.SEQ_NO ASC";

//            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_dept_no);
//            DataTable dt = WebUtil.Query(sql);
//            this.ImportData(dt);
        }


    }
}