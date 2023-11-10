using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace Saving.Applications.admin.w_sheet_am_apvlevel_ctrl
{
    public partial class w_sheet_am_apvlevel : PageWebSheet,WebSheet
    {
        [JsPostBack]
        public string PostApv { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostApv) {
                int row = dsList.GetRowFocus();
                dsDetail.RetrieveApv(dsList.DATA[row].APVLEVEL_ID);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                exed.AddFormView(dsDetail, ExecuteType.Update);
                int i = exed.Execute();                
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }


    }
}