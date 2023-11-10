using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Globalization;
using DataLibrary;
using System.Data;
using System.Web.Services.Protocols;
//using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_reqdetail : PageWebDialog, WebDialog
    {
        protected String MemberNoSearch;
        protected String jsGetPostcode;
        protected String changeDistrict;

       
       
        #region WebDialog Members

        public void InitJsPostBack()
        {
            MemberNoSearch = WebUtil.JsPostBack(this, "MemberNoSearch");
            jsGetPostcode = WebUtil.JsPostBack(this, "jsGetPostcode");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();

            DwList.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                int seq_no = Convert.ToInt32(Request["seq_no"]);
                DwList.InsertRow(0);
                DwUtil.RetrieveDDDW(DwList, "prename_code", "dp_reqdeposit.pbl", null);
                DwList.SetItemDecimal(1, "seq_no", seq_no);
            }
            else
            {
                HdPost.Value = "false";
                this.RestoreContextDw(DwList);

            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "MemberNoSearch")
            {
                JsMemberNoSearch();
            }
            else if (eventArg == "changeDistrict")
            {
                ChangeDistrict();
            }
            else if (eventArg == "jsGetPostcode")
            {
                JsGetPostcode();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwList.SaveDataCache();
        }

        #endregion


        private void ChangeDistrict()
        {
            try
            {
                DataWindowChild childdis = DwList.GetChild("district");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                String provincecode = DwList.GetItemString(1, "province");
                childdis.SetFilter("province_code ='" + provincecode + "'");
                childdis.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsGetPostcode()
        {
            try
            {
                DataWindowChild child = DwList.GetChild("tumbol");
                child.SetTransaction(sqlca);
                child.Retrieve();
                String district_code = DwList.GetItemString(1, "district");
                child.SetFilter("district_code='" + district_code + "'");
                child.Filter();

                String provincecode = DwList.GetItemString(1, "province");
                // String district_code = dw_main.GetItemString(1, "district_code");
                String sql = @"SELECT MBUCFDISTRICT.POSTCODE   FROM MBUCFDISTRICT,  MBUCFPROVINCE  
                                WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
                                and ( MBUCFDISTRICT.PROVINCE_CODE ='" + provincecode + "' ) and   ( MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + "') ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwList.SetItemString(1, "post_code", dt.Rows[0]["postcode"].ToString());
                }
            }
            catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
        }

        private void JsMemberNoSearch()
        {

        }

    }
}
