<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mbshr_apv_mbresign.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_apv_mbresign" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-family: Tahoma;
        }
    </style>
    <%=initJavaScript %>
    <%=postInit%>
    <%=postSetStatus%>
    <%=postRequestStatus%>
    <script type="text/javascript">
        function Validate() {
            objDw_option.AcceptText();
            objDw_list.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_sch") {
                objDw_option.AcceptText();
                postInit();
            }
        }

        function ItemChanged(s, r, c, v) {
            if (c == "member_sno") {
                objDw_option.SetItem(r, "member_eno", v);
            }
        }

        function OnDwListButtonClick(s, r, b) {
            if (objDw_list.RowCount() > 0) {
                if (b == "b_wait" || b == "b_apv" || b == "b_noapv") {
                    Gcoop.GetEl("Hdbutton").value = b;
                    postSetStatus();
                }
            }
        }

        function OnDwListItemChange(s, r, c, v) {
            if (c == "resignreq_status") {
                objDw_list.SetItem(r, "resignreq_status", v);
                objDw_list.AcceptText();
                Gcoop.GetEl("HdRow").value = r + "";
                postRequestStatus();
            }
            else if (c == "approve_tdate") {
                objDw_list.SetItem(1, "approve_tdate", v);
                objDw_list.AcceptText();
                objDw_list.SetItem(1, "apv_date", Gcoop.ToEngDate(v));
                objDw_list.AcceptText();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_mbsrv_list_apvmbresign_opt" LibraryList="~/DataWindow/mbshr/mb_apvmb_resign.pbl"
                        Style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwOptionButtonClick"
                        ClientEventItemChanged="ItemChanged">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel2" class="Detail_H" runat="server" Height="400px" Width="770px"
            ScrollBars="Auto" HorizontalAlign="Left">
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        <strong>รายละเอียดอนุมัติลาออก</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Height="400px">
                            <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                                AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                                DataWindowObject="d_mbsrv_list_apvmbresign" LibraryList="~/DataWindow/mbshr/mb_apvmb_resign.pbl"
                                Style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwListButtonClick" ClientEventItemChanged="OnDwListItemChange">
                            </dw:WebDataWindowControl>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:HiddenField ID="Hdbutton" runat="server" />
                        <asp:HiddenField ID="HdRow" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </p>
</asp:Content>
