using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_insurance_type_ctrl.w_dlg_sl_add_instype_ctrl
{
    public partial class w_dlg_sl_add_instype : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostSave { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            //dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {        
            if (!IsPostBack)
            {
             
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSave)
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                // decimal li_countgroup = 0;
                string INSTYPE_CODE = dsMain.DATA[0].INSTYPE_CODE;
                string INSTYPE_DESC = dsMain.DATA[0].INSTYPE_DESC;
                string INSCOMPANY_NAME = dsMain.DATA[0].INSCOMPANY_NAME;
               

                    try
                    {
                        string sql_insert = @"INSERT INTO insurencetype (INSTYPE_CODE,         INSTYPE_DESC,      INSCOMPANY_NAME ) 
                                                          VALUES ('" + INSTYPE_CODE + "','" + INSTYPE_DESC + "','" + INSCOMPANY_NAME + "')";
                        exed1.SQL.Add(sql_insert);
                        exed1.Execute();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        dsMain.ResetRow();
                        this.SetOnLoadedScript("alert('บันทึกข้อมูลสำเร็จ') \n parent.RefreshSheet(" + INSTYPE_CODE + ")");

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                
            }
        }

        public void WebDialogLoadEnd()
        {
           
        }
        
    }
}