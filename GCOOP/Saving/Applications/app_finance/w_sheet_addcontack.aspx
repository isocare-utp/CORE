<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_addcontack.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_addcontack" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postContact %>
    <%=postGetDistrict %>
    <%=postFormat %>
    <%=postGetBank %>
    <script type="text/javascript">

        function Validate() {
            Gcoop.GetEl("HfContact").value = 0;
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarOpen() {
            Gcoop.OpenIFrameExtend(530, 580, "w_dlg_sl_extmember_search.aspx", "");
        }

        function GetValueFromDlg(contack_no) {
            Gcoop.GetEl("HfContact").value = contack_no;
            objDwMain.SetItem(1, "contack_no", contack_no)
            objDwMain.AcceptText();
            postContact();
        }

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "contack_no") {
                objDwMain.SetItem(1, "contack_no", newValue)
                Gcoop.GetEl("HfContact").value = newValue;
                Gcoop.GetEl("Hfbank").value = newValue;
                objDwMain.AcceptText();
                postFormat();
            }
            else if (columnName == "province") {
                objDwMain.SetItem(1, "province", newValue)
                Gcoop.GetEl("HfProvince").value = newValue;
                objDwMain.AcceptText();
                postGetDistrict();
            }
            else if (columnName == "bank_code") {
                objDwMain.SetItem(1, "bank_code", newValue)
                Gcoop.GetEl("Hfbank").value = newValue;
                objDwMain.AcceptText();
                postGetBank();   //**
            }
        }

        function MenubarNew() {
            window.location = state.SsUrl + "Applications/app_finance/w_sheet_addcontack.aspx";
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="720px" ScrollBars="Auto">
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_contackmaster"
            LibraryList="~/DataWindow/App_finance/addcontack.pbl" ClientScriptable="True"
            ClientEventClicked="OnDwClick" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="DwMainItemChange">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HfContact" runat="server" />
    <asp:HiddenField ID="HfProvince" runat="server" />
    <asp:HiddenField ID="Hfbank" runat="server" />
</asp:Content>
