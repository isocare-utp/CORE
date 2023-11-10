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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_edit_member_no : System.Web.UI.Page
    {
        DwTrans sqlca;
        WebState state;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            String cmd = "";
            try { cmd = Request["cmd"]; }
            catch { }

            if (cmd == "newData") { of_set_meminfo(); }
            else if (cmd == "saveData")
            {
                of_savenew( Request["mnomain"], Request["mnoco"]);
            }
            else { of_set_meminfo(); }
        }

        private void of_set_meminfo()
        {
            string ls_mainmemno, ls_comemno;
            string ls_maindocno, ls_codocno;


            // ดึง Suffix  สมาชิกสมทบ
            Sta ta1 = new Sta(sqlca.ConnectionString);
            String sql1 = @"select	memnomain_doccode, memnoref_doccode, 
                        comemno_suffix from	cmcoopconstant";
            Sdt dt1 = ta1.Query(sql1);
            ta1.Close();
            ls_maindocno = dt1.Rows[0]["memnomain_doccode"].ToString().Trim();
            ls_codocno = dt1.Rows[0]["memnoref_doccode"].ToString().Trim();


            // ดึงทะเบียน สมาชิกหลัก
            Sta ta2 = new Sta(sqlca.ConnectionString);
            String sql2 = @"select 	last_documentno
                        from	cmshrlondoccontrol 
                        where	document_code ='" + ls_maindocno + "'";
            Sdt dt2 = ta2.Query(sql2);
            ta2.Close();
            ls_mainmemno = dt2.Rows[0]["last_documentno"].ToString().Trim();

            // ดึงทะเบียน  สมาชิกสมทบ

            Sta ta3 = new Sta(sqlca.ConnectionString);
            String sql3 = @"select 	last_documentno
                        from	cmshrlondoccontrol 
                        where	document_code = '" + ls_codocno + "'";
            Sdt dt3 = ta3.Query(sql3);
            ta3.Close();
            ls_comemno = dt3.Rows[0]["last_documentno"].ToString().Trim();

            TbMainmemno.Text = WebUtil.StringFormat(ls_mainmemno,"000000");
            TbMainmem.Text = WebUtil.StringFormat(ls_mainmemno,"000000");
            TbComemno.Text = WebUtil.StringFormat(ls_comemno,"000000");
            TbComem.Text = WebUtil.StringFormat(ls_comemno,"000000");
        }

        private void of_savenew(String mnomain, String mnoco)
        {
            string ls_mnomain, ls_mnoco;
            int ll_memno = 0;
            ls_mnomain = mnomain;
            ls_mnoco = mnoco;

            Sta ta1 = new Sta(sqlca.ConnectionString);
            String sql1 = @"select	count(member_no) as mem_no
                            from	mbmembmaster
                            where	member_no = '" + ls_mnomain + "'";
            Sdt dt1 = ta1.Query(sql1);
            ta1.Close();
            ll_memno = Convert.ToInt32(dt1.Rows[0]["mem_no"]);
            if(ll_memno>0){
                LbMessage.Text = "มีเลขทะเบียน " + ls_mnomain + " แล้ว";
                of_set_meminfo();
                return;
            }

            Sta ta2 = new Sta(sqlca.ConnectionString);
            String sql2 = @"update cmshrlondoccontrol 
                            set		last_documentno = "+Convert.ToInt32(ls_mnomain)+@"
                            where	document_code = 'MBMEMBERNO'";
            Sdt dt2 = ta2.Query(sql2);
            ta2.Close();


            Sta ta3 = new Sta(sqlca.ConnectionString);
            String sql3 = @"select	count(member_no) as mem_no
                            from	mbmembmaster
                            where	member_no = '" + ls_mnoco + "'";
            Sdt dt3 = ta3.Query(sql1);
            ta3.Close();
            ll_memno = Convert.ToInt32(dt3.Rows[0]["mem_no"]);
            if (ll_memno > 0)
            {
                LbMessage.Text = "มีเลขทะเบียน " + ls_mnoco + " แล้ว";
                of_set_meminfo();
                return;
            }

            Sta ta4 = new Sta(sqlca.ConnectionString);
            String sql4 = @"update cmshrlondoccontrol 
                            set		last_documentno = " + Convert.ToInt32(ls_mnoco) + @"
                            where	document_code = 'MBMEMBERCONO'";
            Sdt dt4 = ta4.Query(sql4);
            ta4.Close();

            of_set_meminfo();
            LbMessage.Text = "Save!";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            sqlca.Disconnect();
        }
    }




}
