using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_groupusers : PageWebDialog, WebDialog
    {
        private String pbl = "ad_group.pbl";
        private n_adminClient adminService;
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                //DwUtil.RetrieveDataWindow(DwMain, pbl, null, state.SsUsername);
                DwMain.Reset();
                n_adminClient adminService = wcf.NAdmin;
                string result = "";
                try
                {
                    string group_name = Request["group_name"];
                    DwUtil.RetrieveDataWindow(DwMain,pbl,null,group_name);
                    //result = adminService.GetGroupUsers(state.SsWsPass, group_name,state.SsCoopId);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                try
                {
                    //DwMain.ImportString(result, FileSaveAsType.Xml);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {

            DwMain.SaveDataCache();
        }
    }
}