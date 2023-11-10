<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="w_acc_yearclose_depreciation.aspx.cs" Inherits="Saving.Applications.account.w_acc_yearclose_depreciation" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseYear%>
    <%=postNewClear%>

    <script type="text/javascript">

        function CloseYear() {
            var present_account_ye = objDw_main.GetItem(1, "dp_year");
            var isconfirm = confirm("ยืนยันการยกยอดค่าเสื่อมประจำ ปี  " + present_account_ye);
            if (isconfirm) {
                postCloseYear();
            }
            return 0;
        }


        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }
    
    
    
    
    </script>

    <style type="text/css">
        .style1
        {
            width: 563px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <b>
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_acc_current_dp_year"
                            LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </b>
                </td>
            </tr>
            <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_acc_list_process_asset_3"
                        LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        Height="400px" Width="640px" ClientEventButtonClicked="OnEditClickButton">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input id="B_closeyear" type="button" value="ยกยอดค่าเสื่อม" onclick="CloseYear()" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>

