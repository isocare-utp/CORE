<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_cm_report_list.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_cm_report_list" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table>
        <tr>
            <td>
                กลุ่มรายงาน
                <dw:WebDataWindowControl ID="d_cm_report_group" runat="server" DataWindowObject="d_cm_report_group"
                    LibraryList="~/DataWindow/App_finance/cm_report_list.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                รายชื่อรายงาน
                <dw:WebDataWindowControl ID="d_cm_report_detail" runat="server" DataWindowObject="d_cm_report_detail"
                    LibraryList="~/DataWindow/App_finance/cm_report_list.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
