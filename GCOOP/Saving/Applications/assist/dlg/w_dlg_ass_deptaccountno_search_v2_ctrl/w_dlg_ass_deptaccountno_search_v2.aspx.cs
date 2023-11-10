using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.dlg.w_dlg_ass_deptaccountno_search_v2_ctrl
{
    public partial class w_dlg_ass_deptaccountno_search_v2 : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostBranch { get; set; }
        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_getdate = Request["memno"];
                string[] ls_arr_memn = ls_getdate.Split('|');
                string ls_memno = ls_arr_memn[0];
                string ls_moneytype = ls_arr_memn[1];
                string ls_bank_id = ls_arr_memn[3];
                string ls_branch_id = ls_arr_memn[4];
                string ls_expense_accid = ls_arr_memn[5];
                string ls_name = "";
                if (ls_arr_memn[6] == "null" || ls_arr_memn[7] == "null")
                {
                   ls_name = " ";
                }
                else
                {
                   ls_name = ls_arr_memn[8] + ls_arr_memn[6] + ls_arr_memn[7];
                    dsList.DATA[0].pay_name = ls_name;
                }
                dsList.DATA[0].cashtype = ls_moneytype;
                hd_row.Value = ls_arr_memn[2];
                dsList.DATA[0].bank_code=ls_bank_id;
                dsList.DATA[0].branch_code = ls_branch_id;
                dsList.DATA[0].expense_accid = ls_expense_accid;
                dsList.DdBankDesc();
                dsList.DdBranch(ls_bank_id);
                dsList.DdFromAccId();
                string ls_defaultassid = "";
                string sql = @" 
                   SELECT MONEYTYPE_CODE,  
                          MONEYTYPE_DESC,
                          DEFAULTPAY_ACCID
                     FROM CMUCFMONEYTYPE WHERE  MONEYTYPE_CODE={0}  
                  ";
                sql = WebUtil.SQLFormat(sql, ls_moneytype);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_defaultassid = dt.GetString("DEFAULTPAY_ACCID").Trim();
                    dsList.DATA[0].tofrom_accid = ls_defaultassid;
                }
                if (ls_moneytype == "CSH")
                {
                    dsList.DATA[0].bank_code = "";
                    dsList.FindDropDownList(0, "bank_code").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;
                    dsList.FindTextBox(0, "expense_accid").Enabled = false;
                    dsList.FindTextBox(0, "pay_name").Enabled = true;
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
                    dsList.DATA[0].bank_code = "";
                    dsList.DATA[0].branch_code = "";
                    dsList.DATA[0].expense_accid = "";
                }
                else if (ls_moneytype == "TRN" || ls_moneytype == "CDF" )
                {
                    dsList.FindDropDownList(0, "bank_code").Enabled = false;//เปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;
                    dsList.FindTextBox(0, "expense_accid").Enabled = true;
                    dsList.FindTextBox(0, "pay_name").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด
                    dsList.DATA[0].bank_code = "";
                    dsList.DATA[0].branch_code = "";

                }
                else if (ls_moneytype == "CBT" || ls_moneytype == "CBO" || ls_moneytype == "TBK")
                {
                    dsList.FindDropDownList(0, "bank_code").Enabled = true;//เปิด
                    dsList.FindDropDownList(0, "branch_code").Enabled = true;
                    dsList.FindTextBox(0, "expense_accid").Enabled = true;
                    dsList.FindTextBox(0, "pay_name").Enabled = false;//ปิด
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด

                }
                else if (ls_moneytype == "CHQ" || ls_moneytype == "DRF" || ls_moneytype == "BEX")
                {
                    dsList.FindDropDownList(0, "bank_code").Enabled = true;//เปิด
                    dsList.DATA[0].branch_code = "";
                    dsList.FindDropDownList(0, "branch_code").Enabled = false;//ปิด
                    dsList.FindTextBox(0, "expense_accid").Enabled = true;
                    dsList.FindTextBox(0, "pay_name").Enabled = true;
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;//เปิด

                }
                else
                {
                    dsList.DATA[0].bank_code = "";
                    dsList.FindDropDownList(0, "bank_code").Enabled = true;
                    dsList.FindDropDownList(0, "branch_code").Enabled = true;
                    dsList.FindTextBox(0, "expense_accid").Enabled = true;
                    dsList.FindTextBox(0, "pay_name").Enabled = true;
                    dsList.DATA[0].tofrom_accid = "";
                    dsList.FindDropDownList(0, "tofrom_accid").Enabled = true;
                    dsList.DATA[0].bank_code = "";
                    dsList.DATA[0].branch_code = "";
                    dsList.DATA[0].expense_accid = "";
                }
                dsList.DATA[0].tofrom_accid = "";
                dsList.FindDropDownList(0, "tofrom_accid").Enabled = false;//ปิด
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostBranch)
            {
                string ls_branchcode = dsList.DATA[0].bank_code.Trim();
                //ls_branchcode = ls_branchcode.Substring(0, 3);
                dsList.DdBranch(ls_branchcode);
            }
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}