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
using System.Globalization; //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เพิ่มเข้ามา



namespace Saving.Applications.account
{
    public partial class w_acc_ucf_accyear : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_main;
        public String pbl = "cm_constant_config.pbl";
        //===========================

        protected String postInsertRow;
        protected String postDeleteRow;
        protected String postCanDelYearRow;
        protected String postRefresh;

        private n_accountClient accService;
        //===============================
        private void JspostCanDelYearRow()
        {
         try
         {
              n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service

                Int16 RowYear = Convert.ToInt16(Hdrow.Value);
                Int16 AccYear = Convert.ToInt16(Dw_main.GetItemDecimal(RowYear, "account_year"));
                String wsPass = state.SsWsPass;
                //int result = accService.DeleteRowAccyear(wsPass, AccYear); // pom กลับมาแก้เด้อ
                int result = wcf.NAccount.of_candel_year(wsPass, AccYear);
                HdIsFinished.Value = "true";
                Dw_main.DeleteRow(RowYear);
                Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
         
         }
         catch (SoapException ex)
         {
             //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
             LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
         }
         catch(Exception ex)
         {
             LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
         }
               
        }

        private void JspostInsertRow() 
        {
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(Dw_main.RowCount, "coop_id", state.SsCoopId);
        }
       
        private void JspostDeleteRow()
        {
            try 
            {
                Int16 RowDetail = Convert.ToInt16(Hdrow.Value);
                Dw_main.DeleteRow(RowDetail);
                Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }



        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postCanDelYearRow = WebUtil.JsPostBack(this,"postCanDelYearRow");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");

            tdw_main = new DwThDate(Dw_main, this);
            tdw_main.Add("begin", "begin_tdate");
            tdw_main.Add("end_y", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_main.Retrieve(state.SsCoopId);

                for (int i = 1; i <= Dw_main.RowCount; i++)
                {
                    Decimal acc_year = Dw_main.GetItemDecimal(i, "account_year");
                    acc_year = acc_year + 543;
                    Dw_main.SetItemDecimal(i, "account_year_t", acc_year);
                }
                tdw_main.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postInsertRow") 
            {
                JspostInsertRow();
            }
            else if(eventArg == "postCanDelYearRow") 
            {
                JspostCanDelYearRow();
            }
            else if (eventArg == "postRefresh")
            {
                //mai แปลงวันที่จากภาษาไทยเป็นภาษาอังกฤษ
                tdw_main.Thai2EngAllRow();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                accService = wcf.NAccount;
                //DwUtil.UpdateDataWindow(Dw_main, pbl, "accaccountyear");
                //Dw_main.UpdateData();
                String ls_year_xml = Dw_main.Describe("DataWindow.Data.XML");
                int result = accService.of_add_accyear(state.SsWsPass, ls_year_xml);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
           
            Dw_main.SaveDataCache();
        }

        #endregion
    }
}
