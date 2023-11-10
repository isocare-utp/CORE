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
using System.Data.OracleClient;
using System.Globalization;
using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_dlg_dp_dayproc_wizard_new : PageWebSheet, WebSheet
    {
        // JavaSctipt PostBack
        protected String postCloseDay;
        private DwThDate tdw_closeday;
        public string outputProcess;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCloseDay = WebUtil.JsPostBack(this, "postCloseDay");
            tdw_closeday = new DwThDate(Dw_date, this);
            tdw_closeday.Add("proc_date", "proc_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdCloseday.Value = "false";
            if (!IsPostBack)
            {
                Dw_date.InsertRow(0);
                Dw_date.SetItemDate(1, "proc_date", state.SsWorkDate);
                tdw_closeday.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_date);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseDay")
            {
                JsPostCloseDay();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            Dw_date.SaveDataCache();
        }

        #endregion

        private void JsPostCloseDay()
        {
            n_depositClient depService = wcf.NDeposit;
            DateTime closeDate = new DateTime(1370, 1, 1);
            try
            {
                closeDate = Dw_date.GetItemDateTime(1, "proc_date");
            }
            catch { }
            try
            {
                //depService.RunCloseDayProcess(state.SsWsPass, state.CurrentPage, closeDate, state.SsWorkDate, state.SsApplication,state.SsCoopControl, state.SsUsername, state.SsClientIp);
                //HdCloseday.Value = "true";                
                int ln_ckcloseday = 0;
                string sql = @"select count(*) as c_row from dpdepttran 
				inner join dpdeptmaster on dpdepttran.deptaccount_no = dpdeptmaster.deptaccount_no
				where dpdepttran.coop_id = {0} and tran_Date = {1} and tran_status=0 and dpdepttran.deptitem_Amt>0";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, state.SsWorkDate);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ln_ckcloseday = dt.GetInt32("c_row");
                }
                if (ln_ckcloseday == 0)
                {
                    outputProcess = WebUtil.runProcessing(state, "DPCLSDAY", closeDate.ToString("dd/MM/yyyy"), state.SsClientIp, "");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ค้างรายการที่ต้องผ่านรายการที่ >>หน้าจอประมวลฝากจากระบบอื่น<<!!!");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}