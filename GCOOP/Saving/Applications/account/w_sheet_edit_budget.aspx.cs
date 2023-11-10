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
    public partial class w_sheet_edit_budget : PageWebSheet, WebSheet
    {
        private n_accountClient accService;//ประกาศตัวแปร WebService
        protected String jsPostProcess;
        protected String postEditBudget;
        string pbl = "sumbudget_on_tks.pbl";

        #region WebSheet Members
        public void InitJsPostBack()
        {
            jsPostProcess = WebUtil.JsPostBack(this, "jsPostProcess");
            postEditBudget = WebUtil.JsPostBack(this, "postEditBudget");
        }

         private void EditBudget()
         {
            Int16 row = Convert.ToInt16(Hd_row.Value);
            Decimal account_budget = DwDetail.GetItemDecimal(row, "account_budget");
            decimal period = Convert.ToDecimal(Dwmain.GetItemString(1, "period"));
             decimal estimate = (account_budget / 12) * period;

             DwDetail.SetItemDecimal(row, "account_estimate", estimate);
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
            //switch (eventArg)
            //{
            //    case "jsPostProcess":
            //        Process();
            //        break;
            //}
            if (eventArg == "jsPostProcess")
            {
                Process();
            }
            else if (eventArg == "postEditBudget")
            {
                EditBudget();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DwUtil.UpdateDataWindow(DwDetail, "sumbudget_on_tks.pbl", "accperiodsumbudget");
                //DwDetail.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            //try
            //{
            //    DwUtil.RetrieveDDDW(DwDetail, "accrcvpay", pbl, null);
            //}
            //catch { }

            Dwmain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
        #endregion

        #region Function
        private void Process()
        {
            int year = Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543;
            short period = Convert.ToInt16(Dwmain.GetItemString(1, "period"));

            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, year, period,state.SsCoopControl);
        }
        #endregion
    }
}