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
    public partial class w_sheet_kp_mrc_mr_opr_proc : PageWebSheet, WebSheet
    {
        public String pbl = "kp_mrc_mr_opr_proc.pbl";
        private n_keepingClient keepingService;
        private DwThDate tDw_main;

        protected String postInit;
        protected String postNewClear;
        protected String postSetDate;
        //========================

        

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postSetDate = WebUtil.JsPostBack(this, "postSetDate");
            //================
            // DwUtil.RetrieveDDDW(Dw_main, "proc_type", pbl, null);
            //======================
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("operate_date", "operate_tdate");
            tDw_main.Add("slip_date", "slip_tdate");
            tDw_main.Add("mrcreate_sdate", "mrcreate_stdate");
            tDw_main.Add("mrcreate_edate", "mrcreate_etdate");
            
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
            else if (eventArg == "postSetDate")
            {
                JspostSetDate();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String xml_option = Dw_main.Describe("Datawindow.Data.Xml");
                String xml_report_summary = Dw_detail.Describe("Datawindow.Data.Xml");

                str_money_return_xml astr_xml = new str_money_return_xml();
                astr_xml.xml_option = xml_option;
                astr_xml.xml_report_summary = xml_report_summary;
                keepingService.RunMoneyReturn(state.SsWsPass, ref astr_xml, state.SsApplication, state.CurrentPage);
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

                str_money_return_xml astr_return_xml = new str_money_return_xml();
                astr_return_xml.xml_option = xml_option;
                astr_return_xml.xml_report_summary = xml_report_summary;
                
                int result = keepingService.of_init_mr_opr_proc(state.SsWsPass, ref astr_return_xml);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_return_xml.xml_option, Dw_main, null, FileSaveAsType.Xml);
                    tDw_main.Eng2ThaiAllRow();

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_return_xml.xml_report_summary, Dw_detail, null, FileSaveAsType.Xml);
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
            Dw_main.SetItemDate(1, "slip_date", state.SsWorkDate);
            tDw_main.Eng2ThaiAllRow();

            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_detail.Reset();
        }
        //========================

        private void JspostSetDate()
        {
            try 
            {
                Dw_main.SetItemDate(1, "mrcreate_sdate", state.SsWorkDate);
                Dw_main.SetItemDate(1, "mrcreate_edate", state.SsWorkDate);
                tDw_main.Eng2ThaiAllRow();

                string ls_recvperiod = "";
                String sql = @"select max(recv_period) from kpmastreceive";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_recvperiod = dt.GetString("max(recv_period)");
                    Dw_main.SetItemString(1, "recv_period",ls_recvperiod);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}