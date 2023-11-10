using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_acc_coopid_rdate_accid
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
                                  account_id,
                                  account_id||'-'||account_name as display,
                                  account_name,1 as sorter
                             from accmaster
                             where account_level = 4
                             union
                             select '','','','',0 from dual order by sorter,account_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_id1", "display", "account_id");  
        }
        public void DdLoanTypeE()
        {
            String sql = @"select coop_id,
                                  account_id,
                                  account_id||'-'||account_name as display,
                                  account_name,1 as sorter
                             from accmaster
                             where account_level = 4
                             union
                             select '','','','',0 from dual order by sorter,account_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_id2", "display", "account_id");
        }

    }
}