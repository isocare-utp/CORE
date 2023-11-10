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
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finquery_all : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwUser;
        protected String newClear;
        protected String postFinQuery;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwUser = new DwThDate(DwUser, this);
            tDwUser.Add("adtm_date", "adtm_tdate");

            newClear = WebUtil.JsPostBack(this, "newClear");
            postFinQuery = WebUtil.JsPostBack(this, "postFinQuery");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                NewClear();
            }
            else
            {
                this.RestoreContextDw(DwUser);
                this.RestoreContextDw(DwUserDetail);
                this.RestoreContextDw(DwRecv);
                this.RestoreContextDw(DwPay);
            }
            DwUser.SetItemString(1, "as_userid", state.SsUsername);

            DataWindowChild DcBranch = DwUser.GetChild("as_branch");
            string xmlBank = "";
            Int32 result = fin.of_getchildbranch(state.SsWsPass,ref xmlBank);
            DcBranch.ImportString(xmlBank, FileSaveAsType.Xml);
            tDwUser.Eng2ThaiAllRow();
            DwUser.Modify("b_user.visible=0");
            FinQuery();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postFinQuery")
            {
                FinQuery();
            }
            else if (eventArg == "newClear")
            {
                NewClear();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwUser.SaveDataCache();
            DwUserDetail.SaveDataCache();
            DwRecv.SaveDataCache();
            DwPay.SaveDataCache();
        }

        #endregion


        public void NewClear()
        {
            //Clear
            DwUser.Reset();
            DwUserDetail.Reset();
            DwRecv.Reset();
            DwPay.Reset();

            //Add Row
            DwUser.InsertRow(0);
            DwUserDetail.InsertRow(0);

            DwUser.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
            tDwUser.Eng2ThaiAllRow();

            //DwUtil.RetrieveDDDW(DwUser, "as_branch", "finquery.pbl", null);

            DataWindowChild dc = DwUser.GetChild("as_branch");
            DwUser.SetItemString(1, "as_branch", state.SsCoopId);
            string xmlBank = "";
            Int32 result = fin.of_getchildbranch(state.SsWsPass,ref xmlBank);
            dc.ImportString(xmlBank, FileSaveAsType.Xml);
        }

        protected void FinQuery()
        {
            String userXml,userdetail_Xml = "",recv_Xml = "",pay_Xml = "";
            Int32 resultXml;

            try
            {
                userXml = DwUser.Describe("DataWindow.Data.XML");
                resultXml = fin.of_finquery(state.SsWsPass, state.SsApplication, userXml,ref userdetail_Xml,ref recv_Xml,ref pay_Xml);

                DwUserDetail.Reset();
                DwRecv.Reset();
                DwPay.Reset();

                DwUserDetail.ImportString(userdetail_Xml, FileSaveAsType.Xml);
                DwRecv.ImportString(recv_Xml, FileSaveAsType.Xml);
                DwPay.ImportString(pay_Xml, FileSaveAsType.Xml);
            }
            catch (SoapException ex)
            {
                DwUserDetail.Reset();
                DwUserDetail.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }


      
    }
}
