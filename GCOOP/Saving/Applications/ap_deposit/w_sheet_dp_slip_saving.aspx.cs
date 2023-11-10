using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saving.Applications.ap_deposit.w_sheet_dp_slip_saving_ctrl;
using Saving.WcfDeposit;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_slip_saving : PageWebSheet, WebSheet
    {
        protected string postDeptAccountNo;
        protected string postReset;
        protected string postDeptAccountNoHd;

        public void InitJsPostBack()
        {
            postDeptAccountNo = WebUtil.JsPostBack(this, "postDeptAccountNo");
            postDeptAccountNoHd = WebUtil.JsPostBack(this, "postDeptAccountNoHd");
            postReset = WebUtil.JsPostBack(this, "postReset");
        }

        public void WebSheetLoadBegin()
        {
            HdPrintBook.Value = "";
            HdPrintSlip.Value = "";
            HdFinish.Value = "";
            if (!IsPostBack)
            {
                DUtil.InsertFormMode(SlipMaster1);
                DUtil.SetItem(SlipMaster1, 1, "deptcoop_id", state.SsCoopControl);
                DUtil.SetItem(SlipMaster1, 1, "coop_id", state.SsCoopId);
                string clientId = DUtil.GetColumnClientId(SlipMaster1, 1, "deptaccount_no");
                this.SetFocusByClientId(clientId, this.GetType());
            }
            else
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDeptAccountNo")
            {
                JsPostDeptAccountNo();
            }
            else if (eventArg == "postDeptAccountNoHd")
            {
                JsPostDeptAccountNoHd();
            }
            else if (eventArg == "postReset")
            {
                JsPostReset();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DepositClient depServ = wcf.Deposit;
                string selecetd_coop_id = SlipMaster1.GetItemStringTryCatch(1, "deptcoop_id");
                string deptaccount_no = SlipMaster1.GetItemStringTryCatch(1, "deptaccount_no");
                decimal deptslip_amt = SlipMaster1.GetItemDecimalTryCatch(1, "deptslip_amt");

                if (deptslip_amt <= 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกค่ายอดทำรายการด้วย");
                    return;
                }
                else if (deptaccount_no == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกค่าเลขที่บัญชี เพื่อทำรายการก่อน");
                    return;
                }
                else if (selecetd_coop_id == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสสาขา");
                    return;
                }

                String sql = "select dpucfdeptgroup.deptgroup_code from dpdepttype left join dpucfdeptgroup on dpdepttype.deptgroup_code = dpucfdeptgroup.deptgroup_code and dpdepttype.coop_id = dpucfdeptgroup.coop_id where dpdepttype.coop_id='" + state.SsCoopControl + "' and depttype_code = '" + SlipMaster1.GetItemStringTryCatch(1, "depttype_code") + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (!dt.Next())
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบประเภทเงินฝากของเลขบัญชีนี้");
                    return;
                }

                if (dt.GetString(0) != "00")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ เลขบัญชีนี้ ไม่ใช่เงินฝากออมทรัพย์");
                    return;
                }

                string rec = SlipMaster1.GetItemStringTryCatch(1, "recppaytype_code");
                //สามารถทำได้แค่ฝาก-ถอน เงินสดเท่านั้น ส่วนฝาก-ถอน อื่น ๆ ให้ใช้หน้าจอเดิม
                //get ค่าคงที่ เช่นฝาก-ถอน ยอดคงเหลือ ขั้นต่ำ เพื่อตรวจสอบก่อนบันทึก by tong 2 รอแก้ไข
                //เรียก of_check_min_operate(deptype, flag, amt) โดยถ้าจะ check ฝากต่ำสุดให้ flage = 1 ถอนต่ำสุดให้ flag = -1 by tong 3 รอแก้ไข

                string depttype_code = SlipMaster1.GetItemStringTryCatch(1, "depttype_code");
                int ai_flag_dep = 1;
                int ai_flag_wid = -1;
                Decimal dept_amt = SlipMaster1.GetItemDecimalTryCatch(1, "deptslip_amt");

                if (rec == "DEP")
                {
                    SlipMaster1.SetItem(1, "deptwith_flag", "+");
                    SlipMaster1.SetItem(1, "deptitemtype_code", "DEP");
                    SlipMaster1.SetItem(1, "group_itemtpe", "DEP");
                }
                else if (rec == "WID")
                {
                    SlipMaster1.SetItem(1, "deptwith_flag", "-");
                    SlipMaster1.SetItem(1, "deptitemtype_code", "WID");
                    SlipMaster1.SetItem(1, "group_itemtpe", "WID");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกการทำรายการด้วย");
                    return;
                }
                SlipMaster1.SetItem(1, "entry_id", state.SsUsername);
                SlipMaster1.SetItem(1, "entry_date", state.SsWorkDate);
                SlipMaster1.SetItem(1, "calint_from", new DateTime(1900, 1, 1));
                SlipMaster1.SetItem(1, "calint_to", state.SsWorkDate);
                SlipMaster1.SetItem(1, "machine_id", state.SsClientIp);
                SlipMaster1.SetItem(1, "preadjdoc_date", new DateTime(1500, 1, 1));
                SlipMaster1.SetItem(1, "deptslip_netamt", SlipMaster1.GetItemDecimalTryCatch(1, "deptslip_amt") + SlipMaster1.GetItemDecimalTryCatch(1, "other_amt"));
                SlipMaster1.SetItem(1, "operate_time", new DateTime(1500, 1, 1));
                SlipMaster1.SetItem(1, "coop_id", state.SsCoopId);
                String deptFormat = SlipMaster1.GetItemStringTryCatch(1, "deptformat");
                string xml = SlipMaster1.ExportXml();
                bool fin = false;
                String finMessage = "";

                string slip_no = "";
                if (rec == "DEP")
                {
                    //เอ
                    try
                    {
                        //ไม่จำเป็นต้องเรียกตรงนี้ ดอย.
                        //int mindeposit_dep = depServ.of_check_min_operate(state.SsWsPass, depttype_code, ai_flag_dep, dept_amt);

                        slip_no = depServ.DepositPostLite(state.SsWsPass, xml, "", "");
                        if (slip_no != "")
                        {
                            finMessage = "บันทึกข้อมูล ฝากเงินสดออมทรัพย์ " + deptFormat + " สำเร็จ  กรุณากด <span onclick='MenubarNew()' style='cursor:pointer;color:#0000FF'><u>New[F2]</u></span>";
                            fin = true;
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                else if (rec == "WID")
                {
                    //เอ
                    try
                    {
                        //ไม่จำเป็นต้องเรียกตรงนี้ ดอย.
                        //int mindeposit_wid = depServ.of_check_min_operate(state.SsWsPass, depttype_code, ai_flag_wid, dept_amt);

                        slip_no = depServ.WithdrawCloseLite(state.SsWsPass, xml, "");
                        if (slip_no != "")
                        {
                            finMessage = "บันทึกข้อมูล ถอนเงินสดออมทรัพย์ " + deptFormat + " สำเร็จ  กรุณากด <span onclick='MenubarNew()' style='cursor:pointer;color:#0000FF'><u>New[F2]</u></span>";
                            fin = true;
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                if (fin)
                {
                    try
                    {
                        bool isPrintSlip = int.Parse(depServ.GetDpDeptConstant(state.SsWsPass, "printslip_status")) == 1;
                        if (isPrintSlip)
                        {
                            string xml_return = "";
                            int re = depServ.PrintSlip(state.SsWsPass, slip_no, state.SsCoopId, state.SsPrinterSet, 1, ref xml_return);
                            Printing.PrintApplet(this, "dept_slip", xml_return);
                        }
                        LtServerMessage.Text = WebUtil.CompleteMessage(finMessage);
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage(finMessage + "<div align='center'>** ไม่สามารถเชื่อมต่อเครื่องพิมพ์ slip **</div>");
                    }
                    this.SetJsOpenIFrame(900, 540, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + deptaccount_no);
                    SlipMaster1.Reset();
                    SlipMaster1.Reset();

                    DUtil.InsertFormMode(SlipMaster1);
                    DUtil.SetItem(SlipMaster1, 1, "deptcoop_id", state.SsCoopControl);
                    DUtil.SetItem(SlipMaster1, 1, "coop_id", state.SsCoopId);
                    DUtil.SetItem(SlipMaster1, 1, "deptcoop_id", state.SsCoopControl);

                    string clientId = DUtil.GetColumnClientId(SlipMaster1, 1, "deptaccount_no");
                    this.SetFocusByClientId(clientId, this.GetType());
                    HdFinish.Value = "true";
                    LbDetail.Text = "";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            HdDumAccountNo.Value = "";
            HdDumCoopId.Value = "";
            try
            {
                decimal deptslip_amt = DUtil.GetItemDecimalTryCatch(SlipMaster1, 1, "deptslip_amt");
                DUtil.SetItem(SlipMaster1, 1, "total_slip_amt", deptslip_amt.ToString("#,##0"));
            }
            catch { }
        }

        private void JsPostDeptAccountNo()
        {
            LbDetail.Text = "&nbsp;";
            String coopControl = state.SsCoopControl;
            DepositClient deptService = wcf.Deposit;
            string deptAccNo = SlipMaster1.GetItemStringTryCatch(1, "deptaccount_no").Trim();
            deptAccNo = wcf.InterPreter.DeptBarcodeToDeptAccount(state.SsConnectionIndex, state.SsCoopControl, deptAccNo);
            if (deptAccNo != "")
            {
                try
                {
                    deptAccNo = deptService.BaseFormatAccountNo(state.SsWsPass, deptAccNo);
                }
                catch { }
            }

            //string deptCoopId = SlipMaster1.GetItemStringTryCatch(1, "deptcoop_id");

            if (HdDumAccountNo.Value != "")
            {
                SlipMaster1.arg_coop_id = state.SsCoopId;
                SlipMaster1.arg_deptaccount_no = HdDumAccountNo.Value;
                SlipMaster1.arg_coop_control = coopControl;
                deptAccNo = HdDumAccountNo.Value;
            }
            else
            {
                SlipMaster1.arg_coop_id = state.SsCoopId;
                SlipMaster1.arg_deptaccount_no = deptAccNo;
                SlipMaster1.arg_coop_control = coopControl;
            }
            SlipMaster1.arg_application = state.SsApplication;

            int row = SlipMaster1.Retrieve(this);

            //get ประเภทบัญชีเงินฝาก ถ้าไม่ใช่ออมทรัพย์ ในแจ้งเตือนว่าประเภทเงินฝากไม่ถูกต้อง พร้อมทั้ง clear หน้าจอ by tong 1 รอแก้ไข
            string depttype_code = SlipMaster1.GetItemStringTryCatch(1, "depttype_code");

            if (row <= 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขบัญชี " + deptAccNo + " กรุณากด <span onclick='MenubarNew()' style='cursor:pointer;color:#FF9900'><u>New[F2]</u></span>");
                return;
            }
            String sql = "select dpucfdeptgroup.deptgroup_code from dpdepttype left join dpucfdeptgroup on dpdepttype.deptgroup_code = dpucfdeptgroup.deptgroup_code and dpdepttype.coop_id = dpucfdeptgroup.coop_id where dpdepttype.coop_id='" + state.SsCoopControl + "' and depttype_code = '" + depttype_code + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (!dt.Next())
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบประเภทเงินฝากของเลขบัญชีนี้");
                return;
            }
            string deptFormat = deptService.ViewAccountNoFormat(state.SsWsPass, deptAccNo);
            SlipMaster1.SetItem(1, "deptformat", deptFormat);
            string clientId = DUtil.GetColumnClientId(SlipMaster1, 1, "recppaytype_code");
            this.SetFocusByClientId(clientId, this.GetType());

            String sql2 = "SELECT * FROM DPDEPTMASTER WHERE COOP_ID='" + coopControl + "' AND DEPTACCOUNT_NO='" + deptAccNo + "'";
            Sdt dt2 = WebUtil.QuerySdt(sql2);
            if (dt2.Next())
            {
                LbDetail.Text = "<font color='#11AB33'>ยอดอายัด = " + dt2.GetDecimal("sequest_amount").ToString("#,##0.00") + " &nbsp; &nbsp; " +
                    "ยอดเช็คเรียกเก็บ = " + dt2.GetDecimal("checkpend_amt").ToString("#,##0.00") + " &nbsp; &nbsp; </font>";
            }

            if (dt.GetString(0) != "00")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ เลขบัญชีนี้ ไม่ใช่เงินฝากออมทรัพย์");
            }
        }

        private void JsPostDeptAccountNoHd()
        {
            JsPostDeptAccountNo();
        }

        private void JsPostReset()
        {
            LbDetail.Text = "&nbsp;";
            DUtil.Reset(SlipMaster1);
            DUtil.Reset(SlipMaster1);
            DUtil.SetItem(SlipMaster1, 1, "coop_id", state.SsCoopId);
        }
    }
}