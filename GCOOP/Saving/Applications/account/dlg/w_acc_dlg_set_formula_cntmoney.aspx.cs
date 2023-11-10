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
    public partial class w_acc_dlg_set_formula_cntmoney : PageWebDialog, WebDialog 
    {
        private n_accountClient accService; //ประกาศเสมอ
        protected String postSaveConstant;
        protected String postShowConstant;

        //=================================
        private void JspostShowConstant()
        {
            String xmlDw_main = Dw_main.Describe("Datawindow.Data.Xml");
            String xmlDw_detail = Dw_detail.Describe("Datawindow.Data.Xml");
            String ls_flag = Hd_datadesc.Value.Trim();
            //ls_flag = ls_flag.Substring(ls_flag.Length - 2);

            try 
            {
                //short result = accService.GetSetFormulaConstant(state.SsWsPass, ls_flag, ref xmlDw_main, ref xmlDw_detail);
                short result = wcf.NAccount.of_init_consstant_bs(state.SsWsPass, ls_flag, ref xmlDw_main, ref xmlDw_detail);
                if (result == 1)
                {
                    Dw_detail.Reset();
                    Dw_main.Reset();
                    if (xmlDw_main != "")
                    {
                        DwUtil.ImportData(xmlDw_main , Dw_main, null, FileSaveAsType.Xml);
                    }

                    if (xmlDw_detail != "")
                    {
                        DwUtil.ImportData(xmlDw_detail, Dw_detail, null, FileSaveAsType.Xml);
                        ls_flag = ls_flag.Substring(ls_flag.Length - 2);
                        if (ls_flag.Substring(0, 1) == "1")
                        {
                            Dw_detail.SetItemDecimal(1, "cnt_short", 1);
                        }
                        if (ls_flag.Substring(1, 1) == "1")
                        {
                            Dw_detail.SetItemDecimal(1, "cnt_long", 1);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            }
        }
        private void JspostSaveConstant()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;
                // การโยน ไฟล์ xml ไปให้ service
                String xmlDw_main = Dw_main.Describe("Datawindow.Data.Xml");
                String xmlDw_detail = Dw_detail.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                //mai เอา state.ssbranchId ออก
                //int result = accService.SaveFormulaConstant(wsPass, xmlDw_main, xmlDw_detail, Hd_moneycode.Value, Convert.ToInt16(Hd_moneyseq.Value));
                int result = wcf.NAccount.of_save_constant_bs(wsPass, xmlDw_main, xmlDw_detail, Hd_moneycode.Value, Convert.ToInt16(Hd_moneyseq.Value));
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

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSaveConstant = WebUtil.JsPostBack(this, "postSaveConstant");
            postShowConstant = WebUtil.JsPostBack(this, "postShowConstant");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);

            String moneysheet_code = "";
            String data_desc = "";
            Int16 moneysheet_seq = 0;
            
            try
            {
                data_desc = Request["data_desc"];
                moneysheet_code = Request["moneysheet_code"].Trim();
                moneysheet_seq = Convert.ToInt16(Request["moneysheet_seq"]);
                
                Hd_moneycode.Value = moneysheet_code.Trim();
                Hd_moneyseq.Value = Convert.ToString(moneysheet_seq).Trim();
                Hd_datadesc.Value = data_desc.Trim();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }


            if (!IsPostBack) {
                JspostShowConstant();

            } else {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSaveConstant") {
                JspostSaveConstant();
            }
            else if (eventArg == "postShowConstant")
            {
                JspostShowConstant();
            }
        }

        public void WebDialogLoadEnd()
        {
            if (Dw_detail.RowCount > 1) 
            {
                Dw_detail.DeleteRow(Dw_detail.RowCount);
            }
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion
    }
}
