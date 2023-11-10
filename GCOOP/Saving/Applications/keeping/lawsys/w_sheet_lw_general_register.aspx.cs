using System;
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
using DataLibrary;
using System.IO;
using System.Data.OleDb;


namespace Saving.Applications.lawsys
{
    public partial class w_sheet_lw_general_register : PageWebSheet,WebSheet
    {
        protected String initGenRegNo;
        protected String RetriveMain;
        protected String DeleteFileName;
        protected String DeleteRegister;
        private DwThDate tDwMain;
        private String sqlSave;
        
        //String genNo;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            RetriveMain = WebUtil.JsPostBack(this, "RetriveMain");
            initGenRegNo = WebUtil.JsPostBack(this, "initGenRegNo");
            DeleteFileName = WebUtil.JsPostBack(this, "DeleteFileName");
            DeleteRegister = WebUtil.JsPostBack(this, "DeleteRegister");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("register_date", "register_tdate");
            tDwMain.Add("return_date", "return_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                lblAlert.Visible = false;
                NewClear();
            }
            else
            {
                
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "initGenRegNo")
            {
                JsInitGenRegNo();
               
            }
            else if (eventArg == "RetriveMain")
            {
                String gen = HdGetFromDlg.Value;
                SelectGenRegNo(gen);
                DwMain.SetItemString(1, "genreg_no", gen);
              
            }
            else if (eventArg == "DeleteFileName")
            {
                JsDeleteFileName();
              
            }
            else if (eventArg == "DeleteRegister")
            {
                JsDeleteRegister();
            }
        }

        
        

        public void SaveWebSheet()
        {
            sqlSave = "";
            String YY = (DateTime.Today.Year + 543).ToString().Substring(2,2);
            String genRegNo = "";
            String regTitle = DwMain.GetItemString(1, "register_title");
            String regDesc = DwMain.GetItemString(1, "register_desc");
            String regDate = DwMain.GetItemDateTime(1, "register_date").ToString("dd/MM/yyyy", WebUtil.EN);
            String returnDate = DwMain.GetItemDateTime(1, "return_date").ToString("dd/MM/yyyy", WebUtil.EN);
            String entryDate = state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN);
            String editDate = DateTime.Today.ToString("dd/MM/yyyy", WebUtil.EN);
            String entryId = state.SsUsername;
            String editId = state.SsUsername;
            String regFrom = DwMain.GetItemString(1, "register_from");

            try
            {
                genRegNo = DwMain.GetItemString(1, "genreg_no");
            }
            catch
            {
                genRegNo = "";
            }

