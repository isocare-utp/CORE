using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_assdetail_ctrl
{
    public partial class ws_as_assdetail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMBNo { get; set; }
        [JsPostBack]
        public string PostContNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsCont.InitDsCont(this);
            dsContSTM.InitDsContSTM(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMBNo)
            {
                string ls_memno = "", ls_asscontno = "";

                ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                dsMain.RetrieveMain(ls_memno);

                dsList.ResetRow();
                dsList.RetrieveList(ls_memno);

                if (dsList.RowCount > 0)
                {
                    ls_asscontno = dsList.DATA[0].asscontract_no;

                    this.of_retrievecontno(ls_asscontno);
                }
            }
            else if (eventArg == PostContNo)
            {
                Int32 li_row = Convert.ToInt32(hdRow.Value);

                string ls_asscontno = dsList.DATA[li_row].asscontract_no;

                this.of_retrievecontno(ls_asscontno);
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        private void of_retrievecontno(string as_asscontno)
        {
            dsCont.RetrieveData(as_asscontno);
            dsContSTM.RetrieveData(as_asscontno);
        }
    }
}