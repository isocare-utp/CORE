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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;

using CoreSavingLibrary.WcfNCommon; //new common
using CoreSavingLibrary.WcfNDeposit;    //new deposit
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_addnewtype : PageWebSheet,WebSheet
    {
        protected String postDepttypeCode;
        protected String postdeptstatus;
        //private DepositClient depService; 

        private n_depositClient ndept; //new

        private void JspostDepttypeCode()
        {
            try
            {
                String deptTypeCode = DwMain.GetItemString(1, "depttype_group");
                DataTable dt = WebUtil.Query("select document_code as docCode, depttype_desc as depttypeDes from dpdepttype where depttype_code='" + deptTypeCode + "'");
                if (dt.Rows.Count > 0)
                {
                    String documentCode = dt.Rows[0]["docCode"].ToString().Trim();
                    String dettypeDesc = dt.Rows[0]["depttypeDes"].ToString().Trim();
                    DwMain.SetItemString(1, "document_code", documentCode);
                    DwMain.SetItemString(1, "depttype_desc", dettypeDesc);
                }
                else {
                    String documentCode = "DPACCDOCNO" + deptTypeCode;
                    //String dettypeDesc = dt.Rows[0]["depttypeDes"].ToString().Trim();

                    DwMain.SetItemString(1, "document_code", documentCode);
                   // DwMain.SetItemString(1, "depttype_desc", dettypeDesc);
                }
            }
            catch (Exception)
            {

            }
        }
        
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDepttypeCode = WebUtil.JsPostBack(this, "postDepttypeCode");
            postdeptstatus = WebUtil.JsPostBack(this, "postdeptstatus");
        }


        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit; //new
            }
            catch (Exception)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwList.Retrieve();
                WebUtil.RetrieveDDDW(DwMain, "depttype_source", "dp_addnewtype.pbl", state.SsCoopControl);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDepttypeCode")
            {
                JspostDepttypeCode();
            }
            if (eventArg == "postdeptstatus")
            {
                Jspostdeptstatus();
            }
        }

        public void SaveWebSheet()
        {
            String xmlNewDept = "";
            try
            {
                xmlNewDept = DwMain.Describe("DataWindow.Data.XML");
                //depService.AddNewDepttype(state.SsWsPass, xmlNewDept, state.SsCoopId, state.SsWorkDate);

                ndept.of_add_newdepttype(state.SsWsPass, xmlNewDept, state.SsCoopId, state.SsWorkDate); //new
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว...");
                DwList.Retrieve();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion
        public void Jspostdeptstatus()
        { 
            decimal li_check = 0;
            li_check = DwMain.GetItemDecimal( 1, "depttype_status" );
            if (li_check == 1)
            {
                DwMain.Modify("depttype_source.Protect=0");
            }
            else {
                DwMain.Modify("depttype_source.Protect=1");
            }
        }
    }
}
