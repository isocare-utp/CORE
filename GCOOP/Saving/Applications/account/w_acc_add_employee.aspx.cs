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

using System.Globalization;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAccount;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;


namespace Saving.Applications.account
{

    public partial class w_acc_add_employee : PageWebSheet, WebSheet
    {

        private n_accountClient accService;
        //==========================
        protected String postNewClear;
        protected String postEmployeeShow;
        private CultureInfo th;
        private DwThDate tdw_wizard;
        protected String postMember;


        //  public int status;

        //========================================

        private void JspostEmployeeShow()
        {
            String mem_no = "";
            mem_no = HdMemNo.Value.Trim();
            // Hd_accid_master.Value = Acc_Id;
            Dw_master.ClearDataCache();
            Dw_master.Reset();
            Dw_master.SetTransaction(sqlca);
            Dw_master.Retrieve(state.SsCoopId, mem_no);
            try
            {
                DateTime work_date = Dw_master.GetItemDateTime(1, "work_date");
                Dw_master.SetItemString(1, "work_tdate", work_date.ToString("ddMMyyyy", new CultureInfo("th-TH")));
            }
            catch{}
            try{

                DateTime resign_date = Dw_master.GetItemDateTime(1, "resign_date");
                Dw_master.SetItemString(1, "resign_tdate", resign_date.ToString("ddMMyyyy", new CultureInfo("th-TH")));
            }
            catch { 
            }
            SetDwMasterEnable(1);

            //Hd_checkclear.Value = "true";
            Panel2.Visible = true;
        }
        public void SetDwMasterEnable(int protect)
        {
            try
            {
                if (protect == 1)
                {
                    Dw_master.Enabled = false;
                }
                else
                {
                    Dw_master.Enabled = true;
                }
                int RowAll = int.Parse(Dw_master.Describe("Datawindow.Column.Count"));
                for (int li_index = 1; li_index <= RowAll; li_index++)
                {
                    Dw_master.Modify("#" + li_index.ToString() + ".protect= " + protect.ToString());
                }
            }
            catch (Exception)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private void JspostAddNew()
        {

            Dw_master.Reset();
            Dw_master.InsertRow(0);
            Dw_master.SetItemString(Dw_master.RowCount, "coop_id", state.SsCoopId);
            Dw_master.SetItemDecimal(Dw_master.RowCount, "resign_status", 0);
            Dw_master.SetItemString(Dw_master.RowCount, "prename_code", "01");
            Dw_master.SetItemDate(1, "work_date", state.SsWorkDate);
            Dw_master.SetItemString(1, "work_tdate", state.SsWorkDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
            Dw_master.Enabled = true;
            Dw_master.SaveDataCache();
            Panel2.Visible = false;
            Hd_aistatus.Value = "1";
        }

        private void JspostDeleteAccountId()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;
                String account_id;
                account_id = Hd_accid_master.Value;
                String wsPass = state.SsWsPass;
                //เรียกใช้ webservice
                //int result = accService.DeleteAccountId(wsPass, account_id, state.SsCoopId);
                int result = wcf.NAccount.of_delete_accountid(wsPass, account_id, state.SsCoopId);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลรหัสบัญชีเรียบร้อยแล้ว");
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

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_master.InsertRow(0);
            SetDwMasterEnable(1);
            Dw_master.SaveDataCache();
            Panel2.Visible = false;
            //status = 1; 
            Hd_aistatus.Value = "1";
        }
        private void JspostMember()
        {
            String mb_name = "";
            String mb_surname = "";
            String pre_code = "";
            String memberNo = WebUtil.MemberNoFormat(Dw_master.GetItemString(1, "member_no"));
            String sql_chkmemno = @"SELECT member_no, memb_name, memb_surname, prename_code FROM mbmembmaster WHERE member_no = '" + memberNo + "'";
            Sdt chkmemno = WebUtil.QuerySdt(sql_chkmemno);
            try 
            {
                if (chkmemno.Next())
                {
                    mb_name = chkmemno.GetString("memb_name");
                    mb_surname = chkmemno.GetString("memb_surname");
                    pre_code = chkmemno.GetString("prename_code");
                    Dw_master.SetItemString(1, "member_no", memberNo);
                    Dw_master.SetItemString(1, "memb_name", mb_name);
                    Dw_master.SetItemString(1, "memb_surname", mb_surname);
                    Dw_master.SetItemString(1, "prename_code", pre_code);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขที่สมาชิก");
                }
            }
            catch
            {
                
            }

        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            //postNoShowTreeviewAll = WebUtil.JsPostBack(this, "postNoShowTreeviewAll");
            //postShowTreeviewAll = WebUtil.JsPostBack(this, "postShowTreeviewAll");
            //postDeleteAccountId = WebUtil.JsPostBack(this, "postDeleteAccountId");
            postEmployeeShow = WebUtil.JsPostBack(this, "postEmployeeShow");
            postMember = WebUtil.JsPostBack(this, "postMember");
            tdw_wizard = new DwThDate(Dw_master, this);
            tdw_wizard.Add("work_date", "work_tdate");
            tdw_wizard.Add("resign_date", "resign_tdate");

            WebUtil.RetrieveDDDW(Dw_master, "prename_code", "acc_contuse_profit.pbl", null);
        }


        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            th = new CultureInfo("th-TH");
            accService = wcf.NAccount;//ประกาศ new
            Dw_master.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_master.InsertRow(0);
                dw_detail.Retrieve();
                SetDwMasterEnable(1);
                //PopulateRootLevel();
                Panel2.Visible = false;
                tdw_wizard.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_master);
            }
            if (Dw_master.RowCount > 1)
            {
                Dw_master.DeleteRow(Dw_master.RowCount);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }

