using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.dlg.wd_as_expense_detail_ctrl
{
    public partial class wd_as_expense_detail : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostBranch { get; set; }
        [JsPostBack]
        public string PostCashtype { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                String ls_memno, ls_expcode, ls_expbank, ls_expbranch, ls_expaccid, ls_chqpayname, ls_tofromaccid, ls_expclr, ls_deptno = "" , ls_deptaccno = "";

                try
                {
                    ls_memno = Request["memno"].ToString();
                }
                catch { ls_memno = ""; }
                try
                {
                    ls_expcode = Request["exp_code"].ToString();
                }
                catch { ls_expcode = ""; }
                try
                {
                    ls_expbank = Request["exp_bank"].ToString();
                    if (ls_expbank == "null")
                    {
                        ls_expbank = "";
                    }
                }
                catch { ls_expbank = ""; }
                try
                {
                    ls_expbranch = Request["exp_branch"].ToString();
                    if (ls_expbranch == "null")
                    {
                        ls_expbranch = "";
                    }
                }
                catch { ls_expbranch = ""; }
                try
                {
                    ls_expaccid = Request["exp_accid"].ToString();
                    if (ls_expaccid == "null")
                    {
                        ls_expaccid = "";
                    }
                }
                catch { ls_expaccid = ""; }
                try
                {
                    ls_chqpayname = Request["chq_payname"].ToString();
                    if (ls_chqpayname == "null")
                    {
                        ls_chqpayname = "";
                    }
                }
                catch { ls_chqpayname = ""; }
                try
                {
                    ls_tofromaccid = Request["tofrom_accid"].ToString();
                    if (ls_tofromaccid == "null")
                    {
                        ls_tofromaccid = "";
                    }
                }
                catch { ls_tofromaccid = ""; }

                try
                {
                    ls_expclr = Request["exp_clr"].ToString();
                    if (ls_expclr == "null")
                    {
                        ls_expclr = "";
                    }
                }
                catch { ls_expclr = ""; }

                try
                {
                    ls_deptaccno = Request["deptaccount_no"].ToString();
                    if (ls_deptaccno == "null")
                    {
                        ls_deptaccno = "";
                    }
                }
                catch { ls_expclr = ""; }

                dsList.DATA[0].cashtype = ls_expcode;
                dsList.DdBankDesc();
                dsList.DdBranch(ls_expbank);
                dsList.DdFromAccId(ls_expcode);
                

                dsList.DATA[0].bank_code = ls_expbank;
                dsList.DATA[0].branch_code = ls_expbranch;
                dsList.DATA[0].expense_accid = ls_expaccid;
                //dsList.DATA[0].pay_name = ls_chqpayname;
                dsList.DATA[0].tofrom_accid = ls_tofromaccid;
                //dsList.DATA[0].deptaccount_no = ls_deptaccno;
           //     dsList.DATA[0].expense_clearing = ls_expclr;

                if (ls_expcode == "CSH")
                {
                    dsList.DATA[0].bank_code = "";
                    dsList.FindDropDownList(0, "bank_code").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;
                    dsList.FindTextBox(0, "expense_accid").Enabled = false;
                    //dsList.FindTextBox(0, "pay_name").Enabled = false;
             //       dsList.FindDropDownList(0, "expense_clearing").Enabled = false;
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
                    dsList.FindDropDownList(0, "deptaccount_no").Enabled = false;
                }
                else if (ls_expcode == "TRN")
                {
                    dsList.FindDropDownList(0, "bank_code").Enabled = false;//เปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;
                    dsList.FindTextBox(0, "expense_accid").Enabled = false;
                    //dsList.FindTextBox(0, "pay_name").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
                    dsList.FindDropDownList(0, "deptaccount_no").Enabled = true;//เปิด
                    dsList.RetrieveDeptaccount(ls_memno, ref ls_deptno);
                    dsList.DATA[0].deptaccount_no = ls_deptaccno;
                    if (ls_deptaccno == "")
                    {
                        dsList.DATA[0].deptaccount_no = ls_deptno;
                    }
                    //dsList.FindDropDownList(0, "expense_clearing").Enabled = false;
                }
                else if (ls_expcode == "CBT" || ls_expcode == "CBO" || ls_expcode == "CHQ" )
                {
                    dsList.FindDropDownList(0, "bank_code").Enabled = true;//เปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = true;
                    dsList.FindTextBox(0, "expense_accid").Enabled = true;
                    //dsList.FindTextBox(0, "pay_name").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
           //         dsList.FindDropDownList(0, "expense_clearing").Enabled = false;
                    dsList.FindDropDownList(0, "deptaccount_no").Enabled = false;
                }
           //     else if (ls_expcode == "CHQ")
           //     {
           //         dsList.FindDropDownList(0, "bank_code").Enabled = true;//เปิด
           //         dsList.DATA[0].branch_code = "";
           //         dsList.FindDropDownList(0, "branch_code").Enabled = false;//ปิด
           //         dsList.FindTextBox(0, "expense_accid").Enabled = false;
           //         //dsList.FindTextBox(0, "pay_name").Enabled = true;
           //         dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
           ////         dsList.FindDropDownList(0, "expense_clearing").Enabled = true;
           //         dsList.FindDropDownList(0, "deptaccount_no").Enabled = false;
           //     }
                else
                {
                    dsList.DATA[0].bank_code = "";
                    dsList.FindDropDownList(0, "bank_code").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;
                    dsList.FindTextBox(0, "expense_accid").Enabled = false;
                    //dsList.FindTextBox(0, "pay_name").Enabled = false;
                    dsList.DATA[0].tofrom_accid = "";
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "deptaccount_no").Enabled = false;
                }

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostBranch)
            {
                string ls_branchcode = dsList.DATA[0].bank_code.Trim();
                dsList.DATA[0].branch_code = "";
                dsList.DdBranch(ls_branchcode);

               // this.of_setclearing();
            }
            else if (eventArg == PostCashtype)
            {
               // this.of_setclearing();
            }
        }

        private void of_setclearing()
        {
            string ls_sql, ls_expcode, ls_expbank;
            ls_expcode = dsList.DATA[0].cashtype;
            ls_expbank = dsList.DATA[0].bank_code;

            if (ls_expcode != "CBT" )
            {
                return;
            }

            ls_sql = " select clearing_type from cmucfbank where bank_code = '" + ls_expbank + "' ";

            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                //dsList.DATA[0].expense_clearing = dt.GetString("clearing_type");
            }

        }

        public void WebDialogLoadEnd()
        {
        }
    }
}