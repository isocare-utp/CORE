<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_accconfrim.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_accconfrim" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสมาชิก</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var SlipNo = objdw_data.GetItem(rowNumber, "slip_no");
            var MemberNo = objdw_data.GetItem(rowNumber, "member_no");
            window.opener.GetFinSlipFromDlg(SlipNo, MemberNo);
            window.close();
        }
    </script>

</head>
<body>
<form id="form1" runat="server">
  
      
       <table >
        <tr>    
           <td>
                     <dw:WebDataWindowControl ID="Dw_cri" runat="server" ClientScriptable="True" DataWindowObject="d_dlg_finslipconfrm_search"
                     LibraryList="~/DataWindow/app_finance/finslip_spc.pbl">
                     </dw:WebDataWindowControl>  
           </td>  
           <td>
                <asp:Button ID="BtnSearch" runat="server" Text="ค้นหา" onclick="BtnSearch_Click" />
           </td>
       </tr>

        </table>
          <asp:Panel ID="Panel1" runat="server" Height="350" ScrollBars="Auto" Width="660"  Style="border: solid 1px black; overflow: auto;" BorderColor="Red">
          <div>
    <table >
           
        <tr>
            <td  >
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_dlg_accountconfirm"
                    LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" ClientEventClicked="selectRow"> 
                </dw:WebDataWindowControl>
            </td>
            
            
        </tr>
        
    </table>
    </div>
    </asp:Panel>
     <asp:HiddenField ID="hidden_search" runat="server" />
    </form>
    
</body>
</html>
