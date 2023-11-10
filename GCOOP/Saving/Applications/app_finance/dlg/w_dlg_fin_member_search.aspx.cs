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

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_fin_member_search : PageWebDialog, WebDialog
    {
        private String sqlStr;
        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            try
            {

                this.ConnectSQLCA();
                sqlStr = DwList.GetSqlSelect();
                DwMain.SetTransaction(sqlca);
                DwList.SetTransaction(sqlca);
                if (!IsPostBack)
                {
                    DwMain.InsertRow(0);
                    //DwList.Retrieve(state.SsCoopControl);
                }
                else
                {
                    DwMain.RestoreContext();
                    //DwList.RestoreDataCache();
                    DwList.RestoreContext();
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            this.DisConnectSQLCA();
        }

        #endregion

        protected void B_Search_Click(object sender, EventArgs e)
        {
            try
            {

                String memno, memname, memsurname, memgroup, temp, sqlext, salary;
                int rc = 0;
                try
                {
                    memno = (DwMain.GetItemString(1, "member_no")).Trim();
                }
                catch { memno = ""; }
                try
                {
                    salary = (DwMain.GetItemString(1, "salary_id")).Trim();
                }
                catch { salary = ""; }
                try
                {
                    memname = (DwMain.GetItemString(1, "member_name")).Trim();
                }
                catch { memname = ""; }
                try
                {
                    memsurname = (DwMain.GetItemString(1, "member_surname")).Trim();
                }
                catch { memsurname = ""; }
                try
                {
                    memgroup = (DwMain.GetItemString(1, "member_group_no")).Trim();
                }
                catch { memgroup = ""; }
                sqlext = "";

                if (memno.Length > 0)
                {
                   // sqlext = " and ( mbmembmaster.member_no like '" + memno + "%') ";
                    
                        sqlext = " and ( mbmembmaster.member_no like '%" + memno + "%') ";
                      
                }

                if (salary.Length > 0)
                {
                    // sqlext = " and ( mbmembmaster.member_no like '" + memno + "%') ";

                    sqlext = " and ( mbmembmaster.salary_id like '%" + salary + "%') ";

                }

                if (memname.Length > 0)
                {
                    //sqlext += " and ( mbmembmaster.memb_name like '" + memname + "') ";
                    sqlext += " and ( mbmembmaster.memb_name like '" + memname + "%') ";
                }

                if (memsurname.Length > 0)
                {
                   // sqlext += " and ( mbmembmaster.memb_surname like '" + memsurname + "') ";
                    sqlext += " and ( mbmembmaster.memb_surname like '" + memsurname + "%') ";
                }

                if (memgroup.Length > 0)
                {
                    //sqlext += " and ( mbmembmaster.membgroup_code = '" + memgroup + "') ";
                    sqlext += " and ( mbmembmaster.membgroup_code like '%" + memgroup + "') ";
                }

                sqlStr  = @"   SELECT MBMEMBMASTER.MEMBER_NO,   
                                MBUCFPRENAME.PRENAME_DESC,   
                                 MBMEMBMASTER.MEMB_NAME,   
                                 MBMEMBMASTER.MEMB_SURNAME,   
                                 MBMEMBMASTER.MEMBGROUP_CODE,   
                                 MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                                 MBMEMBMASTER.COOP_ID  
                            FROM MBMEMBMASTER,   
                                 MBUCFMEMBGROUP,   
                                 MBUCFPRENAME  
                           WHERE ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
                                 ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE )     
                                and MBMEMBMASTER.COOP_ID = '" +state.SsCoopControl.Trim()+ "' " ;

                temp = sqlStr + sqlext;
                DwList.SetSqlSelect(temp);
                rc = DwList.Retrieve();
                if (rc < 1)
                {
                    LtServerMessage.Text = "ไม่พบข้อมูลที่ค้นหา";
                }
                else { LtServerMessage.Text = ""; }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}
