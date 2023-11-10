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
using System.Collections.Generic;
using System.IO;

namespace Saving
{
    public partial class w_sheet_atmoffline_exp : PageWebSheet, WebSheet
    {
        String FORMAT_CODE = "ATM_BAY_EXP";
        Dictionary<string, object> data = null;
        String[] keys = { "HEADER", "FOOTER", "DETAIL" };
        protected String jsExportFile, jsDownloadFile, jsPreviewFile, jsCancelFile;
        public String output = "";
        public void InitJsPostBack()
        {
            jsExportFile = WebUtil.JsPostBack(this, "jsExportFile");
            jsDownloadFile = WebUtil.JsPostBack(this, "jsDownloadFile");
            jsPreviewFile = WebUtil.JsPostBack(this, "jsPreviewFile");
            jsCancelFile = WebUtil.JsPostBack(this, "jsCancelFile");
            this.IgnoreReadable = true;
        }

        public void init()
        {
            WebUtil.initAtmOfflineConfig();
            try
            {
                string sql = "select txtformat_code,concat(concat(txtformat_code,' - '),txtformat_desc ) as txtformat_desc from cmtxtformatmas where txtformat_code like '%EXP%' order by txtformat_code asc";
                DataTable DpFileFormat = WebUtil.Query(sql);
                DpFileFormatCode.DataTextField = "txtformat_desc";
                DpFileFormatCode.DataValueField = "txtformat_code";
                DpFileFormatCode.DataSource = DpFileFormat;
                DpFileFormatCode.DataBind();
                // ตั้งค่า default ให้ dropdown database
                if (DpFileFormat.Rows.Count > 0)
                {
                    DpFileFormatCode.SelectedIndex = 0;
                }
            }
            catch { }

            this.initDpFileLists();
        }

        public void initDpFileLists()
        {

            try
            {
                string sql = "select distinct ref_docno from atmtmptransdata where format_code = '" + DpFileFormatCode.SelectedValue + "' order by ref_docno desc";
                DataTable DpFileListsData = WebUtil.Query(sql);
                DpFileLists.DataTextField = "ref_docno";
                DpFileLists.DataValueField = "ref_docno";
                DpFileLists.DataSource = DpFileListsData;
                DpFileLists.DataBind();
                // ตั้งค่า default ให้ dropdown database
                if (DpFileListsData.Rows.Count > 0)
                {
                    DpFileLists.SelectedIndex = 0;
                }
            }
            catch { }
        }

        public void initTableData()
        {

            TableGrid.TableQuery = "select * from ATMTMPTRANSDATA where export_status<>-9 and item_status<>-9 and ref_docno='" + this.DpFileLists.SelectedValue + "'  and bank_code=(select bank_code from cmtxtformatmas where txtformat_code='" + this.DpFileFormatCode.SelectedValue + "') ";
            TableGrid.retreive();
        }

        public void getMemberInfo(String saving_acc, String system_code, ref String member_no, ref String memb_name, ref String refcoopacc_no, String loantype_code = "88", String depttype_code = "88")
        {
            String sql = "";
            member_no = "x";
            refcoopacc_no = "x";
            try
            {
                sql = "select * from cmtxtformatmas where txtformat_code='" + this.DpFileFormatCode.SelectedValue + "'";
                Sdt sdt = WebUtil.QuerySdt(sql);
                if (sdt.Next())
                {
                    loantype_code = sdt.GetString("loantype_code");
                    depttype_code = sdt.GetString("depttype_code");
                }

                if (system_code == "01") //LOAN
                {
                    sql = "select * from lncontmaster where expense_accid='" + saving_acc + "' and loantype_code='" + loantype_code + "' ";
                    sdt = WebUtil.QuerySdt(sql);
                    if (sdt.Next())
                    {
                        member_no = sdt.GetString("member_no");
                        refcoopacc_no = sdt.GetString("loancontract_no");
                    }
                }
                else if (system_code == "02") //DEPT
                {
                    sql = "select * from dpdeptmaster where bank_accid='" + saving_acc + "' and depttype_code='" + depttype_code + "' ";
                    sdt = WebUtil.QuerySdt(sql);
                    if (sdt.Next())
                    {
                        member_no = sdt.GetString("member_no");
                        refcoopacc_no = sdt.GetString("deptaccount_no");
                    }
                }

            }
            catch { }
        }