            Sta ta = new Sta(state.SsConnectionString);
            if(HdGenRegNo.Value == "true" && genRegNo != null && genRegNo != "")
            {                
                sqlSave = @"UPDATE LWGENERALREG
                        SET REGISTER_TITLE = '" + regTitle + "',REGISTER_DESC = '" + regDesc + "'," +
                            "REGISTER_DATE = to_date('" + regDate + "', 'dd/mm/yyyy'), RETURN_DATE = to_date('" + returnDate + "', 'dd/mm/yyyy')," +
                            "EDIT_DATE = to_date('" + editDate + "', 'dd/mm/yyyy') ,EDIT_ID = '" + editId + "'," +
                            "REGISTER_FROM = '" + regFrom + "'" + 
                        "WHERE GENREG_NO = '" + genRegNo + "'";

                try
                {
                    ta.Exe(sqlSave);
                    LtServerMessage.Text = WebUtil.CompleteMessage("อัพเดทข้อมูลเลขที่ทะเบียน " + genRegNo + " เรียบร้อยแล้ว");
                    NewClear();
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
            }
            else
            {

                try
                {
                    String sql = "SELECT max(GENREG_NO) FROM LWGENERALREG WHERE GENREG_NO like '" + YY + "%'";
                    DataTable dtSeq = WebUtil.Query(sql);
                    genRegNo = (int.Parse(dtSeq.Rows[0][0].ToString()) + 1).ToString();
                }
                catch
                {
                    genRegNo = YY + "000001";
                }
                sqlSave = @"INSERT INTO LWGENERALREG
                    (GENREG_NO,     REGISTER_TITLE,     REGISTER_DESC,      REGISTER_DATE,      RETURN_DATE, 
                     ENTRY_DATE,    EDIT_DATE,          ENTRY_ID,           EDIT_ID,            REGISTER_FROM)
                     VALUES ('" + genRegNo + "', '" + regTitle + "', '" + regDesc + "' ," + 
                              "to_date('" + regDate + "', 'dd/mm/yyyy'), " +
                              "to_date('" + returnDate + "', 'dd/mm/yyyy'), " + 
                              "to_date('" + entryDate + "', 'dd/mm/yyyy'), " +
                              "to_date('" + editDate + "', 'dd/mm/yyyy'), '" + 
                              entryId + "', '" + editId + "', '" + regFrom + "')";
                try
                {
                    ta.Exe(sqlSave);
                    LtServerMessage.Text = WebUtil.CompleteMessage("เพิ่มข้อมูลเลขที่ทะเบียน " + genRegNo + " เรียบร้อยแล้ว");
                    NewClear();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();

            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                //lblAlert.Visible = false;
                FilesTable();
            }
            catch { }
            DwMain.SaveDataCache();
        }

        #endregion

        private void JsInitGenRegNo()
        {
            String genRegNo = "";
            try
            {
                String regNo = "";
                genRegNo = DwMain.GetItemString(1, "genreg_no");
                String YY = genRegNo.Substring(0, 2);
                if (genRegNo.Length < 8)
                {
                    if (genRegNo.Length > 2)
                    {
                        regNo = WebUtil.Right("000000" + genRegNo.Substring(2, genRegNo.Length - 2), 6);
                        regNo = YY + regNo;
                    }
                }
                else
                {
                    regNo = genRegNo;
                }
                DwMain.SetItemString(1, "genreg_no", regNo);
                SelectGenRegNo(regNo);
            }
            catch(Exception ex)
            {
                genRegNo = "";
                DwMain.SetItemString(1, "genreg_no", genRegNo);
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void SelectGenRegNo(String genRegNo)
        {
            
            String sql = "";
            sql = @"SELECT 
                    REGISTER_TITLE,  REGISTER_DESC,  REGISTER_DATE,      
                    RETURN_DATE,     REGISTER_FROM
                    FROM LWGENERALREG 
                    WHERE GENREG_NO = '" + genRegNo + "'";
            DataTable dt = WebUtil.Query(sql);
            if (dt.Rows.Count > 0)
            {
                DwMain.SetItemString(1, "register_title", dt.Rows[0][0].ToString());
                DwMain.SetItemString(1, "register_desc", dt.Rows[0][1].ToString());
                DwMain.SetItemDateTime(1, "register_date", DateTime.Parse(dt.Rows[0][2].ToString()));
                DwMain.SetItemDateTime(1, "return_date", DateTime.Parse(dt.Rows[0][3].ToString()));
                DwMain.SetItemString(1, "register_from", dt.Rows[0][4].ToString());
                tDwMain.Eng2ThaiAllRow();
                HdGenRegNo.Value = "true";
            }
            else
            {
                DwMain.Reset();
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "genreg_no", genRegNo);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลเลขที่ทะเบียน " + genRegNo);
                HdGenRegNo.Value = "false";
                GvShow.Visible = false;
            }
        }

        private void NewClear()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemDateTime(1, "register_date", state.SsWorkDate);
            DwMain.SetItemDateTime(1, "return_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string regNo = DwMain.GetItemString(1, "genreg_no").ToString();

                if (regNo == "")
                {
                    lblAlert.Visible = true;
                }
                else
                {
                    
                    if (FileUpload1.HasFile)
                    {
                        DirectoryInfo DirInfo = new DirectoryInfo(Server.MapPath("Upload/" + regNo));


                        if (!DirInfo.Exists)
                        {
                            DirInfo.Create();
                        }
                        FileUpload1.SaveAs(Server.MapPath("Upload/" + regNo + "/" + FileUpload1.FileName));
                        FilesTable();
                    }
                }
            }
            catch
            {
                
                lblAlert.Visible = true;
            }

        }

       
        private void FilesTable()
        {
            
            try
            {
               
                string reqNo = DwMain.GetItemString(1, "genreg_no").ToString();
                string mappath = Server.MapPath("Upload/" + reqNo);
                DirectoryInfo DirInfo = new DirectoryInfo(mappath);
                string[] arr = Directory.GetFiles(mappath, "*.*");
                if (arr.Length > 0)
                {
                    DataTable dt = new DataTable();
                    DataColumn col = new DataColumn();
                    col.DataType = Type.GetType("System.String");
                    col.ColumnName = "FileName";
                    dt.Columns.Add(col);
                    DataColumn col_2 = new DataColumn("Download", Type.GetType("System.String"));
                    dt.Columns.Add(col_2);
                    
                    DataRow dr;
                    foreach (FileInfo file in DirInfo.GetFiles("*.*"))
                    {

                        dr = dt.NewRow();
                        dr["FileName"] = file.Name;
                        dr["Download"] = state.SsUrl + "Applications/lawsys/Upload/" + reqNo + "/" + file.Name;
                       
                        dt.Rows.Add(dr);
                    }
                    GvShow.DataSource = dt;
                    GvShow.DataBind();
                    GvShow.Visible = true;

                }
            }
            catch
            {
                lblAlert.Visible = true;
                
            }
        }

       
        private void JsDeleteFileName()
        {
            
            try
            {
                string filename = HdDeleteFileName.Value;
                string folder = DwMain.GetItemString(1, "genreg_no").ToString();
                FileInfo FileIn = new FileInfo(Server.MapPath("Upload/" + folder + "/" + filename));
                if (FileIn.Exists)
                {
                    FileIn.Delete();
                }
            }
            catch
            {
                lblAlert.Visible = true;
            }
        }

        protected void GvShow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void JsDeleteRegister()
        {
            //throw new NotImplementedException();
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                string regNo = DwMain.GetItemString(1, "genreg_no").ToString();
                string sql = "UPDATE LWGENERALREG SET DELETE_STATUS = 1 WHERE GENREG_NO = '" + regNo + "'";
                int result = ta.Exe(sql);
                if (result > 0)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบทะเบียน " + regNo + " สำเร็จแล้ว");
                }
                
                
            }
            catch { }


        }

       

        
    }
}
