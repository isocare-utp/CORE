using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_process_to_cmd_ctrl
{
    public partial class ws_as_process_to_cmd : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostProcess { get; set; }
        [JsPostBack]
        public string PostSave { get; set; }
        [JsPostBack]
        public string PostDefaultAcc { get; set; }
        [JsPostBack]
        public string PostBank { get; set; }
        [JsPostBack]
        public string PostBranch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)//show data first  
            {
                dsMain.GetAssYear();

                // แก้ปัญหาถ้าไม่ active dropdown ปี มัน get ค่า 0 มา
                string sqlStr = @"select max(ass_year) ass_year from assucfyear where coop_id = {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId);
                Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                dt1.Next();
                dsMain.DATA[0].assist_year = Convert.ToDecimal( dt1.GetString("ass_year"));

                dsMain.DATA[0].operate_date = state.SsWorkDate;// state.SsWorkDate.ToString("dd/MM/") + (state.SsWorkDate.Year + 543).ToString();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostProcess)
            {
                dsList.ResetRow();
                GetShowList();
            }

            else if (eventArg == PostSave)
            {
                SaveData();
            }
        }



  
        private void GetShowList()
        {

           //ท่อนดึงข้อมูลผู้ที่มีสิทธิ์ได้รับสวัสดิการ
            string sqlStr = @"
              select  assreqmaster.member_no as member_no, ft_getmemname(assreqmaster.coop_id , assreqmaster.member_no) as full_name, 
            case assreqmastergift.methpay_rcv when '1' then 'รับเอง' when '2' then 'ส่งไปรษณีย์' when '3' then 'รับแทน' else '' end as receive_code 
            , sum(assreqmastergift.unit_amt) as amount_amt , assreqmastergift.invt_id as invt_id, assreqmastergift.unit_code as unit_code
            , assreqmaster.assist_docno as assist_docno
            from assreqmaster , assreqmastergift
            where assreqmaster.assist_docno = assreqmastergift.assist_docno 
		   and assreqmaster.coop_id = {0}
            and assreqmaster.req_status = 1
            and assreqmaster.assist_year = {1}
            and assreqmaster.assisttype_code = '90' 
             and assreqmaster.assist_docno not in (select asssumpaygift.refer_slipno  from asssumpaygift  where asssumpaygift.assist_year = {1})
            group by assreqmaster.member_no , ft_getmemname(assreqmaster.coop_id , assreqmaster.member_no) , 
            assreqmastergift.methpay_rcv, assreqmastergift.invt_id , assreqmastergift.unit_code, assreqmaster.assist_docno";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl , dsMain.DATA[0].assist_year );
            Sdt dt = WebUtil.QuerySdt(sqlStr);
            dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                for (int ii = 0; ii < dt.GetRowCount(); ii++)
                {
                    dsList.InsertAtRow(ii);
                    dsList.DATA[ii].member_no = dt.Rows[ii]["member_no"].ToString();
                    dsList.DATA[ii].full_name = dt.Rows[ii]["full_name"].ToString();
                    dsList.DATA[ii].receive_code = dt.Rows[ii]["receive_code"].ToString();
                    dsList.DATA[ii].amount_amt = Convert.ToDecimal( dt.Rows[ii]["amount_amt"].ToString());
                    dsList.DATA[ii].invt_id = dt.Rows[ii]["invt_id"].ToString();
                    dsList.DATA[ii].unit_code = dt.Rows[ii]["unit_code"].ToString();
                    dsList.DATA[ii].refer_slipno = dt.Rows[ii]["assist_docno"].ToString();

                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("มีรายการประมวลทั้งหมด " + dsList.RowCount + " รายการ");
            }
        }


        public void SaveData()
        {
            //            try
            //            {
            //                string ls_asstypepay = "00";
            //                string sql = @"select
            //	                        assucfassistpaytype.assistpay_code
            //                        from assucfassisttype
            //                        inner join assucfassistpaytype on assucfassisttype.assisttype_group = assucfassistpaytype.assisttype_group
            //                        where assucfassisttype.coop_id = {0} and assucfassisttype.assisttype_code = {1}
            //                        and assucfassistpaytype.assistpay_desc like '%ตนเอง%'";
            //                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMain.DATA[0].assisttype_code);
            //                Sdt dt = WebUtil.QuerySdt(sql);
            //                if (dt.Next()) { ls_asstypepay = dt.GetString("assistpay_code"); }

            //                string ls_reqdate = dsMain.DATA[0].operate_date.Substring(0, 6) + (Convert.ToInt32(dsMain.DATA[0].work_date.Substring(6, 4)) - 543);
            //                for (int ii = 0; ii < dsList.RowCount; ii++)
            //                {
            //                    if (dsList.DATA[ii].choose_flag == 1)
            //                    {
            //                        string ls_assno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTDOCNO");
            //                        string ls_memno = dsList.DATA[ii].member_no;
            //                        decimal dec_appamt = dsList.DATA[ii].approve_amt;

            //                        try
            //                        {
            //                            string sqlStr = @"insert into assreqmaster
            //                            (coop_id                    , coop_control              , assist_docno          , assist_year           , member_no 
            //                            , assisttype_code           , assistpay_code            , approve_amt           , assist_amt            , req_status
            //                            , req_date                  , approve_date              , entry_id              , moneytype_code        , expense_bank
            //                            , expense_branch            , expense_accid             , withdrawable_amt)
            //                            values
            //                            ({0}                        , {1}                       , {2}                   , {3}                   , {4}
            //                            , {5}                       , {6}                       , {7}                   , {8}                   , {9}
            //                            , to_date({10},'dd/mm/yyyy'), to_date({11},'dd/mm/yyyy'), {12}                  , {13}                  , {14}
            //                            , {15}                      , {16}                      , {17})";
            //                            sqlStr = WebUtil.SQLFormat(sqlStr,
            //                                state.SsCoopId, state.SsCoopControl, ls_assno, dsMain.DATA[0].process_year, ls_memno
            //                                , dsMain.DATA[0].assisttype_code, ls_asstypepay, dec_appamt, dec_appamt, "1"
            //                                , ls_reqdate, ls_reqdate, state.SsUsername, dsMain.DATA[0].moneytype_code, dsMain.DATA[0].expense_bank
            //                                , dsMain.DATA[0].expense_branch, dsMain.DATA[0].tofrom_accid, dec_appamt);
            //                            WebUtil.ExeSQL(sqlStr);
            //                        }
            //                        catch
            //                        {
            //                            LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกผิดพลาด เลขสมาชิก " + ls_memno); return;
            //                        }
            //                    }
            //                }
            //                dsList.ResetRow();
            //                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            //            }
            //            catch
            //            {
            //                LtServerMessage.Text = WebUtil.ErrorMessage("Error"); return;
            //            }
        }

        public void SaveWebSheet()
        {
            Int32 seq_no = 0 , month = 0;
            Decimal ldc_unitamt = 0;
            DateTime operate_date = state.SsWorkDate;
            string invt_id = "", ls_err = "", sqlStr = "", ls_unitcode = "", refer_slipno = "";
            try
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    seq_no++;
                    operate_date = dsMain.DATA[0].operate_date;
                    invt_id = dsList.DATA[i].invt_id;
                    ldc_unitamt = dsList.DATA[i].amount_amt;
                    ls_unitcode = dsList.DATA[i].unit_code;
                    refer_slipno = dsList.DATA[i].refer_slipno;
                    month = operate_date.Month;
                    if (ls_err == "")
                    {
                        sqlStr = @"INSERT INTO ASSSUMPAYGIFT
                                        (COOP_ID 			            , OPERATE_DATE 		            , ASSIST_YEAR                    , ASSIST_MONTH                   
					                    , SEQ_NO  						, INVT_ID                  	, UNIT_AMT                     , UNIT_CODE
                                        , POSTTO_DUR                    , REFER_SLIPNO)
                                values
                                        ({0}                            , {1}                           , {2}                           , {3}                           
                                        , {4}                           , {5}                           , {6}                           , {7}
                                        , {8}                           , {9})";

                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, operate_date, dsMain.DATA[0].assist_year, month, seq_no, invt_id, ldc_unitamt, ls_unitcode, 0, refer_slipno);
                        Sdt Insert = WebUtil.QuerySdt(sqlStr);
                    }
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsList.ResetRow();
            }
              catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                return;
            }
        }

        public void WebSheetLoadEnd()
        {
            //if (dsMain.DATA[0].moneytype_code == "CSH")
            //{
            //    this.SetOnLoadedScript("dsMain.GetElement(0, 'expense_bank').style.background = '#CCCCCC'; dsMain.GetElement(0, 'expense_bank').readOnly = true; dsMain.GetElement(0, 'expense_branch').style.background = '#CCCCCC'; dsMain.GetElement(0, 'expense_branch').readOnly = true;");
            //}
        }
    }

}