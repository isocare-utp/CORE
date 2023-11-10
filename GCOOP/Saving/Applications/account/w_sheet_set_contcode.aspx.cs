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

namespace Saving.Applications.account
{
    public partial class w_sheet_set_contcode : PageWebSheet, WebSheet
    {
        protected String jsPostGetlist;
        string pbl = "cm_constant_config.pbl";
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        #region WebSheet Members
        public void InitJsPostBack()
        {
            jsPostGetlist = WebUtil.JsPostBack(this, "jsPostGetlist");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Dwmain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostGetlist":
                    GetList();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            bool flag = true;
            string erroe_code = "";
            decimal ff = 0;
           
            string account_year = (Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543).ToString();
            string period = Dwmain.GetItemString(1, "period");
            Decimal seq_no = 0;
            string cnt_code = "";
            Decimal cnt_short_amt = 0;
            Decimal cnt_long_amt = 0;

            InsertRow = Dwlist.RowCount;
            string sqlcount = @"SELECT * FROM acccntmoneysheet where account_year = '" + account_year + "' and period = '" + period + "' and coop_id = '"+ state.SsCoopControl+"'";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            DataRow = count.GetRowCount();
            try
            {
                for (int j = 1; j <= DataRow; j++)
                {
                    seq_no = Dwlist.GetItemDecimal(j, "seq_no");
                    try { cnt_short_amt = Dwlist.GetItemDecimal(j, "cnt_short_amt"); } catch { cnt_short_amt = 0; }
                    try { cnt_long_amt = Dwlist.GetItemDecimal(j, "cnt_long_amt"); } catch { cnt_long_amt = 0; }

                    string sqlupdate = @"UPDATE acccntmoneysheet SET cnt_short_amt = '" + cnt_short_amt + "',cnt_long_amt = '" + cnt_long_amt + "' WHERE account_year = '" +
                        account_year + "' and period = '" + period + "' and seq_no = '" + seq_no + "' and coop_id = '" + state.SsCoopControl + "'";
                    Sdt update = WebUtil.QuerySdt(sqlupdate);
                }

                for (int i = DataRow + 1; i <= InsertRow; i++)
                {
                    try
                    {
                        seq_no = i;
                        cnt_code = Dwlist.GetItemString(i, "cnt_code").Trim();
                        try { cnt_short_amt = Dwlist.GetItemDecimal(i, "cnt_short_amt"); } catch { cnt_short_amt = 0; }
                        try { cnt_long_amt = Dwlist.GetItemDecimal(i, "cnt_long_amt"); } catch { cnt_long_amt = 0; }

                        string sqlinsert = @"INSERT INTO acccntmoneysheet VALUES('" + state.SsCoopId + "','" + account_year + "','" + period + "','" + seq_no + "','" +
                            cnt_code + "','" + ff + "','" + ff + "','" + cnt_short_amt + "','" + cnt_long_amt + "')";
                        Sdt insert = WebUtil.QuerySdt(sqlinsert);
                    }
                    catch
                    {
                        if (!flag)
                        {
                            erroe_code += ", ";
                        }
                        erroe_code += cnt_code;
                        flag = false;
                    }
                }
                if (flag)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    GetList();
                }
                else
                {
                    Dwlist.Reset();
                    //Dwlist.Retrieve();
                    //DwUtil.RetrieveDataWindow(Dwlist, "cm_constant_config.pbl",null,null);
                    DwUtil.RetrieveDataWindow(Dwlist, pbl, null , account_year, period, state.SsCoopControl);
                    LtServerMessage.Text = WebUtil.ErrorMessage("รหัสกระทบยอด " + erroe_code + " มีอยู่แล้ว");
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
                DwUtil.RetrieveDDDW(Dwlist, "cnt_code", pbl, null);
            }
            catch { }
            Dwmain.SaveDataCache();
            Dwlist.SaveDataCache();
        }
        #endregion

        #region Function
        private void GetList()
        {
            string year = (Convert.ToInt32(Dwmain.GetItemString(1, "year")) - 543).ToString();
            string period = Dwmain.GetItemString(1, "period");
            DwUtil.RetrieveDataWindow(Dwlist, pbl, null, year, period,state.SsCoopControl);

            string sqlcount = @"SELECT * FROM acccntcode where coop_id = '" + state.SsCoopControl + "' order by cnt_code ";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            int acccntcode_row = count.GetRowCount();
            int Dwlist_row = Dwlist.RowCount;
            int row = 0;

            while (count.Next())
            {
                bool IsEquals = false;
                string cnt_code = count.GetString("cnt_code").Trim();
                for (int i = 1; i <= Dwlist_row; i++)
                {
                    if(Dwlist.GetItemString(i, "cnt_code") == cnt_code)
                    {
                        IsEquals = true;
                        break;
                    }
                }
                if (!IsEquals)
                {
                    int row_new = Dwlist.InsertRow(0);
                    Dwlist.SetItemString(row_new, "cnt_code", cnt_code);
                }
                row++;
            }
        }
        #endregion
    }
}