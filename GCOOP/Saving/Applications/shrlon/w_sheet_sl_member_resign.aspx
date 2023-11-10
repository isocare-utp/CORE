<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_member_resign.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_member_resign" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=memNoItemChange%>
    <%=memNoFromDlg %>
    <%=newClear %>

    <script type="text/javascript">
        function Validate() {
            objdw_sum.AcceptText();
            objdw_share.AcceptText();
            objdw_loan.AcceptText(); ;
            objdw_grt.AcceptText();
            objdw_head.AcceptText();
            objdw_deposit.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function showTabPage(sender, rowNumber, buttonName) {
            if (buttonName == "b_share") {
                document.getElementById("tab_1").style.visibility = "visible";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "hidden";
            } else if (buttonName == "b_deposit") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "visible";
            } else if (buttonName == "b_loan") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "visible";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "hidden";
            } else if (buttonName == "b_coll") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "visible";
                document.getElementById("tab_4").style.visibility = "hidden";
            }
        }
        function DwItemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_head.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "000000"));
                objdw_head.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_head.GetItem(rowNumber, "member_no");
                memNoItemChange();
            }
            return 0;
        }
        function DwButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_open") {
                Gcoop.OpenDlg("570", "590", "w_dlg_member_search.aspx", "")
            }
            return 0;
        }

        function GetMemDetFromDlg(memberno) {
            objdw_head.SetItem(1, "member_no", memberno);
            objdw_head.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            memNoFromDlg();
        }
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:TextBox ID="TextGRT" runat="server" Visible="False"></asp:TextBox>
    รายละเอียด
    <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_sl_reqresign_head"
        LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEventItemChanged="DwItemChange" ClientEventButtonClicked="DwButtonClick"
        ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <table style="width: 100%;" border="1">
        <tr>
            <td width="160px">
                <br />
                <dw:WebDataWindowControl ID="dw_sum" runat="server" DataWindowObject="d_sl_reqresign_sum"
                    LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" ClientScriptable="True"
                    ClientEventButtonClicked="showTabPage" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" TabIndex="100">
                </dw:WebDataWindowControl>
            </td>
            <td valign="top" style="padding-left: 5px;">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดหุ้น
                    <dw:WebDataWindowControl ID="dw_share" runat="server" DataWindowObject="d_sl_reqresign_share"
                        LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" Width="540px" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        TabIndex="200">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดสัญญาเงินกู้
                    <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_sl_reqresign_loan"
                        LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" Width="540px" Height="298px"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" TabIndex="300">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดการค้ำประกัน
                    <dw:WebDataWindowControl ID="dw_grt" runat="server" DataWindowObject="d_sl_reqresign_grt"
                        LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" Width="540px" Height="298px"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" TabIndex="400">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดเงินฝาก
                    <dw:WebDataWindowControl ID="dw_deposit" runat="server" DataWindowObject="d_sl_reqresign_deposit"
                        LibraryList="~/DataWindow/shrlon/sl_member_resign.pbl" Width="540px" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        TabIndex="600">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
