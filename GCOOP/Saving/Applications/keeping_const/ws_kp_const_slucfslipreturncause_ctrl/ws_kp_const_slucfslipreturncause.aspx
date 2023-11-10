<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_const_slucfslipreturncause.aspx.cs" Inherits="Saving.Applications.keeping_const.ws_kp_const_slucfslipreturncause_ctrl.ws_kp_const_slucfslipreturncause" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                if (confirm("ยืนยันการลบข้อมูล")) {
                    dsList.SetRowFocus(r);
                    PostDel();
                }
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
