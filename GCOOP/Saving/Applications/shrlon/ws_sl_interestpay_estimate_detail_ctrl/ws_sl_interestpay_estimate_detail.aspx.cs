using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl
{
    public partial class ws_sl_interestpay_estimate_detail_ctrl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostOperateFlag { get; set; }
        [JsPostBack]
        public string PostOperateDate { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                of_initslippayin();              
            }
            else if (eventArg == PostOperateFlag)
            {
                int row = dsDetail.GetRowFocus();
                decimal operate_flag = dsDetail.DATA[row].OPERATE_FLAG;
                decimal principal_payamt = dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                decimal interest_payamt = dsDetail.DATA[row].INTEREST_PAYAMT;
                decimal item_payamt = dsDetail.DATA[row].ITEM_PAYAMT;
                decimal item_balance = dsDetail.DATA[row].ITEM_BALANCE;
                decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;

                if (operate_flag == 1)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = bfshrcont_balamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = dsDetail.DATA[row].COMPUTE1;
                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_PAYAMT = 0;
                    dsDetail.DATA[row].INTEREST_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
            }
            else if (eventArg == PostOperateDate)
            {
                DateTime operate_date = dsMain.DATA[0].OPERATE_DATE; 
                string xml_sliplon = dsDetail.ExportXml();


                try
                {
                     
                    long  ll_return = wcf.NShrlon.of_initslippayin_calint(state.SsWsPass, ref xml_sliplon, "PX", operate_date);
                   
                    // dsMain.ResetRow();
                    dsDetail.ResetRow();
                    dsDetail.ImportData(xml_sliplon);

                    int row = dsDetail.GetRowFocus();
                    decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = bfshrcont_balamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = dsDetail.DATA[row].COMPUTE1;
                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            for (int i = 0; i < dsDetail.RowCount; i++)
            {
                if (dsDetail.DATA[i].OPERATE_FLAG == 1)
                {
                    dsDetail.FindTextBox(i, dsDetail.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.COMPUTE1Column).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_BALANCEColumn).ReadOnly = true;

                }
                else
                {
                    dsDetail.FindTextBox(i, dsDetail.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.COMPUTE1Column).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_BALANCEColumn).ReadOnly = true;

                }
            }
        }

        public void calItemPay()
        {
            int row = dsDetail.RowCount;


            decimal slip_amt = 0;
            for (int i = 0; i < row; i++)
            {
                decimal item_payamt = dsDetail.DATA[i].ITEM_PAYAMT;
                slip_amt += item_payamt;
            }

            dsMain.DATA[0].SLIP_AMT = slip_amt;

        }

        private void of_initslippayin()
        {
            try
            {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                string mem_no = String.Format("{0:00000000}", Convert.ToDecimal(member_no));

                str_slippayin slip = new str_slippayin();
                slip.member_no = mem_no;
                slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                slip.sliptype_code = "px";
                slip.memcoop_id = state.SsCoopId;
        
                wcf.NShrlon.of_initslippayin(state.SsWsPass, ref slip);
                dsMain.ImportData(slip.xml_sliphead);

 
                try
                {
                    dsDetail.ImportData(slip.xml_sliplon);
                   
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                 
            }

        }
    }
}