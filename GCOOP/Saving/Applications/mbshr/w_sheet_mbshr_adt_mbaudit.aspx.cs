using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Data;
using DataLibrary;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_adt_mbaudit : PageWebSheet, WebSheet
    {
        private DwThDate tdw_main;

        String xmlstatus;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String newClear;
        protected String jsRefresh;
        protected String jsGetTambol;
        protected String jsChanegValue;
        protected String changeDistrict;
        protected String jsAddRow;
        protected String jsSetData;
        protected String jsCallRetry;
        protected String jsIdCard;
        protected String jsGetShareBase;
        protected String setpausekeep_date;
        protected String jsGetCurrDistrict;
        protected String jsGetCurrPostcode;
        protected String jsGetCurrTambol;
        protected String jsLinkAddress;
        protected String jsExpense;
        protected String jsBank;
        protected String jsMoneytypeCode;

        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsGetTambol = WebUtil.JsPostBack(this, "jsGetTambol");
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsSetData = WebUtil.JsPostBack(this, "jsSetData");
            jsCallRetry = WebUtil.JsPostBack(this, "jsCallRetry");
            jsIdCard = WebUtil.JsPostBack(this, "jsIdCard");
            jsGetShareBase = WebUtil.JsPostBack(this, "jsGetShareBase");
            setpausekeep_date = WebUtil.JsPostBack(this, "setpausekeep_date");
            jsGetCurrDistrict = WebUtil.JsPostBack(this, "jsGetCurrDistrict");
            jsGetCurrPostcode = WebUtil.JsPostBack(this, "jsGetCurrPostcode");
            jsGetCurrTambol = WebUtil.JsPostBack(this, "jsGetCurrTambol");
            jsLinkAddress = WebUtil.JsPostBack(this, "jsLinkAddress");
            jsExpense = WebUtil.JsPostBack(this, "jsExpense");
            jsBank = WebUtil.JsPostBack(this, "jsBank");
            jsAddRow = WebUtil.JsPostBack(this, "jsAddRow");
            jsMoneytypeCode = WebUtil.JsPostBack(this, "jsMoneytypeCode");

            tdw_main = new DwThDate(dw_main, this);
            tdw_main.Add("birth_date", "birth_tdate");
            tdw_main.Add("retry_date", "retry_tdate");
            tdw_main.Add("member_date", "member_tdate");
            tdw_main.Add("work_date", "work_tdate");
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

            if (IsPostBack)
            {
                try
                {
                    this.RestoreContextDw(dw_main);
                    this.RestoreContextDw(dw_moneytr);
                    HdIsPostBack.Value = "true";
                }
                catch { }
            }
            else
            {
                dw_main.InsertRow(0);
                //dw_moneytr.InsertRow(0);
                
                HdIsPostBack.Value = "false";
            }
        }

        public void RetrieveDDDW()
        {
            DwUtil.RetrieveDDDW(dw_main, "membtype_code", "mb_member_audit.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "prename_code", "mb_member_audit.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "amphur_code", "mb_member_audit.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "province_code", "mb_member_audit.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "tambol_code", "mb_member_audit.pbl", null);

            try
            {
                DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "curramphur_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "currprovince_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "currtambol_code", "mb_member_audit.pbl", null);

            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_moneytr, "trtype_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_moneytr, "moneytype_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
            try
            {
                DwUtil.RetrieveDDDW(dw_moneytr, "bank_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex)
            { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "changeDistrict")
            {
                ChangeDistrict();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsGetTambol")
            {
                JsGetTambol();
            }
            else if (eventArg == "jsSetData")
            {
                int row = Convert.ToInt32(Hrow.Value);
            }
            else if (eventArg == "jsCallRetry")
            {
                JsCallRetry();
            }
            else if (eventArg == "jsIdCard")
            {
                JsIdCard();
            }
            else if (eventArg == "jsGetShareBase")
            {
                JsGetShareBase();
            }
            else if (eventArg == "setpausekeep_date")
            {
                //String pausekeep_flag = dw_status.GetItemString(1, "pausekeep_flag");
            }
            else if (eventArg == "jsGetCurrDistrict")
            {
                JsGetCurrDistrict();
            }
            else if (eventArg == "jsGetCurrTambol")
            {
                JsGetCurrTambol();
            }
            else if (eventArg == "jsChanegValue")
            {
                JsChanegValue();
            }
            else if (eventArg == "jsLinkAddress")
            {
                JsLinkAddress();
            }
            else if (eventArg == "jsExpense")
            {
                JsExpense();
            }
            else if (eventArg == "jsBank")
            {
                JsBank();
            }
            else if (eventArg == "jsAddRow")
            {
                int row = dw_moneytr.RowCount;
                dw_moneytr.InsertRow(row + 1);
                dw_moneytr.SetItemString(dw_moneytr.RowCount, "member_no", Hfmember_no.Value);
            }
            else if (eventArg == "jsMoneytypeCode")
            {
                dw_moneytr.SetItemNull(1, "bank_code");
                dw_moneytr.SetItemNull(1, "bank_branch");
                dw_moneytr.SetItemNull(1, "branch_name");
                dw_moneytr.SetItemNull(1, "bank_accid");
            }
        }

        private void JsGetShareBase()
        {
            Decimal adc_salary = dw_main.GetItemDecimal(1, "salary_amount");

            Decimal salary_amount, incomeetc_amt ,total;
            try { salary_amount = dw_main.GetItemDecimal(1, "salary_amount"); }
            catch { salary_amount = 0; }
            try { incomeetc_amt = dw_main.GetItemDecimal(1, "incomeetc_amt"); }
            catch { incomeetc_amt = 0; }
            total = salary_amount + incomeetc_amt;
            
            String member_no = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            decimal member_type = WebUtil.GetMemberType(state.SsCoopId, member_no);
            String sharetype_code = dw_main.GetItemString(1, "sharetype_code");
            Decimal periodshare_value = Convert.ToDecimal(Hperiodshare_value.Value);
            // dw_main.GetItemDecimal(1, "periodshare_value");
            //Decimal[] minmaxshare = wcf.InterPreter.CalPayShareMonth(state.SsConnectionIndex, state.SsCoopControl, sharetype_code, member_type, salary_amount);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);

            Decimal minmaxshare = CalPayShareMonth(state.SsCoopControl, sharetype_code, member_type, total);

            // dw_main.SetItemDecimal(1, "periodshare_value", minmaxshare[0]);
            dw_main.SetItemDecimal(1, "periodbase_value", minmaxshare);

            if (minmaxshare >= periodshare_value)
            {
                dw_main.SetItemDecimal(1, "periodshare_value", minmaxshare);
            }
            else
            {
                dw_main.SetItemDecimal(1, "periodshare_value", periodshare_value);
                this.SetOnLoadedScript("alert('เงินเดือนที่เปลี่ยนแปลง ทำให้ค่าหุ้นที่ต้องส่งต่อเดือนลดลง \nถ้าต้องการปรับเงินค่าหุ้นที่ส่งประจำเดือน กรุณาไปปรับปรุงที่การส่งหุ้นประจำเดือน')");
            }
        }

        private void JsChanegValue()
        {
            // LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
            dw_main.SetItemDecimal(1, "periodshare_value", 0);
            //Decimal periodbase_value = dw_main.GetItemDecimal(1, "periodbase_value");
            //Decimal periodshare_value = dw_main.GetItemDecimal(1, "periodshare_value");
            HdIsPostBack.Value = "false";
        }

        private void JsIdCard()
        {
            String PID = dw_main.GetItemString(1, "card_person");
            if (PID.Length != 13)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ครบ 13 หลัก");
            }
            else
            {
                Int32 pidchk = 0;
                Int32 dig = 0;
                Int32 fdig = 0;
                String lasttext = "";
                try
                {
                    pidchk = (Convert.ToInt32(PID.Substring(0, 1)) * 13) + (Convert.ToInt32(PID.Substring(1, 1)) * 12) + (Convert.ToInt32(PID.Substring(2, 1)) * 11) + (Convert.ToInt32(PID.Substring(3, 1)) * 10) + (Convert.ToInt32(PID.Substring(4, 1)) * 9) + (Convert.ToInt32(PID.Substring(5, 1)) * 8) + (Convert.ToInt32(PID.Substring(6, 1)) * 7) + (Convert.ToInt32(PID.Substring(7, 1)) * 6) + (Convert.ToInt32(PID.Substring(8, 1)) * 5) + (Convert.ToInt32(PID.Substring(9, 1)) * 4) + (Convert.ToInt32(PID.Substring(10, 1)) * 3) + (Convert.ToInt32(PID.Substring(11, 1)) * 2);

                    dig = pidchk % 11;
                    fdig = 11 - dig;
                    lasttext = fdig.ToString();
                    if (PID.Substring(12, 1) == WebUtil.Right(lasttext, 1))
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("เลขบัตรประชาชนถูกต้อง");
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง"); }
                }
                catch { }
            }
        }

        public void SaveWebSheet()
        {
            dw_main.SetItemString(1, "UPDATE_BYENTRYID", state.SsUsername);
            dw_main.SetItemString(1, "UPDATE_BYENTRYIP", state.SsClientIp);

            try
            {
                str_mbaudit lstr_mbinfo = new str_mbaudit();

                //lstr_mbinfo.coop_id = state.SsCoopId;
                //lstr_mbinfo.member_no = Hfmember_no.Value;
                lstr_mbinfo.xmlmaster = dw_main.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");

                int result = shrlonService.of_save_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_moneytr.SaveDataCache();

            try
            {
                Hsalary_amount.Value = Convert.ToString(dw_main.GetItemDecimal(1, "salary_amount"));
                Hincomeetc_amt.Value = Convert.ToString(dw_main.GetItemDecimal(1, "incomeetc_amt"));
            }
            catch { }
        }

        private void JsNewClear()
        {
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_moneytr.Reset();
            dw_moneytr.InsertRow(0);
        }

        private void JsPostMember()
        {
            RetrieveDDDW();
            try
            {
                str_mbaudit lstr_mbinfo = new str_mbaudit();

                lstr_mbinfo.coop_id = state.SsCoopControl;
                string member_no = Hfmember_no.Value;
                lstr_mbinfo.member_no = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                lstr_mbinfo.xmlmaster = dw_main.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");

                int result = shrlonService.of_init_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {
                    try
                    {
                        try
                        {
                            //dw_main.Reset();
                            //DwUtil.ImportData(lstr_mbinfo.xmlbfmaster, dw_main, null, FileSaveAsType.Xml);
                            //tdw_main.Eng2ThaiAllRow();
                            //TextDwmain.Text = dw_main.Describe("DataWindow.Data.XML");

                            dw_main.Reset();
                            DwUtil.ImportData(lstr_mbinfo.xmlmaster, dw_main, null, FileSaveAsType.Xml);
                            tdw_main.Eng2ThaiAllRow();
                        }
                        catch (Exception ex)
                        {
                            dw_main.Reset();
                            dw_main.InsertRow(0);
                            LtServerMessage.Text = WebUtil.ErrorMessage("dw_main----" + ex);
                        }
                        try
                        {
                            dw_moneytr.Reset();
                            DwUtil.ImportData(lstr_mbinfo.xmlmoneytr, dw_moneytr, null, FileSaveAsType.Xml);
                        }
                        catch (Exception ex)
                        {
                            dw_moneytr.Reset();
                            dw_moneytr.InsertRow(0);
                            LtServerMessage.Text = WebUtil.ErrorMessage("dw_detail----" + ex);
                        }
                        Hperiodshare_value.Value = Convert.ToString(dw_main.GetItemDecimal(1, "periodshare_value"));
                        //ChangeDistrict();
                        //JsGetTambol();
                        //JsGetCurrDistrict();
                        //JsGetCurrTambol();
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        dw_main.Reset(); dw_main.InsertRow(0);
                        dw_moneytr.Reset(); dw_moneytr.InsertRow(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void ChangeDistrict()
        {
            try
            {
                DataWindowChild child = dw_main.GetChild("amphur_code");
                child.SetTransaction(sqlca);
                child.Retrieve();
                String province_code = dw_main.GetItemString(1, "province_code");
                child.SetFilter("province_code='" + province_code + "'");
                child.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsGetTambol()
        {
            try
            {
                DataWindowChild childdis = dw_main.GetChild("tambol_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String amphur_code = dw_main.GetItemString(1, "amphur_code");
                childdis.SetFilter("DISTRICT_CODE='" + amphur_code + "'");
                childdis.Filter();

                String provincecode = dw_main.GetItemString(1, "province_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                              WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                               and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + amphur_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_main.SetItemString(1, "addr_postcode", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //        private void JsGetPostcode()
        //        {
        //            try
        //            {
        //                String provincecode = dw_main.GetItemString(1, "province_code");
        //                String district_code = dw_main.GetItemString(1, "district_code");
        //                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
        //                WERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
        //                and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + "') ";
        //                DataTable dt = WebUtil.Query(sql);
        //                if (dt.Rows.Count > 0)
        //                {
        //                    dw_main.SetItemString(1, "postcode", dt.Rows[0]["postcode"].ToString());
        //                }
        //            }
        //            catch (Exception ex) { ex.ToString(); }
        //        }

        private void JsGetCurrTambol()
        {
            try
            {
                DataWindowChild childdis = dw_main.GetChild("currtambol_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String curramphur_code = dw_main.GetItemString(1, "curramphur_code");
                childdis.SetFilter("DISTRICT_CODE='" + curramphur_code + "'");
                childdis.Filter();

                String currprovince_code = dw_main.GetItemString(1, "currprovince_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                              WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                               and ( MBUCFDISTRICT.PROVINCE_CODE ='" + currprovince_code + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + curramphur_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_main.SetItemString(1, "curraddr_postcode", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch
            {
            }
        }

        private void JsGetCurrDistrict()
        {
            try
            {

                DataWindowChild childdis = dw_main.GetChild("curramphur_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String currprovincecode = dw_main.GetItemString(1, "currprovince_code");
                childdis.SetFilter("province_code='" + currprovincecode + "'");
                childdis.Filter();
            }
            catch
            {
            }
        }
        //        private void JsGetCurrPostcode()
        //        {
        //            try
        //            {
        //                DataWindowChild child = dw_main.GetChild("currtambol_code");
        //                child.SetTransaction(sqlca);
        //                child.Retrieve();
        //                String curramphur_code = dw_main.GetItemString(1, "curramphur_code");
        //                child.SetFilter("DISTRICT_CODE='" + curramphur_code + "'");
        //                child.Filter();

        //                String currprovince_code = dw_main.GetItemString(1, "currprovince_code");
        //                // String district_code = dw_main.GetItemString(1, "district_code");
        //                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
        //                                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
        //                                and ( MBUCFDISTRICT.PROVINCE_CODE ='" + currprovince_code + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + curramphur_code + "') ";
        //                DataTable dt = WebUtil.Query(sql);
        //                if (dt.Rows.Count > 0)
        //                {
        //                    dw_main.SetItemString(1, "curraddr_postcode", dt.Rows[0]["postcode"].ToString());
        //                }
        //            }
        //            catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
        //        }
        private void JsCallRetry()
        {
            try
            {
                String birth_tdate = dw_main.GetItemString(1, "birth_tdate");
                String birth_date = WebUtil.ConvertDateThaiToEng(dw_main, "birth_tdate", null);
                //551108
                //mai แก้ไข error วันที่ 
                //  DateTime dt = Convert.ToDateTime(birth_date, System.Globalization.CultureInfo.CurrentCulture);
                DateTime dt = dw_main.GetItemDate(1, "birth_date");
                DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);
                Decimal year = Convert.ToDecimal(retry.Year) + 543;
                String retry_date = retry.Day.ToString("00") + retry.Month.ToString("00") + year.ToString("0000");
                dw_main.SetItemString(1, "retry_tdate", retry_date);
                dw_main.SetItemDateTime(1, "retry_date", retry);

                // dw_detail.SetItemString(1, "birth_tdate", birth_tdate);
                dw_main.SetItemDateTime(1, "birth_date", dt);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsLinkAddress()
        {
            string memb_addr = "";
            string addr_moo = "";
            string addr_village = "";
            string soi = "";
            string road = "";
            string tambol_code = "";
            string district_code = "";
            string province_code = "";
            string postcode = "";
            string mem_tel = "";

            try
            {
                DwUtil.RetrieveDDDW(dw_main, "curramphur_code", "mb_member_audit.pbl", null);
            }
            catch (Exception ex) { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "currtambol_code", "mb_member_audit.pbl", null);

            }
            catch (Exception ex) { }

            try
            {
                memb_addr = dw_main.GetItemString(1, "addr_no");
                dw_main.SetItemString(1, "curraddr_no", memb_addr);
            }
            catch { }
            try
            {
                addr_moo = dw_main.GetItemString(1, "addr_moo");
                dw_main.SetItemString(1, "curraddr_moo", addr_moo);
            }
            catch { }
            try
            {
                addr_village = dw_main.GetItemString(1, "addr_village");
                dw_main.SetItemString(1, "curraddr_village", addr_village);
            }
            catch { }
            try
            {
                soi = dw_main.GetItemString(1, "addr_soi");
                dw_main.SetItemString(1, "curraddr_soi", soi);
            }
            catch { }
            try
            {
                road = dw_main.GetItemString(1, "addr_road");
                dw_main.SetItemString(1, "curraddr_road", road);
            }
            catch { }
            try
            {
                tambol_code = dw_main.GetItemString(1, "tambol_code");
                dw_main.SetItemString(1, "currtambol_code", tambol_code.Trim());
            }
            catch { }
            try
            {
                district_code = dw_main.GetItemString(1, "amphur_code");
                dw_main.SetItemString(1, "curramphur_code", district_code.Trim());
            }
            catch { }
            try
            {
                province_code = dw_main.GetItemString(1, "province_code");
                dw_main.SetItemString(1, "currprovince_code", province_code.Trim());
            }
            catch { }
            try
            {
                postcode = dw_main.GetItemString(1, "addr_postcode");
                dw_main.SetItemString(1, "curraddr_postcode", postcode);
            }
            catch { }
            try
            {
                mem_tel = dw_main.GetItemString(1, "addr_phone");
                dw_main.SetItemString(1, "curraddr_phone", mem_tel);
            }
            catch { }
        }

        public void JsExpense()
        {
            string expense_code = dw_main.GetItemString(1, "expense_code");
            if (expense_code == "CBT")
            {
                DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "mb_member_audit.pbl", null);
            }
            else if (expense_code == "CSH")
            {
                dw_main.SetItemNull(1, "expense_bank");
                dw_main.SetItemNull(1, "expense_branch");
                dw_main.SetItemNull(1, "expense_accid");
            }
            else if (expense_code == "CBT")
            {
                dw_main.SetItemNull(1, "expense_bank");
                dw_main.SetItemNull(1, "expense_branch");
            }
        }

        public void JsBank()
        {
            try
            {
                dw_main.SetItemNull(1, "expense_branch");
                DataWindowChild childbranch = dw_main.GetChild("expense_branch_1");
                childbranch.SetTransaction(sqlca);
                childbranch.Retrieve();
                String expense_bank = dw_main.GetItemString(1, "expense_bank");
                childbranch.SetFilter("bank_code = '" + expense_bank + "'");
                childbranch.Filter();
            }
            catch { }
        }

        public void JsPostBankBranch()
        {
            String bank_code = dw_moneytr.GetItemString(1, "bank_code");
            String bank_branch = dw_moneytr.GetItemString(1, "bank_branch");

            Sta ta = new Sta(state.SsConnectionString);
            String sql = "";

            sql = @"SELECT branch_name
                    FROM cmucfbankbranch  
                    WHERE ( bank_code = '" + bank_code + @"' ) AND ( branch_id = '" + bank_branch + @"' )";
            Sdt dt = ta.Query(sql);
            dw_moneytr.SetItemString(1, "branch_name", dt.Rows[0]["branch_name"].ToString());
            dw_moneytr.SetItemString(1, "bank_branch", bank_branch);

            ta.Close();
        }

        //คำนวณหุ้นฐาน TKS
        private Decimal CalPayShareMonth(String memcoop_id, String sharetype_code, Decimal member_type, Decimal salary_amount)
        {
            Decimal share = 0;

            //if (member_type != 2)
            //{
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
            //}
            //else
            //{
            //    share = 0;
            //}
            return share;
        }
    }
}
