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

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_sl_sharetype_detail : PageWebSheet, WebSheet
    {
        private String sharetype_Code;
        private DwThDate tDw_data2;

        protected String itemChangedReload;
        protected String insertRowTab2;
        protected String ShareTypeChange;
        protected String shareRateChange;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            itemChangedReload = WebUtil.JsPostBack(this, "itemChangedReload");
            ShareTypeChange = WebUtil.JsPostBack(this, "ShareTypeChange");
            shareRateChange = WebUtil.JsPostBack(this, "shareRateChange");
            insertRowTab2 = WebUtil.JsPostBack(this, "insertRowTab2");

            tDw_data2 = new DwThDate(dw_data2, this);
            tDw_data2.Add("entry_date", "entry_tdate");
        }

        public void WebSheetLoadBegin()
        {
            //sharetype_Code = "01";
            this.ConnectSQLCA();
            dw_data1.SetTransaction(sqlca);
            dw_data2.SetTransaction(sqlca);


            if (IsPostBack)
            {
                dw_data1.RestoreContext();
                dw_data2.RestoreContext();
                dw_membtype.RestoreContext();
            }
            else
            {
                // GetShateDetail();
                dw_membtype.Reset();
                dw_data1.InsertRow(0);
               
                dw_membtype.InsertRow(0);
                
                DwUtil.RetrieveDDDW(dw_data1, "sharegroup_code", "sl_sharetype_detail.pbl", state.SsCoopId);
                string share_type = dw_data1.GetItemString(1, "sharegroup_code");
                dw_data1.Retrieve(state.SsCoopId, share_type);
              
            }

            // HdUser.Value = state.SsUsername;
        }



        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "itemChangedReload")
            {
            }
            else if (eventArg == "insertRowTab2")
            {
                HdCheckRow.Value = dw_data2.RowCount.ToString();
                dw_data2.InsertRow(0);
                decimal seq_no = dw_data2.GetItemDecimal(dw_data2.RowCount, "seq_row");
                dw_data2.SetItemString(dw_data2.RowCount, "coop_id", state.SsCoopId);
                dw_data2.SetItemDecimal(dw_data2.RowCount, "seq_no", seq_no);
                dw_data2.SetItemString(dw_data2.RowCount, "entry_id", state.SsUsername);
                string sharetype_code = dw_data1.GetItemString(1, "sharegroup_code");
                decimal member_type = dw_membtype.GetItemDecimal(1, "member_type");
                dw_data2.SetItemString(dw_data2.RowCount, "sharetype_code", sharetype_code);
                dw_data2.SetItemDateTime(dw_data2.RowCount, "entry_date", state.SsWorkDate);
                dw_data2.SetItemDecimal(dw_data2.RowCount, "member_type", member_type);
                tDw_data2.Eng2ThaiAllRow();
                int r = dw_data2.RowCount;
            }
            else if (eventArg == "ShareTypeChange")
            {
                sharetypechange();
            }
            else if (eventArg == "shareRateChange")
            {
                ShareRateChange();
            }
        }

        private void ShareRateChange()
        {
            dw_data2.InsertRow(0);
            //string share_type = dw_data1.GetItemString(1, "sharegroup_code");
            decimal member_type = dw_membtype.GetItemDecimal(1, "member_type");
            dw_data2.Reset();
            dw_data2.Retrieve(state.SsCoopId, "01", member_type);
            tDw_data2.Eng2ThaiAllRow();
            HdCheckRow.Value = dw_data2.RowCount.ToString();
        }

        private void sharetypechange()
        {
            string share_type = dw_data1.GetItemString(1, "sharegroup_code");
            dw_data1.Retrieve(state.SsCoopId, share_type);
        }


        public void SaveWebSheet()
        {
            
            try
            {
                try
                {
                    decimal i = Convert.ToDecimal(HdCheckRow.Value);
                    if (dw_data2.RowCount > Convert.ToDecimal(HdCheckRow.Value))
                    {


                        Sta ta = new Sta(sqlca.ConnectionString);
                        string coop_id = dw_data2.GetItemString(dw_data2.RowCount, "coop_id");
                        string sharetype_code = dw_data1.GetItemString(1, "sharegroup_code");
                        decimal seq_no = dw_data2.GetItemDecimal(dw_data2.RowCount, "seq_no");
                        decimal start_salary = 0;
                        decimal end_salary = 0;
                        decimal minshare_percent = 0;
                        decimal minshare_amt = 0;
                        decimal maxshare_percent = 0;
                        decimal maxshare_amt = 0;
                        try
                        {
                            start_salary = dw_data2.GetItemDecimal(dw_data2.RowCount, "start_salary");
                        }
                        catch
                        {
                        }
                        try
                        {
                            end_salary = dw_data2.GetItemDecimal(dw_data2.RowCount, "end_salary");
                        }
                        catch
                        {
                        }
                        try
                        {
                            minshare_percent = dw_data2.GetItemDecimal(dw_data2.RowCount, "minshare_percent");
                        }
                        catch
                        {
                        }
                        try
                        {
                            minshare_amt = dw_data2.GetItemDecimal(dw_data2.RowCount, "minshare_amt");
                        }
                        catch
                        {
                        }
                        try
                        {
                            maxshare_percent = dw_data2.GetItemDecimal(dw_data2.RowCount, "maxshare_percent");
                        }
                        catch
                        {
                        }
                        try
                        {
                            maxshare_amt = dw_data2.GetItemDecimal(dw_data2.RowCount, "maxshare_amt");
                        }
                        catch
                        {
                        }
                        string entry_id = dw_data2.GetItemString(dw_data2.RowCount, "entry_id");
                        DateTime entry_date = dw_data2.GetItemDateTime(dw_data2.RowCount, "entry_date");
                        decimal member_type = dw_membtype.GetItemDecimal(1, "member_type");
                        try
                        {
                            String sql = @"insert into shsharetypemthrate (coop_id,sharetype_code,member_type,seq_no, start_salary, end_salary, minshare_percent,minshare_amt,maxshare_percent,maxshare_amt,entry_id, entry_date) values ('" + coop_id + "','" + sharetype_code + "',"+member_type+"," + seq_no + "," + start_salary + " , " + end_salary + "," + minshare_percent + "," + minshare_amt + "," + maxshare_percent + "," + maxshare_amt + ", '" + entry_id + " ',  to_date(  '" + entry_date.ToString("ddMMyyyy") + "', 'ddMMyyyy' ))";
                            ta.Exe(sql);

                        }
                        catch (Exception ex)
                        {

                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);

                        }
                        ta.Close();
                        dw_data1.UpdateData();

                    }
                    else
                    {
                        dw_data1.UpdateData();
                        dw_data2.UpdateData();
                        //DwUtil.UpdateDataWindow(dw_data1, "sl_sharetype_detail.pbl", "SHSHARETYPE");
                        //DwUtil.UpdateDataWindow(dw_data2, "sl_sharetype_detail.pbl", "SHSHARETYPEMTHRATE");
                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }


        }

        public void WebSheetLoadEnd()
        {


            dw_data1.SaveDataCache();
            dw_data2.SaveDataCache();
         
        }

        #endregion

       
    }
}
