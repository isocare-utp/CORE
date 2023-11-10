using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNREQLOANDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQLOAN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_detail");
            this.Register();
        }

        public void RetrieveList(string group, string entry,string str_query)
        {
            String sql = @"select 'CON' as lnrcvfrom_code,   
               lncontmaster.coop_id,  
                lncontmaster.loantype_code,   
                lncontmaster.loancontract_no,   
                lncontmaster.member_no,   
                lncontmaster.withdrawable_amt,   
                lnloantype.prefix,   
                mbucfprename.prename_desc,   
                mbmembmaster.memb_name,   
                mbmembmaster.memb_surname,   
                mbmembmaster.membgroup_code,   
                0 as operate_flag  ,
                0 as loanrequest_status
            from lncontmaster,   
                mbmembmaster,   
                mbucfprename,   
                lnloantype  
            where ( mbmembmaster.member_no = lncontmaster.member_no )  
                and ( mbmembmaster.prename_code = mbucfprename.prename_code )  
                and ( lncontmaster.loantype_code = lnloantype.loantype_code )  
                and ( mbmembmaster.coop_id = lncontmaster.memcoop_id )  
                and ( lnloantype.coop_id = lncontmaster.coop_id)  
                and ( lncontmaster.od_flag = 0 )
                and ( lncontmaster.coop_id = {0} ) 
                and ( lnloantype.loangroup_code like {1}  )
                and ( lncontmaster.approve_id like {2}   )
                and ( lncontmaster.withdrawable_amt > 0 )  
                and ( lncontmaster.contract_status > 0 )
                and ( lncontmaster.loancontract_no not in ( select slslippayout.loancontract_no from slslippayout where slslippayout.slip_status = 8 ) )  
            union   
            select 'REQ' as lnrcvfrom_code,   
                lnreqloan.coop_id,   
                lnreqloan.loantype_code,  
                lnreqloan.loanrequest_docno,   
                lnreqloan.member_no,   
                lnreqloan.loanapprove_amt,   
                lnloantype.prefix,   
                mbucfprename.prename_desc,   
                mbmembmaster.memb_name,   
                mbmembmaster.memb_surname,   
                mbmembmaster.membgroup_code,   
                0 as operate_flag  ,
                lnreqloan.loanrequest_status
            from lnreqloan,   
                mbmembmaster,   
                mbucfprename,   
                lnloantype  
            where ( lnreqloan.member_no = mbmembmaster.member_no )  
                and ( mbmembmaster.prename_code = mbucfprename.prename_code )  
                and ( lnreqloan.loantype_code = lnloantype.loantype_code )  
                and ( mbmembmaster.coop_id = lnreqloan.memcoop_id )  
                and ( lnreqloan.coop_id = lnloantype.coop_id ) 
                and ( lnreqloan.od_flag = 0 )
                and ( lnreqloan.memcoop_id = {0} ) 
                and ( lnloantype.loangroup_code like {1}  )
                and ( lnreqloan.entry_id like {2} )
                and ( lnreqloan.loanrequest_status in (11,12) )
                and ( lnreqloan.loanrequest_docno not in ( select slslippayout.loanrequest_docno from slslippayout where slslippayout.slip_status = 8 ))             
            order by loantype_code";
            /* " + str_query + @"
            order by loantype_code, loancontract_no*/

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, group, entry);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}