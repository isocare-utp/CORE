using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_sharetype_new : PageWebDialog,WebDialog
    {
        protected String saveData;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            saveData = WebUtil.JsPostBack(this, "saveData");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_data.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                dw_data.InsertRow(1);
            }
            else
            {
                this.RestoreContextDw(dw_data);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveData") {
                try
                {
                    dw_data.UpdateData();
                    //Response.Write("<script> alert('บันทึกข้อมูลเรียบร้อยแล้ว'); </script>");
                
                }
                catch {
                    //Response.Write("<script> alert('ไม่สามารถบันทึกข้อมูลได้'); </script>");
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_data.SaveDataCache();
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                String sharetype_code = "";
                String sharetype_desc = "";
                sharetype_code = dw_data.GetItemString(1, "sharetype_code");
                sharetype_desc = dw_data.GetItemString(1, "sharetype_desc");
                dw_data.UpdateData();
                Response.Write("<script> alert('บันทึกข้อมูลสำเร็จ'); </script>");
                //Response.Write("<script> alert('" + sharetype_code + "," + sharetype_desc + "'); </script>");
                Response.Write("<script> window.opener.NewShareType('" + sharetype_code + "','" + sharetype_desc + "'); </script>");
                Response.Write("<script> window.close();</script>");

            }
            catch (Exception ex){
                Response.Write("<script> alert('ไม่สามารถบันทึกข้อมูลได้'); </script>");
            }
        }

        protected void OnClose(object sender, EventArgs e)
        {
            Response.Write("<script> window.close(); </script>");
        }
    }
}
