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
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;
namespace Saving.Criteria
{
    public partial class u_cri_criteria_all : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String exportexcel;
        protected String previewReport;
        protected String popupReport;

        public String outputProcess;

        private DwThDate tdw_criteria;
        private n_commonClient commonService;
        string report_name = "";
        string dwobjectpreview_name = "";
        string report_id = "";
        string dwobject_name = "";
        int count_dwfil = 0;
        int count_dwdatefil = 0;
        int count_dwretrievefil = 0;
        int usedate_flag = 0;
        int useretrieve_flag = 0;
        string colume_name = "";
        string colume_type = "";
        string colume_defualt = "";
        string columedateeng_name = "";
        string columedatethai_name = "";
        string columedate_defualt = "";
        Sdt dt = new Sdt();
        Sdt dt1 = new Sdt();
        Sdt dt2 = new Sdt();
        Sdt dt3 = new Sdt();
        Sdt dt4 = new Sdt();
        Sdt dt5 = new Sdt();
        Sdt dt6 = new Sdt();
        Sdt dt7 = new Sdt();
        Sdt dt8 = new Sdt();
        string addarg_string = "";
        decimal addarg_decimal = 0;
        string addarg_datetime;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            tdw_criteria = new DwThDate(dw_criteria, this);
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            exportexcel = WebUtil.JsPostBack(this, "exportexcel");
            previewReport = WebUtil.JsPostBack(this, "previewReport");

            if (usedate_flag == 1)
            {
                tdw_criteria = new DwThDate(dw_criteria, this);
                for (int k = 0; k < count_dwdatefil; k++)
                {
                    columedateeng_name = dt5.Rows[k]["dwcoldateeng_name"].ToString();
                    columedatethai_name = dt5.Rows[k]["dwcoldatethai_name"].ToString();
                    tdw_criteria.Add(columedateeng_name, columedatethai_name);

                }
            }

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_preview.SetTransaction(sqlca);

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

            Sta ta = new Sta(state.SsConnectionString);
            string sql = "";
            sql = @"SELECT REPORT_NAME,REPORT_ID
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";

            dt = ta.Query(sql);
            dt.Next();
            report_name = dt.GetString("REPORT_NAME");
            report_id = dt.GetString("REPORT_ID");

            string sql1 = @"select dwobject_name,dwobjectpreview_name from crireportid where report_id ='" + report_id + "'";
            dt1 = ta.Query(sql1);
            dt1.Next();
            dwobject_name = dt1.GetString("dwobject_name");
            dwobjectpreview_name = dt1.GetString("dwobjectpreview_name");
            //หาชื่อ datawindows
            string sql2 = "";
            sql2 = @"select usedate_flag,useretrieve_flag from cridwobjectall where dwobject_name ='" + dwobject_name + "'";
            dt2 = ta.Query(sql2);
            dt2.Next();

            usedate_flag = dt2.GetInt32("usedate_flag");
            useretrieve_flag = dt2.GetInt32("useretrieve_flag");


            dw_criteria.DataWindowObject = dwobject_name;

            //หาจำนวนฟิลส์ datawindows
            string sql3 = @"select count(dwobject_name)as count_dwfil from cricolumedetail where dwobject_name ='" + dwobject_name + "'";
            dt3 = ta.Query(sql3);
            dt3.Next();
            count_dwfil = dt3.GetInt32("count_dwfil");

            string sql4 = @"select seq_no ,dwcol_name,dwcol_type,dwcol_default from cricolumedetail where dwobject_name ='" + dwobject_name + "' order by seq_no";
            dt4 = ta.Query(sql4);
            dt4.Next();

            if (usedate_flag == 1)
            {
                string sql5 = @"select seq_no ,dwcoldateeng_name,dwcoldatethai_name,dwcoldate_default from cricoldate where dwobject_name ='" + dwobject_name + "' order by seq_no";
                dt5 = ta.Query(sql5);
                dt5.Next();

                string sql6 = @"select count(dwobject_name)as count_dwobject_name from cricoldate where dwobject_name ='" + dwobject_name + "'";
                dt6 = ta.Query(sql6);
                dt6.Next();
                count_dwdatefil = dt6.GetInt32("count_dwobject_name");

            }

