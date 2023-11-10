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
using CoreSavingLibrary.WcfNKeeping;
using System.Web.Services.Protocols;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_yrc_slcls_year_shr : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
       private n_keepingClient KeepingService;
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcClsYear;
        
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcClsYear = WebUtil.JsPostBack(this, "postProcClsYear");
        }

        public void WebSheetLoadBegin()
        {
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
                case "postProcClsYear":
                    JspostProcClsYear();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_option.SaveDataCache();
        }
        #endregion
        //=============================================

        //function การหาปีปันผล จากตารางปีบัญชี
        private void JspostSetYear()
        {
            try
            {
                int li_year = 0;
                String sql = @"select min(account_year) from accaccountyear where close_account_stat = 0";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    li_year = int.Parse(dt.GetString("min(account_year)")) + 543;
                    Dw_option.SetItemDecimal(1, "clsyr_year", Convert.ToDecimal(li_year));
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
        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entryby_coopid", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            JspostSetYear();
        }

        // function ประมาณผลปันผลเฉลี่ยคืน
        private void JspostProcClsYear()
        {
            try
            {
                KeepingService = wcf.NKeeping;
                str_slcls_proc astr_slcls_proc = new str_slcls_proc();
                astr_slcls_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                KeepingService.RunKpSlClsYearShrProcess(state.SsWsPass, astr_slcls_proc, state.SsApplication, state.CurrentPage);
                Hd_process.Value = "true";
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
    }
}