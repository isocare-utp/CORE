﻿<%@ Page Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_year.aspx.cs"
    Inherits="Saving.Criteria.u_cri_year" Title="Report Criteria" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=runProcess%>
		<%=popupReport%>
		<script type="text/javascript">
		    function OnClickLinkNext(){
		        objdw_criteria.AcceptText();
		        runProcess();
		    }
		    function SheetLoadComplete(){
		        if( Gcoop.GetEl("HdOpenIFrame").value == "True" ){
		            Gcoop.OpenIFrame("220","200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>","");
		        }
		    }
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="text-align:center">
        <tr>
            <td align=center>
                <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
                    Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                    Font-Size="Large" Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="width:500px">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" 
                    ClientScriptable="True" DataWindowObject="d_cri_year"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" 
                    AutoSaveDataCacheAfterRetrieve="True">
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
                        <td align=left style="width:100px;">
                            <asp:LinkButton ID="LinkBack" runat="server">&lt; ย้อนกลับ</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align=left style="width:100px;">
                              <span style="cursor: pointer" onclick="OnClickLinkNext();">ออกรายงาน&gt;</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" value="False"/>
		<%=outputProcess%>      
</asp:Content>
