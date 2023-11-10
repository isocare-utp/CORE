﻿<%@ Page Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_date_year_rmembno_rmembgroup_rloanobjective_name.aspx.cs"
    Inherits="Saving.Criteria.u_cri_date_year_rmembno_rmembgroup_rloanobjective_name" Title="Report Criteria" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=runProcess%>
		<%=popupReport%>
		<%=checkFlag%>
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
		          function OnDwCriteriaItemsClicked(sender, rowNumber, objectName)
                     {
                            Gcoop.CheckDw(sender, rowNumber, objectName, "fixmembgroup_flag", 1, 0);
                             Gcoop.CheckDw(sender, rowNumber, objectName, "fixmembno_flag", 1, 0);
                        if(objectName == "fixmembgroup_flag" || objectName == "fixmembno_flag")
                                {
                                      objdw_criteria.AcceptText();
                                     checkFlag();
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
                    ClientScriptable="True" DataWindowObject="d_cri_date_year_rmembno_rmembgroup_rloanobjective_name"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" 
                    AutoSaveDataCacheAfterRetrieve="True" 
                    ClientEventClicked ="OnDwCriteriaItemsClicked">
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
                             <span style="cursor: pointer" onclick="OnClickLinkNext();"> ออกรายงาน &gt;</span>
                            <%--<asp:LinkButton ID="LinkNext" runat="server" onclick="LinkNext_Click">ออกรายงาน &gt;</asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" value="False"/>
</asp:Content>
