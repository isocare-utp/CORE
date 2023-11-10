using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary;
using EncryptDecryptEngine;

namespace Saving
{
    public partial class websetprinter: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                Decryption decryption = new Decryption();
                String datasource = "Data Source=127.0.0.1/gcoop;Persist Security Info=True;User ID=iscogsb;Password=iscogsb;Unicode=True;";
                datasource = (Request["d"] != null) ? Decryption.UrlFormat(Request["d"]) : datasource;
                String token_id = "token_id";
                token_id = (Request["t"] != null) ? Request["t"] : "";
                string printer = "";
                printer = ((Request["p"] != null) ? Request["p"] : "-");
                LbServerMessage.Text = "";
                Sta ta = new Sta(datasource);
                ta.Transection();
                try
                {
                    ta.Exe("alter table ssotoken add( printer varchar2(255))");
                }
                catch (Exception ex)
                {}
                try
                {
                    ta.Exe("alter table ssotoken add( printer varchar(255))");
                }
                catch (Exception ex)
                {}
                try
                {
                    ta.Exe("update ssotoken set printer='" + printer + "' where token_id='" + token_id + "'");
                    ta.Commit();
                    LbServerMessage.Text = "<script>postOutput('Printer=" + printer + "');</script>";
                }
                catch (Exception ex)
                {
                    LbServerMessage.Text = "<script>alert('" + ex.Message +"');</script>";
                    LbServerMessage.ForeColor = Color.Red;
                }
                finally
                {
                    ta.Close();
                }
            }
        }
    }
}