<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_imp_insurance_excal.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_imp_insurance_excal_ctrl.ws_kp_imp_insurance_excal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {                        
            return confirm("ต้องการ Import ข้อมูลหรือไม่ ?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table class="DataSourceFormView">
        <tr>
            <td style="width: 150px;">
                <span>ปี : </span>
            </td>
            <td>
                <asp:TextBox ID="as_year" runat="server" Width="100px" Style="text-align: center;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span>เดือน : </span>
            </td>
            <td>
                <asp:DropDownList ID="as_month" runat="server" Width="200px">
                    <asp:ListItem Value="00">---เลือกเดือน-</asp:ListItem>
                    <asp:ListItem Value="01">มกราคม</asp:ListItem>
                    <asp:ListItem Value="02">กุมภาพันธ์	</asp:ListItem>
                    <asp:ListItem Value="03">มีนาคม</asp:ListItem>
                    <asp:ListItem Value="04">เมษายน</asp:ListItem>
                    <asp:ListItem Value="05">พฤษภาคม</asp:ListItem>
                    <asp:ListItem Value="06">มิถุนายน</asp:ListItem>
                    <asp:ListItem Value="07">กรกฎาคม</asp:ListItem>
                    <asp:ListItem Value="08">สิงหาคม</asp:ListItem>
                    <asp:ListItem Value="09">กันยายน</asp:ListItem>
                    <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                    <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                    <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td>
                <span>ประเภทรายการ : </span>
            </td>
            <td>
                <asp:DropDownList ID="as_type" runat="server" Width="200px">
                    <%--<asp:ListItem Value="00">---เลือกรายการ-</asp:ListItem>
                    <asp:ListItem Value="CSL">(สปอ.)ฌาปนกิจสงเคราะห์สหกรณ์ออมทรัพย์ครูกรมสามัญศึกษาจังหวัดลำปาง</asp:ListItem>
                    <asp:ListItem Value="CMS">(สส.อส.)สมาคมฌาปนกิจสงเคราะห์สมาชิกสหกรณ์ออมทรัพย์ครูกรมสามัญศึกษาจังหวัดลำปาง จำกัด</asp:ListItem>
                    <asp:ListItem Value="CSS">(สส.ชสน.)สมาคมฌาปนกิจสงเคราะห์ชมรมสหกรณ์ออมทรัพย์สามัญศึกษาภาคเหนือ</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span>File Path : </span>
            </td>
            <td>
                <asp:FileUpload ID="txtInput" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <span>*** หมายเหตุ : </span>
            </td>
            <td>
                ไฟล์ที่จะ Import จะต้องเป็นไฟล์ .xlsx เท่านั้น
            </td>
        </tr>  
    </table>
</asp:Content>
