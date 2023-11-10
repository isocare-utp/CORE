using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl
{
    public partial class w_dlg_sl_receive_ref_slip : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostRefSystem { get; set; }
        [JsPostBack]
        public string PostMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitDs(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    string member_no = Request["member_no"];
                    dsMain.DATA[0].MEMBER_NO = member_no;
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRefSystem")
            {
                try
                {
                    string ref_sys = "", member_no = "";
                    ref_sys = dsMain.DATA[0].REF_SYSTEM;
                    member_no = dsMain.DATA[0].MEMBER_NO;
                    dsList.RetrieveList(ref_sys, member_no);
                }
                catch { }
            }
            else if (eventArg == "PostMemberNo")
            {
                try
                {
                    string ref_sys = "", member_no = "";
                    member_no = dsMain.DATA[0].MEMBER_NO;
                    string sql = "select * from mbmembmaster where member_no={0} and coop_id = {1}";
                    member_no = WebUtil.MemberNoFormat(member_no);
                    sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopId);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMain.DATA[0].MEMBER_NO = member_no;
                        LtServerMessageDlg.Text = "";
                        ref_sys = dsMain.DATA[0].REF_SYSTEM;
                        if (ref_sys != "" && ref_sys != null) {
                            dsList.RetrieveList(ref_sys, member_no);
                        }
                    }
                    else
                    {
                        LtServerMessageDlg.Text = WebUtil.ErrorMessage("ไม่พบเลขสมาชิกดังกล่าว กรุณาตรวจสอบเลขสมาชิก");

                    }


                }
                catch { }

            }
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}