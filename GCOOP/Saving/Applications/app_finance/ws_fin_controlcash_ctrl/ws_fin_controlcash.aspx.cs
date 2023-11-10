using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Drawing;

namespace Saving.Applications.app_finance.ws_fin_controlcash_ctrl
{
    public partial class ws_fin_controlcash : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInitUser { get; set; }
        [JsPostBack]
        public string PostChangeTeller { get; set; }            
        [JsPostBack]
        public string PostPrintSlip { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }
        public void WebSheetLoadBegin()
        {            
            if (!IsPostBack)
            {
                LoadBegin(); 
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInitUser)
            {
                InitUser();
            }
            else if (eventArg == PostChangeTeller)
            {
                ChangeTeller();
            }           
            else if (eventArg == PostPrintSlip)
            {
                PrintSlip();
            }
        }        
        private void LoadBegin()
        {
            //ตรวจสอบเปิดวัน
            Session.Remove("MoneyData");
            HdShowDisplay.Value = "";
            Boolean lbl_fin = of_CheckUsePage();
            if (lbl_fin == false)
            {
                //don't show display 
                this.SetOnLoadedScript("document.getElementById('F_dsShow').style.display = 'none';");
                HdShowDisplay.Value = "fase";
                return;
            }
            else
            {
                //show display 
                this.SetOnLoadedScript("document.getElementById('F_dsShow').style.display = 'block';");
                HdShowDisplay.Value = "true";
                dsMain.ResetRow();
                HdItemtype.Value = "0";
                HdColumn.Value = "";
                HdCashdetail.Value = "";                                             
                this.SetOnLoadedScript("dsMain.Focus(0,'entry_id');");
                dsMain.RetrieveData(state.SsCoopId, state.SsWorkDate);
                dsMain.DATA[0].ITEM_TYPE = dsMain.DDStatus("");
                dsList.RetrieveList();
            }     
        }
        private Boolean of_CheckUsePage()
        {
            financeFunction.ResultClass classChk = new financeFunction.ResultClass();
            try
            {
                classChk = financeFunction.of_is_openday(state.SsCoopId, state.SsWorkDate);
                if (classChk.text != "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(classChk.text); return false;
                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบการเปิดวันได้"); return false;
            }
            return true;
        }
        public void SaveWebSheet()
        {
            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try {
                string sqlStr = "", ls_appcode = "", ls_application="";                
                string ls_tableuser = dsMain.DATA[0].ENTRY_ID.Trim();
                string ls_coopid = state.SsCoopId;
                sqlStr = @"select  count(*),tablefin_code as application,
                        (CASE upper(tablefin_code)
                          WHEN 'FIN' THEN 'การเงิน'
                          WHEN 'DEP' THEN 'เงินฝาก'
                          WHEN 'SHR' THEN 'หุ้นหนี้'
                          ELSE tablefin_code
                        END)as tablefin_code
                        from	amsecusers
                        where	user_name	= {1}
                        and		coop_control = {0}
                        group by tablefin_code";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_tableuser);
                Sdt dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    ls_appcode = dt.GetString("tablefin_code").Trim();
                    ls_application = dt.GetString("application").Trim();
                }
                else {
                    LtServerMessage.Text = WebUtil.ErrorMessage("User Name : " + ls_tableuser + " ไม่มีในระบบ กรุณาตรวจสอบ");
                    return;
                }
                string ls_remark = "", ls_item_desc = "";
                string ls_type_code = "PAY";
                decimal ld_cashout = 0, ld_cashin = 0, ldc_cashsum_amt = 0;
                //สถานะเพิ่ม ลดเงินจำนวนแบงค์
                int ln_cashflag = 0;
                //สถานะเพิ่ม ลดเงินจำนวนเงินลิ้นชักกลาง
                int li_typeflag = 0;
                int ll_status = 11;
                //เงินสดในลิ้นชัก
                decimal[] rerult = financeFunction.of_is_teller(ls_coopid, ls_tableuser, state.SsWorkDate);
                decimal ld_balance = rerult[2];
                decimal ll_seqno = rerult[3] + 1;

                decimal ld_itemtype = dsMain.DATA[0].ITEM_TYPE;               
                string ls_dtmstr_format	=  DateTime.Now.ToString("HH:mm:ss") + " : " + state.SsWorkDate.ToString("dd/MM/") + (state.SsWorkDate.Year + 543) ;
                decimal ld_amount = dsMain.DATA[0].OPERATE_AMT;
                //เงินสดสหกรณ์
                financeFunction.ResultClass classChk = new financeFunction.ResultClass();
                classChk = financeFunction.of_is_openday(ls_coopid, state.SsWorkDate);
                ldc_cashsum_amt = classChk.returnValue[1];
                ld_cashin = classChk.returnValue[4];
                ld_cashout = classChk.returnValue[5];
                
                if (ld_itemtype == 11)
                {
                    if (ld_itemtype == 11 && rerult[1] == 11) {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ลิ้นชักของ " + ls_tableuser + " มีสถานะเปิดอยู่แล้ว ไม่สามารถเปิดได้อีก");
                        return;
                    }                    
                    ls_remark = ls_tableuser + " เปิดลิ้นชัก : " + ls_dtmstr_format;
                    ld_balance = ld_balance + ld_amount;
		            ld_cashout			+= ld_amount;
		            ldc_cashsum_amt 	-= ld_amount;
		            li_typeflag			= -1;
                    ln_cashflag         = 1;
                    ls_item_desc        = "ส่ง" + ls_appcode;
                }
                else if (ld_itemtype == 15 || ld_itemtype == 16 || ld_itemtype == 14)
                {                    
                    if (rerult[0] == 1 && rerult[1] != 11)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่่สามารถทำรายการได้ยังไม่ได้เปิดลิ้นชักของ " + ls_tableuser);
                        return;
                    }
                    if(ld_itemtype == 15)
                    {
                        ls_remark	= ls_tableuser + " เบิกเพิ่ม : " + ls_dtmstr_format;
		                ld_balance  = ld_balance + ld_amount;
                        ld_cashout      += ld_amount;
                        ldc_cashsum_amt -= ld_amount;
                        li_typeflag     = -1;
                        ln_cashflag     = 1;
                        ls_item_desc    = "ส่ง" + ls_appcode;                          
                    }
                    else if(ld_itemtype == 16 ||ld_itemtype == 14)
                    {
                        if(ld_itemtype == 14){
                            ll_status = 14;
                            ls_remark = ls_tableuser + " ปิดลิ้นชัก : " + ls_dtmstr_format;
                        }else{
                            ls_remark   = ls_tableuser + " ส่งคืน : " + ls_dtmstr_format;
                        }                        
		                ld_balance  = ld_balance - ld_amount;
                        ls_type_code		= "RCV";
		                ld_cashin			+= ld_amount;
		                ldc_cashsum_amt 	+= ld_amount;
		                li_typeflag			= 1;
                        ln_cashflag         = -1;
                        ls_item_desc        = "รับคืน" + ls_appcode;
                    }                                   
                }
                string ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, ls_coopid, "FINCASHSLIPNO"); 
                
