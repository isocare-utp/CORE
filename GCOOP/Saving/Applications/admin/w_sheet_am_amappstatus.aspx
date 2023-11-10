<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_amappstatus.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amappstatus" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDwMainItemChanged(s, r, c, v) {
            return 0;
        }

        function MenubarNew() {
            window.location = "";
        }

        function OnDwMainClicked(s, r, c) {
            if (c == "work_tdate") {
                datePicker.PickDw(objdwMain, r, "work_date", c);
            } else if (c == "last_work_tdate") {
                datePicker.PickDw(objdwMain, r, "last_workdate", c);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Button ID="BtChangDate" runat="server" Text="เลื่อนวันทำการทั้งหมด" OnClick="BtChangDate_Click"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtOpenDate" runat="server" Text="เปิดวันทั้งหมด" OnClick="BtOpenDate_Click"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtOpenMonth" runat="server" Text="เปิดเดือนทั้งหมด" OnClick="BtOpenMonth_Click"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtOpenYear" runat="server" Text="เปิดปีทั้งหมด" OnClick="BtOpenYear_Click"
        UseSubmitBehavior="False" />
    <br />
    <dw:WebDataWindowControl ID="dwMain" runat="server" DataWindowObject="d_amappstatus"
        LibraryList="~/DataWindow/admin/am_amappstatus.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" ClientEventItemChanged="OnDwMainItemChanged" ClientEventClicked="OnDwMainClicked">
    </dw:WebDataWindowControl>
</asp:Content>
