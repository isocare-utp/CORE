<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_opr_exp_textfile.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_opr_exp_textfile_ctrl.ws_kp_opr_exp_textfile" %>

<%@ Register Src="DsSalary.ascx" TagName="DsSalary" TagPrefix="uc1" %>
<%@ Register Src="DsCbt.ascx" TagName="DsCbt" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsSalary = new DataSourceTool;
        var dsCbt = new DataSourceTool;

        $(function () {
            if ($('#ctl00_ContentPlace_dd_format').val() == "001" || $('#ctl00_ContentPlace_dd_format').val() == "002") {
                $('#ctl00_ContentPlace_dsSalary_FormView1').show();
                $('#ctl00_ContentPlace_dsCbt_FormView1').hide();
            } else {
                $('#ctl00_ContentPlace_dsCbt_FormView1').show();
                $('#ctl00_ContentPlace_dsSalary_FormView1').hide();
            }

            $('#ctl00_ContentPlace_b_retrieve').click(function () {
                postRetrieve();
            });

            //            $('#ctl00_ContentPlace_b_export').click(function () {
            //                postExpTextfile();
            //            });

            $('#ctl00_ContentPlace_dd_format').change(function () {
                if ($('#ctl00_ContentPlace_dd_format').val() == "001" || $('#ctl00_ContentPlace_dd_format').val() == "002") {
                    $('#ctl00_ContentPlace_dsSalary_FormView1').show();
                    $('#ctl00_ContentPlace_dsCbt_FormView1').hide();
                    $('#ctl00_ContentPlace_txt_totalno').val("");
                    $('#ctl00_ContentPlace_txt_totalamt').val("");
                } else {
                    $('#ctl00_ContentPlace_dsCbt_FormView1').show();
                    $('#ctl00_ContentPlace_dsSalary_FormView1').hide();
                    $('#ctl00_ContentPlace_txt_totalno').val("");
                    $('#ctl00_ContentPlace_txt_totalamt').val("");
                }
            });
        });

        function SheetLoadComplete() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="DataSourceFormView" style="width: 450px;" width='100%'>
        <tr>
            <td width="35%">
                <span>ประเภท TextFile:</span>
            </td>
            <td>
                <asp:DropDownList ID="dd_format" runat="server" Style="width: 280px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <strong><u>เงื่อนไขการดึงข้อมูล</u></strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:DsSalary ID="dsSalary" runat="server" />
                <uc2:DsCbt ID="dsCbt" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="b_retrieve" runat="server" Text="ดึงยอดเรียกเก็บ" Style="width: 282px;" />
            </td>
        </tr>
        <tr>
            <td>
                <span>จำนวน:</span>
            </td>
            <td>
                <asp:TextBox ID="txt_totalno" runat="server" Style="text-align: center"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span>รวมยอดเรียกเก็บ:</span>
            </td>
            <td>
                <asp:TextBox ID="txt_totalamt" runat="server" Style="text-align: right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="b_export" runat="server" Text="ออก TextFile" Style="width: 282px;"
                    OnClick="postExpTextfile" />
            </td>
        </tr>
    </table>
</asp:Content>
