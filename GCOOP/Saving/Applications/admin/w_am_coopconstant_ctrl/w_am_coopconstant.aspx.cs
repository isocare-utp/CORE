using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.admin.w_am_coopconstant_ctrl
{
    public partial class w_am_coopconstant : PageWebSheet, WebSheet
    {
        

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }


        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                //exe.AddRepeater(dsMain);
                exe.AddFormView(dsMain, ExecuteType.Update);
                exe.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        //public string province_dese { get; set; }


        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

    }
}