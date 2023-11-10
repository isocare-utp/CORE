using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_redeem_search_ctrl
{
    public partial class w_dlg_sl_redeem_search : PageWebDialog, WebDialog
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
                String member_no = "", mrtgmast_no = "";                

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";

                member_no = WebUtil.MemberNoFormat(dsCriteria.DATA[0].member_no.Trim());
                mrtgmast_no = dsCriteria.DATA[0].mrtgmast_no.Trim();

                if (member_no != "" && member_no != "00000000")
                {
                    ls_sqlext = " and (  ln.member_no = '" + member_no + "') ";
                }
                else if (mrtgmast_no != "")
                {
                    ls_sqlext = " and (  ln.mrtgmast_no = '" + mrtgmast_no + "') ";
                }
                string sql = sql = @"select distinct
                    ln.mrtgmast_no,
	                mb.member_no, 	            
	                mb.memb_name, 
	                mb.memb_surname,
                    ln.mortgage_date,
                    ln.mortgagesum_amt                                                        
                from mbmembmaster mb,
                    lnmrtgmaster ln	             
                where ( mb.member_no = ln.member_no )	              
	                and ( ln.mrtgmast_status = 0 )	                
                    and ( ln.coop_id = '" + coop_id + "' ) " + ls_sqlext;
                DataTable dt = WebUtil.Query(sql);
                dsList.ImportData(dt);
                LbCount.Text = "ดึงข้อมูลได้"+ dt.Rows.Count + " รายการ";
                dsCriteria.DATA[0].member_no = dt.Rows[0]["member_no"].ToString();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}