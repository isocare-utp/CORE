<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_impexptxt.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_impexptxt" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            font-size: small;
            font-weight: bold;
        }
        .style3
        {
            height: 21px;
            font-weight: bold;
            font-size: small;
        }
        .style4
        {
            font-family: Tahoma;
        }
    </style>
    <%=postRetrieve %>>
    <%=postExport %>
    <%=postImport %>
    <script type="text/javascript">
        function OnDwmainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_retrieve") {
                sender.AcceptText();
                postRetrieve();
            }
            else if (buttonName == "b_export") {
                sender.AcceptText();
                postExport();
            }
            else if (buttonName == "b_import") {
                sender.AcceptText();
                postImport();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                IMP &amp; EXP ข้อมูล TextFile
                <asp:FileUpload ID="FileUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_kp_impexp_main"
                    LibraryList="~/DataWindow/keepingmonth/egat_kp_impexpdisk.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientScriptable="True" ClientValidation="False" ClientEventButtonClicked="OnDwmainButtonClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_data" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
                    DataWindowObject="d_kp_impexp_data" LibraryList="~/DataWindow/keepingmonth/egat_kp_impexpdisk.pbl">
                </dw:WebDataWindowControl>
                <%--<dw:WebDataWindowControl ID="dw_data1" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
                    DataWindowObject="d_kp_imptxt_data" LibraryList="~/DataWindow/keepingmonth/egat_kp_impexpdisk.pbl">
                </dw:WebDataWindowControl>--%>
            </td>
        </tr>
    </table>
</asp:Content>
