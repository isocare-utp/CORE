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

namespace Saving.Applications.Divavg
{
    public partial class w_sheet_divsrv_req_methpay : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_main;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_req_methpay.pbl";

        protected String postNewClear;
        protected String postInit;
        protected String postRefresh;
        protected String postInsertRow;
        protected String postInitMemberNo;
        protected String postSetMoneytype;
        protected String postSetDeptAccountNo;
        protected String postSumReq;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            // Retrieve DropDown
            DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_detail, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(Dw_detail, "expense_bank", pbl, null);
         //   DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, Dw_detail.GetItemString(Dw_detail.RowCount,"expense_bank").Trim());

            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postInitMemberNo = WebUtil.JsPostBack(this, "postInitMemberNo");
            postSetMoneytype = WebUtil.JsPostBack(this, "postSetMoneytype");
            postSetDeptAccountNo = WebUtil.JsPostBack(this, "postSetDeptAccountNo");
            postSumReq = WebUtil.JsPostBack(this, "postSumReq");
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("methreq_date", "methreq_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

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
                     String expense_bank = Dw_detail.GetItemString(1, "expense_bank").Trim();
                     DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, expense_bank);

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
                case "postSumReq":
                    break;

            }
        }

        //function บันทึกข้อมูล
        public void SaveWebSheet()
        {
            for (int i = 1; i <= Dw_detail.RowCount; i++)
            {
                Dw_detail.SetItemString(i, "coop_id", state.SsCoopId);
                Dw_detail.SetItemDecimal(i, "payseq_no", i);
            }
            try
            {
                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_save_methpay(state.SsWsPass, ref astr_divsrv_req);
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
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
            
        }
        #endregion

        private void JspostSetDeptAccountNo()
        {
            int rowcurrent = int.Parse(Hdrow.Value);
            String Deptaccount_no = Hdacountno.Value.Trim();
            String Deptcoop_id = Hdacccoop_id.Value.Trim();

            Dw_detail.SetItemString(rowcurrent, "bizzcoop_id", Deptcoop_id);
            Dw_detail.SetItemString(rowcurrent, "expense_accid", Deptaccount_no);
            Dw_detail.SetItemString(rowcurrent, "bizzaccount_no", Deptaccount_no);
        }

        private void JspostInsertRow()
        {
            Dw_detail.InsertRow(0);
            Dw_detail.SetItemString(Dw_detail.RowCount, "coop_id", state.SsCoopId);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "operate_flag", 1);
            Dw_detail.SetItemDecimal(Dw_detail.RowCount, "payseq_no", Dw_detail.RowCount);
        }

        private void JspostInit()
        {
            try
            {
                string methpay_type = "", methreq_docno = "";
                String member_no = Dw_main.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_methpay(state.SsWsPass, ref astr_divsrv_req);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                    try 
                    {
                        methpay_type = Dw_detail.GetItemString(1, "methpaytype_code");
                    }
                    catch { methpay_type = ""; }
                    
                    if (methpay_type == "CBT" || methpay_type == "CHQ")
                    {
                        String expense_bank = Dw_detail.GetItemString(1, "expense_bank").Trim();
                        DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, expense_bank);
                    }

                    for (int i = 1; i <= Dw_detail.RowCount; i++)
                    {
                        Dw_detail.SetItemDecimal(i, "operate_flag", 1);
                        Dw_detail.SetItemDecimal(i, "payseq_no", i);
                    }
                    methreq_docno = Dw_main.GetItemString(1, "methreq_docno").Trim();
                    if( methreq_docno != null || methreq_docno != "")
                    {
                        if (methreq_docno != "AUTO")
                        {
                            string i_message = "สมาชิกเลขที่ " + member_no + " ได้มีคำขอวิธีจ่ายปันผลแล้วเลขที่ " + methreq_docno;
                            this.SetOnLoadedScript("alert('" + i_message + "')");
                        }
                    }
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
            Dw_main.SetItemDateTime(1, "methreq_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);

            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }

            Dw_main.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_main.Eng2ThaiAllRow();

            if (Dw_detail.RowCount > 0)
            {
                Dw_detail.Reset();
            }


            HdIsPostBack.Value = "false";

        }

        private void JspostInitMemberNo()
                    {
            try
            {
                string methpay_type = "", methreq_docno = "";
                String member_no = Hdmember_no.Value.Trim();
                //String member_no = Dw_main.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_methpay(state.SsWsPass, ref astr_divsrv_req);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                    try 
                    {
                        methpay_type = Dw_detail.GetItemString(1, "methpaytype_code");
                    }
                    catch { methpay_type = ""; }
                    
                    if (methpay_type == "CBT" || methpay_type == "CHQ")
                    {
                        String expense_bank = Dw_detail.GetItemString(1, "expense_bank").Trim();
                        DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, expense_bank);
                    }

                    for (int i = 1; i <= Dw_detail.RowCount; i++)
                    {
                        Dw_detail.SetItemDecimal(i, "operate_flag", 1);
                        Dw_detail.SetItemDecimal(i, "payseq_no", i);
                    }
                    methreq_docno = Dw_main.GetItemString(1, "methreq_docno").Trim();
                    if( methreq_docno != null || methreq_docno != "")
                    {
                        if (methreq_docno != "AUTO")
                        {
                            string i_message = "สมาชิกเลขที่ " + member_no + " ได้มีคำขอวิธีจ่ายปันผลแล้วเลขที่ " + methreq_docno;
                            this.SetOnLoadedScript("alert('" + i_message + "')");
                        }
                    }
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
                Dw_detail.SetItemString(rowcurrent, "paytype_code", "ALL");
                String methpaytype_code = Dw_detail.GetItemString(rowcurrent, "methpaytype_code");
                String moneytype_code = "";
                String sql = @"select join_moneytype_code from yrucfmethpay where methpaytype_code = '" + methpaytype_code + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    moneytype_code = dt.GetString("join_moneytype_code");
                    Dw_detail.SetItemString(rowcurrent, "moneytype_code", moneytype_code);
                    if (methpaytype_code != "CBT" || methpaytype_code != "CHQ")
                    {
                        Dw_detail.SetItemString(rowcurrent, "expense_bank_typ", "");
                        Dw_detail.SetItemString(rowcurrent, "expense_bank", "");
                        Dw_detail.SetItemString(rowcurrent, "expense_branch", "");
                        Dw_detail.SetItemString(rowcurrent, "expense_accid", "");
                        Dw_detail.SetItemString(rowcurrent, "bizzaccount_no", "");
                    }

                    //กรณีเป็น ธกส ให้ default ธนาคารเป็น ธกส
                    if (state.SsCoopId == "010001")
                    {
                        if (methpaytype_code == "CBT")
                        {
                            Dw_detail.SetItemString(rowcurrent, "expense_bank_typ", "S");
                            Dw_detail.SetItemString(rowcurrent, "expense_bank", "034");
                            DwUtil.RetrieveDDDW(Dw_detail, "expense_branch", pbl, "034");
                            Dw_detail.SetItemString(rowcurrent, "expense_branch", "0000");
                            Dw_detail.SetItemString(rowcurrent, "expense_accid", "");
                            Dw_detail.SetItemString(rowcurrent, "bizzaccount_no", "");
                        }
                        else
                        {
                            Dw_detail.SetItemString(rowcurrent, "expense_bank_typ", "");
                            Dw_detail.SetItemString(rowcurrent, "expense_bank", "");
                            Dw_detail.SetItemString(rowcurrent, "expense_branch", "");
                            Dw_detail.SetItemString(rowcurrent, "expense_accid", "");
                            Dw_detail.SetItemString(rowcurrent, "bizzaccount_no", "");
                        }
                    }
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex) { }
        }

        private void JsGetYear()
        {
            Decimal account_year = 0;
            try
            {
                String sql = @"select max(current_year) as current_year  from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = dt.GetDecimal("current_year");
                    //account_year = int.Parse(dt.GetString("max(current_year)"));
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

    }
}