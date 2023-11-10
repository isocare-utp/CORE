using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_approve_reprint_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string member_no, string apv_id, string lntypecode, string contno_s, string contno_e, DateTime apv_date_s, DateTime apv_date_e, string loan_docno)
        {
            string sqlext = "";
            if (member_no != "")
            {
                sqlext += " and lnreqloan.member_no = '" + WebUtil.MemberNoFormat(member_no) + "'";
            }
            if (apv_id != "")
            {
                sqlext += " and lnreqloan.approve_id = '" + apv_id + "'";
            }
            if (lntypecode != "")
            {
                sqlext += " and lnreqloan.loantype_code = '" + lntypecode + "'";
            }
            if (contno_s != "" && contno_e != "")
            {
                sqlext += " and (lnreqloan.loancontract_no between '" + contno_s + "' and '" + contno_e + "')";
            }
            if (apv_date_s.Year > 1900 && apv_date_e.Year > 1900)
            {
                sqlext += " and lnreqloan.loanapprove_date between {1} and {2} ";

            }
            string sql = @" select loancontract_no, loantype_code, member_no, dbo.ft_getmemname( memcoop_id, member_no ) as fullname,
		                            loanapprove_amt, approve_id,LOANREQUEST_docno as loan_docno,dbo.FTCNVTDATE(loanrequest_date,1) as lnrequest_date,loanrequest_date
                            from	lnreqloan
                            where	coop_id = {0} and loanrequest_status <> -9" + sqlext + " order by loanrequest_date DESC ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, apv_date_s, apv_date_e);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}