<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_closemonth.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_closemonth" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=CloseMonth%>

    <script type="text/javascript">
    
    function Validate(){
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    
    function OnMainButtonClick(sender, rowNumber, buttonName)
    {
        if(buttonName == "b_closemonth")
        {
            var isConfirm = confirm("ต้องการปิดสิ้นเดือน ใช่หรือไม่?");
            if(isConfirm == true){
            CloseMonth();
            }
        }
    }
    
    function OnMainItemChange(s, row, c, v)
    {
        if(c == "proc_tyear")
        {
            objdw_main.SetItem(row,c,v);
            objdw_main.AcceptText();
        }
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" 
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
        ClientScriptable="True" DataWindowObject="d_dp_post_wizard_month" 
        LibraryList="~/DataWindow/ap_deposit/dp_closemonth.pbl" ClientEventButtonClicked="OnMainButtonClick" ClientEventItemChanged="OnMainItemChange">
    </dw:WebDataWindowControl>
</asp:Content>
