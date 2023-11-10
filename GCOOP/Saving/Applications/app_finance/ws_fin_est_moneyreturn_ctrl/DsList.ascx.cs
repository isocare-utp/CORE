using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_est_moneyreturn_ctrl
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

        public void RetrieveList(string member_no,DateTime op_date) {

            //old sql
//            string sql = @"select 0 as operate_flag,wfs.member_no,wfs.wrtfund_balance  as return_amount,
//'จ่ายคืนเงินกองทุนกสส '||wfs.ref_contno as description  ,'WRT' as returnitemtype_code ,lm.loancontract_no,wfs.seq_no as seq_wrt
//from mbmembmaster m , lncontmaster lm , wrtfundstatement wfs , (
//	select wf.coop_id , wf.member_no , max( seq_no ) as seq_no
//	from wrtfundstatement wf
//	where wf.coop_id = {1}
//	group by wf.coop_id , wf.member_no
//	) wrfs
//where wfs.coop_id = wrfs.coop_id
//and wfs.member_no = wrfs.member_no
//and wfs.seq_no = wrfs.seq_no
//and wfs.coop_id = lm.coop_id
//and trim( wfs.ref_contno ) = trim( lm.loancontract_no )
//and m.coop_id = lm.memcoop_id
//and m.member_no = lm.member_no
//and m.wrtfund_balance = wfs.wrtfund_balance 
//and wfs.wrtitemtype_code = 'PWT' 
//and m.wrtfund_balance > 0
//and lm.principal_balance = 0
//and m.member_no like {0}
//and wfs.coop_id = {1}";
            string sql = @"select 0 as operate_flag,wfs.member_no,wfs.wrtfund_balance  as return_amount,
'จ่ายคืนเงินกองทุนกสส '||wfs.ref_contno as description  ,'WRT' as returnitemtype_code ,lm.loancontract_no,wfs.seq_no as seq_wrt
from mbmembmaster m , lncontmaster lm ,lncontstatement ls, wrtfundstatement wfs , (
	select wf.coop_id , wf.member_no , max( seq_no ) as seq_no
	from wrtfundstatement wf
	where wf.coop_id = {1}
	group by wf.coop_id , wf.member_no
	) wrfs
where wfs.coop_id = wrfs.coop_id
and wfs.member_no = wrfs.member_no
and wfs.seq_no = wrfs.seq_no
and wfs.coop_id = lm.coop_id
and trim( wfs.ref_contno ) = trim( lm.loancontract_no )
and m.coop_id = lm.memcoop_id
and m.member_no = lm.member_no
and m.wrtfund_balance = wfs.wrtfund_balance 
and wfs.wrtitemtype_code = 'PWT' 
and m.wrtfund_balance > 0
and ls.principal_balance = 0
and lm.loancontract_no=ls.loancontract_no
and ls.loanitemtype_code='LPM'
and ls.slip_date= {2}
and lm.contract_status=-1
and m.member_no like {0}
and wfs.coop_id = {1}";
            
            try
            {
                sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopId,op_date);
                DataTable dt = WebUtil.Query(sql);
                this.ImportData(dt);
            }
            catch (Exception ex) {
                throw new Exception("retrieve user control"+ex.Message);
            }
        }
    }
}