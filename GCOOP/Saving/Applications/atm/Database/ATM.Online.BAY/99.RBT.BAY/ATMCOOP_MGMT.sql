--------------------------------------------------------
--  File created - Monday-June-27-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package ATMCOOP_MGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "ATMCOOP_MGMT" AS
  procedure GetCreditAmt
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2);
   procedure WithDraw
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ItemAmt in out number, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2);
   procedure Payment
   (CoopId in  varchar2, SystemCd varchar2, TransCd varchar2, OperateDate date, SavingAcc varchar2,
	RefNO varchar2, BankCd varchar2, BranchCd varchar2, ItemAmt in out number, ContractNo out varchar2, CreditAmt out number, BalanceAmt out number, ResponseCd out varchar2);
END;

/
