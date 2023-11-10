using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;
//using CoreSavingLibrary.WcfShrlon;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.mbshr.ws_mbshr_req_mbnew_2_ctrl
{
    public partial class ws_mbshr_req_mbnew_2 : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewClear { get; set; }
        [JsPostBack]
        public string PostSetMembdate { get; set; }
        [JsPostBack]
        public string PostSetMemberno { get; set; }
        [JsPostBack]
        public string PostChangSex { get; set; }
        [JsPostBack]
        public string PostSetRetrydate { get; set; }
        [JsPostBack]
        public string PostCardPerson { get; set; }
        [JsPostBack]
        public string PostCheckSalaryId { get; set; }
        [JsPostBack]
        public string PostSalary { get; set; }
        [JsPostBack]
        public string PostGetDistrict { get; set; }
        [JsPostBack]
        public string PostGetCurrDistrict { get; set; }
        [JsPostBack]
        public string PostGetMateDistrict { get; set; }
        [JsPostBack]
        public string PostGetPostcode { get; set; }
        [JsPostBack]
        public string PostGetCurrPostcode { get; set; }
        [JsPostBack]
        public string PostGetMatePostcode { get; set; }
        [JsPostBack]
        public string PostLinkAddress { get; set; }
        [JsPostBack]
        public string PostLinkAddress2 { get; set; }
        [JsPostBack]
        public string PostGetBranch { get; set; }
        [JsPostBack]
        public string PostBankBranch { get; set; }
        [JsPostBack]
        public string PostDocno { get; set; }
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        [JsPostBack]
        public String PostInsertRowGain { get; set; }
        [JsPostBack]
        public String PostDeleteRowGain { get; set; }
        [JsPostBack]
        public String PostMembgroupCodeFind { get; set; }
        [JsPostBack]
        public String JsPostGetTambolPostcode { get; set; }
        [JsPostBack]
        public String JsPostGetCurrTambolPostcode { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMoneytr.InitDsMoneytr(this);
            dsGain.InitDsGaing(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                NewClear();
                for (var i = 0; i < dsMoneytr.RowCount; i++)
                {
                    dsMoneytr.DATA[i].COOP_ID = state.SsCoopId;
                }
                for (var i = 0; i < dsGain.RowCount; i++)
                {
                    dsGain.DATA[i].COOP_ID = state.SsCoopId;
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewClear)
            {
                NewClear();
            }
            else if (eventArg == PostSetMembdate)
            {
                if (dsMain.DATA[0].MEMBDATEFIX_FLAG == 1)
                {
                    dsMain.FindTextBox(0, dsMain.DATA.MEMBDATEFIX_DATEColumn).ReadOnly = false;
                    dsMain.DATA[0].MEMBDATEFIX_DATE = state.SsWorkDate;
                }
                else
                {
                    dsMain.DATA[0].SetMEMBDATEFIX_DATENull();
                    dsMain.FindTextBox(0, dsMain.DATA.MEMBDATEFIX_DATEColumn).ReadOnly = true;
                }
            }
            else if (eventArg == PostSetMemberno)
            {

                dsMain.FindTextBox(0, dsMain.DATA.MEMBER_NOColumn).ReadOnly = false;
            }
            else if (eventArg == PostChangSex)
            {
                string prename_code = dsMain.DATA[0].PRENAME_CODE;

                String sql = @"SELECT sex
                              FROM mbucfprename  
                              WHERE prename_code = {0}";
                sql = WebUtil.SQLFormat(sql, prename_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    String sex = dt.GetString("sex");
                    dsMain.DATA[0].SEX = sex;
                }
            }
            else if (eventArg == PostSetRetrydate)
            {
                age();
            }
            else if (eventArg == PostCardPerson)
            {
                string even = HdCheckIDCard.Value;
                if (even == "card_person")
                {
                    string nationality = "";
                    try
                    {
                        nationality = dsMain.DATA[0].NATIONALITY;
                    }
                    catch (Exception ex)
                    {
                        dsMain.DATA[0].CARD_PERSON = "";
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุสัญชาติ");
                    }

                    if (nationality == "ไทย")
                    {
                        String PID = dsMain.DATA[0].CARD_PERSON;
                        if (PID.Length != 13)
                        {
                            dsMain.DATA[0].IsCARD_PERSONNull();
                            this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ครบ 13 หลัก')");
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

                                    string checkcard = "";

                                    checkcard = CheckPerson(memcoop_id, PID, "");

                                    if (checkcard != "")
                                    {
                                        this.SetOnLoadedScript("alert('" + checkcard + "')");
                                    }
                                }
                                else
                                {
                                    dsMain.DATA[0].IsCARD_PERSONNull();
                                    this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ถูกต้อง')");
                                }
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        }
                    }
                }
                else if (even == "mate_cardperson")
                {
                    String PID = dsMain.DATA[0].MATE_CARDPERSON;
                    if (PID.Length != 13)
                    {
                        dsMain.DATA[0].IsMATE_CARDPERSONNull();
                        this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ครบ 13 หลัก')");
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

                                string checkcard = "";

                                checkcard = CheckPerson(memcoop_id, PID, "");

                                if (checkcard != "")
                                {
                                    this.SetOnLoadedScript("alert('" + checkcard + "')");
                                }
                            }
                            else
                            {
                                dsMain.DATA[0].IsMATE_CARDPERSONNull();
                                this.SetOnLoadedScript("alert('เลขบัตรประชาชนไม่ถูกต้อง')");
                            }
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    }
                }
            }
            else if (eventArg == PostCheckSalaryId)
            {
                try
                {
                    String memcoop_id = state.SsCoopControl;
                    String salary_id = dsMain.DATA[0].SALARY_ID;
                    salary_id = salary_id.Trim();
                    if (salary_id != null)
                    {
                        string checksalary = "";
                        checksalary = CheckPerson(memcoop_id, "", salary_id);

                        if (checksalary != "")
                        {
                            this.SetOnLoadedScript("alert('" + checksalary + "')");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == PostSalary)
            {
                String memcoop_id = state.SsCoopControl;
                String memcoop_id2 = state.SsCoopId;

                Decimal member_type = dsMain.DATA[0].MEMBER_TYPE;
                Decimal salary_amount, incomeetc_amt;
                try { salary_amount = dsMain.DATA[0].SALARY_AMOUNT; }
                catch { salary_amount = 0; }
                try { incomeetc_amt = dsMain.DATA[0].INCOMEETC_AMT; }
                catch { incomeetc_amt = 0; }
                Decimal total = salary_amount + incomeetc_amt;
                String sharetype_code = "01";

                Decimal minmaxshare = CalPayShareMonth(memcoop_id, sharetype_code, member_type, total); // 020001 01 1 

                dsMain.DATA[0].PERIODSHARE_VALUE = minmaxshare;
                dsMain.DATA[0].PERIODBASE_VALUE = minmaxshare;
            }
            else if (eventArg == PostMembgroupCodeFind)
            {
                dsMain.DdMembgroupCodeFind(dsMain.DATA[0].MEMBGROUP_CODE.Trim() + "%");
            }
            else if (eventArg == PostGetDistrict)
            {
                dsMain.DATA[0].DISTRICT_CODE = "";
                dsMain.DATA[0].TAMBOL_CODE = "";
                dsMain.DATA[0].POSTCODE = "";
                dsMain.DdDistrict(dsMain.DATA[0].PROVINCE_CODE);
            }
            else if (eventArg == PostGetCurrDistrict)
            {
                dsMain.DATA[0].CURRAMPHUR_CODE = "";
                dsMain.DATA[0].CURRTAMBOL_CODE = "";
                dsMain.DATA[0].CURRADDR_POSTCODE = "";
                dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);

            }
            else if (eventArg == PostGetMateDistrict)
            {
                dsMain.DATA[0].MATEAMPHUR_CODE = "";
                dsMain.DATA[0].MATETAMBOL_CODE = "";
                dsMain.DATA[0].MATEADDR_POSTCODE = "";
                dsMain.DdMateDistrict(dsMain.DATA[0].MATEPROVINCE_CODE);
            }
            else if (eventArg == PostGetPostcode)
            {
                PostGetPostcode2();
            }
            else if (eventArg == PostGetCurrPostcode)
            {
                PostGetCurrPostcode2();
            }
            else if (eventArg == PostGetMatePostcode)
            {
                PostGetMatePostcode2();
            }
            else if (eventArg == JsPostGetTambolPostcode)
            {
                PostGetTambolPostcode();
            }
            else if (eventArg == JsPostGetCurrTambolPostcode)
            {
                PostGetCurrTambolPostcode();
            }
            else if (eventArg == PostLinkAddress)
            {
                dsMain.DATA[0].CURRADDR_NO = dsMain.DATA[0].MEMB_ADDR;
                dsMain.DATA[0].CURRADDR_MOO = dsMain.DATA[0].ADDR_GROUP;
                dsMain.DATA[0].CURRADDR_SOI = dsMain.DATA[0].SOI;
                dsMain.DATA[0].CURRADDR_VILLAGE = dsMain.DATA[0].MOOBAN;
                dsMain.DATA[0].CURRADDR_ROAD = dsMain.DATA[0].ROAD;
                dsMain.DATA[0].CURRPROVINCE_CODE = dsMain.DATA[0].PROVINCE_CODE;
                dsMain.DATA[0].CURRAMPHUR_CODE = dsMain.DATA[0].DISTRICT_CODE;
                dsMain.DATA[0].CURRTAMBOL_CODE = dsMain.DATA[0].TAMBOL_CODE;
                dsMain.DATA[0].CURRADDR_POSTCODE = dsMain.DATA[0].POSTCODE;
                dsMain.DATA[0].CURRADDR_PHONE = dsMain.DATA[0].MEM_TEL;

                dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);
                dsMain.DdCurrTambol(dsMain.DATA[0].CURRAMPHUR_CODE);
            }
            else if (eventArg == PostLinkAddress2)
            {
                dsMain.DATA[0].MATEADDR_NO = dsMain.DATA[0].MEMB_ADDR;
                dsMain.DATA[0].MATEADDR_MOO = dsMain.DATA[0].ADDR_GROUP;
                dsMain.DATA[0].MATEADDR_SOI = dsMain.DATA[0].SOI;
                dsMain.DATA[0].MATEADDR_VILLAGE = dsMain.DATA[0].MOOBAN;
                dsMain.DATA[0].MATEADDR_ROAD = dsMain.DATA[0].ROAD;
                dsMain.DATA[0].MATEPROVINCE_CODE = dsMain.DATA[0].PROVINCE_CODE;
                dsMain.DATA[0].MATEAMPHUR_CODE = dsMain.DATA[0].DISTRICT_CODE;
                dsMain.DATA[0].MATETAMBOL_CODE = dsMain.DATA[0].TAMBOL_CODE;
                dsMain.DATA[0].MATEADDR_POSTCODE = dsMain.DATA[0].POSTCODE;
                dsMain.DATA[0].MATEADDR_PHONE = dsMain.DATA[0].MEM_TEL;

                dsMain.DdMateDistrict(dsMain.DATA[0].MATEPROVINCE_CODE);
                dsMain.DdMateTambol(dsMain.DATA[0].MATEAMPHUR_CODE);
                //PostGetMatePostcode2();
            }
            else if (eventArg == PostGetBranch)
            {
                dsMain.DdBranch(dsMain.DATA[0].EXPENSE_BANK);
            }
            else if (eventArg == PostBankBranch)
            {
                PostBankBranch2();
            }
            else if (eventArg == PostDocno)
            {
                String docno = dsMain.DATA[0].APPL_DOCNO;
                dsMain.RetrieveMain(docno);
                dsMain.DATA[0].membgroup = dsMain.DATA[0].MEMBGROUP_CODE;
                dsMain.DdAppltypeCode();
                dsMain.DdMemType();
                dsMain.DdMembgroupCode();
                dsMain.DdPrename();
                string matepro = dsMain.DATA[0].MATEPROVINCE_CODE;
                dsMain.DATA[0].MATEPROVINCE_CODE = matepro.Trim();
                dsMain.DdProvince();

                dsMain.DATA[0].bank_desc = dsMain.DATA[0].EXPENSE_BANK;
                dsMain.DdBank();
                dsMain.DATA[0].branch_name = dsMain.DATA[0].EXPENSE_BRANCH;
                dsMain.DdBranch(dsMain.DATA[0].EXPENSE_BANK);
                dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);
                dsMain.DdCurrTambol(dsMain.DATA[0].CURRAMPHUR_CODE);
                dsMain.DdMateDistrict(dsMain.DATA[0].MATEPROVINCE_CODE);
                dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                dsMain.DdDistrict(province_code);
                dsMain.DdTambol(dsMain.DATA[0].DISTRICT_CODE);
                dsMain.DdMateTambol(dsMain.DATA[0].MATEAMPHUR_CODE);

                dsMoneytr.RetrieveMoneytr(docno);
                dsMoneytr.DdMoneyTrType();
                dsMoneytr.DdMoneyType();
                dsMoneytr.DdBank();

                //ดึงชื่อสาขา ของ dsMoneytr
                int row = dsMoneytr.RowCount;
                for (int i = 0; i < row; i++)
                {
                    string bank_code = dsMoneytr.DATA[i].BANK_CODE;
                    string bank_branch = dsMoneytr.DATA[i].BANK_BRANCH;
                    string sql = @" 
                    SELECT branch_name
                    FROM cmucfbankbranch  
                    WHERE ( bank_code = {0} ) AND ( branch_id = {1} )";
                    sql = WebUtil.SQLFormat(sql, bank_code, bank_branch);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMoneytr.DATA[i].branch_name = dt.GetString("branch_name");
                    }
                }
                //ดึงข้อมูลผู้รับผลประโยชน์
                dsGain.RetrieveGain(docno);
                dsGain.DdPrenameGain();
                dsGain.DdRelationGain();

                //ดึงข้อมูล age
                age();
            }
            else if (eventArg == PostInsertRow)
            {
                dsMoneytr.InsertLastRow();
                dsMoneytr.DdMoneyTrType();
                dsMoneytr.DdMoneyType();
                dsMoneytr.DdBank();
            }
            else if (eventArg == PostDeleteRow)
            {
                int r = dsMoneytr.GetRowFocus();
                dsMoneytr.DeleteRow(r);
                dsMoneytr.DdMoneyTrType();
                dsMoneytr.DdMoneyType();
                dsMoneytr.DdBank();
            }
            else if (eventArg == PostInsertRowGain)
            {
                dsGain.InsertLastRow();
                dsGain.DdPrenameGain();
                dsGain.DdRelationGain();
            }
            else if (eventArg == PostDeleteRowGain)
            {
                int r = dsGain.GetRowFocus();
                dsGain.DeleteRow(r);
                dsGain.DdPrenameGain();
                dsGain.DdRelationGain();
            }
        }

        public void NewClear()
        {
            dsMain.ResetRow();
            dsMoneytr.ResetRow();
            dsGain.ResetRow();

            dsMain.DdAppltypeCode();
            dsMain.DdMemType();
            dsMain.DdMembgroupCode();
            dsMain.DdPrename();
            dsMain.DdProvince();
            dsMain.DdBank();

            dsMain.DATA[0].APPLY_DATE = state.SsWorkDate;
            dsMain.DATA[0].NATIONALITY = "ไทย";
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            dsMain.DATA[0].SALARY_ID = "00000000";
            dsMain.DATA[0].MEMBTYPE_CODE = "10";

            dsMoneytr.InsertLastRow(1);
            dsMoneytr.DdMoneyTrType();
            dsMoneytr.DdMoneyType();
            dsMoneytr.DdBank();
            dsMoneytr.DATA[0].TRTYPE_CODE = "KEEP1";

            dsGain.InsertLastRow(1);
            dsGain.DATA[0].SEQ_NO = 1;
            dsGain.DdPrenameGain();
            dsGain.DdRelationGain();
            for (int i = 0; i < dsMoneytr.RowCount; i++)
            {
                if (dsMoneytr.DATA[i].TRTYPE_CODE == "KEEP1")
                {
                    dsMoneytr.DATA[i].MONEYTYPE_CODE = "TMT";
                }
            }
        }

        //เช็คการเป็นสมาชิก TKS
        private string CheckPerson(String memcoop_id, String cardperson, String salary_id)
        {
            String result = "";

            String sql1 = "", sql2 = "", sql3 = "", sql4 = "";
            String txtID;

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

        //คำนวณหุ้นฐาน TKS
        private Decimal CalPayShareMonth(String memcoop_id, String sharetype_code, Decimal member_type, Decimal salary_amount)
        {
            Decimal share_mth = 0;//020001 01 1 
            member_type = dsMain.DATA[0].MEMBER_TYPE;
            string sql1 = @"select * from shsharetypemthrate where coop_id = {0} and sharetype_code= {1} and {2} >= start_salary and {3} <= end_salary and member_type = {4}";
            string sql2 = @"select * from shsharetype where coop_id ={0} and sharetype_code = {1} and 1 = 1";

            sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, sharetype_code, salary_amount, salary_amount, member_type);
            sql2 = WebUtil.SQLFormat(sql2, state.SsCoopControl, sharetype_code);

            Sdt dt = WebUtil.QuerySdt(sql1);
            Sdt dt2 = WebUtil.QuerySdt(sql2);

            bool Isdt2 = dt2.Next();
            if (dt.Next())
            {
                decimal sharerate_percent = dt.GetDecimal("sharerate_percent") / 100;

                share_mth = sharerate_percent * salary_amount;

                //ตรวจสอบขั้นต่ำ
                if (share_mth < dt.GetDecimal("minshare_amt"))
                {
                    share_mth = dt.GetDecimal("minshare_amt") * dt2.GetDecimal("unitshare_value");
                }

                //ตรวจสอบขั้นสูงสุด
                if (share_mth > dt.GetDecimal("maxshare_amt"))
                {
                    share_mth = dt.GetDecimal("maxshare_amt") * dt2.GetDecimal("unitshare_value");
                }
            }
            //tomy ปัดเต็ม 10 
            share_mth = Convert.ToDecimal(Math.Ceiling(Convert.ToDouble(share_mth) / 10) * 10);
            return share_mth;
        }

        public void SaveWebSheet()
        {
            try
            {
                for (int r = 0; r < dsGain.RowCount; r++)
                {
                    dsGain.DATA[r].SEQ_NO = r + 1;
                }
                String memcoop_id = state.SsCoopControl;
                string salary_id = "";
                try
                {
                    salary_id = dsMain.DATA[0].SALARY_ID;//GetItemString(1, "salary_id");
                }
                catch { dsMain.DATA[0].SALARY_ID = ""; }
                str_mbreqnew astr_mbreqnew = new str_mbreqnew();
                astr_mbreqnew.xml_mbdetail = dsMain.ExportXml();
                astr_mbreqnew.xml_mbgain = dsGain.ExportXml();
                astr_mbreqnew.xml_mbmoneytr = dsMoneytr.ExportXml();//dw_moneytr.Describe("DataWindow.Data.XML");
                astr_mbreqnew.entry_id = state.SsUsername;
                int result = wcf.NShrlon.of_savereq_mbnew(state.SsWsPass, ref astr_mbreqnew);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                dsMain.ResetRow();
                dsMoneytr.ResetRow();
                NewClear();
                //ExecuteDataSource exed1 = new ExecuteDataSource(this);
                //string appl_docno = dsMain.DATA[0].APPL_DOCNO;
                //if (appl_docno == "AUTO")
                //{
                //    string sql = dsMain.CreateSyntaxInsert();
                //    exed1.SQL.Add(sql);
                //    //exed1.AddFormView(dsMain,);
                //    exed1.AddRepeater(dsMoneytr);
                //    exed1.Execute();
                //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            //dsMain.DdAppltypeCode();
            //dsMain.DdMemType();
            //dsMain.DdMembgroupCode();
            //dsMain.DdPrename();
            //dsMain.DdProvince();
            //dsMain.DdBank();
            //PostGetPostcode2();
            //PostGetCurrPostcode2();
            //PostGetMatePostcode2();
            //PostBankBranch2();
            //dsMain.DdBranch(dsMain.DATA[0].EXPENSE_BANK);
            //dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);
            //dsMain.DdCurrTambol(dsMain.DATA[0].CURRAMPHUR_CODE);
            //dsMain.DdMateDistrict(dsMain.DATA[0].MATEPROVINCE_CODE);
            //dsMain.DdCurrDistrict(dsMain.DATA[0].CURRPROVINCE_CODE);
            //string province_code = dsMain.DATA[0].PROVINCE_CODE;
            //dsMain.DdDistrict(province_code);

        }

        public void PostGetPostcode2()
        {
            dsMain.DATA[0].TAMBOL_CODE = "";
            dsMain.DdTambol(dsMain.DATA[0].DISTRICT_CODE);

            String sql = @"SELECT MBUCFDISTRICT.POSTCODE   
                FROM MBUCFDISTRICT,  
	                MBUCFPROVINCE         	                        
                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
	                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
	                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].PROVINCE_CODE, dsMain.DATA[0].DISTRICT_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }

        public void PostGetCurrPostcode2()
        {
            dsMain.DATA[0].CURRTAMBOL_CODE = "";
            dsMain.DdCurrTambol(dsMain.DATA[0].CURRAMPHUR_CODE);

            String sql = @"SELECT MBUCFDISTRICT.POSTCODE   
                FROM MBUCFDISTRICT,  
	                MBUCFPROVINCE         	                        
                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
	                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
	                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].CURRPROVINCE_CODE, dsMain.DATA[0].CURRAMPHUR_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].CURRADDR_POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }

        public void PostGetTambolPostcode()
        {
            String sql = @"SELECT MBUCFTAMBOL.POSTCODE
                            FROM MBUCFDISTRICT,  
                                MBUCFPROVINCE,
                                MBUCFTAMBOL				         	                        
                            WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE ) 
                                and ( MBUCFDISTRICT.DISTRICT_CODE = MBUCFTAMBOL.DISTRICT_CODE ) 
                                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
                                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )
                                and ( MBUCFTAMBOL.TAMBOL_CODE = {2} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].PROVINCE_CODE, dsMain.DATA[0].DISTRICT_CODE, dsMain.DATA[0].TAMBOL_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }

        public void PostGetCurrTambolPostcode()
        {
            String sql = @"SELECT MBUCFTAMBOL.POSTCODE
                            FROM MBUCFDISTRICT,  
                                MBUCFPROVINCE,
                                MBUCFTAMBOL				         	                        
                            WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE ) 
                                and ( MBUCFDISTRICT.DISTRICT_CODE = MBUCFTAMBOL.DISTRICT_CODE ) 
                                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
                                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )
                                and ( MBUCFTAMBOL.TAMBOL_CODE = {2} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].CURRPROVINCE_CODE, dsMain.DATA[0].CURRAMPHUR_CODE, dsMain.DATA[0].CURRTAMBOL_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].CURRADDR_POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }

        public void PostGetMatePostcode2()
        {
            dsMain.DATA[0].MATETAMBOL_CODE = "";
            dsMain.DdMateTambol(dsMain.DATA[0].MATEAMPHUR_CODE);

            String sql = @"SELECT MBUCFDISTRICT.POSTCODE   
                FROM MBUCFDISTRICT,  
	                MBUCFPROVINCE         	                        
                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
	                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
	                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].MATEPROVINCE_CODE, dsMain.DATA[0].MATEAMPHUR_CODE);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].MATEADDR_POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }

        public void PostBankBranch2()
        {
            int row = dsMoneytr.GetRowFocus();
            string bank_code = dsMoneytr.DATA[row].BANK_CODE;
            string bank_branch = dsMoneytr.DATA[row].BANK_BRANCH;
            string sql = @" 
                    SELECT branch_name
                    FROM cmucfbankbranch  
                    WHERE ( bank_code = {0} ) AND ( branch_id = {1} )";
            sql = WebUtil.SQLFormat(sql, bank_code, bank_branch);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMoneytr.DATA[row].branch_name = dt.GetString("branch_name");
            }
        }

        //คิดอายุ
        public void age()
        {
            try
            {
                DateTime birth_date = dsMain.DATA[0].BIRTH_DATE;

                string ls_age;
                decimal age = 0;
                DateTime retry_date = state.SsWorkDate;

                string sql = @"select ft_retrydate( {0} , {1} ) as retry, ftcm_calagemth( {2}, {3} ) as age from dual";

                sql = WebUtil.SQLFormat(sql, state.SsCoopId, birth_date, birth_date, state.SsWorkDate);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    retry_date = dt.GetDate("retry");
                    age = dt.GetDecimal("age");
                }
                dsMain.DATA[0].RETRY_DATE = retry_date;

                ls_age = age.ToString();
                if (ls_age.Substring(ls_age.Length - 2, 2) == "00")
                {
                    ls_age = ls_age.Substring(0, 2) + " ปี ";
                }
                else
                {
                    ls_age = ls_age.Replace(".", " ปี ") + " เดือน";
                }

                dsMain.DATA[0].age = ls_age;
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }


    }
}