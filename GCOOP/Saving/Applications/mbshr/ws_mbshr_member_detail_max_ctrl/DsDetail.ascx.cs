using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.DT_DETAILDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAIL;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void RetrieveDetail(String ls_member_no)
        {
            String sql = @"  
                    SELECT MBMEMBMASTER.MEMBER_NO ,          
                           MBMEMBMASTER.PRENAME_CODE ,         
                           MBMEMBMASTER.MEMB_NAME ,        
                           MBMEMBMASTER.MEMB_SURNAME ,     
                           MBMEMBMASTER.BIRTH_DATE ,      
                           MBMEMBMASTER.MEMBER_DATE ,       
                           MBMEMBMASTER.MATE_NAME ,      
                           MBMEMBMASTER.LEVEL_CODE ,      
                           MBMEMBMASTER.SALARY_AMOUNT ,   
                           MBMEMBMASTER.REMARK ,        
                           decode(MBUCFPOSITION.POSITION_DESC ,'',MBMEMBMASTER.POSITION_DESC,MBUCFPOSITION.POSITION_DESC) as POSITION_DESC,   
                           MBMEMBMASTER.CARD_PERSON ,     
                           MBMEMBMASTER.CARD_TAX ,       
                           MBMEMBMASTER.MEMBER_REF ,         
                           MBMEMBMASTER.WORK_DATE ,        
                           MBMEMBMASTER.RETRY_DATE ,       
                           trim(MBMEMBMASTER.SALARY_ID) as salary_id ,          
                           MBMEMBMASTER.RESIGN_DATE ,     
                           MBMEMBMASTER.RESIGN_STATUS ,    
                           MBUCFRESIGNCAUSE.RESIGNCAUSE_DESC ,        
                           MBMEMBMASTER.CLOSE_DATE ,      
decode(MBMEMBMASTER.addr_mobilephone,'',decode(MBMEMBMASTER.addr_phone,'',MBMEMBMASTER.curraddr_phone,MBMEMBMASTER.addr_mobilephone),MBMEMBMASTER.addr_mobilephone) as phone ,
                            MBMEMBMASTER.ADDR_EMAIL,
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
    			           MBMEMBMASTER.MATE_SALARYID ,          
			               MBUCFTAMBOL_B.TAMBOL_DESC ,         
	 	                   MBUCFDISTRICT_B.DISTRICT_DESC ,    
     	                   MBUCFPROVINCE_B.PROVINCE_DESC ,
                           ftcm_calagemth(birth_date,sysdate ) as age,   
                           ftcm_calagemth(work_date,sysdate ) as work_age,   
                           ftcm_calagemth(birth_date,retry_date ) as retry_age,   
                           ftcm_calagemth(member_date ,sysdate ) as memb_age,
                           MBMEMBMASTER.INCOMEETC_AMT       ,
                           MBMEMBMASTER.BUMNED_AMT    ,
                           MBMEMBMASTER.SASOM_AMT    
                      FROM MBMEMBMASTER ,          
			               MBUCFRESIGNCAUSE ,         
 			               MBUCFDISTRICT MBUCFDISTRICT_A ,     
    		               MBUCFPROVINCE MBUCFPROVINCE_A ,   
                           MBUCFDEPARTMENT ,           
			               MBUCFTAMBOL MBUCFTAMBOL_A ,          
 			               MBUCFTAMBOL MBUCFTAMBOL_B ,        
   			               MBUCFDISTRICT MBUCFDISTRICT_B ,      
     		               MBUCFPROVINCE MBUCFPROVINCE_B   ,
                            MBUCFPOSITION   
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
                          ( MBMEMBMASTER.POSITION_CODE = MBUCFPOSITION.POSITION_CODE(+) ) and  
                          (( MBMEMBMASTER.COOP_ID = {0} ) and
 	                      ( MBMEMBMASTER.MEMBER_NO = {1} ) )   ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            string card_person = this.DATA[0].CARD_PERSON.Trim();
            if (card_person.Length == 13)
            {
                card_person = card_person.Substring(0, 1) + "-" + card_person.Substring(1,4) + "-" + card_person.Substring(5, 5) + "-" + card_person.Substring(10, 2) + "-" + card_person.Substring(12, 1);
            }
            this.DATA[0].CARD_PERSON = card_person;

            //decimal retry_status = this.DATA[0].RETRY_STATUS;
            //if (retry_status == 0 || retry_status == 3)
            //{
            //    this.DATA[0].retry_age = "";
            //    this.DATA[0].RETRY_DATE = Convert.ToDateTime("1/1/0544");            
            
            //}

        }
    }
}