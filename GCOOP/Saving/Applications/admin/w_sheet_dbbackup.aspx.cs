using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Security;
using DataLibrary;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.admin
{
    public partial class w_sheet_dbbackup : PageWebSheet, WebSheet
    {

        string dbserver = "rac", sid = "gcoop", backuppath = WebUtil.GetGcoopPathCore() + "\\DB\\";
        string savefile = "C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\0.CreateTaskScheduler_Timer.txt";
        string scriptfile = "C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\0.CreateTaskScheduler_.bat";

        public void InitJsPostBack()
        {
            this.IgnoreReadable = true;
        }

        public void actionPerformDBProfile()
        {

            LiteralMsg.Text = "";
            try
            {
                if (this.actionDBProfileA.Text == "rundelete")
                {
                    //List<string> cmdList = new List<string>(); cmdList.Add("del /Q " + WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.actionDBProfileD.Text + "*");
                    //WebUtil.RunCommand(cmdList, "DEL");
                    File.Delete(WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.actionDBProfileD.Text + "zip");
                    File.Delete(WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.actionDBProfileD.Text + "log");
                    string output = "ลบ File : " + WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.actionDBProfileD.Text +" สำเร็จ";
                    LiteralMsg.Text += "<span style=\"color:blue\">" + output + "</span><br/>";
                }

            }
            catch { }
            try
            {
                if (this.actionDBProfileA.Text == "createbacth")
                {
                    WebUtil.createBatchBackupOracle(this.DBProfile.Text, state.SsCoopId, false, ref fileBatchPath);
                    string output = "สร้าง File : " + fileBatchPath + " <br/>สำหรับใช้ในการ Backup ข้อมูลตาม DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text);
                    LiteralMsg.Text += "<span style=\"color:blue\">" + output + "</span><br/>";
                }

            }
            catch { }
            try
            {
                if (this.actionDBProfileR.Text == "runbatch")
                {
                    string output = WebUtil.createBatchBackupOracle(this.DBProfile.Text, state.SsCoopId, true, ref fileBatchPath);
                    LiteralMsg.Text += "<span style=\"color:blue\">" + output + "</span><br/>";
                }

            }
            catch { }

            try
            {
                //Restore DB
                if (this.actionDBProfileRestore.Text.Trim() != "")
                {
                    this.loadDBProfile();
                    string output=WebUtil.killOracleSessionByUser(this.ORA_TARGET_USR.Text,this.ORA_ADM_USR.Text,this.ORA_ADM_PWD.Text,(this.ORA_TARGET_DB_HOST.Text+":"+this.ORA_TARGET_DB_PORT.Text+"/"+this.ORA_TARGET_DB_SID.Text));
                    LiteralMsg.Text += "<span style=\"color:blue\">" + output + "</span><br/>";
                }

            }
            catch { }

            try
            {
                if (this.actionDBProfileJ.Text == "viewjob")
                {
                    this.loadDBProfile();
                }

            }
            catch { }

            try
            {
                if (this.actionDBProfileJ.Text == "deletejob")
                {
                    this.loadDBProfile();

                    List<string> cmdList = new List<string>();
                    string script_prefix = SCRIPT_PATH.Text.Replace(".bat", "");
                    string script_fullpath = WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.SCRIPT_PATH.Text;
                    cmdList.Add("for /f %%x in ('schtasks /query ^| findstr " + script_prefix + "') do schtasks /Delete /TN %%x /F");

                    script_prefix = WebUtil.RunCommand(cmdList, script_prefix);

                    if (script_prefix != null)
                    {
                        string sql = @"update CMDBPROFILE set SCHEDULER_DEPLOY_FLAG=0 where DBPROFILE_ID={1} and coop_id={0}  ";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, this.DBProfile.Text);
                        WebUtil.ExeSQL(sql);                       
                        string output = "บันทึกลบกำหนดการสำรองข้อมูล ตาม DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + " แล้ว";
                        TaskTimerMsg.Text = "<span style=\"color:blue\">" + output + "</span><br/>";
                    }
                    else
                    {
                        string output = "บันทึกลบกำหนดการสำรองข้อมูล ตาม DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + " ไม่สำเร็จ";
                        TaskTimerMsg.Text = "<span style=\"color:red\">" + output + "</span><br/>";
                    }
                }

            }
            catch { }

            try
            {
                if (this.actionDBProfileJ.Text == "createjob")
                {
                    this.loadDBProfile();

                        List<string> cmdList =new List<string>();
                        string script_prefix=SCRIPT_PATH.Text.Replace(".bat","");
                        string script_fullpath = WebUtil.BACKUP_SCRIPT_PATH_ROOT + this.SCRIPT_PATH.Text;
                        string[] taskTimer = null;
                        String day = null;
                        cmdList.Add("for /f %%x in ('schtasks /query ^| findstr " + script_prefix + "') do schtasks /Delete /TN %%x /F");

                            /* กรณีทำทุกๆวัน */

                            /*
                                taskTimer = this.TaskTimerTxt.Text.Split(',');

                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /TN \"BACKUPDB_" + taskTimer[i].Replace(":","_") + "\" /SC DAILY /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                                }
                             */

                            day = "MON";
                            if (this.TaskTimerTxt_0.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_0.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "TUE";
                            if (this.TaskTimerTxt_1.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_1.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "WED";
                            if (this.TaskTimerTxt_2.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_2.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "THU";
                            if (this.TaskTimerTxt_3.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_3.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "FRI";
                            if (this.TaskTimerTxt_4.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_4.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "SAT";
                            if (this.TaskTimerTxt_5.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_5.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                            day = "SUN";
                            if (this.TaskTimerTxt_6.Text.Trim() != "")
                            {
                                taskTimer = this.TaskTimerTxt_6.Text.Split(',');
                                for (int i = 0; i < taskTimer.Length; i++)
                                {
                                    cmdList.Add("SCHTASKS /Create /SC WEEKLY /TN \"" + script_prefix + "_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                                }
                            }

                    script_prefix=WebUtil.RunCommand(cmdList, script_prefix);

                    if (script_prefix != null)
                    {
                        string sql = @"update CMDBPROFILE set SCHEDULER_DEPLOY_FLAG=1 where DBPROFILE_ID={1} and coop_id={0}  ";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, this.DBProfile.Text);
                        WebUtil.ExeSQL(sql);
                        sql = "select * from CMDBPROFILE where DBPROFILE_ID={1} and coop_id={0} order by DBPROFILE_ID asc";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, this.DBProfile.Text);
                        DataTable dt = WebUtil.Query(sql);
                        if (dt.Rows.Count > 0)
                        {
                            this.TaskTimerTxt_.Text = "DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + "=" + Convert.ToString(dt.Rows[0]["ORA_TARGET_USR"]) + "@" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_HOST"]) + ":" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_PORT"]) + "/" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_SID"]);
                            this.TaskTimerTxt_0.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_MON"]);
                            this.TaskTimerTxt_1.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_TUE"]);
                            this.TaskTimerTxt_2.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_WED"]);
                            this.TaskTimerTxt_3.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_THU"]);
                            this.TaskTimerTxt_4.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_FRI"]);
                            this.TaskTimerTxt_5.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_SAT"]);
                            this.TaskTimerTxt_6.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_SUN"]);
                            string output = "บันทึกสร้างกำหนดการสำรองข้อมูล ตาม DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + " แล้ว";
                            TaskTimerMsg.Text = "<span style=\"color:blue\">" + output + "</span><br/>";
                        }
                    }
                    else
                    {
                        string output = "บันทึกสร้างกำหนดการสำรองข้อมูล ตาม DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + " ไม่สำเร็จ";
                        TaskTimerMsg.Text = "<span style=\"color:red\">" + output + "</span><br/>";
                    }
                }

            }
            catch { }


            this.initLoadDBProfile();
            this.initBackupLists();

        }

        public void initLoadDBProfile()
        {

            bool isClientSupportExpDPAll = true;
            try
            {

                Literal0.Text = "Current Oracle Client Version : " + WebUtil.getOracleClient32Version() + " ORACLE_HOME=" + WebUtil.getOracleClient32Home() + "<br/>";
                Literal0.Text += "Oracle Client ที่ติดตั้ง " + (WebUtil.checkOracleClient32SupportExpDP() ? "รองรับ EXPDP " : "<span style=\"color:red\">ไม่รองรับ EXPDP </span>") + "<br/>";
                Literal0.Text += "<table class=\"DataSourceRepeater\">";
                string sql = "select * from CMDBPROFILE where coop_id={0} and USED_FLAG=1 order by DBPROFILE_ID asc";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                DataTable dt = WebUtil.Query(sql);
                Literal0.Text += "<tr><th>ลำดับ</th><th>ฐานข้อมูล</th><th></th><th>สร้างScript</th><th>สำรองBackup</th><th>ตาราง Backup</th><th>สถานะscheduler</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string connstr = Convert.ToString(dt.Rows[i]["ORA_TARGET_USR"]) + "@" + Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_HOST"]) + ":" + Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_PORT"]) + "/" + Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_SID"]);
                    bool isClientSupportExpDP = (WebUtil.compareOracleClientVOracleDB(Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_HOST"]), Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_PORT"]), Convert.ToString(dt.Rows[i]["ORA_TARGET_DB_SID"]), Convert.ToString(dt.Rows[i]["ORA_TARGET_USR"]), Convert.ToString(dt.Rows[i]["ORA_TARGET_PWD"])) <= 0);
                    isClientSupportExpDPAll = isClientSupportExpDP & isClientSupportExpDPAll;
                    bool script_exists = (File.Exists(WebUtil.GetGcoopPathCore() + "\\DB\\" + Convert.ToString(dt.Rows[i]["SCRIPT_PATH"])));
                    string url = HttpContext.Current.Request.Url.AbsolutePath + "?setApp=" + state.SsApplication + "&setGroup=" + state.CurrentGroup + "&setWinId=" + state.CurrentPageId + "&db=" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]);
                    Literal0.Text +=
                        "<tr>" +
                        "<td align=\"center\">" + (i + 1) + "." + (isClientSupportExpDP?"":"<span style=\"color:red\">ไม่รองรับ Exp</span>") + "</td>" +
                        "<td>" +
                        (script_exists ? "<a href=\"" + WebUtil.GetUrlCore() + "/DB/" + Convert.ToString(dt.Rows[i]["SCRIPT_PATH"]) + "\" target=\"_blank\">" : "") +
                        connstr +
                        (script_exists ? "</a>" : "")+"</td>" +
                        "<td align=\"center\"><a href=\"javascript:performJ('" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]) + "','','viewjob')\" target=\"_self\">แก้ไข</a>&nbsp;</td>" +
                        "<td align=\"center\"><a href=\"javascript:performA('" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]) + "','createbacth')\" target=\"_self\">สร้างBatch</a></td>" +
                        "<td align=\"center\"><a href=\"javascript:performR('" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]) + "','runbatch')\" target=\"_self\">สำรองข้อมูล</a></td>" +
                        "<td align=\"center\"><a href=\"javascript:performJ('" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]) + "','createbacth','createjob')\" target=\"_self\">สร้าง</a>"+
                        "&nbsp;<a href=\"javascript:performJ('" + Convert.ToString(dt.Rows[i]["DBPROFILE_ID"]) + "','','deletejob')\" target=\"_self\">ลบ</a>" + "</td>" +
                        "<td align=\"center\">" + (Convert.ToInt32(dt.Rows[i]["SCHEDULER_DEPLOY_FLAG"])>0?"/":"X") + "</td></tr> ";

                }
            }
            catch { }

            Literal0.Text += "</table>";
            Literal0.Text += (isClientSupportExpDPAll ? "" : "<span style=\"color:red\"> การที่จะรองรับการ ExpDP นั้น Oracle Client Version ใหม่กว่า ไม่สามารถ ExpDP Database ที่เก่ากว่าได้ <br/>หรือ ไม่สามารถเชื่อมต่อฐานข้อมูลตามที่กำหนด ค่าได้ </br> หรือ ลง Oracle Client แบบไม่มี โปรแกรม expdp</span>");
        }

        public void initBackupLists()
        {

            Literal1.Text = "" ;
            DirectoryInfo di = new DirectoryInfo(backuppath);
            FileInfo[] filePaths = di.GetFiles();
            Array.Sort(filePaths, (x, y) => Comparer<DateTime>.Default.Compare(y.CreationTime, x.CreationTime));

            Literal1.Text += "<table class=\"DataSourceRepeater\">";
            Literal1.Text += "<tr><th>ลำดับ</th><th>รายการ</th><th>ไฟล์ Zip</th><th>ไฟล์ Log</th><th>Delete</th><!--<th>Restore</th><th>Restore Log</th>--></tr>";

            for (int i = 0, j = 1; i < filePaths.Length; i++)
            {
                int lastIndex = filePaths[i].Name.LastIndexOf("\\");
                String file = filePaths[i].Name.Substring(lastIndex + 1);

                if (file.ToLower().IndexOf("thumbs.db") >= 0)
                {
                    continue;
                }
                if (file.ToLower().IndexOf("zip") >= 0)
                {
                    string db_id=file.Substring(0, file.ToLower().IndexOf(".")-1);
                    Literal1.Text += "<tr><td align=\"center\">" + (j++) + "</td><td>" + file + "</td>"+
                        "<td align=\"center\"><a href=\"" + WebUtil.GetUrlCore() + "/DB/" + file + "\" target=\"_blank\">Download</a></td>" +
                        "<td align=\"center\"><a href=\"" + WebUtil.GetUrlCore() + "/DB/" + file.Replace("zip", "log") + "\" target=\"_blank\">Log</a></td>" +
                        "<td align=\"center\"><a href=\"javascript:performD('" + db_id + "','" + file.Replace("zip", "") + "','rundelete')\" target=\"_self\">ลบ</a></td>" +
                        "<!--<td align=\"center\"><a href=\"javascript:performRestore('" + db_id + "','" + file + "')\" >Restore</a></td>" +
                        "<td align=\"center\"><a href=\"" + WebUtil.GetUrlCore() + "/DB/" + file.Replace(".zip", "_IMPDP.log") + "\" target=\"_blank\">Restore Log</a></td>-->" +
                        "</tr> ";

                }

            }
            Literal1.Text += "</table>";
        }

        public void WebSheetLoadBegin()
        {

            try
            {
                //XmlService xs = new XmlService();

                //XmlConnection xc = xs.GetXmlConnection(state.SsConnectionIndex);

                String[] connstr = state.SsConnectionString.Split('/');//xc.ConnectionString.Split('/');

                dbserver = connstr[0];

                dbserver = dbserver.Replace("Data Source=", "");

                sid = connstr[1];

                sid=sid.Substring(0,sid.IndexOf(';'));

            }
            catch { }

            if (this.TaskTimerTxt.Text == "")
            {
                try
                {
                    StreamReader sr = new StreamReader(savefile);
                    this.TaskTimerTxt.Text = sr.ReadLine();
                    this.TaskTimerTxt_0.Text = sr.ReadLine();
                    this.TaskTimerTxt_1.Text = sr.ReadLine();
                    this.TaskTimerTxt_2.Text = sr.ReadLine();
                    this.TaskTimerTxt_3.Text = sr.ReadLine();
                    this.TaskTimerTxt_4.Text = sr.ReadLine();
                    this.TaskTimerTxt_5.Text = sr.ReadLine();
                    this.TaskTimerTxt_6.Text = sr.ReadLine();
                    sr.Close();
                }
                catch
                {
                    this.TaskTimerTxt.Text = "12:20,22:00";
                    this.TaskTimerTxt_0.Text = "12:20,22:00";
                    this.TaskTimerTxt_1.Text = "12:20,22:00";
                    this.TaskTimerTxt_2.Text = "12:20,22:00";
                    this.TaskTimerTxt_3.Text = "12:20,22:00";
                    this.TaskTimerTxt_4.Text = "12:20,22:00";
                    this.TaskTimerTxt_5.Text = "22:00";
                    this.TaskTimerTxt_6.Text = "22:00";
                }
            }

            this.initLoadDBProfile();

            this.initBackupLists();

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

        protected void CreateTaskButton_Click(object sender, EventArgs e)
        {
            try
            {
                //บันทึกค่า
                StreamWriter sw = new StreamWriter(savefile, false);
                sw.WriteLine(this.TaskTimerTxt.Text);
                sw.WriteLine(this.TaskTimerTxt_0.Text);
                sw.WriteLine(this.TaskTimerTxt_1.Text);
                sw.WriteLine(this.TaskTimerTxt_2.Text);
                sw.WriteLine(this.TaskTimerTxt_3.Text);
                sw.WriteLine(this.TaskTimerTxt_4.Text);
                sw.WriteLine(this.TaskTimerTxt_5.Text);
                sw.WriteLine(this.TaskTimerTxt_6.Text);
                sw.Close();

                string[] taskTimer = null;
                String day = null;
                sw = new StreamWriter(scriptfile, false);
                sw.WriteLine("for /f %%x in ('schtasks /query ^| findstr BACKUP') do schtasks /Delete /TN %%x /F");

                /* กรณีทำทุกๆวัน */

                /*
                    taskTimer = this.TaskTimerTxt.Text.Split(',');

                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /TN \"BACKUPDB_" + taskTimer[i].Replace(":","_") + "\" /SC DAILY /ST " + taskTimer[i] + " /IT /TR \"" + script_fullpath + "\" ");
                    }
                 */

                day = "MON";
                if (this.TaskTimerTxt_0.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_0.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "TUE";
                if (this.TaskTimerTxt_1.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_1.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "WED";
                if (this.TaskTimerTxt_2.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_2.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "THU";
                if (this.TaskTimerTxt_3.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_3.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "FRI";
                if (this.TaskTimerTxt_4.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_4.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "SAT";
                if (this.TaskTimerTxt_5.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_5.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                day = "SUN";
                if (this.TaskTimerTxt_6.Text.Trim() != "")
                {
                    taskTimer = this.TaskTimerTxt_6.Text.Split(',');
                    for (int i = 0; i < taskTimer.Length; i++)
                    {
                        sw.WriteLine("SCHTASKS /Create /SC WEEKLY /TN \"BACKUPDB_" + day + taskTimer[i].Replace(":", "_") + "\" /D " + day + " /ST " + taskTimer[i] + " /IT /TR \"C:\\GCOOP_ALL\\FSCT\\DB\\DB.Backup\\ExpOracleDB.bat ifsct ifsct " + dbserver + " 1521 " + sid + " " + backuppath + "\" ");
                    }
                }

                    sw.WriteLine("del 0.CreateTaskScheduler_.bak.bat /F");
                    sw.WriteLine("copy 0.CreateTaskScheduler_.bat 0.CreateTaskScheduler_.bak.bat");
                    sw.WriteLine("del 0.CreateTaskScheduler_.bat /F");
                sw.Close();

                LtServerMessage.Text = "<center><p style=\"color:green\">สร้าง Task Scheduler สำเร็จ</p></center>";
            }
            catch
            {
                LtServerMessage.Text = "<center><p style=\"color:red\">สร้าง Task Scheduler ไม่สำเร็จ</p></center>";
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
            if (Arguments != null && Arguments != "") s.Arguments = "/C \"" + Arguments + "\"";
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

        string fileBatchPath;
        protected void RunProcessButton_Click(object sender, EventArgs e)
        {
            //WebUtil.runProcessing(
            //WebUtil.RunCommand("", "call " + WebUtil.GetGcoopPathCore() + "\\DB\\ExpDP_Oracle.bat");
            string output = WebUtil.createBatchBackupOracle("0", state.SsCoopId, true, ref fileBatchPath);
            LtServerMessage.Text = "<center><p style=\"color:blue\">" + output + "</p></center>";
        }

        protected void RunActionDBProfile_Click(object sender, EventArgs e)
        {
            this.actionPerformDBProfile();

        }

        protected void RunSetupDBProfile_Click(object sender, EventArgs e)
        {
            if (this.setupDBProfile())
            {
                LtServerMessage.Text = "<center><p style=\"color:blue\">สร้างค่าเริ่มต้นสำเร็จ</p></center>";
                this.initBackupLists();
            }
            else
            {
                LtServerMessage.Text = "<center><p style=\"color:red\">สร้างค่าเริ่มต้นไม่สำเร็จ</p></center>";
            }

            this.initLoadDBProfile();
        }

        public bool setupDBProfile()
        {

            try
            {
                string sql = @"drop table CMDBPROFILE cascade constraints";
                try { WebUtil.ExeSQL(sql);}catch { }
                sql = @"
                CREATE TABLE CMDBPROFILE (
                 DBPROFILE_ID VARCHAR2(30) NOT NULL, 
                 COOP_ID VARCHAR2(10) NOT NULL,
                 ORA_ADM_USR VARCHAR2(20) NOT NULL,
                 ORA_ADM_PWD VARCHAR2(20) NOT NULL,
                 ORA_ADM_EXP_USR VARCHAR2(20) NOT NULL,
                 ORA_ADM_EXP_PWD VARCHAR2(20) NOT NULL,
                 ORA_TARGET_DB_HOST VARCHAR2(40) NOT NULL,
                 ORA_TARGET_DB_PORT VARCHAR2(6) NOT NULL,
                 ORA_TARGET_DB_SID VARCHAR2(10) NOT NULL,
                 ORA_TARGET_USR VARCHAR2(20) NOT NULL,
                 ORA_TARGET_PWD VARCHAR2(20) NOT NULL,
                 ORA_VERSION VARCHAR2(10) NOT NULL,
                 DATAPUMP_DIR CHAR(1) NOT NULL,
                 DATAPUMP VARCHAR2(20) NOT NULL,
                 ORA_OS_USR VARCHAR2(20) NOT NULL,
                 ORA_OS_PWD VARCHAR2(20) NOT NULL,
                 NLS_LANG VARCHAR2(50) NOT NULL,
                 USED_FLAG NUMBER(1) DEFAULT 1 NOT NULL ,
                 BACKUP_PATH VARCHAR2(250) NOT NULL,
				 SCRIPT_PATH VARCHAR2(250) NOT NULL,
				 SCHEDULER_MON VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_TUE VARCHAR2(50)  DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_WED VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_THU VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_FRI VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_SAT VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
				 SCHEDULER_SUN VARCHAR2(50) DEFAULT '12:20,22:00' NOT NULL,
                 SCHEDULER_DEPLOY_FLAG NUMBER(1) DEFAULT 0 NOT NULL 
                 )  ";
                WebUtil.ExeSQL(sql);
                sql = " ALTER TABLE CMDBPROFILE ADD ( CONSTRAINT CMDBPROFILE_PK PRIMARY KEY ( DBPROFILE_ID,COOP_ID )) ";
                WebUtil.ExeSQL(sql);

                sql = "select NVL(DBPROFILE_ID,0)+1 as DBPROFILE_ID from (select max(DBPROFILE_ID) as DBPROFILE_ID from CMDBPROFILE where coop_id={0} ) ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    this.DBProfile.Text = Convert.ToString(dt.Rows[0]["DBPROFILE_ID"]);
                }

                sql = @"insert into CMDBPROFILE (
                                DBPROFILE_ID,coop_id,
                                SCHEDULER_MON,SCHEDULER_TUE,SCHEDULER_WED,SCHEDULER_THU,SCHEDULER_FRI,SCHEDULER_SAT,SCHEDULER_SUN,
                                ORA_ADM_USR,ORA_ADM_PWD,ORA_ADM_EXP_USR,ORA_ADM_EXP_PWD,
                                ORA_TARGET_DB_HOST,ORA_TARGET_DB_PORT,ORA_TARGET_DB_SID,ORA_TARGET_USR,ORA_TARGET_PWD,
                                ORA_VERSION,DATAPUMP_DIR,DATAPUMP,ORA_OS_USR,ORA_OS_PWD,NLS_LANG,BACKUP_PATH,SCRIPT_PATH 
                               )values( {1},{0},
                                {2},{3},{4},{5},{6},{7},{8},
                                {9},{10},{11},{12},
                                {13},{14},{15},{16},{17},
                                {18},{19},{20},{21},{22},{23},{24},{25} 
                               )";
                sql = WebUtil.SQLFormat(sql,
                     state.SsCoopId, this.DBProfile.Text,
                     "12:20,22:00", "12:20,22:00", "12:20,22:00", "12:20,22:00", "12:20,22:00", "22:00", "22:00",
                     "sys", "admin", "system", "admin",
                     "127.0.0.1", "1521", "icoop", "ISCOICOOPTRN", "ISCOICOOPTRN".ToLower(),
                     "11.2", "C", "DATAPUMP", "Administrator", "Admin123", "AMERICAN_AMERICA.TH8TISASCII", "C:\\DATAPUMP", ("ExpDP_Oracle.DBPROFILE_ID." + this.DBProfile.Text + ".bat")
                    );
                WebUtil.ExeSQL(sql);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool loadDBProfile()
        {
            try
            {

                string sql = "select * from CMDBPROFILE where DBPROFILE_ID={1} and coop_id={0} and USED_FLAG=1 order by DBPROFILE_ID asc";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, this.DBProfile.Text);
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    this.TaskTimerTxt_.Text = "DBPROFILE_ID=" + Convert.ToString(this.DBProfile.Text) + "=" + Convert.ToString(dt.Rows[0]["ORA_TARGET_USR"]) + "@" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_HOST"]) + ":" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_PORT"]) + "/" + Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_SID"]);
                    ORA_ADM_USR.Text = Convert.ToString(dt.Rows[0]["ORA_ADM_USR"]);
                    ORA_ADM_PWD.Text = Convert.ToString(dt.Rows[0]["ORA_ADM_PWD"]);
                    ORA_ADM_EXP_USR.Text = Convert.ToString(dt.Rows[0]["ORA_ADM_EXP_USR"]);
                    ORA_ADM_EXP_PWD.Text = Convert.ToString(dt.Rows[0]["ORA_ADM_EXP_PWD"]);
                    ORA_TARGET_DB_HOST.Text = Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_HOST"]);
                    ORA_TARGET_DB_PORT.Text = Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_PORT"]);
                    ORA_TARGET_DB_SID.Text = Convert.ToString(dt.Rows[0]["ORA_TARGET_DB_SID"]);
                    ORA_TARGET_USR.Text = Convert.ToString(dt.Rows[0]["ORA_TARGET_USR"]).ToUpper();
                    ORA_TARGET_PWD.Text = Convert.ToString(dt.Rows[0]["ORA_TARGET_PWD"]);
                    ORA_VERSION.Text = Convert.ToString(dt.Rows[0]["ORA_VERSION"]);
                    DATAPUMP_DIR.Text = Convert.ToString(dt.Rows[0]["DATAPUMP_DIR"]);
                    DATAPUMP.Text = Convert.ToString(dt.Rows[0]["DATAPUMP"]);
                    ORA_OS_USR.Text = Convert.ToString(dt.Rows[0]["ORA_OS_USR"]);
                    ORA_OS_PWD.Text = Convert.ToString(dt.Rows[0]["ORA_OS_PWD"]);
                    NLS_LANG.Text = Convert.ToString(dt.Rows[0]["NLS_LANG"]);
                    BACKUP_PATH.Text = Convert.ToString(dt.Rows[0]["BACKUP_PATH"]);
                    SCRIPT_PATH.Text = Convert.ToString(dt.Rows[0]["SCRIPT_PATH"]);
                    this.TaskTimerTxt_0.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_MON"]);
                    this.TaskTimerTxt_1.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_TUE"]);
                    this.TaskTimerTxt_2.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_WED"]);
                    this.TaskTimerTxt_3.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_THU"]);
                    this.TaskTimerTxt_4.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_FRI"]);
                    this.TaskTimerTxt_5.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_SAT"]);
                    this.TaskTimerTxt_6.Text = Convert.ToString(dt.Rows[0]["SCHEDULER_SUN"]);
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public bool insertDBProfile()
        {
            try
            {
                string sql = "select NVL(DBPROFILE_ID,0)+1 as DBPROFILE_ID from (select max(DBPROFILE_ID) as DBPROFILE_ID from CMDBPROFILE where coop_id={0} ) ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    this.DBProfile.Text = Convert.ToString(dt.Rows[0]["DBPROFILE_ID"]);
                }

                sql = @"insert into CMDBPROFILE (
                                DBPROFILE_ID,coop_id,
                                SCHEDULER_MON,SCHEDULER_TUE,SCHEDULER_WED,SCHEDULER_THU,SCHEDULER_FRI,SCHEDULER_SAT,SCHEDULER_SUN,
                                ORA_ADM_USR,ORA_ADM_PWD,ORA_ADM_EXP_USR,ORA_ADM_EXP_PWD,
                                ORA_TARGET_DB_HOST,ORA_TARGET_DB_PORT,ORA_TARGET_DB_SID,ORA_TARGET_USR,ORA_TARGET_PWD,
                                ORA_VERSION,DATAPUMP_DIR,DATAPUMP,ORA_OS_USR,ORA_OS_PWD,NLS_LANG,BACKUP_PATH,SCRIPT_PATH
                               )values( {1},{0},
                                {2},{3},{4},{5},{6},{7},{8},
                                {9},{10},{11},{12},
                                {13},{14},{15},{16},{17},
                                {18},{19},{20},{21},{22},{23},{24},{25} 
                               )";
                sql = WebUtil.SQLFormat(sql,
                     state.SsCoopId, this.DBProfile.Text,
                     TaskTimerTxt_0.Text, TaskTimerTxt_1.Text, TaskTimerTxt_2.Text,
                     TaskTimerTxt_3.Text, TaskTimerTxt_4.Text, TaskTimerTxt_5.Text, TaskTimerTxt_6.Text,
                     ORA_ADM_USR.Text, ORA_ADM_PWD.Text, ORA_ADM_EXP_USR.Text, ORA_ADM_EXP_PWD.Text,
                     ORA_TARGET_DB_HOST.Text, ORA_TARGET_DB_PORT.Text, ORA_TARGET_DB_SID.Text, ORA_TARGET_USR.Text, ORA_TARGET_PWD.Text,
                     ORA_VERSION.Text, DATAPUMP_DIR.Text, DATAPUMP.Text, ORA_OS_USR.Text, ORA_OS_PWD.Text, NLS_LANG.Text, BACKUP_PATH.Text, ("ExpDP_Oracle.DBPROFILE_ID." + this.DBProfile.Text + ".bat")
                    );
                WebUtil.ExeSQL(sql);

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void RunActionNewDBProfile_Click(object sender, EventArgs e)
        {
            this.insertDBProfile();
            this.loadDBProfile();
            this.initLoadDBProfile();
            TaskTimerMsg.Text = "<span style=\"color:blue\">บันทึกข้อมูลสำเร็จ</span><br/>";
        }

        public bool updateDBProfile(){
            try
            {

                string sql = @"update CMDBPROFILE set 
                                SCHEDULER_MON={2},SCHEDULER_TUE={3},SCHEDULER_WED={4},
                                SCHEDULER_THU={5},SCHEDULER_FRI={6},SCHEDULER_SAT={7},SCHEDULER_SUN={8},
                                ORA_ADM_USR={9},ORA_ADM_PWD={10},ORA_ADM_EXP_USR={11},ORA_ADM_EXP_PWD={12},
                                ORA_TARGET_DB_HOST={13},ORA_TARGET_DB_PORT={14},ORA_TARGET_DB_SID={15},ORA_TARGET_USR={16},ORA_TARGET_PWD={17},
                                ORA_VERSION={18},DATAPUMP_DIR={19},DATAPUMP={20},ORA_OS_USR={21},ORA_OS_PWD={22},NLS_LANG={23},BACKUP_PATH={24} 
                               where DBPROFILE_ID={1} and coop_id={0}";
                sql = WebUtil.SQLFormat(sql,
                     state.SsCoopId, this.DBProfile.Text,
                     TaskTimerTxt_0.Text, TaskTimerTxt_1.Text, TaskTimerTxt_2.Text,
                     TaskTimerTxt_3.Text, TaskTimerTxt_4.Text, TaskTimerTxt_5.Text, TaskTimerTxt_6.Text,
                     ORA_ADM_USR.Text, ORA_ADM_PWD.Text, ORA_ADM_EXP_USR.Text, ORA_ADM_EXP_PWD.Text,
                     ORA_TARGET_DB_HOST.Text, ORA_TARGET_DB_PORT.Text, ORA_TARGET_DB_SID.Text, ORA_TARGET_USR.Text, ORA_TARGET_PWD.Text,
                     ORA_VERSION.Text, DATAPUMP_DIR.Text, DATAPUMP.Text, ORA_OS_USR.Text, ORA_OS_PWD.Text, NLS_LANG.Text, BACKUP_PATH.Text 
                    );
                WebUtil.ExeSQL(sql);

                return true;
            }
            catch {
                return false;
            }
        }
        protected void RunActionSaveDBProfile_Click(object sender, EventArgs e)
        {
            this.updateDBProfile();
            this.loadDBProfile(); 
            this.initLoadDBProfile();
            TaskTimerMsg.Text = "<span style=\"color:blue\">บันทึกข้อมูลสำเร็จ</span><br/>";
        }


        public bool deleteDBProfile()
        {
            try
            {

                string sql = @"delete from  CMDBPROFILE where DBPROFILE_ID={1} and coop_id={0} ";
                sql = WebUtil.SQLFormat(sql,state.SsCoopId, this.DBProfile.Text);
                WebUtil.ExeSQL(sql);

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected void RunActionDeleteDBProfile_Click(object sender, EventArgs e)
        {
            this.deleteDBProfile();
            TaskTimerTxt_.Text = "";
            this.initLoadDBProfile();
        }

    }
}