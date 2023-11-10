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
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loanreq_register_update : PageWebSheet,WebSheet
    {
        private DwThDate tdwhead;

        protected String jsPostMember;
        protected String jsNewClear;
        protected String jsCancelRequest;

        public void InitJsPostBack()
        {
            jsNewClear = WebUtil.JsPostBack(this, "jsNewClear");
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsCancelRequest = WebUtil.JsPostBack(this, "jsCancelRequest");
            //ตั้งค่าให้วันที่
            tdwhead = new DwThDate(Dw_detail, this);
            tdwhead.Add("lnreqreceive_date", "lnreqreceive_tdate");
            tdwhead.Add("lnmeeting_date", "lnmeeting_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);

            if (IsPostBack)
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);
            }
            else
            {
                JsNewClear();
            }
            Dw_detail.SetItemString(1, "entry_id", state.SsUsername);
            //tdwhead.Eng2ThaiAllRow();
        }
        private void JsCancelRequest()
        {
            String cancelID = state.SsUsername;
            String coop_id = state.SsCoopControl;
            string cancle_date = state.SsWorkDate.ToShortDateString();
            
            try
            {
                Decimal loanrequestStatus = Dw_detail.GetItemDecimal(1, "reqregister_status");
                string loanreqdocno = Dw_detail.GetItemString(1, "reqregister_docno");

                //เปิดให้แก้ไขได้หลังจาก open 
                if ((loanrequestStatus == 8) || (loanrequestStatus == 81))
                {
                    string sql_up = "update lnreqloanregister set reqregister_status = -9, entry_id = '" + cancelID + "' where reqregister_docno = '" + loanreqdocno + "'";
                    WebUtil.ExeSQL(sql_up);
                    WebUtil.ExeSQL("commit");
                    LtServerMessage.Text = "ยกเลิกใบคำขอก้เรียบร้อยแล้ว";
                    
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("JsCancelRequest===>" + ex);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsNewClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsCancelRequest")
            {
                JsCancelRequest();
            }
            
        }

        public void SaveWebSheet()
        {
            String Date_reqreceive = "", Date_meeing = "", Date_entry = "", Num_Row = "";
            String loantype_code, remark, mb_no, mbgroup_code, mbentry_id, lnreqreceive_tdate = "", lnmeeting_tdate = "", compute_4 = "", Doc_Year = "";
            Decimal loanrequest_amt = 0, reqregister_status = 0, Last_Doc = 0;
            try
            {
                try
                {
                    mb_no = Dw_main.GetItemString(1, "member_no");
                }
                catch { mb_no = null; }
                try
                {
                    mbgroup_code = Dw_main.GetItemString(1, "membgroup_code").Trim();
                }
                catch { mbgroup_code = null; }
                try
                {
                    loantype_code = Dw_detail.GetItemString(1, "loantype_code");
                }
                catch { loantype_code = null; }
                try
                {
                    loanrequest_amt = Dw_detail.GetItemDecimal(1, "loanrequest_amt");
                }
                catch { loanrequest_amt = 0; }
                try
                {
                    remark = Dw_detail.GetItemString(1, "remark");
                }
                catch { remark = null; }
                try
                {
                    reqregister_status = Dw_detail.GetItemDecimal(1, "reqregister_status");
                }
                catch { reqregister_status = 0; }
                try
                {
                    mbentry_id = Dw_detail.GetItemString(1, "entry_id");
                }
                catch { mbentry_id = null; }
                try
                {
                    lnreqreceive_tdate = Dw_detail.GetItemString(1, "lnreqreceive_tdate");
                }
                catch { lnreqreceive_tdate = null; }
                try
                {
                    lnmeeting_tdate = Dw_detail.GetItemString(1, "lnmeeting_tdate");
                }
                catch { lnmeeting_tdate = null; }
                try
                {
                    compute_4 = Dw_main.GetItemString(1, "compute_4");
                }
                catch { compute_4 = null; }

                try
                {
                    //ตัดวันที่
                    Date_reqreceive = Dw_detail.GetItemString(1, "lnreqreceive_tdate");
                    Date_reqreceive = Date_reqreceive.Replace("/", "");

                    String str_date_lnreqreceive = Date_reqreceive.Substring(0, 2);
                    String str_month_lnreqreceive = Date_reqreceive.Substring(2, 2);
                    String str_year_lnreqreceive = Convert.ToString(Convert.ToInt32(Date_reqreceive.Substring(4, 4)) - 543);
                    Date_reqreceive = str_date_lnreqreceive + str_month_lnreqreceive + str_year_lnreqreceive;
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                try
                {
                    Date_meeing = Dw_detail.GetItemString(1, "lnmeeting_tdate");
                    Date_meeing = Date_meeing.Replace("/", "");

                    String str_date_lnmeeing = Date_meeing.Substring(0, 2);
                    String str_month_lnmeeing = Date_meeing.Substring(2, 2);
                    String str_year_lnmeeing = Convert.ToString(Convert.ToInt32(Date_meeing.Substring(4, 4)) - 543);
                    Date_meeing = str_date_lnmeeing + str_month_lnmeeing + str_year_lnmeeing;
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                try
                {
                    Date_entry = Dw_main.GetItemString(1, "compute_4");
                    Date_entry = Date_entry.Replace("/", "");

                    String str_date_entry = Date_entry.Substring(0, 2);
                    String str_month_entry = Date_entry.Substring(2, 2);
                    String str_year_entry = Convert.ToString(Convert.ToInt32(Date_entry.Substring(4, 4)) - 543);
                    Date_entry = str_date_entry + str_month_entry + str_year_entry;

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                
                //ดึงข้อมูลมานับเลข
                try
                {
                    Sta ta_se = new Sta(sqlca.ConnectionString);
                    String sql = @"   SELECT cmdocumentcontrol.Last_Documentno, cmdocumentcontrol.Document_Year from	cmdocumentcontrol where Document_Code = 'LONEBOOKNO'";
                    Sdt dt = ta_se.Query(sql);
                    if (dt.Next())
                    {
                        Last_Doc = dt.GetDecimal("Last_Documentno") + 1;
                        Doc_Year = dt.GetString("Document_Year");
                        Num_Row = Doc_Year + "-" + Last_Doc.ToString("00000");
                        LtServerMessage.Text = WebUtil.CompleteMessage(mbgroup_code);
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
                //กำหนดค่าวันที่ให้ text
                DateTime dtx = new DateTime();
                dtx = state.SsWorkDate;
                //เพิ่มลงเบส
                if (reqregister_status == 8)
                {
                    String Date_ln = dtx.ToString("ddMMyyyy", WebUtil.EN);
                    Sta ta = new Sta(sqlca.ConnectionString);
                    try
                    {
                        String sql = @"  INSERT INTO LNREQLOANREGISTER  
                               ( COOP_ID, REQREGISTER_DOCNO, MEMCOOP_ID, MEMBER_NO, LOANTYPE_CODE, LOANREQUEST_AMT, LNREQRECEIVE_DATE, LNMEETING_DATE, LNAPPROVE_DATE, REQREGISTER_STATUS, REMARK, ENTRY_ID, ENTRY_DATE, LOANAPPROVE_AMT, DOCUMENT_STATUS, LASTSEND_STATUS)  
                                VALUES ( '" + state.SsCoopControl + "', '" + Num_Row + "','" + mbgroup_code + "', '" + mb_no + "','" + loantype_code + "'," + loanrequest_amt + ",to_date('" + Date_reqreceive + "','ddmmyyyy'),to_date('" + Date_meeing + "','ddmmyyyy'),to_date('" + Date_ln + "','ddmmyyyy')," + reqregister_status + ",'" + remark + "','" + mbentry_id + "',to_date('" + Date_entry + "','ddmmyyyy'),0,0,1)";
                        ta.Exe(sql);
                        ta.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    Sta ta_udate = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                    try
                    {
                        sqlStr = @"  UPDATE cmdocumentcontrol
                                     SET  Last_Documentno = " + Last_Doc + " WHERE Document_Code = 'LONEBOOKNO'";
                        Sdt dt_update = ta.Query(sqlStr);
                        ta_udate.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                else if (reqregister_status == 1)
                {
                    String Date_ln = dtx.ToString("ddMMyyyy", WebUtil.EN);
                    Sta ta = new Sta(sqlca.ConnectionString);
                    try
                    {
                        String sql = @"  INSERT INTO LNREQLOANREGISTER  
                               ( COOP_ID, REQREGISTER_DOCNO, MEMCOOP_ID, MEMBER_NO, LOANTYPE_CODE, LOANREQUEST_AMT, LNREQRECEIVE_DATE, LNMEETING_DATE, LNAPPROVE_DATE, REQREGISTER_STATUS, REMARK, ENTRY_ID, ENTRY_DATE, LOANAPPROVE_AMT, DOCUMENT_STATUS, LASTSEND_STATUS)  
                                VALUES ( '" + state.SsCoopControl + "', '" + Num_Row + "','" + mbgroup_code + "', '" + mb_no + "','" + loantype_code + "'," + loanrequest_amt + ",to_date('" + Date_reqreceive + "','ddmmyyyy'),to_date('" + Date_meeing + "','ddmmyyyy'),to_date('" + Date_ln + "','ddmmyyyy')," + reqregister_status + ",'" + remark + "','" + mbentry_id + "',to_date('" + Date_entry + "','ddmmyyyy')," + loanrequest_amt + ",1,1)";
                        ta.Exe(sql);
                        ta.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    Sta ta_udate = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                    try
                    {
                        sqlStr = @"  UPDATE cmdocumentcontrol
                                     SET  Last_Documentno = " + Last_Doc + " WHERE Document_Code = 'LONEBOOKNO'";
                        Sdt dt_update = ta.Query(sqlStr);
                        ta_udate.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }

        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
        JsNewClear();
        }
        catch (Exception ex) { 
            LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            JsNewClear();
        }
   }

        public void WebSheetLoadEnd()
        {
            Dw_detail.SaveDataCache();
        }

        private int JsPostMember()
        {
            String Date_lnreqreceive = "";
            string ls_memno = Hfmember_no.Value.ToString();
            Dw_main.Retrieve(ls_memno);

            if (Dw_main.RowCount > 0)
            {
                Dw_detail.Reset();
                Dw_detail.InsertRow(0);
                try
                {
                    //แปลงวันที่จาก state.SsWorkDate
                    DateTime dt = new DateTime();
                    dt = state.SsWorkDate;
                    Date_lnreqreceive = dt.ToString("ddMMyyyy",WebUtil.TH);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
               
                Dw_detail.SetItemString(1, "lnreqreceive_tdate", Date_lnreqreceive);
                Dw_detail.SetItemString(1, "lnmeeting_tdate", Date_lnreqreceive);
                Dw_detail.SetItemString(1, "entry_id", state.SsUsername);
                
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่สมาชิก กรุณาตรวจสอบ");
                JsNewClear();
            }
            return 1;
        }

        private void JsNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);


            Dw_detail.Reset();
            Dw_detail.InsertRow(0);

            //tdwhead.Eng2ThaiAllRow();
        }
    }
}