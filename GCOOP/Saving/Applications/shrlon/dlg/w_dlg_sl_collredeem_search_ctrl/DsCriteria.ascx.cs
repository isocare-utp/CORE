using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_collredeem_search_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.CRITERIADataTable DATA { get; set; }

        public void InitDsCriteria(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CRITERIA;
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.Register();
        }

        public void DdCollmasttypeCode()
        {
            string sql = @"                   
            SELECT COLLMASTTYPE_CODE, COLLMASTTYPE_DESC, 1 as sorter  
            FROM LNUCFCOLLMASTTYPE   COLLMASTTYPE_CODE
            union
            select ' ',' ',0 from dual order by sorter, COLLMASTTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_desc", "COLLMASTTYPE_DESC", "COLLMASTTYPE_CODE");
        }
        public void DdMembGroup()
        {
            string sql = @"
            SELECT 
                MBUCFMEMBGROUP_A.MEMBGROUP_CODE as membgroup_code,
                MBUCFMEMBGROUP_B.membgroup_desc + ' - ' + MBUCFMEMBGROUP_A.membgroup_desc as fullgroup_desc,
                1 as sorter
            FROM 
                MBUCFMEMBGROUP MBUCFMEMBGROUP_A,
                MBUCFMEMBGROUP MBUCFMEMBGROUP_B
            WHERE ( MBUCFMEMBGROUP_A.COOP_ID = MBUCFMEMBGROUP_B.COOP_ID ) and  
                ( MBUCFMEMBGROUP_A.MEMBGROUP_CONTROL = MBUCFMEMBGROUP_B.MEMBGROUP_CODE ) and  
                ( MBUCFMEMBGROUP_A.COOP_ID = {0} ) 
            union
            select '', '', 0 from dual   
            ORDER BY sorter ,  membgroup_code
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            this.DropDownDataBind(sql, "membgroup_desc", "fullgroup_desc", "membgroup_code");
        }
    }
}