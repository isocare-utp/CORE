<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_opr_impfile.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_opr_impfile" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
    <%=postSetCriteria %>
    <script type="text/javascript">
        function OnDwChoiceItemChange(s, r, c, v) {
            if (c == "imp_choice") {
                objDw_choice.SetItem(1, "imp_choice", v);
                objDw_choice.AcceptText();
                postSetCriteria();
            }
        }
        function Validate() {
            var imp_choice = objDw_choice.GetItem(1, "imp_choice");
            if (imp_choice == "01") {
                return confirm("คุณต้องการ Update ข้อมูลเงินเดือน ใช่หรือไม่?");
            }
            else {
                return confirm("คุณต้องการ Update ข้อมูลเรียกเก็บ ใช่หรือไม่?");
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
                    <strong>ประเภท TextFile</strong>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <dw:WebDataWindowControl ID="Dw_choice" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_kp_opr_impfile_choice" LibraryList="~/DataWindow/keeping/kp_opr_impfile.pbl"
                        Style="top: 0px; left: 0px" ClientEventItemChanged="OnDwChoiceItemChange">
                    </dw:WebDataWindowControl>
                    </br>
                    <dw:WebDataWindowControl ID="Dw_cri" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_kp_opr_impfile_kptemp" LibraryList="~/DataWindow/keeping/kp_opr_impfile.pbl"
                        Style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:FileUpload ID="FileUpload" runat="server" />
                    <tr>
                        <td class="style1">
                            <asp:Button ID="b_import" runat="server" OnClick="b_import_Click" Text="Import" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <strong>ข้อมูล Text file</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" Height="300">
                                <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                                    ClientScriptable="True" DataWindowObject="d_kp_opr_impfile_kptemp_text" LibraryList="~/DataWindow/keeping/kp_opr_impfile.pbl"
                                    Style="top: 0px; left: 0px" RowsPerPage="10" PageNavigationBarSettings-NavigatorType="NumericWithQuickGo"
                                    PageNavigationBarSettings-Visible="True">
                                </dw:WebDataWindowControl>
                            </asp:Panel>
                            <dw:WebDataWindowControl ID="Dw_linetext" runat="server" AutoRestoreContext="False"
                                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                                ClientScriptable="True" DataWindowObject="d_kp_opr_linetext" LibraryList="~/DataWindow/keeping/kp_opr_impfile.pbl"
                                Style="top: 0px; left: 0px">
                            </dw:WebDataWindowControl>
                            <asp:GridView ID="GridMemberSalary" runat="server">
                            </asp:GridView>
                        </td>
                        <asp:HiddenField ID="HdRowCount" runat="server" />
                    </tr>
        </table>
    </p>
</asp:Content>
