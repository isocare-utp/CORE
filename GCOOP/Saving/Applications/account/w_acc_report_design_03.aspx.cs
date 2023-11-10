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
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAccount;

using GcoopServiceCs; //ที่เพิ่มมาใหม่ 
using CoreWebServiceLibrary;
using DataLibrary;//ที่เพิ่มมาใหม่ 

namespace Saving.Applications.account
{
    public partial class w_acc_report_design_03 : PageWebSheet,WebSheet 
    {
        //================================
        protected String postNewClear;
        protected String postDeleteRow;
        private n_accountClient Accsrv;
        private accFunction accF;
        protected String saveData;

        private String pbl = "acc_report_design.pbl";
        //================================

       

        private void JspostDeleteRow()
        {
            int Rowdelete = int.Parse(HdRowDelete.Value);
            Dw_show.DeleteRow(Rowdelete);
            Dw_show.UpdateData();
            LtServerMessage.Text = WebUtil.CompleteMessage("ลบแถวข้อมูลเสร็จเรียบร้อยแล้ว");
        }

        private void JspostNewClear()
        {
            Dw_main.Retrieve("03",state.SsCoopControl);
            Dw_main.SetItemDecimal(1, "compare", 1);
            Dw_main.SetItemDecimal(1, "data_1", 1);
            Dw_main.SetItemDecimal(1, "data_2", 1);
            Panel2.Visible = false;
            //B_add.Visible = false;
            //B_insert.Visible = false;
            //B_sortseq.Visible = false;
            //B_process.Visible = true;
            dw_rpt.Visible = false;
            B_backprocess.Visible = false;
            Showformula.SelectedValue = "2";
            Showselect.SelectedValue = "1";
        }


      

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            saveData = WebUtil.JsPostBack(this, "saveData");
            //changeValue = WebUtil.JsPostBack(this, "changeValue");
          
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                Accsrv = wcf.NAccount;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            try
            {
                accF = new accFunction();
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Function ไม่ได้");
                return;
            }


            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);
            Dw_show.SetTransaction(sqlca);
            dw_rpt.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.Retrieve("03", state.SsCoopControl);
                Dw_main.SetItemDecimal(1, "compare", 1);
                Dw_main.SetItemDecimal(1, "data_1", 1);
                Dw_main.SetItemDecimal(1, "data_2", 1);
                //Dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                Panel2.Visible = false;
                //B_add.Visible = false;
                //B_insert.Visible = false;
                //B_sortseq.Visible = false;
                //B_process.Visible = true;
                dw_rpt.Visible = false;
                B_backprocess.Visible = false;
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_show);
                this.RestoreContextDw(dw_rpt);
            }

            if (IsPostBack)
            {
                Dw_main.RestoreContext();
                dw_rpt.RestoreContext();
            }
            if (Dw_main.RowCount < 1)
            {
                Dw_main.Reset();
                dw_rpt.Reset();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "changeValue")
            {
                //GetRetrieve();
            }
          }

        public void SaveWebSheet2()
        {
            try 
            {
                Dw_main.UpdateData();
                Dw_show.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลงบกระแสเงินสดเรียบร้อยแล้ว");
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        //edit save 125
        public int SaveMoneySheetH(String wsPass, String pbl, String xmlMain, String objMain, String xmlDetail, String objDetail, String moneyCode, String as_coopid)
        {
            Security security = new Security(wsPass);
            moneyCode = moneyCode.Trim();
            pbl = security.GcoopPathCore + "Saving\\DataWindow\\" + "account" + "\\" + pbl;
            Sta ta = new Sta(security.ConnectionString);
            ta.Transection();
            int iii = 0;
            try
            {
                DwHandle dwH = new DwHandle(xmlMain, pbl, objMain);
                String sqlUp = dwH.SqlUpdateSyntax("ACCSHEETMONEYHEAD", 1);
                ta.Exe(sqlUp);

                ta.Exe("delete from ACCSHEETMONEYDET where moneysheet_code = '" + moneyCode + "' and FINANCE_MASSTS = 1 and coop_id = '" + as_coopid + "'");

                DwHandle dwH2 = new DwHandle(xmlDetail, pbl, objDetail);

                DataStore dwDetail = dwH2.GetDataStore();
                for (int i = 1; i <= dwDetail.RowCount; i++)
                {
                    decimal seqNo = 0;
                    try
                    {
                        seqNo = dwDetail.GetItemDecimal(i, "moneysheet_seq");
                    }
                    catch { }
                    if (seqNo > 0)
                    {
                        string sqlIns = dwH2.SqlInsertSyntax("ACCSHEETMONEYDET", i);
                        if (iii == 97)
                        {
                            int mini_iii = iii;
                        }
                        iii += ta.Exe(sqlIns);
                    }
                }
                for (int i = 1; i <= dwDetail.RowCount; i++)
                {
                    decimal seqNo = 0;
                    try
                    {
                        seqNo = dwDetail.GetItemDecimal(i, "moneysheet_seq");
                    }
                    catch { }
                    if (seqNo == 0)
                    {
                        string sqlSelect = "select max(moneysheet_seq) as max_seq from ACCSHEETMONEYDET where moneysheet_code = '" + moneyCode + "' and FINANCE_MASSTS = 1 and coop_id = '" + as_coopid + "'";
                        Sdt dt = ta.Query(sqlSelect);
                        if (dt.Next())
                        {
                            int maxSeq = dt.GetInt32(0);
                            decimal newSeq = (maxSeq + 1);
                            dwDetail.SetItemDecimal(i, "moneysheet_seq", newSeq);
                            string sqlIns = dwH2.SqlInsertSyntax("ACCSHEETMONEYDET", i);
                            iii += ta.Exe(sqlIns);
                        }
                    }
                }
                ta.Commit();
                ta.Close();
                //DisConnect();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                //DisConnect();
                Exception ex2 = new Exception(ex.Message + "[" + iii + "]");
                throw ex2;
            }
            return iii;
        }



        //end edit save 125
        public void SaveWebSheet()
        {
            try
            {
                if (Dw_show.RowCount <= 0)
                {
                    throw new Exception("กรุณาเพิ่มข้อมูลรายละเอียดงบการเงินด้วย");
                }
                String moneyCode = Dw_main.GetItemString(1, "moneysheet_code");
                if (string.IsNullOrEmpty(moneyCode))
                {
                    throw new Exception("กรุณาใส่ค่ารหัสงบการเงิน");
                }
                String xmlMain = Dw_main.Describe("DataWindow.Data.XML");
                String objMain = Dw_main.DataWindowObject;
                String xmlDetail = Dw_show.Describe("DataWindow.Data.XML");
                String objDetail = Dw_show.DataWindowObject;
                String as_coopid = state.SsCoopControl;
                //int iii = wcf.NAccount.SaveMoneySheetH(state.SsWsPass, pbl, xmlMain, objMain, xmlDetail, objDetail, moneyCode, as_coopid);
                //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ มีข้อมูลทั้งหมด " + iii + " รายการ");

                SaveMoneySheetH(state.SsWsPass, pbl, xmlMain, objMain, xmlDetail, objDetail, moneyCode, as_coopid);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ ");


                DwUtil.RetrieveDataWindow(Dw_main, pbl, null, moneyCode, state.SsCoopControl);
                DwUtil.RetrieveDataWindow(Dw_show, pbl, null, moneyCode, state.SsCoopControl);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            //if (Dw_main.RowCount > 1)
            //{
            //    Dw_main.DeleteRow(Dw_main.RowCount);
            //}
            //Dw_main.SaveDataCache();
            //Dw_show.SaveDataCache();
            try
            {
                if (Dw_show.RowCount > 0)
                {
                    DwUtil.RetrieveDDDW(Dw_show, "data_type", pbl, null);
                }
            }
            catch { }

            try
            {
                if (Dw_show.RowCount > 0)
                {
                    Dw_show.SetSort("MONEYSHEET_CODE asc, SORT_ORDER asc, MONEYSHEET_SEQ asc");
                    Dw_show.Sort();
                    for (int i = 1; i <= Dw_show.RowCount; i++)
                    {
                        Dw_show.SetItemDecimal(i, "SORT_ORDER", (i * 10));
                    }
                }
            }
            catch { }

            Dw_main.SaveDataCache();
            Dw_show.SaveDataCache();
            
        }

        #endregion

       
        protected void Showformula_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Showformula.SelectedValue == "1") {
                Panel2.Visible = true;
                Dw_show.Retrieve("03",state.SsCoopControl);
                //B_add.Visible = true;
                //B_insert.Visible = true;
                //B_sortseq.Visible = true;
                //B_process.Visible = false;
                dw_rpt.Visible = false;
                B_backprocess.Visible = false;
            }
            else if (Showformula.SelectedValue == "2") {
                Panel2.Visible = false;
                //B_add.Visible = false;
                //B_insert.Visible = false;
                //B_sortseq.Visible = false;
                //B_process.Visible = true;
                dw_rpt.Visible = false;
                B_backprocess.Visible = false;
            }
        }

        protected void Showselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Showselect.SelectedValue == "2") 
            {
                Dw_show.SetFilter("show_status = 1");
                Dw_show.Filter();
            }
            else if (Showselect.SelectedValue == "1") 
            {
                Dw_show.SetFilter("");
                Dw_show.Filter();
            }
        }
        //AddRow
        protected void B_add_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int RowAll = Dw_show.CurrentRow;
            //    if (RowAll > 0)
            //    {
            //        RowAll = int.Parse(Dw_show.Describe("evaluate('max(moneysheet_seq)',0)"));
            //        RowAll = RowAll + 5;
            //        int RowCurrent = Dw_show.InsertRow(0);
            //        Dw_show.SetItemDecimal(RowCurrent, "moneysheet_seq", RowAll);
            //        Dw_show.SetItemString(RowCurrent, "moneysheet_code", "03");
            //        Dw_show.SetItemString(RowCurrent, "coop_id", state.SsCoopControl);
            //    }
            //    else
            //    {
            //        RowAll++;
            //        int RowCurrent = Dw_show.InsertRow(0);
            //        Dw_show.SetItemDecimal(RowCurrent, "moneysheet_seq", RowAll);
            //        Dw_show.SetItemString(RowCurrent, "moneysheet_code", "03");
            //        Dw_show.SetItemString(RowCurrent, "coop_id", state.SsCoopControl);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //}

            String money_code = "";
            try
            {
                money_code = Dw_main.GetItemString(1, "moneysheet_code").Trim();
            }
            catch
            {
                money_code = "";
            }
            if (money_code == "" || money_code == null)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("กรุณาเลือกงบการเงิน");
            }
            else
            {
                try
                {
                    int row = Dw_show.InsertRow(0);
                    Dw_show.SetItemString(row, "coop_id", state.SsCoopControl);
                    Dw_show.SetItemDecimal(row, "sort_order", 0);
                    Dw_show.SetItemString(row, "moneysheet_code", money_code);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }
        // InsertRow
        protected void B_insert_Click(object sender, EventArgs e)
        {
            //int RowCurrent = Dw_show.CurrentRow;
            //int RowAll;
            //RowAll = int.Parse(Dw_show.Describe("evaluate('max(moneysheet_seq)',0)"));
            //RowAll = RowAll + 5;
            //Dw_show.InsertRow(RowCurrent);
            //Dw_show.SetItemDecimal(RowCurrent, "moneysheet_seq", RowAll);
            //Dw_show.SetItemString(RowCurrent, "moneysheet_code", "03");
            //Dw_show.SetItemString(RowCurrent, "coop_id", state.SsCoopControl);
            int RowCurrent = Dw_show.CurrentRow;
            decimal sortOrder = Dw_show.GetItemDecimal(RowCurrent, "sort_order");
            string moneyCode = Dw_show.GetItemString(RowCurrent, "moneysheet_code");
            sortOrder++;

            int rowNew = Dw_show.InsertRow(0);
            Dw_show.SetItemString(rowNew, "coop_id", state.SsCoopId);
            Dw_show.SetItemDecimal(rowNew, "sort_order", sortOrder);
            Dw_show.SetItemString(rowNew, "moneysheet_code", moneyCode);



        }
        // ReSeqRow
        protected void B_sortseq_Click(object sender, EventArgs e)
        {
            try 
            {
                int li_row = Dw_show.RowCount;
                for (int li_index = 1; li_index <= li_row; li_index++)
                {
                    Dw_show.SetItemDecimal(li_index, "moneysheet_seq", li_index * 5);
                    Dw_show.SetItemString(li_index, "coop_id", state.SsCoopControl);
                    Dw_show.SetItemString(li_index, "moneysheet_code", "03");
                }
                Dw_show.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("จัดลำดับเลขที่ใหม่เรียบร้อยแล้ว");
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
          
        }
        // processsheetcashflow
        protected void B_process_Click(object sender, EventArgs e)
        {
            try
            {
                //==== เริ่มกระบวกการจัดทำงบกระแสเงินสด
                dw_rpt.Visible = true;
                Dw_main.Visible = false;
                Dw_show.Visible = false;

                B_backprocess.Visible = true;
                Panel2.Visible = false;
                //B_add.Visible = false;
                Showformula.Visible = false;
                Showselect.Visible = false;
                //B_insert.Visible = false;
                //B_sortseq.Visible = false;
                //B_process.Visible = false;

                dw_rpt.Retrieve("03");

                string str_tmp = "";
                str_tmp = Dw_main.Describe("DataWindow.Data.XML");
                dw_rpt.Reset();
                HdSheetHeadName.Value = Dw_main.GetItemString(1, "report_heading").ToString().Trim();
                HdSheetTypeCode.Value = "03";
                HdSheetHeadCol1.Value = Dw_main.GetItemString(1, "month_1_1").ToString().Trim();
                HdSheetHeadCol2.Value = Dw_main.GetItemString(1, "month_2_1").ToString().Trim();
                int result = 0;
                //str_tmp = Accsrv.GenCashFlowSheet(state.SsWsPass, str_tmp, HdSheetTypeCode.Value, state.SsCoopControl);
                result = wcf.NAccount.of_gen_cashflow_sheet(state.SsWsPass, str_tmp, HdSheetTypeCode.Value, state.SsCoopControl, ref str_tmp);
                dw_rpt.ImportString(str_tmp, FileSaveAsType.Xml);

                str_tmp = "show_header.text = '";
                str_tmp += HdSheetHeadName.Value + "~n";
                str_tmp += "" + Dw_main.GetItemString(1, "moneysheet_name").ToString().Trim() + "~n";
                str_tmp += "สำหรับสิ้นสุดวันที่ " + accF.GetFindToLDayOfM(HdSheetHeadCol1.Value, Dw_main.GetItemString(1, "year_1"), "th") + accF.CnvStrToThaiM(HdSheetHeadCol1.Value, 0) + Dw_main.GetItemString(1, "year_1");
                if (Dw_main.GetItemString(1, "total_show").ToString().Equals("2"))
                {
                    str_tmp += " และ วันที่ " + accF.GetFindToLDayOfM(HdSheetHeadCol2.Value, Dw_main.GetItemString(1, "year_2"), "th") + accF.CnvStrToThaiM(HdSheetHeadCol2.Value, 0) + Dw_main.GetItemString(1, "year_2");
                }
                str_tmp += "'";

                dw_rpt.Modify(str_tmp);
                str_tmp = "t_1.text = '";
                str_tmp += getHDdata(Dw_main.GetItemString(1, "data_1"), Dw_main.GetItemString(1, "compare"), HdSheetHeadCol1.Value, Dw_main.GetItemString(1, "year_1"));
                str_tmp += "'";
                dw_rpt.Modify(str_tmp);

                str_tmp = "t_2.text = '";
                str_tmp += getHDdata(Dw_main.GetItemString(1, "data_2"), Dw_main.GetItemString(1, "compare"), HdSheetHeadCol2.Value, Dw_main.GetItemString(1, "year_2"));
                str_tmp += "'";
                dw_rpt.Modify(str_tmp);
                
                
                //==== จบกระบวนการ
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        // backprocesssheetcashflow
        protected void B_backprocess_Click(object sender, EventArgs e)
        {
            try
            {
                Panel2.Visible = false;
                //B_add.Visible = false;
                //B_insert.Visible = false;
                //B_sortseq.Visible = false;
                //B_process.Visible = true;
                B_backprocess.Visible = false;

                Showformula.Visible = true;
                Showselect.Visible = true;

                dw_rpt.Visible = false;
                Dw_main.Visible = true;
                Dw_show.Visible = true;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private string getHDdata(string i_d, string comp, string month, string year)
        {
            string txt = "";
            if (comp == "1")
            {
                if (i_d == "1")
                {
                    txt = "(สะสม)";
                }
                else
                {
                    txt = "(เดือน)";
                }
            }
            txt += accF.CnvStrToThaiM(month) + year;
            return txt;
        }
    }
}
