using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Globalization;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;
using System.Data;
namespace Saving.Applications.mbshr
{
    public partial class w_sheet_sl_member_new_coopid : PageWebSheet, WebSheet
    {
        String newDocNo = "";
        protected String changeDistrict;
        protected String jsSalary;
        protected String jsIdCard;
        protected String jsCallRetry;
        protected String jsRefresh;
        protected String jsGetPostcode;
        protected String jsGetCurrDistrict;
        protected String jsGetCurrPostcode;
        protected String jsmembgroup_code;
        protected String jsChanegValue;
        protected String jsMemberNo;
        protected String jsGetdocno;
        protected String jsRefreshCoop;
        protected String jsgetpicMember_no;
        protected String jsRunMemberNo;
        protected String jsChangmidgroupcontrol;
        protected String jsLinkAddress;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwMain;
        protected String newclear;
        protected String CheckCoop;
        protected String jsChangmembsection;
        protected String jsGainInsertRow;
        protected String jsGainDeleteRow;
        protected String jsChangSex;
        protected String jsCheckNation;
        public void InitJsPostBack()
        {

            jsGetdocno = WebUtil.JsPostBack(this, "jsGetdocno");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            jsSalary = WebUtil.JsPostBack(this, "jsSalary");
            jsIdCard = WebUtil.JsPostBack(this, "jsIdCard");
            jsCallRetry = WebUtil.JsPostBack(this, "jsCallRetry");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsGetPostcode = WebUtil.JsPostBack(this, "jsGetPostcode");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsChangmembsection = WebUtil.JsPostBack(this, "jsChangmembsection");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsMemberNo = WebUtil.JsPostBack(this, "jsMemberNo");
            newclear = WebUtil.JsPostBack(this, "newclear");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            jsgetpicMember_no = WebUtil.JsPostBack(this, "jsgetpicMember_no");
            jsRunMemberNo = WebUtil.JsPostBack(this, "jsRunMemberNo");
            jsGetCurrDistrict = WebUtil.JsPostBack(this, "jsGetCurrDistrict");
            jsGetCurrPostcode = WebUtil.JsPostBack(this, "jsGetCurrPostcode");
            jsLinkAddress = WebUtil.JsPostBack(this, "jsLinkAddress");
            jsGainInsertRow = WebUtil.JsPostBack(this, "jsGainInsertRow");
            jsGainDeleteRow = WebUtil.JsPostBack(this, "jsGainDeleteRow");
            jsRefreshCoop = WebUtil.JsPostBack(this, "jsRefreshCoop");
            jsChangSex = WebUtil.JsPostBack(this, "jsChangSex");
            jsCheckNation = WebUtil.JsPostBack(this, "jsCheckNation");
            tdwMain = new DwThDate(dw_main, this);
            tdwMain.Add("date_resign", "date_tresign");
            tdwMain.Add("retry_date", "retry_tdate");
            tdwMain.Add("membdatefix_date", "membdatefix_tdate");
            tdwMain.Add("birth_date", "birth_tdate");
            tdwMain.Add("apply_date", "apply_date_tdate");
            tdwMain.Add("work_date", "work_tdate");

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
            dw_main.SetTransaction(sqlca);
            dw_data.SetTransaction(sqlca);
            dw_gain.SetTransaction(sqlca);

            if (IsPostBack)
            {

                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_data);
                this.RestoreContextDw(dw_gain);
                HdIsPostBack.Value = "true";
            }
            else
            {

                //ดึงลำดับเลขที่สมาชิกท้ายสุด+1 มาตั้งค่าให้ฟิลส์เลขสมาชิก
                //string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
                dw_main.SetItemDate(1, "apply_date", state.SsWorkDate);
                DateTime process_date = WebUtil.GetProcessDate(state.SsCoopControl, state.SsWorkDate.Year, state.SsWorkDate.Month);
                //dw_main.SetItemDate(1, "membdatefix_date", process_date);
                dw_main.SetItemString(1, "membdatefix_tdate", "");
                dw_main.SetItemDate(1, "work_date", state.SsWorkDate);
                dw_main.SetItemString(1, "nationality", "ไทย");
                // dw_main.SetItemString(1, "member_no", max_memberno);
                dw_main.SetItemString(1, "member_no", "AUTO");
                dw_main.SetItemDecimal(1, "registype_flag", 1);
                DwUtil.RetrieveDDDW(dw_main, "membtype_code_1", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "tambol_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "district_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "currtambol_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "currprovince_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "curramphur_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "select_coop", "sl_member_new.pbl", state.SsCoopControl);
                tdwMain.Eng2ThaiAllRow();
                dw_data.InsertRow(0);
                HdIsPostBack.Value = "false";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "changeDistrict")
            {
                ChangeDistrict();

            }
            else if (eventArg == "jsSalary")
            {
                JsSalary();

            }
            else if (eventArg == "jsIdCard")
            {
                JsIdCard();

            }

            else if (eventArg == "jsCallRetry")
            {
                JsCallRetry();
            }
            else if (eventArg == "jsRefresh")
            {
                JsRefresh();
            }
            else if (eventArg == "jsGetPostcode")
            {
                JsGetPostcode();
            }
            else if (eventArg == "jsGetCurrDistrict")
            {
                JsGetCurrDistrict();
            }
            else if (eventArg == "jsGetCurrPostcode")
            {
                JsGetCurrPostcode();
            }
            else if (eventArg == "jsmembgroup_code")
            {
                Jsmembgroup_code();
            }
            else if (eventArg == "jsChanegValue")
            {
                JsChanegValue();

            }
            else if (eventArg == "jsMemberNo")
            {
                JsMemberNo();
            }
            else if (eventArg == "newclear")
            {
                NewClear();
            }
            else if (eventArg == "jsGetdocno")
            {
                JsGetdocno();
            }
            else if (eventArg == "jsgetpicMember_no")
            {
                string member_no = Hidmem_no.Value;
                String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
                Image1.ImageUrl = imageUrl;

            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }
            else if (eventArg == "jsRunMemberNo")
            {
                JsRunMemberNo();
            }
            else if (eventArg == "jsLinkAddress")
            {
                JsLinkAddress();
            }
            else if (eventArg == "jsChangmidgroupcontrol")
            {
                JsChangmidgroupcontrol();
            }
            else if (eventArg == "jsChangmembsection")
            {
                JsChangmembsection();
            }
            else if (eventArg == "jsGainInsertRow")
            {
                JsGainInsertRow();
            }
            else if (eventArg == "jsGainDeleteRow")
            {
                JsGainDeleteRow();
            }
            else if (eventArg == "jsRefreshCoop")
            {
                JsRefreshCoop();
            }
            else if (eventArg == "jsChangSex")
            {
                JsChangSex();
            }
            else if (eventArg == "jsCheckNation")
            {
                JsCheckNation();
            }
            
           
        }

       

