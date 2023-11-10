<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsReport.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsReport" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table style="width: 680px;">
            <tr>
                <td width="45%" align="center" valign="top">
                    <fieldset>
                        <legend style="font-family: Tahoma; font-size: 14px;">ออกรายงานใหม่</legend>
                        <table class="DataSourceFormView" style="width: 85%;">
                            <tr>
                                <td width="50%">
                                    <div>
                                        <span>ออกรายงานรายปี</span>
                                    </div>
                                </td>
                                <td width="50%">
                                    <div>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>เดือน</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>ลำดับที่</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>วันที่บันทึก</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td width="10%">
                </td>
                <td width="45%" align="center" valign="top">
                    <fieldset>
                        <legend style="font-family: Tahoma; font-size: 14px;">ลบรายงานเก่า</legend>
                        <table class="DataSourceFormView" style="width: 85%;">
                            <tr>
                                <td width="50%">
                                    <div>
                                        <span>ออกรายงานรายปี</span>
                                    </div>
                                </td>
                                <td width="50%">
                                    <div>
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>เดือน</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>ลำดับที่</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span>วันที่บันทึก</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
