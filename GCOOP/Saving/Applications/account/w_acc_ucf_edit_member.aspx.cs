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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา

namespace Saving.Applications.account
{
    public partial class w_acc_ucf_edit_member : PageWebSheet, WebSheet
    {
        protected String jsPostGetlist;
        protected String jsProcessMember;
        string pbl = "cm_constant_config.pbl";
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        #region WebSheet Members
        public void InitJsPostBack()
        {
            jsPostGetlist = WebUtil.JsPostBack(this, "jsPostGetlist");
            jsProcessMember = WebUtil.JsPostBack(this, "jsProcessMember");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Dwmain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostGetlist":
                    GetList();
                    break;

                case  "jsProcessMember":
                    ProcessMember();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DwUtil.UpdateDataWindow(Dwlist, pbl, "ACC_TRILEBALANCE");
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(Dwlist, "cnt_code", pbl, null);
            }
            catch { }
            Dwmain.SaveDataCache();
            Dwlist.SaveDataCache();
        }
        #endregion

        #region Function
        private void GetList()
        {
            string year = (Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543).ToString();
            string period = Dwmain.GetItemString(1, "period");
            DwUtil.RetrieveDataWindow(Dwlist, pbl, null, year, period, state.SsCoopControl);

        }
        #endregion

        private void ProcessMember()
        {
            try
            {
                int year = Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543;
                int period = Convert.ToInt32(Dwmain.GetItemString(1, "period"));
                int result = wcf.NAccount.of_process_member_balance(state.SsWsPass, (short)year, (short)period, state.SsCoopControl);

                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลจำนวนสมาชิกเสร็จเรียบร้อย");
                    DwUtil.RetrieveDataWindow(Dwlist, pbl, null, year, period, state.SsCoopControl);

                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถประมวลผลจำนวนสมาชิกได้");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}