            if (useretrieve_flag == 1)
            {
                string sql7 = @"select seq_no ,dwcolretrieve_name,dwcolretrieve_path from criretrievedddw where dwobject_name ='" + dwobject_name + "' order by seq_no";
                dt7 = ta.Query(sql7);
                dt7.Next();

                string sql8 = @"select count(dwobject_name)as count_dwobject_name from criretrievedddw where dwobject_name ='" + dwobject_name + "'";
                dt8 = ta.Query(sql8);
                dt8.Next();
                count_dwretrievefil = dt8.GetInt32("count_dwobject_name");

                for (int l = 0; l < count_dwretrievefil; l++)
                {
                    DwUtil.RetrieveDDDW(dw_criteria, dt7.Rows[l]["dwcolretrieve_name"].ToString(), dt7.Rows[l]["dwcolretrieve_path"].ToString(), null);
                }

            }

            InitJsPostBack();
            commonService = wcf.NCommon;
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
                this.RestoreContextDw(dw_preview);
                //dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);

                if (usedate_flag == 1)
                {
                    for (int k = 0; k < count_dwdatefil; k++)
                    {

                        columedateeng_name = dt5.Rows[k]["dwcoldateeng_name"].ToString();
                        columedate_defualt = dt5.Rows[k]["dwcoldate_default"].ToString();
                        dw_criteria.SetItemDateTime(1, columedateeng_name, state.SsWorkDate);

                    }
                }


                for (int i = 0; i < count_dwfil; i++)
                {
                    colume_name = dt4.Rows[i]["dwcol_name"].ToString();
                    colume_type = dt4.Rows[i]["dwcol_type"].ToString();
                    colume_defualt = dt4.Rows[i]["dwcol_default"].ToString();

                    if (colume_type == "string")
                    {
                        dw_criteria.SetItemString(1, colume_name, colume_defualt);
                    }
                    if (colume_type == "decimal")
                    {
                        dw_criteria.SetItemDecimal(1, colume_name, Convert.ToDecimal(colume_defualt));
                    }

                    if (colume_type == "datetime")
                    {
                        dw_criteria.SetItemString(1, colume_name, colume_defualt);
                    }


                }

