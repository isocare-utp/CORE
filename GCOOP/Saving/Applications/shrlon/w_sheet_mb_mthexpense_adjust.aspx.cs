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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_mb_mthexpense_adjust : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        public void InitJsPostBack()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            this.ConnectSQLCA();
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
                dw_minus.InsertRow(1);
                dw_plus.InsertRow(1);

            }

        }



        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }


    }
}