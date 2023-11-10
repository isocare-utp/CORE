<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_date_depttype_recppaytype.DsMain" %>
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
                    <asp:DropDownList ID="as_coopid" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>    
                 <td width="30%">
                    <div>
                        <span>วันที่ :</span>
                    </div>
                </td >
                <td>
                    <asp:TextBox ID="ENTRY_DATE" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>ประเภท :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="depttype_scode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>รหัสรายการ :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="RECPPAYTYPE_CODE" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
     
    </EditItemTemplate>
</asp:FormView>
