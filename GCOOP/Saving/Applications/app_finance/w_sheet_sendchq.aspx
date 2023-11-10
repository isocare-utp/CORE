<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sendchq.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_sendchq1" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postRowMove %>
    <%=postRowDelete %>
    <%=postRowMoveBack %>
    <%=postCheckAll %>
    <script type="text/javascript">

        function Validate() {
            objDwSendChqList.AcceptText();
            objDwSendChq.AcceptText();
            objDwSendChqAcc.AcceptText();
            objDwWaitSendChq.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function CheckAll() {
            objDwSendChqList.AcceptText();
            postCheckAll();
        }

        function DwSendChgListClick(sender, rowNumber, objectName) {
            if (objectName == "select_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "select_flag", 1, 0);
                objDwSendChqList.AcceptText();
                objDwSendChq.AcceptText();
                objDwWaitSendChq.AcceptText();
            }
        }

        function GoSendChq() {
            objDwSendChqList.AcceptText();
            Gcoop.GetEl("HfMoveTo").value = "Send";
            postRowMove();
        }

        function GoWaitSendChq() {
            objDwSendChqList.AcceptText();
            Gcoop.GetEl("HfMoveTo").value = "Wait";
            postRowMove();
        }

        function FromSendChq() {
            objDwSendChq.AcceptText();
            Gcoop.GetEl("HfMoveTo").value = "Send";
            postRowMoveBack();
        }

        function FromWaitSendChq() {
            objDwWaitSendChq.AcceptText();
            Gcoop.GetEl("HfMoveTo").value = "Wait";
            postRowMoveBack();
        }

        function DelRow() {
            objDwWaitSendChq.AcceptText();
            Gcoop.GetEl("HfMoveTo").value = "Wait";
            postRowDelete();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%" border="0">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDate" runat="server" DataWindowObject="d_senchq_date_head"
                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
            <td valign="bottom">
                <dw:WebDataWindowControl ID="DwSendChqAcc" runat="server" DataWindowObject="d_senchq_acc_head"
                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <input style="width: 60px; height: 30px;" id="cb_retrieve" type="button" value="ดึงข้อมูล" />
            </td>
        </tr>
    </table>
    <table border="0" height="423px">
        <tr>
            <td valign="top">
                <asp:CheckBox ID="CheckBoxAll" runat="server" onclick="CheckAll()" Text="เลือกทั้งหมด" />
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="100%">
                    <dw:WebDataWindowControl ID="DwSendChqList" runat="server" DataWindowObject="d_senchq_list"
                        LibraryList="~/DataWindow/App_finance/sendchq.pbl" Width="340px" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="DwSendChgListClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td valign="top">
                <table border="0" height="400px">
                    <tr>
                        <td>
                            <br />
                            <input style="width: 40px; height: 30px;" id="Button1" type="button" value=">" onclick="GoSendChq()" />
                            <input style="width: 40px; height: 30px;" id="Button4" type="button" value="<" onclick="FromSendChq()" />
                            <br />
                            <br />
                        </td>
                        <td valign="top">
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="100%">
                                <dw:WebDataWindowControl ID="DwSendChq" runat="server" DataWindowObject="d_senchq_list_1"
                                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" Width="340px" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                    ClientEventClicked="DwSendChgListClick">
                                </dw:WebDataWindowControl>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <input style="width: 40px; height: 30px;" id="Button5" type="button" value=">" onclick="GoWaitSendChq()" />
                            <input style="width: 40px; height: 30px;" id="Button8" type="button" value="<" onclick="FromWaitSendChq()" />
                            <br />
                            <br />
                            <input style="width: 40px; height: 30px;" id="Button2" type="button" value="ลบ" onclick="DelRow()" />
                        </td>
                        <td valign="top">
                            <br />
                            <asp:RadioButtonList ID="RadioButtonAccKnow" runat="server" RepeatDirection="Horizontal"
                                Enabled="False">
                                <asp:ListItem Text="บัญชีรับรู้" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="บัญไม่รับรู้" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="92%">
                                <dw:WebDataWindowControl ID="DwWaitSendChq" runat="server" DataWindowObject="d_senchq_list_2"
                                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" Width="340px" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                    ClientEventClicked="DwSendChgListClick">
                                </dw:WebDataWindowControl>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfMoveTo" runat="server" />
</asp:Content>
