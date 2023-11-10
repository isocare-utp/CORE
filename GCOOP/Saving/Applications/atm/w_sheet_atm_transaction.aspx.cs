using System;
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
using DataLibrary;
using Saving.WcfATM;
using System.Globalization;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_transaction : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(DwMain, "dp_atm_transaction.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "atmapproval_code", "dddw_atmucfapproval", null);
                DwUtil.RetrieveDDDW(DwMain, "atmsystem_code", "dddw_atmucftransystem", null);
                DwUtil.RetrieveDDDW(DwMain, "atmoperate_code", "dddw_atmucfoperate", null);
                //DwUtil.RetrieveDDDW(DwMain, "atmresponse_code", "dddw_atmucfresponse", null);
                DwUtil.RetrieveDDDW(DwMain, "atmtran_code", "dddw_atmucftranresult", null);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }
    }
}