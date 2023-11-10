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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_sharetype_detail : PageWebSheet, WebSheet
    {
        private String sharetype_Code;
        private DwThDate tDw_data2;

        protected String itemChangedReload;
        protected String getShareDetail;
        protected String getDelete;
        protected String newShareCode;
        protected String insertRowTab2;
        protected String autoSearch;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            itemChangedReload = WebUtil.JsPostBack(this, "itemChangedReload");
            getShareDetail = WebUtil.JsPostBack(this, "getShareDetail");
            getDelete = WebUtil.JsPostBack(this, "getDelete");
            newShareCode = WebUtil.JsPostBack(this, "newShareCode");
            insertRowTab2 = WebUtil.JsPostBack(this, "insertRowTab2");
            autoSearch = WebUtil.JsPostBack(this, "autoSearch");

            tDw_data2 = new DwThDate(dw_data2, this);
            tDw_data2.Add("entry_date", "entry_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try { sharetype_Code = HiddenField1.Value; }
            catch { sharetype_Code = ""; }
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            dw_data1.SetTransaction(sqlca);
            dw_data2.SetTransaction(sqlca);

            if (IsPostBack)
            {
                dw_main.RestoreContext();
                dw_data1.RestoreContext();
                dw_data2.RestoreContext();
            }

            HdUser.Value = state.SsUsername;
        }



        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "itemChangedReload")
            {
            }
            else if (eventArg == "getShareDetail")
            {
                GetShateDetail();
            }
            else if (eventArg == "getDelete")
            {
                DeleteShareType();
            }
            else if (eventArg == "newShareCode")
            {
                NewShareCode();
            }
            else if (eventArg == "insertRowTab2")
            {
                dw_data2.InsertRow(0);
                dw_data2.SetItemString(dw_data2.RowCount, "entry_id", state.SsUsername);
                dw_data2.SetItemString(dw_data2.RowCount, "sharetype_code", dw_main.GetItemString(1, "sharetype_code").Trim());
                dw_data2.SetItemDateTime(dw_data2.RowCount, "entry_date", state.SsWorkDate);
                tDw_data2.Eng2ThaiAllRow();
            }
            else if (eventArg == "autoSearch")
            {
                AutoSearch();
            }
        }

        private void AutoSearch()
        {
            String shcode = "";
            try
            {
                shcode = dw_main.GetItemString(1, "sharetype_code").Trim();
                try
                {
                    //get code from desc
                    Sta ta = new Sta(sqlca.ConnectionString);
                    try
                    {
                        String sql = @"select * from shsharetype where sharetype_code ='" + shcode + "'";
                        Sdt dt = ta.Query(sql);
                        if (!dt.Next())
                        {
                            LtServerMessage.Text = WebUtil.WarningMessage("ไม่มีรายการนี้");
                        }
                    }
                    catch (Exception ex)
                    {
                        //query
                    }


                }
                catch { }

            }
            catch
            { // check desc
                
                try
                {
                    String shdesc = dw_main.GetItemString(1, "sharetype_desc").Trim();

                    //get code from desc
                    Sta ta = new Sta(sqlca.ConnectionString);
                    try
                    {
                        String sql = @"select sharetype_code from shsharetype where sharetype_desc ='" + shdesc + "'";
                        Sdt dt = ta.Query(sql);
                        if (dt.Next())
                        {
                            shcode = dt.GetString(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        //query
                    }


                }
                catch { }
            }
            dw_data1.Reset();
            dw_data1.Retrieve(shcode);
            dw_data2.Reset();
            dw_data2.Retrieve(shcode);
            tDw_data2.Eng2ThaiAllRow();
        }




        public void SaveWebSheet()
        {
            try
            {
                dw_data1.SetItemString(1, "sharetype_code", dw_main.GetItemString(1, "sharetype_code"));
                dw_data1.UpdateData();
                try
                {
                    //อัปเดตข้อมูล
                    dw_data2.UpdateData();
                }
                catch (Exception ee)
                {
                    //เพิ่มรายการ
                    for (int j = 0; j < dw_data2.RowCount; j++)
                    {
                        Double start_salary = dw_data2.GetItemDouble(j + 1, "start_salary");
                        Double end_salary = dw_data2.GetItemDouble(j + 1, "end_salary");
                        Double sharemonth_amt = dw_data2.GetItemDouble(j + 1, "sharemonth_amt");
                        String sharetype_codeX = dw_data2.GetItemString(j + 1, "sharetype_code").Trim();
                        String entry_id = dw_data2.GetItemString(j + 1, "entry_id");
                        DateTime entry_date = dw_data2.GetItemDateTime(j + 1, "entry_date");
                        String yyyy = entry_date.Year.ToString();
                        String mm = entry_date.Month.ToString();
                        String dd = entry_date.Day.ToString();
                        Sta ta = new Sta(sqlca.ConnectionString);
                        try
                        {
                            String sql = @"insert into shsharetypemthrate (sharetype_code, start_salary, end_salary, sharemonth_amt,entry_id, entry_date) values ('" + sharetype_codeX + "'," + start_salary + " , " + end_salary + "," + sharemonth_amt + ", '" + entry_id + " ',  to_date(  '" + yyyy + "/" + mm + "/" + dd + "', 'yyyy/mm/dd' ))";
                            ta.Exe(sql);
                        }
                        catch (Exception ex)
                        {
                            //ถ้าจะอัปเดตโรวเดิม ให้ลบออกแล้วเพิ่มใหม่ เพราะมี pk 3 ตัว ซึ่งจะอัปเดตค่าใหม่นั้น ค่าเก่า ไม่ได้เก็บค่าไว้ ...
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลแถว " + Convert.ToInt32(j + 1) + "ได้ ");

                        }
                        ta.Close();

                    }

                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("มีข้อผิดพลาดในการบันทึกข้อมูล ลองใหม่อีกครั้ง");
            }
        }

        public void WebSheetLoadEnd()
        {

            if (dw_main.RowCount > 1)
            {
                dw_main.Reset();
                dw_main.InsertRow(0);
            }
            else if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
            }

            if (dw_data1.RowCount < 1)
            {
                dw_data1.InsertRow(0);
            }
        }

        #endregion

        /// <summary>
        /// ลบรายการเงินกู้
        /// </summary>
        private void DeleteShareType()
        {
            try
            {
                Sta ta = new Sta(sqlca.ConnectionString);
                try
                {
                    //SHSHARETYPEMTHRATE"."SHARETYPE_CODE" = "SHSHARETYPE"."SHARETYPE_CODE"
                    String sql = @"Delete FROM SHSHARETYPEMTHRATE  where SHARETYPE_CODE  = '" + sharetype_Code + "'";
                    //Sta dt = ta.Query(sql);
                    try
                    {
                        ta.Exe(sql);
                    }
                    catch
                    {
                        LtServerMessage.Text = "Can't Delete Record. Tab2";
                    }
                }
                catch (Exception ex)
                {
                    String err = ex.ToString();

                }
                ta.Close();
                dw_data1.DeleteRow(1);
                dw_data1.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
            }

            //Retrieve ...
            sharetype_Code = "";
            dw_main.Reset();
            dw_data1.Reset();
            dw_data2.Reset();
        }

        private void GetShateDetail()
        {

            String shcode = "";
            String shdesc = "";
            try
            {
                shcode = dw_main.GetItemString(1, "sharetype_code").Trim();
                //shdesc = dw_main.GetItemString(1, "sharetype_desc").Trim();
                dw_data1.Reset();
                dw_data1.Retrieve(shcode);
                dw_data2.Reset();
                dw_data2.Retrieve(shcode);
                tDw_data2.Eng2ThaiAllRow();
            }
            catch
            {

                LtServerMessage.Text = WebUtil.WarningMessage("ไม่มีรายการนี้จากการค้นหา"); ;
            }
        }

        private void NewShareCode()
        {
            try
            {
                sharetype_Code = HiddenField1.Value.Trim();
                sharetype_Code = dw_main.GetItemString(1, "sharetype_code");
                //dw_main.Reset();
                //dw_main.Retrieve(sharetype_Code);
                dw_data1.Reset();
                dw_data1.InsertRow(1);
                dw_data2.Reset();
                dw_data2.InsertRow(1);
                dw_data1.SetItemString(1, "sharetype_code", sharetype_Code);

            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("มีข้อผิดพลาดจากกการดึงข้อมูลใหม่");
            }
        }

        protected void dw_data2_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (true)
            {
                sqlca.Commit();
            }
        }
    }
}