        private void JsCheckNation()
        {
            dw_main.SetItemString(1, "card_person", "");
        }

        private void JsChangSex()
        {
            string prename_code = dw_main.GetItemString(1, "prename_code");

            Sta ta = new Sta(sqlca.ConnectionString);

            String sql = @"   SELECT sex
                              FROM mbucfprename  
                              WHERE prename_code ='" + prename_code + "'";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String sex = dt.GetString("sex");
                dw_main.SetItemString(1, "sex", sex);

            }

        }

        private void JsRefreshCoop()
        {

        }

        private void JsGainDeleteRow()
        {
            int row = Convert.ToInt32(HdChkRowDel.Value);
            dw_gain.DeleteRow(row);
            for (int i = 1; i <= dw_gain.RowCount; i++)
            {
                dw_gain.SetItemDecimal(i, "seq_no", i);
            }
        }

        private void JsGainInsertRow()
        {
            dw_gain.InsertRow(0);
            dw_gain.SetItemDecimal(dw_gain.RowCount, "seq_no", dw_gain.RowCount);
            DwUtil.RetrieveDDDW(dw_gain, "gain_relation", "sl_member_new.pbl", null);
        }

        private void checkCoop()
        {
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            { }
            NewClear();
            if (i == 1)
            {
                dw_main.SetItemDecimal(1, "check_coop", i);
            }
        }

