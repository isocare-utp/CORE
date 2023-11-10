<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="w_acc_ucf_dpyear.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_dpyear" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>

<script type ="text/javascript" >

    function MenubarNew() {
        
    }

 


</script> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
      <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
                        DataWindowObject="d_acc_dpyear" 
                        LibraryList="~/DataWindow/account/asset.pbl" 
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </td>
        </tr>
        <tr>
            <td>
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    </td>
            <td>
                    <asp:HiddenField ID="HdDelrow" runat="server" />
                    </td>
            <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</asp:Content>

