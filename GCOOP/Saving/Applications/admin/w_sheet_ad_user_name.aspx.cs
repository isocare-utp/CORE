using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_user_name : PageWebSheet, WebSheet
    {
        protected String jsinitSearch;
        protected String jsinitPageSearch;

        private String pbl = "ad_user.pbl";

        public void InitJsPostBack()
        {
            jsinitSearch = WebUtil.JsPostBack(this, "jsinitSearch");
            jsinitPageSearch = WebUtil.JsPostBack(this, "jsinitPageSearch");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //DwUserName.InsertRow(0);
                DwDetail.InsertRow(0);
                //DwApplication.InsertRow(0);
                //DwPages.InsertRow(0);
                //string xml = adminService.GetUsers(state.SsWsPass, state.SsCoopId);
                //DwUserName.ImportString(xml, FileSaveAsType.Xml);
                //string test = state.SsCoopId;
                //DwUtil.RetrieveDataWindow(DwUserName, pbl, null, state.SsCoopId);     //Test

            }
            else
            {
                //this.RestoreContextDw(DwUserName);
                this.RestoreContextDw(DwDetail);
                this.RestoreContextDw(DwApplication);
                this.RestoreContextDw(DwPages);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsinitSearch":
                    MemberSearch();
                    break;
                case "jsinitPageSearch":
                    PageSearch();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(DwDetail, "apvlevel_id", "ad_user.pbl", null);
            //DwUserName.SaveDataCache();
            DwDetail.SaveDataCache();
            DwApplication.SaveDataCache();
            DwPages.SaveDataCache();
        }
        public void MemberSearch()
        {
            try
            {
                DwPages.Reset();
                DwDetail.Reset();
                DwApplication.Reset();
                DwUtil.RetrieveDataWindow(DwDetail, pbl, null, userID.Value,state.SsCoopId);       //Test
                DwUtil.RetrieveDataWindow(DwApplication, pbl, null, userID.Value, state.SsCoopId);  //Test
            }
            catch
            {
            }
        }
        public void PageSearch()
        {
            try
            {
                //n_adminClient adminService = wcf.NAdmin;
               // DwPages.Reset();
                //string xmlGetPageUser = adminService.of_getuserpages(state.SsWsPass, userID.Value, AppID.Value);
                //DwPages.ImportString(xmlGetPageUser, FileSaveAsType.Xml);
                //DwUtil.ImportData(xmlGetPageUser, DwPages, null);

                DwUtil.RetrieveDataWindow(DwPages, pbl, null, AppID.Value, userID.Value);       //Test

            }
            catch
            {
            }
        }
    }
}