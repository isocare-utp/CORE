<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_vc_generate_wizard.aspx.cs"
    Inherits="Saving.Applications.account.w_dlg_vc_generate_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
  
    <script type="text/javascript">
    function OnDwWizardItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if(c == "voucher_tdate")
        {
        s.SetItem(1, "voucher_tdate", v );
        s.AcceptText();
        s.SetItem(1, "voucher_date", Gcoop.ToEngDate(v));
        s.AcceptText(); 
        }
        return 0;
    }
    
    function Validate()
    {
    
    }
    function OnButtonBackClick()
    {
        javascript:window.history.back();
    }
    
   
  
    
  
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                    <asp:Panel ID="Panel1" runat="server" Height="260px" 
        Width="430px" BorderStyle="Inset">
                        <dw:WebDataWindowControl ID="Dw_wizard" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_vc_vcgen_wizard_vcdate" LibraryList="~/DataWindow/account/vc_genwizard_vcdate.pbl"
                            ClientEventItemChanged="OnDwWizardItemChange" ClientFormatting="True" 
                            Height="250px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td colspan="3" valign="top">
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Width="396px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="Button1" type="button" value="ย้อนกลับ" 
                            onclick="OnButtonBackClick()"/>
<%--                        <asp:Button ID="B_back" runat="server" Text="&lt; ย้อนกลับ" 
                            UseSubmitBehavior="False" Width="75px" onclick="window.back();" />--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="B_next" runat="server" onclick="B_next_Click" Text="ต่อไป &gt;" 
                            UseSubmitBehavior="False" Width="75px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="HdSys_App" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
    </p>
</asp:Content>
