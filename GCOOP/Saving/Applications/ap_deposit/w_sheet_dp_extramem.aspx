<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_extramem.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_extramem" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postMemberDetail%>
<%=FilterProvince%>
<%=FilterDistrict%>
<script type ="text/javascript">
    function Validate(){
        return confirm("ต้องการทึกข้อมูลใช่ หรือไม่?");
    }
    
    function MenubarNew(){
        var check = confirm("ต้องการเพิ่มข้อมูลใหม่ใช่ หรือไม่?");
        if(check == true){
        window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_dp_extramem.aspx";
        }
    }
    
    function MenubarOpen(){
        Gcoop.OpenDlg(550, 550, "w_dlg_dp_extmember_search.aspx", "");
    }
    
    function GetValueFromDlg(deptmem_id)
    {
        objDwMain.SetItem(1,"deptmem_id",deptmem_id);
        objDwMain.AcceptText();
        postMemberDetail();
    }
    
    function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
        if(columnName == "deptmem_province"){
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
            FilterProvince();
        }
        else if(columnName == "deptmem_district"){
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
            FilterDistrict();
        }
    }
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_extramem_detail"
        LibraryList="~/DataWindow/ap_deposit/dp_extramem.pbl" 
        ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
</asp:Content>
