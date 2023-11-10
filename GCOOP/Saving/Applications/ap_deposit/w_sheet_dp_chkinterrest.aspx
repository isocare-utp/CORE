<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_chkinterrest.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_chkinterrest" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%=initJavaScript%>
  <%=postNewAccount%>
  
  <%=postRecalInterrest%>
 <script type="text/javascript">
     function OnDwMainButtonClicked(s, row, c)
     {
         if (c == "calint_btn") {
             s.AcceptText();
             postRecalInterrest();
         }
     }
     function OnDwMainItemChanged(s, row, c, v) {
         if (c == "deptformat") {
             // s.SetItem(1, "deptformat", v);

             objDwMain.SetItem(1, "deptformat", Gcoop.Trim(v));
             var coopid = s.GetItem(1, "deptcoop_id");
             var accNo = s.GetItem(1, "deptformat");
             s.AcceptText();
             NewAccountNo(coopid, accNo);
             return 0;
         }
     }
     function OnDwHeadItemChanged(s, row, c, v) {
         if (c == "operation") {
             // s.SetItem(1, "deptformat", v);
             //alert(v);
             objDwHead.SetItem(1,"operation",v);
             s.AcceptText();
             //postOperation();
             return 0;
         }
     }
      function NewAccountNo(coopid, accNo) {
            objDwMain.SetItem(1, "deptformat", Gcoop.Trim(accNo));
            Gcoop.GetEl("HfCoopid").value = coopid + "";
            objDwMain.AcceptText();

            if (Gcoop.GetEl("HfCheck").value == "True") {
                setCoopname();
            }
            else if (Gcoop.GetEl("HfCheck").value == "False") {
                postNewAccount();
            }
        }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
  <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
  <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_dp_calint_date"
            LibraryList="~/DataWindow/ap_deposit/dp_interrestbonus.pbl" ClientScriptable="True"
            AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" 
            ClientEventItemChanged="OnDwHeadItemChanged"
            ClientEventItemError="OnError" AutoRestoreContext="False" 
            ClientFormatting="True">
  </dw:WebDataWindowControl>
  <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_cal_interrest"
            LibraryList="~/DataWindow/ap_deposit/dp_interrestbonus.pbl" ClientEventButtonClicked="OnDwMainButtonClicked"
            ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
            ClientEventItemChanged="OnDwMainItemChanged" ClientEventItemError="OnError" AutoRestoreContext="False">
        </dw:WebDataWindowControl>
  <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_dp_slip_item"
            LibraryList="~/DataWindow/ap_deposit/dp_interrestbonus.pbl" 
            ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
            ClientEventItemError="OnError" AutoRestoreContext="False">
   </dw:WebDataWindowControl>

     <asp:HiddenField ID="HdLastFocus" runat="server" />
    <asp:HiddenField ID="HdPrintBook" runat="server" />
    <asp:HiddenField ID="HdPrintSlip" runat="server" />
    <asp:HiddenField ID="HdDayPassCheq" runat="server" />
    <asp:HiddenField ID="HdItemSelectRow" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdNewAccountNo" runat="server" />
    <asp:HiddenField ID="HdPrintFlag" runat="server" Value="false" />
    <asp:HiddenField ID="HdRequireCalInt" runat="server" Value="false" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdCheckApvAlert" runat="server" Value="false" />
    <asp:HiddenField ID="HdProcessId" runat="server" />
    <asp:HiddenField ID="HdAvpCode" runat="server" />
    <asp:HiddenField ID="HdItemType" runat="server" />
    <asp:HiddenField ID="HdAvpAmt" runat="server" />
    <asp:HiddenField ID="HdIsInsertCheque" runat="server" />
    <asp:HiddenField ID="HdDwChequeRow" runat="server" />
    <asp:HiddenField ID="hfSave" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="HfCheck" runat="server" />

</asp:Content>
