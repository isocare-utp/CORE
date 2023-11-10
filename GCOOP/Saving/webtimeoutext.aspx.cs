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
    public partial class webtimeoutext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                Decryption decryption = new Decryption();
                String datasource = "Data Source=127.0.0.1/gcoop;Persist Security Info=True;User ID=iscogsb;Password=iscogsb;Unicode=True;";
                datasource = (Request["d"] != null) ? Decryption.UrlFormat(Request["d"]) : datasource;
                String token_id = "token_id";
                token_id = (Request["t"] != null) ? Request["t"] : "";
                int time_next = 0;
                time_next = Int32.Parse( ((Request["tx"] != null) ? Request["tx"] : "0"));
                LbServerMessage.Text = "";
                Sta ta = new Sta(datasource);
                ta.Transection();
                try
                {
                    ta.Exe("alter table ssotoken add( expsession number(2,0))");
                }
                catch (Exception ex)
                { }
                try
                {
                    ta.Exe("alter table ssotoken add( expsession decimal(2,0))");
                }
                catch (Exception ex)
                { }
                try
                {
                    DateTime nextTime = DateTime.Now.AddMinutes(time_next);
                    ta.Exe("update ssotoken set last_try=to_date('" + nextTime.ToString("yyyy/MM/dd HH:mm:ss") + "','yyyy/MM/dd hh24:mi:ss'),expsession=" + time_next + "  where token_id='" + token_id + "'");
                    ta.Commit();
                    LbServerMessage.Text = "<script>postOutput('Last_Try=" + nextTime.ToString("yyyy/MM/dd HH:mm:ss") + "');</script>";
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