<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_setatm.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_setatm" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsRetrieve%>
    <%=jsAddATM%>
    <%=jsSaveATM%>
    <script type="text/JavaScript">
        function OnDwBranchButtonClicked(s, r, c) {
            switch (c) {
                case "b_del":
                    isconfirm = confirm("ยืนยันการลบข้อมูล");
                    if (!isconfirm) {
                        return false;
                    }
                    Gcoop.GetEl("HdRowDelBranch").value = r;
                    jsDelBranch();
                    break;
            }
        }

        function OnDwCoopButtonClicked(s, r, c) {
            switch (c) {
                case "b_del":
                    isconfirm = confirm("ยืนยันการลบข้อมูล");
                    if (!isconfirm) {
                        return false;
                    }
                    Gcoop.GetEl("HdRowDelCoop").value = r;
                    jsDelCoop();
                    break;
            }
        }

        function OnDwMasterCoopButtonClicked(s, r, c) {
            switch (c) {
                case "b_1":
                    jsSaveCoop();
                    break;
            }
        }

        function OnDwAddATMButtonClicked(s, r, c) {
            switch (c) {
                case "b_save":
                    jsSaveATM();
                    break;
            }
        }

        function AddATM() {
            jsAddATM();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <input id="ShifF5" type="button" value="เพิ่มหมายเลขตู้" onclick="AddATM()" style="height: 40px;
        width: 100px;" />
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atm_setatm"
        LibraryList="~/DataWindow/atm/dp_atm_setatm.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwAddATM" runat="server" DataWindowObject="d_atm_setatm_addatm"
        LibraryList="~/DataWindow/atm/dp_atm_setatm.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwAddATMButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdAddATM" runat="server" />
</asp:Content>

