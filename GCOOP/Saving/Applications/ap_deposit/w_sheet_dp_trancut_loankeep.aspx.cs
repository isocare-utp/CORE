using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_trancut_loankeep : PageWebSheet, WebSheet
    {

        protected String PostCutProcess;
        protected String PostRetriveDepttrans;
        private DwThDate tdw_processday;
        protected str_procdeptupmonth strdps;
        private n_depositClient dpService;
        String YYMM = "";

        public void InitJsPostBack()
        {
            PostRetriveDepttrans = WebUtil.JsPostBack(this, "PostRetriveDepttrans");
            PostCutProcess = WebUtil.JsPostBack(this, "PostCutProcess");
            tdw_processday = new DwThDate(Dw_Main, this);
            tdw_processday.Add("adtm_operatedate", "adtm_tdate");

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_Main.SetTransaction(sqlca);
            Dw_Detail.SetTransaction(sqlca);

            dpService = wcf.NDeposit;

            if (!IsPostBack)
            {
                Dw_Main.InsertRow(0);
                Dw_Main.SetItemDateTime(1, "adtm_operatedate", state.SsWorkDate);
                DateTime adtm_operatedate = Dw_Main.GetItemDateTime(1, "adtm_operatedate");
                tdw_processday.Eng2ThaiAllRow();
                String year = adtm_operatedate.ToString("yyyy", new CultureInfo("th-TH"));
                int year2 = Convert.ToInt16(year);
                Dw_Main.SetItemDecimal(1, "ai_year", year2);
                String month = adtm_operatedate.ToString("MM", new CultureInfo("th-TH"));
                int month2 = Convert.ToInt16(month);
                Dw_Main.SetItemDecimal(1, "ai_month", month2);
            }
            else
            {
                this.RestoreContextDw(Dw_Main, tdw_processday);
                this.RestoreContextDw(Dw_Detail);
                String month = Dw_Main.GetItemDecimal(1, "ai_month").ToString("00");
                String year = Dw_Main.GetItemDecimal(1, "ai_year").ToString("0000");
                YYMM = year + month;


            }
        }

        public void CheckJsPostBack(string eventArg)
        {


            if (eventArg == "PostRetriveDepttrans")
            {
                JsPostRetriveDepttrans();
            }
            if (eventArg == "PostCutProcess")
            {
                JsPostCutProcess();
            }

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_Main.SaveDataCache();
            Dw_Detail.SaveDataCache();
        }


        private void JsPostRetriveDepttrans()
        {
            DwUtil.RetrieveDataWindow(Dw_Detail, "dp_depttrans.pbl", null, state.SsCoopControl, YYMM);
        }

        private void JsPostCutProcess()
        {
            String as_year = "";
            String as_month = "";
            DateTime adtm_pmondate = state.SsWorkDate;
            try
            {
                try
                {
                    as_year = (Dw_Main.GetItemDecimal(1, "ai_year")).ToString();
                }
                catch { as_year = ""; }
                try
                {
                    as_month = (Dw_Main.GetItemDecimal(1, "ai_month")).ToString("00");

                }
                catch { as_month = ""; }

                try
                {
                    adtm_pmondate = Dw_Main.GetItemDateTime(1, "adtm_operatedate");
                    tdw_processday.Eng2ThaiAllRow();
                }
                catch { LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุวันที่"); }


                int result = dpService.of_procdepttrancut_loankeep(state.SsWsPass, adtm_pmondate, state.SsUsername, state.SsClientIp, as_year + as_month, state.SsCoopId);

                Dw_Main.Reset();
                Dw_Detail.Reset();
                LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}