<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_recconcile.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_recconcile" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsUploadFile%>
  
    <script type="text/javascript">

   
//        function Validate() {
//            objDwMain.AcceptText();
//            return confirm("ยืนยันการบันทึกรายการ");
//    
        function UploadFile() {
            jsUploadFile();
        }
      
   

     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   
 
    <asp:FileUpload ID="fiUpload" runat="server" />
    <input type="button" value="Update" onclick="UploadFile();" />
   
    <asp:HiddenField ID="HdRow_Detail" runat="server" Value="" />
    <asp:HiddenField ID="HdUpdate_Flag" runat="server" Value="false" />
    <asp:HiddenField ID="HdUpdate_Flag2" runat="server" Value="false" />
    <asp:HiddenField ID="HdInsert_Flag" runat="server" Value="false" />
    <asp:HiddenField ID="HdRef_Slipno" runat="server" Value="" />
    <asp:HiddenField ID="HdStart_Date" runat="server" Value="" />
    <asp:HiddenField ID="HdEnd_Date" runat="server" Value="" />
</asp:Content>
