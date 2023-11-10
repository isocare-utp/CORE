using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.admin.ws_am_coopmaster_ctrl
{
    public partial class ws_am_coopmaster : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostProvince { get; set; }
        [JsPostBack]
        public string PostAmpur { get; set; }
        [JsPostBack]
        public string PostCurrprovince { get; set; }
        [JsPostBack]
        public string PostCurramphur { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostBank { get; set; }
        [JsPostBack]
        public string PostBranch { get; set; }
        [JsPostBack]
        public string PostLinkAddress { get; set; }
        [JsPostBack]
        public string PostBankBranch { get; set; }
        [JsPostBack]
        public string PostExpenseBank { get; set; }
        [JsPostBack]
        public string PostExpenseBranch { get; set; }
        [JsPostBack]
        public string PostRefresh { get; set; }
        [JsPostBack]
        public string PostPosition { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.Retrieve();
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                string district_code = dsMain.DATA[0].DISTRICT_CODE;
                dsMain.DdProvince();
                dsMain.DdDistrict(province_code);
                
                //dsMain.DDRdSatangTabCode();
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMember)
            {
                try
                {
                    dsMain.Retrieve();
                    string province_code = dsMain.DATA[0].PROVINCE_CODE;
                    string district_code = dsMain.DATA[0].DISTRICT_CODE;
                    dsMain.DATA[0].PROVINCE_CODE = province_code;
                    dsMain.DdProvince();
                    dsMain.DdDistrict(province_code);
                    
                    
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            else if (eventArg == PostProvince)
            {
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                dsMain.DATA[0].DISTRICT_CODE = "";
                dsMain.DdDistrict(province_code);
            }
            else if (eventArg == PostAmpur)
            {
                string district_code = dsMain.DATA[0].DISTRICT_CODE;
                //dsMain.DATA[0].TAMBOL_CODE = "";
                //dsMain.DdTambol(district_code);
                string province_code = dsMain.DATA[0].PROVINCE_CODE;
                string sql = @" 
                               SELECT DISTRICT_CODE,   
                                 PROVINCE_CODE,   
                                 DISTRICT_DESC,   
                                 POSTCODE  
                            FROM MBUCFDISTRICT 
                          where ((PROVINCE_CODE={0}) and (DISTRICT_CODE={1})) ";
                sql = WebUtil.SQLFormat(sql, province_code, district_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].POSTCODE = dt.GetString("POSTCODE");
                }
            }
           
          
            
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
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
    }
}