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


namespace Saving.Applications.app_finance
{
    public partial class w_sheet_fin_cashflowmas : PageWebSheet, WebSheet
    {
        // JavaSctipt PostBack
        protected String postCloseDay;
        private DwThDate tdw_closeday;



        public void InitJsPostBack()
        {
            postCloseDay = WebUtil.JsPostBack(this, "postCloseDay");
            tdw_closeday = new DwThDate(Dw_date, this);
            tdw_closeday.Add("select_date", "start_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            if (!IsPostBack)
            {
                Dw_date.InsertRow(0);
                Dw_date.SetItemDate(1, "select_date", state.SsWorkDate);
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



        private void JsPostCloseDay()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            String start_date;
            String start_date_tt = "";
            Decimal cashflow = 0;
            try
            {
                cashflow = Dw_date.GetItemDecimal(1, "cashflow");
                start_date = Dw_date.GetItemString(1, "start_tdate");
                String entry_year = WebUtil.Right(start_date, 4);
                int yyyy = Convert.ToInt32(entry_year) - 543;
                String entry_day = WebUtil.Left(start_date, 4);
                //String entry_tt = entry_day + year.ToString();
                String dd = WebUtil.Left(entry_day, 2);
                String mm = WebUtil.Right(entry_day, 2);
                start_date_tt = dd + "/" + mm + "/" + yyyy.ToString();

            }
            catch { }
            try
            {

                string sqlStr = @" UPDATE FINCASHFLOWMAS  
                                     SET CASH_FOWARD = " + cashflow + @"
                                   WHERE FINCASHFLOWMAS.OPERATE_DATE =  to_date ('" + start_date_tt + @"' , 'dd/mm/yyyy')";
                ta.Exe(sqlStr);
                  LtServerMessage.Text = WebUtil.CompleteMessage("ตั้งยอดยกไป    " + cashflow + "   เรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}