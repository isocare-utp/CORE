<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ln_nplreport.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplreport_ctrl.w_sheet_ln_nplreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnClickCreateReport() {
            if (confirm("ยืนยันการออกรายงานใหม่ ")) {
                postSubmitReport();
            }
        }

        function OnClickDeleteReport() {
            if (confirm("ยืนยันการลบรายงานเก่า")) {
                postDeleteReport();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
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
                                    <asp:TextBox ID="tbYearTh" runat="server" Style="text-align: right;"></asp:TextBox>
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
                                    <asp:TextBox ID="tbMonth" runat="server" Style="text-align: right;"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                </div>
                            </td>
                            <td>
                                <div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                </div>
                            </td>
                            <td>
                                <div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <input type="button" value="ออกรายงานใหม่" onclick="OnClickCreateReport()" style="width: 110px;" />
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
                                    <asp:DropDownList ID="ddDelYearTh" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddDelYearTh_SelectedIndexChanged">
                                    </asp:DropDownList>
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
                                    <asp:DropDownList ID="ddDelMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddDelMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
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
                                    <asp:DropDownList ID="ddDelSeqNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddDelSeqNo_SelectedIndexChanged">
                                    </asp:DropDownList>
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
                                    <asp:TextBox ID="tbDelEntryDate" runat="server" Style="background: #E7E7E7;" ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <input type="button" value="ลบรายงานเก่า" onclick="OnClickDeleteReport()" style="width: 110px;" />
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
