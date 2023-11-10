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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_mbucftambol : PageWebDialog, WebDialog
    {
        protected String postSave;
        protected String insertRow;
        protected String changeDistrict;
        protected String jsGetTambol;

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            insertRow = WebUtil.JsPostBack(this, "insertRow");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
            jsGetTambol = WebUtil.JsPostBack(this, "jsGetTambol");
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
                dw_tambol.SetTransaction(sqlca);
                dw_search.SetTransaction(sqlca);
                HdCloseDlg.Value = "false";


                if (!IsPostBack)
                {
                    dw_tambol.Retrieve();

                }
                else
                {
                    this.RestoreContextDw(dw_tambol);
                    //DwMain.RestoreContext();
                }
            }
            catch (Exception) { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSave")
            {
                JsPostSave();
            }
            else if (eventArg == "insertRow")
            {
                int row = dw_tambol.RowCount;
                dw_tambol.InsertRow(row + 1);
                InsertTambol();

            }
            else if (eventArg == "changeDistrict")
            {
                ChangeDistrict();

            }
            else if (eventArg == "jsGetTambol")
            {
                JsGetTambol();
            }
        }

        private void JsPostSave()
        {
            try
            {
                dw_tambol.UpdateData();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void InsertTambol()
        {
            String district_code = Hdistrict_code.Value;
            String province_code = Hprovince_code.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {


                String Sql = @" SELECT   MAX(MBUCFTAMBOL.TAMBOL_CODE)as TAMBOL_CODE 
                                FROM MBUCFDISTRICT,   
                                     MBUCFPROVINCE,   
                                     MBUCFTAMBOL  
                               WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE ) and  
                                     ( MBUCFTAMBOL.DISTRICT_CODE = MBUCFDISTRICT.DISTRICT_CODE )    and 
                                 MBUCFTAMBOL.DISTRICT_CODE ='" + district_code + "'  and MBUCFPROVINCE.PROVINCE_CODE ='" + province_code + "'";
                Sdt dt = ta.Query(Sql);
                if (dt.Next())
                {
                    String tambol_cut = WebUtil.Right(dt.GetString("TAMBOL_CODE"), 2);
                    Int32 TAMBOLCODE = Convert.ToInt32(tambol_cut)+1;
                    String tambol_code = district_code + TAMBOLCODE.ToString("00");
                    dw_tambol.SetItemString(dw_tambol.RowCount, "tambol_code", tambol_code);
                }

            }
            catch (Exception ex) { dw_tambol.SetItemString(dw_tambol.RowCount, "tambol_code", district_code + "01"); }
            ta.Close();
            dw_tambol.SetItemString(dw_tambol.RowCount, "province_code", province_code);
            dw_tambol.SetItemString(dw_tambol.RowCount, "district_code", district_code);
        }

        private void JsGetTambol()
        {
            String district_code = "", province_code = "";


            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_tambol.GetSqlSelect();
            try
            {
                district_code = Hdistrict_code.Value;

            }
            catch { district_code = ""; }
            try
            {
                province_code = Hprovince_code.Value;

            }
            catch { province_code = ""; }


            if (district_code.Length > 0)
            {
                ls_sqlext = "  AND (  MBUCFTAMBOL.DISTRICT_CODE  ='" + district_code + "') ";
            }
            if (province_code.Length > 0)
            {
                ls_sqlext += "  AND ( MBUCFPROVINCE.PROVINCE_CODE = '" + province_code + "') ";
            }


            ls_temp = ls_sql + ls_sqlext;
            dw_tambol.SetSqlSelect(ls_temp);
            dw_tambol.Retrieve();



        }

        public void WebDialogLoadEnd()
        {
            dw_search.InsertRow(1);
            dw_search.SetItemString(1, "province_code", Hprovince_code.Value);
            dw_search.SetItemString(1, "district_code", Hdistrict_code.Value);
            ChangeDistrict();
            dw_tambol.SaveDataCache();
        }


        private void ChangeDistrict()
        {
            try
            {


                String provincecode = Hprovince_code.Value;
                DataWindowChild childdis = dw_search.GetChild("district_code");
                childdis.SetTransaction(sqlca);
                childdis.Retrieve();
                childdis.SetFilter("province_code='" + provincecode + "'");
                childdis.Filter();



            }
            catch (Exception ex)
            {

                //LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }
        protected void PreUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (!state.IsWritable)
            //{
            //    e.Cancel = true;
            //    sqlca.Rollback();
            //}
        }

        protected void PostUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (true)
            {
                sqlca.Commit();
            }
        }



    }
}

