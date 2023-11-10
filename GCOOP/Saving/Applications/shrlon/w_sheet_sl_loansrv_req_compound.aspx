<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_loansrv_req_compound.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_loansrv_req_compound" Title="Untitled Page" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=itemChangedReload %>
    <%=callInitCompound%>
    <%=calMonth %>
<script type="text/javascript">
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล ");
    }
    function ItemChanged(s, r, c, v){
        if(c == "member_no"){
            objdw_head.SetItem(1, c, v);
            objdw_head.AcceptText();
            callInitCompound();
        }
    }
    function ItemChangedShare(s, r, c, v){
        if( c=="comp_period"){
            objdw_share.SetItem(r, c, v);
            objdw_share.AcceptText();
            calMonth();
        }
    } 
    function ItemChangedLoan(s, r, c, v){
        if( c=="comp_period"){
            objdw_loan.SetItem(r, c, v);
            objdw_loan.AcceptText();
            calMonth();
        }
    }
    function OnEventClick(s, r, c){
            if(c == "comp_cancelflag"){
                Gcoop.CheckDw(s, r, c, "comp_cancelflag", 1, 0);
            }else if(c == "operate_flag"){
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td valign="top" width="30%">
                <div>
                    <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_loansrv_req_compound"
                        LibraryList="~/DataWindow/shrlon/sl_loansrv_req_compound.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        TabIndex="1" ClientFormatting="True" ClientEventItemChanged="ItemChanged">
                    </dw:WebDataWindowControl>
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div>
                    <asp:Label ID="Label2" runat="server" Text="รายละเอียดการขอผ่อนผันหุ้น" Font-Bold="True"
                        Font-Size="10pt"></asp:Label>
                    <dw:WebDataWindowControl ID="dw_share" runat="server" DataWindowObject="d_sl_req_compounddets_shr"
                        LibraryList="~/DataWindow/shrlon/sl_loansrv_req_compound.pbl;~/DataWindow/shrlon/cmcomddw.pbl"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" Height="150px" Width="740px" ClientEventClicked="OnEventClick"
                        TabIndex="200" ClientFormatting="True" ClientEventItemChanged="ItemChangedShare">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" width="30%">
            <asp:Label ID="Label3" runat="server" Text="รายละเอียดการขอผ่อนผันหนี้" Font-Bold="True"
                        Font-Size="10pt"></asp:Label>
                <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_loansrv_req_compounddet"
                    LibraryList="~/DataWindow/shrlon/sl_loansrv_req_compound.pbl"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientScriptable="True"  TabIndex="500"  Height="200px" Width="740px"
                    ClientFormatting="True" ClientEventClicked="OnEventClick" ClientEventItemChanged="ItemChangedLoan">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdMemno" runat="server" />
</asp:Content>
