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
using System.Collections.Generic;

namespace Saving
{
    public partial class ReportDefault : PageWebSheet, WebSheet
    {
        private String app = "";
        private String gid = "";

        public void InitJsPostBack()
        {
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            string savingUrlCore = WebUtil.GetSavingUrlCore();
            if ((gid != null) && (gid != ""))
            {
                String output = "";
                List<MenuSubReport> sub = new List<MenuSubReport>();
                sub = new MenuSubReport().GetMenuSubReport(app, gid);
                for (int i = 0; i < sub.Count; i++)
                {
                    output += "<tr><td class=\"tdpoint\" width=\"15px\"><img style=\" visibility:hidden;\" id=\"p_row" + i
                            + "\" alt=\"\" src=\"" + savingUrlCore + "Image/arrow.ico\" /></td><td style=\"background-color:Transparent\" id=\"t_row" + i
                            + "\" onmouseover=\"showPointer(" + i + ");\" onmouseout=\"hindPointer(" + i
                            + ");\"><a href=\"" + sub[i].PageLink + "\" style='font-family:tahoma;font-size:14px;color:#467aa7;'>" + sub[i].ReportName + "</a></td></tr>";
                }
                ltr_submenu.Text = output;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        public void SaveWebSheet()
        {
        }
    }
}