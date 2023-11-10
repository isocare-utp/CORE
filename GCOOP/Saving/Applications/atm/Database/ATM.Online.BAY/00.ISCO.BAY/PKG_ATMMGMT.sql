--------------------------------------------------------
--  File created - Thursday-August-04-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package PKG_ATMMGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "PKG_ATMMGMT" AS


        C_COOPID CONSTANT CHAR(4) := '2500'; -- ??????????
        CONTRC_CHG CONSTANT CHAR(1) := 'C'; -- ????????????
        CONTRC_NEW CONSTANT CHAR(1) := 'Y'; -- ?????????
        POSTED CONSTANT CHAR(1) := 'Y'; -- Post ????
        LOANTYPE CONSTANT CHAR(2) := '88'; -- Loan Type
        DEPTTYPE CONSTANT CHAR(2) := '88'; -- Dept Type
        SLIP_ENABLE CONSTANT boolean := True; --
        RECV_ENABLE CONSTANT boolean := True; --
        KEEPING_ENABLE CONSTANT boolean := False; --


--        Procedure UpdateLoanMaster(CoopID varchar2, ContNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, retVal OUT INTEGER );
        Function x(ContNo varchar2) return varchar2;
        Function IsLinkValid(SLink varchar2) return boolean;
        Procedure GenDocNoNew(Doccd varchar2, DocPF IN varchar2 Default '', LastDocNo OUT varchar2, retVal OUT INTEGER);
        Procedure Process_Move2Success;
        Procedure Process_SetTransPosted(aSeqNo number, retVal OUT INTEGER);
        Function IsUseLoanRec return boolean;
        Function IsUseSlip return boolean;
        Procedure getDeptCoop_ID(CoopAcc IN varchar2 Default '',Coop_ID OUT VARCHAR2 );
        Procedure getDeptType(CoopAcc IN varchar2 Default '',DeptType OUT VARCHAR2 );
        Procedure getLoanCoop_ID(ContNo IN varchar2 Default '',Coop_ID OUT VARCHAR2 );
        Procedure getLoanType(ContNo IN varchar2 Default '',LoanType OUT VARCHAR2 );

-- Loan Process
                -- ???????????
        Procedure GetMember(CoopID varchar2, MemNo varchar2, MemNm OUT varchar2, MemSurNm OUT varchar2, PersonID OUT varchar2, retVal OUT INTEGER ) ;

	-- ??????????????????????????????
        Procedure Process_AddLoanSlip(CoopID varchar2,ContNo varchar2,MemNo varchar2, SlipNo varchar2,SlipTypeCode varchar2, BranchId varchar2, RefNo varchar2,
	SlipDate date, OperateDate date, ExpBank varchar2, ExpBranch varchar2, ExpAccID varchar2, ToFromAccId varchar2,
	SlipAmt number, EntryId varchar2, EntryDate date, retVal OUT INTEGER ) ;
        Function GetAccNo(CoopID varchar2, ContNo varchar2, MemNo varchar2) return varchar2;
        Function IsHaveLoanStmt(CoopID varchar2, ContNo varchar2, BranchId varchar2, StmtDate Date) return boolean;
        Function GetLoanMasterDt(CoopID varchar2, ContNo varchar2, PrnBal out number, WithDraw out number)  return integer;
        Function GetLoanMasterDt(CoopID varchar2, ContNo varchar2, LoanType out varchar2, LastStmtNo out number, PrnBal out number, WithDraw out number, ContSts out number, IntArr out number, LastPerdPay out number, PerdPayAll out number, ContIntType out number, RkeepPrn out number, RkeepInt out number, NkeepInt out number, LastProcDate out date, LastCalcIntDate out date, StartContDate out date, PaySts out number, PerdPayAmt out number, StartKeepDate out date)  return integer;
        Procedure Process_GetKPTempRecvDt(CoopID varchar2, ContNo varchar2, RecvPerd out varchar2, KpTot out number, KpPrnc out number, KpInt out number, retVal out integer);
        Procedure Process_UpdateKPTEMPRECEIVEDET(CoopID varchar2, ContNo varchar2, RecPerd varchar2, ReGenPrn number, ReKpGenPrn number, RegenInt number,
			CalcIntFrom date, CalcIntTo date, LastCalcInt date, retVal OUT INTEGER);
        Procedure Process_UpdateLNCONTMASTER(CoopID varchar2, ContNo varchar2, WithDrawAmt number, PrnBal number, retVal OUT INTEGER);
        Procedure Process_UpdateLNCONTMASTER(CoopID varchar2, ContNo varchar2, PrnBal number, PerdPay number, WithDrawAmt number, IntArr number, StartContDate date, StartKeepDate date, LastPerdPay number, LastStmtNo number, LastCalcIntDate date, LastRecDate date, LastProcDate date, LastAccDate date, NKeepPrn number default null, NKeepInt number default null, RKeepPrn number default null, RKeepInt number default null, retVal OUT INTEGER);
        Procedure Process_AddLoanSTMT(CoopID varchar2, ContNo varchar2, SeqNo number, BranchId varchar2, LoanItemType varchar2, StmtDate date,
	Refno varchar2, Perd number, PrnPay number, PrnBal number, CalcIntFrom date, CalcIntTo date, IntPerd number,
	IntArr number, MoneyType varchar2, ItemSts number, EntryID varchar2, EntryDate date, CoopBranch varchar2, LoanRecNo varchar2, retVal OUT INTEGER);
        Procedure CancelLoanSTMT(CoopID varchar2, ContNo varchar2, SeqNo number, BranchId varchar2, LoanItemType varchar2, StmtDate date,
	Refno varchar2, Perd number, PrnPay number, PrnBal number, CalcIntFrom IN OUT date, CalcIntTo IN OUT date, IntPerd IN OUT number,
	IntArr IN OUT number, MoneyType varchar2, ItemSts number, EntryID varchar2, EntryDate date, CoopBranch varchar2, LoanRecNo out varchar2, retVal OUT INTEGER );
        Procedure CancelLoanRec(CoopID varchar2,LoanItemType varchar2, Refno varchar2, ReconcileDate Date, retVal OUT INTEGER );
        Procedure Process_AddShrLonSlip(CoopID varchar2, SlipNo varchar2,StmItemType varchar2, BranchId varchar2, Seqno number, ShrLonTypeCD varchar2,
	ContNo varchar2, PerdCountSts number, Perd number, PrinPay number, CalIntFrom date, CalIntTo date, BfPerd number,
	BfLastCalIntDate date, BfWithDrawAmt number, BfBalAmt number, BfContSts number, ContIntType number, RkeepPrin number,
	RkeepInt number, NkeepInt number, LnBranchId varchar2, retVal OUT INTEGER ) ;
        Procedure Process_AddLoanRec(CoopID varchar2, SlipNo varchar2,StmtRefno varchar2, BranchID varchar2, MemNo varchar2, ContNo varchar2,
	LoanType varchar2, BfPerd number, BfPrinAmt number, BfLastCalIntDate date, BfLastRecDate date, BfLastProcDate date,
	SharStkVal number, IntAcc number, LoanRecDate date, LoanRecAmt number, RecPerdFlaf number, ExpBank varchar2,
	ExpBranch varchar2, ExpAccID varchar2, ContIntType number, EntryId varchar2, EntryDate date, ToFromAccId varchar2, retVal OUT INTEGER ) ;

