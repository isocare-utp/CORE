<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="ws_am_coopmaster.aspx.cs" Inherits="Saving.Applications.admin.ws_am_coopmaster_ctrl.ws_am_coopmaster" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool;

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล")
    }

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "province_code") {
            PostProvince();
        }
        else if (c == "district_code") {
            PostAmpur();
        }

    }

    function OnDsMainClicked(s, r, c) {
    }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
