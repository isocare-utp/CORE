using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_process_moneyreturn_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DTListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void RetrieveDetail( string recv_period, string smembgroup_code, string emembgroup_code)
        {
            String sql = @"  
                     select mr.member_no as member_no,
                    dbo.ft_memname(mr.coop_id , mr.member_no ) as memb_name,
                    dbo.ft_memgrp( mr.coop_id, mb.membgroup_code ) as memb_group,
                    mr.loancontract_no as loancontract_no,
                    mr.return_amount as int_return,
                    mr.deptaccount_no as deptaccount_no
                     from mbmoneyreturn mr , mbmembmaster mb
                    where mr.member_no = mb.member_no
                    and mr.coop_id = mb.coop_id
                    and mr.coop_id = {0}
                    and rtrim( ltrim( ref_docno )) = {1}
                    and mb.membgroup_code between {2} and {3}
                    and mr.return_status = 8
                    and mr.return_amount >= 100
                    order by mr.member_no , mr.loancontract_no";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, recv_period, smembgroup_code, emembgroup_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveDet(string recv_period, string smembgroup_code, string emembgroup_code)
        {
            String sql = @"  
                     select *
                    from mbmoneyreturn
                    where
                    mr.coop_id = {0}
                    and rtrim( ltrim( ref_docno )) = {1}
                    and mb.membgroup_code between {2} and {3}
                    and mr.return_status = 8
                    and mr.return_amount >= 100
                    order by mr.member_no , mr.loancontract_no";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, recv_period, smembgroup_code, emembgroup_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}