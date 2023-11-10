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

namespace Saving.Applications.account
{
    public partial class w_acc_ucf_template : PageWebSheet, WebSheet
    {
        String pbl = "cm_constant_config.pbl";
        protected String jsPostVouchertypeChange;
        protected String jsPostDwmainInsertRow;
        protected String jsPostGetDetail;
        protected String jsPostDwmainDeleteRow;
        protected String jsPostDwdetailInsertRow;
        protected String jsPostAccountid;
        protected String jsPostSelectDetail;
        protected String jsPostDwdetailDeleteRow;
        protected String jsPostAllDeleteRow;
        private n_accountClient accService;//ประกาศตัวแปร WebService
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        #region WebSheet Member
        public void InitJsPostBack()
        {
            jsPostVouchertypeChange = WebUtil.JsPostBack(this, "jsPostVouchertypeChange");
            jsPostDwmainInsertRow = WebUtil.JsPostBack(this, "jsPostDwmainInsertRow");
            jsPostGetDetail = WebUtil.JsPostBack(this, "jsPostGetDetail");
            jsPostDwmainDeleteRow = WebUtil.JsPostBack(this, "jsPostDwmainDeleteRow");
            jsPostDwdetailInsertRow = WebUtil.JsPostBack(this, "jsPostDwdetailInsertRow");
            jsPostAccountid = WebUtil.JsPostBack(this, "jsPostAccountid");
            jsPostSelectDetail = WebUtil.JsPostBack(this, "jsPostSelectDetail");
            jsPostDwdetailDeleteRow = WebUtil.JsPostBack(this, "jsPostDwdetailDeleteRow");
            jsPostAllDeleteRow = WebUtil.JsPostBack(this, "jsPostAllDeleteRow");
        }

