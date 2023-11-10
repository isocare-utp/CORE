using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.divavg.dlg
{
    public partial class w_dlg_divsrv_search_bank : PageWebDialog, WebDialog
    {
        protected String postSearch;
        protected String postNewClear;
        protected String postSearchBankBranch;
        protected String postFilterBranch;
        protected String postSetBranch;
        //==========================
        public String pbl = "divsrv_req_methpay.pbl";
        WebState state;
        DwTrans SQLCA;

        public void InitJsPostBack()
        {
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postSearchBankBranch = WebUtil.JsPostBack(this, "postSearchBankBranch");
            postFilterBranch = WebUtil.JsPostBack(this, "postFilterBranch");
            postSetBranch = WebUtil.JsPostBack(this, "postSetBranch");
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            Dw_main.SetTransaction(SQLCA);
            Dw_bank.SetTransaction(SQLCA);
            Dw_branch.SetTransaction(SQLCA);


            if (!IsPostBack)
            {
                JspostNewClear();
            }

            //if (!hidden_search.Value.Equals(""))
            //{
            //    Dw_bank.SetSqlSelect(hidden_search.Value);
            //    Dw_bank.Retrieve();
            //    Dw_branch.Reset();
            //}
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSearch")
            {
               // JspostSearch();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postSearchBankBranch")
            {
                JspostSearchBankBranch();
            }
            else if (eventArg == "postFilterBranch")
            {
                JspostFilterBranch();
            }
            else if (eventArg == "postSetBranch")
            {
                JspostSetBranch();
            }
        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_bank.SaveDataCache();
            Dw_branch.SaveDataCache();

            SQLCA.Disconnect();
        }
        //========================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_bank.Retrieve();
            Dw_branch.Reset();
        }

     
        protected void JspostClearHidden()
        {
            hidden_search.Value = "";
           
        }

        protected void JspostSearchBankBranch()
        {
            try
            {
                //กรณีที่ค้นหาธนาคาร
                String expense_bank, expense_branch;
                String FieldChange = HdChange.Value.Trim();
                
                //field ที่เปลี่ยน
                if (FieldChange == "bank_name")
                {
                    Dw_main.SetItemString(1, "expense_bank", "");
                }
                else
                {
                    Dw_main.SetItemString(1, "expense_branch", "");
                }


                try 
                {
                    expense_bank = Dw_main.GetItemString(1, "expense_bank");
                }
                catch { expense_bank = ""; }

                try
                {
                    expense_branch = Dw_main.GetItemString(1, "expense_branch");
                }
                catch { expense_branch = ""; }

                String ls_sql = "", ls_sqlext = "", ls_temp = "";

                if (expense_bank == "")
                {
                    String bank_name = "";
                    

                    ls_sql = Dw_bank.GetSqlSelect();

                    try
                    {
                        bank_name = Hdbank.Value.Trim();

                    }
                    catch { bank_name = ""; }

                    if (bank_name.Length > 0)
                    {
                        ls_sqlext += " where ( cmucfbank.bank_desc like '%" + bank_name + "%') order by bank_code";
                    }

                    ls_temp = ls_sql + ls_sqlext;
                    if (ls_sql != ls_temp)
                    {
                        hidden_search.Value = ls_temp;
                        DwUtil.ImportData(ls_temp, Dw_bank, null);
                        Dw_branch.Reset();
                    }
                    else
                    {
                        Dw_bank.Reset();
                    }
                }
                //กรณีที่ค้นหาสาขาธนาคาร
                else
                {
                    String branch_name = "";
                    ls_sql = Dw_branch.GetSqlSelect();

                    try
                    {
                        branch_name = Hdbranch.Value.Trim();

                    }
                    catch { branch_name = ""; }


                    if (branch_name.Length > 0)
                    {
                        ls_sqlext += " where ( cmucfbankbranch.bank_code = '"+expense_bank+"' and branch_name like '%" + branch_name + "%') order by branch_id";
                    }

                    ls_temp = ls_sql + ls_sqlext;
                    if (ls_sql != ls_temp)
                    {
                        hidden_search.Value = ls_temp;
                        DwUtil.ImportData(ls_temp, Dw_branch, null);
                        //   JspostClearHidden();
                    }
                    else
                    {
                        Dw_bank.Reset();
                    }
                }
               

                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostFilterBranch()
        {
            try 
            {
                string ls_sql, ls_temp,ls_sqltext ;
                String expense_bank = Hdexpensebank.Value.Trim();
                Dw_main.SetItemString(1, "expense_bank", expense_bank);
                Dw_branch.Reset();

                ls_sql = Dw_branch.GetSqlSelect();
                ls_sqltext = "where ( cmucfbankbranch.bank_code = '" + expense_bank + "') order by branch_id";
                ls_temp = ls_sql + ls_sqltext;
                DwUtil.ImportData(ls_temp, Dw_branch, null);

                int RowClickList = int.Parse(HdRowBank.Value);
                Dw_bank.SelectRow(0, false);
                Dw_bank.SelectRow(RowClickList, true);
                Dw_bank.SetRow(RowClickList);

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetBranch()
        {
            try 
            {
                String expense_branch = Hdexpensebranch.Value.Trim();
                Dw_main.SetItemString(1, "expense_branch", expense_branch);
                int RowClickList = int.Parse(HdRowBranch.Value);
                Dw_branch.SelectRow(0, false);
                Dw_branch.SelectRow(RowClickList, true);
                Dw_branch.SetRow(RowClickList);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}