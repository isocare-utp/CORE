<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMrtger.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsMrtger" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 720px;">
            <tr>
                <td colspan="4">
                    <strong><u>รายละเอียดผู้จำนอง 1</u></strong>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mrtg_name1" runat="server" Style="width: 562px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>บัตรประชาชน:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="mrtg_personid1" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>อายุ:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="mrtg_age1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เชื้อชาติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_nationality1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สัญชาติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_citizenship1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>บิดา/มารดา ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_parentname1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>คู่สมรสชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_matename1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>ที่อยู่ 1</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมู่บ้าน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_village1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_address1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมู่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_moo1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ซอย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_soi1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถนน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_road1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_tambol1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_amphur1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_province1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>รายละเอียดผู้จำนอง 2</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mrtg_name2" runat="server" Style="width: 562px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>บัตรประชาชน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_personid2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>อายุ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_age2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เชื้อชาติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_nationality2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สัญชาติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_citizenship2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>บิดา/มารดา ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_parentname2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>คู่สมรสชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_matename2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>ที่อยู่ 2</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมู่บ้าน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_village2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_address2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมู่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_moo2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ซอย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_soi2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถนน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_road2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_tambol2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_amphur2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mrtg_province2" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
