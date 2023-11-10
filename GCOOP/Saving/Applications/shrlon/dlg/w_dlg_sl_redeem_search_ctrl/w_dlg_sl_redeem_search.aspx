<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" 
    CodeBehind="w_dlg_sl_redeem_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_redeem_search_ctrl.w_dlg_sl_redeem_search" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         var dsList = new DataSourceTool();
         var dsCriteria = new DataSourceTool();         

         function OnDsListClicked(s, r, c, v) {
             if (c == "mrtgmast_no" || c == "member_no" || c == "memb_name" || c == "memb_surname") {                
                 dsList.SetRowFocus(r);
                 var mrtgmast_no = dsList.GetItem(r, "mrtgmast_no");
                 var member_no = dsList.GetItem(r, "member_no");
                 var memb_name = dsList.GetItem(r, "memb_name");
                 var memb_surname = dsList.GetItem(r, "memb_surname");
                 var mortgage_date = dsList.GetItem(r, "mortgage_date");
                 var mortgagesum_amt = dsList.GetItem(r, "mortgagesum_amt");                                     
                 try {
                     window.opener.GetValueFromDlg(mrtgmast_no, member_no, memb_name, memb_surname, mortgage_date, mortgagesum_amt);
                     window.close();
                 } catch (err) {
                     parent.GetValueFromDlg(mrtgmast_no, member_no, memb_name, memb_surname, mortgage_date, mortgagesum_amt);
                     window.close();
                 }
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
      <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <div align="center">
        <table>
            <tr>
                <td>
                    <uc1:DsCriteria ID="dsCriteria" runat="server" />
                </td>
                <td>
                    <asp:Button ID="BtSearch" runat="server" Text="ค้น" Width="60px" Height="60px" OnClick="BtSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <uc2:DsList ID="dsList" runat="server" />
        <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px"></asp:Label>
    </div>
    <br />
</asp:Content>
