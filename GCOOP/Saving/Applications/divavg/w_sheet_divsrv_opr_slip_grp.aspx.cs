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
    public partial class w_sheet_divsrv_opr_slip_grp : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable

        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_opr_slip_grp.pbl";
        protected String postNewClear;
        protected String postInit;
        protected String postRefresh;
        protected String postSetMoneytype;
        public String outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_main, this);
            //tDw_option.Add("operate_date", "operate_tdate");
            tDw_option.Add("slip_date", "slip_tdate");
            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postSetMoneytype = WebUtil.JsPostBack(this, "postSetMoneytype");
        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";

            if (!IsPostBack)
            {
                this.ConnectSQLCA();
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_report);
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
                case "postSetMoneytype":
                    JspostSetMoneytype();
                    break;
            }
        }

        //function บันทึกข้อมูล
        public void SaveWebSheet()
        {
            try
            {
                //DivavgService = wcf.NDivavg;
                //str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                //astr_divavg_oper.xml_option = Dw_main.Describe("DataWindow.Data.XML");
                //int result = DivavgService.of_save_slip_grp(state.SsWsPass, ref astr_divavg_oper);
                //if (result == 1)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                //    JspostNewClear();
                //}

                //DivavgService = wcf.NDivavg;
                //str_divsrv_oper astr_divsrv_oper = new str_divsrv_oper();
                //astr_divsrv_oper.xml_option = Dw_main.Describe("DataWindow.Data.XML");
                //DivavgService.of_save_slip_grp(state.SsWsPass, ref astr_divsrv_oper);
                //Hd_process.Value = "true";
                string xml_option = Dw_main.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "DIVPROCDIVPAY", xml_option, "", "");

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
            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_option.Eng2ThaiAllRow();

            Dw_main.SaveDataCache();

            Dw_report.SaveDataCache();
        }
        #endregion

        // function InitData
        private void JspostInit()
        {
            try
            {
                DivavgService = wcf.NDivavg;
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_option = Dw_main.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_slip_grp(state.SsWsPass, ref astr_divavg_oper);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_option, Dw_main, tDw_option, FileSaveAsType.Xml);
                    DwUtil.ImportData(astr_divavg_oper.sql_select_report, Dw_report, null);
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

            Dw_report.Reset();
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);

            //Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }

            Dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            
            // Retrieve DropDown
            DwUtil.RetrieveDDDW(Dw_main, "coop_id", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "methpaytype_code", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "smembtype_code_1", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "emembtype_code_1", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "smembgroup_code_1", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "emembgroup_code_1", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "tofrom_accid_1", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "moneytype_code", pbl, null);
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

        private void JspostSetMoneytype()
        {
            try
            {
                String methpaytype_code = Dw_main.GetItemString(1, "methpaytype_code");
                String moneytype_code = "";
                String sql = @"select join_moneytype_code from yrucfmethpay where methpaytype_code = '" + methpaytype_code + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    moneytype_code = dt.GetString("join_moneytype_code");
                    Dw_main.SetItemString(1, "moneytype_code", moneytype_code);
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}