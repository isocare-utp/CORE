<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_apvpopup_list_tks.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_apvpopup_list_tks" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=ShowAvpListDetail%>
    <%=ApvPermis%>

    <script type="text/javascript">
        function Validate(){
            return confirm("ต้องการอนุมัติ หรือไม่อนุมัติรายการ?");
        }
        
        function OnDwHeadButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_apv"){
                Gcoop.GetEl("HdRow").value = rowNumber.toString();
                var itemType = objDwHead.GetItem(rowNumber,"itemtypedesc");
                var accountNo = objDwHead.GetItem(rowNumber,"account_no");
                var nameReq = objDwHead.GetItem(rowNumber,"namereq");
                var queryString = "?itemType=" + itemType + "&accountNo=" + accountNo + "&nameReq=" + nameReq;
                Gcoop.OpenIFrame(430, 150, "w_dlg_dp_approve.aspx", queryString);
                //Gcoop.OpenDlg(430, 150, "w_dlg_dp_approve.aspx", queryString);
                objDwHead.AcceptText();
            }
        }
        
        function OnDwHeadClicked(sender, rowNumber, objectName){
            if(objectName == "datawindow" || rowNumber <= 0 || objectName == "cb_apv"){ 
                return 0;
            }
            var proccessId = objDwHead.GetItem(rowNumber,"proccessid");
            Gcoop.GetEl("HdProccessId").value = proccessId;
            ShowAvpListDetail();
            objDwHead.AcceptText(); 
        }
        
        function GetValueFromDlg(appr){
            Gcoop.GetEl("HdAppr").value = appr + "";
            ApvPermis();
//            if(appr == 1)
//            {
//                alert("อนุมัติเรียบร้อยแล้ว");
//            }
//            else(appr == 9)
//            {
//                alert("ไม่อนุมัติ");
//            }
        }
        
        function MenubarNew(){
            window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_apvpopup_list_tks.aspx";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />

       <%-- <asp:Label ID="Label1" runat="server" Text="ค้นหาทะเบียนสมาชิก"></asp:Label>
        <asp:TextBox ID="TxtMemId" runat="server" Width="173px"></asp:TextBox>
        <asp:Button ID="BtnSearch" runat="server" Text="ค้นหา" />--%>
        <asp:Button ID="BtnGetData" runat="server" Text="ดึงข้อมูล" OnClick="BtnGetData_Click" />
   
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="350px">
        <dw:WebDataWindowControl ID="DwHead" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" LibraryList="~/DataWindow/ap_deposit/dp_apvpopup_list_tks.pbl"
            ClientEventClicked="OnDwHeadClicked" DataWindowObject="d_apv_approve_list_tks"
            ClientEventButtonClicked="OnDwHeadButtonClicked" RowsPerPage="10">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_apv_approve_detail_tks" LibraryList="~/DataWindow/ap_deposit/dp_apvpopup_list_tks.pbl"
        ClientFormatting="True" ClientEventButtonClicked="OnDwIntRateButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdProccessId" runat="server" />
    <asp:HiddenField ID="HdwidthMax" runat="server" />
    <asp:HiddenField ID="HddeptMax" runat="server" />
    <asp:HiddenField ID="HdAppr" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>
