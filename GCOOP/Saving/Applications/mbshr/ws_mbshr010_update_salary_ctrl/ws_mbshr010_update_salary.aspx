<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr010_update_salary.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr010_update_salary_ctrl.ws_mbshr010_update_salary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .File_Name, .hiddenF
        {
            visibility: hidden;
        }
        #progressbox
        {
            border: 1px solid #4E616D;
            padding: 1px;
            position: relative;
            width: 400px;
            border-radius: 3px;
            margin: 10px;
            display: block;
            text-align: left;
        }
        #progressbar
        {
            height: 20px;
            border-radius: 3px;
            background-color: #D84A38;
            width: 1%;
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
    <%=postDatatxt%>
    <%=CMSaveSalary%>
    <script type="text/javascript">
        var dsFilepath = new DataSourceTool();

        //ทำงานเหมือน load begin
        $(function () {
            //เมื่อมีการ upoload file  
            if ($('.File_Name').val()) {
                $('.txtFileName').text($('.File_Name').val());
            }

            $('#ctl00_ContentPlace_Import').click(function () {
                if (confirm("ยืนยันการทำรายการ")) {
                    postDatatxt();
                }
            });

            $('#ctl00_ContentPlace_Update').click(function () {
                if (confirm("ยืนยันการทำรายการ")) {
                    CMSaveSalary();
                }
            });


            //Progress Bar โดยใช้ Jquery ยังไม่รู้จะเอาประยุกต์ใช้ยังไง By maxim
            $('#progressbox, .btajax1, .btajax2, .btajax3').hide(); //ซ่อนไปก่อน
            OnloadProgress();

            $(".btajax1").click(AjaxCall001);
            $(".btajax2").click(AjaxCall002);

        });

        function OnloadProgress() {
            var idTime = self.setInterval(function () { clock() }, 400); //ตั้งเวลา

            var i = 0;
            function clock() {
                i = i + 1;
                $('#progressbar').width(i + '%');
                $('#statustxt').text(i + '%');

                if (i == 100) {
                    //clearInterval(idTime); //กรณีที่ไม่ต้องการ Loop  100% แล้วให้หยุด
                    i = 0;
                }
            }
        }

        function AjaxCall001() {
            $.ajax({
                type: "POST",
                url: "w_sheet_mb_update_salary.aspx/GetText",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return false;
        }

        function AjaxCall002() {
            $.ajax({
                type: "POST",
                url: "w_sheet_mb_update_salary.aspx/GetData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    eval(response.d);
                    genValue(memo)
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return false;
        }

        function genValue(memo) {
            for (var i = 0; i < memo.length; i++) {
                alert(memo[i][0] + " " + memo[i][1]);
            }
        }

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
    <asp:HiddenField ID="HdRunProcess" runat="server" />
    <asp:HiddenField ID="Hd_process" runat="server" />
    <div id="progressbox">
        <div id="progressbar">
        </div>
        <div id="statustxt">
            0%</div>
    </div>
    <br />
    <div>
        <asp:RadioButtonList ID="rdoPriceRange" runat="server" RepeatLayout="Flow">
            <asp:ListItem Value="txtFile" Selected="True"> TextFile <font color="red">(*salary_id , salary_amt)</font></asp:ListItem>
            <asp:ListItem Value="exFile"> ExcelFile <font color="red">(*salary_id, salary_amt)</font></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <span class="txtFileName"></span>
        <asp:TextBox ID="File_Name" class="File_Name" runat="server"></asp:TextBox>
    </div>
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
    </table>
    <br />
    <table class="DataSourceFormView" style="width: 360px;">
        <tr>
            <td colspan="2">
                <asp:Button ID="Import" runat="server" Text="Import ข้อมูลเงินเดือน" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>จำนวนข้อมูลใน Disk:</span>
                </div>
            </td>
            <td>
                <asp:TextBox ID="all_disk" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>จำนวนข้อมูลที่ตรงกับสมาชิก:</span>
                </div>
            </td>
            <td>
                <asp:TextBox ID="match_item" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>จำนวนคนที่ต้องปรับ งด:</span>
                </div>
            </td>
            <td>
                <asp:TextBox ID="chg_amt" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>ปรับขึ้น:</span>
                </div>
            </td>
            <td>
                <asp:TextBox ID="inc_amt" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>ปรับลง:</span>
                </div>
            </td>
            <td>
                <asp:TextBox ID="dec_amt" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table width='200px'>
        <tr>
            <td>
                <asp:Button ID="Update" runat="server" Text="Update ข้อมูลเงินเดือน" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button class="btajax1" runat="server" Text="ทดสอบ Ajax Call str" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button class="btajax2" runat="server" Text="ทดสอบ Ajax Call str[]" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button class="btajax3" runat="server" Text="ทดสอบ Ajax Call sql" />
            </td>
        </tr>
    </table>
</asp:Content>
