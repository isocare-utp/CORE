using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CoreSavingLibrary;
using System.Threading;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace Saving
{
    public partial class setup : System.Web.UI.Page
    {
        public bool AuthenFlag
        {
            get { 
                try{
                   return (Session["AuthenFlag"].ToString().ToLower() == "true");
                }catch{
                   return false;
                }
            }
        }

        public string CORE_PATH = "CORE";
        public string COOP_PATH = "GCOOP";
        public string ROOT_PATH = "C:\\GCOOP_ALL";
        public string XML_PATH = "XMLConfig";
        public string XML_SELECTED_PATH {
            get {
                try
                {
                    return this.XmlFilesListDDW.SelectedValue;
                }
                catch
                {
                    return "";
                }
            }

        }

        public List<string> EXTEND_LIST = new List<string>();
        public List<string> XML_LIST = new List<string>();
        public bool selectedExtendsFlag = false;

        public bool IsAuthenticated(string username, string password)
        {
            bool authenticated = false;

            try
            {
                String batchfilename="CheckPass";
                String outputfile = System.Environment.GetEnvironmentVariable("TEMP")+"\\"+batchfilename + "_" + DateTime.Now.Ticks + ".txt";
                List<string> cmdList=new List<string>();
                cmdList.Add("net use \\\\127.0.0.1\\" + System.Environment.GetEnvironmentVariable("TEMP").Replace(":","$") + " " + password + " /user:" + username + ">" + outputfile);
                WebUtil.RunCommand(cmdList,batchfilename);
                Thread.Sleep(2000);
                string results = File.ReadAllText(outputfile);
                authenticated = results.IndexOf("success")>0;
            }
            catch
            {
                
            }

            return authenticated;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.getCurrentRootPath();
                this.loadExtendsLists();

            }
            else
            {
                //this.loadXmlLists();
                this.selectedExtendsFlag = Directory.Exists(this.ExtendsListDDW.SelectedValue);
            }
        }

        public void loadGridXmlEditable()
        {

            this.gvconstmap.Visible = false;
            this.gvconnection.Visible = false;

            if (this.XML_SELECTED_PATH.IndexOf("constmap") >= 0)
            {
                this.constmapBindGrid();
            }
            else if (this.XML_SELECTED_PATH.IndexOf("connection") >= 0)
            {
                this.connectionBindGrid();
            }
        }

        public void getCurrentRootPath()
        {
            this.ROOT_PATH = Server.MapPath("~").Substring(0, 1) + ":\\"+COOP_PATH+"_ALL";
        }

        public void loadExtendsLists()
        {

            DirectoryInfo di = new DirectoryInfo(this.ROOT_PATH);
            DirectoryInfo[] dirPaths = di.GetDirectories();
            Array.Sort(dirPaths, (x, y) => Comparer<DateTime>.Default.Compare(y.CreationTime, x.CreationTime));
            this.ExtendsListDDW.Items.Clear();
            this.EXTEND_LIST = new List<string>();
            String dir = "-- เลือก Extends เพื่อกำหนดค่า --";
            ListItem item = new ListItem();
            item.Text = dir.Trim();
            item.Value = dir.Trim();
            this.ExtendsListDDW.Items.Add(item);
            for (int i = 0, j = 1; i < dirPaths.Length; i++)
            {
                int lastIndex = dirPaths[i].Name.LastIndexOf("\\");
                dir = dirPaths[i].Name.Substring(lastIndex + 1);

                if (dir.ToLower().IndexOf(COOP_PATH.ToLower()) >= 0)
                {
                    continue;
                }
                if (Directory.Exists(this.ROOT_PATH + "\\" + dir + "\\" + COOP_PATH + "\\" + XML_PATH + "\\"))
                {
                    this.EXTEND_LIST.Add(dir);
                    item = new ListItem();
                    item.Text = dir.Trim();
                    item.Value = this.ROOT_PATH + "\\" + dir + "\\";
                    this.ExtendsListDDW.Items.Add(item);
                }

            }
        }

        public void loadXmlLists()
        {
            this.XmlFilesListDDW.Items.Clear();
            this.XML_LIST = new List<string>();

            String xml = "-- เลือก XML เพื่อกำหนดค่า --";
            ListItem item = new ListItem();
            item.Text = xml.Trim();
            item.Value ="";
            this.XmlFilesListDDW.Items.Add(item);
            this.XML_LIST.Add(xml);

            xml = "xmlconf.constmap.xml";
            item = new ListItem();
            item.Text = "Constant Map - ผังค่าคงที่";
            item.Value = this.ExtendsListDDW.SelectedValue + "\\" + COOP_PATH + "\\" + XML_PATH + "\\"+xml.Trim();
            this.XmlFilesListDDW.Items.Add(item);

            xml = "server.connection_string.xml";
            item = new ListItem();
            item.Text = "Database Connection String";
            item.Value = this.ExtendsListDDW.SelectedValue + "\\" + COOP_PATH + "\\" + XML_PATH + "\\" + xml.Trim() ;
            this.XmlFilesListDDW.Items.Add(item);
        }

        protected void ExtendsListDDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadXmlLists();
            this.loadGridXmlEditable();
        }

        protected void XmlFilesListDDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.XML_SELECTED_PATH = this.XmlFilesListDDW.SelectedValue;
        }

        protected void ExtendsListBtn_Click(object sender, EventArgs e)
        {
            this.loadXmlLists();
            XmlDataLtl.Text = "";
        }

        protected void XmlFilesListBtn_Click(object sender, EventArgs e)
        {
            //this.XML_SELECTED_PATH = this.XmlFilesListDDW.SelectedValue;

            if (!File.Exists(this.XML_SELECTED_PATH))
            {
                String xmlData = File.ReadAllText(this.XML_SELECTED_PATH + ".dist");
                XDocument doc1 = XDocument.Parse(xmlData);
                doc1.Save(this.XML_SELECTED_PATH);
            }

            string output = "<table border=1 cellpadding=2 cellspacing=2 >";
            if (File.Exists(this.XML_SELECTED_PATH))
            {
                this.loadGridXmlEditable();

                //Reading XML
                //string XmlData = File.ReadAllText(this.XML_SELECTED_PATH);
                XDocument xmlDoc = XDocument.Load(this.XML_SELECTED_PATH);
                //XDocument doc1 = XDocument.Parse(XmlData);
                // or if you have related file simply use XDocument doc1 = XDocument.Load(fileFullName);
                /*
                var element =
                      doc1.Descendants("d_constant_codemap_row").Elements("Answer")
                      .Where(x => x.Attribute("FName") != null
                            && x.Attribute("FName").Value == "test").SingleOrDefault();
                if (element != null)
                {
                    var attr = element.Attribute("FName");
                    attr.Value = "Changed";
                }

                doc1.Save(filePath);
                 */
                //Think something needs to reference Child nodes, so i may Foreach though them
                int i = 0, j = 0, k = 0;
                foreach (var xmlData in xmlDoc.Elements())
                {
                    j = 0;
                    foreach (var row in xmlData.Elements())
                    {
                        k = 0;
                        string[] color = new string[] { "#FFCC99", "#FFFF99", "#FFFFFF" };
                        string outv = "";
                        string outh = "";
                        foreach (var column in row.Elements())
                        {
                            if (j == 0)
                            {
                                outh += "<td>"+column.Name+"</td>";
                            }
                            outv += "<td bgcolor=\"" + color[k > 1 ? 2 : k] + "\"><input type=\"text\" name=\"" + (column.Name.ToString() + i + "" + j) + "\" id=\"" + (column.Name.ToString() + i + "" + j) + "\" value=\"" + column.Value + "\" style=\"width:" + (k > 1 ? "350" : "200") + "px;\"/></td>";
                            k++;
                        }
                        output += "<tr>";
                        output+=outh;
                        output += "</tr><tr>";
                        output+=outv;
                        output += "</tr>";
                        j++;
                    }
                    i++;
                }
                output += "</table>";
                XmlDataLtl.Text = output;
            } 
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            Session["AuthenFlag"] =this.IsAuthenticated(this.UserTbx.Text, this.PwdTbx.Text);
            if (Session["AuthenFlag"].ToString() != "True")
            {
                this.LoginLtl.Text = "กรอกรายละเอียด Login ไม่ถูกต้อง";
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            String csname1 = "logout";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "window.location='/" + CORE_PATH + "/';";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
        }

        protected void constmapBindGrid()
        {
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds.ReadXml(this.XML_SELECTED_PATH);

                if (ds != null && ds.HasChanges())
                {

                    gvconstmap.DataSource = ds;
                    gvconstmap.DataBind();

                }
                else
                {

                    gvconstmap.DataBind();

                }

                gvconstmap.Visible = true;
            }
            catch {
                gvconstmap.Visible = false;
            }
        }

        protected void gvconstmap_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("Add"))
            {

                TextBox txtconfig_code = (TextBox)gvconstmap.FooterRow.FindControl("txtAddconfig_code");
                TextBox txtconfig_name = (TextBox)gvconstmap.FooterRow.FindControl("txtAddconfig_name");
                TextBox txtconfig_value = (TextBox)gvconstmap.FooterRow.FindControl("txtAddconfig_value");
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(this.XML_SELECTED_PATH);

                XmlElement parentelement = xmldoc.CreateElement("d_constant_codemap_row");
                XmlElement config_code = xmldoc.CreateElement("config_code");
                XmlElement config_name = xmldoc.CreateElement("config_name");
                XmlElement config_value = xmldoc.CreateElement("config_value");

                config_code.InnerText = txtconfig_code.Text;
                parentelement.AppendChild(config_code);
                config_name.InnerText = txtconfig_name.Text;
                parentelement.AppendChild(config_name);
                config_value.InnerText = txtconfig_value.Text;
                parentelement.AppendChild(config_value);

                xmldoc.DocumentElement.AppendChild(parentelement);
                xmldoc.Save(this.XML_SELECTED_PATH);
                constmapBindGrid();

            }

        }

        protected void gvconstmap_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvconstmap.EditIndex = e.NewEditIndex;
            constmapBindGrid();

        }

        protected void gvconstmap_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            constmapBindGrid();
            DataSet ds = gvconstmap.DataSource as DataSet;
            ds.Tables[0].Rows[gvconstmap.Rows[e.RowIndex].DataItemIndex].Delete();
            ds.WriteXml(this.XML_SELECTED_PATH);
            constmapBindGrid();

        }

        protected void gvconstmap_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int i = gvconstmap.Rows[e.RowIndex].DataItemIndex;
            string config_code = (gvconstmap.Rows[e.RowIndex].FindControl("txtconfig_code") as TextBox).Text;
            string config_name = (gvconstmap.Rows[e.RowIndex].FindControl("txtconfig_name") as TextBox).Text;
            string config_value = (gvconstmap.Rows[e.RowIndex].FindControl("txtconfig_value") as TextBox).Text;
            
            gvconstmap.EditIndex = -1;

            constmapBindGrid();


            DataSet ds = (DataSet)gvconstmap.DataSource;
            ds.Tables[0].Rows[i]["config_code"] = config_code;
            ds.Tables[0].Rows[i]["config_name"] = config_name;
            ds.Tables[0].Rows[i]["config_value"] = config_value;

            ds.WriteXml(this.XML_SELECTED_PATH);
            constmapBindGrid();

        }

        protected void gvconstmap_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvconstmap.EditIndex = -1;
            constmapBindGrid();

        }

        protected void gvconstmap_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvconstmap.PageIndex = e.NewPageIndex;
            constmapBindGrid();

        }



        protected void connectionBindGrid()
        {
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds.ReadXml(this.XML_SELECTED_PATH);

                if (ds != null && ds.HasChanges())
                {

                    gvconnection.DataSource = ds;
                    gvconnection.DataBind();

                }
                else
                {

                    gvconnection.DataBind();

                }

                gvconnection.Visible = true;
            }
            catch
            {
                gvconnection.Visible = false;
            }
        }

        protected void gvconnection_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("Add"))
            {

                TextBox txtid = (TextBox)gvconnection.FooterRow.FindControl("txtAddid");
                TextBox txtprofile = (TextBox)gvconnection.FooterRow.FindControl("txtAddprofile");
                TextBox txtconnection_string = (TextBox)gvconnection.FooterRow.FindControl("txtAddconnection_string");
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(this.XML_SELECTED_PATH);

                XmlElement parentelement = xmldoc.CreateElement("d_connection_string_row");
                XmlElement id = xmldoc.CreateElement("id");
                XmlElement profile = xmldoc.CreateElement("profile");
                XmlElement connection_string = xmldoc.CreateElement("connection_string");

                id.InnerText = txtid.Text;
                parentelement.AppendChild(id);
                profile.InnerText = txtprofile.Text;
                parentelement.AppendChild(profile);
                connection_string.InnerText = txtconnection_string.Text;
                parentelement.AppendChild(connection_string);

                xmldoc.DocumentElement.AppendChild(parentelement);
                xmldoc.Save(this.XML_SELECTED_PATH);
                connectionBindGrid();

            }

        }

        protected void gvconnection_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvconnection.EditIndex = e.NewEditIndex;
            connectionBindGrid();

        }

        protected void gvconnection_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            connectionBindGrid();
            DataSet ds = gvconnection.DataSource as DataSet;
            ds.Tables[0].Rows[gvconnection.Rows[e.RowIndex].DataItemIndex].Delete();
            ds.WriteXml(this.XML_SELECTED_PATH);
            connectionBindGrid();

        }

        protected void gvconnection_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int i = gvconnection.Rows[e.RowIndex].DataItemIndex;
            string id = (gvconnection.Rows[e.RowIndex].FindControl("txtid") as TextBox).Text;
            string profile = (gvconnection.Rows[e.RowIndex].FindControl("txtprofile") as TextBox).Text;
            string connection_string = (gvconnection.Rows[e.RowIndex].FindControl("txtconnection_string") as TextBox).Text;

            gvconnection.EditIndex = -1;

            connectionBindGrid();


            DataSet ds = (DataSet)gvconnection.DataSource;
            ds.Tables[0].Rows[i]["id"] = id;
            ds.Tables[0].Rows[i]["profile"] = profile;
            ds.Tables[0].Rows[i]["connection_string"] = connection_string;

            ds.WriteXml(this.XML_SELECTED_PATH);
            connectionBindGrid();

        }

        protected void gvconnection_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvconnection.EditIndex = -1;
            connectionBindGrid();

        }

        protected void gvconnection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvconnection.PageIndex = e.NewPageIndex;
            connectionBindGrid();

        } 

    }
}