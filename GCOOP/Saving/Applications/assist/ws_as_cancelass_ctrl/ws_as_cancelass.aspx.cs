using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_cancelass_ctrl
{
    public partial class ws_as_cancelass : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMBNo { get; set; }
        [JsPostBack]
        public string PostAssContNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMBNo)
            {
                string ls_mbno = dsMain.DATA[0].MEMBER_NO;

                ls_mbno = WebUtil.MemberNoFormat(ls_mbno);

                dsMain.DATA[0].MEMBER_NO = ls_mbno;

                this.of_setmbinfo(ls_mbno);
            }
            else if (eventArg == PostAssContNo)
            {
                string ls_acccontno;

                ls_acccontno = dsMain.DATA[0].ASSCONTRACT_NO;
                dsMain.InitAssContno(ls_acccontno);
                dsMain.DdAssContNo(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].ASSIST_YEAR = dsMain.DATA[0].ASSIST_YEAR + 543;
            }
        }

        public void SaveWebSheet()
        {
            string ls_asscontno, ls_cause, ls_sql, ls_assisreq, invt_id, ls_unitcode;
            Int32 postto_durflag = 0, assist_mth = 0, seq_no = 0;
            decimal ldc_unitamt = 0;

            ls_asscontno = dsMain.DATA[0].ASSCONTRACT_NO;
            ls_cause = dsMain.DATA[0].CANCEL_CAUSE;
            ls_assisreq = dsMain.DATA[0].ASSISTREQ_DOCNO;

            try
            {
                ls_sql = @" update asscontmaster 
                            set cancel_id = {2}, cancel_date = {3}, cancel_cause = {4}, withdrawable_amt = 0, asscont_status = -9
                            where coop_id = {0} and asscontract_no = {1} ";

                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscontno, state.SsUsername, state.SsWorkDate, ls_cause);
                WebUtil.ExeSQL(ls_sql);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกยกเลิกสวัสดิการไม่สำเร็จ : " + ex);
                return;
            }

            try
            {
                ls_sql = @" update assreqmaster 
                            set req_status = 8, approve_id = null, approve_date = null
                            where coop_id = {0} and assist_docno = {1} ";

                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_assisreq);
                WebUtil.ExeSQL(ls_sql);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกการคืนรายการใบคำขอไม่สำเร็จ : " + ex);
                return;
            }



            if (dsMain.DATA[0].assisttype_group == "07")
            {

                try
                {
                    ls_sql = @" update assreqmastergift 
                            set slip_status = 8
                            where coop_id = {0} and assist_docno = {1} ";

                    ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_assisreq);
                    WebUtil.ExeSQL(ls_sql);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรายการยกเลิกฝั่งพัสดุครุภัณฑ์ไม่สำเร็จ : " + ex);
                    return;
                }

                //ตรวจสอบข้อมูลฝั่งพัสดุว่ามีการดึงข้อมูลไปตัด stock แล้วรึยัง
                string sqlGift = @"select * from asssumpaygift where coop_id = {0} and refer_slipno = {1} and  seq_no = ( select max(seq_no) from asssumpaygift where refer_slipno = {1} )";
                sqlGift = WebUtil.SQLFormat(sqlGift, state.SsCoopId, ls_assisreq);
                Sdt dt1 = WebUtil.QuerySdt(sqlGift);
                dt1.Next();
                postto_durflag  = Convert.ToInt32(dt1.GetString("postto_dur")); // 0 ยังไม่ post ไปตัด stock 1 ตัด stock แล้ว  ต้องยิงรายการยกเลิกไปแทน
                assist_mth      = Convert.ToInt32(dt1.GetString("assist_month"));
                invt_id         = dt1.GetString("invt_id");
                ldc_unitamt = Convert.ToDecimal(dt1.GetString("unit_amt"));
                ls_unitcode = dt1.GetString("unit_code");
                seq_no = Convert.ToInt32(dt1.GetString("seq_no"));

                if (postto_durflag == 0)
                {
                    try
                    {
                        ls_sql = @" update asssumpaygift 
                            set slip_status = -9
                            where coop_id = {0} and refer_slipno = {1} and seq_no = {2}";

                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_assisreq, seq_no);
                        WebUtil.ExeSQL(ls_sql);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรายการยกเลิกการส่งข้อมูลไปฝั่งพัสดุครุภัณฑ์ไม่สำเร็จ : " + ex);
                        return;
                    }
                }
                else if (postto_durflag == 1)
                {
                    try
                    {
                        string sqlStr = @"INSERT INTO ASSSUMPAYGIFT
                    (COOP_ID 			            , OPERATE_DATE 		            , ASSIST_YEAR                   , ASSIST_MONTH                   
					, SEQ_NO  						, INVT_ID                  	    , UNIT_AMT                      , UNIT_CODE
                    , POSTTO_DUR                    , REFER_SLIPNO                  , PAY_RECV_STATUS               , SLIP_STATUS)
                    values
                    ({0}                            , {1}                           , {2}                           , {3}                           
                    , {4}                           , {5}                           , {6}                           , {7}
                    , {8}                           , {9}                           , {10}                          , {11})";

                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, state.SsWorkDate, dsMain.DATA[0].ASSIST_YEAR - 543, assist_mth, seq_no+1, invt_id, ldc_unitamt, ls_unitcode, 0, ls_assisreq, 1, 1);
                        Sdt Insert = WebUtil.QuerySdt(sqlStr);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรายการคืนของขวัญไปฝั่งพัสดุครุภัณฑ์ไม่สำเร็จ : " + ex);
                        return;
                    }
                }
            }

            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            dsMain.ResetRow();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        private void of_setmbinfo(string as_memno)
        {
            string ls_sql;
            ls_sql = @" select 
		                        mpre.prename_desc +mb.memb_name + '  ' + mb.memb_surname as mbname,
		                        rtrim(ltrim( mb.membgroup_code) )+' - '+mgrp.membgroup_desc as mbgroup,
		                        rtrim( ltrim(mb.membtype_code) )+ ':' + mt.membtype_desc  as mbtype
                        from	mbmembmaster mb
		                        join mbucfprename mpre on mb.prename_code = mpre.prename_code
		                        join mbucfmembgroup mgrp on mb.membgroup_code = mgrp.membgroup_code
		                        join mbucfmembtype mt on mb.membtype_code = mt.membtype_code
                        where mb.coop_id = {0}
                        and mb.member_no = {1}";

            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_memno);
            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (!dt.Next())
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิกเลขที่ "+as_memno+" กรุณาตรวจสอบ");
                return;
            }

            dsMain.DATA[0].mbname = dt.GetString("mbname");
            dsMain.DATA[0].mbgroup = dt.GetString("mbgroup");
            dsMain.DATA[0].mbtype = dt.GetString("mbtype");

            dsMain.DdAssContNo(as_memno);
        }

    }
}