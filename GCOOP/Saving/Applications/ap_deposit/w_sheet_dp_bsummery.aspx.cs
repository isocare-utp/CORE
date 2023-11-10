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
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit
using Saving;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon;  //new common
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_bsummery : PageWebSheet, WebSheet
    {
        protected String PostRetrive;
        protected String jsPostBlank;
        private DwThDate tdw_processday;
        //private DepositClient dpService;
        private n_depositClient ndept; // new deposit       
        String YYMM = "";


        public void InitJsPostBack()
        {
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
            jsPostBlank = WebUtil.JsPostBack(this, "jsPostBlank");
            tdw_processday = new DwThDate(Dw_Main, this);
            tdw_processday.Add("adtm_date", "adtm_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_Main.SetTransaction(sqlca);

            ndept = wcf.NDeposit;

            if (!IsPostBack)
            {
                Dw_Main.InsertRow(0);
                Dw_Main.SetItemDate(1, "adtm_date", state.SsWorkDate);
            }
            else
            { 
                this.RestoreContextDw(Dw_Main, tdw_processday);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrive")
            {
                JsPostRetrive();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            tdw_processday.Eng2ThaiAllRow();
            Dw_Main.SaveDataCache();
        }
        private void JsPostRetrive()
        {
            //Dw_Detail.InsertRow(0);
            // Dw_Detail.Retrieve();
            String sum_xml = "";
            DateTime adtm_date = Dw_Main.GetItemDateTime(1, "adtm_date");
            // DwUtil.RetrieveDataWindow(Dw_Main, "deposit.pbl", null, YYMM);

            int result = ndept.of_init_sum_with_dept(state.SsWsPass, adtm_date, ref sum_xml);
            if (result == 1)
            {
                Dw_Main.Reset();
                Dw_Main.ImportString(sum_xml, FileSaveAsType.Xml);
                
                

            }

        }
    }
}