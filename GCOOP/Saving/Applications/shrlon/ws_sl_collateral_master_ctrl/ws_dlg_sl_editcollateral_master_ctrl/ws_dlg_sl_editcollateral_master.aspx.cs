using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.ws_dlg_sl_editcollateral_master_ctrl
{
    public partial class ws_dlg_sl_editcollateral_master : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                string member_no = "";
                string cp_name = "";
                string salary_id = "";
                string collmast_no = "";

                try
                {
                    member_no = "'%" + dsMain.DATA[0].member_no + "%'";
                }
                catch { member_no = "'%'"; }

                try
                {
                    cp_name = "'%" + dsMain.DATA[0].cp_name + "%'";
                }
                catch { cp_name = "'%'"; }

                /*try
                {
                    salary_id = "'%" + dsMain.DATA[0].salary_id + "%'";
                }
                catch { salary_id = "'%'"; }
                */
                try
                {
                    collmast_no = "'%" + dsMain.DATA[0].collmast_no + "%'";
                }
                catch { collmast_no = "'%'"; }
                string sql_search = "and mbmembmaster.member_no like " + member_no + " and mbmembmaster.memb_name + ' '+ mbmembmaster.memb_surname like " + cp_name 
                    //+ " and mbmembmaster.salary_id like " + salary_id 
                    + " and lncollmaster.collmast_no like " + collmast_no;

                dsList.RetrieveList(sql_search);
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}