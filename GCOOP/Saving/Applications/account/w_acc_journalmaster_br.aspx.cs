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


    public partial class w_acc_journalmaster_br : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_date1;
        private n_accountClient accService;//ประกาศตัวแปร WebService

        //JavaSctip PostBack
        protected String postJournal;
        protected String postSaveJournal;
        protected String postSaveFinish;
        protected String postNewClear;
        


      
        // JS-PostBack SaveJournal
        private void JspostSaveJournal()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                    Dw_list.SetFilter("posttoacc_flag = 1");
                    Dw_list.Filter();
                    n_accountClient accService = wcf.NAccount;

                    // การโยน ไฟล์ xml ไปให้ service
                    String postlist = Dw_list.Describe("Datawindow.Data.Xml");
                    String entry_id = state.SsUsername;
                    String wsPass = state.SsWsPass;
                    

                    //เรียกใช้ webservice
                    //int result = accService.SavePostToGl(state.SsWsPass, postlist, entry_id);
                    int result = wcf.NAccount.of_save_vcpost_to_gl(state.SsWsPass, postlist, entry_id);

                    HdIsFinished.Value = "true";

                    if (result != 1)
                    {
                        throw new Exception("ไม่สามารถผ่านรายการไปแยกประเภทได้");
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("ผ่านรายการไปแยกประเภทเรียบร้อยแล้ว");
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

        // JsPostBack ดึงข้อมูลใบ Voucher
        private void JspostJournal()
        {
            // ส่วนติดต่อ SERVICE
            try
            {
                
                DateTime journal_d = Dw_master.GetItemDateTime(1, "journal_d");
                DateTime to_jour_date = Dw_master.GetItemDateTime(1, "to_jour_date");
                String wsPass = state.SsWsPass;
                Int16 PostStatus = 0;   //ยังไม่ผ่านรายการ
                String as_vclist_xml = Dw_list.Describe("Datawindow.Data.Xml");
                //short result = accService.InitPostToGl(state.SsWsPass, journal_d, to_jour_date, PostStatus, ref as_vclist_xml, state.SsCoopId);
                short result = wcf.NAccount.of_init_vcpost_to_gl(state.SsWsPass, journal_d, to_jour_date, PostStatus, ref as_vclist_xml, state.SsCoopId);
                if (as_vclist_xml == "")
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่มีข้อมูลรายการ voucher ที่รอผ่านรายการ");
                    JspostNewClear();
                }
                else
                {
                    Dw_list.Reset();
                    //Dw_list.ImportString(as_vclist_xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(as_vclist_xml, Dw_list, tdw_date1,FileSaveAsType.Xml);
                }
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }

            catch (Exception ex) 
            { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            
            }
        }

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_list.Reset();
            Dw_master.InsertRow(0);
            Dw_master.SetItemDate(1, "to_jour_date", state.SsWorkDate);
            Dw_master.SetItemDate(1, "journal_d", state.SsWorkDate);
            tdw_date1.Eng2ThaiAllRow();
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postJournal = WebUtil.JsPostBack(this, "postJournal");
            postSaveJournal = WebUtil.JsPostBack(this, "postSaveJournal");
            postSaveFinish = WebUtil.JsPostBack(this, "postSaveFinish");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
      
            //การเรียกใช้ DwThDate

            tdw_date1 = new DwThDate(Dw_master, this);
            tdw_date1.Add("journal_d", "journal_d_tdate");
            tdw_date1.Add("to_jour_date", "to_jour_tdate");
        }

        // From Load
        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
           // th = new CultureInfo("th-TH");
            Dw_master.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
                //Dw_master.InsertRow(0);
              
            }
            else
            {
                //ทำเฉพาะตัว Datawindow ที่ตั้งเป็น false true true
                this.RestoreContextDw(Dw_master);
                this.RestoreContextDw(Dw_list);
            }
            
           

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postJournal")
            {
                JspostJournal();
            }
            else if (eventArg == "postNewClear") {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {
            JspostSaveJournal();
            JspostJournal();
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_master.RowCount > 1)
            {
                Dw_master.DeleteRow(Dw_master.RowCount);
            }
            Dw_master.SaveDataCache();
            Dw_list.SaveDataCache();
            tdw_date1.Eng2ThaiAllRow();
        }

        #endregion

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                for (int i = 1; i <= Dw_list.RowCount; i++)
                {
                    Dw_list.SetItemDecimal(i, "posttoacc_flag", 1);
                }
            }
            else 
            {
                for (int i = 1; i <= Dw_list.RowCount; i++)
                {
                    Dw_list.SetItemDecimal(i, "posttoacc_flag", 0);
                }
            }
        }

       


        










    }
}
