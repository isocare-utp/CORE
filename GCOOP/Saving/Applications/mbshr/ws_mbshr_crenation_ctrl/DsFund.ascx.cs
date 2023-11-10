using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_crenation_ctrl
{
    public partial class DsFund : DataSourceFormView
    {
        public DataSet1.DT_FundDataTable DATA { get; set; }

        public void InitDsFund(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_Fund;
            this.EventItemChanged = "OnDsFundItemChanged";
            this.EventClicked = "OnDsFundClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsFund");
            this.Register();
        }

        public void RetrieveData(String ls_memberno)
        {
           String sql = @"  
                  SELECT 
                    cmttype_code,
                    cmtaccount_name,
                    cremation_amt,
                    cremation_amt as cremationold_amt,
                    APPLY_DATE
                    FROM 
                    mbcremationthai 
                    WHERE
                    ( ( mbcremationthai.member_no = {1}) AND  
                    ( mbcremationthai.coop_id = {0}) AND 
                    mbcremationthai.cmtclose_status = '1')    AND cmttype_code = '99'
                    order  by cmttype_code asc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_memberno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}