                tdw_criteria.Eng2ThaiAllRow();
            }



            //Report Name.
            try
            {

                ReportName.Text = report_name;

            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            ta.Close();
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
            else if (eventArg == "exportexcel")
            {
                Exportexcel();
            }
            else if (eventArg == "previewReport")
            {
                PreviewReport();
            }
        }

        private void Exportexcel()
        {
            try
            {
                string[] argexcel_value =new string[20];
            
            String filename = "report" + dwobjectpreview_name + ".xls";
            str_rptexcel astr_rptexcel = new str_rptexcel();
            astr_rptexcel.as_path = WebUtil.PhysicalPath + "Saving//filecommon//" + filename;
            astr_rptexcel.as_dwobject = dwobjectpreview_name;
           // String agm = "astr_rptexcel.as_argument0";
            String c_type = "";
            for (int i = 0; i < count_dwfil; i++)
            {
                colume_name = dt4.Rows[i]["dwcol_name"].ToString();
                colume_type = dt4.Rows[i]["dwcol_type"].ToString();

               // agm = agm + i.ToString();

                if (colume_type == "string")
                {
                    c_type = "S";
                    addarg_string = dw_criteria.GetItemString(1, colume_name);
                    argexcel_value[i] = c_type + addarg_string;
                }
                if (colume_type == "decimal")
                {
                    c_type = "I";
                    addarg_decimal = dw_criteria.GetItemDecimal(1, colume_name);
                    argexcel_value[i] = c_type + addarg_decimal.ToString();
                }
                if (colume_type == "datetime")
                {
                    c_type = "D";
                    addarg_datetime = WebUtil.ConvertDateThaiToEng(dw_criteria, colume_name, null);
                    argexcel_value[i] = c_type + addarg_datetime;

                }
                
            }

            if (count_dwfil == 1)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
               
            }
            if (count_dwfil == 2)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
            }
            if (count_dwfil == 3)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2];
                
            }
            if (count_dwfil == 4)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2];
                astr_rptexcel.as_argument04 = argexcel_value[3];
               
            }
            if (count_dwfil == 5)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2];
                astr_rptexcel.as_argument04 = argexcel_value[3];
                astr_rptexcel.as_argument05 = argexcel_value[4];
               
            }
            if (count_dwfil == 6)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2]; 
                astr_rptexcel.as_argument04 = argexcel_value[3];
                astr_rptexcel.as_argument05 = argexcel_value[4];
                astr_rptexcel.as_argument06 = argexcel_value[5];
            }
            if (count_dwfil == 7)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2]; 
                astr_rptexcel.as_argument04 = argexcel_value[3];
                astr_rptexcel.as_argument05 = argexcel_value[4];
                astr_rptexcel.as_argument06 = argexcel_value[5];
                astr_rptexcel.as_argument07 = argexcel_value[6];
               
            }
            if (count_dwfil == 8)
            {
                astr_rptexcel.as_argument01 = argexcel_value[0];
                astr_rptexcel.as_argument02 = argexcel_value[1];
                astr_rptexcel.as_argument03 = argexcel_value[2];
                astr_rptexcel.as_argument04 = argexcel_value[3];
                astr_rptexcel.as_argument05 = argexcel_value[4];
                astr_rptexcel.as_argument06 = argexcel_value[5];
                astr_rptexcel.as_argument07 = argexcel_value[6];
                astr_rptexcel.as_argument08 = argexcel_value[7];
               
            }
            
                int result = commonService.of_dwexportexcel_rpt(state.SsWsPass,ref astr_rptexcel);
                LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่  <a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");

            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }


        }

        private void PreviewReport()
        {
            dw_preview.DataWindowObject = dwobjectpreview_name;
            dw_preview.SetTransaction(sqlca);
            object[] arg_retrieve = new object[10];
            for (int j = 0; j < count_dwfil; j++)
            {
                colume_name = dt4.Rows[j]["dwcol_name"].ToString();
                colume_type = dt4.Rows[j]["dwcol_type"].ToString();
                colume_defualt = dt4.Rows[j]["dwcol_default"].ToString();
                if (colume_type == "string")
                {
                    addarg_string = dw_criteria.GetItemString(1, colume_name);
                    arg_retrieve[j] = addarg_string;

                }
                if (colume_type == "decimal")
                {
                    addarg_decimal = dw_criteria.GetItemDecimal(1, colume_name);
                    arg_retrieve[j] = addarg_decimal;

                }
                if (colume_type == "datetime")
                {
                    // addarg_datetime = dw_criteria.GetItemString(1, colume_name);
                    // addarg_datetime = WebUtil.ConvertDateThaiToEng(dw_criteria, colume_name, null);

                    string tdate = dw_criteria.GetItemString(1, colume_name);
                    DateTime reqdate;
                    reqdate = DateTime.ParseExact(tdate, "ddMMyyyy", WebUtil.TH);
                    arg_retrieve[j] = reqdate;

                }
            }
            if (count_dwfil == 1)
            {
                dw_preview.Retrieve(arg_retrieve[0]);
            }
            if (count_dwfil == 2)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1]);
            }
            if (count_dwfil == 3)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2]);
            }
            if (count_dwfil == 4)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2], arg_retrieve[3]);
            }
            if (count_dwfil == 5)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2], arg_retrieve[3],arg_retrieve[4]);
            }
            if (count_dwfil == 6)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2], arg_retrieve[3],arg_retrieve[4], arg_retrieve[5]);
            }
            if (count_dwfil == 7)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2], arg_retrieve[3],arg_retrieve[4], arg_retrieve[5],arg_retrieve[6]);
            }
            if (count_dwfil == 8)
            {
                dw_preview.Retrieve(arg_retrieve[0], arg_retrieve[1], arg_retrieve[2], arg_retrieve[3],arg_retrieve[4], arg_retrieve[5],arg_retrieve[6], arg_retrieve[7]);
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
            dw_preview.SaveDataCache();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.  //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            for (int j = 0; j < count_dwfil; j++)
            {
                colume_name = dt4.Rows[j]["dwcol_name"].ToString();
                colume_type = dt4.Rows[j]["dwcol_type"].ToString();
                colume_defualt = dt4.Rows[j]["dwcol_default"].ToString();
                if (colume_type == "string")
                {
                    addarg_string = dw_criteria.GetItemString(1, colume_name);
                    lnv_helper.AddArgument(addarg_string, ArgumentType.String);
                }
                if (colume_type == "decimal")
                {
                    addarg_decimal = dw_criteria.GetItemDecimal(1, colume_name);
                    lnv_helper.AddArgument(addarg_decimal.ToString(), ArgumentType.Number);
                }
                if (colume_type == "datetime")
                {

                    addarg_datetime = WebUtil.ConvertDateThaiToEng(dw_criteria, colume_name, null);
                    lnv_helper.AddArgument(addarg_datetime, ArgumentType.DateTime);
                }
            }


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
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
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
        #endregion
    }
}
