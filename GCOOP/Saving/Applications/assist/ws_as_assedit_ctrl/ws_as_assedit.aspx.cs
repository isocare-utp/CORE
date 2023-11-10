using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_assedit_ctrl
{
    public partial class ws_as_assedit : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMBNo { get; set; }
        [JsPostBack]
        public string PostAssContNo { get; set; }
        [JsPostBack]
        public string PostSetAttrib { get; set; }
        [JsPostBack]
        public string PostNewSTM { get; set; }
        [JsPostBack]
        public string PostDelSTM { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsCont.InitDsCont(this);
            dsContSTM.InitDsContSTM(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMBNo)
            {
                string ls_mbno = dsMain.DATA[0].member_no;

                ls_mbno = WebUtil.MemberNoFormat(ls_mbno);

                dsMain.DATA[0].member_no = ls_mbno;

                this.of_setmbinfo(ls_mbno);
            }
            else if (eventArg == PostAssContNo)
            {
                string ls_acccontno;

                ls_acccontno = dsMain.DATA[0].asscontract_no;

                dsCont.ResetRow();
                dsContSTM.ResetRow();
                dsCont.RetrieveData(ls_acccontno);
                dsContSTM.RetrieveData(ls_acccontno);
            }
            else if (eventArg == PostSetAttrib)
            {
                this.of_setattrib();
            }
            else if (eventArg == PostNewSTM)
            {
                dsContSTM.InsertLastRow();

                Int32 li_row = dsContSTM.RowCount - 1;

                dsContSTM.DATA[li_row].COOP_ID = state.SsCoopControl;
                dsContSTM.DATA[li_row].ASSCONTRACT_NO = dsMain.DATA[0].asscontract_no;
                dsContSTM.DATA[li_row].SEQ_NO = li_row + 1;
            }
            else if (eventArg == PostDelSTM)
            {
                Int32 li_row = dsContSTM.GetRowFocus();

                dsContSTM.DeleteRow(li_row);

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsCont, ExecuteType.Update);
                exe.AddRepeater(dsContSTM);
                exe.Execute();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                return;
            }

            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
        }

        public void WebSheetLoadEnd()
        {
            this.of_setattrib();
        }

        private void of_setattrib()
        {
            string ls_money, ls_system, ls_bank, ls_memno, ls_asscode;

            ls_memno = dsMain.DATA[0].member_no;

            ls_asscode = dsCont.DATA[0].ASSISTTYPE_CODE;
            ls_money = dsCont.DATA[0].MONEYTYPE_CODE;
            ls_system = dsCont.DATA[0].SEND_SYSTEM;
            ls_bank = dsCont.DATA[0].EXPENSE_BANK;

            dsMain.DdAssContNo(ls_memno);
            dsCont.DdMoneyType();
            dsCont.DdAssistType();
            dsCont.DdAssistPay(ls_asscode);
            dsContSTM.DdAssItemCode();

            if (ls_money == "CBT" || ls_money == "CBO")
            {
                dsCont.DdBank();

                if (!string.IsNullOrEmpty(ls_bank))
                {
                    dsCont.DdBranch(ls_bank);
                }

                dsCont.FindDropDownList(0, "expense_bank").Enabled = true;
                dsCont.FindDropDownList(0, "expense_branch").Enabled = true;
                dsCont.FindTextBox(0, "expense_accid").Enabled = true;
                dsCont.FindDropDownList(0, "send_system").Enabled = false;
                dsCont.FindDropDownList(0, "deptaccount_no").Enabled = false;

                dsCont.DATA[0].SEND_SYSTEM = "";
                dsCont.DATA[0].DEPTACCOUNT_NO = "";
            }
            else if (ls_money == "TRN")
            {
                if (ls_system == "DEP")
                {
                    dsCont.DdDeptaccount(ls_memno);
                    dsCont.FindDropDownList(0, "deptaccount_no").Enabled = true;
                }
                else
                {
                    dsCont.FindDropDownList(0, "deptaccount_no").Enabled = false;
                }

                dsCont.FindDropDownList(0, "expense_bank").Enabled = false;
                dsCont.FindDropDownList(0, "expense_branch").Enabled = false;
                dsCont.FindTextBox(0, "expense_accid").Enabled = false;
                dsCont.FindDropDownList(0, "send_system").Enabled = true;

                dsCont.DATA[0].EXPENSE_BANK = "";
                dsCont.DATA[0].EXPENSE_BRANCH = "";
                dsCont.DATA[0].EXPENSE_ACCID = "";
            }
            else
            {
                dsCont.FindDropDownList(0, "expense_bank").Enabled = false;
                dsCont.FindDropDownList(0, "expense_branch").Enabled = false;
                dsCont.FindTextBox(0, "expense_accid").Enabled = false;
                dsCont.FindDropDownList(0, "send_system").Enabled = false;
                dsCont.FindDropDownList(0, "deptaccount_no").Enabled = false;

                dsCont.DATA[0].EXPENSE_BANK = "";
                dsCont.DATA[0].EXPENSE_BRANCH = "";
                dsCont.DATA[0].EXPENSE_ACCID = "";
                dsCont.DATA[0].SEND_SYSTEM = "";
                dsCont.DATA[0].DEPTACCOUNT_NO = "";
            }
        }

        private void of_setmbinfo(string as_memno)
        {
            dsMain.InitMembData(as_memno);
            dsMain.DdAssContNo(as_memno);
        }
    }
}