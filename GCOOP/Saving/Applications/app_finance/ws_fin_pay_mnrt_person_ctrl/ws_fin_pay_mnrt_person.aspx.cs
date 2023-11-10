using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_pay_mnrt_person_ctrl
{
    public partial class ws_fin_pay_mnrt_person : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostMembno { get; set; }
        [JsPostBack]
        public string JsPostBank { get; set; }
        [JsPostBack]
        public string JsPostMoneytypeCode { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].SLIPTYPE_CODE = "LRT";
                dsMain.DATA[0].MONEYTYPE_CODE = "CSH";
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DdMoneyType();
                dsMain.DdFromAccId("CSH");
                dsMain.DdBankDesc();
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostMembno)
            {




                string member_no = "";
                member_no = dsMain.DATA[0].MEMBER_NO;
                member_no = WebUtil.MemberNoFormat(member_no);
                //dsMain.RetrieveMemb(member_no);
                string sql = @"select mb.member_no,mp.prename_desc||mb.memb_name||' '||mb.memb_surname as fullname ,mg.membgroup_code||'-'||mg.membgroup_desc as membgroup
from mbmembmaster mb  ,mbucfprename mp,mbucfmembgroup mg
where mb.member_no = {0}
and mb.coop_id = {1}  
and mb.prename_code = mp.prename_code
and mb.membgroup_code = mg.membgroup_code
and mb.coop_id = mg.coop_id";
                sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].MEMBER_NO = member_no;
                    dsMain.DATA[0].FULLNAME = dt.GetString("fullname");
                    dsMain.DATA[0].MEMBGROUP = dt.GetString("membgroup");

                    SetMoneyTr(member_no);

                    dsList.RetrievePrnInt(member_no);
                }
                else
                {

                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิก");
                }
            }
            else if (eventArg == JsPostBank)
            {
                string bank = dsMain.DATA[0].EXPENSE_BANK;
                dsMain.DdBranch(bank);
            }
            else if (eventArg == JsPostMoneytypeCode) {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsMain.DdFromAccId(moneytype_code);
            }
        }

        public void SaveWebSheet()
        {
            try{
                string payoutslip_no = Get_NumberDOC("SLSLIPPAYOUT");
                string coop_id = state.SsCoopId;
                decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;
                string member_no = dsMain.DATA[0].MEMBER_NO;
                dsMain.DATA[0].PAYOUTSLIP_NO = payoutslip_no;
                dsMain.DATA[0].COOP_ID = coop_id;
                dsMain.DATA[0].MEMCOOP_ID = coop_id;
                dsMain.DATA[0].DOCUMENT_NO="KPSLIPNO";
                dsMain.DATA[0].PAYOUT_AMT = payoutnet_amt;
                dsMain.DATA[0].BFCONTLAW_STATUS = 1;
                dsMain.DATA[0].BFPAYMENT_STATUS = 1;
                dsMain.DATA[0].SLIP_STATUS = 1;
                dsMain.DATA[0].ENTRY_BYCOOPID = coop_id;

                for (int i = 0; i < dsList.RowCount; i++) {
                    decimal prn_amt = dsList.DATA[i].PRINCIPAL_PAYAMT;
                    decimal int_amt = dsList.DATA[i].INTEREST_PAYAMT;
                    dsList.DATA[i].PAYOUTSLIP_NO = payoutslip_no;
                    dsList.DATA[i].COOP_ID = coop_id;
                    dsList.DATA[i].CONCOOP_ID = coop_id;
                    dsList.DATA[i].SLIPITEMTYPE_CODE = "MRL";
                    dsList.DATA[i].ITEM_PAYAMT = prn_amt + int_amt;
                    dsList.DATA[i].BFCONTLAW_STATUS = 1;
                    dsList.DATA[i].SEQ_NO = i+1;
                    dsList.ChangeRowStatusInsert();
                }

                ExecuteDataSource exed = new ExecuteDataSource(this);
                exed.AddFormView(dsMain, ExecuteType.Insert);
                exed.AddRepeater(dsList);
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal seq_prn = dsList.DATA[i].SEQ_PRN;
                    decimal seq_int = dsList.DATA[i].SEQ_INT;
                    string cont_no = dsList.DATA[i].LOANCONTRACT_NO;
                    string sqlprn = "update mbmoneyreturn set return_status=1 where coop_id={0} and member_no={1} and loancontract_no={2} and seq_no={3} and returnitemtype_code='PRN'";
                    string sqlint = "update mbmoneyreturn set return_status=1 where coop_id={0} and member_no={1} and loancontract_no={2} and seq_no={3} and returnitemtype_code='INT'";
                    sqlprn = WebUtil.SQLFormat(sqlprn, coop_id, member_no, cont_no, seq_prn);
                    sqlint = WebUtil.SQLFormat(sqlint, coop_id, member_no, cont_no, seq_int);
                    exed.SQL.Add(sqlprn);
                    exed.SQL.Add(sqlint);
                }
                exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }catch(Exception ex ){}
            
        }

        public void WebSheetLoadEnd()
        {
        }

        public void SetMoneyTr(string member_no)
        {
            try
            {
                string sql = "select * from mbmembmoneytr where member_no ={0} and trtype_code='RET'";
                sql = WebUtil.SQLFormat(sql, member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    string moneytype_code = dt.GetString("moneytype_code");
                    string bank = dt.GetString("bank_code");
                    string branch = dt.GetString("bank_branch");
                    string accid = dt.GetString("bank_accid");
                    dsMain.DATA[0].EXPENSE_BANK = bank;
                    dsMain.DdBranch(bank);
                    dsMain.DATA[0].EXPENSE_BRANCH = branch;
                    dsMain.DATA[0].EXPENSE_ACCID = accid;
                    dsMain.DATA[0].MONEYTYPE_CODE = moneytype_code;

                    dsMain.DdFromAccId(moneytype_code);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

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
    }
}