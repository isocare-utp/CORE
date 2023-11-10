<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_calint_f.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_calint_f" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostRetrive%>
    <%=PostRetriveDetail%>
    <%=PostGenInt%>
    <%=PostData%>
    <%=PostDelete%>
    <%=PostReport%>
    <%=PostAddrow%>
    <script type="text/javascript">

        function Validate() {
            return confirm("ต้องการบันทึก ใช่หรือไม่ ?");
        }

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                Gcoop.GetEl("Hd_deptaccountno").value = v;
                objDwMain.AcceptText();
                PostRetrive();
            }
        }
        function OnDwListItemChanged(s, r, c, v) {
            objDwList.SetItem(r, c, v);
            objDwList.AcceptText();
        }

        function OnDwListClicked(s, r, c) {
            if (r >= 0) {
                Gcoop.GetEl("HdListRow").value = r + "";
                if (c == "b_del") {
                    objDwList.AcceptText();
                    PostDelete();
                }
            }
        }


        function OnClickGenInt() {
            PostGenInt();
        }
        function OnClickData() {
            PostData();
        }

        function OnClickReport() {
            PostReport();
        }

        function OnClickAddRow() {
            PostAddrow();
        }

        

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" TabIndex="1">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_correct_statement_head"
                        LibraryList="~/DataWindow/ap_deposit/dept_calint_f.pbl" ClientEventItemChanged="OnDwMainItemChanged"
                        ClientEventClicked="OnDwMainClicked" ClientEventItemError="OnError" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <div align="left">
                    <span class="NewRowLink" onclick="OnClickAddRow()">เพิ่มแถว</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" Width="700" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_correct_statement_detial_ext"
                        LibraryList="~/DataWindow/ap_deposit/dept_calint_f.pbl" ClientEventItemChanged="OnDwListItemChanged"
                        ClientEventClicked="OnDwListClicked" ClientEventItemError="OnError" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
                <br />
                <div align="left">
                    <input type="button" value="คำนวณ" style="width: 60px" onclick="OnClickGenInt()" />
                    <input type="button" value="ดึงข้อมูล" style="width: 60px" onclick="OnClickData()" />
                    <input type="button" value="พิมพ์" style="width: 60px;" onclick="OnClickReport()" />
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hd_deptaccountno" runat="server" />
    <asp:HiddenField ID="HdListRow" runat="server" />
</asp:Content>
