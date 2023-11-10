<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_const_lnucfloanobjective.aspx.cs" Inherits="Saving.Applications.shrlon_const.ws_sl_const_lnucfloanobjective_ctrl.ws_sl_const_lnucfloanobjective" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "loantype_code") {
                PostLoanObject();
            }
        }

        //function OnDsMainClicked(s, r, c) {
          //  if (c == "loantype_code") {
            //    PostLoanObject();
           // }
       // }

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "probitemtype") {
                PostProbitemtype();
            }
        }

        function SheetLoadComplete() {
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
    <uc2:DsMain ID="dsMain" runat="server" />
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
