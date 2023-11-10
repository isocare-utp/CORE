<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_cmucfsalarybalance.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_cmucfsalarybalance_ctrl.w_sheet_sl_cmucfsalarybalance" %>

<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        
        var dsList = new DataSourceTool();
        
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "salarycode") {
                PostSalaryCode();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                PostDelRow();
            }
        }

        function OnClickNewRow() {
            PostNewRow();
        }
        
        function SheetLoadComplete() {
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>

    <uc1:DsList ID="dsList" runat="server" />

</asp:Content>
