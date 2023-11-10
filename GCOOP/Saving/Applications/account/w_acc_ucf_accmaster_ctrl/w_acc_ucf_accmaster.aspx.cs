using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.account.w_acc_ucf_accmaster_ctrl
{
    public partial class w_acc_ucf_accmaster : PageWebSheet, WebSheet
    {
       
        [JsPostBack]
        public String Postaccdetail { get; set; }
        [JsPostBack]
        public String PostaddRow { get; set; }
        [JsPostBack]
        public String PosteditRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
       


        public void InitJsPostBack()
        {
          
            wd_list.InitList(this);
            wd_main.InitList(this);
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                wd_list.RetrieveList();
                wd_main.Retrievedetail("10000000");
                wd_main.acctype();
                wd_main.accbooktype();
                wd_main.accountrevgroup();
                wd_main.FindTextBox(0, "ACCOUNT_ID").Enabled = false;
                wd_main.FindTextBox(0, "SECTION_ID").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_NAME").Enabled = false;
                wd_main.FindDropDownList(0, "account_level").Enabled = false;
                wd_main.FindDropDownList(0, "account_type_id").Enabled = false;
                wd_main.FindDropDownList(0, "account_group_id").Enabled = false;
                wd_main.FindCheckBox(0, "ON_REPORT").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_NATURE").Enabled = false;
                wd_main.FindCheckBox(0, "ACTIVE_STATUS").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_CONTROL_ID").Enabled = false;
                wd_main.FindTextBox(0, "account_activity").Enabled = false;
                wd_main.FindTextBox(0, "acc_astimate_dc").Enabled = false;
                wd_main.FindDropDownList(0, "account_rev_group").Enabled = false;
               
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
           

          if (eventArg == Postaccdetail)
            {
               
                int r = wd_list.GetRowFocus();
                string account_id = wd_list.DATA[r].ACCOUNT_ID;
                wd_main.Retrievedetail(account_id);
                wd_main.acctype(); 
                wd_main.accbooktype();
                wd_main.accountrevgroup();
               
                wd_main.FindTextBox(0, "ACCOUNT_ID").Enabled = false;
                wd_main.FindTextBox(0, "SECTION_ID").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_NAME").Enabled = false;
                wd_main.FindDropDownList(0, "account_level").Enabled = false;
                wd_main.FindDropDownList(0, "account_type_id").Enabled = false;
                wd_main.FindDropDownList(0, "account_group_id").Enabled = false;
                wd_main.FindCheckBox(0, "ON_REPORT").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_NATURE").Enabled = false;
                wd_main.FindCheckBox(0, "ACTIVE_STATUS").Enabled = false;
                wd_main.FindTextBox(0, "ACCOUNT_CONTROL_ID").Enabled = false;
                wd_main.FindTextBox(0, "account_activity").Enabled = false;
                wd_main.FindTextBox(0, "acc_astimate_dc").Enabled = false;
                wd_main.FindDropDownList(0, "account_rev_group").Enabled = false;


            }
            else if (eventArg == PostaddRow)
            {
                int r = wd_main.GetRowFocus();

                wd_main.SetItem(r, wd_main.DATA.ACCOUNT_IDColumn, "*** ระบุรหัสบัญชี");
                wd_main.SetItem(r, wd_main.DATA.SECTION_IDColumn,wd_main.DATA[r].SECTION_ID);
                wd_main.SetItem(r, wd_main.DATA.ACCOUNT_NAMEColumn, "*** ระบุชื่อบัญชี");
                wd_main.FindTextBox(0, "ACCOUNT_ID").Enabled = true;
                wd_main.FindTextBox(0, "SECTION_ID").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_NAME").Enabled = true;
                wd_main.FindDropDownList(0, "account_level").Enabled = true;
                wd_main.FindDropDownList(0, "account_type_id").Enabled = true;
                wd_main.FindDropDownList(0, "account_group_id").Enabled = true;
                wd_main.FindCheckBox(0, "ON_REPORT").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_NATURE").Enabled = true;
                wd_main.FindCheckBox(0, "ACTIVE_STATUS").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_CONTROL_ID").Enabled = true;
                wd_main.FindTextBox(0, "account_activity").Enabled = true;
                wd_main.FindTextBox(0, "acc_astimate_dc").Enabled = true;
                wd_main.FindDropDownList(0, "account_rev_group").Enabled = true;
            }

            else if (eventArg == PosteditRow)
            {
                wd_main.FindTextBox(0, "ACCOUNT_ID").Enabled = false;
                wd_main.FindTextBox(0, "SECTION_ID").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_NAME").Enabled = true;
                wd_main.FindDropDownList(0, "account_level").Enabled = true;
                wd_main.FindDropDownList(0, "account_type_id").Enabled = true;
                wd_main.FindDropDownList(0, "account_group_id").Enabled = true;
                wd_main.FindCheckBox(0, "ON_REPORT").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_NATURE").Enabled = true;
                wd_main.FindCheckBox(0, "ACTIVE_STATUS").Enabled = true;
                wd_main.FindTextBox(0, "ACCOUNT_CONTROL_ID").Enabled = true;
                wd_main.FindTextBox(0, "account_activity").Enabled = true;
                wd_main.FindTextBox(0, "acc_astimate_dc").Enabled = true;
                wd_main.FindDropDownList(0, "account_rev_group").Enabled = true;
                
               
            }

            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = wd_main.GetRowFocus();
                string account_id = wd_main.DATA[ls_getrow].ACCOUNT_ID;
                if (IsUsedaaccountid(account_id))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    ExecuteDataSource exx = new ExecuteDataSource(this);
                    String sql = "delete from accmaster where account_id = '" + account_id + "'";
                    WebUtil.Query(sql);


                    wd_list.RetrieveList();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบคู่บัญชี " + account_id + " สำเร็จ");
                }
            }
        }

        public void SaveWebSheet()
        {
            string coop_id = state.SsCoopControl;
            string account_id = wd_main.DATA[0].ACCOUNT_ID;
            string section_id = wd_main.DATA[0].SECTION_ID;
            string account_name = wd_main.DATA[0].ACCOUNT_NAME;
            string account_type_id = wd_main.DATA[0].ACCOUNT_TYPE_ID;
            string account_group_id = wd_main.DATA[0].ACCOUNT_GROUP_ID;
            decimal account_level = wd_main.DATA[0].ACCOUNT_LEVEL;
            string account_nature = wd_main.DATA[0].ACCOUNT_NATURE;
            string account_control_id = wd_main.DATA[0].ACCOUNT_CONTROL_ID;
            string account_rev_group = wd_main.DATA[0].ACCOUNT_REV_GROUP;
            decimal active_status = wd_main.DATA[0].ACTIVE_STATUS;
            decimal on_report = wd_main.DATA[0].ON_REPORT; 
            decimal intcr_flag = wd_main.DATA[0].INTCR_FLAG;

            if (checkupdateorinsert(account_id))
            {
                try
                {
                    ExecuteDataSource exx = new ExecuteDataSource(this);
                    String sql = "update accmaster set   section_id = '" + section_id + "', account_name =  '" + account_name + "' , account_type_id =  '" + account_type_id + "', account_group_id = '" + account_group_id + "', account_level = '" + account_level + "'  , account_nature = '" + account_nature + "' , account_control_id =  '" + account_control_id + "'  , account_rev_group = '" + account_rev_group + "'  , account_activity = '" + active_status + "' , active_status = 1 , on_report = '" + on_report + "' , intcr_flag = 1 where   account_id =  '" + account_id + "'";
                    WebUtil.Query(sql);


                    wd_list.RetrieveList();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูล " + account_id + " สำเร็จ");



                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }

            else 
            {
                try
                {
                    ExecuteDataSource exx = new ExecuteDataSource(this);
                    String sql = "INSERT INTO accmaster( coop_id , account_id , section_id , account_name , account_type_id , account_group_id , account_level , balance_begin , account_nature , account_control_id , account_close_id , account_sort , sum_cramt_year , sum_dramt_year , acc_astimate_dc , account_rev_group , account_control_2level , account_control_3level , account_control_4level , account_newid , account_oldid , branch_id , desc_cr , desc_dr , account_activity , active_status , on_report , intcr_flag) VALUES('" + coop_id + "', '" + account_id + "' , '" + section_id + "' ,'" + account_name + "' , '" + account_type_id + "' , '" + account_group_id + "' , '" + account_level + "' , 0,'" + account_nature + "' , '" + account_control_id + "' , '' , '' ,'','','','" + account_rev_group + "' , '' , ''  , '' , '' , '' , '001' , '' ,'' , '' , '" + active_status + "' , '" + on_report + "' , 1)";
                    WebUtil.Query(sql);


                    wd_list.RetrieveList();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูล " + account_id + " สำเร็จ");



                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
           
            
        }

        public void WebSheetLoadEnd()
        {

        }

        public bool IsUsedaaccountid(string account_id)
        {
            string sql = "select account_id from vcvoucherdet where account_id={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, account_id);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return chk;
        }

        public bool checkupdateorinsert(string account_id)
        {
            string sql = "select account_id from accmaster where account_id={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, account_id);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return chk;
        }
        


    }
}