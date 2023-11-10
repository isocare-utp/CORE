using System;
using CoreSavingLibrary;
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
using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAccount;
namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_vc_voucher_edit_detail : PageWebDialog, WebDialog 
    {
        private DwThDate tDwMain;
        protected String postInsertDwDetail; //insert แถว
        protected String postVoucherEdit;
        protected String postAddnewUpdateVoucher;
        protected String postDeleteRowDetail;
        protected String postRefresh;

        private n_accountClient accService; //ประกาศเสมอ
        //========================================

        private void JspostDeleteRowDetail()
        {
            Int16 row = Convert.ToInt16(HdDetailRow.Value);
            Dw_detail.DeleteRow(row);
        }

        //j-event Add New Update Voucher
        private void JspostAddnewUpdateVoucher()
        {
            try
            {
                String queryStrSeq = "";
                try { queryStrSeq = Request.QueryString["seq_no"].ToString().Trim(); }
                catch { }
                String queryStrAccId = "";
                try { queryStrAccId = Request["acc_id"].Trim(); }
                catch { }
                String vc_no = Dw_main.GetItemString(1, "voucher_no");
                //String acc_id = Dw_detail.GetItemString(1, "account_id");
                //String acc_side = Dw_detail.GetItemString(1, "account_side");

                Dw_detail.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                Decimal dr_total = Convert.ToDecimal(Dw_detail.GetItemString(1, "compute_2"));
                Decimal cr_total = Convert.ToDecimal(Dw_detail.GetItemString(1, "compute_3"));
                Sta ta = new Sta(state.SsConnectionString);
                try
                {
                    string sql = "update vcvoucherdet set dr_amt = '" + dr_total + "',cr_amt = '" + cr_total + "' where  voucher_no = '" + vc_no + "' and account_id = '" + queryStrAccId + "' and seq_no = '" + queryStrSeq + "'";
                    int ii = ta.Exe(sql);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
                HdIsFinished.Value = "true";

            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + WebUtil.SoapMessage(ex));
            }
        }

        // J-คลิกปุ่ม แก้ไขดึงข้อมูลมา show
        private void JsPostVoucherEdit()
        {
            String grp_no1 = "";
            String s_code1 = "";
            String acc_side1 = "";
            String vcNo = Dw_main.GetItemString(1, "voucher_no");
            try
            {
                String queryStrVcDesc = "";
                try { queryStrVcDesc = Request["vc_desc"].Trim(); }
                catch { }
                String queryStrVcDate = "";
                try { queryStrVcDate = Request["vcDate"].Trim(); }
                catch { }
                String queryStrAccId = "";
                try { queryStrAccId = Request["acc_id"].Trim(); }
                catch { }
                String queryStrSeq = "";
                try { queryStrSeq = Request.QueryString["seq_no"].ToString().Trim(); }
                catch { }
                //String queryStrS_code = "";
                //try { queryStrSeq = Request.QueryString["s_code"].ToString().Trim(); }
                //catch { }
                //String queryStrGrp_no = "";
                //try { queryStrSeq = Request.QueryString["grp_no"].ToString().Trim(); }
                //catch { }
                //String queryStrAcc_side = "";
                //try { queryStrSeq = Request.QueryString["acc_side"].ToString().Trim(); }
                //catch { }

                Sta ta = new Sta(state.SsConnectionString);
                DataTable dt = new DataTable();
                try
                {
                    string sql = "select system_code, account_side, vcgrp_no from vcvoucherdet where voucher_no = '" + vcNo + "' and account_id = '" + queryStrAccId + "' and coop_id = '" + state.SsCoopControl + "' and seq_no = '" + queryStrSeq + "'";
                    dt = ta.Query(sql);
                    s_code1 = dt.Rows[0]["system_code"].ToString();
                    grp_no1 = dt.Rows[0]["vcgrp_no"].ToString();
                    acc_side1 = dt.Rows[0]["account_side"].ToString();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
                
                DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));
                Dw_main.SetItemDate(1, "voucher_date", vcDate);
                Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                Dw_main.SetItemString(1, "voucher_desc", queryStrVcDesc);
                Dw_detail.Retrieve(vcNo, state.SsCoopControl, queryStrAccId, s_code1, grp_no1, acc_side1);
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

        #region WebDialog Members

        public void InitJsPostBack()
        {
            //JavaScript PostBack
            postInsertDwDetail = WebUtil.JsPostBack(this, "postInsertDwDetail");
            postVoucherEdit = WebUtil.JsPostBack(this, "postVoucherEdit");
            postAddnewUpdateVoucher = WebUtil.JsPostBack(this, "postAddnewUpdateVoucher");
            postDeleteRowDetail = WebUtil.JsPostBack(this, "postDeleteRowDetail");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh"); 
        }

        public void WebDialogLoadBegin()
        {

            this.ConnectSQLCA();

            n_accountClient accservice = wcf.NAccount;//ประกาศ new
            Dw_main.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);

            tDwMain = new DwThDate(Dw_main);
            tDwMain.Add("voucher_date", "voucher_tdate"); // วันที่ภาษาอังกฤษและวันที่ภาษาไทย
            try
            {
                if (!IsPostBack)
                {
                    try
                    {
                        Dw_main.InsertRow(0);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = ex.ToString();
                    }


                    try
                    {
                        String queryStrVcNo = "";
                        String queryStrVcDate = "";
                        String queryStrAccId = "";

                        try { queryStrVcNo = Request["vcNo"].Trim(); }
                        catch { }

                        try { queryStrVcDate = Request["vcDate"].Trim(); }
                        catch { }

                        try { queryStrAccId = Request["acc_id"].Trim(); }
                        catch { }


                        if (queryStrVcNo != "")
                        {
                            Dw_main.SetItemString(1, "voucher_no", queryStrVcNo);

                            JsPostVoucherEdit();
                        }
                        else if (queryStrVcDate != "")
                        {
                            DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));

                            if (vcDate.Year > 1370)
                            {
                                // การ set วันที่ด้วย code behind
                                Dw_main.SetItemDate(1, "voucher_date", vcDate);
                                Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = ex.ToString();
                    }

                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                    this.RestoreContextDw(Dw_detail);
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = ex.ToString();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            Decimal seq;
            String grp_no1 = "";
            String s_code1 = "";
            String acc_side1 = "";

            if (eventArg == "postInsertDwDetail")
            {
                String vc_no = Dw_main.GetItemString(1, "voucher_no");
                String acc_id = "";
                try { acc_id = Request["acc_id"].Trim(); }
                catch { }
                String acc_name = "";
                try { acc_name = Request["acc_name"].Trim(); }
                catch { }
                String queryStrSeq = "";
                try { queryStrSeq = Request.QueryString["seq_no"].ToString().Trim(); }
                catch { }
                //String s_code1 = "";
                //try { s_code1 = Request["s_code"].Trim(); }
                //catch { }
                //String grp_no1 = "";
                //try { grp_no1 = Request["grp_no"].Trim(); }
                //catch { }
                //String acc_side1 = "";
                //try { acc_side1 = Request["acc_side"].Trim(); }
                //catch { }
                Sta ta = new Sta(state.SsConnectionString);
                DataTable dt = new DataTable();
                try
                {
                    string sql = "select system_code, account_side, vcgrp_no from vcvoucherdet where voucher_no = '" + vc_no + "' and account_id = '" + acc_id + "' and coop_id = '" + state.SsCoopControl + "' and seq_no = '" + queryStrSeq + "'";
                    dt = ta.Query(sql);

                    s_code1 = dt.Rows[0]["system_code"].ToString();
                    acc_side1 = dt.Rows[0]["account_side"].ToString();
                    grp_no1 = dt.Rows[0]["vcgrp_no"].ToString(); 
                        
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();


                //String acc_name = Dw_detail.GetItemString(1, "account_name");
                //String acc_id = Dw_detail.GetItemString(1,"account_id");
                //String acc_side = Dw_detail.GetItemString(1, "account_side");
                //String system_code = Dw_detail.GetItemString(1, "system_code");
                //String vcgrp_no = Dw_detail.GetItemString(1, "vcgrp_no");
                if (acc_side1 == "DR")
                {
                    if (Dw_detail.RowCount == 0)
                    {
                        seq = 1;
                    }
                    else
                    {
                        seq = Dw_detail.GetItemDecimal(Dw_detail.RowCount, "order_no");
                        seq = seq + 1;
                    }
                }
                else
                {
                    if (Dw_detail.RowCount == 0)
                    {
                        seq = 10;
                    }
                    else
                    {
                        seq = Dw_detail.GetItemDecimal(Dw_detail.RowCount, "order_no");
                        seq = seq + 1;
                    }
                }
                Dw_detail.InsertRow(Dw_detail.RowCount + 1);
                Dw_detail.SetItemString(Dw_detail.RowCount, "coop_id", state.SsCoopControl);
                Dw_detail.SetItemString(Dw_detail.RowCount, "account_id", acc_id);
                Dw_detail.SetItemString(Dw_detail.RowCount, "account_name", acc_name);
                Dw_detail.SetItemString(Dw_detail.RowCount, "account_side", acc_side1);
                Dw_detail.SetItemDecimal(Dw_detail.RowCount, "order_no", seq);
                Dw_detail.SetItemString(Dw_detail.RowCount, "voucher_no", vc_no);
                Dw_detail.SetItemString(Dw_detail.RowCount, "system_code", s_code1);
                Dw_detail.SetItemString(Dw_detail.RowCount, "vcgrp_no", grp_no1);
            }
            else if (eventArg == "postAddnewUpdateVoucher")
            {
                JspostAddnewUpdateVoucher();
            }
            else if (eventArg == "postVoucherEdit")
            {
                JsPostVoucherEdit();
            }
            else if (eventArg == "postDeleteRowDetail")
            {
                Int16 RowDetail = Convert.ToInt16(HdDetailRow.Value);
                Dw_detail.DeleteRow(RowDetail);

            }
            else if (eventArg == "postRefresh")
            {
               
            }

        }

        public void WebDialogLoadEnd()
        {
            
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();

        }

        #endregion
    }
}
