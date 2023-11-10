<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_reqchgdept.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_reqchgdept" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=postChgAcc%>
<%=postAmt%>

<script type ="text/javascript">

    function MenubarNew() {
        if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
            window.location = "";
        }
    }

    function MenubarOpen() {
        Gcoop.GetEl("HfCheck").value = "True";
        Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
    }

    function NewAccountNo(coopid, accNo) {
        parent.RemoveIFrame();
        objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
        Gcoop.GetEl("HfCoopid").value = coopid + "";
        Gcoop.GetEl("HfAccNo").value = accNo + "";
        objDwMain.AcceptText();
        postChgAcc();
    }
    function OnDwMainItemChanged(s, r, c, v) {
        if (c == "deptaccount_no") {
            Gcoop.GetEl("HfAccNo").value = v + "";
            postChgAcc();
        } else if (c == "deptmonth_newamt") {
            s.SetItem(r, c, v);
            s.AcceptText();
            postAmt();
        }

    }
    function SheetLoadComplete() {
        if (Gcoop.GetEl("HdIsPostBack").value == "true") {
            Gcoop.SetLastFocus("deptaccount_no_0");
            Gcoop.Focus();
        }
    }
    
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล?");
    }
   
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
       <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="dw_dp_reqchgdept"
                    LibraryList="~/DataWindow/ap_deposit/dp_reqsequest.pbl" 
                    ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True">
        </dw:WebDataWindowControl>
         <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_chgdeptmount"
                    LibraryList="~/DataWindow/ap_deposit/dp_reqsequest.pbl" 
                    ClientFormatting="True">
        </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="HfCheck" runat="server" />
    <asp:HiddenField ID="HfAccNo" runat="server" />
    <asp:HiddenField ID="HfAmt" runat="server" />
    <asp:HiddenField ID="HdFocusdeptmonth" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
</asp:Content>
