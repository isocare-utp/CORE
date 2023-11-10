<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_replication_detail.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_replication_detail" %>

<%@ Register Src="w_sheet_am_replication_detail_ctrl/DsDetail.ascx" TagName="DsDetail"
    TagPrefix="uc1" %>
<%@ Register Src="w_sheet_am_replication_detail_ctrl/DsSpare.ascx" TagName="DsSpare"
    TagPrefix="uc2" %>
<%@ Register Src="w_sheet_am_replication_detail_ctrl/DsMain.ascx" TagName="DsMain"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_try") {
                PostTry();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <span>ฐานข้อมูลสำรอง</span>
    <uc3:DsMain ID="dsMain" runat="server" />
    <br />
    <table style="width: 700px;">
        <tr>
            <td width="50%">
                ฐานข้อมูลหลัก ข้อมูลล่าสุด
                : จำนวนเรคคอร์ด</td>
            <td width="50%">
                ฐานข้อมูลสำรอง ข้อมูลล่าสุด
                : จำนวนเรคคอร์ด</td>
        </tr>
        <tr>
            <td width="50%">
                <uc1:DsDetail ID="dsDetail" runat="server" />
            </td>
            <td>
                <uc2:DsSpare ID="dsSpare" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
