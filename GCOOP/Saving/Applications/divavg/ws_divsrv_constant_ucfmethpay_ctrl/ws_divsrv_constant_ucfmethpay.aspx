<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_divsrv_constant_ucfmethpay.aspx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ucfmethpay_ctrl.ws_divsrv_constant_ucfmethpay" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "b_delete") {
                dsList.SetRowFocus(r);
                PostDeleteRow();
            }
        }
        function OnDsListItemChanged(s, r, c, v) {

        }
        function SheetLoadComplete() {
            

        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
