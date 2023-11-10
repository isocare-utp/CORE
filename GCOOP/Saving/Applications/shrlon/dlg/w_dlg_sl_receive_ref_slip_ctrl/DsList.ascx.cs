using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.REFSLIPDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            
            DataSet1 ds = new DataSet1();
            this.DATA = ds.REFSLIP;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
        public void RetrieveList(string ref_system,string member_no) {
            string sql = "";
            if (ref_system == "DEP") {
                sql = @"select dm.member_no,
                    mp.prename_desc||mb.memb_name||' '||mb.memb_surname as fullname,
                    ds.deptaccount_no as acc_id,
                    ds.deptslip_no as slip_no,
                    ds.deptslip_netamt as slip_amt,
                    ds.deptslip_date as slip_date,
                    ds.recppaytype_code as recppaytype_code
                    from dpdeptslip ds,dpdeptmaster dm ,mbmembmaster mb,mbucfprename mp
                    where ds.recppaytype_code in ('WPL','WFS', 'WPX', 'WTR','CTR') 
                    and dm.member_no={0}
                    and ds.coop_id = {1}
                    and ds.deptaccount_no =dm.deptaccount_no
                    and dm.member_no = mb.member_no
                    and mb.prename_code = mp.prename_code
                    and ds.coop_id= dm.coop_id
                    and dm.coop_id = mb.coop_id";
                sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopControl);
            }
            else if (ref_system == "DIV")
            {
                sql = @"select ypo.member_no, 
                    '' as acc_id,
                    ypo.payoutslip_no as slip_no,
                    ypd.item_payment as slip_amt,
                    ypo.slip_date as slip_date,
                    '' as recppaytype_code
                    from yrslippayout ypo, yrslippayoutdet ypd
                    where ypo.coop_id =  ypd.coop_id
                    and ypo.payoutslip_no =  ypd.payoutslip_no
                    and ypo.coop_id = {0}
                    and ypo.member_no = {1}
                    and ypo.slip_date = {2}
                    and ypo.slip_status = 1
                    and ypd.moneytype_code = 'TRN'
                    order by ypo.payoutslip_no desc";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no, state.SsWorkDate);
            }
            else if (ref_system == "ASS")
            {
                sql = @"select payoutslip_no as slip_no,
                    depaccount_no as acc_id,
                    slip_date,
                    '' as recpaytype_code,
                    payout_amt as slip_amt
                    from asnslippayout
                    where member_no = {0} 
                    and slip_date = {1}
                    and slip_status = 1";
                sql = WebUtil.SQLFormat(sql, member_no, state.SsWorkDate);
            }

            if (sql != "" && sql != null)
            {
                DataTable dt = WebUtil.Query(sql);
                this.ImportData(dt);
            }
        }
    }
}