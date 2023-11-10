using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using CoreSavingLibrary.WcfNKeeping;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl
{
    public partial class ws_fin_pay_moneyreturn : PageWebSheet,WebSheet
    {
        [JsPostBack]
        public string PostInitList { get; set; }

        private n_keepingClient keepingService;


        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            //dsProcess.InitDsProcess(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                
                try
                {
                    dsMain.DDStartMembgroup();
                    dsMain.DDEndMembgroup();
                    dsMain.DATA[0].START_MEMBGROUP = "10000000000000";
                    dsMain.DATA[0].END_MEMBGROUP = "XXXXXXXXXXXXXX";
                    dsMain.DATA[0].ITEMTYPE_CODE = "%";

                    dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                    dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                    //string ls_recvperiod = "";
                    //String sql = @"select max(recv_period) from kpmastreceive";
                    //Sdt dt = WebUtil.QuerySdt(sql);
                    //if (dt.Next())
                    //{
                    //    ls_recvperiod = dt.GetString("max(recv_period)");
                    //    dsProcess.DATA[0].RECV_PERIOD = ls_recvperiod;
                    //}
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInitList) {
                try
                {
                    string member_no = dsMain.DATA[0].MEMBER_NO.Trim();
                    string salary_id = dsMain.DATA[0].SALARY_ID.Trim();
                    string s_membgroup = dsMain.DATA[0].START_MEMBGROUP.Trim();
                    string e_membgroup = dsMain.DATA[0].END_MEMBGROUP.Trim();
                    string bank_code = dsMain.DATA[0].BANK_CODE.Trim();
                    string item_type = dsMain.DATA[0].ITEMTYPE_CODE.Trim();

                    if (member_no == null || member_no == "")
                    {
                        member_no = "%";
                    }
                    else {
                        member_no = WebUtil.MemberNoFormat(member_no);
                    }
                    if (salary_id == null || salary_id == "")
                    {
                        salary_id = "%";
                    }
                    if (bank_code == null || bank_code == "")
                    {
                        bank_code = "%";
                    }
                    if (item_type == null || item_type == "")
                    {
                        item_type = "%";
                    }
                    dsList.ResetRow();
                    //dsList.RetrieveList(member_no, salary_id, s_membgroup, e_membgroup, bank_code, item_type);
                    dsList.RetrieveListLite(member_no, salary_id, s_membgroup, e_membgroup);
                    dsList.DDBank();
                }
                catch (Exception ex){ }
            }
            
            
        }

        public void SaveWebSheet()
        {
            #region old
            //try
            //{
            //    //DateTime slip_date = dsMain.DATA[0].SLIP_DATE;
            //    //DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
            //    //DateTime mrcreate_sdate = dsMain.DATA[0].START_DATE;
            //    //DateTime mrcreate_edate = dsMain.DATA[0].END_DATE;
            //    string recv_period = dsMain.DATA[0].PERIOD;
            //    decimal proc_type = 60; // 1=ทั้งหมด 20=ประเภทสมาชิก  40=ตามสังกัด  60=ตามทะเบียนสมาชิก
            //    string proc_txt = "";
            //    decimal mrcreate_status = 0;
            //    decimal mrpost_status = 1;

            //    //dsProcess.DATA[0].SLIP_DATE = slip_date;
            //    //dsProcess.DATA[0].OPERATE_DATE = operate_date;
            //    //dsProcess.DATA[0].MRCREATE_SDATE = mrcreate_sdate;
            //    //dsProcess.DATA[0].MRCREATE_EDATE = mrcreate_edate;
            //    //dsProcess.DATA[0].RECV_PERIOD = recv_period;
            //    dsProcess.DATA[0].PROC_TYPE = proc_type;
            //    proc_txt = GetProcTxtMembIn();
            //    dsProcess.DATA[0].PROC_TEXT = proc_txt;
            //    dsProcess.DATA[0].MRCREATE_STATUS = mrcreate_status;
            //    dsProcess.DATA[0].MRPOST_STATUS = mrpost_status;


            //    try
            //    {
            //        String xml_option = dsProcess.ExportXml();
            //        String xml_report_summary = "";

            //        str_money_return_xml astr_xml = new str_money_return_xml();
            //        astr_xml.xml_option = xml_option;
            //        astr_xml.xml_report_summary = xml_report_summary;
            //        keepingService.RunMoneyReturn(state.SsWsPass, ref astr_xml, state.SsApplication, state.CurrentPage);
            //        Hdprocess.Value = "true";
            //    }
            //    catch (SoapException ex)
            //    {
            //        LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            //    }
            //    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            //}
            //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            #endregion

            try
            {
                string test_text = "";
                ExecuteDataSource exed = new ExecuteDataSource(this);
                for (int i = 0; i < dsList.RowCount; i++) {
                    if (dsList.DATA[i].OPERATE_FLAG == 1) {
                        string sql = "";
                        decimal old_wrtbal = 0;
                        sql = "select wrtfund_balance from mbmembmaster  where member_no={0} and coop_id={1}";
                        sql = WebUtil.SQLFormat(sql, dsList.DATA[i].MEMBER_NO, state.SsCoopId);
                        Sdt dtWrt = WebUtil.QuerySdt(sql);
                        if (dtWrt.Next())
                        {
                            old_wrtbal = dtWrt.GetDecimal("wrtfund_balance");

                        }
                        exed.SQL.Add(GenSqlInsertSlippayout(i));
                        exed.SQL.Add(GenSqlUpdateMoneyreturn(i));
                        exed.SQL.Add(GenSqlInsertWrt(i, old_wrtbal));
                        exed.SQL.Add(GenSqlUpdateMembmaster(i, old_wrtbal));
                        //test_text += GenSqlInsertSlippayout(i)+"\n";
                        //test_text += GenSqlUpdateMoneyreturn(i)+"\n";
                        //test_text += GenSqlInsertWrt(i,old_wrtbal)+"\n";
                        //test_text += GenSqlUpdateMembmaster(i, old_wrtbal) + "\n";
                    }
                }
                //LtServerMessage.Text = test_text;
                exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex) { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            }
        }

        public void WebSheetLoadEnd()
        {
        }


        public string GetProcTxtMembIn() {
            string proc_txt = "";
            decimal cnt = 0;
            try
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 1)
                    {
                        if (cnt >= 1)
                        {
                            proc_txt += ",";
                        }
                        proc_txt += WebUtil.MemberNoFormat(dsList.DATA[i].MEMBER_NO);
                        cnt++;
                    }

                }
            }catch(Exception ex){}
            return proc_txt;
        }

        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        public string GenSqlInsertSlippayout(int i) {
            string sql = "";
            try
            {
                string slippayout_no = Get_NumberDOC("SLSLIPPAYOUT");
                string sliptype_code = dsList.DATA[i].RETURNITEMTYPE_CODE;
                string member_no = dsList.DATA[i].MEMBER_NO;
                string coop_id = state.SsCoopId;
                DateTime slip_date = dsMain.DATA[0].SLIP_DATE;
                DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
                string loancontract_no = dsList.DATA[i].LOANCONTRACT_NO;
                decimal payout_amt = dsList.DATA[i].RETURN_AMOUNT;
                string moneytype_code = dsList.DATA[i].MONEYTYPE_CODE;
                string expense_bank = dsList.DATA[i].BANK_CODE;
                string expense_branch = dsList.DATA[i].BANK_BRANCH;
                string expense_accid = dsList.DATA[i].BANK_ACCID;
                sql = @"INSERT INTO SLSLIPPAYOUT(
                            COOP_ID,            PAYOUTSLIP_NO,          MEMCOOP_ID,         MEMBER_NO,
                            DOCUMENT_NO,        SLIPTYPE_CODE,          SLIP_DATE,          OPERATE_DATE,
                            SHRLONTYPE_CODE,    RCVFROMREQCONT_CODE ,   LOANCONTRACT_NO,    LOANREQUEST_DOCNO,
                            RCVPERIOD_FLAG,     RCV_PERIOD,             PAYOUT_AMT,         PAYOUTCLR_AMT, 
                            PAYOUTNET_AMT,      BFPERIOD,               BFLOANAPPROVE_AMT,  BFSHRCONT_BALAMT, 
                            BFWITHDRAW_AMT,     BFINTEREST_ARREAR ,     BFCONTLAW_STATUS,   PRNCALINT_AMT, 
                            MONEYTYPE_CODE,     EXPENSE_BANK,           EXPENSE_BRANCH,     EXPENSE_ACCID,
                            ACCID_FLAG,         TOFROM_ACCID,           SLIP_STATUS,        SLIPCLEAR_NO,
                            MEMBGROUP_CODE,     ENTRY_ID,               ENTRY_DATE,         ENTRY_BYCOOPID,
                            POSTTOVC_FLAG,      POST_TOFIN,             RETURNETC_AMT) 
                            VALUES (
                            {0},                {1},                    {2},                {3},
                            {4},                {5},                    {6},                {7},
                            {8},                {9},                    {10},               {11},
                            {12},               {13},                   {14},               {15},
                            {16},               {17},                   {18},               {19},
                            {20},               {21},                   {22},               {23},
                            {24},               {25},                   {26},               {27},
                            {28},               {29},                   {30},               {31},
                            {32},               {33},                   {34},               {35},
                            {36},               {37},                   {38})";

                sql = WebUtil.SQLFormat(sql,
                                coop_id, slippayout_no, coop_id, member_no,
                                "", sliptype_code, slip_date, operate_date,
                                "", "", loancontract_no, "",
                                0, 0, payout_amt, 0,
                                payout_amt, 0, 0, 0,
                                0, 0, 1, 0,
                                moneytype_code, expense_bank, expense_branch, expense_accid,
                                0, "", 1, "",
                                "", state.SsUsername, state.SsWorkDate, coop_id,
                                0, 0, payout_amt);
            }catch {
                sql ="";
            }
            return sql;
        }

        public string GenSqlUpdateMoneyreturn(int i) {
            string sql = "";
            try
            {
                sql = "update mbmoneyreturn set return_status =1  where coop_id={0} and member_no={1} and seq_no={2} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsList.DATA[i].MEMBER_NO, dsList.DATA[i].SEQ_NO);
            }
            catch { }

            return sql;

        
        }

        public string GenSqlInsertWrt(int i,decimal wrtbal_i) {
            string sql = "";
            decimal seq=0;
            string sqlSeq = "";
            decimal new_bal = 0;
            try
            {
                sqlSeq = "select nvl(max(seq_no),0) as max_seqno from wrtfundstatement where member_no = {0} and coop_id={1} ";
                sqlSeq = WebUtil.SQLFormat(sqlSeq, dsList.DATA[i].MEMBER_NO, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sqlSeq);
                if (dt.Next())
                {
                    seq = dt.GetDecimal("max_seqno") + 1;
                }
                else
                {
                    seq = 1;
                }
                new_bal = wrtbal_i - dsList.DATA[i].RETURN_AMOUNT;


                sql = @"INSERT INTO wrtfundstatement (
                        coop_id,            member_no,      seq_no,             operate_date,
                        wrtitemtype_code,   wrtfund_amt,    wrtfund_balance,    ref_contno,
                        moneytype_code,     item_status,    entry_id,           entry_date,
                        ref_docno) 
                        values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                sql = WebUtil.SQLFormat(sql,
                        state.SsCoopId,                 dsList.DATA[i].MEMBER_NO,       seq,                dsMain.DATA[0].OPERATE_DATE,
                        "RWT",                          dsList.DATA[i].RETURN_AMOUNT,   new_bal,            dsList.DATA[i].LOANCONTRACT_NO,
                        dsList.DATA[i].MONEYTYPE_CODE,  1,                              state.SsUsername,   state.SsWorkDate,
                        "");
            }
            catch {
                sql = "";
            }
            return sql;
        }

        public string GenSqlUpdateMembmaster(int i, decimal wrtbal_i) {
            string sql = "";
            decimal new_bal = 0;
            try
            {
                new_bal = wrtbal_i - dsList.DATA[i].RETURN_AMOUNT;
                sql = "update mbmembmaster set wrtfund_balance ={2}  where coop_id={0} and member_no={1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsList.DATA[i].MEMBER_NO, new_bal);

            }
            catch {
                sql = "";
            }
            return sql;
        }
    }
}