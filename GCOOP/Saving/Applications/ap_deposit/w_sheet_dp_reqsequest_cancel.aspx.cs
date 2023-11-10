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
//using CoreSavingLibrary.WcfNDeposit;
//using CoreSavingLibrary.WcfNCommon;

using CoreSavingLibrary.WcfNDeposit;    //new deposit
using CoreSavingLibrary.WcfNCommon;     //new common 
using DataLibrary;
using Sybase.DataWindow;


using System.Web.Services.Protocols;
//using Saving.ConstantConfig;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_reqsequest_cancel : PageWebSheet, WebSheet
    {
        //private DepositClient depService;

        private n_depositClient ndept;
        protected String postAccount;
        protected String postSequestType;


        private DwThDate tDwMain;

        #region WebSheet Members

        private void JspostAccount()
        {
            string sequest_xml = "";
            string deptAcc = DwMain.GetItemString(1, "deptaccount_no");
            // string memcoop = DwMain.GetItemString(1, "memcoop_id");
            //string accNo = depService.BaseFormatAccountNo(state.SsWsPass, deptAcc);

            string accNo = ndept.of_analizeaccno(state.SsWsPass, deptAcc);
            try
            {
                //sequest_xml = depService.InitDataForSequest(state.SsWsPass, accNo, state.SsCoopId, state.SsClientIp, state.SsUsername, state.SsWorkDate);

                sequest_xml = ndept.of_init_data_for_sequest(state.SsWsPass, accNo, state.SsCoopId, state.SsClientIp, state.SsUsername, state.SsWorkDate);
                DwMain.Reset();
                //string a = DwMain.Describe("DataWindow.Data.XML");
                // DwMain.ImportString(sequest_xml, Sybase.DataWindow.FileSaveAsType.Xml);
                DwUtil.ImportData(sequest_xml, DwMain, tDwMain, FileSaveAsType.Xml);
                //String accFormat = depService.ViewAccountNoFormat(state.SsWsPass, accNo);
                String accFormat = WebUtil.ViewAccountNoFormat(accNo); //new
                DwMain.SetItemString(1, "deptaccount_no", accFormat);
                tDwMain.Eng2ThaiAllRow();
                try
                {
                    Dw_detail.InsertRow(0);
                    DwUtil.RetrieveDataWindow(Dw_detail, "dp_reqsequest.pbl", tDwMain, accNo);
                    tDwMain.Eng2ThaiAllRow();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void InitJsPostBack()
        {
            postAccount = WebUtil.JsPostBack(this, "postAccount");
            postSequestType = WebUtil.JsPostBack(this, "postSequestType");
            //entry_tdate
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("entry_date", "entry_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit; //new
            }
            catch
            { }

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                //////Edit
                DwUtil.RetrieveDDDW(DwMain, "sequest_type", "dp_reqsequest.pbl", null);                
                DwMain.SetItemString(1, "sequest_type", "1");
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postAccount")
            {
                JspostAccount();
            }
            else if (eventArg == "postSequestType")
            {

            }
        }


        public void SaveWebSheet()
        {
            string sequest_xml = "";
            String accNo = DwMain.GetItemString(1, "deptaccount_no");
            //accNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);
            accNo = ndept.of_analizeaccno(state.SsWsPass, accNo);
            DwMain.SetItemString(1, "deptaccount_no", accNo);
            sequest_xml = DwMain.Describe("DataWindow.Data.XML");
            try
            {
                //depService.UpdateSequest(state.SsWsPass, sequest_xml);
                ndept.of_update_sequest(state.SsWsPass, sequest_xml);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            //String accFormat = depService.ViewAccountNoFormat(state.SsWsPass, accNo);
            String accFormat = WebUtil.ViewAccountNoFormat(accNo); //new
            DwMain.SetItemString(1, "deptaccount_no", accFormat);
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion
    }
}