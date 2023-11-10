using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_editgroup : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsDescription;
        
        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsDescription = WebUtil.JsPostBack(this, "jsDescription");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwCri.InsertRow(0);
             //   DwMain.InsertRow(0);
               
            }
            else
            {
                this.RestoreContextDw(DwCri);
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsSearch")
            {
                JsSearch();
            }
            else if (eventArg == "jsDescription")
            {
                DescriptionCheck();
            }
        }
        public void DescriptionCheck()
        {
            HdCkDes.Value = "1";
            string user_n = DwCri.GetItemString(1,"user_name");
            string des = DwCri.GetItemString(1,"description");
            string sqlck = "select description from amsecusers where description ='"+des+"' and user_name <>'"+user_n+"'";
            Sdt ckdes = WebUtil.QuerySdt(sqlck);
            if (ckdes.Next())
            {
                LtServerMessage.Text = WebUtil.WarningMessage("คำอธิบายนี้มีอยู่แล้วในระบบ กรุณากรอกคำอธิบายใหม่");
                HdCkDes.Value = "0";
            }
        }
        public void JsSearch()
        {
            string user_id = DwCri.GetItemString(1, "user_name");

            try
            {
                DwUtil.RetrieveDataWindow(DwCri, "ad_group.pbl", null, user_id, state.SsCoopId);
                DwUtil.RetrieveDataWindow(DwMain, "ad_group.pbl", null, user_id, state.SsCoopId);
                string test = DwCri.GetItemString(1, "user_name");
            }
            catch
            {
                DwCri.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลกลุ่มผู้ใช้");
               // DwCri.SetItemString(1, "user_name", user_id);
            }

        }

        public void SaveWebSheet()
        {
            n_adminClient adminService = wcf.NAdmin;
            string user_name = DwCri.GetItemString(1, "user_name");
            string description = DwCri.GetItemString(1, "description");
            int result = 0;
            try
            {
                result = adminService.of_existinguser(state.SsWsPass, user_name, description);
            }
            catch (Exception e)
            { LtServerMessage.Text = WebUtil.ErrorMessage(e); }

            if (result == 1 && HdCkDes.Value != "0")
            {
                //int resultsave = 0;
                try
                {
                    DwUtil.UpdateDataWindow(DwCri, "ad_group.pbl", "amsecusers");
                    //resultsave = 1;
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                catch
                {
                    //resultsave = -1;
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
                }
               

            }
            else
            { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสกลุ่มผู้ใช้งานในระบบ หรือ รายละเอียดกลุ่มผู้ใช้งานซ้ำ"); }

        }

        public void WebSheetLoadEnd()
        {
            DwCri.SaveDataCache();
            DwMain.SaveDataCache();
        }
    }
}