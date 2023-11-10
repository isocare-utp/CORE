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
//using CoreSavingLibrary.WcfNDeposit;

using CoreSavingLibrary.WcfNCommon; // new common
using CoreSavingLibrary.WcfNDeposit;// new deposit
using System.Web.Services.Protocols;
using System.ServiceModel.Channels;
using System.Xml;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_cancle_deptedit : PageWebSheet, WebSheet
    {
        //private DepositClient depServ;
        private n_depositClient ndept;
        private DwThDate tDwMaster;
        protected String postDeptAccountNo;
        protected String postCancelDetail;
        protected String postSaveNoCheckApv;
        protected String RejectDate;

        private void JsNewClear()
        {
            DwMaster.Reset();
            DwMaster.InsertRow(0);
            DwMaster.SetItemString(1, "coop_id", state.SsCoopControl );
            DwDetail.Reset();
        }

        private void JsPostDeptAccountNo()
        {
            String deptAccountNo = "";
            String CoopID = "";
            try
            {
                deptAccountNo = DwUtil.GetString(DwMaster, 1, "deptaccount_no");// DwMaster.GetItemString(1, "deptaccount_no");
                //deptAccountNo = depServ.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);
                deptAccountNo = ndept.of_analizeaccno(state.SsWsPass, deptAccountNo); //new

                //deptAccountNo = ndept.of_analizeaccno(state.SsWsPass, deptAccountNo);
                DateTime workDate = DwUtil.GetDateTime(DwMaster, 1, "work_date");
                //CoopID = DwUtil.GetString(DwMaster, 1, "coop_id");
                CoopID = state.SsCoopControl;
                object[] argsMaster = new object[2] { deptAccountNo, CoopID };
                DwUtil.RetrieveDataWindow(DwMaster, "dp_cancle_deptedit.pbl", null, argsMaster);
                DwMaster.SetItemDateTime(1, "work_date", workDate);
                tDwMaster.Eng2ThaiAllRow();
                if (DwMaster.RowCount < 1)
                {
                    JsNewClear();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            JsPostCancelDetail();
        }

        private void JsPostCancelDetail()
        {
            try
            {
                String deptAccountNo = DwUtil.GetString(DwMaster, 1, "deptaccount_no");
                String workTDate = DwUtil.GetString(DwMaster, 1, "work_tdate");
                DateTime workDate = DwUtil.GetDateTime(DwMaster, 1, "work_date");
                //DateTime workDate = Convert.ToDateTime(workTDate);
                if (!string.IsNullOrEmpty(deptAccountNo))
                {
                    //deptAccountNo = depServ.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);
                    deptAccountNo = ndept.of_analizeaccno(state.SsWsPass, deptAccountNo); //new

                    //deptAccountNo = ndept.of_analizeaccno(state.SsWsPass, deptAccountNo);
                    //object[] argsDetail = new object[3] { deptAccountNo, workDate, state.SsCoopId};
                    DwDetail.Reset();
                    DwUtil.RetrieveDataWindow(DwDetail, "dp_cancle_deptedit.pbl", null, deptAccountNo, workDate, state.SsCoopId);
                    DwMaster.SetItemString(1, "deptaccount_no", WebUtil.ViewAccountNoFormat(deptAccountNo));
                    
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void SaveSheet(String recpCode, String deptAccountNo, String slip)
        {
            int row = int.Parse(HdDwDetailRow.Value);
            try
            {
                String result = "";
                String as_apv_doc = Hdas_apvdoc.Value;
                String apvId = DwUtil.GetString(DwDetail, row, "authorize_id", "");
                if (recpCode.Substring(0, 1) == "C")
                {
                    //result = depServ.CloseCancel(state.SsWsPass, deptAccountNo, state.SsCoopId, state.SsWorkDate, state.SsUsername, apvId);
                    result = ndept.of_cancel_close(state.SsWsPass, deptAccountNo, state.SsCoopId, state.SsWorkDate, state.SsUsername, apvId); //new
                }
                else
                {
                    //result = depServ.OperateCancel(state.SsWsPass, slip, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, apvId, as_apv_doc);
                    result = ndept.of_operate_cancel(state.SsWsPass, slip, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, apvId); //new
                }
                if (result != "")
                { 
                    string slipNo = result;
                    string message ="";
                    CallPrintBook(slipNo, message);
                    HdDwDetailRow.Value = "0";
                    LtServerMessage.Text = WebUtil.CompleteMessage("ย้อนรายการเลขที่ " + slip + " - สำเร็จ");
                    JsPostDeptAccountNo();
                    JsPostCancelDetail();
                    Hdas_apvdoc.Value = "";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                String msg = WebUtil.ErrorMessage(ex);
                try
                {
                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch
                {
                }

            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDeptAccountNo = WebUtil.JsPostBack(this, "postDeptAccountNo");
            postCancelDetail = WebUtil.JsPostBack(this, "postCancelDetail");
            postSaveNoCheckApv = WebUtil.JsPostBack(this, "postSaveNoCheckApv");
            RejectDate = WebUtil.JsPostBack(this, "RejectDate");
            tDwMaster = new DwThDate(DwMaster, this);
            tDwMaster.Add("work_date", "work_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdCheckApvAlert.Value = "";
            HdWorkDate.Value = state.SsWorkDate.ToString("yyyy-MM-dd", WebUtil.EN);
            try
            {
                //depServ = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ WebService ได้");
                return;
            }

            if (!IsPostBack)
            {
                Hdas_apvdoc.Value = "";
                DwMaster.InsertRow(0);
                DwMaster.SetItemString(1, "coop_id", state.SsCoopId);
                DwMaster.SetItemDateTime(1, "work_date", state.SsWorkDate);
                tDwMaster.Eng2ThaiAllRow();   
            }
            else
            {
                this.RestoreContextDw(DwMaster);
                this.RestoreContextDw(DwDetail);
            }
            
        }

        public void CheckJsPostBack(string eventName)
        {
            if (eventName == "postDeptAccountNo")
            {
                JsPostDeptAccountNo();
            }
            else if (eventName == "postCancelDetail")
            {
                JsPostCancelDetail();
            }
            else if (eventName == "postSaveNoCheckApv")
            {
                int detailRow = int.Parse(HdDwDetailRow.Value);
                String deptAccountNo = DwUtil.GetString(DwMaster, 1, "deptaccount_no");
                //deptAccountNo = depServ.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);
                deptAccountNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, deptAccountNo); //new
               // String recpCode = DwDetail.GetItemString(detailRow, "recppaytype_code");
                String recpCode = DwDetail.GetItemString(detailRow, "group_itemtpe");
                String slip = DwDetail.GetItemString(detailRow, "deptslip_no").Trim();
                SaveSheet(recpCode, deptAccountNo, slip);
            }
            else if (eventName == "RejectDate")
            {
                DwMaster.SetItemDateTime(1, "work_date", state.SsWorkDate);
                tDwMaster.Eng2ThaiAllRow();
                JsPostCancelDetail();
            }
        }

        public void SaveWebSheet()
        {
            int detailRow = -1;
            try
            {
                detailRow = int.Parse(HdDwDetailRow.Value);
                
                if (detailRow > 0)
                {
                    String deptAccountNo = DwUtil.GetString(DwMaster, 1, "deptaccount_no");
                    //deptAccountNo = depServ.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);
                    deptAccountNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, deptAccountNo); //new
                    String recpCode = DwDetail.GetItemString(detailRow, "group_itemtpe");
                    // String recpCode = DwDetail.GetItemString(detailRow, "recppaytype_code");group_itemtpe
                    String slip = DwDetail.GetItemString(detailRow, "deptslip_no").Trim();
                    //String[] apv = depServ.CheckRightPermissionCancel(state.SsWsPass, state.SsUsername, state.SsCoopId, slip, state.SsWorkDate);
                    String[] apv = { "true" };                   
                    if (apv[0] == "true")
                    {
                        SaveSheet(recpCode, deptAccountNo, slip);
                    }
                    else
                    {
                        DataTable dt = WebUtil.Query("select member_no from dpdeptmaster where deptaccount_no='" + deptAccountNo + "'");
                        String memberNo = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
                        Decimal netAmt = DwUtil.GetDec(DwMaster, detailRow, "deptslip_netamt", 0);
                        String accName = DwUtil.GetString(DwMaster, 1, "deptaccount_name", "");
                        String itemType = DwUtil.GetString(DwDetail, detailRow, "recppaytype_code", "");
                        String itemTypeDesc = apv[1].Trim();
                        itemTypeDesc = itemTypeDesc.Length > 59 ? itemTypeDesc.Substring(0, 59) : itemTypeDesc;
                        //String reportNo = depServ.AddApvTask(state.SsWsPass, state.SsUsername, state.SsApplication, state.SsClientIp, itemType, itemTypeDesc, deptAccountNo, memberNo, state.SsWorkDate, netAmt, deptAccountNo, accName, apv[0], "DEP", 1, state.SsCoopId);

                        //HdProcessId.Value = reportNo;
                        HdAvpCode.Value = apv[0];
                        HdItemType.Value = itemType;
                        HdAvpAmt.Value = netAmt.ToString();
                        HdCheckApvAlert.Value = "true";
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรายการที่ต้องการด้วย");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMaster.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion

        private void CallPrintBook(String slipNo, String message)
        {
        
            try
            {
                Int32 PrintSlipStatus = Convert.ToInt32(WebUtil.GetDpDeptConstant("printslip_status"));
                if (PrintSlipStatus == 1)
                {
                    //depService.PrintSlip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet);
                    int printStatus = xmlconfig.DepositPrintMode;
                    string xml_return = "", xml_return_bf = "";
                    //depServ.PrintSlip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet, 1, ref xml_return);
                    ndept.of_print_slip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet, 1, ref xml_return); //new

                    if (xml_return != "")
                    {
                        Printing.PrintApplet(this, "dept_slip", xml_return);
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage(message);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(message + " , ไม่สามารถเชื่อมต่อเครื่องพิมพ์ slip");
            }
            //JsNewClear();
        }
    }
}