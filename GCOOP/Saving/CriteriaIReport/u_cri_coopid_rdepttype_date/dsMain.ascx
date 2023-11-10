<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dsMain.ascx.cs" 
Inherits="Saving.CriteriaIReport.u_cri_coopid_rdepttype_date.dsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            
           <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>ตั้งแต่ประเภท :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="depttype_scode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>ถึงประเภท :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="depttype_ecode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>วันที่ :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:TextBox ID="as_date" runat="server"></asp:TextBox>
                </td>
            </tr>              
        </table>
     
    </EditItemTemplate>
</asp:FormView>
