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

using DataLibrary;
using System.Globalization;
//using CoreSavingLibrary.WcfShrlon;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_req_mbresign : PageWebSheet, WebSheet
    {
        private DwThDate tdw_head;
        //private ShrlonClient shrlonService;
        //private CommonClient commonService;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String memNoItemChange;
        protected String memNoFromDlg;
        protected String newClear;
        protected String dateChange;
        protected String jsCoopSelect;
        protected String jsResignCause;
        protected String postSalaryId;
        protected String postResigndtfalg;
        protected String postApv;

        public void InitJsPostBack()
        {
            memNoItemChange = WebUtil.JsPostBack(this, "memNoItemChange");
            memNoFromDlg = WebUtil.JsPostBack(this, "memNoFromDlg");
            newClear = WebUtil.JsPostBack(this, "newClear");
            dateChange = WebUtil.JsPostBack(this, "dateChange");
            jsCoopSelect = WebUtil.JsPostBack(this, "jsCoopSelect");
            jsResignCause = WebUtil.JsPostBack(this, "jsResignCause");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
            postResigndtfalg = WebUtil.JsPostBack(this, "postResigndtfalg");
            postApv = WebUtil.JsPostBack(this, "postApv");
            tdw_head = new DwThDate(dw_head, this);
            tdw_head.Add("resignreq_date", "resignreq_tdate");
            tdw_head.Add("apv_date", "approve_tdate");
            tdw_head.Add("resigndtfix_date", "resigndtfix_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //shrlonService = wcf.NShrlon;
                //commonService = wcf.NCommon;
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            try
            {
                this.ConnectSQLCA();
                dw_head.SetTransaction(sqlca);
                dw_sum.SetTransaction(sqlca);
                dw_share.SetTransaction(sqlca);
                dw_loan.SetTransaction(sqlca);
                dw_deposit.SetTransaction(sqlca);
                dw_grt.SetTransaction(sqlca);

                if (!IsPostBack)
                {
                    JsNewClear();
                }
                else
                {
                    HdIsPostBack.Value = "true";
                    //try
                    //{
                    //    String tDate1 = dw_head.GetItemString(1, "approve_tdate");
                    //    DateTime dt1 = DateTime.ParseExact(tDate1, "ddMMyyyy", WebUtil.TH);
                    //    dw_head.SetItemDateTime(1, "approve_date", dt1);
                    //}
                    //catch (Exception ex) { }
                    this.RestoreContextDw(dw_head);
                    this.RestoreContextDw(dw_sum);
                    this.RestoreContextDw(dw_share);
                    this.RestoreContextDw(dw_loan);
                    this.RestoreContextDw(dw_grt);
                    this.RestoreContextDw(dw_deposit);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

            //if (dw_head.RowCount < 1)
            //{
            //    HdIsPostBack.Value = "false";
            //    //dw_coop.InsertRow(0);
            //    dw_head.InsertRow(0);
            //    dw_sum.InsertRow(0);
            //    dw_share.InsertRow(0);
            //    dw_loan.InsertRow(0);
            //    dw_grt.InsertRow(0);
            //    dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
            //    //dw_head.SetItemDate(1, "approve_date", state.SsWorkDate);
            //    dw_head.SetItemDecimal(1, "check_resigncause", 1);
            //    dw_deposit.InsertRow(0);
            //    DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", state.SsCoopId);
            //    HdCheckSave.Value = "True";
            //    // DwUtil.RetrieveDDDW(dw_coop, "memcoop_select", "mbshr_common.pbl", state.SsCoopControl);
            //    tdw_head.Eng2ThaiAllRow();
            //}
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "memNoItemChange")
            {
                GetMemberDetail();
            }
            else if (eventArg == "memNoFromDlg")
            {
                Jsgetdocno();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "dateChange")
            {
                DateChange();
            }
            else if (eventArg == "jsResignCause")
            {
                JsResignCause();
            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
            }
            else if (eventArg == "postResigndtfalg")
            {
                PostResigndtfalg();
            }
            else if (eventArg == "postApv")
            {
                PostApv();
            }
        }

        private void Jsgetdocno()
        {
            JsNewClear();
            string doc_no = HdDocno.Value;
            dw_head.Retrieve(state.SsCoopId, doc_no);
            // dw_sum.Retrieve(state.SsCoopId, doc_no);
            dw_share.Retrieve(state.SsCoopId, doc_no);
            dw_loan.Retrieve(state.SsCoopId, doc_no);
            dw_deposit.Retrieve(state.SsCoopId, doc_no);
            dw_grt.Retrieve(state.SsCoopId, doc_no);
        }

        private void JsResignCause()
        {
            decimal sum_loan = 0;
            decimal sum_intloan = 0;
            decimal sum_share = 0;
            decimal sum_dept = 0;
            decimal count_coll = 0;
            decimal sum_pos = 0;
            decimal sum_nav = 0;
            // by mong
            decimal li_checkshrlon = 0, li_checkcoll = 0;
            decimal check_resign = dw_head.GetItemDecimal(1, "check_resigncause");
            string ls_resigncause = dw_head.GetItemString(1, "resigncause_code");

            string ls_sqlcause = " select  checkshrlon_status, checkcoll_status from  mbucfresigncause where resigncause_code = '" + ls_resigncause + "'";
            Sdt dt_resign = WebUtil.QuerySdt(ls_sqlcause);
            if (dt_resign.Next())
            {
                li_checkshrlon = dt_resign.GetDecimal("checkshrlon_status");
                li_checkcoll = dt_resign.GetDecimal("checkcoll_status");
            }
            else
            {
                li_checkshrlon = 0;
                li_checkcoll = 0;
            }

            if (count_coll > 0 && li_checkcoll == 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("มีภาระค้ำประกัน");
                // HdCheckSave.Value = "false";
            }
            else
            {
                HdCheckSave.Value = "true";
            }
            if (li_checkshrlon == 1) //(check_resign == 1)
            {
                sum_share = dw_sum.GetItemDecimal(1, "share");
                sum_loan = dw_sum.GetItemDecimal(1, "loan");
                sum_dept = dw_sum.GetItemDecimal(1, "deposit");
                count_coll = dw_sum.GetItemDecimal(1, "coll_count");
                try
                {
                    sum_intloan = dw_loan.GetItemDecimal(1, "c_interest");
                }
                catch
                { }

                sum_pos = sum_share + sum_dept;
                sum_nav = sum_loan + sum_intloan;
                //if (count_coll > 0)
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage("มีภาระค้ำประกัน");
                //    HdCheckSave.Value = "true";
                //}
                //else
                //{
                if (sum_pos < sum_nav)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("มียอดหนี้ค้างชำระ > ยอดเงินฝาก+ยอดหุ้น");
                    // HdCheckSave.Value = "false";
                    HdCheckSave.Value = "True";
                }
                else
                {
                    HdCheckSave.Value = "True";
                }

                //}
            }
            else
            {
                HdCheckSave.Value = "True";
            }
        }

        public void SaveWebSheet()
        {
            JsResignCause();

            if (HdCheckSave.Value == "True")
            {
                try
                {
                    String xml_grt;
                    String xml_dept;// = dw_deposit.Describe("DataWindow.Data.XML"); 
                    String xml_loan;//= dw_loan.Describe("DataWindow.Data.XML");
                    String xml_request;// = dw_head.Describe("DataWindow.Data.XML");
                    String xml_share;// = dw_share.Describe("DataWindow.Data.XML");
                    String xml_sum;//= dw_sum.Describe("DataWindow.Data.XML");
                    String resigncause_code = dw_head.GetItemString(1, "resigncause_code");

                    if (resigncause_code == "ฮฮ")
                    { LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกสาเหตุลาออก"); }
                    else
                    {

                        xml_grt = dw_grt.RowCount > 0 ? dw_grt.Describe("DataWindow.Data.XML") : "";
                        xml_dept = dw_deposit.RowCount > 0 ? dw_deposit.Describe("DataWindow.Data.XML") : "";
                        xml_loan = dw_loan.RowCount > 0 ? dw_loan.Describe("DataWindow.Data.XML") : "";
                        xml_request = dw_head.RowCount > 0 ? dw_head.Describe("DataWindow.Data.XML") : "";
                        xml_share = dw_share.RowCount > 0 ? dw_share.Describe("DataWindow.Data.XML") : "";
                        xml_sum = dw_sum.RowCount > 0 ? dw_sum.Describe("DataWindow.Data.XML") : "";

                        String member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
                        //DateTime entry_date = dw_head.GetItemDate(1, "approve_date");
                        DateTime entry_date = state.SsWorkDate;
                        DateTime resignreq_date = dw_head.GetItemDate(1, "resignreq_date");
                        //Session["approve_date"] = entry_date;
                        Session["resignreq_date"] = resignreq_date;
                        String entry_id = state.SsUsername;
                        str_mbreqresign mbreqresign = new str_mbreqresign();
                        //int result = shrlonService.SaveRequestResign(state.SsWsPass, mbreqresign, member_no, entry_date, entry_id, xml_dept, xml_grt, xml_loan, xml_request, xml_share, xml_sum);

                        mbreqresign.coop_id = state.SsCoopId;
                        mbreqresign.entry_date = state.SsWorkDate;
                        mbreqresign.entry_id = state.SsUsername;
                        mbreqresign.member_no = member_no;
                        mbreqresign.memcoop_id = state.SsCoopId;
                        mbreqresign.req_date = resignreq_date;
                        mbreqresign.xml_dept = xml_dept;
                        mbreqresign.xml_grt = xml_grt;
                        mbreqresign.xml_loan = xml_loan;
                        mbreqresign.xml_request = xml_request;
                        mbreqresign.xml_share = xml_share;
                        mbreqresign.xml_sum = xml_sum;
                        int result = shrlonService.of_savereq_mbresign(state.SsWsPass,ref mbreqresign);
                        if (result == 1)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                            if (state.SsCoopControl == "008001")
                            {
                                string sql_value = GetSql_Value("select max(resignreq_docno) as sql_value from mbreqresign");
                                string textalert = "เลขใบคำขอ " + sql_value;
                                this.SetOnLoadedScript("alert('" + textalert + "')");
                            }
                        }
                        JsNewClear();
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
        }

        public void WebSheetLoadEnd()
        {
            //try
            //{
            //    DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", state.SsCoopControl);
            //}
            //catch { }
            tdw_head.Eng2ThaiAllRow();
            //DateTime entry_date = Convert.ToDateTime(Session["approve_date"].ToString());
            //DateTime resignreq_date = Convert.ToDateTime(Session["resignreq_date"].ToString());
            //dw_head.SetItemDate(1, "resignreq_date", resignreq_date);
            //dw_head.SetItemDate(1, "approve_date", entry_date);
            dw_head.SaveDataCache();
            dw_sum.SaveDataCache();
            dw_share.SaveDataCache();
            dw_loan.SaveDataCache();
            dw_grt.SaveDataCache();
            dw_deposit.SaveDataCache();

            //if (dw_head.RowCount > 1)
            //{
            //    dw_head.DeleteRow(dw_head.RowCount);
            //}
        }

        private void GetMemberDetail()
        {
            try
            {
                String member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
                DateTime entry_date = state.SsWorkDate;
                str_mbreqresign mbreqresign = new str_mbreqresign();
                mbreqresign.member_no = member_no;
                mbreqresign.entry_date = entry_date;
                mbreqresign.req_date = entry_date;
                //int result = shrlonService.of_initreq_mbresign(state.SsWsPass, ref mbreqresign);
                int result = shrlonService.of_initreq_mbresign(state.SsWsPass, ref mbreqresign);
                if (result == 1)
                {

                    try
                    {
                        dw_head.Reset();
                        //dw_head.ImportString(mbreqresign.xml_request, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_request, dw_head, tdw_head, FileSaveAsType.Xml);
                    }
                    catch
                    { //dw_head.InsertRow(0); 
                    }
                    try
                    {
                        dw_share.Reset();
                        //dw_share.ImportString(mbreqresign.xml_share, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_share, dw_share, tdw_head, FileSaveAsType.Xml);
                    }
                    catch { }

                    try
                    {
                        dw_deposit.Reset();
                        //dw_head.ImportString(mbreqresign.xml_request, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_dept, dw_deposit, tdw_head, FileSaveAsType.Xml);
                    }
                    catch
                    { //dw_head.InsertRow(0); 
                    }

                    try
                    {
                        dw_loan.Reset();
                        //dw_loan.ImportString(mbreqresign.xml_loan, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_loan, dw_loan, tdw_head, FileSaveAsType.Xml);
                    }
                    catch { }

                    try
                    {
                        dw_grt.Reset();
                        //dw_grt.ImportString(mbreqresign.xml_grt, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_grt, dw_grt, tdw_head, FileSaveAsType.Xml);
                    }
                    catch { }

                    try
                    {
                        dw_sum.Reset();
                        //dw_sum.ImportString(mbreqresign.xml_sum, FileSaveAsType.Xml);
                        DwUtil.ImportData(mbreqresign.xml_sum, dw_sum, tdw_head, FileSaveAsType.Xml);
                    }
                    catch { }

                    dw_head.SetItemDecimal(1, "check_resigncause", 1);
                    DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", state.SsCoopControl);
                }

                DateTime resignreq_date;
                dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);

                //DateTime approve_date;
                //try
                //{
                //    approve_date = Convert.ToDateTime(Session["approve_date"].ToString());
                //    resignreq_date = Convert.ToDateTime(Session["resignreq_date"].ToString());
                //    dw_head.SetItemDate(1, "resignreq_date", resignreq_date);
                //    dw_head.SetItemDate(1, "approve_date", approve_date);
                //}
                //catch
                //{
                //    dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
                //    dw_head.SetItemDate(1, "approve_date", state.SsWorkDate);
                //}
                string deptaccount_no = "";
                try
                {
                    string sqldapt = @"select deptaccount_no from wcdeptmaster where member_no ='" + member_no + @"'";
                    Sdt dtdapt = WebUtil.QuerySdt(sqldapt);
                    if (dtdapt.Next())
                    {
                        deptaccount_no = dtdapt.GetString("deptaccount_no");
                    }
                }
                catch { }

                if (deptaccount_no != "")
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("เป็นสมาชิกฌาปนกิจสงเคาระห์ของสหกรณ์");
                }

                //
                string chkreg = GetSql_Value(@"SELECT MBREQRESIGN.RESIGNREQ_DOCNO as sql_value
                                            FROM  MBMEMBMASTER LEFT JOIN MBUCFPRENAME ON mbmembmaster.prename_code = mbucfprename.prename_code,
                                                  MBREQRESIGN
                                               WHERE ( MBREQRESIGN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                                             ( MBREQRESIGN.COOP_ID = MBMEMBMASTER.COOP_ID )  and 
		                                     MBREQRESIGN.RESIGNREQ_STATUS = 8 and 
                                             MBREQRESIGN.MEMBER_NO = '" + member_no + "'");

                if (chkreg != "")
                {
                    this.SetOnLoadedScript("alert('สมาชิกเคยทำรายการไปแล้วรออนุมัติ')");
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกเคยทำรายการไปแล้วรออนุมัติ");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JsNewClear()
        {
            //DateTime approve_date = Convert.ToDateTime(Session["approve_date"].ToString());
            //DateTime resignreq_date = Convert.ToDateTime(Session["resignreq_date"].ToString());
            dw_head.Reset();
            dw_sum.Reset();
            dw_share.Reset();
            dw_loan.Reset();
            dw_grt.Reset();
            dw_deposit.Reset();

            HdIsPostBack.Value = "false";
            dw_head.InsertRow(0);
            dw_sum.InsertRow(0);
            dw_share.InsertRow(0);
            dw_loan.InsertRow(0);
            dw_grt.InsertRow(0);
            dw_deposit.InsertRow(0);

            dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
            //dw_head.SetItemDate(1, "approve_date", approve_date);
            dw_head.SetItemDecimal(1, "check_resigncause", 1);

            DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", state.SsCoopControl);
            tdw_head.Eng2ThaiAllRow();
        }

        private void DateChange()
        {
            try
            {
                DateTime dt = new DateTime();
                //dt = dw_head.GetItemDateTime(1, "approve_date");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void JsPostSalaryId()
        {
            String salary_id = dw_head.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                //เซตค่าของเลขสมาชิกที่ได้มาจากเลขพนักงานให้กับตัวแปร Hfmember_no
                Hfmember_no.Value = dtMemb.GetString("member_no");
                dw_head.SetItemString(1, "member_no", Hfmember_no.Value);
                GetMemberDetail();
            }
            else
            {
                this.JsNewClear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + Hfmember_no.Value);
            }
        }

        public void PostResigndtfalg()
        {
            decimal resigndtfix_flag = 0;
            try
            {
                resigndtfix_flag = dw_head.GetItemDecimal(1, "resigndtfix_flag");
            }
            catch { }

            if (resigndtfix_flag == 1)
            {
                dw_head.SetItemDate(1, "resigndtfix_date", state.SsWorkDate);
                dw_head.SetItemDecimal(1, "resigndtfix_flag", 1);
            }
            else
            {
                dw_head.SetItemNull(1, "resigndtfix_date");
            }
        }

        private void PostApv()
        {
            decimal apv_flag = 0;
            try
            {
                apv_flag = dw_head.GetItemDecimal(1, "apvflag");
            }
            catch
            { }
            if (apv_flag == 1)
            {
                dw_head.SetItemDate(1, "apv_date", state.SsWorkDate);
            }
            else
            {
                dw_head.SetItemNull(1, "apv_date");
            }
        }

        // string sql_value = GetSql_Value(select max(appl_docno) as sql_value from mbreqappl)
        public string GetSql_Value(string Select_Condition)
        {
            string max_value = "";
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetString("sql_value");
            }
            return max_value;
        }
    }
}

