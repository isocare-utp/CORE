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
using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_vc_trn_fin : PageWebDialog, WebDialog
    {
        //private DwThDate tDwMain;
        private n_accountClient accService; //ประกาศเสมอ
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();

            n_accountClient accservice = wcf.NAccount;//ประกาศ new
            Dw_main.SetTransaction(sqlca);
            //tDwMain = new DwThDate(Dw_main);
            //tDwMain.Add("voucher_date", "voucher_tdate");
            try
            {
                if (!IsPostBack)
                {
                    Dw_main.InsertRow(0);
                    try
                    {
                        DateTime queryStrVcDate;
                        try { 
                            queryStrVcDate = Convert.ToDateTime(Request["vcDate"]);
                            Dw_main.Retrieve(queryStrVcDate, state.SsCoopControl);
                        }
                        catch { }
                       

                        
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = ex.ToString();
                    }
                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = ex.ToString();
            }
        }


        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
        }
    }
}
