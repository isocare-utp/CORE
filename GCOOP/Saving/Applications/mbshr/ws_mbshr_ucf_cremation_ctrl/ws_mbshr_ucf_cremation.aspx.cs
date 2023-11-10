using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_ucf_cremation_ctrl
{
    public partial class ws_mbshr_ucf_cremation : PageWebSheet, WebSheet
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
                dsList.SetItem(0, dsList.DATA.COOP_IDColumn, state.SsCoopId);//set value to primary key
            }
            else if (eventArg == PostDelRow)
            {
                String ls_chktype = "";
                int row = dsList.GetRowFocus();
                String ls_type = dsList.DATA[row].CMTTYPE_CODE;
                
                //chk รายละเอียด
                string sql = @"select cmttype_code from mbcremationdet where cmttype_code={0} and coop_id={1}  ";
                sql = WebUtil.SQLFormat(sql, ls_type, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sql);
          
                if (dt.Next())
                {
                    ls_chktype = dt.GetString("cmttype_code");
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('รหัสประเภทนี้มีการใช้แล้วไม่สามารถลบได้');", true); return;
                }

                else
                {
                    dsList.DeleteRow(row);
                    try
                    {
                        ls_chktype = @"delete from MBUCFCREMATION where coop_id = {0} and cmttype_code={1} ";
                        ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopId, ls_type);
                        WebUtil.ExeSQL(ls_chktype);
                        dsList.Retrieve();
                        dsList.RetriveGroup();
                        LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                    }
                }
            }
        }
        
        public void SaveWebSheet()
        {
            string sqlStr, ls_cmtcode, ls_cmtdesc, ls_chkcode = "";
            int li_row;

            try
            {
                //for chk
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].CMTTYPE_CODE.ToString() == "") {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกรหัส');", true); return;
                    }
                    else if (dsList.DATA[li_row].CMTTYPE_DESC.ToString() == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกคำอธิบาย');", true); return;
                    }
                    else if (ls_chkcode.IndexOf(dsList.DATA[li_row].CMTTYPE_CODE.ToString()) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('รหัสประเภทซ้ำกัน กรุณาตรวจสอบ');", true); return;
                    }
                    ls_chkcode = ls_chkcode + ", " + dsList.DATA[li_row].CMTTYPE_CODE.ToString();                    
            }
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    ls_cmtcode = dsList.DATA[li_row].CMTTYPE_CODE.ToString();
                    ls_cmtdesc = dsList.DATA[li_row].CMTTYPE_DESC.ToString();

                    
                    //chk ประเภท
                    string sql = @"select cmttype_code from MBUCFCREMATION where cmttype_code={0} and coop_id={1}";
                    sql = WebUtil.SQLFormat(sql, ls_cmtcode, state.SsCoopId);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        sqlStr = @"update MBUCFCREMATION set 
                                cmttype_desc={0}
                                where cmttype_code={1} and coop_id={2}
                                ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_cmtdesc, ls_cmtcode, state.SsCoopId);
                        WebUtil.ExeSQL(sqlStr);
                    }else{
                        sqlStr = @"insert into MBUCFCREMATION 
                            (cmttype_code, cmttype_desc, coop_id)
                            values
                            ({0}, {1}, {2})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_cmtcode, ls_cmtdesc, state.SsCoopId);
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
            //for (int ii = 0; ii < dsList.RowCount; ii++)
            //{
            //    if (dsList.DATA[ii].STM_FLAG == 0)
            //    {
            //        dsList.FindDropDownList(ii, "process_flag").Enabled = false;
            //    }
            //}
        }
    }
}