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

//ค้างเรื่องปุ่ม up down ไว้

using GcoopServiceCs; //ที่เพิ่มมาใหม่ 
using CoreWebServiceLibrary;//ที่เพิ่มมาใหม่ 


namespace Saving.Applications.account
{
    public partial class w_acc_report_design : PageWebSheet, WebSheet
    {
        //Js - Postback
        protected String postMoneyCode;
        protected String postNewClear;
        protected String postInsertRow;
        protected String postUpBotton;
        protected String postDownBotton;
        protected String postInsertAfterRow;
        protected String postDeleterow;
        protected String postPost;

        private String pbl = "acc_report_design.pbl";

        // Js - Event
        private void JspostDeleterow()
        {
            try
            {
                if (state.IsWritable)
                {
                    int RowDelete = int.Parse(HdRowDelete.Value);
                    string moneyCode = DwUtil.GetString(Dw_detail, RowDelete, "moneysheet_code", "");
                    decimal moneysheet_seq = DwUtil.GetDec(Dw_detail, RowDelete, "moneysheet_seq", 0);
                    bool isSaved = true;
                    if (moneysheet_seq > 0)
                    {
                        //Sta ta = 
                        Sta ta = new Sta(state.SsConnectionString);
                        try
                        {
                            string sql = "delete from  ACCSHEETMONEYDET where  moneysheet_code = '" + moneyCode + "' and moneysheet_seq = " + moneysheet_seq;
                            int ii = ta.Exe(sql);
                        }
                        catch (Exception ex)
                        {
                            isSaved = false;
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                        ta.Close();
                    }
                    if (isSaved)
                    {
                        Dw_detail.DeleteRow(RowDelete);
                        LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("คุณไม่มีสิทธิ์แก้ไขข้อมูลหน้าจอนี้");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostInsertAfterRow()
        {
            Sta ta = new Sta(state.SsConnectionString);
            int RowCurrent = Dw_detail.CurrentRow;
            decimal newSeq = 0;
            decimal sortOrder = Dw_detail.GetItemDecimal(RowCurrent, "sort_order");
            string moneyCode = Dw_detail.GetItemString(RowCurrent, "moneysheet_code");
            sortOrder--;

            int rowNew = Dw_detail.InsertRow(0);
            Dw_detail.SetItemString(rowNew, "coop_id", state.SsCoopId);
            Dw_detail.SetItemDecimal(rowNew, "sort_order", sortOrder);
            Dw_detail.SetItemString(rowNew, "moneysheet_code", moneyCode);

            string sqlSelect = "select max(moneysheet_seq) as max_seq from ACCSHEETMONEYDET where moneysheet_code = '" + moneyCode + "' and FINANCE_MASSTS = 1 and coop_id = '" + state.SsCoopId + "'";
            Sdt dt = ta.Query(sqlSelect);
            if (dt.Next())
            {
                int maxSeq = dt.GetInt32(0);
                newSeq = (maxSeq + 1);
                //Dw_detail.SetItemDecimal(rowNew, "moneysheet_seq", newSeq);
            }

                    try
                    {
                        string SqlInsertMu = "INSERT INTO ACCSHEETMONEYDET" +
                                    "( MONEYSHEET_CODE,              MONEYSHEET_SEQ,              COOP_ID, " +     //1
                                    "DATA_TYPE,                      DATA_DESC,                   DESCRIPTION," +   //2
                                    "AMOUNT1,                        AMOUNT2,                     AMOUNT3," +       //3
                                    "AMOUNT4,                        SHOW_STATUS,                 SHOW_DET_STATUS1," +      //4
                                    "SHOW_DET_STATUS2,               SHOW_DET_STATUS3,            SHOW_DET_STATUS4," +      //5
                                    "SHOW_DET_PERCENT,               TOG_STATUS,                  TOG_SEQ," +               //6
                                    "TOG_DESC,                       TOG_DATA_GROUP,              TOG_SHOW_STATUS," +       //7
                                    "DATA_GROUP,                     REMARK,                      DOWN_LINE," +             //8
                                    "UP_LINE,                        OPERATE_NATURE,              SHEETCODE_REF," +         //9
                                    "SORT_ORDER,                     FINANCE_MASSTS,              OPERATE_TYPE," +          //10
                                    "OPERATION_TYPE,                 CNT_MONEYDET )" +                                      //11
                                "VALUES" +
                                    "('" + moneyCode + "',         '" + newSeq + "',       '"+state.SsCoopId+"'," +    //1
                                    "null,                         null,                      ' '," +                   //2
                                    "0,                            0,                         0," +                     //3
                                    "0,                            1,                         0," +                     //4
                                    "0,                            0,                         0," +                     //5
                                    "0,                            0,                         0," +                     //6
                                    "null,                         0,                         0," +                     //7
                                    "0,                            null,                      0," +                     //8
                                    "0,                            'DR',                      null," +                     //9
                                    "'" + sortOrder + "',          1,                         0," +                     //10
                                    "0,                             0)";                                                 //11
                        
                        ta.Exe(SqlInsertMu);
                        DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, moneyCode, state.SsCoopId);
                    }
                        
                    catch (Exception Ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถแทรกแถวได้");
                        LtServerMessage.Text = WebUtil.ErrorMessage(Ex);
                        //ChkComp = false;
                    }
                    ta.Close();

            //Dw_detail.InsertRow(0);
            //Dw_detail.SetItemDecimal(Dw_detail.RowCount, "sort_order", RowCurrent);

            //Dw_detail.SetItemString(Dw_detail.RowCount, "moneysheet_code", Dw_master.GetItemString(1, "moneysheet_code").Trim());

            //Dw_detail.SetSort("sort_order asc");
            //Dw_detail.Sort();

            //for (int i = 1; i <= Dw_detail.RowCount; i++)
            //{
            //    Dw_detail.SetItemDecimal(i, "sort_order", i);
            //}
            //HdRowInsert.Value = RowCurrent.ToString();
        }

        private void JspostUpBotton()
        {
            //Int16 RowCurrent = Convert.ToInt16(HdRowClick.Value);
            int row = int.Parse(HdRowClick.Value);
            decimal order = Dw_detail.GetItemDecimal(row, "sort_order");
            order -= 11;
            Dw_detail.SetItemDecimal(row, "sort_order", order);

            //Int16 RowCurrent = Convert.ToInt16(HdRowClick.Value);
            //if (RowCurrent > 1)
            //{
            //    try
            //    {
            //        Dw_detail.RowsMove(RowCurrent, RowCurrent, Sybase.DataWindow.DataBuffer.Primary, Dw_detail, RowCurrent - 1, Sybase.DataWindow.DataBuffer.Primary);
            //        Dw_detail.SelectRow(0, false);
            //        Dw_detail.SelectRow(RowCurrent - 1, true);
            //        Dw_detail.SetRow(RowCurrent - 1);
            //    }
            //    catch (Exception ex)
            //    {
            //        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //    }
            //}
        }

        private void JspostDownBotton()
        {
            int row = int.Parse(HdRowClick.Value);
            decimal order = Dw_detail.GetItemDecimal(row, "sort_order");
            order += 11;
            Dw_detail.SetItemDecimal(row, "sort_order", order);

            //Int16 RowCurrent = Convert.ToInt16(HdRowClick.Value);
            //if (RowCurrent < Dw_detail.RowCount)
            //{
            //    try
            //    {
            //        Dw_detail.RowsMove(RowCurrent, RowCurrent, Sybase.DataWindow.DataBuffer.Primary, Dw_detail, RowCurrent + 2, Sybase.DataWindow.DataBuffer.Primary);
            //        Dw_detail.SelectRow(0, false);
            //        Dw_detail.SelectRow(RowCurrent + 1, true);
            //        Dw_detail.SetRow(RowCurrent + 1);
            //    }
            //    catch (Exception ex)
            //    {
            //        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //    }
            //}

        }

        private void JspostMoneyCode()
        {
            string moneysheet_code = null;
            moneysheet_code = DwUtil.GetString(Dw_master, 1, "moneysheet_code", "");

            if (moneysheet_code == null || moneysheet_code == "")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลรายละเอียดงบการเงิน รหัส : " + moneysheet_code);
                JspostNewClear();
            }
            else
            {
                DwUtil.RetrieveDataWindow(Dw_master, pbl, null, moneysheet_code,state.SsCoopId);
                DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, moneysheet_code,state.SsCoopId);

                if (Dw_master.RowCount == 0)
                {
                    String moneycode = HdMoneyCode.Value;
                    Dw_master.Reset();
                    Dw_master.InsertRow(0);
                    Dw_master.SetItemString(1, "moneysheet_code", moneycode);
                    Dw_master.SetItemString(1, "coop_id", state.SsCoopId);
                    Dw_detail.Reset();
                }
            }
        }

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_detail.Reset();
            Dw_master.InsertRow(0);
            HdRowInsert.Value = "";
        }

        private void JspostInsertRow()
        {
            String money_code = "";
            try
            {
                money_code = Dw_master.GetItemString(1, "moneysheet_code").Trim();
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
                    Sta ta = new Sta(state.SsConnectionString);
                    decimal newSeq = 0;
                    decimal newSort = 0;
                    int row = Dw_detail.InsertRow(0);
                    //int RowCurrent = Dw_detail.CurrentRow;
                    //decimal sortOrder = Dw_detail.GetItemDecimal(RowCurrent, "sort_order");
                    string sqlSelectSort = "select max(sort_order) as max_sort from ACCSHEETMONEYDET where moneysheet_code = '" + money_code + "' and FINANCE_MASSTS = 1 and coop_id = '" + state.SsCoopId + "'";
                    Sdt dt = ta.Query(sqlSelectSort);
                    if (dt.Next())
                    {
                        int max_sort = dt.GetInt32(0);
                        newSort = (max_sort + 1);
                    }

                    Dw_detail.SetItemString(row, "coop_id", state.SsCoopId);
                    Dw_detail.SetItemDecimal(row, "sort_order", newSort);
                    Dw_detail.SetItemString(row, "moneysheet_code", money_code);

                    string sqlSelect = "select max(moneysheet_seq) as max_seq from ACCSHEETMONEYDET where moneysheet_code = '" + money_code + "' and FINANCE_MASSTS = 1 and coop_id = '" + state.SsCoopId + "'";
                    Sdt dt2 = ta.Query(sqlSelect);
                    if (dt2.Next())
                    {
                        int maxSeq = dt2.GetInt32(0);
                        newSeq = (maxSeq + 1);
                    }

                    try
                    {
                        string SqlInsertMu = "INSERT INTO ACCSHEETMONEYDET" +
                                    "( MONEYSHEET_CODE,              MONEYSHEET_SEQ,              COOP_ID, " +     //1
                                    "DATA_TYPE,                      DATA_DESC,                   DESCRIPTION," +   //2
                                    "AMOUNT1,                        AMOUNT2,                     AMOUNT3," +       //3
                                    "AMOUNT4,                        SHOW_STATUS,                 SHOW_DET_STATUS1," +      //4
                                    "SHOW_DET_STATUS2,               SHOW_DET_STATUS3,            SHOW_DET_STATUS4," +      //5
                                    "SHOW_DET_PERCENT,               TOG_STATUS,                  TOG_SEQ," +               //6
                                    "TOG_DESC,                       TOG_DATA_GROUP,              TOG_SHOW_STATUS," +       //7
                                    "DATA_GROUP,                     REMARK,                      DOWN_LINE," +             //8
                                    "UP_LINE,                        OPERATE_NATURE,              SHEETCODE_REF," +         //9
                                    "SORT_ORDER,                     FINANCE_MASSTS,              OPERATE_TYPE," +          //10
                                    "OPERATION_TYPE,                 CNT_MONEYDET )" +                                      //11
                                "VALUES" +
                                    "('" + money_code + "',         '" + newSeq + "',       '" + state.SsCoopId + "'," +    //1
                                    "null,                         null,                      ' '," +                   //2
                                    "0,                            0,                         0," +                     //3
                                    "0,                            1,                         0," +                     //4
                                    "0,                            0,                         0," +                     //5
                                    "0,                            0,                         0," +                     //6
                                    "null,                         0,                         0," +                     //7
                                    "0,                            null,                      0," +                     //8
                                    "0,                            'DR',                      null," +                     //9
                                    "'" + newSort + "',             1,                         0," +                     //10
                                    "0,                             0)";                                                 //11

                        ta.Exe(SqlInsertMu);
                        DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, money_code, state.SsCoopId);
                    }

                    catch (Exception Ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถเพิ่มแถวได้");
                        LtServerMessage.Text = WebUtil.ErrorMessage(Ex);
                        //ChkComp = false;
                    }
                    ta.Close();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postMoneyCode = WebUtil.JsPostBack(this, "postMoneyCode");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postUpBotton = WebUtil.JsPostBack(this, "postUpBotton");
            postDownBotton = WebUtil.JsPostBack(this, "postDownBotton");
            postInsertAfterRow = WebUtil.JsPostBack(this, "postInsertAfterRow");
            postDeleterow = WebUtil.JsPostBack(this, "postDeleterow");
            postPost = WebUtil.JsPostBack(this, "postPost");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Dw_master.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(Dw_master);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMoneyCode")
            {
                JspostMoneyCode();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInsertRow")
            {
                JspostInsertRow();
            }
            else if (eventArg == "postUpBotton")
            {
                JspostUpBotton();
            }
            else if (eventArg == "postDownBotton")
            {
                JspostDownBotton();
            }
            else if (eventArg == "postInsertAfterRow")
            {
                JspostInsertAfterRow();
            }
            else if (eventArg == "postDeleterow")
            {
                JspostDeleterow();
            }
        }

        public void SaveWebSheet2()
        {
            // แก้ไขการ save
            Dw_master.UpdateData();
            try
            {
                n_accountClient accService = wcf.NAccount;
                if (HdRowInsert.Value != "")
                {
                    int rowinsert = int.Parse(HdRowInsert.Value);
                    rowinsert = rowinsert + 1;
                    Dw_detail.DeleteRow(rowinsert);
                }
                int rowall = Dw_detail.RowCount;
                String xmlDw_deatil = Dw_detail.Describe("Datawindow.Data.Xml");
                String money_code = Dw_master.GetItemString(1, "moneysheet_code").Trim();
                String wsPass = state.SsWsPass;
                //เรียกใช้ webservice
    //            int result = accService.SaveMoneySheet(wsPass, xmlDw_deatil, money_code);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลงบการเงินเรียบร้อยแล้ว");
                HdRowInsert.Value = "";
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
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
                if (Dw_detail.RowCount <= 0)
                {
                    throw new Exception("กรุณาเพิ่มข้อมูลรายละเอียดงบการเงินด้วย");
                }
                String moneyCode = Dw_master.GetItemString(1, "moneysheet_code");
                if (string.IsNullOrEmpty(moneyCode))
                {
                    throw new Exception("กรุณาใส่ค่ารหัสงบการเงิน");
                }
                String xmlMain = Dw_master.Describe("DataWindow.Data.XML");
                String objMain = Dw_master.DataWindowObject;
                String xmlDetail = Dw_detail.Describe("DataWindow.Data.XML");
                String objDetail = Dw_detail.DataWindowObject;
                String as_coopid = state.SsCoopId;
                //int iii = wcf.NAccount.SaveMoneySheetH(state.SsWsPass, pbl, xmlMain, objMain, xmlDetail, objDetail, moneyCode, as_coopid);   //12.0
                //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ มีข้อมูลทั้งหมด " + iii + " รายการ");   // 12.0

                SaveMoneySheetH(state.SsWsPass, pbl, xmlMain, objMain, xmlDetail, objDetail, moneyCode, as_coopid);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ ");



                DwUtil.RetrieveDataWindow(Dw_master, pbl, null, moneyCode, state.SsCoopId);
                DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, moneyCode, state.SsCoopId);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                if (Dw_detail.RowCount > 0)
                {
                    DwUtil.RetrieveDDDW(Dw_detail, "data_type", pbl, null);
                }
            }
            catch { }

            try
            {
                if (Dw_detail.RowCount > 0)
                {
                    Dw_detail.SetSort("MONEYSHEET_CODE asc, SORT_ORDER asc, MONEYSHEET_SEQ asc");
                    Dw_detail.Sort();
                    for (int i = 1; i <= Dw_detail.RowCount; i++)
                    {
                        Dw_detail.SetItemDecimal(i, "SORT_ORDER", (i * 10));
                    }
                }
            }
            catch { }

            Dw_master.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion
    }
}
