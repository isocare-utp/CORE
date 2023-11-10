using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_assist.ws_sl_shareucftype_ctrl
{
    public partial class ws_sl_shareucftype : PageWebSheet, WebSheet
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
                dsList.Retrieve();//show data first
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertAtRow(0);
                dsList.SetItem(0, dsList.DATA.COOP_IDColumn, state.SsCoopControl);//set value to primary key
            }
            else if (eventArg == PostDelRow)
            {
                String ls_chktype = "";
                int row = dsList.GetRowFocus();
                String ls_type = dsList.DATA[row].SHARETYPE_CODE;
                
                //chk master
                string sql = @"select sharetype_code from shsharemaster where sharetype_code={0} and coop_id={1} and sharetype_code>0 and rownum=1";
                sql = WebUtil.SQLFormat(sql, ls_type, state.SsCoopControl);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_chktype = dt.GetString("sharetype_code");
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('ประเภทหุ้นนี้มีการใช้งานแล้วไม่สามารถลบได้');", true); return;
                }                
                else
                {
                    //dsList.DeleteRow(row);
                    try
                    {
                        ls_chktype = @"delete from shsharetype where coop_id = {0} and sharetype_code={1} ";
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
            string sqlStr, ls_shrcode, ls_shrdesc, ls_chkassiscode = "";
            int li_row;

            try
            {
                //for chk
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].SHARETYPE_CODE.ToString() == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกรหัสประเภทหุ้น');", true); return;
                    }
                    else if (dsList.DATA[li_row].SHARETYPE_DESC.ToString() == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกชื่อประเภทหุ้น');", true); return;
                    }
                    else if (ls_chkassiscode.IndexOf(dsList.DATA[li_row].SHARETYPE_CODE.ToString()) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('รหัสประเภทประเภทหุ้นซ้ำกัน กรุณาตรวจสอบ');", true); return;
                    }
                    ls_chkassiscode = ls_chkassiscode + ", " + dsList.DATA[li_row].SHARETYPE_CODE.ToString();                    
                }
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    ls_shrcode = dsList.DATA[li_row].SHARETYPE_CODE.ToString();
                    ls_shrdesc = dsList.DATA[li_row].SHARETYPE_DESC.ToString();
                    //chk ประเภทสวัสดิการ
                    string sql = @"select sharetype_code from shsharetype where sharetype_code={0} and coop_id={1}";
                    sql = WebUtil.SQLFormat(sql, ls_shrcode, state.SsCoopControl);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        sqlStr = @"update shsharetype set 
                                sharetype_desc={0} where sharetype_code={1} and coop_id={2}
                                ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_shrdesc, ls_shrcode, state.SsCoopId);
                        WebUtil.ExeSQL(sqlStr);
                    }else{//,stm_flag
                        sqlStr = @"insert into shsharetype 
                            (sharetype_code, sharetype_desc, coop_id)
                            values
                            ({0}, {1}, {2})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_shrcode, ls_shrdesc, state.SsCoopId);
                        WebUtil.ExeSQL(sqlStr);
                    }
                }
                dsList.Retrieve();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}