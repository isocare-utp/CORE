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
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;
using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_closemonth : PageWebSheet,WebSheet
    {
        private DwThDate tDwMain;
        private n_depositClient depService;
        protected string CloseMonth;

        private void JsCloseMonth()
        {
            short month = Convert.ToInt16(dw_main.GetItemDecimal(1, "proc_month"));
            short year = Convert.ToInt16(dw_main.GetItemDecimal(1, "proc_tyear"));
            year -= 543;
            try
            {
                bool isComplete = depService.of_close_month(state.SsWsPass, state.SsWorkDate, state.SsApplication, month, year, state.SsCoopId, state.SsClientIp);
                if (isComplete)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อยแล้ว");
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            CloseMonth = WebUtil.JsPostBack(this, "CloseMonth");

            tDwMain = new DwThDate(dw_main);
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);

            try
            {
                depService = wcf.NDeposit;
            }
            catch
            { }

            if (!IsPostBack)
            {
                int month = state.SsWorkDate.Month;
                int year = state.SsWorkDate.Year + 543;
                dw_main.InsertRow(0);
                dw_main.SetItemDecimal(1, "proc_tyear", year);
                dw_main.SetItemDecimal(1, "proc_month", month);
                dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();

            }
            else
            {
                this.RestoreContextDw(dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "CloseMonth")
            {
                JsCloseMonth();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
        }

        #endregion
    }
}
