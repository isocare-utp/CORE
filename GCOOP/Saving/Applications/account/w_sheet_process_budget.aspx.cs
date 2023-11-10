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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เรียกใช้ service

namespace Saving.Applications.account
{
    public partial class w_sheet_process_budget : PageWebSheet, WebSheet
    {
        private n_accountClient accService;//ประกาศตัวแปร WebService
        protected String jsPostProcess;
        string pbl = "sumbudget_on_tks.pbl";

        #region WebSheet Members
        public void InitJsPostBack()
        {
            jsPostProcess = WebUtil.JsPostBack(this, "jsPostProcess");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            Dwmain.SetTransaction(sqlca);
            DwDetail.SetTransaction(sqlca);
            accService = wcf.NAccount;//ประกาศ new

            if (!IsPostBack)
            {
                Dwmain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostProcess":
                    Process();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            //try
            //{
            //    DwUtil.UpdateDataWindow(DwDetail, "sumbudget_on_tks.pbl", "accperiodsumbudget");
            //    //DwDetail.UpdateData();
            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //}
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(DwDetail, "accrcvpay", pbl, null);
            }
            catch { }

            Dwmain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
        #endregion

        #region Function
        private void Process()
        {
            try
            {
                string coop_id = state.SsCoopId;
                int year = Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543;
                short period = Convert.ToInt16(Dwmain.GetItemString(1, "period"));
                int result = accService.of_process_budget(state.SsWsPass, coop_id, (short)year, period);
                //int result = wcf.NAccount.of_process_budget(state.SsWsPass, coop_id, (short)year, period);

                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลการเบิกจ่ายงบประมาณเสร็จเรียบร้อย");

                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, year, period);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถประมวลการเบิกจ่ายงบประมาณ");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        #endregion
    }
}