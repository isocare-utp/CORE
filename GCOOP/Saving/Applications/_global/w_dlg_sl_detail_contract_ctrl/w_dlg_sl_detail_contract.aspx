<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_detail_contract.aspx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.w_dlg_sl_detail_contract" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>
<%@ Register Src="DsStatement.ascx" TagName="DsStatement" TagPrefix="uc1" %>
<%@ Register Src="DsCollateral.ascx" TagName="DsCollateral" TagPrefix="uc1" %>
<%@ Register Src="DsChgpay.ascx" TagName="DsChgpay" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsDetail = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function SheetLoadComplete() {

        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }

        }
        function MenubarOpen() {
            Gcoop.OpenSearchMemberNo();
        }
        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {
            if (c == "btn_search") {
                Gcoop.OpenSearchMemberNo();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsDetailItemChanged(s, r, c, v) {

        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsLoanClicked(s, r, c) {
            if (c == "member_no") {
                LoanMember();
            }
        }

        function OnClickInsertRow() {
            PostInsertRow();
        }
        function GetValueSearchMemberNo(member_no) {

            dsMain.SetItem(0, "member_no", member_no);

            PostMemberNo();
        }
        function DialogLoadComplete() {


        }
    </script>
  
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
   
   <div id="tabs">
        <ul>
            <li><a href="#tabs-1">รายละเอียดสัญญา</a></li>
            <li><a href="#tabs-2">Statement</a></li>
             <li><a href="#tabs-3">หลักประกัน</a></li>
              <li><a href="#tabs-4">การส่งงวด</a></li>
        
        </ul>
        <div id="tabs-1">
         <uc1:DsDetail ID="dsDetail" runat="server" />
        </div>
        <div id="tabs-2">
                 <uc1:DsStatement ID="dsStatement" runat="server" />
        </div>
        <div id="tabs-3">
        <uc1:DsCollateral ID="dsCollateral" runat="server" />
        </div>
        <div id="tabs-4">
            <uc1:DsChgpay ID="dsChgpay" runat="server" />
        </div>
  </div>
   <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
   
    </style>
    <script type="text/javascript" >
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <br />
        <table style="width: 100%; font-size: x-small" border="0">
                    <tr>
                        <td>
                           &nbsp; B/F : ยกยอดมาต้นปี
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; LPM : ชำระรายเดือน
                        </td>
                        <td>
                            RPM : ยกเลิกชำระรายเดือน
                        </td>
                        <td>
                            LPX : ชำระหนี้พิเศษ
                        </td>
                        <td>
                            RPX : ยกเลิกชำระหนี้พิเศษ
                        </td>
                    </tr>
                    <tr>
                        <td>
                           &nbsp; LRC : รับเงินกู้
                        </td>
                        <td>
                            RRC : ยกเลิกการรับเงินกู้
                        </td>
                        <td>
                            LCL : หักกลบกู้สัญญาใหม่
                        </td>
                        <td>
                            RCL : ยกเลิกหักกลบกู้สัญญาใหม่
                        </td>
                    </tr>
                    <tr>
                        <td>
                           &nbsp; LTL : โอนหุ้นมาชำระหนี้
                        </td>
                        <td>
                            RTL : ยกเลิกโอนหุ้นมาชำระหนี้
                        </td>
                        <td>
                            LRT : คืนเงินต้น,ดอกเบี้ย
                        </td>
                        <td>
                            RRT : ยกเลิกคืนเงินต้น,ดอกเบี้ย
                        </td>
                    </tr>
                    <tr>
                        <td>
                           &nbsp; TLG : โอนหนี้ให้ผู้ค้ำ
                        </td>
                        <td>
                            RLG : ยกเลิกโอนหนี้ให้ผู้ค้ำ
                        </td>
                        <td>
                            TRG : รับโอนหนี้ค้ำประกัน
                        </td>
                        <td>
                            RRG : ยกเลิกรับโอนหนี้ค้ำประกัน
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        
</asp:Content>