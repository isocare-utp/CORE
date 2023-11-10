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
using System.Data.OracleClient;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.account
{
    public partial class w_sheet_cm_constant_config : PageWebSheet, WebSheet 
    {
        protected String postDeleteRow;
        protected String postAddRow;
        protected String postCheckType;

        //==================================
        private void JspostAddRow()
        {
            Dw_data.InsertRow(0);  
            decimal row = Dw_data.RowCount;
            if (row > 0)
            {
                for (int i=1; i <= row; i++)
                {
                    Dw_data.SetItemString(i, "coop_id", state.SsCoopId);
                }
            }
            else { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มี Row"); }
              

        }

        private void JspostDeleteRow()
        {
            try 
            {
                Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
                Dw_data.DeleteRow(RowDetail);
                Dw_data.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                Dw_data.Retrieve(state.SsCoopControl);
            }
            catch (Exception ex)
            { 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void CheckTypeCodeMissMap()
        {
            String message = "";
            //หาประเภทเงินฝากที่ยังไม่ได้จับคู่บัญชี
            string sqlDeptcount = @"SELECT depttype_code || ' ' || depttype_desc as depttype_desc FROM dpdepttype where coop_id = '" + 
                state.SsCoopControl + "' and depttype_code not in ( select shrlontype_code from vcmapaccid where system_code = 'DEP' and slipitemtype_code = 'DEP' and coop_id = '"+
                state.SsCoopControl + "') order by depttype_code";
            Sdt Dpcount = WebUtil.QuerySdt(sqlDeptcount);

            while (Dpcount.Next())
            {
                string depttype_desc = Dpcount.GetString("depttype_desc").Trim();
                message += ' ' + depttype_desc;
            }

            //หาประเภทเงินกู้ที่ยังไม่ได้จับคู่บัญชี
            string sqlLontcount = @"SELECT loantype_code || ' ' || loantype_desc as lntype_desc FROM lnloantype where coop_id = '" +
                state.SsCoopControl + "' and loantype_code not in ( select shrlontype_code from vcmapaccid where system_code = 'LON' and slipitemtype_code = 'LON' and coop_id = '" +
                state.SsCoopControl + "') order by loantype_code";
            Sdt Lncount = WebUtil.QuerySdt(sqlLontcount);

            while (Lncount.Next())
            {
                string lntype_desc = Lncount.GetString("lntype_desc").Trim();
                message += ' ' + lntype_desc;
            }


            if (message != "")
            {
                LtServerMessage.Text = WebUtil.WarningMessage("มีประเภทรายการที่ยังไม่ได้ Map คู่บัญชี คือ " + message);
            }
            else
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบรายการที่ยังไม่ได้ Map คู่บัญชี");
            }
        }
        //==============================
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");
            postCheckType = WebUtil.JsPostBack(this, "postCheckType");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_data.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_data.Retrieve(state.SsCoopControl);
                
            } 
            else
            {
                this.RestoreContextDw(Dw_data);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postAddRow")
            {
                JspostAddRow();
            }
            else if (eventArg == "postCheckType")
            {
                CheckTypeCodeMissMap();
            }
        }

        public void SaveWebSheet()
        {
            try 
            {
                Dw_data.SetItemString(1, "coop_id",state.SsCoopId);
                Dw_data.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                Dw_data.Retrieve(state.SsCoopControl);
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_data.SaveDataCache();
        }

        #endregion
    }
}
