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
using CoreSavingLibrary.WcfNAccount;  //เพิ่มเข้ามา
using Sybase.DataWindow;  //เพิ่มเข้ามา
using System.Globalization;  //เพิ่มเข้ามา
using System.Data.OracleClient;  //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using System.Web.Services.Protocols;
using CoreSavingLibrary; //เพิ่มเข้ามา


namespace Saving.Applications.account 
{
    public partial class w_sheet_close_month : PageWebSheet, WebSheet
    {

        private CultureInfo th;
        private DwThDate tdw_main;
        private n_accountClient accService; //ประกาศตัวแปร WebService

        //=================================
        protected String postCloseMonth;
        protected String postNewClear;



        //=================================
       


        private void JsSearchPeriod()
        {
            //mai หาปีบัญชีล่าสุดที่ยังไม่ได้ปิดสิ้นปี
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                Int16 accperiod = 0; string min_account_year  = "";
                String sql = @"select min(account_year) as min_account_year  from accaccountyear where close_account_stat = '0'and coop_id = '" + state.SsCoopId + "'";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    min_account_year = dt.GetString("min_account_year");
                    int account_year = Convert.ToInt16(min_account_year);
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
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบปีบัญชีที่ต้องการปิดสิ้นเดือน");
            }
            ta.Close();


            //mai แก้ไขดึงงวดล่าสุดของปีบัญชีที่ยังไม่ได้ปิดสิ้นเดือน
            Sta ta1 = new Sta(sqlca.ConnectionString);
            try
            {
                Int16 accperiod = 0; string min_period = "";
                String sql1 = @"select min(period) as min_period  from accperiod where close_flag = '0' and account_year = '" + (Dw_main.GetItemDecimal(1, "account_tyear") - 543) + "' and coop_id = '" + state.SsCoopId + "'";
                Sdt dt1 = ta1.Query(sql1);
                if (dt1.Next())
                {
                    min_period = dt1.GetString("min_period");
                    int period = Convert.ToInt16(min_period);

                    Dw_main.SetItemDecimal(1, "period", period);
                }

            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบงวดบัญชีที่ต้องการปิดสิ้นเดือน");
            }
            ta1.Close();
        }

        private void JspostCloseMonth()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                Int16 accyear = Convert.ToInt16(Dw_main.GetItemDecimal(1, "account_tyear") - 543 ); 
                Int16 accperiod = Convert.ToInt16(Dw_main.GetItemDecimal(1, "period"));
                String status_vc = "";//Dw_main.GetItemString(1, "finstatus_code");
                String wsPass = state.SsWsPass;
                String coop_id  = state.SsCoopId ;

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

                //เรียกใช้ webservice
                if (status_vc == "01") //ขึ้นเลขที่เอกสารใหม่ทุกๆเดือน
                {
                    //int result = accService.CloseMonthClearVcNo(wsPass, accyear, accperiod, coop_id);
                    int result = wcf.NAccount.of_close_month_clear_vcno(wsPass, accyear, accperiod);
                }
                else //ขึ้นเลขที่เอกสารใหม่ทุกๆสิ้นปี   02
                {
                    //int result = accService.CloseMonth(wsPass, accyear, accperiod, coop_id);
                    int result = wcf.NAccount.of_close_month(wsPass, accyear, accperiod);
                }
                
                HdIsFinished.Value = "true";
                LtServerMessage.Text = WebUtil.CompleteMessage("ปิดสิ้นเดือนบัญชีเรียบร้อยแล้ว");
                Hd_year.Value = "";
                Dw_detail.Visible = true;
                DwUtil.RetrieveDataWindow(Dw_detail, "close_month.pbl", null, accyear, accperiod);
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
            postCloseMonth = WebUtil.JsPostBack(this, "postCloseMonth");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            th = new CultureInfo("th-TH");

            if (!IsPostBack)
            {
                
                Dw_main.SetTransaction(sqlca);
                Dw_main.InsertRow(0);
                Dw_detail.Visible = false;
                JsSearchPeriod();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseMonth") {
                JspostCloseMonth();
            }
            else if (eventArg == "postNewClear") {
                JsSearchPeriod();
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
