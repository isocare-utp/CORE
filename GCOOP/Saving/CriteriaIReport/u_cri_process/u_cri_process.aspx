<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_process.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_process.u_cri_process" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
        <script type="text/javascript">
            //            $(function () {
            //                AutoSlash('input[name="ctl00$ContentPlace$year_start"]');
            //                AutoSlash('input[name="ctl00$ContentPlace$year_end"]');
            //            });

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
        <br />
    </center>
    <br />
    <table class="iReportDataSourceFormView">
       <tr>
            <td width="70">
                <div>
                    <span>เดือน :</span>
                </div>
            </td>
            <td width="100">
                <asp:DropDownList ID="str_month_" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="01"> มกราคม</asp:ListItem>
                        <asp:ListItem Value="02"> กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="03"> มีนาคม</asp:ListItem>
                        <asp:ListItem Value="04"> เมษายน</asp:ListItem>
                        <asp:ListItem Value="05"> พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="06"> มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="07"> กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="08"> สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="09"> กันยายน</asp:ListItem>
                        <asp:ListItem Value="10"> ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11"> พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12"> ธันวาคม</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td width="50">
                <div>
                    <span>ปี :</span>
                </div>
            </td>
            <td width="100">
                <asp:TextBox ID="str_date" runat="server" ></asp:TextBox>
            </td>
          
        </tr>
    </table>
</asp:Content>

