<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finquery.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_finquery" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postFinQuery %>
    <%--    <%=postInitUser%>--%>
    <%=newClear%>
    <%=postFinQuery%>
    <%=postProcess %>
    <script type="text/javascript">

        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }

        function OnDwChanged(sender, rowNumber, columnName, newValue) {
            sender.SetItem(rowNumber, columnName, newValue);
            if (columnName == "as_userid") {
                sender.AcceptText();
                postFinQuery();
            }
        }

        function DwUserClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_user") {
                Gcoop.OpenIFrame(500, 520, "w_dlg_fin_showuser.aspx", "");
            }
        }

        function SelectUser(username, full_name) {
            objDwUser.SetItem(1, "as_userid", username);
            objDwUser.AcceptText();
            postFinQuery();
        }

        function DwUserDetailButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_process") {
                postProcess();
            }
        }

        $(function () {
//            if ($('input[name="as_userid_0"]').val() == "") {
//                objDwUser.SetItem(1, "as_userid", "sajee");
//                setTimeout(function () { postFinQuery(); }, 5000)
//            } else {
//                setTimeout(function () { postProcess(); }, 60000)
//            }
        })

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
                        AutoRestoreDataCache="True" ClientScriptable="True" DataWindowObject="d_finquery_usedetail_old"
                        Height="80px" LibraryList="~/DataWindow/app_finance/finquery.pbl" Width="435px"
                        ClientEventButtonClicked="DwUserDetailButtonClick" AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
                <td>
                    <dw:WebDataWindowControl ID="DwProc" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        ClientScriptable="True" DataWindowObject="d_listchooseproc" Height="70px" LibraryList="~/DataWindow/app_finance/finquery.pbl"
                        AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="DwProcItemChanged">
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
    <%=outputProcess%>
</asp:Content>
