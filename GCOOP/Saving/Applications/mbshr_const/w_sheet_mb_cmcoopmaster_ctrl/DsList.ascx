<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_cmcoopmaster_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="4" >
                    <div>
                        <span style="text-align: center; width:99%;">กำหนดอายุสมาชิก</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>อายุสมาชิกสูงสุด:</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:TextBox ID="maxmbage_amt" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <span>อายุงานสมาชิกสูงสุด:</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:TextBox ID="maxmbworkage_amt" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สมัครสมาชิกใหม่ได้สูงสุด :</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="maxapplnew_amt" runat="server" style="width:99%;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div>
                        <span style="text-align: center; width:99%;">กำหนดวัน เดือน ที่เกษียณอายุ</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่เกียณอายุ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="retry_day" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    *หมายเหตุ หากไม่ต้องการกำหนดให้ใส่ 0
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เดือนที่เกษียณอายุ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="retry_month" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เกษียณเมื่ออายุ =</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="retry_age" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
