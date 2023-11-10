using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBMONEYRETURNDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMONEYRETURN;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }

        public void DDBank() {

            string sql = @"
            select bank_code, bank_desc , 1 as sorter  from  cmucfbank  
union
select '', '', 0 from dual order by sorter, bank_code
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }

        //retrieve list for more args
        public void RetrieveList(string member_no,string salary_id,string s_membgroup,string e_membgroup,string bank_code,string item_type) {
            string sql = @"select mr.*,tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid ,bb.branch_name,mp.prename_desc||mb.memb_name||' '||mb.memb_surname as full_name
from mbmoneyreturn mr ,mbmembmoneytr tr  ,mbmembmaster mb,mbucfprename mp,cmucfbankbranch bb
where mr.return_status = 0
and (mb.member_no like {0} and mb.salary_id like {1}||'%')
and (mb.membgroup_code between {2} and {3})
and mr.coop_id = {6}
and tr.bank_code like {4}
and mr.returnitemtype_code like {5}
and mr.member_no = mb.member_no
and mr.member_no=tr.member_no
and mr.coop_id = tr.coop_id
and mr.returnitemtype_code=tr.trtype_code
and mb.prename_code = mp.prename_code
and tr.bank_branch = bb.branch_id(+)
and tr.bank_code = bb.bank_code(+)";
            sql = WebUtil.SQLFormat(sql, member_no, salary_id, s_membgroup, e_membgroup, bank_code, item_type, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            

        }

        public void RetrieveListLite(string member_no, string salary_id, string s_membgroup, string e_membgroup) {
            string sql = @"select mr.*,tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid ,bb.branch_name,mp.prename_desc||mb.memb_name||' '||mb.memb_surname as full_name
from mbmoneyreturn mr ,mbmembmoneytr tr  ,mbmembmaster mb,mbucfprename mp,cmucfbankbranch bb
where mr.return_status = 0
and (mb.member_no like {0} and mb.salary_id like {1}||'%')
and (mb.membgroup_code between {2} and {3})
and mr.coop_id = {4}
and trim(mr.returnitemtype_code) ='WRT'
and mr.member_no = mb.member_no
and mr.member_no=tr.member_no
and mr.coop_id = tr.coop_id
and trim(mr.returnitemtype_code)=trim(tr.trtype_code)
and mb.prename_code = mp.prename_code
and tr.bank_branch = bb.branch_id(+)
and tr.bank_code = bb.bank_code(+)";
            sql = WebUtil.SQLFormat(sql, member_no, salary_id, s_membgroup, e_membgroup,  state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}