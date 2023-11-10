using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications._global.w_dlg_ln_loanrequest_search_ctrl
{
    public partial class w_dlg_ln_loanrequest_search : PageWebDialog, WebDialog
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
                try
                {
                    if (Request["cmd"] == "req_status8")
                    {
                        try
                        {
                            dsCriteria.DATA[0].member_no = Request["member_no"];
                            dsCriteria.DATA[0].loanrequest_status = 8;
                            Search();
                            if (dsSearch.RowCount > 0)
                            {
                                LtServerMessage.Text = WebUtil.WarningMessage("สมาชิกท่านนี้มีใบคำขอที่ยังไม่ได้อนุมัติ");
                            }
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                }
                catch { }
                dsCriteria.DdLoanTypeCode(dsCriteria.DATA[0].coop_id);
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
                string where = " and lnreqloan.coop_id = '" + dsCriteria.DATA[0].coop_id + "'";
                if (dsCriteria.DATA[0].member_no.Length > 0)
                {
                    string membNo = WebUtil.MemberNoFormat(dsCriteria.DATA[0].member_no);
                    dsCriteria.DATA[0].member_no = membNo;
                    dsCriteria.DATA[0].memb_name = "";
                    dsCriteria.DATA[0].memb_surname = "";
                    dsCriteria.DATA[0].loanrequest_docno = "";
                    where += " and lnreqloan.member_no = '" + membNo + "' ";
                }
                if (dsCriteria.DATA[0].loanrequest_docno.Length > 0)
                {
                    where += " and lnreqloan.loanrequest_docno like '%" + dsCriteria.DATA[0].loanrequest_docno + "%'";
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
                    where += " and lnreqloan.loantype_code = '" + dsCriteria.DATA[0].loantype_code + "'";
                }
                if (dsCriteria.DATA[0].loanrequest_date.Year > 1900)
                {
                    where += " and lnreqloan.loanrequest_date = to_date('" + dsCriteria.DATA[0].loanrequest_date.ToString("yyyy-MM-dd", WebUtil.EN) + "', 'yyyy-mm-dd')";
                }
                if (dsCriteria.DATA[0].loanrequest_status != 0)
                {
                    where += " and lnreqloan.loanrequest_status = " + dsCriteria.DATA[0].loanrequest_status + "";
                }
                string sql = @"
                    select 
                      lnreqloan.loanrequest_docno,
                      lnreqloan.member_no,
                      mbmembmaster.memb_name,
                      mbmembmaster.memb_surname,
                      mbucfmembgroup.membgroup_code,
                      mbucfmembgroup.membgroup_desc,
                      mbucfprename.prename_desc,
                      lnreqloan.loantype_code,
                      lnreqloan.loanrequest_date,
                      lnreqloan.loanrequest_status,
                      lnreqloan.loancontract_no,
                      lnreqloan.loanrcvfix_date,
                      lnreqloan.expense_code,
                      lnreqloan.paytoorder_desc,
                      lnreqloan.coop_id,
                      decode(loanrequest_status, 1, 'อนุมัติ', 8, 'รอ', -1, 'ยกเลิก', '-') AS STATUS_DESC
                    from 
                      lnreqloan,
                      mbmembmaster,
                      mbucfmembgroup,
                      mbucfprename,
                      lnloantype
                    where 
                      lnreqloan.coop_id = mbmembmaster.coop_id and
                      lnreqloan.coop_id = mbucfmembgroup.coop_id and
                      lnreqloan.coop_id = lnloantype.coop_id and
                      lnreqloan.loantype_code = lnloantype.loantype_code and
                      lnreqloan.member_no = mbmembmaster.member_no and
                      mbmembmaster.prename_code = mbucfprename.prename_code and
                      mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code and
                      rownum <= 300" + where;
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