using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Drawing;

namespace Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl
{
    public partial class ws_mbshr_close_mems : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostRetrieveList { get; set; }
        [JsPostBack]
        public string PostRetriveDetail { get; set; }
        [JsPostBack]
        public string PostFlagCheck { get; set; }

        public void InitJsPostBack()
        {
            dsCriteria.InitDsCriteria(this);
            dsList.InitDsList(this);
            dsShare.InitDsShare(this);
            dsLoan.InitDsLoan(this);
            dsCollwho.InitDsCollwho(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Hdclose_type.Value = "0";
                RetrieveList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrieveList")
            {
                RetrieveList();
            }
            else if (eventArg == "PostRetriveDetail")
            {
                try
                {
                    int row = dsList.GetRowFocus();

                    setcolor(row);                    

                    string member_no = dsList.DATA[row].MEMBER_NO;
                    dsShare.ResetRow();
                    dsLoan.ResetRow();
                    dsCollwho.ResetRow();
                    dsShare.RetriveShare(member_no);
                    dsLoan.RetriveLoan(member_no);
                    dsCollwho.RetriveCollwho(member_no);
      
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
        }

        private void RetrieveList()
        {
            try
            {
                decimal close_type = Convert.ToDecimal(Hdclose_type.Value);
                dsList.ResetRow();
                dsShare.ResetRow();
                dsLoan.ResetRow();
                dsCollwho.ResetRow();

                dsList.PostRetrieveList(close_type);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void setcolor( int r)
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(255, 255, 255);
            for (int i = 0; i < dsList.RowCount; i++)
            {
                dsList.FindTextBox(i, "running_number").BackColor = myRgbColor;
                dsList.FindTextBox(i, "member_no").BackColor = myRgbColor;
                dsList.FindTextBox(i, "cp_name").BackColor = myRgbColor;
                dsList.FindTextBox(i, "cp_groupdesc").BackColor = myRgbColor;
                dsList.FindTextBox(i, "resign_date").BackColor = myRgbColor;
                dsList.FindTextBox(i, "resigncause_desc").BackColor = myRgbColor;
            }

            myRgbColor = Color.FromArgb(50, 150, 255);

            dsList.FindTextBox(r, "running_number").BackColor = myRgbColor;
            dsList.FindTextBox(r, "member_no").BackColor = myRgbColor;
            dsList.FindTextBox(r, "cp_name").BackColor = myRgbColor;
            dsList.FindTextBox(r, "cp_groupdesc").BackColor = myRgbColor;
            dsList.FindTextBox(r, "resign_date").BackColor = myRgbColor;
            dsList.FindTextBox(r, "resigncause_desc").BackColor = myRgbColor;
        }

        public void SaveWebSheet()
        {
            try
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].operate_flag == 1)
                    {
                        dsList.DATA[i].CLOSE_DATE = state.SsWorkDate;
                        dsList.DATA[i].MEMBER_STATUS = -1; 
                    }
                    else
                    {
                        dsList.DATA[i].MEMBER_STATUS = 1;
                        dsList.DATA[i].SetCLOSE_DATENull();
                    }
                }

                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                RetrieveList();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {
            decimal close_type = Convert.ToDecimal(Hdclose_type.Value);
            if (close_type == 1)
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    dsList.FindCheckBox(i, dsList.DATA.operate_flagColumn).Enabled = false;
                }
            }
        }
    }
}