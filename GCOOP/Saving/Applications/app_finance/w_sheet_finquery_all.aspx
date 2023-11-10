<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finquery_all.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_finquery_all" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postFinQuery %>
    <%=newClear%>

    <script type="text/javascript">
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }
        function OnDwChanged(s, r, c, v) {
            if (c == "as_userid") {
                SelectUser(Gcoop.Trim(v));
            }
        }
        function DwUserClicked(sender, row, oName) {
            if (oName == "b_user") {
                Gcoop.OpenIFrame(435, 250, "w_dlg_fin_showuser.aspx", "");
            }
        }
        function UpdateDw() {
            if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
                objd_finquery_userid.Update();
            }
        }

        function OnInsertRow() {
            objd_finquery_userid.InsertRow(objd_finquery_userid.RowCount() + 1);
        }

        function SelectUser(username) {
            objDwUser.SetItem(1, "as_userid", username);
            objDwUser.AcceptText();
            postFinQuery();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="ตรวจสอบเงินสด" ForeColor="#000099" Font-Bold="True"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                    <dw:WebDataWindowControl ID="DwUser" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        ClientScriptable="True" DataWindowObject="d_finquery_userid" Height="80px" LibraryList="~/DataWindow/app_finance/finquery.pbl"
                        Width="220px" HorizontalScrollBar="None" VerticalScrollBar="None" ClientEventButtonClicked="DwUserClicked"
                        ClientEventItemChanged="OnDwChanged" AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="รายละเอียดผู้ใช้" ForeColor="#000099"
                        Font-Bold="True" Font-Underline="True" Font-Size="Medium"></asp:Label>
                    <dw:WebDataWindowControl ID="DwUserDetail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" ClientScriptable="True" DataWindowObject="d_finquery_usedetail"
                        Height="80px" LibraryList="~/DataWindow/app_finance/finquery.pbl" Width="500px"
                        AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <table style="vertical-align: top" width="100%" border="0">
            <tr>
                <td style="vertical-align: top">
                    <asp:Label ID="Label3" runat="server" Text="รายรับ" ForeColor="#000099" Font-Bold="True"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="250px">
                        <dw:WebDataWindowControl ID="DwRecv" runat="server" DataWindowObject="d_finquery_listrecv"
                            LibraryList="~/DataWindow/app_finance/finquery.pbl" AutoRestoreContext="False">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td style="vertical-align: top">
                    <asp:Label ID="Label4" runat="server" Text="รายจ่าย" ForeColor="#000099" Font-Bold="True"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="250px">
                        <dw:WebDataWindowControl ID="DwPay" runat="server" DataWindowObject="d_finquery_listpay"
                            LibraryList="~/DataWindow/app_finance/finquery.pbl" AutoRestoreContext="False">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HdF" runat="server" />
</asp:Content>
