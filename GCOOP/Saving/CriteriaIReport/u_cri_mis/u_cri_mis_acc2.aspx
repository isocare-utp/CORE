<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_mis_acc2.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_mis.u_cri_mis_acc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <br />
    <table class="iReportDataSourceFormView">
    <tr>
<td width="30">
                <div>
                    <span>รหัสงบ :</span>
                </div>
            </td>
            <td width="100">
                <asp:DropDownList ID="money_code" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="00"> 00 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                        <asp:ListItem Value="01"> 01 - งบกำไรขาดทุน(ประจำปี)</asp:ListItem>
                        <asp:ListItem Value="02"> 02 - งบดุล(ประจำปี)</asp:ListItem>
                        <asp:ListItem Value="03"> 03 - งบกระแสเงินสด</asp:ListItem>
                        <asp:ListItem Value="04"> 04 - งบดุลเปรียบเทียบ(ประจำเดือน)</asp:ListItem>
                        <asp:ListItem Value="05"> 05 - งบกำไรขาดทุน(เดือน)</asp:ListItem>
                        <asp:ListItem Value="06"> 06 - งบกระแสเงินสดเปรียบเทียบ</asp:ListItem>
                        <asp:ListItem Value="07"> 07 - งบดุลเปรียบเทียบ(ประจำเดือน)</asp:ListItem>
                        <asp:ListItem Value="08"> 08 - งบกำไรขาดทุน(รายเดือน)</asp:ListItem>
                        <asp:ListItem Value="10"> 10 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                        <asp:ListItem Value="20"> 20 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                        <asp:ListItem Value="30"> 30 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                        <asp:ListItem Value="40"> 40 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                        <asp:ListItem Value="50"> 50 - รายงานเงินหมุนเวียนคงเหลือรายวัน</asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td width="30">
                <div>
                    
                </div>
            </td>
            <td width="30">
                
            </td>
            </tr>
            <tr>
            <td width="30">
                <div>
                   <span> ข้อมูลที่ 1</span>
                </div>
            </td>
            <td width="30">
                
            </td>
            </tr>
            <tr>
            <td width="30">
                <div>
                    <span>เดือน :</span>
                </div>
            </td>
            <td width="100">
                <asp:DropDownList ID="str_month1" runat="server">
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
            <td width="10">
                <div>
                    <span>ปี :</span>
                </div>
            </td>
            <td width="30">
                <asp:TextBox ID="str_year1" runat="server" ></asp:TextBox>
            </td>
</tr>
</tr>
            
    </table>
</asp:Content>
