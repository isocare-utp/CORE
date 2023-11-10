using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_mg_autzd_search_ctrl
{
    public partial class w_dlg_mg_autzd_search : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
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

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string ls_autzd_name = "";
                try
                {
                    ls_autzd_name = autzd_name.Text.ToString().Trim();
                    ls_autzd_name = "%" + ls_autzd_name + "%";
                }
                catch { ls_autzd_name = "%"; }

                string sql = sql = @"select template_no, 
                    autzd_name, 
                    'บ้าน' + autzd_village +  ' ' + autzd_address + ' หมู่ ' + autzd_moo + ' ซ.' + autzd_soi + ' ถ.' + autzd_road + ' ต.' + autzd_tambol + ' อ.' + autzd_amphur + ' จ.' + autzd_province as cp_address 
                    from lnmrtgtemplateautzd 
                    where coop_id = '" + state.SsCoopControl + "' and autzd_name like '" + ls_autzd_name + "'";
                DataTable dt = WebUtil.Query(sql);
                dsList.ImportData(dt);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}