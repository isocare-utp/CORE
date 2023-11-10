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
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon;  //new common
using System.Globalization;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_inrate : PageWebSheet, WebSheet
    {
        protected string PostRetrive;
        String pbl = "dp_deptintrate.pbl";
        //private DepositClient depService;
        private n_depositClient ndept; // new deposit
        private DwThDate tDwDetail;


        public void InitJsPostBack()
        {
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
            tDwDetail = new DwThDate(DwDetail);
            tDwDetail.Add("use_date", "use_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "depttype_select", pbl, state.SsCoopControl);

            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrive")
            {
                JsRetrive();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
        public void JsRetrive()
        {
            ndept = wcf.NDeposit;
            string as_display = "";
            String as_dptype_code = DwMain.GetItemString(1, "depttype_select");
            
            
            DwDetail.Reset();
            //display = depService.GetIntDisplay(state.SsWsPass, as_dptype_code);
            wcf.NDeposit.of_get_intdisplay(state.SsWsPass, as_dptype_code, ref as_display); //new

           DwDetail.ImportString(as_display , Sybase.DataWindow.FileSaveAsType.Xml);
            tDwDetail.Eng2ThaiAllRow();

        }

    }
}