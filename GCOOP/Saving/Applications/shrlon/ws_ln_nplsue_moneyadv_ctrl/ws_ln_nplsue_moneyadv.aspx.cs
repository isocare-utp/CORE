using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl
{
    public partial class ws_ln_nplsue_moneyadv : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string ls_membno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(ls_membno);
                dsDetail.ResetRow();
                string ls_sql = @"select count(1) as sum_row
                    from lnnplfollowmaster
                    where coop_id = {0}
                    and member_no = {1}";
                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_membno);
                Sdt dt = WebUtil.QuerySdt(ls_sql);

                if (dt.Next())
                {
                    if (dt.GetInt32("sum_row") > 0)
                    {
                        dsDetail.Retrieve(ls_membno);
                    }
                }
                dsDetail.DdContlawStatus();
            }
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
                string ls_membno = dsDetail.DATA[0].MEMBER_NO;

                if (ls_membno == "")
                {
                    dsDetail.DATA[0].COOP_ID = state.SsCoopControl;
                    dsDetail.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
                    dsDetail.DATA[0].FOLLOW_SEQ = 1;
                    exe.AddFormView(dsDetail, ExecuteType.Insert);
                    exe.Execute();
                }
                else
                {
                    exe.AddFormView(dsDetail, ExecuteType.Update);
                    exe.Execute();
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsMain.ResetRow();
                dsDetail.ResetRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}