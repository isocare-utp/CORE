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
    public partial class w_sheet_ad_groupname : PageWebSheet, WebSheet
    {
        protected String jsinitSearch;
        protected String jsinitPageSearch;
        protected String jsMemberClick;
        private String pbl = "ad_group.pbl";
        

        public void InitJsPostBack()
        {
            jsinitSearch = WebUtil.JsPostBack(this, "jsinitSearch");
            jsinitPageSearch = WebUtil.JsPostBack(this, "jsinitPageSearch");
            jsMemberClick = WebUtil.JsPostBack(this, "jsMemberClick");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                n_adminClient adminService = wcf.NAdmin;
                DwUserName.InsertRow(0);
                DwDetail.InsertRow(0);
                DwApplication.InsertRow(0);
                //DwPages.InsertRow(0);

                //DwUtil.RetrieveDataWindow(DwUserName, pbl, null, null);
                DwUserName.Reset();
                string xml = adminService.of_getgroup(state.SsWsPass, state.SsCoopId);
                DwUserName.ImportString(xml, FileSaveAsType.Xml);
            }
            else
            {
                this.RestoreContextDw(DwUserName);
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
                    GroupSearch();
                    break;
                case "jsinitPageSearch":
                    PageSearch();
                    break;
                case "jsMemberClick":
                    MemberClick();
                    break;
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwUserName.SaveDataCache();
            DwDetail.SaveDataCache();
            DwApplication.SaveDataCache();
            DwPages.SaveDataCache();
        }
        public void GroupSearch()
        {
            try
            {
                n_adminClient adminService = wcf.NAdmin;
                DwDetail.Reset();
                DwApplication.Reset();
                string xmlGetGroup = adminService.of_getuser(state.SsWsPass, GroupID.Value);
                DwDetail.ImportString(xmlGetGroup, FileSaveAsType.Xml);
                //string xmlgetGroupApps = adminService.GetUserApps(state.SsWsPass, GroupID.Value, state.SsCoopId);
                //DwApplication.ImportString(xmlgetGroupApps, FileSaveAsType.Xml);

                //DwUtil.RetrieveDataWindow(DwDetail, pbl, null, GroupID.Value);       //Test
                DwUtil.RetrieveDataWindow(DwApplication, pbl, null, GroupID.Value , state.SsCoopId);  //Test
            }
            catch
            {
            }
        }
        public void PageSearch()
        {
            try
            {
                n_adminClient adminService = wcf.NAdmin;
                DwPages.Reset();
                string xmlGetGroupPages = adminService.of_getgrouppages(state.SsWsPass, GroupID.Value, AppID.Value);
                DwPages.ImportString(xmlGetGroupPages, FileSaveAsType.Xml);

                //DwUtil.RetrieveDataWindow(DwPages, pbl, null, GroupID.Value, AppID.Value);       //Test

            }
            catch
            {
            }
        }
        public void MemberClick()
        {
            n_adminClient adminService = wcf.NAdmin;
            string GetGroupUserXML = adminService.of_getgroupusers(state.SsWsPass, GroupID.Value);
            DwDetail.ImportString(GetGroupUserXML, FileSaveAsType.Xml);
        }
    }
}