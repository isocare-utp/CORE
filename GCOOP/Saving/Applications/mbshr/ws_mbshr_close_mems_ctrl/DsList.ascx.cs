using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void PostRetrieveList(decimal close_type)
        {
            String sql = "";
            if (close_type == 0)
            {
                sql = @"select TOP 500
mrs.coop_id , mrs.member_no , mp.prename_desc , mm.memb_name , mm.memb_surname , mm.membgroup_code , mg.membgroup_desc , mm.resign_date , mr.resigncause_desc
            from(	
	            /*หุ้น*/
	            select 'SHR' as bizz_system , m.coop_id , m.member_no , (Case when  sm.sharestk_amt is null then  ' '  else sm.sharestk_amt end) as bizz_balance
	            from mbmembmaster m , shsharemaster sm
	            where m.coop_id = sm.coop_id
	            and m.member_no = sm.member_no
	            and m.member_status = 1
	            and m.resign_status =1
	            and sm.sharestk_amt >= 0
	            union
	            /*หนี้*/
	            select 'LON' as bizz_system , m.coop_id , m.member_no , (case when  lm.principal_balance is null  then ' ' else  lm.principal_balance end) as bizz_balance
	            from mbmembmaster m , lncontmaster lm
	            where m.coop_id = lm.memcoop_id
	            and m.member_no = lm.member_no
	            and m.member_status = 1
	            and m.resign_status = 1
	            and lm.principal_balance >= 0
	            union
	            /*ภาระค้ำ*/
	            select 'LON' as bizz_system , m.coop_id , m.member_no , (case when  lm.principal_balance is null then ' ' else  lm.principal_balance end) as bizz_balance
	            from mbmembmaster m , lncontcoll lc , lncontmaster lm
	            where m.coop_id = lc.refcoop_id
	            and m.member_no = lc.ref_collno
	            and lc.coop_id = lm.coop_id
	            and lc.loancontract_no = lm.loancontract_no
	            and m.member_status = 1
	            and m.resign_status = 1
	            and lc.loancolltype_code = 01
	            and lm.principal_balance >= 0
	            union
	            /*เงินฝาก*/
	            select 'DEP' as bizz_system , m.coop_id , m.member_no , (Case when dm.prncbal is null then ' ' else dm.prncbal end  ) as bizz_balance
	            from mbmembmaster m , dpdeptmaster dm
	            where m.coop_id = dm.memcoop_id
	            and m.member_no = dm.member_No
	            and m.member_status = 1
	            and m.resign_status =1
	            and dm.prncbal >= 0
            ) mrs ,mbucfprename mp , mbmembmaster mm 
    
            left join mbucfmembgroup mg   on mm.membgroup_code = mg.membgroup_code and mm.coop_id = mg.coop_id
           left join  mbucfresigncause mr on mm.coop_id = mr.coop_id  and mm.resigncause_code = mr.resigncause_code

            where mrs.coop_id = mm.coop_id
            and mrs.member_no = mm.member_no
            and mm.prename_code = mp.prename_code


            group by mrs.coop_id , mrs.member_no , mp.prename_desc , mm.memb_name , mm.memb_surname , mm.membgroup_code , mg.membgroup_desc , mm.resign_date , mr.resigncause_desc
            having sum( mrs.bizz_balance ) = 0 ";
            }
            else 
            {
                sql = @"select TOP 500
mrs.coop_id , mrs.member_no , mp.prename_desc , mm.memb_name , mm.memb_surname , mm.membgroup_code , mg.membgroup_desc , mm.resign_date , mr.resigncause_desc
            from(	
	            /*หุ้น*/
	            select 'SHR' as bizz_system , m.coop_id , m.member_no , (case when  sm.sharestk_amt is null  then ' ' else sm.sharestk_amt  end ) as bizz_balance
	            from mbmembmaster m , shsharemaster sm
	            where m.coop_id = sm.coop_id
	            and m.member_no = sm.member_no
	            and m.member_status = 1
	            and m.resign_status = 1
	            and sm.sharestk_amt >= 0
	            union
	            /*หนี้*/
	            select 'LON' as bizz_system , m.coop_id , m.member_no , ( case when lm.principal_balance is null  then ' ' else lm.principal_balance end  ) as bizz_balance
	            from mbmembmaster m , lncontmaster lm
	            where m.coop_id = lm.memcoop_id
	            and m.member_no = lm.member_no
	            and m.member_status = 1
	            and m.resign_status = 1
	            and lm.principal_balance >= 0
	            union
	            /*ภาระค้ำ*/
	            select 'LON' as bizz_system , m.coop_id , m.member_no ,(case when  lm.principal_balance is null  then ' ' else lm.principal_balance end ) as bizz_balance
	            from mbmembmaster m , lncontcoll lc , lncontmaster lm
	            where m.coop_id = lc.refcoop_id
	            and m.member_no = lc.ref_collno
	            and lc.coop_id = lm.coop_id
	            and lc.loancontract_no = lm.loancontract_no
	            and m.member_status = 1
	            and m.resign_status = 1
	            and lc.loancolltype_code = '01'
	            and lm.principal_balance >= 0
	            union
	            /*เงินฝาก*/
	            select 'DEP' as bizz_system , m.coop_id , m.member_no , (case when  dm.prncbal is null  then ' ' else dm.prncbal end  ) as bizz_balance
	            from mbmembmaster m , dpdeptmaster dm
	            where m.coop_id = dm.memcoop_id
	            and m.member_no = dm.member_No
	            and m.member_status = 1
	            and m.resign_status = 1
	            and dm.prncbal >= 0
            ) mrs , mbmembmaster mm 

           left join mbucfprename mp  on mm.prename_code = mp.prename_code
           left  join mbucfmembgroup mg on mm.coop_id = mg.coop_id and mm.membgroup_code = mg.membgroup_code
          left join mbucfresigncause mr on mm.coop_id = mr.coop_id and mm.resigncause_code = mr.resigncause_code

            where mrs.coop_id = mm.coop_id
            and mrs.member_no = mm.member_no

            
         

            group by mrs.coop_id , mrs.member_no , mp.prename_desc , mm.memb_name , mm.memb_surname , mm.membgroup_code , mg.membgroup_desc , mm.resign_date , mr.resigncause_desc
            having sum( mrs.bizz_balance ) > 0 ";
            }
            //sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}