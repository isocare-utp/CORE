using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary;

namespace Saving
{
    public partial class webprocessing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                /*
                    CREATE TABLE "CMPROCESSING" ("PROCESS_ID" VARCHAR2(30) NOT NULL, "COOP_CONTROL" VARCHAR2(10), "COOP_ID" VARCHAR2(10), "ENTRY_ID" VARCHAR2(30) NOT NULL,"ENTRY_DATE" DATE, "START_PROCESS" NUMBER(4,0) DEFAULT 0 NOT NULL, "END_PROCESS" NUMBER(4,0) DEFAULT 0 NOT NULL, "READ_DATE" DATE, "FINISH_DATE" DATE, "RUNTIME_STATUS" NUMBER(2,0), "SHOW_FLAG" NUMBER(1,0) DEFAULT 1 NOT NULL, "RUNTIME_MESSAGE" VARCHAR2(500), "OBJECT_NAME" VARCHAR2(150), "CMD" VARCHAR2(500), "LABEL_NAME" VARCHAR2(50), "CRITERIA_XML" CLOB, "CRITERIA_XML_1" CLOB, "CRITERIA_XML_2" CLOB) ;
                    ALTER TABLE "CMPROCESSING" ADD ( CONSTRAINT CMP_PK PRIMARY KEY ( "PROCESS_ID", "ENTRY_ID" )) ;
                    ALTER TABLE CMPROCESSING ADD ( APPLICATION VARCHAR2(20), REPORT_GROUP_ID VARCHAR2(40) ,REPORT_ID VARCHAR2(40),REPORT_PATH VARCHAR2(255),PRINTER VARCHAR2(150) );
                    ALTER TABLE CMPROCESSING ADD ( WORKDATE DATE );
                    CREATE TABLE "CMPRINTER" ("PRINTER_ID" VARCHAR2(30) NOT NULL, "COOP_CONTROL" VARCHAR2(10), "COOP_ID" VARCHAR2(10) NOT NULL, "PRINTER_NAME" VARCHAR2(150) NOT NULL, "PRINTER_IP" VARCHAR2(80), "DEFAULT_FLAG" NUMBER(1,0) DEFAULT 0) ;
                    ALTER TABLE "CMPRINTER" ADD ( CONSTRAINT PRINTER_PK PRIMARY KEY ( "PRINTER_ID", "COOP_ID" )) ;                
                */
                String datasource = "Data Source=127.0.0.1/gcoop;Persist Security Info=True;User ID=iscogsb;Password=iscogsb;Unicode=True;";
                datasource = (Request["d"] != null) ? Request["d"] : datasource;
                String user_name = "admin";
                user_name = (Request["u"] != null) ? Request["u"] : "";
                String process_id = "process_id";
                process_id = (Request["p"] != null) ? Request["p"] : "";
                String cancel_flag = "0";
                cancel_flag = (Request["c"] != null) ? Request["c"] : ""; 
                String show_flag = "0";
                show_flag = (Request["s"] != null) ? Request["s"] : "";
                LbServerMessage.Text = "";
                Sta ta = new Sta(datasource);
                ta.Transection();
                string output = "";
                //เอาออกก่อน เพราะทำให้รายงานไม่ออก by Mike 25/12/2558
                //try
                //{
                //    ta.Exe("delete from CMPROCESSING where entry_date <sysdate-7d");
                //    ta.Commit();
                //}
                //catch { }
                try
                {
                    if (cancel_flag == "1")
                    {
                        ta.Exe("update CMPROCESSING set read_date=sysdate ,RUNTIME_STATUS=-9 where PROCESS_ID='" + process_id + "' ");
                        WebUtil.CloseProcess(process_id);
                        ta.Commit();
                    }
                    if (show_flag == "1")
                    {
                        ta.Exe("update CMPROCESSING set read_date=sysdate ,show_flag=-9 where PROCESS_ID='" + process_id + "' ");
                        WebUtil.CloseProcess(process_id);
                        ta.Commit();
                    }
                }
                catch { }
                try
                {
                    String sql = "select '['||START_PROCESS||'/'||END_PROCESS||']'||RUNTIME_MESSAGE as msg,RUNTIME_STATUS as status,report_id,report_path,RUNTIME_MESSAGE,printer from CMPROCESSING where PROCESS_ID='" + process_id + "' and SHOW_FLAG=1 ";
                    if (Sta.IS_OLEDB_MODE)
                    {
                        sql = "select '['||CONVERT(varchar(10), START_PROCESS)||'/'||CONVERT(varchar(10), END_PROCESS)||']'||RUNTIME_MESSAGE as msg,RUNTIME_STATUS as status,report_id,report_path,RUNTIME_MESSAGE,printer from CMPROCESSING where PROCESS_ID='" + process_id + "' and SHOW_FLAG=1 ";                                            
                    }
                    Sdt dt = ta.Query(sql);
                    if (dt != null)
                    {
                        while (dt.Next())
                        {
                            //CASE Process REPORT
                            if ((dt.GetString(2) == null || dt.GetString(2) == "")==false)
                            {
                                if (dt.GetDecimal(1) == 0)
                                {
                                    output += ""  + dt.GetString(4) + "\r\n";
                                }
                                else
                                {
                                    int i=dt.GetString(3).LastIndexOf("\\");
                                    string report_filename = dt.GetString(3).Substring(i+1);
                                    output += "1;" + report_filename + "";
                                    string printer = dt.GetString(5);
                                    output += ":" + printer + "";
                                }
                                ta.Exe("update CMPROCESSING set read_date=sysdate where PROCESS_ID='" + process_id + "' ");
                                ta.Commit();                            
                            }
                            //CASE Process 
                            else
                            {
                                output += (dt.GetDecimal(1) == 0 ? "" : "1;") + dt.GetString(0) + "\r\n";
                                ta.Exe("update CMPROCESSING set read_date=sysdate where PROCESS_ID='" + process_id + "' ");
                                ta.Commit();
                            }
                        }
                    }
                    LbServerMessage.Text = output;
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