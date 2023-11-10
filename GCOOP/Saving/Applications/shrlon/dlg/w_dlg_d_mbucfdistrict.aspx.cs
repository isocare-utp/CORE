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
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_d_mbucfdistrict : PageWebDialog, WebDialog
    {
        protected String postSave;
        protected String insertRow;
        String province_code;
        protected String changeDistrict;

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            insertRow = WebUtil.JsPostBack(this, "insertRow");
            changeDistrict = WebUtil.JsPostBack(this, "changeDistrict");
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
                dw_district.SetTransaction(sqlca);
                //   dw_main.SetTransaction(sqlca);
                HSql.Value = dw_district.GetSqlSelect();
                HdCloseDlg.Value = "false";
                try
                {

                    province_code = Request["province"];
                    if (province_code == "")
                    {
                        dw_district.Retrieve();
                    }
                    else
                    {
                        String sql_province = " WHERE MBUCFDISTRICT.PROVINCE_CODE = '" + province_code + "'";
                        dw_district.SetSqlSelect(HSql.Value + sql_province);
                        dw_district.Retrieve();
                    }
                }
                catch { }

                if (!IsPostBack)
                {
                    // dw_main.InsertRow(1);
                    dw_district.Retrieve();

                }
                else
                {
                    this.RestoreContextDw(dw_district);

                }
            }
            catch (Exception) { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSave")
            {
                dw_districtUpdate();
                //    HdCloseDlg.Value = "true";
            }
            else if (eventArg == "insertRow")
            {
                int row = dw_district.RowCount;
                //dw_district.InsertRow(row + 1);
                //dw_district.SetItemString(row + 1, "province_code", province_code);
                dw_district.InsertRow(1);
                dw_district.SetItemString(1, "province_code", province_code);

            }
            else if (eventArg == "changeDistrict")
            {
                //  ChangeDistrict();
            }
        }

        public void WebDialogLoadEnd()
        {

            dw_district.SaveDataCache();
        }

        private void dw_districtUpdate()
        {
            try
            {
                String postcode = "";
                String district_desc = "";
                Sta ta = new Sta(sqlca.ConnectionString);
                int r = Convert.ToInt32(Hidrow.Value);
                String district_code;

                district_code = Hdistrict_code.Value;
                if (district_code == "")
                {
                    district_code = dw_district.GetItemString(r, "district_code");
                }


                district_desc = Hdistrict_desc.Value;
                if (district_desc == "")
                {
                    try
                    {
                        district_desc = dw_district.GetItemString(r, "district_desc");
                    }
                    catch
                    {
                        district_desc = Hdistrict_desc.Value;
                    }
                }



                postcode = Hpostcode.Value;
                if (postcode == "")
                {

                    try
                    {
                        postcode = dw_district.GetItemString(r, "postcode");

                    }
                    catch { postcode = "00000"; }
                }

                String DISTRICT_CODE = "";
                String sqlStr = @"  SELECT DISTRICT_CODE  
                                     FROM  MBUCFDISTRICT 
                                     WHERE MBUCFDISTRICT.DISTRICT_CODE = '" + district_code + @"' 
                                     AND MBUCFDISTRICT.PROVINCE_CODE =  '" + province_code + "' ";
                Sdt dt = ta.Query(sqlStr);
                if (dt.Next())
                {

                    DISTRICT_CODE = dt.GetString("DISTRICT_CODE");
                }
                if (DISTRICT_CODE == district_code)
                {

                    String sqlUp = @"    UPDATE MBUCFDISTRICT  
                                             SET DISTRICT_DESC = '" + district_desc + @"',   
                                             POSTCODE = '" + postcode + @"'
                                             WHERE ( MBUCFDISTRICT.DISTRICT_CODE ='" + district_code + @"'  ) AND  
                                            ( MBUCFDISTRICT.PROVINCE_CODE =  '" + province_code + @"' ) ";

                    ta.Exe(sqlUp);
                    ta.Close();

                }
                else
                {

                    String sqlIn = @"  INSERT INTO MBUCFDISTRICT  
                                 ( DISTRICT_CODE,   
                                   DISTRICT_DESC,   
                                   POSTCODE,   
                                   PROVINCE_CODE )  
                          VALUES ( '" + district_code + "',     '" + district_desc + "',   '" + postcode + "',    '" + province_code + "' ) ";
                    ta.Exe(sqlIn);
                    ta.Close();

                }



            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                sqlca.Rollback();
            }
            ChangeDistrict();

        }
        public void ChangeDistrict()
        {
            try
            {
                String provincecode = Hprovince_code.Value;
                String sql_province = " WHERE MBUCFDISTRICT.PROVINCE_CODE = '" + provincecode + "'";
                dw_district.SetSqlSelect(HSql.Value + sql_province);
                dw_district.Retrieve();

            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }
    }
}
