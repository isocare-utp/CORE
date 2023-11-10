using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;
using System.IO;
using System.Text;
using DataLibrary;
//using Saving.WcfFinance;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_recconcile : PageWebSheet, WebSheet
    {
        protected String jsUploadFile;

        public void InitJsPostBack()
        {
            jsUploadFile = WebUtil.JsPostBack(this, "jsUploadFile");
        }

        public void WebSheetLoadBegin()
        {

            if (!IsPostBack)
            {
            //    DwMain.Reset();
            //    DwMain.InsertRow(0);
            //    DwDetail.Visible = false;
            //    DwInsert.Visible = false;
            //    DwMain.SetItemDateTime(1, "start_date", state.SsWorkDate);
            //    DwMain.SetItemDateTime(1, "end_date", state.SsWorkDate);
            //    tDwMain.Eng2ThaiAllRow();
            //    tDwDetail.Eng2ThaiAllRow();
            //    tDwInsert.Eng2ThaiAllRow();
            }
            else
            {
                //this.RestoreContextDw(DwMain, tDwMain);
                //this.RestoreContextDw(DwDetail, tDwDetail);
                //this.RestoreContextDw(DwInsert, tDwInsert);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsUploadFile":
                    UploadFile();
                    break;
                
            }
        }

        public void SaveWebSheet()
      {
//            String ErrorMassage = String.Empty;
//            try
//            {
//                if (HdUpdate_Flag.Value == "true")
//                {
//                    string ref_slipno = string.Empty;
//                    if (HdUpdate_Flag2.Value == "true")
//                    {
//                        ref_slipno = HdRef_Slipno.Value.Substring(1, HdRef_Slipno.Value.Length - 1);
//                    }
//                    else
//                    {
//                        ref_slipno = "00000000000000";
//                    }
//                    String SqlString = "  DELETE FROM FINHRWELFARE WHERE REF_SLIPNO in (" + ref_slipno + @") AND (receipt_no is null or trim(receipt_no) ='') and
//                                          OPERATE_DATE between to_date('" + HdStart_Date.Value + "','ddmmyyyy') and to_date('" + HdEnd_Date.Value + "','ddmmyyyy') ";
//                    String SqlString2 = "  DELETE FROM FINHRTUITION WHERE REF_SLIPNO in (" + ref_slipno + @") AND (receipt_no is null or trim(receipt_no) ='') and
//                                          OPERATE_DATE between to_date('" + HdStart_Date.Value + "','ddmmyyyy') and to_date('" + HdEnd_Date.Value + "','ddmmyyyy') ";


//                    FinService = wcf.Finance;
//                    String XmlDetail = DwDetail.Describe("DataWindow.Data.XML");
//                    int result = 0;
//                    if (DwDetail.RowCount < 1)
//                    {
//                        try
//                        {
//                            WebUtil.QuerySdt(SqlString);
//                            WebUtil.QuerySdt(SqlString2);
//                            result = 1;
//                        }
//                        catch (Exception ex)
//                        {
//                            result = 0;
//                            ErrorMassage += ex.Message;
//                        }
//                    }
//                    else
//                    {
//                        result = FinService.HrSaveWelfare(state.SsWsPass, pbl, XmlDetail, SqlString, SqlString2, "d_welfare_tuition");
//                    }
//                    if (result != 1)
//                    {
//                        ErrorMassage += "[อัพเดทไม่สำเร็จ] ";
//                    }
//                    else
//                    {
//                        ErrorMassage += "[อัพเดทสำเร็จ] ";
//                    }
//                }
//                if (HdInsert_Flag.Value == "true")
//                {
//                    string ref_slipno = string.Empty;
//                    int rowcount = DwInsert.RowCount;
//                    for (int i = 1; i <= rowcount; i++)
//                    {
//                        if (i == 1) ref_slipno += "'" + DwInsert.GetItemString(i, "ref_slipno") + "'";
//                        ref_slipno += ", '" + DwInsert.GetItemString(i, "ref_slipno") + "'";
//                    }
//                    String SqlString = "  DELETE FROM FINHRWELFARE WHERE REF_SLIPNO in (" + ref_slipno + @") AND 
//                                           (receipt_no is null or trim(receipt_no) ='')";
//                    String SqlString2 = "  DELETE FROM FINHRTUITION WHERE REF_SLIPNO in (" + ref_slipno + @") AND 
//                                           (receipt_no is null or trim(receipt_no) ='')";
//                    FinService = wcf.Finance;
//                    String XmlInsert = DwInsert.Describe("DataWindow.Data.XML");
//                    int result = FinService.HrSaveWelfare(state.SsWsPass, pbl, XmlInsert, SqlString, SqlString2, "d_welfare_insert");
//                    if (result != 1)
//                    {
//                        ErrorMassage += "[บันทึกข้อมูลใหม่ไม่สำเร็จ] ";
//                    }
//                    else
//                    {
//                        ErrorMassage += "[บันทึกข้อมูลใหม่สำเร็จ] ";
//                    }
//                }
//                if (ErrorMassage.Trim() == "")
//                {
//                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
//                }
//                else
//                {
//                    LtServerMessage.Text = WebUtil.CompleteMessage(ErrorMassage);
//                }
//                HdUpdate_Flag.Value = "false";
//                HdInsert_Flag.Value = "false";
//                HdUpdate_Flag2.Value = "false";
//                DwDetail.Reset();
//                DwInsert.Reset();
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ErrorMassage + ex.Message);
            //}
        }

        public void WebSheetLoadEnd()
        {
            //DwInsert.SaveDataCache();
            //DwDetail.SaveDataCache();
            //DwMain.SaveDataCache();
        }

        public void UploadFile()
        {
            //LtServerMessage.Text = WebUtil.CompleteMessage("File uploaded successfully.");
            
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
                        filename = DateTime.Now.ToString("ddMMyyyy_hhmmss") + "_" + fiUpload.FileName;
                        this.fiUpload.SaveAs(Server.MapPath("atm_txt/" + filename));
                        LtServerMessage.Text = filename + " Uploaded.";
                        //LtServerMessage.Text = WebUtil.CompleteMessage("File uploaded successfully.");
                        PreviewData(Server.MapPath("atm_txt/" + filename), filename);
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
      
        public void PreviewData(string filename, string name_f)
        {

            try
            {

                FileInfo fi = new FileInfo(filename);
                if (fi.Exists)
                {

                    // String[] arr4 = new String[50];
                    string str = "", str1 = "";

                    str = File.ReadAllText(filename);
                    //LtServerMessage.Text = WebUtil.ErrorMessage(str);
                    str = str.Replace("\n", "Ø");
                    str1 = str.Replace("\"", "");
                    String[] spRow = str1.Split('Ø');
                    //LtServerMessage.Text = WebUtil.ErrorMessage(spRow[25]);
                    String RowErr = String.Empty;
                    int err = 0;
                    for (int i = 0; i < spRow.Length - 1; i++)
                    {
                        String COOP_ID = String.Empty,
                               MEMBER_NO = String.Empty,
                               TRANS_DATE =  String.Empty,
                               RECORD_TYPE = String.Empty,
                               ATMCARD_OWNER = String.Empty,
                               ATMCARD_ID = String.Empty,
                               TERM_OWNER = String.Empty,
                               TERM_NO = String.Empty,
                               TERM_LOCATION = String.Empty,
                               TERM_CITY = String.Empty,
                               TERM_STATE = String.Empty,
                               TERM_STSEQ = String.Empty,
                               TRANS_CODE = String.Empty,
                               COOP_BANK_ACC = String.Empty,
                               MEMBER_BANK_ACC = String.Empty,
                               ITEM_AMT = String.Empty,
                               DISP_AMT = String.Empty,
                               FEE = String.Empty,
                               RESPONSE_BY = String.Empty,
                               RESPONSE_CODE01 = String.Empty,
                               RESPONSE_CODE02 = String.Empty,
                               REVERSAL_CODE = String.Empty,
                               APPROVE_CODE = String.Empty,
                               CHK = String.Empty
                               ;
                        int RECORD_TYPE1 = 0;
                        Decimal ITEM_AMT1 = 0, DISP_AMT1 = 0, FEE1 = 0;
                        CHK = spRow[i].Substring(0,1);
                        if (CHK != "0" && CHK != "9")
                        {
                            COOP_ID = spRow[i].Substring(98, 6);
                            MEMBER_NO = spRow[i].Substring(104, 13);
                            TRANS_DATE = spRow[i].Substring(58, 14);

                            RECORD_TYPE = spRow[i].Substring(0, 1);
                            RECORD_TYPE1 = Convert.ToInt32(RECORD_TYPE);

                            ATMCARD_OWNER = spRow[i].Substring(1, 3);
                            ATMCARD_ID = spRow[i].Substring(4, 19);
                            TERM_OWNER = spRow[i].Substring(23, 3);
                            TERM_NO = spRow[i].Substring(26, 7);
                            TERM_LOCATION = spRow[i].Substring(33, 15);
                            TERM_CITY = spRow[i].Substring(48, 2);
                            TERM_STATE = spRow[i].Substring(50, 2);
                            TERM_STSEQ = spRow[i].Substring(52, 6);
                            TRANS_CODE = spRow[i].Substring(72, 6);
                            COOP_BANK_ACC = spRow[i].Substring(78, 10);
                            MEMBER_BANK_ACC = spRow[i].Substring(88, 10);

                            ITEM_AMT = spRow[i].Substring(117, 11);
                            ITEM_AMT1 = Convert.ToDecimal(ITEM_AMT);
                            ITEM_AMT1 = ITEM_AMT1/100;

                            DISP_AMT = spRow[i].Substring(128, 11);
                            DISP_AMT1 = Convert.ToDecimal(DISP_AMT);
                            DISP_AMT1 = DISP_AMT1/100;

                            FEE  = spRow[i].Substring(139, 9);
                            FEE1 = Convert.ToDecimal(FEE);
                            FEE1 = FEE1 / 100;

                            RESPONSE_BY = spRow[i].Substring(148, 1);
                            RESPONSE_CODE01 = spRow[i].Substring(149, 1);
                            RESPONSE_CODE02 = spRow[i].Substring(150, 2);
                            REVERSAL_CODE = spRow[i].Substring(152, 2);
                            APPROVE_CODE = spRow[i].Substring(154, 6);

                            try
                            {
                                String insert = @"INSERT INTO ATMBANKTRANS(COOP_ID, MEMBER_NO, TRANS_DATE, RECORD_TYPE
                                                                         , ATMCARD_OWNER, ATMCARD_ID, TERM_OWNER, TERM_NO 
                                                                         , TERM_LOCATION, TERM_CITY, TERM_STATE, TERM_STSEQ
                                                                         , TRANS_CODE, COOP_BANK_ACC, MEMBER_BANK_ACC, ITEM_AMT
                                                                         , DISP_AMT, FEE, RESPONSE_BY, RESPONSE_CODE01, RESPONSE_CODE02 
                                                                         , REVERSAL_CODE, APPROVE_CODE, FILE_NAME)
                                                                    VALUES('" + COOP_ID + "', '" + MEMBER_NO + "', to_date('" + TRANS_DATE + "','yyyymmddhh24miss'),'" + RECORD_TYPE1 + @"'
                                                                         , '" + ATMCARD_OWNER + "', '" + ATMCARD_ID + "', '" + TERM_OWNER + "', '" + TERM_NO + @"'
                                                                         , '" + TERM_LOCATION + "', '" + TERM_CITY + "', '" + TERM_STATE + "', '" + TERM_STSEQ +@"'
                                                                         , '" + TRANS_CODE + "', '" + COOP_BANK_ACC + "', '" + MEMBER_BANK_ACC + "', '" + ITEM_AMT1 + @"' 
                                                                         , '" + DISP_AMT1 + "', '" + FEE1 + "', '" + RESPONSE_BY + "', '" + RESPONSE_CODE01 + "', '" + RESPONSE_CODE02 + @"'
                                                                         , '" + REVERSAL_CODE + "', '" + APPROVE_CODE + "', '" + name_f + "')";
                                WebUtil.Query(insert);
                            }
                            catch (Exception ex)
                            {
                                err++;
                                if (err != 1)
                                    RowErr += ",";

                                RowErr = RowErr+(i+1);

                               
                            }
                        }
                       
                    }
                    //fi.Delete();
                    //// File.Delete(filename);
                    if (err > 0)
                        LtServerMessage.Text = WebUtil.WarningMessage("แถวที่"+ RowErr + "มีอยู่แล้ว");
                    else
                    LtServerMessage.Text = WebUtil.CompleteMessage("Update Success");
                    ////DwInsert.Visible = true;
                                            
                }
            }
            catch (Exception ex)
            {
                File.Delete(filename);
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}