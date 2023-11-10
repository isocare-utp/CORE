<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sh_detail_share.aspx.cs" Inherits="Saving.Applications._global.w_dlg_sh_detail_share_ctrl.w_dlg_sh_detail_share" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsStatement.ascx" TagName="DsStatement" TagPrefix="uc1" %>
<%@ Register Src="DsPayment.ascx" TagName="DsPayment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

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
            <li><a href="#tabs-1">Statement</a></li>
            <li><a href="#tabs-2">การส่งงวด</a></li>
        </ul>
        <div id="tabs-1">
            <uc1:DsStatement ID="dsStatement" runat="server" />
        </div>
        <div id="tabs-2">
            <uc1:DsPayment ID="dsPayment" runat="server" />
        </div>
    </div>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <br />
    <table style="width: 100%; font-size: x-small">
        <tr>
            <td>
                &nbsp;&nbsp;B/F : ยกยอดมาต้นปี
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
                &nbsp;&nbsp;SPX : ซื้อหุ้นพิเศษ
            </td>
            <td>
                RPX : ยกเลิกซื้อหุ้นพิเศษ
            </td>
            <td>
                SPM : ส่งหุ้นประจำเดือน
            </td>
            <td>
                RPM : ยกเลิกส่งหุ้นประจำเดือน
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;STL : โอนหุ้นชำระหนี้
            </td>
            <td>
                RTL : ยกเลิกโอนหุ้นชำระหนี้
            </td>
            <td>
                SWD : ถอนหุ้น
            </td>
            <td>
                RWD : ยกเลิกถอนหุ้น
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;SSR : เก็บเกินจ่ายคืน
            </td>
            <td>
                RSR : ยกเลิกเก็บเกินจ่ายคืน
            </td>
            <td>
                STR : รับโอนหุ้นสมัครใหม่
            </td>
            <td>
                RTR : รับโอนหุ้นสมัครใหม่
            </td>
        </tr>
    </table>
</asp:Content>
