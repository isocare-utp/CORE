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
    public partial class w_sheet_atm_control : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(DwMain, "dp_atm_control.pbl", null);
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
            try
            {
                Sta Query = new Sta(state.SsConnectionString);
                String SqlString = String.Empty;
                for (int i = 1; i <= DwMain.RowCount; i++)
                {
                    decimal signon_flag = DwMain.GetItemDecimal(i, "atmmachin_signon_flag");
                    string atm_no = DwMain.GetItemString(i, "atmmachin_atm_no");
                    SqlString = "UPDATE ATMMACHIN SET signon_flag = " + signon_flag + " WHERE ATM_NO = '" + atm_no.Trim() +"'";
                    Query.Exe(SqlString);
                }
                Query.Close();//พจน์  เพิ่มปิด Connect Database
                DwUtil.RetrieveDataWindow(DwMain, "dp_atm_control.pbl", null);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            {
                DwUtil.RetrieveDataWindow(DwMain, "dp_atm_control.pbl", null);
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }
    }
}