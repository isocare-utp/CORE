using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr_const.ws_mb_mbconstant_ctrl
{
    public partial class ws_mb_mbconstant : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberType { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.SetItem(0, dsMain.DATA.COOP_IDColumn, state.SsCoopControl);
                dsMain.SetItem(0, dsMain.DATA.MEMBER_TYPEColumn, 1);
                dsMain.retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberType)
            {
                dsMain.retrieve();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                exed.AddFormView(dsMain, ExecuteType.Update);
                int result = exed.Execute();
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    dsMain.ResetRow();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ตรวจสอบอีกครั้ง");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}