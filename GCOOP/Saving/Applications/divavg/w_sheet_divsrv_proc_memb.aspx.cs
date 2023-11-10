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
    public partial class w_sheet_divsrv_proc_memb : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_proc_memb.pbl";
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcMember;
        protected String postSetAccDate;
        public string outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_option, this);
            tDw_option.Add("operate_date", "operate_tdate");
            tDw_option.Add("eacc_date", "eacc_tdate");
            tDw_option.Add("sacc_date", "sacc_tdate");
            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcMember = WebUtil.JsPostBack(this, "postProcMember");
            postSetAccDate = WebUtil.JsPostBack(this, "postSetAccDate");
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
                case "postRefresh":
                    //Refresh();
                    break;
                case "postProcMember":
                    JspostProcMember();
                    break;
                case "postSetAccDate":
                    JspostSetAccDate();
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

        //function การหาช่วงวันที่จากข้อมุลปีปันผล
        private void JspostSetAccDate()
        {
            String ls_divyear, ls_sacctdate, ls_eacctdate;
            int li_divyear;
            ls_divyear = Dw_option.GetItemString(1, "div_year");
            ls_divyear = ls_divyear.Substring(0, 4);
            li_divyear = int.Parse(ls_divyear) - 543;

            try
            {
                String sql = @"select beginning_of_accou,ending_of_account from accaccountyear where account_year = '" + li_divyear.ToString() + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_sacctdate = dt.GetString("beginning_of_accou");
                    ls_eacctdate = dt.GetString("ending_of_account");
                    Dw_option.SetItemDateTime(1, "sacc_date", Convert.ToDateTime(ls_sacctdate));
                    Dw_option.SetItemDateTime(1, "eacc_date", Convert.ToDateTime(ls_eacctdate));
                    tDw_option.Eng2ThaiAllRow();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลปีบัญชี : " + ls_divyear);
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

       

        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "eacc_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "sacc_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            JsGetYear();
        }

        // function ประมาณผลปันผลเฉลี่ยคืน
        private void JspostProcMember()
        {
            try
            {
                DivavgService = wcf.NDivavg;
                //str_divsrv_proc astr_divsrv_proc = new str_divsrv_proc();
                //astr_divsrv_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //DivavgService.RunDivavgProcMemb(state.SsWsPass, ref astr_divsrv_proc, state.SsApplication, state.CurrentPage);
                //Hd_process.Value = "true";
                string xml_option = Dw_option.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "DIVPROCESSMEM", xml_option, "", "");


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
                    JspostSetAccDate();
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