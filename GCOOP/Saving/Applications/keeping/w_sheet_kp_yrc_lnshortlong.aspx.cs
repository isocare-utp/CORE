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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;
using System.Web.Services.Protocols;
using System.Globalization;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_yrc_lnshortlong : PageWebSheet, WebSheet
    {
        #region WebSheet Members

        protected String jsProcess;
        protected String clearProcess;
        protected String postSetDate;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwcriteria;
        public string outputProcess;

        public void InitJsPostBack()
        {
            jsProcess = WebUtil.JsPostBack(this, "jsProcess");
            clearProcess = WebUtil.JsPostBack(this, "clearProcess");
            postSetDate = WebUtil.JsPostBack(this, "postSetDate");
            tdwcriteria = new DwThDate(dw_criteria, this);
            tdwcriteria.Add("acc_start", "acc_tstart");
            tdwcriteria.Add("acc_end", "acc_tend");
        }

        public void WebSheetLoadBegin()
        {
            HdRunProcess.Value = "false";
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {
                try
                {
                    dw_criteria.RestoreContext();
                }
                catch { }
            }
            else
            {
                int li_year = 0;
                if (dw_criteria.RowCount < 1)
                {
                    dw_criteria.InsertRow(0);
                    dw_criteria.SetItemDecimal(1, "acc_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
                    dw_criteria.SetItemDateTime(1, "acc_start", state.SsWorkDate);
                    dw_criteria.SetItemDateTime(1, "acc_end", state.SsWorkDate);
                }
                li_year = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "acc_year"));
                string ls_sql = @"select closeyear_status 
                from amappstatus 
                where coop_id = {0}
                and application = {1}";
                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, state.SsApplication);
                Sdt dt = WebUtil.QuerySdt(ls_sql);
                if (dt.Next())
                {
                    int li_clsyrstatus = dt.GetInt32("closeyear_status");
                    if (li_clsyrstatus == 0)
                    {
                        dw_criteria.SetItemDecimal(1, "acc_year", li_year);

                        ls_sql = @"select accstart_date, accend_date
                        from cmaccountyear
                        where coop_id = {0}
                        and account_year = {1}";
                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, li_year);
                        Sdt dt1 = WebUtil.QuerySdt(ls_sql);
                        if (dt1.Next())
                        {
                            dw_criteria.SetItemDateTime(1, "acc_start", dt1.GetDate("accstart_date"));
                            dw_criteria.SetItemDateTime(1, "acc_end", dt1.GetDate("accend_date"));
                        }
                    }
                }
                tdwcriteria.Eng2ThaiAllRow();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsProcess")
            {
                JsProcess();
            }
            else if (eventArg == "clearProcess")
            {
                JsClearProcess();
            }
            else if (eventArg == "postSetDate")
            {
                int li_month = 0;
                int li_year = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "acc_year")) - 543;
                DateTime accstart_date = dw_criteria.GetItemDateTime(1, "acc_start");
                DateTime accend_date = dw_criteria.GetItemDateTime(1, "acc_end");
                try
                {
                    li_month = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "acc_month")) + 12;
                    if (li_month >= 13)
                    {
                        li_month -= 12;
                        li_year += 1;
                    }
                }
                catch
                {
                    li_month = accstart_date.Month;
                }
                int day1 = DateTime.DaysInMonth(li_year - 1, li_month + 1);
                int day2 = DateTime.DaysInMonth(li_year, li_month);
                DateTime ldt_accstart = new DateTime(li_year - 1, li_month + 1, 1);
                DateTime ldt_accend = new DateTime(li_year, li_month, day2);
                dw_criteria.SetItemDateTime(1, "acc_start", ldt_accstart);
                dw_criteria.SetItemDateTime(1, "acc_end", ldt_accend);
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            if (dw_criteria.RowCount > 1)
            {
                dw_criteria.DeleteRow(dw_criteria.RowCount);
            }
            tdwcriteria.Eng2ThaiAllRow();
        }

        private void JsProcess()
        {
            //try
            //{
            //    String entry_id = state.SsUsername;
            //    String ls_xmldwcriteria = dw_criteria.Describe("DataWindow.Data.XML");
            //    shrlonService.RunLnShotLongProcess(state.SsWsPass, ls_xmldwcriteria, entry_id, state.SsApplication, state.CurrentPage);//RunLnShotLongProcess
            //    HdRunProcess.Value = "true";
            //}
            //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            try
            {
                string option_xml = dw_criteria.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "LNSHORTLONG", option_xml, "", "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
        }

        private void JsClearProcess()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDecimal(1, "acc_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
        }

        #endregion
    }
}
