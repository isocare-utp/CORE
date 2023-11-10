using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_mg_template_autzd_ctrl
{
    public partial class ws_sl_mg_template_autzd : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostTemplateNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostTemplateNo")
            {
                decimal ldc_templateno = dsMain.DATA[0].TEMPLATE_NO;
                dsMain.Retrieve(ldc_templateno);
            }
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
                decimal ldc_tempno = dsMain.DATA[0].TEMPLATE_NO;
                if (ldc_tempno == 0)
                {
                    string ls_sql = "select (case when (max(template_no)) is null then  0 else (max(template_no)) end ) as template_no from lnmrtgtemplateautzd where coop_id = '" + state.SsCoopControl + "'";
                    Sdt dt = WebUtil.QuerySdt(ls_sql);
                    if (dt.Next())
                    {
                        dsMain.DATA[0].TEMPLATE_NO = dt.GetInt32("template_no") + 1;
                    }
                    dsMain.DATA[0].COOP_ID = state.SsCoopControl;

                    exe.AddFormView(dsMain, ExecuteType.Insert);
                    int result = exe.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    dsMain.ResetRow();
                }
                else
                {
                    exe.AddFormView(dsMain, ExecuteType.Update);
                    int result = exe.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
                    dsMain.ResetRow();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}