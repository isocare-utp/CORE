<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_conteck_lite_ctrl.DsMain" %>
<style type="text/css">
    .TbMain
    {
        font-family: Tahoma;
        font-size: 13px;
        border: none;
    }
    .TbMain td
    {
        font-family: Tahoma;
        font-size: 13px;
    }
    .TbMain span
    {
        float: left;
        width: 96%;
        height: 22px;
        margin: 2px 2px 0px 0px;
        line-height: 22px;
        text-align: right;
        vertical-align: middle;
        border: 1px solid #000000;
        background-color: rgb(211, 231, 255);
        font-family: Tahoma;
        font-size: 13px;
    }
    .TbMain div
    {
        overflow: hidden;
        height: 26px;
        font-family: Tahoma;
        font-size: 13px;
        width: 100%;
        padding: 0px 0px 0px 0px;
        margin: 0px 0px 0px 0px;
        border: none;
        vertical-align: middle;
    }
    .TbMain input[type=text], select
    {
        width: 96%;
        height: 22px;
        float: left;
        font-family: Tahoma;
        font-size: 13px;
        border: 1px solid #000000;
        margin: 2px 2px 0px 0px;
        vertical-align: middle;
    }
</style>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table width="750" class="TbMain">
            <tr>
                <td width="11%">
                    <div>
                        <span>สหกรณ์:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="COOP_ID" runat="server" Style="width: 52px;"></asp:TextBox>
                        <span style="width: 69px; margin-left: 3px">เลขบัญชี:</span>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <asp:TextBox ID="DEPTACCOUNT_NO" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="58%">
                    <div>
                        <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBER_NO" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทบัญชี:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="DEPTTYPE_CODE" runat="server" Style="width: 11%;"></asp:TextBox>
                        <asp:TextBox ID="DEPTTYPE_DESC" runat="server" Style="width: 358px; margin-left: 3px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
