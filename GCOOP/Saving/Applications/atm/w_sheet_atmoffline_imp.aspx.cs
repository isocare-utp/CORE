using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
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
using System.Data;

namespace Saving
{

    public partial class w_sheet_atmoffline_imp : PageWebSheet, WebSheet
    {
        String FORMAT_CODE = "ATM_BAY_IMP";
        Dictionary<string, object> data = null;
        String[] keys = { "HEADER", "FOOTER", "DETAIL" };
        protected String jsUploadFile, jsDownloadFile, jsPreviewFile, jsCancelFile, jsSaveData, jsPostFile;
        public String output = "";

        #region Events Handlers

        public void InitJsPostBack()
        {
            jsUploadFile = WebUtil.JsPostBack(this, "jsUploadFile");
            jsDownloadFile = WebUtil.JsPostBack(this, "jsDownloadFile");
            jsPreviewFile = WebUtil.JsPostBack(this, "jsPreviewFile");
            jsCancelFile = WebUtil.JsPostBack(this, "jsCancelFile");
            jsSaveData = WebUtil.JsPostBack(this, "jsSaveData");
            jsPostFile = WebUtil.JsPostBack(this, "jsPostFile");
            this.IgnoreReadable = true;
        }

        public void init()
        {
            WebUtil.initAtmOfflineConfig();
            try
            {
                string sql = "select txtformat_code,concat(concat(txtformat_code,' - '),txtformat_desc ) as txtformat_desc from cmtxtformatmas where txtformat_code like '%IMP%' order by txtformat_code asc";
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

            TableGrid.TableQuery = "select * from ATMTMPTRANSDATA where export_status=-9 and post_status=0 and item_status<>-9 and ref_docno='" + this.DpFileLists.SelectedValue + "' and bank_code=(select bank_code from cmtxtformatmas where txtformat_code='"+this.DpFileFormatCode.SelectedValue+"') ";
            TableGrid.retreive();
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

            //WebUtil.GenerateGridViewEditable(ref this.TableGridView, (String)Session["TableSelected"]);
            //CreateTemplatedGridView();

        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsUploadFile":
                    UploadFile();
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
                case "jsSaveData":
                    SaveData();
                    break;
                case "jsPostFile":
                    PostFile();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            SaveData();
        }

        public void SaveData(){
            this.output = "SAVE DATA <br/>";
            this.HdFilename.Value = Session["HdFilename"].ToString();
            this.PreviewData(this.HdFilename.Value);
            this.output = "";
            String filename = this.HdFilename.Value.Substring(this.HdFilename.Value.LastIndexOf("\\")+1);
            this.saveToTemp(filename);
            this.initDpFileLists();
            this.DpFileLists.SelectedValue = filename;
            this.initTableData();
            Session["HdFilename"] = "";
            LtServerMessage.Text = WebUtil.CompleteMessage("Save Data from " + filename + " uploaded successfully.");
        }

        public void PostFile()
        {
            String sql = "";
            try
            {
                sql = "select * from coop";
                Sdt sdt = WebUtil.QuerySdt(sql);
                bool valid = true;
                if (sdt.Next() && valid)
                {
                    String coop_id = sdt.GetString("coop_id");
                    sql = "select * from ATMTMPTRANSDATA where member_no <>'x' and ref_accno<>'x' and export_status=-9 and post_status=0 and item_status<>-9";
                    sdt = WebUtil.QuerySdt(sql);
                    while (sdt.Next())
                    {
                        String system_code=sdt.GetString("system_code");
                        String operate_code=sdt.GetString("operate_code");
                        sql = "select * from " + (system_code =="01"?"atmloan":"atmdept")+ " where saving_acc='" + sdt.GetString("saving_acc") + "'";
                        Sdt sdt_ = WebUtil.QuerySdt(sql);
                        if (sdt_.Next())
                        {
                            Decimal credit_amt = sdt_.GetDecimal((system_code == "01" ? "credit_amt" : "withatm_amt"));
                            Decimal receive_amt = sdt_.GetDecimal((system_code == "01" ? "receive_amt" : "receive_amt"));
                            Decimal pay_amt = sdt_.GetDecimal((system_code == "01" ? "pay_amt" : "pay_amt"));
                            Decimal curcredit_amt = credit_amt + pay_amt - receive_amt;
                            Decimal o_flag = (operate_code == "002" ? -1 : 1);                            
                            Decimal item_amt = sdt.GetDecimal("item_amt") ;

                            sql = @"INSERT INTO ATMTRANSACTION (
                                MEMBER_NO,SAVING_ACC,ATM_NO ,ATM_SEQNO,
                                CCS_OPERATE_DATE , OPERATE_DATE,
                                ITEM_AMT,SYSTEM_CODE ,OPERATE_CODE ,
                                COOP_ID,BANK_CODE ,BRANCH_CODE ,
                                ITEM_STATUS ,POST_STATUS )VALUES(
                                '" + sdt.GetString("member_no") + @"','" + sdt.GetString("saving_acc") + @"','" + sdt.GetString("atm_no") + @"' ,'" + sdt.GetString("atm_seqno") + @"',
                                sysdate, to_date('" + sdt.GetDate("operate_date").ToString("yyyy/MM/dd HH:mm:ss") + @"','yyyy/mm/dd hh24:mi:ss'),
                                '" + sdt.GetString("item_amt") + @"','" + sdt.GetString("system_code") + @"' ,'" + sdt.GetString("operate_code") + @"'  ,
                                '" + coop_id + @"','" + sdt.GetString("bank_code") + @"','" + sdt.GetString("branch_code") + @"' ,0, 0 
                                )";

                            if ((curcredit_amt + (item_amt * o_flag ))< 0)
                            {
                                valid = false;
                                LtServerMessage.Text = WebUtil.WarningMessage("เกินวงเงินเลขที่บัญชี " + sql);
                            }
                            else
                            {
                                WebUtil.ExeSQL(sql);
                                sql = "update ATMTMPTRANSDATA set item_status=1,post_status=1,post_date=sysdate where member_no='" + sdt.GetString("member_no") + @"' and ref_docno='" + sdt.GetString("ref_docno") + @"' and atm_no='" + sdt.GetString("atm_no") + @"' and atm_seqno='" + sdt.GetString("atm_seqno") + @"'  ";
                                WebUtil.ExeSQL(sql);
                            }

                        }
                        else
                        {
                            valid = false;
                            LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชี " + sql);
                        }
                    }
                    if (valid)
                        LtServerMessage.Text = WebUtil.CompleteMessage("ทำรายการสำเร็จ"+sql);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่มี TABLE รองรับ ATM Transaction");
                }
            }catch(Exception e){
                LtServerMessage.Text = WebUtil.ErrorMessage("post error. " + e.Message + " : "+sql);
            }
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
            String root_path = Server.MapPath("fileupload/");
            string filename = root_path+this.DpFileLists.SelectedValue;
            PreviewData(filename);
        }

