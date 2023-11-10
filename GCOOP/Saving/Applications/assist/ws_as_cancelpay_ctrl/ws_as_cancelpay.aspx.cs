using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
namespace Saving.Applications.assist.ws_as_cancelpay_ctrl
{
    public partial class ws_as_cancelpay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DDloantype();
                dsMain.DATA[0].select_check = "0";
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                dsList.ResetRow();
                dsMain.DATA[0].select_check = "0";
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                string sqlwhere = "";
                //เพิ่มช่วงวันที่
                sqlwhere += " and po.slip_date between '" + dsMain.DATA[0].start_date + "'  and '" +dsMain.DATA[0].end_date + "' ";

                if (dsMain.DATA[0].member_no != "")
                {
                    sqlwhere += " and po.member_no like '%" + dsMain.DATA[0].member_no + "%' ";
                }

                if (dsMain.DATA[0].assisttype_code != "")
                {
                    sqlwhere += " and po.assisttype_code = '" + dsMain.DATA[0].assisttype_code + "' ";
                }

                dsList.RetrieveList(sqlwhere);
            }
        }

        public void SaveWebSheet()
        {
            string ls_slipno, ls_contno, ls_deptno, ls_chkflag = "0" ;
            string ls_sql;
            decimal ldc_bfwtd, ldc_bfpaybal, ldc_payamt, ldc_wtdbal, ldc_paybal;
            Int32 li_lastperiod, li_laststm, li_period;
            DateTime ldtm_slipdate, ldtm_bflastpay;
            Sdt dt;

            ExecuteDataSource exe = new ExecuteDataSource(this);

            for (Int32 i = 0; i < dsList.RowCount; i++)
            {
                ls_chkflag = dsList.DATA[i].choose_flag;

                if (ls_chkflag != "1")
                {
                    continue;
                }

                ls_slipno = dsList.DATA[i].assistslip_no;
                ls_contno = dsList.DATA[i].asscontract_no;
                ldc_payamt = dsList.DATA[i].payout_amt;
                li_period = dsList.DATA[0].pay_period;
                ldtm_slipdate = dsList.DATA[i].slip_date;
                ldtm_bflastpay = dsList.DATA[i].bflastpay_date;

                // ดึงข้อมูลสำหรับทำการยกเลิก
                ls_sql = @"select withdrawable_amt, pay_balance, last_periodpay, last_stm from asscontmaster where asscontract_no = '" + ls_contno + "'";
                dt = WebUtil.QuerySdt(ls_sql);

                if (dt.Next())
                {
                    ldc_bfwtd = dt.GetDecimal("withdrawable_amt");
                    ldc_bfpaybal = dt.GetDecimal("pay_balance");
                    li_lastperiod = dt.GetInt32("last_periodpay");
                    li_laststm = dt.GetInt32("last_stm");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลทะเบียนสวัสดิการ เลขที่" + ls_contno);
                    return;
                }

                ldc_wtdbal = ldc_bfwtd + ldc_payamt;
                ldc_paybal = ldc_bfpaybal - ldc_payamt;

                li_lastperiod = li_lastperiod - 1;
                li_laststm = li_laststm + 1;

                // ปรับค่าในทะเบียนสวัสดิการ
                ls_sql = @" update asscontmaster 
                            set withdrawable_amt = {1}, pay_balance = {2}, last_periodpay = {3}, last_stm = {4}, lastpay_date = {5}
                            where asscontract_no = {0} ";
                ls_sql = WebUtil.SQLFormat(ls_sql, ls_contno, ldc_wtdbal, ldc_paybal, li_lastperiod, li_laststm, ldtm_bflastpay);
                exe.SQL.Add(ls_sql);

                // บันทึกลง statement
                ls_sql = @" insert into asscontstatement
                                ( coop_id, asscontract_no, seq_no, slip_date, operate_date, item_code, ref_slipno, 
                                  period, pay_amt, pay_balance, entry_id, entry_date )
                            values
                                ( {0}, {1}, {2}, {3}, {4}, 'CRC', {5}, {6}, {7}, {8}, {9}, {10} ) ";

                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_contno, li_laststm, ldtm_slipdate, state.SsWorkDate,
                                           ls_slipno, li_period, ldc_payamt, ldc_paybal, state.SsUsername, DateTime.Now);
                exe.SQL.Add(ls_sql);

                // ปรับสถานะในใบจ่าย
                ls_sql = @" update assslippayout 
                            set slip_status = -9, cancel_id={1}, cancel_date={2}
                            where assistslip_no = {0}";
                ls_sql = WebUtil.SQLFormat(ls_sql, ls_slipno, state.SsUsername, state.SsWorkDate);
                exe.SQL.Add(ls_sql);

                // ถ้าเป็นการโอนไปเงินฝาก ต้องตามไป Update สถานะเป็นไม่่โอน
                ls_sql = " select expense_accid from assslippayoutdet where assistslip_no = '" + ls_slipno + "' and item_code = 'PAY' and moneytype_code = 'TRN'";
                dt = WebUtil.QuerySdt(ls_sql);

                while (dt.Next())
                {
                    ls_deptno = dt.GetString("expense_accid");
                    ls_sql = " update dpdepttran set tran_status = -9 where deptaccount_no = '" + ls_deptno + "' and ref_slipno = '" + ls_slipno + "' ";

                    exe.SQL.Add(ls_sql);
                }

                // ถ้าเป็นการโอนไปเงินรอจ่ายคืน ต้องตามไป Update สถานะเป็นไม่่โอน
                ls_sql = " select assistslip_no from assslippayoutdet where assistslip_no = '" + ls_slipno + "' and item_code = 'PAY' and moneytype_code = 'TRN'and methpaytype_code = 'MRT'";
                dt = WebUtil.QuerySdt(ls_sql);

                while (dt.Next())
                {
                    ls_sql = " update mbmembmoneyreturn set return_status = -9 where ref_slipno = '" + ls_slipno + "' and system_code = 'ASS'";

                    exe.SQL.Add(ls_sql);
                }

            }

            try
            {
                exe.Execute();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ : " + ex);
                return;
            }

            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

            dsMain.ResetRow();
            dsList.ResetRow();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}