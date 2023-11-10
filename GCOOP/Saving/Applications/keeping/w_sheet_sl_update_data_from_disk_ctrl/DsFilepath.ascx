<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFilepath.ascx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl.DsFilepath" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <%--<table class="DataSourceFormView" width='100%'>
            <tr>
                <td width='15%'>
                    <div>
                        <span>File Path : </span>
                    </div>
                </td>
                <td width='20%'>
                    <asp:TextBox ID="File_Name" runat="server"></asp:TextBox>
                </td>
                <td width='10%'>
                    <asp:Button ID="Browse" runat="server" Text="Browse" />
                </td>
                <td width='10%'>
                    <asp:Button ID="Import" runat="server" Text="Import" />
                </td>
                <td width='10%'>
                    <asp:Button ID="Clear" runat="server" Text="Clear" />
                </td>
                <td width='10%'>
                    <asp:Button ID="Update" runat="server" Text="Update" />
                </td>
            </tr>
        </table>--%>
        <table class="DataSourceFormView" width='750px'>
            <tr>
                <td colspan="2">
                    <span style="text-align: center;">Read</span>
                </td>
                <td colspan="2">
                    <span style="text-align: center;">Write</span>
                </td>
                <td colspan="2">
                    <span style="text-align: center;">เลขที่ใบเสร็จ</span>
                </td>
            </tr>
            <tr>
                <td width='15%'>
                    <span style="text-align: center;">จำนวนข้อมูล</span>
                </td>
                <td width='15%'>
                    <span style="text-align: center;">จำนวนสมาชิก</span>
                </td>
                <td width='15%'>
                    <span style="text-align: center;">จำนวนข้อมูล</span>
                </td>
                <td width='15%'>
                    <span style="text-align: center;">จำนวนสมาชิก</span>
                </td>
                <td width='15%'>
                    <span style="text-align: center;">จากเลขที่</span>
                </td>
                <td width='15%'>
                    <span style="text-align: center;">ถึงเลขที่</span>
                </td>
            </tr>
            <tr>
                <td width='15%'>
                    <asp:TextBox ID="readdata_count" runat="server"></asp:TextBox>
                </td>
                <td width='15%'>
                    <asp:TextBox ID="readmem_count" runat="server"></asp:TextBox>
                </td>
                <td width='15%'>
                    <asp:TextBox ID="writedata_count" runat="server"></asp:TextBox>
                </td>
                <td width='15%'>
                    <asp:TextBox ID="writemem_count" runat="server"></asp:TextBox>
                </td>
                <td width='15%'>
                    <asp:TextBox ID="startslip_no" runat="server"></asp:TextBox>
                </td>
                <td width='15%'>
                    <asp:TextBox ID="endslip_no" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width='15%'>
                    <asp:CheckBox ID="status_001" runat="server" Text="กฟน.1 (N1)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_002" runat="server" Text="กฟน.2 (N2)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_003" runat="server" Text="กฟน.3 (N3)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_004" runat="server" Text="กฟฉ.1 (NE1)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_005" runat="server" Text="กฟฉ.2 (NE2)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_006" runat="server" Text="กฟฉ.3 (NE3)" />
                </td>
            </tr>
            <tr>
                <td width='15%'>
                    <asp:CheckBox ID="status_007" runat="server" Text="กฟก.1 (C1)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_008" runat="server" Text="กฟก.2 (C2)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_009" runat="server" Text="กฟก.3 (C3)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_010" runat="server" Text="กฟต.1 (S1)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_011" runat="server" Text="กฟต.2 (S2)" />
                </td>
                <td width='15%'>
                    <asp:CheckBox ID="status_012" runat="server" Text="กฟต.3 (S3)" />
                </td>
            </tr>
            <tr>
                <td width='15%'>
                    <asp:CheckBox ID="status_013" runat="server" Text="CEN" />
                </td>
            </tr>
        </table>
        <table width='750px'>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="รายละเอียด" Font-Size="Small" Font-Bold="true"></asp:Label>
                </td>
                <td width='30%' align="right">
                    <asp:Button ID="cb_1" runat="server" Text="ดึงข้อมูล FileImport" />
                    <asp:Button ID="cb_2" runat="server" Text="Gen Report ครับ" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
