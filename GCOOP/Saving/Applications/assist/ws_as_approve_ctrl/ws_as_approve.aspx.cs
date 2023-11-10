using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_approve_ctrl
{
    public partial class ws_as_approve : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostChangeStatus { get; set; }
        [JsPostBack]
        public string PostSort { get; set; }
        [JsPostBack]
        public string InitAsssistpaytype { get; set; }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                Hd_sortbegin.Value = "1";
                dsMain.DATA[0].sort_type = "";
                this.of_getdata();
            }
            else if (eventArg == PostChangeStatus)
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].choose_flag == "1")
                    {
                        dsList.DATA[i].REQ_STATUS = Convert.ToDecimal(dsMain.DATA[0].req_status);
                    }
                }
            }
            else if (eventArg == PostSort)
            {
                Hd_sortbegin.Value = "2";
                this.of_getdata();
            }
            else if (eventArg == InitAsssistpaytype)
            {
                string ls_minpaytype = "", ls_maxpaytype = "";
                if (dsMain.DATA[0].assisttype_code == "")
                {
                    dsMain.ResetRow();
                    dsMain.DdAssistType();
                    dsMain.DATA[0].conclusion_no = "";
                    dsMain.DATA[0].conclusion_date = state.SsWorkDate;
                }
                else
                {
                    dsMain.AssistPayType(dsMain.DATA[0].assisttype_code, ref ls_minpaytype, ref ls_maxpaytype);
                    dsMain.DATA[0].assistpay_code1 = ls_minpaytype;
                    dsMain.DATA[0].assistpay_code2 = ls_maxpaytype;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setlabel", "setlabel()", true);
            }
        }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void SetgrpLabel()
        {
            Hd_setlabel.Value = "";
            string sqlassgrp = @" select  assisttype_code,assisttype_group from assucfassisttype
                                where assisttype_code={0}";
            sqlassgrp = WebUtil.SQLFormat(sqlassgrp, dsMain.DATA[0].assisttype_code);
            Sdt ta = WebUtil.QuerySdt(sqlassgrp);
            if (ta.Rows.Count > 0)
            {
                if (ta.Next())
                { 
                    Hd_setlabel.Value = ta.GetString("assisttype_group");
                }
            }
        }

        public void SaveWebSheet()
        {

            string ls_assisreq = "", ls_assiscont = "", ls_remark = "", ls_contractno = "", ls_moneytype = "", ls_sendsystem = "", ls_deptno = "";
            Int32 li_apvstt;
            Int32 li_result = 0, li_stmflag = 0;

            // ตรวจสอบก่อนว่ามีปรับสถานะเป็นอนุมัติหรือยัง
            for (int i = 0; i < dsList.RowCount; i++)
            {
                if (dsList.DATA[i].choose_flag == "1")
                {
                    li_apvstt = Convert.ToInt32(dsList.DATA[i].REQ_STATUS);
                    if (li_apvstt == 8)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุสถานะการอนุมัติให้ครบถ้วนด้วย");
                        return;
                    }
                }
            }

            for (int i = 0; i < dsList.RowCount; i++)
            {
                if (dsList.DATA[i].choose_flag == "1")
                {
                    ls_assisreq = dsList.DATA[i].ASSIST_DOCNO.Trim();
                    li_apvstt = Convert.ToInt32(dsList.DATA[i].REQ_STATUS);
                    ls_remark = dsMain.DATA[0].conclusion_no;
                    li_stmflag = dsList.DATA[i].stm_flag; //กรณีจ่ายต่อเนื่อง ไม่ต้องสร้างสัญญาใหม่
                    ls_contractno = dsList.DATA[i].asscontract_no;
                    ls_moneytype = dsList.DATA[i].moneytype_code;
                    ls_sendsystem = dsList.DATA[i].send_system;
                    ls_deptno = dsList.DATA[i].deptaccount_no;

                    if (li_stmflag != 1 && (ls_contractno == "" || ls_contractno == null))
                    {
                        if (li_apvstt == 1)
                        {
                            //----------------------------------------------!
                           ls_assiscont = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTCONTNO");
                        }

                        li_result = this.of_buildasscont(ls_assisreq, ls_assiscont, li_apvstt, ls_remark, li_stmflag);

                        if (li_result != 1)
                        {
                            return;
                        }
                    }
                    else if (li_stmflag == 1 && (ls_contractno == "" || ls_contractno == null))
                    {
                        if (li_apvstt == 1)
                        {
                            ls_assiscont = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTCONTNO");
                        }

                        li_result = this.of_buildasscont(ls_assisreq, ls_assiscont, li_apvstt, ls_remark, li_stmflag);

                        if (li_result != 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        li_result = this.of_Updateasscont(ls_assisreq, ls_contractno, li_apvstt, ls_remark, li_stmflag, ls_moneytype, ls_sendsystem, ls_deptno);

                        if (li_result != 1)
                        {
                            return;
                        }
                    }

                    if (dsList.DATA[i].assisttype_group == "07")
                    {
                        Int32 seq_no = 0, month = 0;
                        Decimal ldc_unitamt = 0;
                        DateTime operate_date = state.SsWorkDate;
                        string invt_id = "", ls_err = "", sqlStr = "", ls_unitcode = "", refer_slipno = "";

                        string sqlGift = @"select assist_docno , invt_id , unit_code ,sum(unit_amt)  as unit_amt from assreqmastergift where coop_id = {0} and assist_docno = {1} and slip_status = 8
                                            group by assist_docno , invt_id , unit_code ";
                        sqlGift = WebUtil.SQLFormat(sqlGift, state.SsCoopId, ls_assisreq);
                        Sdt dt1 = WebUtil.QuerySdt(sqlGift);
                        dt1.Next();
                        //ldc_unitamt = Convert.ToDecimal(dt1.GetString("unit_amt"));
                        ldc_unitamt = dt1.GetDecimal("unit_amt");
                        invt_id = dt1.GetString("invt_id");
                        ls_unitcode = dt1.GetString("unit_code");

                        try
                        {
                            string ls_sql = @" update assreqmastergift 
                            set slip_status = 1
                            where coop_id = {0} and assist_docno = {1} ";

                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopId, ls_assisreq);
                            WebUtil.ExeSQL(ls_sql);
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรายการอนุมัติของขวัญไม่สำเร็จ : " + ex);
                            return;
                        }

                        try
                        {
                            sqlGift = @"select isnull( seq_no , 0 ) as seq_no from asssumpaygift where coop_id = {0} and refer_slipno = {1} and  seq_no = ( select max(seq_no) from asssumpaygift where refer_slipno = {1} ) ";
                            sqlGift = WebUtil.SQLFormat(sqlGift, state.SsCoopId, ls_assisreq);
                            dt1 = WebUtil.QuerySdt(sqlGift);
                            dt1.Next();
                            //seq_no = Convert.ToInt32(dt1.GetString("seq_no"));
                            seq_no = dt1.GetInt32("seq_no");
                            seq_no++;
                            operate_date = dsMain.DATA[0].conclusion_date;
                            //invt_id = dsList.DATA[i].invt_id;
                            //ldc_unitamt = dsList.DATA[i].amount_amt;
                            //ls_unitcode = dsList.DATA[i].unit_code;
                            //refer_slipno = dsList.DATA[i].ASSIST_DOCNO.Trim();
                            month = operate_date.Month;
                            if (ls_err == "")
                            {
                                sqlStr = @"INSERT INTO ASSSUMPAYGIFT
                                        (COOP_ID 			            , OPERATE_DATE 		            , ASSIST_YEAR                   , ASSIST_MONTH                   
					                    , SEQ_NO  						, INVT_ID                  	    , UNIT_AMT                      , UNIT_CODE
                                        , POSTTO_DUR                    , REFER_SLIPNO                  , PAY_RECV_STATUS               , SLIP_STATUS)
                                values
                                        ( {0}                           , {1}                           , {2}                           , {3}                           
                                        , {4}                           , {5}                           , {6}                           , {7}
                                        , {8}                           , {9}                           , {10}                          , {11})";

                                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, operate_date, dsList.DATA[0].assist_year, month, seq_no, invt_id, ldc_unitamt, ls_unitcode, 0, ls_assisreq, -1, 1);
                                Sdt Insert = WebUtil.QuerySdt(sqlStr);
                            }

                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถส่งข้อมูลสวัสดิการของขวัญไปฝ่ายพัสดุครุภัณฑ์ได้" + ex);
                            return;
                        }
                    }
                }
            }

            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            dsList.ResetRow();
        }

        private void of_getdata()
        {
            dsList.ResetRow();
            dsMain.DATA[0].select_check = "0";
            dsMain.DATA[0].req_status = "8";

            string sqlwhere = "", sqlorder = "";

            if (dsMain.DATA[0].member_no != "")
            {
                sqlwhere += " and req.member_no like '%" + dsMain.DATA[0].member_no + "%' ";
            }
            else { sqlwhere += ""; }

            if (dsMain.DATA[0].assisttype_code != "")
            {
                sqlwhere += " and req.assisttype_code = '" + dsMain.DATA[0].assisttype_code + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].assistpay_code1 != "")
            {
                sqlwhere += " and req.assistpay_code between '" + dsMain.DATA[0].assistpay_code1 + "'  and '" + dsMain.DATA[0].assistpay_code2 + "' ";
            }
            else { sqlwhere += ""; }
            if (Hd_sortbegin.Value == "1")
            {
                sqlorder = "order by req.assisttype_code,req.edu_levelcode asc,req.edu_gpa desc,req.member_no asc";
            }
            else {
                if (dsMain.DATA[0].sort_type == "1")
                {
                    sqlorder = "order by req.assisttype_code,mb.salary_amount,req.member_no asc ";

                }
                else
                {
                    sqlorder = "order by req.assisttype_code,req.postsend_date,req.member_no asc ";
                }
            }

            //if (dsMain.DATA[0].sort_type == "2")
            //{
            //    sqlorder = "order by req.assisttype_code, req.member_no ";
            //}
            //else
            //{
            //    sqlorder = "order by req.assisttype_code, req.assist_docno ";
            //}
            
            SetgrpLabel();
            if (Hd_setlabel.Value == "01")
            {
                dsList.RetrieveList(sqlwhere, sqlorder);
            }
            else
            {
                dsList.RetrieveListass(sqlwhere, sqlorder);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "setlabel", "setlabel()", true);

        }

        private Int32 of_buildasscont(string as_reqno, string as_contno, Int32 ai_status, string remark, Int32 stm_flag)
        {
            string SqlIns = "", SqlUpd = "";
            DateTime ldtm_apvdate;

            ldtm_apvdate = dsMain.DATA[0].conclusion_date;

            SqlIns = @"insert into  asscontmaster
		                        ( coop_id, asscontract_no, assisttype_code, member_no, assistpay_code, assistreq_docno, 
		                        assist_year, approve_date, approve_amt, withdrawable_amt, pay_balance, 
		                        last_periodpay, payment_status, last_stm, ass_rcvcardid, ass_prcardid, 
		                        moneytype_code, expense_bank, expense_branch, expense_accid, send_system, deptaccount_no, 
		                        ass_rcvname, approve_id, asscont_status, stm_flag, stmpay_num, stmpay_type , remark )
                        select	a.coop_id, {1}, a.assisttype_code, a.member_no, a.assistpay_code, a.assist_docno,
		                        a.assist_year, {2}, a.assistnet_amt, a.assistnet_amt, 0, 
                                0, 1, 0, a.ass_rcvcardid,  a.ass_prcardid, 
		                        a.moneytype_code, a.expense_bank, a.expense_branch, a.expense_accid, a.send_system, a.deptaccount_no, 
                                a.ass_rcvname, {3}, 1, a.stm_flag, a.stmpay_num, a.stmpay_type , {4}
                        from	assreqmaster a
		                        join mbmembmaster mb on a.member_no = mb.member_no
		                        join assucfassisttype b on a.assisttype_code = b.assisttype_code
		                        join assucfassisttypegroup c on b.assisttype_group = c.assisttype_group
                        where assist_docno = {0} ";

            SqlUpd = "update assreqmaster set req_status = {1}, approve_id = {2}, approve_date = {3} where assist_docno = {0} ";

            try
            {
                // ถ้าอนุมัติให้สร้างทะเบียนสวัสดิการขึ้นมา
                if (ai_status == 1)
                {
                    SqlIns = WebUtil.SQLFormat(SqlIns, as_reqno, as_contno, ldtm_apvdate, state.SsUsername, remark);
                    WebUtil.ExeSQL(SqlIns);
                }

                SqlUpd = WebUtil.SQLFormat(SqlUpd, as_reqno, ai_status, state.SsUsername, ldtm_apvdate);
                WebUtil.ExeSQL(SqlUpd);

            }
            catch (Exception ex)
            {
                WebUtil.ExeSQL("rollback");
                LtServerMessage.Text = WebUtil.ErrorMessage("Error Build ASSCONTMASTER " + ex);
                return -1;
            }

            return 1;
        }

        private Int32 of_Updateasscont(string as_reqno, string as_contno, Int32 ai_status, string remark, Int32 stm_flag, string as_moneytype, string as_sendsystem, string as_deptno)
        {
            string SqlUpd = "", SqlUpMas = "";
            DateTime ldtm_apvdate;

            ldtm_apvdate = state.SsWorkDate;

            SqlUpMas = "update asscontmaster set assistreq_docno = {0} , moneytype_code = {2} , send_system = {3} , deptaccount_no = {4} where asscontract_no = {1} ";

            SqlUpd = "update assreqmaster set req_status = {1}, approve_id = {2}, approve_date = {3} where assist_docno = {0} ";

            try
            {
                SqlUpMas = WebUtil.SQLFormat(SqlUpMas, as_reqno, as_contno, as_moneytype, as_sendsystem, as_deptno);
                WebUtil.ExeSQL(SqlUpMas);

            }
            catch (Exception ex)
            {
                WebUtil.ExeSQL("rollback");
                LtServerMessage.Text = WebUtil.ErrorMessage("Error Update ASSCONTMASTER " + ex);
                return -1;
            }

            try
            {
                SqlUpd = WebUtil.SQLFormat(SqlUpd, as_reqno, ai_status, state.SsUsername, ldtm_apvdate);
                WebUtil.ExeSQL(SqlUpd);

            }
            catch (Exception ex)
            {
                WebUtil.ExeSQL("rollback");
                LtServerMessage.Text = WebUtil.ErrorMessage("Error Build ASSCONTMASTER " + ex);
                return -1;
            }

            return 1;
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            if (!IsPostBack)
            {
                dsMain.DdAssistType();
                dsMain.DATA[0].conclusion_no = "";
                dsMain.DATA[0].conclusion_date = state.SsWorkDate;
                Hd_sortbegin.Value = "1";
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }

}