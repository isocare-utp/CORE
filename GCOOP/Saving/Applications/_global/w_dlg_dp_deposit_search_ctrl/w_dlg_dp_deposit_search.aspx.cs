using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_dp_deposit_search_ctrl
{
    public partial class w_dlg_dp_deposit_search : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DpBranchGroup();
                dsMain.DATA[0].COOP_ID = state.SsCoopControl;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String ls_deptaccount_no = "", ls_member_no = "", ls_deptaccount_name = "";
                String ls_memb_name = "", ls_memb_surname = "";

                String ls_coop_id = state.SsCoopControl;
                string ls_sqlext = "";

                ls_deptaccount_no = dsMain.DATA[0].DEPTACCOUNT_NO.Trim();
                ls_member_no = dsMain.DATA[0].MEMBER_NO.Trim();
                ls_deptaccount_name = dsMain.DATA[0].DEPTACCOUNT_NAME.Trim();
                ls_memb_name = dsMain.DATA[0].memb_name.Trim();
                ls_memb_surname = dsMain.DATA[0].memb_surname.Trim();

                if (ls_deptaccount_no.Length > 0)
                {
                    ls_member_no = " and (  dpdeptmaster.deptaccount_no like '%" + ls_deptaccount_no + "%') ";
                }
                if (ls_member_no.Length > 0)
                {
                    ls_sqlext += " and ( dpdeptmaster.member_no like '%" + ls_member_no + "%') ";
                }
                if (ls_deptaccount_name.Length > 0)
                {
                    ls_sqlext += " and ( dpdeptmaster.deptaccount_name like '%" + ls_deptaccount_name + "%') ";
                }
                if (ls_memb_name.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memb_name + "%' )";
                }
                if (ls_memb_surname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_memb_surname + "%' )";
                }

                String sql = @"
                SELECT dpdeptmaster.deptaccount_no  ,dpdeptmaster.member_no , 
                       dpdeptmaster.deptaccount_name , dpdeptmaster.PRNCBAL , dpdeptmaster.coop_id ,
                       Prename_desc || mbmembmaster.memb_name || ' ' || mbmembmaster.memb_surname as member_name
                FROM   dpdeptmaster , mbmembmaster 
                INNER JOIN mbucfprename
                      ON mbmembmaster.Prename_code=mbucfprename.Prename_code
                WHERE  dpdeptmaster.coop_id = mbmembmaster.coop_id and 
                       dpdeptmaster.member_no = mbmembmaster.member_no and
                       rownum <= 300 and 
                       dpdeptmaster.coop_id = '" + dsMain.DATA[0].COOP_ID + "' " + ls_sqlext;
                DataTable dt = WebUtil.Query(sql);
                dsList.ImportData(dt);
                LbCount.Text = "ดึงข้อมูล" + (dt.Rows.Count >= 300 ? "แบบสุ่ม" : "ได้") + " " + dt.Rows.Count + " รายการ";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}