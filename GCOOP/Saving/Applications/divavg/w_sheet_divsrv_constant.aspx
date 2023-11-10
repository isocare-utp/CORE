<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_constant.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_constant" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postRefresh%>
    <%=postNewClear%>
    <%=postRefresh%>
    <script type="text/javascript">
        function OnDwMainClick(s, r, c) {
            if (c == "lockproc_flag") {
                Gcoop.CheckDw(s, r, c, "lockproc_flag", 1, 0);
                postRefresh();
            }
        }

        function MenubarNew() {
            postNewClear();
        }


        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

        <br />
        <table style="width: 100%;">
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_year" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_divsrv_constant" LibraryList="~/DataWindow/divavg/divsrv_constant.pbl"
                            ClientEventItemChanged="OnDwMainItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" Height="300px" 
                        ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" 
    DataWindowObject="d_divsrv_constant_rate" LibraryList="~/DataWindow/divavg/divsrv_constant.pbl" 
                            ClientEventClicked="OnDwMainClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="Hd_row" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
