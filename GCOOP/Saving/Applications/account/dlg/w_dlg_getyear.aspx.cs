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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_getyear : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwYear.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwYear);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            DwYear.SaveDataCache();
        }
    }
}