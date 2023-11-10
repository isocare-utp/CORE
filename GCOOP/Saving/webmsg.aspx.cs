using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;

namespace Saving
{
    public partial class webmsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                /*
                             CREATE TABLE "CMMSG" ("PK_ID" VARCHAR2(50) NOT NULL, "USER_NAME" VARCHAR2(50) NOT NULL, "READ_FLAG" NUMBER(1,0) DEFAULT 0 NOT NULL,READ_DATE DATE NULL, "SENDER" VARCHAR2(50) NULL,SEND_DATE DATE NULL, "MSG" VARCHAR2(255) NOT NULL) ;
                             ALTER TABLE "CMMSG" ADD ( CONSTRAINT cmmsg_pk PRIMARY KEY ( "PK_ID", "USER_NAME" )) ;           
                             */
                String datasource = "Data Source=127.0.0.1/gcoop;Persist Security Info=True;User ID=iscogsb;Password=iscogsb;Unicode=True;";
                datasource = (Request["d"] != null) ? Request["d"] : datasource;
                String user_name = "admin";
                user_name = (Request["u"] != null) ? Request["u"] : "";
                LbServerMessage.Text = "";
                Sta ta = new Sta(datasource);
                ta.Transection();
                string output = "";
                try
                {
                    String sql = "select msg from cmmsg where user_name='" + user_name + "' and read_flag=0";
                    Sdt dt = ta.Query(sql);
                    if (dt != null)
                    {
                        while (dt.Next())
                        {
                            output += dt.GetString(0) + "\r\n";
                        }
                    }
                    LbServerMessage.Text = output;
                    ta.Exe("update cmmsg set read_flag=1,read_date=sysdate where user_name='" + user_name + "' and read_flag=0");
                    ta.Commit();
                }
                catch (Exception ex)
                {
                    LbServerMessage.Text = ex.Message;
                    LbServerMessage.ForeColor = Color.Red;
                }
                ta.Close();
            }
        }
    }
}