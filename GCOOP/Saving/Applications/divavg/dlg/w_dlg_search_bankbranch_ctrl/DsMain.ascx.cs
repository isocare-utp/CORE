using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.dt_mainDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.dt_main;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_search");
            this.Button.Add("b_submit");
            this.Register();
        }

        public void DdBank()
        {
            string sql = @"
            SELECT 
                bank_code,
                bank_code || ' - ' || bank_desc as bank_desc,
                1 as sorter
            FROM 
                cmucfbank	                   
            union
            select '', '', 0 from dual   
            ORDER BY sorter ,  bank_code
            ";
            this.DropDownDataBind(sql, "bank_code", "bank_desc", "bank_code");
        }

        public void DdBankBranch(string bank_code)
        {
            string sql = @"
            SELECT 
                branch_id,
                branch_id || ' - ' || branch_name as branch_name,
                1 as sorter
            FROM 
                cmucfbankbranch
            where 
                bank_code = {0}	                   
            union
            select '', '', 0 from dual   
            ORDER BY sorter ,  branch_id
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            this.DropDownDataBind(sql, "branch_id", "branch_name", "branch_id");
        }
    }
}