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
using DataLibrary;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_mrc_receive_store_recvnum : PageWebSheet, WebSheet
    {
        protected String postRecpnoStatus;
        protected String getXml;
        protected String getAccid;
        protected String postSetProtype;
        protected string postSetCalintDate;
        public string outputProcess;

        private n_keepingClient kpService;
        private DwThDate tDwMain;
        private n_commonClient commonService;
        protected String chgProcDate;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postRecpnoStatus = WebUtil.JsPostBack(this, "postRecpnoStatus");
            getAccid = WebUtil.JsPostBack(this, "getAccid");
            getXml = WebUtil.JsPostBack(this, "getXml");
            chgProcDate = WebUtil.JsPostBack(this, "chgProcDate");
            postSetProtype = WebUtil.JsPostBack(this, "postSetProtype");
            postSetCalintDate = WebUtil.JsPostBack(this, "postSetCalintDate");
            //====================
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("receipt_date", "receipt_tdate");
            tDwMain.Add("calint_date", "calint_tdate");
            tDwMain.Add("startcont_date", "startcont_tdate");
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
                DwMain.SetItemDecimal(1, "receive_month", last.Month);
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                DwMain.SetItemString(1, "entry_id", state.SsUsername);                                

                of_setprocessdate(last);
                JsRecpnoStatus();

                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwMain, tDwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRecpnoStatus")
            {
                JsRecpnoStatus();
            }
            else if (eventArg == "getXml")
            {
                String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
                //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
                CallWSRunRcvProcess(xml_tmp);
            }
            else if (eventArg == "getAccid")
            {
                GetAccidToFrom();
            }
            else if (eventArg == "chgProcDate")
            {
                JsChgProcDate();
            }
            else if (eventArg == "postSetProtype")
            {
                JspostSetProtype();
            }
            else if (eventArg == "postSetCalintDate")
            {
                DateTime receipt_date = DwMain.GetItemDateTime(1, "receipt_date");
                DwMain.SetItemDateTime(1, "calint_date", receipt_date);
                tDwMain.Eng2ThaiAllRow();
            }

        }

        private void JsRecpnoStatus()
        {
            Decimal recpno_status = DwMain.GetItemDecimal(1, "recpno_status");
            if (recpno_status == 1)
            {
                string sql = "select last_documentno from cmdocumentcontrol where coop_id = '" + state.SsCoopControl + "' and document_code = 'KPRECEIPTNO'";

                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    decimal last_documentno = dt.GetDecimal("last_documentno") + 1;
                    DwMain.SetItemDecimal(1, "recpno_startnum", last_documentno);
                }
            }
            else
            {
                DwMain.SetItemDecimal(1, "recpno_startnum", 1);
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
                //kpService.RunRcvProcess(state.SsWsPass, xml, state.SsApplication, state.CurrentPage);
                //HdRunProcess.Value = "true";
                outputProcess = WebUtil.runProcessing(state, "KEEPINGPROCESS", xml, "", "");

            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }
        }

        protected void cb_process_Click(object sender, EventArgs e)
        {


            //String calint_date = WebUtil.ConvertDateThaiToEng(DwMain, "calint_tdate", null);
            //DateTime calint = Convert.ToDateTime(calint_date);

            //DwMain.SetItemDateTime(1, "calint_date", calint);
            //String receipt_date = WebUtil.ConvertDateThaiToEng(DwMain, "receipt_tdate", null);
            //DateTime receipt = Convert.ToDateTime(receipt_date);

            //DwMain.SetItemDateTime(1, "receipt_date", receipt);

            String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
            //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
            CallWSRunRcvProcess(xml_tmp);

        }
        
        /// <summary>
        /// //ดึงวันที่ประมวลผล set วันที่ slip วันที่ calint 
        /// </summary>
        private void of_setprocessdate(DateTime adt_lastdate)
        {                      
            string ls_sql = @"select processdate, postingdate
                from amworkcalendar
                where coop_id = {0}
                and year = {1}
                and month = {2}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, adt_lastdate.Year + 543, adt_lastdate.Month);
            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                int li_post = dt.GetInt32("postingdate");
                int li_proc = dt.GetInt32("processdate");
                if (li_post > 0)
                {
                    DateTime ldt_post = new DateTime(adt_lastdate.Year, adt_lastdate.Month, li_post);
                    DwMain.SetItemDateTime(1, "receipt_date", ldt_post);
                    DwMain.SetItemDateTime(1, "calint_date", ldt_post);
                }
                else
                {
                    DwMain.SetItemDateTime(1, "receipt_date", adt_lastdate);
                    DwMain.SetItemDateTime(1, "calint_date", adt_lastdate);
                }

                if (li_proc > 0)
                {
                    DateTime ldt_proc = new DateTime(adt_lastdate.Year, adt_lastdate.Month, li_proc);
                    DwMain.SetItemDateTime(1, "startcont_date", ldt_proc);
                }
                else
                {
                    DwMain.SetItemDateTime(1, "startcont_date", state.SsWorkDate);
                }
            }
            else
            {
                DwMain.SetItemDateTime(1, "receipt_date", adt_lastdate);
                DwMain.SetItemDateTime(1, "calint_date", adt_lastdate);
                DwMain.SetItemDateTime(1, "startcont_date", state.SsWorkDate);
            }
        }

        private void JsChgProcDate()
        {
            String t_date = "01";
            Decimal t_month = DwMain.GetItemDecimal(1, "receive_month");
            Decimal t_year = (DwMain.GetItemDecimal(1, "receive_year") - 543);
            String t_receive = t_month.ToString() + "/" + t_date + "/" + t_year.ToString();
            DateTime endate = DateTime.Parse(t_receive);

            try
            {
                DateTime newdate = commonService.of_lastdayofmonth(state.SsWsPass, endate);
                of_setprocessdate(newdate);
                tDwMain.Eng2ThaiAllRow();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JspostSetProtype()
        {
            DwMain.SetItemString(1, "memb_text", "");
            DwMain.SetItemString(1, "group_text", "");
            DwMain.SetItemString(1, "mem_text", "");
        }
    }
}
