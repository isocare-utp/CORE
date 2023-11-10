using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNFinance;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loancredit : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private n_financeClient fin;
        private String pbl = "sl_loancredit.pbl";
        protected String jsPostMember;
        protected String jsPostLnrcvList;
        protected String jsGetexpensememno;
        protected String jsPostCancelList;
        protected String jsExpenseCode;
        protected String jsExpenseBank;
        protected String jsExpensebankbrRetrieve;
        public void InitJsPostBack()
        {
            jsExpenseCode = WebUtil.JsPostBack(this, "jsExpenseCode");
            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsExpensebankbrRetrieve = WebUtil.JsPostBack(this, "jsExpensebankbrRetrieve");
            jsGetexpensememno = WebUtil.JsPostBack(this, "jsGetexpensememno");
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostLnrcvList = WebUtil.JsPostBack(this, "jsPostLnrcvList");
            jsPostCancelList = WebUtil.JsPostBack(this, "jsPostCancelList");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {

                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_listDet.RestoreContext();

                }
                catch { }

            }

            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_listDet.InsertRow(0);

            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsPostLnrcvList")
            {
                JsPostLnrcvList();
            }
            else if (eventArg == "jsPostCancelList")
            {
                JsPostCancelList();
            }
            else if (eventArg == "jsGetexpensememno")
            {
                JsGetexpensememno();
            }
            else if (eventArg == "jsExpensebankbrRetrieve")
            {
                JsExpensebankbrRetrieve();
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();

            }
            else if (eventArg == "jsExpenseCode")
            {
                dw_listDet.SetItemString(1, "loanrcv_bank", "");
                dw_listDet.SetItemString(1, "loanrcv_branch", "");
                dw_listDet.SetItemString(1, "loanrcv_accid", "");
                JsExpenseCode();
            }

        }

        public void SaveWebSheet()
        {
           
            
            string loan_code, loanbank, loanbranch, loanaccid, contcredit_no ;
            Decimal loancreditbal_amt = 0,contcredit_status=0;
            try
            {
                try
                {
                    contcredit_status = dw_listDet.GetItemDecimal(1, "contcredit_status");

                }
                catch { contcredit_status = 0; }
                if (contcredit_status == 1)
                {
                    try
                    {
                        contcredit_no = dw_listDet.GetItemString(1, "contcredit_no");

                    }
                    catch { contcredit_no = ""; }
                    try
                    {
                        loan_code = dw_listDet.GetItemString(1, "loanrcv_code");

                    }
                    catch { loan_code = ""; }
                    try
                    {
                        loanbank = dw_listDet.GetItemString(1, "loanrcv_bank");
                    }
                    catch { loanbank = ""; }
                    try
                    {
                        loanbranch = dw_listDet.GetItemString(1, "loanrcv_branch");
                    }
                    catch { loanbranch = ""; }
                    try
                    {
                        loanaccid = dw_listDet.GetItemString(1, "loanrcv_accid");
                    }
                    catch { loanaccid = ""; }

                    try
                    {
                        loancreditbal_amt = dw_listDet.GetItemDecimal(1, "loancreditbal_amt");
                    }
                    catch { loancreditbal_amt = 0; }


                    string ls_sqlexc = @"  update	LNCONTCREDIT      set		LOANRCV_CODE	= '" + loan_code.Trim() + @"', 
		                LOANRCV_BANK	= '" + loanbank.Trim() + "',  LOANRCV_BRANCH		= '" + loanbranch.Trim() + @"' ,
                    LOANRCV_ACCID = '" + loanaccid.Trim() + "'  where	( contcredit_no		= '" + contcredit_no.Trim() + "' ) ";
                    try
                    {
                        //sql = WebUtil.SQLFormat(sql, arr);

                        int sql_q = WebUtil.ExeSQL(ls_sqlexc);
                        //ส่งค่า start_membgroup ไปให้ Stored Procedures ชื่อ SP_RPT_COLL_DETAIL
                        //ta.Exe(ls_sqlexc);

                        //ta.Commit(true);
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");

                        //ta.Close();
                    }
                    catch (Exception ex)
                    { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการ บันทึกข้อมูล " + ex); }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สัญญาเงินกู้หลักนี้ได้ทำการยกเลิกหรือปิดไปแล้ว ไม่สามารถแก้ไขได้อีก");
                }
            }


            catch (Exception ex) { }
        
        }

        public void WebSheetLoadEnd()
        {
            //if (dw_list.RowCount < 1)
            //{
            //    dw_list.InsertRow(0);
            //}
            //if (dw_main.RowCount > 1)
            //{
            //    dw_main.DeleteRow(0);
            //    dw_list.DeleteRow(0);
            //}
            //if (dw_listDet.RowCount > 1)
            //{
            //    dw_listDet.DeleteRow(0);
            //}
            //dw_listDet.SaveDataCache();
            //dw_main.SaveDataCache();
            //dw_list.SaveDataCache();

            DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_code", pbl, null);

            //DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_bank_1", pbl, null);
            //DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_branch_1", pbl, "034");  

        }
        private void JsExpensebankbrRetrieve()
        {
            try
            {

                String bankCode;
                try { bankCode = dw_listDet.GetItemString(1, "loanrcv_branch").Trim(); }
                catch { bankCode = "034"; }
                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_branch_1", "sl_loancredit.pbl", bankCode);

            }
            catch { }


        }
        private void JsExpenseBank()
        {
            try
            {

                //wa
                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_bank_1", pbl, null);
                String bankCode;
                try { bankCode = dw_listDet.GetItemString(1, "loanrcv_bank").Trim(); }
                catch { bankCode = "034"; }
                String bankbranch;
                try { bankbranch = dw_listDet.GetItemString(1, "loanrcv_branch").Trim(); }
                catch { bankbranch = "0000"; }


                dw_listDet.SetItemString(1, "loanrcv_branch", "");

                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_branch_1", "sl_loancredit.pbl", bankCode);


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void JsExpenseCode()
        {
            //str_itemchange strList = new str_itemchange();
            //strList = WebUtil.nstr_itemchange_session(this);
            string expendCode = "";
            try
            {
                expendCode = dw_listDet.GetItemString(1, "loanrcv_code");
            }
            catch { }
            if (expendCode == null || expendCode == "") return;

            if ((expendCode == "CHQ") || (expendCode == "TRN") || (expendCode == "CBT") || (expendCode == "DRF") || (expendCode == "TBK") ||
               (expendCode == "CBO") || (expendCode == "SAL") || (expendCode == "TAG") || (expendCode == "TDF") || (expendCode == "TRS") ||
                (expendCode == "DRF") || (expendCode == "TMT") || (expendCode == "KOT"))
            {

                //ฝั่งธนาคาร



                dw_listDet.Modify("t_9.visible =1");
                dw_listDet.Modify("loanrcv_bank.visible =1");
                dw_listDet.Modify("t_10.visible =1");
                dw_listDet.Modify("loanrcv_bank_1.visible =1");

                dw_listDet.Modify("t_11.visible =1");
                dw_listDet.Modify("loanrcv_branch.visible =1");

                dw_listDet.Modify("loanrcv_branch_1.visible =1");
                dw_listDet.Modify("loanrcv_accid.visible =1");


                try
                {

                    DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_bank_1", "sl_loancredit.pbl", null);
                    //DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment_cen.pbl", "006");
                    //DataWindowChild dwExpenseBranch = dw_main.GetChild("expense_branch_1");
                    //DataWindowChild dwExpenseBank = dw_main.GetChild("expense_bank_1");
                    // DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                    // JsGetexpensememno();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((expendCode == "CSH") || (expendCode == "BEX") || (expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {

                //ฝั่งธนาคาร


                dw_listDet.Modify("t_9.visible =0");
                dw_listDet.Modify("loanrcv_bank.visible =0");
                dw_listDet.Modify("t_10.visible =0");
                dw_listDet.Modify("loanrcv_bank_1.visible =0");

                dw_listDet.Modify("t_11.visible =0");
                dw_listDet.Modify("loanrcv_branch.visible =0");

                dw_listDet.Modify("loanrcv_branch_1.visible =0");
                dw_listDet.Modify("loanrcv_accid.visible =0");

            }
            if ((expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {
                //if ((strList.xml_main == null) || (strList.xml_main == ""))
                //{
                //    //strList.xml_main = dw_main.Describe("DataWindow.Data.XML");
                //    //strList.xml_main = shrlonService.ReCalFee(state.SsWsPass, strList.xml_main);
                //    //นำเข้าข้อมูลหลัก
                //    //dw_main.Reset();
                //    //dw_main.ImportString(strList.xml_main, FileSaveAsType.Xml);
                //    ////DwUtil.ImportData(strList.xml_main, dw_main, tDwMain, FileSaveAsType.Xml);
                //    //if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);
                //}
            }
            if (expendCode == "TRN")
            {
                JsGetexpensememno();
            }
        }
        private void JsGetexpensememno()
        {
            try
            {
                string memno = dw_main.GetItemString(1, "member_no");
                string strsql = @"select expense_code, expense_bank, expense_branch, expense_accid 
                                        from mbmembmaster where member_no = '" + memno + "'";
                try
                {
                    Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                    if (dtloanrcv.GetRowCount() <= 0)
                    {

                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + memno);
                    }
                    if (dtloanrcv.Next())
                    {

                        string loanrcv_code = dtloanrcv.GetString("expense_code");
                        string loanrcv_bank = dtloanrcv.GetString("expense_bank");
                        string loanrcv_branch = dtloanrcv.GetString("expense_branch");
                        string loanrcv_accid = dtloanrcv.GetString("expense_accid");

                        if (loanrcv_code != null)
                        {
                            dw_listDet.SetItemString(1, "loanrcv_code", loanrcv_code);
                            dw_listDet.SetItemString(1, "loanrcv_bank", loanrcv_bank);
                            dw_listDet.SetItemString(1, "loanrcv_branch", loanrcv_branch);
                            dw_listDet.SetItemString(1, "loanrcv_accid", loanrcv_accid);
                        }
                        else
                        {
                            dw_listDet.SetItemString(1, "loanrcv_code", "TRN");
                        }



                        JsExpensebankbrRetrieve();


                    }
                    //JsExpenseBank();
                }
                catch { }

            }
            catch
            {
            }

        }
        public void JsPostMember()
        {
            string member_no = WebUtil.MemberNoFormat(Hfmember_no.Value.ToString());
            DwUtil.RetrieveDataWindow(dw_main, pbl, null, member_no);
            DwUtil.RetrieveDataWindow(dw_list, pbl, null, member_no);
            if (dw_listDet.RowCount > 0)
            {
                dw_listDet.Reset();
                dw_listDet.InsertRow(0);
            }
            try
            {
                HflnStatus.Value = "0";
                string sql = @" 
                         select droploanall_flag from mbmembmaster where coop_id = {0} and member_no = {1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    decimal droploanall_flag = dt.GetDecimal("droploanall_flag");
                    if (droploanall_flag == 1)
                    {
                        HflnStatus.Value = "1";
                    }
                }
            }
            catch { }
        }
        public void JsPostLnrcvList()
        {
            String contcredit_no;
            int RowNo = Convert.ToInt32(HfContNo.Value);

            contcredit_no = dw_list.GetItemString(RowNo, "contcredit_no");

            DwUtil.RetrieveDataWindow(dw_listDet, pbl, null, contcredit_no);
            if (dw_listDet.RowCount <= 0)
            {
                dw_listDet.Reset();
                dw_listDet.InsertRow(0);
            }
            else
            {
                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_code", pbl, null);

                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_bank_1", pbl, null);

                DwUtil.RetrieveDDDW(dw_listDet, "loanrcv_branch_1", pbl, dw_listDet.GetItemString(1, "loanrcv_bank").Trim());
            }

        }
        public void JsPostCancelList()
        {

            string ls_entryid = state.SsUsername;
            DateTime ldtm_today = state.SsWorkDate;
            string contcredit_no = dw_listDet.GetItemString(1, "contcredit_no");

            string ls_sqlexc = @"  update	lncontcredit      set		contcredit_status	= -9, 
		                cancel_id	= '" + ls_entryid + "',  cancel_date		= to_date('" + ldtm_today.ToString("yyyy-MM-dd HH:mm:ss", WebUtil.EN) +
                    @"', 'yyyy-mm-dd hh24:mi:ss')   where	( contcredit_no		= '" + contcredit_no + "' ) ";

            //Sta ta = new Sta(state.SsConnectionString);

            try
            {
                //sql = WebUtil.SQLFormat(sql, arr);
                int sql_q = WebUtil.ExeSQL(ls_sqlexc);
                //ส่งค่า start_membgroup ไปให้ Stored Procedures ชื่อ SP_RPT_COLL_DETAIL
                //ta.Exe(ls_sqlexc);

                //ta.Commit(true);
                LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกวงเงินเรียบร้อย");

                //ta.Close();
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการ ยกเลิกวงเงิน " + ex); }
        }
    }
}