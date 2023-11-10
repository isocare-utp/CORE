using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport
{
    public partial class u_cri_rperiod_membgroup_export : PageWebReport, WebReport
    {
        
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String post;
        protected String postShowReport;

        #region WebSheet Members

                
       
        public void InitJsPostBack()
        {
          
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            post = WebUtil.JsPostBack(this, "post");
            postShowReport = WebUtil.JsPostBack(this, "postShowReport");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
           
            //InitJsPostBack();
            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup", "criteria.pbl", null);
            
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                int year, month;

                dw_criteria.InsertRow(0);
                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "start_membgroup", minmax[0]);
                dw_criteria.SetItemString(1, "end_membgroup", minmax[1]);


                String kp_month = Convert.ToString(DateTime.Now.Month);
                if(kp_month.Length != 2)
                {
                    kp_month = "0" + kp_month;
                }

                dw_criteria.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
                dw_criteria.SetItemString(1, "month", kp_month);
        
                               
            }

           

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "post")
            {
                DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup_1", "criteria.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup_1", "criteria.pbl", null);
            }
            else if (eventArg == "postShowReport")
            {
               
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
        #region Report Process
        public void RunReport()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String year = dw_criteria.GetItemString(1, "year");
            String month = dw_criteria.GetItemString(1, "month");
            String period = year + month;
            //string repportname = "รายงานการเรียกเก็บประจำเดือน" + month + "  ปี พ.ศ. " + year;//ir_keep_save_disk_surin
            string repportname = "ir_keep_save_disk_surin";//ir_keep_save_disk_surin
            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_period", iReportArgumentType.String, period);
                arg.Add("as_start_membgroup", iReportArgumentType.String, start_membgroup);
                arg.Add("as_end_membgroup", iReportArgumentType.String, end_membgroup);
                iReportBuider report = new iReportBuider(this, arg, repportname);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String year = dw_criteria.GetItemString(1, "year");
            String month = dw_criteria.GetItemString(1, "month");
            String period = year + month;
            //string repportname = "รายงานการเรียกเก็บประจำเดือน" + month + "  ปี พ.ศ. " + year;//ir_keep_save_disk_surin
            string repportname = "ir_keep_save_disk_surin";//ir_keep_save_disk_surin
            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_period", iReportArgumentType.String, period);
                arg.Add("as_start_membgroup", iReportArgumentType.String, start_membgroup);                
                arg.Add("as_end_membgroup", iReportArgumentType.String, end_membgroup);
                iReportBuider report = new iReportBuider(this, arg, repportname);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
         
       
        #endregion

    }
}
