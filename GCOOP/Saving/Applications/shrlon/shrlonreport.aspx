<%@ Page Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="shrlonreport.aspx.cs" Inherits="Saving.Applications.shrlon.shrlonreport" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Repeater ID="RepeaterMenuSub" runat="server" DataSourceID="DataSourceMenuSub">
    <ItemTemplate>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#Eval("PageLink")%>'>
                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Name")%>' />
            </asp:HyperLink><br />
    </ItemTemplate>
</asp:Repeater>
<asp:ObjectDataSource ID="DataSourceMenuSub" runat="server" SelectMethod="GetMenuSub"
    TypeName="CommonLibrary.MenuSub" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="pagePermiss" SessionField="ss_pagepermiss"
            Type="Object" />
        <asp:SessionParameter DefaultValue="0" Name="menuGroup" SessionField="ss_menugroup"
            Type="Object" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
