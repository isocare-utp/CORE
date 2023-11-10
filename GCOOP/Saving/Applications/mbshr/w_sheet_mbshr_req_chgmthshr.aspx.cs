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
using DataLibrary;
using System.Globalization;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_req_chgmthshr : PageWebSheet, WebSheet
    {

        private DwThDate tdw_data;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String memNoItemChange;
        protected String memNoFromDlg;
        protected String newClear;
        protected String jsGetShareBase;
        protected String jsChanegValue;
        protected String jsCheckStopShare;
        protected String postSalaryId;
        private void SetAvpDateAllRow()
        {

            dw_data.SetItemDateTime(1, "payadjust_date", state.SsWorkDate);

        }

        public void InitJsPostBack()
        {
            newClear = WebUtil.JsPostBack(this, "newClear");
            memNoItemChange = WebUtil.JsPostBack(this, "memNoItemChange");
            memNoFromDlg = WebUtil.JsPostBack(this, "memNoFromDlg");
            jsGetShareBase = WebUtil.JsPostBack(this, "jsGetShareBase");
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsCheckStopShare = WebUtil.JsPostBack(this, "jsCheckStopShare");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
            tdw_data = new DwThDate(dw_data, this);
            tdw_data.Add("payadjust_date", "payadjust_tdate");
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
            dw_data.SetTransaction(sqlca);

            if (IsPostBack)
            {

                try
                {

                    dw_data.RestoreContext();

                    HdIsPostBack.Value = "true";
                }
                catch { }

            }
            else
            {
                dw_data.InsertRow(0);
                dw_data.SetItemDate(1, "payadjust_date", state.SsWorkDate);
                dw_data.SetItemString(1, "entry_id", state.SsUsername);
                DwUtil.RetrieveDDDW(dw_data, "select_coop", "sl_shrpayment_adjust.pbl", state.SsCoopControl);
                tdw_data.Eng2ThaiAllRow();
                HdIsPostBack.Value = "false";
                HidMemcoopid.Value = state.SsCoopControl;
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "memNoItemChange")
            {
                GetMemberDetail();
            }
            else if (eventArg == "memNoFromDlg")
            {
                JsGetDocNo();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();

            }
            else if (eventArg == "jsGetShareBase")
            {
                JsGetShareBase();

            }
            else if (eventArg == "jsChanegValue")
            {
                JsChanegValue();

            }
            else if (eventArg == "jsCheckStopShare")
            {
                JsCheckStopShare();

            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
            }
        }

        private void JsGetDocNo()
        {
            string docno = HdDocno.Value;
            dw_data.Retrieve(state.SsCoopId, docno);
            tdw_data.Eng2ThaiAllRow();
        }

        private void JsCheckStopShare()
        {
            String member_no = dw_data.GetItemString(1, "member_no");
            string sharetype = WebUtil.GetShareType(state.SsCoopControl, member_no);
            decimal chkchgstopshr = CheckStopSharePeriod();
            decimal shradjust_status = dw_data.GetItemDecimal(1, "new_paystatus");
            if (shradjust_status == -1)
            {
                if (chkchgstopshr == 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("" + member_no + " ไม่สามารถงดส่งค่าหุ้นได้");
                    JsNewClear();
                }
                else
                {
                    dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                }
            }
        }
        private decimal CheckStopSharePeriod()
        {
            String coop_id = state.SsCoopControl;
            String member_no = dw_data.GetItemString(1, "member_no");
            String sharetype_code = WebUtil.GetShareType(state.SsCoopControl, dw_data.GetItemString(1, "member_no"));
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
        private void JsGetShareBase()
        {
            //Decimal adc_salary = dw_data.GetItemDecimal(1, "salary_amount");
            //Decimal adc_sharebase = 0, adc_minshare = 0;
            //int result = shrlonService.GetShareBase(state.SsWsPass,state.SsCoopId, adc_salary, ref adc_sharebase);

            string member_no = WebUtil.MemberNoFormat(dw_data.GetItemString(1, "member_no"));
            Decimal member_type = WebUtil.GetMemberType(state.SsCoopControl, member_no);
            string sharetype_code = WebUtil.GetShareType(state.SsCoopControl, member_no);

            Decimal salary_amount, incomeetc_amt, total;
            try { salary_amount = dw_data.GetItemDecimal(1, "salary_amount"); }
            catch { salary_amount = 0; }
            try
            {
               incomeetc_amt = dw_data.GetItemDecimal(1, "incomeetc_amt");
            }
            catch { incomeetc_amt = 0; }
            // เงินเดือน + เงินอื่นๆ(ค่าไฟ) สำหับ กฟภ.  ไม่กระทบ ธกส.
            total = salary_amount + incomeetc_amt;
            //DataTable dt = WebUtil.Query("select sharetype_code from shsharemaster where member_no ='" + member_no + "' and coop_id ='" + state.SsCoopControl + "'");
            //if (dt.Rows.Count > 0)
            //{
            //    sharetype_code = dt.Rows[0]["sharetype_code"].ToString();
            //}
            string coop_id = state.SsCoopId;

            string minmaxshare = CalPayShareMonth(sharetype_code, state.SsCoopControl, total, member_type);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);
            //Decimal[] minmaxshare = CalPayShareMonth(sharetype_code, coop_id, salary_amount, member_type);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);
            dw_data.SetItemDecimal(1, "periodbase_value", minmaxshare[0]);

        }

        private void GetMemberDetail()
        {
            String memcoop_id = HidMemcoopid.Value;

            try
            {
                String as_xmlreq = dw_data.Describe("DataWindow.Data.XML");
                string member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
                DateTime adtm_datereq = dw_data.GetItemDateTime(1, "payadjust_date");
                int result = shrlonService.of_initreq_chgmthshr(state.SsWsPass, state.SsCoopId, member_no, memcoop_id, adtm_datereq, ref as_xmlreq);

                dw_data.Reset();
                dw_data.ImportString(as_xmlreq, FileSaveAsType.Xml);
                dw_data.SetItemString(1, "member_no", member_no);
                dw_data.SetItemString(1, "memcoop_id", memcoop_id);
                dw_data.SetItemDateTime(1, "payadjust_date", adtm_datereq);
                tdw_data.Eng2ThaiAllRow();
                if (dw_data.RowCount > 1)
                {
                    dw_data.DeleteRow(dw_data.RowCount);
                }

                JsGetShareBase();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsChanegValue()
        {
            String memcoop_id = HidMemcoopid.Value;
            Decimal minshare_low = 0, timeminshare_low = 0;
            string member_no = dw_data.GetItemString(1, "member_no");
            string sharetype = WebUtil.GetShareType(state.SsCoopControl, member_no);
            decimal member_type = WebUtil.GetMemberType(state.SsCoopControl, member_no);
            decimal old_periodvalue = dw_data.GetItemDecimal(1, "old_periodvalue");
            Decimal salary_amount = dw_data.GetItemDecimal(1, "salary_amount");
            //DataTable dt = WebUtil.Query("select sharetype_code from shsharemaster where member_no ='" + member_no + "' and coop_id ='" + state.SsCoopControl + "'");
            //if (dt.Rows.Count > 0)
            //{
            //    sharetype_code = dt.Rows[0]["sharetype_code"].ToString();
            //}

            string minmaxshare = CalPayShareMonth(sharetype, state.SsCoopControl, salary_amount, member_type);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);

            Decimal newshare_value = dw_data.GetItemDecimal(1, "new_periodvalue");
            Decimal shrlast_period = dw_data.GetItemDecimal(1, "shrlast_period");
            Decimal sharestk_value = dw_data.GetItemDecimal(1, "sharestk_value");
            DataLibrary.Sdt dt2 = WebUtil.QuerySdt("select minshare_low,timeminshare_low from shsharetype");
            if (dt2.Next())
            {

                minshare_low = dt2.GetDecimal("minshare_low");
                timeminshare_low = dt2.GetDecimal("timeminshare_low");
            }

            if (newshare_value % 10 != 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่อ 1 หน่วย ไม่ตรงตามฐาน");
                dw_data.SetItemDecimal(1, "new_periodvalue", 0);
            }
            else
            {
                if (newshare_value < old_periodvalue)
                {
                    if (shrlast_period < timeminshare_low || sharestk_value < minshare_low)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนงวดหุ้นต่ำกว่าที่ลดส่งได้ งวดหุ้นต่ำสุดที่ลดส่งได้คือ " + timeminshare_low + " หรือจำนวนขั้นต่ำน้อยกว่าที่ลดส่งได้ จำนวนขั้นต่ำที่งดส่งได้คือ " + minshare_low);
                    }
                    else
                    {
                        //string chkchgshr = wcf.InterPreter.CountAllChangeShare(state.SsConnectionIndex, memcoop_id, sharetype, member_no);
                        string chkchgshr = CountAllChangeShare(memcoop_id, member_no, sharetype);
                        if (chkchgshr == "1")
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนการเปลี่ยนแปลงค่าหุ้นเกินกว่าที่กำหนด");
                            dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                        }
                        else
                        {
                            if (newshare_value < minmaxshare[0])
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                                dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                            }
                        }
                    }
                }
                else
                {
                    if (newshare_value > old_periodvalue)
                    {
                        if (newshare_value > minmaxshare[1])
                        {
                            //LtServerMessage.Text = WebUtil.WarningMessage("มูลค่าหุ้นสูงกว่าหุ้นตามฐาน");
                            //dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                        }
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("มูลค่าหุ้นเท่ากับค่าหุ้นเดิม");
                        // dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                    }
                }
            }

        }

        private void JsNewClear()
        {
            dw_data.Reset();

            //DateTime payadjust_date = Convert.ToDateTime(Session["payadjust_date"].ToString());
            //dw_data.SetItemDate(1, "payadjust_date", payadjust_date);
            //dw_data.SetItemString(1, "entry_id", state.SsUsername);
            //tdw_data.Eng2ThaiAllRow();
            dw_data.InsertRow(0);
            dw_data.SetItemDate(1, "payadjust_date", state.SsWorkDate);
            dw_data.SetItemString(1, "entry_id", state.SsUsername);
            tdw_data.Eng2ThaiAllRow();
            HdIsPostBack.Value = "false";
        }

        public void SaveWebSheet()
        {
            try
            {
                Decimal new_periodvalue = dw_data.GetItemDecimal(1, "new_periodvalue");
                Decimal old_periodvalue = dw_data.GetItemDecimal(1, "old_periodvalue");
                Decimal new_paystatus = dw_data.GetItemDecimal(1, "new_paystatus");
                Decimal newshare_value = dw_data.GetItemDecimal(1, "new_periodvalue");
                Decimal periodbase_value = dw_data.GetItemDecimal(1, "periodbase_value");
                // Decimal new_paystatus = dw_data.GetItemDecimal(1, "new_paystatus"); 
                String as_xmlreq = dw_data.Describe("DataWindow.Data.XML");

                DateTime payadjust_date = dw_data.GetItemDate(1, "payadjust_date");

                Session["payadjust_date"] = payadjust_date;

                String entry_id = state.SsUsername;
                str_mbreqresign mbreqresign = new str_mbreqresign();

                if (new_paystatus == 1)
                {
                    if (newshare_value == old_periodvalue)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นเท่ากับค่าหุ้นเดิม");
                        dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                    }
                    else if (newshare_value < periodbase_value)
                    {

                        LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                        dw_data.SetItemDecimal(1, "new_periodvalue", 0);
                    }
                    else
                    {
                        int result = shrlonService.of_savereq_chgmthshr(state.SsWsPass, as_xmlreq, entry_id, payadjust_date);
                        if (result == 1)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                            JsNewClear();
                        }
                    }
                }
                else
                {
                    int result = shrlonService.of_savereq_chgmthshr(state.SsWsPass, as_xmlreq, entry_id, payadjust_date);
                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                        JsNewClear();
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            DateTime payadjust_date = Convert.ToDateTime(Session["payadjust_date"].ToString());
            dw_data.SetItemDate(1, "payadjust_date", payadjust_date);
            tdw_data.Eng2ThaiAllRow();

            dw_data.SetItemString(1, "entry_id", state.SsUsername);
            dw_data.SaveDataCache();
        }

        private void JsPostSalaryId()
        {
            String salary_id = dw_data.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                //เซตค่าของเลขสมาชิกที่ได้มาจากเลขพนักงานให้กับตัวแปร Hfmember_no
                Hfmember_no.Value = dtMemb.GetString("member_no");
                dw_data.SetItemString(1, "member_no", Hfmember_no.Value);
                GetMemberDetail();
            }
            else
            {
                this.JsNewClear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + Hfmember_no.Value);
            }
        }

        public string CountAllChangeShare(string coop_id, string member_no, string sharetype)
        {
            String result = "";
            //int conIndex = int.Parse(Request["con_index"]);
            //string xmlPath = WebInterpreter.IntUtility.GcoopPath;

            //DataLibrary.Sta ta = new DataLibrary.Sta(conIndex, xmlPath);

            DataLibrary.Sdt dt = WebUtil.QuerySdt("select count(payadjust_docno)as countchg from shpaymentadjust where member_no ='" + member_no + "' and coop_id ='" + coop_id + "' and old_periodvalue > new_periodvalue");
            DataLibrary.Sdt dt2 = WebUtil.QuerySdt("select *  from shsharetype where coop_id ='" + coop_id + "' and sharetype_code = '" + sharetype + "'");

            bool Isdt2 = dt2.Next();
            while (dt.Next())
            {
                if (dt.GetDecimal("countchg") >= dt2.GetDecimal("chgcountlow_amt"))
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            return result;
        }

        public string CalPayShareMonth(String shareType, String coop_control, decimal salary, decimal member_type)
        {
            String result = "";

            if (member_type != 2)
            {
                member_type = 1; //HC By Bank สำหรับ หาค่าหุ้นฐาน สมาชิกโอนย้ายจาก สอ. อื่น
                DataLibrary.Sdt dt = WebUtil.QuerySdt("select * from shsharetypemthrate where sharetype_code='" + shareType + "' and coop_id ='" + coop_control + "' and " + salary + " >= start_salary and " + salary + " <= end_salary and member_type =" + member_type + " ");
                DataLibrary.Sdt dt2 = WebUtil.QuerySdt("select * from shsharetype where sharetype_code='" + shareType + "' and coop_id ='" + coop_control + "'and 1 = 1 ");
                bool Isdt2 = dt2.Next();
                while (dt.Next())
                {
                    //decimal d2 = Convert.ToDecimal( dt2.Rows[0][""]);
                    decimal maxshare_percent = dt.GetDecimal("maxshare_percent");
                    decimal maxshare = dt.GetDecimal("maxshare_amt") * dt2.GetDecimal("unitshare_value");
                    if (member_type == 1)
                    {
                        maxshare_percent = dt.GetDecimal("maxshare_percent") * salary;
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
                    result = "min" + (dt.GetDecimal("minshare_amt") * dt2.GetDecimal("unitshare_value")) + "max" + temp;
                    //Response.Write("min" + (dt.GetDecimal("minshare_amt") * dt2.GetDecimal("unitshare_value")) + "max" + temp);
                }
            }
            else
            {
                result = "min" + 0 + "max" + 0;
            }
            return result;
        }
    }
}

