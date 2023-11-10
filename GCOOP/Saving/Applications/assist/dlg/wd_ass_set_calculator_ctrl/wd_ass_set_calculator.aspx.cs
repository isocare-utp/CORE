using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl
{
    public partial class wd_ass_set_calculator : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSave { get; set; }
        [JsPostBack]
        public string Postdel { get; set; }
        [JsPostBack]
        public String PostSaveData { get; set; }

        public void InitJsPostBack()
        {
            dsMaster.InitDsMaster(this);
            dsData.InitDsData(this);
        }

        public void WebDialogLoadBegin()
        {
            
            if (!IsPostBack)//show data first  
            {       
                string assisttype_code =  Request["assisttype_code"];
                dsMaster.RetrieveCalculator();
                dsData.RetrieveData(assisttype_code);
                HD_sheetcode.Value = assisttype_code;
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == Postdel)
            {
                int row = dsData.GetRowFocus();
                dsData.DeleteRow(row);
            }
            if (eventArg == PostSave)
            {
                Decimal check_flag = 0;
                for (int i = 0; i <= dsMaster.RowCount; i++)
                {
                    try
                    {
                        check_flag = dsMaster.DATA[i].check_flag;

                    }
                    catch { check_flag = 0; }

                    if (check_flag == 1)
                    {
                        String CALCULATOR_TYPE = dsMaster.DATA[i].CALCULATOR_TYPE;
                        String CALCULATOR_DESC = dsMaster.DATA[i].CALCULATOR_DESC;

                        int ll_insert = dsData.InsertLastRow(1) - 1;
                        dsData.DATA[ll_insert].CALCULATOR_TYPE = CALCULATOR_TYPE;
                        dsData.DATA[ll_insert].calculator_name = CALCULATOR_DESC;
                        dsData.DATA[ll_insert].OPERATION_TYPE = "1";
                        dsMaster.DATA[i].check_flag = 0;
                    }
                }
            }
            if (eventArg == PostSaveData)
            {
                savedata();
            }
        }
        public void savedata()
        {
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                string assisttype_code = HD_sheetcode.Value;
                String sqldel = @"DELETE FROM asscalculatorassist WHERE assisttype_code='" + assisttype_code + "'";
                Sdt del_datacalcu = WebUtil.QuerySdt(sqldel);

                for (int i = 0; i < dsData.RowCount; i++)
                {
                    string CALCULATOR_TYPE = dsData.DATA[i].CALCULATOR_TYPE;
                    string calculator_name = dsData.DATA[i].calculator_name;
                    string OPERATION_TYPE = dsData.DATA[i].OPERATION_TYPE;
                    //string SORT_ORDER = dsData.DATA[i].SORT_ORDER;
                    int SORT_ORDER = i + 1;

                    string SqlInsertSetcalcu ="INSERT INTO asscalculatorassist "+
                            "(coop_id, assisttype_code, calculator_type, operation_type, sort_order) "+
                            "VALUES " + "('"+ state.SsCoopId +"','"+assisttype_code+"','"+CALCULATOR_TYPE+"','"+ OPERATION_TYPE +"','"+ SORT_ORDER +"')";
                    ta.Exe(SqlInsertSetcalcu);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ ");
                }

            }
            catch (Exception Ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้");
                LtServerMessage.Text = WebUtil.ErrorMessage(Ex);
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}