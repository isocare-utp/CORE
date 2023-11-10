--------------------------------------------------------
--  File created - Thursday-August-04-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package PKG_ATMMGMT2
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "PKG_ATMMGMT2" AS
        C_COOPID CONSTANT CHAR(4) := '2500'; -- ??????????
        CONTRC_CHG CONSTANT CHAR(1) := 'C'; -- ????????????
        CONTRC_NEW CONSTANT CHAR(1) := 'Y'; -- ?????????
        POSTED CONSTANT CHAR(1) := 'Y'; -- Post ????
        TRANS1 CONSTANT CHAR(3) := '002'; --
        TRANS2 CONSTANT CHAR(3) := '012'; --
        TRANS3 CONSTANT CHAR(3) := '003'; --
        TRANS4 CONSTANT CHAR(3) := '013'; --
        C_BranchId CONSTANT CHAR(3) := '001'; --
        C_CASHTYPE CONSTANT CHAR(3) := 'CBT'; --
        C_TOFROMACCID CONSTANT CHAR(8) := '2111000'; --
        C_ENTRYID CONSTANT VARCHAR2(15) := 'ATMKTB'; --
        C_LOANTYPE CONSTANT CHAR(2) := '88'; -- Loan Type
        C_DEPTTYPE CONSTANT CHAR(2) := '88'; -- Dept Type
        KEEPING_ENABLE CONSTANT boolean := False; --
        REGEN_PERIOD_PAYMENT_ENABLE CONSTANT boolean := False; --
        /*
        LoanItemType1 CONSTANT varchar2(3) := 'LRC'; --
        SlipTypeCode1 CONSTANT varchar2(3) := 'LWD'; --
        StmItemType1  CONSTANT varchar2(3) := 'LRC'; --
        
        LoanItemType2 CONSTANT varchar2(3) := 'LPX'; --
        SlipTypeCode2 CONSTANT varchar2(3) := 'PX'; --
        StmItemType2  CONSTANT varchar2(3) := 'LPX'; --
        
        LoanItemType1Cancel CONSTANT varchar2(3) := 'RRC'; --
        LoanItemType2Cancel CONSTANT varchar2(3) := 'RPX'; --
        CMSLIPNO CONSTANT VARCHAR2(10)    := 'CMSLIPNO'; --
        LNRECEIVENO CONSTANT VARCHAR2(10) := 'LNRECEIVENO'; --
        
        DeptItemType1 CONSTANT varchar2(3) := 'WTE'; --
        DeptItemType2 CONSTANT varchar2(3) := 'DTE'; --
        DeptItemType1Cancel CONSTANT varchar2(3) := 'AJI'; --
        DeptItemType2Cance2 CONSTANT varchar2(3) := 'AJO'; --
        DPSLIPNO CONSTANT VARCHAR2(10)     := 'DPSLIPNO'; --
        */
        Function x(ContNo varchar2) return varchar2;
        Function getToFromAccID(CoopID varchar2) return varchar2;
        Function IsLinkValid(SLink varchar2) return boolean;
        Procedure Process_Move2Success;
        Procedure Process_SetTransPosted(aSeqNo number, retVal OUT INTEGER);
------------------------------------------------- Member Process
        Function IsNewMember(CoopID varchar2, memno varchar2) return boolean;
        Procedure Process_AddMember(coopid varchar2, memno varchar2, memnm varchar2, memsurnm varchar2, personid varchar2, retVal OUT INTEGER );

------------------------------------------------- Loan Process
        Function GetContract(CoopID varchar2, MemNo varchar2) return varchar2;
        Function GetLoanContractSTS(CoopID varchar2, MemNo varchar2, ContNo varchar2) return char;
        Procedure Process_AddLoanContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecAmt number, PayAmt number, AccNo varchar2, IsHold number, retVal OUT INTEGER);
        Procedure Process_UpdateLoanContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER);
        Function IsHaveLoanAcc(CoopID varchar2, MemNo varchar2, AccNo varchar2) return boolean;
        Function IsLoanAccCHG(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2) return boolean;
        Procedure Process_HoldLoanAccNo(CoopID varchar2, MemNo varchar2, retVal OUT INTEGER);
        Procedure Process_UpdateLoanAccNo(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2, retVal OUT INTEGER);
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecvAmt number,PayAmt number, retVal OUT INTEGER);
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, retVal OUT INTEGER);
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, OperateCD varchar2, PayAmt number);
        Procedure Process_UpdateLoanWithDraw(CoopID varchar2, retVal out integer);
------------------------------------------------- Dept Process
        Procedure Process_UpdateDeptAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecvAmt number,PayAmt number, retVal OUT INTEGER);
        Procedure Process_UpdateDeptAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, retVal OUT INTEGER) ;
        Procedure Process_UpdateDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER);
        Procedure Process_AddDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecAmt number, PayAmt number, AccNo varchar2, IsHold number, retVal OUT INTEGER);
        Procedure Process_DelDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER) ;
        Procedure Process_UpdateDeptAccNo(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2, retVal OUT INTEGER);
        Procedure Process_HoldDeptAccNo(CoopID varchar2, MemNo varchar2, retVal OUT INTEGER);
        Function IsDeptAccCHG(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2) return boolean;
        Function GetDeptContractSTS(CoopID varchar2, MemNo varchar2, ContNo varchar2) return char;
        Function IsHaveDeptAcc(CoopID varchar2, MemNo varchar2, AccNo varchar2) return boolean;
        Procedure Process_UpdateDeptWithDraw(CoopID varchar2, retVal out integer);
        Function GetDepttype(CoopID varchar2, MemNo varchar2,AccNo varchar2) return varchar2;
        Function GetDept(CoopID varchar2, MemNo varchar2,AccNo varchar2) return varchar2;
        Procedure Process_Loan(CoopID varchar2);
        Procedure Process_Dept(CoopID varchar2);
        Procedure ReconcileLoan(CoopID varchar2);
        Procedure ReconcileDept(CoopID varchar2);
        Procedure Post2ATMTrans;
END;

/