        public void WebSheetLoadBegin()
        {
            accService = wcf.NAccount;//ประกาศ new

            if (!IsPostBack)
            {
                Dwhead.InsertRow(0);
                Getyear(state.SsWorkDate);
            }
            else
            {
                this.RestoreContextDw(Dwhead);
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwdetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostVouchertypeChange":
                    VouchertypeChange();
                    break;
                case "jsPostDwmainInsertRow":
                    DwmainInsertRow();
                    break;
                case "jsPostGetDetail":
                    GetDetail();
                    break;
                case "jsPostDwmainDeleteRow":
                    DwmainDeleteRow();
                    break;
                case "jsPostDwdetailInsertRow":
                    DwdetailInsertRow();
                    break;
                case "jsPostAccountid":
                    PostAccountid();
                    break;
                case "jsPostSelectDetail":
                    SelectDetail();
                    break;
                case "jsPostDwdetailDeleteRow":
                    DwdetailDeleteRow();
                    break;
                case "jsPostAllDeleteRow":
                    AllDeleteRow();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            bool flag = true;
            string erroe_code = "";
            string tvcauto_code = "";
            string vcauto_code = "";
            string vcauto_desc = "";
            string voucher_type = Dwhead.GetItemString(1, "voucher_type");
            InsertRow = Dwmain.RowCount;
            string sqlcount = @"SELECT * FROM vcautomas WHERE voucher_type='" + voucher_type + "'";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            DataRow = count.GetRowCount();
            try
            {
                for (int j = 1; j <= DataRow; j++)
                {
                    vcauto_code = Dwmain.GetItemString(j, "vcauto_code");
                    vcauto_desc = Dwmain.GetItemString(j, "vcauto_desc");
                    string sqlupdate = @"UPDATE vcautomas SET vcauto_desc = '" + vcauto_desc + "' WHERE vcauto_code = '" + vcauto_code + "'";
                    Sdt update = WebUtil.QuerySdt(sqlupdate);
                }

                for (int i = DataRow + 1; i <= InsertRow; i++)
                {
                    try
                    {
                        tvcauto_code = Dwmain.GetItemString(i, "tvcauto_code");
                        if (tvcauto_code.Length < 2)
                        {
                            tvcauto_code = "00" + tvcauto_code;
                        }
                        else if (tvcauto_code.Length < 3)
                        {
                            tvcauto_code = "0" + tvcauto_code;
                        }
                        Dwmain.SetItemString(i, "tvcauto_code", tvcauto_code);
                        Dwmain.SetItemString(i, "vcauto_code", Dwmain.GetItemString(i, "tvcauto_code_prefix") + tvcauto_code);

                        vcauto_code = Dwmain.GetItemString(i, "vcauto_code");
                        vcauto_desc = Dwmain.GetItemString(i, "vcauto_desc");
                        string sqlinsert = @"INSERT INTO vcautomas VALUES('" + vcauto_code + "','" + state.SsCoopId + "','" + vcauto_desc + "','" + voucher_type + "')";
                        Sdt insert = WebUtil.QuerySdt(sqlinsert);
                    }
                    catch
                    {
                        if (!flag)
                        {
                            erroe_code += ", ";
                        }
                        erroe_code += vcauto_code;
                        flag = false;
                    }
                }

                SaveDetail();

                if (flag)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                }
                else
                {
                    Dwmain.Reset();
                    VouchertypeChange();
                    LtServerMessage.Text = WebUtil.ErrorMessage("รหัสTemplate " + erroe_code + " มีอยู่แล้ว");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(Dwhead, "voucher_type", pbl, null);
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(Dwdetail, "account_id", pbl, null);
            }
            catch { }
            Dwhead.SaveDataCache();
            Dwmain.SaveDataCache();
            Dwdetail.SaveDataCache();
        }
        #endregion

        #region Function
        private void VouchertypeChange()
        {
            try
            {
                DwUtil.RetrieveDataWindow(Dwmain, pbl, null, Dwhead.GetItemString(1, "voucher_type"));

                for (int i = 1; i <= Dwmain.RowCount; i++)
                {
                    Dwmain.SetItemString(i, "tvcauto_code_prefix", Dwmain.GetItemString(i, "vcauto_code").Substring(0, 2));
                    Dwmain.SetItemString(i, "tvcauto_code", Dwmain.GetItemString(i, "vcauto_code").Substring(2, 3));
                }

                Dwdetail.Reset();
            }
            catch { }
        }

        private void DwmainInsertRow()
        {
            Dwmain.InsertRow(0);
            Dwmain.SetItemString(Dwmain.RowCount, "tvcauto_code_prefix", Dwhead.GetItemString(1, "voucher_type"));
        }

        private void DwmainDeleteRow()
        {
            Int16 RowSelect = Convert.ToInt16(Hdrow_mas.Value);
            try
            {
                string sqldelete = @"DELETE FROM vcautomas WHERE vcauto_code = '" + Dwmain.GetItemString(RowSelect, "vcauto_code") + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                Dwmain.DeleteRow(RowSelect);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                Hdrow_mas.Value = "";
            }
            catch
            {
                try
                {
                    Dwmain.GetItemString(RowSelect, "vcauto_code");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    Dwmain.DeleteRow(RowSelect);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                    Hdrow_mas.Value = "";
                }
            }
        }

        private void GetDetail()
        {
            try
            {
                int rowcurrent = int.Parse(Hdrow_mas.Value);
                Dwmain.SelectRow(0, false);
                Dwmain.SelectRow(rowcurrent, true);
                string vcauto_code = Dwmain.GetItemString(rowcurrent, "vcauto_code");
                if (vcauto_code == "" || vcauto_code == null)
                {
                    vcauto_code = Dwmain.GetItemString(rowcurrent, "tvcauto_code_prefix") + Dwmain.GetItemString(rowcurrent, "tvcauto_code");
                }

                DwUtil.RetrieveDataWindow(Dwdetail, pbl, null, vcauto_code);
            }
            catch
            {
                Dwdetail.Reset();
            }
        }

        private void DwdetailInsertRow()
        {
            int row = Dwdetail.InsertRow(0);
            Hdrow_det.Value = row.ToString();
            SelectDetail();
        }

        private void PostAccountid()
        {
            Int16 RowSelect = Convert.ToInt16(Hdrow_det.Value);
            Dwdetail.SetItemString(RowSelect, "account_id", Hdacc_id.Value);
        }

        private void SaveDetail()
        {
            Int16 MainRowSelect = Convert.ToInt16(Hdrow_mas.Value);
            int RowCount = Dwdetail.RowCount;
            string vcauto_code = Dwmain.GetItemString(MainRowSelect, "vcauto_code");

            if (RowCount > 0)
            {
                try
                {
                    string sqldelete = @"DELETE FROM vcautodet WHERE vcauto_code = '" + vcauto_code + "'";
                    Sdt delete = WebUtil.QuerySdt(sqldelete);

                    for (int i = 1; i <= RowCount; i++)
                    {
                        string sqlinsert = @"INSERT INTO vcautodet VALUES('" + vcauto_code + "','" + state.SsCoopId + "','" + i + "','" + Dwdetail.GetItemString(i, "account_side")
                            + "','" + Dwdetail.GetItemString(i, "account_id") + "')";
                        Sdt insert = WebUtil.QuerySdt(sqlinsert);
                    }
                }
                catch { }
            }
        }

        private void SelectDetail()
        {
            int rowcurrent = int.Parse(Hdrow_det.Value);
            Dwdetail.SelectRow(0, false);
            Dwdetail.SelectRow(rowcurrent, true);
        }

        private void DwdetailDeleteRow()
        {
            Int16 MainRowSelect = Convert.ToInt16(Hdrow_mas.Value);
            Int16 RowSelect = Convert.ToInt16(Hdrow_det.Value);
            try
            {
                string sqldelete = @"DELETE FROM vcautodet WHERE vcauto_code = '" + Dwmain.GetItemString(MainRowSelect, "vcauto_code") + "' and seq_no = '"
                    + Dwdetail.GetItemDecimal(RowSelect, "seq_no") + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                Dwdetail.DeleteRow(RowSelect);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                Hdrow_det.Value = "";
            }
            catch
            {
                try
                {
                    Dwdetail.GetItemString(RowSelect, "seq_no");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    Dwdetail.DeleteRow(RowSelect);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                    Hdrow_det.Value = "";
                }
            }
        }

        private void AllDeleteRow()
        {
            Int16 MainRowSelect = Convert.ToInt16(Hdrow_mas.Value);
            try
            {
                string sqldelete = @"DELETE FROM vcautodet WHERE vcauto_code = '" + Dwmain.GetItemString(MainRowSelect, "vcauto_code") + "'";
                Sdt deletedet = WebUtil.QuerySdt(sqldelete);
                Dwdetail.Reset();

                sqldelete = @"DELETE FROM vcautomas WHERE vcauto_code = '" + Dwmain.GetItemString(MainRowSelect, "vcauto_code") + "'";
                Sdt deletemain = WebUtil.QuerySdt(sqldelete);
                Dwmain.DeleteRow(MainRowSelect);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                Hdrow_mas.Value = "";
                Hdrow_det.Value = "";
            }
            catch
            {
                try
                {
                    Dwmain.GetItemString(MainRowSelect, "vcauto_code");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    Dwdetail.Reset();
                    Dwmain.DeleteRow(MainRowSelect);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                    Hdrow_mas.Value = "";
                    Hdrow_det.Value = "";
                }
            }
        }

        private void Getyear(DateTime dt)
        {
            short year = 0;
            short period = 0;
            short result = accService.of_get_year_period(state.SsWsPass, dt, state.SsCoopId, ref year, ref period);
            if (result == 1)
            {
                Hdyear.Value = year.ToString();
            }
        }
        #endregion
    }
}