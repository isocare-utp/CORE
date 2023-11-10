using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_contact_ctrl
{
    public partial class ws_contact : PageWebSheet, WebSheet
    {
        private string memb_no;
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string SetRefMember { get; set; }
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        //public string PostMem { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDs(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                this.SetOnLoadedScript(" parent.Setfocus();");
                //dsList.Retrieve();//show data first
                //dsList.RetriveGroup();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
                dsList.RetrieveData(memb_no);
                //cal_sum_recv();
                //update_numrow();
            }
            else if (eventArg == "SetRefMember")
            {
                int row = Convert.ToInt32(HdRow.Value);
                string refmemb_no = WebUtil.MemberNoFormat(dsList.DATA[row].REFMEMBER_NO);
                String DESCRIPTION = "";
                String REFMEMBER_ADDRESS = "";
                String REFMEMBER_TEL = "";
                String REFMEMBER_FLAG = "";
                //dsMain.RetrieveMain(memb_no);
                    try
                    {
                        String sql = @" select mbmembmaster.coop_id,  
                           mbmembmaster.member_no as REFMEMBER_NO,  
                           mbmembmaster.prename_code,        
                           mbmembmaster.member_type,
                          1 refmember_flag,
                          isnull(rtrim(ltrim(mbucfprename.prename_desc)),'') + isnull(rtrim(ltrim(mbmembmaster.memb_name) ),'')  
                          +' '+ isnull(rtrim(ltrim(mbmembmaster.memb_surname)),'') as DESCRIPTION,

           ('เลขที่'+(isnull(rtrim(ltrim(mbmembmaster.addr_no)),''))+' '+'ม.'+(isnull(rtrim(ltrim(mbmembmaster.addr_moo)),''))+' '+'อ.'+(isnull(rtrim(ltrim(mbucfdistrict.district_desc)),''))  
                           +' '+'จ.'+(isnull(rtrim(ltrim(mbucfprovince.province_desc)),''))+' '+'รหัสไปรษณีย์ '+(isnull(rtrim(ltrim(mbmembmaster.addr_postcode)),'')) ) as REFMEMBER_ADDRESS,
              isnull(rtrim(ltrim(mbmembmaster.addr_phone)),'') + ' ' + isnull(rtrim(ltrim(mbmembmaster.addr_mobilephone)),'')  as REFMEMBER_TEL
                      from mbmembmaster
      left join mbmembcontact on ltrim(rtrim(mbmembmaster.coop_id)) = ltrim(rtrim(mbmembcontact.coop_id)) and ltrim(rtrim(mbmembmaster.member_no)) = ltrim(rtrim(mbmembcontact.member_no))
      left join mbucfdistrict on mbmembmaster.amphur_code = mbucfdistrict.district_code
      left join mbucfprovince on mbmembmaster.province_code = mbucfprovince.province_code
      left join mbucfmembgroup on mbmembmaster.coop_id = mbucfmembgroup.coop_id and mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code
      left join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code                
                     where (mbmembmaster.member_no = {1}) and ( mbmembmaster.coop_id = {0} )";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopControl, refmemb_no);
                        Sdt dt2 = WebUtil.QuerySdt(sql);
                        if (dt2.Next())
                        {
                            DESCRIPTION = dt2.GetString("DESCRIPTION");
                            REFMEMBER_ADDRESS = dt2.GetString("REFMEMBER_ADDRESS");
                            REFMEMBER_TEL = dt2.GetString("REFMEMBER_TEL");
                        }

                        if (dsList.DATA[row].REFMEMBER_FLAG == "")
                        {
                            dsList.DATA[row].REFMEMBER_FLAG = "1";

                        }

                        dsList.DATA[row].DESCRIPTION = DESCRIPTION ;
                        dsList.DATA[row].REFMEMBER_ADDRESS = REFMEMBER_ADDRESS;
                        dsList.DATA[row].REFMEMBER_TEL = REFMEMBER_TEL;
                        dsList.DATA[row].REFMEMBER_NO = refmemb_no;


                        //REFMEMBER_FLAG = dsList.DATA[0].REFMEMBER_FLAG;
                        //SEQ_NO = dsList.DATA[0].SEQ_NO;
                        //dsList.RetrieveMemb_no(refmemb_no, seq_no);
                       //LtServerMessage.Text = WebUtil.CompleteMessage("ข้อมูลสำเร็จ");
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ข้อมูลผิดพลาด");
                    }
                
            }
            else if (eventArg == PostNewRow)
            {

                //dsList.InsertRow(1);
                String refmember_no = "";
                String refmember_flag = "";
                int row = dsList.RowCount; //นับแถว
                //String row_seq = dsList.DATA[row - 1].SEQ_NO;
                dsList.DATA[row - 1].SEQ_NO = Convert.ToString(row);
                //refmember_flag = dsList.DATA[row - 1].REFMEMBER_FLAG;
                //refmember_flag = dsList.Data.("REFMEMBER_FLAG");
                dsList.DATA[0].REFMEMBER_FLAG = refmember_flag;
            }
            else if (eventArg == PostDelRow)
            {
                String ls_memb = "";
                int row = dsList.GetRowFocus();
                String member_no = dsMain.DATA[0].MEMBER_NO;
                String seq_no = dsList.DATA[row].SEQ_NO;

                //dsList.DeleteRow(row);
                try
                {
                    ls_memb = @"DELETE FROM mbmembcontact WHERE coop_id={0} and  member_no={1} and seq_no={2}";
                    ls_memb = WebUtil.SQLFormat(ls_memb, state.SsCoopId, member_no, seq_no);
                    WebUtil.ExeSQL(ls_memb);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    dsList.RetrieveData(member_no);
                    SaveWebSheet();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                }
            }
            //else if (eventArg == PostMem)
            //{
            //    member();
            //    //cal_sum_recv();
            //}
            this.SetOnLoadedScript(" parent.Setfocus();");
        
        }

        public void SaveWebSheet()
        {
            try
            {
                String member_no = dsMain.DATA[0].MEMBER_NO;            
                String refmember_flag = "";
                String seq_no = "";
                String description = "";
                String refmember_address = "";
                String refmember_tel = "";
                String refmember_no = "";
                String refmember_relation = "";
                String member_address = "";
                String member_workaddress = "";
                String member_tel = "";
                String member_worktel = "";
                String sqlStr = "";
                int row = dsList.RowCount;
                int num = 0;

                string sql = @"DELETE FROM mbmembcontact WHERE coop_id={0} and  member_no={1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
                WebUtil.ExeSQL(sql);

                for  (int i = 0; i < row ; i++){
                    refmember_flag = dsList.DATA[i].REFMEMBER_FLAG;
                    seq_no = dsList.DATA[i].SEQ_NO;
                    description = dsList.DATA[i].DESCRIPTION;
                    refmember_address = dsList.DATA[i].REFMEMBER_ADDRESS;
                    refmember_tel = dsList.DATA[i].REFMEMBER_TEL;
                    refmember_no = dsList.DATA[i].REFMEMBER_NO;
                    refmember_relation = dsList.DATA[i].REFMEMBER_RELATION;
                    member_address = dsList.DATA[i].MEMBER_ADDRESS;
                    member_workaddress = dsList.DATA[i].MEMBER_WORKADDRESS;
                    member_tel = dsList.DATA[i].MEMBER_TEL;
                    member_worktel = dsList.DATA[i].MEMBER_WORKTEL;
                    num = i + 1;
                    try
                    {                        
                        sqlStr = @"INSERT INTO mbmembcontact 
                        (coop_id,member_no,refmember_flag,description,refmember_address,refmember_tel,refmember_no,refmember_relation,member_address,member_workaddress,member_tel,member_worktel,seq_no)
                        VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},'1')";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, refmember_flag,  description, refmember_address, refmember_tel, refmember_no,refmember_relation,member_address,member_workaddress,member_tel,member_worktel);
                        WebUtil.ExeSQL(sqlStr);
                    }
                    catch { }
                    

                }               

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                dsMain.ResetRow();
                dsList.ResetRow();
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }
          

        public void WebSheetLoadEnd()
        {

           
        }

        //public void member()
        //{
        //    int row = dsList.RowCount;
        //    string MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
        //    string REFMEMBER_FLAG = "";
        //    string REFMEMBER_NO = "";
        //    for (int i = 0; i < row; i++)
        //    {
        //        REFMEMBER_FLAG = dsList.DATA[i].REFMEMBER_FLAG;
        //        if (REFMEMBER_FLAG == "1")
        //        {
        //            MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
        //        }
        //    }
        //    dsList.DATA[0].REFMEMBER_NO = REFMEMBER_NO;
        //}

        //public void cal_sum_recv()
        //{
        //    int row = dsList.RowCount;
        //    decimal SUMMTHEXPENSE_AMT = 0;
        //    string SIGN_FLAG = "";
        //    for (int i = 0; i < row; i++)
        //    {
        //        SIGN_FLAG = dsList.DATA[i].SIGN_FLAG;
        //        if (SIGN_FLAG == "1")
        //        {
        //            SUMMTHEXPENSE_AMT += dsList.DATA[i].MTHEXPENSE_AMT;
        //        }
        //    }
        //    dsSum.DATA[0].SUM_RECV = SUMMTHEXPENSE_AMT;
        //}

//        public void update_numrow() 
//        {
//        String member_no = dsMain.DATA[0].MEMBER_NO;
//        int row = dsList.RowCount;
//        int seq_no = 0;
//        for (int i = 0; i < row; i++) {
//            int _cal = i + 1;
//            seq_no = Convert.ToInt32(dsList.DATA[i].SEQ_NO);
//            if(_cal != seq_no){
//                try
//                {
//                    //เรียงลำดับ
//                    String sqlStr = @" UPDATE mbmembmthexpense set seq_no={3}
//                    WHERE coop_id={0} AND member_no={1} AND seq_no ={2}";
//                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no, seq_no, _cal);
//                    WebUtil.ExeSQL(sqlStr);
//                }
//                catch (Exception ex)
//                {
//                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
//                } 


//            }

//         }
        
//        }
    }
}