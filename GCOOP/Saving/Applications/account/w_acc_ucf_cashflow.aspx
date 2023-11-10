<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_cashflow.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_cashflow" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostGetAccid%>
    <script type="text/javascript">
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            if (!isconfirm) {
                return false;
            }
            else {
                return true;
            }
        }

        function GetAccidButtonClick(sender, row, bName) {
            Gcoop.GetEl("Hdrow").value = row + "";
            var accid_list = objDwmain.GetItem(row, "accid_list");
            if (accid_list == null) {
                accid_list = "";
            }
            Gcoop.OpenIFrame(950, 550, "w_dlg_select_account.aspx", "?acc_list=" + accid_list);
        }

        function GetAccount(acc_id) {
            Gcoop.GetEl("Hdacclist").value = acc_id;
            jsPostGetAccid();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="Dwmain" runat="server" ClientScriptable="True" DataWindowObject="d_acc_ucf_cashflow"
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" ClientEventButtonClicked="GetAccidButtonClick"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="Hdrow" runat="server" />
    <asp:HiddenField ID="Hdacclist" runat="server" />
</asp:Content>
