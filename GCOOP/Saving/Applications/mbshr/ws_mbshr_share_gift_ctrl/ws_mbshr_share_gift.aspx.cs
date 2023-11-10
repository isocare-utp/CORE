using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_share_gift_ctrl
{
    public partial class ws_mbshr_share_gift : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostProcessShrGift { get; set; }
        [JsPostBack]
        public string PostShrAmt { get; set; }
        [JsPostBack]
        public string PostRetrieveMain { get; set; }

        Sdt ta = new Sdt();

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].MEMBER_TYPE = "1";
                RetrieveMain();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostProcessShrGift")
            {
                of_postshrgift();
            }
            if (eventArg == "PostShrAmt")
            {
                of_postshramt();
            }
            if (eventArg == "PostRetrieveMain")
            {
                RetrieveMain();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        private void of_postshrgift()
        {
            String member_no , membgroup_code, last_period, gift_year, gift_period;
            String shshare_type;
            decimal sharestk_amt, last_stm_no, sharestk_gift;
            decimal unitshar, share_amount;
            shshare_type = dsMain.DATA[0].SHARETYPE_CODE;
            unitshar = GetUnitshr(shshare_type);
            share_amount = dsMain.DATA[0].SHARE_AMOUNT;
            sharestk_gift = share_amount / unitshar;
            gift_year = Convert.ToString(WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate));
            gift_period = gift_year + state.SsWorkDate.ToString("MM");
            //ดึงข้อมูลสมาชิกที่จะได้รางวัลหุ้น
            try
            {
                String sqlre = @"SELECT MBMEMBMASTER.MEMBER_NO, 
                        MBMEMBMASTER.MEMBGROUP_CODE,
                        SHSHAREMASTER.LAST_PERIOD,
                        SHSHAREMASTER.SHARESTK_AMT, 
                        SHSHAREMASTER.LAST_STM_NO 
                        FROM SHSHAREMASTER, MBMEMBMASTER 
                        WHERE SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO AND
                        SHAREMASTER_STATUS = 1 AND
                        SHARETYPE_CODE = '01' AND
                        SHSHAREMASTER.COOP_ID = {0} AND
                        MBMEMBMASTER.MEMBER_TYPE {1}
                        ORDER BY MBMEMBMASTER.MEMBER_NO";
                sqlre = WebUtil.SQLFormat(sqlre, state.SsCoopControl, dsMain.DATA[0].MEMBER_TYPE);
                ta = WebUtil.QuerySdt(sqlre);
                while(ta.Next())
                {
                    member_no = ta.GetString("MBMEMBMASTER.MEMBER_NO");
                    membgroup_code = ta.GetString("MBMEMBMASTER.MEMBGROUP_CODE").Trim();
                    last_period = ta.GetString("SHSHAREMASTER.LAST_PERIOD");
                    sharestk_amt = ta.GetDecimal("SHSHAREMASTER.SHARESTK_AMT") + sharestk_gift;
                    last_stm_no = ta.GetDecimal("SHSHAREMASTER.LAST_STM_NO") + 1;
                    last_period = ta.GetString("SHSHAREMASTER.LAST_PERIOD");
                    //insert SHSHAREGIFT
                    String insershgilf = @"INSERT INTO SHSHAREGIFT 
                        (SHRGIFT_YEAR , SHRGIFT_PERIOD , MEMBER_NO , SHARETYPE_CODE, SHARESTK_VALUE , SHRGIFT_AMT , 
                        SHRGIFT_VALUE, SHRGIFT_STATUS, POSTING_STATUS , POSTING_DATE , MEMBGROUP_CODE, OPERATE_DATE) values 
                        {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11} ";
                    insershgilf = WebUtil.SQLFormat(insershgilf, gift_year, gift_period, member_no, shshare_type, sharestk_amt, share_amount,
                                    sharestk_gift, 1, 1, state.SsWorkDate, membgroup_code, state.SsWorkDate);
                    Sdt sd1 = WebUtil.QuerySdt(insershgilf);

                    //insert shsharestatement
                    String insshrstat = @"INSERT INTO SHSHARESTATEMENT 
                        (COOP_ID, MEMBER_NO, SHARETYPE_CODE, SEQ_NO , SLIP_DATE , OPERATE_DATE , SHARE_DATE , ACCOUNT_DATE , 
                        REF_DOCNO , REF_SLIPNO, SHRITEMTYPE_CODE, PERIOD, SHARE_AMOUNT, SHARESTK_AMT , 
                        MONEYTYPE_CODE, ITEM_STATUS , ENTRY_ID , ENTRY_DATE, ENTRY_BYCOOPID, REMARK, CALDIV_STATUS ) VALUES
                        {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}";
                    insshrstat = WebUtil.SQLFormat(insshrstat, state.SsCoopControl, member_no, shshare_type, last_stm_no, state.SsWorkDate,
                        state.SsWorkDate, state.SsWorkDate, state.SsWorkDate, null, null, "SPW", last_period, sharestk_gift, sharestk_amt,
                        "TRN", 1, state.SsUsername, state.SsWorkDate, state.SsCoopId, "รางวัลหุ้น", 1);
                    Sdt sd2 = WebUtil.QuerySdt(insshrstat);

                    //update sharemaster
                    String sqlupshr = @"update shsharemaster set sharestk_amt = {0}, last_stm_no = {1} where member_no = {2}";
                    sqlupshr = WebUtil.SQLFormat(sqlupshr, sharestk_amt, last_stm_no, member_no);
                    Sdt sd3 = WebUtil.QuerySdt(sqlupshr);
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch(Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex);}
        }

        private void RetrieveMain()
        {
            decimal count_memb = 0;
            string sharetype_code = "";

            String sql = @"select count(shsharemaster.member_no) as count_memb, shsharemaster.sharetype_code  
                           from shsharemaster,  mbmembmaster 
						   where shsharemaster.member_no = mbmembmaster.member_no
						   and shsharemaster.coop_id = mbmembmaster.coop_id
						   and shsharemaster.sharemaster_status = 1
                           and shsharemaster.sharetype_code = '01'
                           and shsharemaster.coop_id = {0}
						   and mbmembmaster.member_type = {1}
                           group by shsharemaster.sharetype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MEMBER_TYPE);
            ta = WebUtil.QuerySdt(sql);
            if (ta.Next())
            {
                count_memb = ta.GetDecimal("count_memb");
                sharetype_code = ta.GetString("sharetype_code");
            }
            dsMain.DATA[0].COOP_ID = state.SsCoopControl;
            dsMain.DATA[0].COUNT_MEMB = count_memb;
            dsMain.DATA[0].SHARETYPE_CODE = sharetype_code;
            dsMain.DATA[0].SHARE_AMOUNT = 0;
            dsMain.DATA[0].SHARE_AMTNET = 0;
        }

        private void of_postshramt()
        {
            decimal sharamt = 0, cmemb = 0, sumshrnet = 0 ;
            sharamt = dsMain.DATA[0].SHARE_AMOUNT;
            cmemb = dsMain.DATA[0].COUNT_MEMB;
            sumshrnet = sharamt * cmemb;
            dsMain.DATA[0].SHARE_AMTNET = sumshrnet;
        }

        private decimal GetUnitshr(String sharetypecode)
        {
            decimal unitshr = 0;
            String sql = @"select * from shsharetype where coop_id = {0} and sharetype_code = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, sharetypecode);
            ta = WebUtil.QuerySdt(sql);
            if (ta.Next())
            {
                unitshr = ta.GetDecimal("unitshare_value");
            }
            return unitshr;
        }
    }
}