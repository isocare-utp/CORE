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
using DataLibrary; //เพิ่มเข้ามา
using System.Web.Services.Protocols; //เพิ่มเข้ามา


namespace Saving.Applications.account
{
    public partial class w_acc_ucf_accperiod : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_accyear;
        private DwThDate tdw_period;
        private n_accountClient accService;//ประกาศตัวแปร WebService
        

        //ประกาศ JavaScript Postback
        protected String postGetAccyear;
        protected String postCanDelPeriod; //เช็คว่าลบ period ได้หรือไม่
        protected String postDwPeriodInsertRow; //เช็คว่าลบ period ได้หรือไม่
        protected String postDeleteRow;
        protected String postNewClear;
        protected String postRefresh;

        //JS-Event
        private void JspostNewClear()
        {
            Dw_accyear.Reset();
            Dw_accyear.Retrieve();
            Dw_accperiod.Reset();
            
            HdAccyear.Value = "";
          
            tdw_accyear.Eng2ThaiAllRow();
        }

        private void JspostGetAccyear()
        {
            Int16 Accyear = Convert.ToInt16(HdAccyear.Value);
            int rowcurrent = int.Parse(Hdrow.Value);
            Dw_accperiod.SetTransaction(sqlca);
            
            Dw_accyear.SelectRow(0, false);
            Dw_accyear.SelectRow(rowcurrent, true);
            Dw_accyear.SetRow(rowcurrent);
            Dw_accperiod.Retrieve(Accyear,state.SsCoopId);

            for (int i = 1; i <= Dw_accperiod.RowCount; i++)
            {
                Decimal acc_year = Dw_accperiod.GetItemDecimal(i, "account_year_prev");
                acc_year = acc_year + 543;
                Dw_accperiod.SetItemDecimal(i, "acc_year_prev_t", acc_year);
            }

            tdw_period.Eng2ThaiAllRow();
          
        }

        private void JspostDwPeriodInsertRow()
        {

            if (HdAccyear.Value == "")
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("กรุณาคลิกเลือกปีบัญชีก่อน");
            }
            else
            {
                Dw_accperiod.InsertRow(0);
                Dw_accperiod.SetItemDecimal(Dw_accperiod.RowCount, "period", Dw_accperiod.RowCount);
                Dw_accperiod.SetItemString(Dw_accperiod.RowCount, "coop_id", state.SsCoopId);
                Dw_accperiod.SetItemString(Dw_accperiod.RowCount, "account_year", HdAccyear.Value.Trim());
            }
        }

        private void JspostDeleteRow()
        {
            Int16 RowPeriod = Convert.ToInt16(HdRowPeriod.Value);
            Dw_accperiod.DeleteRow(RowPeriod);
        }


        private void JspostCanDelPeriod()
        {
            try
            {
                
                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                Int16 RowPeriod = Convert.ToInt16(HdRowPeriod.Value);
                Int16 AccYear1 = Convert.ToInt16(Dw_accperiod.GetItemDecimal(RowPeriod, "account_year"));
                Int16 AccPeriod = Convert.ToInt16(Dw_accperiod.GetItemDecimal(RowPeriod, "period"));
                String wsPass = state.SsWsPass;
                //เรียกใช้ webservice
                int result = wcf.NAccount.of_candel_period(wsPass, AccYear1, AccPeriod);
                //int result = accService.DeleteRowAccperiod(wsPass, AccYear1, AccPeriod, state.SsCoopId);
                HdIsFinished.Value = "true";
                Dw_accperiod.DeleteRow(RowPeriod);
                Dw_accperiod.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
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
            postGetAccyear = WebUtil.JsPostBack(this, "postGetAccyear");
            postCanDelPeriod = WebUtil.JsPostBack(this, "postCanDelPeriod");
            postDwPeriodInsertRow = WebUtil.JsPostBack(this, "postDwPeriodInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");

            tdw_accyear = new DwThDate(Dw_accyear,this);
            tdw_accyear.Add("begin", "begin_tdate");
            tdw_accyear.Add("end_y", "end_tdate");

            tdw_period = new DwThDate(Dw_accperiod, this);
            tdw_period.Add("period_end", "period_end_tdate");


        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            th = new CultureInfo("th-TH");
            Dw_accyear.SetTransaction(sqlca);
            Dw_accperiod.SetTransaction(sqlca);

            if (!IsPostBack)
            {

                Dw_accyear.Retrieve(state.SsCoopId);
                //mai แก้ไขให้แสดงปี พ.ศ.
                for (int i = 1; i <= Dw_accyear.RowCount; i++)
                {
                    Decimal acc_year = Dw_accyear.GetItemDecimal(i, "account_year");
                    acc_year = acc_year + 543;
                    Dw_accyear.SetItemDecimal(i, "account_year_t", acc_year);
                }

                tdw_accyear.Eng2ThaiAllRow();
                tdw_period.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_accyear);
                this.RestoreContextDw(Dw_accperiod);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postGetAccyear")
            {
                JspostGetAccyear();
            }
            else if (eventArg == "postCanDelPeriod")
            {
                JspostCanDelPeriod();
            }
            else if (eventArg == "postDwPeriodInsertRow")
            {
                JspostDwPeriodInsertRow();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postNewClear") {
                JspostNewClear();
            }
            else if (eventArg == "postRefresh")
            { 
                //mai แปลงค่าวันที่ภาษาไทยเป็นภาษาอังกฤษ
                tdw_period.Thai2EngAllRow();
            }



        }

        public void SaveWebSheet()
        {
            try
            {
                int rowAll = Dw_accperiod.RowCount;
                if (rowAll > 12)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่สามารถบันทึกได้ เนื่องจากมีงวดบัญชีเกิน 12 งวด");
                }
                else 
                {
                    accService = wcf.NAccount;
                    Int16 acc_year = Int16.Parse( HdAccyear.Value);
                    String ls_period_xml = Dw_accperiod.Describe("DataWindow.Data.XML");

                    //int result = accService.UpdateAccPeriod(state.SsWsPass, acc_year, ls_period_xml);
                    int result = wcf.NAccount.of_add_accperiod(state.SsWsPass, acc_year, ls_period_xml);
                    //DwUtil.UpdateDataWindow(Dw_accperiod, "cm_constant_config.pbl", "accperiod");
                    
                   // Dw_accperiod.UpdateData();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_accyear.SaveDataCache();
            Dw_accperiod.SaveDataCache();
        }

        #endregion
    }
}
