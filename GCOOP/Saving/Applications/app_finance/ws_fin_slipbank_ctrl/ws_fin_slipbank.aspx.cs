
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.ws_fin_slipbank_ctrl
{
    public partial class ws_fin_slipbank : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostGetBank { get; set; }
        [JsPostBack]
        public string JsPostRrieveData { get; set; }
        [JsPostBack]
        public string JsPostRrieveDataFrmAccno { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.ResetRow();
                PostSetLoadBegin();
                dsMain.DDBank();                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostGetBank)
            {
                string bank_code = dsMain.DATA[0].BANK_CODE;
                dsMain.DDBank();
                dsMain.DDBankbranch(bank_code);
            }
            else if (eventArg == JsPostRrieveData)
            {
                string ls_accno = dsMain.DATA[0].ACCOUNT_NO;
                string ls_bankcode = dsMain.DATA[0].BANK_CODE;
                string ls_bankbranch = dsMain.DATA[0].BANK_BRANCH;
                dsMain.ResetRow();
                PostRetriveData(ls_accno, ls_bankcode, ls_bankbranch);
            }
            else if (eventArg == JsPostRrieveDataFrmAccno)
            {
                string ls_accno = dsMain.DATA[0].ACCOUNT_NO;
                dsMain.ResetRow();                
                dsMain.RetrieveFromAcc(ls_accno);
                if (dsMain.DATA[0].BANK_CODE.Trim().Length > 0)
                {
                    string ls_bankcode = dsMain.DATA[0].BANK_CODE;
                    dsMain.DDBank();
                    dsMain.DDBankbranch(ls_bankcode);                   
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ระบุ!"); dsMain.DATA[0].ACCOUNT_NO = ls_accno; 
                }
                PostSetLoadBegin();
            }
        }
        private void PostSetLoadBegin()
        {
            dsMain.DATA[0].COOP_ID = state.SsCoopControl;
            dsMain.DATA[0].ENTRY_DATE = DateTime.Now;
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            dsMain.DATA[0].MACHINE_ID = state.SsClientIp;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            dsMain.DATA[0].SLIP_NO = "AUTO";
        }
        private void PostRetriveData(string ls_accno, string ls_bankcode, string ls_bankbranch)
        {
            try
            {
                dsMain.RetrieveData(ls_accno, ls_bankcode, ls_bankbranch);
                if (dsMain.DATA[0].BANK_CODE.Trim().Length > 0)
                {
                    ls_bankcode = dsMain.DATA[0].BANK_CODE;
                    dsMain.DDBank();
                    dsMain.DDBankbranch(ls_bankcode);
                    PostSetLoadBegin();
                }
                else
                {
                    PostSetLoadBegin();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ระบุ!"); return;
                }
            }
            catch (Exception err)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ" + err.Message);
            }
        }
        public void SaveWebSheet()
        {
            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try {
                decimal ld_flag=0;
                string ls_coopid = state.SsCoopControl;
                string ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIPTBANK");
                string ls_tofromaccid = dsMain.DATA[0].ACCOUNT_ID;
                decimal item_amt = dsMain.DATA[0].ITEM_AMT;
                string ls_itemcode = dsMain.DATA[0].ITEM_CODE;
                String ls_itemdesc = dsMain.DATA[0].ITEM_DESC;
                if (ls_itemcode == "OCA" || ls_itemcode == "DCA")
                { 
                    ld_flag = 1;
                    if (ls_itemcode == "OCA") { ls_itemdesc = "เปิด บ/ช : " + ls_itemdesc; } else { ls_itemdesc = "ฝากเงิน : " + ls_itemdesc; }
                }
                else
                {
                    ld_flag = -1;
                    if (ls_itemcode == "CCA") { ls_itemdesc = "ปิด บ/ช : " + ls_itemdesc; } else { ls_itemdesc = "ถอนเงิน : " + ls_itemdesc; }
                }
                String[] sql_insert = financeFunction.of_save_bank(ls_coopid, ls_tofromaccid, state.SsUsername, state.SsWorkDate, item_amt, state.SsClientIp, ls_slipno, ld_flag, ls_itemcode, 1, 1, ls_itemdesc);
                if (sql_insert[0] != "") { exedinsert.SQL.Add(sql_insert[0]); }
                if (sql_insert[1] != "") { exedinsert.SQL.Add(sql_insert[1]); }
                if (sql_insert[2] != "") { exedinsert.SQL.Add(sql_insert[2]); }
                exedinsert.Execute();
                exedinsert.SQL.Clear();

                dsMain.ResetRow();
                PostSetLoadBegin();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");
            }
            catch (Exception err)
            {
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + err.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}