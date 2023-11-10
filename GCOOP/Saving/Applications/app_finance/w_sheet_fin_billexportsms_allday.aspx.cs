using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_fin_billexportsms_allday: PageWebSheet,WebSheet
    {
        n_financeClient fin;
        private DwThDate tDwMain;
        protected String jsSendSMS;


        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        public void InitJsPostBack()
        {
            jsSendSMS = WebUtil.JsPostBack(this, "jsSendSMS");

            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("adtm_start", "as_start");
            
        }


        public void WebSheetLoadBegin()
        {
            try
            {
              //  fin = new WsFinance.Finance();
                if (!IsPostBack)
                {
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwMain.SetItemDateTime(1, "adtm_start", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();
                }
                else { this.RestoreContextDw(DwMain); }
            }
            catch (SoapException ex) { LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex)); }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsSendSMS")
            {
                SendSMS();
            }
           
        }

        public void SaveWebSheet()
        {
          
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        private void SendSMS()
        {
            try
            {
                String strReturn;
                String strDate = DwMain.GetItemString(1, "as_start");
                DateTime _dte;
                if (strDate.Length == 8)
                {
                    _dte = DateTime.ParseExact(strDate, "ddMMyyyy", WebUtil.TH);
                }
                else { _dte = new DateTime(1900, 1, 1); }

               // strReturn = fin.ofP  of_postTextportText_smsAllDay(state.SsWsPass, _dte);
              //  LtServerMessage.Text = WebUtil.CompleteMessage(strReturn);
            }
            catch (SoapException ex) { LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex)); }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            

        }
    }
}
 