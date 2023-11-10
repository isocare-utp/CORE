<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_postto_pay.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_postto_pay" Title="Untitled Page" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 98px;
        }
        .style2
        {
            width: 367px;
        }
        .style3
        {
        }
    </style>
    
    
<%=initJavaScript%>
<%=postInsertRow%>
<%=postDeleteRow%>
<%=postChange%>
<script type ="text/javascript" >
    function OnDwDataClick(s,r,c)
    {
      Gcoop.CheckDw(s, r, c, "postto_flag", 1, 0);
        if(c == "postto_flag")
        {
           postChange();
        }
        return 0
    }
    
    function OnDwDataInsertRow()
    {
        postInsertRow();
    }
    
    function OnDwDataDeleteRow(s,r,b)
    {
        if(b == "b_del"){
            Gcoop.GetEl("HdRow").value = r + "";
            postDeleteRow();
        }
        return 0
    }
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="3" valign="top" class="style1">
                <asp:Panel ID="Panel1" runat="server" Height="89px" Width="731px">
                    <dw:WebDataWindowControl ID="Dw_data" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientScriptable="True" DataWindowObject="d_acc_post_pay" 
                        LibraryList="~/DataWindow/account/acc_post_pay.pbl" 
                        ClientEventButtonClicked="OnDwDataDeleteRow" 
                        ClientEventClicked="OnDwDataClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style3" colspan="2">
                &nbsp;
                <span class="linkSpan" onclick="OnDwDataInsertRow()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว&nbsp; </span>
            </td>
            <td>
                &nbsp;
                <asp:Button ID="B_ok" runat="server" Text="ตกลง" UseSubmitBehavior="False" 
                    Width="50px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="B_cancel" runat="server" Text="ยกเลิก" Width="50px" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td class="style2">
                &nbsp;<asp:HiddenField ID="HdRow" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
