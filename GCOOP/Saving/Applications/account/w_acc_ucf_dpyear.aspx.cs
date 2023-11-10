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
using System.Globalization; //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เพิ่มเข้ามา



namespace Saving.Applications.account
{
    public partial class w_acc_ucf_dpyear : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_main;
        public String pbl = "asset.pbl";
        //===========================
        //===============================

        #region WebSheet Members

        public void InitJsPostBack()
        {

            tdw_main = new DwThDate(Dw_main, this);
            tdw_main.Add("beginning_of_dp", "begin_tdate");
            tdw_main.Add("ending_of_dp", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_main.Retrieve(state.SsCoopId);

                for (int i = 1; i <= Dw_main.RowCount; i++)
                {
                    Decimal dp_year = Dw_main.GetItemDecimal(i, "dp_year");
                    dp_year = dp_year + 543;
                    Dw_main.SetItemDecimal(i, "dp_tyear", dp_year);
                }
                tdw_main.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
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

            Dw_main.SaveDataCache();
        }

        #endregion
    }
}
