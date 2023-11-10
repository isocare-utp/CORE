using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Drawing;

namespace Saving.Applications.shrlon.ws_sl_auditloan_ctrl
{
    public partial class ws_sl_auditloan : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostLoanContractNo { get; set; }

        public void InitJsPostBack()
        {            
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsDetail.InitDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                /*
                try { WebUtil.ExeSQL("Alter table dpdeptmaster add bank_accid varchar2(15)"); }
                catch { }
                try { WebUtil.ExeSQL("Alter table lncontmaster add expense_accid varchar2(15)"); }
                catch { }
                 */
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsDetail.ResetRow();

                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                dsList.RetrieveList(member_no);

                decimal cp_sumprincipal = 0;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    cp_sumprincipal += dsList.DATA[i].principal_balance;
                }
                dsList.principal_balance.Text = cp_sumprincipal.ToString("#,##0.00");
            }
            else if (eventArg == PostLoanContractNo)
            {
                int r = dsList.GetRowFocus();

                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if(i != r){
                        dsList.FindTextBox(i, "loantype_code").BackColor = Color.White;
                        dsList.FindTextBox(i, "loancontract_no").BackColor = Color.White;
                        dsList.FindTextBox(i, "principal_balance").BackColor = Color.White;
                    }
                }
                dsList.FindTextBox(r, "loantype_code").BackColor = Color.SkyBlue;
                dsList.FindTextBox(r, "loancontract_no").BackColor = Color.SkyBlue;
                dsList.FindTextBox(r, "principal_balance").BackColor = Color.SkyBlue;

                string loancontract_no = dsList.DATA[r].loancontract_no;
                dsDetail.RetrieveDetail(loancontract_no);
                dsDetail.Ddloantype();
                dsDetail.DdLoanobjective();
            }
        }

        public void SaveWebSheet()
        {
            dsDetail.DATA[0].UPDATE_BYENTRYID = state.SsUsername;
            dsDetail.DATA[0].UPDATE_BYENTRYIP = state.SsClientIp;
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsDetail, ExecuteType.Update);
                exe.Execute();
                try
                {

                    WebUtil.ExeSQL("update lncontmaster set contract_status=contract_status where loancontract_no='" + dsDetail.DATA[0].LOANCONTRACT_NO + "' ");
                }
                catch { }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsMain.ResetRow();
                dsList.ResetRow();
                dsDetail.ResetRow();
                dsList.principal_balance.Text = "";
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
            catch (Exception ex) { 
                
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex);

                this.SetOnLoadedScript(" parent.Setfocus();");

            
            }

        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}