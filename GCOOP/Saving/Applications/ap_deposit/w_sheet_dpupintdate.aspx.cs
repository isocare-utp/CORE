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
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon; // new common
using System.Globalization;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;// new deposit
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dpupintdate : PageWebSheet, WebSheet
    {

        protected String changeType;//สร้างตัวแปรแบบ protected
        protected String postType;
        protected String postChange;
        protected String bfUpdateDw;
        protected String postAddRow;
        protected String FilterBookType;
        protected String postRate;
        protected String delinterestrate;
        protected String postUpInt;
        //protected String CoopControl = "001001";
        //private DepositClient depService;
        private n_depositClient ndept;
        private DwThDate tDwInterestNew;
        private string typeCode;

        private void JsFilterBookType()
        {
            try
            {
                //if (DwSaving.GetItemString(1, "book_stmbase") != null)
                //{
                //    String bookType = DwSaving.GetItemString(1, "book_stmbase");
                //    DataWindowChild dc2 = DwSaving.GetChild("book_group");
                //    dc2.SetTransaction(sqlca);
                //    dc2.Retrieve();
                //    dc2.SetFilter("book_type = '" + bookType + "'");
                //    dc2.Filter();
                //}
            }
            catch (Exception)
            {

            }
        }

        private void JspostChange()
        {
            //decimal maxBalance = DwSaving.GetItemDecimal(1, "maxbalance_flag");
            //decimal limitDept = DwSaving.GetItemDecimal(1, "limitdept_flag");
            //decimal limitWith = DwSaving.GetItemDecimal(1, "limitwith_flag");
            //decimal feeNoMove = DwFix.GetItemDecimal(1, "feenomove_flag");
            //decimal haveTax = DwFix.GetItemDecimal(1, "havetax_prefer");
            //decimal taxWay = DwFix.GetItemDecimal(1, "taxway_compute");
            //decimal previosInt = DwInterestBf.GetItemDecimal(1, "previos_intflag");
            //decimal previosCon = DwInterestBf.GetItemDecimal(1, "previos_config");

            //maxbalance_flag
            //if (maxBalance == 0)
            //{
            //    DwSaving.SetItemDecimal(1, "maxbalance", 0);
            //}

            //limitdept_flag
            //if (limitDept == 0)
            //{
            //    DwSaving.SetItemDecimal(1, "limitdept_amt", 0);
            //}

            //limitwith_flag
            //if (limitWith == 0)
            //{
            //    DwSaving.SetItemDecimal(1, "limitwith_amt", 0);
            //}

            //if (feeNoMove == 0)
            //{
            //    DwFix.SetItemDecimal(1, "minbal_feeamt", 0);
            //    DwFix.SetItemDecimal(1, "minbal_feeduring", 0);
            //    DwFix.SetItemDecimal(1, "minmonth_feeamt", 0);

            //}

        }

        private void JsPostType()
        {
            string display = "";
            string persong;
            typeCode = HfDeptType.Value;
            persong = Hdpersong.Value;
            String typeSelect = DwMain.GetItemString(1, "depttype_select");
            String persongroup = DwMain.GetItemString(1, "persongrp_code");
            string sql = "select depttype_code from dpdepttype where coop_id = '" + state.SsCoopId + "' and depttype_group='" + typeSelect.Trim() + "' and persongrp_code='" + persongroup.Trim() + "' order by coop_id, depttype_group, persongrp_code";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                typeCode = dt.GetString(0);
                HfDeptType.Value = typeCode;
                Hdpersong.Value = persong;

            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบประเภทเงินฝาก");
                //DwSaving.Reset();
                DwUpInt.Reset();
                //DwFix.Reset();
                //DwFixOther.Reset();
                //DwInterestBf.Reset();
                //DwInterestNew.Reset();
                return;
            }

            DwMain.Reset();
            //DwMain.Retrieve(state.SsCoopControl,typeCode);  รอการแก้ใขเฟม

            DwMain.Retrieve(state.SsCoopControl, typeCode);
            DwMain.SetItemString(1, "depttype_select", typeSelect);

            //SAVING
            //DwSaving.Reset();
            //DwSaving.Retrieve(state.SsCoopControl, typeCode);

            //Fix
            //DwFix.Reset();
            //DwFix.Retrieve(state.SsCoopControl, typeCode);

            //Filter DwUpint
            DwUpInt.Reset();
            DwUpInt.Retrieve(typeCode, state.SsCoopControl);

            //FixOther
            //DwFixOther.Reset();
            //DwFixOther.Retrieve(state.SsCoopControl, typeCode);

            //DwInterestBf
            //DwInterestBf.Reset();
            //DwInterestBf.Retrieve(state.SsCoopControl, typeCode);

            //DwInterestNew.Reset();
            //display = depService.GetIntDisplay(state.SsWsPass, typeCode);
            ndept.of_get_intdisplay(state.SsWsPass, typeCode,ref display); //new

            //DwInterestNew.ImportString(display, Sybase.DataWindow.FileSaveAsType.Xml);
            tDwInterestNew.Eng2ThaiAllRow();

        }

        private void BeforeUpdateDwUpInt()
        {
            string deptType = DwMain.GetItemString(1, "depttype_code");
            decimal seqNo = 0;
            for (int i = 1; i <= DwUpInt.RowCount; i++)
            {
                seqNo++;
                DwUpInt.SetItemString(i, "depttype_code", deptType);
                DwUpInt.SetItemDecimal(i, "seq_no", seqNo);
                DwUpInt.SetItemString(1, "coop_id", state.SsCoopControl);
            }
        }

        private void BeforeUpdateDwInterestBf()
        {
            string deptType = DwMain.GetItemString(1, "depttype_code");
            decimal seqNo = 0;
            //for (int i = 1; i <= DwInterestBf.RowCount; i++)
            //{
            //    seqNo++;
            //    DwInterestBf.SetItemString(i, "depttype_code", deptType);
            //    DwInterestBf.SetItemDecimal(i, "preseq_no", seqNo);
            //    DwInterestBf.SetItemString(i, "coop_id", state.SsCoopId);
            //}
        }

        private void AddRows()
        {
            DwUpInt.InsertRow(0);
            int rowAdd = DwUpInt.RowCount;
            DwUpInt.SetItemDecimal(rowAdd, "seq_no", rowAdd);
            DwUpInt.SetItemString(rowAdd, "depttype_code", typeCode);

        }
        //private void JspostRate()
        //{
        //    string xml_head = "";
        //    string xml_rate = "";
        //    xml_head = DwMain.Describe("DataWindow.Data.XML");

        //    try
        //    {
        //        DwMain.Reset();
        //        xml_rate = depService.InitIntRateOneDate(state.SsWsPass, xml_head);
        //        if (xml_rate != null && xml_rate != "")
        //        {
        //            DwMain.ImportString(xml_rate, Sybase.DataWindow.FileSaveAsType.Xml);
        //        }
        //    }
        //    catch (SoapException ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
        //    }
        //    catch (Exception ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
        //    }
        //}

        #region WebSheet Members

        public void InitJsPostBack()
        {
            // ชื่อตัวแปล กับ String argument ต้องเหมือนกัน !!!!
            changeType = WebUtil.JsPostBack(this, "changeType");
            postType = WebUtil.JsPostBack(this, "postType");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");
            postChange = WebUtil.JsPostBack(this, "postChange");
            bfUpdateDw = WebUtil.JsPostBack(this, "bfUpdateDw");
            FilterBookType = WebUtil.JsPostBack(this, "FilterBookType");
            postRate = WebUtil.JsPostBack(this, "postRate");
            delinterestrate = WebUtil.JsPostBack(this, "delinterestrate");
            postUpInt = WebUtil.JsPostBack(this, "postUpInt");
            //tDwInterestNew = new DwThDate(DwInterestNew);
            //tDwInterestNew.Add("use_date", "use_tdate");
        }

        public void WebSheetLoadBegin()
        {

            this.ConnectSQLCA();

            DwMain.SetTransaction(sqlca);
            //DwSaving.SetTransaction(sqlca);
            //DwFix.SetTransaction(sqlca);
            DwUpInt.SetTransaction(sqlca);
            //DwFixOther.SetTransaction(sqlca);
            //DwInterestBf.SetTransaction(sqlca);
            //DwInterestNew.SetTransaction(sqlca);
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            { }

            //DwInterestBf.SetTransaction(SQLCA);
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                WebUtil.RetrieveDDDW(DwMain, "depttype_select", "dpdepttypecond.pbl", state.SsCoopControl);
                WebUtil.RetrieveDDDW(DwMain, "persongrp_code", "dpdepttypecond.pbl", state.SsCoopControl);

            }
            else
            {
                this.RestoreContextDw(DwMain);
                //this.RestoreContextDw(DwSaving);
                //this.RestoreContextDw(DwFix);
                this.RestoreContextDw(DwUpInt);
                //this.RestoreContextDw(DwFixOther);
                //this.RestoreContextDw(DwInterestBf);
                //this.RestoreContextDw(DwInterestNew);

                //typeCode = DwMain.GetItemString(1, "depttype_select");
            }

            //DataWindowChild dcAccId = DwSaving.GetChild("chargebook_accid");
            //dcAccId.SetTransaction(sqlca);
            //dcAccId.Retrieve();
            //DataWindowChild dcCloseBfIntType = DwSaving.GetChild("closebf_inttype");
            //dcCloseBfIntType.SetTransaction(sqlca);
            //dcCloseBfIntType.Retrieve();
            //DataWindowChild dcBookGroup = DwSaving.GetChild("book_group");
            //dcBookGroup.SetTransaction(sqlca);
            //dcBookGroup.Retrieve();
            //book_group
            //closebf_inttype
            //typeCode = DwMain.GetItemString(1, "depttype_select");
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postType")
            {
                JsPostType();
            }
            else if (eventArg == "postAddRow")
            {
                AddRows();
            }
            else if (eventArg == "postChange")
            {
                JspostChange();
            }
            else if (eventArg == "bfUpdateDw")
            {
                BeforeUpdateDwInterestBf();
            }
            else if (eventArg == "FilterBookType")
            {
                JsFilterBookType();
            }
            else if (eventArg == "delinterestrate")
            {
                DelInterestRate();
            }
            //else if (eventArg == "postRate")
            //{
            //    JspostRate();
            //}
            else if (eventArg == "postUpInt")
            {
                JspostUpInt();
            }
        }

        private void JspostUpInt()
        {
            try { DwUpInt.Retrieve(Convert.ToInt16(HdMonth.Value), state.SsCoopControl); }
            catch { }
        }

        public void SaveWebSheet()
        {
            try
            {
                //DwMain.UpdateData();
                //DwSaving.UpdateData();
                //DwFix.UpdateData();

                //BeforeUpdateDwUpInt();
                //DwUpInt.UpdateData();
                //SaveUpInt();
                DwUtil.UpdateDataWindow(DwUpInt, "dpdepttypecond.pbl", "DPUPINTDATE");
                //DwFixOther.UpdateData();

                //BeforeUpdateDwInterestBf();
                //DwInterestBf.UpdateData();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            //HfDeptType.Value = typeCode;
            
            DwMain.SaveDataCache();
            //DwFix.SaveDataCache();
            //DwInterestNew.SaveDataCache();
            //DwFixOther.SaveDataCache();
            //DwSaving.SaveDataCache();
            DwUpInt.SaveDataCache();
            //DwInterestBf.SaveDataCache();
            JsFilterBookType();

            //try
            //{
            //    //DwSaving.SetFilter("depttype_code = '" + typeCode + "'");
            //    //DwSaving.Filter();

            //    //DwFix.SetFilter("depttype_code = '" + typeCode + "'");
            //    //DwFix.Filter();

            //    //DwFixOther.SetFilter("depttype_code = '" + typeCode + "'");
            //    //DwFixOther.Filter();

            //    //DwUpInt.SetFilter("depttype_code = '" + typeCode + "'");
            //    //DwUpInt.Filter();


            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //}
        }

        /// <summary>
        /// bank -  วันที่จ่าย Int./Div. แก้ไขการเซฟ
        /// </summary>
        private void SaveUpInt()
        {
            Decimal prncbal_min, prncbal_max, upint_date, upint_month, calint_to;
            int row = DwUpInt.RowCount;
            String deptType = DwMain.GetItemString(1, "depttype_code");
            String sql = "";

            sql = @"DELETE FROM DPUPINTDATE WHERE DEPTTYPE_CODE='" + deptType + "' and coop_id = '" + state.SsCoopControl + "'";
            WebUtil.Query(sql);

            for (int i = 1; i <= row; i++)
            {

                try { prncbal_min = DwUpInt.GetItemDecimal(i, "prncbal_min"); }
                catch { prncbal_min = 0; }
                try { prncbal_max = DwUpInt.GetItemDecimal(i, "prncbal_max"); }
                catch { prncbal_max = 0; }
                try { upint_date = DwUpInt.GetItemDecimal(i, "upint_date"); }
                catch { upint_date = 0; }
                try { upint_month = DwUpInt.GetItemDecimal(i, "upint_month"); }
                catch { upint_month = 0; }
                try { calint_to = DwUpInt.GetItemDecimal(i, "calint_to"); }
                catch { calint_to = 0; }

                sql = @"insert into DPUPINTDATE
	 	    (DEPTTYPE_CODE,		                PRNCBAL_MIN	, 	            PRNCBAL_MAX ,			    UPINT_DATE , 
             COOP_ID ,                          SEQ_NO,                     UPINT_MONTH,                CALINT_TO) 
            values
            ('" + deptType + "' , 	            " + prncbal_min + " ,       " + prncbal_max + ", 	    " + upint_date + @",
             '" + state.SsCoopControl + "',     " + i + ",                  " + upint_month + ",        " + calint_to + ")";
                WebUtil.Query(sql);
            }
            ExecuteDataSource exe = new ExecuteDataSource(this);
            exe.Execute();

        }
        #endregion
        public void DelInterestRate()
        {
            int display;
            string sdisplay="",depttype_code = "";
           /// DwInterestNew.Reset();
            try
            {
                depttype_code = DwMain.GetItemString(1, "depttype_select_1");
                Int32 row = Convert.ToInt32(HdRow.Value.ToString());
                DateTime dtm = new DateTime();
                String tdtm = "";
                try
                {
                    //dtm = DwInterestNew.GetItemDateTime(row, "use_date");
                    //tdtm = DwInterestNew.GetItemString(row, "use_tdate");
                }
                catch { }
                //doys-exception
                //display = depService.of_datete_int_rate(state.SsWsPass,depttype_code , dtm);
                //DwInterestNew.Reset();
                //sdisplay = depService.GetIntDisplay(state.SsWsPass, depttype_code);
                
                ///comment by cherry 
                //ndept.of_get_intdisplay(state.SsWsPass, depttype_code,ref sdisplay); //new
                //DwInterestNew.ImportString(sdisplay, Sybase.DataWindow.FileSaveAsType.Xml);
                try
                {
                    String sqdel_interest = @"delete dpdeptintrate where depttype_code = '" + depttype_code + @"' 
                                        and effective_date = to_date('" + dtm.ToString("dd/MM/yyyy") + "','dd/MM/yyyy')";
                    Sdt ta = WebUtil.QuerySdt(sqdel_interest);
                    JsPostType();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ได้ทำการลบกำหนดอัตรา Int./Div. วันที่" + tdtm);
                }
                catch
                { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการลบข้อมูล"); }

            }
            catch { }
        }
    }
}