<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_fin_finitemtype.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_finitemtype_ctrl.ws_fin_finitemtype" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsList = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function SheetLoadComplete() {
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "CONTRACT_TYPE") {
                PostOnTractType();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
