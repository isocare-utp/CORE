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
    public partial class w_acc_delserise : PageWebSheet, WebSheet
    {
        protected String jsPostEdit;
        protected String jsPostDelete;
        protected String jsPostAccountid;
        protected String jsPostInsertDwRate;
        protected String jsPostDeleteDwRate;
        protected String jsPostBlank;
        protected String jsPostDupComplete;
        protected String jsPostReplaceAll;
        DwThDate tReceive;
        DwThDate tDwRate;
        string pbl = "asset.pbl";
        private n_accountClient accService;//ประกาศตัวแปร WebService

        #region WebSheet Members
        public void InitJsPostBack()
        {
            tReceive = new DwThDate(Dwmain, this);
            tReceive.Add("receive_date", "receive_tdate");
            tDwRate = new DwThDate(DwRate, this);
            tDwRate.Add("efective_date", "efective_tdate");
            tDwRate.Add("expire_date", "expire_tdate");
            jsPostEdit = WebUtil.JsPostBack(this, "jsPostEdit");
            jsPostDelete = WebUtil.JsPostBack(this, "jsPostDelete");
            jsPostAccountid = WebUtil.JsPostBack(this, "jsPostAccountid");
            jsPostInsertDwRate = WebUtil.JsPostBack(this, "jsPostInsertDwRate");
            jsPostDeleteDwRate = WebUtil.JsPostBack(this, "jsPostDeleteDwRate");
            jsPostBlank = WebUtil.JsPostBack(this, "jsPostBlank");
            jsPostDupComplete = WebUtil.JsPostBack(this, "jsPostDupComplete");
            jsPostReplaceAll = WebUtil.JsPostBack(this, "jsPostReplaceAll");
        }

        public void WebSheetLoadBegin()
        {
            accService = wcf.NAccount;//ประกาศ new
            this.ConnectSQLCA();
            Dwlist.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                //Getyear();
                ResetDwmain();
                InsertDwRate();
                RetriveList();
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwlist);
                this.RestoreContextDw(DwRate, tDwRate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostEdit":
                    EditData();
                    break;
                case "jsPostDelete":
                    DeleteData();
                    break;
                case "jsPostAccountid":
                    PostAccountid();
                    break;
                case "jsPostInsertDwRate":
                    InsertDwRate();
                    break;
                case "jsPostDeleteDwRate":
                    DeleteDwRate();
                    break;
                case "jsPostDupComplete":
                    DupComplete();
                    break;
                case "jsPostReplaceAll":
                    //ReplaceAll();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            Decimal add_amount = Dwmain.GetItemDecimal(1, "add_amount");
            Dwmain.SetItemDecimal(1, "before_amount", add_amount);
            Dwmain.SetItemDecimal(1, "total_amount", add_amount);
            Dwmain.SetItemDecimal(1, "standing_balance", add_amount);

            String xml = Dwmain.Describe("Datawindow.Data.XML");
            String ratexml = DwRate.Describe("Datawindow.Data.XML");

            try
            {
                int result = accService.of_save_asset(state.SsWsPass, xml, ratexml, state.SsCoopId);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อย");
                    Hdasset_doc.Value = Dwmain.GetItemString(1, "asset_doc");
                    Hdreceive_tdate.Value = Dwmain.GetItemString(1, "receive_tdate");
                    if (Dwmain.GetItemDecimal(1, "flag") == 0)
                    {
                        IsOpenDuplicate.Value = "true";
                    }
                    else if (Dwmain.GetItemDecimal(1, "flag") == 1)
                    {
                        //HdAssGroup_new.Value = Dwmain.GetItemString(1, "asset_doc_grp");
                        //ItemStatus status = Dwmain.GetItemStatus(1, "asset_doc_grp", 0);
                        //if (status == ItemStatus.Modified)
                        //{
                        //    if (CountAssetDocGroup())
                        //    {
                        //        IsReplaceAll.Value = "true";
                        //    }
                        //}
                    }
                    ResetDwmain();
                    DwRate.Reset();
                    InsertDwRate();
                    RetriveList();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(Dwmain, "type_series", pbl, null);
            }
            catch { }
            tReceive.Eng2ThaiAllRow();
            tDwRate.Eng2ThaiAllRow();
            Dwmain.SaveDataCache();
            Dwlist.SaveDataCache();
            DwRate.SaveDataCache();
        }
        #endregion

        #region Function
        //private void Getyear()
        //{
        //    short year = 0;
        //    short period = 0;
        //    short result = 1;// accService.of_get_year_period(state.SsWsPass, state.SsWorkDate, state.SsCoopId, ref year, ref period);
        //    if (result == 1)
        //    {
        //        Hdyear.Value = year.ToString();
        //    }
        //}

        private void ResetDwmain()
        {
            Dwmain.Reset();
            Dwmain.InsertRow(0);
            Dwmain.SetItemDecimal(1, "flag", 0);
            Dwmain.SetItemString(1, "coop_id", state.SsCoopId);
            Dwmain.SetItemString(1, "master_branch_id", state.SsCoopId);
            //Dwmain.SetItemString(1, "typedel_branch_id", state.SsCoopId);
            //Dwmain.SetItemDecimal(1, "year", Convert.ToInt32(Hdyear.Value));
        }

        private void RetriveList()
        {
            DwUtil.RetrieveDataWindow(Dwlist, pbl, null, state.SsCoopId);
            Filter();
        }

        private void EditData()
        {
            int row = Convert.ToInt32(Hdrow.Value);
            ResetDwmain();
            String asset_doc = Dwlist.GetItemString(row, "asset_doc");
            DwUtil.RetrieveDataWindow(Dwmain, pbl, null, asset_doc);
            Dwmain.SetItemDecimal(1, "flag", 1);
            //HdAssGroup.Value = Dwmain.GetItemString(1, "asset_doc_grp");
            Hdasset_doc.Value = Dwmain.GetItemString(1, "asset_doc");
            tReceive.Eng2ThaiAllRow();
            Hdreceive_tdate.Value = Dwmain.GetItemString(1, "receive_tdate");
            //try { Dwmain.SetItemString(1, "asset_doc", Dwlist.GetItemString(row, "asset_doc")); }
            //catch { }
            //try { Dwmain.SetItemString(1, "desc_text", Dwlist.GetItemString(row, "desc_text")); }
            //catch { }
            //try { Dwmain.SetItemString(1, "account_id", Dwlist.GetItemString(row, "account_id")); }
            //catch { }
            //try { Dwmain.SetItemString(1, "type_series", Dwlist.GetItemString(row, "type_series")); }
            //catch { }
            //try { Dwmain.SetItemString(1, "desc_why", Dwlist.GetItemString(row, "desc_why")); }
            //catch { }
            //try { Dwmain.SetItemDecimal(1, "before_amt", Dwlist.GetItemDecimal(row, "before_amt")); }
            //catch { }
            //try { Dwmain.SetItemDecimal(1, "add_amount", Dwlist.GetItemDecimal(row, "add_amount")); }
            //catch { }
            //try { Dwmain.SetItemDecimal(1, "type_of_caldp", Dwlist.GetItemDecimal(row, "type_of_caldp")); }
            //catch { }
            //try { Dwmain.SetItemDateTime(1, "receive_date", Dwlist.GetItemDateTime(row, "receive_date")); }
            //catch { }
            //try { Dwmain.SetItemDecimal(1, "status", Dwlist.GetItemDecimal(row, "status")); }
            //catch { }
            //try { Dwmain.SetItemString(1, "asset_doc_grp", Dwlist.GetItemString(row, "asset_doc_grp")); }
            //catch { }
            
            DwUtil.RetrieveDataWindow(DwRate, pbl, null, Dwlist.GetItemString(row, "asset_doc"));
        }

        private void DeleteData()
        {
            try
            {
                int row = Convert.ToInt32(Hdrow.Value);
                String asset_doc = Dwlist.GetItemString(row, "asset_doc");

                string sqldelete = @"DELETE FROM acc_cal_dp WHERE asset_doc = '" + asset_doc + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);

                string sqldelete2 = @"DELETE FROM acc_dp_rate WHERE asset_doc = '" + asset_doc + "'";
                Sdt delete2 = WebUtil.QuerySdt(sqldelete2);

                string sqldelete3 = @"DELETE FROM acc_asset_year WHERE asset_doc = '" + asset_doc + "'";
                Sdt delete3 = WebUtil.QuerySdt(sqldelete3);

                string sqldelete4 = @"DELETE FROM acc_delseries WHERE asset_doc = '" + asset_doc + "'";
                Sdt delete4 = WebUtil.QuerySdt(sqldelete4);

                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเรียบร้อยแล้ว");
                ResetDwmain();
                DwRate.Reset();
                InsertDwRate();
                RetriveList();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
            }
        }

        private void PostAccountid()
        {
            Dwmain.SetItemString(1, "account_id", Hdacc_id.Value);
        }

        private void InsertDwRate()
        {
            int row = DwRate.InsertRow(0);
            DwRate.SetItemString(row, "coop_id", state.SsCoopId);
        }

        private void DeleteDwRate()
        {
            int row = Convert.ToInt32(Hdrow.Value);
            DwRate.DeleteRow(row);
        }

        private void DupComplete()
        {
            LtServerMessage.Text = WebUtil.CompleteMessage("คัดลอกสินทรัพย์สำเร็จ");
            RetriveList();
        }

        private bool CountAssetDocGroup()
        {
            String asset_doc_grp = HdAssGroup.Value;
            String sqlcount = "select count(*) from acc_delseries where asset_doc_grp = '" + asset_doc_grp + "'";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            if (count.Next())
            {
                if (count.GetDecimal("count(*)") > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //private void ReplaceAll()
        //{
        //    String asset_doc_grp_old = HdAssGroup.Value;
        //    String asset_doc_grp = HdAssGroup_new.Value;
        //    String sqlreplace = "update acc_delseries set asset_doc_grp = '" + asset_doc_grp + "' where asset_doc_grp = '" + asset_doc_grp_old + "'";
        //    Sdt replace = WebUtil.QuerySdt(sqlreplace);

        //    ResetDwmain();
        //    DwRate.Reset();
        //    InsertDwRate();
        //    RetriveList();
        //}

        protected void Radio1_CheckChanged(object sender, EventArgs e)
        {
            if (Radio1.Checked)
            {
                //Radio2.Checked = false;
            }
            Filter();
        }

        //protected void Radio2_CheckChanged(object sender, EventArgs e)
        //{
        //    if (Radio2.Checked)
        //    {
        //        Radio1.Checked = false;
        //    }
        //    Filter();
        //}

        private void Filter()
        {
            if (Radio1.Checked)
            {
                Dwlist.SetFilter("type_of_caldp = 1");
                Dwlist.Filter();
            }
            else
            {
                Dwlist.SetFilter("type_of_caldp <> 1");
                Dwlist.Filter();
            }
        }
        #endregion
    }
}