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
    public partial class w_sheet_sl_req_loanregister : PageWebSheet,WebSheet
    {

        private DwThDate tdwhead;

        protected String jsPostMember;
        protected String jsNewClear;
        protected String jsPostIns;
        protected String jsCancelRequest;
        protected String jsDeleteRequest;
        public void InitJsPostBack()
        {
            jsDeleteRequest = WebUtil.JsPostBack(this, "jsDeleteRequest");
            jsNewClear = WebUtil.JsPostBack(this, "jsNewClear");
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostIns = WebUtil.JsPostBack(this, "jsPostIns");
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
            Dw_list.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);

                if (!IsPostBack)
                {
                    JsNewClear();
                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                    this.RestoreContextDw(Dw_list);
                    this.RestoreContextDw(Dw_detail);
                }
                tdwhead.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsPostIns")
            {
                JscalinsureExpire();
            }
            else if (eventArg == "jsNewClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsCancelRequest")
            {
                JsCancelRequest();
            }
            else if (eventArg == "jsDeleteRequest")
            {
                JsDeleteRequest();
            }
        }

        public void SaveWebSheet()
        {
            String Date_lnreqreceive = "";
            String Date_lnmeeing = "";
            try
            {
                String record_no = Dw_detail.GetItemString(1, "reqregister_docno").Trim();
                String loantype_code, remark, loanentry_id, ems_docno;
                Decimal loanrequest_amt = 0, loanapprove_amt = 0, document_status = 0, reqregister_status = 0;
                String lnreqreceive_tdate, lnmeeting_tdate;

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
                    loanapprove_amt = Dw_detail.GetItemDecimal(1, "loanapprove_amt");
                }
                catch { loanapprove_amt = 0; }
                try
                {
                    document_status = Dw_detail.GetItemDecimal(1, "document_status");
                }
                catch { document_status = 0; }
                try
                {
                    //ตัดวันที่
                    Date_lnreqreceive = Dw_detail.GetItemString(1, "lnreqreceive_tdate");
                    Date_lnreqreceive = Date_lnreqreceive.Replace("/", "");

                    String str_date_lnreqreceive = Date_lnreqreceive.Substring(0, 2);
                    String str_month_lnreqreceive = Date_lnreqreceive.Substring(2, 2);
                    String str_year_lnreqreceive = Convert.ToString(Convert.ToInt32(Date_lnreqreceive.Substring(4, 4)) - 543);
                    Date_lnreqreceive = str_year_lnreqreceive + str_month_lnreqreceive + str_date_lnreqreceive;
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                try
                {
                    loanentry_id = Dw_detail.GetItemString(1, "loanentry_id");
                }
                catch { loanentry_id = null; }
                try
                {
                    ems_docno = Dw_detail.GetItemString(1, "ems_docno");
                }
                catch { ems_docno = null; }

                try
                {
                    Date_lnmeeing = Dw_detail.GetItemString(1, "lnmeeting_tdate");
                    Date_lnmeeing = Date_lnmeeing.Replace("/", "");

                    String str_date_lnmeeing = Date_lnmeeing.Substring(0, 2);
                    String str_month_lnmeeing = Date_lnmeeing.Substring(2, 2);
                    String str_year_lnmeeing = Convert.ToString(Convert.ToInt32(Date_lnmeeing.Substring(4, 4)) - 543);
                    Date_lnmeeing = str_year_lnmeeing + str_month_lnmeeing + str_date_lnmeeing;
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                if (reqregister_status == 8)
                {
                    Sta ta = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                    try
                    {
                        sqlStr = @"  UPDATE LNREQLOANREGISTER
                                     SET LOANTYPE_CODE = '" + loantype_code + "', LOANREQUEST_AMT = " + loanrequest_amt + ", LNREQRECEIVE_DATE = to_date('" + Date_lnreqreceive + "' ,'yyyymmdd'), LNMEETING_DATE = to_date('" + Date_lnmeeing + "' ,'yyyymmdd'), REMARK = '" + remark + "', LOANAPPROVE_AMT = " + loanapprove_amt + ", DOCUMENT_STATUS = " + document_status + ", loanentry_id = '" + loanentry_id + "' , ems_docno = '" + ems_docno + "' WHERE REQREGISTER_DOCNO = '" + record_no + "'";
                        Sdt dt_update = ta.Query(sqlStr);

                        ta.Close();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการแก้ไขข้อมูลเสร็จเรียบร้อยแล้ว");
                        JsNewClear();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                }
                else if (reqregister_status == 1)
                {
                    Sta ta = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                    try
                    {
                        sqlStr = @"  UPDATE LNREQLOANREGISTER
                                     SET LOANTYPE_CODE = '" + loantype_code + "', LOANREQUEST_AMT = " + loanrequest_amt + ", LNREQRECEIVE_DATE = to_date('" + Date_lnreqreceive + "' ,'yyyymmdd'), LNMEETING_DATE = to_date('" + Date_lnmeeing + "' ,'yyyymmdd'), REQREGISTER_STATUS = " + reqregister_status + ", REMARK = '" + remark + "', LOANAPPROVE_AMT = " + loanapprove_amt + ", DOCUMENT_STATUS = " + document_status + ", loanentry_id = '" + loanentry_id + "' , ems_docno = '" + ems_docno + "' WHERE REQREGISTER_DOCNO = '" + record_no + "'";
                        Sdt dt_update = ta.Query(sqlStr);

                        ta.Close();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการแก้ไขข้อมูลเสร็จเรียบร้อยแล้ว");
                        JsNewClear();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                else {
                    Sta ta = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                    try
                    {
                        sqlStr = @"  UPDATE LNREQLOANREGISTER
                                     SET LOANTYPE_CODE = '" + loantype_code + "', LOANREQUEST_AMT = " + loanrequest_amt + ", LNREQRECEIVE_DATE = to_date('" + Date_lnreqreceive + "' ,'yyyymmdd'), LNMEETING_DATE = to_date('" + Date_lnmeeing + "' ,'yyyymmdd'), REQREGISTER_STATUS = " + reqregister_status + ", REMARK = '" + remark + "', LOANAPPROVE_AMT = " + loanapprove_amt + ", DOCUMENT_STATUS = " + document_status + ", loanentry_id = '" + loanentry_id + "' , ems_docno = '" + ems_docno + "' WHERE REQREGISTER_DOCNO = '" + record_no + "'";
                        Sdt dt_update = ta.Query(sqlStr);

                        ta.Close();
                        
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการแก้ไขข้อมูลเสร็จเรียบร้อยแล้ว");
                        JsNewClear();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                
                
                }
            }
            catch (Exception ex) { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                JsNewClear();
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_list.SaveDataCache();
            Dw_detail.SaveDataCache();
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
                if (loanrequestStatus == 8 )
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
        private void JsDeleteRequest()
        {
            String member_no = "";
            String reqregister_docno = "";
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                member_no = Dw_main.GetItemString(1, "member_no").Trim();
                reqregister_docno = Dw_detail.GetItemString(1, "reqregister_docno").Trim();

                String sql = @"Delete from LNREQLOANREGISTER where member_no = '" + member_no +@"' 
                              and reqregister_docno = '"+reqregister_docno+"'";
                try
                {
                    ta.Exe(sql);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเรียบร้อยแล้ว");
                    JsPostMember();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลรายการ " + reqregister_docno + " ได้");
                }
            }

            catch (Exception ex)
            {
                String err = ex.ToString();

            }
            ta.Close();

            //Retrieve ...
            JsPostMember();
        }
        private int JsPostMember()
        {
            string ls_memno = Hfmember_no.Value.ToString();
            string membno = WebUtil.MemberNoFormat(ls_memno);
            Dw_main.Retrieve(membno);
          
            if (Dw_main.RowCount > 0)
            {
                Dw_list.Retrieve(membno);
                Dw_detail.Reset();
                Dw_detail.InsertRow(0);
                tdwhead.Eng2ThaiAllRow();
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่สมาชิก กรุณาตรวจสอบ");
                JsNewClear();
            }
            return 1;
        }

        public int JscalinsureExpire()
        {
            string ls_row = HfSlipNo.Value.ToString();
            string reg_docno = Dw_list.GetItemString(Convert.ToInt32(ls_row), "reqregister_docno");
            if (reg_docno != null)
            {
                Dw_detail.Retrieve(reg_docno);
                tdwhead.Eng2ThaiAllRow();

                Decimal reqregister_status = 0, document_status = 0;
                try
                {
                    reqregister_status = Dw_detail.GetItemDecimal(1, "reqregister_status");
                }
                catch { reqregister_status = 0; }
                try
                {
                    document_status = Dw_detail.GetItemDecimal(1, "document_status");
                }
                catch { document_status = 0; }

                if (reqregister_status == 1 || reqregister_status == -9 )
                {

                        Dw_detail.Modify("lnreqreceive_tdate.protect = 1");
                        Dw_detail.Modify("lnmeeting_tdate.protect = 1");
                        Dw_detail.Modify("loantype_code.protect = 1");
                        Dw_detail.Modify("loantype_code_1.protect = 1");
                        Dw_detail.Modify("loanrequest_amt.protect = 1");
                        Dw_detail.Modify("reqregister_status.protect = 1");
                        Dw_detail.Modify("loanapprove_amt.protect = 1");
                        if (document_status == 1)
                        {
                            Dw_detail.Modify("document_status.protect = 1");
                        }
                    }
                    else if (reqregister_status == 8){
                        Dw_detail.Modify("lnreqreceive_tdate.protect = 0");
                        Dw_detail.Modify("loanrequest_amt.protect = 0");
                        if (document_status == 1)
                        {
                            Dw_detail.Modify("document_status.protect = 0");
                        }
                    }
                    else{
                        JsNewClear();
                    }
            }
            else {
                JsNewClear();
            }
            return 1;
        }

        private void JsNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);

            Dw_list.Reset();

            Dw_detail.Reset();
            Dw_detail.InsertRow(0);

            tdwhead.Eng2ThaiAllRow();
        }
    }
}