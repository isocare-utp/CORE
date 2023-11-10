using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_edit_salary_ctrl
{
    public partial class ws_sl_edit_salary : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostSalary { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
            }
            else if (eventArg == "PostSalary")
            {
                String memcoop_id = state.SsCoopControl;

                Decimal member_type = dsMain.DATA[0].MEMBER_TYPE;
                Decimal salary_amount, incomeetc_amt;
                try { salary_amount = dsMain.DATA[0].new_salary; }
                catch { salary_amount = 0; }
                try { incomeetc_amt = dsMain.DATA[0].INCOMEETC_AMT; }
                catch { incomeetc_amt = 0; }
                Decimal total = salary_amount + incomeetc_amt;
                String sharetype_code = dsMain.DATA[0].SHARETYPE_CODE;

                Decimal minmaxshare = CalPayShareMonth(memcoop_id, sharetype_code, member_type, total);
                // ปัด 100
                minmaxshare = Math.Ceiling(minmaxshare / 100) * 100;
                //
                decimal periodshare_value = dsMain.DATA[0].periodshare_value;
                if (periodshare_value > minmaxshare)
                {
                    dsMain.DATA[0].new_periodshare_value = periodshare_value;
                }
                else
                {
                    dsMain.DATA[0].new_periodshare_value = minmaxshare;
                }
                dsMain.DATA[0].new_periodbase_value = minmaxshare;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                decimal unitshare_value = 0, adjsalarychgshrperiod_flag = 0;
                string se = @"select * from shsharetype where coop_id = {0} and sharetype_code = {1}";
                se = WebUtil.SQLFormat(se, state.SsCoopControl, dsMain.DATA[0].SHARETYPE_CODE);
                Sdt dt2 = WebUtil.QuerySdt(se);
                if (dt2.Next())
                {
                    unitshare_value = dt2.GetDecimal("unitshare_value");
                    adjsalarychgshrperiod_flag = dt2.GetDecimal("adjsalarychgshrperiod_flag");
                }
                decimal periodbase_amt = dsMain.DATA[0].new_periodbase_value / unitshare_value;
                decimal periodshare_amt = dsMain.DATA[0].new_periodshare_value / unitshare_value;
                string memb_no = dsMain.DATA[0].MEMBER_NO;
                decimal salary_amount = dsMain.DATA[0].new_salary;

                string last_docno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "MBADJSAL");

                if (adjsalarychgshrperiod_flag == 1)
                {
                    try
                    {
                        String sqlinsert = @"update shsharemaster set periodbase_amt={0},periodshare_amt={1} where member_no ={2}";
                        sqlinsert = WebUtil.SQLFormat(sqlinsert, periodbase_amt, periodshare_amt, memb_no);
                        WebUtil.Query(sqlinsert);
                    }
                    catch { }
                }
                try
                {
                    String sqlinsert1 = @"update mbmembmaster set salary_amount={0} ,incomeetc_amt={2} where member_no ={1}";
                    sqlinsert1 = WebUtil.SQLFormat(sqlinsert1, salary_amount, memb_no, dsMain.DATA[0].INCOMEETC_AMT);
                    WebUtil.Query(sqlinsert1);
                }
                catch { }

                try
                {
                    string sqlinsert2 = @"insert into mbadjsalary(
                                            coop_id,
                                            adjslip_no,
                                            adjsal_type,
                                            operate_date,
                                            sharetype_code,
                                            member_no,
                                            old_salary,
                                            old_sharebase,
                                            old_shareperiod,
                                            new_salary,
                                            new_sharebase,
                                            new_shareperiod,
                                            posting_flag,
                                            entry_id,
                                            entry_date,
                                            entry_bycoopid) 
                                          values
                                            ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})";
                    sqlinsert2 = WebUtil.SQLFormat(sqlinsert2, state.SsCoopControl, last_docno, "MEM", state.SsWorkDate,
                                 dsMain.DATA[0].SHARETYPE_CODE, memb_no, dsMain.DATA[0].SALARY_AMOUNT, dsMain.DATA[0].PERIODBASE_AMT,
                                 dsMain.DATA[0].PERIODSHARE_AMT, salary_amount, periodbase_amt, periodshare_amt, "1", state.SsUsername,
                                 state.SsWorkDate, state.SsCoopId);
                    WebUtil.Query(sqlinsert2);
                }
                catch { }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                dsMain.ResetRow();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {


        }

        private Decimal CalPayShareMonth(String memcoop_id, String sharetype_code, Decimal member_type, Decimal salary_amount)
        {
            Decimal share_mth = 0, shrperunit = 0;
            Decimal ldc_minshr = 0, ldc_maxshr = 0;
            member_type = dsMain.DATA[0].MEMBER_TYPE;
            string sql1 = @"select * from shsharetypemthrate where coop_id = {0} and sharetype_code= {1} and {2} >= start_salary and {3} <= end_salary and member_type = {4}";
            string sql2 = @"select * from shsharetype where coop_id ={0} and sharetype_code = {1} and 1 = 1";

            sql1 = WebUtil.SQLFormat(sql1, state.SsCoopId, sharetype_code, salary_amount, salary_amount, member_type);
            sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, sharetype_code);

            Sdt dt = WebUtil.QuerySdt(sql1);
            Sdt dt2 = WebUtil.QuerySdt(sql2);

            //bool Isdt2 = dt2.Next();

            if (dt2.Next())
            {
                shrperunit = dt2.GetDecimal("unitshare_value");
            }
            else
            {
                shrperunit = 10;
            }

            if (dt.Next())
            {
                decimal sharerate_percent = dt.GetDecimal("sharerate_percent") / 100;

                share_mth = sharerate_percent * salary_amount;

                ldc_maxshr = dt.GetDecimal("maxshare_amt") * shrperunit;
                ldc_minshr = dt.GetDecimal("minshare_amt") * shrperunit;



                //ตรวจสอบขั้นต่ำ
                if (share_mth < ldc_minshr)
                {
                    share_mth = ldc_minshr;
                }

                //ตรวจสอบขั้นสูงสุด
                if (share_mth > ldc_maxshr)
                {
                    share_mth = ldc_maxshr;
                }
            }

            return share_mth;
        }
    }
}