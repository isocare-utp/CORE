using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_adduser : PageWebSheet, WebSheet
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
                DwUserName.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwUserName);

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    JsSearch();
                    break;
                case "jsDescription":
                    DescriptionCheck();
                    break;
            }
        }

        public bool loadLDAPprofile()
        {
            Sta ta = new Sta(state.SsConnectionString);
            String user_n = DwUserName.GetItemString(1, "user_name");
            String des = "";//DwUserName.GetItemString(1, "full_name");
            bool check = false;
            XmlConfigService xml = new XmlConfigService(WebUtil.GetGcoopPath());
            if (xml.UseLDAPFlag && user_n != null && user_n.Length>0)
            {
                try
                {
                    ArrayList userDetail = Sta.findUserDetailLDAP(ta,xml.ldap_url, xml.ldap_fusr, xml.ldap_fpwd, user_n);
                    //098107109111
                    //string passEncrypt = Encryption.UserPassword(password);
                    if (userDetail.Count > 0)
                    {
                        check = true;
                        des = "";
                        String department = "";
                        String email = "";
                        String card_person = "";
                        String employee_id = "";
                        for (int i = 0; i < userDetail.Count; i++)
                        {
                            String[] value = (String[])userDetail[i];
                            if (value[0] == "Last_Name" || value[0] == "First_Name")
                            {
                                des = value[1] + " " + des;
                            }
                            else if (value[0] == "department")
                            {
                                department = value[1];
                            }
                            else if (value[0] == "Email_Address")
                            {
                                email = value[1];
                            }
                            else if (value[0] == "card_person")
                            {
                                card_person = value[1];
                            }
                            else if (value[0] == "employee_id")
                            {
                                employee_id = value[1];
                            }
                        }
                        DwUserName.SetItemString(1, "full_name", des);
                        DwUserName.SetItemString(1, "description", department);
                        HdCkDes.Value = "1";
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบชื่อนี้ในระบบ LDAP " + xml.ldap_url + " กรุณากรอกชื่อใหม่");
                        HdCkDes.Value = "0";
                    }
                }
                catch { }
            }
            else
            {
                check = true;
            }
            ta.Close();
            return check;
        }

        public void DescriptionCheck()
        {
            HdCkDes.Value = "1";
            string old_user = "";
            string user_n = DwUserName.GetItemString(1, "user_name");
            string des = DwUserName.GetItemString(1, "full_name");
            string sqlck = "select user_name, full_name from amsecusers where full_name ='" + des + "' and user_name <>'" + user_n + "'";
            Sdt ckdes = WebUtil.QuerySdt(sqlck);
            if (ckdes.Next())
            {
                //หากชื่อซ้ำแต่ user name ไม่ซ้ำ ให้ทำการบันทึกได้
                old_user = ckdes.GetString("user_name");
                LtServerMessage.Text = WebUtil.WarningMessage("ชื่อ" + des + " นี้มีอยู่แล้วในระบบ USER ที่ใช้คือ " + old_user);
                HdCkDes.Value = "1";
            }

            this.loadLDAPprofile();
        }
        public void JsSearch()
        {
            string user_id = DwUserName.GetItemString(1, "user_name");

            try
            {
                DwUtil.RetrieveDataWindow(DwUserName, "ad_user.pbl", null, user_id);
                string test = DwUserName.GetItemString(1, "user_name");
                LtServerMessage.Text = WebUtil.WarningMessage("รหัสนี้มีอยุ่ในระบบแล้ว");
            }
            catch
            {
                DwUserName.InsertRow(0);
                DwUserName.SetItemString(1, "user_name", user_id);
                DwUserName.SetItemString(1, "coop_id", state.SsCoopId);
                DwUserName.SetItemString(1, "password", "1234");
                DwUserName.SetItemString(1, "coopbranch_id", "000");
                DwUserName.SetItemString(1, "tablefin_code", "XXX");
                DwUserName.SetItemDecimal(1, "user_type", 1);
                DwUserName.SetItemDecimal(1, "freez_flag", 0);
            }

            this.loadLDAPprofile();

        }

        public void SaveWebSheet()
        {
            string error = "";
            n_adminClient adminService = wcf.NAdmin;
            string user_name = DwUserName.GetItemString(1,"user_name");
            string full_name = DwUserName.GetItemString(1, "full_name");
            DwUserName.SetItemString(1, "coop_control", state.SsCoopControl);
            int result=2;
            try
            {
                result = adminService.of_existinguser(state.SsWsPass, user_name, full_name);
            }
            catch (Exception e)
            { LtServerMessage.Text = WebUtil.ErrorMessage(e); }


            if (result == 0 && HdCkDes.Value == "1")
            {
                String d_um_user_xml = DwUserName.Describe("Datawindow.data.XML");
                try
                {
                    int resultsave = adminService.of_savenewuser(state.SsWsPass, d_um_user_xml, ref error);
                    if (resultsave == 1)
                    {
                        if (xmlconfig.UseLDAPFlag)
                        {
                            Sta ta = new Sta(state.SsConnectionString);
                            Sta.findUserDetailLDAP(ta, xmlconfig.ldap_url, xmlconfig.ldap_fusr, xmlconfig.ldap_fpwd, user_name);
                            ta.Close();
                        }
                        LtServerMessage.Text = WebUtil.CompleteMessage("เพิ่มรหัสผู้ใช้งานใหม่สำเร็จ");
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกข้อมูลผู้ใช้ไม่สำเร็จ"+error); }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            { LtServerMessage.Text = WebUtil.WarningMessage("รหัสผู้ใช้งาน หรือ ชื่อผู้ใช้งานซ้ำ"); }



        }

        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(DwUserName, "apvlevel_id", "ad_user.pbl", null);
            DwUserName.SaveDataCache();
        }
    }
}