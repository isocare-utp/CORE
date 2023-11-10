using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_ucftambol_ctrl
{
    public partial class ws_mb_ucftambol : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostProvince { get; set; }
        [JsPostBack]
        public String PostDistrict { get; set; }
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
                wd_main.DdDistrict(wd_main.DATA[0].province_code);
            }
            else if (eventArg == PostDistrict)
            {
                wd_list.RetrieveList(wd_main.DATA[0].DISTRICT_CODE);
            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_row = wd_list.GetRowFocus();
                string tambol_code = wd_list.DATA[ls_row].TAMBOL_CODE;
                if (IsUsedTambol(tambol_code))
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
                int ls_row = wd_list.RowCount - 1;
                wd_list.DATA[ls_row].DISTRICT_CODE = wd_main.DATA[0].DISTRICT_CODE;
                wd_list.FindTextBox(ls_row, "TAMBOL_CODE").Focus();
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

        public bool IsUsedTambol(string tambol_code)
        {
            string sql = "select member_no from mbmembmaster where tambol_code={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, tambol_code);
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