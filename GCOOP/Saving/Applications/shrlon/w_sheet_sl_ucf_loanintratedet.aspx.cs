using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using Sybase.DataWindow;
using System.Globalization;
namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_ucf_loanintratedet : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;
        protected String postLoanintrate;
        protected String PostEffDate;
        private DwThDate teffectiveDate;
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม
        private String pbl = "loan_ucf.pbl";

        #region WebSheet Members
        public void InitJsPostBack()
        {
            teffectiveDate = new DwThDate(DwMain, this);
            teffectiveDate.Add("effective_date", "effective_tdate");
            teffectiveDate.Add("expire_date", "expire_tdate");
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postLoanintrate = WebUtil.JsPostBack(this, "postLoanintrate");
            PostEffDate = WebUtil.JsPostBack(this, "PostEffDate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwHead.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwHead);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertRow")
            {
                jspostInsertRow();
            }
            else if (eventArg == "postDeleteRow")
            {
                jspostDeleteRow();
            }
            else if (eventArg == "postLoanintrate")
            {
                jspostLoanintrate();
            }
            else if (eventArg == "PostEffDate")
            {
                Int16 rowMain = Convert.ToInt16(Hd_row.Value);
                int i = rowMain - 1;
                DateTime effective_date = DwMain.GetItemDateTime(rowMain, "effective_date");
                DateTime eff_date = effective_date.AddDays(-1);
                DwMain.SetItemDateTime(i, "expire_date", eff_date);
                DwMain.SetItemString(i, "expire_tdate", Convert.ToString(eff_date));




            }

        }

        public void SaveWebSheet()
        {
            SaveInfo();
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(DwHead, "loanintrate_code", pbl, null);
            }
            catch { }
            teffectiveDate.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwHead.SaveDataCache();
        }
        #endregion

        private void jspostInsertRow()
        {
            int seq = 0;
            DwMain.InsertRow(0);
            DwMain.ScrollLastPage();
            for (int i = 1; i < DwMain.RowCount; i++)
            {
                if (DwMain.GetItemDecimal(i, "seq_no") > seq)
                {
                    seq = (int)DwMain.GetItemDecimal(i, "seq_no");
                }
            }
            DwMain.SetItemDecimal(DwMain.RowCount, "seq_no", seq + 1);
            //DateTime effective_date = state.SsWorkDate;
            //DateTime expire_date = state.SsWorkDate;
            int rowcount = DwMain.RowCount;
            DateTime expire_d = new DateTime(9456, 12, 31);
            DwMain.SetItemDateTime(rowcount, "expire_date", expire_d); //Date = {7/6/2013 12:00:00 AM}
            DwMain.SetItemDateTime(rowcount, "effective_date", state.SsWorkDate);
            DwMain.SetItemDateTime(rowcount-1, "expire_date", state.SsWorkDate.AddDays(-1));
            DateTime effective_date = DwMain.GetItemDateTime(rowcount, "effective_date");
            DateTime expire_date = DwMain.GetItemDateTime(rowcount, "expire_date");
            //DateTime ex_date = expire_date.AddDays(-1);
            //DwMain.SetItemDateTime(rowcount, "expire_date", ex_date);


        }

        private void jspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
            try
            {
                string sqlselect = @"SELECT * FROM lccfloanintratedet WHERE loanintrate_code = '" + DwHead.GetItemString(1, "loanintrate_code") + "' and seq_no = '" +
                    DwMain.GetItemDecimal(RowDetail, "seq_no").ToString() + "'";
                Sdt select = WebUtil.QuerySdt(sqlselect);
                if (select.Next())
                {
                    string sqldelete = @"DELETE FROM lccfloanintratedet WHERE loanintrate_code = '" + DwHead.GetItemString(1, "loanintrate_code") + "' and seq_no = '" +
                        DwMain.GetItemDecimal(RowDetail, "seq_no").ToString() + "'";
                    Sdt delete = WebUtil.QuerySdt(sqldelete);
                    DwMain.DeleteRow(RowDetail);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                }
                else
                {
                    DwMain.DeleteRow(RowDetail);
                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเร็จ");
            }
        }

        private void jspostLoanintrate()
        {
            string intrateCode = DwHead.GetItemString(1, "loanintrate_code");
            if (intrateCode != "")
            {
                DwMain.Retrieve(intrateCode);
            }
        }

        private void SaveInfo()
        {
            try
            {
                teffectiveDate.Thai2EngAllRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("aaaa" + ex);
            }
            int rowcount = DwMain.RowCount;

            for (int k = 1; k <= rowcount; k++)
            {


                DateTime effective_d = DwMain.GetItemDateTime(k, "effective_date");
                DateTime effective_d2 = DwMain.GetItemDateTime(rowcount, "effective_date");
                DateTime expire_d = DwMain.GetItemDateTime(k, "expire_date");
                DateTime expire_d2 = DwMain.GetItemDateTime(rowcount, "expire_date");
                decimal low_amt = DwMain.GetItemDecimal(k, "lower_amt");
                decimal low_amt2 = DwMain.GetItemDecimal(rowcount, "lower_amt");
                decimal up_amt = DwMain.GetItemDecimal(k, "upper_amt");
                decimal up_amt2 = DwMain.GetItemDecimal(rowcount, "upper_amt");
                //if (effective_d >= expire_d)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("วันที่ไม่ถูกต้องกรุณาตรวจสอบ");
                //}
                //else if (effective_d2 <= expire_d)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("วันที่ไม่ถูกต้องกรุณาตรวจสอบ");
                //}else if (effective_d2 >= expire_d2)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("วันที่ไม่ถูกต้องกรุณาตรวจสอบ");
                //}
                //else if (low_amt >= up_amt)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("ยอดเงินไม่ถูกต้องกรุณาตรวจสอบ");
                //} 
                //else if (low_amt2 <= up_amt)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("ยอดเงินไม่ถูกต้องกรุณาตรวจสอบ");
                //}
                //else if (low_amt2 >= up_amt2)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("ยอดเงินไม่ถูกต้องกรุณาตรวจสอบ");
                //}
                //else
                //{

                bool flag = true;
                string erroe_code = "";
                string loanintrate_code = DwHead.GetItemString(1, "loanintrate_code");
                string seq_no = "";
                string lower_amt = "";
                string upper_amt = "";
                string interest_rate = "";
                DateTime effective_date = state.SsWorkDate;
                DateTime expire_date = state.SsWorkDate;
                InsertRow = DwMain.RowCount;
                string sqlcount = @"SELECT * FROM lccfloanintratedet WHERE loanintrate_code = '" + loanintrate_code + "'";
                Sdt count = WebUtil.QuerySdt(sqlcount);
                DataRow = count.GetRowCount();
                try
                {
                    for (int j = 1; j <= DataRow; j++)
                    {
                        seq_no = DwMain.GetItemDecimal(j, "seq_no").ToString();
                        lower_amt = DwMain.GetItemDecimal(j, "lower_amt").ToString();
                        upper_amt = DwMain.GetItemDecimal(j, "upper_amt").ToString();
                        interest_rate = DwMain.GetItemDecimal(j, "interest_rate").ToString();
                        try
                        {
                            String tDate2 = DwMain.GetItemString(j, "effective_tdate");
                            String tDate3 = DwMain.GetItemString(j, "expire_tdate");
                            DateTime dt2 = DateTime.ParseExact(tDate2, "ddMMyyyy", WebUtil.TH);
                            DateTime dt3 = DateTime.ParseExact(tDate3, "ddMMyyyy", WebUtil.TH);
                            DwMain.SetItemDateTime(j, "effective_date", dt2);
                            DwMain.SetItemDateTime(j, "expire_date", dt3);
                            effective_date = DwMain.GetItemDateTime(j, "effective_date");
                            expire_date = DwMain.GetItemDateTime(j, "expire_date");
                        }
                        catch { }

                        string sqlupdate = @"UPDATE lccfloanintratedet SET effective_date = to_date('" + effective_date.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy'),expire_date = to_date('" + expire_date.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy'), lower_amt = '" +
                            lower_amt + "', upper_amt = '" + upper_amt + "', interest_rate = '" + interest_rate + "' WHERE loanintrate_code = '" + loanintrate_code + "' and seq_no = '" +
                                seq_no + "'";
                        Sdt update = WebUtil.QuerySdt(sqlupdate);
                    }

                    for (int i = DataRow + 1; i <= InsertRow; i++)
                    {
                        try
                        {
                            seq_no = DwMain.GetItemDecimal(i, "seq_no").ToString();
                            lower_amt = DwMain.GetItemDecimal(i, "lower_amt").ToString();
                            upper_amt = DwMain.GetItemDecimal(i, "upper_amt").ToString();
                            interest_rate = DwMain.GetItemDecimal(i, "interest_rate").ToString();
                            try
                            {
                                String tDate2 = DwMain.GetItemString(i, "effective_tdate").Replace("/", "");
                                String tDate3 = DwMain.GetItemString(i, "expire_tdate").Replace("/", "");
                                DateTime dt2 = DateTime.ParseExact(tDate2, "ddMMyyyy", WebUtil.TH);
                                DateTime dt3 = DateTime.ParseExact(tDate3, "ddMMyyyy", WebUtil.TH);
                                DwMain.SetItemDateTime(i, "effective_date", dt2);
                                DwMain.SetItemDateTime(i, "expire_date", dt3);
                                effective_date = DwMain.GetItemDateTime(i, "effective_date");
                                expire_date = DwMain.GetItemDateTime(i, "expire_date");
                            }
                            catch { }

                            string sqlinsert = @"INSERT INTO lccfloanintratedet (coop_id,loanintrate_code,seq_no,effective_date,expire_date,lower_amt,upper_amt,interest_rate) VALUES('" +
                                state.SsCoopId + "','" + loanintrate_code + "','" + seq_no + "', to_date('" + effective_date.ToString("dd/MM/yyyy", WebUtil.EN) +
                                "','dd/mm/yyyy'),to_date('" + expire_date.ToString("dd/MM/yyyy", WebUtil.EN) +
                                "','dd/mm/yyyy'), '" + lower_amt + "','" + upper_amt + "','" + interest_rate + "')";
                            Sdt insert = WebUtil.QuerySdt(sqlinsert);
                        }
                        catch
                        {
                            if (!flag)
                            {
                                erroe_code += ", ";
                            }
                            erroe_code += seq_no;
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    }
                    else
                    {
                        DwMain.Reset();
                        DwMain.Retrieve();
                        LtServerMessage.Text = WebUtil.ErrorMessage("ข้อมูลลำดับที่ " + erroe_code + " ไม่สามารถบันทึกได้");
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
                }
                //}
            }
        }
    }
}