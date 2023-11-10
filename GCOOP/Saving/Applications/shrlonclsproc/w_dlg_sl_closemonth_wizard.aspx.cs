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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;

namespace Saving.Applications.shrlonclsproc
{
    public partial class w_dlg_sl_closemonth_wizard : PageWebSheet, WebSheet
    {

        
        #region WebSheet Members

        protected String postCloseMonth;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String clearProcess;

        public void InitJsPostBack()
        {
            postCloseMonth = WebUtil.JsPostBack(this, "postCloseMonth");
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
            if (dw_criteria.RowCount < 1)
            {
                Int16 closeMonth = Convert.ToInt16(workDate.Month);
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDecimal(1, "close_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
                dw_criteria.SetItemDecimal(1, "close_month", closeMonth);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseMonth")
            {
                JsPostCloseMonth();
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
        }

        private void JsPostCloseMonth()
        {
            try
            {

                short ai_year = (short)Convert.ToInt16(dw_criteria.GetItemString(1, "close_year"));
                short ai_month = (short)Convert.ToInt16(dw_criteria.GetItemString(1, "close_month"));
                String as_appname = "shrlon";
                String as_userid = state.SsUsername;
                //shrlonService.RunClosemonthProcess(state.SsWsPass, ai_year, ai_month, as_appname, as_userid, state.SsApplication, state.CurrentPage);
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
            Int16 closeMonth = Convert.ToInt16(workDate.Month);
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDecimal(1, "close_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
            dw_criteria.SetItemDecimal(1, "close_month", closeMonth);
        }

        #endregion
    }
}
