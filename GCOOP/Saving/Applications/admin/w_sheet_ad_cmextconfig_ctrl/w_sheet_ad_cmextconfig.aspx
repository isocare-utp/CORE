<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ad_cmextconfig.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_cmextconfig_ctrl.w_sheet_ad_cmextconfig" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    var dsList = new DataSourceTool();

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function OnDsListItemChanged(s, r, c, v) {
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


    <%--<input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />--%>
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
