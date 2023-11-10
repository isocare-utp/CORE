using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon;  //new common
using DataLibrary;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit
using System.Web.Services.Protocols;
using Saving.ConstantConfig;
namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_reclint : PageWebSheet, WebSheet
    {
        protected String postNewAccount;  //ประกาศไว้ให้ตรงกับหน้า aspx 
        protected String Pcalculate;
        //private DepositClient depService;
        private n_depositClient ndept; // new deposit   
        private String pblFileName = "dp_recalint.pbl";
        private DwThDate tdw_main;
        protected String newClear;
        private String deptAccountNo = null;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postNewAccount = WebUtil.JsPostBack(this, "postNewAccount");
            Pcalculate = WebUtil.JsPostBack(this, "Pcalculate");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdw_main = new DwThDate(DwMain, this);
            tdw_main.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin() // โหลดหน้าครั้งแรก
        {
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            { }
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tdw_main.Eng2ThaiAllRow();
               // DwDetail.InsertRow(0);
                DwDetail.Visible = false;
                DwDetailBouns.Visible = false;


            }
            else
            {
                this.RestoreContextDw(DwMain);
                
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "newClear")
            {
                JsNewClear();
            }
            if (eventArg == "postNewAccount")
            {
                JsNewAccountNo();

            }
            if (eventArg == "Pcalculate")
            {
                Fccaliinterest();
               
            }
        }

        private void JsNewClear()
        {
            DwMain.Reset();  //ถ้า reset แล้ว ไม่่ insert หน้าพวก form ที่เราสร้างไว้จะหาย
            DwMain.InsertRow(0);
            DwDetail.Reset();
            DwDetailBouns.Reset();
            //DwMain.
           
             
        }

        public void SaveWebSheet() //ถ้ามีการ save  ต้องใส่ code ตรงนี้ด้วย
        {

        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache(); // ให้จำค่าที่กรอกเข้าไปด้วย เพราะถ้ามันรีเฟรสมันจะไม่จำข้อมูล
            DwDetail.SaveDataCache();
        }

        #endregion

        private void JsNewAccountNo()  // ฟังก์ชันดึงข้อมูลมาแสดง
        {
           String accNo = null;
           DwDetail.Reset();
          
               accNo = DwMain.GetItemString(1, "deptaccount_no");
               //accNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);
               accNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, accNo); //new
               string sql = "select Deptaccount_Name,Deptaccount_No  from dpdeptmaster where Deptaccount_No = '" + accNo + "' ";
               Sdt dt = WebUtil.QuerySdt(sql);

               while (dt.Next())
               {
                   
                   
                   string name = dt.GetString("Deptaccount_Name");
                   string accountNO = dt.GetString("Deptaccount_No");

                   DwMain.SetItemString(1, "deptaccount_no", accountNO);
                   DwMain.SetItemString(1, "deptaccount_name", name);//ใส่ค่าเข้าไปใน datawindown
                   tdw_main.Eng2ThaiAllRow();
               }
               if (dt.Rows.Count < 1)
               {
                   DwDetail.Visible = false;
                   DwDetailBouns.Visible = false;
                   JsNewClear();
                   DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                   tdw_main.Eng2ThaiAllRow();
                   LtServerMessage.Text = "ไม่พบเลขบัญชี";
               }

               
             
         
            //string sql = "select Deptaccount_Name,Deptaccount_No  from dpdeptmaster where Deptaccount_No = '" + accNo + "' ";
            //Sdt dt = WebUtil.QuerySdt(sql);


            //try
            //{

            //    while (dt.Next())
            //    {
            //        string name = dt.GetString("Deptaccount_Name");
            //        string accountNO = dt.GetString("Deptaccount_No");

            //        DwMain.SetItemString(1, "deptaccount_no", accountNO);
            //        DwMain.SetItemString(1, "deptaccount_name", name);//ใส่ค่าเข้าไปใน datawindown
            //        tdw_main.Eng2ThaiAllRow();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    JsNewClear();
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //}
                   
            }


        
        // fc ที่ใช้ดึง servive ของพี่หนุ่ม  โดย select ค่า จาก db มาเก็บใส่ตัวแปร ต้องประกาศ type ของตัวแปรให้ตรงกับ type ใน database 
        private void Fccaliinterest()
        {
            DwDetail.Visible = true;
          //  DwDetail.InsertRow(0);
            DwDetailBouns.Visible = true;
           // DwDetail.InsertRow(0);

            string accNo = DwMain.GetItemString(1, "deptaccount_no");
            decimal calinterest = 0; //เก็บดอกเบี้ยที่คำนวณได้
            decimal caltax = 0;//เก็บภาษีที่คำนวณได้
            decimal bonus = 0;//เก็บโบนัส
            Int16 intchreturn = 0; //คำนวณตั้งแต่รายการแรกหรือไม่ 1 ใช่ 0 ไม่ใช่ ให้ส่งมา เป็น 1
            DateTime date = DwMain.GetItemDateTime(1, "operate_date");
            String wsPass = state.SsWsPass;
            String xml_result = "", xml_resultBonus="";

            //เรียกใช้ webservice


            DwDetail.Reset();
            DwDetail.InsertRow(0);
            string sql = "select depttype_code,spcint_rate,spcint_rate_status  from dpdeptmaster where Deptaccount_No = '" + accNo + "' ";
            Sdt dt = WebUtil.QuerySdt(sql);

            while (dt.Next())
            {
                string depttype_code = dt.GetString("depttype_code");
                decimal spcint_rate = dt.GetDecimal("spcint_rate");
                Int16 spcint_rate_status = Convert.ToInt16(dt.GetDecimal("spcint_rate_status"));
                try
                {

                    ndept.of_recallinterest_statement(state.SsWsPass, depttype_code, accNo, spcint_rate, spcint_rate_status, date, ref calinterest, ref caltax, intchreturn, ref xml_result);
                  //depService.of_recallinterestBonus(state.SsWsPass, depttype_code, accNo, date, intchreturn, ref bonus, ref xml_resultBonus);
                    if (xml_result == "" || xml_result == null )//|| xml_resultBonus == "" || xml_resultBonus==null)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขที่บัญชี : " + accNo);
                        JsNewClear();
                    }
                    else
                    {

                        DwUtil.ImportData(xml_result, DwDetail, null, FileSaveAsType.Xml); //ให้เอาผลลัพธ์มาแสดงใน DwDetail
                      //  DwUtil.ImportData(xml_resultBonus, DwDetailBouns, null, FileSaveAsType.Xml); //ให้เอาผลลัพธ์มาแสดงใน DwDetail
                    }
                }

                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    JsNewClear();
                }

            }
        }

  
    }
}




