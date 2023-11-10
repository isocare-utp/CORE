<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paytrnbank_cancelapv.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paytrnbank_cancelapv" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDetail%>
    <%=checkAll%>

    <script type="text/javascript">
        function Validate(){
            return confirm("ต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_ok"){
                postDetail();
            }
        }
        
        function OnDwDetailCliked(sender, rowNumber, objectName){
            Gcoop.CheckDw(sender,rowNumber,objectName,"paytrnbank_status",-9,1);
        }
        
        function OnDwMainItemChanged(sender, rowNumber, colunmName, newValue){
            if(colunmName == "start_tdate"){
                objDwMain.SetItem(rowNumber,colunmName,newValue);
                objDwMain.AcceptText();
                return 0;
            } 
        }
        
        function ClickCheckAll(){
            if(objDwDetail.RowCount() > 1){
                checkAll();
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_appv_master"
        LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <table width="100%" border="0">
        <tr>
            <td width="58%">
            </td>
            <td>
                <asp:CheckBox ID="chkAll" runat="server" Text="เลือกทั้งหมด" onclick="ClickCheckAll()" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Vertical">
        <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            DataWindowObject="d_fn_paytrnbank_cancelapv_list" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
            ClientFormatting="True" ClientEventClicked="OnDwDetailCliked">
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>
