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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;
using Sybase.DataWindow;
using System.Data.OracleClient;

namespace Saving.Criteria
{
    public partial class u_cri_date_rmembno_excel : PageWebSheet, WebSheet
    {
        private n_commonClient commonService;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postSetStartMemberno;
        protected String postSetEndMemberno;

        private DwThDate tdw_criteria;
        #region WebSheet Members

        //===================================
        private void JspostNewClear()
        {
            //default values.
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
            tdw_criteria.Eng2ThaiAllRow();

            string[] minmax = ReportUtil.GetMinMaxMembno();
            dw_criteria.SetItemString(1, "start_membno", minmax[0]);
            dw_criteria.SetItemString(1, "end_membno", minmax[1]);
        }

        private void JspostSetEndMemberno()
        {
            String membNo = dw_criteria.GetItemString(1, "end_membno");
            membNo = WebUtil.MemberNoFormat(membNo);
            dw_criteria.SetItemString(1, "end_membno", membNo);
        }

        private void JspostSetStartMemberno()
        {
            String membNo = dw_criteria.GetItemString(1, "start_membno");
            membNo = WebUtil.MemberNoFormat(membNo);
            dw_criteria.SetItemString(1, "start_membno", membNo);
        }

        public void InitJsPostBack()
        {
            postSetStartMemberno = WebUtil.JsPostBack(this, "postSetStartMemberno");
            postSetEndMemberno = WebUtil.JsPostBack(this, "postSetEndMemberno");
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
	
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("start_date", "start_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try 
            {
                 commonService = wcf.NCommon; 
            }
            catch { LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ WebService ไม่ได้"); }
            
            InitJsPostBack();
           
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                JspostNewClear();   
            }
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
            else if (eventArg == "postSetStartMemberno")
            {
                JspostSetStartMemberno();
            }

            else if (eventArg == "postSetEndMemberno")
            {
                JspostSetEndMemberno();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
            if (dw_criteria.RowCount > 1)
            {
                dw_criteria.DeleteRow(dw_criteria.RowCount);
            }
        }

        #endregion
        #region Report Process

        private void RunProcess()
        {
            //ออก Report เป็น Excel
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String start_member = dw_criteria.GetItemString(1, "start_membno").Trim();
            String end_member = dw_criteria.GetItemString(1, "end_membno").Trim();
            String report_id = dw_criteria.GetItemString(1, "report_id");
            if (report_id == "1")
            {
                try
                {
                    String filename = "SlipConfirm_Memberno" + ".xls";
                    str_rptexcel astr_rptexcel = new str_rptexcel();
                    astr_rptexcel.as_path = WebUtil.PhysicalPath + "Saving//filecommon//" + filename;
                    astr_rptexcel.as_dwobject = "r_shlnproc_confirmbal_mem";
                    astr_rptexcel.as_argument01 = "D" + start_date;
                    astr_rptexcel.as_argument02 = "S" + start_member;
                    astr_rptexcel.as_argument03 = "S" + end_member;
                    int result = commonService.of_dwexportexcel_rpt(state.SsWsPass,ref astr_rptexcel);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่  <a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายงานตามเงื่อนไขที่ระบุ กรุุณาทำรายการใหม่");
                    JspostNewClear();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else
            {
                try
                {
                    String filename = "SlipConfirm_GroupMemberno" + ".xls";
                    str_rptexcel astr_rptexcel = new str_rptexcel();
                    astr_rptexcel.as_path = WebUtil.PhysicalPath + "Saving//filecommon//" + filename;
                    astr_rptexcel.as_dwobject = "r_shlnproc_confirmbal_mem_grp";
                    astr_rptexcel.as_argument01 = "D" + start_date;
                    astr_rptexcel.as_argument02 = "S" + start_member;
                    astr_rptexcel.as_argument03 = "S" + end_member;
                    astr_rptexcel.as_dwobject = "r_shlnproc_confirmbal_mem_grp";
                    int result = commonService.of_dwexportexcel_rpt(state.SsWsPass,ref astr_rptexcel);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่  <a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายงานตามเงื่อนไขที่ระบุ กรุุณาทำรายการใหม่");
                    JspostNewClear();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        public void PopupReport()
        {

        }
        #endregion

        
    }
}
