<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_loan_pause.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_pause" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript %>
<%=initLnPause %>
<script type="text/javascript">
    function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
       
        if (columnName == "member_no") {
            objdw_main.AcceptText();
            objdw_main.SetItem(rowNumber, columnName, newValue);
            initLnPause();
        } else if (columnName == "checkall_flag") {
            objdw_main.AcceptText();
            objdw_main.SetItem(rowNumber, columnName, newValue);
            if (newValue == 1) {
                newValue = "PUS";
            } else {
            newValue = "NML";
            }
        var rowCount = objdw_list.RowCount();
       
            for (var i = 0; i <= rowCount; i++) {
                objdw_list.SetItem(i, "operate_code", newValue)

            }
        }
    }
    function Validate(){
        objdw_main.AcceptText();
        objdw_list.AcceptText();
        var NMLalert=false;
        var PUSalert=false;

        var rowCount = objdw_list.RowCount();
        for(var i=0;i<=rowCount;i++){
            var rows = i+1;
            var oprNew = objdw_list.GetItem(rows,"operate_code");
            var oprOld = objdw_list.GetItem(rows,"operate_old")
            if(oprNew!=oprOld){
                if((oprOld=="NML")&&(oprNew=="PUS")){
                    if(objdw_main.GetItem(1,"pauseloan_cause")=="" || objdw_main.GetItem(1,"pauseloan_cause")== null){
                        PUSalert = true;
                    }
                }else if((oprOld=="PUS")&&(oprNew=="NML")){
                    if(objdw_main.GetItem(1,"continue_cause")=="" || objdw_main.GetItem(1,"continue_cause")== null){
                       // NMLalert = true;
                    }
                }
            }
        }
        if(PUSalert){
            alert("กรุณากรอกเหตุผล การห้ามกู้");
            return false;
        }else if(NMLalert){
            alert("กรุณากรอกเหตุผล การให้กู้");
            return false;
        }else if(PUSalert && NMLalert){
            alert("กรุณากรอกเหตุผล การห้ามกู้ และ ให้กู้");
            return false;
        }else{
            return true;
        }
        
    }
    function OnDwListClicked(sender, rowNumber, objectName){
        if(objectName == "operate_code"){
            Gcoop.CheckDw(sender, rowNumber, objectName, "operate_code", "NML", "PUS");
        }
    }

    function MenubarOpen() {
        Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
    }

    function GetMemDetFromDlg(memberno) {
        objdw_main.SetItem(1, "member_no", memberno);
        objdw_main.AcceptText();
        initLnPause();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_loansrv_req_loanpause"
        LibraryList="~/DataWindow/shrlon/sl_slipreqall.pbl"
        AutoRestoreContext="False" AutoRestoreDataCache="True" 
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
        ClientScriptable="True" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="MenubarOpen">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_loansrv_req_loanpausedet"
        LibraryList="~/DataWindow/shrlon/sl_slipreqall.pbl"
        AutoRestoreContext="False" AutoRestoreDataCache="True" 
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
        ClientScriptable="True" ClientEventClicked="OnDwListClicked">
    </dw:WebDataWindowControl>
</asp:Content>
