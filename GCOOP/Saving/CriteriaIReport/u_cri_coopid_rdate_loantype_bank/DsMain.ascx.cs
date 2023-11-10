using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rloantype_bank
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"  SELECT COOP_ID,   
                                    COOP_NAME  
                               FROM CMCOOPMASTER ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }
        public void DdLoanTypeS()
        {
            String sql = @"select coop_id,
                                  loantype_code,
                                  loantype_code||'-'||loantype_desc as display,
                                  loantype_desc,1 as sorter
                             from lnloantype
                             union
                             select '','','','',0 from dual order by sorter,loantype_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "start_loantype", "display", "loantype_code");
        }
        public void DdLoanTypeE()
        {
            String sql = @"select bank_code,
                                  bank_desc,
                                  bank_code||'-'||bank_desc as display,
                                  bank_code,1 as sorter
                             from cmucfbank
                             union
                             select '','','','',0 from dual order by sorter,bank_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "end_loantype", "display", "bank_code");
        }

    }
}