        private void JsGetdocno()
        {
            //String memcoop_id = state.SsCoopId;
            String memcoop_id = Hdcoop_id.Value.Trim();
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            { }
            if (i == 1)
            {
                memcoop_id = dw_main.GetItemString(1, "select_coop");
            }


            String doc_no = Hdoc_no.Value;
            dw_main.Retrieve(memcoop_id, doc_no);
            dw_data.Retrieve(memcoop_id, doc_no);
            dw_gain.Retrieve(memcoop_id, doc_no);
            JsSalary();
            ChangeDistrict();
            JsGetPostcode();
            JsGetCurrDistrict();
            JsGetCurrPostcode();
            tdwMain.Eng2ThaiAllRow();
        }

        public void SaveWebSheet()
        {
           

            String memcoop_id = state.SsCoopControl;
            //ค่า coop_id ที่มีการสมัครสมาชิกมาจากสาขาอื่น
            String coop_id = Hdcoop_id.Value.Trim();
          
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            {
              
            }

            if (i == 1)
            {
                memcoop_id = dw_main.GetItemString(1, "select_coop");
            }


                try
                {
                    string salary_id = "";
                    try
                    {
                        salary_id = dw_main.GetItemString(1, "salary_id");
                    }
                    catch { dw_main.SetItemString(1, "salary_id", "00000000"); }
                    str_mbreqnew astr_mbreqnew = new str_mbreqnew();
                    astr_mbreqnew.xml_mbdetail = dw_main.Describe("DataWindow.Data.XML");
                    astr_mbreqnew.entry_id = state.SsUsername;
                    //int result = shrlonService.SaveReqmbnew(state.SsWsPass, state.SsCoopId, memcoop_id, ref astr_mbreqnew);
                    //mai แก้ไขกรณีที่อยู่สาขาศิริราช แล้วเปิดใบคำขอมาแก้ไข
                    int result = shrlonService.of_savereq_mbnew(state.SsWsPass, ref astr_mbreqnew);
                    if (result == 1)
                    {
                        if (dw_gain.RowCount > 0)
                        {
                            string docno = "";
                            try
                            {
                                docno = dw_main.GetItemString(1, "appl_docno");
                            }
                            catch
                            {
                                DataTable dt = WebUtil.Query(@"SELECT * FROM CMDOCUMENTCONTROL  WHERE COOP_ID = '" + memcoop_id + "' and DOCUMENT_CODE ='MBAPPLDOCNO'");
                                docno = dt.Rows[0]["last_documentno"].ToString();
                                docno = WebUtil.Right("0000000000" + docno, 10);
                            }

                            DateTime birth_date = dw_main.GetItemDateTime(1, "birth_date");
                            decimal age = DateTime.Today.Year - birth_date.Year;
                            try
                            {
                                for (int k = 1; k <= dw_gain.RowCount; k++)
                                {
                                    //mai แก้ไข Set ค่า coop_id ให้ในกรณีแก้ไขข้อมูลผ่านสาขาศิริราช
                                    //dw_gain.SetItemString(k, "coop_id", state.SsCoopId);
                                    dw_gain.SetItemString(k, "coop_id", coop_id);
                                    dw_gain.SetItemDateTime(k, "write_date", state.SsWorkDate);
                                    dw_gain.SetItemString(k, "write_at", state.SsCoopName);
                                    dw_gain.SetItemString(k, "appl_docno", docno);
                                    dw_gain.SetItemString(k, "age", age.ToString());
                                }

                                dw_gain.UpdateData();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                        NewClear();
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            
        }

        public void WebSheetLoadEnd()
        {
            //String membdate_date;
            //try
            //{
            //    membdate_date = dw_main.GetItemDate(1, "membdatefix_date").ToString("MM/dd/yyyy"); //WebUtil.ConvertDateThaiToEng(dw_main, "membdatefix_tdate", null);

            //}
            //catch { membdate_date = ""; }
            //String apply_date = dw_main.GetItemDate(1, "apply_date").ToString("MM/dd/yyyy");
            //Session["membdatefix_date"] = membdate_date;
            //Session["apply_date_tdate"] = apply_date;
            //DateTime membdatefix_date = Convert.ToDateTime(Session["membdatefix_date"].ToString());
            //DateTime apply_date_tdate = Convert.ToDateTime(Session["apply_date_tdate"].ToString());
            //if (dw_data.RowCount > 1) { DwUtil.DeleteLastRow(dw_data); }
            //else if (dw_data.RowCount < 1)
            //{
            //    dw_data.InsertRow(0);
            //}
            //try
            //{
            //    if (dw_main.GetItemString(1, "district_code") != null)
            //    {
            //        ChangeDistrict();
            //    }
            //}
            //catch { }

            ////H-Code  เรื่อง Session
            //TextBox1.Text = Session["membdatefix_date"].ToString();
            //TextBox2.Text = Session["apply_date_tdate"].ToString();

            dw_main.SaveDataCache();
            dw_data.SaveDataCache();
            dw_gain.SaveDataCache();

        }

        private void JsRunMemberNo()
        {
            //ตรวจสอบว่าเลือกกำหนดเลขสมาชิกเองหรือไม่ ?
            decimal i = dw_main.GetItemDecimal(1, "cre_membno");
            if (i == 0)
            {
                //string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
                // dw_main.SetItemString(1, "member_no", max_memberno);
                dw_main.SetItemString(1, "member_no", "AUTO");
                dw_main.SetItemDecimal(1, "registype_flag", 1);
            }
            else
            {
                dw_main.SetItemString(1, "member_no", "");
                dw_main.SetItemDecimal(1, "registype_flag", 2);
            }
        }

        private void JsCallRetry()
        {
            String memcoop_id = state.SsCoopControl;
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            {
              
            }
            if (i == 1)
            {
                memcoop_id = dw_main.GetItemString(1, "select_coop");
            }
            

            try
            {

                String birth_date = dw_main.GetItemString(1, "birth_tdate");
                DateTime dt = DateTime.ParseExact(birth_date, "ddMMyyyy", WebUtil.TH);

                DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);

                dw_main.SetItemString(1, "retry_tdate", retry.ToString("ddMMyyyy", WebUtil.TH));
                dw_main.SetItemDateTime(1, "retry_date", retry);
                dw_main.SetItemDateTime(1, "birth_date", dt);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }

        private void JsRefresh()
        {
            String appltype_code = HidColname.Value;

            if (appltype_code == "appltype_code")
            {
                String appltype = dw_main.GetItemString(1, "appltype_code");
                if (appltype == "04")
                {
                    String setting = dw_data.Describe("recv_shrstatus.Protect");
                    dw_data.Modify("recv_shrstatus.Protect=1");

                    // dw_data.Modify("recv_shrstatus.Protect='1~tIf(IsRowNew(),0,1)'");
                    String setting1 = dw_data.Describe("recv_shrstatus.Protect");

                }
            }

        }

        private void JsIdCard()
        {
            string nationality = "";
            try
            {
                nationality = dw_main.GetItemString(1, "nationality");
            }
            catch (Exception ex)
            {
                dw_main.SetItemString(1, "card_person", "");
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุสัญชาติ");

            }

            if (nationality == "ไทย")
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
                            String memcoop_id = state.SsCoopControl;
                            decimal i = 0;
                            try
                            {
                                i = dw_main.GetItemDecimal(1, "check_coop");
                            }
                            catch
                            {
                              
                            }
                            if (i == 1)
                            {
                                memcoop_id = dw_main.GetItemString(1, "select_coop");
                            }

                            string checkcard = wcf.InterPreter.CheckCardPerson(state.SsConnectionIndex, memcoop_id, PID);


                            //Sta ta1 = new Sta(sqlca.ConnectionString);
                            //String sql1 = " SELECT MBMEMBMASTER.MEMBER_NO,    MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME  ,MBMEMBMASTER.CARD_PERSON FROM MBMEMBMASTER WHERE ( MBMEMBMASTER.CARD_PERSON ='" + PID + "' )  AND MBMEMBMASTER.RESIGN_STATUS=0 and MBMEMBMASTER.COOP_ID ='" + memcoop_id + "' ";
                            //Sdt dt1 = ta1.Query(sql1);
                            if (checkcard == "")
                            {
                                dw_main.SetItemString(1, "card_person", PID);

                            }
                            else
                            {   //aek รอถาม ให้สามารถบันทึกเลขบัตรประชาชนที่ซ้ำได้
                                dw_main.SetItemString(1, "card_person", "");
                                LtServerMessage.Text = WebUtil.ErrorMessage(checkcard);

                            }
                        }
                        else { LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง"); }

                    }
                    catch { }
                }
            }
        }

        private void NewClear()
        {
            dw_main.Reset();
            dw_data.Reset();
            dw_gain.Reset();
            dw_data.InsertRow(0);
            dw_main.InsertRow(0);
            H_periodshare_value.Value = "";
            Image1.ImageUrl = "";
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            ////H-Code  เรื่อง Session
            //DateTime membdatefix_date = Convert.ToDateTime(Session["membdatefix_date"].ToString());
            //DateTime apply_date_tdate = Convert.ToDateTime(Session["apply_date_tdate"].ToString());
            // string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
            dw_main.SetItemString(1, "member_no", "AUTO");
            dw_main.SetItemString(1, "nationality", "ไทย");
            dw_main.SetItemDate(1, "apply_date", state.SsWorkDate);
            //dw_main.SetItemDate(1, "membdatefix_date", membdatefix_date);
            dw_main.SetItemDecimal(1, "registype_flag", 1);
            DwUtil.RetrieveDDDW(dw_main, "membtype_code_1", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "tambol_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "district_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "select_coop", "sl_member_new.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_main, "currtambol_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "currprovince_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "curramphur_code", "sl_member_new.pbl", null);
            tdwMain.Eng2ThaiAllRow();




        }

        private void JsSalary()
        {
            String memcoop_id = state.SsCoopControl;
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            {
               
            }
            if (i == 1)
            {
                memcoop_id = dw_main.GetItemString(1, "select_coop");
            }
           

            Decimal member_type = dw_main.GetItemDecimal(1, "member_type");
            Decimal salary_amount = dw_main.GetItemDecimal(1, "salary_amount");
            String sharetype_code = dw_data.GetItemString(1, "sharetype_code");
            string minmaxshare = CalPayShareMonth(sharetype_code, memcoop_id, salary_amount, member_type);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);
            dw_data.SetItemDecimal(1, "periodbase_value", minmaxshare[0]);
            // dw_data.SetItemDecimal(1, "periodshare_value", minmaxshare[0]);
            dw_data.SetItemDecimal(1, "maxshare_value", minmaxshare[1]);

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
        private void ChangeDistrict()
        {
            try
            {

                DataWindowChild childdis = dw_main.GetChild("district_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String provincecode = dw_main.GetItemString(1, "province_code");
                childdis.SetFilter("province_code='" + provincecode + "'");
                childdis.Filter();

            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }

        private void JsGetPostcode()
        {
            try
            {
                DataWindowChild child = dw_main.GetChild("tambol_code");
                child.SetTransaction(sqlca);
                child.Retrieve();
                String district_code = dw_main.GetItemString(1, "district_code");
                child.SetFilter("district_code='" + district_code + "'");
                child.Filter();

                String provincecode = dw_main.GetItemString(1, "province_code");
                // String district_code = dw_main.GetItemString(1, "district_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                                and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_main.SetItemString(1, "postcode", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
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

        private void JsGetCurrPostcode()
        {
            try
            {
                DataWindowChild child = dw_main.GetChild("currtambol_code");
                child.SetTransaction(sqlca);
                child.Retrieve();
                String curramphur_code = dw_main.GetItemString(1, "curramphur_code");
                child.SetFilter("DISTRICT_CODE='" + curramphur_code + "'");
                child.Filter();

                String currprovince_code = dw_main.GetItemString(1, "currprovince_code");
                // String district_code = dw_main.GetItemString(1, "district_code");
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

        private void Jsmembgroup_code()
        {

            String group = Hidgroup.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            //            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
            //         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
            //         MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
            //    FROM MBUCFMEMBGROUP  
            //   WHERE length ( trim( mbucfmembgroup.membgroup_code ) ) > ( select length( max( trim( b.membgroup_control ) ) ) from mbucfmembgroup b )  AND  MBUCFMEMBGROUP.MEMBGROUP_CODE='" + group + "' AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "' ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";
            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
                              MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                              MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
                              FROM MBUCFMEMBGROUP  
                              WHERE MBUCFMEMBGROUP.MEMBGROUP_LEVEL=2 AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "' and MBUCFMEMBGROUP.membgroup_code ='" + group + "'  ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String MEMBGROUP_DESC = dt.GetString("MEMBGROUP_DESC");
                dw_main.SetItemString(1, "membgroup_code_1", MEMBGROUP_DESC);
                String MEMBGROUP_CODE = dt.GetString("MEMBGROUP_CODE");
                dw_main.SetItemString(1, "membgroup_code", MEMBGROUP_CODE);

            }

            string group_control = dw_main.GetItemString(1, "membgroup_code");
            dw_main.SetItemString(1, "membsection_code", "");
            DwUtil.RetrieveDDDW(dw_main, "membsection_code_1", "sl_member_new.pbl", state.SsCoopControl, group_control);
            //string membgroup_code = dw_main.GetItemString(1, "membgroup_code");
            //string midgroup_control = WebUtil.Mid(membgroup_code, 1, 3);
            //string membgroup_control = WebUtil.Mid(membgroup_code, 1, 6);
            //DwUtil.RetrieveDDDW(dw_main, "midgroup_control", "sl_member_new.pbl", state.SsCoopControl, midgroup_control);
            //DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", state.SsCoopControl, membgroup_control);
            //dw_main.SetItemString(1, "group_control", midgroup_control);
            //dw_main.SetItemString(1, "midgroup_control", membgroup_control);
            //dw_main.SetItemString(1, "membgroup_code_1", membgroup_code);
        }

        private void JsChangmidgroupcontrol()
        {
            string group = Hidmembsection.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
                              MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                              MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
                              FROM MBUCFMEMBGROUP  
                              WHERE MBUCFMEMBGROUP.MEMBGROUP_LEVEL=3 AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "'  and mbucfmembgroup.membgroup_code ='" + group + "' ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String MEMBGROUP_DESC = dt.GetString("MEMBGROUP_DESC");
                dw_main.SetItemString(1, "membsection_code_1", MEMBGROUP_DESC);
                String MEMBGROUP_CODE = dt.GetString("MEMBGROUP_CODE");
                dw_main.SetItemString(1, "membsection_code", MEMBGROUP_CODE);

            }

        }

        private void JsChangmembsection()
        {
            string group_control = dw_main.GetItemString(1, "membgroup_code");
            DwUtil.RetrieveDDDW(dw_main, "membsection_code_1", "sl_member_new.pbl", state.SsCoopControl, group_control);
        }

        private void JsChanegValue()
        {
            // LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
            dw_data.SetItemDecimal(1, "periodshare_value", 0);
            //Decimal periodbase_value = dw_main.GetItemDecimal(1, "periodbase_value");
            //Decimal periodshare_value = dw_main.GetItemDecimal(1, "periodshare_value");

        }

        private void JsMemberNo()
        {
            Sta ta2 = new Sta(sqlca.ConnectionString);
            Sta ta = new Sta(sqlca.ConnectionString);
            String mem_no = Hidmem_no.Value;
            String coop_id = state.SsCoopId;
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            {
                coop_id = state.SsCoopControl;
            }
            if (i == 1)
            {
                coop_id = dw_main.GetItemString(1, "select_coop");
            }


            Sta ta1 = new Sta(sqlca.ConnectionString);
            String sql1 = " SELECT MBMEMBMASTER.MEMBER_NO,    MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME   FROM MBMEMBMASTER WHERE ( MBMEMBMASTER.MEMBER_NO ='" + mem_no + "' )  and MBMEMBMASTER.COOP_ID ='" + coop_id + "' ";
            Sdt dt1 = ta1.Query(sql1);
            if (dt1.Next())
            {
                String member_no = dt1.GetString("MEMBER_NO");
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขสมาชิกนี้     " + member_no + "   เป็นสมาชิกอยู่แล้ว");
                dw_main.SetItemString(1, "member_no", "");

            }
            String APPL_DOCNO = "";
            String sql2 = " SELECT MBREQAPPL.MEMBER_NO   FROM MBREQAPPL  WHERE MBREQAPPL.MEMBER_NO ='" + mem_no + "' and MBREQAPPL.COOP_ID ='" + coop_id + "' and appl_status <> 1";
            Sdt dt2 = ta2.Query(sql2);
            if (dt2.Next())
            {
                String member_no2 = dt2.GetString("MEMBER_NO");
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขสมาชิกนี้" + member_no2 + "รออนุมัติอยู่");
                dw_main.SetItemString(1, "member_no", "");
                String sql = "  SELECT APPL_DOCNO FROM MBREQAPPL   WHERE MEMBER_NO =  '" + member_no2 + "' and MBREQAPPL.COOP_ID ='" + coop_id + "' ";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    APPL_DOCNO = dt.GetString("APPL_DOCNO");
                    dw_main.Retrieve(state.SsCoopId, APPL_DOCNO);
                    dw_data.Retrieve(state.SsCoopId, APPL_DOCNO);
                    tdwMain.Eng2ThaiAllRow();
                }
            }

            String sqlshare = "  SELECT  PERIODSHARE_VALUE,  PERIODBASE_VALUE  FROM MBREQAPPLSHARE   WHERE ( APPL_DOCNO ='" + APPL_DOCNO + "' ) and MBREQAPPLSHARE.COOP_ID ='" + coop_id + "'  ";
            Sdt dtshare = ta.Query(sqlshare);
            if (dtshare.Next())
            {
                Decimal PERIODSHARE_VALUE = dtshare.GetDecimal("PERIODSHARE_VALUE");
                Decimal PERIODBASE_VALUE = dtshare.GetDecimal("PERIODBASE_VALUE");
                dw_data.SetItemDecimal(1, "periodshare_value", PERIODSHARE_VALUE);
                dw_data.SetItemDecimal(1, "periodbase_value", PERIODBASE_VALUE);
            }
        }

        private void JsLinkAddress()
        {
            string memb_addr = "";
            string addr_group = "";
            string mooban = "";
            string soi = "";
            string road = "";
            string tambol_code = "";
            string district_code = "";
            string province_code = "";
            string postcode = "";
            string mem_tel = "";
            try
            {
                memb_addr = dw_main.GetItemString(1, "memb_addr");
                dw_main.SetItemString(1, "curraddr_no", memb_addr);
            }
            catch { }
            try
            {
                addr_group = dw_main.GetItemString(1, "addr_group");
                dw_main.SetItemString(1, "curraddr_moo", addr_group);
            }
            catch { }
            try
            {
                mooban = dw_main.GetItemString(1, "mooban");
                dw_main.SetItemString(1, "curraddr_village", mooban);
            }
            catch { }
            try
            {
                soi = dw_main.GetItemString(1, "soi");
                dw_main.SetItemString(1, "curraddr_soi", soi);
            }
            catch { }
            try
            {
                road = dw_main.GetItemString(1, "road");
                dw_main.SetItemString(1, "curraddr_road", road);
            }
            catch { }
            try
            {
                tambol_code = dw_main.GetItemString(1, "tambol_code");
                dw_main.SetItemString(1, "currtambol_code", tambol_code);
            }
            catch { }
            try
            {
                district_code = dw_main.GetItemString(1, "district_code");
                dw_main.SetItemString(1, "curramphur_code", district_code);
            }
            catch { }
            try
            {
                province_code = dw_main.GetItemString(1, "province_code");
                dw_main.SetItemString(1, "currprovince_code", province_code);
            }
            catch { }
            try
            {
                postcode = dw_main.GetItemString(1, "postcode");
                dw_main.SetItemString(1, "curraddr_postcode", postcode);
            }
            catch { }
            try
            {
                mem_tel = dw_main.GetItemString(1, "mem_tel");
                dw_main.SetItemString(1, "curraddr_phone", mem_tel);
            }
            catch { }
        }


    }
}
