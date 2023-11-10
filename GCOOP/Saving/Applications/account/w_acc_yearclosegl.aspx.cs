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
using CoreSavingLibrary.WcfNAccount;  //เพิ่มเข้ามา
using Sybase.DataWindow;  //เพิ่มเข้ามา
using System.Globalization;  //เพิ่มเข้ามา
using System.Data.OracleClient;  //เพิ่มเข้ามา
using DataLibrary;
using System.Web.Services.Protocols;


namespace Saving.Applications.account
{
    public partial class w_acc_yearclosegl : PageWebSheet, WebSheet
    {

        private CultureInfo th;
        private DwThDate tdw_begin;
        private n_accountClient accService;//ประกาศตัวแปร WebService
        //=======================
        protected  String postCloseYear;
        protected String postNewClear;

        //===========================
        //ค้างไว้ก่อน
        private void JspostBeginEndDate()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
               // Int16 accperiod = 0;
                int account_year = DateTime.Now.Year;
                String sql = @"select Beginning_of_accou, ending_of_account, present_account_ye from accconstant  where  coop_id ='" + state.SsCoopId + "'";
                    
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("present_account_ye"));
                    account_year = account_year + 543;
                   // account_year = account_year + 543;
                    DateTime begin_date = dt.GetDate("Beginning_of_accou");
                    DateTime end_date = dt.GetDate("ending_of_account");
                    Dw_main.SetItemDate(1, "begin_acc", begin_date);
                    Dw_main.SetItemString(1, "begin_acc_tdate",begin_date.ToString("ddMMyyyy", new CultureInfo ("th-TH")));

                    Dw_main.SetItemDate(1, "end_acc", end_date);
                    Dw_main.SetItemString(1, "end_acc_tdate", end_date.ToString("ddMMyyyy", new CultureInfo("th-TH")));

                    Dw_main.SetItemDecimal(1, "account_year_t", Convert.ToDecimal(account_year));

                }
                else
                {
                      LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลวันเริ่มต้นรอบบัญชีและวันสิ้นสุดรอบบัญชี ");
                      sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            ta.Close();
        }

        private void JspostCloseYear()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                n_accountClient accService = wcf.NAccount;
                Decimal accyear = Dw_main.GetItemDecimal(1, "present_account_ye");
                String wsPass = state.SsWsPass;
                String coop_id = state.SsCoopId;

                //เรียกใช้ webservice
                //int result = accService.CloseYear(wsPass, Convert.ToInt16(accyear), coop_id);
                int result = wcf.NAccount.of_close_year(wsPass, Convert.ToInt16(accyear));

                HdIsFinished.Value = "true";
                if (result != 1)
                {
                    throw new Exception("ไม่สามารถปิดสิ้นปีบัญชีได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ปิดสิ้นปีบัญชีเรียบร้อยแล้ว");
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
            
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCloseYear = WebUtil.JsPostBack(this, "postCloseYear");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

            tdw_begin = new DwThDate(Dw_main,this);
            tdw_begin.Add("begin_acc", "begin_acc_tdate");
            tdw_begin.Add("end_acc", "end_acc_tdate");
            tdw_begin.Eng2ThaiAllRow();
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            
            th = new CultureInfo("th-TH");
            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.InsertRow(0);
                Dw_main.Retrieve(state.SsCoopId);
                JspostBeginEndDate();
            }
            else {
                Dw_main.RestoreContext();
            }
        }


        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseYear") {
                JspostCloseYear();
            }
            else if (eventArg == "postNewClear") {
                JspostBeginEndDate();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

      
    }
}
