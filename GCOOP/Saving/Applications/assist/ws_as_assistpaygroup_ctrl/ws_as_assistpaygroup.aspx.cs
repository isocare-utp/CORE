using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_assistpaygroup_ctrl
{
    public partial class ws_as_assistpaygroup : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string InitAsssistpaytype { get; set; }
        [JsPostBack]
        public string JsSavedata { get; set; }

        String[] assists;
        int currentAssist = 0, ii_saveresult = 0;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsSMain.InitDsSMain(this);
            dsSPayto.InitDsSPayto(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                ii_saveresult = 0;
                dsMain.DdMoneyType();
                dsMain.DDAssisttype();
                dsMain.DATA[0].select_check = "0";
                dsMain.DATA[0].moneytype_code = "TRN";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                PostListRetrieve();
            }
            else if (eventArg == InitAsssistpaytype)
            {
                string ls_minpaytype = "", ls_maxpaytype = "";
                if (dsMain.DATA[0].assisttype_code == "")
                {
                    dsMain.ResetRow();
                    dsMain.DDAssisttype();
                }
                else
                {
                    dsMain.AssistPayType(dsMain.DATA[0].assisttype_code, ref ls_minpaytype, ref ls_maxpaytype);
                    dsMain.DATA[0].assistpay_code1 = ls_minpaytype;
                    dsMain.DATA[0].assistpay_code2 = ls_maxpaytype;
                }
            }
            else if (eventArg == JsSavedata) {
                PostSavedata();
            }
        }

        public void PostListRetrieve() {
            dsList.ResetRow();
            dsMain.DATA[0].select_check = "0";
            string sqlwhere = "";

            if (dsMain.DATA[0].moneytype_code != "")
            {
                sqlwhere += " and ass.moneytype_code = '" + dsMain.DATA[0].moneytype_code + "' ";
            }
            else { sqlwhere += ""; }

            if (dsMain.DATA[0].assisttype_code != "")
            {
                sqlwhere += " and ass.assisttype_code = '" + dsMain.DATA[0].assisttype_code + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].assistpay_code1 != "")
            {
                sqlwhere += " and ass.assistpay_code between '" + dsMain.DATA[0].assistpay_code1 + "'  and '" + dsMain.DATA[0].assistpay_code2 + "' ";
            }
            else { sqlwhere += ""; }
            dsList.RetrieveList(sqlwhere);
        }

        public void PostSavedata()
        {
            string ls_slipno;
            string ls_upcont = "", ls_inscont = "";
            Int32 li_chk = 0;
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                for (int j = 0; j < dsList.RowCount; j++)
                {
                    if (dsList.DATA[j].choose_flag == "1")
                    {
                       string assist = dsList.DATA[j].asscontract_no;
                       of_initassistpay(assist);

                        ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopControl, "ASSISTSLIPNO");

                        dsSMain.DATA[0].COOP_ID = state.SsCoopControl;
                        dsSMain.DATA[0].ASSISTSLIP_NO = ls_slipno;
                        dsSMain.DATA[0].ENTRY_ID = state.SsUsername;
                        dsSMain.DATA[0].SLIP_STATUS = 1;
                        dsSMain.DATA[0].ENTRY_DATE = DateTime.Today;


                        for (Int32 i = 0; i < dsSPayto.RowCount; i++)
                        {
                            dsSPayto.DATA[i].COOP_ID = state.SsCoopControl;
                            dsSPayto.DATA[i].ASSISTSLIP_NO = ls_slipno;
                            dsSPayto.DATA[i].ITEM_CODE = "PAY";
                            dsSPayto.DATA[i].SEQ_NO = i + 1;

                            if (dsSPayto.DATA[i].MONEYTYPE_CODE == "TRN")
                            {
                                dsSPayto.DATA[i].METHPAYTYPE_CODE = dsSMain.DATA[i].send_system;
                            }
                            else
                            {
                                dsSPayto.DATA[i].METHPAYTYPE_CODE = dsSPayto.DATA[i].MONEYTYPE_CODE;
                            }

                            // ใช้การเงินบัญชี
                            dsSPayto.DATA[i].ASSISTSLIP_CONTROL = ls_slipno;
                        }

                        //for (Int32 i = 0; i < dsLoan.RowCount; i++)
                        //{
                        //    dsLoan.DATA[i].COOP_ID = state.SsCoopControl;
                        //    dsLoan.DATA[i].ASSISTSLIP_NO = ls_slipno;
                        //    dsLoan.DATA[i].ITEM_CODE = "CLR";
                        //    dsLoan.DATA[i].SEQ_NO = i + 1;
                        //    dsLoan.DATA[i].MONEYTYPE_CODE = "TRN";

                        //    // ใช้การเงินบัญชี
                        //    dsLoan.DATA[i].ASSISTSLIP_CONTROL = ls_slipno;
                        //}

                        // ทำ query การปรับปรุงทะเบียนสวัสดิการ
                        li_chk = this.of_getsqlpostcont(ls_slipno, ref ls_upcont, ref ls_inscont);
                        if (li_chk != 1)
                        {
                            return;
                        }

                            exe.AddFormView(dsSMain, ExecuteType.Insert);
                            exe.AddRepeater(dsSPayto);
                            //exe.AddRepeater(dsLoan);
                            exe.SQL.Add(ls_upcont);
                            exe.SQL.Add(ls_inscont);
                            exe.Execute();
                            exe.SQL.Clear();
                       

                        // ส่งไปเงินฝาก
                        this.of_postdepttran();

                        ii_saveresult = 1;
                    }
                }
                //dsList.ResetRow();
                PostListRetrieve();
                //ii_saveresult = 1;
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                return;
            }
            
        }
        private void of_initassistpay(string assist)
        {
            //assists = Request["assists"].Split(',');
            //lbCurrentAssist.Text = (currentAssist + 1) + "/" + assists.Length;

            //string assist = assists[currentAssist];

            dsSMain.ResetRow();
            dsSPayto.ResetRow();

            dsSMain.InitDetail(assist);

            dsSMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            dsSMain.DATA[0].OPERATE_DATE = state.SsWorkDate;

            dsSMain.DATA[0].PAY_PERIOD = dsSMain.DATA[0].BFPERIOD + 1;

            this.of_initpaycondition();
            this.of_initpayto();

            dsSPayto.DdMoneyType();

            //dsPayto.DdToFromAccID();
            int li_maxrow = dsSPayto.RowCount - 1;

            for (int li_row = 0; li_row < dsSPayto.RowCount; li_row++)
            {
                string min_tofromaccid = "";
                dsSPayto.DdToFromAccIDRow(dsSPayto.DATA[li_row].MONEYTYPE_CODE, li_row, ref min_tofromaccid);
                dsSPayto.DATA[li_row].TOFROM_ACCID = min_tofromaccid;
            }
            //HdIndex.Value = currentAssist + "";
        }

        //private void of_initassistpay(string assist)
        //{
        //    //string ass = Hd_assist.Value;
        //    //assists = ass.Split(',');

        //     //assist = assists[currentAssist];

        //    dsSPayto.ResetRow();
        //    dsSPayto.ResetRow();

        //    dsSMain.InitDetail(assist);
        //    dsSMain.DATA[0].SLIP_DATE = state.SsWorkDate;
        //    dsSMain.DATA[0].OPERATE_DATE = state.SsWorkDate;

        //    dsSMain.DATA[0].PAY_PERIOD = dsSMain.DATA[0].BFPERIOD + 1;

        //    this.of_initpaycondition();
        //    this.of_initpayto();

        //    dsSPayto.DdMoneyType();
        //    dsSPayto.DdToFromAccID();

        //    HdIndex.Value = currentAssist + "";

        //}

        private void of_initpaycondition()
        {
            string ls_asstype;
            string ls_sql;
            Int32 li_period, li_paytype;
            decimal ldc_apvamt = 0, ldc_wtdamt = 0;
            decimal ldc_payout = 0;
            decimal ldc_assamt = 0, ldc_assmax = 0;

            ls_asstype = dsSMain.DATA[0].ASSISTTYPE_CODE;
            li_period = Convert.ToInt32(dsSMain.DATA[0].PAY_PERIOD);
            ldc_apvamt = dsSMain.DATA[0].BFAPV_AMT;
            ldc_wtdamt = dsSMain.DATA[0].BFWTD_AMT;

            ls_sql = @" select first_payamt, next_payamt,
                               first_typepay, next_typepay,
                               max_firstpayamt, max_nextpayamt
                        from assucfassisttype
                        where assisttype_code = '" + ls_asstype + "' ";
            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                if (li_period == 1)
                {
                    li_paytype = dt.GetInt32("first_typepay");
                    ldc_assamt = dt.GetDecimal("first_payamt");
                    ldc_assmax = dt.GetDecimal("max_firstpayamt");
                }
                else
                {
                    li_paytype = dt.GetInt32("next_typepay");
                    ldc_assamt = dt.GetDecimal("next_payamt");
                    ldc_assmax = dt.GetDecimal("max_nextpayamt");
                }
            }
            else
            {
                li_paytype = 2;
                ldc_assamt = 100;
                ldc_assmax = 99999999;
            }

            if (li_paytype == 1) // บาท
            {
                ldc_payout = ldc_apvamt;
                if (ldc_payout > ldc_assamt)
                {
                    ldc_payout = ldc_assamt;
                }
            }
            else
            {
                ldc_payout = ldc_apvamt * (ldc_assamt / 100);
            }

            if (ldc_payout > ldc_assmax)
            {
                ldc_payout = ldc_assmax;
            }

            if (ldc_payout > ldc_wtdamt)
            {
                ldc_payout = ldc_wtdamt;
            }

            //ตรวจสอบสุดท้ายกรณีสมาชิกเสียชีวิตจ่านทั้งหมด
            if (dsSMain.DATA[0].dead_status == 1)
            {
                ldc_payout = ldc_wtdamt;
            }

            dsSMain.DATA[0].PAYOUT_AMT = ldc_payout;
            dsSMain.DATA[0].PAYOUTCLR_AMT = 0;
            dsSMain.DATA[0].PAYOUTNET_AMT = ldc_payout;
        }

        private void of_initpayto()
        {
            string ls_asscontno, ls_sql;
            string ls_expcode, ls_expbank, ls_expbranch, ls_expaccid, ls_system, ls_deptno;
            decimal ldc_payout;

            ls_asscontno = dsSMain.DATA[0].ASSCONTRACT_NO;
            ldc_payout = dsSMain.DATA[0].PAYOUTNET_AMT;

            ls_sql = @" select moneytype_code, 
                                   expense_bank, expense_branch, expense_accid,
                                   send_system, deptaccount_no 
                            from   asscontmaster
                            where asscontract_no = '" + ls_asscontno + "'";
            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                ls_expcode = dt.GetString("moneytype_code");
                ls_expbank = dt.GetString("expense_bank");
                ls_expbranch = dt.GetString("expense_branch");
                ls_expaccid = dt.GetString("expense_accid");
                ls_system = dt.GetString("send_system");
                ls_deptno = dt.GetString("deptaccount_no");
            }
            else
            {
                return;
            }

            dsSPayto.InsertAtRow(0);

            dsSPayto.DATA[0].MONEYTYPE_CODE = ls_expcode;
            dsSPayto.DATA[0].EXPENSE_BANK = ls_expbank;
            dsSPayto.DATA[0].EXPENSE_BRANCH = ls_expbranch;
            dsSPayto.DATA[0].EXPENSE_ACCID = ls_expaccid;

            if (ls_expcode == "TRN" && ls_system == "DEP")
            {
                dsSPayto.DATA[0].DEPTACCOUNT_NO = ls_deptno;
            }
            else if (ls_expcode == "CBT")
            {
                //if (!string.IsNullOrEmpty(ls_expbank))
                //{
                //    ls_sql = " select clearing_type from cmucfbank where bank_code = '"+ls_expbank+"' ";

                //    dt = WebUtil.QuerySdt(ls_sql);
                //    if (dt.Next())
                //    {
                //        dsPayto.DATA[0].EXPENSE_CLEARING = dt.GetString("clearing_type");
                //    }
                //}
            }

            // ส่วนที่นำไปใช้กับบัญชีการเงิน
            dsSPayto.DATA[0].BANK_CODE = ls_expbank;
            dsSPayto.DATA[0].BANKBRANCH_ID = ls_expbranch;
            dsSPayto.DATA[0].BANK_ACCID = ls_expaccid;
            dsSPayto.DATA[0].DEPTACCOUNT_NO = ls_deptno;

            dsSPayto.DATA[0].ITEMPAY_AMT = ldc_payout;
            dsSPayto.SetRowFocus(0);

            this.of_setpaytodesc();
            this.of_setfee(0);
            this.of_setdefaultaccid(0);
        }

        private void of_setpaytodesc()
        {
            string ls_moneytype, ls_expbank, ls_expbranch, ls_bankaccid, ls_chqpayname, ls_desc = "", ls_expclr = "", ls_deptaccno = "";
            string ls_shtbk = "", ls_brnname = "", ls_sendsystem = "";
            Int32 li_row = 0;

            li_row = dsSPayto.GetRowFocus();

            ls_moneytype = dsSPayto.DATA[li_row].MONEYTYPE_CODE;
            ls_expbank = dsSPayto.DATA[li_row].EXPENSE_BANK;
            ls_expbranch = dsSPayto.DATA[li_row].EXPENSE_BRANCH;
            ls_bankaccid = dsSPayto.DATA[li_row].EXPENSE_ACCID;
            ls_chqpayname = dsSPayto.DATA[li_row].CHQ_PAYNAME;
            ls_expclr = dsSPayto.DATA[li_row].EXPENSE_CLEARING;
            ls_deptaccno = dsSPayto.DATA[li_row].DEPTACCOUNT_NO;
            ls_sendsystem = dsSMain.DATA[li_row].send_system;

            // ใส่ข้อมูล field ที่การเงินและบัญชีจะดึงไปใช้
            dsSPayto.DATA[li_row].BANK_CODE = ls_expbank;
            dsSPayto.DATA[li_row].BANKBRANCH_ID = ls_expbranch;
            dsSPayto.DATA[li_row].BANK_ACCID = ls_bankaccid;

            if (ls_moneytype == "CSH")
            {
                ls_desc = "เงินสด";
            }
            else if (ls_moneytype == "CBT" || ls_moneytype == "CBO")
            {
                string sql;
                Sdt dt;
                sql = @"select rtrim(ltrim(bank_shortname)) bank_desc from cmucfbank where bank_code = '" + ls_expbank + "'";
                dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_shtbk = dt.GetString("bank_desc");
                }

                sql = "select branch_name from cmucfbankbranch where bank_code = '" + ls_expbank + "' and branch_id = '" + ls_expbranch + "' ";
                dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_brnname = dt.GetString("branch_name");
                }

                ls_desc = "โอน " + ls_shtbk + " สาขา " + ls_brnname + " เลขที่ " + ls_bankaccid + " (" + ls_expclr + ")";
            }
            else if (ls_moneytype == "CHQ")
            {
                ls_desc = "เช็คสั่งจ่าย " + ls_chqpayname;

                dsSPayto.DATA[li_row].PAYTO_WHOM = ls_chqpayname;
            }
            else if (ls_moneytype == "TRN")
            {
                if (ls_sendsystem == "MRT")
                {
                    ls_desc = "โอนเงินรอจ่ายคืน";
                }
                else if (ls_sendsystem == "SHR")
                {
                    ls_desc = "โอนซื้อหุ้น";
                }
                else
                {
                    ls_desc = "โอนบัญชีสหกรณ์ " + ls_deptaccno;

                    dsSPayto.DATA[li_row].DEPTACCOUNT_NO = ls_deptaccno;
                }
            }

            dsSPayto.DATA[li_row].DESCRIPTION = ls_desc;
        }

        private void of_setfee(Int32 ai_row)
        {
            string ls_moneytype, ls_bank, ls_branch, ls_sql;
            decimal ldc_payout, ldc_fee = 0, ldc_srv = 0;
            Sdt dt;

            ls_moneytype = dsSPayto.DATA[ai_row].MONEYTYPE_CODE;
            ls_bank = dsSPayto.DATA[ai_row].EXPENSE_BANK.Trim();
            ls_branch = dsSPayto.DATA[ai_row].EXPENSE_BRANCH.Trim();
            ldc_payout = dsSPayto.DATA[ai_row].ITEMPAY_AMT;

            if (ls_moneytype == "CBT" || ls_moneytype == "CBO")
            {
                if (string.IsNullOrEmpty(ls_branch) || string.IsNullOrEmpty(ls_bank))
                {
                    ldc_fee = 0;
                }

                ls_sql = " select * from cmucfbankbranch where bank_code = '" + ls_bank + "' and branch_id = '" + ls_branch + "'";
                dt = WebUtil.QuerySdt(ls_sql);
                if (dt.Next())
                {
                    if (ls_moneytype == "CBT")
                    {
                        ldc_fee = dt.GetDecimal("serviceassist_amt");
                    }
                    else
                    {
                        ldc_srv = dt.GetDecimal("service_amt");
                        if (dt.GetInt32("fee_status") == 1)
                        {
                            ldc_fee = this.of_calbankfee(ls_bank, ldc_payout);
                        }
                        ldc_fee = ldc_fee + ldc_srv;
                    }
                }
            }

            dsSPayto.DATA[ai_row].FEE_AMT = ldc_fee;

            int li_maxrow = dsSPayto.RowCount - 1;

            for (int li_row = 0; li_row < dsSPayto.RowCount; li_row++)
            {
                string min_tofromaccid = "";
                dsSPayto.DdToFromAccIDRow(dsSPayto.DATA[li_row].MONEYTYPE_CODE, li_row, ref min_tofromaccid);
                dsSPayto.DATA[li_row].TOFROM_ACCID = min_tofromaccid;


            }
        }

        private decimal of_calbankfee(string as_bank, decimal adc_payamt)
        {
            string ls_sql;
            decimal ldc_feeamt = 0, ldc_prnamt, ldc_feeper, ldc_minamt, ldc_maxamt, ldc_roundup, ldc_step, ldc_fraction;
            Sdt dt;

            ls_sql = " select * from cmucfbankfeerate where bank_code = '" + as_bank + "' and " + adc_payamt.ToString() + " between start_amt and end_amt ";
            dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                ldc_prnamt = dt.GetDecimal("principal_amt");
                ldc_roundup = dt.GetDecimal("roundup_amt");
                ldc_feeper = dt.GetDecimal("fee_amt");
                ldc_minamt = dt.GetDecimal("minfee_value");
                ldc_maxamt = dt.GetDecimal("maxfee_value");

                ldc_step = Math.Truncate(adc_payamt / ldc_prnamt);
                ldc_fraction = (adc_payamt / ldc_prnamt) - Math.Truncate(adc_payamt / ldc_prnamt);

                ldc_feeamt = ldc_step * ldc_feeper;
                if (ldc_fraction >= ldc_roundup)
                {
                    ldc_feeamt = ldc_feeamt + ldc_feeper;
                }

                if (ldc_feeamt < ldc_minamt)
                {
                    ldc_feeamt = ldc_minamt;
                }

                if (ldc_feeamt > ldc_maxamt && ldc_maxamt > 0)
                {
                    ldc_feeamt = ldc_maxamt;
                }
            }

            return ldc_feeamt;
        }

        private void of_setdefaultaccid(Int32 ai_row)
        {
            string ls_moneytype, ls_accid = "", ls_sql;
            Sdt dt;

            ls_moneytype = dsSPayto.DATA[ai_row].MONEYTYPE_CODE;

            //if (ls_moneytype != "TRN")
            //{
            ls_sql = @" select * 
                            from cmucftofromaccid 
                            where applgroup_code = 'SLN' 
                            and sliptype_code = 'LWD' 
                            and moneytype_code = '" + ls_moneytype + @"'
                            order by defaultpayout_flag desc, account_id ";
            dt = WebUtil.QuerySdt(ls_sql);

            if (dt.Next())
            {
                ls_accid = dt.GetString("account_id");
            }
            //}

            dsSPayto.DATA[ai_row].TOFROM_ACCID = ls_accid;
        }

        public void SaveWebSheet()
        {
            
        }

        private Int32 of_getsqlpostcont(string as_slipno, ref string as_updcont, ref string as_insstm)
        {
            // ปรับปรุงค่าใส่สัญญา
            string ls_contno, ls_sql;
            Int32 li_seqno, li_period;
            decimal ldc_bfwtd, ldc_bfpaybal, ldc_wtdamt, ldc_paybal, ldc_payamt;
            DateTime ldtm_slipdate;

            ls_contno = dsSMain.DATA[0].ASSCONTRACT_NO;
            li_period = Convert.ToInt32(dsSMain.DATA[0].PAY_PERIOD);
            ldc_payamt = dsSMain.DATA[0].PAYOUT_AMT;
            ldtm_slipdate = dsSMain.DATA[0].SLIP_DATE;

            ls_sql = @"select last_stm, withdrawable_amt, pay_balance
                       from asscontmaster where coop_id = {0}
                       and asscontract_no = {1} ";

            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_contno);
            Sdt dt;

            dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                li_seqno = dt.GetInt32("last_stm");
                ldc_bfwtd = dt.GetDecimal("withdrawable_amt");
                ldc_bfpaybal = dt.GetDecimal("pay_balance");
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลทะเบียนสวัสดิการ เลขที่ " + ls_contno);
                return -1;
            }

            ldc_wtdamt = ldc_bfwtd - ldc_payamt;
            ldc_paybal = ldc_bfpaybal + ldc_payamt;

            li_seqno++;

            // ส่วนการ update ทะเบียนสวัสดิการ
            as_updcont = @"update asscontmaster 
                            set withdrawable_amt = {1},
                                pay_balance = {2},
                                last_stm = {3},
                                last_periodpay = {4},
                                lastpay_date = {5}
                            where asscontract_no = {0} ";

            as_updcont = WebUtil.SQLFormat(as_updcont, ls_contno, ldc_wtdamt, ldc_paybal, li_seqno, li_period, ldtm_slipdate);

            // ส่วนการ insert STM สวัสดิการ
            as_insstm = @" insert into asscontstatement
                                ( coop_id, asscontract_no, seq_no, slip_date, operate_date, item_code, 
                                  ref_slipno, period, pay_amt, pay_balance, entry_id, entry_date )
                           values
                                ( {0}, {1}, {2}, {3}, {4}, 'RCV', 
                                  {5}, {6}, {7}, {8}, {9}, sysdate ) ";

            as_insstm = WebUtil.SQLFormat(as_insstm, state.SsCoopControl, ls_contno, li_seqno, ldtm_slipdate, state.SsWorkDate,
                                       as_slipno, li_period, ldc_payamt, ldc_paybal, state.SsUsername);

            return 1;
        }

        private Int32 of_postdepttran()
        {
            string ls_sql, ls_expcode, ls_deptno, ls_memno, ls_slipno, ls_sendsystem, ls_contno, ls_desc;
            Int32 li_year = 0, li_seqno;
            decimal ldc_payamt;
            DateTime ldtm_slipdate;
            Sdt dt;

            ls_slipno = dsSMain.DATA[0].ASSISTSLIP_NO;
            ls_memno = dsSMain.DATA[0].MEMBER_NO;
            ldtm_slipdate = dsSMain.DATA[0].SLIP_DATE;
            ls_sendsystem = dsSMain.DATA[0].send_system;
            ls_contno = dsSMain.DATA[0].ASSCONTRACT_NO;
            ls_desc = dsSMain.DATA[0].assisttype_desc;

            ls_sql = "select account_year from cmaccountyear where {0} between accstart_date and accend_date ";
            ls_sql = WebUtil.SQLFormat(ls_sql, ldtm_slipdate);
            dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                li_year = dt.GetInt32("account_year");
            }

            for (Int32 i = 0; i < dsSPayto.RowCount; i++)
            {

                ls_expcode = dsSPayto.DATA[0].MONEYTYPE_CODE;


                if (ls_expcode != "TRN")
                {
                    continue;
                }

                ls_deptno = dsSPayto.DATA[0].DEPTACCOUNT_NO;
                ldc_payamt = dsSPayto.DATA[0].ITEMPAY_AMT;

                if (ls_sendsystem == "MRT")
                {

                    ls_sql = "select max(seq_no) as seq_no from mbmembmoneyreturn where system_code = 'ASS' and member_no = {0}  and ref_slipno = {1} ";
                    ls_sql = WebUtil.SQLFormat(ls_sql, ls_memno, ls_slipno);
                    dt = WebUtil.QuerySdt(ls_sql);
                    if (dt.Next())
                    {
                        li_seqno = dt.GetInt32("seq_no");
                    }
                    else
                    {
                        li_seqno = 0;
                    }

                    li_seqno++;

                    ls_sql = @" insert into mbmembmoneyreturn
                                     ( coop_id,  member_no, seq_no, ref_slipno, system_code,
                                       description, loancontract_no, return_amt, return_balance, return_status , entry_id , entry_date )
                                values
                                     ( {0}, {1},  {2}, {3}, {4},     
                                       {5},  {6}, {7}, {8} , {9}, {10}, {11}) ";

                    try
                    {
                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_memno, li_seqno, ls_slipno, "ASS", ls_desc, ls_contno, ldc_payamt, ldc_payamt, 8, state.SsUsername, state.SsWorkDate);
                        WebUtil.ExeSQL(ls_sql);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการเงินรอจ่ายคืนได้" + ex);
                        return -1;
                    }
                }
                else if (ls_sendsystem == "SHR")
                {

                    ls_sql = "select max(seq_no) as seq_no from sltranspayin where trnsource_code = 'ASS' and member_no = {0}  and trans_date = {1} ";
                    ls_sql = WebUtil.SQLFormat(ls_sql, ls_memno, ldtm_slipdate);
                    dt = WebUtil.QuerySdt(ls_sql);
                    if (dt.Next())
                    {
                        li_seqno = dt.GetInt32("seq_no");
                    }
                    else
                    {
                        li_seqno = 0;
                    }

                    li_seqno++;

                    ls_sql = @" insert into sltranspayin
                                     ( coop_id, member_no,  trans_date, seq_no, transitem_code,  shrtype_code, contcoop_id, transpay_type,
                                       trans_amt, sliptype_code, trnsource_code, trnsource_status, trnsource_refslipno , moneytype_code  )
                                values
                                     ( {0}, {1},  {2},  {3}, 'SHR', '01' , {4},  2, {5},  'PX', 'ASS', 8, {6} , 'TRN') ";

                    try
                    {
                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_memno, ldtm_slipdate, li_seqno, state.SsCoopControl, ldc_payamt, ls_slipno);
                        WebUtil.ExeSQL(ls_sql);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("Error Sltrans" + ex);
                        return -1;
                    }
                }
                else
                {


                    ls_sql = "select max(seq_no) as seq_no from dpdepttran where system_code = 'ASS' and deptaccount_no = {0}  and tran_date = {1} ";
                    ls_sql = WebUtil.SQLFormat(ls_sql, ls_deptno, ldtm_slipdate);
                    dt = WebUtil.QuerySdt(ls_sql);
                    if (dt.Next())
                    {
                        li_seqno = dt.GetInt32("seq_no");
                    }
                    else
                    {
                        li_seqno = 0;
                    }

                    li_seqno++;

                    ls_sql = @" insert into dpdepttran
                                     ( coop_id, deptaccount_no, member_no, system_code, tran_date, seq_no,
                                       deptitem_amt, tran_status, sequest_status, ref_coopid, ref_slipno )
                                values
                                     ( {0}, {1},  {2}, 'ASS', {3}, {4},    
                                       {5},  0, 0, {0} , {6}) ";

                    try
                    {
                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_deptno, ls_memno, ldtm_slipdate, li_seqno, ldc_payamt, ls_slipno);
                        WebUtil.ExeSQL(ls_sql);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("Error Depttrans" + ex);
                        return -1;
                    }
                }
            }

            return 1;
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}