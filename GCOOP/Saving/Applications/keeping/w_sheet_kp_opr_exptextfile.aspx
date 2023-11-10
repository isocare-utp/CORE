<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_opr_exptextfile.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_opr_exptextfile" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
    <%=initJavaScript %>
    <%=postInit%>
    
    <script type="text/javascript">
        function OnDwChoiceItemChange(s, r, c, v) {
            if (c == "format_text") {
                objDw_choice.SetItem(1, "format_text", v);
                objDw_choice.AcceptText();
                postChangeOption();
            }
        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_init") {
                postInit();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td class="style1">

                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="style1">
                    <strong>เงื่อนไขการดึงข้อมูล</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_kp_opr_exptextfile_cri" LibraryList="~/DataWindow/keeping/kp_opr_exptextfile.pbl"
                         ClientEventButtonClicked="OnDwMainButtonClick" ClientEventItemChanged="OnDwMainItemChange" Style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>ข้อมูลเรียกเก็บ</strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Panel ID="Panel1" runat="server" Height="300">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_kp_opr_exptextfile_list" LibraryList="~/DataWindow/keeping/kp_opr_exptextfile.pbl"
                            Style="top: 0px; left: 0px" RowsPerPage="10" PageNavigationBarSettings-NavigatorType="NumericWithQuickGo"
                            PageNavigationBarSettings-Visible="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <asp:HiddenField ID="HdRowCount" runat="server" />
            </tr>
        </table>
    </p>
</asp:Content>
