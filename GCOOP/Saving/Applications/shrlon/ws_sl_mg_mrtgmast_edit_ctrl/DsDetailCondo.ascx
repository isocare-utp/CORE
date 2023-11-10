<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailCondo.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.DsDetailCondo" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 750px; background-color: #CCFF99;border:1px double black;">
            <tr>
                <td colspan="4">
                    <strong><u>ตำแหน่งที่ดิน</u></strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>โฉนดเลขที่:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="land_docno" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="pos_tambol" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="pos_amphur" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="pos_province" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong><u>รายละเอียดอาคารชุด</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่ออาคารชุด:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="condo_name" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนเลขที่:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="condo_regisno" runat="server" Style="width: 618px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong><u>รายละเอียดห้องชุด</u></strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ห้องชุดเลขที่:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="condo_roomno" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>ชั้น:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="condo_floor" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เนื้อที่ประมาณ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="condo_roomsize" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>อาคารเลขที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="condo_towerno" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>