using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Saving
{
    public partial class DownloadFiles : PageWebSheet, WebSheet
    {
        public void InitJsPostBack()
        {
            this.IgnoreReadable = true;
        }

        public void WebSheetLoadBegin()
        {
            Literal1.Text = "";
            string[] filePaths = Directory.GetFiles(WebUtil.PhysicalPath + "Saving\\Downloads\\");
            for (int i = 0; i < filePaths.Length; i++)
            {
                int lastIndex = filePaths[i].LastIndexOf("\\");
                String file = filePaths[i].Substring(lastIndex + 1);
                if (file.ToLower().IndexOf("thumbs.db") >= 0)
                {
                    continue;
                }
                Literal1.Text += "&nbsp; &nbsp; * <a href=\"" + state.SsUrl + "Downloads/" + file + "\">" + file + "</a><br />";
            }
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
    }
}