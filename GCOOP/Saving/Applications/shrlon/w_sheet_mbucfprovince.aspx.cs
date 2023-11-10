using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_mbucfprovince : PageWebSheet, WebSheet
    {
        //private DwTrans SQLCA;
        //private WebState state;
        public void InitJsPostBack()
        {
          

        }

        public void WebSheetLoadBegin()
        {
            
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                // dw_main.InsertRow(1);
                dw_main.Retrieve();

            }
            else
            {
                this.RestoreContextDw(dw_main);

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
            dw_main.SaveDataCache();
            // try
            //{
            //    s.Disconnect();
            //}
            //catch { }
        }
       
    }
}

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
       