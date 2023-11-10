using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_ln_loan_search_ctrl
{
    public partial class w_dlg_ln_loan_search : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsCriteria.InitDs(this);
            dsSearch.InitDs(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsCriteria.DATA[0].coop_id = state.SsCoopControl;
                String cmd = "";
                try
                {
                    cmd = Request["cmd"];
                }
                catch { }
                try
                {
                    if (cmd == "member")
                    {
                        try
                        {
                            dsCriteria.DATA[0].member_no = Request["member_no"];
                            dsCriteria.DATA[0].lawtype_code = -1;
                            Search();

                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                    else if (cmd == "npl")
                    {
                        dsCriteria.DATA[0].lawtype_code = 999;
                    }
                    else
                    {
                        dsCriteria.DATA[0].lawtype_code = -1;
                    }
                }
                catch { }
                dsCriteria.DdLoanTypeCode(dsCriteria.DATA[0].coop_id);
                dsCriteria.DdLawtypeCode();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                int rowNum = 300;
                string union = "";
                string where = " and lncontmaster.coop_id = '" + dsCriteria.DATA[0].coop_id + "'";
                string orderBy = "";
                if (dsCriteria.DATA[0].member_no.Length > 0)
                {
                    string membNo = WebUtil.MemberNoFormat(dsCriteria.DATA[0].member_no);
                    dsCriteria.DATA[0].member_no = membNo;
                    dsCriteria.DATA[0].memb_name = "";
                    dsCriteria.DATA[0].memb_surname = "";
                    dsCriteria.DATA[0].loancontract_no = "";
                    where += " and lncontmaster.member_no = '" + membNo + "' ";

                    //--------
                    union = @" union
                    select
	                    lncontmaster.coop_id,
	                    lncontmaster.member_no,
	                    lncontmaster.loancontract_no,
	                    lnucfnpllawtype.lawtype_desc,
	                    mbmembmaster.memb_name,
	                    mbmembmaster.memb_surname,
	                    mbucfprename.prename_desc,
	                    lncontmaster.loantype_code,
	                    lncontmaster.principal_balance,
                        lncontmaster.lastpayment_date,
	                    lncontmaster.contract_status,
                        decode(lnnplmaster.lawtype_code, null, -1, lnnplmaster.lawtype_code) as lawtype_code,
	                    decode(lncontmaster.contract_status, 1, 'ปกติ', 8, 'รอ', -1, 'ออก', '-') AS STATUS_DESC
                    from 
	                    lncontmaster,
	                    mbmembmaster,
	                    mbucfprename,
	                    lnloantype,
	                    lnnplmaster,
	                    lnucfnpllawtype
                    where 
	                    lncontmaster.coop_id = mbmembmaster.coop_id and
	                    lncontmaster.coop_id = lnloantype.coop_id and
	                    lncontmaster.coop_id = lnnplmaster.coop_id and
	                    lnnplmaster.coop_id = lnucfnpllawtype.coop_id and
                        lnnplmaster.lawtype_code = lnucfnpllawtype.lawtype_code and
	                    lncontmaster.loancontract_no = lnnplmaster.loancontract_no and
	                    lncontmaster.loantype_code = lnloantype.loantype_code and
	                    lnnplmaster.member_no = mbmembmaster.member_no and
	                    mbmembmaster.prename_code = mbucfprename.prename_code and
                        lnnplmaster.coop_id = {0} and
                        lnnplmaster.member_no = {1}";
                    union = WebUtil.SQLFormat(union, state.SsCoopControl, membNo);
                    rowNum = 99999;
                    orderBy = " order by lawtype_code desc  ";
                }
                if (dsCriteria.DATA[0].loancontract_no.Length > 0)
                {
                    where += " and lncontmaster.loancontract_no like '%" + dsCriteria.DATA[0].loancontract_no + "%'";
                }
                if (dsCriteria.DATA[0].memb_name.Length > 0)
                {
                    where += " and mbmembmaster.memb_name like '%" + dsCriteria.DATA[0].memb_name + "%'";
                }
                if (dsCriteria.DATA[0].memb_surname.Length > 0)
                {
                    where += " and mbmembmaster.memb_surname like '%" + dsCriteria.DATA[0].memb_surname + "%'";
                }
                if (dsCriteria.DATA[0].loantype_code.Length > 0)
                {
                    where += " and lncontmaster.loantype_code = '" + dsCriteria.DATA[0].loantype_code + "'";
                }
                if (dsCriteria.DATA[0].lawtype_code == 999)
                {
                    where += " and lnnplmaster.lawtype_code is not null ";
                }
                else if (dsCriteria.DATA[0].lawtype_code >= 0)
                {
                    where += " and lnnplmaster.lawtype_code = " + dsCriteria.DATA[0].lawtype_code;
                }
                string sql = @"
                    select
                        distinct
	                    lncontmaster.coop_id,
	                    lncontmaster.member_no,
	                    lncontmaster.loancontract_no,
	                    lnucfnpllawtype.lawtype_desc,
	                    mbmembmaster.memb_name,
	                    mbmembmaster.memb_surname,
	                    mbucfprename.prename_desc,
	                    lncontmaster.loantype_code,
	                    lncontmaster.principal_balance,
                        lncontmaster.lastpayment_date,
	                    lncontmaster.contract_status,
                        decode(lnnplmaster.lawtype_code, null, -1, lnnplmaster.lawtype_code) as lawtype_code,
	                    decode(lncontmaster.contract_status, 1, 'ปกติ', 8, 'รอ', -1, 'ออก', '-') AS STATUS_DESC
                    from 
	                    lncontmaster,
	                    mbmembmaster,
	                    mbucfprename,
	                    lnloantype,
	                    lnnplmaster,
	                    lnucfnpllawtype
                    where 
	                    lncontmaster.coop_id = mbmembmaster.coop_id and
	                    lncontmaster.coop_id = lnloantype.coop_id and
	                    lncontmaster.coop_id = lnnplmaster.coop_id (+) and
	                    lnnplmaster.coop_id = lnucfnpllawtype.coop_id (+) and
                        lnnplmaster.lawtype_code = lnucfnpllawtype.lawtype_code (+) and
	                    lncontmaster.loancontract_no = lnnplmaster.loancontract_no (+) and
	                    lncontmaster.loantype_code = lnloantype.loantype_code and
	                    lncontmaster.member_no = mbmembmaster.member_no and
	                    mbmembmaster.prename_code = mbucfprename.prename_code and
	                    rownum <= " + rowNum + " " + where + union + orderBy;// +@"
                //order by member_no, contract_status desc, loantype_code, loancontract_no";
                DataTable dt = WebUtil.Query(sql);
                dsSearch.ImportData(dt);
                LbCount.Text = "ดึงข้อมูล" + (dt.Rows.Count >= 300 ? "แบบสุ่ม" : "ได้") + " " + dt.Rows.Count + " รายการ";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}