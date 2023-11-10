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

using DataLibrary;  // เพิ่มเข้ามา
using System.Data.OracleClient; // เพิ่มเข้ามา
using System.Globalization; // เพิ่มเข้ามา
using Sybase.DataWindow; // เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; // service


namespace Saving.Applications.account
{
    public partial class w_sheet_vc_voucher_cancel_post : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_main;
        private n_accountClient accService;//ประกาศตัวแปร WebService

        // JavaSctipt PostBack
        protected String postCancelJournal;
        protected String postSaveCancelJournal;
        protected String postNewClear;
        

        
        //Js-PostBack
        private void JspostCancelJournal()
        {
            // ส่วนติดต่อ SERVICE
            try
            {
                DateTime journal_date = Dw_master.GetItemDate(1, "journal_d");
                DateTime journal_date1 = Dw_master.GetItemDate(1, "to_jour_date");
                String wsPass = state.SsWsPass;
                Int16 PostStatus = 1; //ผ่านรายการไปแล้ว
                String as_vclist_xml = Dw_list.Describe("Datawindow.Data.Xml");
                //short result = accService.InitPostToGl(state.SsWsPass, journal_date, journal_date1, PostStatus, ref as_vclist_xml, state.SsCoopId);
                short result = wcf.NAccount.of_init_vcpost_to_gl(state.SsWsPass, journal_date, journal_date1, PostStatus, ref as_vclist_xml, state.SsCoopId);
                if (as_vclist_xml == "")
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายการ voucher ที่ต้องการยกเลิกผ่านรายการ");
                    JspostNewClear();
                }
                else
                {
                    Dw_list.Reset();
                    //Dw_list.ImportString(as_vclist_xml, FileSaveAsType.Xml);    tdw_main    
                    DwUtil.ImportData(as_vclist_xml, Dw_list, tdw_main,FileSaveAsType.Xml);
                }
            }
            catch (SoapException ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostSaveCancelJournal()
        {
            //ส่วนติดต่อ SERVICE
            try
            {

                Dw_list.SetFilter("posttoacc_flag = 0");
                Dw_list.Filter();

                if (Dw_list.RowCount > 0)
                {
                    n_accountClient accService = wcf.NAccount;

                    // การโยน ไฟล์ xml ไปให้ service
                    String postlist = Dw_list.Describe("Datawindow.Data.Xml");
                    String entry_id = state.SsUsername;
                    String wsPass = state.SsWsPass;
                    
                    DateTime journal_d = Dw_master.GetItemDateTime(1, "journal_d");
                    //เรียกใช้ webservice
                    //int result = accService.CancelPostToGl(wsPass, postlist, entry_id, state.SsCoopId, journal_d);
                    int result = wcf.NAccount.of_cancel_post_to_gl(wsPass, postlist, entry_id, state.SsCoopId, journal_d);

                    HdIsFinished.Value = "true";
                    if (result != 1)
                    {
                        throw new Exception("ไม่สามารถยกเลิกผ่านรายการไปบัญชีได้");
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกการผ่านรายการไปบัญชีเรียบร้อยแล้ว");

                    }
                }
                else {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรายการที่ต้องการยกเลิก");
                }
                
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        
        }

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_list.Reset();
            Dw_master.InsertRow(0);
            
            Dw_master.SetItemDate(1, "journal_d", state.SsWorkDate);
            Dw_master.SetItemDate(1, "to_jour_date", state.SsWorkDate);
            tdw_main.Eng2ThaiAllRow();
        }
            
            
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCancelJournal = WebUtil.JsPostBack(this, "postCancelJournal");
            postSaveCancelJournal = WebUtil.JsPostBack(this, "postSaveCancelJournal");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            

            //การเรียกใช้ DwThDate
            tdw_main = new DwThDate(Dw_master, this);
            tdw_main.Add("journal_d", "journal_d_tdate");
            tdw_main.Add("to_jour_date", "to_jour_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
    
            Dw_master.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_master);
                this.RestoreContextDw(Dw_list);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCancelJournal")
            {
                JspostCancelJournal();
            }
            else if (eventArg == "postNewClear") {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {
            JspostSaveCancelJournal();
            JspostCancelJournal();
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_master.RowCount > 1) 
            {
                Dw_master.DeleteRow(Dw_master.RowCount);
            }
            Dw_master.SaveDataCache();
            Dw_list.SaveDataCache();
            tdw_main.Eng2ThaiAllRow();
        }

        #endregion

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                for (int i = 1; i <= Dw_list.RowCount; i++)
                {
                    Dw_list.SetItemDecimal(i, "posttoacc_flag", 0);
                }
            }
            else 
            {
                for (int i = 1; i <= Dw_list.RowCount; i++)
                {
                    Dw_list.SetItemDecimal(i, "posttoacc_flag", 1);
                }
            }
        }
    }
}
