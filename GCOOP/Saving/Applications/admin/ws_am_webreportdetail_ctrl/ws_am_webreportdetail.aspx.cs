using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.admin.ws_am_webreportdetail_ctrl
{
    public partial class ws_am_webreportdetail : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string JsPostApp { get; set; }
        [JsPostBack]
        public string JsPostGroup { get; set; }
        [JsPostBack]
        public string JsPostInsert { get; set; }
        [JsPostBack]
        public string JsPostDelete { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DDapplication();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostApp)
            {
                string app = dsMain.DATA[0].APPLICATION;
                dsMain.DDgroup(app);
            }
            else if (eventArg == JsPostGroup)
            {
                string group = dsMain.DATA[0].GROUP_ID;
                dsList.retrieve(group);

            }
            else if (eventArg == JsPostInsert)
            {
                string group = dsMain.DATA[0].GROUP_ID;
                dsList.InsertLastRow();
                int row = dsList.RowCount - 1;
                dsList.DATA[row].GROUP_ID = group;
            }
            else if (eventArg == JsPostDelete)
            {
                int rowDel = dsList.GetRowFocus();
                dsList.DeleteRow(rowDel);
            }
        }

        public void SaveWebSheet()
        {
            //for (int i = 0; i < dsList.RowCount; i++) {
            //    string sql = "update";
            //    string report_id = dsList.DATA[i].REPORT_ID;
            //    Sdt dt = WebUtil.QuerySdt(sql);                
            //}
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                exed1.AddRepeater(dsList);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }catch(Exception ex){
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
            }

        }

        public void WebSheetLoadEnd()
        {
        }
    }
}