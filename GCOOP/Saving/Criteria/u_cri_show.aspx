<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_show.aspx.cs" Inherits="Saving.Criteria.u_cri_show" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=runProcess%>
<%=popupReport%>
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<table><tr><td align="left" style="width:100px;">
                        <span style="cursor: pointer" onclick="OnClickLinkNext();"> ออกรายงาน&gt; </span>
                            <%--<asp:LinkButton ID="LinkNext" runat="server" onclick="LinkNext_Click">ออกรายงาน &gt;</asp:LinkButton>--%>
                        </td></tr></table>
  <asp:HiddenField ID="HdOpenIFrame" runat="server" value="False"/>
</asp:Content>
