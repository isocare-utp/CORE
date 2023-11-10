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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols; //เรียกใช้ service

namespace Saving.Applications.account
{
    public partial class w_sheet_vc_voucher_cancel_by_vcno : PageWebSheet, WebSheet
    {
        private DwThDate tdw_date;
        private DwThDate tdw_main;
        private n_accountClient accService;
        private CultureInfo th;
        // private ArrayList vc_no = new ArrayList();
        //===================================
        String vc_no;


        //ประกาศ Js PostBack
        protected String postVcDate;
        protected String postClickVoucher;
        protected String postNewClear;
        protected String postSelectVoucher;
        //    protected String postShowData;

        //====================================

        private void JspostSelectVoucher()
        {
            try
            {
                vc_no = "";
                int rowAll = Dw_list.RowCount;
                Int16 li_status;
                string voucher_no = "";
                for (int li_index = 1; li_index <= rowAll; li_index++)
                {
                    li_status = Convert.ToInt16(Dw_list.GetItemDecimal(li_index, "voucher_status"));
                    if (li_status == -9)
                    {
                        voucher_no = Dw_list.GetItemString(li_index, "voucher_no");
                        vc_no += voucher_no + ",";
                    }
                }

                if (vc_no != "")
                {
                    vc_no = vc_no.Substring(0, vc_no.Length - 1);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }


        private void JspostNewClear()
        {
            Dw_date.Reset();
            Dw_date.InsertRow(0);
            // Dw_date.SaveDataCache();

            Dw_list.Reset();
            //  Dw_list.SaveDataCache();

            Dw_main.Reset();
            Dw_main.InsertRow(0);
            //  Dw_main.SaveDataCache();

            Dw_detail.Reset();
            //  Dw_detail.SaveDataCache();

            Dw_footer.Reset();
            //Dw_footer.SaveDataCache();

        }

        private void JspostClickVoucher()
        {
            try
            {
                //DateTime VcDate = Dw_date.GetItemDate(1, "voucher_date");
                String vcNo = Hd_vcno.Value;
                String as_vcmas_xml = Dw_main.Describe("Datawindow.Data.Xml");
                String as_vcdet_xml = Dw_main.Describe("Datawindow.Data.Xml");

                //int result = accService.GetListVcMasDetail(state.SsWsPass, vcNo, ref as_vcmas_xml, ref as_vcdet_xml);
                int result = wcf.NAccount.of_init_vcmastdet(state.SsWsPass, vcNo, ref as_vcmas_xml, ref as_vcdet_xml);
                if (result == 1)
                {
                    Dw_main.Reset();
                    Dw_main.ImportString(as_vcmas_xml, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    Dw_detail.ImportString(as_vcdet_xml, FileSaveAsType.Xml);

                    Dw_footer.Reset();
                    Dw_footer.ImportString(as_vcdet_xml, FileSaveAsType.Xml);
                }

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostVcDate()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                String VcNo = Dw_date.GetItemString(1, "voucher_no");
                String as_vclist_xml = Dw_list.Describe("Datawindow.Data.Xml");
                string coop_id = state.SsCoopControl;
                //int result = accService.GetVcListVcCancel(state.SsWsPass, VcNo, coop_id, ref as_vclist_xml);
                int result = wcf.NAccount.of_init_vclistcancel_by_vcno(state.SsWsPass, VcNo, coop_id, ref as_vclist_xml);
                if (result == 1)
                {
                    Dw_list.Reset();
                    DwUtil.ImportData(as_vclist_xml, Dw_list, null, FileSaveAsType.Xml);

                    //ส่วนที่กำหนด จัดรูปแบบวันที่ให้กับ Dw_date
                    Dw_main.SetItemString(1, "voucher_no", VcNo);

                    //   ส่งค่า Vc_no ให้ dw_main และ dw_detail retreive
                    if (Dw_list.RowCount > 0)
                    {
                        String listVcno = Dw_list.GetItemString(1, "voucher_no");
                        Hd_vcno.Value = listVcno;
                        JspostClickVoucher();
                    }
                }
                else
                {
                    Dw_list.Reset();
                    Dw_main.Reset();
                    Dw_detail.Reset();
                    Dw_footer.Reset();
                    Dw_main.InsertRow(0);

                    Dw_main.SetItemString(1, "voucher_no", VcNo);
                    //Dw_main.SetItemString(1, "voucher_tdate", VcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                    //tdw_date.Eng2ThaiAllRow();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายละเอียด Voucher");

                }
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }

            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postVcDate = WebUtil.JsPostBack(this, "postVcDate");
            postClickVoucher = WebUtil.JsPostBack(this, "postClickVoucher");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postSelectVoucher = WebUtil.JsPostBack(this, "postSelectVoucher");
            
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
            th = new CultureInfo("th-TH");


            Dw_date.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);
            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);
            Dw_footer.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.InsertRow(0);
                Dw_date.InsertRow(0);

                //JspostVcDate();

                HdBranchId.Value = state.SsCoopId;
            }
            else
            {
                //ใช้คู่กับ savedatacatch
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_date);
                this.RestoreContextDw(Dw_list);
                this.RestoreContextDw(Dw_detail);
                this.RestoreContextDw(Dw_footer);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postVcDate")
            {
                JspostVcDate();
            }
            else if (eventArg == "postClickVoucher")
            {
                JspostClickVoucher();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {
            JspostSelectVoucher();
            //ส่วนติดต่อ SERVICE
            try
            {
                n_accountClient accService = wcf.NAccount;

                // การโยน ไฟล์ xml ไปให้ service
                DateTime voucher_date = Dw_main.GetItemDate(1, "voucher_date");
                String wsPass = state.SsWsPass;
                string coop_id = state.SsCoopControl;
                string entry_id = state.SsUsername;
                DateTime cancel_date = state.SsWorkDate;

                //เรียกใช้ webservice
                //int result = accService.SaveCancelVoucher(wsPass, voucher_date, vc_no, coop_id, entry_id, cancel_date);
                int result = wcf.NAccount.of_save_vclistcancel(wsPass, voucher_date, vc_no, coop_id,entry_id,cancel_date);

                if (result != 1)
                {
                    throw new Exception("ไม่สามารถบันทึกการยกเลิกคีย์รายวันได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการยกเลิกคีย์รายวันเรียบร้อยแล้ว");
                }

               // JspostVcDate();
                Dw_list.Reset();
                Dw_main.Reset();
                Dw_detail.Reset();
                Dw_footer.Reset();
                Dw_date.Reset();
                Dw_date.InsertRow(0);
                Dw_main.InsertRow(0);

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_date.RowCount > 1)
            {
                Dw_date.DeleteRow(Dw_date.RowCount);
            }
            else if (Dw_main.RowCount > 1)
            {
                Dw_main.DeleteRow(Dw_main.RowCount);
            }

            Dw_main.SaveDataCache();
            Dw_date.SaveDataCache();
            Dw_list.SaveDataCache();
            Dw_detail.SaveDataCache();
            Dw_footer.SaveDataCache();
        }

        #endregion
    }
}
