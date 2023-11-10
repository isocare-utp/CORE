using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.keeping.ws_kp_acc_ccl_ctrl
{
    public partial class ws_kp_acc_ccl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String postMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                SetMaxPeriod();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postMemberNo)
            {
                string recv_period = dsMain.DATA[0].recv_period;
                dsMain.Retrieve();
                dsMain.DATA[0].recv_period = recv_period;
                dsList.Retrieve(dsMain.DATA[0].MEMBER_NO, recv_period);
            }
        }

        public void SetMaxPeriod()
        {
            string sql = "select max(recv_period) from kpmastreceive where coop_id = '" + state.SsCoopControl + "'";
            Sdt dt = WebUtil.QuerySdt(sql); ;
            if (dt.Next())
            {
                string recv_period = dt.GetString("max(recv_period)");
                dsMain.DATA[0].recv_period = recv_period;
            }
        }
        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                dsMain.ResetRow();
                dsList.ResetRow();
                SetMaxPeriod();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {            
        }
    }
}