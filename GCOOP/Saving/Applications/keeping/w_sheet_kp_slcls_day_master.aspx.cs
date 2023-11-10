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
using System.Globalization;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_slcls_day_master : PageWebSheet, WebSheet
    {
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        //ประกาศตัวแปร
        #region Variable
        private n_keepingClient KeepingService;
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcClsDay;
        private DwThDate tDwOption;

        //mikekong new process
        public string outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            tDwOption = new DwThDate(Dw_option, this);
            tDwOption.Add("clsday_date", "clsday_tdate");

            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcClsDay = WebUtil.JsPostBack(this, "postProcClsDay");
        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
                JspostNewClear();
                of_checkclsday();
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
                case "postProcClsDay":
                    JspostProcClsDay();
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
        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemDate(1, "clsday_date", state.SsWorkDate);
            tDwOption.Eng2ThaiAllRow();
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            Dw_option.SetItemString(1, "entryby_coopid", state.SsCoopId);
            Dw_option.SetItemDate(1, "entry_date", DateTime.Now);
            Dw_option.SetItemString(1, "application", state.SsApplication);
        }


        private void JspostProcClsDay()
        {
            DateTime ldtm_clsdate = Dw_option.GetItemDateTime(1, "clsday_date");
            DateTime ldtm_workdate = state.SsWorkDate;
            if (ldtm_clsdate != ldtm_workdate)
            {
                this.SetOnLoadedScript("alert('วันที่ปิดสิ้นวันไม่ใช่วันทำการ  \\nวันที่ทำรายการ : " + ldtm_workdate.ToString("dd/MM/yyyy", th) + "\\nกรุณาตรวจสอบ')");
                return;
            }

            try
            {
                //KeepingService = wcf.NKeeping;
                //str_slcls_proc astr_slcls_proc = new str_slcls_proc();
                //astr_slcls_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //KeepingService.RunKpSlClsDayProcess(state.SsWsPass, ref astr_slcls_proc, state.SsApplication, state.CurrentPage);//RunKpSlClsDayProcess
                //Hd_process.Value = "true";

                //mikekong new process
                string option_xml = Dw_option.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "LNCLOSEDAYMAS", option_xml, "", "");
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

        //เช็คปิดวัน
        public void of_checkclsday()
        {
            string ls_closedayid = "";
            DateTime ldtm_clsdate = Dw_option.GetItemDateTime(1, "clsday_date");
            DateTime ldtm_workdate = state.SsWorkDate;
            string sql = @"select closeday_id , workdate
                from amappstatus
                where coop_id = {0}
                and application = {1}
                and closeday_status = 1";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, state.SsApplication);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                ls_closedayid = dt.GetString("closeday_id");
                ldtm_workdate = dt.GetDate("workdate");

                this.SetOnLoadedScript("alert('ระบบปิดสิ้นวันไปเรียบร้อยแล้ว \\nผู้ทำรายการ : " + ls_closedayid + " \\nวันที่ทำรายการ : " + ldtm_workdate.ToString("dd/MM/yyyy", th) + "\\nกรุณาตรวจสอบ')");
                return;
            }
        }
    }
}