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
    public partial class w_dlg_sl_cfloan_inttable_newtype : PageWebDialog,WebDialog
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
            DwMain.SetTransaction(sqlca);
            
            if (!IsPostBack)
            {
                DwMain.InsertRow(1);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveData")
            {
                try
                {
                    DwMain.UpdateData();
                    //Response.Write("<script> alert('บันทึกข้อมูลเรียบร้อยแล้ว'); </script>");

                }
                catch
                {
                    //Response.Write("<script> alert('ไม่สามารถบันทึกข้อมูลได้'); </script>");
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                String loanintrate_code = "";
                String loanintrate_desc = "";
                loanintrate_code = DwMain.GetItemString(1, "loanintrate_code");
                loanintrate_desc = DwMain.GetItemString(1, "loanintrate_desc");
                DwMain.UpdateData();
                Response.Write("<script> alert('บันทึกข้อมูลสำเร็จ'); </script>");
                //Response.Write("<script> alert('" + sharetype_code + "," + sharetype_desc + "'); </script>");
                Response.Write("<script> window.opener.NewRateCode('" + loanintrate_code + "','" + loanintrate_desc + "'); </script>");
                Response.Write("<script> window.close();</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('ไม่สามารถบันทึกข้อมูลได้'); </script>");
            }
        }

        protected void OnClose(object sender, EventArgs e)
        {
            Response.Write("<script> window.close(); </script>");
        }
    }
}
