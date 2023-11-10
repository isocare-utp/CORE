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

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_memdet_methpay_detail : PageWebSheet,WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
   //     private DwThDate tDw_main;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_memdet_methpay_detail.pbl";

        protected String postNewClear;
        protected String postInit;
        protected String postRefresh;
        protected String postInsertRow;
        protected String postInitMemberNo;
        protected String postSetMoneytype;
        protected String postSetDeptAccountNo;
        protected String postSetSequestAmt;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {

            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postInitMemberNo = WebUtil.JsPostBack(this, "postInitMemberNo");
            postSetMoneytype = WebUtil.JsPostBack(this, "postSetMoneytype");
            postSetDeptAccountNo = WebUtil.JsPostBack(this, "postSetDeptAccountNo");
            postSetSequestAmt = WebUtil.JsPostBack(this, "postSetSequestAmt");
            //ประกาศฟังก์ชันการใช้วันที่
            //tDw_main = new DwThDate(Dw_main, this);
            //tDw_main.Add("methreq_date", "methreq_tdate");

            DwUtil.RetrieveDDDW(Dw_detail, "expense_bank", pbl, null);
            DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, null);
        }

        public void WebSheetLoadBegin()
        {
           

            if (!IsPostBack)
            {
                JspostNewClear();
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
                case "postInsertRow":
                    JspostInsertRow();
                    break;
                case "postInitMemberNo":
                    JspostInitMemberNo();
                    break;
                case "postSetMoneytype":
                    JspostSetMoneytype();
                    break;
                case "postSetDeptAccountNo":
                    JspostSetDeptAccountNo();
                    break;
                case "postSetSequestAmt":
                    JspostSetSequestAmt();
                    break;
            }
        }

        //function บันทึกข้อมูล
        public void SaveWebSheet()
        {
          
            try
            {
                DivavgService = wcf.NDivavg;
                str_divsrv_det astr_divsrv_det = new str_divsrv_det();
                astr_divsrv_det.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_det.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_save_memdet_methpay(state.SsWsPass, ref astr_divsrv_det);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
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
            
            
            // แปลงค่าวันที่จาก Eng เป็น Thai          
           // tDw_main.Eng2ThaiAllRow();

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }
        #endregion
        private void JspostSetSequestAmt()
        {
            int row = int.Parse(Hdrow.Value);
            decimal sequest_flag = Dw_detail.GetItemDecimal(row, "sequest_flag");
            decimal expense_amt = Dw_detail.GetItemDecimal(row, "expense_amt");
            if (sequest_flag == 1)
            {
                Dw_detail.SetItemDecimal(row, "sequest_amt", expense_amt);
            }
            else
            {
                Dw_detail.SetItemDecimal(row, "sequest_amt", 0);
            }
                
        }
        private void JspostSetDeptAccountNo()
        {
            int rowcurrent = int.Parse(Hdrow.Value);
            String Deptaccount_no = Hdacountno.Value.Trim();
            Dw_detail.SetItemString(rowcurrent, "expense_accid", Deptaccount_no);
        }

        private void JspostInsertRow()
        {
            Dw_detail.InsertRow(0);
            Dw_detail.SetItemString(Dw_detail.RowCount, "coop_id", state.SsCoopId);
            //Dw_detail.SetItemDecimal(Dw_detail.RowCount, "operate_flag", 1);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "payseq_no", Dw_detail.RowCount);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "seq_no", Dw_detail.RowCount);
            Dw_detail.SetItemString(Dw_detail.RowCount, "member_no", Dw_main.GetItemString(1, "member_no"));
            Dw_detail.SetItemString(Dw_detail.RowCount, "div_year", Dw_main.GetItemString(1, "div_year"));
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "div_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "avg_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "etc_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "prin_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "int_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "sequest_flag", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "sequest_amt", 0);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "methpay_status", 0);
            Dw_detail.SetItemString(Dw_detail.RowCount, "paytype_code", "ALL");
        }
        // function InitData

        private void JspostInit()
        {
            try
            {
                String member_no = Dw_main.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_det astr_divsrv_det = new str_divsrv_det();
                astr_divsrv_det.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_det.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_memdet_methpay(state.SsWsPass, ref astr_divsrv_det);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_main, Dw_main, null, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
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
            if (Dw_main.RowCount > 0)
            {
                Dw_main.Reset();
            }
            Dw_main.InsertRow(0);
            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
               // Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }

            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
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
                str_divsrv_det astr_divsrv_det = new str_divsrv_det();
                astr_divsrv_det.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_det.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_memdet_methpay(state.SsWsPass, ref astr_divsrv_det);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_main, Dw_main, null, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
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

        private void JspostSetMoneytype()
        {
            try
            {
                int rowcurrent = int.Parse(Hdrow.Value);
                String methpaytype_code = Dw_detail.GetItemString(rowcurrent, "methpaytype_code");
                String moneytype_code = "";
                String sql = @"select join_moneytype_code from yrucfmethpay where methpaytype_code = '" + methpaytype_code + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    moneytype_code = dt.GetString("join_moneytype_code");
                    Dw_detail.SetItemString(rowcurrent, "moneytype_code", moneytype_code);
                }
                else
                {
                    sqlca.Rollback();
                }

               // int rowcurrent = int.Parse(Hdrow.Value);
            //    String methpaytype_code = Dw_detail.GetItemString(rowcurrent, "methpaytype_code");
                if (methpaytype_code == "CSH" || methpaytype_code == "DEP" || methpaytype_code == "CBT")
                {
                    Dw_detail.SetItemString(rowcurrent, "expense_bank", "");
                    Dw_detail.SetItemString(rowcurrent, "expense_branch", "");
                    Dw_detail.SetItemString(rowcurrent, "expense_accid", "");
                }
            }
            catch (Exception ex) { }
        }

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    Dw_main.SetItemString(1, "div_year", Convert.ToString(account_year));
                  //  JspostSetAccDate();
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

    }
}