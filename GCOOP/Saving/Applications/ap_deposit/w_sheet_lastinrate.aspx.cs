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
using DataLibrary;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit
using System.Web.Services.Protocols;
using Sybase.DataWindow;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_lastinrate : PageWebSheet, WebSheet
    {
        protected string PostRetrive;
        String pbl = "dp_deptintrate.pbl";
        //private DepositClient depService;
        private n_depositClient ndept; // new deposit
        

        public void InitJsPostBack()
        {
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //DwMain.InsertRow(0);
                ndept = wcf.NDeposit;
                int display;
                String ls_xml_int ="";

                DwMain.Reset();
                display = ndept.of_show_int_all(state.SsWsPass, ref ls_xml_int);

                DwMain.ImportString(ls_xml_int, Sybase.DataWindow.FileSaveAsType.Xml);
                //tDwDetail.Eng2ThaiAllRow();
                //DwUtil.RetrieveDataWindow(DwMain, pbl, null, null);

            }
            else
            {
                this.RestoreContextDw(DwMain);
                //this.RestoreContextDw(DwDetail);
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            //if (eventArg == "PostRetrive")
            //{
            //    JsRetrive();
            //}
        }

        public void SaveWebSheet()
        {
           
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
           // DwDetail.SaveDataCache();
        }
        //public void JsRetrive()
        //{
        //    String as_dptype_code = DwMain.GetItemString(1, "as_type_desc");
        //    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, as_dptype_code);
        
        //}

    }
}