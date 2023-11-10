using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using Sybase.DataWindow;

namespace Saving.Applications.account
{
    public partial class w_sheet_ucf_contsave: PageWebSheet, WebSheet
    {
        protected String postInit;
        protected String postDeleteRow;
        protected String postAddRow;
        protected String postNewClear;
        public String pbl = "cm_constant_config.pbl";
        //==============
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_data.Reset();
        }

        private void JspostDeleteRow()
        {
            int rowcurrent = int.Parse(Hd_row.Value);
            Dw_data.DeleteRow(rowcurrent);
            Dw_data.UpdateData();
            LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเรียบร้อยแล้ว");
        }
        
        private void JspostAddRow()
        {
            try 
            {
                Decimal acc_year = Dw_main.GetItemDecimal(1, "acc_year");
                Decimal period = Dw_main.GetItemDecimal(1, "acc_period");

                Dw_data.InsertRow(Dw_data.RowCount + 1);
                Dw_data.SetItemString(Dw_data.RowCount, "coop_id", state.SsCoopId);
                Dw_data.SetItemDecimal(Dw_data.RowCount, "seq_no", Dw_data.RowCount);
                Dw_data.SetItemDecimal(Dw_data.RowCount, "period", period);
                Dw_data.SetItemDecimal(Dw_data.RowCount, "account_year", acc_year - 543);
                Dw_data.SetItemDecimal(Dw_data.RowCount, "flag", Dw_data.RowCount);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostInit()
        {
            try 
            {
            Decimal acc_year = Dw_main.GetItemDecimal(1, "acc_year");
            Decimal acc_period = Dw_main.GetItemDecimal(1, "acc_period");

            acc_year = acc_year - 543;
            Dw_data.SetTransaction(sqlca);
            Dw_data.Retrieve(acc_year, acc_period, state.SsCoopId);
            
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            
        }

        //===============
        public void InitJsPostBack()
        {
            postInit = WebUtil.JsPostBack(this, "postInit");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

            WebUtil.RetrieveDDDW(Dw_data, "cnt_code", "cm_constant_config.pbl", state.SsCoopId);
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_data.SetTransaction(sqlca);
            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.InsertRow(0);
                Dw_data.Reset();
            }
            else
            {
                this.RestoreContextDw(Dw_data);
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postAddRow")
            {
                JspostAddRow();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {
            try 
            {
                Dw_data.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                JspostNewClear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_data.SaveDataCache();
        }
    }
}