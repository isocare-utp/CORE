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
using DataLibrary;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_kp_mastreceive_search : PageWebDialog,WebDialog
    {
        private DwThDate tdwmain;

        public void InitJsPostBack()
        {
            tdwmain = new DwThDate(Dw_detail, this);
            tdwmain.Add("receipt_date", "receipt_tdate");

        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                string member_no = "";

                try
                {
                    member_no = Request["member_no"];
                }
                catch
                { }

                DwUtil.RetrieveDataWindow(Dw_detail, "kp_adjust_monthly.pbl", null, state.SsCoopId, state.SsCoopControl, member_no);
                tdwmain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_detail);
            }
                
        }

        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

        public void WebDialogLoadEnd()
        {
            Dw_detail.SaveDataCache();

        }
    }
}