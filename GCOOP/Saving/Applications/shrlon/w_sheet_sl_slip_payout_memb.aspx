<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_slip_payout_memb.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_slip_payout_memb" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInitDwMain%>
    <%=postNewClear%>

    <script type="text/javascript">
    function ForLoopMoney(){
        var fullTotal = 0;
        var total = 0;
        var i = 0;
        for(i = 1; i <= objDwDetail.RowCount(); i++){
            //alert("opf = " + i + " , " + objDwDetail.GetItem(i, "operate_flag"));
            if(objDwDetail.GetItem(i, "operate_flag") == 1){
                total = Gcoop.ParseFloat(objDwDetail.GetItem(i, "item_payamt"));
                fullTotal += total;
            }
        }
        objDwMain.SetItem(1, "payoutnet_amt", fullTotal);
        objDwMain.AcceptText();
    }
    
    function MenubarNew(){
        if(confirm("ยืนยันการล้างหน้าจอ")){
            postNewClear();
        }
    }
    
    function OnDwDetailClicked(s, r, c){
        Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
        if(c == "operate_flag" && r > 0){
            ForLoopMoney();
        }
        return 0;
    }
    
    function OnDwDetailItemChanged(s, r, c, v){
        return 0;
    }
    
    function OnDwMainItemChanged(s, r, c, v){
        if(c == "member_no"){
            var memNo = Gcoop.StringFormat(v, "000000");
            s.SetItem(r, c, memNo);
            s.AcceptText();
            postInitDwMain();
        }
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdIsPostBack").value == "false"){
            Gcoop.Focus("member_no_0");
        }
    }
    
    function Validate(){
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Text="การจ่ายเงินให้สมาชิก" Font-Bold="True"
        Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
        DataWindowObject="d_sl_payoutslip" LibraryList="~/DataWindow/shrlon/sl_slip_payout_memb.pbl"
        ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
    <br />
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="รายการจ่ายคืน" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" ForeColor="#0099CC"></asp:Label>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_sl_payoutslipdet"
        LibraryList="~/DataWindow/shrlon/sl_slip_payout_memb.pbl" ClientEventClicked="OnDwDetailClicked"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" ClientScriptable="True" ClientEventItemChanged="OnDwDetailItemChanged">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
</asp:Content>
