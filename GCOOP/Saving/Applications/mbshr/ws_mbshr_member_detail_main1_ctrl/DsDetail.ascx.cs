using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
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
            String sql = @"  SELECT MBMEMBMASTER.MEMBER_NO ,          
                           MBMEMBMASTER.PRENAME_CODE ,         
                           MBMEMBMASTER.MEMB_NAME ,        
                           MBMEMBMASTER.MEMB_SURNAME ,     
                           MBMEMBMASTER.BIRTH_DATE ,      
                           MBMEMBMASTER.MEMBER_DATE ,       
                           MBMEMBMASTER.MATE_NAME ,      
                           MBMEMBMASTER.LEVEL_CODE ,      
                           MBMEMBMASTER.SALARY_AMOUNT ,   
                           MBMEMBMASTER.REMARK ,        
                           (CASE WHEN MBUCFPOSITION.POSITION_DESC = '' THEN MBMEMBMASTER.POSITION_DESC ELSE MBUCFPOSITION.POSITION_DESC END) as POSITION_DESC,   
                           MBMEMBMASTER.CARD_PERSON ,     
                           MBMEMBMASTER.CARD_TAX ,       
                           MBMEMBMASTER.MEMBER_REF ,         
                           MBMEMBMASTER.WORK_DATE ,        
                           MBMEMBMASTER.RETRY_DATE ,       
                           ltrim(rtrim(MBMEMBMASTER.SALARY_ID)) as salary_id ,          
                           MBMEMBMASTER.RESIGN_DATE ,     
                           MBMEMBMASTER.RESIGN_STATUS ,    
                           MBUCFRESIGNCAUSE.RESIGNCAUSE_DESC ,        
                           MBMEMBMASTER.CLOSE_DATE ,      
(case when MBMEMBMASTER.addr_mobilephone = '' then MBMEMBMASTER.addr_phone
         when MBMEMBMASTER.addr_phone = '' then MBMEMBMASTER.curraddr_phone 
        else MBMEMBMASTER.addr_mobilephone end) as phone,
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
                           dbo.ftcm_calagemth(birth_date,getdate () ) as age,   
                          dbo.ftcm_calagemth(work_date,getdate () ) as work_age,   
                           dbo.ftcm_calagemth(birth_date,retry_date ) as retry_age,   
                           dbo.ftcm_calagemth(member_date ,getdate () ) as memb_age,
                           MBMEMBMASTER.INCOMEETC_AMT       ,
                           MBMEMBMASTER.BUMNED_AMT    ,
                           MBMEMBMASTER.SASOM_AMT    
                      FROM MBMEMBMASTER left join MBUCFRESIGNCAUSE on MBUCFRESIGNCAUSE.RESIGNCAUSE_CODE = MBMEMBMASTER.RESIGNCAUSE_CODE and
                                                                     MBUCFRESIGNCAUSE.COOP_ID  = MBMEMBMASTER.COOP_ID
                                                         left join MBUCFDISTRICT MBUCFDISTRICT_A on MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT_A.DISTRICT_CODE and
                                                                     MBMEMBMASTER.PROVINCE_CODE = MBUCFDISTRICT_A.PROVINCE_CODE
			                                           left join MBUCFPROVINCE MBUCFPROVINCE_A on MBUCFPROVINCE_A.PROVINCE_CODE = MBMEMBMASTER.PROVINCE_CODE
                                                        left join  MBUCFDEPARTMENT on MBMEMBMASTER.DEPARTMENT_CODE = MBUCFDEPARTMENT.DEPARTMENT_CODE and
                                                                     MBMEMBMASTER.COOP_ID = mbucfdepartment.coop_id
                                                        left join  MBUCFTAMBOL MBUCFTAMBOL_A on MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL_A.TAMBOL_CODE   
			                                          left join  MBUCFTAMBOL MBUCFTAMBOL_B on MBMEMBMASTER.CURRTAMBOL_CODE = MBUCFTAMBOL_B.TAMBOL_CODE
                                                        left join MBUCFDISTRICT MBUCFDISTRICT_B on MBMEMBMASTER.CURRAMPHUR_CODE = MBUCFDISTRICT_B.DISTRICT_CODE 
 			                                          left join  MBUCFPROVINCE MBUCFPROVINCE_B on MBMEMBMASTER.CURRPROVINCE_CODE = MBUCFPROVINCE_B.PROVINCE_CODE
                                                         left join MBUCFPOSITION on MBMEMBMASTER.POSITION_CODE = MBUCFPOSITION.POSITION_CODE

                    WHERE     
                          MBMEMBMASTER.COOP_ID = {0}  and
 	                      MBMEMBMASTER.MEMBER_NO = {1}   "; 
                   

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