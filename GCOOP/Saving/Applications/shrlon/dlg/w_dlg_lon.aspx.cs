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

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_lon : PageWebDialog ,WebDialog 
    {
       
        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
            }
            catch { LtServerMessage.Text = WebUtil.CompleteMessage("ติดต่อ Database ไม่ได้"); }


            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                String member_no = "";
                try
                {
                    member_no = Request["member_no"].Trim();
                }
                catch { }

                Dw_main.Retrieve(member_no);
                if (Dw_main.RowCount < 1)
                {
                    Dw_main.Reset();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสัญญาเงินกู้");
                }
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
        }

        #endregion
    }
}