        public void DownloadFile()
        {
            String root_path = Server.MapPath("fileupload/");
             string filename = root_path+this.DpFileLists.SelectedValue;
             LtServerMessage.Text = WebUtil.CompleteMessage("File exported successfully." + filename);
             CoreSavingLibrary.WebUtil.SetCurrentFileContentDownload(filename);
        }

        public void UploadFile()
        {
            string filename = string.Empty;
            try
            {
                string[] validFileTypes = { "txt", "ini", "csv" };
                string ext = System.IO.Path.GetExtension(fiUpload.PostedFile.FileName);
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

                    LtServerMessage.Text = WebUtil.ErrorMessage("Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes));
                }
                else
                {
                    if (this.fiUpload.HasFile)
                    {
                        //filename = DateTime.Now.ToString("ddMMyyyy_hhmmss") + "_" + fiUpload.FileName;
                        filename = fiUpload.FileName;
                        try { Directory.CreateDirectory(Server.MapPath("fileupload/")); }
                        catch { }
                        this.HdFilename.Value = Server.MapPath("fileupload/" + filename);
                        Session["HdFilename"] = this.HdFilename.Value;
                        filename = DateTime.Now.ToString("ddMMyyyy_hhmmss") + "_" + fiUpload.FileName;
                        this.fiUpload.SaveAs(this.HdFilename.Value);
                        LtServerMessage.Text = filename + " Uploaded.";
                        LtServerMessage.Text = WebUtil.CompleteMessage("File uploaded successfully." + filename);
                        this.output = "UPLOAD DATA <br/>";
                        this.PreviewData(Session["HdFilename"].ToString());
                    }
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


        #endregion

        public void getMemberInfo(String saving_acc, String system_code, ref String member_no, ref String memb_name, ref String refcoopacc_no,String loantype_code = "88",String depttype_code = "88")
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
                    sql = "select * from lncontmaster where expense_accid='" + saving_acc + "' and loantype_code='" + loantype_code + "' and contract_status=1 ";
                    sdt = WebUtil.QuerySdt(sql);
                    if (sdt.Next())
                    {
                        member_no = sdt.GetString("member_no");
                        refcoopacc_no = sdt.GetString("loancontract_no");
                    }
                }
                else if (system_code == "02") //DEPT
                {
                    sql = "select * from dpdeptmaster where bank_accid='" + saving_acc + "' and depttype_code='" + depttype_code + "' and deptclose_status=0 ";
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


        public void saveATM_BAY_IMP(String ref_docno,Dictionary<string, string>[] HEADER ,Dictionary<string, string>[] DETAIL ,Dictionary<string, string>[] FOOTER )
        {
                String sql = "delete from ATMTMPTRANSDATA where ref_docno='" + ref_docno + "'";
                WebUtil.ExeSQL(sql);

                String loantype_code = "88", depttype_code = "88";
                Sdt sdt = WebUtil.QuerySdt("select * from cmtxtformatmas where txtformat_code='" + FORMAT_CODE + "'");
                sdt.Next(); 
                loantype_code = sdt.GetString("loantype_code"); 
                depttype_code = sdt.GetString("depttype_code"); ;
                    
                for (int i = 0; i < DETAIL.Length;i++ )
                {
                    String SYSTEM_CODE = "x", OPERATE_CODE = "x";
                    try
                    {
                        SYSTEM_CODE = DETAIL[i]["DCSDCOT-D-TRANS-CODE"].Trim();
                        OPERATE_CODE = DETAIL[i]["DCSDCOT-D-TRANS-CODE"].Trim();
                        
                        /*
                        423401 = loan coop to current
                        423411 = loan coop to saving
                        430134 = payment coop from current
                        431134 = payment coop from saving
                        430114 = deposit from current to coop
                        430014 = deposit from ba to coop
                        431114 = deposit from saving to coop
                        421401 = withdraw from coop to current
                        421411 = withdraw from coop to saving
                        1014xx = cash withdraw from acc coop deposit
                        1034xx = cash withdraw from coop loan
                        */

                        if (SYSTEM_CODE.IndexOf("423401") >= 0)
                        {
                            SYSTEM_CODE = "01";
                            OPERATE_CODE = "002";

                        }
                        else if (SYSTEM_CODE.IndexOf("423411") >= 0)
                        {
                            SYSTEM_CODE = "01";
                            OPERATE_CODE = "002";

                        }else if (SYSTEM_CODE.IndexOf("431134") >= 0)
                        {
                            SYSTEM_CODE = "01";
                            OPERATE_CODE = "003";

                        }
                        else if (SYSTEM_CODE.IndexOf("430134") >= 0)
                        {
                            SYSTEM_CODE = "01";
                            OPERATE_CODE = "003";

                        }
                        else if (SYSTEM_CODE.IndexOf("430114") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "003";

                        }
                        else if (SYSTEM_CODE.IndexOf("430014") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "003";

                        }
                        else if (SYSTEM_CODE.IndexOf("431114") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "003";

                        }
                        else if (SYSTEM_CODE.IndexOf("421401") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "002";

                        }
                        else if (SYSTEM_CODE.IndexOf("421411") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "002";

                        }
                        else if (SYSTEM_CODE.IndexOf("1014") >= 0)
                        {
                            SYSTEM_CODE = "02";
                            OPERATE_CODE = "002";

                        }
                        else if (SYSTEM_CODE.IndexOf("1034") >= 0)
                        {
                            SYSTEM_CODE = "01";
                            OPERATE_CODE = "002";

                        }

                    }
                    catch { }
                    String saving_acc=DETAIL[i]["DCSDCOT-D-TO-AC-NUM"].Trim() ;

                    String member_no="",memb_name="",refcoopacc_no="";
                    this.getMemberInfo(saving_acc, SYSTEM_CODE, ref  member_no, ref  memb_name, ref  refcoopacc_no, loantype_code, depttype_code);

                    sql = "update  lncontmaster set contract_status=contract_status where member_no='" + member_no + "' ";
                    WebUtil.ExeSQL(sql);

                    sql = "update  dpdeptmaster set deptclose_status=deptclose_status where member_no='" + member_no + "'";
                    WebUtil.ExeSQL(sql);


                    sql = @" insert into ATMTMPTRANSDATA ( 
                                    COOP_ID , 	
                                    ATM_COOP_ID, 	
	                                MEMBER_NO , 	
	                                REF_ACCNO , 
	                                SAVING_ACC , 
	                                COOP_SAVING_ACC , 
	                                OPERATE_DATE , 
	                                SYSTEM_CODE , 
	                                OPERATE_CODE , 
	                                ITEM_AMT ,
	                                FREE_AMT ,
	                                DISCOUNT_AMT,
	                                CREDIT_AMT ,
	                                BALANCE_AMT ,
	                                BANK_CODE ,
	                                BRANCH_CODE ,
	                                CCS_OPERATE_DATE ,
	                                ATM_NO ,
	                                ATM_SEQNO ,
	                                TRANS_CODE,
                                    APPROVE_CODE,
                                    CARD_NO ,
	                                ITEM_STATUS,
	                                POST_STATUS ,
	                                POST_DATE,
	                                EXPORT_STATUS ,
	                                EXPORT_DATE ,
	                                RECONCILE_DATE ,
	                                MEMBER_NAME ,
	                                ATM_LOCATION,
	                                REF_DOCNO,
                                    FORMAT_CODE    
                                    )values(";
                             sql += "'" + /*COOP_ID*/state.SsCoopId + "' ," +
                                    "'" + /*ATM_COOP_ID*/DETAIL[i]["DCSDCOT-D-TERM-OWNER"].Trim() + "' ," +
                                    "'" + /*MEMBER_NO*/ member_no + "' ," + 
	                                "'" + /*REF_ACCNO*/ refcoopacc_no+ "' ," +
                                    "'" + /*SAVING_ACC*/ DETAIL[i]["DCSDCOT-D-TO-AC-NUM"].Trim() + "' ," +
                                    "'" + /*COOP_SAVING_ACC*/ DETAIL[i]["DCSDCOT-D-FROM-AC-NUM"].Trim() + "' ," +
                                    "" + /*OPERATE_DATE*/ "to_date('"+DETAIL[i]["DCSDCOT-D-TRANS-DATE"].Trim() + DETAIL[i]["DCSDCOT-D-TRANS-TIME"].Trim() + "','yyyyMMddHH24miss') "+" ," +
                                    "'" + /*SYSTEM_CODE*/ SYSTEM_CODE + "' ," +
                                    "'" + /*OPERATE_CODE*/ OPERATE_CODE + "' ," +
                                    "'" + /*ITEM_AMT*/ (Convert.ToDecimal(DETAIL[i]["DCSDCOT-D-TRANS-AMOUNT"].Trim())/100) + "' ," +
                                    "'" + /*FREE_AMT*/ (Convert.ToDecimal(DETAIL[i]["DCSDCOT-D-TRANS-FEE"].Trim())/100) + "' ," +
                                    "'" + /*DISCOUNT_AMT*/ (Convert.ToDecimal(DETAIL[i]["DCSDCOT-D-DISP-AMOUNT"].Trim()) /100)+ "' ," +
	                                "'" + /*CREDIT_AMT*/ "0.00"+ "' ," +
	                                "'" + /*BALANCE_AMT*/ "0.00"+ "' ," +
                                    "'" + /*BANK_CODE*/ "025"+ "' ," +
	                                "'" + /*BRANCH_CODE*/ "999"+ "' ," +
                                    "" + /*CCS_OPERATE_DATE*/ "to_date('" + DETAIL[i]["DCSDCOT-D-TRANS-DATE"].Trim() + DETAIL[i]["DCSDCOT-D-TRANS-TIME"].Trim() + "','yyyyMMddHH24miss') " + " ," +
                                    "'" + /*ATM_NO*/ DETAIL[i]["DCSDCOT-D-TERM-NUM"].Trim() + "' ," +
                                    "'" + /*ATM_SEQNO*/ DETAIL[i]["DCSDCOT-D-TERM-TXSEQ"].Trim() + "' ," +
                                    "'" + /*TRANS_CODE*/DETAIL[i]["DCSDCOT-D-TRANS-CODE"].Trim() + "' ," +
                                    "'" + /*APPROVE_CODE*/ DETAIL[i]["DCSDCOT-D-APPROVE-CODE"].Trim() + "' ," +
                                    "'" + /*CARD_NO*/ DETAIL[i]["DCSDCOT-D-CARD-NUM"].Trim() + "' ," +
	                                "'" + /*ITEM_STATUS*/ '0'+ "' ," +
	                                "'" + /*POST_STATUS*/ '0'+ "' ," +
	                                "" + /*POST_DATE*/ "NULL"+ " ," +
	                                "" + /*EXPORT_STATUS*/ "-9"+ " ," +
	                                "" + /*EXPORT_DATE*/ "NULL"+ " ," +
	                                "" + /*RECONCILE_DATE*/ "NULL"+ " ," +
	                                "'" + /*MEMBER_NAME*/ '-'+ "' ," +
                                    "'" + /*ATM_LOCATION*/ DETAIL[i]["DCSDCOT-D-TERM-LOCATION"].Trim() + "' ," +
                                    "'" + /*REF_DOCNO*/ ref_docno + "', "+
                                    "'" + /*FORMAT_CODE*/ FORMAT_CODE + "' )";
                    WebUtil.ExeSQL(sql);
                }
        }

        public void saveToTemp(String ref_docno)
        {
            Dictionary<string, string>[] HEADER = (Dictionary<string, string>[])this.data["HEADER"];
            Dictionary<string, string>[] DETAIL = (Dictionary<string, string>[])this.data["DETAIL"];
            Dictionary<string, string>[] FOOTER = (Dictionary<string, string>[])this.data["FOOTER"];

            if (HEADER.Length > 0)
            {
                if (DpFileFormatCode.SelectedValue == "ATM_BAY_IMP")
                {
                    this.saveATM_BAY_IMP(ref_docno,HEADER,DETAIL,FOOTER);
                }
              
            }
        }


    }
}
