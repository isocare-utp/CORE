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

namespace Saving
{
    public partial class dbms : System.Web.UI.Page
    {

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

        protected void Page_Load(object sender, EventArgs e)
        {
            String datasource = "Data Source=10.100.110.154/gcoop;Persist Security Info=True;User ID=ifsct;Password=ifsct;Unicode=True;";
            datasource = (Request["d"] != null) ? Request["d"] : datasource;
            if (TbConnectionString.Text == null || TbConnectionString.Text.Trim() == "")
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
                        LbServerMessage.Text = "ข้อมูล = " + dt.Rows.Count + " row";
                        LbServerMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        LbServerMessage.Text = "ไม่พบข้อมูล";
                        LbServerMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    
                    ta.Exe(sql);
                    LbServerMessage.Text = "ทำรายการสำเร็จ";
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
                LbServerMessage.Text = "ทำงานเสร็จ";
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
                LbServerMessage.Text = "ทำงานเสร็จ";
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
                        //LbServerMessage.Text = filename + " Uploaded.<br/><div align=\"left\">"+GcoopServiceCs.SvnService.getZipFileLists(Server.MapPath("uploads/svn.zip" )).Replace("\n","<br/>")+"</div>";
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
           //string svnZipFile= GcoopServiceCs.SvnService.CreateZipSVNFolder(this.svnRootPath.Text, Convert.ToInt32(this.svnVersion.Text));
           //LbServerMessage.Text = "<a href=\"" + svnZipFile + "\" >" + svnZipFile + "</a>";
        }

