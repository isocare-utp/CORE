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
using System.Data.OracleClient;  //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา

namespace Saving.Applications.account.dlg
{
    public partial class d_dlg_select_account : PageWebDialog, WebDialog 
    {
        protected String jsPostSearch;
        protected String jsPostDelete;
        string pbl = "budget.pbl";

        #region WebDialog Members
        public void InitJsPostBack()
        {
            jsPostSearch = WebUtil.JsPostBack(this, "jsPostSearch");
            jsPostDelete = WebUtil.JsPostBack(this, "jsPostDelete");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                Dwsearch.InsertRow(0);
                DwUtil.RetrieveDataWindow(Dwlist, pbl, null, "%", "%");
                GetAccountList();
            }
            else
            {
                this.RestoreContextDw(Dwselect);
                this.RestoreContextDw(Dwsearch);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostSearch":
                    Search();
                    break;
                case "jsPostDelete":
                    Delete();
                    break;
            }
        }

        public void WebDialogLoadEnd()
        {
            Dwselect.SaveDataCache();
            Dwsearch.SaveDataCache();
            Dwlist.SaveDataCache();
        }
        #endregion

        #region Function
        private void Search()
        {
            string acc_id = "";
            string acc_name = "%";

            try
            {
                acc_id = Dwsearch.GetItemString(1, "acc_id");
                acc_id += "%";
            }
            catch
            {
                acc_id += "%";
            }
            try
            {
                acc_name += Dwsearch.GetItemString(1, "acc_name");
                acc_name += "%";
            }
            catch
            {

            }
            try
            {
                DwUtil.RetrieveDataWindow(Dwlist, pbl, null, acc_id, acc_name);
            }
            catch { }
        }

        private void Delete()
        {
            int row = Convert.ToInt32(Hdrow.Value);
            Dwselect.DeleteRow(row);
        }

        private void GetAccountList()
        {
            string[] acc_id = Request["acc_list"].Trim().Split(',');
            int count = acc_id.Length;
            for (int i = 0; i < count; i++)
            {
                if (acc_id[i] != "")
                {
                    int row = Dwselect.InsertRow(0);
                    Dwselect.SetItemString(row, "account_id", acc_id[i]);

                    for (int j = 1; j <= Dwlist.RowCount; j++)
                    {
                        if (Dwlist.GetItemString(j, "account_id").Trim() == acc_id[i])
                        {
                            Dwselect.SetItemString(row, "account_name", Dwlist.GetItemString(j, "account_name"));
                            break;
                        }
                    }
                }
            }
        }

        protected void B_select_Click(object sender, EventArgs e)
        {
            Decimal flag;
            for (int i = 1; i <= Dwlist.RowCount; i++)
            {
                try
                {
                    flag = Dwlist.GetItemDecimal(i, "flag");
                }
                catch { flag = 0; }

                if (flag == 1)
                {
                    String account_id = Dwlist.GetItemString(i, "account_id");
                    String account_name = Dwlist.GetItemString(i, "account_name");

                    int ll_insert = Dwselect.InsertRow(0);
                    Dwselect.SetItemString(ll_insert, "account_id", account_id.Trim());
                    Dwselect.SetItemString(ll_insert, "account_name", account_name.Trim());

                    Dwlist.SetItemDecimal(i, "flag", 0);
                }
            }
        }
        #endregion
    }
}