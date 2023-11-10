using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.fund.ws_fund_ucf_fundcollkeeptype_ctrl
{
    public partial class ws_fund_ucf_fundcollkeeptype : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertAtRow(0);
            }
            else if (eventArg == PostDelRow)
            {
                String ls_chktype = "";
                int row = dsList.GetRowFocus();
                String ls_type = dsList.DATA[row].FUNDKEEPTYPE;

                string sql_pay = @"SELECT FUNDKEEPTYPE FROM FUNDCOLLKEEPRATE WHERE COOP_ID={0} AND FUNDKEEPTYPE={1}";
                sql_pay = WebUtil.SQLFormat(sql_pay, ls_type, state.SsCoopControl);
                Sdt dt_pay = WebUtil.QuerySdt(sql_pay);               

                if (dt_pay.Next())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กองทุนมีการกำหนดเงื่อนไขการจ่ายแล้วไม่สามารถลบได้ กรุณาลบเงื่อนไขการจ่ายก่อน!');", true); return;
                }
                else
                {
                    try
                    {
                        ls_chktype = @"DELETE FROM FUNDCOLLKEEPTYPE WHERE COOP_ID = {0} AND FUNDKEEPTYPE={1} ";
                        ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopId, ls_type);
                        WebUtil.ExeSQL(ls_chktype);
                        dsList.Retrieve();
                        LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    }
                    catch {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                    }
                }
            }
        }
        
        public void SaveWebSheet()
        {
            string sqlStr, ls_fundcode, ls_funddesc, ls_chkcode = "";
            decimal ld_sort = 0;
            int li_row;
            try
            {
                string ls_coopcontrol = state.SsCoopControl;
                //for chk
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].FUNDKEEPTYPE == "") {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกรหัสกองทุน!');", true); return;
                    }
                    else if (dsList.DATA[li_row].FUNDKEEPDESC == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกคำอธิบายกองทุน!');", true); return;
                    }                   
                    else if (dsList.DATA[li_row].SORT.ToString() == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกลำดับ!');", true); return;
                    }
                    else if (ls_chkcode.IndexOf(dsList.DATA[li_row].FUNDKEEPTYPE.ToString()) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('รหัสประเภทกองทุนซ้ำกัน กรุณาตรวจสอบ');", true); return;
                    }
                    ls_chkcode = ls_chkcode + ", " + dsList.DATA[li_row].FUNDKEEPTYPE.ToString();                    
                }
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    ls_fundcode = dsList.DATA[li_row].FUNDKEEPTYPE;
                    ls_funddesc = dsList.DATA[li_row].FUNDKEEPDESC;
                    ld_sort = dsList.DATA[li_row].SORT;
                   
                    string sql = @"SELECT FUNDKEEPTYPE FROM FUNDCOLLKEEPTYPE where COOP_ID={0} and FUNDKEEPTYPE={1}";
                    sql = WebUtil.SQLFormat(sql,ls_coopcontrol, ls_fundcode);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        sqlStr = @"UPDATE FUNDCOLLKEEPTYPE SET 
                                FUNDKEEPDESC={2} , SORT={3} 
                                where COOP_ID={0} and FUNDKEEPTYPE={1}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopcontrol, ls_fundcode
                            , ls_funddesc, ld_sort);
                        WebUtil.ExeSQL(sqlStr);
                    }else{
                        sqlStr = @"INSERT INTO FUNDCOLLKEEPTYPE 
                            (COOP_ID, FUNDKEEPTYPE,FUNDKEEPDESC,SORT)
                            VALUES
                            ({0}, {1}, {2},{3})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopcontrol, ls_fundcode, ls_funddesc, ld_sort);
                        WebUtil.ExeSQL(sqlStr);
                    }
                }
                dsList.Retrieve();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ " + ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}