<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_update_data_from_disk.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl.w_sheet_sl_update_data_from_disk" %>

<%@ Register Src="DsFilepath.ascx" TagName="DsFilepath" TagPrefix="uc1" %>
<%@ Register Src="Dwlist.ascx" TagName="Dwlist" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .File_Name,.hiddenF
        {
            visibility: hidden;
        }
    </style>
    <%=postDatatxt%>
    <%=ImportDatatxt%>
    <%=ClearDatatxt%>
    <%=LoadDatatxt%>
    <%=UpdateFileData%>
    <script type="text/javascript">
        var dsFilepath = new DataSourceTool();
        var dwlist = new DataSourceTool();

        //ทำงานเหมือน load begin
        $(function () {
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_001', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_002', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_003', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_004', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_005', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_006', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_007', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_008', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_009', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_010', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_011', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_012', true);
            SetDisable('#ctl00_ContentPlace_dsFilepath_FormView1_status_013', true);

            if ($('.File_Name').val()) {
                $('.txtFileName').text($('.File_Name').val());
            }

            if ($('#ctl00_ContentPlace_dsFilepath_FormView1_readdata_count').val() == 0) {
                SetDisable('#ctl00_ContentPlace_Update', true);
            } else {
                SetDisable('#ctl00_ContentPlace_Update', false);
            }

            //เมื่อมีการ upoload file  
            $(".Filetxt").change(function () {
                postDatatxt();
            });

            $("#ctl00_ContentPlace_Import").click(function () {
                ImportDatatxt();
            });

            $("#ctl00_ContentPlace_Clear").click(function () {
                ClearDatatxt();
            });

            $("#ctl00_ContentPlace_dsFilepath_FormView1_cb_1").click(function () {
                LoadDatatxt();
            });

            $("#ctl00_ContentPlace_Update").click(function () {
                if (confirm("ระบบจะทำการ Update ข้อมูลสมาชิกทุกรายการ กรุณายืนยัน")) {
                    UpdateFileData();
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdRunProcess" runat="server" />
    <div id="progressbar">
    </div>
    <div>
        <span class="txtFileName"></span>
        <asp:TextBox ID="File_Name" class="File_Name" runat="server"></asp:TextBox>
    </div>
    <table class="DataSourceFormView" width='100%'>
        <tr>
            <td width='15%'>
                <div>
                    <span>File Path : </span>
                </div>
            </td>
            <td width='20%'>
                <asp:FileUpload ID="txtInput" class="Filetxt" runat="server" />
            </td>
            <%--<td width='10%'>
                <asp:Button ID="Import" runat="server" Text="Import" />
            </td>--%>
            <td width='10%'>
                <asp:Button ID="Clear" runat="server" Text="Clear" />
            </td>
            <td width='10%'>
                <asp:Button ID="Update" runat="server" Text="Update" />
            </td>
        </tr>
    </table>
    <uc1:DsFilepath ID="dsFilepath" runat="server" />
    <div class="hiddenF">
        <uc2:Dwlist ID="dwlist" runat="server" />
    </div>
</asp:Content>
