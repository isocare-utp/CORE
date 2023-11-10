<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_upload_mem_pic.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_upload_mem_pic_ctrl.ws_mbshr_upload_mem_pic" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            return confirm("ต้องการบันทึกหรือไม่ ?");
        }
        var dsMain = new DataSourceTool();
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMembNo();
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessege" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table class="DataSourceFormView">
        <tr>
            <td width="20%">
                <div>
                    <span>รูปสมาชิก :</span>
                </div>
            </td>
            <td>
                <div>
                    <asp:FileUpload ID="UploadProfile" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>รูปลายเซ็นต์ :</span>
                </div>
            </td>
            <td>
                <div>
                    <asp:FileUpload ID="UploadSignature" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <span>รูปลายเซ็นต์บัญชีเงินฝาก :</span>
                </div>
            </td>
            <td width="100%">
                <div>
                                
                    <asp:FileUpload ID="UploadDept" runat="server" style="width:280px"/><br/>
                    <asp:FileUpload ID="UploadDept_2" runat="server" style="width:280px"/>
                </div>
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="HdRowNumber" runat="server" />
     <asp:HiddenField ID="HdColumnName" runat="server" />
</asp:Content>
