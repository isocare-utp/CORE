using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CoreSavingLibrary.WcfShrlon;
//using CoreSavingLibrary.WcfNCommon;

using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_req_chgmthshr_ctrl
{
    public partial class ws_mbshr_req_chgmthshr : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostChanegValue { get; set; }
        [JsPostBack]
        public string PostCheckStopShare { get; set; }
        [JsPostBack]
        public string PostGetFromDlg { get; set; }
        [JsPostBack]
        public string PostSalary { get; set; }
        
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                HdMemcoopid.Value = state.SsCoopControl;
                dsMain.DATA[0].PAYADJUST_DATE = state.SsWorkDate;
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                //dsMain.DATA[0].APVIMMEDIATE_FLAG = 1;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                
                String memcoop_id = HdMemcoopid.Value;
                decimal req_status = 0, chkresign_status = 0;
                string membno = "", is_message = "";
                membno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                chkresign_status = CheckResignStatus(membno);
                

                if (chkresign_status == 1)
                {
                    is_message = "เลขสมาชิก " + membno + " ลาออกแล้วกรุณาตรวจสอบ";
                    this.SetOnLoadedScript("alert('" + is_message + "')");
                    LtServerMessage.Text = WebUtil.WarningMessage2(is_message);
                    dsMain.ResetRow();
                    return;
                }
                else
                {
                    ///ตรวจคำขอเปลี่ยนแปลงค่าหุ้น EXAT
                    if (state.SsCoopControl == "022001")
                    {
                        req_status = CheckReqChgmthshr(membno);
                    }
                    if (req_status == 1)
                    {
                        dsMain.RetrieveDocno(HdDocno.Value);
                        is_message = "เลขสมาชิก " + membno + " มีคำขอแล้ว";
                        this.SetOnLoadedScript("alert('" + is_message + "')");
                    }
                    else
                    {
                        try
                        {
                            String as_xmlreq = dsMain.ExportXml();
                            string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                            string salary_id = GetSQL_Value("select salary_id as sql_value from mbmembmaster where member_no = '" + member_no + "' ");
                            DateTime adtm_datereq = dsMain.DATA[0].PAYADJUST_DATE;
                            

                            //int result = wcf.NShrlon.InitRequestShrPayment(state.SsWsPass, state.SsCoopId, member_no, memcoop_id, adtm_datereq, ref as_xmlreq);
                            int result = wcf.NShrlon.of_initreq_chgmthshr(state.SsWsPass, state.SsCoopId, member_no, memcoop_id, adtm_datereq, ref as_xmlreq);

                            if (result == 1)
                            {
                                dsMain.ResetRow();
                                dsMain.ImportData(as_xmlreq);
                                dsMain.DATA[0].SALARY_ID = salary_id;
                                //dsMain.DATA[0].APVIMMEDIATE_FLAG = 1;
                            }
                            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                            dsMain.DATA[0].PAYADJUST_DATE = state.SsWorkDate;
                            JsGetShareBase();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                }
            }
            else if (eventArg == PostSalary)
            {
                String memcoop_id = HdMemcoopid.Value;

                try
                {
                    String as_xmlreq = dsMain.ExportXml();
                    string salary_id = dsMain.DATA[0].SALARY_ID;

                    string member_no = GetSQL_Value("select member_no as sql_value from mbmembmaster where salary_id = '" + salary_id + "' ");
                    dsMain.DATA[0].MEMBER_NO = WebUtil.MemberNoFormat(member_no);

                    DateTime adtm_datereq = dsMain.DATA[0].PAYADJUST_DATE;

                    //int result = wcf.NShrlon.InitRequestShrPayment(state.SsWsPass, state.SsCoopId, member_no, memcoop_id, adtm_datereq, ref as_xmlreq);
                    int result = wcf.NShrlon.of_initreq_chgmthshr(state.SsWsPass, state.SsCoopId, member_no, memcoop_id, adtm_datereq, ref as_xmlreq);

                    if (result == 1)
                    {
                        dsMain.ResetRow();
                        dsMain.ImportData(as_xmlreq);
                        dsMain.DATA[0].SALARY_ID = salary_id;
                    }
                    dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                    dsMain.DATA[0].PAYADJUST_DATE = state.SsWorkDate;
                    JsGetShareBase();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == PostChanegValue)
            {

                decimal salary_amount = dsMain.DATA[0].SALARY_AMOUNT;
                decimal new_periodvalue = dsMain.DATA[0].NEW_PERIODVALUE;
                decimal periodbase_value = dsMain.DATA[0].PERIODBASE_VALUE;
                //wa กรณีลดหุ้นต่ำกว่าหุ้นฐานให้อ่านค่างคงที่มาตรวจด้วย
                decimal share_period = dsMain.DATA[0].SHRLAST_PERIOD;
                decimal sharestk_value = dsMain.DATA[0].SHARESTK_VALUE;
                if (new_periodvalue % 10 != 0)
                {
                    this.SetOnLoadedScript("มูลค่าหุ้นต่อ 1 หน่วย ไม่ตรงตามฐาน");
                    dsMain.DATA[0].NEW_PERIODVALUE = new_periodvalue;
                }
                    // ปิดไว้แม่โจ้ไม่ใช่
                //else if (new_periodvalue < periodbase_value && share_period < 120 && sharestk_value < 60000)
                //{
                //    decimal new_paystatus = dsMain.DATA[0].NEW_PAYSTATUS;

                //    if (new_paystatus != -1)
                //    {
                //        this.SetOnLoadedScript("alert('หุ้นที่เปลี่ยนน้อยกว่าหุ้นฐาน หุ้นฐาน = " + periodbase_value + "')");
                //        dsMain.DATA[0].NEW_PERIODVALUE = periodbase_value;
                //    }
                //    else {
                //        dsMain.DATA[0].NEW_PERIODVALUE = 0;
                //    }
                //}
                else
                {

                    decimal loan_pay = 0, salary_balance = 0;
                    string sql = @"SELECT sum(lncontmaster.period_payment) as loan_pay
                    FROM lncontmaster, 
                    lnloantype 
                    WHERE ( lncontmaster.loantype_code = lnloantype.loantype_code ) 
                    and ( lncontmaster.coop_id = lnloantype.coop_id ) 
                    and ( ( lncontmaster.memcoop_id = {0} ) 
                    and ( lncontmaster.member_no = {1} )
                    and ( lncontmaster.loanpay_code = 'KEP' )
                    and ( lncontmaster.principal_balance + lncontmaster.withdrawable_amt > 0 ) 
                    and ( lncontmaster.contract_status > 0 ) ) ";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO);
                    Sdt dt = WebUtil.QuerySdt(sql);

                    if (dt.Next())
                    {
                        loan_pay = dt.GetDecimal("loan_pay");
                    }

                    salary_balance = salary_amount - new_periodvalue - loan_pay;

                    if (salary_balance < 2000)
                    {
                        this.SetOnLoadedScript("alert('เงินเดือนคงเหลือไม่เป็นไปตามที่กำหนด \\nเงินเดือน = " + salary_amount.ToString("#,##0.00") + "\\nหุ้นที่ต้องการเพิ่ม = " + new_periodvalue.ToString("#,##0.00") + "\\nหักหนี้โดยประมาณ = " + loan_pay.ToString("#,##0.00") + "\\nเงินเดือนคงเหลือ = " + salary_balance.ToString("#,##0.00") + "\\nเงินเดือนคงเหลือขั้นต่ำ = 2,000.00 \\nกรุณาตรวจสอบด้วย')");
                    }
                }
                //------------------------//
                if (state.SsCoopId == "011001") // ทำให้ PTT
                {
                    string sql = @"SELECT end_salary , maxshare_amt ,minshare_amt
                    FROM shsharetypemthrate
                    where coop_ID = '011001'
                     ";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    decimal[] arrsalary = new decimal[dt.GetRowCount()];
                    decimal[] arrmaxshare = new decimal[dt.GetRowCount()];
                    decimal[] arrminshare = new decimal[dt.GetRowCount()];
                    if (dt.Next())
                    {
                        for (int i = 0; i < dt.GetRowCount(); i++)
                        {
                            arrsalary[i] = decimal.Parse(dt.Rows[i]["end_salary"].ToString());
                            arrmaxshare[i] = decimal.Parse(dt.Rows[i]["maxshare_amt"].ToString())*10;
                            arrminshare[i] = decimal.Parse(dt.Rows[i]["minshare_amt"].ToString()) * 10;

                            if (dsMain.DATA[0].SALARY_AMOUNT < arrsalary[i])
                            {
                                if (dsMain.DATA[0].NEW_PERIODVALUE > arrmaxshare[i])
                                {

                                    this.SetOnLoadedScript("alert('สมาชิกคนนี้ ผิดเงื่อนไขการขอลด/เพิ่ม ส่งหุ้น \\nส่งหุ้นสูงกว่าหุ้นตามฐานเงินเดือน หุ้นตามฐานเงินเดือน หุ้นตามฐาน = "+arrmaxshare[i].ToString("#,##0.00")+"')");
                                    break;
                                }

                                if (dsMain.DATA[0].NEW_PERIODVALUE < arrminshare[i])
                                {

                                    this.SetOnLoadedScript("alert('สมาชิกคนนี้ ผิดเงื่อนไขการขอลด/เพิ่ม ส่งหุ้น \\nส่งหุ้นต่ำกว่าหุ้นตามฐานเงินเดือน หุ้นตามฐานเงินเดือน หุ้นตามฐาน = " + arrminshare[i].ToString("#,##0.00") + "')");
                                    break;
                                }

                                break;

                            }
                        }

                        
                    }
                
                }
             //-----------------------end-----------------------//
            }
            else if (eventArg == PostCheckStopShare)
            {
                decimal chkchgstopshr = CheckStopSharePeriod();
                decimal new_paystatus = dsMain.DATA[0].NEW_PAYSTATUS;
                if (new_paystatus == -1)
                {
                    if (chkchgstopshr == 1)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("งวดหุ้น = " + dsMain.DATA[0].SHRLAST_PERIOD + " ไม่สามารถงดส่งค่าหุ้นได้");
                        //dsMain.DATA[0].NEW_PAYSTATUS = 1;
                    }
                }
            }
            else if (eventArg == PostGetFromDlg)
            {
                dsMain.RetrieveDocno(HdDocno.Value);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String as_xmlreq = dsMain.ExportXml();
                DateTime payadjust_date = dsMain.DATA[0].PAYADJUST_DATE;
                
                

                decimal apvimmediate_flag = dsMain.DATA[0].APVIMMEDIATE_FLAG;

             
                //int result = wcf.NShrlon.SaveRequestShrPayment(state.SsWsPass, as_xmlreq, state.SsUsername, payadjust_date);
                int result = wcf.NShrlon.of_savereq_chgmthshr(state.SsWsPass, as_xmlreq, state.SsUsername, payadjust_date);

                if (result == 1)
                {



                    if (apvimmediate_flag != 1)
                    {
                        string payadjust_docno = "";

                        string sql = @" 
                               select max(payadjust_docno) as  payadjust_docno from shpaymentadjust ";
                        sql = WebUtil.SQLFormat(sql);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            payadjust_docno = dt.GetString("payadjust_docno");
                        }
                        
                        ExecuteDataSource exe1 = new ExecuteDataSource(this);
                        String sqlupdate1 = "update shpaymentadjust set shrpayadj_status = 8 where payadjust_docno = '" + payadjust_docno + "' ";
                        exe1.SQL.Add(sqlupdate1);
                        exe1.Execute();

                    }
                   

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    JsNewClear();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            if (state.SsCoopControl == "020001")
            {
                dsMain.DATA[0].APVIMMEDIATE_FLAG = 1;
            }
        }

        private void JsNewClear()
        {
            dsMain.ResetRow();
            HdMemcoopid.Value = state.SsCoopControl;
            dsMain.DATA[0].PAYADJUST_DATE = state.SsWorkDate;
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
        }

        private void JsGetShareBase()
        {
            string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
            Decimal member_type = WebUtil.GetMemberType(state.SsCoopControl, member_no);
            string sharetype_code = WebUtil.GetShareType(state.SsCoopControl, member_no);

            Decimal salary_amount = dsMain.DATA[0].SALARY_AMOUNT;

            string coop_id = state.SsCoopId;

            Decimal minmaxshare = CalPayShareMonth(coop_id, sharetype_code, member_type, salary_amount);

            dsMain.DATA[0].PERIODBASE_VALUE = minmaxshare;
        }

        //คำนวณหุ้นฐาน TKS
        private Decimal CalPayShareMonth(String memcoop_id, String sharetype_code, Decimal member_type, Decimal salary_amount)
        {
            Decimal share = 0;
            member_type = 1;
            string sql1 = @"select * from shsharetypemthrate where coop_id = {0} and sharetype_code= {1} and {2} >= start_salary and {3} <= end_salary and member_type = {4}";
            string sql2 = @"select * from shsharetype where coop_id ={0} and sharetype_code = {1} and 1 = 1";

            sql1 = WebUtil.SQLFormat(sql1, state.SsCoopId, sharetype_code, salary_amount, salary_amount, member_type);
            sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, sharetype_code);

            Sdt dt = WebUtil.QuerySdt(sql1);
            Sdt dt2 = WebUtil.QuerySdt(sql2);

            bool Isdt2 = dt2.Next();
            while (dt.Next())
            {
                decimal maxshare_percent = dt.GetDecimal("maxshare_percent");
                decimal maxshare = dt.GetDecimal("maxshare_amt") * dt2.GetDecimal("unitshare_value");
                if (member_type == 1)
                {
                    maxshare_percent = dt.GetDecimal("maxshare_percent") * salary_amount;
                }
                else
                {
                    maxshare_percent = maxshare;
                }
                decimal temp;
                if (maxshare_percent >= maxshare)
                {
                    temp = maxshare;
                }
                else
                {
                    temp = maxshare_percent;
                }

                share = dt.GetDecimal("minshare_amt") * dt2.GetDecimal("unitshare_value");
            }
            return share;
        }

        private decimal CheckStopSharePeriod()
        {
            String coop_id = state.SsCoopControl;
            String member_no = dsMain.DATA[0].MEMBER_NO;
            String sharetype_code = WebUtil.GetShareType(state.SsCoopControl, dsMain.DATA[0].MEMBER_NO);
            decimal timeminshare_stop = 0;
            decimal chkchgstopshr = 0;

            string sql1 = "select * from shsharemaster where member_no ='" + member_no + "' and coop_id ='" + coop_id + "'";
            string sql2 = "select * from shsharetype  where sharetype_code ='" + sharetype_code + "' and coop_id ='" + coop_id + "'";

            Sdt dt1 = WebUtil.QuerySdt(sql1);
            Sdt dt2 = WebUtil.QuerySdt(sql2);

            bool Isdt2 = dt1.Next();
            sharetype_code = dt1.GetString("sharetype_code");

            while (dt2.Next())
            {
                int count = 0;
                //DateTime chkmemdate =dt.GetDate("chkmemdate");
                Decimal last_period = Convert.ToDecimal(dt1.GetDecimal("last_period"));
                timeminshare_stop = Convert.ToDecimal(dt2.GetString("timeminshare_stop"));
                //เป็นสมาชิกไม่น้อยกว่า 15 ปี
                //if (chkmemdate<=DateTime.Now)
                //{
                //    count++;
                //}
                //ส่งค่าหุ้นมาแล้วไม่น้อยกว่า 180
                if (last_period >= timeminshare_stop)
                {
                    count++;
                }
                //ไม่มีหนี้สินกับสหกรณ์
                if (1 == 1)
                {
                    count++;
                }

                if (count == 2)
                {
                    chkchgstopshr = 0;
                }
                else
                {
                    chkchgstopshr = 1;
                }
            }
            return chkchgstopshr;
        }

        public string GetSQL_Value(string Select_Condition)
        {
            string sql_value = "";
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                sql_value = dt.GetString("sql_value");
            }
            return sql_value;
        }

        //เข็คสถานะกรณีมีคำขอการเปลี่ยนแปลงหุ้นแล้ว
        private decimal CheckReqChgmthshr(String MemberNo)
        {
            decimal checkstatus = 0;
            string payadjust_docno = "";
            string se = @"select * from shpaymentadjust where coop_id ={0} and member_no = {1} and shrpayadj_status = 8 ";
            se = WebUtil.SQLFormat(se, state.SsCoopId, MemberNo);
            Sdt dt = WebUtil.QuerySdt(se);
            if (dt.Next())
            {
                payadjust_docno = dt.GetString("payadjust_docno").Trim();
            }
            if (payadjust_docno != null && payadjust_docno != "")
            {
                HdDocno.Value = payadjust_docno;
                checkstatus = 1;
            }
            return checkstatus;
        }
        //เข็คสถานะสมาชิกลาออก
        private decimal CheckResignStatus(String MemberNo)
        {
            decimal chkresign_status = 0;
            string se = @"select resign_status from mbmembmaster where coop_id = {0} and member_no = {1}";
            se = WebUtil.SQLFormat(se, state.SsCoopId, MemberNo);
            Sdt dt = WebUtil.QuerySdt(se);
            if (dt.Next())
            {
                chkresign_status = dt.GetDecimal("resign_status");
            }
            return chkresign_status;
        }
    }
}