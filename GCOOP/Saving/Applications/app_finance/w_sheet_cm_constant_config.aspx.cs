using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_cm_constant_config : System.Web.UI.Page
    {
        WebState state;
        String menu_temp = "";
        String link_temp = "";
        String listmenu = "";
        String pagename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            ltr_cnstmenu.Text = getConstantConfigMenu(state.SsApplication, "");
            try
            {
                if (Request["uoucf"] != null && Request["uoucf"].Trim() != "")
                {
                    ChangePanel(Request["uoucf"].Trim().ToLower());
                    getConstantConfigMenu(state.SsApplication, Request["uoucf"].Trim().ToLower());
                    lbl_cnstmenu.Text = pagename;
                }
                else
                {
                    ChangePanel("u_fnucftaxtype");
                    getConstantConfigMenu(state.SsApplication, "u_fnucftaxtype");
                    lbl_cnstmenu.Text = pagename;
                }
            }
            catch { }

        }

        private String getConstantConfigMenu(String app, String uopage)
        {
            Sta ta = new Sta(new DwTrans().ConnectionString);
            if (uopage == "")
            {
                String sql = "select * from amcnstconfigsystem where application = '" + app + "' order by seq_no asc";
                Sdt dt = ta.Query(sql);
                int i = 0;
                while (dt.Next())
                {
                    menu_temp = dt.Rows[i]["config_desc"].ToString();
                    link_temp = dt.Rows[i]["object_name"].ToString();
                    listmenu = listmenu + "<a href=\"?uoucf=" + link_temp + "\">" + menu_temp + "</a>";
                    i++;
                }
            }
            else
            {
                String sql = "select config_desc from amcnstconfigsystem where application = '" + app + "' and object_name='" + uopage + "'";
                Sdt dt = ta.Query(sql);
                this.pagename = dt.Rows[0]["config_desc"].ToString();

            }
            ta.Close();
            return listmenu;
        }


        private void ChangePanel(String uo)
        {
            uo = "w_sheet_cm_constant_config/" + uo + ".ascx";
            try
            {
                pnl_cnst.Controls.Clear();
            }
            catch { }
            Control ct = LoadControl(uo);
            pnl_cnst.Controls.Add(ct);

        }
    }
}
