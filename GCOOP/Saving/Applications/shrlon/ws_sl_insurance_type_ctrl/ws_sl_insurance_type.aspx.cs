using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Drawing;

namespace Saving.Applications.shrlon.ws_sl_insurance_type_ctrl
{
    public partial class ws_sl_insurance_type : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostInsType { get; set; }

        public void InitJsPostBack()
        {            
            
            dsList.InitDsList(this);
            dsDetail.InitDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            InitJsPostBack();
            if (!IsPostBack)
            {
                dsList.RetrieveList();
            }else{
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsType)
            {
                int r = dsList.GetRowFocus();

                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if(i != r){
                        dsList.FindTextBox(i, "instype_code").BackColor = Color.White;
                        dsList.FindTextBox(i, "instype_desc").BackColor = Color.White;
                    }
                }
                dsList.FindTextBox(r, "instype_code").BackColor = Color.SkyBlue;
                dsList.FindTextBox(r, "instype_desc").BackColor = Color.SkyBlue;

                string instype_code = dsList.DATA[r].INSTYPE_CODE;
                dsDetail.RetrieveDetail(instype_code);
              //  dsDetail.Ddloantype();

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsDetail, ExecuteType.Update);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsList.ResetRow();
                dsDetail.ResetRow();
                dsList.RetrieveList();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}