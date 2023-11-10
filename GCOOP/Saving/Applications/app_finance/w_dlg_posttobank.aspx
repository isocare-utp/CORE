<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_posttobank.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_dlg_posttobank" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postCheckAll%>
    <%=postBankProcess%>
    <%=postCancelBankProcess %>
    <%=postRetreiveData %>
    <%=postCheckAll%>

    <script type="text/javascript">

        function DwHeadButtonClicked(sender, row, oName) {
            objDwHead.AcceptText();
            postRetreiveData();
        }

        function DwHeadItemChanged(sender, row, col, nValue) {
            //DwThDate("entry_date", "entry_tdate", objDwHead, sender, row, col, nValue);          
            objDwHead.AcceptText();
        }

        function DwListItemChanged() {
            objDwList.AcceptText();
            postCheckAll();
        }

        function DwListButtonClicked(sender, row, oName) {
            if (oName == "b_postbank") {
                objDwList.AcceptText();
                postBankProcess();
            }
            else if (oName == "b_cancelpostbank") {
                objDwList.AcceptText();
                postCancelBankProcess();
            }
        }

        function ButtonClick() {
            objDwList.AcceptText();
            postBankProcess();
        }

        function DwListClick(sender, rowNumber, objectName) {
            if (objectName == "select_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "select_flag", 1, 0);
                objDwList.AcceptText();
            }
        }
      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <b>วันที่ข้อมูล</b>
    <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_fin_posttobank_head"
        LibraryList="~/DataWindow/App_finance/closeday.pbl" ClientScriptable="True" ClientEventButtonClicked="DwHeadButtonClicked"
        AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
        ClientEventItemChanged="DwHeadItemChanged" ClientFormatting="True">
    </dw:WebDataWindowControl>
    <hr />
    <table width="100%" border="0">
        <tr>
            <td width="30%">
                <b>รายการ</b>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="ผ่านรายการธนาคาร" Height="35px" Width="270px"
                    Font-Bold="True" Font-Size="Large" OnClientClick="ButtonClick()" UseSubmitBehavior="False" />
            </td>
            <td>
                <asp:CheckBox ID="CheckBoxAll" runat="server" onclick="DwListItemChanged()" Text="เลือกทั้งหมด" />
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_fin_posttobank_list"
        LibraryList="~/DataWindow/App_finance/closeday.pbl" ClientScriptable="True" ClientEventClicked="DwListClick"
        ClientEventItemChanged="DwListItemChanged" ClientEventButtonClicked="DwListButtonClicked"
        AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true">
    </dw:WebDataWindowControl>
</asp:Content>
