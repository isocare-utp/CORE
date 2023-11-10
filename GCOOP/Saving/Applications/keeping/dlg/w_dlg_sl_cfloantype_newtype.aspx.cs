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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_cfloantype_newtype : PageWebDialog, WebDialog
    {
        protected String JSNewType;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            JSNewType = WebUtil.JsPostBack(this, "JSNewType");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_data.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                if (dw_data.RowCount < 1)
                {
                    dw_data.InsertRow(0);
                }
            }
            else
            {
                try
                {
                    dw_data.RestoreContext();

                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "JSNewType")
            {
                try
                {
                    InsertNewType();
                }
                catch (Exception e)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(e);
                }
            }
        }

        private void InsertNewType()
        {
            try
            {
                //insert into LNLOANTYPE(LOANTYPE_CODE,PREFIX, LOANTYPE_DESC, LOANGROUP_CODE )
                //values ('99', 'dd', 'ss', '02' );
                String loantype_code = dw_data.GetItemString(1, "loantype_code").Trim();
                String prefix = dw_data.GetItemString(1, "prefix").Trim();
                String loangroup_desc = dw_data.GetItemString(1, "loantype_desc").Trim();
                String loangroup_code = dw_data.GetItemString(1, "loangroup_code").Trim();

                String sql = @"insert into LNLOANTYPE(LOANTYPE_CODE,PREFIX, LOANTYPE_DESC, LOANGROUP_CODE ) values ('" + loantype_code + "', '" + prefix + "', '" + loangroup_desc + "', '" + loangroup_code + "' )";
                Sta ta = new Sta(sqlca.ConnectionString);
                int s = ta.Exe(sql);
                ta.Close();
                LtServerMessage.Text = WebUtil.CompleteMessage("สร้างรายการใหม่สำเร็จ");
            }
            catch (Exception er)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(er);
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion


    }
}
