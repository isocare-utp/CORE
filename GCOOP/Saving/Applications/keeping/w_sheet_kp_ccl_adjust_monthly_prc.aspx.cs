using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNKeeping;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_ccl_adjust_monthly_prc : PageWebSheet, WebSheet
    {
        public String pbl = "kp_ccl_adjust_monthly_prc.pbl";
        private n_keepingClient keepingService;
        private DwThDate tDw_main;
        protected String postInit;
        protected String postNewClear;
        protected String postRefresh;
        //========================

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            //======================
         //   DwUtil.RetrieveDDDW(Dw_main, "proc_type", pbl, null);
            //DwUtil.RetrieveDDDW(Dw_main, "receive_month", pbl, null);
            //======================
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            Hdprocess.Value = "false";
            try
            {
                keepingService = wcf.NKeeping;
            }
            catch { LtServerMessage.Text = WebUtil.CompleteMessage("ติดต่อ Service ไม่ได้"); }

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main, tDw_main);
                this.RestoreContextDw(Dw_detail);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInit")
            {
                JspostInit();
            }
          
            else if (eventArg == "postRefresh")
            {

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String xml_option = Dw_main.Describe("Datawindow.Data.Xml");
                String xml_report_summary = Dw_detail.Describe("Datawindow.Data.Xml");

                str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                astr_keep_adjust.xml_option = xml_option;
                astr_keep_adjust.xml_report_summary = xml_report_summary;
                keepingService.RunKpAdjustMonthlyPrc(state.SsWsPass, astr_keep_adjust, state.SsApplication, state.CurrentPage);
                Hdprocess.Value = "true";
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);

            }

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        private void JspostInit()
        {
            try
            {
                String xml_option = Dw_main.Describe("Datawindow.Data.Xml");
                String xml_report_summary = Dw_detail.Describe("Datawindow.Data.Xml");
                Decimal receive_year = Dw_main.GetItemDecimal(1,"receive_year");
                Decimal receive_month = Dw_main.GetItemDecimal(1,"receive_month");
                String recvperiod = receive_year.ToString() + receive_month.ToString();
                str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                astr_keep_adjust.xml_option = xml_option;
                astr_keep_adjust.xml_report_summary = xml_report_summary;
                

              //  dw_data.Modify("recv_shrstatus.Protect=1");

                int result = keepingService.of_init_adjust_monthly_prc_option(state.SsWsPass, ref astr_keep_adjust);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_keep_adjust.xml_option, Dw_main, null, FileSaveAsType.Xml);
                    tDw_main.Eng2ThaiAllRow();

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_keep_adjust.xml_report_summary, Dw_detail, null, FileSaveAsType.Xml);
                    Dw_detail.Modify("t_recvperiod.text = " + recvperiod);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
            tDw_main.Eng2ThaiAllRow();
            String month = Convert.ToString(DateTime.Now.Month);
            if (month.Length != 2)
            {
                month = "0" + month.ToString();
            }
            Dw_main.SetItemDecimal(1, "receive_year",Convert.ToDecimal(DateTime.Now.Year + 543));
            Dw_main.SetItemDecimal(1, "receive_month",Convert.ToDecimal(month));
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_detail.Reset();

            Dw_detail.Visible = false;
        }
        //========================

       
    }
}