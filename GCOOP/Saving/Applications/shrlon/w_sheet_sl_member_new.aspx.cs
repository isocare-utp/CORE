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
namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_member_new : PageWebSheet, WebSheet
    {
        String newDocNo = "";
        protected String changeDistrict;
        protected String jsSalary;
        protected String jsIdCard;
        protected String jsCallRetry;
        protected String jsRefresh;
        protected String jsGetPostcode;
        protected String jsmembgroup_code;
        protected String jsChanegValue;
        protected String jsMemberNo;
        protected String jsGetdocno;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwMain;
        protected String newclear;
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
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsMemberNo = WebUtil.JsPostBack(this, "jsMemberNo");
            newclear = WebUtil.JsPostBack(this, "newclear");
            tdwMain = new DwThDate(dw_main, this);
            tdwMain.Add("date_resign", "date_tresign");
            tdwMain.Add("retry_date", "retry_tdate");
            tdwMain.Add("membdatefix_date", "membdatefix_tdate");
            tdwMain.Add("birth_date", "birth_tdate");
            tdwMain.Add("apply_date", "apply_date_tdate");

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

            if (IsPostBack)
            {

                dw_main.RestoreContext();
                dw_data.RestoreContext();

            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                dw_main.SetItemDate(1, "apply_date", state.SsWorkDate);
                dw_main.SetItemDate(1, "membdatefix_date", state.SsWorkDate);
                //DwUtil.RetrieveDDDW(dw_main, "subgroup_code", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "tambol_code", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "district_code", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", null);
                //DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
                

                tdwMain.Eng2ThaiAllRow();
                dw_data.InsertRow(0);
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

        }

        private void JsGetdocno()
        {
            String doc_no = Hdoc_no.Value;
            dw_main.Retrieve(doc_no);
            dw_data.Retrieve(doc_no);
        }

        public void SaveWebSheet()
        {

            try
            {
                String district_code, appl_docno, card_person, province_code, mate_name, mem_tel, resigncause_code, old_member_no, subgroup_code;
                String mem_telwork, mem_telmobile, email_address, remark, memb_addr, addr_group, mooban, soi, road, salary_id, tambol_code, mariage, postcode;
                Decimal salary_amount = 0, membdatefix_flag, periodshare_value, periodbase_value;
                //DateTime birth_date = dw_main.GetItemDate(1, "birth_date");
                String birth = dw_main.GetItemString(1, "birth_tdate");

                String birth_date1 = WebUtil.Left(birth, 2);
                String birth_date2 = WebUtil.Left(birth, 4);
                String birth_date3 = WebUtil.Right(birth, 4);
                Int32 year = Convert.ToInt32(birth_date3) - 543;
                String birth_date4 = WebUtil.Right(birth_date2, 2);
                String birth_date = birth_date4 + "/" + birth_date1 + "/" + year.ToString();

            //    String birth_date = dw_main.GetItemDate(1, "birth_date").ToString("MM/dd/yyyy");
                String retry_date = dw_main.GetItemDate(1, "retry_date").ToString("MM/dd/yyyy"); //WebUtil.ConvertDateThaiToEng(dw_main, "retry_tdate", null);
                String date_resign;

                String appltype_code = dw_main.GetItemString(1, "appltype_code");
                dw_main.SetItemString(1, "member_no", dw_main.GetItemString(1, "member_no").Trim());
                String coopbranch_id = state.SsCoopId;

                String apply_date_tdate = dw_main.GetItemDate(1, "apply_date").ToString("MM/dd/yyyy");// WebUtil.ConvertDateThaiToEng(dw_main, "apply_date_tdate", null);

                String membdatefix_date;
                periodshare_value = dw_data.GetItemDecimal(1, "periodshare_value");
                periodbase_value = dw_data.GetItemDecimal(1, "periodbase_value");
                String memb_name = dw_main.GetItemString(1, "memb_name");
                String prename_code = dw_main.GetItemString(1, "prename_code");
                String memb_surname = dw_main.GetItemString(1, "memb_surname");
                String membgroup_code = dw_main.GetItemString(1, "membgroup_code");
                String sex = dw_main.GetItemString(1, "sex");
                try
                {
                    membdatefix_date = dw_main.GetItemDate(1, "membdatefix_date").ToString("MM/dd/yyyy"); //WebUtil.ConvertDateThaiToEng(dw_main, "membdatefix_tdate", null);

                }
                catch { membdatefix_date = ""; }
                try
                {
                    date_resign = dw_main.GetItemDate(1, "date_resign").ToString("MM/dd/yyyy"); //WebUtil.ConvertDateThaiToEng(dw_main, "date_tresign", null);

                }
                catch { date_resign = ""; }
                try
                {
                    resigncause_code = dw_main.GetItemString(1, "resigncause_code");

                }
                catch { resigncause_code = ""; }
                try
                {
                    old_member_no = dw_main.GetItemString(1, "old_member_no");

                }
                catch { old_member_no = ""; }
                try
                {
                    memb_addr = dw_main.GetItemString(1, "memb_addr");

                }
                catch { memb_addr = ""; }
                try
                {
                    addr_group = dw_main.GetItemString(1, "addr_group");

                }
                catch { addr_group = ""; }
                try
                {
                    mooban = dw_main.GetItemString(1, "mooban");

                }
                catch { mooban = ""; }
                try
                {
                    soi = dw_main.GetItemString(1, "soi");

                }
                catch { soi = ""; }
                try
                {
                    road = dw_main.GetItemString(1, "road");

                }
                catch { road = ""; }
                try
                {
                    remark = dw_main.GetItemString(1, "remark");

                }
                catch { remark = ""; }
                try
                {
                    email_address = dw_main.GetItemString(1, "email_address");

                }
                catch { email_address = ""; }
                try
                {
                    mem_telmobile = dw_main.GetItemString(1, "mem_telmobile");

                }
                catch { mem_telmobile = ""; }
                try
                {
                    mem_tel = dw_main.GetItemString(1, "mem_tel");

                }
                catch { mem_tel = ""; }
                try
                {
                    mem_telwork = dw_main.GetItemString(1, "mem_telwork");

                }
                catch { mem_telwork = ""; }
                try
                {
                    mate_name = dw_main.GetItemString(1, "mate_name");

                }
                catch { mate_name = ""; }
                try
                {
                    district_code = dw_main.GetItemString(1, "district_code");

                }
                catch { district_code = ""; }
                try
                {
                    tambol_code = dw_main.GetItemString(1, "tambol_code");

                }
                catch { tambol_code = ""; }
                try
                {

                    card_person = dw_main.GetItemString(1, "card_person");

                }
                catch { card_person = ""; }
                try
                {

                    salary_amount = dw_main.GetItemDecimal(1, "salary_amount");

                }
                catch { salary_amount = 0; }
                try
                {

                    province_code = dw_main.GetItemString(1, "province_code");
                }
                catch { province_code = ""; }
                try
                {

                    salary_id = dw_main.GetItemString(1, "salary_id");
                }
                catch { salary_id = ""; }
                try
                {

                    appl_docno = dw_main.GetItemString(1, "appl_docno");
                }
                catch { appl_docno = ""; }
                try
                {

                    mariage = dw_main.GetItemString(1, "mariage");
                }
                catch { mariage = ""; }
                try
                {

                    subgroup_code = dw_main.GetItemString(1, "subgroup_code");
                }
                catch { subgroup_code = ""; }
                try{

                    postcode = dw_main.GetItemString(1, "postcode");
                }
                catch { postcode = ""; }
                
                membdatefix_flag = dw_main.GetItemDecimal(1, "membdatefix_flag");
                String member_no = dw_main.GetItemString(1, "member_no").Trim();
                member_no = WebUtil.MemberNoFormat(member_no);
                ////H-Code  เรื่อง Session
                Session["membdatefix_date"] = membdatefix_date;
                Session["apply_date_tdate"] = apply_date_tdate;

                Decimal appl_status = 8;
                Sta ta = new Sta(sqlca.ConnectionString);
                Sta taupdate = new Sta(sqlca.ConnectionString);
                if (appl_docno != "")
                {
                    String sqlupdate = @" UPDATE  MBREQAPPL 
                                 SET        PRENAME_CODE = '" + prename_code + "',     MEMB_NAME =  '" + memb_name + @"',SUBGROUP_CODE='"+subgroup_code+@"',POSTCODE='"+postcode+@"',
                                 MEMB_SURNAME =  '" + memb_surname + "',   MEMBGROUP_CODE =  '" + membgroup_code + "',         SEX =  '" + sex + @"',  MARIAGE='" + mariage + @"'   ,
                                 DISTRICT_CODE =  '" + district_code + "',  CARD_PERSON =  '" + card_person + "',      SALARY_AMOUNT =  " + salary_amount + ",    SALARY_ID =  '" + salary_id + @"',     
                                 COOPBRANCH_ID =  '" + coopbranch_id + "',  MEMBER_NO =  '" + member_no + "',        PROVINCE_CODE =  '" + province_code + "' ,    APPL_STATUS = " + appl_status + @" ,
                                 APPLY_DATE = to_date('" + apply_date_tdate + "','mm/dd/yyyy') ,     RETRY_DATE = to_date('" + retry_date + "','mm/dd/yyyy'),       BIRTH_DATE = to_date('" + birth_date + "','mm/dd/yyyy'),       MATE_NAME = '" + mate_name + @"',
                                 MEM_TEL = '" + mem_tel + "',           MEM_TELWORK = '" + mem_telwork + "',    EMAIL_ADDRESS= '" + email_address + "',    MEM_TELMOBILE = '" + mem_telmobile + @"',
                                 REMARK = '" + remark + "',           MEMB_ADDR =  '" + memb_addr + "',         ADDR_GROUP = '" + addr_group + "',     SOI ='" + soi + "',    MOOBAN = '" + mooban + @"',  
                                 ROAD= '" + road + "', OLD_MEMBER_NO = '" + old_member_no + "',RESIGNCAUSE_CODE = '" + resigncause_code + @"' ,
                                 DATE_RESIGN= to_date('" + date_resign + "','mm/dd/yyyy'),MEMBDATEFIX_DATE=to_date('" + membdatefix_date + "','mm/dd/yyyy') ,MEMBDATEFIX_FLAG =" + membdatefix_flag + ",TAMBOL_CODE='"+tambol_code+"' WHERE APPL_DOCNO = '" + appl_docno + "'";
                    taupdate.Exe(sqlupdate);

                    String sqlStr = @"UPDATE MBREQAPPLSHARE  
                                         SET PERIODBASE_VALUE = " + periodbase_value + @",   
                                             PERIODSHARE_VALUE = " + periodshare_value + @" 
                                       WHERE MBREQAPPLSHARE.APPL_DOCNO ='" + appl_docno + "'";
                    taupdate.Exe(sqlStr);
                    taupdate.Close();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขเรียบร้อย");
                }
                else
                {
                    newDocNo = commonService.of_getnewdocno(state.SsWsPass,state.SsCoopId, "MBAPPLDOCNO");
                    dw_main.SetItemString(1, "appl_docno", newDocNo);
                    String sql = @"  INSERT INTO MBREQAPPL  
                               ( APPL_DOCNO,     APPLTYPE_CODE,    PRENAME_CODE,     MEMB_NAME,
                                 MEMB_SURNAME,   MEMBGROUP_CODE, SUBGROUP_CODE,      SEX,      MARIAGE, 
                                 DISTRICT_CODE,  CARD_PERSON,      SALARY_AMOUNT,    SALARY_ID,     
                                 COOPBRANCH_ID,  MEMBER_NO,        PROVINCE_CODE ,   POSTCODE, APPL_STATUS,
                                 APPLY_DATE ,     RETRY_DATE,       BIRTH_DATE,       MATE_NAME,
                                 MEM_TEL,           MEM_TELWORK,    EMAIL_ADDRESS,    MEM_TELMOBILE,
                                 REMARK,           MEMB_ADDR,         ADDR_GROUP,     SOI,    MOOBAN,  
                                ROAD, OLD_MEMBER_NO,RESIGNCAUSE_CODE ,DATE_RESIGN,MEMBDATEFIX_DATE,MEMBDATEFIX_FLAG ,TAMBOL_CODE)  

                             VALUES ( '" + newDocNo + "', '" + appltype_code + "','" + prename_code + "', '" + memb_name + "', '" + memb_surname + "',  '" + membgroup_code + "','" + subgroup_code + "'  ,'" + sex + "', '" + mariage + "',  '" + district_code + "',   '" + card_person + "',   " + salary_amount + ",  '" + salary_id + "',        '" + coopbranch_id + "',           '" + member_no + "',            '" + province_code + "','" + postcode + "'," + appl_status + " ,to_date('" + apply_date_tdate + "','mm/dd/yyyy'),to_date('" + retry_date + "','mm/dd/yyyy'),to_date('" + birth_date + "','mm/dd/yyyy'),'" + mate_name + "','" + mem_tel + "','" + mem_telwork + "','" + email_address + "','" + mem_telmobile + "','" + remark + "',  '" + memb_addr + "', '" + addr_group + "', '" + soi + "','" + mooban + "',  '" + road + "','" + old_member_no + "','" + resigncause_code + "',to_date('" + date_resign + "','mm/dd/yyyy'),to_date('" + membdatefix_date + "','mm/dd/yyyy')," + membdatefix_flag + ",'" + tambol_code + "')  ";
                    ta.Exe(sql);
                    ta.Close();
                    dw_data.UpdateData();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");


                }
                NewClear();

            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        public void WebSheetLoadEnd()
        {
            tdwMain.Eng2ThaiAllRow();

            try
            {
                if (dw_main.GetItemString(1, "district_code") != null)
                {
                    ChangeDistrict();
                }
            }
            catch { }

            this.DisConnectSQLCA();
            ////H-Code  เรื่อง Session
            TextBox1.Text = Session["membdatefix_date"].ToString();
            TextBox2.Text = Session["apply_date_tdate"].ToString();
            DwUtil.RetrieveDDDW(dw_main, "subgroup_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "tambol_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "district_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
        }

        protected void dw_main_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            newDocNo = commonService.of_getnewdocno(state.SsWsPass,state.SsCoopId, "MBAPPLDOCNO");
            dw_main.SetItemString(1, "appl_docno", newDocNo);
            dw_main.SetItemString(1, "member_no", dw_main.GetItemString(1, "member_no").Trim());
            dw_main.SetItemString(1, "coopbranch_id", state.SsCoopId);


        }

        protected void dw_main_EndUpdate(object sender, EndUpdateEventArgs e)
        {
            try
            {
                dw_data.UpdateData();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }


        }

        protected void dw_data_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {

            dw_data.SetItemString(1, "appl_docno", newDocNo);


        }

        protected void dw_data_EndUpdate(object sender, EndUpdateEventArgs e)
        {
            if (true)
            {
                sqlca.Commit();
            }
        }

        private void JsCallRetry()
        {
            try
            {

                String birth_date = dw_main.GetItemString(1, "birth_tdate"); 
                String birth_date6=WebUtil.ConvertDateThaiToEng(dw_main, "birth_tdate", null);
                String birth_date1 = WebUtil.Left(birth_date, 2);
                String birth_date2 = WebUtil.Left(birth_date, 4);
                String birth_date3 = WebUtil.Right(birth_date, 4);
                Int32 year = Convert.ToInt32(birth_date3) - 543;
                String birth_date4 = WebUtil.Right(birth_date2, 2);
                String birth_date5 = birth_date4 + "/" + birth_date1 + "/" + year.ToString();

                DateTime dt = Convert.ToDateTime(birth_date5, System.Globalization.CultureInfo.CurrentCulture);
                DateTime retry = shrlonService.of_calretrydate(state.SsWsPass, dt);
                String retry_tdate = retry.ToString("ddMMyyyy");
                dw_main.SetItemString(1, "retry_tdate", retry_tdate);
                dw_main.SetItemDate(1, "retry_date", retry);
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

        private void NewClear()
        {
            dw_main.Reset();
            dw_data.Reset();
            dw_main.InsertRow(0);
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            ////H-Code  เรื่อง Session
            DateTime membdatefix_date = Convert.ToDateTime(Session["membdatefix_date"].ToString());
            DateTime apply_date_tdate = Convert.ToDateTime(Session["apply_date_tdate"].ToString());


            dw_main.SetItemDate(1, "apply_date", apply_date_tdate);
            dw_main.SetItemDate(1, "membdatefix_date", membdatefix_date);
            DwUtil.RetrieveDDDW(dw_main, "subgroup_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "tambol_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "district_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
            
            tdwMain.Eng2ThaiAllRow();
            dw_data.InsertRow(0);
        }

        private void JsSalary()
        {

            Decimal salary_amount = dw_main.GetItemDecimal(1, "salary_amount");
            String sharetype_code = dw_data.GetItemString(1, "sharetype_code");
            Decimal share_amt = shrlonService.of_getsharemonthrate(state.SsWsPass, sharetype_code, salary_amount);
            dw_data.SetItemDecimal(1, "periodbase_value", share_amt);
            dw_data.SetItemDecimal(1, "periodshare_value", share_amt);

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

        private void Jsmembgroup_code()
        {
            String group = Hidgroup.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
         MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
    FROM MBUCFMEMBGROUP  
   WHERE length ( trim( mbucfmembgroup.membgroup_code ) ) > ( select length( max( trim( b.membgroup_control ) ) ) from mbucfmembgroup b )  AND  MBUCFMEMBGROUP.MEMBGROUP_CODE='" + group + "' ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";
            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String MEMBGROUP_DESC = dt.GetString("MEMBGROUP_DESC");
                dw_main.SetItemString(1, "membgroup_code_1", MEMBGROUP_DESC);
                String MEMBGROUP_CODE = dt.GetString("MEMBGROUP_CODE");
                dw_main.SetItemString(1, "membgroup_code", MEMBGROUP_CODE);
            }
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
            String mem_no = Hidmem_no.Value;
            Sta ta1 = new Sta(sqlca.ConnectionString);
            String sql1 = " SELECT MBMEMBMASTER.MEMBER_NO,    MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME   FROM MBMEMBMASTER WHERE ( MBMEMBMASTER.MEMBER_NO ='" + mem_no + "' )   ";
            Sdt dt1 = ta1.Query(sql1);
            if (dt1.Next())
            {
                String member_no = dt1.GetString("MEMBER_NO");
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขสมาชิกนี้     " + member_no + "   เป็นสมาชิกอยู่แล้ว");
                dw_main.SetItemString(1, "member_no", "");
            }
            Sta ta2 = new Sta(sqlca.ConnectionString);
            Sta ta = new Sta(sqlca.ConnectionString);

            String sql2 = " SELECT MBREQAPPL.MEMBER_NO   FROM MBREQAPPL  WHERE MBREQAPPL.MEMBER_NO ='" + mem_no + "' ";
            Sdt dt2 = ta2.Query(sql2);
            if (dt2.Next())
            {
                String member_no = dt2.GetString("MEMBER_NO");
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขสมาชิกนี้    " + member_no + "    รออนุมัติอยู่");
                dw_main.SetItemString(1, "member_no", "");
                String sql = "  SELECT APPL_DOCNO FROM MBREQAPPL   WHERE MEMBER_NO =  '" + member_no + "'  ";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    String APPL_DOCNO = dt.GetString("APPL_DOCNO");
                    dw_main.Retrieve(APPL_DOCNO);
                    dw_data.Retrieve(APPL_DOCNO);
                }
            }

        }
    }
}
