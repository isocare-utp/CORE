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
    public partial class w_sheet_bg_budgetsetamt : PageWebSheet,WebSheet
    {
        protected String initJavaScript;
        protected String RetrieveList;
        protected String jsPostInsertRow;
        protected String jsPostDeleteRow;
        protected String jsPostGetAccount;
        protected String jsPostGetAllBudget;
        protected String jsPostYear;
        protected String jsPostBGType;
        protected String jsPostInsertBfRow;
        protected String jsPostMoveUp;
        protected String jsPostMoveDown;
        protected String jsSetBGDetail;
        string pbl = "budget.pbl";

        private n_accountClient accService;//ประกาศตัวแปร WebService

        #region WebSheet Members
        public void InitJsPostBack()
        {
            initJavaScript = WebUtil.JsPostBack(this, "initJavaScript");
            RetrieveList = WebUtil.JsPostBack(this, "RetrieveList");
            jsPostInsertRow = WebUtil.JsPostBack(this, "jsPostInsertRow");
            jsPostDeleteRow = WebUtil.JsPostBack(this, "jsPostDeleteRow");
            jsPostGetAccount = WebUtil.JsPostBack(this, "jsPostGetAccount");
            jsPostGetAllBudget = WebUtil.JsPostBack(this, "jsPostGetAllBudget");
            jsPostYear = WebUtil.JsPostBack(this, "jsPostYear");
            jsPostBGType = WebUtil.JsPostBack(this, "jsPostBGType");
            jsPostInsertBfRow = WebUtil.JsPostBack(this, "jsPostInsertBfRow");
            jsPostMoveUp = WebUtil.JsPostBack(this, "jsPostMoveUp");
            jsPostMoveDown = WebUtil.JsPostBack(this, "jsPostMoveDown");
            jsSetBGDetail = WebUtil.JsPostBack(this, "jsSetBGDetail");
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
                case "jsPostBGType":
                    ResetAccBG();
                    break;
                case "RetrieveList":
                    retrieve();
                    break;
                case "jsPostInsertRow":
                    InsertRow();
                    break;
                case "jsPostDeleteRow":
                    DeleteRow();
                    break;
                case "jsPostGetAccount":
                    GetAccount();
                    break;
                case "jsPostGetAllBudget":
                    GetAllBudget();
                    break;
                case "jsPostYear":
                    CopyBudget(Hdyear.Value.ToString());
                    break;
                case "jsPostInsertBfRow":
                    InsertBfRow();
                    break;
                case "jsPostMoveUp":
                    MoveRowUp();
                    break;
                case "jsPostMoveDown":
                    MoveRowDown();
                    break;
                case "jsSetBGDetail":
                    JsSetBGDetail();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            #region Service
            String xmlList = "";
            Decimal bgYear = DwMain.GetItemDecimal(1, "budgetyear") - 543;
            try
            {
                short year = short.Parse(bgYear.ToString());
                xmlList = DwList.Describe("DataWindow.Data.XML");
                string accrcvpay = DwMain.GetItemString(1, "accrcvpay");
                int result = accService.of_set_budget(state.SsWsPass, xmlList, state.SsCoopId, year, accrcvpay);
                //int result = wcf.NAccount.of_set_budget(state.SsWsPass, xmlList, state.SsCoopId, year, accrcvpay);
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

            #region Coding
            //string budget_id = "";
            //string accbudgetgroup_typ = "";
            //string budget_detail = "";
            //string account_id_list = "";
            //Decimal account_budget = 0;
            //Decimal year = DwMain.GetItemDecimal(1, "budgetyear") - 543;
            //InsertRow = DwList.RowCount;
            //string sqlcount = @"SELECT * FROM accbudget where account_year = " + year;
            //Sdt count = WebUtil.QuerySdt(sqlcount);
            //DataRow = count.GetRowCount();
            //try
            //{
            //    for (int j = 1; j <= DataRow; j++)
            //    {
            //        budget_id = DwList.GetItemString(j, "budget_id");
            //        accbudgetgroup_typ = DwList.GetItemString(j, "accbudgetgroup_typ");
            //        budget_detail = DwList.GetItemString(j, "budget_detail");
            //        account_budget = DwList.GetItemDecimal(j, "account_budget");
            //        account_id_list = DwList.GetItemString(j, "account_id_list");
            //        string sqlupdate = @"UPDATE accbudget SET accbudgetgroup_typ = '" + accbudgetgroup_typ + "', budget_detail = '" + budget_detail + "', account_budget = " +
            //            account_budget + ", account_id_list = '" + account_id_list + "' WHERE budget_id = '" + budget_id + "'";
            //        Sdt update = WebUtil.QuerySdt(sqlupdate);
            //    }

            //    for (int i = DataRow + 1; i <= InsertRow; i++)
            //    {
            //        accbudgetgroup_typ = DwList.GetItemString(i, "accbudgetgroup_typ");
            //        budget_detail = DwList.GetItemString(i, "budget_detail");
            //        account_budget = DwList.GetItemDecimal(i, "account_budget");
            //        account_id_list = DwList.GetItemString(i, "account_id_list");

            //        string sqlinsert = @"INSERT INTO accbudget VALUES( accbudget_seq.nextval ,'" + accbudgetgroup_typ + "','" + state.SsCoopId + "','" + state.SsCoopId + "','" +
            //            year + "','" + account_budget + "','" + account_id_list + "','" + budget_detail + "')";
            //        Sdt insert = WebUtil.QuerySdt(sqlinsert);
            //    }

            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            //}
            #endregion
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }
        #endregion

        #region Function

        private void JsSetBGDetail()
        {
            int row = Convert.ToInt16(Hdrow.Value);
            DwList.GetItemString(row, "accbudgetgroup_typ");
            String sqlselectBGdetail = @"select accbudgetgroup_des from accbudget_group where accbudgetgroup_typ = '" +
                DwList.GetItemString(row, "accbudgetgroup_typ") + "'";
            Sdt accbudgetBGDetail = WebUtil.QuerySdt(sqlselectBGdetail);
            DwList.SetItemString(row, "budget_detail", "");

            while (accbudgetBGDetail.Next())
            {
                String accbudgetgroup_des = accbudgetBGDetail.GetString("accbudgetgroup_des");               
                DwList.SetItemString(row, "budget_detail", accbudgetgroup_des);
            }

        }

        private void MoveRowUp()
        {
            int row = Convert.ToInt16(Hdrow.Value);
            DwList.RowsMove(row, row, Sybase.DataWindow.DataBuffer.Primary, DwList, row - 1, Sybase.DataWindow.DataBuffer.Primary);
            DwList.SetRow(row - 1);
        }

        private void MoveRowDown()
        {
            int row = Convert.ToInt16(Hdrow.Value);
            DwList.RowsMove(row, row, Sybase.DataWindow.DataBuffer.Primary, DwList, row + 2, Sybase.DataWindow.DataBuffer.Primary);
            DwList.SetRow(row + 1);
        }

        private void InsertRow()
        {
            int row = DwList.InsertRow(0);
            DwList.SetItemString(row, "coop_id", state.SsCoopId);
            String accrcvpay = DwMain.GetItemString(1, "accrcvpay");
            DwList.SetItemString(row, "accrcvpay", accrcvpay);
            DwList.SetItemDecimal(row, "sort_seq", row);
        }

        private void InsertBfRow()
        //{
        //    int SelectRow = Convert.ToInt16(HdrowSelect.Value);
        //    int row = DwList.InsertRow(SelectRow);
        //    DwList.SetItemString(row, "coop_id", state.SsCoopId);
        //    String accrcvpay = DwMain.GetItemString(1, "accrcvpay");
        //    DwList.SetItemString(row, "accrcvpay", accrcvpay);
        //    DwList.SetItemDecimal(row, "sort_seq", row);
        //}
        {
            Sta ta = new Sta(state.SsConnectionString);
            int SelectRow = Convert.ToInt16(HdrowSelect.Value);
            //int row = DwList.InsertRow(SelectRow);
            //DwList.SetItemString(row, "coop_id", state.SsCoopId);
            String accrcvpay = DwMain.GetItemString(1, "accrcvpay");
            //DwList.SetItemString(row, "accrcvpay", accrcvpay);
            //DwList.SetItemDecimal(row, "sort_seq", row);
            Decimal bgYear = DwMain.GetItemDecimal(1, "budgetyear") - 543;
            Decimal sort_seq = DwList.GetItemDecimal(SelectRow, "sort_seq");
            sort_seq--;

            try
            {
                string SqlInsertAccBG = "INSERT INTO ACCBUDGET" +
                            "( COOP_ID,                    ACCOUNT_YEAR,              ACCOUNT_BUDGET, " +     //1
                            "ACCOUNT_ID_LIST,              BUDGET_DETAIL,             BUDGET_SUPERGRP," +   //2
                            "BUDGET_TYPE,                  BUDGET_LEVEL,              ACCBUDGETGROUP_TYP," +       //3
                            "SORT_SEQ,                     ACCRCVPAY )" +
                            "VALUES" +
                            "('" + state.SsCoopControl + "',  '" + bgYear + "',       0," +    //1
                            "null,                         null,                      null," +                   //2
                            "null,                         null,                         null," +                     //3
                            "'" + sort_seq + "',        '" + accrcvpay + "')";
                ta.Exe(SqlInsertAccBG);
                DwUtil.RetrieveDataWindow(DwList, pbl, null, bgYear, state.SsCoopId, accrcvpay);
            }

            catch (Exception Ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถแทรกแถวได้");
                LtServerMessage.Text = WebUtil.ErrorMessage(Ex);
                //ChkComp = false;
            }
            ta.Close();
        }


        private void retrieve()
        {
            Decimal bgYear = DwMain.GetItemDecimal(1, "budgetyear") - 543;
            string accrcvpay = DwMain.GetItemString(1, "accrcvpay");
            try
            {
                DwUtil.RetrieveDataWindow(DwList, pbl, null, bgYear, state.SsCoopId, accrcvpay);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void DeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hdrow.Value);
            Decimal bgYear = DwMain.GetItemDecimal(1, "budgetyear") - 543;
            String accbudgetgroup_typ = DwList.GetItemString(RowDetail, "accbudgetgroup_typ");
            String coop_id = state.SsCoopId;
            try
            {
                string sqldelete = @"DELETE FROM accbudget WHERE coop_id = '" + coop_id + "' and accbudgetgroup_typ = '"+accbudgetgroup_typ+"' and account_year = '"+ bgYear + "'";
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

        private void GetAccount()
        {
            string acc_list = Hdacclist.Value;
            int row = Convert.ToInt32(Hdrow.Value);
            DwList.SetItemString(row, "account_id_list", acc_list);
        }

        private void GetAllBudget()
        {
            String accrcvpay = DwMain.GetItemString(1, "accrcvpay");
            String sqlselectbg_group = @"SELECT accbudgetgroup_typ, accbudgetgroup_des, budget_type, budget_level, budget_supergrp, sort_seq FROM accbudget_group WHERE accrcvpay = '" + 
                accrcvpay + "' order by sort_seq";
            Sdt accbudgetgroup_typ = WebUtil.QuerySdt(sqlselectbg_group);
            int rowcount = DwList.RowCount;

            while (accbudgetgroup_typ.Next())
            {
                Decimal sort_seq = accbudgetgroup_typ.GetDecimal("sort_seq");
                String accbggroup_type = accbudgetgroup_typ.GetString("accbudgetgroup_typ");
                String budget_type = accbudgetgroup_typ.GetString("budget_type");
                Decimal budget_level = accbudgetgroup_typ.GetDecimal("budget_level");
                String budget_supergrp = accbudgetgroup_typ.GetString("budget_supergrp");
                String accbudgetgroup_des = accbudgetgroup_typ.GetString("accbudgetgroup_des");
                int Found = DwList.FindRow("accbudgetgroup_typ = '" + accbggroup_type.Trim() + "'", 0, rowcount);

                if (Found == 0 )
                {
                    int row = DwList.InsertRow(0);
                    DwList.SetItemString(row, "budget_type", budget_type);
                    DwList.SetItemString(row, "accbudgetgroup_typ", accbggroup_type);
                    DwList.SetItemDecimal(row, "budget_level", budget_level);
                    DwList.SetItemString(row, "budget_supergrp", budget_supergrp);
                    DwList.SetItemString(row, "coop_id", state.SsCoopId);
                    DwList.SetItemString(row, "budget_detail", accbudgetgroup_des);
                    DwList.SetItemDecimal(row, "sort_seq", sort_seq);
                    DwList.SetItemString(row, "accrcvpay", accrcvpay);
                }
            }
        }

        private void CopyBudget(String year)
        {
            String accrcvpay = DwMain.GetItemString(1, "accrcvpay");
            String sqlselectbudget = @"select accbudgetgroup_typ, budget_detail, account_id_list, budget_type, sort_seq, budget_level, budget_supergrp from accbudget where account_year = '" +
                year + "' and accrcvpay = '" + accrcvpay + "' order by sort_seq";
            Sdt accbudget = WebUtil.QuerySdt(sqlselectbudget);
            int rowcount = DwList.RowCount;

            while (accbudget.Next())
            {
                String accbggroup_type = accbudget.GetString(0);
                int Found = DwList.FindRow("accbudgetgroup_typ = '" + accbggroup_type.Trim() + "'", 0, rowcount);

                if (Found == 0)
                {
                    String budget_detail = accbudget.GetString(1);
                    String account_id_list = accbudget.GetString(2);
                    String budget_type = accbudget.GetString(3);
                    Decimal sort_seq = accbudget.GetDecimal(4);
                    Decimal budget_level = accbudget.GetDecimal(5);
                    String budget_supergrp = accbudget.GetString(6);
                    int row = DwList.InsertRow(0);
                    DwList.SetItemString(row, "budget_type", budget_type);
                    DwList.SetItemString(row, "accbudgetgroup_typ", accbggroup_type);
                    DwList.SetItemString(row, "budget_detail", budget_detail);
                    DwList.SetItemString(row, "account_id_list", account_id_list);
                    DwList.SetItemString(row, "coop_id", state.SsCoopId);
                    DwList.SetItemDecimal(row, "sort_seq", sort_seq);
                    DwList.SetItemDecimal(row, "budget_level", budget_level);
                    DwList.SetItemString(row, "budget_supergrp", budget_supergrp);
                    DwList.SetItemString(row, "accrcvpay", accrcvpay);
                }
            }
        }

        private void ResetAccBG()
        {
            int row = Convert.ToInt16(Hdrow.Value);
            if (DwList.GetItemString(row, "budget_type") == "H")
            {
                DwList.SetItemNull(row, "account_budget");
                DwList.SetItemNull(row, "account_id_list");
            }
            else if (DwList.GetItemString(row, "budget_type") == "D")
            {
                DwList.SetItemDecimal(row, "account_budget", 0);
            }
        }
        #endregion
    }
}
