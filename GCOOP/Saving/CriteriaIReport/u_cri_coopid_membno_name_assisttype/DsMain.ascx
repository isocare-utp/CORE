﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_membno_name_assisttype.DsMain" %>
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
                <td>
                    <div>
                        <span>ทะเบียนสมาชิก:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="memno" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
           
            
            <tr>
                <td>
                    <span>ประเภทสวัสดิการ:</span>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="assisttype_code" runat="server" ></asp:DropDownList>
                </td>
            </tr>
           
        </table>
    </EditItemTemplate>
</asp:FormView>
