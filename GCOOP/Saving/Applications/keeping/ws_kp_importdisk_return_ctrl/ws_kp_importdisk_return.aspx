<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_importdisk_return.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_importdisk_return_ctrl.ws_kp_importdisk_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PostImport() {
            PostImport();
        }

        //ทำงานเหมือน load begin
        $(function () {
            //เมื่อมีการ upoload file  
            if ($('.File_Name').val()) {
                $('.txtFileName').text($('.File_Name').val());
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div>
        <span class="txtFileName"></span>
        <asp:TextBox ID="File_Name" class="File_Name" runat="server" Visible="False"></asp:TextBox>
    </div>
    <table class="DataSourceFormView" style="width: 360px;" width='100%'>
        <tr>
            <td width="25%">
                <div>
                    <span>ปี :</span></div>
            </td>
            <td width="75%">
                <asp:TextBox ID="year" runat="server" Style="text-align: center;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>เดือน:</span></div>
            </td>
            <td>
                <asp:DropDownList ID="month" runat="server">
                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                    <asp:ListItem Value="1">มกราคม</asp:ListItem>
                    <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                    <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                    <asp:ListItem Value="4">เมษายน</asp:ListItem>
                    <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                    <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                    <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                    <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                    <asp:ListItem Value="9">กันยายน</asp:ListItem>
                    <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                    <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                    <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>ประเภทสมาชิก:</span></div>
            </td>
            <td>
                <asp:DropDownList ID="member_type" runat="server">
                    <asp:ListItem Value="1" Selected="True">ปกติ</asp:ListItem>
                    <asp:ListItem Value="2">สมทบ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>        
    </table>
    <br />
    <table class="DataSourceFormView" width='100%'>
        <tr>
            <td width='5%'>
                <div>
                    <span>File Path : </span>
                </div>
            </td>
            <td width='20%'>
                <asp:FileUpload ID="txtInput" class="Filetxt" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--<asp:Button ID="Import" runat="server" Text="Import ข้อมูล" />--%>
                <input type="button" value="Import ข้อมูล" style="width: 342px;" onclick="PostImport()" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
