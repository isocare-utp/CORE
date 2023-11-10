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
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon; // new common
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_cm_constant_config : PageWebSheet, WebSheet
    {
        //private WebState state;
        String menu_temp = "";
        String link_temp = "";
        String listmenu = "";
        String pagename = "";
        protected String linkForeDd = "";

        private String getConstantConfigMenu(String app, String uopage)
        {
            //Sta ta = new Sta(state.SsConnectionIndex);
            if (uopage == "")
            {
                String sql = "select * from amcnstconfigsystem where application = '" + state.SsApplication + "' order by seq_no asc";
                //Sdt dt = ta.Query(sql);
                //int i = 0;
                //while (dt.Next())
                //{
                //    menu_temp = dt.Rows[i]["config_desc"].ToString();
                //    link_temp = dt.Rows[i]["object_name"].ToString();
                //    listmenu = listmenu + "<a href=\"?uoucf=" + link_temp + "\">" + menu_temp + "</a>";
                //    i++;
                //}
                DataTable dt1 = WebUtil.Query(sql);
                DropDownList1.DataSource = dt1;
                DropDownList1.DataBind();
            }
            else
            {
                String sql = "select config_desc from amcnstconfigsystem where application = '" + state.SsApplication + "' and object_name='" + uopage + "'";
                //Sdt dt = ta.Query(sql);
                //this.pagename = dt.Rows[0]["config_desc"].ToString();
                DataTable dt1 = WebUtil.Query(sql);
                DropDownList1.DataSource = dt1;
                DropDownList1.DataBind();
            }
            //ta.Close();
            return listmenu;
        }

        private void ChangePanel(String uo)
        {
            uo = "w_sheet_cm_constant_config/" + uo + ".ascx";
            try
            {
                Panel1.Controls.Clear();
            }
            catch { }
            Control ct = LoadControl(uo);
            Panel1.Controls.Add(ct);
        }

        public void InitJsPostBack()
        {
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //ltr_cnstmenu.Text = getConstantConfigMenu(state.SsApplication, "");
                getConstantConfigMenu(state.SsApplication, "");
            }
            linkForeDd = Request.Url.AbsoluteUri;
            try
            {
                int indexOflinkForExdd = linkForeDd.IndexOf("&uoucf=");
                if (indexOflinkForExdd > 0)
                {
                    linkForeDd = linkForeDd.Substring(0, indexOflinkForExdd);
                }
            }
            catch { }
            try
            {
                String uoName = "";
                try
                {
                    uoName = Request["uoucf"].Trim();
                }
                catch { }
                if (string.IsNullOrEmpty(uoName))
                {
                    uoName = DropDownList1.SelectedItem.Value;
                }
                else
                {
                    for (int i = 0; i < DropDownList1.Items.Count; i++)
                    {
                        if (uoName == DropDownList1.Items[i].Value)
                        {
                            DropDownList1.SelectedIndex = i;
                            break;
                        }
                    }
                }
                ChangePanel(uoName);
                lbl_cnstmenu.Text = pagename;
                //if (Request["uoucf"] != null && Request["uoucf"].Trim() != "")
                //{
                //    ChangePanel(Request["uoucf"].Trim().ToLower());
                //    getConstantConfigMenu(state.SsApplication, Request["uoucf"].Trim().ToLower());
                //    lbl_cnstmenu.Text = pagename;
                //}
                //else
                //{
                //    ChangePanel("u_cmucfsliptype");
                //    getConstantConfigMenu(state.SsApplication, "u_cmucfsliptype");
                //    lbl_cnstmenu.Text = pagename;
                //}
            }
            catch { }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}