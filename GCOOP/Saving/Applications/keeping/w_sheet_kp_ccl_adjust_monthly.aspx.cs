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
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using DataLibrary;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_ccl_adjust_monthly : PageWebSheet, WebSheet
    {
        public decimal principal_adjamt;
        public decimal interest_adjamt;
        public decimal ldc_principal;
        public decimal ldc_interest;

        private DwThDate tdwmain;
        protected String jsPostMember;
        protected String jsPostGroup;
        protected String newClear;
        protected String jsRefresh;
        protected String jsmembgroup_code;
        protected String jsCoopSelect;
        protected String jsChangmidgroupcontrol;
        protected String postRefresh;
        protected String postCheckStatus;
        protected String postSumItemadj;
        protected String postSetPrinInt;
        protected String postGetPrinInt;

        public String pbl = "kp_ccl_adjust_monthly.pbl";

        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsCoopSelect = WebUtil.JsPostBack(this, "jsCoopSelect");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postCheckStatus = WebUtil.JsPostBack(this, "postCheckStatus");
            postSumItemadj = WebUtil.JsPostBack(this, "postSumItemadj");
            postSetPrinInt = WebUtil.JsPostBack(this, "postSetPrinInt");
            postGetPrinInt = WebUtil.JsPostBack(this, "postGetPrinInt");
            tdwmain = new DwThDate(dw_main, this);
            tdwmain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            //keepingService = wcf.NKeeping;

            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);

            if (IsPostBack)
            {
                try
                {
                    this.RestoreContextDw(dw_main);
                    this.RestoreContextDw(dw_detail);
                    HdIsPostBack.Value = "true";
                }
                catch { }
            }
            else
            {
                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);
                dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);                
                tdwmain.Eng2ThaiAllRow();
                DwUtil.RetrieveDDDW(dw_main, "adjtype_code", pbl, state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_main, "slipretcause_code", pbl, state.SsCoopControl);
                if (state.SsCoopControl == "023001" || state.SsCoopControl == "022001")
                {
                    dw_main.SetItemString(1, "slipretcause_code", "001");
                }
                DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", pbl, state.SsCoopControl, "KEP", "ADJ");
                if (state.SsCoopControl == "023001")
                {
                    dw_main.SetItemString(1, "tofrom_accid", "11180310");
                }
                else if (state.SsCoopControl == "022001")
                {
                    dw_main.SetItemString(1, "tofrom_accid", "11121300");
                }
                HdIsPostBack.Value = "False";

            }
        }

        public void CheckJsPostBack(String eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "postRefresh")
            {
                //Set Member_no
                if (SearchType.SelectedValue == "memberNo")
                {
                    String member_no = dw_main.GetItemString(1, "member_no");
                    member_no = WebUtil.MemberNoFormat(member_no);
                    dw_main.SetItemString(1, "member_no", member_no);
                }
                else if (SearchType.SelectedValue == "saralyId")
                {
                    String member_no = dw_main.GetItemString(1, "member_no");
                    member_no = PostSaralyId(member_no) ;
                    dw_main.SetItemString(1, "member_no", member_no);
                }
            }
            else if (eventArg == "postCheckStatus")
            {
                JspostCheckStatus();
            }
            else if (eventArg == "postSumItemadj")
            {
                JspostSumItemadj();
            }
            else if (eventArg == "postSetPrinInt")
            {
                JspostSetPrinInt();
            }
            else if (eventArg == "postGetPrinInt")
            {
                JspostGetPrinInt();
            }
        }

        public void SaveWebSheet()
        {
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            try
            {
                str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                astr_keep_adjust.xml_main = dw_main.Describe("DataWindow.Data.XML");
                astr_keep_adjust.xml_detail = dw_detail.Describe("DataWindow.Data.XML");
                astr_keep_adjust.cancel_id = state.SsUsername;
                astr_keep_adjust.operate_date = state.SsWorkDate;

                //int result = keepingService.SaveAdjustMonthly(state.SsWsPass, astr_keep_adjust);
                int result = wcf.NKeeping.of_save_adjust_monthly(state.SsWsPass, astr_keep_adjust);
                if (result == 1)
                {
                    //by mikekong ของออมสินต้องย้ายไปเอกเทน เพราะต้องเพิ่มในส่วนนำต้นค้างดอกเบี้ยค้าง มาหักลบกับต้นคืนดอกเบี้ยคืน
                    JsNewClear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        private void JsPostMember()
        {
            String memcoop_id = "";
            String member_no = dw_main.GetItemString(1, "member_no");

            if (1 == 1)
            {
                memcoop_id = state.SsCoopControl;
            }
            else
            {
                memcoop_id = state.SsCoopId;
            }

            dw_main.SetItemString(1, "memcoop_id", memcoop_id);

            try
            {
                str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                astr_keep_adjust.xml_main = dw_main.Describe("DataWindow.Data.XML");
                astr_keep_adjust.xml_detail = dw_detail.Describe("DataWindow.Data.XML");
                //int result = keepingService.InitAdjustMonthly(state.SsWsPass, ref astr_keep_adjust);
                int result = wcf.NKeeping.of_init_adjust_monthly(state.SsWsPass,ref astr_keep_adjust);
                if (result == 1)
                {
                    dw_main.Reset();
                    dw_detail.Reset();
                    DwUtil.ImportData(astr_keep_adjust.xml_main, dw_main, tdwmain, FileSaveAsType.Xml);
                    DwUtil.ImportData(astr_keep_adjust.xml_detail, dw_detail, null, FileSaveAsType.Xml);
                    dw_main.SetItemDecimal(1, "slipretall_flag", 0);
                    decimal slipretall_flag = dw_main.GetItemDecimal(1, "slipretall_flag");
                    if (slipretall_flag == 0)
                    {
                        for (int i = 1; i <= dw_detail.RowCount; i++)
                        {
                            dw_detail.SetItemDecimal(i, "operate_flag", 0);
                        }
                    }
                    if (state.SsCoopControl == "022001" || state.SsCoopControl == "027001")
                    {
                        DateTime adj_date = dw_main.GetItemDateTime(1, "ref_slipdate");
                        dw_main.SetItemDate(1, "adjslip_date", adj_date);
                        dw_main.SetItemDate(1, "operate_date", adj_date);
                    }
                    else
                    {
                        dw_main.SetItemDate(1, "adjslip_date", state.SsWorkDate);
                    }

                    DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", pbl, state.SsCoopControl, "KEP", "ADJ");
                    if (state.SsCoopControl == "023001")
                    {
                        dw_main.SetItemString(1, "tofrom_accid", "11180310");
                    }
                    else if (state.SsCoopControl == "022001")
                    {
                        dw_main.SetItemString(1, "tofrom_accid", "11121300");
                    }
                    JspostSumItemadj();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsNewClear()
        {
            lbl_cancel.Text = 0.ToString("#,##0.00");
            lbl_nocancel.Text = 0.ToString("#,##0.00");
            dw_main.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(0);
            dw_detail.InsertRow(0);
            dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
            tdwmain.Eng2ThaiAllRow();
            HdIsPostBack.Value = "False";
        }

        private void JspostCheckStatus()
        {
            decimal slipretall_flag = dw_main.GetItemDecimal(1, "slipretall_flag");
            if (dw_detail.RowCount > 0)
            {
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    dw_detail.SetItemDecimal(i, "operate_flag", slipretall_flag);
                }
                JspostSumItemadj();
            }
        }

        private void JspostSumItemadj()
        {
            try
            {
                int rowcurrent = 0;
                try 
                {
                    rowcurrent = int.Parse(Hdrow.Value);
                }
                catch { rowcurrent = 0; }
               
                 
                decimal Sumnocancel = 0;
                decimal SumCancel = 0;
                decimal operate_flag = 0;
                decimal item_adjamt = 0;
                decimal sign_flag = 0;
                string slipitemtype_code = "";
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    slipitemtype_code = dw_detail.GetItemString(i, "slipitemtype_code");
                    if (slipitemtype_code == "MRT")
                    {
                        sign_flag = -1;
                    }
                    else
                    {
                        sign_flag = 1;
                    }
                    operate_flag = dw_detail.GetItemDecimal(i, "operate_flag");
                    item_adjamt = dw_detail.GetItemDecimal(i, "item_adjamt");
                    item_adjamt = item_adjamt * sign_flag;
                    if (operate_flag == 1)
                    {
                        //ถึง row ที่เลือก
                        
                        if (i == rowcurrent)
                        {

                            if (principal_adjamt > 0)
                            {
                                ldc_principal = principal_adjamt;
                                HdPrin.Value = ldc_principal.ToString();
                            }
                            else
                            {
                                try { principal_adjamt = dw_detail.GetItemDecimal(rowcurrent, "principal_adjamt"); }
                                catch { principal_adjamt = 0; }
                                
                                ldc_principal = principal_adjamt;
                                HdPrin.Value = ldc_principal.ToString();
                            }

                            //กรณีที่ยอดต้นเงินยังไม่มียอดเงิน
                            if (interest_adjamt > 0)
                            {
                                ldc_principal = interest_adjamt;
                                HdInt.Value = ldc_interest.ToString();
                            }
                            else
                            {
                                try { interest_adjamt = dw_detail.GetItemDecimal(rowcurrent, "interest_adjamt"); }
                                catch { interest_adjamt = 0; }
                               
                                ldc_interest = interest_adjamt;
                                HdInt.Value = ldc_interest.ToString();
                            }
                        }

                        SumCancel = SumCancel + item_adjamt;
                    }
                    else
                    {
                        Sumnocancel = Sumnocancel + item_adjamt;
                    }
                }
                lbl_cancel.Text = SumCancel.ToString("#,##0.00");
                lbl_nocancel.Text = Sumnocancel.ToString("#,##0.00");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetPrinInt()
        {
            try 
            {
                int rowcurrent = int.Parse(Hdrow.Value);
                string slipitemtype_code = dw_detail.GetItemString(rowcurrent, "slipitemtype_code");         
                decimal item_adjamt = 0;

                //กรณีเป็นเงินกู้
                if (slipitemtype_code == "LON")
                {
                    item_adjamt = dw_detail.GetItemDecimal(rowcurrent, "item_adjamt");
                    ldc_principal = Convert.ToDecimal(HdPrin.Value);
                    ldc_interest = Convert.ToDecimal(HdInt.Value);
                    //ถ้ายอดเงินรวมมากกว่าเท่ากับดอก ให้หักดอกเบี้ยก่อน
                    if (item_adjamt > (ldc_principal + ldc_interest))
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ยอดเงินที่ท่านกรอกเข้าไป เกินจากยอดที่หักชำระ ต้นเงินและดอกเบี้ยรวม = " + (ldc_principal+ldc_interest).ToString("#,##0.00") + " กรุณากรอกยอดเงินใหม่" );
                        dw_detail.SetItemDecimal(rowcurrent, "item_adjamt", 0);
                    }
                    else
                    {
                        if (item_adjamt >= ldc_interest)
                        {
                            //ถ้ายอดเงินที่กรอกเยอะกว่าดอกเบี้ยสามารถหักดอกได้หมด ก็ให้ set ดอกเบี้ยเป็น 0 เลย
                            dw_detail.SetItemDecimal(rowcurrent, "interest_adjamt", 0);
                            item_adjamt = item_adjamt - ldc_interest;
                            //กรณีหักต้นเงินแล้วเหลือให้ไปหักดอกต่อ
                            if (item_adjamt > 0)
                            {
                                //กรณีที่ยอด
                                if (item_adjamt >= ldc_principal)
                                {
                                    dw_detail.SetItemDecimal(rowcurrent, "principal_adjamt", 0);
                                }
                                //กรณีที่ยอด item_adjamt เหลือน้อยกว่ายอดดอกเบี้ยให้นำยอดเงินที่เหลือไป set 
                                else
                                {
                                    ldc_principal = ldc_principal - item_adjamt;
                                    dw_detail.SetItemDecimal(rowcurrent, "principal_adjamt", ldc_principal);
                                }
                            }
                            //กรณีที่ต้นเงินเแล้ว ยอดเงินไม่เหลือให้ set ต้นเงินเป็น 0
                            else
                            {
                                dw_detail.SetItemDecimal(rowcurrent, "principal_adjamt", ldc_principal);
                            }
                        }
                        //กรณีที่ยอดเงินน้อยกว่าต้นเงิน
                        else
                        {
                            item_adjamt = dw_detail.GetItemDecimal(rowcurrent, "item_adjamt");
                            ldc_interest = ldc_interest - item_adjamt;
                            dw_detail.SetItemDecimal(rowcurrent, "interest_adjamt", ldc_interest);
                            dw_detail.SetItemDecimal(rowcurrent, "principal_adjamt", ldc_principal);
                        }

                        //principal_adjamt = dw_detail.GetItemDecimal(rowcurrent, "principal_adjamt");
                        //interest_adjamt = dw_detail.GetItemDecimal(rowcurrent, "interest_adjamt");
                        //dw_detail.SetItemDecimal(rowcurrent, "item_adjamt", principal_adjamt + interest_adjamt);
                    }
                }
                //กรณีที่ไม่ใช้ยอดเงินกู้
                else
                {
                    item_adjamt = dw_detail.GetItemDecimal(rowcurrent, "item_adjamt");
                    dw_detail.SetItemDecimal(rowcurrent, "principal_adjamt", item_adjamt);

                    principal_adjamt = dw_detail.GetItemDecimal(rowcurrent, "principal_adjamt");
                    dw_detail.SetItemDecimal(rowcurrent, "item_adjamt", principal_adjamt);
                }

                decimal Sumnocancel = 0;
                decimal SumCancel = 0;
                decimal operate_flag = 0;
                
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    operate_flag = dw_detail.GetItemDecimal(i, "operate_flag");
                    item_adjamt = dw_detail.GetItemDecimal(i, "item_adjamt");
                    if (operate_flag == 1)
                    {
                        
                        SumCancel = SumCancel + item_adjamt;
                    }
                    else
                    {
                        Sumnocancel = Sumnocancel + item_adjamt;
                    }
                }
                lbl_cancel.Text = SumCancel.ToString("#,##0.00");
                lbl_nocancel.Text = Sumnocancel.ToString("#,##0.00");
                
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostGetPrinInt()
        {
            try 
            {                                   
                //กรณีที่ยอดต้นเงินยังไม่มียอดเงิน               
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        String PostSaralyId(String memberNo)
        {
            try
            {
                String sqmem = @"select member_no from mbmembmaster where salary_id like '%"+ memberNo.Trim() +"%'";
                Sdt ta = WebUtil.QuerySdt(sqmem);
                if (ta.Next())
                {
                    memberNo = ta.GetString("member_no");
                }
            }
            catch
            { }

            return memberNo;
        }
    }
}
