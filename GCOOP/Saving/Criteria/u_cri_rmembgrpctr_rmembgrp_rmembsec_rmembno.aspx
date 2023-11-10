<%@ Page Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_rmembgrpctr_rmembgrp_rmembsec_rmembno.aspx.cs"
    Inherits="Saving.Criteria.u_cri_rmembgrpctr_rmembgrp_rmembsec_rmembno" Title="Report Criteria" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=runProcess%>
    <%=popupReport%>
    <%=checkFlag%>
    <%=checkFlag1%>
    <%=checkFlag2%>
    <%=checkFlag3%>
    <%=jsRefresh%>
    <%=jsRetreivemidgroup%>
    <%=jsRetreivemidgroup2%>
    <script type="text/javascript">
        function OnClickLinkNext() {
            objdw_criteria.AcceptText();
            runProcess();
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
            }
        }

        function OnDwCriteriaItemsChange(sender, rowNumber, columnName, newValue) {
            /*เล็ก DEBUG*///alert(Gcoop.StringFormat(newValue, "00000000"));
            var value = "";
            value = Gcoop.StringFormat(newValue, "00000000");
            if (columnName == "as_startgroup") {
                Gcoop.GetEl("HdStGroup").value = value;
                checkFlag();
            }
            else if (columnName == "as_endgroup") {
                Gcoop.GetEl("HdEnGroup").value = value;
                checkFlag1();
            }
            else if (columnName == "start_membno") {
                Gcoop.GetEl("HdStMem").value = value;
                checkFlag2();
            }
            else if (columnName == "end_membno") {
                Gcoop.GetEl("HdEnMem").value = value;
                checkFlag3();
            }
            if (columnName == "add_type") {
                objdw_criteria.SetItem(rowNumber, columnName, newValue);
                objdw_criteria.AcceptText();
                jsRefresh();
            }
            if (columnName == "membgroup_1") {
                objdw_criteria.SetItem(rowNumber, columnName, newValue);
                objdw_criteria.AcceptText();
                jsRetreivemidgroup();
            }
            if (columnName == "membgroup_2") {
                objdw_criteria.SetItem(rowNumber, columnName, newValue);
                objdw_criteria.AcceptText();
                jsRetreivemidgroup2();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="text-align: center">
        <tr>
            <td align="center">
                <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
                    Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
                    Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 500px">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_rmembgrpctr_rmembgrp_rmembsec_rmembno"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="OnDwCriteriaItemsChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 100%;">
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
                        <td align="left" style="width: 100px;">
                            <asp:LinkButton ID="LinkBack" runat="server">&lt; ย้อนกลับ</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" style="width: 100px;">
                            <span style="cursor: pointer" onclick="OnClickLinkNext();">ออกรายงาน &gt;</span>
                            <%--<asp:LinkButton ID="LinkNext" runat="server" onclick="LinkNext_Click">ออกรายงาน &gt;</asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdStGroup" runat="server" />
    <asp:HiddenField ID="HdEnGroup" runat="server" />
    <asp:HiddenField ID="HdStMem" runat="server" />
    <asp:HiddenField ID="HdEnMem" runat="server" />
</asp:Content>
