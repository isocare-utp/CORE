<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dp_import_text_salary.aspx.cs" 
Inherits="Saving.Applications.ap_deposit.ws_dp_import_text_salary_ctrl.ws_dp_import_text_salary" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            //uplond text file
            if ($('.FileName').val()) {
                $('.textFileName').text($('.FileName').val());
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessege" runat="server"></asp:Literal>
    <div> 
        <span class="txtFileName"></span>
        <asp:TextBox ID="FileName" class="FileName" runat="server" Visible="false"></asp:TextBox>
    </div>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table class="DataSourceFormView" width="100%">
        <tr>
            <td width='5%'>
                <div>
                    <span>File Path : </span>
                </div>
            </td>
            <td width='30%'>
                <asp:FileUpload ID="txtInput" class="Filetxt" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="button" value="Import ข้อมูล" style="width: 342px;" onclick="PostImport()" />
            </td>
        </tr>
    </table>
</asp:Content>
