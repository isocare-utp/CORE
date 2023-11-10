using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmemgrpcontrol_ctrl.w_dlg_sl_searchmembgroupcontrol_ctrl
{
    public partial class w_dlg_sl_searchmembgroupcontrol : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostProvince { get; set; }
        [JsPostBack]
        public String PostAmphur { get; set; }
        [JsPostBack]
        public String PostSave { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {                
                string ls_membgroup_code = Request["ls_membgroup_code"].Trim();
                if (ls_membgroup_code == "" || ls_membgroup_code == null)
                {
                    dsMain.DATA[0].JS_POSTSAVE = 1;
                }
                else { 
                    dsMain.DATA[0].JS_POSTSAVE = 0;
                    dsMain.Retrievedata(ls_membgroup_code);                
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSave)
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                string ls_membgroup_code = dsMain.DATA[0].MEMBGROUP_CONTROL;
                string ls_membgroup_desc = dsMain.DATA[0].MEMBGROUP_CONTROLDESC;
                
                try
                {
                    if (dsMain.DATA[0].JS_POSTSAVE == 1)
                    {
                        string sql_insert = @"INSERT INTO mbucfmembgroupcontrol (COOP_ID,   MEMBGROUP_CONTROL,  MEMBGROUP_CONTROLDESC) 
                                        VALUES ('" + state.SsCoopControl + "','" + ls_membgroup_code + "','" + ls_membgroup_desc + "')";
                        exed1.SQL.Add(sql_insert);
                        exed1.Execute();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        dsMain.ResetRow();
                        this.SetOnLoadedScript("alert('บันทึกข้อมูลสำเร็จ') \n parent.RefreshSheet()");             
                    }
                    else {
                        string sql_update = "update mbucfmembgroupcontrol set MEMBGROUP_CONTROLDESC='" + ls_membgroup_desc +
                                        "' where COOP_ID='" + state.SsCoopControl +
                                        "' and MEMBGROUP_CONTROL='" + ls_membgroup_code + "'";
                        exed1.SQL.Add(sql_update);
                        exed1.Execute();
                        LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
                        this.SetOnLoadedScript("alert('แก้ไขข้อมูลสำเร็จ') \n parent.RefreshSheet()");                    
                    }
                }
                catch (Exception ex)
                {                  
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกข้อมูลไม่สำเร็จ "+ex);
                }                     
            }
        }
        public void WebDialogLoadEnd()
        {
            
        }
    }
}