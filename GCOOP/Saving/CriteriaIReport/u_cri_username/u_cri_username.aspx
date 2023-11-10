<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_username.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_username.u_cri_username" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();
    var dsList = new DataSourceTool();
    function OnDsMainItemChanged(s, r, c, v) {

    }

    function OnDsListClicked(s, r, c) {
        if (c == "user_name" || c == "full_name") {
            dsMain.SetItem(0, "user_name", dsList.GetItem(r, "user_name"));
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
