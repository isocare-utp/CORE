using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_approve_loan_ctrl
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
        public void RetrieveList(string sqlsearch, string ordertype)
        {
            string ls_order;

            if (ordertype == "2")
            { 
                ls_order = " order by mbmembmaster.membgroup_code, mbmembmaster.member_no";
            }
            else if (ordertype == "3")
            {
                ls_order = " order by lnreqloan.loanrequest_docno";
            }
            else
            {
                ls_order = " order by lnreqloan.loantype_code, lnreqloan.loanrequest_docno";
            }

            String sql = @"select mbucfprename.prename_desc,   
                                mbmembmaster.memb_name,   
                                mbmembmaster.memb_surname,   
                                mbmembmaster.membgroup_code,
                                lnreqloan.coop_id,   
                                lnreqloan.loanrequest_docno,   
                                lnreqloan.member_no,
                                lnreqloan.loantype_code,
                                lnreqloan.loanrequest_amt,
                                lnreqloan.loanrequest_status,
                                lnreqloan.loancontract_no,
                                lnloantype.prefix,
                                lnloantype.document_code
                            from mbmembmaster,
                                mbucfprename,
                                lnreqloan,
                                lnloantype
                            where ( mbmembmaster.prename_code = mbucfprename.prename_code ) and  
                                    ( lnreqloan.member_no = mbmembmaster.member_no ) and  
                                    ( mbmembmaster.coop_id = lnreqloan.memcoop_id ) and  
                                    ( lnreqloan.coop_id = lnloantype.coop_id ) and
                                    ( lnreqloan.loantype_code = lnloantype.loantype_code ) and
                                    ( lnreqloan.loanrequest_status = 8 ) and  
                                    ( lnreqloan.coop_id = {0} )
                                    " + sqlsearch + ls_order;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}