            else if (eventArg == "postEmployeeShow")
            {
                JspostEmployeeShow();
            }
            else if (eventArg == "postMember")
            {
                JspostMember();
            }

        }

        public void SaveWebSheet()
        {
            try
            {
                n_accountClient accService = wcf.NAccount;

                // Int16  account_status = Convert.ToInt16(Hd_aistatus.Value);
                Int16 account_status = Convert.ToInt16(Hd_aistatus.Value);
                String xmlDw_main = Dw_master.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                //เรียกใช้ webservice
                int result = accService.of_add_newemployee(wsPass, xmlDw_main, account_status);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเจ้าหน้าที่เรียบร้อยแล้ว");
                dw_detail.Retrieve();
                Dw_master.Reset();
                Dw_master.InsertRow(0);
                SetDwMasterEnable(1);
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



        public void WebSheetLoadEnd()
        {
            Dw_master.SaveDataCache();
            dw_detail.SaveDataCache();
        }

       

     
        #endregion
    


        protected void B_edit_Click(object sender, EventArgs e)
        {
            SetDwMasterEnable(0);
            Hd_aistatus.Value = "2";
            //String  Acc_Id = TreeView1.SelectedNode.Value.Trim();
            String memb_no = Dw_master.GetItemString(1, "member_no");
            Dw_master.Reset();
            Dw_master.Retrieve(state.SsCoopId, memb_no);
            try
            {
                DateTime work_date = Dw_master.GetItemDateTime(1, "work_date");
                Dw_master.SetItemString(1, "work_tdate", work_date.ToString("ddMMyyyy", new CultureInfo("th-TH")));
            }
            catch { }
            try
            {

                DateTime resign_date = Dw_master.GetItemDateTime(1, "resign_date");
                Dw_master.SetItemString(1, "resign_tdate", resign_date.ToString("ddMMyyyy", new CultureInfo("th-TH")));
            }
            catch
            {
            }
        }

      
        protected void B_add_Click(object sender, EventArgs e)
        {

            // ถ้ากเพิ่มข้อมูล ให้ Get ค่าก่อนหน้านั้นมา

            //status = 1;
            JspostAddNew();
        }
    }
}
