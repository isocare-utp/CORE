using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving
{
    public partial class ApplicationSelectionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigService xml = new XmlConfigService(WebUtil.GetGcoopPath());
            LbSiteNameThai.Text = xml.SavOfflineLabelThai;
            LbSiteNameEnglish.Text = xml.SavOfflineLabelEng;

            // ทำ server message เป็นค่าว่างก่อนเริ่มทำงาน
            LtServerMessage.Text = "";

            // ประกาศ web state
            WebState state = new WebState();
            if (string.IsNullOrEmpty(state.SsTokenId)) return;
            if (string.IsNullOrEmpty(state.SsConnectionString)) return;

            // ประกาศ transaction แบบ auto commit
            Sta ta = null;
            try
            {
                ta = new Sta(state.SsConnectionString);
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถเชื่อมต่อฐานข้อมูล");
                return;
            }

            // ประกาศตัวแปร สำหรับเช็คว่า user คลิกเลือกระบบรึยัง
            string selectApp = "";
            try
            {
                selectApp = Request["setApp"];
            }
            catch
            {
                selectApp = "";
            }

            // ถ้าพบว่ามีการเลือกระบบ ให้ทำการเช็คสิทธิ์การใช้ระบบก่อน หากมีสิทธิ์จะ redirect ไปหน้า Default.aspx (หน้าแรกของระบบ)
            if (!string.IsNullOrEmpty(selectApp))
            {
                string sqlSelectApp = @"
                    select
	                    a.coop_id,
	                    a.application,
                        a.description,
	                    a.workdate,
	                    u.user_name
                    from amappstatus a, amsecuseapps u
                    where 
	                    a.coop_id = u.coop_id and
	                    a.application = u.application and
	                    a.coop_id = {0} and
	                    a.application = {1} and
	                    u.user_name = {2} and
	                    a.used_flag = 1
                ";
                sqlSelectApp = WebUtil.SQLFormat(sqlSelectApp, state.SsCoopId, selectApp, state.SsUsername);
                try
                {
                    Sdt dtSelectApp = ta.Query(sqlSelectApp);

                    if (dtSelectApp.Rows.Count <= 0)
                    {
                        sqlSelectApp = @"
                            select
	                            a.coop_id,
	                            a.application,
                                a.description,
	                            a.workdate,
	                            u.user_name
                            from amappstatus a, amsecuseapps u,cmcoopmaster c
                            where 
	                            c.coop_id = u.coop_id and
	                            a.coop_id = c.coop_control and
	                            a.application = u.application and
	                            c.coop_id = {0} and
	                            a.application = {1} and
	                            u.user_name = {2} and
	                            a.used_flag = 1
                        ";
                        sqlSelectApp = WebUtil.SQLFormat(sqlSelectApp, state.SsCoopId, selectApp, state.SsUsername); 
                        dtSelectApp = ta.Query(sqlSelectApp);
                    }

                    if (dtSelectApp.Next())
                    {
                        string sqlUpdate = @"
                        update ssotoken set
                            application = {0},
                            token_lock = 0,
                            last_try = sysdate
                        where token_id = {1}";
                        sqlUpdate = WebUtil.SQLFormat(sqlUpdate, dtSelectApp.GetString("application"), state.SsTokenId);
                        int ii = ta.Exe(sqlUpdate);
                        if (ii == 1)
                        {
                            // ใส่ตัวแปร session เอง
                            Session["SsApplication"] = dtSelectApp.GetString("application");
                            Session["SsApplicationName"] = dtSelectApp.GetString("description");
                            ta.Close();
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            throw new Exception("ไม่สามารถ update การยืนยันตัวตนเข้าใช้ระบบได้");
                        }
                    }
                    else
                    {
                        throw new Exception("ไม่พบสิทธิ์การใช้งานระบบ " + selectApp);
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }

            // ถ้าไม่มีอะไรเกิดขึ้นให้สร้างเมนูเลือกระบบ
            try
            {
                MenuApplications m = new MenuApplications();
                Repeater1.DataSource = m.GetMenuApplication(ta, state.SsCoopId, state.SsUsername);
                Repeater1.DataBind();
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างเมนูเลือกระบบได้");
            }
            try
            {
                ta.Close();
            }
            catch { }
        }
    }
}