        public void WebSheetLoadBegin()
        {
            

            if (!IsPostBack)
            {
                Session["HdFilename"] = "";
                this.init();
                this.initDpFileLists();
            }
            else
            {
            }
            this.FORMAT_CODE = this.DpFileFormatCode.SelectedValue;
            this.initTableData();
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsExportFile":
                    ExportFile();
                    break;
                case "jsDownloadFile":
                    DownloadFile();
                    break;
                case "jsPreviewFile":
                    PreviewFile();
                    break;
                case "jsCancelFile":
                    CancelFile();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
        }
        
        public void CancelFile()
        {
            string filename = this.DpFileLists.SelectedValue;
            try
            {
                String sql = "update ATMTMPTRANSDATA set item_status=-9,post_date=sysdate where export_status=-9 and ref_docno='" + this.DpFileLists.SelectedValue + "'";
                WebUtil.ExeSQL(sql);
                this.initTableData();
                LtServerMessage.Text = WebUtil.CompleteMessage("Cancel successfully." + filename);
            }
            catch { 
            
            }
        }


        public void PreviewFile()
        {
            String root_path = Server.MapPath("fileexport/");
            string filename = root_path+this.DpFileLists.SelectedValue;
            PreviewData(filename);
        }

        public void DownloadFile()
        {
            String root_path = Server.MapPath("fileexport/");
             string filename = root_path+this.DpFileLists.SelectedValue;
             LtServerMessage.Text = WebUtil.CompleteMessage("File exported successfully." + filename);
             CoreSavingLibrary.WebUtil.SetCurrentFileContentDownload(filename);
        }


        public void ExportFile()
        {
            string filename = string.Empty;
            try
            {

                if (Session["HdFilename"] != null && File.Exists(Session["HdFilename"].ToString()))
                {
                    filename = Session["HdFilename"].ToString();
                    this.HdFilename.Value = filename;
                    try { Directory.CreateDirectory(Server.MapPath("fileexport/")); }
                    catch { }
                    String root_path = Server.MapPath("fileexport/");
                    String suffix = "";
                    String prefix = "";
                    Dictionary<string, object> data = WebUtil.getFileFormatDataBy(FORMAT_CODE, filename);
                    String fileexport = WebUtil.saveToFileFormat(FORMAT_CODE, data, root_path, state.SsCoopId, suffix, prefix, DateTime.Now);
                    this.output = "EXPORT DATA <br/>";
                    this.output += fileexport;
                    PreviewData(fileexport);
                    LtServerMessage.Text = fileexport + " Export File.";
                    LtServerMessage.Text = WebUtil.CompleteMessage("File exported successfully.");
                    //this.PreviewData(Session["HdFilename"].ToString());
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void PreviewData(string filename)
        {
            //String output = "";
            try
            {

                FileInfo fi = new FileInfo(filename);
                if (fi.Exists)
                {

                    data = WebUtil.getFileFormatDataBy(FORMAT_CODE, filename);
                    output += "<br/>";
                    for (int k = 0; k < keys.Length; k++)
                    {
                        Dictionary<string, string>[] list = (Dictionary<string, string>[])data[keys[k]];
                        output += "" + keys[k] + "<br/>==========<br/>";
                        for (int i = 0; i < list.Length; i++)
                        {
                            Dictionary<string, string> d = (Dictionary<string, string>)list[i];
                            foreach (string key in d.Keys)
                            {
                                output += (i + 1) + ")." + key + "=" + d[key] + "<br/>";
                            }
                        }
                        output += "" + keys[k] + "<br/>==========<br/>";
                    }


                }
            }
            catch { }
        }

    }
}
