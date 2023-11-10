<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dep_edit_sliptofrom.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_sliptofrom_ctrl.ws_dep_edit_sliptofrom" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
       function Validate() {
            var alertstr = "";                     
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "choose_flag") == 1) {
                    alertstr = "true";                    
                }
            }            
            if (alertstr != "") {
                return confirm("ยืนยันการแก้ไข");
            } else {
                alert('กรุณาเลือกรายการที่จะทำรายการ!!'); return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td >
                <uc1:DsMain ID="dsMain" runat="server" Width="100%"/>
            </td>
            <td>
                <asp:Button ID="BtSearch" runat="server" Text="ค้นหา" Width="80px" Height="80px"
                    OnClick="BtSearch_Click" />
            </td>
        </tr>
        <tr><uc2:DsList ID="dsList" runat="server"/></tr>
    </table>
</asp:Content>