<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_detail_contract.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.w_dlg_sl_detail_contract_ctrl.w_dlg_sl_detail_contract" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsData.ascx" TagName="DsData" TagPrefix="uc2" %>
<%@ Register Src="DsStatement.ascx" TagName="DsStatement" TagPrefix="uc3" %>
<%@ Register Src="DsCollateral.ascx" TagName="DsCollateral" TagPrefix="uc4" %>
<%@ Register Src="DsChgpay.ascx" TagName="DsChgpay" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsStatement = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }


        function DialogLoadComplete() {

            $(function () {
                var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
                $("#tabs").tabs({
                    active: tabIndex,
                    activate: function (event, ui) {
                        $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                    }
                });
            });

            for (var i = 0; i < dsStatement.GetRowCount(); i++) {
                var sign_flag = dsStatement.GetItem(i, "sign_flag");
                if (sign_flag > "0") {
                    dsStatement.GetElement(i, "cp_principal_dr").style.color = "#FF0000";
                    dsStatement.GetElement(i, "cp_interest_dr").style.color = "#FF0000";
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
     <br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
    <div align="center">
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">รายละเอียดสัญญา </a></li>
                <li><a href="#tabs-2">Statement</a></li>
                <li><a href="#tabs-3">หลักประกัน</a></li>
                <li><a href="#tabs-4">การส่งงวด</a></li>
            </ul>
            <div id="tabs-1">
                <uc2:DsData ID="dsData" runat="server" />
            </div>
            <div id="tabs-2">
                <uc3:DsStatement ID="dsStatement" runat="server" />
                <table style="width:90%; font-size: small">
                    <tr>
                        <td>LPM : ชำระรายเดือน </td>
                        <td>RPM : ยกเลิกรับชำระรายเดือน </td> 	
                        <td>LPX : ชำระหนี้พิเศษ </td>
                        <td>RPX : ยกเลิกชำระหนี้พิเศษ </td>
                    </tr>
                    <tr>
                        <td>LRC : รับเงินกู้ </td>
                        <td>RRC : ยกเลิกการรับเงินกู้  </td> 	
                        <td>LCL : หักกลบกู้สัญญาใหม่ </td>
                        <td>RCL : ยกเลิกหักกลบกู้สัญญาใหม่ </td>
                    </tr>
                    <tr>
                        <td>LTL : โอนหุ้นมาชำระหนี้ </td>
                        <td>RTL : ยกเลิกโอนหุ้นมาชำระหนี้ </td> 	
                        <td>LRT : คืนเงินต้น,ดอกเบี้ย </td>
                        <td>RRT : ยกเลิกคืนเงินต้น,ดอกเบี้ย </td>
                    </tr>
                    <tr>
                        <td>TLG : โอนหนี้ให้ผู้ค้ำ </td>
                        <td>RLG : ยกเลิกโอนหนี้ให้ผู้ค้ำ </td> 	
                        <td>TRG : รับโอนหนี้ค้ำประกัน </td>
                        <td>RRG : ยกเลิกรับโอนหนี้ค้ำประกัน </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-3">
                <uc4:DsCollateral ID="dsCollateral" runat="server" />
            </div>
            <div id="tabs-4">
                <uc5:DsChgpay ID="dsChgpay" runat="server" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" runat="server" Value="0" />
    <style type='text/css'>
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 860px;
            height:470px;
        }
    </style>
</asp:Content>
