using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finance_usebalance_list : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;

        public void InitJsPostBack()
        {
            //tDwMain = new DwThDate(DwMain, this);
            //tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwDetail.InsertRow(0);

                DwUtil.RetrieveDataWindow(DwMain, "finquery.pbl", null, state.SsWorkDate, state.SsCoopId);
                DwUtil.RetrieveDataWindow(DwDetail, "finquery.pbl", null, state.SsWorkDate, state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
  
        }

        public void SaveWebSheet()
        {
        
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
    }
}