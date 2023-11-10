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
using System.Data.OracleClient;  //เพิ่มเข้ามา
using Sybase.DataWindow; //เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
//using DataLibrary; //เพิ่มเข้ามา

namespace Saving.Applications.account
{
    public partial class w_acc_set_formula : PageWebSheet, WebSheet
    {
        public String postDwChoose;
        public String postNewClear;
        
        

        private void JspostNewClear()
        {
            Dw_choose.Reset();
            Dw_data.Reset();
            Dw_choose.InsertRow(0);
            Hd_sectionid.Value = "";
        }

        private void JspostDwChoose()
        {
            String Section_id = Hd_sectionid.Value;
            //mai เพิ่ม retrieve coopid
            Dw_data.Retrieve(Section_id,state.SsCoopId);
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postDwChoose = WebUtil.JsPostBack(this, "postDwChoose");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_choose.SetTransaction(sqlca);
            Dw_data.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_choose.InsertRow(0);
                DwUtil.RetrieveDDDW(Dw_choose, "section_id", "acc_set_formula.pbl", state.SsCoopId);
           
            }
            else 
            {
                this.RestoreContextDw(Dw_choose);
                this.RestoreContextDw(Dw_data);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDwChoose") {
                JspostDwChoose();
            }
            else if (eventArg == "postNewClear") {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_choose.RowCount > 1)
            {
                Dw_choose.DeleteRow(Dw_choose.RowCount);
            }
            Dw_data.SaveDataCache();
            Dw_choose.SaveDataCache();
        }

        #endregion
    }
}
