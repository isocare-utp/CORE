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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_mrc_receive_store_report : PageWebSheet, WebSheet
    {
        protected String postProcStatus;
        protected String getXml;
        protected String getAccid;
        private n_keepingClient kpService;
        private DwThDate tDwMain;
        private n_commonClient commonService;
        protected String chgProcDate;
        public string outputProcess;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postProcStatus = WebUtil.JsPostBack(this, "postProcStatus");
            getAccid = WebUtil.JsPostBack(this, "getAccid");
            getXml = WebUtil.JsPostBack(this, "getXml");
            chgProcDate = WebUtil.JsPostBack(this, "chgProcDate");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("receipt_date", "receipt_tdate");
            tDwMain.Add("calint_date", "calint_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdRunProcess.Value = "false";
            kpService = wcf.NKeeping;
            commonService = wcf.NCommon;
            if (!IsPostBack)
            {
                if (DwMain.RowCount < 1)
                {
                    DwMain.InsertRow(0);
                }
                DateTime last = commonService.of_lastdayofmonth(state.SsWsPass, state.SsWorkDate);
                DwMain.SetItemDecimal(1, "receive_year", last.Year + 543);
                //DwMain.SetItemDateTime(1, "receipt_date", last);
               // DwMain.SetItemDateTime(1, "calint_date", last);
                DwMain.SetItemDecimal(1, "receive_month", last.Month);
                //DwMain.SetItemDecimal(1, "receive_year",  state.SsWorkDate.Year + 543);
                //DwMain.SetItemDateTime(1, "receipt_date", state.SsWorkDate);
                //DwMain.SetItemDateTime(1, "calint_date", state.SsWorkDate);
                //DwMain.SetItemDecimal(1, "receive_month", state.SsWorkDate.Month);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                try
                {
                   
                    this.RestoreContextDw(DwMain);
                }
                catch { }
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postProcStatus")
            {

            }
            if (eventArg == "getXml")
            {
                String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
                //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
                CallWSRunRcvProcess(xml_tmp);
            }
            if (eventArg == "getAccid")
            {
                GetAccidToFrom();
            }
            if (eventArg == "chgProcDate")
            {
                JsChgProcDate();
            }
        }

        private void GetAccidToFrom()
        {
        //    DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "kp_recieve_store.pbl", null);
        //    DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "kp_recieve_store.pbl", null);
        //    DataWindowChild dc = DwMain.GetChild("tofrom_accid");
        //    String moneyType_code = DwMain.GetItemString(1, "moneytype_code");
        //    dc.SetFilter("moneytype_code ='" + moneyType_code + "'");
        //    dc.Filter();
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            try
            {
                GetAccidToFrom();
            }
            catch (Exception ex) { }
        }

        #endregion

        private void CallWSRunRcvProcess(string xml)
        {
            try
            {
                string recv_period = DwMain.GetItemDecimal(1, "receive_year").ToString() + DwMain.GetItemDecimal(1, "receive_month").ToString("00");
                //string year = DwMain.GetItemDecimal(1, "receive_year").ToString() + DwMain.GetItemDecimal(1, "receive_month").ToString(); 
                //kpService.RunRcvProcessReport(state.SsWsPass, xml, state.SsApplication, state.CurrentPage);
                //outputProcess = WebUtil.runProcessing(state, "KEEPGENREPORT", xml, "", "");
                outputProcess = WebUtil.runProcessing(state, "KEEPGENREPORT", state.SsCoopControl, recv_period, "");
                HdRunProcess.Value = "true";
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }
        }

      
        private void JsChgProcDate()
        {
            //String t_date = "01";
            //Decimal t_month = DwMain.GetItemDecimal(1, "receive_month");
            //Decimal t_year = (DwMain.GetItemDecimal(1, "receive_year") - 543);
            //String t_receive = t_month.ToString() + "/" + t_date + "/" + t_year.ToString();
            //DateTime endate = DateTime.Parse(t_receive);

            //try
            //{
            //    DateTime newdate = commonService.of_lastdayofmonth(state.SsWsPass, endate);
            //    DwMain.SetItemDate(1, "receipt_date", newdate);
            //    DwMain.SetItemDate(1, "calint_date", newdate);
            //    tDwMain.Eng2ThaiAllRow();

            //}
            //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }
    }
}
