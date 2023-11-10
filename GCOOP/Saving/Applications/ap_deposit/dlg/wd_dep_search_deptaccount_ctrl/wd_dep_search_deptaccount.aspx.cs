using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl
{
    public partial class wd_dep_search_deptaccount : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string JsSearchDeptAcc { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitdsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_member_no = "";
                try
                {
                    ls_member_no = Request["member_no"];
                }catch{ls_member_no="";}
                dsMain.DATA[0].MEMBER_NO = ls_member_no;
                dsMain.DATA[0].CHANGEPAGE = "vfalse";
                dsMain.DATA[0].DEPTCLOSE_STATUS = 0;
                dsMain.DD_Depttype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsSearchDeptAcc)
            {
                SearchData();                
            }
        }
        private void SearchData()
        {
            try
            {
                String ls_memno = "", ls_salaryid = "", ls_memname = "", ls_memsurname = "";
                String ls_depttypecode = "", ls_deptaccountno = "", ls_deptaccountname = "", ls_tel="",ls_carperson="";
                decimal ld_deptclose_status = 0;
                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                ls_memno = dsMain.DATA[0].MEMBER_NO.Trim();
                ls_memname = dsMain.DATA[0].MEMB_NAME.Trim();
                ls_memsurname = dsMain.DATA[0].MEMB_SURNAME.Trim();
                ls_salaryid = dsMain.DATA[0].SALARY_ID.Trim();
                ls_depttypecode = dsMain.DATA[0].DEPTTYPE_CODE.Trim();
                ls_deptaccountno = dsMain.DATA[0].DEPTACCOUNT_NO.Replace("-", "").Trim();
                ls_deptaccountname = dsMain.DATA[0].DEPTACCOUNT_NAME.Trim();
                ld_deptclose_status = dsMain.DATA[0].DEPTCLOSE_STATUS;
                ls_tel = dsMain.DATA[0].mem_telmobile.Replace("-", "").Trim();
                ls_carperson = dsMain.DATA[0].card_person.Replace("-", "").Trim();
                if (ld_deptclose_status == 0) { 
                    ls_sqlext += "and (dpdeptmaster.deptclose_status =0)";                 
                } else {
                    ls_sqlext += "and (dpdeptmaster.deptclose_status <> 0 )"; 
                }
                if (ls_memno.Length > 0)
                {
                    ls_sqlext += " and (  mbmembmaster.member_no like '%" + ls_memno + "%') ";
                }
                if (ls_salaryid.Length > 0)
                {
                    ls_sqlext += " and (  mbmembmaster.salary_id like '%" + ls_salaryid + "%') ";
                }
                if (ls_memname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memname + "%') ";
                }
                if (ls_memsurname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_memsurname + "%') ";
                }
                if (ls_depttypecode.Length > 0)
                {
                    ls_sqlext += " and ( dpdeptmaster.depttype_code = '" + ls_depttypecode + "') ";
                }
                if (ls_deptaccountno.Length > 0)
                {
                    ls_sqlext += " and ( dpdeptmaster.deptaccount_no like '%" + ls_deptaccountno + "%') ";
                }
                if (ls_deptaccountname.Length > 0)
                {
                    ls_sqlext += " and ( dpdeptmaster.deptaccount_name like '%" + ls_deptaccountname + "%') ";
                }
                if (ls_tel.Length > 0)
                {
                    ls_sqlext += " and ( MBMEMBMASTER.mem_telmobile like '%" + ls_tel + "%') ";
                }
                if (ls_carperson.Length > 0)
                {
                    ls_sqlext += " and ( MBMEMBMASTER.card_person like '%" + ls_carperson + "%') ";
                }
                String sqlMain = @"SELECT  DPDEPTMASTER.MEMBER_NO, 
                    MBUCFPRENAME.PRENAME_DESC||MBMEMBMASTER.MEMB_NAME||'  '||MBMEMBMASTER.MEMB_SURNAME as fullname, 
                    DPDEPTMASTER.PRNCBAL,
                    DPDEPTMASTER.DEPTACCOUNT_NO,
                    DPDEPTMASTER.DEPTACCOUNT_NAME,
                    (CASE WHEN DPDEPTMASTER.DEPTCLOSE_STATUS = 0 THEN 1 
		            WHEN DPDEPTMASTER.DEPTCLOSE_STATUS = 1 THEN 2
		            ELSE 3 END) SORT,
                    (CASE WHEN DPDEPTMASTER.DEPTCLOSE_STATUS = 0 THEN 'เปิดบัญชี' 
		                    WHEN DPDEPTMASTER.DEPTCLOSE_STATUS = 1 THEN  'ปิดบัญชี' 
		                    WHEN DPDEPTMASTER.DEPTCLOSE_STATUS = -9 THEN  'ยกเลิก' END)DEPTCLOSE_DESC
                    FROM MBMEMBMASTER INNER JOIN DPDEPTMASTER ON MBMEMBMASTER.MEMBER_NO =DPDEPTMASTER.MEMBER_NO
                    AND MBMEMBMASTER.COOP_ID= DPDEPTMASTER.COOP_ID
                    INNER JOIN MBUCFPRENAME ON MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE  
                    WHERE  
                    DPDEPTMASTER.COOP_ID = '" + state.SsCoopControl + "' " + ls_sqlext + "ORDER BY SORT,DPDEPTMASTER.DEPTTYPE_CODE,DPDEPTMASTER.DEPTACCOUNT_NO";
                sqlMain = WebUtil.SQLFormat(sqlMain);
                DataTable dt = WebUtil.Query(sqlMain);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                HdRow.Value = Convert.ToString(dt.Rows.Count);
                if (dt.Rows.Count < 1)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล"); return;
                }
                LtServerMessage.Text = "";
                if (dsMain.DATA[0].CHANGEPAGE != "true")
                {
                    GridView1.PageIndex = 0;
                    GridView1.DataBind();
                }
                dsMain.DD_Depttype();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        //แบ่งหน้า
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dsMain.DATA[0].CHANGEPAGE = "true";
            GridView1.PageIndex = e.NewPageIndex;
            SearchData();
        }
        public void WebDialogLoadEnd()
        {
            
        }
    }
}