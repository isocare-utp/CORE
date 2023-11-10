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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;
using Sybase.DataWindow;

using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mb_member_loan_audit : PageWebSheet, WebSheet
    {
        private DwThDate tdw_main;

        String xmlstatus;
        //private ShrlonClient shrlonService;
        //private CommonClient commonService;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostCollmast;
        protected String newClear;
        protected String jsRefresh;
        protected String jsGetTambol;
        protected String jsChanegValue;
        protected String changeDistrict;
        protected String jsInsertRow;
        protected String jsInsertRowRemarkstat;
        protected String jsSetData;
        protected String jsCallRetry;
        protected String getpicture;
        protected String jsIdCard;
        protected String jsGetShareBase;
        protected String jsgetpicMember_no;
        protected String setpausekeep_date;
        protected String jsGetCurrDistrict;
        protected String jsGetCurrPostcode;
        protected String jsGetCurrTambol;
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsGetTambol = WebUtil.JsPostBack(this, "jsGetTambol");
            jsInsertRow = WebUtil.JsPostBack(this, "jsInsertRow");
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsInsertRowRemarkstat = WebUtil.JsPostBack(this, "jsInsertRowRemarkstat");
            jsSetData = WebUtil.JsPostBack(this, "jsSetData");
            jsCallRetry = WebUtil.JsPostBack(this, "jsCallRetry");
            getpicture = WebUtil.JsPostBack(this, "getpicture");
            jsIdCard = WebUtil.JsPostBack(this, "jsIdCard");
            jsGetShareBase = WebUtil.JsPostBack(this, "jsGetShareBase");
            jsgetpicMember_no = WebUtil.JsPostBack(this, "jsgetpicMember_no");
            setpausekeep_date = WebUtil.JsPostBack(this, "setpausekeep_date");
            jsGetCurrDistrict = WebUtil.JsPostBack(this, "jsGetCurrDistrict");
            jsGetCurrPostcode = WebUtil.JsPostBack(this, "jsGetCurrPostcode");
            jsGetCurrTambol = WebUtil.JsPostBack(this, "jsGetCurrTambol");
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
                RetrieveDDDW();
               
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
                // DwUtil.RetrieveDDDW(dw_status, "appltype_code", "mb_member_audit.pbl", null);
                //DwUtil.RetrieveDDDW(dw_moneytr, "trtype_code", "mb_member_audit.pbl", null);
                //DwUtil.RetrieveDDDW(dw_moneytr, "moneytype_code", "mb_member_audit.pbl", null);
                //DwUtil.RetrieveDDDW(dw_moneytr, "bank_code", "mb_member_audit.pbl", null);
                //  DwUtil.RetrieveDDDW(dw_status, "remarkstattype_code", "mb_member_audit.pbl", null);

            
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
            else if (eventArg == "jsInsertRow")
            {

                int row = dw_moneytr.RowCount;
                dw_moneytr.InsertRow(row + 1);
                dw_moneytr.SetItemString(dw_moneytr.RowCount, "member_no", Hfmember_no.Value);
            }
            else if (eventArg == "jsSetData")
            {
                int row = Convert.ToInt32(Hrow.Value);
                //dw_remarkstat.SetItemDate(row, "remarkstat_date", state.SsWorkDate);
                //dw_remarkstat.SetItemString(row, "remarkstat_entryid", state.SsUsername);
            }
            else if (eventArg == "jsCallRetry")
            {
                JsCallRetry();
            }
            else if (eventArg == "getpicture")
            {
                GetPictureMember();
            }
            else if (eventArg == "jsIdCard")
            {
                JsIdCard();
            }
            else if (eventArg == "jsGetShareBase")
            {

                JsGetShareBase();
            }
            else if (eventArg == "jsgetpicMember_no")
            {
                //string member_no = Hfmember_no.Value;
                //String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
                //Image1.ImageUrl = imageUrl;
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
        }


        private void JsGetShareBase()
        {
            Decimal adc_salary = dw_main.GetItemDecimal(1, "salary_amount");

            Decimal salary_amount = dw_main.GetItemDecimal(1, "salary_amount");
            String member_no = dw_main.GetItemString(1, "member_no");
            decimal member_type = WebUtil.GetMemberType(state.SsCoopId, member_no);
            String sharetype_code = dw_main.GetItemString(1, "sharetype_code");
            string minmaxshare = CalPayShareMonth(sharetype_code, state.SsCoopControl, salary_amount, member_type);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);

            // dw_main.SetItemDecimal(1, "periodshare_value", minmaxshare[0]);
            dw_main.SetItemDecimal(1, "periodbase_value", minmaxshare[0]);
            dw_main.SetItemDecimal(1, "maxshare_value", minmaxshare[1]);
            HdIsPostBack.Value = "false";

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

            try
            {

                str_mbaudit lstr_mbinfo = new str_mbaudit();

                lstr_mbinfo.coop_id = state.SsCoopId;
                lstr_mbinfo.member_no = Hfmember_no.Value;
                lstr_mbinfo.xmlmaster = dw_main.Describe("DataWindow.Data.XML");

                int row_moneytr = dw_moneytr.RowCount;
                int row_remarkstat = 1;

                if (row_remarkstat > 0)
                {
                    //lstr_mbinfo.xmlremarkstat = dw_status.Describe("DataWindow.Data.XML");
                    //lstr_mbinfo.xmlbfremarkstat = Textdwdata1.Text;
                }
                else
                {
                    lstr_mbinfo.xmlremarkstat = "";
                    lstr_mbinfo.xmlbfremarkstat = "";
                }

                if (row_moneytr > 0)
                {
                    lstr_mbinfo.xmlmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");
                    lstr_mbinfo.xmlbfmoneytr = Textdwdata2.Text;
                }
                else
                {
                    lstr_mbinfo.xmlmoneytr = "";
                    lstr_mbinfo.xmlbfmoneytr = "";
                }

                lstr_mbinfo.xmlbfmaster = TextDwmain.Text;
                lstr_mbinfo.xmlbfremarkstat = Textdwdata1.Text;
                lstr_mbinfo.xmlbfmoneytr = Textdwdata2.Text;

                lstr_mbinfo.userid = state.SsUsername;
                lstr_mbinfo.operate_date = state.SsWorkDate;


                int result = shrlonService.of_save_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย");
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {

            dw_main.SaveDataCache();
            dw_moneytr.SaveDataCache();
            //dw_status.SaveDataCache();


        }

        private void JsNewClear()
        {

            dw_main.Reset();
            //dw_status.Reset();
            dw_moneytr.Reset();
            dw_main.InsertRow(0);
            //dw_status.InsertRow(0);

        }

        private void JsPostMember()
        {
            try
            {
                str_mbaudit lstr_mbinfo = new str_mbaudit();

                lstr_mbinfo.coop_id = state.SsCoopId;
                //lstr_mbinfo.member_no = Hfmember_no.Value;
                lstr_mbinfo.member_no = dw_main.GetItemString(1, "member_no");
                lstr_mbinfo.xmlmaster = dw_main.Describe("DataWindow.Data.XML");
                //lstr_mbinfo.xmlbfremarkstat = dw_status.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");

                int result = shrlonService.of_init_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {



                    try
                    {

                        if (lstr_mbinfo.xmlmoneytr == "")
                        {

                        }
                        else
                        {

                            Textdwdata2.Text = lstr_mbinfo.xmlbfmoneytr;
                            dw_moneytr.Reset();
                            dw_moneytr.ImportString(lstr_mbinfo.xmlmoneytr, FileSaveAsType.Xml);
                        }

                        try
                        {
                            dw_main.Reset();
                            dw_main.ImportString(lstr_mbinfo.xmlbfmaster, FileSaveAsType.Xml);
                            tdw_main.Eng2ThaiAllRow();
                            TextDwmain.Text = dw_main.Describe("DataWindow.Data.XML");

                            dw_main.Reset();
                            dw_main.ImportString(lstr_mbinfo.xmlmaster, FileSaveAsType.Xml);
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

                            Textdwdata1.Text = lstr_mbinfo.xmlbfremarkstat;
                            //dw_status.Reset();
                            //dw_status.ImportString(lstr_mbinfo.xmlremarkstat, FileSaveAsType.Xml);
                        }
                        catch (Exception ex)
                        {
                            //dw_status.Reset();
                            //dw_status.InsertRow(0);
                            //LtServerMessage.Text = WebUtil.ErrorMessage("dw_status----" + ex);
                        }

                        ChangeDistrict();
                        JsGetTambol();
                        JsGetCurrDistrict();
                        JsGetCurrTambol();

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        dw_main.Reset(); dw_main.InsertRow(0);
                        dw_moneytr.Reset(); dw_moneytr.InsertRow(0);
                        //dw_status.Reset(); dw_status.InsertRow(0);

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

                //   LtServerMessage.Text = WebUtil.ErrorMessage(ex);

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
                //  LtServerMessage.Text = ex.ToString();
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
                childdis.SetFilter("province_code='"+currprovincecode+"'");
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
                //DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);
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

        private void GetPictureMember()
        {


        }
    }
}
