using System;
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
using System.Globalization; //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using System.Web.Services.Protocols;
using CoreSavingLibrary; //เพิ่มเข้ามา

namespace Saving.Applications.account
{
    public partial class w_sheet_cancel_close_month : PageWebSheet,WebSheet
    {

        private CultureInfo th;
        private DwThDate tdw_main;
        private n_accountClient accService;//ประกาศตัวแปร WebService
        //=====================================
        protected String postCancelCloseMonth;
        protected String postNewClear;

        //===================================
        private void JspostSearchPeriod()
        {
            //หาปี
            Sta ta = new Sta(sqlca.ConnectionString);
            int account_year = 0;
            try
            {
                //Int16 accperiod = 0; 
                string min_account_year  = "";
                String sql = @"select min(account_year) as min_account_year  from accaccountyear where close_account_stat = '0'and coop_id = '" + state.SsCoopId + "'";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    min_account_year = dt.GetString("min_account_year");
                    account_year = Convert.ToInt16(min_account_year);
                    account_year = account_year + 543;
                    Dw_main.SetItemDecimal(1, "account_tyear", account_year);
                    
                }
                else
                {
                    try
                    {
                        Int16 accyear = Convert.ToInt16(Dw_main.GetItemDecimal(1, "account_tyear"));
                        LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบรายการงวดบัญชีที่ยังไม่ได้มีการปิดสิ้นเดือนประจำปี  : " + accyear + " กรุณากรอกข้อมูลใหม่");
                        sqlca.Rollback();
                    }
                    catch (Exception ex)
                    {
                        sqlca.Rollback();
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบปีบัญชีที่ต้องการยกเลิกปิดสิ้นเดือน");
            }
            ta.Close();


            
            Sta ta1 = new Sta(sqlca.ConnectionString);
            try
            {
                Int16 accperiod = 0; string max_period = "";
                String sql1 = @"select max(period) AS max_period  from accperiod where close_flag = '1' and account_year = '" + (Dw_main.GetItemDecimal(1, "account_tyear") - 543) + "' and coop_id = '" + state.SsCoopId + "'";
                Sdt dt1 = ta1.Query(sql1);
                if (dt1.Next())
                {
                    max_period = dt1.GetString("max_period");
                    int period = Convert.ToInt16(max_period);
                    Dw_main.SetItemDecimal(1, "period", period);
                    Dw_detail.Retrieve(account_year - 543, period);
                }

            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบงวดบัญชีที่ต้องการยกเลิกปิดสิ้นเดือน");
                Dw_detail.Visible = false;
            }
            ta1.Close();
        }

        

        private void JspostCancelCloseMonth()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                Int16 accyear = Convert.ToInt16(Dw_main.GetItemDecimal(1, "account_tyear") - 543); 
                Int16 accperiod = Convert.ToInt16(Dw_main.GetItemDecimal(1, "period"));
                String wsPass = state.SsWsPass;
                String status_vc = "";

                Sta ta1 = new Sta(sqlca.ConnectionString);
                try
                {
                    String sql1 = @"select finstatus_code from accconstant where coop_id = '" + state.SsCoopId + "'";
                    Sdt dt1 = ta1.Query(sql1);
                    if (dt1.Next())
                    {
                        status_vc = dt1.GetString("finstatus_code");
                    }

                }
                catch (Exception ex)
                {
                    sqlca.Rollback();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบสถานะการใช้งานเลขที่ Voucher");
                }
                ta1.Close();

                if (status_vc == "01") //กรณีขึ้นเลขทีเอกสารทุกสิ้นเดือน
                {
                    //int result = accService.CancelCloseMonthClearVcNo(wsPass, accyear, accperiod, state.SsCoopId); 
                    int result = wcf.NAccount.of_cancel_closemonth_clear_vcno(wsPass, accyear, accperiod);
                }
                else
                {
                    //เรียกใช้ webservice
                    //int result = accService.CancelCloseMonth(wsPass, accyear, accperiod, state.SsCoopId);
                    int result = wcf.NAccount.of_cancel_closemonth(wsPass, accyear, accperiod);
                }

                HdIsFinished.Value = "true";
                LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกการปิดสิ้นเดือนบัญชีเรียบร้อยแล้ว");
                Dw_detail.Reset();


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
            postCancelCloseMonth = WebUtil.JsPostBack(this, "postCancelCloseMonth");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            th = new CultureInfo("th-TH");
            Dw_detail.Visible = true;

            if (!IsPostBack)
            {
                Dw_main.SetTransaction(sqlca);
                Dw_detail.SetTransaction(sqlca);
                Dw_main.InsertRow(0);
                Dw_detail.InsertRow(0);
                JspostSearchPeriod();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCancelCloseMonth")
            {
                JspostCancelCloseMonth();
            }
            else if (eventArg == "postNewClear")
            {
                JspostSearchPeriod();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion

       
    }
}
