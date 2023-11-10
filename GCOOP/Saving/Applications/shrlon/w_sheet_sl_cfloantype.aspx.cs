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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_cfloantype : PageWebSheet, WebSheet
    {
        private String loan_code;
        protected String jssetmbtype;
        private String tab;
        private String as_coopid;
        protected String getLoanConfig;
        protected String itemChangedReload;
        protected String getDelete;
        protected String linkForeDd;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            getLoanConfig = WebUtil.JsPostBack(this, "getLoanConfig");
            itemChangedReload = WebUtil.JsPostBack(this, "itemChangedReload");
            getDelete = WebUtil.JsPostBack(this, "getDelete");
            jssetmbtype = WebUtil.JsPostBack(this, "jssetmbtype");
        }

        public void WebSheetLoadBegin()
        {
            //http://sav.doysdev.gcoop/CEN/GCOOP/Saving/Applications/shrlon/w_sheet_sl_cfloantype.aspx?setApp=shrlon&setGroup=ZCF&setWinId=SL-ZCF0010
            linkForeDd = Request.Url.AbsoluteUri;
            int indexOflinkForExdd = linkForeDd.IndexOf("&exdd=");
            if (indexOflinkForExdd > 0)
            {
                linkForeDd = linkForeDd.Substring(0, indexOflinkForExdd);
            }
            String membtype_code = "";
            try
            {
                loan_code = HiddenField1.Value;
                loan_code.Trim();
                as_coopid = state.SsCoopControl;
                this.ConnectSQLCA();
                dw_mbtype.SetTransaction(sqlca);
                dw_data_list.SetTransaction(sqlca);
                dw_data_1.SetTransaction(sqlca);
                dw_data_21.SetTransaction(sqlca);
                dw_data_22.SetTransaction(sqlca);
                dw_data_23.SetTransaction(sqlca);
                dw_data_31.SetTransaction(sqlca);
                dw_data_32.SetTransaction(sqlca);
                dw_data_41.SetTransaction(sqlca);
                dw_data_42.SetTransaction(sqlca);
                dw_data_43.SetTransaction(sqlca);
                dw_data_51.SetTransaction(sqlca);
                dw_data_52.SetTransaction(sqlca);
                dw_data_53.SetTransaction(sqlca);
                dw_data_61.SetTransaction(sqlca);
                dw_data_62.SetTransaction(sqlca);
                dw_data_71.SetTransaction(sqlca);
                dw_data_72.SetTransaction(sqlca);
                dw_mbtype.Reset();
                dw_mbtype.InsertRow(1);
                DwUtil.RetrieveDDDW(dw_mbtype, "membtype_code", "sl_cfloantype.pbl", null);
                membtype_code = dw_mbtype.GetItemString(1, "membtype_code").Trim();
                string mbloan_group1 = @"    SELECT MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                                                     MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                                     MBUCFMEMBTYPE.LOANMBGROUP_CODE  
                                                FROM MBUCFMEMBTYPE  WHERE MBUCFMEMBTYPE.MEMBTYPE_CODE=  '" + membtype_code + @"'   ";

                Sdt dtmbloan_group1 = WebUtil.QuerySdt(mbloan_group1);
                if (dtmbloan_group1.Next())
                {
                    membtype_code = dtmbloan_group1.GetString("LOANMBGROUP_CODE");
                }

                //Doys เพิ่มส่วนให้ Retrieve ตาม Dropdown
                if (!IsPostBack)
                {
                    //SQL สำหรับ DropdownList
                    String sqlfordd = @"
                    SELECT 
                        MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                        MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                        MBUCFMEMBTYPE.LOANMBGROUP_CODE  
                    FROM MBUCFMEMBTYPE  WHERE MBUCFMEMBTYPE.coop_id = '" + state.SsCoopControl + "' order by MBUCFMEMBTYPE.MEMBTYPE_CODE";

                    //ทำ Query sql ให้ออกเป็น DataTable
                    DataTable dtdd = WebUtil.Query(sqlfordd);

                    //แมพฟิวส์ให้ DropdownList
                    DdMembType.DataValueField = "MEMBTYPE_CODE";
                    DdMembType.DataTextField = "MEMBTYPE_DESC";

                    //นำ DataTable ที่ได้มาใช้กับ DropdownList
                    DdMembType.DataSource = dtdd;
                    DdMembType.DataBind();

                    //เช็คค่าจาก url เพื่อดึงค่าจากการเลือก DropdownList
                    string exdd = "";
                    try
                    {
                        exdd = Request["exdd"].Trim();
                    }
                    catch { exdd = ""; }

                    //ดึงค่าจาก DropDownList มาใส่ตัวแปร membType
                    if (exdd != "")
                    {
                        for (int i = 0; i < DdMembType.Items.Count; i++)
                        {
                            if (DdMembType.Items[i].Value == exdd)
                            {
                                DdMembType.SelectedIndex = i;
                            }
                        }
                    }
                    String membType = DdMembType.SelectedValue;
                    if (membType != "002001")
                    {
                        membType = "01";
                    }
                    //ทำการ Retrieve
                    dw_data_list.Reset();
                    dw_data_list.Retrieve(as_coopid, membType);
                }
                //Doys - end


                if (loan_code != null)
                {
                    FilterLoan(as_coopid, loan_code);
                }
                if (IsPostBack)
                {
                    try
                    {
                        dw_mbtype.RestoreContext();
                        dw_data_list.RestoreContext();
                        dw_data_1.RestoreContext();
                        dw_data_21.RestoreContext();
                        dw_data_22.RestoreContext();
                        dw_data_23.RestoreContext();
                        dw_data_31.RestoreContext();
                        dw_data_32.RestoreContext();
                        dw_data_41.RestoreContext();
                        dw_data_42.RestoreContext();
                        dw_data_43.RestoreContext();
                        dw_data_51.RestoreContext();
                        dw_data_52.RestoreContext();
                        dw_data_53.RestoreContext();
                        dw_data_61.RestoreContext();
                        dw_data_62.RestoreContext();
                        dw_data_71.RestoreContext();
                        dw_data_72.RestoreContext();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
                }
                else
                {
                    //membtype_code
                    membtype_code = dw_mbtype.GetItemString(1, "membtype_code").Trim();
                    string mbloan_group = @"    SELECT MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                                                                         MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                                                         MBUCFMEMBTYPE.LOANMBGROUP_CODE  
                                                                    FROM MBUCFMEMBTYPE  WHERE MBUCFMEMBTYPE.MEMBTYPE_CODE=  '" + membtype_code + @"'   ";
                    Sdt dtmbloan_group = WebUtil.QuerySdt(mbloan_group);
                    if (dtmbloan_group.Next())
                    {
                        membtype_code = dtmbloan_group.GetString("LOANMBGROUP_CODE");
                    }
                    //dw_data_list.Reset();
                    //dw_data_list.Retrieve(as_coopid, membtype_code);
                    String code_tmp = dw_data_list.GetItemString(1, "loantype_code").Trim();
                    HiddenField1.Value = code_tmp;
                    GetLoanConfig();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void InsertRowBlank()
        {
            if (dw_data_1.RowCount < 1)
            {
                dw_data_1.InsertRow(1);
            }
            if (dw_data_21.RowCount < 1)
            {
                dw_data_21.InsertRow(1);
            }
            if (dw_data_31.RowCount < 1)
            {
                dw_data_31.InsertRow(1);
            }
            if (dw_data_41.RowCount < 1)
            {
                dw_data_41.InsertRow(1);
            }
            if (dw_data_43.RowCount < 1)
            {
                dw_data_43.InsertRow(1);
            }
            if (dw_data_51.RowCount < 1)
            {
                dw_data_51.InsertRow(1);
            }
            if (dw_data_52.RowCount < 1)
            {
                dw_data_52.InsertRow(1);
            }
            if (dw_data_61.RowCount < 1)
            {
                dw_data_61.InsertRow(1);
            }
            if (dw_data_71.RowCount < 1)
            {
                dw_data_71.InsertRow(1);
            }
            if (dw_data_72.RowCount < 1)
            {
                dw_data_72.InsertRow(1);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "getLoanConfig")
            {
                GetLoanConfig();
            }
            else if (eventArg == "itemChangedReload")
            {

            }
            else if (eventArg == "getDelete")
            {
                GetDelete();
            }
            else if (eventArg == "jssetmbtype") { JsSetmbtype(); }
        }

        private void JsSetmbtype()
        {
            //dw_data_list.Reset();
            //dw_data_list.Retrieve(as_coopid);
            String membtype_code = dw_mbtype.GetItemString(1, "membtype_code").Trim();
            string mbloan_group = @"    SELECT MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                                                     MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                                     MBUCFMEMBTYPE.LOANMBGROUP_CODE  
                                                FROM MBUCFMEMBTYPE  WHERE MBUCFMEMBTYPE.MEMBTYPE_CODE=  '" + membtype_code + @"'   ";
            Sdt dtmbloan_group = WebUtil.QuerySdt(mbloan_group);
            if (dtmbloan_group.Next())
            {

                membtype_code = dtmbloan_group.GetString("LOANMBGROUP_CODE");

            }
            dw_data_list.Reset();
            dw_data_list.Retrieve(as_coopid, membtype_code);
        }

        private void GetDelete()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = @"Delete FROM LNLOANTYPE  where LOANTYPE_CODE  = '" + HiddenField1.Value + "' and coop_id ='" + state.SsCoopId + "' ";
                //Sta dt = ta.Query(sql);
                try
                {
                    ta.Exe(sql);
                }
                catch
                {
                    LtServerMessage.Text = "Can't Delete Record.";
                }
            }
            catch (Exception ex)
            {
                String err = ex.ToString();

            }
            ta.Close();

            //Retrieve ...
            loan_code = "";
            GetLoanConfig();
        }


        public void SaveWebSheet()
        {
            try
            {

                as_coopid = state.SsCoopControl;
                // 43, 52, 71 ถ้าไม่สามารถบันทึกข้อมูลได้เพราะว่าไม่มีการเซตค่าให้  บันทึกค่าว่างไม่ได้  ไม่มีผลอะไร
                try
                {
                    dw_data_1.UpdateData();
                    dw_data_21.UpdateData();
                    dw_data_31.UpdateData();
                    dw_data_41.UpdateData();

                    dw_data_51.UpdateData();
                    dw_data_61.UpdateData();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");
                }
                catch { }
                if (dw_data_22.RowCount > 0)
                {
                    int r = dw_data_22.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_22.SetItemDecimal(i, "seq_no", i);
                        dw_data_22.SetItemString(i, "loantype_code", loan_code);
                        dw_data_22.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_22.UpdateData();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 2");
                    }

                }
                if (dw_data_23.RowCount > 0)
                {
                    int r = dw_data_23.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_23.SetItemDecimal(i, "seq_no", i);
                        dw_data_23.SetItemString(i, "loantype_code", loan_code);
                        dw_data_23.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_23.UpdateData();
                    }
                    catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 2"); }

                }
                if (dw_data_32.RowCount > 0)
                {
                    int r = dw_data_32.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_32.SetItemDecimal(i, "seq_no", i);
                        dw_data_32.SetItemString(i, "loantype_code", loan_code);
                        dw_data_32.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_32.UpdateData();
                    }
                    catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 3"); }


                }
                if (dw_data_42.RowCount > 0)
                {
                    int r = dw_data_42.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_42.SetItemDecimal(i, "seq_no", i);
                        dw_data_42.SetItemString(i, "loantype_code", loan_code);
                        dw_data_42.SetItemString(i, "coop_id", as_coopid);


                    }
                    try
                    {
                        dw_data_42.UpdateData();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex + "ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 4"); }


                }
                if (dw_data_43.RowCount > 0)
                {
                    int r = dw_data_43.RowCount;
                    for (int i = 1; i <= r; i++)
                    {
                        String collmasttype_code;
                        String loancolltype_code;
                        try { loancolltype_code = dw_data_43.GetItemString(i, "loancolltype_code"); }
                        catch { loancolltype_code = ""; }
                        try { collmasttype_code = dw_data_43.GetItemString(i, "collmasttype_code"); }
                        catch { collmasttype_code = "00"; }
                        dw_data_43.SetItemString(i, "loantype_code", loan_code);
                        dw_data_43.SetItemString(i, "coop_id", as_coopid);
                        dw_data_43.SetItemString(i, "collmasttype_code", collmasttype_code);
                        dw_data_43.SetItemString(i, "loancolltype_code", loancolltype_code);

                    }
                    try
                    {
                        dw_data_43.UpdateData();
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex + "ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 4");
                    }


                }
                if (dw_data_52.RowCount > 0)
                {
                    int r = dw_data_52.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_52.SetItemString(i, "loantype_code", loan_code);
                        dw_data_52.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_52.UpdateData();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 5");
                    }
                }
                if (dw_data_53.RowCount > 0)
                {
                    int r = dw_data_53.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_53.SetItemDecimal(i, "seq_no", i);
                        dw_data_53.SetItemString(i, "loantype_code", loan_code);
                        dw_data_53.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_53.UpdateData();
                    }
                    catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 5"); }


                }
                if (dw_data_62.RowCount > 0)
                {
                    int r = dw_data_62.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_62.SetItemDecimal(i, "seq_no", i);
                        dw_data_62.SetItemString(i, "loantype_code", loan_code);
                        dw_data_62.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_62.UpdateData();
                    }
                    catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 6"); }


                }
                if (dw_data_71.RowCount > 0)
                {
                    int r = dw_data_71.RowCount;
                    for (int i = 1; i <= r; i++)
                    {
                        dw_data_71.SetItemString(i, "loantype_code", loan_code);
                        dw_data_71.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_71.UpdateData();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 7");
                    }


                }
                if (dw_data_72.RowCount > 0)
                {
                    int r = dw_data_72.RowCount;
                    for (int i = 1; i <= r; i++)
                    {

                        dw_data_72.SetItemString(i, "loantype_code", loan_code);
                        dw_data_72.SetItemString(i, "coop_id", as_coopid);

                    }
                    try
                    {
                        dw_data_72.UpdateData();
                    }
                    catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ลองตรวจสอบที่แท๊บ 7"); }


                }


                //  
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e.Message);
            }

        }

        public void WebSheetLoadEnd()
        {
            if (dw_mbtype.RowCount > 1)
            {
                DwUtil.DeleteLastRow(dw_mbtype);
            }
        }

        #endregion

        /// <summary>
        /// เลือกรายการแล้ว retrieve ข้อมูลทุก tab
        /// </summary>
        private void GetLoanConfig()
        {
            try
            {
                loan_code = HiddenField1.Value;
                //dw_data_list.Reset();
                dw_data_1.Reset();
                dw_data_21.Reset();
                dw_data_22.Reset();
                dw_data_23.Reset();
                dw_data_31.Reset();
                dw_data_32.Reset();
                dw_data_41.Reset();
                dw_data_42.Reset();
                dw_data_43.Reset();
                dw_data_51.Reset();
                dw_data_52.Reset();
                dw_data_53.Reset();
                dw_data_61.Reset();
                dw_data_62.Reset();
                dw_data_71.Reset();
                dw_data_72.Reset();
                //retreive ต้อง filter ทุกตัวด้วย ... เอง
                String membtype_code = dw_mbtype.GetItemString(1, "membtype_code").Trim();
                as_coopid = state.SsCoopControl;
                string mbloan_group = @"    SELECT MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                                                     MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                                     MBUCFMEMBTYPE.LOANMBGROUP_CODE  
                                                FROM MBUCFMEMBTYPE  WHERE MBUCFMEMBTYPE.MEMBTYPE_CODE=  '" + membtype_code + @"'   ";
                Sdt dtmbloan_group = WebUtil.QuerySdt(mbloan_group);
                if (dtmbloan_group.Next())
                {

                    membtype_code = dtmbloan_group.GetString("LOANMBGROUP_CODE");

                }
                //dw_data_list.Retrieve(as_coopid, membtype_code);
                dw_data_1.Retrieve(as_coopid);
                dw_data_21.Retrieve(as_coopid);
                dw_data_22.Retrieve(as_coopid);
                dw_data_23.Retrieve(as_coopid);
                dw_data_31.Retrieve(as_coopid);
                dw_data_32.Retrieve(as_coopid);
                dw_data_41.Retrieve(as_coopid);
                dw_data_42.Retrieve(as_coopid);
                dw_data_43.Retrieve(as_coopid);
                dw_data_51.Retrieve(as_coopid);
                dw_data_52.Retrieve(as_coopid);
                dw_data_53.Retrieve(as_coopid);
                dw_data_61.Retrieve(as_coopid);
                dw_data_62.Retrieve(as_coopid);
                dw_data_71.Retrieve(as_coopid);
                dw_data_72.Retrieve(as_coopid);

                FilterLoan(as_coopid, loan_code);

            }
            catch
            {
                loan_code = "";
                dw_data_1.Reset();
                dw_data_21.Reset();
                dw_data_22.Reset();
                dw_data_23.Reset();
                dw_data_31.Reset();
                dw_data_32.Reset();
                dw_data_41.Reset();
                dw_data_42.Reset();
                dw_data_43.Reset();
                dw_data_51.Reset();
                dw_data_52.Reset();
                dw_data_53.Reset();
                dw_data_61.Reset();
                dw_data_62.Reset();
                dw_data_71.Reset();
                dw_data_72.Reset();
            }

        }

        private void FilterLoan(String as_coopid, String loan_code)
        {
            dw_data_1.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_1.Filter();
            dw_data_21.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_21.Filter();
            dw_data_22.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_22.Filter();
            dw_data_23.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_23.Filter();
            dw_data_31.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_31.Filter();
            dw_data_32.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_32.Filter();
            dw_data_41.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_41.Filter();
            dw_data_42.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_42.Filter();
            dw_data_43.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_43.Filter();
            dw_data_51.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_51.Filter();
            dw_data_52.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_52.Filter();
            dw_data_53.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_53.Filter();
            dw_data_61.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_61.Filter();
            dw_data_62.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_62.Filter();
            dw_data_71.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_71.Filter();
            dw_data_72.SetFilter(" coop_id ='" + as_coopid + "'  and LOANTYPE_CODE = '" + loan_code + "'");
            dw_data_72.Filter();

        }
    }
}
