using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_search_request_old_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.DtCriteriaDataTable DATA { get; set; }

        public void InitDsCriteria(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DtCriteria;
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
                ( MBUCFMEMBGROUP.COOP_ID = {0} ) 
            union
            select '', 'ทั้งหมด', 0  
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
                ( coop_id = {0} ) 
            union
            select '', 'ทั้งหมด', 0   
            ORDER BY sorter ,  membtype_code
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            this.DropDownDataBind(sql, "membtype_desc", "membtype_desc", "membtype_code");
        }

        public void AssistType(ref string ls_assisttype)
        {
            string sql = @"select * from
                        (
	                        select
		                        ASSISTTYPE_CODE, 
		                        ASSISTTYPE_CODE+' - '+ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
	                        union
	                        select top 1 
		                        '00', 
		                        'ทั้งหมด' as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
                        )as display
                        order by sorter, assisttype_code
                        ";
            DataTable dt = WebUtil.Query(sql);
            ls_assisttype = dt.Rows[0].Field<string>("assisttype_code");
            this.DropDownDataBind(sql, "assisttype_code", "display", "assisttype_code");

        }
        public void AssistPayType(string ls_asscode, ref string ls_minpaytype, ref string ls_maxpaytype)
        {
            string sql = @"select 
	                        assistpay_code, 
	                        assistpay_code + ' - ' + assistpay_desc display
                        from assucfassisttypepay 
                        where coop_id= {0} and assisttype_code = {1}
                        order by assistpay_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_asscode);
            DataTable dt = WebUtil.Query(sql);
            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
            ls_maxpaytype = dt.Rows[Convert.ToInt32(dt.Rows.Count) - 1].Field<string>("assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code1", "display", "assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code2", "display", "assistpay_code");
        }

        public void GetAssYear()
        {
            string sql = @"select ass_year + 543 ass_show, ass_year from assucfyear order by ass_year desc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assist_year", "ass_show", "ass_year");
        }
    }
}