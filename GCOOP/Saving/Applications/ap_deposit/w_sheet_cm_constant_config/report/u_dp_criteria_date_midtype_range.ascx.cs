using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sybase.DataWindow;
using CommonLibrary;

namespace Saving.Applications.ap_deposit.report
{
    public partial class u_dp_criteria_date_midtype_range : System.Web.UI.UserControl
    {
        private DwTrans sqlca;
        private WebState state;
        private DwThDate tDwData;

        protected String getData;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_data.SetTransaction(sqlca);
            String as_branch;
            try
            {
                as_branch = dw_data.GetItemString(1, "branch_id");
            }
            catch { as_branch = ""; }
            if (as_branch == "")
            {
                PnReport.Visible = false;
                dw_data.InsertRow(0);
                DataWindowChild c_branch_id = dw_data.GetChild("branch_id");
                DataWindowChild c_start_dp_type = dw_data.GetChild("start_dp_type");
                DataWindowChild c_end_dp_type = dw_data.GetChild("end_dp_type");

                c_branch_id.SetTransaction(sqlca);
                c_start_dp_type.SetTransaction(sqlca);
                c_end_dp_type.SetTransaction(sqlca);

                c_branch_id.Retrieve();
                c_start_dp_type.Retrieve();
                c_end_dp_type.Retrieve();

                tDwData = new DwThDate(dw_data);
                tDwData.Add("start_date", "start_tdate");
                tDwData.Add("end_date", "end_tdate");
                dw_data.SetItemDate(1, "start_date", state.SsWorkDate);
                dw_data.SetItemDate(1, "end_date", state.SsWorkDate);
                tDwData.Eng2ThaiAllRow();
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try { sqlca.Disconnect(); }
            catch { }
        }

        protected void getDataForReport(object sender, EventArgs e)
        {

            PnReport.Visible = true;
            //DateTime adtm_start = dw_data.GetItemDate(1, "start_date");
            //DateTime adtm_end = dw_data.GetItemDate(1, "end_date");
            //String as_type1 = dw_data.GetItemString(1, "start_dp_type");
            //String as_type2 = dw_data.GetItemString(1, "end_dp_type");
            //String as_coop = "SCOBKCAT";
            //String as_branch = dw_data.GetItemString(1, "branch_id");

            DateTime dtstart = new DateTime(2009, 08, 30);
            DateTime dtend = new DateTime(2010, 08, 30); 
            DateTime adtm_start = dtstart;
            DateTime adtm_end = dtend;
            String as_type1 = "01";
            String as_type2 = "24";
            String as_coop = "SCOBKCAT";
            String as_branch = "001";
            dw_report.SetTransaction(sqlca);
            dw_report.InsertRow(0);
            dw_report.Retrieve(adtm_start, adtm_end, as_type1, as_type2, as_coop, as_branch);
            dw_report.Modify("DataWindow.Export.PDF.Method = Distill!");
            dw_report.Modify("DataWindow.Export.PDF.Distill.CustomPostScript='1'");
            
        }
    }
}