-- Dept Process
	-- ??????????????????????????????
        Function GetDeptMasterDt(CoopID varchar2, DeptNo varchar2, PrnBal out number, WithDraw out number, WithAmt out number, DeptAmt out number, DeptCloseSts out number, BranchId out varchar2) return number;
        Function GetDeptMasterDt(CoopID varchar2, DeptNo varchar2, DeptType out varchar2, LastStmt out number, PrnBal out number,
	WithDraw out number, DeptCloseSts out number, SequsetSts out number, LastCalcIntDate out date, AccIntAmt out number,
	IntArrearAmt out number, SpcIntRate out number, SpcIntRateSts out number, ChqPendAmt out number, DeptAmt out number,
	WithAmt out number, BranchId out varchar2) return number;
        Procedure Process_UpdateDeptMaster(CoopID varchar2, DeptNo varchar2, BranchId varchar2, Withraw number, PrncBal1 number, WithAmt number, DeptAmt number, retVal OUT INTEGER);
        Procedure Process_UpdateDeptMaster(CoopID varchar2, DeptNo varchar2, BranchId varchar2, PrncBal1  number,
	Withraw number, AccuInt number, LastCalntDate date, LastAccDate date, LastStmSeqNo number, DoperateBal number,
	LastMoveDate date, DeptAmt number, WithAmt number, retVal OUT INTEGER);
        Procedure Process_AddDeptSlip(CoopID varchar2, SlipNo varchar2, DeptAccNo varchar2, DeptType varchar2, SlipDate Date,
	RecPayType varchar2, SlipAmt number, PrnBal number, WithDraw number, EntryId varchar2, EntryDate date,
	MachID varchar2, DeptItemType varchar2, DpStmNo varchar2, CalIntFrom date, CalIntTo date, DeptGrp varchar2,
	ToFromAccId varchar2, RefApp varchar2, RefSlipNo varchar2, CashType varchar2, IntAmt number, AccIntAmt number,
	BranchId varchar2, BranchOrg varchar2, retVal OUT INTEGER ) ;
        Function IsHaveDeptStmt(CoopID varchar2, DeptAccNo varchar2, DeptType varchar2, StmtDate Date) return boolean;
        Procedure Process_AddDeptSTMT(CoopID varchar2, SeqNo number, DeptAccNo varchar2, DeptType varchar2, SlipDate Date,
	RefNo varchar2, SlipAmt number, PrnBal number, PrnBalTemp number, PrncNo number, IntAmt number, AccIntAmt number,
	EntryId varchar2, EntryDate date, CalIntFrom date, CalIntTo date, CashType varchar2, MachID varchar2, IsChqPending number,
	BranchID varchar2, retVal OUT INTEGER);
        Procedure Process_PostLoanAmtLimit2ATM( CoopID varchar2, retVal OUT INTEGER);
        Procedure Process_PostDeptAmtLimit2ATM( CoopID varchar2, retVal OUT INTEGER);
END;

/
