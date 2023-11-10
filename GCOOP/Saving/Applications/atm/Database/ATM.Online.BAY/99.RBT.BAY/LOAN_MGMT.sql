--------------------------------------------------------
--  File created - Monday-July-18-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package LOAN_MGMT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "LOAN_MGMT" AS

        Procedure GetInitVal(CountDateType out number, IntRoundNum out number, IntRoundSumType out number, IntRoundFormat out number, IntRoundKpType out number,

			GBfSts out number, CountDateFstType out number, IntRoundType out number, FixPayCalcType out number, IncludeIntArrSts out number, KpItemArrD out number);

        Procedure GetInitVal(LoanType varchar2, LoanPayType out number, PayRoundFactor out number, PayRoundType out number);

        Function CalcContrcPerdPay(LoanType varchar2, LoanPayType varchar2,adc_money number,adtm_operate date, IntRoundType number, PayRoundType number, FixPayCalcType number, PayRoundFactor number, PrnBal in out number, PerdPay in out number, LastPerdPay in out number) return number;

        Function GetDayofYear (d date ) return integer;

        Function RoundMoney (Money number, t number) return number ;

        Function CalcInt (LoanType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, CountDateType varchar2, IntRoundNum number, IntRoundSumType number, IntRoundFormat number, IntRoundKpType number) return number;

        Function CalcInt (LoanType varchar2, BfPrnBal number, PrnBal in out number, CalcIntFrom in out date, CalcIntTo in out date, StmtDate date, LastStmtNo number, PerdPay in out number, Rkeep number, RkeepInt number, IntArrAmt in out number, LastCalcIntDate in out date, StartContDate in out date, LastProcDate in out date, LastPerdPay in out number, PerdPayAll in out number, RegenRkeeing out boolean) return number ;

        Procedure UpdateLoanMaster(CoopID varchar2, ContNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, retVal OUT INTEGER );

        Procedure UpdateLoanWithDraw(CoopID varchar2, ContNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER );

        Procedure UpdateLoanWithDrawAndAddSlip(CoopID varchar2, ContNo varchar2, MemNo varchar2, BankCd varchar2, BrandCd varchar2, AccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER );

--        Procedure ReconcileLoan(CoopID varchar2, ContNo varchar2, MemNo varchar2, BankCd varchar2, BrandCd varchar2, AccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER );

        function GetSlipNo(CoopID varchar2, MemNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number) return varchar2;

        Procedure Process_UpdateSlipSts(CoopID varchar2, MemNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, Sts integer, retVal OUT INTEGER);

END;

/
