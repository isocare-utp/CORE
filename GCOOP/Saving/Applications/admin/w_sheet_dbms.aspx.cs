using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DataLibrary;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Net.Mail;
using System.Text;
using CoreSavingLibrary;

namespace Saving.Applications.admin
{
    public partial class w_sheet_dbms : PageWebSheet, WebSheet
    {

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                String datasource = state.SsConnectionString;// "Data Source=rac/gcoop;Persist Security Info=True;User ID=ifsct;Password=ifsct;Unicode=True;";
                datasource = (Request["d"] != null) ? Request["d"] : datasource;
                if (this.TbConnectionString.Text == null || TbConnectionString.Text.Trim() == "")
                {
                    TbConnectionString.Text = datasource;
                }
                if (TbConnectionStringSrc.Text == null || TbConnectionStringSrc.Text.Trim() == "")
                {
                    TbConnectionStringSrc.Text = datasource;
                }
                LbServerMessage.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();

                try
                {
                   WebUtil.Query("create or replace view v_current_login as select USERNAME,application,CREATE_TIME,LAST_TRY from ssotoken order by LAST_TRY desc ");
                }
                catch { }
            }
            else
            {
            }
        }

        public void InitJsPostBack()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    break;

            }
        }

        public string ExecuteCmd(string Arguments, string user, string password, string domain)
        {
            return ExecuteCommand("cmd", Arguments, user, password, domain);
        }

        public string ExecuteCommand(string command, string Arguments, string user, string password, string domain)
        {
            string output = null;

            Process p = new Process();

            ProcessStartInfo s = new ProcessStartInfo();
            if (domain != null || domain != "") s.Domain = domain;
            if (user != null || user != "") s.UserName = user;
            if (password != null || password != "")
            {
                s.Password = new SecureString();
                char[] passwords = password.ToCharArray();
                for (int i = 0; i < password.Length; i++)
                {
                    s.Password.AppendChar(passwords[i]);
                }
            }
            s.FileName = command;
            s.UseShellExecute = false;
            s.RedirectStandardOutput = true;
            s.RedirectStandardError = true;
            s.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            if (Arguments != null && Arguments != "") s.Arguments = "/C \"" + Arguments+"\"";
            p.StartInfo = s;
            p.EnableRaisingEvents = true;
            try
            {
                p.Start();

                while (!p.HasExited)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                //check to see what the exit code was
                if (p.ExitCode != 0)
                {
                    output = "Exitcode: " + p.ExitCode + "  - Err1: " + p.StandardError + " - Executor: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                }
                else
                {
                    output = "Command Result: " + p.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception ex)
            { output += ex.Message; }

            return output;
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LbOutput.Text = "";
            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            ta.Transection();
            try
            {
                string sql = TbSQL.Text.Trim();
                if (sql.ToLower().IndexOf("select") >= 0)
                {
                    DataTable dt = ta.QueryDataTable(sql);
                    if (dt != null)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        LbServerMessage.Text = DateTime.Now+" ข้อมูล = " + dt.Rows.Count + " row";
                        LbServerMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        LbServerMessage.Text = DateTime.Now + " ไม่พบข้อมูล";
                        LbServerMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    
                    ta.Exe(sql);
                    LbServerMessage.Text = DateTime.Now + " ทำรายการสำเร็จ";
                    LbServerMessage.ForeColor = Color.Green;
                }
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            LbOutput.Text = "";
            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            ta.Transection();
            try
            {
                string sql = TbSQL.Text.Trim();
                ta.Exe(sql);
                ta.Commit();
                ta.Close();
                LbServerMessage.Text = DateTime.Now + " ทำงานเสร็จ";
                LbServerMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LbOutput.Text = "";
            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            ta.Transection();
            try
            {
                string sql = TbSQL.Text.Trim();
                ta.ExePlSql(sql);
                ta.Commit();
                ta.Close();
                LbServerMessage.Text = DateTime.Now + " ทำงานเสร็จ";
                LbServerMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            LbOutput.Text = "";
            string cmd = TbSQL.Text.Trim();
            LbServerMessage.Text =this.ExecuteCmd(cmd, "", "", "");
            LbServerMessage.ForeColor = Color.Green;
        }

        public void RestartIreportBuilder()
        {
            try
            {
                Process[] process = System.Diagnostics.Process.GetProcessesByName("javaw");
                for (int i = 0; i < process.Length; i++)
                {
                    process[i].Kill();
                }
                //System.Diagnostics.Process.Start("C:\\GCOOP_ALL\\FSCT\\GCOOP\\run_ireport_builder.bat");
            }
            catch {
            }

            this.ExecuteCmd("taskkill /F /IM javaw.exe", "", "", "");
            System.Diagnostics.Process.Start("C:\\GCOOP_ALL\\FSCT\\GCOOP\\run_ireport_builder.bat");
            /*
            string args = "";
            string str2 = "C:\\GCOOP_ALL\\FSCT\\0.ResetIreportBuilder.bat";

            ProcessStartInfo startinfo = new ProcessStartInfo(str2, args);
            startinfo.CreateNoWindow = true;
            startinfo.UseShellExecute = false;
            startinfo.WorkingDirectory = "C:\\GCOOP_ALL\\FSCT";
            // *** Redirect the output ***
            startinfo.RedirectStandardError = true;
            startinfo.RedirectStandardOutput = true;

            Process process;
            process = Process.Start(startinfo);
            process.WaitForExit();
            //string stdinput = process.StandardOutput.ReadToEnd();
            //string stdoutput = process.StandardError.ReadToEnd();
            int ecode = process.ExitCode;
            LbServerMessage.ForeColor = Color.Green;
            //LbServerMessage.Text = (string)ecode+"";
            //+": <br/>" + stdoutput;
             */
        }

        protected void Button45_Click(object sender, EventArgs e)
        {
            LbOutput.Text = "";
            RestartIreportBuilder();
        }

        private void createDataDic()
        {

            string v_flag = Request["v"];
            LbOutput.Text = "";
            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            Sta ta_ = new Sta(connectionString);
            string output = "<table border=1 cellpadding=0 cellspacing=0 >";
            int i = 1;
            try
            {
                string sql = "select tname from tab where ( tname not like '%$%' and tname not like '%PB%'  ) and lower(tname) like lower('" + TbSQL.Text + "') order by tname asc";
                Sdt dt = ta.Query(sql);
                if (dt != null)
                {
                    while (dt.Next())
                    {
                        string table_name = dt.GetString(0);
                        //  output += "<tr><td colspan=5 ><b>"+i+"."+table_name+"</b></td><tr> ";
                        sql = @"SELECT column_name,data_type, data_length,DATA_PRECISION,DATA_SCALE,nullable , " +
                              " (SELECT decode(A.CONSTRAINT_TYPE,'P','PK','') as pk FROM USER_CONSTRAINTS A, USER_CONS_COLUMNS B WHERE A.TABLE_NAME = B.TABLE_NAME AND B.TABLE_NAME = C.table_name  AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME and A.CONSTRAINT_TYPE in ('P') and c.column_name =b.column_name and rownum =1 ) as  IS_PK , " +
                              " (SELECT decode(A.CONSTRAINT_TYPE,'R','FK','') as fk  FROM USER_CONSTRAINTS A, USER_CONS_COLUMNS B WHERE A.TABLE_NAME = B.TABLE_NAME AND B.TABLE_NAME = C.table_name  AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME and A.CONSTRAINT_TYPE in ('R') and c.column_name =b.column_name and rownum =1 ) as  IS_FK  " +
                              " , (select d.cdesc from CMTABCOLS d where  lower(c.table_name) =lower(d.tname) and lower(c.column_name)=lower(d.cname) ) as cdesc " +
                              " , (select d.tdesc from CMTABSYS d where  lower(c.table_name) =lower(d.tname) ) as tdesc " +
                              " , (select d.from_system from CMTABSYS d where  lower(c.table_name) =lower(d.tname) ) as from_system " +
                              ", c.table_name " +
                              "FROM USER_TAB_COLUMNS c  WHERE  lower(c.table_name) = lower('" + table_name + "') order by c.table_name asc , IS_PK asc, IS_FK asc";
                        Sdt dt_ = ta_.Query(sql);
                        int j = 1;
                        output += "<tr><td>Table Name</td><td> Table Column Name </td><td> Description </td><td>Data Type</td></tr>";
                        while (dt_.Next())
                        {
                            string cols_name = dt_.GetString(0);
                            output += "<tr><td>" + (j == 1 ? (table_name.ToLower() + "") : "") + "";
                            output += (j == 2 ? ((v_flag != null ? dt_.GetString(9) : ("<input id='T--" + (table_name) + "*desc' name='T--" + (table_name) + "*tdesc' type='text' value='" + dt_.GetString(9) + "'/>"))) : "");
                            output += (j == 2 ? ((v_flag != null ? dt_.GetString(10) : (
                                    "<select id='T--" + (table_name) + "*from_system' name='T--" + (table_name) + "*from_system' >"
                                    + "<option value='ALL' " + (dt_.GetString(10) =="ALL"? "selected" : "") + ">N/A</option>"
                                     + "<option value='MEM' " + (dt_.GetString(10) == "MEM" ? "selected" : "") + ">01.MEM</option>"
                                     + "<option value='SHR' " + (dt_.GetString(10) == "SHR" ? "selected" : "") + ">02.SHR</option>"
                                     + "<option value='DEP' " + (dt_.GetString(10) == "DEP" ? "selected" : "") + ">03-04.DEP-PRM</option>"
                                     + "<option value='LON' " + (dt_.GetString(10) == "LON" ? "selected" : "") + ">05.LON</option>"
                                     + "<option value='TRD' " + (dt_.GetString(10) == "TRD" ? "selected" : "") + ">06.TRD</option>"
                                     + "<option value='INV' " + (dt_.GetString(10) == "INV" ? "selected" : "") + ">07.INV</option>"
                                     + "<option value='FIN' " + (dt_.GetString(10) == "FIN" ? "selected" : "") + ">08.FIN</option>"
                                     + "<option value='ACC' " + (dt_.GetString(10) == "ACC" ? "selected" : "") + ">09.ACC</option>"
                                     + "<option value='ADM' " + (dt_.GetString(10) == "ADM" ? "selected" : "") + ">10.ADMIN</option>"
                                     + "<option value='DIV' " + (dt_.GetString(10) == "DIV" ? "selected" : "") + ">11.DIV</option>"
                                     + "<option value='MIS' " + (dt_.GetString(10) == "MIS" ? "selected" : "") + ">12.MIS</option>"
                                     + "<option value='OTR' " + (dt_.GetString(10) == "OTR" ? "selected" : "") + ">13.TMP</option>"
                                     +"</select>"))) : "");
                            output += "</td>";
                            output += "<td>" + dt_.GetString(0).ToLower() + (dt_.GetString(6) != "" ? ("(" + dt_.GetString(6) + ")") : "") + (dt_.GetString(7) != "" ? ("(" + dt_.GetString(7) + ")") : "") + "</td>";
                            output += "<td>" + (v_flag != null ? dt_.GetString(8) : ("<input id='T**" + (table_name + "*" + cols_name) + "' name='T**" + (table_name + "*" + cols_name) + "' type='text' value='" + dt_.GetString(8) + "'/>")) + "</td>";
                            output += "<td>" + dt_.GetString(1) + "";
                            if (dt_.GetString(1) != "DATE")
                            {
                                if (dt_.GetString(1) == "NUMBER")
                                {
                                    if (dt_.GetString(3) != "" && dt_.GetString(4) != "")
                                    {
                                        output += "(" + (dt_.GetString(3) + ((dt_.GetString(4) != "0" && dt_.GetString(4) != "") ? ("," + dt_.GetString(4)) : "")) + ")";
                                    }
                                    else
                                    {
                                        output += "(" + dt_.GetString(2) + ")";
                                    }
                                }
                                else
                                {
                                    output += "(" + dt_.GetString(2) + ")";
                                }
                            }
                            output += "</td>";
                            //output+=" <td>"+ dt_.GetString(5)+"</td>";

                            //select constraint_name,constraint_type from all_constraints where constraint_type in ('P','U','R') and table_name='<your table here>'

                            output += "</tr>";
                            j++;
                        }
                        /*
                        sql = "select distinct constraint_name,DECODE(constraint_type ,'P','PK','U','UNQ','R','FK','') as constraint_type_ ,constraint_type from all_constraints where constraint_type in ('P','U','R') and table_name='" + table_name + "' order by constraint_type asc";
                        dt_ = ta_.Query(sql);
                        int k = 1;
                        while (dt_.Next())
                        {
                            if (k == 1)
                            {
                                output += "<tr><td colspan=4 >constraint </td><tr> ";
                            }
                            output += "<tr><td colspan=3 >" + k + "." + dt_.GetString(0) + "</td><td>" + dt_.GetString(1) + "</td></tr>";
                            k++;
                        }
                        //output+="</tr>";
                         */
                        i++;
                    }
                }
                //LbServerMessage.Text = "ทำงานเสร็จ";
                //LbServerMessage.ForeColor = Color.Green;
                output += "</table>";
                LbOutput.Text = output;

                //TbSQL.Text = output;
            }
            catch (Exception ex)
            {
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
            ta_.Close();
            ta.Close();
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            this.createDataDic();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {

            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            Sta ta_ = new Sta(connectionString);
            string dblink = "gcoop_src";
            string output = "DROP DATABASE LINK " + dblink + " ;\r\n";
                   output+=" CREATE DATABASE LINK " + dblink + " CONNECT TO IFSCT IDENTIFIED BY ifsct USING '" + dblink + "';\r\n";
            
            int i = 1;
            try
            {
                string sql = "select tname from tab where ( tname not like '%$%' and tname not like '%PB%'  ) and lower(tname) like lower('" + TbSQL.Text + "') order by tname asc";
                Sdt dt = ta.Query(sql);
                if (dt != null)
                {
                    while (dt.Next())
                    {
                        string table_name = dt.GetString(0);
                        //  output += "<tr><td colspan=5 ><b>"+i+"."+table_name+"</b></td><tr> ";
                        sql = @"SELECT column_name,nullable  FROM USER_TAB_COLUMNS c  WHERE  lower(c.table_name) = lower('" + table_name + "') ";
                        Sdt dt_ = ta_.Query(sql);
                        int j = 1;
                        output += "delete from " + table_name + ";\r\n";
                        output += "commit;\r\n";
                        string cols_names = " ";
                        string cols_names_ = " ";
                        while (dt_.Next())
                        {
                            string cols_name = dt_.GetString(0);
                            cols_names += (dt_.GetString(1) == "Y" ? "NVL(" : "") + cols_name + (dt_.GetString(1) == "Y" ? ",'')" : "") + ",";
                            cols_names_ += cols_name  + ",";
                            j++;
                        }
                        cols_names += ";";
                        cols_names_ += ";";
                        cols_names_ = cols_names_.Replace(",;", " ");
                        cols_names = cols_names.Replace(",;", " ");
                        output += " insert into " + table_name + " ( " + cols_names_ + " ) ( \r\n";
                        output += " select " + cols_names +  " from " + table_name + "@" + dblink + " \r\n ";
                        output += " )  ;\r\n ";
                        output += "commit;\r\n";
                        i++;
                    }
                }
                //LbServerMessage.Text = "ทำงานเสร็จ";
                //LbServerMessage.ForeColor = Color.Green;
                //LbOutput.Text = output;

                TbSQL.Text = output;
            }
            catch (Exception ex)
            {
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
            ta_.Close();
            ta.Close();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String values = "";
            String table = "";
            String from_system = "";
            String column = "";
            String desc = "";

            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            Sta ta_ = new Sta(connectionString);
            ta.Transection();
            try
            {
                foreach (string s in Request.Params.Keys)
                {
                    if (s.IndexOf("T**") >= 0)
                    {
                        string tmp = s.Replace("T**", "");
                        string[] tmps = tmp.Split('*');
                        try
                        {
                            table = tmps[0];
                            column = tmps[1];
                            desc = Request.Params[s];
                            //values += table + ":" + column + "=" + desc + "\r\n";
                            try
                            {
                                ta.Exe("insert into CMTABCOLS (tname,cname,cdesc) values('" + table + "','" + column + "','" + desc + "')");                              
                            }
                            catch
                            {                                 
                                ta.Exe("update CMTABCOLS set cdesc='" + desc + "' where tname='" + table + "' and cname='" + column + "' ");                               
                            }
                        }
                        catch (Exception em)
                        {
                            values += tmp + "=" + em.Message + "<br/>";
                        }
                    }


                    if (s.IndexOf("T--") >= 0)
                    {
                        string tmp = s.Replace("T--", "");
                        string[] tmps = tmp.Split('*');
                        try
                        {
                            table = tmps[0];
                            from_system = tmps[1];
                            desc = Request.Params[s];
                            //values += table + ":" + column + "=" + desc + "\r\n";
                            if (s.IndexOf("tdesc") >= 0)
                            {

                                Sdt dt = ta_.Query("select * from  CMTABSYS  where tname='" + table + "'  ");
                                try
                                {
                                    if (dt.Next()==false)
                                    {
                                        ta.Exe("insert into CMTABSYS (tname,tdesc,from_system) values('" + table + "','" + desc + "','ALL')");
                                    }
                                    else
                                    {
                                        ta.Exe("update CMTABSYS set tdesc='" + desc + "' where tname='" + table + "'  ");
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (s.IndexOf("from_system") >= 0)
                            {
                                try
                                {
                                    ta.Exe("update CMTABSYS set from_system='" + desc + "' where tname='" + table + "'  ");
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch (Exception em)
                        {
                            values += tmp + "=" + em.Message + "<br/>";
                        }
                    }
                }
                LbServerMessage.Text = values;
                LbServerMessage.ForeColor = Color.Red;
            }
            catch { } 

            ta.Commit();
            ta.Close();
            ta_.Close();

            this.createDataDic();

            LbServerMessage.Text = "สำเร็จ";
            LbServerMessage.ForeColor = Color.Green;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            this.TbSQL.Text = this.DropDownList1.SelectedValue;
            this.Button1_Click(sender, e);
        }

        protected void UploadSVNBtn_Click(object sender, EventArgs e)
        {

            string filename = string.Empty;
            try
            {
                string[] validFileTypes = { "zip" };
                string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                bool isValidFile = false;
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext == "." + validFileTypes[i])
                    {
                        isValidFile = true;
                        break;
                    }
                }
                if (!isValidFile)
                {
                    LbServerMessage.Text = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);
                    LbServerMessage.ForeColor = Color.Red;
                }
                else
                {
                    if (this.FileUpload1.HasFile)
                    {
                        filename = FileUpload1.FileName;
                        this.FileUpload1.SaveAs(Server.MapPath("uploads/svn.zip" ));
                        LbServerMessage.Text = filename;//+ " Uploaded.<br/><div align=\"left\">"+GcoopServiceCs.SvnService.getZipFileLists(Server.MapPath("uploads/svn.zip" )).Replace("\n","<br/>")+"</div>";
                        //GcoopServiceCs.SvnService.ExtractZipSVNFolder(Server.MapPath("uploads/svn.zip"), this.svnPath.Text);
                        LbServerMessage.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                LbServerMessage.Text =ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            string svnZipFile = "";//GcoopServiceCs.SvnService.CreateZipSVNFolder(this.svnRootPath.Text, Convert.ToInt32(this.svnVersion.Text));
           LbServerMessage.Text = "<a href=\"" + svnZipFile + "\" >" + svnZipFile + "</a>";
        }

        protected void Button11_Click(object sender, EventArgs e)
        {


            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            Sta ta_ = new Sta(connectionString);
            TbSQL.Text = TbSQL.Text.Replace("  ", "");
            String[] vals =TbSQL.Text.Split(';');
            //ตัวอย่าง Connection String
            //Data Source=127.0.0.1/iorcl;Persist Security Info=True;User ID=scomsv;Password=scomsv;Unicode=True;
            //ตัวอย่าง String เพื่อกำหนดค่า
            //iorclcenter;scomsv;scomsv;mbmembmaster
            string dblink = vals[0];
            string user = vals[1];
            string pwd = vals[2];
            string tablenames = vals[3];
            string output = "DROP DATABASE LINK " + dblink + " ;\r\n";
            output += " CREATE DATABASE LINK " + dblink + " CONNECT TO "+user+" IDENTIFIED BY "+pwd+" USING '" + dblink + "';\r\n";
            /*
             CREATE TABLE "CMLOGSYNCDATA" ("BRANCH_ID" VARCHAR2(20) NOT NULL,"LOG_DATE" DATE NOT NULL,"MODE" CHAR(1) NOT NULL,"TABLE_NM" VARCHAR2(150) NOT NULL, "SQL" CLOB NOT NULL) ;
             ALTER TABLE "CMLOGSYNCDATA" ADD ( CONSTRAINT pk_logid PRIMARY KEY ( "BRANCH_ID","LOG_DATE","MODE","TABLE_NM" )) ;
             CREATE TABLE "CMLOGSYNCTABLE" ("TABLE_NM" VARCHAR2(150) NOT NULL,"FLAG" DECIMAL(1,0) NOT NULL);
             ALTER TABLE "CMLOGSYNCTABLE" ADD ( CONSTRAINT pk_synctableid PRIMARY KEY ( "TABLE_NM" )) ;
             */
            int i = 1;
            try
            {
                string sql = "select tname from tab where ( tname not like '%$%' and tname not like '%PB%'  ) and lower(tname) like lower('" + tablenames.Replace("  ", "") + "') order by tname asc";
                Sdt dt = ta.Query(sql);
                if (dt != null)
                {
                    while (dt.Next())
                    {
                        
                        string table_name = dt.GetString(0);

                        output += @"  
                    -- Start " + table_name + @"     
                        create or replace TRIGGER " + table_name + @"_" + dblink + @" 
                          AFTER DELETE OR INSERT OR UPDATE ON " + table_name + @"  
                          for each row 
                        DECLARE 
                          ACT  char(1);
                          Procedure doPutLog is 
	                        begin
	
		                            if INSERTING  then
				                            -- สร้าง Insert Statement เพื่อ สร้าง Row ตามต้นทาง
				                            ACT := 'I';
				                            insert into " + table_name + @"@" + dblink + @" 
                                                        ";
                        
                        sql = @"select * from (
                                select c.column_name, c.data_type, c.data_length,(
                                 SELECT ac.constraint_name
                                  FROM all_constraints ac JOIN all_cons_columns acc  ON (ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME)
                                    JOIN all_tab_cols atc ON (ac.owner = atc.owner AND  ac.table_name = atc.TABLE_NAME AND  acc.COLUMN_NAME = atc.COLUMN_NAME)
                                 WHERE ac.constraint_type = 'P' and ac.table_name = c.table_name and acc.COLUMN_NAME=c.column_name and ROWNUM=1) as constraint_name
                                 from user_tab_columns c 
                                where lower(c.table_name) = lower('" + table_name + @"') 
                                )
                                order by  constraint_name asc , column_name desc ";
                        Sdt dt_ = ta_.Query(sql);
                        string cols_names = "(";
                        string cols_names_ = "(";
                        string cols_names_values = "(";
                        string cols_names_values_pk = "";
                        while (dt_.Next())
                        {
                            string cols_name = dt_.GetString(0);
                            if (dt_.GetString(1).ToUpper() != "BLOB")
                            {
                                cols_names += cols_name + ",";
                                cols_names_ += ":new." + cols_name + ",";
                                cols_names_values += "#'||:new." + cols_name + "||'#,";
                            }
                        }
                        cols_names += ")"; cols_names = cols_names.Replace(",)", ")");
                        cols_names_ += ")"; cols_names_ = cols_names_.Replace(",)", ")");
                        cols_names_values += ")"; cols_names_values = cols_names_values.Replace(",)", ")");
                        output += cols_names+@" 
				                            values 
				                            " +cols_names_+@";
				
                            insert into CMLOGSYNCDATA@" + dblink + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'I','" + table_name + "','insert into  " + table_name + @"@" + dblink + " " + cols_names.Replace("'", "") + @" values " + cols_names_values + @"');

		                            elsif DELETING then
	
				                            ACT := 'D';
			      	                            delete from " + table_name + @"@" + dblink + @"
				                             where ";

                         sql = @"select * from (
                                select c.column_name, c.data_type, c.data_length,NVL((
                                 SELECT ac.constraint_name
                                  FROM all_constraints ac JOIN all_cons_columns acc  ON (ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME)
                                    JOIN all_tab_cols atc ON (ac.owner = atc.owner AND  ac.table_name = atc.TABLE_NAME AND  acc.COLUMN_NAME = atc.COLUMN_NAME)
                                 WHERE ac.constraint_type = 'P' and ac.table_name = c.table_name and acc.COLUMN_NAME=c.column_name and ROWNUM=1),'-') as constraint_name
                                 from user_tab_columns c 
                                where lower(c.table_name) = lower('" + table_name + @"') 
                                ) 
                                order by  constraint_name asc , column_name desc ";
                         dt_ = ta_.Query(sql);
                         cols_names = "";
                         cols_names_ = "";
                         cols_names_values = "";
                        while (dt_.Next())
                        {
                            string cols_name = dt_.GetString(0);
                            string constraint_name = dt_.GetString(3);

                            if (dt_.GetString(1).ToUpper() != "BLOB")
                            {
                                if (constraint_name != "-")
                                {
                                    cols_names += " " + cols_name + "=:old." + cols_name + " and";
                                    cols_names_values_pk += " " + cols_name + "=#'||:old." + cols_name + "||'# and";
                                }
                                else
                                {
                                    cols_names_ += " " + cols_name + "=:new." + cols_name + " ,";
                                    cols_names_values += " " + cols_name + "=#'||:new." + cols_name + "||'# ,";
                                }
                            }
                        }
                        cols_names += ")"; cols_names = cols_names.Replace("and)", " ");
                        cols_names_values_pk += ")"; cols_names_values_pk = cols_names_values_pk.Replace("and)", " ");
                        cols_names_ += ")"; cols_names_ = cols_names_.Replace(",)", " ");
                        cols_names_values += ")"; cols_names_values = cols_names_values.Replace(",)", " ");

                        output += cols_names+@";
				
                                            insert into CMLOGSYNCDATA@" + dblink + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'D','" + table_name + "','delete from  " + table_name + @"@" + dblink + @" where " + cols_names_values_pk + @"');
			

		                            elsif UPDATING then
	
				                            ACT := 'U';
				                            update  " + table_name + @"@" + dblink + @"  
                                            set  
				                            " + cols_names_ + @"
				                            where " + cols_names + @";

                                            insert into CMLOGSYNCDATA@" + dblink + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'U','" + table_name + "','update  " + table_name + @"@" + dblink + " set " + cols_names_values + @" where " + cols_names_values_pk + @"');
			
		                            end if;

	                            EXCEPTION 
    	                                 WHEN others THEN
   		                            dbms_output.put_line('Error!');

                              end doPutlog;
                            BEGIN
                                if true then doPutLog; end if;
                            END;
                            -- End " + table_name + @" 
                            / 
                            ";

                        i++;
                    }
                }
                //LbServerMessage.Text = "ทำงานเสร็จ";
                //LbServerMessage.ForeColor = Color.Green;
                //LbOutput.Text = output;

                TbSQL.Text = output;
            }
            catch (Exception ex)
            {
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
            ta_.Close();
            ta.Close();
        }


        public static int sendGMail(string fromAddress, string fromPassword, string[] toAddress, string subject, string body)
        {
            //กรณีผู้ส่งเป็น gmail ต้อง Set lesssecure = on ที่ Link https://www.google.com/settings/security/lesssecureapps
            return sendMail(fromAddress, fromPassword, toAddress, subject, body, "smtp.gmail.com", 587);
        }
        public static int sendMail(string fromAddress, string fromPassword, string[] toAddress, string subject, string body, string host, int port)
        {

            string your_id = fromAddress;
            string your_password = fromPassword;
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(your_id, your_password),
                    Timeout = 10000,
                };
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(your_id);
                for (int i = 0; i < toAddress.Length; i++)
                {
                    mm.To.Add(toAddress[i]);
                }
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.IsBodyHtml = true;
                //mm. = MailFormat.Html;
                mm.Subject = subject;
                mm.Body = body;
                client.Send(mm);
                Console.WriteLine("Email Sent");

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not end email\n\n" + e.ToString());
                return -1;
            }
        }
    }
}