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
using System.Data.OracleClient;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.account
{
    public partial class w_acc_ucf_constant : PageWebSheet,WebSheet
    {
        private DwThDate tdw_main;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tdw_main = new DwThDate(Dw_main, this);
            tdw_main.Add("begind", "begind_tdate");
            tdw_main.Add("endd", "endd_tdate");

            WebUtil.RetrieveDDDW(Dw_main, "coop_registered_no", "cm_constant_config.pbl", state.SsCoopId);
//            Dw_main.SetItemString(1, "coop_registered_no", state.SsCoopId);
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Decimal begin_year = 0;
                Decimal account_year = 0;

                Dw_main.Retrieve(state.SsCoopId);
                begin_year = Dw_main.GetItemDecimal(1, "begin_year");
                begin_year = begin_year + 543;
                account_year = Dw_main.GetItemDecimal(1, "begin_year") + 544;
                Dw_main.SetItemDecimal(1, "account_year", account_year);
                Dw_main.SetItemDecimal(1, "begin_year_t", begin_year);

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
            try
            {
                Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);
            }
            Dw_main.SaveDataCache();
        }

        #endregion
    }
}
