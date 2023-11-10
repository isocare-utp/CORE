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
using System.Data;


namespace Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl
{
    public partial class ws_sl_collateral_master_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostCollmast { get; set; }
        [JsPostBack]
        public String PostGetMember { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsCollprop.InitDsCollprop(this);
            dsColluse.InitDsColluse(this);
            dsColluseSum.InitDsColluseSum(this);
            dsDetail.InitDsDetail(this);
            dsList.InitDsList(this);
            dsListSum.InitDsListSum(this);
            dsMemco.InitDsMemco(this);
            dsReview.InitDsReview(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DdCollmasttype();
                dsDetail.DdCollrelation();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                PostMemberNo2();
            }
            else if (eventArg == PostCollmast)
            {
                PostCollmast2();
            }
            else if (eventArg == PostGetMember) {
                PostGetMember2();
            }

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        private void PostMemberNo2()
        {
            try
            {
                dsDetail.DdCollrelation();
                dsDetail.DdCollmasttype();
                //เมื่อ member_no มีการเปลี่ยนแปลง
                str_lncollmast strlncoll = new str_lncollmast();
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                //dsMain.RetrieveMain(member_no);
                strlncoll.member_no = member_no;
                strlncoll.coop_id = state.SsCoopControl;
                strlncoll.xml_memdet = dsMain.ExportXml();
                strlncoll.xml_collmastlist = dsList.ExportXml();

                int result = wcf.NShrlon.of_initlncollmastall(state.SsWsPass, ref strlncoll);
                if (result == 1)
                {
                    try
                    {
                        dsMain.ResetRow();
                        dsMain.ImportData(strlncoll.xml_memdet);
                        // dsList.RetrieveList(member_no);
                        try
                        {
                            dsMemco.InsertLastRow();

                            string sql = @" 
                                                            select ft_memname( coop_id , member_no ) as memname 
                                                            from mbmembmaster 
                                                            where coop_id = {0} 
                                                            and member_no = {1}";

                            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
                            Sdt dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            {
                                string memname = dt.GetString("memname");
                                dsCollprop.InsertLastRow();
                                dsCollprop.DATA[0].PROP_DESC = memname;
                            }

                            int row = dsMemco.RowCount - 1;
                            dsMemco.DATA[row].MEMCO_NO = member_no;
                            dsMemco.DATA[row].COLLMASTMAIN_FLAG = 1;
                            Hdmemco_no.Value = member_no;
                            PostGetMember2();
                        }
                        catch { }
                        if (strlncoll.xml_collmastlist == "")
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบหลักทรัพย์ กรุณเพิ่มหลักทรัพย์ " + strlncoll.xml_collmastlist);
                            JsPostInsertRow();
                        }
                        else
                        {

                            dsList.ResetRow();
                            dsList.ImportData(strlncoll.xml_collmastlist);
                            int row_count = dsList.RowCount;
                            decimal sum_cp_redeem = 0;
                            for (int i = 0; i < row_count; i++)
                            {
                                decimal cp_redeem = dsList.DATA[i].cp_redeem;
                                sum_cp_redeem += cp_redeem;
                            }
                            dsListSum.DATA[0].cp_sum_redeemflag = sum_cp_redeem;
                            //dsList.RetrieveList(member_no);
                            // DwUtil.RetrieveDDDW(dw_list, "collmasttype_code", "sl_collateral_master.pbl", null);
                        }
                    }

                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบสมาชิกท่านนี้ กรุณากรอกเลขสมาชิกอีกครั้ง");
                        dsMain.ResetRow();

                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }
        private void PostCollmast2()
        {
            try
            {
                int row = dsList.GetRowFocus();
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.collmast_no = dsList.DATA[row].COLLMAST_NO;
                strlncoll.coop_id = state.SsCoopControl;
                //int li_row = dsList.FindRow("collmast_no = '" + strlncoll.collmast_no.Trim() + "'", 0, dw_list.RowCount);
                //if (li_row > 0) 
                //{
                //    strlncoll.xml_collmastdet = dw_head.Describe("DataWindow.Data.XML");
                //    strlncoll.xml_collmemco = dw_detail.Describe("DataWindow.Data.XML");
                //    strlncoll.xml_review = dw_review.Describe("DataWindow.Data.XML");
                //    strlncoll.xml_prop = dw_collprop.Describe("DataWindow.Data.XML");
                //    strlncoll.xml_colluse = dw_colluse.Describe("DataWindow.Data.XML");
                strlncoll.xml_collmastdet = dsDetail.ExportXml();
                strlncoll.xml_collmemco = dsMemco.ExportXml();
                strlncoll.xml_review = dsReview.ExportXml();
                strlncoll.xml_prop = dsCollprop.ExportXml();
                strlncoll.xml_colluse = dsColluse.ExportXml();
                int result = wcf.NShrlon.of_initlncollmastdet(state.SsWsPass, ref  strlncoll);
                try
                {
                    try
                    {
                        dsDetail.ResetRow();
                        dsDetail.ImportData(strlncoll.xml_collmastdet);

                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }


                    if (dsDetail.RowCount > 1)
                    {
                        // dsDetail.DeleteRow(dsDetail.RowCount);
                    }

                    try
                    {
                        dsMemco.ResetRow();
                        dsMemco.ImportData(strlncoll.xml_collmemco);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    try
                    {
                        dsReview.ResetRow();
                        dsReview.ImportData(strlncoll.xml_review);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    try
                    {
                        dsCollprop.ResetRow();
                        dsCollprop.ImportData(strlncoll.xml_prop);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    try
                    {
                        dsColluse.ResetRow();
                        dsColluse.ImportData(strlncoll.xml_colluse);
                        int row_count = dsColluse.RowCount;
                        decimal sum_cp_colluse = 0;
                        for (int i = 0; i < row_count; i++)
                        {
                            decimal cp_colluse = dsColluse.DATA[i].cp_colluse;
                            sum_cp_colluse += cp_colluse;
                        }
                        dsColluseSum.DATA[0].sum_cp_colluse = sum_cp_colluse;
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    dsDetail.DdCollmasttype();
                    dsDetail.DdCollrelation();
                }
                catch
                {

                }
                try
                {
                    string sql = @" 
                         select		sum( tmp_coll.coll_use ) as sumcoll_use
		                from
		                ( select		trunc(( ( ( b.principal_balance + b.withdrawable_amt ) * a.coll_percent ) ) / a.base_percent, 3)  as coll_use 
		                from		lncontcoll a, lncontmaster b
		                where	( a.coop_id	= b.coop_id )
		                and		( a.loancontract_no = b.loancontract_no )
		                and		( a.loancolltype_code = '04' )
		                and		( a.ref_collno = {0} )
		                and		( b.contract_status > 0 ) ) tmp_coll";
                    String collmast_no = dsDetail.DATA[0].COLLMAST_NO;//.GetItemString(1, "collmast_no");
                    sql = WebUtil.SQLFormat(sql, collmast_no);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        decimal colluse_amt = dt.GetDecimal("sumcoll_use");
                        dsDetail.DATA[0].colluse_amt = colluse_amt;//.SetItemDecimal(1, "colluse_amt", colluse_amt);
                    }
                }
                catch { }

                //ไถ่ถอน

                decimal flag = dsDetail.DATA[0].REDEEM_FLAG;//.GetItemDecimal(1, "redeem_flag");
                Hdfredeemflag.Value = Convert.ToString(flag);


                //dsList.SelectRow(0, false);
                //dw_list.SelectRow(li_row, true);
                //dw_list.SetRow(li_row);
                //}
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }
        private void PostGetMember2()
        {
            String member_no = Hdmemco_no.Value;//WebUtil.MemberNoFormat(Hdmemco_no.Value);
            string member_no2 = WebUtil.MemberNoFormat(member_no);
            int row = dsMemco.RowCount - 1;
            String sql = @" SELECT MEMBER_NO,   
                                       ( MBMEMBMASTER.MEMB_NAME||'  '||  MBMEMBMASTER.MEMB_SURNAME)as MEMB_NAME
                                    FROM MBMEMBMASTER       WHERE   MBMEMBMASTER.MEMBER_NO='" + member_no2 + "' ";
            DataTable dt = WebUtil.Query(sql);
            if (dt.Rows.Count > 0)
            {
                dsMemco.DATA[row].MEMCO_NO = dt.Rows[0]["MEMBER_NO"].ToString();//.Get("MEMBER_NO");//dt.Rows.GetString("MEMBER_NO");//.SetItemString(row, "memco_no", dt.Rows[0]["MEMBER_NO"].ToString());
                dsMemco.DATA[row].mem_name = dt.Rows[0]["MEMB_NAME"].ToString();
            }

        }
        private void JsPostInsertRow()
        {


            if (dsDetail.RowCount > 1)
            {
                //dsDetail.Delete(dsDetail.RowCount);
            }
            //GetDDDW();
        }
        private void JsNewClear()
        {
            dsMain.ResetRow();
            dsList.ResetRow();
            dsDetail.ResetRow();
            dsColluse.ResetRow();
            dsMemco.ResetRow();
            dsCollprop.ResetRow();
            dsReview.ResetRow();


            dsList.InsertLastRow();
            dsMemco.InsertLastRow();
            dsCollprop.InsertLastRow();


            //tdwhead.Eng2ThaiAllRow();
            //tdwreview.Eng2ThaiAllRow();

            //GetDDDW();
        }
    }
}