using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbankbranch_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.CMUCFBANKBRANCHDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFBANKBRANCH;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }

        public void DdApplication()
        {
            string sql = @"
            select  bank_code , bank_code + ' - ' + bank_desc as bank_desc , 1 as sorter  from  CMUCFBANK 
            union  
            select '','', 0 from dual
            order by sorter, bank_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
    }
}