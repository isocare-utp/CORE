using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsDeposit : DataSourceRepeater
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; set; }

        public void InitDsDeposit(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;
            this.EventItemChanged = "OnDsDepositItemChanged";
            this.EventClicked = "OnDsDepositClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDeposit");
            this.Register();
        }

        public void Retrieve(string member_no)
        {
            string sql = @"  SELECT DPDEPTMASTER.DEPTACCOUNT_NO,   
         DPDEPTMASTER.DEPTTYPE_CODE,   
         DPDEPTMASTER.DEPTACCOUNT_NAME,   
         DPDEPTMASTER.PRNCBAL  
    FROM DPDEPTMASTER  
   WHERE ( DPDEPTMASTER.MEMBER_NO = {0} ) AND  
         ( DPDEPTMASTER.DEPTCLOSE_STATUS = 0 )    
";
            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}