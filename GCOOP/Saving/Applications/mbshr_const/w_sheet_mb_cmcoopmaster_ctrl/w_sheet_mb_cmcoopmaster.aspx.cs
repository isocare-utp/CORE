using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_cmcoopmaster_ctrl
{
    public partial class w_sheet_mb_cmcoopmaster : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                exed.AddFormView(dsList, ExecuteType.Update);
                int result = exed.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ"); //หากบันทึกให้ขึ้น บันทึกแล้ว

            }
            catch (Exception ex) {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        public void WebSheetLoadEnd()
        {
        }
    }
}