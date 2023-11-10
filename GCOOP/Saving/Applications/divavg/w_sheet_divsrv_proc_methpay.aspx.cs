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
using CoreSavingLibrary;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_proc_methpay : PageWebSheet, WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_proc_methpay.pbl";
        protected String postNewClear;
        protected String postProcess;
        protected String postRefresh;
        public string outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_option, this);
            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postProcess = WebUtil.JsPostBack(this, "postProcess");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_option);
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
                case "postProcess":
                    JspostProcess();
                    break;
                case "postRefresh":
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            // Retrieve DropDown
            // DwUtil.RetrieveDDDW(Dw_option, "proc_type", pbl, null);

            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_option.Eng2ThaiAllRow();
            Dw_option.SaveDataCache();

        }
        #endregion
        //=============================================  

        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            JsGetYear();
        }
        
        private void JspostProcess()
        {
            try
            {
                DivavgService = wcf.NDivavg;
                //str_divsrv_proc astr_divsrv_proc = new str_divsrv_proc();
                //astr_divsrv_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //DivavgService.of_prc_methpay_opt(state.SsWsPass, ref astr_divsrv_proc);
                //DivavgService.RunRcvProcess(state.SsWsPass, ref astr_divsrv_proc, state.SsApplication, "w_sheet_divsrv_proc_methpay");
                //DivavgService.RunRcvProcess(state.SsWsPass, ref astr_divsrv_proc, state.SsApplication, state.CurrentPage);
                //Hd_process.Value = "true";
                string xml_option = Dw_option.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "YRDIVMETHODPAY", xml_option, "", "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
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
                    //account_year = int.Parse(dt.GetString("max(current_year)"));
                    account_year = dt.GetDecimal("current_year");
                    Dw_option.SetItemString(1, "div_year", Convert.ToString(account_year));
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
                Dw_option.SetItemString(1, "div_year", account_year.ToString());
            }
        }
    }
}