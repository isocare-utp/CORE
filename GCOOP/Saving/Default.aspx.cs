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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Collections.Generic;

namespace Saving
{
    public partial class Default : PageWebSheet, WebSheet
    {
        public void InitJsPostBack()
        {
            this.IgnoreReadable = true;
        }

        public void WebSheetLoadBegin()
        {
            //WebUtil.txtFormatRegisterDB();
            //Dictionary<string, object>  data=WebUtil.getFileFormatDataBy("ATM_BAY_IMP", "D:\\GCOOP_ALL\\CORE\\GCOOP\\Saving\\Applications\\atm_offline\\\ATM.BAY\\Examples\\Import\\161210_coa004_loan_d1.09.txt");

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
