using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_collredeem_search_ctrl
{
    public partial class w_dlg_sl_collredeem_search : PageWebDialog, WebDialog
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
                dsCriteria.DdCollmasttypeCode();
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
                String member_no = "", memb_name = "", memb_surname = "";
                String membgroup_code = "", collmasttype_code = "";

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";

                member_no = WebUtil.MemberNoFormat(dsCriteria.DATA[0].member_no.Trim());
                memb_name = dsCriteria.DATA[0].memb_name.Trim();
                memb_surname = dsCriteria.DATA[0].memb_surname.Trim();
                membgroup_code = dsCriteria.DATA[0].membgroup_no.Trim();
                collmasttype_code = dsCriteria.DATA[0].membtype_code.Trim();

                if (member_no.Length > 0)
                {
                    ls_sqlext = " and (  lncollmastmemco.memco_no = '" + member_no + "') ";
                }
                if (memb_name.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + memb_name + "%') ";
                }
                if (memb_surname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + memb_surname + "%') ";
                }

                if (membgroup_code.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + membgroup_code + "' )";
                }
                if (collmasttype_code.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.collmasttype_code = '" + collmasttype_code + "' )";
                }

                string sql = sql = @"select distinct top 300
	                mbmembmaster.member_no, 
	                mbucfprename.prename_desc, 
	                mbmembmaster.memb_name, 
	                mbmembmaster.memb_surname, 
	                mbmembmaster.membgroup_code, 
	                mbucfmembgroup.membgroup_desc,
	                lncollmaster.collmast_no,
	                lncollmaster.collmasttype_code,
	                lncollmaster.collmast_desc,
	                lncollmaster.collmast_price
                from mbmembmaster, 
	                mbucfmembgroup, 
	                mbucfprename,
	                lncollmaster,
	                lncollmastmemco
                where ( mbmembmaster.coop_id = mbucfmembgroup.coop_id )
	                and ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code )
	                and ( mbmembmaster.prename_code = mbucfprename.prename_code )
	                and ( mbmembmaster.coop_id = lncollmastmemco.coop_id )
	                and ( mbmembmaster.member_no = lncollmastmemco.memco_no )
 	                and ( lncollmaster.coop_id = lncollmastmemco.coop_id )
	                and ( lncollmaster.collmast_no = lncollmastmemco.collmast_no )  
	                and ( lncollmaster.redeem_flag = 0 )
	               
                    and ( lncollmaster.coop_id = '" + coop_id + "' ) " + ls_sqlext;
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