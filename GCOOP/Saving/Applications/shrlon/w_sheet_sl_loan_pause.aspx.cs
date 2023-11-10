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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon{
    public partial class w_sheet_sl_loan_pause : PageWebSheet, WebSheet
    {
        protected String initLnPause;
        private n_shrlonClient shrlonSrv;
        private DwThDate dwMainThDate;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            initLnPause = WebUtil.JsPostBack(this, "initLnPause");
            dwMainThDate = new DwThDate(dw_main, this);
            dwMainThDate.Add("pauseloan_date", "pauseloan_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonSrv = wcf.NShrlon;
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            if (IsPostBack)
            {
                dw_main.RestoreContext();
                dw_list.RestoreContext();
            }
            else
            {
                this.InitDataWindow();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "initLnPause")
            {
                this.InitLnPause();
            }
        }

        public void SaveWebSheet()
        {
            str_lnpause strLnPause = new str_lnpause();
            strLnPause.entry_id = state.SsUsername;
            strLnPause.loanpause_date = dw_main.GetItemDateTime(1, "pauseloan_date");
            strLnPause.member_no = WebUtil.StringFormat(dw_main.GetItemString(1, "member_no"), "000000");
            strLnPause.xml_pause = dw_main.Describe("DataWindow.Data.XML");
            strLnPause.xml_pausedet = dw_list.Describe("DataWindow.Data.XML");
            try{
                int result = shrlonSrv.of_savereq_lnpause(state.SsWsPass, ref strLnPause);
            if(result==1){
                LtServerMessage.Text =  WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dw_main.Reset();
                dw_list.Reset();
                InitDataWindow();

            }else{
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
            }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            dwMainThDate.Eng2ThaiAllRow();
        }

        #endregion

        private void InitDataWindow()
        {
            dw_main.InsertRow(0);
            dw_main.SetItemDateTime(1, "pauseloan_date", state.SsWorkDate);
        }

        private void InitLnPause()
        {
            str_lnpause strLnPause = new str_lnpause();
            strLnPause.entry_id = state.SsUsername;
            strLnPause.loanpause_date = dw_main.GetItemDateTime(1, "pauseloan_date");
            strLnPause.member_no = WebUtil.StringFormat(dw_main.GetItemString(1, "member_no"),"00000000");
          //  shrlonSrv.of_initreq_lnpause(state.SsWsPass, ref strLnPause);
            try
            {
                shrlonSrv.of_initreq_lnpause(state.SsWsPass, ref strLnPause);
                dw_main.Reset();
                DwUtil.ImportData(strLnPause.xml_pause, dw_main, dwMainThDate, FileSaveAsType.Xml);
                //DwUtil.ImportData(strLnPause.xml_pause, dw_main,);
                //dw_main.ImportString(strLnPause.xml_pause, FileSaveAsType.Xml);
                DwUtil.DeleteLastRow(dw_main);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                dw_main.Reset();
            }
            try
            {
                dw_list.Reset();
                DwUtil.ImportData(strLnPause.xml_pausedet, dw_list, dwMainThDate, FileSaveAsType.Xml);
               // DwUtil.ImportData(strLnPause.xml_pausedet, dw_list, null);
               // dw_list.ImportString(strLnPause.xml_pausedet, FileSaveAsType.Xml);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                dw_list.Reset();
            }
        }
    }
}


