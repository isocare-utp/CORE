--------------------------------------------------------
--  File created - Monday-July-18-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package DEPT_MGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "DEPT_MGMT" AS
        Procedure GetInitVal(DeptType varchar2, IntMeth out number, MinBal out number, CalcIntRound out number);
        Procedure GetInitVal(CalcIntMeth out number, IntRoundMeth out number, IntRoundType out number, RoundIntPos out number, CountDateType out number, ChqIntMeth out number, AccUIntPos out number);
        Function RoundMoney (Money number, t integer) return number;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number ;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, IntMeth number) return number ;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, SpcIntSts number, SpcIntRate number, ChqPend number) return number ;
        Function CalcIntDays (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number ;
        Function CalcIntMonths (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number;
        Function CalcIntFix (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number ;
        Function CalcIntDivd (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number;
        Procedure UpdateDeptMaster(CoopID varchar2, DeptAccNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, retVal OUT INTEGER );
        Procedure UpdateDeptWithDrawAndAddSlip(CoopID varchar2, DeptAccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, CashType varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER);
        function GetSlipNo(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number) return varchar2;
        Procedure UpdateDeptSlip(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, PrncBal1 number, StmtDate date, StmtRefNo varchar2, CalcIntFrom date, CalcIntTo date, IntAmt number, AccIntAmt number, retVal OUT INTEGER);
        Procedure Process_UpdateSlipSts(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, Sts integer, retVal OUT INTEGER);
END;

/
