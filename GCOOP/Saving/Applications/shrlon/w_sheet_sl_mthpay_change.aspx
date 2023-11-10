<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_mthpay_change.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_mthpay_change" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=JsPostMemberNo%>
    <%=JsPostOperateTypeShare%>
    <%=JsPostOperateTypeLoan%>
    <script type="text/javascript">
        function OnDwMainItemChanged(sender, row, col, nvalue) {
            sender.SetItem(row, col, nvalue);
            sender.AcceptText();
            if (col == "member_no") {
                JsPostMemberNo();
            }
           
        }
        function OnDwShareItemChanged(sender, row, col, nvalue) {
            sender.SetItem(row, col, nvalue);
            sender.AcceptText();
             if (col == "operate_type") {
                 JsPostOperateTypeShare();
             }
            else if (col == "newperiod_payment"){
                 var basepayment = sender.GetItem(row, "sharebase_amt");
                 var newpayment = sender.GetItem(row, "newperiod_payment");
                 if (basepayment > newpayment) {
                     alert("งวดส่งหุ้นน้อยกว่าหุ้นฐาน กรุณาตรวจสอบ");
                 }
             }
         }
         function OnDwLoanItemChanged(sender, row, col, nvalue) {
             sender.SetItem(row, col, nvalue);
             sender.AcceptText();
             if (col == "operate_type") {
                 Gcoop.GetEl("HdRowOfLoan").value = row;
                 JsPostOperateTypeLoan();
             }
             
         } 

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
     <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
     <table style="width: 100%;">
     <tr>
        <td>
              <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_mthpay_change"
              LibraryList="~/DataWindow/shrlon/sl_mthpay_change.pbl" ClientScriptable="True"
              AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
              ClientEventButtonClicked="OnButtonClick" ClientEventItemChanged="OnDwMainItemChanged">
              </dw:WebDataWindowControl>
              <br>
        </td>
        <td>
               <br>
        </td>
             <br>
    </tr>
    <tr>
        <td>
              <dw:WebDataWindowControl ID="dw_share" runat="server" DataWindowObject="d_sl_mthpay_change_share"
              LibraryList="~/DataWindow/shrlon/sl_mthpay_change.pbl" ClientScriptable="True"
              AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
              ClientEventItemChanged="OnDwShareItemChanged">
              </dw:WebDataWindowControl>
              <br>
        </td>
         <td>
               <br>
        </td>
       <br>
    </tr>
    <tr>
        <td>
              <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_sl_mthpay_change_loan"
              LibraryList="~/DataWindow/shrlon/sl_mthpay_change.pbl" ClientScriptable="True"
              AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
              ClientEventItemChanged="OnDwLoanItemChanged">
              </dw:WebDataWindowControl>  
        </td>
    </tr>
    </table>
    <asp:HiddenField ID="HdRowOfLoan" runat="server"/>
</asp:Content>
