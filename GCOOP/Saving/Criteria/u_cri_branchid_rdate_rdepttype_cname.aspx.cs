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

namespace Saving.Criteria
{
    public partial class u_cri_branchid_rdate_rdepttype_cname : PageWebSheet, WebSheet
    {
        private String app;
        private String gid;
        private String rid;
        private DwThDate tdw_criteria;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();

            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "start_type", "00");
                dw_criteria.SetItemString(1, "end_type", "99");
                dw_criteria.SetItemString(1, "coop_name", "สหกรณ์ออมทรัพย์การสื่อสารแห่งประเทศไทย จำกัด");
                dw_criteria.SetItemString(1, "branch_id", "000");
                tdw_criteria.Eng2ThaiAllRow();
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
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        protected void LinkNext_Click(object sender, EventArgs e)
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            DateTime startDate = dw_criteria.GetItemDateTime(1,"start_date");
            DateTime endDate = dw_criteria.GetItemDateTime(1, "end_date");
            String startType = dw_criteria.GetItemString(1, "start_type");
            String endType = dw_criteria.GetItemString(1, "end_type");
            String coopName = dw_criteria.GetItemString(1, "coop_name");
            String branchID = dw_criteria.GetItemString(1, "branch_id");
            String coop_name = state.SsCoopName;
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(branchID, ArgumentType.String);

            lnv_helper.AddArgument(startDate.ToString("yyyy-MM-dd",WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(endDate.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(startType, ArgumentType.String);
            lnv_helper.AddArgument(endType, ArgumentType.String);
            lnv_helper.AddArgument(coop_name, ArgumentType.String);
            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_"+gid+"_"+rid+".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            String pdfURL = "";
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //int li_return = lws_report.ReportPDF(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                /*LtServerMessage.Text = "<b>ReportPDF</b> return(" + Convert.ToString(li_return) + @") <br>
                <b>criteria:</b><code> " + criteriaXML + @"</code><br>
                <b>app:</b> " + app + @"<br>
                <b>gid:</b>" + gid + @"<br>
                <b>rid:</b>" + rid + @"<br>
                <b>pdf:</b>" + pdfFileName;*/
                //if (1 != li_return)
                //{
                //    //throw new Exception("สร้างรายงานไม่สำเร็จ");
                //}
                //pdfURL = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            //LtServerMessage.Text = "PDF Created: <a href='"+pdfURL+"'>"+pdfURL+"</a>";
            
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('"+pdfURL+"')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
    }
}
