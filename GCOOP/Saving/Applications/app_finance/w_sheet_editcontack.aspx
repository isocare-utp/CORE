<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_editcontack.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_editcontack" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%
    Page_LoadComplete();
%>
<script type="text/javascript">

    function MenubarOpen() {
        mywindow = window.open("dlg/w_dlg_sl_extmember_search.aspx","mywindow",
        "width=650,height=500status=no,toolbar=no,menubar=no,location=no,resizable=no");
        mywindow.moveTo(100, 100);
    }
    function GetValueFromDlg(contack_no) {
        var str_temp = window.location.toString();
        var str_arr = str_temp.split("?", 2);
        window.location = str_arr[0] + "?contack_no=" + contack_no;
    }
    function MenubarSave(){
            if(confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")){
                objd_edit_contackmaster.Update();
                }
        }
    function MenubarNew(){
         if(confirm("ยืนยันการสร้างข้อมูลใหม่?")){
         var str_new = window.location.toString();
         var str_new2 = str_new.split("?", 2);
         window.location = str_new2[0];
                }
    }

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server" >
    <asp:Panel ID="Panel1" runat="server" Width="720px" Height="600px" ScrollBars="Auto"> 
        <dw:WebDataWindowControl ID="d_edit_contackmaster" runat="server"
        DataWindowObject="d_edit_contackmaster"  
            LibraryList="~/DataWindow/App_finance/editcontack.pbl" ClientScriptable="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
   
</asp:Content>
