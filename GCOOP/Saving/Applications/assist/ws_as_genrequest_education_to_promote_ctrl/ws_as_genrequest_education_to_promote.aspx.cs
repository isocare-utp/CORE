using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_genrequest_education_to_promote_ctrl
{
    public partial class ws_as_genrequest_education_to_promote : PageWebSheet, WebSheet
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
                string sqlStr = @"select max(ass_year + 543) ass_year from assucfyear where coop_id = {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId);
                Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                dt1.Next();
                dsMain.DATA[0].assist_year = dt1.GetString("ass_year");
            //    dsMain.DATA[0].process_month = state.SsWorkDate.Month.ToString();

                dsMain.AssistType();
                dsMain.AssistType2();
                //dsMain.DdMoneyType();
            //    dsMain.DdFromAccId();
                dsMain.DATA[0].operate_date = state.SsWorkDate;//.ToString("dd/MM/") + (state.SsWorkDate.Year + 543).ToString();
                dsMain.DATA[0].assisttype_code = "11";
                dsMain.DATA[0].assisttype_code2 = "10";
                //GetDefaultAcc(); //get เงินสด accid 
             //   this.SetOnLoadedScript("dsMain.GetElement(0, 'expense_bank').style.background = '#CCCCCC'; dsMain.GetElement(0, 'expense_bank').readOnly = true; dsMain.GetElement(0, 'expense_branch').style.background = '#CCCCCC'; dsMain.GetElement(0, 'expense_branch').readOnly = true;");
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostProcess)
            {
              //  dsList.ResetRow();
                GetShowList();
            }
            else if (eventArg == PostDefaultAcc)
            {
                GetDefaultAcc();
            }
            else if (eventArg == PostBank)
            {
                dsMain.DdBankDesc();
             //   InitTofromaccid();
            }
            else if (eventArg == PostBranch)
            {
                dsMain.DdBranch(dsMain.DATA[0].expense_bank);
            }


            else if (eventArg == PostSave)
            {
                SaveData();
            }
        }

        private void GetDefaultAcc()
        {
            string sql = @"select cash_account_code from accconstant";
            sql = WebUtil.SQLFormat(sql);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
              //  dsMain.DATA[0].tofrom_accid = dt.GetString("cash_account_code");
            }
        }

        private void InitTofromaccid()
        {
            string sql = @" 
                       SELECT MONEYTYPE_CODE,  
                              MONEYTYPE_GROUP, 
                              MONEYTYPE_DESC,   
                              SORT_ORDER  ,
                              MONEYTYPE_CODE + ' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY,
                              DEFAULTPAY_ACCID
                         FROM CMUCFMONEYTYPE WHERE  MONEYTYPE_GROUP IN('BNK','CHQ','CSH')  AND MONEYTYPE_CODE={0}  order by sort_order
                ";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].moneytype_code);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].tofrom_accid = dt.GetString("DEFAULTPAY_ACCID").Trim();
            }
            else
            {
                dsMain.DATA[0].tofrom_accid = "0";
            }
            dsMain.DdBranch(dsMain.DATA[0].expense_bank);
        }
        private void GetShowList()
        {
            dsList.ResetRow();
            string ls_asscode = dsMain.DATA[0].assisttype_code;
            string  sqlStr ;
            int li_assyear = int.Parse(dsMain.DATA[0].assist_year) - 543;
            //เช็คว่าเป็นการคำนวณแบบเดือน หรือ ปี
            string sql_chk1 = @"select * from assucfassisttype where coop_id = {0} and assisttype_code = {1}";
            sql_chk1 = WebUtil.SQLFormat(sql_chk1, state.SsCoopId, ls_asscode);
            Sdt dt_chk1 = WebUtil.QuerySdt(sql_chk1);
          
            //ท่อนดึงข้อมูลผู้ที่มีสิทธิ์ได้รับสวัสดิการ
            sqlStr = @"
            select asm.req_date , asm.member_no , dbo.ft_getmemname(asm.coop_id , asm.member_no) as full_name , asm.ass_rcvname , asm.edu_gpa  , asm.assist_docno
            ,asm.edu_levelcode, asm.edu_school, asm.deptaccount_no, asm.edu_childbirthdate, asm.ass_prcardid, asm.expense_bank, asm.expense_branch, asm.expense_accid
            ,asm.remark, asm.moneytype_code, asm.ass_rcvcardid, asm.send_system, asm.postsend_date, asm.salary_amount
            from assreqmaster  asm
            where asm.coop_id = {0}
            and asm.req_status = 8 
            and asm.assist_year = {1}
            and asm.assisttype_code = {2}
            and asm.member_no not in ( select am.member_no from assreqmaster am where am.assist_year = {1} and am.assisttype_code = {3} and am.req_status <> -9 )
            order by asm.edu_gpa , asm.req_date ";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, li_assyear, dsMain.DATA[0].assisttype_code, dsMain.DATA[0].assisttype_code2);
            Sdt dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                for (int ii = 0; ii < dt.GetRowCount(); ii++)
                {
                    dsList.InsertAtRow(ii);
                    dsList.DATA[ii].choose_flag = 0;
                    dsList.DATA[ii].req_date = Convert.ToDateTime(dt.Rows[ii]["req_date"].ToString());
                    dsList.DATA[ii].member_no = dt.Rows[ii]["member_no"].ToString();
                    dsList.DATA[ii].full_name = dt.Rows[ii]["full_name"].ToString();
                    dsList.DATA[ii].child_name = dt.Rows[ii]["ass_rcvname"].ToString();
                    dsList.DATA[ii].gpa = dt.Rows[ii]["edu_gpa"].ToString();
                    dsList.DATA[ii].assist_docno = dt.Rows[ii]["assist_docno"].ToString();
                    dsList.DATA[ii].edu_levelcode = dt.Rows[ii]["edu_levelcode"].ToString();
                    dsList.DATA[ii].edu_school = dt.Rows[ii]["edu_school"].ToString();
                    dsList.DATA[ii].deptaccount_no = dt.Rows[ii]["deptaccount_no"].ToString();
                    dsList.DATA[ii].edu_childbirthdate = Convert.ToDateTime(dt.Rows[ii]["edu_childbirthdate"].ToString());
                    dsList.DATA[ii].ass_prcardid = dt.Rows[ii]["ass_prcardid"].ToString();
                    dsList.DATA[ii].expense_bank = dt.Rows[ii]["expense_bank"].ToString();
                    dsList.DATA[ii].expense_branch = dt.Rows[ii]["expense_branch"].ToString();
                    dsList.DATA[ii].expense_accid = dt.Rows[ii]["expense_accid"].ToString();
                    dsList.DATA[ii].remark = dt.Rows[ii]["remark"].ToString();
                    dsList.DATA[ii].moneytype_code = dt.Rows[ii]["moneytype_code"].ToString();
                    dsList.DATA[ii].ass_rcvcardid = dt.Rows[ii]["ass_rcvcardid"].ToString();
                    dsList.DATA[ii].send_system = dt.Rows[ii]["send_system"].ToString();
                    dsList.DATA[ii].postsend_date = Convert.ToDateTime(dt.Rows[ii]["postsend_date"].ToString());
                    dsList.DATA[ii].salary_amount = Convert.ToDecimal(dt.Rows[ii]["salary_amount"].ToString());

                }
              }
            else
            {
                dsMain.DATA[0].all_check = 1;
                LtServerMessage.Text = WebUtil.CompleteMessage("มีรายการใบคำขอทั้งหมด " + dsList.RowCount + " รายการ");
            }
        }


        public void SaveData()
        {
            //หายอดเงินทุนส่งเสริม
            decimal ldc_payamt = 0;
            String sqlSelect = @" select max_firstpayamt from assucfassisttype where assisttype_code = '10' ";
            Sdt dt = WebUtil.QuerySdt(sqlSelect);

            if (dt.Next())
            {
                ldc_payamt = dt.GetDecimal("max_payamt");
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบยอดเงินของสวัสดิการทุนส่งเสริม กรุณาตรวจสอบ");
            }

            for (int ii = 0; ii < dsList.RowCount; ii++)
            {
                int li_check = dsList.DATA[ii].choose_flag;
                int li_assyear = int.Parse(dsMain.DATA[0].assist_year) - 543;
                if (li_check > 0 ) {

                string ls_reqno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTDOCNO");
                string sqlStr = @"INSERT INTO ASSREQMASTER
                    (COOP_ID 			            , ASSIST_DOCNO 		            , ASSISTTYPE_CODE               , ASSIST_YEAR                   , REQ_DATE 
                    , CALAGE_DATE                   , MEMBER_NO                     , ASSISTPAY_CODE                , ASSIST_AMT                    , ASSISTMAX_AMT
                    , ASSISTEVER_AMT                , ASSISTCUT_AMT                 , ASSISTNET_AMT                 , REMARK                        , REQ_STATUS 
                    , ENTRY_ID                     
                    , MONEYTYPE_CODE                , EXPENSE_BANK                  , EXPENSE_BRANCH                , EXPENSE_ACCID                 , SEND_SYSTEM 
                    , DEPTACCOUNT_NO                , ASS_RCVNAME                   , ASS_RCVCARDID                 , ASS_PRCARDID                  , EDU_CHILDBIRTHDATE 
                    , EDU_SCHOOL                    , EDU_LEVELCODE                 , EDU_GPA                       , MED_CAUSE                     , POSTSEND_DATE    
                    , SALARY_AMOUNT            
                    )
                    values
                    ({0}                            , {1}                           , {2}                           , {3}                           , {4} 
                    , {5}                           , {6}                           , {7}                           , {8}                           , {9}  
                    , {10}                          , {11}                          , {12}                          , {13}                          , {14}   
                    , {15}                         
                    , {16}                          , {17}                          , {18}                          , {19}                          , {20}
                    , {21}                          , {22}                          , {23}                          , {24}                          , {25}
                    , {26}                          , {27}                          , {28}                          , {29}                          , {30}
                    , {31}
                    )";

                sqlStr = WebUtil.SQLFormat(sqlStr
              , state.SsCoopId, ls_reqno, dsMain.DATA[0].assisttype_code2, li_assyear, dsMain.DATA[0].operate_date
              , dsMain.DATA[0].operate_date, dsList.DATA[ii].member_no, "01", ldc_payamt, ldc_payamt
              , 0, 0, ldc_payamt, dsList.DATA[ii].remark, 8
              , state.SsUsername
              , dsList.DATA[ii].moneytype_code, dsList.DATA[ii].expense_bank, dsList.DATA[ii].expense_branch, dsList.DATA[ii].expense_accid, dsList.DATA[ii].send_system
              , dsList.DATA[ii].deptaccount_no, dsList.DATA[ii].child_name, dsList.DATA[ii].ass_rcvcardid, dsList.DATA[ii].ass_prcardid, dsList.DATA[ii].edu_childbirthdate
              , dsList.DATA[ii].edu_school, dsList.DATA[ii].edu_levelcode, dsList.DATA[ii].gpa, "ASS", dsList.DATA[ii].postsend_date, dsList.DATA[ii].salary_amount
                    //, dsAmount.DATA[0].DEC_DEADDATE, dsAmount.DATA[0].DEC_CAUSE
                    //, dsAmount.DATA[0].FAM_DOCDATE, dsAmount.DATA[0].DIS_ADDR, dsAmount.DATA[0].DIS_DISDATE, dsAmount.DATA[0].DIS_DISAMT, dsAmount.DATA[0].MED_HPNAME
                    //, dsAmount.DATA[0].MED_CAUSE, dsAmount.DATA[0].MED_STARTDATE, dsAmount.DATA[0].MED_ENDDATE, dsAmount.DATA[0].MED_DAY, dsAmount.DATA[0].STM_FLAG
                    //, dsAmount.DATA[0].STMPAY_TYPE, dsAmount.DATA[0].STMPAY_NUM, dsAmount.DATA[0].PRINCIPAL_BALANCE, dsAmount.DATA[0].PRINCIPAL_CAL, dsAmount.DATA[0].SHARESTK_AMT
                    //, dsAmount.DATA[0].SHARE_VALUE
              );


                WebUtil.ExeSQL(sqlStr);

                string sqlStr_update = "UPDATE ASSREQMASTER SET" +
                    " REQ_STATUS	    = -99" +
                    " WHERE ASSIST_DOCNO = '" + dsList.DATA[ii].assist_docno + "' and coop_id = '" + state.SsCoopId + "'";
                Sdt sql_update = WebUtil.QuerySdt(sqlStr_update);
                }
            }
            GetShowList();
        }

        public void SaveWebSheet()
        {
            SaveData();
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