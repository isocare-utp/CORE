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
using Sybase.DataWindow;
using DataLibrary;
using CoreSavingLibrary.WcfNDivavg;
using System.Web.Services.Protocols;
using System.Globalization;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_opr_slip : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_main;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_opr_slip.pbl";

        protected String postNewClear;
        protected String postInit;
        protected String postRefresh;
        protected String postInitMemberNo;
        protected String postSlipDate;
        protected String postDeldetail;

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String ireport_obj;
        public String outputProcess = "";

        DateTime idtm_lastDate, idtm_activedate;
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("slip_date", "slip_tdate");

            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postInitMemberNo = WebUtil.JsPostBack(this, "postInitMemberNo");
            postSlipDate = WebUtil.JsPostBack(this, "postSlipDate");
            postDeldetail = WebUtil.JsPostBack(this, "postDeldetail");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                this.ConnectSQLCA();
                JspostNewClear();
                of_activeworkdate();
                idtm_activedate = Convert.ToDateTime(HdActiveDate.Value);
                Dw_main.SetItemDateTime(1, "operate_date", idtm_activedate);
                Dw_main.SetItemDateTime(1, "slip_date", idtm_activedate);
                if (idtm_activedate != state.SsWorkDate)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันแล้ว ระบบจะทำการเปลี่ยนวันที่เป็น " + idtm_activedate.ToString("dd/MM/yyyy", th));
                }
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            // Event ที่เกิดจาก JavaScript
            switch (eventArg)
            {
                case "postNewClear":
                    JspostNewClear();
                    break;
                case "postInit":
                    JspostInit();
                    break;
                case "postRefresh":
                    break;
                case "postInitMemberNo":
                    JspostInitMemberNo();
                    break;
                case "postSlipDate":
                    tDw_main.Thai2EngAllRow();
                    DateTime ldtm_slipdate = Dw_main.GetItemDate(1, "slip_date");
                    idtm_lastDate = Convert.ToDateTime(HdLastDate.Value);
                    idtm_activedate = Convert.ToDateTime(HdActiveDate.Value);
                    if (ldtm_slipdate < idtm_activedate)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่อนุญาตให้วันที่จ่ายปันผล - เฉลี่ยคืน " + ldtm_slipdate.ToString("dd/MM/yyyy", th) + " น้อยกว่าวันทำการ " + idtm_activedate.ToString("dd/MM/yyyy", th) + " กรุณาตรวจสอบ");
                        Dw_main.SetItemDateTime(1, "slip_date", idtm_lastDate);
                        return;
                    }
                    //ตรวจสอบวันที่ป้อน ว่าเป็นวันทำการหรือไม่
                    Boolean status = wcf.NCommon.of_isworkingdate(state.SsWsPass, ldtm_slipdate);
                    if (status == false)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("วันที่จ่ายปันผล - เฉลี่ยคืน " + ldtm_slipdate.ToString("dd/MM/yyyy", th) + " ไม่ใช่วันทำการ กรุณาตรวจสอบ");
                        Dw_main.SetItemDateTime(1, "slip_date", idtm_lastDate);
                        return;
                    }
                    HdLastDate.Value = Convert.ToString(ldtm_slipdate);
                    break;
                case "postDeldetail":
                    decimal payout_payment = 0;
                    int row = Convert.ToInt32(Hdrow.Value);
                    Dw_detail.DeleteRow(row);
                    for (int i = 1; i <= Dw_detail.RowCount; i++)
                    {
                        payout_payment += Dw_detail.GetItemDecimal(i, "item_payment");
                    }
                    Dw_main.SetItemDecimal(1, "payout_payment", payout_payment);
                    break;
            }
        }

        //function บันทึกข้อมูล
        public void SaveWebSheet()
        {
            //เช็ค tofrom_accid null
            for (int i = 1; i <= Dw_detail.RowCount; i++)
            {
                if (Dw_detail.GetItemString(i, "tofrom_accid") == "")
                {
                    this.SetOnLoadedScript("alert('กรุณาเลือกรหัสบัญชี!!'); ");
                    return;
                }
            }
            //เช็คลิ้นชักกรณีจ่ายเงินสด
            Boolean lbl_fin = of_checkfin();
            if (lbl_fin == false)
            {
                return;
            }

            try
            {
                DivavgService = wcf.NDivavg;
                str_divsrv_oper astr_divsrv_oper = new str_divsrv_oper();
                astr_divsrv_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_oper.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_save_slip(state.SsWsPass, ref astr_divsrv_oper);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    Print(state.SsCoopControl, astr_divsrv_oper.payoutslip_no);
                    JspostNewClear();
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            // Retrieve DropDown
            DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, null);
            DwUtil.RetrieveDDDW(Dw_detail, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(Dw_detail, "expense_bank", pbl, null);
            //DwUtil.RetrieveDDDW(Dw_detail, "tofrom_accid", pbl);

            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_main.Eng2ThaiAllRow();

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }
        #endregion

        // function InitData
        private void JspostInit()
        {
            try
            {
                String member_no = Dw_main.GetItemString(1, "member_no");

                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;

                str_divsrv_oper astr_divsrv_oper = new str_divsrv_oper();
                astr_divsrv_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_oper.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_init_slip(state.SsWsPass, ref astr_divsrv_oper);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_oper.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);
                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_oper.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(Dw_detail, "tofrom_accid", pbl, state.SsCoopId);
                    DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, state.SsCoopId);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        // ฟังก์ชัน Load หน้าจอแรก
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            Dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);


            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }

            //Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            Dw_detail.Reset();
        }

        private void JspostInitMemberNo()
        {
            try
            {
                String member_no = Hdmember_no.Value.Trim();
                Dw_main.SetItemString(1, "member_no", member_no);

                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_oper astr_divsrv_oper = new str_divsrv_oper();
                astr_divsrv_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_oper.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_init_slip(state.SsWsPass, ref astr_divsrv_oper);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_oper.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_oper.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(Dw_detail, "tofrom_accid", pbl, state.SsCoopId);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsGetYear()
        {
            Decimal account_year = 0;
            try
            {
                String sql = @"select max(current_year) as current_year from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    //account_year = int.Parse(dt.GetString("max(current_year)"));
                    account_year = dt.GetDecimal("current_year");
                    Dw_main.SetItemString(1, "div_year", Convert.ToString(account_year));
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                account_year = DateTime.Now.Year;
                account_year = account_year + 543;
                Dw_main.SetItemString(1, "div_year", account_year.ToString());
            }
        }

        /// <summary>
        /// get วันทำการ
        /// </summary>       
        public void of_activeworkdate()
        {
            try
            {
                string sqlStr;
                int li_clsdaystatus = 0;
                DateTime ldtm_workdate;
                Sdt dt;
                sqlStr = @" select workdate, closeday_status
                    from amappstatus 
                    where coop_id = '" + state.SsCoopId + @"'
                    and application = '" + state.SsApplication + "'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    ldtm_workdate = dt.GetDate("workdate");
                    li_clsdaystatus = dt.GetInt32("closeday_status");
                    if (li_clsdaystatus == 1)
                    {
                        int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref idtm_activedate);
                        HdLastDate.Value = Convert.ToString(idtm_activedate);
                        HdActiveDate.Value = Convert.ToString(idtm_activedate);
                    }
                    else
                    {
                        HdLastDate.Value = Convert.ToString(state.SsWorkDate);
                        HdActiveDate.Value = Convert.ToString(state.SsWorkDate);
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// ตรวจสอบการเงิน
        /// </summary>
        private Boolean of_checkfin()
        {
            string moneytype_code = "";
            for (int i = 1; i <= Dw_detail.RowCount; i++)
            {
                moneytype_code = Dw_detail.GetItemString(i, "moneytype_code");
                if (moneytype_code == "CSH")
                {
                    i = Dw_detail.RowCount;
                    break;
                }
            }

            string sqlStr = "";
            Sdt dt;
            if (moneytype_code == "CSH")
            {
                //เช็คว่าวันจ่ายเงินกู้เป็นวันเดียวกันกับวันทำการหรือไม่
                idtm_activedate = Convert.ToDateTime(HdActiveDate.Value);
                if (state.SsWorkDate != idtm_activedate)
                {
                    this.SetOnLoadedScript("alert('ประเภทการจ่ายเป็นเงินสด ไม่สามารถจ่ายเงินกู้ล่วงหน้าได้ กรุณาตรวจสอบ');");
                    return false;
                }

                //เช็คว่าต้องการตรวจสอบการเงินหรือไม่
                sqlStr = @"select allpay_atfin from finconstant where coop_id = '" + state.SsCoopControl + "'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    if (dt.GetInt32("allpay_atfin") == 0)
                    {
                        return true;
                    }
                }

                //เช็คลิ้นชักการเงิน
                try
                {
                    sqlStr = @"select status from fintableusermaster where user_name = {0} and opdatework = {1}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsUsername, state.SsWorkDate);
                    dt = WebUtil.QuerySdt(sqlStr);
                    if (dt.Next())
                    {
                        int status = dt.GetInt32("status");
                        if (status == 14)
                        {
                            this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้เนื่องจากมีการปิดลิ้นชักไปแล้วของ " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        this.SetOnLoadedScript("alert('ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                        return false;
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            return true;
        }

        private void Print(string coopId, string payoutslip_no)
        {
            switch (coopId)
            {
                case "020001":
                    ireport_obj = "r_fin_form_bill_pay_divavg";
                    String member_no = Dw_main.GetItemString(1, "member_no");
                    Printing.PrintIRDivPayPBN(this, member_no, ireport_obj);
                    break;
                case "022001":
                    //string slip_no = "";
                    string memberNo = Dw_main.GetItemString(1, "member_no");
                    ireport_obj = "r_divavg_form_bill_pay";
                    //string se = @"select max(payoutslip_no) as payoutslip_no from yrslippayout where member_no = '"+memberNo+"'";
                    //Sdt ta = WebUtil.QuerySdt(se);
                    //if (ta.Next())
                    //{
                    //    slip_no = ta.GetString("payoutslip_no");
                    //}
                    Printing.IRPrint(this, payoutslip_no, ireport_obj);
                    break;
                case "028001":
                    ireport_obj = "r_sl_slip_out_div";
                    Printing.PrintIRDivPayRBT(this, payoutslip_no, ireport_obj);
                    break;
            }
        }
    }


}