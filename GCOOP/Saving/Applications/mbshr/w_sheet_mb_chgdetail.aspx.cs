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
    public partial class w_sheet_mb_chgdetail : PageWebSheet, WebSheet
    {
        private DwThDate tdwData1;
        private DwThDate tdwData2;
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
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsGetTambol = WebUtil.JsPostBack(this, "jsGetTambol");
            jsInsertRow = WebUtil.JsPostBack(this, "jsInsertRow");
            jsInsertRowRemarkstat = WebUtil.JsPostBack(this, "jsInsertRowRemarkstat");
            jsSetData = WebUtil.JsPostBack(this, "jsSetData");
            jsCallRetry = WebUtil.JsPostBack(this, "jsCallRetry");
            getpicture = WebUtil.JsPostBack(this, "getpicture");
            jsIdCard = WebUtil.JsPostBack(this, "jsIdCard");
            jsGetShareBase = WebUtil.JsPostBack(this, "jsGetShareBase");
            jsgetpicMember_no = WebUtil.JsPostBack(this, "jsgetpicMember_no");
            setpausekeep_date = WebUtil.JsPostBack(this, "setpausekeep_date");
            tdwData1 = new DwThDate(dw_detail, this);
            tdwData1.Add("birth_date", "birth_tdate");
            tdwData1.Add("retry_date", "retry_tdate");
            tdwData1.Add("member_date", "member_tdate");
            tdwData1.Add("work_date", "work_tdate");
            tdwData2 = new DwThDate(dw_status, this);
            tdwData2.Add("pausekeep_date", "pausekeep_tdate");

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
                    this.RestoreContextDw(dw_detail, tdwData1);
                    this.RestoreContextDw(dw_status);
                    this.RestoreContextDw(dw_moneytr);
                    this.RestoreContextDw(dw_remarkstat);
                    HdIsPostBack.Value = "true";
                }
                catch { }

            }
            else
            {
                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);
                dw_status.InsertRow(0);              
                // dw_remarkstat.InsertRow(0);
                RetrieveDDDW();
                HdIsPostBack.Value = "false";
            }


        }

        public void RetrieveDDDW()
        {
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "membtype_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "subgroup_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "prename_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_detail, "district_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_detail, "province_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_detail, "tambol_code", "mb_chgdetail.pbl", null); 
                
                DwUtil.RetrieveDDDW(dw_status, "appltype_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_moneytr, "trtype_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_moneytr, "moneytype_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_moneytr, "bank_code", "mb_chgdetail.pbl", null);
                DwUtil.RetrieveDDDW(dw_remarkstat, "remarkstattype_code", "mb_chgdetail.pbl", null);
               ChangeDistrict();
                // JsGetTambol();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
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
                dw_remarkstat.SetItemDate(row, "remarkstat_date", state.SsWorkDate);
                dw_remarkstat.SetItemString(row, "remarkstat_entryid", state.SsUsername);
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
                string member_no = Hfmember_no.Value;
                String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
                Image1.ImageUrl = imageUrl;
            }
            else if (eventArg == "setpausekeep_date")
            {
                String pausekeep_flag = dw_status.GetItemString(1, "pausekeep_flag");
                
            }


        }

        private void JsGetShareBase()
        {
            Decimal adc_salary = dw_detail.GetItemDecimal(1, "salary_amount");
            Decimal adc_sharebase = 0;

            //int result = shrlonService.GetShareBase(state.SsWsPass,state.SsCoopId, adc_salary, ref adc_sharebase);
            int result = shrlonService.of_getsharebase(state.SsWsPass, state.SsCoopId, adc_salary, ref adc_sharebase);

            dw_detail.SetItemDecimal(1, "periodbase_value", adc_sharebase);


        }

        private void JsIdCard()
        {

            String PID = dw_detail.GetItemString(1, "card_person");
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
                str_adjust_mbinfo lstr_mbinfo = new str_adjust_mbinfo();
                lstr_mbinfo.coop_id = state.SsCoopId;
                lstr_mbinfo.member_no= Hfmember_no.Value;
                lstr_mbinfo.xmlmaster = dw_main.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmldetail = dw_detail.Describe("DataWindow.Data.XML");
                //lstr_mbinfo.xmldetail = TextDetail.Text;
                lstr_mbinfo.xmlstatus = dw_status.Describe("DataWindow.Data.XML");
                int row_moneytr = dw_moneytr.RowCount;
                int row_remarkstat = dw_remarkstat.RowCount;

                if (row_moneytr > 0)
                {
                    lstr_mbinfo.xmlmoneytr= dw_moneytr.Describe("DataWindow.Data.XML");
                    lstr_mbinfo.xmlbfmoneytr = Textdwdata3.Text;
                }
                else
                {
                    lstr_mbinfo.xmlmoneytr = "";
                    lstr_mbinfo.xmlbfmoneytr = "";
                }
                if (row_remarkstat > 0)
                {
                    lstr_mbinfo.xmlremarkstat= dw_remarkstat.Describe("DataWindow.Data.XML");
                    lstr_mbinfo.xmlbfremarkstat = Textdwdata4.Text;
                }
                else
                {
                    lstr_mbinfo.xmlremarkstat = "";
                    lstr_mbinfo.xmlbfremarkstat = "";
                }
                lstr_mbinfo.xmlbfmaster= TextDwmain.Text;
                lstr_mbinfo.xmlbfdetail= Textdwdata1.Text;
                lstr_mbinfo.xmlbfstatus = Textdwdata2.Text;
                lstr_mbinfo.appname= state.SsApplication;
                lstr_mbinfo.userid= state.SsUsername;
                lstr_mbinfo.operate_date = state.SsWorkDate;


                //int result = shrlonService.SaveRequestChangeMb(state.SsWsPass, ref lstr_mbinfo);
                int result = shrlonService.of_saverequest(state.SsWsPass, ref lstr_mbinfo);
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
            
            //if (dw_main.RowCount > 1) { dw_main.DeleteRow(dw_main.RowCount); }
            //if (dw_detail.RowCount > 1) { dw_detail.DeleteRow(dw_detail.RowCount); }
            //if (dw_status.RowCount > 1) { dw_status.DeleteRow(dw_status.RowCount); }
            //tdwData1.Eng2ThaiAllRow();
            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();
            dw_moneytr.SaveDataCache();
            dw_remarkstat.SaveDataCache();
            dw_status.SaveDataCache();
            //str_adjust_mbinfo lstr_mbinfo = new str_adjust_mbinfo();
            try
            {
                TextDetail.Text = dw_detail.Describe("DataWindow.Data.XML");
            }
            catch
            { }


        }

        private void JsNewClear()
        {

            dw_main.Reset();
            dw_detail.Reset();
            dw_status.Reset();
            dw_moneytr.Reset();
            dw_remarkstat.Reset();

            dw_main.InsertRow(0);
            dw_detail.InsertRow(0);
            dw_status.InsertRow(0);

            Image1.ImageUrl = "";
        }

        private void JsPostMember()
        {
            try
            {
                str_adjust_mbinfo lstr_mbinfo = new str_adjust_mbinfo();

                lstr_mbinfo.coop_id = state.SsCoopId;
                lstr_mbinfo.member_no = Hfmember_no.Value;
                lstr_mbinfo.xmlmaster=dw_main.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmldetail= dw_detail.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlstatus=dw_status.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlremarkstat= dw_remarkstat.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");

                //int result = shrlonService.of_initrequest(state.SsWsPass, ref lstr_mbinfo);
                int result = shrlonService.of_initrequest(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {
                    TextDwmain.Text = lstr_mbinfo.xmlmaster;
                    Textdwdata1.Text = lstr_mbinfo.xmldetail;
                    Textdwdata2.Text = lstr_mbinfo.xmlstatus;
                    Textdwdata3.Text = lstr_mbinfo.xmlmoneytr;
                    Textdwdata4.Text = lstr_mbinfo.xmlremarkstat;
                    try
                    {

                        if (lstr_mbinfo.xmlmoneytr == "")
                        {

                        }
                        else
                        {
                            dw_moneytr.Reset();
                            dw_moneytr.ImportString(lstr_mbinfo.xmlmoneytr, FileSaveAsType.Xml);

                        }

                        try
                        {
                            dw_main.Reset();
                            dw_main.ImportString(lstr_mbinfo.xmlmaster, FileSaveAsType.Xml);
                        }
                        catch (Exception ex)
                        {
                            dw_main.Reset();
                            dw_main.InsertRow(0);
                            LtServerMessage.Text = WebUtil.ErrorMessage("dw_main----" + ex);
                        }


                        try
                        {
                            dw_detail.Reset();
                            dw_detail.ImportString(lstr_mbinfo.xmldetail, FileSaveAsType.Xml);
                            tdwData1.Eng2ThaiAllRow();
                            //TextBox1.Text =dw_detail.Describe("DataWindow.Data.XML"); 
                        }
                        catch (Exception ex)
                        {
                            dw_detail.Reset();
                            dw_detail.InsertRow(0);
                            LtServerMessage.Text = WebUtil.ErrorMessage("dw_detail----" + ex);
                        }

                        try
                        {
                            dw_status.Reset();
                            dw_status.ImportString(lstr_mbinfo.xmlstatus, FileSaveAsType.Xml);
                       
                        }
                        catch (Exception ex)
                        {
                            dw_status.Reset();
                            dw_status.InsertRow(0);
                          //  LtServerMessage.Text = WebUtil.ErrorMessage("dw_status----" + ex);
                        }

                       
                        try
                        {

                            dw_remarkstat.Reset();
                            dw_remarkstat.ImportString(lstr_mbinfo.xmlremarkstat, FileSaveAsType.Xml);

                        }
                        catch { dw_remarkstat.Reset(); }

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        dw_main.Reset(); 
                        dw_main.InsertRow(0);
                        dw_detail.Reset();
                        dw_detail.InsertRow(0);
                        dw_status.Reset();
                        dw_status.InsertRow(0);

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

                DataWindowChild child = dw_detail.GetChild("district_code");
                child.SetTransaction(sqlca);
                child.Retrieve();
                String province_code = dw_detail.GetItemString(1, "province_code");
                child.SetFilter("province_code='" + province_code + "'");
                child.Filter();
            }
            catch (Exception ex)
            {

             //LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }

        private void JsGetTambol()
        {
            try
            {
                DataWindowChild childdis = dw_detail.GetChild("tambol_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String district_code = dw_detail.GetItemString(1, "district_code");
                childdis.SetFilter("district_code='" + district_code + "'");
                childdis.Filter();

                String provincecode = dw_detail.GetItemString(1, "province_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                              WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                               and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_detail.SetItemString(1, "postcode", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch (Exception ex)
            {
              //  LtServerMessage.Text = ex.ToString();
            }
        }

        private void JsGetPostcode()
        {
            try
            {
                String provincecode = dw_detail.GetItemString(1, "province_code");
                String district_code = dw_detail.GetItemString(1, "district_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
   WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
  and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_detail.SetItemString(1, "postcode", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch (Exception ex) { ex.ToString(); }
        }

        private void JsCallRetry()
        {
            try
            {
                String birth_tdate = dw_detail.GetItemString(1, "birth_tdate");
                String birth_date = WebUtil.ConvertDateThaiToEng(dw_detail, "birth_tdate", null);
                DateTime dt = Convert.ToDateTime(birth_date, System.Globalization.CultureInfo.CurrentCulture);
                //DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);
                DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);

                Decimal year = Convert.ToDecimal(retry.Year) + 543;
                String retry_date = retry.Day.ToString("00") + retry.Month.ToString("00") + year.ToString("0000");
                dw_detail.SetItemString(1, "retry_tdate", retry_date);
                dw_detail.SetItemDateTime(1, "retry_date", retry);

                // dw_detail.SetItemString(1, "birth_tdate", birth_tdate);
                dw_detail.SetItemDateTime(1, "birth_date", dt);
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
