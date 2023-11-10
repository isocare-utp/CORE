﻿using System;
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
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Criteria
{
    public partial class u_cri_rmembgrpctr_rmembgrp_rmembno : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        //private DwThDate tdw_criteria;
        protected String checkFlag;
        protected String checkFlag1;
        protected String checkFlag2;
        protected String checkFlag3;
        protected String jsRefresh;
        protected String jsRetreivemidgroup;
        protected String jsRetreivemidgroup2;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            checkFlag1 = WebUtil.JsPostBack(this, "checkFlag1");
            checkFlag2 = WebUtil.JsPostBack(this, "checkFlag2");
            checkFlag3 = WebUtil.JsPostBack(this, "checkFlag3");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsRetreivemidgroup = WebUtil.JsPostBack(this, "jsRetreivemidgroup");
            jsRetreivemidgroup2 = WebUtil.JsPostBack(this, "jsRetreivemidgroup2");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "membgroup_1", "criteria.pbl", state.SsCoopControl, 1);
            if (!IsPostBack)
            {
                string[] minmax = new string[2];
                DataTable dt = WebUtil.Query("select min(membgroup_code) as min, max(membgroup_code) as max from mbucfmembgroup where coop_id ='" + state.SsCoopControl + "' and membgroup_level = 1");
                if (dt.Rows.Count > 0) { minmax = new string[2] { (dt.Rows[0][0]).ToString(), (dt.Rows[0][1]).ToString() }; }
                else { minmax = null; }
                string[] minmax2 = ReportUtil.GetMinMaxMembgroupCoopid(state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "membgroup_2", "criteria.pbl", state.SsCoopControl, minmax[0]);
                //DwUtil.RetrieveDDDW(dw_criteria, "membgroup_3", "criteria.pbl", state.SsCoopControl, minmax2[0]);
                //default values.
                dw_criteria.InsertRow(0);
                string[] minmax3 = new string[2];
                DataTable dt3 = WebUtil.Query("select min(membgroup_code) as min, max(membgroup_code) as max from mbucfmembgroup where coop_id ='" + state.SsCoopControl + "' and membgroup_level = 3");
                if (dt3.Rows.Count > 0) { minmax3 = new string[2] { (dt3.Rows[0][0]).ToString(), (dt3.Rows[0][1]).ToString() }; }
                else { minmax3 = null; }
                dw_criteria.SetItemString(1, "membgroup_1", minmax[0]);
                dw_criteria.SetItemString(1, "membgroup_2", minmax2[0]);
                //dw_criteria.SetItemString(1, "membgroup_3", minmax3[0]);
                dw_criteria.SetItemString(1, "start_membno", "00000000");
                dw_criteria.SetItemString(1, "end_membno", "99999999");
                //tdw_criteria.Eng2ThaiAllRow();
            }
            else
            {
                dw_criteria.RestoreContext();
                //JsRetreivemidgroup2();
            }

            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "checkFlag")
            {
                CheckFlag();
            }
            else if (eventArg == "checkFlag1")
            {
                CheckFlag1();
            }
            else if (eventArg == "checkFlag2")
            {
                CheckFlag2();
            }
            else if (eventArg == "checkFlag3")
            {
                CheckFlag3();
            }
            else if (eventArg == "jsRefresh")
            {
                JsRefresh();
            }
            else if (eventArg == "jsRetreivemidgroup")
            {
                JsRetreivemidgroup();
            }
            else if (eventArg == "jsRetreivemidgroup2")
            {
                JsRetreivemidgroup2();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
        private void CheckFlag()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_startgroup", HdStGroup.Value);
        }
        private void CheckFlag1()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_endgroup", HdEnGroup.Value);
        }
        private void CheckFlag2()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "start_membno", HdStMem.Value);
        }
        private void CheckFlag3()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "end_membno", HdEnMem.Value);
        }
        private void JsRefresh()
        {
            dw_criteria.Reset();
            decimal add_type = dw_criteria.GetItemDecimal(1, "add_type");
            if (add_type == 1)
            {
                dw_criteria.DataWindowObject = "d_mb_addgroup_detail_level1";
                DwUtil.RetrieveDataWindow(dw_criteria, "mb_add_newgroup.pbl", null, state.SsCoopControl);
            }
            else
            {
                dw_criteria.DataWindowObject = "d_mb_addgroup_detail";

                if (add_type == 2)
                {
                    DwUtil.RetrieveDDDW(dw_criteria, "membgroup_1", "mb_add_newgroup.pbl", state.SsCoopControl, 1);
                }
                else if (add_type == 3)
                {
                    DwUtil.RetrieveDDDW(dw_criteria, "membgroup_1", "mb_add_newgroup.pbl", state.SsCoopControl, 1);
                    //string membgroup_code = dw_criteria.GetItemString(1, "membgroup_1");
                    //DwUtil.RetrieveDDDW(dw_criteria, "membgroup_1", "mb_add_newgroup.pbl", state.SsCoopControl, membgroup_code);
                }
            }
        }
        private void JsRetreivemidgroup()
        {
            //ดึงรหัสคณะจากหน้าจอ Criteria มาเกก็บไว้ในตัวแปร membgroup_code1
            String membgroup_code1 = dw_criteria.GetItemString(1, "membgroup_1");
            //ดึงข้อมูลของภาควิชา มาแสดงที่หน้าจอ Criteria โดยกำหนดเงื่อนไขจาก รหัสสาขา และ รหัสคณะ
            DwUtil.RetrieveDDDW(dw_criteria, "membgroup_2", "criteria.pbl", state.SsCoopControl, membgroup_code1);
            string[] membgroup_code2 = new string[2];
            DataTable dt = WebUtil.Query("select min(membgroup_code) as min, max(membgroup_code) as max from mbucfmembgroup where coop_id ='" + state.SsCoopControl + "'and ( MBUCFMEMBGROUP.MEMBGROUP_CONTROL = '" + membgroup_code1 + "' ) and membgroup_level = 2");
            if (dt.Rows.Count > 0) { membgroup_code2 = new string[2] { (dt.Rows[0][0]).ToString(), (dt.Rows[0][1]).ToString() }; }
            else { membgroup_code2 = null; }
            //เซตค่าของรหัสภาควิชา ให้กับหน้าจอ Criteria
            dw_criteria.SetItemString(1, "membgroup_2", membgroup_code2[0]);
            //ดึงข้อมูลของแผนก มาแสดงที่หน้าจอ Criteria โดยกำหนดเงื่อนไขจาก รหัสสาขา และ รหัสภาควิชา
            //DwUtil.RetrieveDDDW(dw_criteria, "membgroup_3", "criteria.pbl", state.SsCoopControl, membgroup_code2[0]);
            //string[] membgroup_code3 = new string[2];
            //DataTable dt2 = WebUtil.Query("select min(membgroup_code) as min, max(membgroup_code) as max from mbucfmembgroup where coop_id ='" + state.SsCoopControl + "'and ( MBUCFMEMBGROUP.MEMBGROUP_CONTROL = '" + membgroup_code2[0] + "' ) and membgroup_level = 3");
            //if (dt2.Rows.Count > 0)
            //{
            //    membgroup_code3 = new string[2] { (dt2.Rows[0][0]).ToString(), (dt2.Rows[0][1]).ToString() };
            //}
            //else { membgroup_code3 = null; }
            ////เซตค่าของรหัสแผนก ให้กับหน้าจอ Criteria
            //dw_criteria.SetItemString(1, "membgroup_3", membgroup_code3[0]);
        }
        private void JsRetreivemidgroup2()
        {
            //ดึงรหัสคณะจากหน้าจอ Criteria มาเกก็บไว้ในตัวแปร membgroup_code1
            String membgroup_code1 = dw_criteria.GetItemString(1, "membgroup_1");
            //ดึงข้อมูลของภาควิชา มาแสดงที่หน้าจอ Criteria โดยกำหนดเงื่อนไขจาก รหัสสาขา และ รหัสคณะ
            DwUtil.RetrieveDDDW(dw_criteria, "membgroup_2", "criteria.pbl", state.SsCoopControl, membgroup_code1);
            //ดึงรหัสภาควิชาจากหน้าจอ Criteria มาเกก็บไว้ในตัวแปร membgroup_code2
            String membgroup_code2 = dw_criteria.GetItemString(1, "membgroup_2");
            //ดึงข้อมูลของแผนก มาแสดงที่หน้าจอ Criteria โดยกำหนดเงื่อนไขจาก รหัสสาขา และ รหัสภาควิชา
            //DwUtil.RetrieveDDDW(dw_criteria, "membgroup_3", "criteria.pbl", state.SsCoopControl, membgroup_code2);
            //string[] membgroup_code3 = new string[2];
            //DataTable dt = WebUtil.Query("select min(membgroup_code) as min, max(membgroup_code) as max from mbucfmembgroup where coop_id ='" + state.SsCoopControl + "'and ( MBUCFMEMBGROUP.MEMBGROUP_CONTROL = '" + membgroup_code2 + "' ) and membgroup_level = 3");
            //if (dt.Rows.Count > 0)
            //{
            //    membgroup_code3 = new string[2] { (dt.Rows[0][0]).ToString(), (dt.Rows[0][1]).ToString() };
            //}
            //else { membgroup_code3 = null; }
            //ดึงรหัสภาควิชาจากหน้าจอ Criteria มาเกก็บไว้ในตัวแปร membgroup_code2
            //String membgroup_code3 = dw_criteria.GetItemString(1, "membgroup_3");
            //เซตค่าของรหัสภาควิชา ให้กับหน้าจอ Criteria
            dw_criteria.SetItemString(1, "membgroup_2", membgroup_code2);
            //เซตค่าของรหัสแผนก ให้กับหน้าจอ Criteria
            //dw_criteria.SetItemString(1, "membgroup_3", membgroup_code3);
        }
        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_membgrpctr = dw_criteria.GetItemString(1, "membgroup_1");
            String end_membgrpctr = dw_criteria.GetItemString(1, "membgroup_1");
            String start_membgrp = dw_criteria.GetItemString(1, "membgroup_2");
            String end_membgrp = dw_criteria.GetItemString(1, "membgroup_2");
            //String start_membsec = dw_criteria.GetItemString(1, "membgroup_3");
            //String end_membsec = dw_criteria.GetItemString(1, "membgroup_3");
            String start_membno = dw_criteria.GetItemString(1, "start_membno");
            String end_membno = dw_criteria.GetItemString(1, "end_membno");
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(start_membgrpctr, ArgumentType.String);
            lnv_helper.AddArgument(end_membgrpctr, ArgumentType.String);
            lnv_helper.AddArgument(start_membgrp, ArgumentType.String);
            lnv_helper.AddArgument(end_membgrp, ArgumentType.String);
            //lnv_helper.AddArgument(start_membsec, ArgumentType.String);
            //lnv_helper.AddArgument(end_membsec, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            //----------------------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
        }
        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
        private string Convert(string p)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
