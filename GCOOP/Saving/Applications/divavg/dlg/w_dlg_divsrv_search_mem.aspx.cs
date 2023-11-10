using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.divavg.dlg
{
    public partial class w_dlg_divsrv_search_mem : PageWebDialog,WebDialog
    {
        protected String postSearch;
        protected String postNewClear;
        //==========================
        public String pbl = "divsrv_search_mem.pbl";
        WebState state;
        DwTrans SQLCA;

        public void InitJsPostBack()
        {
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

        
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            Dw_main.SetTransaction(SQLCA);
            Dw_detail.SetTransaction(SQLCA);


            if (!IsPostBack)
            {
               
                JspostNewClear();
            }

            if (!hidden_search.Value.Equals(""))
            {
                Dw_detail.SetSqlSelect(hidden_search.Value);
                Dw_detail.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSearch")
            {
                JspostSearch();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
        }

        public void WebDialogLoadEnd()
        {

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();

            SQLCA.Disconnect();
        }
        //========================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "branch_id", state.SsCoopId);
            Dw_detail.Reset();
            hidden_search.Value = "";
            Hdbranch_id.Value = "";
            hd_smembtype_code.Value = "";
            hd_emembtype_code.Value = "";
            Hd_smembgroup_code.Value = "";
            Hd_emembgroup_code.Value = "";
            Hd_smember_no.Value = "";
            Hd_emember_no.Value = "";
            Hd_memb_name.Value = "";
            Hd_memb_ename.Value = "";

            DwUtil.RetrieveDDDW(Dw_main, "branch_id", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "smembtype_code_1", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "emembtype_code_1", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "smembgroup_code_1", pbl, null);
            DwUtil.RetrieveDDDW(Dw_main, "emembgroup_code_1", pbl, null);
        }

        private void JspostSearch()
        {
            String ls_branchid = "", ls_emembtype_code = "", ls_smembgroup_code = "";
            String ls_smembtype_code = "", ls_emembgroup_code = "", ls_membno = "";
            String ls_emember_no = "", ls_memb_name = "", ls_smember_no = "";
            String ls_memb_ename = "";

            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = Dw_detail.GetSqlSelect();
            try
            {
                //ls_branchid = Dw_main.GetItemString(1, "branch_id").Trim();
                ls_branchid = Hdbranch_id.Value.Trim();

            }
            catch { ls_branchid = ""; }

            try
            {
                //ls_smembtype_code = Dw_main.GetItemString(1, "smembtype_code").Trim();
                ls_smembtype_code = hd_smembtype_code.Value.Trim();

            }
            catch { ls_smembtype_code = ""; }

            try
            {
                //ls_emembtype_code = Dw_main.GetItemString(1, "emembtype_code").Trim();
                ls_emembtype_code = hd_emembtype_code.Value.Trim();

            }
            catch { ls_emembtype_code = ""; }

            try
            {
                //ls_smembgroup_code = Dw_main.GetItemString(1, "smembgroup_code").Trim();
                ls_smembgroup_code = Hd_smembgroup_code.Value.Trim();

            }
            catch { ls_emembtype_code = ""; }

            try
            {
                //ls_emembgroup_code = Dw_main.GetItemString(1, "emembgroup_code").Trim();
                ls_emembgroup_code = Hd_emembgroup_code.Value.Trim();

            }
            catch { ls_emembtype_code = ""; }

            try
            {
                // ls_smember_no = Dw_main.GetItemString(1, "smember_no").Trim();
                ls_smember_no = Hd_smember_no.Value.Trim();

            }
            catch { ls_smember_no = ""; }
            try
            {
                //ls_emember_no = Dw_main.GetItemString(1, "emember_no").Trim();
                ls_emember_no = Hd_emember_no.Value.Trim();

            }
            catch { ls_emember_no = ""; }

            try
            {
                //ls_memb_name = Dw_main.GetItemString(1, "memb_name").Trim();
                ls_memb_name = Hd_memb_name.Value.Trim();

            }
            catch { ls_memb_name = ""; }

            try
            {
                //ls_memb_ename = Dw_main.GetItemString(1, "memb_ename").Trim();
                ls_memb_ename = Hd_memb_ename.Value.Trim();

            }
            catch { ls_memb_ename = ""; }



            if (ls_branchid.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.branch_id like '%" + ls_branchid + "%') ";
            }
            if (ls_smembtype_code.Length > 0 && ls_emembtype_code.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.membtype_code between '" + ls_smembtype_code + "' and '" + ls_emembtype_code + "') ";
            }
            if (ls_smembgroup_code.Length > 0 && ls_emembgroup_code.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.membgroup_code between '" + ls_smembgroup_code + "' and '" + ls_emembgroup_code + "') ";
            }
            if (ls_smember_no.Length > 0 && ls_emember_no.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no between '" + ls_smember_no + "' and '" + ls_emember_no + "') ";
            }
            if (ls_memb_name.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memb_name + "%')";
            }

            if (ls_memb_ename.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_ename like '" + ls_memb_ename + "%')";
            }

            ls_temp = ls_sql + ls_sqlext;
            if (ls_sql != ls_temp)
            {
                hidden_search.Value = ls_temp;
                DwUtil.ImportData(ls_temp, Dw_detail, null);
                JspostClearHidden();
            }
            else
            {
                Dw_detail.Reset();
            }
        }

        protected void JspostClearHidden()
        {
            hidden_search.Value = "";
            Hdbranch_id.Value = "";
            hd_smembtype_code.Value = "";
            hd_emembtype_code.Value = "";
            Hd_smembgroup_code.Value = "";
            Hd_emembgroup_code.Value = "";
            Hd_smember_no.Value = "";
            Hd_emember_no.Value = "";
            Hd_memb_name.Value = "";
            Hd_memb_ename.Value = "";
        }
    }
}