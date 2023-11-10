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
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_processsltrnpayin : PageWebSheet, WebSheet
    {
        private n_shrlonClient slService;
        private n_commonClient commonSrv;
        private n_keepingClient kpService;
        protected String callGendisk;
        private DwThDate tDwMain;
      

        #region WebSheet Members

        public void InitJsPostBack()
        {
            callGendisk = WebUtil.JsPostBack(this, "callGendisk");

            tDwMain = new DwThDate(dw_cri, this);
            tDwMain.Add("rcv_date", "rcv_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();          
            dw_cri.SetTransaction(sqlca);
            HdRunProcess.Value = "false";
            HdSavedisk.Value = "false";
            kpService = wcf.NKeeping;

            if (!IsPostBack)
            {
                if (dw_cri.RowCount < 1)
                {
                    dw_cri.InsertRow(0);
                }
                dw_cri.SetItemString(1, "coop_id", state.SsCoopControl);
                dw_cri.SetItemDate(1, "rcv_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                try
                {
                    this.RestoreContextDw(dw_cri);
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "callGendisk")
            {
                JSGenDisk();
            }
           
        }



        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            dw_cri.SaveDataCache();
        }

        #endregion

        private void JSGenDisk()
        {

            str_proctrnpayin strtrnpayin = new str_proctrnpayin();
            strtrnpayin.coop_id = dw_cri.GetItemString(1, "coop_id").Trim();
            strtrnpayin.entry_id = state.SsUsername;
            strtrnpayin.source_code = dw_cri.GetItemString(1, "system_code");
            strtrnpayin.trans_date = dw_cri.GetItemDate(1, "rcv_date");
            strtrnpayin.contcoop_id = state.SsCoopId;

            try
            {
                //int result = wcf.NShrlon.of_proc_trnpayin(state.SsWsPass, strtrnpayin);
                //if (result==1) { LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลสำเร็จ"); }
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e.ToString());
            }

        }

      
    }
}
