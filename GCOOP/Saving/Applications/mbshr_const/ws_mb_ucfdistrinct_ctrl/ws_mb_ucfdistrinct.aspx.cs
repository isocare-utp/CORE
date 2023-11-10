using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_ucfdistrinct_ctrl
{
    public partial class ws_mb_ucfdistrinct : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostProvince { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        [JsPostBack]
        public String PostInsertRow { get; set; }
        public void InitJsPostBack()
        {
            wd_main.InitMain(this);
            wd_list.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                wd_main.DdProvince();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostProvince)
            {
                wd_list.RetrieveList(wd_main.DATA[0].PROVINCE_CODE);
            }
            else if (eventArg == PostDeleteRow)
            {
                string amphur_code = "";
                int ls_row = wd_list.GetRowFocus();
                amphur_code = wd_list.DATA[ls_row].DISTRICT_CODE;
                if (IsUsedDistrict(amphur_code))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    wd_list.DeleteRow(ls_row);
                }
            }
            else if (eventArg == PostInsertRow)
            {
                wd_list.InsertLastRow();
                int ls_currow = wd_list.RowCount - 1;
                wd_list.DATA[ls_currow].PROVINCE_CODE = wd_main.DATA[0].PROVINCE_CODE;
                wd_list.FindTextBox(ls_currow, "DISTRICT_CODE").Focus();
            }

        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                exed1.AddRepeater(wd_list);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                wd_list.ResetRow();
                wd_main.ResetRow();
                wd_main.DdProvince();
            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        public bool IsUsedDistrict(string amphur_code)
        {
            string sql = "select member_no from mbmembmaster where amphur_code={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, amphur_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return chk;
        }
    }
}