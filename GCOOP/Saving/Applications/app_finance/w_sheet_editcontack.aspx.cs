using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_editcontack : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            d_edit_contackmaster.SetTransaction(SQLCA);
           
           if (!IsPostBack) {
               d_edit_contackmaster.InsertRow(0);
           }
            try
            {
                if (Request["contack_no"] != null && Request["contack_no"].Trim() != "")
                {
                    d_edit_contackmaster.Retrieve(Request["contack_no"].Trim());

                }
            }
            catch { 
            
            }
        }

        protected void Page_LoadComplete(){
            SQLCA.Disconnect();
        }

      
    }
}
