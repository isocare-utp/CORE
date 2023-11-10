using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DataSet1TableAdapters;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class ws_sl_cfloantype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostLoanTypeCode { get; set; }
        [JsPostBack]
        public String PostInsertRowRightcollmast { get; set; }
        [JsPostBack]
        public String PostDelRowRightcollmast { get; set; }
        [JsPostBack]
        public String PostInsertRowRightcustom { get; set; }
        [JsPostBack]
        public String PostDelRowRightcustom { get; set; }
        [JsPostBack]
        public String PostInsertRowIntspc { get; set; }
        [JsPostBack]
        public String PostDelRowIntspc { get; set; }
        [JsPostBack]
        public String PostInsertRowCollreqgrt { get; set; }
        [JsPostBack]
        public String PostDelRowCollreqgrt { get; set; }
        [JsPostBack]
        public String PostInsertRowCollcanuse { get; set; }
        [JsPostBack]
        public String PostDelRowCollcanuse { get; set; }
        [JsPostBack]
        public String PostInsertRowClearlist { get; set; }
        [JsPostBack]
        public String PostDelRowClearlist { get; set; }
        [JsPostBack]
        public String PostInsertRowClearbuyshr { get; set; }
        [JsPostBack]
        public String PostDelRowClearbuyshr { get; set; }
        [JsPostBack]
        public String PostInsertRowPaymentlist { get; set; }
        [JsPostBack]
        public String PostDelRowPaymentlist { get; set; }
        [JsPostBack]
        public String PostInsertRowDropln { get; set; }
        [JsPostBack]
        public String PostDelRowDropln { get; set; }
        [JsPostBack]
        public String PostInsertRowPermdown { get; set; }
        [JsPostBack]
        public String PostDelRowPermdown { get; set; }
        [JsPostBack]
        public String PostCkSalarybal { get; set; }
        [JsPostBack]
        public String PostInsertRowLnSalbal { get; set; }
        [JsPostBack]
        public String PostDelRowLnSalbal { get; set; }
        [JsPostBack]
        public String PostCmSalbalCode { get; set; }



        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsGeneral.InitDsGeneral(this);
            dsRightdet.InitDsRightdet(this);
            dsIntdetail.InitDsIntdetail(this);
            dsColldet.InitDsColldet(this);
            dsCollreqgrt.InitDsCollreqgrt(this);
            dsDpcancoll.InitDsDpcancoll(this);
            dsCollcanuse.InitDsCollcanuse(this);
            dsCleardet.InitDsCleardet(this);
            dsClearlist.InitDsClearlist(this);
            dsClearbuyshr.InitDsClearbuyshr(this);
            dsDropln.InitDsDropln(this);
            dsMbsubgrp.InitDsMbsubgrp(this);
            dsPaymentdet.InitDsPaymentdet(this);
            dsPaymentlist.InitDsPaymentlist(this);
            dsPermdown.InitDsPermdown(this);
            dsRightcollmast.InitDsRightcollmast(this);
            dsRightcustom.InitDsRightcustom(this);
            dsIntspc.InitDsIntspc(this);
            dsSalbal.InitDsSalbal(this);
            dsCmSalbal.InitDsCmSalbal(this);
            dsLoantypeSalbal.InitDsLoantypeSalbal(this);
        }

        public void WebSheetLoadBegin()
        {
            dsMain.Ddloantype();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostInsertRowRightcollmast")
            {
                dsRightcollmast.InsertLastRow();
                dsRightcollmast.DdCollmasttype();
                dsRightcollmast.DdColltype();

                int row = dsRightcollmast.RowCount - 1;
                dsRightcollmast.DATA[row].COOP_ID = state.SsCoopControl;
                dsRightcollmast.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;

            }
            else if (eventArg == "PostDelRowRightcollmast")
            {
                int r = dsRightcollmast.GetRowFocus();
                dsRightcollmast.DeleteRow(r);
                dsRightcollmast.DdCollmasttype();
                dsRightcollmast.DdColltype();
            }
            else if (eventArg == "PostInsertRowRightcustom")
            {
                dsRightcustom.InsertLastRow();
                int row = dsRightcustom.RowCount - 1;
                dsRightcustom.DATA[row].COOP_ID = state.SsCoopControl;
                dsRightcustom.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsRightcustom.DATA[row].SEQ_NO = dsRightcustom.RowCount;

            }
            else if (eventArg == "PostDelRowRightcustom")
            {
                int r = dsRightcustom.GetRowFocus();
                dsRightcustom.DeleteRow(r);
            }
            else if (eventArg == "PostInsertRowIntspc")
            {
                dsIntspc.InsertLastRow();
                dsIntspc.DdIntrate_code();

                int row = dsIntspc.RowCount - 1;
                dsIntspc.DATA[row].INTTIME_TYPE = 1;
                dsIntspc.DATA[row].COOP_ID = state.SsCoopControl;
                dsIntspc.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsIntspc.DATA[row].SEQ_NO = dsIntspc.RowCount;
            }
            else if (eventArg == "PostDelRowIntspc")
            {
                int r = dsIntspc.GetRowFocus();
                dsIntspc.DeleteRow(r);
                dsIntspc.DdIntrate_code();
            }
            else if (eventArg == "PostInsertRowCollreqgrt")
            {
                dsCollreqgrt.InsertLastRow();

                int row = dsCollreqgrt.RowCount - 1;
                dsCollreqgrt.DATA[row].COOP_ID = state.SsCoopControl;
                dsCollreqgrt.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsCollreqgrt.DATA[row].SEQ_NO = dsCollreqgrt.RowCount;
                dsCollreqgrt.DATA[row].USEMEM_OPERATION = 1;
            }
            else if (eventArg == "PostDelRowCollreqgrt")
            {
                int r = dsCollreqgrt.GetRowFocus();
                dsCollreqgrt.DeleteRow(r);
            }
            else if (eventArg == "PostInsertRowCollcanuse")
            {
                dsCollcanuse.InsertLastRow();
                dsCollcanuse.DdCollmasttype();
                dsCollcanuse.DdColltype();

                int row = dsCollcanuse.RowCount - 1;
                dsCollcanuse.DATA[row].COOP_ID = state.SsCoopControl;
                dsCollcanuse.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
            }
            else if (eventArg == "PostDelRowCollcanuse")
            {
                int r = dsCollcanuse.GetRowFocus();
                dsCollcanuse.DeleteRow(r);
                dsCollcanuse.DdCollmasttype();
                dsCollcanuse.DdColltype();
            }
            else if (eventArg == "PostInsertRowClearlist")
            {
                dsClearlist.InsertLastRow();
                dsClearlist.DdGrploanpermiss();

                int row = dsClearlist.RowCount - 1;
                dsClearlist.DATA[row].COOP_ID = state.SsCoopControl;
                dsClearlist.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
            }
            else if (eventArg == "PostDelRowClearlist")
            {
                int r = dsClearlist.GetRowFocus();
                dsClearlist.DeleteRow(r);
                dsClearlist.DdGrploanpermiss();
            }
            else if (eventArg == "PostInsertRowClearbuyshr")
            {
                dsClearbuyshr.InsertLastRow();

                int row = dsClearbuyshr.RowCount - 1;
                dsClearbuyshr.DATA[row].COOP_ID = state.SsCoopControl;
                dsClearbuyshr.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsClearbuyshr.DATA[row].SEQ_NO = dsClearbuyshr.RowCount;
            }
            else if (eventArg == "PostDelRowClearbuyshr")
            {
                int r = dsClearbuyshr.GetRowFocus();
                dsClearbuyshr.DeleteRow(r);
            }
            else if (eventArg == "PostInsertRowPaymentlist")
            {
                dsPaymentlist.InsertLastRow();

                int row = dsPaymentlist.RowCount - 1;
                dsPaymentlist.DATA[row].COOP_ID = state.SsCoopControl;
                dsPaymentlist.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsPaymentlist.DATA[row].SEQ_NO = dsPaymentlist.RowCount;
            }
            else if (eventArg == "PostDelRowPaymentlist")
            {
                int r = dsPaymentlist.GetRowFocus();
                dsPaymentlist.DeleteRow(r);
            }
            else if (eventArg == "PostInsertRowDropln")
            {
                dsDropln.InsertLastRow();
                dsDropln.DdGrploanpermiss();

                int row = dsDropln.RowCount - 1;
                dsDropln.DATA[row].COOP_ID = state.SsCoopControl;
                dsDropln.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
            }
            else if (eventArg == "PostDelRowDropln")
            {
                int r = dsDropln.GetRowFocus();
                dsDropln.DeleteRow(r);
                dsDropln.DdGrploanpermiss();
            }
            else if (eventArg == "PostInsertRowPermdown")
            {
                dsPermdown.InsertLastRow();
                dsPermdown.DdTypedown();

                int row = dsPermdown.RowCount - 1;
                dsPermdown.DATA[row].COOP_ID = state.SsCoopControl;
                dsPermdown.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
            }
            else if (eventArg == "PostDelRowPermdown")
            {
                int r = dsPermdown.GetRowFocus();
                dsPermdown.DeleteRow(r);
                dsPermdown.DdTypedown();
            }
            else if (eventArg == "PostCkSalarybal")
            {
                if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 1)
                {
                    InitCkDsSalbal(dsGeneral.DATA[0].SALARYBAL_CODE);
                }
                else if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 2)
                {
                    InitCkDsSalbal(dsMain.DATA[0].LOANTYPE_CODE);
                }
            }
            else if (eventArg == "PostInsertRowLnSalbal")
            {
                dsLoantypeSalbal.InsertLastRow();
                int row = dsLoantypeSalbal.RowCount - 1;
                dsLoantypeSalbal.DATA[row].COOP_ID = state.SsCoopControl;
                dsLoantypeSalbal.DATA[row].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
                dsLoantypeSalbal.DATA[row].SEQ_NO = row + 1;
            }
            else if (eventArg == "PostDelRowLnSalbal")
            {
                int r = dsLoantypeSalbal.GetRowFocus();
                dsLoantypeSalbal.DeleteRow(r);
            }
            else if (eventArg == "PostCmSalbalCode")
            {
                int r = dsCmSalbal.GetRowFocus();
                dsGeneral.DATA[0].SALARYBAL_CODE = dsCmSalbal.DATA[r].SALARYBAL_CODE;
                PostInitCmBalCk(r, dsCmSalbal.DATA[r].SALARYBAL_CODE);
            }
            else if (eventArg == "PostLoanTypeCode")
            {
                string loantype_code = dsMain.DATA[0].LOANTYPE_CODE;

                dsGeneral.Retrieve(loantype_code);
                dsRightdet.Retrieve(loantype_code);
                dsIntdetail.Retrieve(loantype_code);
                dsColldet.Retrieve(loantype_code);
                dsCleardet.Retrieve(loantype_code);
                dsPaymentdet.Retrieve(loantype_code);
                dsRightcollmast.Retrieve(loantype_code);
                dsRightcustom.Retrieve(loantype_code);
                dsIntspc.Retrieve(loantype_code);
                dsCollreqgrt.Retrieve(loantype_code);
                dsCollcanuse.Retrieve(loantype_code);
                dsClearlist.Retrieve(loantype_code);
                dsClearbuyshr.Retrieve(loantype_code);
                dsPaymentlist.Retrieve(loantype_code);
                dsDropln.Retrieve(loantype_code);
                dsPermdown.Retrieve(loantype_code);
                dsMbsubgrp.RetrieveMembtype();
                dsDpcancoll.RetrieveDep();

                String sqllnloantypembtype = @"  SELECT membtype_code  FROM LNLOANTYPEMBTYPE WHERE loantype_code = {0}   ";
                sqllnloantypembtype = WebUtil.SQLFormat(sqllnloantypembtype, loantype_code);
                Sdt result = WebUtil.QuerySdt(sqllnloantypembtype);

                while (result.Next())
                {
                    string membtype_code = result.GetString("membtype_code");

                    for (int i = 0; i < dsMbsubgrp.RowCount; i++)
                    {
                        if (membtype_code == dsMbsubgrp.DATA[i].MEMBTYPE_CODE)
                        {
                            dsMbsubgrp.DATA[i].operate_flag = 1;
                            dsMbsubgrp.DATA[i].loantype_code = loantype_code;
                        }
                    }
                }

                String sqllnloantypedeptcancoll = @"SELECT depttype_code  FROM LNLOANTYPEDEPTCANCOLL WHERE loantype_code = {0}";
                sqllnloantypedeptcancoll = WebUtil.SQLFormat(sqllnloantypedeptcancoll, loantype_code);
                Sdt dtlnloantypedeptcancoll = WebUtil.QuerySdt(sqllnloantypedeptcancoll);
                while (dtlnloantypedeptcancoll.Next())
                {
                    string depttype_code = dtlnloantypedeptcancoll.GetString("depttype_code");

                    for (int i = 0; i < dsDpcancoll.RowCount; i++)
                    {
                        if (depttype_code == dsDpcancoll.DATA[i].DEPTTYPE_CODE)
                        {
                            dsDpcancoll.DATA[i].operate_flag = 1;
                        }
                    }
                }

                dsGeneral.DdLoanobjective(loantype_code);
                dsGeneral.DdSalarybal();
                dsRightdet.DdGrploanpermiss();
                dsRightcollmast.DdColltype();
                dsRightcollmast.DdCollmasttype();
                dsIntdetail.DdIntrateCode();
                dsIntdetail.DdInttabrateCode();
                dsIntspc.DdIntrate_code();
                dsColldet.Ddmangrtpermgrp();
                dsColldet.Ddmangrtpermgrpco();
                dsCollcanuse.DdColltype();
                dsCollcanuse.DdCollmasttype();
                dsClearlist.DdGrploanpermiss();
                dsDropln.DdGrploanpermiss();
                dsPermdown.DdTypedown();
                dsSalbal.DATA[0].CKSALARYBAL_STATUS = dsGeneral.DATA[0].SALARYBAL_STATUS; //ย้ายจาก dsgeneral มา dssalbal
                dsCmSalbal.Retrieve(dsGeneral.DATA[0].SALARYBAL_CODE);
                dsCmSalbal.DdCmSalarybal();
                dsLoantypeSalbal.Retrieve(dsGeneral.DATA[0].LOANTYPE_CODE);
                InitCkDsSalbal(dsGeneral.DATA[0].SALARYBAL_CODE);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsGeneral, ExecuteType.Update);
                exe.AddFormView(dsRightdet, ExecuteType.Update);
                exe.AddFormView(dsIntdetail, ExecuteType.Update);
                exe.AddFormView(dsColldet, ExecuteType.Update);
                exe.AddFormView(dsCleardet, ExecuteType.Update);
                exe.AddFormView(dsPaymentdet, ExecuteType.Update);
                exe.AddRepeater(dsRightcustom);
                exe.AddRepeater(dsIntspc);
                exe.AddRepeater(dsCollreqgrt);
                exe.AddRepeater(dsClearbuyshr);
                exe.AddRepeater(dsPaymentlist);
                exe.Execute();

                try
                {
                    //dsRightcollmast
                    String sqldelcollmast = ("delete LNLOANTYPERIGHTCOLL where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadelcollmast = new Sta(state.SsConnectionString);
                    tadelcollmast.Exe(sqldelcollmast);

                    for (int i = 0; i < dsRightcollmast.RowCount; i++)
                    {
                        ExecuteDataSource exedinsertcollmast = new ExecuteDataSource(this);
                        string sqlInsertcollmast = @"insert into LNLOANTYPERIGHTCOLL(COOP_ID,
                                                LOANTYPE_CODE,
                                                LOANCOLLTYPE_CODE ,
                                                COLLMASTTYPE_CODE,
                                                RIGHT_TYPE ,
                                                RIGHT_PERC ,
                                                RIGHT_MAXAMT ,
                                                RIGHT_FORMAT ,
                                                RIGHT_RATIO)values(
                                                {0},{1},{2},{3},{4},{5},{6},{7},{8})";
                        object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].LOANTYPE_CODE,
                        dsRightcollmast.DATA[i].LOANCOLLTYPE_CODE,
                        dsRightcollmast.DATA[i].COLLMASTTYPE_CODE,
                        dsRightcollmast.DATA[i].RIGHT_TYPE,
                        dsRightcollmast.DATA[i].RIGHT_PERC,
                        dsRightcollmast.DATA[i].RIGHT_MAXAMT,
                        dsRightcollmast.DATA[i].RIGHT_FORMAT,
                        dsRightcollmast.DATA[i].RIGHT_RATIO };
                        sqlInsertcollmast = WebUtil.SQLFormat(sqlInsertcollmast, argslistInsert);
                        exedinsertcollmast.SQL.Add(sqlInsertcollmast);
                        exedinsertcollmast.Execute();
                    }
                }
                catch { }

                try
                {
                    //dsCollcanuse
                    String sqldelcollcanuse = ("delete LNLOANTYPECOLLUSE where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadelcollcanuse = new Sta(state.SsConnectionString);
                    tadelcollcanuse.Exe(sqldelcollcanuse);

                    for (int i = 0; i < dsCollcanuse.RowCount; i++)
                    {
                        ExecuteDataSource exedinsertcollcanuse = new ExecuteDataSource(this);
                        string sqlInsertcollcanuse = @"insert into LNLOANTYPECOLLUSE (COOP_ID,
                                                LOANTYPE_CODE,
                                                LOANCOLLTYPE_CODE ,
                                                COLLMASTTYPE_CODE,
                                                COLL_PERCENT,
                                                SUBSHRCOLL_STATUS)values(
                                                {0},{1},{2},{3},{4},{5})";
                        object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].LOANTYPE_CODE,
                        dsCollcanuse.DATA[i].LOANCOLLTYPE_CODE,
                        dsCollcanuse.DATA[i].COLLMASTTYPE_CODE,
                        dsCollcanuse.DATA[i].COLL_PERCENT,
                        dsCollcanuse.DATA[i].SUBSHRCOLL_STATUS};
                        sqlInsertcollcanuse = WebUtil.SQLFormat(sqlInsertcollcanuse, argslistInsert);
                        exedinsertcollcanuse.SQL.Add(sqlInsertcollcanuse);
                        exedinsertcollcanuse.Execute();
                    }
                }
                catch { }

                try
                {
                    //dsclearlist
                    String sqldelclearlist = ("delete LNLOANTYPECLR where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadelclearlist = new Sta(state.SsConnectionString);
                    tadelclearlist.Exe(sqldelclearlist);

                    for (int i = 0; i < dsClearlist.RowCount; i++)
                    {
                        ExecuteDataSource exedinsertclearlist = new ExecuteDataSource(this);
                        string sqlInsertclearlist = @"insert into LNLOANTYPECLR (COOP_ID,
                                                LOANTYPE_CODE, 
                                                LOANTYPE_CLEAR, 
                                                MINPERIOD_PAY, 
                                                MINPERCENT_PAY, 
                                                CHKCONTCREDIT_FLAG, 
                                                FINE_AMT, 
                                                FINE_MAXAMT, 
                                                FINE_PERCENT, 
                                                FINECOND_TYPE)values(
                                                {0},{1},{2},{3},{4},{5},{6},{7},{8},{9})";
                        object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].LOANTYPE_CODE,
                        dsClearlist.DATA[i].LOANTYPE_CLEAR,  
                        dsClearlist.DATA[i].MINPERIOD_PAY,   
                        dsClearlist.DATA[i].MINPERCENT_PAY,   
                        dsClearlist.DATA[i].CHKCONTCREDIT_FLAG,   
                        dsClearlist.DATA[i].FINE_AMT,   
                        dsClearlist.DATA[i].FINE_MAXAMT,   
                        dsClearlist.DATA[i].FINE_PERCENT,   
                        dsClearlist.DATA[i].FINECOND_TYPE};
                        sqlInsertclearlist = WebUtil.SQLFormat(sqlInsertclearlist, argslistInsert);
                        exedinsertclearlist.SQL.Add(sqlInsertclearlist);
                        exedinsertclearlist.Execute();
                    }
                }
                catch { }

                try
                {
                    //dsDropln
                    String sqldelDropln = ("delete LNLOANTYPEPAUSE where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadelDropln = new Sta(state.SsConnectionString);
                    tadelDropln.Exe(sqldelDropln);

                    for (int i = 0; i < dsDropln.RowCount; i++)
                    {
                        ExecuteDataSource exedinsertDropln = new ExecuteDataSource(this);
                        string sqlInsertDropln = @"insert into LNLOANTYPEPAUSE (COOP_ID,
                                                LOANTYPE_CODE, 
                                                LOANTYPE_PAUSE) values (
                                                {0},{1},{2})";
                        object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].LOANTYPE_CODE,
                        dsDropln.DATA[i].LOANTYPE_PAUSE};
                        sqlInsertDropln = WebUtil.SQLFormat(sqlInsertDropln, argslistInsert);
                        exedinsertDropln.SQL.Add(sqlInsertDropln);
                        exedinsertDropln.Execute();
                    }
                }
                catch { }

                try
                {
                    //dsPermdown
                    String sqldelPermdown = ("delete LNLOANTYPEPERMDOWN where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadelPermdown = new Sta(state.SsConnectionString);
                    tadelPermdown.Exe(sqldelPermdown);

                    for (int i = 0; i < dsPermdown.RowCount; i++)
                    {
                        ExecuteDataSource exedinsertPermdown = new ExecuteDataSource(this);
                        string sqlInsertPermdown = @"insert into LNLOANTYPEPERMDOWN
                                                (COOP_ID,
                                                LOANTYPE_CODE, 
                                                LOANTYPE_DOWN,
                                                LNSEND_PERIOD,
                                                MAXPERMISS_AMT
                                                ) values(
                                                {0},{1},{2},{3},{4})";
                        object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].LOANTYPE_CODE,
                        dsPermdown.DATA[i].LOANTYPE_DOWN  ,
                        dsPermdown.DATA[i].LNSEND_PERIOD,
                        dsPermdown.DATA[i].MAXPERMISS_AMT};
                        sqlInsertPermdown = WebUtil.SQLFormat(sqlInsertPermdown, argslistInsert);
                        exedinsertPermdown.SQL.Add(sqlInsertPermdown);
                        exedinsertPermdown.Execute();
                    }
                }
                catch { }

                try
                {
                    String sqldel = ("delete LNLOANTYPEMBTYPE where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadel = new Sta(state.SsConnectionString);
                    int ree = tadel.Exe(sqldel);

                    for (int i = 0; i < dsMbsubgrp.RowCount; i++)
                    {
                        if (dsMbsubgrp.DATA[i].operate_flag == 1)
                        {
                            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
                            string sqlInsert = @"insert into LNLOANTYPEMBTYPE(MEMBTYPE_CODE, COOP_ID, operate_flag, loantype_code) values ({0},{1},{2},{3})";
                            sqlInsert = WebUtil.SQLFormat(sqlInsert, dsMbsubgrp.DATA[i].MEMBTYPE_CODE, state.SsCoopControl, dsMbsubgrp.DATA[i].operate_flag, dsMain.DATA[0].LOANTYPE_CODE);
                            exedinsert.SQL.Add(sqlInsert);
                            exedinsert.Execute();
                        }
                    }
                }
                catch { }

                try
                {
                    String sqldeldep = ("delete LNLOANTYPEDEPTCANCOLL where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                    Sta tadeldep = new Sta(state.SsConnectionString);
                    int re = tadeldep.Exe(sqldeldep);

                    for (int i = 0; i < dsDpcancoll.RowCount; i++)
                    {
                        if (dsDpcancoll.DATA[i].operate_flag == 1)
                        {
                            ExecuteDataSource exedinsertdep = new ExecuteDataSource(this);
                            string sqlInsertdep = @"insert into LNLOANTYPEDEPTCANCOLL(depttype_code, COOP_ID, operate_flag, loantype_code) values ({0},{1},{2},{3})";
                            sqlInsertdep = WebUtil.SQLFormat(sqlInsertdep, dsDpcancoll.DATA[i].DEPTTYPE_CODE, state.SsCoopControl, dsDpcancoll.DATA[i].operate_flag, dsMain.DATA[0].LOANTYPE_CODE);
                            exedinsertdep.SQL.Add(sqlInsertdep);
                            exedinsertdep.Execute();
                        }
                    }
                }
                catch { }
                //insert ให้เฉพาะขั้นบันได
                if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 2)
                {
                    try
                    {
                        string sqlsalbal = ("delete LNLOANTYPESALBAL where LOANTYPE_CODE ='" + dsMain.DATA[0].LOANTYPE_CODE + "'");
                        Sta tadelsal = new Sta(state.SsConnectionString);
                        int re = tadelsal.Exe(sqlsalbal);

                        for (int i = 0; i < dsLoantypeSalbal.RowCount; i++)
                        {
                            ExecuteDataSource exedinsertsalbal = new ExecuteDataSource(this);
                            string sqlInsertsalbal = @"insert into lnloantypesalbal 
                            (coop_id, loantype_code, seq_no, money_from, money_to, salbalmin_perc, salbalmin_amt) values 
                            ({0},{1},{2},{3},{4},{5},{6})";
                            sqlInsertsalbal = WebUtil.SQLFormat(sqlInsertsalbal, state.SsCoopControl, dsMain.DATA[0].LOANTYPE_CODE,
                                i, dsLoantypeSalbal.DATA[i].MONEY_FROM, dsLoantypeSalbal.DATA[i].MONEY_TO,
                                dsLoantypeSalbal.DATA[i].SALBALMIN_PERC, dsLoantypeSalbal.DATA[i].SALBALMIN_AMT);
                            exedinsertsalbal.SQL.Add(sqlInsertsalbal);
                            exedinsertsalbal.Execute();
                        }
                    }
                    catch { }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        private void InitCkDsSalbal(string salarybalCode)
        {
            if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 0) //ไม่ตรวจ
            {
                dsGeneral.DATA[0].SALARYBAL_STATUS = dsSalbal.DATA[0].CKSALARYBAL_STATUS;
                dsCmSalbal.Visible = false;
                dsLoantypeSalbal.Visible = false;
                LntypeSalbal.Visible = false; 
            }
            else if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 1) //ตรวจเงินเดือน
            {
                if (salarybalCode == null || salarybalCode == "")
                {
                    salarybalCode = dsGeneral.DATA[0].LOANTYPE_CODE;
                }
                dsCmSalbal.Retrieve(salarybalCode);
                dsCmSalbal.DdCmSalarybal();
                dsGeneral.DATA[0].SALARYBAL_STATUS = dsSalbal.DATA[0].CKSALARYBAL_STATUS;
                dsCmSalbal.Visible = true;
                dsLoantypeSalbal.Visible = false;
                LntypeSalbal.Visible = false;  
            }
            else if (dsSalbal.DATA[0].CKSALARYBAL_STATUS == 2) //ตรวจเงินเดือนเป็นขั้น
            {
                dsGeneral.DATA[0].SALARYBAL_STATUS = dsSalbal.DATA[0].CKSALARYBAL_STATUS;
                dsGeneral.DATA[0].SALARYBAL_CODE = dsGeneral.DATA[0].LOANTYPE_CODE;
                salarybalCode = dsGeneral.DATA[0].LOANTYPE_CODE;
                dsLoantypeSalbal.Retrieve(salarybalCode);
                dsLoantypeSalbal.Visible = true;
                dsCmSalbal.Visible = false;
                LntypeSalbal.Visible = true; 
            }
        }

        private void PostInitCmBalCk(int rowse, string salarybal_code)
        {
            string sqlGetval = @"select salarybal_amt, salarybal_percent from cmucfsalarybalance where salarybal_code = {0}";
            sqlGetval = WebUtil.SQLFormat(sqlGetval, salarybal_code);
            Sdt ta = WebUtil.QuerySdt(sqlGetval);
            if (ta.Next())
            {
                dsCmSalbal.DATA[rowse].SALARYBAL_AMT = ta.GetDecimal("salarybal_amt");
                dsCmSalbal.DATA[rowse].SALARYBAL_PERCENT = ta.GetDecimal("salarybal_percent");
            }

        }
    }
}