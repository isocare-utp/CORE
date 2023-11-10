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
using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_reqdetail_lap : PageWebDialog, WebDialog
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
            Decimal ref_type = DwList.GetItemDecimal(1, "ref_type");
            String ref_id = DwList.GetItemString(1, "ref_id");
            if (ref_type == 1) 
            { 
                ref_id = WebUtil.MemberNoFormat(ref_id);
                DwList.SetItemString(1, "ref_id", ref_id);
                String sql = @"SELECT MBMEMBMASTER.MEMBER_NO ,          
                               MBMEMBMASTER.PRENAME_CODE ,         
                               MBMEMBMASTER.MEMB_NAME ,        
                               MBMEMBMASTER.MEMB_SURNAME ,     
                               MBMEMBMASTER.MATE_NAME ,  
                               MBUCFPOSITION.POSITION_DESC ,   
                               MBMEMBMASTER.CARD_PERSON ,   
                               MBMEMBMASTER.RESIGN_STATUS ,     
                               MBMEMBMASTER.CLOSE_DATE ,       
                               MBUCFPROVINCE_A.PROVINCE_DESC ,         
                               MBUCFDISTRICT_A.DISTRICT_DESC ,        
                               MBUCFDEPARTMENT.DEPARTMENT_DESC ,      
                               MBUCFTAMBOL_A.TAMBOL_DESC ,          
                               MBMEMBMASTER.TAMBOL_CODE ,         
                               MBMEMBMASTER.MEM_TELWORK ,       
                               MBMEMBMASTER.ADDR_NO ,        
                               MBMEMBMASTER.ADDR_MOO ,         
                               MBMEMBMASTER.ADDR_SOI ,       
                               MBMEMBMASTER.ADDR_VILLAGE ,   
                               MBMEMBMASTER.ADDR_ROAD ,   
                               MBMEMBMASTER.AMPHUR_CODE ,  
                               MBMEMBMASTER.PROVINCE_CODE ,     
                               MBMEMBMASTER.ADDR_POSTCODE ,       
                               MBMEMBMASTER.ADDR_PHONE ,        
                               MBMEMBMASTER.ADDR_MOBILEPHONE ,        
                               MBMEMBMASTER.ADDR_EMAIL ,         
                               MBMEMBMASTER.MARIAGE_STATUS ,    
                               MBMEMBMASTER.CURRADDR_NO ,    
                               MBMEMBMASTER.CURRADDR_MOO , 
                               MBMEMBMASTER.CURRADDR_SOI ,    
                               MBMEMBMASTER.CURRADDR_VILLAGE , 
                               MBMEMBMASTER.CURRADDR_ROAD ,       
                               MBMEMBMASTER.CURRTAMBOL_CODE ,        
                               MBMEMBMASTER.CURRAMPHUR_CODE ,        
                               MBMEMBMASTER.CURRPROVINCE_CODE ,      
                               MBMEMBMASTER.CURRADDR_POSTCODE ,    
                               MBMEMBMASTER.CURRADDR_PHONE ,        
                               MBMEMBMASTER.RETRY_STATUS ,    
			                   MBUCFTAMBOL_B.TAMBOL_DESC ,         
	 	                       MBUCFDISTRICT_B.DISTRICT_DESC ,    
     	                       MBUCFPROVINCE_B.PROVINCE_DESC       
                          FROM MBMEMBMASTER ,          
			                   MBUCFRESIGNCAUSE ,         
 			                   MBUCFDISTRICT MBUCFDISTRICT_A ,     
    		                   MBUCFPROVINCE MBUCFPROVINCE_A ,   
                               MBUCFDEPARTMENT ,           
			                   MBUCFTAMBOL MBUCFTAMBOL_A ,          
 			                   MBUCFTAMBOL MBUCFTAMBOL_B ,        
   			                   MBUCFDISTRICT MBUCFDISTRICT_B ,      
     		                   MBUCFPROVINCE MBUCFPROVINCE_B ,MBUCFPOSITION    
                        WHERE ( MBUCFRESIGNCAUSE.RESIGNCAUSE_CODE (+) = MBMEMBMASTER.RESIGNCAUSE_CODE) and  
        	                  ( MBUCFRESIGNCAUSE.COOP_ID (+) = MBMEMBMASTER.COOP_ID) and       
   		                      ( MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT_A.DISTRICT_CODE (+)) and   
       	                      ( MBUCFPROVINCE_A.PROVINCE_CODE (+) = MBMEMBMASTER.PROVINCE_CODE) and  
                              ( MBMEMBMASTER.PROVINCE_CODE = MBUCFDISTRICT_A.PROVINCE_CODE (+)) and 
                              ( MBMEMBMASTER.DEPARTMENT_CODE = MBUCFDEPARTMENT.DEPARTMENT_CODE (+)) and   
                              ( MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL_A.TAMBOL_CODE (+)) and         
                              ( MBMEMBMASTER.CURRTAMBOL_CODE = MBUCFTAMBOL_B.TAMBOL_CODE (+)) and       
   		                      ( MBMEMBMASTER.CURRAMPHUR_CODE = MBUCFDISTRICT_B.DISTRICT_CODE (+)) and   
                              ( MBMEMBMASTER.CURRPROVINCE_CODE = MBUCFPROVINCE_B.PROVINCE_CODE (+)) and      
                              ( MBMEMBMASTER.COOP_ID = mbucfdepartment.coop_id(+) ) and 
                              (MBUCFPOSITION.POSITION_CODE(+) = MBMEMBMASTER.POSITION_CODE) and                              
 	                          ( MBMEMBMASTER.MEMBER_NO = '" + ref_id + "' )    ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwList.SetItemString(1, "prename_code", dt.Rows[0]["prename_code"].ToString());
                    DwList.SetItemString(1, "name", dt.Rows[0]["MEMB_NAME"].ToString());
                    DwList.SetItemString(1, "surname", dt.Rows[0]["MEMB_SURNAME"].ToString());
                    DwList.SetItemString(1, "house_no", dt.Rows[0]["ADDR_NO"].ToString());
                    DwList.SetItemString(1, "group_no", dt.Rows[0]["ADDR_MOO"].ToString());
                    DwList.SetItemString(1, "soi", dt.Rows[0]["ADDR_SOI"].ToString());
                    DwList.SetItemString(1, "road", dt.Rows[0]["ADDR_ROAD"].ToString());
                    DwList.SetItemString(1, "province", dt.Rows[0]["PROVINCE_CODE"].ToString());
                    DwList.SetItemString(1, "district", dt.Rows[0]["AMPHUR_CODE"].ToString());
                    //retrive data district
                    DataWindowChild childdis = DwList.GetChild("district");
                    childdis.SetTransaction(sqlca);
                    childdis.Retrieve();
                    childdis.Filter();
                    DwList.SetItemString(1, "tumbol", dt.Rows[0]["TAMBOL_CODE"].ToString());
                    //retrive data tumbol
                    DataWindowChild child = DwList.GetChild("tumbol");
                    child.SetTransaction(sqlca);
                    child.Retrieve();
                    child.Filter();
                    DwList.SetItemString(1, "post_code", dt.Rows[0]["ADDR_POSTCODE"].ToString());
                    DwList.SetItemString(1, "phone_no", dt.Rows[0]["ADDR_PHONE"].ToString());          
                }                                  
            }
            else if (ref_type == 2) { 
               
                String sql = @"SELECT MBMEMBMASTER.MEMBER_NO ,          
                               MBMEMBMASTER.PRENAME_CODE ,         
                               MBMEMBMASTER.MEMB_NAME ,        
                               MBMEMBMASTER.MEMB_SURNAME ,     
                               MBMEMBMASTER.MATE_NAME ,  
                               MBUCFPOSITION.POSITION_DESC ,   
                               MBMEMBMASTER.CARD_PERSON ,   
                               MBMEMBMASTER.RESIGN_STATUS ,     
                               MBMEMBMASTER.CLOSE_DATE ,       
                               MBUCFPROVINCE_A.PROVINCE_DESC ,         
                               MBUCFDISTRICT_A.DISTRICT_DESC ,        
                               MBUCFDEPARTMENT.DEPARTMENT_DESC ,      
                               MBUCFTAMBOL_A.TAMBOL_DESC ,          
                               MBMEMBMASTER.TAMBOL_CODE ,         
                               MBMEMBMASTER.MEM_TELWORK ,       
                               MBMEMBMASTER.ADDR_NO ,        
                               MBMEMBMASTER.ADDR_MOO ,         
                               MBMEMBMASTER.ADDR_SOI ,       
                               MBMEMBMASTER.ADDR_VILLAGE ,   
                               MBMEMBMASTER.ADDR_ROAD ,   
                               MBMEMBMASTER.AMPHUR_CODE ,  
                               MBMEMBMASTER.PROVINCE_CODE ,     
                               MBMEMBMASTER.ADDR_POSTCODE ,       
                               MBMEMBMASTER.ADDR_PHONE ,        
                               MBMEMBMASTER.ADDR_MOBILEPHONE ,        
                               MBMEMBMASTER.ADDR_EMAIL ,         
                               MBMEMBMASTER.MARIAGE_STATUS ,    
                               MBMEMBMASTER.CURRADDR_NO ,    
                               MBMEMBMASTER.CURRADDR_MOO , 
                               MBMEMBMASTER.CURRADDR_SOI ,    
                               MBMEMBMASTER.CURRADDR_VILLAGE , 
                               MBMEMBMASTER.CURRADDR_ROAD ,       
                               MBMEMBMASTER.CURRTAMBOL_CODE ,        
                               MBMEMBMASTER.CURRAMPHUR_CODE ,        
                               MBMEMBMASTER.CURRPROVINCE_CODE ,      
                               MBMEMBMASTER.CURRADDR_POSTCODE ,    
                               MBMEMBMASTER.CURRADDR_PHONE ,        
                               MBMEMBMASTER.RETRY_STATUS ,    
			                   MBUCFTAMBOL_B.TAMBOL_DESC ,         
	 	                       MBUCFDISTRICT_B.DISTRICT_DESC ,    
     	                       MBUCFPROVINCE_B.PROVINCE_DESC       
                          FROM MBMEMBMASTER ,          
			                   MBUCFRESIGNCAUSE ,         
 			                   MBUCFDISTRICT MBUCFDISTRICT_A ,     
    		                   MBUCFPROVINCE MBUCFPROVINCE_A ,   
                               MBUCFDEPARTMENT ,           
			                   MBUCFTAMBOL MBUCFTAMBOL_A ,          
 			                   MBUCFTAMBOL MBUCFTAMBOL_B ,        
   			                   MBUCFDISTRICT MBUCFDISTRICT_B ,      
     		                   MBUCFPROVINCE MBUCFPROVINCE_B ,MBUCFPOSITION    
                        WHERE ( MBUCFRESIGNCAUSE.RESIGNCAUSE_CODE (+) = MBMEMBMASTER.RESIGNCAUSE_CODE) and  
        	                  ( MBUCFRESIGNCAUSE.COOP_ID (+) = MBMEMBMASTER.COOP_ID) and       
   		                      ( MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT_A.DISTRICT_CODE (+)) and   
       	                      ( MBUCFPROVINCE_A.PROVINCE_CODE (+) = MBMEMBMASTER.PROVINCE_CODE) and  
                              ( MBMEMBMASTER.PROVINCE_CODE = MBUCFDISTRICT_A.PROVINCE_CODE (+)) and 
                              ( MBMEMBMASTER.DEPARTMENT_CODE = MBUCFDEPARTMENT.DEPARTMENT_CODE (+)) and   
                              ( MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL_A.TAMBOL_CODE (+)) and         
                              ( MBMEMBMASTER.CURRTAMBOL_CODE = MBUCFTAMBOL_B.TAMBOL_CODE (+)) and       
   		                      ( MBMEMBMASTER.CURRAMPHUR_CODE = MBUCFDISTRICT_B.DISTRICT_CODE (+)) and   
                              ( MBMEMBMASTER.CURRPROVINCE_CODE = MBUCFPROVINCE_B.PROVINCE_CODE (+)) and      
                              ( MBMEMBMASTER.COOP_ID = mbucfdepartment.coop_id(+) ) and 
                              ( MBUCFPOSITION.POSITION_CODE(+) = MBMEMBMASTER.POSITION_CODE) and                              
 	                          ( MBMEMBMASTER.CARD_PERSON  = '" + ref_id + "' )    ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwList.SetItemString(1, "prename_code", dt.Rows[0]["prename_code"].ToString());
                    DwList.SetItemString(1, "name", dt.Rows[0]["MEMB_NAME"].ToString());
                    DwList.SetItemString(1, "surname", dt.Rows[0]["MEMB_SURNAME"].ToString());
                    DwList.SetItemString(1, "house_no", dt.Rows[0]["ADDR_NO"].ToString());
                    DwList.SetItemString(1, "group_no", dt.Rows[0]["ADDR_MOO"].ToString());
                    DwList.SetItemString(1, "soi", dt.Rows[0]["ADDR_SOI"].ToString());
                    DwList.SetItemString(1, "road", dt.Rows[0]["ADDR_ROAD"].ToString());
                    DwList.SetItemString(1, "province", dt.Rows[0]["PROVINCE_CODE"].ToString());
                    DwList.SetItemString(1, "district", dt.Rows[0]["AMPHUR_CODE"].ToString());
                    //retrive data district
                    DataWindowChild childdis = DwList.GetChild("district");
                    childdis.SetTransaction(sqlca);
                    childdis.Retrieve();
                    childdis.Filter();
                    DwList.SetItemString(1, "tumbol", dt.Rows[0]["TAMBOL_CODE"].ToString());
                    //retrive data tumbol
                    DataWindowChild child = DwList.GetChild("tumbol");
                    child.SetTransaction(sqlca);
                    child.Retrieve();
                    child.Filter();
                    DwList.SetItemString(1, "post_code", dt.Rows[0]["ADDR_POSTCODE"].ToString());
                    DwList.SetItemString(1, "phone_no", dt.Rows[0]["ADDR_PHONE"].ToString());
                    //ref_id = WebUtil.ViewCardMemberFormat(ref_id);
                    //DwList.SetItemString(1, "ref_id", ref_id);
                } 
            }
            
        }

    }
}
