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
    public partial class w_acc_ucf_botmap : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tDw_main;
        private DwThDate tdw_period;
        private n_accountClient accService;//ประกาศตัวแปร WebService


        //ประกาศ JavaScript Postback
        protected String postGetAccyear;
        protected String postDwPeriodInsertRow; //เช็คว่าลบ period ได้หรือไม่
        protected String postDeleteRow;
        protected String postNewClear;
        protected String postRefresh;
        protected String postMainInsertRow;
        protected String postDeleteMainRow;

        //JS-Event
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.Retrieve();
            Dw_Detail.Reset();

            HdAccyear.Value = "";

            tDw_main.Eng2ThaiAllRow();
        }

        private void JspostGetAccyear()
        {
            try
            {
                int rowcurrent = int.Parse(Hdrow.Value);
                Dw_main.SelectRow(0, false);
                Dw_main.SelectRow(rowcurrent, true);
                String Botmap_id = HdBotid.Value;
                Dw_Detail.SetTransaction(sqlca);
                Dw_Detail.Retrieve(Botmap_id);
            }
            catch {
                Dw_Detail.Reset();
            }

        }

        private void JspostDwPeriodInsertRow()
        {
            Dw_Detail.InsertRow(0);
            String Botmap_id = HdBotid.Value;
            int rowcurrent = int.Parse(Hdrow.Value);
            Dw_Detail.SetItemString(Dw_Detail.RowCount, "accbotmas_id", Botmap_id);

        }

        private void JspostMainInsertRow()
        {
            Dw_main.InsertRow(0);
        }

        private void JspostDeleteRow()
        {
            try
            {
                Int16 RowDetail = Convert.ToInt16(HdRowDetail.Value);
                Dw_Detail.DeleteRow(RowDetail);
                Dw_Detail.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเรียบร้อยแล้ว");
            }
            catch { }
        }

        private void JspostDeleteMainRow()
        {
            try
            {
                Int16 RowMain = Convert.ToInt16(Hdrow_mas.Value);
                Dw_main.DeleteRow(RowMain);
                Dw_main.UpdateData();
                Dw_Detail.Reset();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเรียบร้อยแล้ว");
            }
            catch { }
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postGetAccyear = WebUtil.JsPostBack(this, "postGetAccyear");
            postDwPeriodInsertRow = WebUtil.JsPostBack(this, "postDwPeriodInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postMainInsertRow = WebUtil.JsPostBack(this, "postMainInsertRow");
            postDeleteMainRow = WebUtil.JsPostBack(this, "postDeleteMainRow");

            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("begin", "begin_tdate");
            tDw_main.Add("end_y", "end_tdate");

            tdw_period = new DwThDate(Dw_Detail, this);
            tdw_period.Add("period_end", "period_end_tdate");


        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            th = new CultureInfo("th-TH");
            Dw_main.SetTransaction(sqlca);
            Dw_Detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {

                Dw_main.Retrieve();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_Detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postGetAccyear")
            {
                JspostGetAccyear();
            }
            else if (eventArg == "postDwPeriodInsertRow")
            {
                JspostDwPeriodInsertRow();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postDeleteMainRow")
            {
                JspostDeleteMainRow();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postRefresh")
            {
                //mai แปลงค่าวันที่ภาษาไทยเป็นภาษาอังกฤษ
                tdw_period.Thai2EngAllRow();
            }
            else if (eventArg == "postMainInsertRow")
            {
                JspostMainInsertRow();
            }



        }

        public void SaveWebSheet()
        {
            try
            {
                //DwUtil.UpdateDataWindow(Dw_main, "cm_constant_config.pbl", "accbotmaster");
                //DwUtil.UpdateDataWindow(Dw_Detail, "cm_constant_config.pbl", "accbotmap");
                Dw_main.UpdateData();
                Dw_Detail.UpdateData();
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
            Dw_Detail.SaveDataCache();
        }

        #endregion
    }
}
