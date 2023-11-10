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
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlonclsproc
{
    public partial class w_dlg_sl_closeday_wizard : PageWebSheet, WebSheet
    {

        protected String postCloseDay;
        private DwThDate tdw_closeday;
        protected String clearProcess;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCloseDay = WebUtil.JsPostBack(this, "postCloseDay");
            tdw_closeday = new DwThDate(dw_criteria, this);
            tdw_closeday.Add("closeday_date", "closeday_tdate");
            clearProcess = WebUtil.JsPostBack(this, "clearProcess");
        }

        public void WebSheetLoadBegin()
        {
            HdRunProcess.Value = "false";
            this.ConnectSQLCA();
            Sta ta = new Sta(sqlca.ConnectionString);
            DateTime workDate = new DateTime();
            try
            {
                String sql = @"SELECT WORKDATE,CLOSEDAY_STATUS FROM AMAPPSTATUS WHERE APPLICATION = 'shrlon'";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    workDate = dt.GetDate("workdate");
                }
            }
            catch (Exception ex)
            {
                String err = ex.ToString();
            }
            ta.Close();
            if (!IsPostBack)
            {
                dw_criteria.InsertRow(0);
                //dw_criteria.SetItemDate(1, "closeday_date", state.SsWorkDate);
                dw_criteria.SetItemDate(1, "closeday_date", workDate);
                tdw_closeday.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(dw_criteria);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseDay")
            {
                JsPostCloseDay();
            }
            if (eventArg == "clearProcess")
            {
                JsClearProcess();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            tdw_closeday.Eng2ThaiAllRow();
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void JsPostCloseDay()
        {
            n_shrlonClient shrlonService = wcf.NShrlon;
            DateTime closeDate = new DateTime(1370, 1, 1);
            String appName = "shrlon";
            try
            {
                closeDate = dw_criteria.GetItemDateTime(1, "closeday_date");
            }
            catch { }
            try
            {
                //shrlonService.RunClosedayProcess(state.SsWsPass, closeDate, appName, state.SsUsername, state.SsApplication, state.CurrentPage);
                HdRunProcess.Value = "true";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsClearProcess()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            DateTime workDate = new DateTime();
            try
            {
                String sql = @"SELECT WORKDATE,CLOSEDAY_STATUS FROM AMAPPSTATUS WHERE APPLICATION = 'shrlon'";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    workDate = dt.GetDate("workdate");
                }
            }
            catch (Exception ex)
            {
                String err = ex.ToString();
            }
            ta.Close();
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDate(1, "closeday_date", workDate);
            tdw_closeday.Eng2ThaiAllRow();
        }

    }
}
