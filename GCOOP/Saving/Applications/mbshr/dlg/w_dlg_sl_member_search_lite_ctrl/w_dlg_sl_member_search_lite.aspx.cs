using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.dlg.w_dlg_sl_member_search_lite_ctrl
{
    public partial class w_dlg_sl_member_search_lite : PageWebDialog, WebDialog
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
                dsCriteria.DdMembType();
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
                String ls_memno = "", ls_salaryid = "", ls_cardperson = "";
                String ls_memname = "", ls_memsurname = "", ls_contno = "";
                String ls_memgroup = "", ls_memtypecode = "", ls_memgroup_desc = "";

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";

                ls_memno = dsCriteria.DATA[0].member_no.Trim();
                ls_salaryid = dsCriteria.DATA[0].salary_id.Trim();
                ls_cardperson = dsCriteria.DATA[0].card_person.Trim();
                ls_memname = dsCriteria.DATA[0].memb_name.Trim();
                ls_memsurname = dsCriteria.DATA[0].memb_surname.Trim();
                ls_contno = dsCriteria.DATA[0].loancontract_no.Trim();
                ls_memgroup = dsCriteria.DATA[0].membgroup_no.Trim();
                ls_memtypecode = dsCriteria.DATA[0].membtype_code.Trim();
                ls_memgroup_desc = dsCriteria.DATA[0].membgroup_desc.Trim();

                if (ls_memno.Length > 0)
                {
                    ls_sqlext = " and (  mbmembmaster.member_no like '%" + ls_memno + "%') ";
                }
                if (ls_salaryid.Length > 0)
                {
                    ls_sqlext = " and (  mbmembmaster.salary_id like '%" + ls_salaryid + "%') ";
                }
                if (ls_cardperson.Length > 0)
                {
                    ls_sqlext = " and (  mbmembmaster.card_person like '%" + ls_cardperson + "%') ";
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
                if (ls_memtypecode.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.membtype_code = '" + ls_memtypecode + "' )";
                }
                if (ls_contno.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '%" + ls_contno + "%')) ";
                }
                if (ls_memgroup_desc.Length > 0)
                {
                    ls_sqlext += " and ( mbucfmembgroup.membgroup_desc  like '%" + ls_memgroup_desc + "%') ";
                }

                string sql = sql = @"
                  SELECT TOP 100
                    MBMEMBMASTER.MEMBER_NO, 
                    MBUCFPRENAME.PRENAME_DESC, 
                    MBMEMBMASTER.MEMB_NAME, 
                    MBMEMBMASTER.MEMB_SURNAME, 
                    MBMEMBMASTER.MEMBGROUP_CODE, 
                    MBUCFMEMBGROUP.MEMBGROUP_DESC
                FROM         
                    MBMEMBMASTER, MBUCFMEMBGROUP, MBUCFPRENAME
                WHERE     
                    MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE AND 
                    MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID AND 
                    MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE AND 
                    MBMEMBMASTER.COOP_ID = '" + coop_id + "' " + ls_sqlext ;

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