                #region FINCASHMASTER-DETAIL
                if (ld_itemtype == 11 && rerult[0] == 0)
                {
                    sqlStr = @" INSERT INTO FINTABLEUSERMASTER 
		                (	USER_NAME,	    APPLICATION,		OPDATEWORK,			STATUS,				AMOUNT_BALANCE,
			                laststm_no,		coop_id                   
		                )  
	                    VALUES
		                (	{0},		    {1},			    {2},			    {3},				{4},
			                {5},		    {6}   
		                ) ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_tableuser, ls_application, state.SsWorkDate, 11, ld_amount,
                         ll_seqno, ls_coopid);
                    exedinsert.SQL.Add(sqlStr);
                }
                else
                {
                    sqlStr = @"UPDATE FINTABLEUSERMASTER  SET
                    AMOUNT_BALANCE	= {3},  STATUS			= {4},   LASTSTM_NO		= {5}
                    WHERE  USER_NAME 	= {2}   AND	OPDATEWORK 	= {1}    and	COOP_ID= {0}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, state.SsWorkDate, ls_tableuser,
                            ld_balance, ll_status, ll_seqno);
                    exedinsert.SQL.Add(sqlStr);
                }
                sqlStr = @"INSERT INTO FINTABLEUSERDETAIL  
	            (	USER_NAME,		APPLICATION,				OPDATEWORK,				SEQNO,				OPDATE,
		            STATUS,			AMOUNT,						ITEMTYPE_CODE,			ITEMTYPE_DESC,		DOC_NO,
		            MEMBER_NO,		AMOUNT_BALANCE,				MONEYTYPE_CODE,			MONEYTYPE_DESC,		NAMEAPPORVE,
		            ref_status,		coop_id			 )  
                VALUES
	            (	{0},		    {1},					    {2},			        {3},			    {4},
		            {5},	        {6},				        '',		                {7},			    {11},
		            '',				{8},				        '',						'',					{9},   
		            {5},	        {10}			       
	            )";
                sqlStr = WebUtil.SQLFormat(sqlStr, ls_tableuser, ls_application, state.SsWorkDate, ll_seqno, DateTime.Now,
                     ld_itemtype, ld_amount, ls_remark, ld_balance, ls_tableuser, ls_coopid, ls_slipno);
                exedinsert.SQL.Add(sqlStr);
                                              
                #endregion

                
                decimal ld_lastseqno = classChk.returnValue[3] + 1;
