<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_update_expaccid.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_update_expaccid_ctrl.ws_mbshr_update_expaccid" %>

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
        <tr>
            <td colspan="2">                
                <input type="button" value="รายงาน" style="width: 342px;" onclick="PostReport()" />
            </td>
        </tr>
        <tr>
            <td colspan="2">                
                <input type="button" value="Update ข้อมูล" style="width: 342px;" onclick="PostUpdate()" />
            </td>
        </tr>
    </table>
</asp:Content>
