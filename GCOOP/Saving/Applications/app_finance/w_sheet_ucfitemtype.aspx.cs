using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.app_finance
{

    public partial class w_sheet_ucfitemtype : PageWebSheet, WebSheet
    {

        #region WebSheet Members

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        #endregion

        protected void itemtype_click(object sender, EventArgs e)
        {
            try
            {
                string de = @"
                              DELETE FROM FINUCFITEMTYPE WHERE coop_id = {0}";
                de = WebUtil.SQLFormat(de, state.SsCoopControl);
                WebUtil.ExeSQL(de);

                String se = "INSERT INTO FINUCFITEMTYPE " + 
                "( COOP_ID, SLIPITEMTYPE_CODE, ITEM_DESC, ACCNATURE_FLAG, " + 
                "ACCMAP_CODE, GENVC_FLAG, ACCOUNT_ID ) "+  
                "(select coop_id, account_id, account_name, 1, " +
                "'AID', 1, account_id from accmaster where account_level = 4 and account_type_id = 3" +
                " and account_id not in ( select slipitemtype_code from finucfitemtype ))";
                Sdt ta = WebUtil.QuerySdt(se);
                LtServerMessage.Text = WebUtil.CompleteMessage("อัพเดทรายการการเงินสำเร็จ");
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

    }
}
