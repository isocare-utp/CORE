using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.shrlon.ws_sl_reprint_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; set; }


        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        //        public void Retrieve(string member_no, string entry_id, string sliptype_code, string payinslip_no_s, string payinslip_no_e, string slip_date_s, string slip_date_e)
        //        {
        //            string sqlext = "";
        //            if (member_no != "")
        //            {
        //                sqlext += " and slslippayin.member_no = '" + String.Format("{0:00000000}", Convert.ToDecimal(member_no)) + "'";
        //            }
        //            if (entry_id != "")
        //            {
        //                sqlext += " and slslippayin.entry_id = '" + entry_id + "'";
        //            }
        //            if (sliptype_code != "")
        //            {
        //                sqlext += " and slslippayin.sliptype_code = '" + sliptype_code + "'";
        //            }
        //            if (payinslip_no_s != "" && payinslip_no_e != "")
        //            {
        //                sqlext += " and (slslippayin.payinslip_no between '" + payinslip_no_s + "' and '" + payinslip_no_e + "')";
        //            }
        //            if (slip_date_s != "" && slip_date_e != "")
        //            {
        //                sqlext += " and (slslippayin.slip_date between to_date('" + slip_date_s + "','dd/mm/yyyy') and  to_date('" + slip_date_e + "','dd/mm/yyyy'))";

        //            }
        //            string sql = @"  select MBUCFPRENAME.PRENAME_DESC,MBMEMBMASTER.MEMB_NAME,MBMEMBMASTER.MEMB_SURNAME,   
        //                            MBMEMBMASTER.MEMBGROUP_CODE, slslippayin.* 
        //                            from slslippayin,MBMEMBMASTER,MBUCFPRENAME
        //                            WHERE MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE and  
        //                            slslippayin.MEMBER_NO = MBMEMBMASTER.MEMBER_NO and  
        //                            MBMEMBMASTER.COOP_ID = slslippayin.MEMCOOP_ID  and         
        //                            slslippayin.COOP_ID = {0} " + sqlext;
        //            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
        //            DataTable dt = WebUtil.Query(sql);
        //            ImportData(dt);
        //        }

        public void Retrieve(string member_no, string entry_id, string sliptype_code, string document_no_s, string document_no_e, DateTime slip_date_s, DateTime slip_date_e)
        {
            string sqlext = "";
            if (member_no != "")
            {
                sqlext += " and slslippayin.member_no = '" + WebUtil.MemberNoFormat(member_no) + "'";
            }
            if (entry_id != "")
            {
                sqlext += " and slslippayin.entry_id = '" + entry_id + "'";
            }
            if (sliptype_code != "")
            {
                sqlext += " and slslippayin.sliptype_code = '" + sliptype_code + "'";
            }
            if (document_no_s != "" && document_no_e != "")
            {
                sqlext += " and (slslippayin.document_no between '" + document_no_s + "' and '" + document_no_e + "')";
            }
            if (slip_date_s.Year > 1900 && slip_date_e.Year > 1900)
            {
                sqlext += " and slslippayin.slip_date between {1} and {2} ";

            }
            string sql = @"  select TOP 100 MBUCFPRENAME.PRENAME_DESC,MBMEMBMASTER.MEMB_NAME,MBMEMBMASTER.MEMB_SURNAME,   
                            MBMEMBMASTER.MEMBGROUP_CODE, slslippayin.* 
                            from slslippayin,MBMEMBMASTER,MBUCFPRENAME
                            WHERE MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE and  
                            slslippayin.MEMBER_NO = MBMEMBMASTER.MEMBER_NO and  
                            MBMEMBMASTER.COOP_ID = slslippayin.MEMCOOP_ID  and     
                            slslippayin.entry_bycoopid = {3} and
                            slslippayin.COOP_ID = {0} " + sqlext + "order by " + "slslippayin.slip_date desc ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, slip_date_s, slip_date_e, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}