using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_npl_contcoll
{
    public partial class npl_contcoll : PageWebReport, WebReport
    {
        public void InitJsPostBack()
        {
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Retrieve();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal lawType = dsList.DATA[i].LAWTYPE_CODE;
                    if (lawType == 1 || lawType == 2 || lawType == 3 || lawType == 4)
                    {
                        dsList.DATA[i].CHECK_FLAG = 1;
                    }
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
            try
            {
                string rid = Request["rid"];
                string colltype = rid == "[LNNPL11]" ? "01" : "04";
                string mortgageSum = colltype == "01" ? "0" : "(select n_pk_lnnpl.of_r_sum_mortgage_price(npl.coop_id, npl.member_no, npl.lawtype_code) from dual)";
                string mortgageAll = colltype == "01" ? "0" : "(select n_pk_lnnpl.of_r_sum_mortgage_all(npl.coop_id, {1}) from dual)";
                string lawtypes = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].CHECK_FLAG == 1)
                    {
                        lawtypes += "," + dsList.DATA[i].LAWTYPE_CODE;
                    }
                }
                if (lawtypes.Length > 0)
                {
                    lawtypes = lawtypes.Substring(1);
                }
                String sql = @"
                    select
                      npl.coop_id,
                      npl.loancontract_no,
                      LAG(npl.loancontract_no, 1, 0) OVER (ORDER BY npl.lawtype_code, npl.member_no, npl.loancontract_no, c.ref_collno) AS last_loan,
                      c.ref_collno,
                      decode(cm.collmast_no, null, c.ref_collno || ' ' || c.description, c.description) as coll_desc,
                      decode(cm.mortgage_price, null, 0, cm.mortgage_price) as mortgage_price,
                      npl.member_no,
                      npl.lawtype_code,
                      lawtype.lawtype_desc,
                      loan.principal_balance,
                      loan.intset_arrear,
                      pre.prename_desc,
                      memb.prename_code,
                      memb.memb_name,
                      memb.memb_surname,
                      memb.membgroup_code,
                      memb.addr_phone,
                      mg.membgroup_desc,
                      n_pk_lnnpl.of_getadvance_div(npl.coop_id, npl.member_no, npl.follow_seq) as margin,
                      " + mortgageSum + @" as mortgage_sum,
                      " + mortgageAll + @" as mortgage_all,
                      coop.coop_name
                    from lnnplmaster npl
                      inner join cmcoopmaster coop on npl.coop_id = coop.coop_id
                      inner join lnucfnpllawtype lawtype on npl.coop_id = lawtype.coop_id and npl.lawtype_code = lawtype.lawtype_code
                      inner join lncontmaster loan on npl.coop_id = loan.coop_id and npl.loancontract_no = loan.loancontract_no
                      inner join mbmembmaster memb on npl.coop_id = memb.coop_id and npl.member_no = memb.member_no
                      inner join mbucfprename pre on memb.prename_code = pre.prename_code
                      inner join mbucfmembgroup mg on npl.coop_id = mg.coop_id and memb.membgroup_code = mg.membgroup_code
                      left join lncontcoll c on c.coop_id = npl.coop_id and c.loancontract_no = npl.loancontract_no
                      left join lncollmaster cm on cm.coop_id = npl.coop_id and cm.collmast_no = c.ref_collno
                    where
                      npl.coop_id = {0} and
                      npl.lawtype_code in (" + lawtypes + @") and
                      (c.loancolltype_code = {2} or c.loancolltype_code is null)
                    order by npl.lawtype_code, npl.member_no, npl.loancontract_no, c.ref_collno
                    ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, lawtypes, colltype);
                iReportArgument args = new iReportArgument(sql);
                iReportBuider report = new iReportBuider(this, args);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}