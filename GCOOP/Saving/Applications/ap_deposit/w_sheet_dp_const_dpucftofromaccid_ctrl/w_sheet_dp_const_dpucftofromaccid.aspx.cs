﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.ap_deposit.w_sheet_dp_const_dpucftofromaccid_ctrl
{
    /// <summary>
    /// หน้าจอกำหนดประเภทคู่บัญชี (Dept)
    /// </summary>
    public partial class w_sheet_dp_const_dpucftofromaccid : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Retrieve();//
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertAtRow(0);
                dsList.SetItem(0, dsList.DATA.COOP_IDColumn, state.SsCoopControl);//
                dsList.SetItem(0, dsList.DATA.BANK_BRANCHColumn, "ALL");//
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                int result = exe.Execute();
                dsList.Retrieve();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ (" + result + ")");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}