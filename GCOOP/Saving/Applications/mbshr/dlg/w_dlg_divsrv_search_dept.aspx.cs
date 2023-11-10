﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_divsrv_search_dept : PageWebDialog,WebDialog
    {
        public String pbl = "divsrv_req_methpay.pbl";
        //=======================================
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                String member_no = "";
                String memcoop_id = "";
                member_no = Request["member_no"].Trim();
                memcoop_id = Request["memcoop_id"].Trim();
                Dw_main.Reset();
                DwUtil.RetrieveDataWindow(Dw_main, pbl, null, member_no, memcoop_id);
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
            if (Dw_main.RowCount == 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขที่บัญชีเงินฝาก");
            }
            Dw_main.SaveDataCache();
        }
    }
}