//                CREATE TABLE dbo.FINRQCASHFLOWSTATEMENT (COOP_ID char(6) NOT NULL , OPERATE_DATE datetime NOT NULL , SEQ_NO numeric(5,0) NOT NULL , FINSLIP_NO char(13) NOT NULL , ITEM_AMT numeric(17,2) NULL , BAL_FORWARD numeric(17,2) NULL , BALANCE_AMT numeric(17,2) NULL , ENTRY_ID char(32) NULL , ENTRY_DATE datetime NULL , APPROVE_DATE datetime NULL , APPROVE_ID char(32) NULL , ITEM_FLAG numeric(1,0) NULL , ITEM_TYPECODE char(3) NULL , MACHINE_ID char(20) NULL , ITEM_DESC char(20) NULL , CONSTRAINT pk_finrqcashflowstatement PRIMARY KEY CLUSTERED (COOP_ID, OPERATE_DATE, SEQ_NO, FINSLIP_NO)) ;
//CREATE UNIQUE INDEX id_finrqcashflowstatement ON dbo.FINRQCASHFLOWSTATEMENT (COOP_ID , OPERATE_DATE , SEQ_NO , FINSLIP_NO ) ;
                sqlStr = @"INSERT INTO finrqcashflowstatement
	            (	OPERATE_DATE,				SEQ_NO,					FINSLIP_NO,
		            ITEM_AMT,					BAL_FORWARD,			BALANCE_AMT,
		            ENTRY_ID,					ENTRY_DATE,			    APPROVE_DATE,
		            APPROVE_ID,					ITEM_FLAG,				ITEM_TYPECODE,
		            MACHINE_ID,					COOP_ID,		        item_desc
	            )  
            VALUES
	            (	{0},			            {1},			        {2},
		            {3},					      0,			        {4},
		            {5},						{6},		            {6},
		            {5},						{7},				    {8},
		            {9},					    {10},			        {11}
	            )  ";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsWorkDate, ld_lastseqno, ls_slipno,
                     ld_amount, ldc_cashsum_amt,
                     ls_tableuser, state.SsWorkDate,
                     li_typeflag,ls_type_code,
                     state.SsClientIp, ls_coopid, ls_item_desc);
                exedinsert.SQL.Add(sqlStr);

                sqlStr = @"UPDATE	FINCASHFLOWMAS
                        SET		CASH_SUMAMT	        = {2},
				                CASH_IN				= {3},
				                CASH_OUT			= {4},
				                lastseq_no			= {5}
                        WHERE	OPERATE_DATE	    = {1}  and
				                     COOP_ID		= {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, state.SsWorkDate, ldc_cashsum_amt ,
                     ld_cashin, ld_cashout, ld_lastseqno);
                exedinsert.SQL.Add(sqlStr);
                                                
                exedinsert.Execute();
                exedinsert.SQL.Clear();
                HdPrint.Value = "true";
                HdSlipno.Value= ls_slipno;

                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, ls_coopid);
                args.Add("as_slipno", iReportArgumentType.String, ls_slipno);
                iReportBuider report = new iReportBuider(this, "");
                report.AddCriteria("ir_fin_receipt_cashdetail", "ใบการเบิก-จ่าย เงินสด", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();


                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                LoadBegin();
            }
            catch (Exception err)
            {
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + err.Message);
            }
        }
        
        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
        private void ChangeTeller()
        {
            decimal ItemType = dsMain.DATA[0].ITEM_TYPE;
            decimal status   = dsMain.DATA[0].STATUS;
            decimal cashAmt = 0, moneyRemain = 0, amount_amt = 0;
            string columnName = "";
            string ls_coopid = state.SsCoopId;
            //retrive date new (ItemChanged)           
            string sql = @"SELECT	FINCASHFLOWMAS.CASH_SUMAMT AS CASH_AMT,
                    FINTABLEUSERMASTER.AMOUNT_BALANCE as MONEY_REMAIN
                    FROM	FINTABLEUSERMASTER,FINCASHFLOWMAS
                    WHERE	
                    ( FINTABLEUSERMASTER.COOP_ID = FINCASHFLOWMAS.COOP_ID ) AND 
                    ( FINTABLEUSERMASTER.OPDATEWORK = FINCASHFLOWMAS.OPERATE_DATE ) AND
                    ( FINTABLEUSERMASTER.USER_NAME		= {2} ) AND
                    ( FINTABLEUSERMASTER.OPDATEWORK	    = {1} ) 	and		            
                    ( FINTABLEUSERMASTER.coop_id		= {0} )";
            sql = WebUtil.SQLFormat(sql, ls_coopid, state.SsWorkDate, dsMain.DATA[0].ENTRY_ID);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                if (HdItemtype.Value == "1")
                {
                    dsMain.DATA[0].CASH_AMT = dt.GetDecimal("CASH_AMT");
                    dsMain.DATA[0].MONEY_REMAIN = dt.GetDecimal("MONEY_REMAIN");
                    dsMain.DATA[0].OPERATE_AMT = 0;
                }
                else if (HdItemtype.Value == "2") {
                    dsMain.DATA[0].MONEY_REMAIN = dt.GetDecimal("MONEY_REMAIN");
                }
            }
           
            //ดึงค่ามาใหม่
            financeFunction.ResultClass classChk = new financeFunction.ResultClass();
            classChk = financeFunction.of_is_openday(ls_coopid, state.SsWorkDate);
            cashAmt = classChk.returnValue[1];
            moneyRemain = dsMain.DATA[0].MONEY_REMAIN;
            amount_amt = dsMain.DATA[0].OPERATE_AMT;  
            if (ItemType != status)
            {              
                columnName  = HdColumn.Value;                       
                try
                {
                    if (ItemType == 14) //14 ปิดลิ้นชัก
                    {
                        if (moneyRemain >= 0)
                        {
                            cashAmt = cashAmt + moneyRemain;
                            dsMain.DATA[0].MONEY_REMAIN = 0;
                            dsMain.DATA[0].CASH_AMT = cashAmt;
                            dsMain.DATA[0].OPERATE_AMT = moneyRemain;
                            dsMain.DATA[0].T_MONEYRETURN ="ยอดเงินที่ต้องส่งคืนทั้งหมด =" + moneyRemain.ToString("#,##0.00") ;
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินสดคงเหลือติดลบ");
                            dsMain.DATA[0].ITEM_TYPE = 15;
                            HdItemtype.Value = "0";
                        }
                    }
                    else if (ItemType == 11 && columnName == "operate_amt")//11 เปิดลิ้นชัก
                    {
                        cashAmt = cashAmt - amount_amt;
                        dsMain.DATA[0].CASH_AMT = cashAmt;
                        dsMain.DATA[0].OPERATE_AMT = amount_amt;
                        dsMain.DATA[0].MONEY_REMAIN = amount_amt;
                        dsMain.DATA[0].T_MONEYRETURN = "";
                    }
                    else if (ItemType == 15)//15  เบิกเงินเพิ่ม
                    {
                        if (status == 11 && columnName == "operate_amt")
                        {
                            if (amount_amt <= cashAmt)
                            {
                                cashAmt = cashAmt - amount_amt;
                                moneyRemain = moneyRemain + amount_amt;
                                dsMain.DATA[0].CASH_AMT = cashAmt;
                                dsMain.DATA[0].OPERATE_AMT = amount_amt;
                                dsMain.DATA[0].MONEY_REMAIN = moneyRemain;
                                dsMain.DATA[0].T_MONEYRETURN = ""; 
                            }
                            else if (amount_amt > cashAmt)
                            {
                                dsMain.DATA[0].OPERATE_AMT = 0;
                                LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินที่เบิกมากกว่าจำนวนเงินคงเหลือ");
                            }
                        }
                        else if (status == 11 && columnName == "item_type")
                        {
                            dsMain.DATA[0].OPERATE_AMT = 0;
                            dsMain.DATA[0].CASH_AMT = cashAmt;
                            dsMain.DATA[0].MONEY_REMAIN = moneyRemain;
                        }
                        else
                        {
                            dsMain.DATA[0].ITEM_TYPE = 11;
                            HdItemtype.Value = "0";
                            LtServerMessage.Text = WebUtil.ErrorMessage("ต้องมีสถานะเปิดลิ้นชักเท่านั้น");
                        }
                    }
                    else if (ItemType == 16)//16 ส่งเงินคืน
                    {
                        if (status == 11 && columnName == "operate_amt")
                        {
                            if (amount_amt <= moneyRemain)
                            {
                                cashAmt = cashAmt + amount_amt;
                                moneyRemain = moneyRemain - amount_amt;
                                dsMain.DATA[0].CASH_AMT = cashAmt;
                                dsMain.DATA[0].OPERATE_AMT = amount_amt;
                                dsMain.DATA[0].MONEY_REMAIN = moneyRemain;
                                dsMain.DATA[0].T_MONEYRETURN = ""; 
                            }
                            else if (amount_amt > moneyRemain)
                            {
                                dsMain.DATA[0].OPERATE_AMT = 0;
                                LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินที่คืนมากกว่าจำนวนเงินคงเหลือ");
                            }
                        }
                        else if (status == 11 && columnName == "item_type")
                        {
                            dsMain.DATA[0].OPERATE_AMT = 0;
                            dsMain.DATA[0].CASH_AMT = cashAmt;
                            dsMain.DATA[0].MONEY_REMAIN = moneyRemain;
                        }
                        else
                        {
                            dsMain.DATA[0].ITEM_TYPE = 11;
                            HdItemtype.Value = "0";
                            LtServerMessage.Text = WebUtil.ErrorMessage("ต้องมีสถานะเปิดลิ้นชักเท่านั้น");
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }
        private void InitUser()
        {
            string ls_sqlstatus = "";
            string ls_tableuser = dsMain.DATA[0].ENTRY_ID;
            int result = dsMain.DDUsername(state.SsCoopControl,ls_tableuser);
            if (result == 1)
            {
                DateTime work_date = dsMain.DATA[0].OPERATE_DATE;
                string ls_coopid = state.SsCoopId;
                decimal[] ld_teller = financeFunction.of_is_teller(ls_coopid, ls_tableuser, work_date);                
                if (ld_teller[0] == 1)
                {
                    dsMain.RetriveUser(ls_coopid, ls_tableuser, work_date);
                    if (dsMain.DATA[0].STATUS == 11) {
                        dsMain.DATA[0].T_STATUS = "เปิดลิ้นชัก";
                        dsMain.DATA[0].ITEM_TYPE = 15;
                        if (dsMain.DATA[0].MONEY_REMAIN > 0)
                        {
                            ls_sqlstatus = "AND FINUCFSTATUS.STATUS NOT IN('11')";
                        }
                        else
                        {
                            ls_sqlstatus = "AND FINUCFSTATUS.STATUS NOT IN('11','16')";
                        }
                    }
                    else if (dsMain.DATA[0].STATUS == 14)
                    {
                        dsMain.DATA[0].T_STATUS = "ปิดลิ้นชัก";
                        dsMain.DATA[0].ITEM_TYPE = 11;
                        ls_sqlstatus = "AND FINUCFSTATUS.STATUS NOT IN('14','15','16')";
                    }
                    else
                    {
                        dsMain.DATA[0].ITEM_TYPE = 11;
                        ls_sqlstatus = "AND FINUCFSTATUS.STATUS NOT IN('14')";
                    }                    
                }
                else {
                    //ยอดเงินสดคงเหลือ
                    financeFunction.ResultClass classChk = new financeFunction.ResultClass();
                    classChk = financeFunction.of_is_openday(ls_coopid, state.SsWorkDate);
                    dsMain.DATA[0].CASH_AMT = classChk.returnValue[1];                    
                    dsMain.DATA[0].ITEM_TYPE = 11;
                    dsMain.DATA[0].MONEY_REMAIN = 0;
                    dsMain.DATA[0].OPERATE_AMT = 0;
                    dsMain.DATA[0].T_STATUS = "ปิดลิ้นชัก";
                    dsMain.DATA[0].T_MONEYRETURN = "";
                    string sql = @" SELECT  AMSECUSERS.USER_NAME,AMSECUSERS.FULL_NAME                         
                        FROM AMSECUSERS  
                       WHERE ( AMSECUSERS.USER_NAME = {1} ) AND  
                             ( AMSECUSERS.COOP_CONTROL = {0} ) ";
                    sql = WebUtil.SQLFormat(sql, ls_coopid, ls_tableuser);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMain.DATA[0].FULL_NAME = dt.GetString("FULL_NAME").ToString(); ;
                    }
                    ls_sqlstatus = "AND FINUCFSTATUS.STATUS NOT IN('14','15','16')";
                }
                HdOperateAmt.Value = dsMain.DATA[0].OPERATE_AMT.ToString();
                dsMain.DDStatus(ls_sqlstatus);
            }
            else { 
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลผู้ร้องขอ กรุณาตรวจสอบข้อมูล"); 
            }
        }
        private void PrintSlip() {            
            try
            {

                String ReportName = "ir_fin_cashdetail_main";
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                args.Add("as_slipno", iReportArgumentType.String, HdSlipno.Value);
                iReportBuider report = new iReportBuider(this, "กำลังสร้างใบเบิก-จ่าย เงินสด");
                report.AddCriteria(ReportName, "ใบเบิก-จ่าย เงินสด", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างใบเบิก-จ่าย เงินสด");
            }
            
        }
    }
}