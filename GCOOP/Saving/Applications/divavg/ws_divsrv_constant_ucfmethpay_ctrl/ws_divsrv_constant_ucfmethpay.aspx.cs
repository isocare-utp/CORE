﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.ws_divsrv_constant_ucfmethpay_ctrl
{
    public partial class ws_divsrv_constant_ucfmethpay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        public void InitJsPostBack()
        {
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                dsList.RetrieveList();

                decimal li_row = dsList.RowCount;
                for (int i = 0; i < li_row; i++)
                {
                    dsList.FindTextBox(i, "methpaytype_code").ReadOnly = true;
                    //dsList.FindTextBox(i, "methpaytype_desc").ReadOnly = true;
                    dsList.FindTextBox(i, "sign_flag_text").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaystm_itemtype").ReadOnly = true;

                    dsList.FindTextBox(i, "running_number").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_code").BackColor = System.Drawing.Color.LightGray;
                    //dsList.FindTextBox(i, "methpaytype_desc").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "sign_flag_text").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaystm_itemtype").BackColor = System.Drawing.Color.LightGray;
                    decimal li_sign_flag = dsList.DATA[i].SIGN_FLAG;
                    if (li_sign_flag == 1)
                    {
                        dsList.DATA[i].sign_flag_text = "+";
                    }
                    else
                    {
                        dsList.DATA[i].sign_flag_text = "-";
                    }


                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                dsList.DATA[dsList.RowCount - 1].COOP_ID = state.SsCoopId;
                dsList.FindTextBox(dsList.RowCount - 1, "methpaytype_code").Focus();

                decimal li_row = dsList.RowCount - 1;
                for (int i = 0; i < li_row; i++)
                {
                    dsList.FindTextBox(i, "methpaytype_code").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaytype_desc").ReadOnly = true;
                    dsList.FindTextBox(i, "sign_flag_text").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaystm_itemtype").ReadOnly = true;

                    dsList.FindTextBox(i, "running_number").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_code").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_desc").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "sign_flag_text").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaystm_itemtype").BackColor = System.Drawing.Color.LightGray;
                    decimal li_sign_flag = dsList.DATA[i].SIGN_FLAG;
                    if (li_sign_flag == 1)
                    {
                        dsList.DATA[i].sign_flag_text = "+";
                    }
                    else
                    {
                        dsList.DATA[i].sign_flag_text = "-";
                    }
                }
                dsList.FindTextBox(dsList.RowCount - 1, "methpaytype_code").ReadOnly = false;
                dsList.FindTextBox(dsList.RowCount - 1, "methpaytype_code").BackColor = System.Drawing.Color.White;
            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = dsList.GetRowFocus();
                dsList.DeleteRow(ls_getrow);
                decimal li_row = dsList.RowCount;
                for (int i = 0; i < li_row; i++)
                {
                    dsList.FindTextBox(i, "methpaytype_code").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaytype_desc").ReadOnly = true;
                    dsList.FindTextBox(i, "sign_flag_text").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaystm_itemtype").ReadOnly = true;

                    dsList.FindTextBox(i, "running_number").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_code").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_desc").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "sign_flag_text").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaystm_itemtype").BackColor = System.Drawing.Color.LightGray;
                    decimal li_sign_flag = dsList.DATA[i].SIGN_FLAG;
                    if (li_sign_flag == 1)
                    {
                        dsList.DATA[i].sign_flag_text = "+";
                    }
                    else
                    {
                        dsList.DATA[i].sign_flag_text = "-";
                    }

                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                decimal li_rowcount = dsList.RowCount;
                for (int i = 0; i < li_rowcount; i++)
                {
                    string ls_sig_flag_text = dsList.DATA[i].sign_flag_text;
                    if (ls_sig_flag_text == "+")
                    {
                        dsList.DATA[i].SIGN_FLAG = 1;
                    }
                    else
                    {
                        dsList.DATA[i].SIGN_FLAG = -1;
                    }

                }
                exed1.AddRepeater(dsList);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsList.ResetRow();
                dsList.RetrieveList();
                decimal li_row = dsList.RowCount;
                for (int i = 0; i < li_row; i++)
                {
                    dsList.FindTextBox(i, "methpaytype_code").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaytype_desc").ReadOnly = true;
                    dsList.FindTextBox(i, "sign_flag_text").ReadOnly = true;
                    dsList.FindTextBox(i, "methpaystm_itemtype").ReadOnly = true;

                    dsList.FindTextBox(i, "running_number").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_code").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaytype_desc").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "sign_flag_text").BackColor = System.Drawing.Color.LightGray;
                    dsList.FindTextBox(i, "methpaystm_itemtype").BackColor = System.Drawing.Color.LightGray;
                    decimal li_sign_flag = dsList.DATA[i].SIGN_FLAG;
                    if (li_sign_flag == 1)
                    {
                        dsList.DATA[i].sign_flag_text = "+";
                    }
                    else
                    {
                        dsList.DATA[i].sign_flag_text = "-";
                    }

                }

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