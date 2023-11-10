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
using System.Web.Services.Protocols;
using Saving.ConstantConfig;
namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_req_mbnew : PageWebSheet, WebSheet
    {
        String newDocNo = "";
        protected String changeDistrict;
        protected String jsSalary;
        protected String jsIdCard;
        protected String jsSalary_id;
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
        protected String jsChangmembsection;
        protected String jsGainInsertRow;
        protected String jsGainDeleteRow;
        protected String jsChangSex;
        protected String jsCheckNation;
        protected String jsChk_NameSurname;
        protected String jsPostBankBranch;
        protected String jsPostBank;
        protected String jsMoneyTrInsertRow;
        protected String jsMoneyTrDeleteRow;
        protected String jsGetMateDistrict;
        protected String jsGetMatePostcode;
        protected String jsRefreshExpense;
        protected String jsMembdatefix;
        protected String jsExpenseBank;
        protected String jsMateCard;
        protected String jsMateSalary;
        protected String jsChkperiodshare_value;

        protected int statusPerson = 1 ;

        public void InitJsPostBack()
        {
            jsGetdocno = WebUtil.JsPostBack(this, "jsGetdocno");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            jsSalary = WebUtil.JsPostBack(this, "jsSalary");
            jsIdCard = WebUtil.JsPostBack(this, "jsIdCard");
            jsSalary_id = WebUtil.JsPostBack(this, "jsSalary_id");
            jsCallRetry = WebUtil.JsPostBack(this, "jsCallRetry");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsGetPostcode = WebUtil.JsPostBack(this, "jsGetPostcode");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsChangmembsection = WebUtil.JsPostBack(this, "jsChangmembsection");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsChanegValue = WebUtil.JsPostBack(this, "jsChanegValue");
            jsMemberNo = WebUtil.JsPostBack(this, "jsMemberNo");
            newclear = WebUtil.JsPostBack(this, "newclear");
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
            jsChk_NameSurname = WebUtil.JsPostBack(this, "jsChk_NameSurname");
            jsPostBankBranch = WebUtil.JsPostBack(this, "jsPostBankBranch");
            jsPostBank = WebUtil.JsPostBack(this, "jsPostBank");
            jsMoneyTrInsertRow = WebUtil.JsPostBack(this, "jsMoneyTrInsertRow");
            jsMoneyTrDeleteRow = WebUtil.JsPostBack(this, "jsMoneyTrDeleteRow");

            jsGetMateDistrict = WebUtil.JsPostBack(this, "jsGetMateDistrict");
            jsGetMatePostcode = WebUtil.JsPostBack(this, "jsGetMatePostcode");
            jsRefreshExpense = WebUtil.JsPostBack(this, "jsRefreshExpense");
            jsMembdatefix = WebUtil.JsPostBack(this, "jsMembdatefix");
            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsMateCard = WebUtil.JsPostBack(this, "jsMateCard");
            jsMateSalary = WebUtil.JsPostBack(this, "jsMateSalary");
            jsChkperiodshare_value = WebUtil.JsPostBack(this, "jsChkperiodshare_value");

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
            dw_gain.SetTransaction(sqlca);
            dw_moneytr.SetTransaction(sqlca);
            if (IsPostBack)
            {

                this.RestoreContextDw(dw_main);                
                this.RestoreContextDw(dw_moneytr);
                //this.RestoreContextDw(dw_gain);
                HdIsPostBack.Value = "true";
            }
            else
            {
                //ดึงลำดับเลขที่สมาชิกท้ายสุด+1 มาตั้งค่าให้ฟิลส์เลขสมาชิก
                //string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
                NewClear();
                HdIsPostBack.Value = "false";
            }
            //ที่ tks ไม่ได้ใช้ก็เลยซ่อนไว้ก่อน
            if (state.SsCoopControl == "010001")
            {
                dw_gain.Visible = false;
            }

            //if (dw_gain.RowCount <= 0)
            //{
            //    dw_gain.InsertRow(0);
            //    dw_gain.SetItemDecimal(dw_gain.RowCount, "seq_no", dw_gain.RowCount);
            //}
            //if (dw_moneytr.RowCount <= 0)
            //{
            //    dw_moneytr.InsertRow(0);
            //} 
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
            else if (eventArg == "jsSalary_id")
            {
                JsSalary_id();

            }
            else if (eventArg == "jsChk_NameSurname")
            {
                JsChk_NameSurname();
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
            else if (eventArg == "jsGetMateDistrict")
            {
                JsGetMateDistrict();
            }
            else if (eventArg == "jsGetMatePostcode")
            {
                JsGetMatePostcode();
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
                //Image1.ImageUrl = imageUrl;

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

            else if (eventArg == "jsPostBank")
            {
                JsPostBank();
            }
            else if (eventArg == "jsPostBankBranch")
            {
                JsPostBankBranch();
            }
            else if (eventArg == "jsMoneyTrInsertRow")
            {
                JsMoneyTrInsertRow();
            }
            else if (eventArg == "jsMoneyTrDeleteRow")
            {
                JsMoneyTrDeleteRow();
            }
            else if (eventArg == "jsRefreshExpense")
            {
                JsRefreshExpense();
            }
            else if (eventArg == "jsMembdatefix")
            {
                JsMembdatefix();
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();
            }
            else if (eventArg == "jsMateCard")
            {
                JsMateCard();
            }
            else if (eventArg == "jsMateSalary")
            {
                JsMateSalary();
            }
            else if( eventArg == "jsChkperiodshare_value") 
            {
                Chkperiodshare_value();
            }
        }

        private void JsMembdatefix()
        {
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "membdatefix_flag");
            }
            catch
            {

            }
            if (i == 1)
            {
                dw_main.SetItemDateTime(1, "membdatefix_date", state.SsWorkDate);
            }
            else
            {
                dw_main.SetItemNull(1, "membdatefix_date");
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

        private void JsMoneyTrDeleteRow()
        {
            int row = Convert.ToInt32(HdChkRowDel.Value);
            dw_moneytr.DeleteRow(row);
            for (int i = 1; i <= dw_gain.RowCount; i++)
            {
                dw_moneytr.SetItemDecimal(i, "seq_no", i);
            }
        }

        private void JsMoneyTrInsertRow()
        {
            dw_moneytr.InsertRow(0);
            dw_moneytr.SetItemDecimal(dw_moneytr.RowCount, "seq_no", dw_moneytr.RowCount);
            //DwUtil.RetrieveDDDW(dw_gain, "gain_relation", "sl_member_new.pbl", null);
        }

        private void JsGetdocno()
        {
            String memcoop_id = state.SsCoopId;

            String doc_no = Hdoc_no.Value;
            dw_main.Retrieve(memcoop_id, doc_no);
            dw_moneytr.Retrieve(memcoop_id, doc_no);
            dw_gain.Retrieve(memcoop_id, doc_no);
            //JsSalary();
            ChangeDistrict();
            JsGetPostcode();
            JsGetCurrDistrict();
            JsGetCurrPostcode();
            JsExpenseBank();
            
            tdwMain.Eng2ThaiAllRow();
            JsCallRetry();
        }

        public void SaveWebSheet()
        {

            String memcoop_id = state.SsCoopControl;

            try
            {
                string salary_id = "";
                try
                {
                    salary_id = dw_main.GetItemString(1, "salary_id");
                }
                catch { dw_main.SetItemString(1, "salary_id", ""); }
                str_mbreqnew astr_mbreqnew = new str_mbreqnew();
                astr_mbreqnew.xml_mbdetail = dw_main.Describe("DataWindow.Data.XML");
                //astr_mbreqnew.xml_mbshare = dw_data.Describe("DataWindow.Data.XML");
                astr_mbreqnew.xml_mbgain = dw_gain.Describe("DataWindow.Data.XML");
                astr_mbreqnew.xml_mbmoneytr = dw_moneytr.Describe("DataWindow.Data.XML");
                astr_mbreqnew.entry_id = state.SsUsername;
                int result = shrlonService.of_savereq_mbnew(state.SsWsPass, ref astr_mbreqnew);
                if (result == 1)
                {
                    //if (dw_gain.RowCount > 0)
                    //{
                    //    string docno = "";
                    //    try
                    //    {
                    //        docno = dw_main.GetItemString(1, "appl_docno");
                    //    }
                    //    catch
                    //    {
                    //        DataTable dt = WebUtil.Query(@"SELECT * FROM CMDOCUMENTCONTROL  WHERE COOP_ID = '" + memcoop_id + "' and DOCUMENT_CODE ='MBAPPLDOCNO'");
                    //        docno = dt.Rows[0]["last_documentno"].ToString();
                    //        docno = WebUtil.Right("0000000000" + docno, 10);
                    //    }

                    //    DateTime birth_date = dw_main.GetItemDateTime(1, "birth_date");
                    //    decimal age = DateTime.Today.Year - birth_date.Year;
                    //    try
                    //    {
                    //        for (int k = 1; k <= dw_gain.RowCount; k++)
                    //        {
                    //            dw_gain.SetItemString(k, "coop_id", state.SsCoopId);
                    //            dw_gain.SetItemDateTime(k, "write_date", state.SsWorkDate);
                    //            dw_gain.SetItemString(k, "write_at", state.SsCoopName);
                    //            dw_gain.SetItemString(k, "appl_docno", docno);
                    //            dw_gain.SetItemString(k, "age", age.ToString());
                    //        }

                    //        dw_gain.UpdateData();
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
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
            dw_main.Modify(" t_age.text = '" + Hdtextage.Value + "'");
            tdwMain.Eng2ThaiAllRow();
            this.dw_main.SaveDataCache();            
            this.dw_moneytr.SaveDataCache();
            //this.dw_gain.SaveDataCache();
        }

        private void JsRunMemberNo()
        {
            //ตรวจสอบว่าเลือกกำหนดเลขสมาชิกเองหรือไม่ ?
            decimal i = dw_main.GetItemDecimal(1, "memnofix_flag");
            if (i == 0)
            {
                //string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
                // dw_main.SetItemString(1, "member_no", max_memberno);
                //dw_main.SetItemString(1, "member_no", "AUTO");
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

            try
            {
                tdwMain.Thai2EngAllRow();
                DateTime birth_date = dw_main.GetItemDate(1, "birth_date");

                string birth_tdate = birth_date.ToString("ddMMyyyy", WebUtil.EN);
                string ls_age;
                decimal age = 0;
                DateTime retry_date = state.SsWorkDate;
                //to_date('" + birth_tdate + "','ddMMyyyy')
                string sql = @"select ft_retrydate( {0} , {1} ) as retry, ftcm_calagemth( {2}, {3} ) as age from dual";

                sql = WebUtil.SQLFormat(sql, state.SsCoopId, birth_date, birth_date, state.SsWorkDate);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    retry_date = dt.GetDate("retry");
                    age = dt.GetDecimal("age");
                }
                
                dw_main.SetItemDate(1, "retry_date", retry_date);
                ls_age = age.ToString();
                if (ls_age.Substring(ls_age.Length - 2, 2) == "00")
                {
                    ls_age = ls_age.Substring(0, 2) + " ปี " ;
                }
                else
                {
                    ls_age = ls_age.Replace(".", " ปี ") + " เดือน";
                }
                dw_main.Modify(" t_age.text = '" + ls_age + "'" );
                Hdtextage.Value = ls_age;
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
                    //String setting = dw_data.Describe("recv_shrstatus.Protect");
                    //dw_data.Modify("recv_shrstatus.Protect=1");

                    // dw_data.Modify("recv_shrstatus.Protect='1~tIf(IsRowNew(),0,1)'");
                    //String setting1 = dw_data.Describe("recv_shrstatus.Protect");

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
                    dw_main.SetItemNull(1, "card_person");
                    this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ครบ 13 หลัก')");
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

                            //string checkcard = wcf.InterPreter.CheckCardPerson(state.SsConnectionIndex, memcoop_id, PID);
                            string checkcard = "";
                            if(memcoop_id == "010001")  // ใช้ที่ ธกส.
                            {
                                checkcard = CheckPerson(memcoop_id, PID, "");
                            }
                            else if (memcoop_id == "008001") // ใช้ที่ กฟภ.
                            {
                                checkcard = CheckPerson2(memcoop_id, PID, "");
                            }
                            else {
                                checkcard = CheckPerson2(memcoop_id, PID, "");
                            }
                            

                            //Sta ta1 = new Sta(sqlca.ConnectionString);
                            //String sql1 = " SELECT MBMEMBMASTER.MEMBER_NO,    MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME  ,MBMEMBMASTER.CARD_PERSON FROM MBMEMBMASTER WHERE ( MBMEMBMASTER.CARD_PERSON ='" + PID + "' )  AND MBMEMBMASTER.RESIGN_STATUS=0 and MBMEMBMASTER.COOP_ID ='" + memcoop_id + "' ";
                            //Sdt dt1 = ta1.Query(sql1);
                            if (checkcard == "")
                            {
                                dw_main.SetItemString(1, "card_person", PID);

                            }
                            else
                            {   //aek รอถาม ให้สามารถบันทึกเลขบัตรประชาชนที่ซ้ำได้
                                    dw_main.SetItemString(1, "card_person", PID);
                                this.SetOnLoadedScript("alert('" + checkcard + "')");
                                LtServerMessage.Text = WebUtil.ErrorMessage(checkcard);
                            }
                            if (this.statusPerson == 0)
                            {
                                dw_main.SetItemString(1, "card_person", "");
                            }
                            else if (this.statusPerson == 1)
                            {
                                dw_main.SetItemString(1, "card_person", PID);
                            }
                        }
                        else
                        {
                            dw_main.SetItemNull(1, "card_person");
                            this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ถูกต้อง')");
                            LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง");
                        }
                        
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }
        }

        private void JsSalary_id()
        {
            String memcoop_id = state.SsCoopControl;
            try
            {
                String salary_id = dw_main.GetItemString(1, "salary_id");
                salary_id = salary_id.Trim();
                salary_id = string.Format("{0:000000000000000}", salary_id);

                if (salary_id != null)
                {
                    string checksalary = "";
                    if (memcoop_id == "010001")  // ใช้ที่ ธกส.
                    {
                        checksalary = CheckPerson(memcoop_id, "", salary_id);
                    }
                    else if (memcoop_id == "008001") // ใช้ที่ กฟภ.
                    {
                        checksalary = CheckPerson2(memcoop_id, "", salary_id);
                    }
                    else {
                        checksalary = CheckPerson2(memcoop_id, "", salary_id);
                    }

                    if (checksalary == "")
                    {
                        dw_main.SetItemString(1, "salary_id", salary_id);
                    }
                    else
                    {   //aek รอถาม ให้สามารถบันทึกเลขพนักงานที่ซ้ำได้
                        this.SetOnLoadedScript("alert('" + checksalary + "')");
                        dw_main.SetItemString(1, "salary_id", salary_id);
                        LtServerMessage.Text = WebUtil.ErrorMessage(checksalary);
                    }
                    if (this.statusPerson == 0)
                    {
                        dw_main.SetItemString(1, "salary_id", "");
                    }
                    else if (this.statusPerson == 1)
                    {
                        dw_main.SetItemString(1, "salary_id", salary_id);
                    }
                    //Sta taChk = new Sta(sqlca.ConnectionString);
                    //String sqlChk = "SELECT MBMEMBMASTER.SALARY_ID FROM MBMEMBMASTER WHERE trim(SALARY_ID) = '" + salary_id + "'";
                    //Sdt dtChk = taChk.Query(sqlChk);
                    //if (dtChk.Next())
                    //{
                    //    HdChkSalaryId.Value = "เลขพนักงาน " + salary_id + " เป็นสมาชิกสหกรณ์แล้ว : กรุณากรอกใหม่อีกครั้ง";
                    //    LtServerMessage.Text = WebUtil.ErrorMessage(HdChkSalaryId.Value);
                    //    dw_main.SetItemString(1, "salary_id", "");
                    //}
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void NewClear()
        {
            dw_main.Reset();
            dw_gain.Reset();
            dw_moneytr.Reset();
            dw_main.InsertRow(0);
            dw_moneytr.InsertRow(0);
            dw_moneytr.InsertRow(0);
            dw_moneytr.InsertRow(0);
            H_periodshare_value.Value = "";

            DwUtil.RetrieveDDDW(dw_moneytr, "trtype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_moneytr, "moneytype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "appltype_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "membgroup_code_1", "sl_member_new.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_main, "prename_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "province_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "currprovince_code", "sl_member_new.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "mateprovince_code", "sl_member_new.pbl", null);
            
            ////H-Code  เรื่อง Session
            //DateTime membdatefix_date = Convert.ToDateTime(Session["membdatefix_date"].ToString());
            //DateTime apply_date_tdate = Convert.ToDateTime(Session["apply_date_tdate"].ToString());
            // string max_memberno = wcf.InterPreter.GetAutoRunMemberNo();
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            dw_main.SetItemString(1, "member_no", "AUTO");
            dw_main.SetItemDate(1, "work_date", state.SsWorkDate);
            dw_main.SetItemString(1, "nationality", "ไทย");
            dw_main.SetItemDate(1, "apply_date", state.SsWorkDate);
            //DateTime process_date = WebUtil.GetProcessDate(state.SsCoopControl, state.SsWorkDate.Year, state.SsWorkDate.Month);
            dw_main.SetItemString(1, "membdatefix_tdate", "");
            //dw_main.SetItemDecimal(1, "registype_flag", 1);
            dw_main.SetItemString(1, "position_desc", "ไม่ระบุ");
            //dw_main.SetItemDecimal(1, "membtype_code_1", "ฮฮ");

            dw_moneytr.SetItemString(1, "trtype_code", "DVAV1");
            dw_moneytr.SetItemString(2, "trtype_code", "KEEP1");
            dw_moneytr.SetItemString(3, "trtype_code", "KEEP2");

            tdwMain.Eng2ThaiAllRow();
        }

        private void JsSalary()
        {
            String memcoop_id = state.SsCoopControl;

            Decimal member_type = dw_main.GetItemDecimal(1, "member_type");
            Decimal salary_amount , incomeetc_amt;
            try{salary_amount   = dw_main.GetItemDecimal(1, "salary_amount");}
            catch{salary_amount = 0;}
            try{incomeetc_amt = dw_main.GetItemDecimal(1, "incomeetc_amt");}
            catch{ incomeetc_amt = 0;} 
            Decimal total = salary_amount + incomeetc_amt;
            String sharetype_code = "01"; //dw_data.GetItemString(1, "sharetype_code");

            //Decimal[] minmaxshare = wcf.InterPreter.CalPayShareMonth(state.SsConnectionIndex, memcoop_id, sharetype_code, member_type, salary_amount);//shrlonService.GetShareMonthRate(state.SsWsPass, sharetype_code, salary_amount);

            Decimal minmaxshare = CalPayShareMonth(memcoop_id, sharetype_code, member_type, total);

            dw_main.SetItemDecimal(1, "periodbase_value", minmaxshare);
            dw_main.SetItemDecimal(1, "periodshare_value", minmaxshare);
            H_periodbase_value.Value = Convert.ToString(minmaxshare);
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

        private void JsGetMateDistrict()
        {
            try
            {
                DataWindowChild childdis = dw_main.GetChild("mateamphur_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String mateprovincecode = dw_main.GetItemString(1, "mateprovince_code");
                childdis.SetFilter("province_code='" + mateprovincecode + "'");
                childdis.Filter();
            }
            catch
            {
            }
        }

        private void JsGetMatePostcode()
        {
            try
            {
                DataWindowChild child = dw_main.GetChild("matetambol_code");
                child.SetTransaction(sqlca);              
                child.Retrieve();
                String mateamphur_code = dw_main.GetItemString(1, "mateamphur_code");
                child.SetFilter("DISTRICT_CODE='" + mateamphur_code + "'");
                child.Filter();

                String mateprovince_code = dw_main.GetItemString(1, "mateprovince_code");
                // String district_code = dw_main.GetItemString(1, "district_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                                and ( MBUCFDISTRICT.PROVINCE_CODE ='" + mateprovince_code + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + mateamphur_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    dw_main.SetItemString(1, "mateaddr_postcode", dt.Rows[0]["postcode"].ToString());
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

        private void JsChk_NameSurname()
        {
            try
            {
                String m_name = dw_main.GetItemString(1, "memb_name");
                String m_surname = dw_main.GetItemString(1, "memb_surname");
                m_name = m_name.Trim();
                m_surname = m_surname.Trim();
                if (m_surname != null && m_name != null)
                {
                    Sta taChk = new Sta(sqlca.ConnectionString);
                    String sqlChk = "SELECT MBMEMBMASTER.MEMBER_NO FROM MBMEMBMASTER WHERE MEMB_NAME = '" + m_name + "' AND MEMB_SURNAME = '" + m_surname + "' ";
                    Sdt dtChk = taChk.Query(sqlChk);
                    if (dtChk.Next())
                    {
                        HdChkRepteName.Value = "ชื่อ " + m_name + " " + m_surname + "  เป็นสมาชิกสหกรณ์แล้ว : กรุณากรอกใหม่อีกครั้ง";
                    }
                }
                else
                {
                    HdChkRepteName.Value = ": กรุณากรอก ชื่อ-สกุล ให้สมบูรณ์";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void JsChangmembsection()
        {
            string group_control = dw_main.GetItemString(1, "membgroup_code");
            DwUtil.RetrieveDDDW(dw_main, "membsection_code_1", "sl_member_new.pbl", state.SsCoopControl, group_control);
        }

        private void JsChanegValue()
        {
            LtServerMessage.Text = WebUtil.ErrorMessage("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
            dw_main.SetItemDecimal(1, "periodshare_value", 0);
            Decimal periodbase_value = dw_main.GetItemDecimal(1, "periodbase_value");
            Decimal periodshare_value = dw_main.GetItemDecimal(1, "periodshare_value");
        }

        private void JsMemberNo()
        {
            Sta ta2 = new Sta(sqlca.ConnectionString);
            Sta ta = new Sta(sqlca.ConnectionString);
            String mem_no = WebUtil.MemberNoFormat(Hidmem_no.Value);
            String coop_id = state.SsCoopId;

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
                    //dw_data.Retrieve(state.SsCoopId, APPL_DOCNO);
                    tdwMain.Eng2ThaiAllRow();
                }
            }

            String sqlshare = "  SELECT  PERIODSHARE_VALUE,  PERIODBASE_VALUE  FROM MBREQAPPL   WHERE ( APPL_DOCNO ='" + APPL_DOCNO + "' ) and MBREQAPPLSHARE.COOP_ID ='" + coop_id + "'  ";
            Sdt dtshare = ta.Query(sqlshare);
            if (dtshare.Next())
            {
                Decimal PERIODSHARE_VALUE = dtshare.GetDecimal("PERIODSHARE_VALUE");
                Decimal PERIODBASE_VALUE = dtshare.GetDecimal("PERIODBASE_VALUE");
                dw_main.SetItemDecimal(1, "periodshare_value", PERIODSHARE_VALUE);
                dw_main.SetItemDecimal(1, "periodbase_value", PERIODBASE_VALUE);
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
                dw_main.SetItemString(1, "currtambol_code", tambol_code.Trim());
            }
            catch { }
            try
            {
                district_code = dw_main.GetItemString(1, "district_code");
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

        void JsPostBankBranch()
        {
            int row = Convert.ToInt16(HdRow.Value);
            String BankCode = HdBankCode.Value.ToString().Trim();
            String BankBranchId = HdBankBranchCode.Value.ToString().Trim();

            Sta ta = new Sta(state.SsConnectionString);
            String sql = "";
            sql = @"SELECT bank_desc
                    FROM cmucfbank  
                    WHERE ( bank_code = '" + BankCode + @"')";
            Sdt dt = ta.Query(sql);
            dw_moneytr.SetItemString(row, "bank_desc", dt.Rows[0]["bank_desc"].ToString());
            dw_moneytr.SetItemString(row, "bank_code", BankCode);
            sql = @"SELECT branch_name
                    FROM cmucfbankbranch  
                    WHERE ( bank_code = '" + BankCode + @"' ) AND ( branch_id = '" + BankBranchId + @"' )";
            dt = ta.Query(sql);
            dw_moneytr.SetItemString(row, "branch_name", dt.Rows[0]["branch_name"].ToString());
            dw_moneytr.SetItemString(row, "bank_branch", BankBranchId);

            ta.Close();
        }

        void JsPostBank()
        {
            int row = Convert.ToInt16(HdRow.Value);
            String BankCode = HdBankCode.Value.ToString().Trim();

            //String BankCode = dw_moneytr.GetItemString(1, "bank_code");
            //String BankBranchId = dw_moneytr.GetItemString(1, "bank_branch");
            Sta ta = new Sta(state.SsConnectionString);
            String sql = "";
            sql = @"SELECT bank_desc
                    FROM cmucfbank  
                    WHERE ( bank_code = '" + BankCode + @"')";
            Sdt dt = ta.Query(sql);

            dw_moneytr.SetItemString(row, "bank_desc", dt.Rows[0]["bank_desc"].ToString());
            dw_moneytr.SetItemString(row, "bank_code", BankCode);
            try
            {
                dw_moneytr.SetItemString(row, "branch_name", "");
                dw_moneytr.SetItemString(row, "bank_branch", "");
            }
            catch
            { }
            ta.Close();
        }

        void JsExpenseBank()
        {
            String Expensebank = Hidexpbank.Value.ToString().Trim();
            Sta ta = new Sta(sqlca.ConnectionString);

            String sql = @"SELECT BANK_CODE, BANK_DESC
                    FROM cmucfbank  
                    WHERE ( bank_code = '" + Expensebank + @"')";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String BANK_DESC = dt.GetString("BANK_DESC");
                dw_main.SetItemString(1, "expense_bank_1", BANK_DESC);
                String BANK_CODE = dt.GetString("BANK_CODE");
                dw_main.SetItemString(1, "expense_bank", BANK_CODE);

            }
            try
            {
                string expense_bank = dw_main.GetItemString(1, "expense_bank");
                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_member_new.pbl", expense_bank);
            }
            catch { }
        }

        private void JsRefreshExpense()
        {
            string exp_code = "";
            try
            {
                exp_code = dw_main.GetItemString(1, "expense_code");
            }
            catch
            {
            }
            if (exp_code == "CSH" || exp_code == "TRN")
            {
                dw_main.SetItemString(1, "expense_bank_1", "");
                dw_main.SetItemString(1, "expense_bank", "");
                dw_main.SetItemString(1, "expense_branch_1", "");
                dw_main.SetItemString(1, "expense_branch", "");
                dw_main.SetItemString(1, "expense_accid", "");
            }
            else if (exp_code == "CBT")
            {
            }
        }

        private void JsMateCard()
        {
            String mate_cardperson = dw_main.GetItemString(1, "mate_cardperson");
            if (mate_cardperson.Length != 13)
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
                    pidchk = (Convert.ToInt32(mate_cardperson.Substring(0, 1)) * 13) + (Convert.ToInt32(mate_cardperson.Substring(1, 1)) * 12) + (Convert.ToInt32(mate_cardperson.Substring(2, 1)) * 11) + (Convert.ToInt32(mate_cardperson.Substring(3, 1)) * 10) + (Convert.ToInt32(mate_cardperson.Substring(4, 1)) * 9) + (Convert.ToInt32(mate_cardperson.Substring(5, 1)) * 8) + (Convert.ToInt32(mate_cardperson.Substring(6, 1)) * 7) + (Convert.ToInt32(mate_cardperson.Substring(7, 1)) * 6) + (Convert.ToInt32(mate_cardperson.Substring(8, 1)) * 5) + (Convert.ToInt32(mate_cardperson.Substring(9, 1)) * 4) + (Convert.ToInt32(mate_cardperson.Substring(10, 1)) * 3) + (Convert.ToInt32(mate_cardperson.Substring(11, 1)) * 2);

                    dig = pidchk % 11;
                    fdig = 11 - dig;
                    lasttext = fdig.ToString();
                    if (mate_cardperson.Substring(12, 1) == WebUtil.Right(lasttext, 1))
                    {
                        String memcoop_id = state.SsCoopControl;

                        try
                        {
                            Sta taChk = new Sta(sqlca.ConnectionString);
                            String sqlChk = "SELECT MBMEMBMASTER.salary_id FROM MBMEMBMASTER WHERE card_person = '" + mate_cardperson + "'";
                            Sdt dtChk = taChk.Query(sqlChk);
                            if (dtChk.Next())
                            {
                                String salary_id = dtChk.GetString("salary_id");
                                dw_main.SetItemString(1, "mate_salaryid", salary_id);
                            }
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง"); }
                }
                catch { }
            }
        }

        private void JsMateSalary()
        {
            String mate_salaryid = dw_main.GetItemString(1, "mate_salaryid");
            if (mate_salaryid == "")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขพนักงาน");
            }
            mate_salaryid = mate_salaryid.Trim();
            try
            {
                Sta taChk = new Sta(sqlca.ConnectionString);
                String sqlChk = "SELECT MBMEMBMASTER.card_person FROM MBMEMBMASTER WHERE trim(salary_id) = '" + mate_salaryid + "'";
                Sdt dtChk = taChk.Query(sqlChk);
                if (dtChk.Next())
                {
                    String card_person = dtChk.GetString("card_person");
                    dw_main.SetItemString(1, "mate_cardperson", card_person);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void Chkperiodshare_value()
        {
            decimal periodbase_value = dw_main.GetItemDecimal(1, "periodbase_value");
            decimal salary_amount = dw_main.GetItemDecimal(1, "salary_amount");
            decimal periodshare_value = dw_main.GetItemDecimal(1, "periodshare_value");
            //decimal Hd_periodbase_value = Convert.ToDecimal(H_periodbase_value.Value);
           if (periodshare_value >= salary_amount )
           {
               dw_main.SetItemDecimal(1, "periodshare_value", periodbase_value);
               Response.Write("<script language='javascript'>alert('มูลค่าหุ้น/เดือน มากกว่าหรือเท่ากับ เงินเดือน'\n );</script>");
           }
           else if( periodshare_value < periodbase_value)
           {
               dw_main.SetItemDecimal(1, "periodshare_value", periodbase_value);
               Response.Write("<script language='javascript'>alert('มูลค่าหุ้น/เดือน น้อยกว่า หุ้นตามฐาน เงินเดือน'\n );</script>");
           }
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

        //เช็คการเป็นสมาชิก TKS
        private string CheckPerson(String memcoop_id, String cardperson, String salary_id)
        {
            String result = "";

            String sql1 = "", sql2 = "", sql3 = "", sql4 = "";
            String txtID;
            this.statusPerson = 1; // เชคแล้วอนุญาต

            if (cardperson != "")
            {
                sql1 = "select max(resign_date) as resigndate , count(member_no) as rowcount from mbmembmaster where coop_id ='" + memcoop_id + "' and CARD_PERSON ='" + cardperson + "' and resign_status = 1 order by resign_date";
                sql2 = "select * from mbreqappl where coop_id='" + memcoop_id + "' and appl_status ='8' and card_person ='" + cardperson + "'";
                sql3 = "select member_no from mbmembmaster where coop_id ='" + memcoop_id + "' and CARD_PERSON ='" + cardperson + "' and resign_status = 0";
                sql4 = "select * from mbmembmaster where coop_id = '010001' and member_type = 2 and card_person = '" + cardperson + "'";
            txtID = cardperson;
            }
            else
            {
                sql1 = "select max(resign_date) as resigndate , count(member_no) as rowcount from mbmembmaster where coop_id ='" + memcoop_id + "' and trim(salary_id) ='" + salary_id + "' and resign_status = 1 order by resign_date";
                sql2 = "select * from mbreqappl where coop_id='" + memcoop_id + "' and appl_status ='8' and trim(salary_id) ='" + salary_id + "'";
                sql3 = "select member_no from mbmembmaster where coop_id ='" + memcoop_id + "' and trim(salary_id) ='" + salary_id + "' and resign_status = 0";
                sql4 = "select * from mbmembmaster where coop_id = '010001' and member_type = 2 and trim(salary_id) = '" + salary_id + "'";
                txtID = salary_id;
            }
            
            if (cardperson != "") { }
            DataLibrary.Sdt dt = WebUtil.QuerySdt(sql1);
            DataLibrary.Sdt dt2 = WebUtil.QuerySdt(sql2);
            DataLibrary.Sdt dt3 = WebUtil.QuerySdt(sql3);
            DataLibrary.Sdt dt4 = WebUtil.QuerySdt(sql4);

            if (dt2.Next())
            {
                result = "สมาชิก" + " " + cardperson + " มีคำขอรออนุมัติอยู่" + "กด F8 เพื่อแก้ไขเอกสารเก่า";
            }
            else if (dt3.Next())
            {
                result = "สมาชิก" + " " + cardperson + " เป็นสมาชิกสหกรณ์อยู่เเล้ว" + "ทะเบียน " + dt3.GetString("member_no");
            }
            else if (dt.Next())
            {
                DateTime resigndate = dt.GetDate("resigndate");
                Decimal rowcount = dt.GetDecimal("rowcount");

                TimeSpan diff1 = DateTime.Now.Subtract(resigndate);
                Double TotalYear = diff1.TotalDays / 356;
                String DispTotalYear = String.Format("{0:F2}", TotalYear);
                if (dt4.Next())
                {
                    if (TotalYear <= 2)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมายังไม่ถึง 2 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else if ((rowcount >= 1) && (rowcount < 2))
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมาแล้วเกิน 1 ครั้ง \n" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else if ((rowcount > 2))
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมาแล้วเกิน 2 ครั้ง \n" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else
                    {
                        result = "";
                    }
                }
                else
                {
                    if (TotalYear <= 1)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมายังไม่ถึง 1 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else if (rowcount > 1)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมากกว่า 1 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                }
            }
            else
            {
                result = "";
            }
            return result;
        }

        //เช๋คการเป็นสมาชิก PEA
        private string CheckPerson2(String memcoop_id, String cardperson, String salary_id)
        {
            String result = "";

            String sql1 = "", sql2 = "", sql3 = "", sql4 = "";
            String txtID;
            this.statusPerson = 1; // เชคแล้วอนุญาต

            if (cardperson != "")
            {
                sql1 = "select max(resign_date) as resigndate , count(member_no) as rowcount from mbmembmaster where coop_id ='" + memcoop_id + "' and CARD_PERSON ='" + cardperson + "' and resign_status = 1 order by resign_date";
                sql2 = "select * from mbreqappl where coop_id='" + memcoop_id + "' and appl_status ='8' and card_person ='" + cardperson + "'";
                sql3 = "select member_no from mbmembmaster where coop_id ='" + memcoop_id + "' and CARD_PERSON ='" + cardperson + "' and resign_status = 0";
                sql4 = "select * from mbmembmaster where coop_id = '" + memcoop_id + "' and member_type = 2 and card_person = '" + cardperson + "'";
                txtID = cardperson;
            }
            else
            {
                sql1 = "select max(resign_date) as resigndate , count(member_no) as rowcount from mbmembmaster where coop_id ='" + memcoop_id + "' and trim(salary_id) ='" + salary_id + "' and resign_status = 1 order by resign_date";
                sql2 = "select * from mbreqappl where coop_id='" + memcoop_id + "' and appl_status ='8' and trim(salary_id) ='" + salary_id + "'";
                sql3 = "select member_no from mbmembmaster where coop_id ='" + memcoop_id + "' and trim(salary_id) ='" + salary_id + "' and resign_status = 0";
                sql4 = "select * from mbmembmaster where coop_id = '" + memcoop_id + "' and member_type = 2 and trim(salary_id) = '" + salary_id + "'";
                txtID = salary_id;
            }

            if (cardperson != "") { }
            DataLibrary.Sdt dt = WebUtil.QuerySdt(sql1);
            DataLibrary.Sdt dt2 = WebUtil.QuerySdt(sql2);
            DataLibrary.Sdt dt3 = WebUtil.QuerySdt(sql3);
            DataLibrary.Sdt dt4 = WebUtil.QuerySdt(sql4);


            if (dt2.Next())
            {
                result = "สมาชิก" + " " + cardperson + " มีคำขอรออนุมัติอยู่" + "กด F8 เพื่อแก้ไขเอกสารเก่า";
            }
            else if (dt3.Next())
            {
                result = "สมาชิก" + " " + cardperson + " เป็นสมาชิกสหกรณ์อยู่เเล้ว" + "ทะเบียน " + dt3.GetString("member_no");
            }
            else if (dt.Next())
            {
                DateTime resigndate = dt.GetDate("resigndate");
                Decimal rowcount = dt.GetDecimal("rowcount");

                TimeSpan diff1 = DateTime.Now.Subtract(resigndate);
                Double TotalYear = diff1.TotalDays / 356;
                String DispTotalYear = String.Format("{0:F2}", TotalYear);
                if (dt4.Next())
                {
                    
                    if (TotalYear <= 2)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมายังไม่ถึง 2 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else if ((rowcount >= 1) && (rowcount < 2))
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมาแล้วเกิน 1 ครั้ง \n" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else if ((rowcount > 2))
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมาแล้วเกิน 2 ครั้ง \n" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                    else
                    {
                        result = "";
                    }
                }
                else
                {
                    if (TotalYear <= 1)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมายังไม่ถึง 1 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                        this.statusPerson = 0; // เชคแล้วไม่อนุญาต
                    }
                    else if (rowcount > 1)
                    {
                        result = "สมาชิก" + " " + txtID + " ลาออกมากกว่า 1 ปี" + "ลาออกครั้งล่าสุดเมื่อ วันที่ " + resigndate.ToShortDateString() + " เป็นวันเวลา " + DispTotalYear + "" + " ปี";
                    }
                }
            }
            else
            {
                result = "";
            }
            return result;
        }
    }
}