        protected void Button11_Click(object sender, EventArgs e)
        {


            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            Sta ta_ = new Sta(connectionString);
            TbSQL.Text = TbSQL.Text.Replace("  ", "");
            String[] vals =TbSQL.Text.Split(';');
            //ตัวอย่าง Connection String
            //Data Source=127.0.0.1/orcl;Persist Security Info=True;User ID=scopsm;Password=scopsm;Unicode=True;
            //ตัวอย่าง String เพื่อกำหนดค่า
            //scopsm;scomsv;scomsv;CMCLSDAYETCVALUE;JavaConvertEN2TH;127.0.0.1:1521/orclcenter;
            string dblink = vals[0];
            string user = vals[1];
            string pwd = vals[2];
            string tablenames = vals[3];
            string convert = vals[4];
            string tnsname = vals[5];
            string dblinkfield = "dblink";
            string output = "DROP DATABASE LINK " + user + " ;\r\n";
            output += " CREATE DATABASE LINK " + user + " CONNECT TO " + user + " IDENTIFIED BY " + pwd + " USING '" + tnsname + "';\r\n";
            /*
			
			สรุปขั้นตอนการดำเนินการเกี่ยวกับงาน Sync ข้อมูล รวมศูนย์ระบบงาน
 
				--SYNC_FLAG : 0=ไม่ Sync , 1= One Way , 2 = Two Way
				--ACTIVE_FLAG : 0 = ปิด , 1 = เปิด
			 
				1.สร้าง TABLE สำหรับเก็บ ข้อมูล DB Host ทีจะเชื่อมมายัง DB CENTER
				
				 CREATE TABLE "CMLOGSYNCHOSTS" ("HOST_IP" VARCHAR2(150) NOT NULL,"SID" VARCHAR2(50) NOT NULL,"DBLINK"  VARCHAR2(50) NOT NULL,"SYNC_FLAG" DECIMAL(1,0) NOT NULL);
				 ALTER TABLE "CMLOGSYNCHOSTS" ADD ( CONSTRAINT PK_CMLOGSYNCHOSTS PRIMARY KEY ( "HOST_IP", "SID","DBLINK")) ;
				
				-- MSV --
host_ip	sid	dblink	sync_flag
127.0.0.1	ORCLCENTER	SCOMSV	0
127.0.0.1	ORCL	SCOPSM	1
127.0.0.1	ORCL	SCOBSU	1
127.0.0.1	ORCL	SCOMKU	1
127.0.0.1	ORCL	SCOPLU	1
127.0.0.1	ORCL	SCOSKU	1					
				
				2.สร้าง TABLE สำหรับเก็บ  ข้อมูล รายการ Table ที่จะ Sync มายัง DB CENTER
				
				 CREATE TABLE "CMLOGSYNCTABLES" ("TABLE_NM" VARCHAR2(150) NOT NULL,"TABLE_DESC" VARCHAR2(150) NOT NULL,ORDER_NO DECIMAL(4,0) NOT NULL,"ACTIVE_FLAG" DECIMAL(1,0) NOT NULL,"SYNC_FLAG" DECIMAL(1,0) NOT NULL);
				 ALTER TABLE "CMLOGSYNCTABLES" ADD ( CONSTRAINT PK_CMLOGSYNCTABLES PRIMARY KEY ( "TABLE_NM" )) ;
				
				-- MSV --
table_nm	table_desc	order_no	active_flag	sync_flag
mbucfdistrict	ค่าคงที่อำเภอ	1	1	1
mbucfappltype	ค่าคงที่ประเภทการสมัคร	2	1	1
mbucfmembtype	ค่าคงที่ประเภทสมาชิก	3	1	1
mbucfdepartment	ค่าคงที่สังกัด	4	1	1
mbucfposition	ค่าคงที่ตำแหน่ง	5	1	1
mbucfprename	ค่าคงที่คำนำหน้าชื่อ	6	1	1
mbucfmembgroup	ค่าคงที่หน่วยงาน	7	1	1
mbmembmaster	ทะเบียนสมาชิก	8	1	1
shsharemaster	ทะเบียนหุ้น	9	1	1
mbmembexpense	ทะเบียนการรับจ่ายเงิน	10	1	1
mbgainmaster	ทะเบียนผู้รับโอนผลประโยชน์	11	1	1
mbgaindetail	รายละเอียดผู้รับโอนผลประโยชน์	12	1	1
mbaddress	ทะเบียนที่อยู่สมาชิกที่อยู่อื่นๆ	13	1	1
shsharestatement	รายการเคลื่อนไหวหุ้น	14	1	1
mumembmaster	ทะเบียนกองทุน สวส.	15	1	1
mugaindetail	ทะเบียนผู้รับโอนกองทุน สวส.	16	1	1
mustatement	รายการเคลื่อนไหวกองทุน สวส.	17	1	1
lncontmaster	ทะเบียนเงินกู้	18	1	1
lncontcoll	ทะเบียนผู้ค้ำประกัน	19	1	1
lncontstatement	รายการเคลื่อนไหวเงินกู้	20	1	1
lncollmaster	ทะเบียนหลักทรัพย์	21	1	1
lncolldetail	รายละเอียดหลักทรัพย์1	22	1	1
lncollmastmemco	รายละเอียดหลักทรัพย์2	23	1	1
lncontcredit	ทะเบียนวงเงินกู้ OD	24	1	1
dpdeptmaster	ทะเบียนเงินฝาก	25	1	1
dpdeptstatement	รายการเคลื่อนไหวเงินฝาก	26	1	1
dpdeptprnfixed	รายการต้นเงินฝาก	27	1	1
dpcodeposit	รายการเงื่อนไขในการฝากถอน	28	1	1
dpsignatur	รายการลายเซ้นเงินฝาก	29	1	1
dpcloseaccomonth	เงินฝากปิดประจำเดือน	30	1	1
cmclsmthshrlonbal	หุ้นหนี้ปิดประจำเดือน	31	1	1
cmclsdayetcvalue	หุ้นหนี้ทำกราฟ	32	1	1
cmclsdayloanbal	หุ้นหนี้ประจำวัน	33	1	1
cmclsdaysharebal	หุ้นหนี้คงเหลือประจำวัน	34	1	1
kptempreceive	รายงานใบเสร็จเรียกเก็บkpt	35	1	1
kptempreceivedet	รายงานรายละเอียดใบเสร็จเรียกเก็บkpt	36	1	1
kpmastreceive	รายงานใบเสร็จเรียกเก็บkpm	37	1	1
kpmastreceivedet	รายงานรายละเอียดใบเสร็จเรียกเก็บkpm	38	1	1
mbdivavgemp	รายการปันผลเฉลี่ยคืน	39	1	1
mbdivavgtempdet	รายการปันผลเฉลี่ยคืนรายละเอียด	40	1	1

				
				3.สร้าง TABLE สำหรับตั้งค่าเริ่มต้น DB CENTER

				4.เตรียม โครงสร้าง DB CENTER โดยทำการ compare ข้อมูล ให้ Column ต้องมีเท่าทั้ง DATA TYPE และ COLUMN
				 4.1 DROP FK ทั้งหมดของ DB CENTER
				 
				 -- Script ตัวอย่างเพื่อดูว่า มี FK อะไรบ้าง พร้อม Drop Script 
				 
				 SELECT ac.table_name, ac.constraint_name, accs.column_name, accs.position, ac.r_constraint_name,'ALTER TABLE '||ac.table_name||' DROP CONSTRAINT '||ac.constraint_name||' ;' as sql
				  FROM ALL_CONSTRAINTS ac, all_cons_columns accs
				 WHERE ac.owner = '<owner>'
				   AND ac.constraint_type = 'R'
				   AND ac.table_name = '<src_table>'
				   AND accs.owner = ac.owner
				   AND accs.table_name = ac.table_name
				   AND accs.constraint_name = ac.constraint_name
				ORDER BY ac.table_name, ac.constraint_name, accs.position
				 
				 4.2 ALTER ADD COLUMN DBLINK ไปยัง TABLE DB CENTER 
				 4.3 QUERY เก็บค่า PK ของ TABLE ทั้งหมดและแก้ไขเพิ่ม Column DBLINK เป็น PK ด้วย
				 
				 
				 -- Script ตัวอย่างเพื่อดูว่า มี FK อะไรบ้าง พร้อม Drop Script 
				 
				 SELECT ac.table_name, ac.constraint_name, accs.column_name, accs.position, ac.r_constraint_name,'ALTER TABLE '||ac.table_name||' DROP CONSTRAINT '||ac.constraint_name||' ;' as sql
				  FROM ALL_CONSTRAINTS ac, all_cons_columns accs
				 WHERE ac.owner = '<owner>'
				   AND ac.constraint_type = 'P'
				   AND ac.table_name = '<src_table>'
				   AND accs.owner = ac.owner
				   AND accs.table_name = ac.table_name
				   AND accs.constraint_name = ac.constraint_name
				ORDER BY ac.table_name, ac.constraint_name, accs.position
				 
				 
				 4.4 DROP PK และ นำ SCRIPR ข้อ 4.3 มา RUN
				 4.5 สร้าง TABLE สำหรับเก็บ  SQL ข้อมูลที่ sync 
				 
				 CREATE TABLE "CMLOGSYNCDATA" ("BRANCH_ID" VARCHAR2(20) NOT NULL,"LOG_DATE" DATE NOT NULL,"MODE" CHAR(1) NOT NULL,"TABLE_NM" VARCHAR2(150) NOT NULL, "SQL" CLOB NOT NULL) ;
				 ALTER TABLE "CMLOGSYNCDATA" ADD ( CONSTRAINT PK_CMLOGSYNCDATA PRIMARY KEY ( "BRANCH_ID","LOG_DATE","MODE","TABLE_NM" )) ;

				5.เตรียมข้อมูล DB CENTER 
				 5.1 DEL ข้อมูลทั้งหมดใน TABLE ที่จะ SYNC ตาม TABLE ข้อ 2
				 5.2 INSERT Data โดย Query TABLE ตาม ข้อ 1 และ ตามจำนวน TABLE ข้อ 2 โดย FIX DBLINK column เป็นชื่อ SCHEMA 

				6.เตรียม Trigger ข้อมูลติดตั้งใน DB สาขา
				 6.1 GEN script Alter ตาม Table ข้อ 2 และ Save เก็บไว้เป็น File.sql
				 6.2 สร้าง DBLINK ตามชื่อ SCHEMA ของข้อมูล DB สาขา
				 6.3 นำ Script ข้อ 6.1 ตาม RUN ให้ครบตามจำนวน TABLE ข้อ 2
			
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
                        string sql_insert="",sql_delete = "", sql_update = "";
                        string cols_convert="";
                        while (dt_.Next())
                        {
                            string cols_name = dt_.GetString(0);

                            bool isChar = (dt_.GetString(1).ToUpper().IndexOf("CHAR") >= 0) && convert.Length>0;
                            bool isVarChar = (dt_.GetString(1).ToUpper().IndexOf("VARCHAR") >= 0) && convert.Length > 0;
                            cols_convert=(isChar)?(convert+"Char"):( (isVarChar)?(convert+"VarChar"):"" ) ;

                            if (dt_.GetString(1).ToUpper() != "BLOB")
                            {
                                cols_names += cols_name + ",";
                                cols_names_ += cols_convert+"(:new." + cols_name + "),";
                                cols_names_values += "#'||" + cols_convert + "(:new." + cols_name + ")||'#,";
                            }
                        }
                        cols_names += (dblinkfield!=""?(dblinkfield+","):"") + ")"; cols_names = cols_names.Replace(",)", ")");
                        cols_names_ += (dblinkfield != "" ? ("'"+dblink + "',") : "") + ")"; cols_names_ = cols_names_.Replace(",)", ")");
                        cols_names_values += (dblinkfield != "" ? ("#" + dblink + "#,") : "") + ")"; cols_names_values = cols_names_values.Replace(",)", ")");
                        sql_insert = "insert into " + table_name + @"@" + user + @" "+cols_names + @" values " + cols_names_ + @";";
                        output += sql_insert + @"
				
                            insert into CMLOGSYNCDATA@" + user + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'I','" + table_name + "','insert into  " + table_name + @" " + cols_names.Replace("'", "") + @" values " + cols_names_values + @"');

		                            elsif DELETING then
	
				                            ACT := 'D';
			      	                        ";

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

                            bool isChar = (dt_.GetString(1).ToUpper().IndexOf("CHAR") >= 0) && convert.Length > 0;
                            bool isVarChar = (dt_.GetString(1).ToUpper().IndexOf("VARCHAR") >= 0) && convert.Length > 0;
                            cols_convert = (isChar) ? (convert + "Char") : ((isVarChar) ? (convert + "VarChar") : "");

                            if (dt_.GetString(1).ToUpper() != "BLOB")
                            {
                                if (constraint_name != "-")
                                {
                                    cols_names += " " + cols_name + "=" + cols_convert + "(:old." + cols_name + ") and";
                                    cols_names_values_pk += " " + cols_name + "=#'||" + cols_convert + "(:old." + cols_name + ")||'# and";
                                }
                                else
                                {
                                    cols_names_ += " " + cols_name + "=(:new." + cols_name + ") ,";
                                    cols_names_values += " " + cols_name + "=#'||" + cols_convert + "(:new." + cols_name + ")||'# ,";
                                }
                            }
                        }
                        cols_names += (dblinkfield != "" ? (" " + dblinkfield + "='" + dblink + "'" + " and") : "") + ")"; cols_names = cols_names.Replace("and)", " ");
                        cols_names_values_pk += (dblinkfield != "" ? (" "+dblinkfield + "=#" + dblink + "#" + " and") : "") + ")"; cols_names_values_pk = cols_names_values_pk.Replace("and)", " ");
                        cols_names_ += ")"; cols_names_ = cols_names_.Replace(",)", " ");
                        cols_names_values += ")"; cols_names_values = cols_names_values.Replace(",)", " ");
                        sql_delete = "delete from " + table_name + @"@" + user + @" where " + cols_names + @";";
                        sql_update = "update  " + table_name + @"@" + user + @" set " + cols_names_ + @" where " + cols_names + @";";
                        output += sql_delete + @"
				
                                            insert into CMLOGSYNCDATA@" + user + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'D','" + table_name + "','delete from  " + table_name + @" where " + cols_names_values_pk + @"');
			

		                            elsif UPDATING then
	
				                            ACT := 'U';
                                            " + sql_delete + @"
                                            " + sql_insert + @"
                                            /*
				                            " +sql_update+@"
                                            */
                                            insert into CMLOGSYNCDATA@" + user + @" ( BRANCH_ID,LOG_DATE,""MODE"",TABLE_NM,""SQL"") values('" + dblink + "',sysdate,'U','" + table_name + "','update  " + table_name + @" set " + cols_names_values + @" where " + cols_names_values_pk + @"');
			
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
    }
}