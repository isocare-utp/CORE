--------------------------------------------------------
--  File created - Sunday-July-17-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package Body ATMCOOP_MGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "ATMCOOP_MGMT" AS
  procedure GetCreditAmt
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2)
     is
		CoopSavingAcc varchar2(15);
		MemNo			varchar2(8);
     begin
		ResponseCd := '0011';
		if SystemCd = '01' then
				SELECT ( ATMLOAN.CREDIT_AMT- ATMLOAN.RECEIVE_AMT +(COOP.PXCREDIT*ATMLOAN.PAY_AMT )) AS CURCREDIT_AMT,
					contract_no, ATMLOAN.BALANCE_AMT, COOP.SAVING_ACC, ATMLOAN.member_no
			INTO	CreditAmt, ContractNo, BalanceAmt, CoopSavingAcc, MemNo
			FROM ATMLOAN,COOP
			WHERE ATMLOAN.COOP_ID=CoopId AND
			trim(ATMLOAN.SAVING_ACC)=SavingAcc AND
			ATMLOAN.LOANHOLD=0 AND
			ATMLOAN.COOP_ID = COOP.COOP_ID AND
			COOP.COOPHOLD=0 AND
			(SELECT COUNT(T.SAVING_ACC) FROM ATMLOAN T WHERE  T.SAVING_ACC=ATMLOAN.SAVING_ACC) =1;

			INSERT INTO ATMACT
			( CCS_OPERATE_DATE, ATM_NO, ATM_SEQNO, OPERATE_DATE, COOP_ID, COOP_SAVING_ACC, CCS_COOP_SAVING_ACC,
			MEMBER_NO, SAVING_ACC, SYSTEM_CODE, OPERATE_CODE,TRANSACTION_AMT, BANK_CODE, BRANCH_CODE, CREDIT_AMT, BALANCE_AMT,
			RESPONSE_CODE, REQUEST, RESPONSE) VALUES
			(sysdate, substr(RefNO, 1, 12), substr(RefNO, 13, 8), OperateDate, CoopId, CoopSavingAcc, CoopSavingAcc,
			MemNo, SavingAcc, SystemCd, '001', 0, BankCd, BranchCd, CreditAmt, BalanceAmt,
			TransCd, 'X', 'X');
			ResponseCd := '0000';
		end if;
	exception
 	  when no_data_found then
		null;
     end;
   procedure WithDraw
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ItemAmt in out number, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2)
     is
		CoopSavingAcc varchar2(15);
		MemNo			varchar2(8);
     begin
		ResponseCd := '0011';
		if SystemCd = '01' then
				SELECT  nvl(ATMLOAN.CREDIT_AMT, 0)- nvl(ATMLOAN.RECEIVE_AMT, 0) + (COOP.PXCREDIT*ATMLOAN.PAY_AMT ) AS CURCREDIT_AMT,
					contract_no, nvl(ATMLOAN.BALANCE_AMT, 0), COOP.SAVING_ACC, ATMLOAN.member_no
			INTO	CreditAmt, ContractNo, BalanceAmt, CoopSavingAcc, MemNo
			FROM ATMLOAN,COOP
			WHERE ATMLOAN.COOP_ID=CoopId AND
			trim(ATMLOAN.SAVING_ACC)=SavingAcc AND
			ATMLOAN.LOANHOLD=0 AND
			ATMLOAN.COOP_ID = COOP.COOP_ID AND
			COOP.COOPHOLD=0 AND
			(SELECT COUNT(T.SAVING_ACC) FROM ATMLOAN T WHERE  T.SAVING_ACC=ATMLOAN.SAVING_ACC) =1;
			CreditAmt := CreditAmt - ItemAmt;
			BalanceAmt := BalanceAmt + ItemAmt;
			INSERT INTO ATMACT
			( CCS_OPERATE_DATE, ATM_NO, ATM_SEQNO, OPERATE_DATE, COOP_ID, COOP_SAVING_ACC, CCS_COOP_SAVING_ACC,
			MEMBER_NO, SAVING_ACC, SYSTEM_CODE, OPERATE_CODE,TRANSACTION_AMT, BANK_CODE, BRANCH_CODE, CREDIT_AMT, BALANCE_AMT,
			RESPONSE_CODE, REQUEST, RESPONSE) VALUES
			(sysdate, substr(RefNO, 1, 12), substr(RefNO, 13, 8), OperateDate, CoopId, CoopSavingAcc, CoopSavingAcc,
			MemNo, SavingAcc, SystemCd, '002', ItemAmt, BankCd, BranchCd, CreditAmt, BalanceAmt,
			TransCd, 'X', 'X');
			ResponseCd := '0000';
		end if;
	exception
 	  when no_data_found then
		null;
     end;
   procedure Payment
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ItemAmt in out number, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2)
     is
     begin
        null;
     end;
END;

/
