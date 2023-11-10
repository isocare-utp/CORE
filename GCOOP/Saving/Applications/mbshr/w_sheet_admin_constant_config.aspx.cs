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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_admin_constant_config : System.Web.UI.Page
    {
        private WebState state;
        String menu_temp = "";
        String link_temp = "";
        String listmenu = "";
        String pagename = "";
        String header = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            //if (state.IsReadable)
            //{
                LtListMenu.Text = this.getConstantConfigMenu("", "");
                try
                {
                    if (Request["uoucf"] != null && Request["uoucf"].Trim() != "")
                    {
                        ChangePanel(Request["uoucf"].Trim().ToLower());
                        getConstantConfigMenu(Request["appl"].ToString(), Request["uoucf"].Trim().ToLower());
                        lbl_cnstmenu.Text = pagename;
                    }
                }
                catch { }
            //}
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
        }

        private String getConstantConfigMenu(String app, String uopage)
        {
            Sta ta = new Sta(new DwTrans().ConnectionString);
            if (uopage == "")
            {
                String[] allAppl = this.GetAdminApplSupport();
                for (int a = 0; a < allAppl.Length; a++)
                {   
                    String sql = "select * from amcnstconfigsystem where application = '" + allAppl.GetValue(a) +"' order by seq_no asc";
                    
                    Sdt dt = ta.Query(sql);
                    int i = 0;
                    String head;
                    while (dt.Next())
                    {
                        if (header == "")
                        {
                            header = dt.Rows[i]["application"].ToString();
                            head = header;
                            if (head == "admins")
                            {
                                head = "ผู้ดูแลระบบ";
                            }
                            else if (head == "ap_deposit")
                            {
                                head = "ระบบเงินฝาก";
                            }
                            else if (head == "keeping")
                            {
                                head = "ระบบประมวลผลจัดเก็บ";
                            }
                            else if (head == "member")
                            {
                                head = "ระบบสมาชิก";
                            }
                            else if (head == "mbshr")
                            {
                                head = "ระบบสมาชิก";
                            }
                            else if (head == "shrlon")
                            {
                                head = "ระบบหุ้นหนี้";
                            }
                            listmenu += "<tr><td><span onclick=\"clickedMenu('" + header + "')\" style=\"cursor:pointer;\">" + head + "</span></td></tr><tr><td><div id=\"" + header + "_detail\" ><table>";
                           
                        }
                        else if (header != dt.Rows[i]["application"].ToString())
                        {

                          header = dt.Rows[i]["application"].ToString();
                          head = header;
                          if (head == "admins")
                          {
                              head = "ผู้ดูแลระบบ";
                          }
                          else if (head == "ap_deposit")
                          {
                              head = "ระบบเงินฝาก";
                          }
                          else if (head == "keeping")
                          {
                              head = "ระบบประมวลผลจัดเก็บ";
                          }
                          else if (head == "member")
                          {
                              head = "ระบบสมาชิก";
                          }
                          else if (head == "shrlon")
                          {
                              head = "ระบบหุ้นหนี้";
                          }
                          listmenu += "<tr><td><span onclick=\"clickedMenu('" + header + "')\" style=\"cursor:pointer;\">" + head + "</span></td></tr><tr><td><div id=\"" + header + "_detail\" ><table>";
                       
                        }
                        
                        menu_temp = dt.Rows[i]["config_desc"].ToString();
                        link_temp = dt.Rows[i]["object_name"].ToString();
                        listmenu += "<tr><td><a href=\"?uoucf=" + link_temp + "&appl=" + app + "\"> - " + menu_temp + "</a></td></tr>";
                        i++;
                       
                    }
                    listmenu += "</div></table></td></tr>";
                    
                }
            }
            else
            {
                
                String sql = "select config_desc from amcnstconfigsystem where application ='" + app + "' and object_name='" + uopage + "'";
                Sdt dt = ta.Query(sql);
                this.pagename = dt.Rows[0]["config_desc"].ToString();

            }
            ta.Close();
           
            return listmenu;
        }

        private void ChangePanel(String uo)
        {
            uo = "w_sheet_admin_constant_config/" + uo + ".ascx";
            try
            {
                Panel1.Controls.Clear(); Control ct = LoadControl(uo);
                Panel1.Controls.Add(ct);
            }
            catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }

        }

        private String[] GetAdminApplSupport()
        {


            Sta ta = new Sta(new DwTrans().ConnectionString);
            //aek
            //String sql = "select * from AMCNSTCONFIGSUPP where application = 'admins' order by applsupport asc";
            String sql = "select * from AMCNSTCONFIGSUPP where application = '"+state.SsApplication+"' order by applsupport asc";
            Sdt dt = ta.Query(sql);
            int allrow = dt.Rows.Count;
            String[] allApp = new String[allrow];
            int i = 0;
            while (dt.Next())
            {
                allApp[i] = dt.Rows[i]["applsupport"].ToString();
               
                i++;
            }
            return allApp;
        }
    }
}
