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
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เรียกใช้ service

namespace Saving.Applications.account
{
    public partial class w_acc_set_salary_employee : PageWebSheet, WebSheet
    {
        protected String initJavaScript;
        protected String RetrieveList;
        protected String jsPostDeleteRow;
        protected String jsPostGetAllEmp;

        string pbl = "acc_contuse_profit.pbl";

        private n_accountClient accService;//ประกาศตัวแปร WebService

        #region WebSheet Members
        public void InitJsPostBack()
        {
            initJavaScript = WebUtil.JsPostBack(this, "initJavaScript");
            RetrieveList = WebUtil.JsPostBack(this, "RetrieveList");
            jsPostDeleteRow = WebUtil.JsPostBack(this, "jsPostDeleteRow");
            jsPostGetAllEmp = WebUtil.JsPostBack(this, "jsPostGetAllEmp");

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwList.SetTransaction(sqlca);
            accService = wcf.NAccount;//ประกาศ new

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "RetrieveList":
                    retrieve();
                    break;
                case "jsPostDeleteRow":
                    DeleteRow();
                    break;

                case "jsPostGetAllEmp":
                    GetAllEmployee();
                    break;

            }
        }

        public void SaveWebSheet()
        {
            #region Service
            String xmlList = "";
            Decimal salary_year = DwMain.GetItemDecimal(1, "salary_year") - 543;

            try
            {
                short year = short.Parse(salary_year.ToString());
                xmlList = DwList.Describe("DataWindow.Data.XML");
                int result = wcf.NAccount.of_set_employee_salary(state.SsWsPass, xmlList, state.SsCoopId, year);
                if (result == 1)
                {
                    DwList.Reset();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                    retrieve();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            #endregion
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }
        #endregion

        #region Function


        private void retrieve()
        {
            Decimal salary_year = DwMain.GetItemDecimal(1, "salary_year") - 543;
            try
            {
                DwUtil.RetrieveDataWindow(DwList, pbl, null, salary_year, state.SsCoopId);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void DeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hdrow.Value);
            Decimal salary_year = DwMain.GetItemDecimal(1, "salary_year") - 543;
            String member_no = DwList.GetItemString(RowDetail, "member_no");
            String coop_id = state.SsCoopId;
            try
            {
                string sqldelete = @"DELETE FROM accemsalary WHERE coop_id = '" + coop_id + "' and member_no = '" + member_no + "' and account_year = '" + salary_year + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                DwList.DeleteRow(RowDetail);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch
            {
                try
                {
                    //DwList.GetItemDecimal(RowDetail, "budget_id");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    //DwList.DeleteRow(RowDetail);
                }
            }
        }

      
        private void GetAllEmployee()
        {
            Decimal salary_year = DwMain.GetItemDecimal(1, "salary_year");
            String sqlselect_employee = @"SELECT member_no, prename_code FROM accemployee WHERE resign_status = 0 order by member_no";
            Sdt accEmployee = WebUtil.QuerySdt(sqlselect_employee);
            int rowcount = DwList.RowCount;

            while (accEmployee.Next())
            {
                String mem_name = "";
                String member_no = accEmployee.GetString("member_no");
                String prename_code = accEmployee.GetString("prename_code");
                String sql_memname = @"SELECT mp.prename_desc || mb.memb_name || ' ' || mb.memb_surname as mem_name FROM mbmembmaster mb, mbucfprename mp WHERE mb.prename_code = mp.prename_code and mb.member_no = '"+ member_no +"'";
                Sdt memname = WebUtil.QuerySdt(sql_memname);
                if (memname.Next())
                {
                    mem_name = memname.GetString("mem_name");
                }

                //Decimal accyear = accEmployee.GetDecimal("account_year");
;
                int Found = DwList.FindRow("member_no = '" + member_no.Trim() + "'", 0, rowcount);

                if (Found == 0)
                {
                    int row = DwList.InsertRow(0);
                    DwList.SetItemString(row, "coop_id", state.SsCoopId);
                    DwList.SetItemString(row, "member_no", member_no);
                    DwList.SetItemDecimal(row, "account_year", salary_year);
                    DwList.SetItemDecimal(row, "salary_amt", 0);
                    DwList.SetItemString(row, "mem_name", mem_name);

                }
            }
        }

        #endregion
    }
}
