<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_loansrv_proc_preparepay.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_loansrv_proc_preparepay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postProcPreparepay%>
    <script type="text/JavaScript">
        function OnDwLoanClick(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            }
        }
        //Function Main
        function B_ProcessClick() {
            var isconfirm = confirm("ต้องการประมวลชำระเงินกู้ล่วงหน้า ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postProcPreparepay();
        }

        function OnDwOptionItemChange(s, r, c, v) {
            if (c == "proc_type") {
                objDw_option.SetItem(1, "proc_type", v);
                objDw_option.AcceptText();
                var proc_type = objDw_option.GetItem(1, "proc_type");
                if (proc_type == 1) {
                    objDw_option.SetItem(1, "proc_text", "");
                    objDw_option.AcceptText();
                }
                postRefresh();
            }
            else if (c == "caltype_code") {
                objDw_option.SetItem(1, "caltype_code", v);
                objDw_option.AcceptText();
            }
            else if (c == "preparetype_code") {
                objDw_option.SetItem(1, "preparetype_code", v);
                objDw_option.AcceptText();
            }
        }

        function OnDwOptionClick(s, r, c) {
            if (c == "prepareclr_flag") {
                Gcoop.CheckDw(s, r, c, "prepareclr_flag", 1, 0);
            }
            else if (c == "preparelon_flag") {
                Gcoop.CheckDw(s, r, c, "preparelon_flag", 1, 0);
            }
        }

        //Function Default
        //=============================================================
        function Validate() {

        }

        function MenubarOpen() {

        }

        function MenubarNew() {
            postNewClear();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลชำระเงินกู้ล่วงหน้า", true, true, ProcPreparepayComplete);
            }
        }

        function ProcPreparepayComplete() {
            postNewClear();
        }   

    </script>
    <style type="text/css">
        .style1
        {
            width: 98px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server">
                        <dw:WebDataWindowControl ID="Dw_option" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_loansrv_proc_lnprepare" LibraryList="~/DataWindow/shrlon/sl_loansrv_proc_preparepay.pbl"
                            ClientEventClicked="OnDwOptionClick" ClientEventItemChanged="OnDwOptionItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" Width="500px">
                        <dw:WebDataWindowControl ID="Dw_loan" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_loansrv_proc_lnprepare_lntyp" LibraryList="~/DataWindow/shrlon/sl_loansrv_proc_preparepay.pbl" ClientEventClicked="OnDwLoanClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="Panel3" runat="server" Width="300px">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style1">
                                    <asp:Button ID="Button1" runat="server" Text="Button" UseSubmitBehavior="False" Visible="False" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="B_next" runat="server" Text="ต่อไป &gt;" UseSubmitBehavior="False"
                                        OnClick="B_next_Click" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="B_cancel" runat="server" Text="ยกเลิก" UseSubmitBehavior="False"
                                        OnClick="B_cancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="Panel4" runat="server" Width="300px">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="B_previous" runat="server" Text="&lt; ย้อนกลับ" UseSubmitBehavior="False"
                                        OnClick="B_previous_Click" />
                                </td>
                                <td>
                                    <input id="B_process" type="button" value="ประมวลผล" onclick="B_ProcessClick()" />
                                </td>
                                <td>
                                    <asp:Button ID="B_close" runat="server" Text="ยกเลิก" UseSubmitBehavior="False" OnClick="B_close_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="Hd_process" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
    <%=outputProcess%>
</asp:Content>
