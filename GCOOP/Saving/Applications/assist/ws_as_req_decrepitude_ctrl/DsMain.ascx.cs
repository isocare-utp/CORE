using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
namespace Saving.Applications.assist.ws_as_req_decrepitude_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_search");
            this.Button.Add("b_add");
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            string sql = @"select 
		                        m.member_no,
		                        ft_getmbname(m.coop_id,m.member_no) as mbname,
		                        m.membgroup_code || ' : ' || mbgroup.membgroup_desc as mbgroup,
                                m.membtype_code,
		                        t.membtype_code || ' ' || t.membtype_desc mbtypedesc,
		                        m.birth_date,
		                        m.member_date,
		                        ftcm_calagemth(m.birth_date,{1}) birth_age,
		                        ftcm_calagemth(m.member_date,{1}) as member_age,
                                trunc(months_between({1},m.member_date)) as age_membmth,
                                m.salary_amount,
		                        m.card_person,
                                t.membcat_code,
                                ft_getmbaddr(m.coop_id, m.member_no, 1) as mbaddr,
                                case m.mariage_status when 1 then 'โสด' when 2 then 'สมรส' when 3 then 'หย่า' when 4 then 'หม้าย' else 'ไม่ระบุ' end as mariage_desc,
                                case rq.req_status when 1 then 'อนุมัติ' when 8 then 'รออนุมัติ' when -9 then 'ยกเลิก'  else 'ไม่ระบุ' end as reqstatus_desc,
                                rq.*
                         from assreqmaster rq
                            join mbmembmaster m on rq.member_no = m.member_no
                            join mbucfmembtype t on t.membtype_code = m.membtype_code
                            join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                where rq.assist_docno = {0} ";
            sql = WebUtil.SQLFormat(sql, as_reqno, state.SsWorkDate);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void AssistType()
        {
            string sql = @" select
		                        ASSISTTYPE_CODE, 
		                        ASSISTTYPE_CODE||' - '||ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
                            where assisttype_code = '50'
                        ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");

        }

        public void GetAssYear()
        {
            string sql = @"select ass_year + 543 ass_show, ass_year from assucfyear order by ass_year desc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assist_year", "ass_show", "ass_year");
        }
    }
}