<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_upload_mem_pic_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" >
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขสมาชิก :</span>
                    </div>
                </td>
                <td width="80%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" style="width:120px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td >
                    <div>
                        <span>ชื่อ-นามสกุล:</span>
                    </div>
                </td>
                <td >
                    <div>
                        <asp:TextBox ID="full_name" runat="server" style="width:300px;" ReadOnly></asp:TextBox>
                        
                       
                    </div>
                </td>
            </tr>

            <tr>
                <td >
                    <div>
                        <span>เลขบัญชี:</span>
                    </div>
                </td>
                <td >
                    <div>
                        <asp:DropDownList ID="DEPTACCOUNT_NO" runat="server" style="width:300px;" ></asp:DropDownList>
                        
                       
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
