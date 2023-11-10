using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;  //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols;

namespace Saving.Applications.account.dlg
{
    public partial class w_acc_dlg_setformula_det : PageWebDialog,WebDialog 
    {
        //=============================
        private n_accountClient accService;  
        protected String postSaveData;
        protected String postDeleteRow;
        protected String jsPostSearch;
        string pbl = "acc_set_formula.pbl";
        //=============================
        private void JspostGetSetFormulaFC()
        {
            try
            {
                String as_money_sheetcode = Hd_moneycode.Value.Trim();
                Int16  an_data_group = Convert.ToInt16(Hd_moneyseq.Value.Trim());
                String as_formula_fc_xml = Dw_data.Describe("Datawindow.Data.Xml");
                String as_formular_fc_choose_xml = Dw_master.Describe("Datawindow.Data.Xml");

                //int result = accService.GetSetFormulaFC(state.SsWsPass, as_money_sheetcode, an_data_group, ref as_formula_fc_xml, ref as_formular_fc_choose_xml);
                int result = wcf.NAccount.of_init_formular_fc(state.SsWsPass, as_money_sheetcode, an_data_group, ref as_formula_fc_xml, ref as_formular_fc_choose_xml);
                Dw_data.Reset();
                Dw_master.Reset();

                if (as_formula_fc_xml != "")
                {
                    DwUtil.ImportData(as_formula_fc_xml, Dw_data  , null, FileSaveAsType.Xml);                    
                }

                if (as_formular_fc_choose_xml != "")
                {
                    DwUtil.ImportData(as_formular_fc_choose_xml, Dw_master , null, FileSaveAsType.Xml);                    
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

      
        private void JspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(HdRowDelete.Value);
            Dw_data.DeleteRow(RowDetail);
        }

        

        private void JspostSaveData()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;
                String xmlMain = Dw_data.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                //int result = accService.SaveFormulaFC(state.SsWsPass, xmlMain, Hd_moneycode.Value, Hd_moneyseq.Value);
                int result = wcf.NAccount.of_save_formula_fc(state.SsWsPass, xmlMain, Hd_moneycode.Value, Hd_moneyseq.Value);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        //=============================
        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSaveData = WebUtil.JsPostBack(this, "postSaveData");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            jsPostSearch = WebUtil.JsPostBack(this, "jsPostSearch");
        }

        public void WebDialogLoadBegin()
        {
            try 
            {
                this.ConnectSQLCA();
                accService = wcf.NAccount;//ประกาศ new
                Dw_data.SetTransaction(sqlca);
                Dw_master.SetTransaction(sqlca);

                String moneysheet_code = "";
                Int16 moneysheet_seq = 0;

                try
                {
                    moneysheet_code = Request["moneysheet_code"].Trim();
                    moneysheet_seq = Convert.ToInt16(Request["moneysheet_seq"].Trim());
                    Hd_moneycode.Value = moneysheet_code;
                    Hd_moneyseq.Value = Convert.ToString(moneysheet_seq);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }

                try 
                {
                    if (!IsPostBack)
                    {
                        JspostGetSetFormulaFC();
                        Dwsearch.InsertRow(0);
                        //DwUtil.RetrieveDataWindow(Dw_master, pbl, null, "%", state.SsCoopId);
                        //GetAccountList();
                    }
                    else
                    {
                        this.RestoreContextDw(Dw_master);
                        this.RestoreContextDw(Dw_data);
                        this.RestoreContextDw(Dwsearch);
                    }
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }  
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Database ไม่ได้"); 
            }      
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSaveData") 
            {
                JspostSaveData();
            }
         
            else if (eventArg == "postDeleteRow") {
                JspostDeleteRow();
            }
            else if (eventArg == "jsPostSearch") {
                Search();
            }

                  
        }

        public void WebDialogLoadEnd()
        {
            Dw_master.SaveDataCache();
            Dw_data.SaveDataCache();
            Dwsearch.SaveDataCache();
        }

        #endregion
        #region Function
        private void Search()
        {
            string acc_id = "";
            string acc_name = "%";

            try
            {
                acc_id = Dwsearch.GetItemString(1, "acc_id");
                acc_id += "%";
            }
            catch
            {
                acc_id += "%";
            }
            try
            {
                acc_name += Dwsearch.GetItemString(1, "acc_name");
                acc_name += "%";
            }
            catch
            {

            }
            try
            {
                DwUtil.RetrieveDataWindow(Dw_master, pbl, null, acc_id, acc_name);
            }
            catch { }
        }

        private void GetAccountList()
        {
            //string[] acc_id = Request["acc_list"].Trim().Split(',');
            //int count = acc_id.Length;
            //for (int i = 0; i < count; i++)
            //{
            //    if (acc_id[i] != "")
            //    {
            //        int row = Dw_data.InsertRow(0);
            //        Dw_data.SetItemString(row, "account_id", acc_id[i]);

            //        for (int j = 1; j <= Dw_master.RowCount; j++)
            //        {
            //            if (Dw_master.GetItemString(j, "account_id").Trim() == acc_id[i])
            //            {
            //                Dw_data.SetItemString(row, "account_name", Dw_master.GetItemString(j, "account_name"));
            //                break;
            //            }
            //        }
            //    }
            //}
        }



        protected void B_back_Click(object sender, EventArgs e)
        {
            Decimal choose_flag;
            for (int i = 1; i <= Dw_master.RowCount; i++)
            {
                try
                {
                    choose_flag = Dw_master.GetItemDecimal(i, "choose_flag");
                }
                catch { choose_flag = 0; }

                if (choose_flag == 1)
                {
                    String section_id = Dw_master.GetItemString(i,"section_id");
                    String account_id = Dw_master.GetItemString(i,"account_id");
                    String account_name = Dw_master.GetItemString(i,"account_name");
                    String account_nature = Dw_master.GetItemString(i,"account_nature");

                    int ll_insert = Dw_data.InsertRow(0);
                    Dw_data.SetItemString(ll_insert,"data_desc",section_id.Trim() + "," + account_id.Trim());
                    Dw_data.SetItemString(ll_insert,"description",account_name.Trim());
                    Dw_data.SetItemString(ll_insert,"operate_nature",account_nature);
                    
                    Dw_master.SetItemDecimal(i, "choose_flag", 0);
                }
            }
        }
        #endregion
    }
}
