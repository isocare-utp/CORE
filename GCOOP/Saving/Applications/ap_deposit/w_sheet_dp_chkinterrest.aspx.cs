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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;
using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_chkinterrest : PageWebSheet, WebSheet
    {
        private n_depositClient depService;        
        //private DepositConfig depConfig;
        private DwThDate tDwMain;
        private DwThDate tDwHead;
        private DwThDate tDwDetail;
        private bool isException = false;
        private String deptAccountNo = null;

        //post back
        protected String postNewAccount;
        protected String postRecalInterrest;
        public void InitJsPostBack()
        {
            postNewAccount = WebUtil.JsPostBack(this, "postNewAccount");
            postRecalInterrest = WebUtil.JsPostBack(this, "postRecalInterrest");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("deptslip_date", "deptslip_date_tdate");
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("calint_date", "calint_tdate");
            tDwDetail = new DwThDate(DwDetail, this);
            tDwDetail.Add("calint_from", "calint_from_tdate");
            tDwDetail.Add("calint_to", "calint_to_tdate");
            tDwDetail.Add("prncdue_date", "prncdue_tdate");//prnc_tdate
            tDwDetail.Add("prnc_date", "prnc_tdate");//prnc_tdate
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                depService = wcf.NDeposit;                
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            //depConfig = new DepositConfig(depService);

            if (IsPostBack)
            {
                try
                {
                    this.RestoreContextDw(DwMain, tDwMain);
                    this.RestoreContextDw(DwDetail, tDwDetail);
                    this.RestoreContextDw(DwHead, tDwHead);

                }
                catch { }
            }
            else
            {
                DwHead.InsertRow(0);
                DwHead.SetItemDate(1, "calint_date", state.SsWorkDate);
                DwHead.SetItemString(1, "operation", "cls"); // HC ให้เป็น การปิดบัญชี เพื่อ init ดอกเบี้ย
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemDate(1, "deptslip_date", state.SsWorkDate);
                DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);

                HfCheck.Value = "False";
                HfCoopid.Value = state.SsCoopId;

            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewAccount")
            {
                JsNewAccountNo();
            }
           
            else if (eventArg == "postRecalInterrest")
            {
                JsRecalInterrest();
            }
        }

        public void SaveWebSheet()
        {
            // หน้านี้ ห้ามให้ save เด็ดขาด
        }

        public void WebSheetLoadEnd()
        {

            if (DwHead.RowCount > 1)
            {

                DwHead.DeleteRow(0);

            } // แก้ปัญหา dw ขึ้นซ้อนกัน
            tDwMain.Eng2ThaiAllRow();
            tDwHead.Eng2ThaiAllRow();
            tDwDetail.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
            DwHead.SaveDataCache();

        }
        private void JsNewAccountNo()
        {
            String accNo = null;
            DwDetail.Reset();
            try
            {
                accNo = DwMain.GetItemString(1, "deptformat");
                accNo = depService.of_analizeaccno(state.SsWsPass, accNo);
            }
            catch
            {
                this.deptAccountNo = null;
                return;
            }
            try
            {
                
                isException = false;
                String coopid = state.SsCoopId;
                String deptcoop_id = HfCoopid.Value;
                n_depositClient depService = wcf.NDeposit;
                DateTime calint_Date = new DateTime(1370, 1, 1);
                try
                {
                    calint_Date = DwHead.GetItemDateTime(1, "calint_date"); // get วันที่ ส่งแทน workdate
                }
                catch { }
                String ls_xml = depService.of_init_deptslip(state.SsWsPass, state.SsCoopId, accNo, deptcoop_id, calint_Date, state.SsUsername, state.SsClientIp);
                DwUtil.ImportData(ls_xml, DwMain, tDwMain, FileSaveAsType.Xml);
                String depformat = ViewAccountNoFormat(state.SsWsPass, accNo);
                DwMain.SetItemString(1, "deptformat", depformat);

                if (DwMain.RowCount != 1)
                {
                    throw new Exception("Import ไม่สำเร็จ ไม่ทราบสาเหตุ");
                }
                HdNewAccountNo.Value = "true";
            }
            catch (Exception ex)
            {
                isException = true;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                JsNewClear();
            }
            finally
            {
                if (isException)
                {
                    accNo = null;
                    JsNewClear();
                }
            }
            this.deptAccountNo = accNo;
            PostOperation(); // เชคเครื่องหมาย ว่า เป็นการปิดหรือถอนเงิน(function ในอนาคต)
        }
        public String ViewAccountNoFormat(String wsPass, String deptAccountNo)
        {
            Sdt dt = WebUtil.QuerySdt("select deptcode_mask from DPDEPTCONSTANT");
            if (!dt.Next())
            {
                throw new Exception("ไม่สามารถติดต่อกับ Database ได้");
            }
            
            String format = dt.GetString(0).ToUpper();//"X-XX-XXXXXXX";
            char[] fc = format.ToCharArray();
            char[] ac = deptAccountNo.ToCharArray();
            String accNo = "";
            int j = 0;
            for (int i = 0; i < fc.Length; i++)
            {
                if (fc[i] != 'X')
                {
                    accNo += fc[i].ToString();
                }
                else
                {
                    try
                    {
                        accNo += ac[j++];
                    }
                    catch { accNo += ""; }
                }
            }
            return accNo;
        }
        private void JsNewClear()
        {

            DwMain.Reset();
            DwHead.Reset();
            DwDetail.SaveDataCache();
            DwMain.InsertRow(0);
            DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);
            DwMain.SetItemDateTime(1, "deptslip_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            HdRequireCalInt.Value = "false";
            HdIsPostBack.Value = "false";

        }

        private void PostOperation()
        {
            try
            {
                string oparation = "";
                try
                {
                    oparation = DwHead.GetItemString(1, "operation");
                }
                catch { oparation = ""; }

                if (oparation == "cls")
                {
                    CloseDeposit();

                }
                else if (oparation == "wid")
                {
                    WithdrawDeposit(); // ฟังก์ชั่นสำหรับรายการถอน
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรายการ");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        // function สำหรับ init ดอกเบี้ย
        private void CloseDeposit()
        {
            // เตรียมข้อมูลก่อนส่งคำนวณดอกเบี้ย
            try
            {
                String recpPayGrp, deptgroup_code;
                isException = false;
                DwMain.SetItemString(1, "tofrom_accid", "");
                DwMain.SetItemString(1, "group_itemtpe", "CLS");
                recpPayGrp = DwMain.GetItemString(1, "group_itemtpe");
                String deptTypeCode = DwMain.GetItemString(1, "DEPTTYPE_CODE");
                deptgroup_code = depService.of_get_deptgroup(state.SsWsPass, deptTypeCode);
                DwMain.SetItemDecimal(1, "deptslip_amt", DwMain.GetItemDecimal(1, "prncbal"));
                if (deptgroup_code == "01") // ถ้าเป็น บัญชีประจำ จะต้อง init prncfixed ด้วย
                {
                    try
                    {
                        DateTime calint_Date = new DateTime(1370, 1, 1);
                        try
                        {
                            calint_Date = DwHead.GetItemDateTime(1, "calint_date");
                        }
                        catch { }
                        String deptcoop_id = DwMain.GetItemString(1, "deptcoop_id");
                        String accountNo = DwMain.GetItemString(1, "deptaccount_no");
                        String xmlSlipDetail = depService.of_init_deptslip_det(state.SsWsPass, deptTypeCode, accountNo, deptcoop_id, calint_Date, recpPayGrp);
                        DwUtil.ImportData(xmlSlipDetail, DwDetail, tDwDetail, FileSaveAsType.Xml);
                    }
                    catch (Exception ex)
                    {
                        DwDetail.Reset();
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                isException = true;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            // ส่งค่าคิดดอกเบี้ย
            String accNo = DwMain.GetItemString(1, "deptaccount_no");
            String ls_xml = "";
            String ls_xml_det = "", ls_deptcoop_id;

            ls_deptcoop_id = DwMain.GetItemString(1, "deptcoop_id");
            Decimal adc_intsum = 0;

            //ถ้าไม่ใช่เงินฝากประเภทประจำ ไม่ต้องส่ง detail ไป  deptgroup_code = 
            if (depService.of_get_deptgroup(state.SsWsPass, DwMain.GetItemString(1, "DEPTTYPE_CODE")) != "01") { ls_xml_det = ""; }

            string errorMessage = "";
            DateTime calintDate = new DateTime(1370, 1, 1); // อ่านวันที่คิดดอกเบี้ยถึง
            try
            {
                calintDate = DwHead.GetItemDateTime(1, "calint_date");
            }
            catch { }

            Sta ta = new Sta(state.SsConnectionString);

            // get recppaytype เพื่อ ทำสลิป ส่งไปคำนวณดอกเบี้ย
            String sql = "";
            sql = @"SELECT max(recppaytype_code) as recppaytype
                    FROM dpucfrecppaytype  
                    WHERE ( moneytype_support= 'CSH' ) AND ( group_itemtpe = '" + DwMain.GetItemString(1, "group_itemtpe") + @"' )";
            Sdt dt = ta.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string ts = dt.Rows[0]["recppaytype"].ToString();
                DwMain.SetItemString(1, "recppaytype_code", ts);
            }
            ta.Close();
            ls_xml = DwMain.Describe("DataWindow.Data.XML");
            if (DwDetail.RowCount > 0)
            {
                ls_xml_det = DwDetail.Describe("DataWindow.Data.XML");
            }
            Int32 result = depService.of_init_deptslip_calint(state.SsWsPass, accNo, ls_deptcoop_id, state.SsUsername, calintDate, state.SsClientIp, ref ls_xml, ref ls_xml_det, ref errorMessage, ref adc_intsum);
            DwUtil.ImportData(ls_xml, DwMain, tDwMain, FileSaveAsType.Xml); // imp ส่วนข้อมูล บัญขี

            DwDetail.Reset();
            if (WebUtil.IsXML(ls_xml_det))
            {
                DwUtil.ImportData(ls_xml_det, DwDetail, tDwDetail, FileSaveAsType.Xml); // imp prncfixed 
            }

            this.deptAccountNo = accNo;
        }

        // function ในอนาคต
        private void WithdrawDeposit()
        {
            if (DwMain.GetItemString(1, "Depttype_code") == "50")
            {
                JsNewClear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ประเภทบัญชีนี้ไม่สามารถถอนได้");
            }


        }

        // ฟังชั่นเมื่อมีการกดปุ่มคำนวณดอกเบี้ย เพื่อให้คำนวณดอกเบี้ยใหม่
        private void JsRecalInterrest()
        {
            JsNewAccountNo();
        }

    }
}