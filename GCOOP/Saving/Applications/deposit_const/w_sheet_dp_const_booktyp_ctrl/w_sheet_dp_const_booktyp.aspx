<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_const_booktyp.aspx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_booktyp_ctrl.w_sheet_dp_const_booktyp" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_del") {
                if (confirm("ต้องการลบข้อมูลใช่หรือไม่")) {
                    dsMain.SetRowFocus(r);
                    Postdel();
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span> &nbsp; &nbsp;
    <uc1:DsMain ID="dsMain" runat="server" />
    
</asp:Content>
