<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_approve.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_approve" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div style="padding-left:2em; color:blue;">
        <asp:RadioButtonList ID="ApvStatus" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" 
        Font-Size="Medium" AutoPostBack="true">
            <asp:ListItem Value="8" Selected="True"> รออนุมัติ </asp:ListItem>
            <asp:ListItem Value="1"> อนุมัติ </asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_deptapprove_list"
                    LibraryList="~/DataWindow/ap_deposit/dp_deptapprove.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
   
</asp:Content>
