using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_acc_ccl_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.kpmastreceivedetDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.kpmastreceivedet;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string member_no, string recv_period)
        {
            string sql = @"select kpmastreceivedet.coop_id,   
                                kpmastreceivedet.kpslip_no,   
                                kpmastreceivedet.seq_no,   
                                kpucfkeepitemtype.keepitemtype_code,   
                                kpucfkeepitemtype.keepitemtype_desc,   
                                kpmastreceivedet.shrlontype_code,   
                                kpmastreceivedet.loancontract_no,   
                                kpmastreceivedet.description,   
                                kpmastreceivedet.principal_payment,   
                                kpmastreceivedet.interest_payment,   
                                kpmastreceivedet.item_payment,   
                                kpmastreceivedet.principal_balance,   
                                kpmastreceivedet.keepitem_status,   
                                keepitem_status as bfkeepitem_status,   
                                kpucfkeepitemtype.keepitemtype_grp,   
                                kpmastreceivedet.bizzcoop_id,   
                                kpmastreceivedet.cancel_id,   
                                kpmastreceivedet.cancel_accid  
                        from kpmastreceivedet,   
                                kpucfkeepitemtype  
                        where ( kpucfkeepitemtype.coop_id = kpmastreceivedet.coop_id ) and  
                                ( kpucfkeepitemtype.keepitemtype_code = kpmastreceivedet.keepitemtype_code )    
                    and kpmastreceivedet.coop_id = {0}
                    and kpmastreceivedet.member_no = {1}
                    and kpmastreceivedet.recv_period = {2}
                    and kpmastreceivedet.keepitem_status = -9";
            sql = WebUtil.SQLFormat(sql,state.SsCoopControl, member_no,recv_period);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdAccid();
        }

        public void DdAccid()
        {
            string sql = @"select account_id,   
                account_id||':'||account_name  as display, 1 as sorter   
            from accmaster  
            where ( accmaster.coop_id = {0} ) AND  
                (  exists ( select 1 from cmucftofromaccid cuf 
                where cuf.coop_id = accmaster.coop_id and 
                cuf.account_id = accmaster.account_id  and
                cuf.coop_id = {0} and
                cuf.applgroup_code = 'KEP' and
                cuf.sliptype_code = 'KTC' ) )
            union 
            select '','',0 from dual order by sorter, account_id desc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "cancel_accid", "display", "account_id");
        }
    }
}
