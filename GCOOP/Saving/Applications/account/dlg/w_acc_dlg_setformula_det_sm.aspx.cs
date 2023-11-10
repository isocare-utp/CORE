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
using Sybase.DataWindow;
using System.Data.OracleClient;
using CoreSavingLibrary.WcfNAccount;
using DataLibrary;
using System.Web.Services.Protocols;


namespace Saving.Applications.account.dlg
{
    public partial class w_acc_dlg_setformula_det_sm : PageWebDialog, WebDialog 
    {
        private n_accountClient accService;
        protected  String postDwSheetChoose;
        protected String postDeleteRowDwData;
        protected String postSaveData;
        //===============================

        private void JspostSavedata()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;

                String xmlMain = Dw_data.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                //int result = accService.SaveFormulaSM(wsPass, xmlMain, Hd_moneycode.Value, Hd_moneyseq.Value);
                int result = wcf.NAccount.of_save_formula_sm(wsPass, xmlMain, Hd_moneycode.Value, Hd_moneyseq.Value);
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
        private void JspostDeleteRowDwData()
        {
            try 
            {
                int RowDwdata = int.Parse(Hd_RowDwdata.Value);
                Dw_data.DeleteRow(RowDwdata);
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private void JspostGetSetFormulaSM()
        {
            String as_money_sheetcode = Hd_moneycode.Value.Trim();
            short an_data_group = Convert.ToInt16(Hd_moneyseq.Value.Trim());
            String as_formula_sm_xml = Dw_data.Describe("Datawindow.Data.Xml");
            String as_formular_sm_choose_xml = Dw_master.Describe("Datawindow.Data.Xml");
           try
           {
               //short result = accService.GetSetFormulaSM(state.SsWsPass, as_money_sheetcode, an_data_group, ref as_formula_sm_xml, ref as_formular_sm_choose_xml);
               short result = wcf.NAccount.of_init_formula_sm(state.SsWsPass, as_money_sheetcode, an_data_group, ref as_formula_sm_xml, ref as_formular_sm_choose_xml);
               if (result == 1)
               {
                   if (as_formula_sm_xml != "")
                   {
                       DwUtil.ImportData(as_formula_sm_xml, Dw_data, null, FileSaveAsType.Xml);
                       DwUtil.RetrieveDDDW(Dw_data, "sheetcode_ref", "acc_set_formula", state.SsCoopControl);
                   }
                   if (as_formular_sm_choose_xml != "")
                   {
                       DwUtil.ImportData(as_formular_sm_choose_xml , Dw_master  , null, FileSaveAsType.Xml);
                   }
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

        private void JspostDwSheetChoose()
        {
            try 
            {
                String moneysheet_code = Hd_sectionid.Value.Trim();
                Dw_master.SetFilter("moneysheet_code = '" + moneysheet_code + "'");
                Dw_master.Filter();
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postDwSheetChoose = WebUtil.JsPostBack(this, "postDwSheetChoose");
            postDeleteRowDwData = WebUtil.JsPostBack(this, "postDeleteRowDwData");
            postSaveData = WebUtil.JsPostBack(this, "postSaveData");

            WebUtil.RetrieveDDDW(Dw_sheetchoose, "section_id", "acc_set_formula.pbl", state.SsCoopId);
        }

        public void WebDialogLoadBegin()
        {
            try 
            {
                this.ConnectSQLCA();
                accService = wcf.NAccount;
                Dw_data.SetTransaction(sqlca);
                Dw_master.SetTransaction(sqlca);
                Dw_sheetchoose.SetTransaction(sqlca);

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
                        JspostGetSetFormulaSM();
                        Dw_sheetchoose.InsertRow(0);
                    }
                    else
                    {
                        this.RestoreContextDw(Dw_data);
                        this.RestoreContextDw(Dw_master);
                        this.RestoreContextDw(Dw_sheetchoose);
                    }

                }
                catch (Exception ex) 
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
            if (eventArg == "postDwSheetChoose") {
                JspostDwSheetChoose();
            }
            else if (eventArg == "postDeleteRowDwData") {
                JspostDeleteRowDwData();
            }
            else if (eventArg == "postSaveData")
            {
                JspostSavedata();
            }
        }

        public void WebDialogLoadEnd()
        {
            if (Dw_sheetchoose.RowCount > 1) {
                Dw_sheetchoose.DeleteRow(Dw_sheetchoose.RowCount);
            }

            Dw_data.SaveDataCache();
            Dw_master.SaveDataCache();
            Dw_sheetchoose.SaveDataCache();
        }

        #endregion

        protected void B_back_Click(object sender, EventArgs e)
        {
            try 
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
                        String data_type = Dw_master.GetItemString(i, "data_type").Trim();
                        if (data_type != "TX")
                        {
                            String data_desc = Convert.ToString(Dw_master.GetItemString(i, "c_account"));
                            String description = Dw_master.GetItemString(i, "description");
                            String operate_nature = Dw_master.GetItemString(i, "operate_nature");
                            String sheetcode_ref = Dw_master.GetItemString(i, "moneysheet_code");
                            int ll_insert = Dw_data.InsertRow(0);
                            Dw_data.SetItemString(ll_insert, "data_desc", data_desc);
                            Dw_data.SetItemString(ll_insert, "description", description);
                            Dw_data.SetItemString(ll_insert, "operate_nature", operate_nature);
                            Dw_data.SetItemString(ll_insert, "sheetcode_ref", sheetcode_ref);
                        }
                        Dw_master.SetItemDecimal(i, "choose_flag", 0);
                    }
                }
            }
            catch (Exception ex)
            { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            }
        }
    }
}
