using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary;
using CommonLibrary.WsCommon;

namespace Saving.Applications.keeping
{
    public partial class w_dlg_sl_kpslipno_search : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        string ls_sqlcoop = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);
            ls_sqlcoop = "and (  mbmembmaster.coop_id = '" + state.SsCoopId+"') ";

            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
            }
            if (!hidden_search.Value.Equals(""))
            {
                //dw_detail.SetSqlSelect(hidden_search.Value);
                //aek
               // dw_detail.Retrieve();

                String ls_sql = "", ls_sqlext = "", ls_temp = "";
                
                ls_sql = dw_detail.GetSqlSelect();
                ls_sqlext = ls_sqlcoop;
                ls_temp = ls_sql + ls_sqlext;
                hidden_search.Value = ls_temp;
                dw_detail.SetSqlSelect(hidden_search.Value);
                dw_detail.Retrieve(); 
            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SQLCA.Disconnect();
        }

        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_docno = "", ls_period = "";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_detail.GetSqlSelect();
            try
            {
                ls_memno = dw_data.GetItemString(1, "member_no").Trim();
                ls_memno = WebUtil.MemberNoFormat(ls_memno);
              

            }
            catch { ls_memno = ""; }
           
            try
            {
                ls_memname = dw_data.GetItemString(1, "memb_name").Trim();

            }
            catch { ls_memname = ""; }
            try
            {
                ls_memsurname = dw_data.GetItemString(1, "memb_surname").Trim();

            }
            catch { ls_memsurname = ""; }
            try
            {
                ls_docno = dw_data.GetItemString(1, "doc_no").Trim();

            }
            catch { ls_docno = ""; }
            try
            {
                ls_period = dw_data.GetItemString(1, "recv_period").Trim();

            }
            catch { ls_period = ""; }

            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '" + ls_memno + "%')";
            }
           
            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') ";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%')";
            }
            if (ls_period.Length > 0)
            {
                ls_sqlext += " and ( kptempreceive.recv_period like '" + ls_period + "%') ";
            }
            if (ls_docno.Length > 0)
            {
                ls_sqlext += " and ( kptempreceive.kpslip_no like '" + ls_docno + "%')";
            }

            ls_temp = ls_sql + ls_sqlext + ls_sqlcoop;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();

        }
    }
}
