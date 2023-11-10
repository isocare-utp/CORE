<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_update_shr.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_update_shr_ctrl.ws_mbshr_update_shr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .File_Name, .hiddenF
        {
            visibility: hidden;
        }
        #statustxt
        {
            top: 3px;
            left: 50%;
            position: absolute;
            display: inline-block;
            color: #003333;
        }
    </style>
    <%=JsUpdateShr%>

    <script type="text/javascript">
        var dsFilepath = new DataSourceTool();

        //ทำงานเหมือน load begin
        $(function () {
            //เมื่อมีการ upoload file  
            if ($('.File_Name').val()) {
                $('.txtFileName').text($('.File_Name').val());
            }

            $('#ctl00_ContentPlace_Update').click(function () {
                if (confirm("ยืนยันการทำรายการ")) {
                    JsUpdateShr();
                }
            });
        });

        function endproc() {

        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลปรับเงินเดือน", true, true, "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <table width='200px'>
        <tr>
            <td>
                <asp:Button ID="Update" runat="server" Text="ปรับปรุงข้อมูลหุ้นฐาน" />
            </td>
        </tr>
    </table>
</asp:Content>

