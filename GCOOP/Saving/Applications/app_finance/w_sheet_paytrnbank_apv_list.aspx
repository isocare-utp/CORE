<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paytrnbank_apv_list.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paytrnbank_apv_list" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDetail%>
    <script type="text/javascript">

        function Validate(){
            return confirm("ต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_ok"){
                postDetail();
            }
        }
        
        function OnDwMainItemChanged(sender, rowNumber, colunmName, newValue){
            if(colunmName == "start_tdate"){
                objDwMain.SetItem(rowNumber,colunmName,newValue);
                objDwMain.SetItem(rowNumber,"start_date",Gcoop.ToEngDate(newValue));
                objDwMain.AcceptText();
            }
            else if(colunmName == "end_tdate"){
                objDwMain.SetItem(rowNumber,colunmName,newValue);
                objDwMain.SetItem(rowNumber,"end_date",Gcoop.ToEngDate(newValue));
                objDwMain.AcceptText();
            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_fn_paytrnbank_appv_master" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
        ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_appv_list"
        LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
        ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
