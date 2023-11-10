using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.assist.dlg.wd_as_member_search_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet2.CRITERIADataTable DATA { get; set; }

        public void InitDsCriteria(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet2 ds = new DataSet2();
            this.DATA = ds.CRITERIA;
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.Register();
        }

        public void DdMembGroup()
        {
            string sql = @"
            SELECT 
                MBUCFMEMBGROUP.MEMBGROUP_CODE as membgroup_code,
                MBUCFMEMBGROUP.membgroup_desc + ' - ' + MBUCFMEMBGROUP.membgroup_desc as fullgroup_desc,
                1 as sorter
            FROM 
                MBUCFMEMBGROUP
            WHERE 
                 MBUCFMEMBGROUP.COOP_ID = {0}  
            union
            select '', 'ทั้งหมด', 0 from dual   
            ORDER BY sorter ,  membgroup_code
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            this.DropDownDataBind(sql, "membgroup_nodd", "fullgroup_desc", "membgroup_code");
        }

        public void DdMembType()
        {
            string sql = @"
            SELECT 
                membtype_code,
                membtype_desc,
                1 as sorter
            FROM 
                mbucfmembtype	                   
            WHERE 
                 coop_id = {0} 
            union
            select '', 'ทั้งหมด', 0 from dual   
            ORDER BY sorter ,  membtype_code
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            this.DropDownDataBind(sql, "membtype_desc", "membtype_desc", "membtype_code");
        }
    }
}