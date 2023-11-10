using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_process_moneyreturn_ctrl
{
    public partial class ws_kp_process_moneyreturn : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSetMoneyreturn { get; set; }
        [JsPostBack]
        public string InitIntreturn { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }
        

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                string recv_period = "";
                string sqlselect = @"select max(recv_period) as recv_period from kptempreceive where coop_id = {0}";
                sqlselect = WebUtil.SQLFormat(sqlselect, state.SsCoopId);
                Sdt dtContmas = WebUtil.QuerySdt(sqlselect);
                while (dtContmas.Next())
                {
                    recv_period = dtContmas.GetString("recv_period");
                }

                dsMain.DATA[0].RECV_PERIOD = recv_period;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                dsMain.DdStartMembgroup();
                dsMain.DdEndMembgroup();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSetMoneyreturn) {
                try
                {
                    SetMoneyreturnNew();
                }catch(Exception ex){
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else if (eventArg == InitIntreturn)
            {
                string recv_period = dsMain.DATA[0].RECV_PERIOD;
                string smembgroup_code = dsMain.DATA[0].SMEMBGROUP_CODE.Trim();
                string emembgroup_code = emembgroup_code = dsMain.DATA[0].EMEMBGROUP_CODE.Trim();

                dsList.RetrieveDetail(recv_period, smembgroup_code, emembgroup_code);
            }
        }
                
        public void SaveWebSheet()
        {
            string smembgroup_code = "", emembgroup_code = "", recv_period = "", Updatemoneyreturn = "", sqlMbReturn = "";
            string loancontract_no = "", dept_accno = "";
            decimal interest_return = 0;
            smembgroup_code = dsMain.DATA[0].SMEMBGROUP_CODE.Trim();
            emembgroup_code = dsMain.DATA[0].EMEMBGROUP_CODE.Trim();

            recv_period = dsMain.DATA[0].RECV_PERIOD.Trim();
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                //dsList.RetrieveDetail(recv_period, smembgroup_code, emembgroup_code);
                //Sdt dtMbReturn = WebUtil.QuerySdt(sqlMbReturn);
                //while (dtMbReturn.Next())
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    loancontract_no = dsList.DATA[i].LOANCONTRACT_NO;
                    interest_return = dsList.DATA[i].INT_RETURN;
                    dept_accno = dsList.DATA[i].DEPTACCOUNT_NO;

                    Updatemoneyreturn = @"update	mbmoneyreturn
			        set		deptaccount_no	= {0},  
                            return_amount   = {1}
			        where	coop_id				= {2}
			        and		loancontract_no	    = {3}
                    and     rtrim(ltrim(ref_docno))     = {4}";

                    Updatemoneyreturn = WebUtil.SQLFormat(Updatemoneyreturn, dept_accno, interest_return, state.SsCoopId, loancontract_no, recv_period);
                    exed1.SQL.Add(Updatemoneyreturn);
                    exed1.Execute();
                    exed1.SQL.Clear();
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsList.ResetRow();
                dsList.RetrieveDetail(recv_period, smembgroup_code, emembgroup_code);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
               
        public void WebSheetLoadEnd()
        {

        }


        public void SetMoneyreturnNew()
        {
            try
            {
                int year = 0;
                string member_no = "", recv_period = "",  loancontract_no = "";
                string sqlMbReturn = "", sqlInsInt = "", sqlIns = "";
                decimal interest_return = 0, seq = 0, cnt = 0;
                string dept_accno = "", UpdateMbmoneyreturn = "";
                string smembgroup_code = "", emembgroup_code = "";
                smembgroup_code = dsMain.DATA[0].SMEMBGROUP_CODE.Trim();
                emembgroup_code = dsMain.DATA[0].EMEMBGROUP_CODE.Trim();

                recv_period = dsMain.DATA[0].RECV_PERIOD.Trim();

                ExecuteDataSource exed = new ExecuteDataSource(this);
                DateTime slip_date = dsMain.DATA[0].SLIP_DATE;
                DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;

                sqlMbReturn = @"select mr.member_no as member_no,
                    dbo.ft_memname(mr.coop_id , mr.member_no ) as memb_name,
                    dbo.ft_memgrp( mr.coop_id, mb.membgroup_code ) as memb_group,
                    mr.loancontract_no as loancontract_no,
                    mr.return_amount as int_return,
                    mr.deptaccount_no as deptaccount_no,
                    mr.seq_no as seq_no,
                    substring( mr.ref_docno , 1 , 4 ) as year
                    from mbmoneyreturn mr , mbmembmaster mb
                    where mr.member_no = mb.member_no
                    and mr.coop_id = mb.coop_id
                    and mr.coop_id = {0}
                    and rtrim(ltrim(ref_docno)) = {1}
                    and mb.membgroup_code between {2} and {3}
                    and mr.return_status = 8
                    and mr.return_amount >= 100
                    order by mr.member_no , mr.loancontract_no";
                sqlMbReturn = WebUtil.SQLFormat(sqlMbReturn, state.SsCoopId, recv_period, smembgroup_code, emembgroup_code);
                Sdt dtMbReturn = WebUtil.QuerySdt(sqlMbReturn);
                while (dtMbReturn.Next())
                {

                    member_no = dtMbReturn.GetString("member_no");
                    loancontract_no = dtMbReturn.GetString("loancontract_no");
                    interest_return = dtMbReturn.GetDecimal("int_return");
                    dept_accno = dtMbReturn.GetString("deptaccount_no");
                    seq = dtMbReturn.GetDecimal("seq_no");
                    year = int.Parse(dtMbReturn.GetString("year"));

                    sqlIns = @"  INSERT INTO DPDEPTTRAN    ( coop_id , deptaccount_no, memcoop_id , member_no , system_code, tran_year , tran_date , seq_no , deptitem_amt , tran_status , lncont_no , branch_operate)  
                                VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11} )  ";

                    sqlInsInt = WebUtil.SQLFormat(sqlIns, state.SsCoopId, dept_accno, state.SsCoopId, member_no, "TRL", year, slip_date, seq, interest_return, 0, loancontract_no , "001");

                    UpdateMbmoneyreturn = @"update	mbmoneyreturn
			        set			return_status	= {0},
                                return_date     = {4},
                                return_id       = {5}
			        where	coop_id				= {1}
			        and		loancontract_no	    = {2}
                    and     rtrim(ltrim(ref_docno))     = {3}";

                    UpdateMbmoneyreturn = WebUtil.SQLFormat(UpdateMbmoneyreturn, 1, state.SsCoopId, loancontract_no, recv_period, slip_date, state.SsUsername);
                    if (interest_return > 0)
                    {
                        exed.SQL.Add(sqlInsInt);
                    }
                    exed.SQL.Add(UpdateMbmoneyreturn);
                    exed.Execute();
                    exed.SQL.Clear();
                    cnt++;
                }
                dsList.RetrieveDetail(recv_period, smembgroup_code, emembgroup_code);
                LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลข้อมูลไประบบเงินฝากสำเร็จ " + cnt + " สัญญา");
            }
            catch (Exception e) { throw e; }

        }
    }
}