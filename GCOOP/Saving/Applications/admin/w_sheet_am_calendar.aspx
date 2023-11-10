<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_calendar.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_calendar" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMonth%>
    <%=jsPostYear%>
    <%=jsPostChangeColor%>
    <%=jsPostClickDetail%>
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }

        function OnDwMainChange(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "month":
                    jsPostMonth();
                    break;
                case "year":
                    jsPostYear();
                    break;
            }
        }

        function OnDwMainClick(s, r, c) {
            if (c == "cell1" || c == "cell2" || c == "cell3" || c == "cell4" || c == "cell5" || c == "cell6" || c == "cell7" || c == "cell8" || c == "cell9" || c == "cell10" || c == "cell11" || c == "cell12" || c == "cell13" || c == "cell14" || c == "cell15" || c == "cell16" || c == "cell17" || c == "cell18" || c == "cell19" || c == "cell20" || c == "cell21" || c == "cell22" || c == "cell23" || c == "cell24" || c == "cell25" || c == "cell26" || c == "cell27" || c == "cell28" || c == "cell29" || c == "cell30" || c == "cell31" || c == "cell32" || c == "cell33" || c == "cell34" || c == "cell35" || c == "cell36" || c == "cell37" || c == "cell38" || c == "cell39" || c == "cell40" || c == "cell41" || c == "cell42") {
                Gcoop.GetEl("HdClick").value = c;
                jsPostChangeColor();
            }
        }

        function OnDwDetailClick(s, r, c) {
            if (c != "datawindow") {
                Gcoop.GetEl("HdRow").value = r;
                jsPostClickDetail();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td valign="top">
                <asp:Label ID="Label1" runat="server" Text="ปีปฏิทิน :" Font-Size="Medium"></asp:Label>
                &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                &nbsp;<asp:Button ID="Button1" runat="server" Text="ดึงข้อมูล" OnClick="Button1_Click" />
                &nbsp;<asp:Button ID="Button2" runat="server" Text="เพิ่มปีปฏิทิน" OnClick="Button1_Click2" />
                <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_am_calendar_detail"
                    LibraryList="~/DataWindow/admin/am_amappstatus.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientEventClicked="OnDwDetailClick">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="สีแดง:วันหยุด" ForeColor="Red" Font-Size="Small"></asp:Label>
                &nbsp;
                <asp:Label ID="Label3" runat="server" Text="สีดำ:วันทำงาน" Font-Size="Small"></asp:Label>
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_am_calendar"
                    LibraryList="~/DataWindow/admin/am_amappstatus.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientEventItemChanged="OnDwMainChange" ClientEventClicked="OnDwMainClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdClick" runat="server" Value="" />
    <asp:HiddenField ID="HdCellName" runat="server" Value="" />
    <asp:HiddenField ID="HdCellStart" runat="server" Value="" />
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
</asp:Content>
