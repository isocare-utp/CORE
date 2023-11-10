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
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbsrv_req_chggrp : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        public String pbl = "mb_req_chggroup.pbl";
        private DwThDate tdwmain;
        protected String postNewClear;
        protected String postRefresh;
        protected String postInit;
        protected String postFilterSection;
        protected String postInitMember;
        protected String postSetNewGroup;
        protected String postSetDocno;
        protected String postAddRow;
        protected String postClearText;
        protected String postSalaryId;
        protected String postExpenseCode;
        protected String postApv;
        protected String postExpenseBank;
        String salary_id = "";
        //===================================
        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postFilterSection = WebUtil.JsPostBack(this, "postFilterSection");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
            postSetNewGroup = WebUtil.JsPostBack(this, "postSetNewGroup");
            postSetDocno = WebUtil.JsPostBack(this, "postSetDocno");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");
            postClearText = WebUtil.JsPostBack(this, "postClearText");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
            postExpenseCode = WebUtil.JsPostBack(this, "postExpenseCode");
            postApv = WebUtil.JsPostBack(this, "postApv");
            postExpenseBank = WebUtil.JsPostBack(this, "postExpenseBank");
            //======================
            DwUtil.RetrieveDDDW(Dw_main, "expense_bank_1", "mb_req_chggroup.pbl", null);            

            tdwmain = new DwThDate(Dw_main, this);

            tdwmain.Add("req_date", "req_tdate");
            tdwmain.Add("entry_date", "entry_tdate");
            //======================
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
            }

            try
            {
                this.ConnectSQLCA();

                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                    this.RestoreContextDw(Dw_history);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postRefresh")
            {

            }
            else if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postFilterSection")
            {
                JspostFilterSection();
            }
            else if (eventArg == "postInitMember")
            {
                JspostInitMember();
            }
            else if (eventArg == "postSetNewGroup")
            {
                JspostSetNewGroup();
            }
            else if (eventArg == "postSetDocno")
            {
                JspostSetDocno();
            }
            else if (eventArg == "postAddRow")
            {
                JspostAddRow();
            }
            else if (eventArg == "postClearText")
            {
                JspostClearText();
            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
            }
            else if (eventArg == "postExpenseCode")
            {
                JsExpenseCode();
            }
            else if (eventArg == "postApv")
            {
                JsApv();
            }
            else if (eventArg == "postExpenseBank")
            {
                JsExpenseBank();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String xml_main = Dw_main.Describe("DataWindow.Data.XML");
                //String as_entrydate = Dw_main.GetItemDate(1, "entry_date").ToString();
                //String ls_moneytyp, ls_expaccid;
                int li_index, li_count;
                Boolean lb_err = false;
                //ตรวจสอบข้อมูล
                if (lb_err == true)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณา กรอกข้อมูลเลขที่บัญชีเงินฝาก");
                }
                else
                {

                    //Session["entry_date"] = as_entrydate;
                    tdwmain.Eng2ThaiAllRow();
                    try
                    {
                        str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                        astr_mbreqchggrp.xml_reqdetail = xml_main;
                        int result = shrlonService.of_savereq_chggrp(state.SsWsPass, astr_mbreqchggrp);
                        if (result == 1)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                            JspostNewClear();

                            ////H-Code  เรื่อง Session
                            //DateTime entry_date = Convert.ToDateTime(Session["entry_date"].ToString());
                            //Dw_main.SetItemDate(1, "entry_date", entry_date);
                        }
                    }
                    catch (Exception ex)
                    {

                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }

        }

        public void WebSheetLoadEnd()
        {
            if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);
            }

            Dw_main.SaveDataCache();
            Dw_history.SaveDataCache();
            //=================
            tdwmain.Eng2ThaiAllRow();
            //=================
            DwUtil.RetrieveDDDW(Dw_main, "new_group_1", pbl, state.SsCoopControl);
            DwUtil.RetrieveDDDW(Dw_main, "new_membtype_1", pbl, state.SsCoopControl);
            //DwUtil.RetrieveDDDW(Dw_expense, "monthlycut_type", pbl, null);
        }

        //==================================================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "memcoop_id", state.SsCoopId);
            //Dw_main.SetItemDate(1, "req_date", state.SsWorkDate);
            tdwmain.Eng2ThaiAllRow();
            //Panel1.Visible = false;
            Dw_history.Reset();
        }
        //==================================
        //Init ข้อมูล
        private void JspostInit()
        {
            try
            {
                try
                {
                    String member_no = Dw_main.GetItemString(1, "member_no");
                    member_no = WebUtil.MemberNoFormat(member_no);
                    Dw_main.SetItemString(1, "member_no", member_no);


                    //String as_xmlreq = Dw_main.Describe("DataWindow.Data.XML");

                    try
                    {
                        str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                        astr_mbreqchggrp.member_no = member_no;

                        int result = shrlonService.of_initreq_chggrp(state.SsWsPass, ref astr_mbreqchggrp);
                        if (result == 1)
                        {
                            try
                            {
                                Dw_main.Reset();
                                // Dw_main.ImportString(astr_mbreqchggrp.xml_reqdetail, FileSaveAsType.Xml);
                                DwUtil.ImportData(astr_mbreqchggrp.xml_reqdetail, Dw_main, null, FileSaveAsType.Xml);

                                Dw_history.Reset();
                                if (astr_mbreqchggrp.xml_reqhistory != "")
                                {
                                    Dw_history.Reset();
                                    DwUtil.ImportData(astr_mbreqchggrp.xml_reqhistory, Dw_history, null, FileSaveAsType.Xml);

                                    //  Dw_history.ImportString(astr_mbreqchggrp.xml_reqhistory, FileSaveAsType.Xml);
                                }

                                Dw_main.SetItemString(1, "entry_id", state.SsUsername);
                                Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                                string chgg_docno = Dw_main.GetItemString(1, "chggroup_docno");
                                //string exp_bank = "";
                                //try
                                //{
                                //    exp_bank = Dw_main.GetItemString(1, "expense_bank");
                                //}
                                //catch
                                //{ }
                                //DwUtil.RetrieveDDDW(Dw_main, "expense_branch", "mb_req_chggroup.pbl", exp_bank);

                                if (chgg_docno == "")
                                {
                                    Dw_main.SetItemDate(1, "req_date", state.SsWorkDate);
                                    tdwmain.Eng2ThaiAllRow();
                                }
                                else
                                {
                                    
                                }

                                //if (salary_id == null || salary_id == "")
                                //{
                                //    //ดึงเลขพนักงานจากเลขสมาชิก
                                //    string sqlMemb = @"select Trim(salary_id) as salary_id from mbmembmaster where member_no = '" + member_no + @"' and member_status = 1";
                                //    Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
                                //    if (dtMemb.Next())
                                //    {
                                //        //เซตค่าของเลขพนักงานที่ได้มาจากเลขสมาชิกให้กับตัวแปร salary_id
                                //        salary_id = dtMemb.GetString("salary_id");
                                //        Dw_main.SetItemString(1, "salary_id", salary_id);
                                //    }
                                //    else
                                //    {
                                //        this.JspostNewClear();
                                //        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขพนักงาน " + salary_id);
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                if (astr_mbreqchggrp.xml_reqdetail == "")
                                {
                                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกย้ายไปแล้วรออนุมัติ");
                                }
                                else
                                {
                                    LtServerMessage.Text = WebUtil.ErrorMessage(ex + "as_xmlreq");
                                }
                                JspostNewClear();
                            }

                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        //=======================================
        private void JspostFilterSection()
        {
            try
            {
                String new_group = Dw_main.GetItemString(1, "new_group");
                //Dw_main.SetItemString(1, "new_membsection", "");
                //DwUtil.RetrieveDDDW(Dw_main, "new_membsection_1", pbl, state.SsCoopControl, new_group);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        //======================================

        private void JspostInitMember()
        {                                   
            try
            {
                String memcoop_id = "";

                memcoop_id = state.SsCoopControl;

                try
                {
                    String member_no = Dw_main.GetItemString(1, "member_no");
                    member_no = WebUtil.MemberNoFormat(member_no);
                    Dw_main.SetItemString(1, "member_no", member_no);
                    //String member_no = Hdmember_no.Value.Trim();
                    //member_no = WebUtil.MemberNoFormat(member_no);
                    //Dw_main.SetItemString(1, "member_no", member_no);

                    //String as_xmlreq = Dw_main.Describe("DataWindow.Data.XML");

                    try
                    {
                        str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                        astr_mbreqchggrp.member_no = member_no;
                        //str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                        //astr_mbreqchggrp.xml_reqdetail = as_xmlreq;

                        int result = shrlonService.of_initreq_chggrp(state.SsWsPass, ref astr_mbreqchggrp);
                        if (result == 1)
                        {
                            try
                            {
                                Dw_main.Reset();
                                DwUtil.ImportData(astr_mbreqchggrp.xml_reqdetail, Dw_main, null, FileSaveAsType.Xml);
                                //Dw_main.ImportString(astr_mbreqchggrp.xml_reqdetail, FileSaveAsType.Xml);
                                Dw_history.Reset();

                                if (astr_mbreqchggrp.xml_reqhistory != "")
                                {
                                    DwUtil.ImportData(astr_mbreqchggrp.xml_reqhistory, Dw_history, null, FileSaveAsType.Xml);
                                    //Dw_history.ImportString(astr_mbreqchggrp.xml_reqhistory, FileSaveAsType.Xml);
                                }

                                //if (salary_id == null || salary_id == "")
                                //{
                                //    //ดึงเลขพนักงานจากเลขสมาชิก
                                //    string sqlMemb = @"select Trim(salary_id) as salary_id from mbmembmaster where member_no = '" + member_no + @"' and member_status = 1";
                                //    Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
                                //    if (dtMemb.Next())
                                //    {
                                //        //เซตค่าของเลขพนักงานที่ได้มาจากเลขสมาชิกให้กับตัวแปร salary_id
                                //        salary_id = dtMemb.GetString("salary_id");
                                //        Dw_main.SetItemString(1, "salary_id", salary_id);
                                //    }
                                //    else
                                //    {
                                //        this.JspostNewClear();
                                //        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขพนักงาน " + salary_id);
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                if (astr_mbreqchggrp.xml_reqdetail == "")
                                {
                                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกย้ายไปแล้วรออนุมัติ");
                                }
                                else
                                {
                                    LtServerMessage.Text = WebUtil.ErrorMessage(ex + "as_xmlreq");
                                }
                                JspostNewClear();
                            }
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        //====================================
        private void JspostSetNewGroup()
        {
            String new_group = Hd_membgroup.Value.Trim();
            Dw_main.SetItemString(1, "new_group", new_group);
            JspostFilterSection();
        }

        private void JspostSetDocno()
        {
            try
            {
                String docno = Hd_docno.Value.Trim();
                String coop_id = state.SsCoopId;
                str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                //astr_mbreqchggrp.coop_id = coop_id;
                astr_mbreqchggrp.regchgdoc_no = docno;
                int result = shrlonService.of_openreq_chggrp(state.SsWsPass, ref astr_mbreqchggrp);
                if (result == 1)
                {
                    Dw_main.Reset();
                    Dw_main.ImportString(astr_mbreqchggrp.xml_reqdetail, FileSaveAsType.Xml);
                    Dw_history.Reset();

                    if (astr_mbreqchggrp.xml_reqhistory != "")
                    {
                        Dw_history.ImportString(astr_mbreqchggrp.xml_reqhistory, FileSaveAsType.Xml);
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostAddRow()
        {
            int li_row;
            //li_row = Dw_expense.InsertRow(0);
            //Dw_expense.SetItemDecimal(li_row, "chg_status", 0);
            //Dw_expense.SetItemDecimal(li_row, "sort_in_monthlycut", 1);
        }

        private void JspostClearText()
        {
            int rowcurrent = int.Parse(Hd_row.Value);
            //Dw_expense.SetItemString(rowcurrent, "expense_bank", "");
            //Dw_expense.SetItemString(rowcurrent, "expense_branch", "");
            //Dw_expense.SetItemString(rowcurrent, "expense_accid", "");
        }

        private void JsPostSalaryId()
        {
            String memberNo = "";
            String salary_id = Dw_main.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                memberNo = dtMemb.GetString("member_no");
                Dw_main.SetItemString(1, "member_no", memberNo);
                JspostInit();
            }
            else
            {
                this.JspostNewClear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }

        private void JsApv()
        {
            decimal apv_flag = 0;
            try
            {
                apv_flag = Dw_main.GetItemDecimal(1, "apvflag");
            }
            catch
            { }
            if (apv_flag == 1)
            {
                Dw_main.SetItemDate(1, "apv_date", state.SsWorkDate);
                Dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
                Dw_main.SetItemString(1, "apv_id", state.SsUsername);
                //apv_date apv_id

            }
            else
            {
                Dw_main.SetItemNull(1, "apv_dat");
                Dw_main.SetItemNull(1, "entry_date");
            }
        }

        private void JsExpenseCode()
        {
            string exp_code = "";
            try
            {
                exp_code = Dw_main.GetItemString(1, "expense_code");
            }
            catch
            {

            }
            if (exp_code == "CSH" || exp_code == "TRN")
            {

                //dw_main.SetItemString(1, "expense_bank_1", "");
                //dw_main.SetItemString(1, "expense_bank", "");
                //dw_main.SetItemString(1, "expense_branch_1", "");
                //dw_main.SetItemString(1, "expense_branch", "");
            }
            else if (exp_code == "CBT")
            {
                DwUtil.RetrieveDDDW(Dw_main, "expense_bank_1", "mb_req_chggroup.pbl", null);
                //DwUtil.RetrieveDDDW(Dw_main, "expense_branch", "mb_req_chggroup.pbl", null);
            }

        }

        private void JsExpenseBank()
        {
            string exp_bank = "";
            try
            {
                exp_bank = Dw_main.GetItemString(1, "expense_bank");
            }
            catch
            { }

            DwUtil.RetrieveDDDW(Dw_main, "expense_branch","mb_req_chggroup.pbl", exp_bank);
        }
    }
}
