using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_mb_member_search_lite_ctrl
{
    public partial class w_dlg_mb_member_search_lite : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsCriteria.InitDsCriteria(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsCriteria.DdMembGroup();
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
                String ls_memno = "", ls_memname = "", ls_memsurname = "";
                String ls_memgroup = "", ls_contno = "";

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";

                ls_memno = dsCriteria.DATA[0].member_no.Trim();
                ls_memname = dsCriteria.DATA[0].memb_name.Trim();
                ls_memsurname = dsCriteria.DATA[0].memb_surname.Trim();
                ls_memgroup = dsCriteria.DATA[0].membgroup_no.Trim();
                ls_contno = dsCriteria.DATA[0].loancontract_no.Trim();

                if (ls_memno.Length > 0)
                {
                    ls_sqlext = " and (  mbmembmaster.member_no like '%" + ls_memno + "%') ";
                }
                if (ls_memname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memname + "%') ";
                }
                if (ls_memsurname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_memsurname + "%') ";
                }
                if (ls_memgroup.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + ls_memgroup + "' )";
                }
                if (ls_contno.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '%" + ls_contno + "%')) ";
                }
                string sql = @"
                select 
                    mbmembmaster.member_no, 
                    mbucfprename.prename_desc, 
                    mbmembmaster.memb_name, 
                    mbmembmaster.memb_surname, 
                    mbmembmaster.membgroup_code, 
                    mbucfmembgroup.membgroup_desc
                from         
                    mbmembmaster, mbucfmembgroup, mbucfprename
                where
                    mbmembmaster.coop_id = mbucfmembgroup.coop_id and
                    mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code and
                    mbmembmaster.prename_code = mbucfprename.prename_code and
                    rownum <= 300 and
                    mbmembmaster.coop_id = '" + state.SsCoopControl + "' " + ls_sqlext;
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