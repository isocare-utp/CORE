using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_approve_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }
        public void RetrieveList(string sqlsearch, string sqlorder)
        {
            String sql = @"select 
	                        dbo.ft_getmemname( mb.coop_id, mb.member_no ) as mbname,
	                        mb.membgroup_code,
	                        req.assist_docno,   
                            req.member_no,
                            req.edu_levelcode,
                            lv.edulevel_desc,
                            req.postsend_date,
                            req.salary_amount,
                            req.postsend_date as ass_date ,
	                        req.assisttype_code,
                            req.req_date,
	                        req.assistnet_amt,
	                        req.req_status,
                            req.assist_year,
	                        req.assisttype_code + ':' + ast.assisttype_desc+' ('+uap.assistpay_desc+')' as assistpay_desc,
                            'ผู้รับทุน  :  '+req.ass_rcvname as ass_rcvname,
                            ast.stm_flag,
                            ast.assisttype_group,
                            (select asm.asscontract_no from asscontmaster asm where asm.member_no = req.member_no and asm.assisttype_code = req.assisttype_code and asm.withdrawable_amt > 0) as asscontract_no,
                            req.moneytype_code ,
                            req.send_system ,
                            req.deptaccount_no
                        from mbmembmaster mb
                            join assreqmaster req on req.member_no = mb.member_no
                            join assucfassisttype ast on req.assisttype_code = ast.assisttype_code
                            join assucfassisttypepay uap on req.assisttype_code = uap.assisttype_code and req.assistpay_code = uap.assistpay_code
                            left join assucfedulevel lv on req.edu_levelcode = lv.edulevel_code and req.assist_year = lv.assist_year
                        where req.req_status = 8
                        and req.coop_id = {0}
                        " +sqlsearch + sqlorder;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            Int32 s_req = 0;
            decimal s_balance = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                s_req = this.RowCount;
                s_balance += this.DATA[i].ASSISTNET_AMT;
            }
            sum_req.InnerText = s_req.ToString("#,##0");
            sum_balance.InnerText = s_balance.ToString("N");
        }

        public void RetrieveListass(string sqlsearch, string sqlorder)
        {
            String sql = @"select 
	                        dbo.ft_getmemname( mb.coop_id, mb.member_no ) as mbname,
	                        mb.membgroup_code,
	                        req.assist_docno,   
                            req.member_no,
                            req.edu_levelcode,
                            req.assist_amt as edulevel_desc,
                            req.assistcut_amt as salary_amount,
	                        req.assisttype_code,
                            req.req_date as ass_date,
	                        req.assistnet_amt,
	                        req.req_status,
                            req.assist_year,
	                        req.assisttype_code + ':' + ast.assisttype_desc+' ('+uap.assistpay_desc+')' as assistpay_desc,
                            'ผู้รับทุน  :  '+req.ass_rcvname as ass_rcvname,
                            ast.stm_flag,
                            ast.assisttype_group,
                            (select asm.asscontract_no from asscontmaster asm where asm.member_no = req.member_no and asm.assisttype_code = req.assisttype_code and asm.withdrawable_amt > 0) as asscontract_no,
                            req.moneytype_code ,
                            req.send_system ,
                            req.deptaccount_no
                        from mbmembmaster mb
                            join assreqmaster req on req.member_no = mb.member_no
                            join assucfassisttype ast on req.assisttype_code = ast.assisttype_code
                            join assucfassisttypepay uap on req.assisttype_code = uap.assisttype_code and req.assistpay_code = uap.assistpay_code
                            left join assucfedulevel lv on req.edu_levelcode = lv.edulevel_code and req.assist_year = lv.assist_year
                        where req.req_status = 8
                        and req.coop_id = {0}
                        " + sqlsearch + sqlorder;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            Int32 s_req = 0;
            decimal s_balance = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                s_req = this.RowCount;
                s_balance += this.DATA[i].ASSISTNET_AMT;
            }
            sum_req.InnerText = s_req.ToString("#,##0");
            sum_balance.InnerText = s_balance.ToString("N");
        }
    }
}