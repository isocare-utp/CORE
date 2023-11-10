using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CoreSavingLibrary.WcfShrlon;
//using CoreSavingLibrary.WcfNCommon;

using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr.ws_mbshr_adt_mbaudit_ctrl
{
    public partial class ws_mbshr_adt_mbaudit : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostProvince { get; set; }
        [JsPostBack]
        public string PostAmpur { get; set; }
        [JsPostBack]
        public string PostCurrprovince { get; set; }
        [JsPostBack]
        public string PostCurramphur { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostBank { get; set; }
        [JsPostBack]
        public string PostBranch { get; set; }
        [JsPostBack]
        public string PostLinkAddress { get; set; }
        [JsPostBack]
        public string PostBankBranch { get; set; }
        [JsPostBack]
        public string PostExpenseBank { get; set; }
        [JsPostBack]
        public string PostExpenseBranch { get; set; }
        [JsPostBack]
        public string PostRefresh { get; set; }
        [JsPostBack]
        public string PostPosition { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                string district_code = dsMain.DATA[0].AMPHUR_CODE;
                string currprovince_code = dsMain.DATA[0].CURRPROVINCE_CODE;
                string currdistrict_code = dsMain.DATA[0].CURRAMPHUR_CODE;
                dsMain.DdMemType();
                dsMain.DdPrename();
                dsMain.DdTambol(district_code);
                dsMain.DdDistrict(province_code);
                dsMain.DdProvince();
                dsMain.DdCurrProvince();
                dsMain.DdCurrDistrict(currprovince_code);
                dsMain.DdCurrTambol(currdistrict_code);
                dsList.DdMoneytrtype();
                dsList.DdMoneytype();
                dsList.DdBank();
                dsMain.DdBank();
                string bank_code = dsMain.DATA[0].bank_name;
                dsMain.DdBranch(bank_code);
                string position_code = dsMain.DATA[0].POSITION_CODE.Trim();
                this.SetOnLoadedScript(" parent.Setfocus();");
                //dsMain.DdPosition(position_code);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                dsList.DdMoneytrtype();
                dsList.DdMoneytype();
                dsList.DdBank();
            }
            else if (eventArg == PostMember)
            {
                try
                {
                    string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                    dsMain.RetrieveMain(member_no);
                    dsList.RetrieveList(member_no);
                    string province_code = dsMain.DATA[0].PROVINCE_CODE;
                    string district_code = dsMain.DATA[0].AMPHUR_CODE;
                    dsMain.DATA[0].PROVINCE_CODE = province_code;
                    dsMain.DdProvince();
                    dsMain.DdDistrict(province_code);
                    dsMain.DdTambol(district_code);
                    string currprovince_code = dsMain.DATA[0].CURRPROVINCE_CODE;
                    string currdistrict_code = dsMain.DATA[0].CURRAMPHUR_CODE;
                    dsMain.DdCurrProvince();
                    dsMain.DdCurrDistrict(currprovince_code);
                    dsMain.DdCurrTambol(currdistrict_code);
                    dsMain.DdBank();
                    string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                    dsMain.DATA[0].bank_name = expense_bank;
                    string bank_code = dsMain.DATA[0].bank_name;
                    dsMain.DdBranch(bank_code);
                    string expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                    dsMain.DATA[0].BRANCH_NAME = expense_branch;
                    string position_code = dsMain.DATA[0].POSITION_CODE.Trim();
                    dsMain.DATA[0].POSITION_CODE = position_code;
                    //dsMain.DdPosition(position_code);

                    //str_mbaudit lstr_mbinfo = new str_mbaudit();
                    //lstr_mbinfo.member_no = member_no;
                    //wcf.NShrlon.of_init_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                    //dsMain.DdMemType();
                    //dsMain.ImportData(lstr_mbinfo.xmlmaster);
                    //dsList.ImportData(lstr_mbinfo.xmlmoneytr);

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            else if (eventArg == PostProvince)
            {
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                dsMain.DATA[0].AMPHUR_CODE = "";
                dsMain.DdDistrict(province_code);
            }
            else if (eventArg == PostRefresh)
            {
                int row = dsList.GetRowFocus();
                dsList.DATA[row].BANK_ACCID = dsList.DATA[row].BANK_ACCID;
            }
            else if (eventArg == PostAmpur)
            {
                string district_code = dsMain.DATA[0].AMPHUR_CODE;
                dsMain.DATA[0].TAMBOL_CODE = "";
                dsMain.DdTambol(district_code);
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                string sql = @" 
                               SELECT DISTRICT_CODE,   
                                 PROVINCE_CODE,   
                                 DISTRICT_DESC,   
                                 POSTCODE  
                            FROM MBUCFDISTRICT 
                          where ((PROVINCE_CODE={0}) and (DISTRICT_CODE={1})) ";
                sql = WebUtil.SQLFormat(sql, province_code, district_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].ADDR_POSTCODE = dt.GetString("POSTCODE");
                }
            }
            else if (eventArg == PostCurrprovince)
            {
                string currprovince_code = dsMain.DATA[0].CURRPROVINCE_CODE;
                dsMain.DATA[0].CURRAMPHUR_CODE = "";
                dsMain.DdCurrDistrict(currprovince_code);
            }
            else if (eventArg == PostCurramphur)
            {
                string currdistrict_code = dsMain.DATA[0].CURRAMPHUR_CODE;
                dsMain.DATA[0].CURRTAMBOL_CODE = "";
                dsMain.DdCurrTambol(currdistrict_code);
                string currprovince_code = dsMain.DATA[0].CURRPROVINCE_CODE;
                string sql = @" 
                               SELECT DISTRICT_CODE,   
                                 PROVINCE_CODE,   
                                 DISTRICT_DESC,   
                                 POSTCODE  
                            FROM MBUCFDISTRICT 
                          where ((PROVINCE_CODE={0}) and (DISTRICT_CODE={1})) ";
                sql = WebUtil.SQLFormat(sql, currprovince_code, currdistrict_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].CURRADDR_POSTCODE = dt.GetString("POSTCODE");
                }
            }
            else if (eventArg == PostDeleteRow)
            {
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
                dsList.DdMoneytrtype();
                dsList.DdMoneytype();
                dsList.DdBank();
            }
            else if (eventArg == PostBank)
            {
                string bank_code = dsMain.DATA[0].bank_name;
                dsMain.DATA[0].EXPENSE_BANK = bank_code;
                dsMain.DATA[0].EXPENSE_BRANCH = "";
                dsMain.DATA[0].BRANCH_NAME = "";
                dsMain.DdBranch(bank_code);
                //string branch_code = dsMain.DATA[0].branch_name;
                //dsMain.DATA[0].EXPENSE_BRANCH = branch_code;
            }
            else if (eventArg == PostBranch)
            {

                string bank_code = dsMain.DATA[0].bank_name;
                dsMain.DdBranch(bank_code);
            }
            else if (eventArg == PostLinkAddress)
            {
                string addr_no = dsMain.DATA[0].ADDR_NO;
                string addr_moo = dsMain.DATA[0].ADDR_MOO;
                string addr_soi = dsMain.DATA[0].ADDR_SOI;
                string addr_village = dsMain.DATA[0].ADDR_VILLAGE;
                string addr_road = dsMain.DATA[0].ADDR_ROAD;
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                string amphur_code = dsMain.DATA[0].AMPHUR_CODE;
                string tambol_code = dsMain.DATA[0].TAMBOL_CODE;
                string addr_postcode = dsMain.DATA[0].ADDR_POSTCODE;
                string addr_phone = dsMain.DATA[0].ADDR_PHONE; //โทรศัพท์: ดึงมาจากที่อยู่ตามทะเบียนบ้าน
                string addr_mobilephone = dsMain.DATA[0].ADDR_MOBILEPHONE; //มือถือ: ดึงมาจากที่อยู่ตามทะเบียนบ้าน

                dsMain.DATA[0].CURRADDR_NO = addr_no;
                dsMain.DATA[0].CURRADDR_MOO = addr_moo;
                dsMain.DATA[0].CURRADDR_SOI = addr_soi;
                dsMain.DATA[0].CURRADDR_VILLAGE = addr_village;
                dsMain.DATA[0].CURRADDR_ROAD = addr_road;
                dsMain.DATA[0].CURRPROVINCE_CODE = province_code;
                dsMain.DATA[0].CURRAMPHUR_CODE = amphur_code;
                dsMain.DATA[0].CURRTAMBOL_CODE = tambol_code;
                dsMain.DATA[0].CURRADDR_POSTCODE = addr_postcode;
                //เช็คว่าถ้า โทรศัพท์: ดึงมาจากที่อยู่ตามทะเบียนบ้าน = ค่าว่าง >> ถ้าใช่: ให้เซตค่า โทรศัพท์: ที่อยู่ที่ติดต่อได้ = มือถือ: ดึงมาจากที่อยู่ตามทะเบียนบ้าน, ถ้าไม่ใช่ ให้เซตค่า โทรศัพท์: ที่อยู่ที่ติดต่อได้ = โทรศัพท์: ดึงมาจากที่อยู่ตามทะเบียนบ้าน
                if (addr_phone == null || addr_phone == "") dsMain.DATA[0].CURRADDR_PHONE = addr_mobilephone; //เซตค่า โทรศัพท์: ที่อยู่ที่ติดต่อได้
                else dsMain.DATA[0].CURRADDR_PHONE = addr_phone; //เซตค่า โทรศัพท์: ที่อยู่ที่ติดต่อได้
                string currprovince_code = dsMain.DATA[0].CURRPROVINCE_CODE;
                string curramphur_code = dsMain.DATA[0].CURRAMPHUR_CODE;
                dsMain.DdCurrDistrict(currprovince_code);
                dsMain.DdCurrTambol(curramphur_code);
                dsMain.DATA[0].PROVINCE_CODE = province_code;
            }
            else if (eventArg == PostBankBranch)
            {
                int row = dsList.GetRowFocus();
                string bank_code = dsList.DATA[row].BANK_CODE;
                string bank_branch = dsList.DATA[row].BANK_BRANCH;
                string sql = @" 
                    SELECT branch_name
                    FROM cmucfbankbranch  
                    WHERE ( bank_code = {0} ) AND ( branch_id = {1} )";
                sql = WebUtil.SQLFormat(sql, bank_code, bank_branch);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsList.DATA[row].BRANCH_NAME = dt.GetString("branch_name");
                }
            }
            else if (eventArg == PostExpenseBank)
            {
                string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                dsMain.DATA[0].bank_name = expense_bank;
                dsMain.DdBank();
                dsMain.DdBranch(expense_bank);

            }
            else if (eventArg == PostExpenseBranch)
            {
                string expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                dsMain.DATA[0].BRANCH_NAME = expense_branch;
                string bank_code = dsMain.DATA[0].bank_name;
                dsMain.DATA[0].BRANCH_NAME = expense_branch;
                dsMain.DdBranch(bank_code);
            }
            else if (eventArg == PostPosition)
            {
                string position_code = dsMain.DATA[0].POSITION_CODE.Trim();
                //dsMain.DdPosition(position_code);

            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void SaveWebSheet()
        {
            dsMain.DATA[0].UPDATE_BYENTRYID = state.SsUsername;
            dsMain.DATA[0].UPDATE_BYENTRYIP = state.SsClientIp;
            try
            {
                str_mbaudit lstr_mbinfo = new str_mbaudit();

                String CARD_PERSON = "";
                CARD_PERSON = dsMain.DATA[0].CARD_PERSON;
                // CARD_PERSON.Trim();
                // dsMain.DATA[0].CARD_PERSON = CARD_PERSON;
                CARD_PERSON = CARD_PERSON.Replace(" ", "");
                dsMain.DATA[0].CARD_PERSON = CARD_PERSON;


                //lstr_mbinfo.coop_id = state.SsCoopId;
                //lstr_mbinfo.member_no = Hfmember_no.Value;
                lstr_mbinfo.xmlmaster = dsMain.ExportXml();//dw_main.Describe("DataWindow.Data.XML");
                lstr_mbinfo.xmlmoneytr = dsList.ExportXml();//dw_moneytr.Describe("DataWindow.Data.XML");

                //int result = wcf.NShrlon.of_save_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                int result = wcf.NShrlon.of_save_mbaudit(state.SsWsPass, ref lstr_mbinfo);
                if (result == 1)
                {
                    string sql = @"update mbmembmaster set position_code = {2} , mateconfirmloan_date = {3} where coop_id = {0} and member_no = {1}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].POSITION_CODE, dsMain.DATA[0].MATECONFIRMLOAN_DATE);
                    try
                    {
                        Sdt dt = WebUtil.QuerySdt(sql);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    string sql_sh = @"update shsharemaster set periodbase_amt = {2} , periodshare_amt = {3} where coop_id = {0} and member_no = {1}";
                    sql_sh = WebUtil.SQLFormat(sql_sh, state.SsCoopId, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].periodbase_value / 10, dsMain.DATA[0].periodshare_value / 10);
                    try
                    {
                        Sdt dt_sh = WebUtil.QuerySdt(sql_sh);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    this.SetOnLoadedScript(" parent.Setfocus();");
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    dsMain.ResetRow();
                    dsList.ResetRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            if (dsMain.DATA[0].EXPENSE_CODE == "CSH")
            {
                dsMain.FindTextBox(0, "expense_bank").ReadOnly = true;
                dsMain.FindDropDownList(0, "bank_name").Enabled = false;
                dsMain.FindButton(0, "b_bank").Enabled = false;
                dsMain.FindTextBox(0, "expense_branch").ReadOnly = true;
                dsMain.FindDropDownList(0, "branch_name").Enabled = false;
                dsMain.FindButton(0, "b_branch").Enabled = false;
                dsMain.FindTextBox(0, "expense_accid").ReadOnly = true;
            }
            else if (dsMain.DATA[0].EXPENSE_CODE == "TRN")
            {
                dsMain.FindTextBox(0, "expense_bank").ReadOnly = true;
                dsMain.FindDropDownList(0, "bank_name").Enabled = false;
                dsMain.FindButton(0, "b_bank").Enabled = false;
                dsMain.FindTextBox(0, "expense_branch").ReadOnly = true;
                dsMain.FindDropDownList(0, "branch_name").Enabled = false;
                dsMain.FindButton(0, "b_branch").Enabled = false;
                dsMain.FindTextBox(0, "expense_accid").ReadOnly = false;
            }
            else
            {
                dsMain.FindTextBox(0, "expense_bank").ReadOnly = false;
                dsMain.FindDropDownList(0, "bank_name").Enabled = true;
                dsMain.FindButton(0, "b_bank").Enabled = true;
                dsMain.FindTextBox(0, "expense_branch").ReadOnly = false;
                dsMain.FindDropDownList(0, "branch_name").Enabled = true;
                dsMain.FindButton(0, "b_branch").Enabled = true;
                dsMain.FindTextBox(0, "expense_accid").ReadOnly = false;

            }
        }
    }
}