using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl
{
    public partial class w_dlg_dp_slip_recppaytype : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostList { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostList)
            {
                switch (dsMain.DATA[0].DEPTWITH_FLAG)
                {
                    case "1":
                        //ฝาก
                        dsList.RecpPayType("D");
                        break;
                    case "2":
                        //ถอน
                        dsList.RecpPayType("W");
                        break;
                    case "3":
                        //ปิดบัญชี
                        dsList.RecpPayType("C");
                        break;
                    default:
                        dsList.ResetRow();
                        break;
                }
            }
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}