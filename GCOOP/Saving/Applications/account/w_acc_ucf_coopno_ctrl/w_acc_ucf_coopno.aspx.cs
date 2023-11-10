using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
namespace Saving.Applications.account.w_acc_ucf_coopno_ctrl
{
    public partial class w_acc_ucf_coopno : PageWebSheet, WebSheet
    {
        String coop_desc = "";
        public void InitJsPostBack()
        {
            wd_list.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                wd_list.RetrieveList();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

        public void SaveWebSheet()
        {
            try
            {
                string coop_register = wd_list.DATA[0].COOP_REGISTERED_NO;
                string coop_desc = wd_list.DATA[0].COOP_DESC;
                String update_data = "UPDATE ACCCNTCOOP set COOP_DESC ='" + coop_desc + "',COOP_REGISTERED_NO='" + coop_register + "' where coop_id ='" + state.SsCoopId + "' ";
                Sdt sqlupdate = WebUtil.QuerySdt(update_data);

                //ExecuteDataSource exed1 = new ExecuteDataSource(this);
                //exed1.AddRepeater(wd_list);
                //exed1.Execute();
                //wd_list.RetrieveList();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                wd_list.ResetRow();
                wd_list.RetrieveList();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}