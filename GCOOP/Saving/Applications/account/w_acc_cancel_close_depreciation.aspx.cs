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
//เพิ่มเข้ามา
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
using CoreSavingLibrary.WcfNAccount;
using DataLibrary;
using System.Web.Services.Protocols;


namespace Saving.Applications.account
{
    public partial class w_acc_cancel_close_depreciation : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_main;
        private n_accountClient accService;//ประกาศตัวแปร WebService
        protected String postCancelCloseYear;
        protected String postNewClear;
        protected String postSetDateStartEnd;
        public int account_year;
        //=================================
        private void JspostSetStartEndDate()
        {
            //int year = int.Parse(Dw_main.GetItemString(1, "account_year_t"));
            //DateTime starDate = new DateTime(year - 543, 1, 1);
            //DateTime endDate = new DateTime(year - 543, 12, 31);

            //Dw_main.SetItemDateTime(1, "begin_acc", starDate);
            //Dw_main.SetItemDateTime(1, "end_acc", endDate);
            //tdw_main.Eng2ThaiAllRow();
            DateTime starDate = state.SsWorkDate;
            DateTime endDate = state.SsWorkDate;
            int year = int.Parse(Dw_main.GetItemString(1, "account_year_t"));
            string sqlselectaccyear = "select beginning_of_accou, ending_of_account from accaccountyear where account_year = '" + (year - 543) + "'";
            Sdt accyear = WebUtil.QuerySdt(sqlselectaccyear);
            if (accyear.Next())
            {
                starDate = accyear.GetDate("beginning_of_accou");
                endDate = accyear.GetDate("ending_of_account");
            }
            //DateTime starDate = new DateTime(year - 543, 4, 1);
            //DateTime endDate = new DateTime(year - 542, 3, 31);

            Dw_main.SetItemDateTime(1, "begin_acc", starDate);
            Dw_main.SetItemDateTime(1, "end_acc", endDate);
            tdw_main.Eng2ThaiAllRow();
        }

        private void JspostNewClear()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = @"select max(account_year) from accaccountyear where close_account_stat = 1";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(account_year)"));
                    account_year = account_year + 543;
                    Dw_main.SetItemDecimal(1, "account_year_t", Convert.ToDecimal(account_year));
                    JspostSetStartEndDate();
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostCancelCloseYear()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                String account_year_t = Dw_main.GetItemString(1, "account_year_t");
                int accyear = int.Parse(account_year_t) - 543;
                String wsPass = state.SsWsPass;

                //เรียกใช้ webservice
                //int result = accService.CancelCloseYear(wsPass, Convert.ToInt16(accyear), state.SsCoopId);
                int result = wcf.NAccount.of_cancel_closeyear(wsPass, Convert.ToInt16(accyear));

                HdIsFinished.Value = "true";
                if (result != 1)
                {
                    throw new Exception("ไม่สามารถยกเลิกปิดสิ้นปีบัญชีได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกปิดสิ้นปีบัญชีเรียบร้อยแล้ว");
                }

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        //=================================
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCancelCloseYear = WebUtil.JsPostBack(this, "postCancelCloseYear");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postSetDateStartEnd = WebUtil.JsPostBack(this, "postSetDateStartEnd");
            //======================================
            tdw_main = new DwThDate(Dw_main, this);
            tdw_main.Add("begin_acc", "begin_acc_tdate");
            tdw_main.Add("end_acc", "end_acc_tdate");
            tdw_main.Eng2ThaiAllRow();
        }

        public void WebSheetLoadBegin()
        {

            this.ConnectSQLCA();

            th = new CultureInfo("th-TH");
            Dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_main.Retrieve(state.SsCoopId);
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCancelCloseYear")
            {
                JspostCancelCloseYear();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postSetDateStartEnd")
            {
                JspostSetStartEndDate();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);
            }
            Dw_main.SaveDataCache();
        }

        #endregion


    }
}
