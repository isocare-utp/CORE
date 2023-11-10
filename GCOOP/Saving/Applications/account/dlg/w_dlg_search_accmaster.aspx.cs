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
using DataLibrary;
using System.Data.OracleClient;

namespace Saving.Applications.account.dlg
{

    public partial class w_dlg_search_accmaster : PageWebDialog,WebDialog 
    {
        protected String postFindAccmaster;
        private string is_sql;
        
        //========================
        private void JspostFindAccmaster()
        {

            string ls_acc_no, ls_sqltext, ls_temp, ls_acc_name;
         
            ls_sqltext = "";

            try
            {
                ls_acc_no = Dw_find.GetItemString(1, "account_id");
            }
            catch
            {
                ls_acc_no = "";
            }
           
            try
            {
                ls_acc_name = Dw_find.GetItemString(1, "account_name");
            }
            catch
            {
                ls_acc_name = "";
            }
            //===

            if (ls_acc_no.Length > 0)
            {
                ls_sqltext += "and ( ACCMASTER.ACCOUNT_ID like  '" + ls_acc_no + "%') ";
            }
            if (ls_acc_name.Length > 0)
            {
                ls_sqltext += "and ( ACCMASTER.ACCOUNT_NAME like  '" + ls_acc_name + "%') ";
            }


            if (ls_sqltext == null) ls_sqltext = "";
            ls_temp = is_sql + ls_sqltext;
            HSqlTemp.Value = ls_temp;
            Dw_detail.SetSqlSelect(HSqlTemp.Value);
            Dw_detail.Retrieve();

            if (Dw_detail.RowCount < 1)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ยังไม่พบรายการรหัสบัญชี/ชื่อบัญชี ที่ค้นหา");
                Dw_detail.Reset();
            }
        
        }
        #region WebDialog Members

        public void InitJsPostBack()
        {
            postFindAccmaster = WebUtil.JsPostBack(this, "postFindAccmaster");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_find.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);

           
            is_sql = Dw_detail.GetSqlSelect();

            if (!IsPostBack)
            {
                Dw_find.InsertRow(0);
                Dw_detail.Retrieve();
                HSqlTemp.Value = is_sql;
            }
            else
            {
                this.RestoreContextDw(Dw_find);
                this.RestoreContextDw(Dw_detail);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postFindAccmaster")
            {
                JspostFindAccmaster();
            }
        }

        public void WebDialogLoadEnd()
        {
            if (Dw_find.RowCount > 1) 
            {
                Dw_find.DeleteRow(Dw_find.RowCount);
            }

            Dw_find.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion
    }
}
